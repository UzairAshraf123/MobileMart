using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class CreateShopViewModel
    {
        public string UserID { get; set; }
        public int OwnerID { get; set; }
        public string OwnerName { get; set; }
        public string Mobile { get; set; }
        public string ProfilePhotoPath { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
