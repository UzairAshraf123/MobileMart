using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class DisplayProductViewModel
    {
        public int ProductID { get; set; }
        public string Category { get; set; }
        public string Company { get; set; }
        public string Color { get; set; }
        public string ProductName { get; set; }
        public string ProductImage1 { get; set; }
        public string ProductImage2 { get; set; }
        public string ProductImage3 { get; set; }
        public string ProductImage4 { get; set; }
        public string ProductDetail { get; set; }
        public decimal price { get; set; }
        public string IMEI { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
