using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public interface IShopNotificationRepository
    {
        List<ShopNotification> Get();
        IEnumerable<ShopNotification> GetUnSeen();
        void ChangeIsSeenByID(int shopID);
    }
}
