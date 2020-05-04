using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITShop.Pages.Products
{
    public class DetailModel : PageModel
    {
        private readonly IProductData productData;
        public Product Product { get; set; }
        public DetailModel(IProductData productData)
        {
            this.productData = productData;
        }
        public void OnGet(int id)
        {
            Product = productData.GetProductById(id);
        }
    }
}