using Core;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Data.SqlData
{
    public class CategorySqlData : ICategoryData
    {
        private readonly ITShopDbContext dbContext;

        public CategorySqlData(ITShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Category> GetCategories()
        {
            return dbContext.Categories
                .Include(x => x.Products)
                .ToList();
        }

        public Category GetCategoryById(int id)
        {
            return dbContext.Categories
                .Include(x=>x.Products)
                .SingleOrDefault(x => x.Id == id);
        }
    }
}
