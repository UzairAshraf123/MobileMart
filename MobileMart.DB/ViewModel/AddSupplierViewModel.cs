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
        public int shopID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string SupplierName { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [Display(Name = "Mobile")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^((\03-?)|0)?[0-9]{10}$", ErrorMessage = "Not a valid Phone number")]
        public string SupplierContact { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string SupplierAddress { get; set; }

        public DateTime CreatedON { get; set; }

        [Required]
        [RegularExpression(@"^[0-9+]{5}-[0-9+]{7}-[0-9]{1}$", ErrorMessage = "Entered CNIC format is not valid.")]
        [Display(Name = "CNIC")]
        public string CNIC { get; set; }

        public int ShopID { get; set; }

    }
}
