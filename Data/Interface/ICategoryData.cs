using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interface
{
    public interface ICategoryData
    {
        Category GetCategoryById(int id);
        IEnumerable<Category> GetCategories();
    }
}
