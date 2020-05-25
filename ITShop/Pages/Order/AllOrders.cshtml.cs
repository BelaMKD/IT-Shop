using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITShop.Pages.Order
{
    public class AllOrdersModel : PageModel
    {
        private readonly IOrderData orderData;
        public IEnumerable<Core.Order> Orders { get; set; }
        public AllOrdersModel(IOrderData orderData)
        {
            this.orderData = orderData;
        }
        public void OnGet()
        {
            Orders = orderData.GetAllOrders();
        }
    }
}