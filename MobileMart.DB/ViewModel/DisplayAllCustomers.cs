using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class DisplayAllCustomers
    {
        public int CustomerID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "DOB")]
        public DateTime? DOB { get; set; }

        [Display(Name = "Created")]
        public DateTime? CreatedON { get; set; }

        [Display(Name = "Profile Photo")]
        public string ProfilePicturePath { get; set; }

        [Display(Name = "Address")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Province")]
        public string State { get; set; }

        [Display(Name ="City")]
        public string City { get; set; }

        [Display(Name = "City ID")]
        public int CityID { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        public string AspID { get; set; }

    }
}
