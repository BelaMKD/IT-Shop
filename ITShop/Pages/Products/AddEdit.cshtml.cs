using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.Mime.MediaTypeNames;

namespace ITShop
{
    public class AddEditModel : PageModel
    {
        private readonly IProductData productData;
        private readonly ICategoryData categoryData;

        [BindProperty]
        public Product Product { get; set; }
        public List<SelectListItem> CategoryTypes { get; set; }
        [BindProperty]
        public string ImgPath { get; set; }
        public AddEditModel(IProductData productData, ICategoryData categoryData)
        {
            this.productData = productData;
            this.categoryData = categoryData;
        }

    public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Product = productData.GetProductById(id.Value);
                if (Product==null)
                {
                    return RedirectToPage("./List");
                }
                Product.Discount = Product.Discount * 100;
                ImgPath = Product.ImagePath;
            }
            else
            {
                Product = new Product();
            }
            CategoryTypes = categoryData.GetCategories().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Type
            }).ToList();
            return Page();
        }
        public IActionResult OnPost(IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var category = categoryData.GetCategoryById(Product.CategoryId.Value);
                Product.Category = category;
                Product.Discount = Product.Discount / 100;
                Product.TotalPrice = Product.Price * (1 - Product.Discount);
                if (file != null)
                {
                    string uploadsFolder = Path.Combine(@"wwwroot/images/", $"{Product.Category.Type}/", $"{Product.Name}.jpg");
                    string imagePath = Path.Combine("~/images/", $"{Product.Category.Type}/", $"{Product.Name}.jpg");
                    using (var stream = new FileStream(uploadsFolder, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        stream.Close();

                    }
                    Product.ImagePath = imagePath;
                }
                if (Product.Id==0)
                {
                    Product.DateAdded = DateTime.Now;
                    productData.AddProduct(Product);
                    TempData["Message"] = "Product is created";
                }
                else
                {
                    productData.UpdateProduct(Product);
                    TempData["Message"] = "Product is updated";
                }
                productData.Commit();
                return RedirectToPage("./List");
            }
            CategoryTypes = categoryData.GetCategories().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Type
            }).ToList();
            return Page();
        }
    }
}