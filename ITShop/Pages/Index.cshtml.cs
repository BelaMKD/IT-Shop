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
        private readonly IMembershipData membershipData;

        public IEnumerable<Product> NewProducts { get; set; }
        public IEnumerable<Product> DiscountedProducts { get; set; }
        public List<Membership> Memberships { get; set; }
        public ProgressBar ProgressBarMetod { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IProductData productData, IMembershipData membershipData)
        {
            _logger = logger;
            this.productData = productData;
            this.membershipData = membershipData;
        }

        public void OnGet()
        {
            NewProducts = productData.GetFirtsFiveNewProducts();
            DiscountedProducts = productData.GetFirstFiveDiscountedProducts();
            ProgressBarMetod = new ProgressBar();
            Memberships = membershipData.GetMemberships();
        }
    }
}
