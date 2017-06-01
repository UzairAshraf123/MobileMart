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
   public class AddProductViewModel
    {
        public int ShopID { get; set; }

        public int ProductID { get; set; }

        public int SupplierID { get; set; }

        [Required(ErrorMessage = "You must select a sub-category.")]
        [Display(Name = "Sub-Category")]
        public int SubCategoryID { get; set; }

        [Required(ErrorMessage = "You must select a category.")]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "You must select a Company.")]
        [Display(Name = "Company")]
        public int CompanyID { get; set; }

        [Required(ErrorMessage = "Color name is required.")]
        [Display(Name = "Color")]
        public String Color { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Required")]
        [MinimumFileSizeValidator(0.0001)]
        [MaximumFileSizeValidator(3.0)]
        public HttpPostedFileBase ProductImage1 { get; set; }

        public string ProductImagePath1 { get; set; }
        
        [Required(ErrorMessage = "Required")]
        [MinimumFileSizeValidator(0.0001)]
        [MaximumFileSizeValidator(3.0)]
        public HttpPostedFileBase ProductImage2 { get; set; }

        public string ProductImagePath2 { get; set; }

        [Required(ErrorMessage = "Required")]
        [MinimumFileSizeValidator(0.0001)]
        [MaximumFileSizeValidator(3.0)]
        public HttpPostedFileBase ProductImage3 { get; set; }

        public string ProductImagePath3 { get; set; }

        [Required(ErrorMessage = "Required")]
        [MinimumFileSizeValidator(0.0001)]
        [MaximumFileSizeValidator(3.0)]
        public HttpPostedFileBase ProductImage4 { get; set; }

        public string ProductImagePath4 { get; set; }

        [Required(ErrorMessage = "Required")]
        public string ProductDetail { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [StringLength(16)]
        [RegularExpression("([0-9])+", ErrorMessage ="Enter only numeric value..")]
        public string IMEI { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [RegularExpression("([0-9])+", ErrorMessage = "Enter only numeric value..")]
        public int Quantity { get; set; }

        public bool IsOld { get; set; }
    }
}
