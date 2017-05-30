using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class ShopDetailViewModel
    {
        public int ShopID { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Logo { get; set; }
        public DateTime CreatedON { get; set; }

        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }
        
        public string Mobile { get; set; }
    }
}
