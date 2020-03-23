using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITShop
{
    public class OrdersModel : PageModel
    {
        private readonly IOrderData orderData;
        private readonly UserManager<IdentityUser> userManager;

        public IEnumerable<Order> Order { get; set; }
        public OrdersModel(IOrderData orderData, UserManager<IdentityUser> userManager)
        {
            this.orderData = orderData;
            this.userManager = userManager;
        }
        public void OnGet()
        {
            Order = orderData.GetOrdersByUserId(userManager.GetUserId(User));
        }
    } 
}