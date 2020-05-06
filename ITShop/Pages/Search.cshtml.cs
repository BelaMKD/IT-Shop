using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITShop.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IProductData productData;
        public IEnumerable<Product> Products { get; set; }
        public string Search { get; set; }
        public SearchModel(IProductData productData)
        {
            this.productData = productData;
        }
        public void OnGet(string search)
        {
            Search = search;
            Products = productData.GetProducts(search);
        }
        public int CalculateProgressBar(int quantity)
        {
            var maxQuantity = 50;
            var percent = (double)quantity / maxQuantity;

            return Convert.ToInt32(percent * 100);
        }
    }
}