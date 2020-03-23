using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IEnumerable<Product> Products { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IProductData productData)
        {
            _logger = logger;
            this.productData = productData;
        }

        public void OnGet()
        {
            Products = productData.GetFirtsFiveNewProducts();
        }
    }
}
