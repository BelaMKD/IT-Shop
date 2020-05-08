using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITShop
{
    public class DeleteModel : PageModel
    {
        private readonly IProductData productData;
        private readonly ICategoryData categoryData;

        [BindProperty]
        public Product Product { get; set; }
        public DeleteModel(IProductData productData, ICategoryData categoryData)
        {
            this.productData = productData;
            this.categoryData = categoryData;
        }
        public IActionResult OnGet(int id)
        {
            Product = productData.GetProductById(id);
            if (Product == null)
            {
                return RedirectToPage("./List");
            }
            return Page();
        } 
        public IActionResult OnPost(int id)
        {
            //var category = categoryData.GetCategoryById(Product.CategoryId.Value);
            //Product.Category = category;



            Product = productData.DeleteProduct(id);

            var path = Path.Combine(@"wwwroot/images/", $"{Product.Category.Type}/", $"{Product.Name}.jpg");
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            if (Product==null)
            {
                return RedirectToPage("./List");
            }
            TempData["Message"] = "Product is deleted";
            productData.Commit();
            return RedirectToPage("./List");
        }
    }
}