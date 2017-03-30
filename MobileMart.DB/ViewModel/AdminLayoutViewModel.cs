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
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Shop> Shops { get; set; }
    }
}
