using Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Data
{
    public class ITShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public ITShopDbContext(DbContextOptions<ITShopDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Type = "Mobile"
                },
                new Category
                {
                    Id = 2,
                    Type = "TV"
                },
                new Category
                {
                    Id = 3,
                    Type = "Audio"
                }
            });
            modelBuilder.Entity<Membership>().HasData(new List<Membership>
            {
                new Membership
                {
                    Id = 1,
                    Type = "Bronze",
                    Discount = 0.05,
                    Price = 25
                },
                new Membership
                {
                    Id = 2,
                    Type = "Silver",
                    Discount = 0.1,
                    Price = 35
                },
                new Membership
                {
                    Id = 3,
                    Type = "Gold",
                    Discount = 0.15,
                    Price = 45
                }
            });
        }
    }
}
