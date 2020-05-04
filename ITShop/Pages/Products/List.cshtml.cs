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
        public string CanNotDelete { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public ListModel(IProductData productData)
        {
            this.productData = productData;
        }
        public void OnGet()
        {
            Products = productData.GetProducts();
        }
        public IActionResult OnPostDelete(int id)
        {
            Product = productData.DeleteProduct(id);
            if (Product == null)
            {
                return RedirectToPage("./List");
            }
            if (Product.OrderDetails.Count != 0)
            {
                TempData["Message"] = "This product cannot be deleted, because he is ordered by some customer!";
                return RedirectToPage("./List");
            }
            TempData["Message"] = "Product is deleted";
            productData.Commit();
            return RedirectToPage("./List");
        }
    }
}