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
    public class AddCompanyViewModel
    {
        public int CompanyId { get; set; }
        [Required]
        [Display(Name = "Company")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Company Logo")]
        [MinimumFileSizeValidator(0.0001)]
        [MaximumFileSizeValidator(3.0)]
        public HttpPostedFileBase CompanyLogo { get; set; }
    }
}
