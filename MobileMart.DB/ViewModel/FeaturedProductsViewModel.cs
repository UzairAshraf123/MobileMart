using MobileMart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class FeaturedProductsViewModel
    {
        public IEnumerable<IndexViewModel> Products { get; set; }
        public Pager Pager { get; set; }
    }
}
