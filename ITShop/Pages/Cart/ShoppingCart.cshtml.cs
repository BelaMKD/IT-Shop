using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ITShop
{
    public class ShoppingCartModel : PageModel
    {
        private readonly IProductData productData;
        private readonly CartBL cartBL;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMembershipData membershipData;

        public List<ShoppingCart> CartItems { get; set; }
        [TempData]
        public string Message { get; set; }
        public double TotalPrice { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ShoppingCartModel(IProductData productData, CartBL cartBL, UserManager<ApplicationUser> userManager, IMembershipData membershipData)
        {
            this.productData = productData;
            this.cartBL = cartBL;
            this.userManager = userManager;
            this.membershipData = membershipData;
            CartItems = new List<ShoppingCart>();
        }
        public async Task<IActionResult> OnGet()
        {
            //ApplicationUser = await userManager.GetUserAsync(User);
            //if (ApplicationUser != null)
            //{
            //    if (ApplicationUser.IsMember)
            //    {
            //        var membership = membershipData.GetMembershipById(ApplicationUser.MembershipId.Value);
            //        ApplicationUser.Membership = membership;
            //    }


            //    if (HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems") != null)
            //    {
            //        CartItems = HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems").ToList();


            //        if (ApplicationUser.IsMember)
            //        {

            //            TotalPrice = cartBL.TotalPrice(CartItems, ApplicationUser.Membership.Discount);
            //        }
            //        else
            //        {
            //            TotalPrice = cartBL.TotalPrice(CartItems, 0);
            //        }
            //    }

            //    HttpContext.Session.SetString("TotalPrice", TotalPrice.ToString());
            //}
            //CartItems = HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems").ToList();

            //return Page();


            if (HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems") != null)
            {
                CartItems = HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems").ToList();
            }

            ApplicationUser = await userManager.GetUserAsync(User);
            if (ApplicationUser != null)
            {
                if (ApplicationUser.IsMember)
                {
                    var membership = membershipData.GetMembershipById(ApplicationUser.MembershipId.Value);
                    ApplicationUser.Membership = membership;
                    TotalPrice = cartBL.TotalPrice(CartItems, ApplicationUser.Membership.Discount);
                }

            }
            TotalPrice = cartBL.TotalPrice(CartItems, 0);
            HttpContext.Session.SetString("TotalPrice", TotalPrice.ToString());

            CartItems = HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems").ToList();

            return Page();



        }
    
        public IActionResult OnGetBuy(int id)
         {
            var product = productData.GetProductById(id);
            if (HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems") != null)
            {
                CartItems = HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems").ToList();
                var productiInCart = CartItems.SingleOrDefault(x => x.Product.Id == id);

                if (CartItems.Contains(productiInCart))
                {
                    return RedirectToPage("./ShoppingCart");
                }
            }
            CartItems.Add(cartBL.AddCartItem(product));

            HttpContext.Session.SetObjectAsJson("CartItems", CartItems);
            CartItems = HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems").ToList();
            return RedirectToPage("./ShoppingCart");
            //return Page();
        }
        public IActionResult OnPostMinus(int id)
        {
            CartItems = HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems").ToList();
            var productiInCart = CartItems.SingleOrDefault(x => x.Product.Id == id);
            var cartItemMinus = cartBL.MinusCartItem(productiInCart);
            if (cartItemMinus == null)
            {
                CartItems.Remove(productiInCart);
            }
            else
            {
                CartItems
                .Where(x => x.Id == id)
                .Select(x => { x.Quantity = cartItemMinus.Quantity; return x; }).ToList();
            }
            HttpContext.Session.SetObjectAsJson("CartItems", CartItems);
            CartItems = HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems").ToList();
            return RedirectToPage("./ShoppingCart");
        }
        public IActionResult OnPostPlus(int id)
        {
            CartItems = HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems").ToList();

            var productiInCart = CartItems.SingleOrDefault(x => x.Product.Id == id);
            var cartItemPlus = cartBL.PlusCartItem(productiInCart);

            CartItems
                .Where(x => x.Id == id)
                .Select(x => { x.Quantity = cartItemPlus.Quantity; return x; }).ToList();
            if (cartItemPlus.Quantity == productiInCart.Product.Quantity)
            {
                TempData["Message"] = $"You have reached the limit for {productiInCart.Product.Name}!";
            }
            HttpContext.Session.SetObjectAsJson("CartItems", CartItems);
            CartItems = HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems").ToList();
            return RedirectToPage("./ShoppingCart");
        }
    }
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}