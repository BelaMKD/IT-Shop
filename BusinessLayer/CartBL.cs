using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class CartBL
    {
        public double TotalPrice(List<ShoppingCart> cartItems)
        {
            var total = 0.0;
            foreach (var cartItem in cartItems)
            {
                total += cartItem.Quantity * cartItem.Product.Price;
            }
            return total;
        }
        public ShoppingCart AddCartItem(Product product)
        {
            return new ShoppingCart
            {
                Id = product.Id,
                Product = product,
                Quantity = 1
            };
        }
        public ShoppingCart PlusCartItem(ShoppingCart cartItem, int qty = 1)
        {
            return PlusOrMinusCartItem(cartItem, qty);
        }
        public ShoppingCart MinusCartItem(ShoppingCart cartItem, int qty = -1)
        {
            return PlusOrMinusCartItem(cartItem, qty);
        }
        public ShoppingCart PlusOrMinusCartItem(ShoppingCart cartItem, int qty)
        {
            var tempShopCartItem = new ShoppingCart
            {
                Id = cartItem.Id,
                Product = cartItem.Product,
                Quantity = cartItem.Quantity
            };

            tempShopCartItem.Quantity += qty;
            if (tempShopCartItem.Quantity <= 0)
            {
                return null;
            }
            if (tempShopCartItem.Quantity > cartItem.Product.Quantity)
            {
                tempShopCartItem.Quantity -= 1;
            }

            return tempShopCartItem;

        }
    }
}
