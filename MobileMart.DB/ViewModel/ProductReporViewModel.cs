using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class ProductReporViewModel
    {
        public IEnumerable<DisplayProductViewModel> Products { get; set; }

        [Required]
        //[DataType(DataType.Date, ErrorMessage = "Field must be date.")]
        public DateTime From { get; set; }

        [Required]
        //[DataType(DataType.Date, ErrorMessage = "Field must be date.")]
        public DateTime To { get; set; }

    }
}
