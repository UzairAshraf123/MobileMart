using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class DisplayShopViewModel
    {
        public int OwnerID { get; set; }
        public string OwnerName { get; set; }
        public string Profile { get; set; }
        public string Contact { get; set; }
        public DateTime? OwnerCreatedOn { get; set; }

        public int ShopID { get; set; }
        public string ShopName { get; set; }
        public string ShopLogo { get; set; }
        public string ShopAddress { get; set; }
        public DateTime? ShopCreatedOn { get; set; }


    }
}


