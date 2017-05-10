using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class DisplayProductViewModel
    {
        public int ProductID { get; set; }
        [Display(Name ="Sub Category")]
        public string SubCategory { get; set; }

        [Display(Name ="Category")]
        public string Category { get; set; }

        [Display(Name ="Company")]
        public string Company { get; set; }

        [Display(Name ="Color")]
        public string Color { get; set; }

        [Display(Name ="Name")]
        public string ProductName { get; set; }

        [Display(Name ="Image 1")]
        public string ProductImage1 { get; set; }

        [Display(Name ="Image 2")]
        public string ProductImage2 { get; set; }

        [Display(Name ="Image 3")]
        public string ProductImage3 { get; set; }

        [Display(Name ="Image 4")]
        public string ProductImage4 { get; set; }

        [Display(Name ="Detail")]
        public string ProductDetail { get; set; }

        [Display(Name ="Price")]
        public decimal price { get; set; }

        public string IMEI { get; set; }

        [Display(Name ="Created")]
        public DateTime? CreatedOn { get; set; }

        [Display(Name ="Status")]
        public bool IsActive { get; set; }

        public bool? New { get; set; }
    }
}
