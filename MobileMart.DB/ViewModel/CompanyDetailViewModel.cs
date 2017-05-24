using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class CompanyDetailViewModel
    {
        public int CompanyId { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        [Display(Name ="Logo")]
        public string Logo { get; set; }
    }
}
