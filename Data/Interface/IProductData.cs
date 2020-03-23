using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interface
{
    public interface IProductData
    {
        Product GetProductById(int id);
        IEnumerable<Product> GetProducts(string name=null);
        IEnumerable<Product> GetFirtsFiveNewProducts();
        Product AddProduct(Product product);
        Product UpdateProduct(Product product);
        Product DeleteProduct(int id);
        int Commit();
    }
}
