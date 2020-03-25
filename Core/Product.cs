using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core
{
    [JsonObject(IsReference = true)]
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }

        [Required, Display(Name = ("Discount %"))]
        public double Discount { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        public string ImagePath { get; set; }
        [Required, Display(Name = "Category Type")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
