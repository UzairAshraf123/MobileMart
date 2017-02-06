using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MobileMart.DB.ViewModel
{
   public class AddProductViewModel
    {
        public int id { get; set; }
        public int Category { get; set; }
        public int Company { get; set; }
        public int Color { get; set; }
        public string ProductName { get; set; }
        public HttpPostedFileBase ProductImage { get; set; }
        public string ProductImagePath { get; set; }
        public string ProductDetail { get; set; }
    }
}
