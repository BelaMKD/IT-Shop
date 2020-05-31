using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;

namespace ITShop.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly IOrderData orderData;
        private readonly IOrderDetailData orderDetailData;
        private readonly IProductData productData;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMembershipData membershipData;

        public double TotalEarnings { get; set; }
        public int TotalOrders { get; set; }
        public int ProductSold { get; set; }
        public int NumberOfUsers { get; set; }
        public ProgressBar ProgressBarMetod { get; set; }
        public List<StatisticsCore> ByDate { get; set; }
        public List<StatisticsCore> ByCategory { get; set; }
        public List<StatisticsCore> ProductQuantity { get; set; }
        public List<StatisticsCore> UserMemberships { get; set; }
        public IEnumerable<StatisticsCore> Top3 { get; set; }
        public IEnumerable<Product> ProductsWithLowestQuantity { get; set; }
        public DashboardModel(IOrderData orderData, IOrderDetailData orderDetailData, IProductData productData, UserManager<ApplicationUser> userManager, IMembershipData membershipData)
        {
            this.orderData = orderData;
            this.orderDetailData = orderDetailData;
            this.productData = productData;
            this.userManager = userManager;
            this.membershipData = membershipData;
            ByDate = new List<StatisticsCore>();
            ByCategory = new List<StatisticsCore>();
            UserMemberships = new List<StatisticsCore>();
            ProductQuantity = new List<StatisticsCore>();
        }
        public void OnGet()
        {
            TotalEarnings = orderData.GetAllOrders().Sum(x => x.OrderTotal);
            TotalOrders = orderData.GetAllOrders().Count();
            ProductSold = orderDetailData.GetOrderDetails().Sum(x => x.Quantity);
            NumberOfUsers = userManager.Users.Count();
            ProgressBarMetod = new ProgressBar();
            ProductsWithLowestQuantity = productData.GetFirstFiveProductsWithLowestQuantity();

            var byDate = orderData.GetAllOrders().GroupBy(x => x.DateOrdered);
            foreach (var item in byDate)
            {
                var x = 0;
                foreach (var oD in item)
                {
                    x += oD.OrderDetails.Sum(x => x.Quantity);
                }
                ByDate.Add(new StatisticsCore
                {
                    DateTime = item.Key,
                    NumberOfProducts = x
                });
            }

            var proQuantity = orderData.GetAllOrders().GroupBy(x => x.OrderDetails);
            List<StatisticsCore> productQuantity = new List<StatisticsCore>();
            foreach (var item in proQuantity)
            {
                foreach (var item1 in item.Key)
                {
                    productQuantity.Add(new StatisticsCore
                    {
                        ProductName = item1.Product.Name,
                        NumberOfProducts = item1.Quantity
                    });
                }
            }
            var tempPQ = productQuantity.GroupBy(x=>x.ProductName);
            foreach (var item in tempPQ)
            {
                ProductQuantity.Add(new StatisticsCore
                {
                    ProductName = item.Key,
                    NumberOfProducts = item.Sum(x => x.NumberOfProducts)
                }); ;
            }

            Top3 = ProductQuantity.OrderByDescending(x => x.NumberOfProducts).Take(3);

            List<StatisticsCore> categoryQuantity = new List<StatisticsCore>();
            foreach (var item in proQuantity)
            {
                foreach (var item1 in item.Key)
                {
                    categoryQuantity.Add(new StatisticsCore
                    {
                        CategoryName = item1.Product.Category.Type,
                        NumberOfProducts = item1.Quantity
                    });
                }
            }
            var tempCQ = categoryQuantity.GroupBy(x => x.CategoryName);
            foreach (var item in tempCQ)
            {
                ByCategory.Add(new StatisticsCore
                {
                    CategoryName = item.Key,
                    NumberOfProducts = item.Sum(x => x.NumberOfProducts)
                }); ;
            }


            var tempMemberships = userManager.Users.Include(x=>x.Membership);

            List<StatisticsCore> tempUserMember = new List<StatisticsCore>();

            foreach (var item in tempMemberships)
            {
                if (item.Membership!=null)
                {
                    tempUserMember.Add(new StatisticsCore
                    {
                        MembershipType = item.Membership.Type,
                        NumberOfUsers = 1
                    });
                }
            }
            

            foreach (var item in tempUserMember.GroupBy(x=>x.MembershipType))
            {
                    UserMemberships.Add(new StatisticsCore
                    {
                        MembershipType = item.Key,
                        NumberOfUsers = item.Sum(x=>x.NumberOfUsers)
                    });
               
            }
        }
    }
}