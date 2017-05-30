using MobileMart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class ShopWithProductsViewModel
    {
        public ShopDetailViewModel ShopDetail { get; set; }
        public IEnumerable<DisplayProductViewModel> ProductDetail { get; set; }
        public IEnumerable<DisplaySupplierViewModel> SupplierDetail { get; set; }
        public Pager Pager { get; set; }

    }
}
