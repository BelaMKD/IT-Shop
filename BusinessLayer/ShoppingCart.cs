using Core;
using System;

namespace BusinessLayer
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
