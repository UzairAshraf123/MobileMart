using MobileMart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class AllProductsViewModel
    {
        public IEnumerable<IndexViewModel> ProductsList { get; set; }
        public Pager Pager { get; set; }
    }
}
