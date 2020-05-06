using Core;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.SqlData
{
    public class ProductSqlData : IProductData
    {
        private readonly ITShopDbContext dbContext;

        public ProductSqlData(ITShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Product AddProduct(Product product)
        {
            dbContext.Products.Add(product);
            return product;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Product DeleteProduct(int id)
        {
            var tempProduct = dbContext.Products
                .Include(x=>x.Category)
                .Include(x=>x.OrderDetails)
                .SingleOrDefault(x => x.Id == id);
            if (tempProduct != null)
            {
                dbContext.Products.Remove(tempProduct);
            }
            return tempProduct;
        }

        public Product GetProductById(int id)
        {
            return dbContext.Products
                .Include(x=>x.Category)
                .Include(x=>x.OrderDetails)
                .SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Product> GetProducts(string name = null)
        {
            return dbContext.Products
                .Where(x => string.IsNullOrEmpty(name) || x.Name.StartsWith(name) || x.Category.Type.StartsWith(name))
                .Include(x=>x.Category)
                .ToList();
        } 

        public Product UpdateProduct(Product product)
        {
            dbContext.Entry(product).State = EntityState.Modified;
            return product;
        }
        public IEnumerable<Product> GetFirtsFiveNewProducts()
        {
            return dbContext.Products
                .OrderByDescending(x => x.DateAdded)
                .Take(8)
                .ToList();
        }
        public IEnumerable<Product> GetFirstFiveDiscountedProducts()
        {
            return dbContext.Products
                .Where(x => x.Discount != 0)
                .Take(4)
                .ToList();
        }
    }
}
