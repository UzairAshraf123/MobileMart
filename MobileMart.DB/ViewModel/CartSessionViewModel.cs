using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class CartSessionViewModel
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public ProductDetailViewModel ProductDetail;
    }
}
