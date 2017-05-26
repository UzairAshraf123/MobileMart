using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MobileMart.DB.ViewModel
{
    public class DisplayShopViewModel
    {
        public string UserID { get; set; }
        public int OwnerID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }
        [Required]
        [Display(Name = "Profile")]
        public string OwnerProfilePath { get; set; }

        [Display(Name = "Profile Picture")]
        public HttpPostedFileBase OwnerProfilePhoto { get; set; }

        [Required]
        [Display(Name = "Contact")]
        public string Contact { get; set; }
        [Required]
        [Display(Name = "Created")]
        public DateTime? OwnerCreatedOn { get; set; }

        public int ShopID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string ShopName { get; set; }
        [Required]
        [Display(Name = "Logo")]
        public string ShopLogo { get; set; }

        [Display(Name = "Profile Picture")]
        public HttpPostedFileBase ShopImage { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string ShopAddress { get; set; }
        [Required]
        [Display(Name = "Created")]
        public DateTime? ShopCreatedOn { get; set; }

        [Display(Name = "Products")]
        public int productcount { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int? Country { get; set; }
        [Required]
        [Display(Name = "State")]
        public int? State { get; set; }
        [Required]
        [Display(Name = "City")]
        public int? City { get; set; }
    }
}


