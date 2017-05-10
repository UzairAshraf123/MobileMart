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
        public IEnumerable<ProductDetailViewModel> ProductDetail { get; set; }
        public IEnumerable<DisplaySupplierViewModel> SupplierDetail { get; set; }
    }
}
