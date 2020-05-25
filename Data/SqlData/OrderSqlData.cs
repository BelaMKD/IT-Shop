using Core;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.SqlData
{
    public class OrderSqlData : IOrderData
    {
        private readonly ITShopDbContext dbContext;

        public OrderSqlData(ITShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Order CreateOrder(Order order)
        {
            dbContext.Orders.Add(order);
            return order;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return dbContext.Orders
                .Include(x => x.OrderDetails)
                .ThenInclude(z => z.Product)
                .Include(x => x.User)
                .OrderBy(x => x.User.Email)
                .ToList();
                
        }

        public IEnumerable<Order> GetOrdersByUserId(string userId)
        {
            return dbContext.Orders
                .Where(x => x.UserId == userId)
                .Include(x => x.OrderDetails)
                .ThenInclude(z => z.Product)
                .ToList();
        }
    }
}
