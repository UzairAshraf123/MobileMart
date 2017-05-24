using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class CategoryDetalViewModel
    {
        [Display(Name = "Category ID")]
        public int? CategoryID { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Sub Category")]
        public int? ParentCategoryID { get; set; }
    }
}
