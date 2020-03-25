using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Membership
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
    }
}
