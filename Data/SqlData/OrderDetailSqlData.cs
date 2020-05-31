using Core;
using Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.SqlData
{
    public class OrderDetailSqlData : IOrderDetailData
    {
        private readonly ITShopDbContext dbContext;

        public OrderDetailSqlData(ITShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<OrderDetail> GetOrderDetails()
        {
            return dbContext.OrderDetail
                .ToList();
        }
    }
}
