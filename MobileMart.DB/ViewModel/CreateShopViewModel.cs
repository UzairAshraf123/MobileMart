using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MobileMart.DB.ViewModel
{
    public class CreateShopViewModel
    {
        public string UserID { get; set; }

        public int ShopID { get; set; }

        public int? OwnerID { get; set; }
        [Required]
        [Display(Name = "Shop Name")]
        public string ShopName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string ShopAddress { get; set; }
        [Required]
        [Display(Name = "Country")]
        public int? Country { get; set; }
        [Required]
        [Display(Name = "State")]
        public int? State { get; set; }
        [Required]
        [Display(Name = "City")]
        public int? City { get; set; }

        [Required(ErrorMessage = "Logo is required.")]
        [Display(Name = "Shop Logo")]
        public HttpPostedFileBase ShopLogo { get; set; }

        public DateTime CreatedOn { get; set; }

        public string GetLogo { get; set; }

    }
}
