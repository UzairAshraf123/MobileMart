using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
 public class ProductDetailViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ShopName { get; set; }
        public string ProductDetail { get; set; }
        public string ProductImage1 { get; set; }
        public string ProductImage2 { get; set; }
        public string ProductImage3 { get; set; }
        public string ProductImage4 { get; set; }
        public string Color { get; set; }
        public string Company { get; set; }
        public Shop ProductShop { get; set; }
    }
}
