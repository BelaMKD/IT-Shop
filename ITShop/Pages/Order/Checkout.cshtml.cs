using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLayer;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITShop
{
    public class CheckoutModel : PageModel
    {
        private readonly IOrderData orderData;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductData productData;
        private readonly IMembershipData membershipData;
        private readonly CartBL cartBL;

        [BindProperty]
        public List<Membership> Memberships { get; set; }
        [BindProperty]
        public string Selected { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        [BindProperty]
        public Order Order { get; set; }
        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }
        public CheckoutModel(IOrderData orderData, UserManager<ApplicationUser> userManager, IProductData productData, IMembershipData membershipData, CartBL cartBL)
        {
            this.orderData = orderData;
            this.userManager = userManager;
            this.productData = productData;
            this.membershipData = membershipData;
            this.cartBL = cartBL;
            OrderDetails = new List<OrderDetail>();
        }
        public async Task<IActionResult> OnGet()
        {
            Selected = string.Empty;
            Memberships = membershipData.GetMemberships();
            Order = new Order();
            Order.OrderTotal = Convert.ToDouble(HttpContext.Session.GetString("TotalPrice"));
            ApplicationUser = await userManager.GetUserAsync(User);
            return Page();
        } 
        public async Task<IActionResult> OnPost()
        {
            ApplicationUser = await userManager.GetUserAsync(User);
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

                if (Selected!=null)
                {
                    var membership = membershipData.GetMembershipById(Convert.ToInt32(Selected));
                    ApplicationUser.Membership = membership;
                    Order.OrderTotal = cartBL.MemberOnCheckout(Convert.ToDouble(HttpContext.Session.GetString("TotalPrice")), ApplicationUser);
                }
                else
                {
                    Order.OrderTotal = Convert.ToDouble(HttpContext.Session.GetString("TotalPrice"));
                }
                Order.OrderDetails = OrderDetails;
                Order.UserId = ApplicationUser.Id;
                orderData.CreateOrder(Order);
                orderData.Commit();
                return RedirectToPage("/Order/CheckoutComplete");
            }
            return Page();
        }
    }
}