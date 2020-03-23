using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ITShop
{
    public class ShoppingCartModel : PageModel
    {
        private readonly IProductData productData;
        private readonly CartBL cartBL;
        public List<ShoppingCart> CartItems { get; set; }
        [TempData]
        public string Message { get; set; }
        public double TotalPrice { get; set; }
        public ShoppingCartModel(IProductData productData, CartBL cartBL)
        {
            this.productData = productData;
            this.cartBL = cartBL;
            CartItems = new List<ShoppingCart>();
        }
        public void OnGet()
        {
            if (HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems") != null)
            {
                CartItems = HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems").ToList();
                TotalPrice = cartBL.TotalPrice(CartItems);
            }
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