using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class OrderReportViewModel
    {
        public IEnumerable<DisplayOrderViewModel> Orders { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Field must be date.")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime From { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Field must be date.")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }

    }
}
