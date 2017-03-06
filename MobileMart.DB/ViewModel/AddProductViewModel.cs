using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MobileMart.DB.ViewModel
{
   public class AddProductViewModel
    {
        public int id { get; set; }
        public int Category { get; set; }
        public int Company { get; set; }
        public int Color { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Required")]
        public HttpPostedFileBase ProductImage1 { get; set; }
        public string ProductImagePath1 { get; set; }
        [Required(ErrorMessage = "Required")]
        public HttpPostedFileBase ProductImage2 { get; set; }
        public string ProductImagePath2 { get; set; }
        [Required(ErrorMessage = "Required")]
        public HttpPostedFileBase ProductImage3 { get; set; }
        public string ProductImagePath3 { get; set; }
        [Required(ErrorMessage = "Required")]
        public HttpPostedFileBase ProductImage4 { get; set; }
        public string ProductImagePath4 { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ProductDetail { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required(ErrorMessage = "Required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Required")]
        public string IMEI { get; set; }

    }
}
