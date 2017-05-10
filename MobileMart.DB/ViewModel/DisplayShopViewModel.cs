using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class DisplayShopViewModel
    {
        public string UserID { get; set; }
        public int OwnerID { get; set; }
        [Required]
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }
        [Required]
        [Display(Name = "Profile")]
        public string OwnerProfile { get; set; }
        [Required]
        [Display(Name = "Contact")]
        public string Contact { get; set; }
        [Required]
        [Display(Name = "Created")]
        public DateTime? OwnerCreatedOn { get; set; }

        public int ShopID { get; set; }
        [Required]
        [Display(Name = "Shop Name")]
        public string ShopName { get; set; }
        [Required]
        [Display(Name = "Shop Logo")]
        public string ShopLogo { get; set; }
        [Required]
        [Display(Name = "Shop Address")]
        public string ShopAddress { get; set; }
        [Required]
        [Display(Name = "Created")]
        public DateTime? ShopCreatedOn { get; set; }

        [Display(Name = "Products")]
        public int productcount { get; set; }
    }
}


