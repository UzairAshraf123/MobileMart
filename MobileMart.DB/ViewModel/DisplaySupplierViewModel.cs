using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
   public class DisplaySupplierViewModel
    {
        public int SupplierID { get; set; }
        [Display(Name ="Name")]
        public string SupplierName { get; set; }

        [Display(Name ="Mobile")]
        public string Supplierno { get; set; }

        [Display(Name ="Address")]
        public string SupplierAddress { get; set; }
    }
}
