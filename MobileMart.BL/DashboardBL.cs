using MobileMart.DB.ViewModel;
using MobileMart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.BL
{
    public class DashboardBL
    {
        public int GetNewOrders()
        {
            return new OrderRepository().Get().Where(s=> s.CreatedOn >= DateTime.Now.AddDays(-7)).Count();
        }
        public int GetAllOrdersCount()
        {
            return new OrderRepository().Get().Count();
        }
        public int GetNewProductsCount()
        {
            return new ProductRepository().Get().Where(s => s.CreatedOn >= DateTime.Now.AddDays(-7)).Count();
        }
        public int GetAllProductsCount()
        {
            return new ProductRepository().Get().Count();
        }

        public int GetNewCustomersCount()
        {
            return new CustomerRepository().Get().Where(s => s.CreatedOn >= DateTime.Now.AddDays(-7)).Count();
        }
        public int GetAllCustomersCount()
        {
            return new CustomerRepository().Get().Count();
        }
        public IEnumerable<CustomerDetailViewModel> GetNewCustomers()
        {
            return new CustomerRepository().Get().Take(20).OrderByDescending(s=> s.CreatedOn).Select(w=> new CustomerDetailViewModel
            {
                CustomerID = w.CustomerID,
                FirstName = w.FirstName,
                ProfilePicturePath = w.ProfilePicture,
            });
        }
        
        public int GetNewShopsCount()
        {
            return new ShopRepository().Get().Where(s => s.CreatedOn >= DateTime.Now.AddDays(-7)).Count();
        }
        public int GetAllShopsCount()
        {
            return new ShopRepository().Get().Count();
        }

        public IEnumerable<DisplayOrderViewModel> TodaysOrders()
        {
            return new OrderRepository().Get().Where(s => s.CreatedOn >= DateTime.Today).Select(w => new DisplayOrderViewModel
            {
                OrderID = w.OrderID,
                CustomerName = new CustomerRepository().GetCustomerByID(w.CustomerID).FirstName,
                CreatedOn = w.CreatedOn,
                Tax = w.Tax,
                Total = w.Total,
                Shipping = w.Shipping,
                SubTotal = w.SubTotal,
            });
        }

        public decimal? GetTotalSale()
        {
            var orders = new OrderRepository().Get();
            decimal? count = 0;
            foreach (var item in orders)
            {
                count = item.Total + count;
            }
            return count;
        }

        public IEnumerable<DisplayOrderViewModel> GetOrders()
        {
            var orderRepo = new OrderRepository();
            var customerRepo = new CustomerRepository();
            IEnumerable<DisplayOrderViewModel> orderList = orderRepo.Get().Select(s => new DisplayOrderViewModel
            {
                OrderID = s.OrderID,
                CustomerName = customerRepo.GetCustomerByID(s.CustomerID).FirstName,
                CreatedOn = s.CreatedOn,
                Tax = s.Tax,
                Total = s.Total,
                Shipping = s.Shipping,
                SubTotal = s.SubTotal,
            });
            return orderList;
        }
    }
}
