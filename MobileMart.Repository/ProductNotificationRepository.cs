using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;
using System.Data.Entity;

namespace MobileMart.Repository
{
    public class ProductNotificationRepository : IProductNotificationRepository
    {
        private MobileMartEntities _Context;
        public List<ProductNotification> Get()
        {
            _Context = new MobileMartEntities();
            return _Context.ProductNotifications.ToList();
        }
        public List<ProductNotification> GetUnSeen()
        {
            _Context = new MobileMartEntities();
            return _Context.ProductNotifications.Where(s=>s.IsSeen == false).ToList();
        }
        public void ChangeIsSeenByID(int productID)
        {
            _Context = new MobileMartEntities();
            var info = _Context.ProductNotifications.Where(s => s.ProductNotificationID == productID).FirstOrDefault();
            info.IsSeen = true;
            _Context.SaveChanges();
        }
        public void Insert(ProductNotification entity)
        {
            _Context = new MobileMartEntities();
            _Context.ProductNotifications.Add(entity);
            _Context.SaveChanges();
        }
    }
}
