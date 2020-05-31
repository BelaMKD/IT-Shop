using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interface
{
    public interface IOrderDetailData
    {
        IEnumerable<OrderDetail> GetOrderDetails();
    }
}
