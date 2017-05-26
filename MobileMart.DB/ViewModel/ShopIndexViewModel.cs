using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class ShopIndexViewModel
    {
        public int NewOrdersCount { get; set; }

        public int AllOrdersCount { get; set; }

        public int NewProductsCount { get; set; }

        public int AllProductsCount { get; set; }

        public int AllSuppliersCount { get; set; }

        public IEnumerable<DisplayOrderViewModel> Orders { get; set; }

        public IEnumerable<DisplayOrderViewModel> TotalOrders { get; set; }

        public decimal? TotalSale { get; set; }


    }

    public class AdminIndexViewModel
    {
        public int NewOrdersCount { get; set; }

        public int AllOrdersCount { get; set; }

        public int NewProductsCount { get; set; }

        public int AllProductsCount { get; set; }

        public int NewCustomersCount { get; set; }

        public int AllCustomersCount { get; set; }

        public int NewShopsCount { get; set; }

        public int AllShopsCount { get; set; }

        public int AllSuppliersCount { get; set; }

        public IEnumerable<CustomerDetailViewModel> NewCustomers { get; set; }

        public IEnumerable<DisplayOrderViewModel> Orders { get; set; }

        public IEnumerable<DisplayOrderViewModel> TotalOrders { get; set; }

        public decimal? TotalSale { get; set; }


    }
}
