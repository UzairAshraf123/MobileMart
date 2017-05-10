using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;
namespace MobileMart.DB.ViewModel
{
    public class AdminLayoutViewModel
    {
        public int CustomerCount { get; set; }
        public int ShopCount { get; set; }
        public int ProductCount { get; set; }
        public int OrderCount { get; set; }
    }
}
