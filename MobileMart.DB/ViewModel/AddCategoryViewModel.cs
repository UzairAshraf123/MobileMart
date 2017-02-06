using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MobileMart.DB.ViewModel
{
   public class AddCategoryViewModel
    {
       
        public string CategoryName { get; set; }
        public HttpPostedFileBase CategoryImage { get; set; }
    }
}
