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
    public class LoginRegiseterViewModel
    {
        //public CustomerRegisterViewModel RegisterViewModel { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }
    public class CustomerRegisterViewModel
    {
        public LoginRegiseterViewModel CustomerLoginViewModel { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Profile Photo is Required")]
        [MinimumFileSizeValidator(0.0001)]
        [MaximumFileSizeValidator(3.0)]
        public HttpPostedFileBase ProfilePicture { get; set; }

        [Required(ErrorMessage = "First name is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is Required")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^((\03-?)|0)?[0-9]{10}$", ErrorMessage = "Not a valid Phone number")]
        public string Mobile { get; set; }

        public string AspNetUserID { get; set; }

        [Required(ErrorMessage = "Country is Required")]
        [Display(Name = "Country")]
        public int? Country { get; set; }

        [Required(ErrorMessage = "State is Required")]
        [Display(Name = "Province")]
        public int? State { get; set; }

        [Required(ErrorMessage = "City is Required")]
        [Display(Name = "City")]
        public int? City { get; set; }
        //public string CustomerConfirmation { get; set; }
    }
}
