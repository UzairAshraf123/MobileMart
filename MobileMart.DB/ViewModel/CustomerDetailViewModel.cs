using MobileMart.DB.CustomValidators;
using MobileMart.DB.Model;
using MobileMart.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MobileMart.DB.ViewModel
{
    public class CustomerDetailViewModel
    {
        public string AspNetUserID { get; set; }
        public int CustomerID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "DOB")]
        public DateTime? DOB { get; set; }

        [Display(Name = "Created On")]
        public DateTime? CreatedON { get; set; }

        [Display(Name = "Profile Photo")]
        public string ProfilePicturePath { get; set; }

        [Display(Name = "Profile Photo")]
        [MinimumFileSizeValidator(0.0001)]
        [MaximumFileSizeValidator(3.0)]
        public HttpPostedFileBase ProfilePicture { get; set; }

        [Display(Name = "Address")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Province")]
        public string State { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Country ID")]
        public int? CountryID { get; set; }

        [Display(Name = "State ID")]
        public int? StateID { get; set; }

        [Display(Name = "City ID")]
        public int? CityID { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        public CustomerChangePasswordViewModel ChangePassword { get; set; }

        public IEnumerable<DisplayOrderViewModel> CustomerOrders { get; set; }

        public IEnumerable<DisplayWishListViewModel> WishList { get; set; }
    }
    public class CustomerChangePasswordViewModel
    {
        [Required(ErrorMessage ="Old password is required...")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required...")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    
    public class DisplayWishListViewModel
    {
        public int? ProductID { get; set; }

        public int? CustomerID { get; set; }

        public ProductDetailViewModel Product { get; set; }

        public int WishListID { get; set; }

        public IEnumerable<string> Items { get; set; }
        public Pager Pager { get; set; }
    }
    public class WishListViewModel
    {
        public IEnumerable<DisplayWishListViewModel> WishList { get; set; }
        public Pager Pager { get; set; }

    }
}
