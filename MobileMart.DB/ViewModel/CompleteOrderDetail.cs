using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class CompleteOrderDetail
    {
        public int? OrderID { get; set; }

        public int? CustomerID { get; set; }

        public string OrderBy { get; set; }

        public string paymentMethod { get; set; }

        public DateTime? CreatedOn { get; set; }

        public decimal? Tax { get; set; }

        public decimal? Shipping { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? Total { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; }

        public int OrderDetailID { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }
    }
}
