using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class AddOrderViewModel
    {
        public int CustomerID { get; set; }
        public DateTime CreatedOn { get; set; }
        public int PaymentID { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public decimal Shipping { get; set; }
        public string PayPalReference { get; set; }
        public decimal SubTotal { get; set; }
    }
}
