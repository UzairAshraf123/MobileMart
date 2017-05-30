using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class OrderViewModel
    {
        public DisplayOrderViewModel Order { get; set; }
        public IEnumerable<DisplayOrderDetailViewModel> OrderDetail { get; set; }
    }
    public class DisplayOrderViewModel
    {
        public int OrderID { get; set; }

        [Display(Name = "Customer")]
        public string CustomerName { get; set; }

        [Display(Name = "Created")]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Tax")]
        public decimal? Tax { get; set; }

        [Display(Name = "Total")]
        public decimal? Total { get; set; }

        [Display(Name = "Shipping")]
        public decimal? Shipping { get; set; }

        [Display(Name = "SubTotal")]
        public decimal? SubTotal { get; set; }

        [Display(Name = "Reference ID")]
        public string PayPalReference { get; set; }
    }
    public class DisplayOrderDetailViewModel
    {
        public int? OrderDetailID { get; set; }
        public int? OrderID { get; set; }
        public string Product { get; set; }
        public int? Quantity { get; set; }
        public Decimal? UnitPrice { get; set; }
    }
}
