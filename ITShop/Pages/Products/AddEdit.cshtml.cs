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

namespace ITShop
{
    public class AddEditModel : PageModel
    {
        private readonly IProductData productData;
        private readonly ICategoryData categoryData;

        [BindProperty]
        public Product Product { get; set; }
        public List<SelectListItem> CategoryTypes { get; set; }
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
                if (Product!=null)
                {
                    return RedirectToPage("./List");
                }
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
                if (Product.Id==0)
                {
                    if (file != null)
                    {
                        string uploadsFolder = Path.Combine(@"wwwroot/images/", $"{Product.Category.Type}/", $"{Product.Name}.jpg");
                        string imagePath = Path.Combine("~/images/", $"{Product.Category.Type}/", $"{Product.Name}.jpg");
                        file.CopyTo(new FileStream(uploadsFolder, FileMode.Create));
                        Product.ImagePath = imagePath;
                    }
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