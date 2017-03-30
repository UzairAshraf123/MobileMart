using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MobileMart.DB.ViewModel
{
    public class EditShopViewModel
    {
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

        public string UserID { get; set; }

        public int OwnerID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string OwnerName { get; set; }
        [Required]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Required]
        [Display(Name = "Profile Picture")]
        public HttpPostedFileBase ProfilePhoto { get; set; }

        public string ProfilePhotoPath { get; set; }

        [Required]
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
    }
}
