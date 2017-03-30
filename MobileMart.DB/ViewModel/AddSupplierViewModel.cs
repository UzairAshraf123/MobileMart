using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
   public class AddSupplierViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string SupplierName { get; set; }

        [Required]
        [Display(Name = "Contact")]
        public string SupplierContact { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string SupplierAddress { get; set; }

        public DateTime CreatedON { get; set; }

        [Required]
        [Display(Name = "CNIC")]
        public string CNIC { get; set; }

    }
}
