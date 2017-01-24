using MobileMart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.BL
{
    public class AdminBL
    {
        public void CreateShop()
        {
            IShopRepository shopRepo = new ShopRepository();
            IOwnerRepository ownerRepo = new OwnerRepository();
        }
    }
}
