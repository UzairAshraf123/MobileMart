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
        public HttpPostedFileBase CompanyLogo { get; set; }
    }
}
