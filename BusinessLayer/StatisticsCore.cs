using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLayer
{
    public class StatisticsCore
    {
        public DateTime DateTime { get; set; }
        public int NumberOfProducts { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string MembershipType { get; set; }
        public int NumberOfUsers { get; set; }
    }
}
