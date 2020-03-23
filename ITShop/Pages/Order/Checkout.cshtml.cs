using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLayer;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITShop
{
    public class CheckoutModel : PageModel
    {
        private readonly IOrderData orderData;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IProductData productData;

        public List<OrderDetail> OrderDetails { get; set; }
        [BindProperty]
        public Order Order { get; set; }
        public CheckoutModel(IOrderData orderData, UserManager<IdentityUser> userManager, IProductData productData)
        {
            this.orderData = orderData;
            this.userManager = userManager;
            this.productData = productData;
            OrderDetails = new List<OrderDetail>();
        }
        public IActionResult OnGet()
        {
            Order = new Order();

            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                foreach (var cartItem in HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems").ToList())
                {
                    OrderDetails.Add(new OrderDetail
                    {
                        Product = productData.GetProductById(cartItem.Id),
                        Quantity = cartItem.Quantity
                    });
                }
                Order.OrderDetails = OrderDetails;
                Order.UserId = userManager.GetUserId(User);
                orderData.CreateOrder(Order);
                orderData.Commit();
                return RedirectToPage("/Order/CheckoutComplete");
            }
            return Page();
        }
    }
}