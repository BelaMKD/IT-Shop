using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITShop
{
    public class ListModel : PageModel
    {
        private readonly IProductData productData;
        [TempData]
        public string Message { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public ListModel(IProductData productData)
        {
            this.productData = productData;
        }
        public void OnGet()
        {
            Products = productData.GetProducts();
        }
    }
}