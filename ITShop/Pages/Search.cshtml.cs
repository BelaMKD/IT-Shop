using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ITShop.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IProductData productData;
        private readonly ICategoryData categoryData;

        public IEnumerable<Product> Products { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public Category Category { get; set; }
        public string Search { get; set; }
        public SearchModel(IProductData productData, ICategoryData categoryData)
        {
            this.productData = productData;
            this.categoryData = categoryData;
        }
        public void OnGet(string search)
        {
            Search = search;
            Products = productData.GetProducts(search);
            Categories = categoryData.GetCategories().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Type.ToUpper()
            }).ToList();
        }
        public IActionResult OnPost(int categoryId)
        {
            if (ModelState.IsValid)
            {
                Category = categoryData.GetCategoryById(categoryId);
                Products = Category.Products;
                Categories = categoryData.GetCategories().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Type.ToUpper()
                }).ToList();
                return Page();
            }
            return RedirectToPage("/Search");
        }
        public int CalculateProgressBar(int quantity)
        {
            var maxQuantity = 50;
            var percent = (double)quantity / maxQuantity;

            return Convert.ToInt32(percent * 100);
        }
    }
}