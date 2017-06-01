using MobileMart.DB.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MobileMart.DB.ViewModel
{
    public class CreateOwnerViewModel
    {
        [Required(ErrorMessage = "Profile Photo is required.")]
        [Display(Name = "Profile Picture")]
        [MinimumFileSizeValidator(0.0001)]
        [MaximumFileSizeValidator(3.0)]
        public HttpPostedFileBase ProfilePhotoPath { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name ="Owner Name")]
        public string OwnerName { get; set; }


        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^((\03-?)|0)?[0-9]{10}$", ErrorMessage = "Not a valid Phone number")]
        public string Mobile { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserID { get; set; }

        public int OwnerID { get; set; }
    }
}
