using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITShop.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        public int ShoppingCartItems { get; set; }

        public IViewComponentResult Invoke()
        {
            if (HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems") != null)
            {
                var cartItems = HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("CartItems").ToList();
                ShoppingCartItems = cartItems.Count();
            }
            return View(ShoppingCartItems);
        }
    }
}
