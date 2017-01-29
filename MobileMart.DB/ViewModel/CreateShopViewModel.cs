using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class CreateShopViewModel
    {
        public int OwnerID { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public string ShopLogo { get; set; }
        public DateTime CreatedOn { get; set; }
        
    }
}
