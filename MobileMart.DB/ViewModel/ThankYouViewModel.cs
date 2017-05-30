using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class ThankYouViewModel
    {
        public IEnumerable<DisplayOrderDetailViewModel> OrderDetail { get; set; }
        public AddOrderViewModel Order { get; set; }
    }
}
