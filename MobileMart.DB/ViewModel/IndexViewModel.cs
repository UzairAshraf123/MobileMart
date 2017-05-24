using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
  public  class IndexViewModel
    {
        public int ProductID { get; set; }

        public string Category { get; set; }

        public string Company { get; set; }

        public string Color { get; set; }

        [Display(Name ="Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Product Image")]
        public string ProductImage { get; set; }

        public decimal Price { get; set; }

        [Display(Name ="Product Detail")]
        public string ProductDetail { get; set; }

        public bool? IsOld { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int OwnerID { get; set; }

        public bool? IsFeature { get; set; }

        [Display(Name ="Owner Name")]
        public string OwnerName { get; set; }

        public int? ShopID { get; set; }

        [Display(Name ="Shop Name")]
        public string ShopName { get; set; }
    }
}
