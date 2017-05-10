using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;
using System.Data.Entity;

namespace MobileMart.Repository
{
    public class ShopNotificationRepository : IShopNotificationRepository
    {
        private MobileMartEntities _Context;
        public List<ShopNotification> Get()
        {
            _Context = new MobileMartEntities();
            return _Context.ShopNotifications.ToList();
        }
        public IEnumerable<ShopNotification> GetUnSeen()
        {
            _Context = new MobileMartEntities();
            return _Context.ShopNotifications.Where(s=>s.IsSeen == false).Take(5).ToList().OrderByDescending(s => s.Timestamp); ;
        }
        public void ChangeIsSeenByID(int shopID)
        {
            _Context = new MobileMartEntities();
            var info = _Context.ShopNotifications.Where(s => s.ShopNotificationID == shopID).FirstOrDefault();
            info.IsSeen = true;
            _Context.SaveChanges();
        }
        public void Insert(ShopNotification entity)
        {
            _Context = new MobileMartEntities();
            _Context.ShopNotifications.Add(entity);
            _Context.SaveChanges();
        }
    }
}
