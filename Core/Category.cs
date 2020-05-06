using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core
{
    public class Category
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<Product> Products { get; set; }
    }
}
