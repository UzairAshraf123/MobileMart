using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class AddCompanyViewModel
    {
        public int CompanyId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "No more than 50 characters.", MinimumLength = 1)]
        [Display(Name = "Name")]
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
    }
}
