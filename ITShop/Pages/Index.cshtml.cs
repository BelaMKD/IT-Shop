using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ITShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductData productData;

        public IEnumerable<Product> NewProducts { get; set; }
        public IEnumerable<Product> DiscountedProducts { get; set; }
        public ProgressBar ProgressBarMetod { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IProductData productData)
        {
            _logger = logger;
            this.productData = productData;
        }

        public void OnGet()
        {
            NewProducts = productData.GetFirtsFiveNewProducts();
            DiscountedProducts = productData.GetFirstFiveDiscountedProducts();
            ProgressBarMetod = new ProgressBar();
        }
        public int CalculateProgressBar(int quantity)
        {
            var maxQuantity = 50;
            var percent = (double) quantity / maxQuantity;

            return Convert.ToInt32(percent * 100);
        }
    }
}
