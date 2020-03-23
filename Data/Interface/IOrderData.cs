using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interface
{
    public interface IOrderData
    {
        IEnumerable<Order> GetOrdersByUserId(string id);
        Order CreateOrder(Order order);
        IEnumerable<Order> GetAllOrders();
        int Commit();
    }
}
