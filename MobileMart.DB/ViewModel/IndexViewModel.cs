using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
  public  class IndexViewModel
    {
        public int ProductID { get; set; }
        public string Category { get; set; }
        public string Company { get; set; }
        public string Color { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }

        public int OwnerID { get; set; }
        public string OwnerName { get; set; }

        public int ShopID { get; set; }
        public string ShopName { get; set; }
    }
}
