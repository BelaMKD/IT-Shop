using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITShop.Pages.Order
{
    [Authorize]
    public class MyOrdersModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrderData orderData;
        public IEnumerable<Core.Order> Orders { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string UserId { get; set; }
        public MyOrdersModel(UserManager<ApplicationUser> userManager, IOrderData orderData)
        {
            this.userManager = userManager;
            this.orderData = orderData;
        }
        public void OnGet()
        {
            Orders = orderData.GetOrdersByUserId(userManager.GetUserId(User));
        }
    }
}