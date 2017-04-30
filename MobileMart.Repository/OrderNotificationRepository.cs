using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;
using System.Data.Entity;

namespace MobileMart.Repository
{
    public class OrderNotificationRepository : IOrderNotificationRepository
    {
        private MobileMartEntities _Context;
        public List<OrderNotification> Get()
        {
            _Context = new MobileMartEntities();
            return _Context.OrderNotifications.ToList();
        }
        public List<OrderNotification> GetUnSeen()
        {
            _Context = new MobileMartEntities();
            return _Context.OrderNotifications.Where(s=>s.IsSeen == false).ToList();
        }
        public void ChangeIsSeenByID(int orderID)
        {
            _Context = new MobileMartEntities();
            var info = _Context.OrderNotifications.Where(s => s.OrderNotificationID == orderID).FirstOrDefault();
            info.IsSeen = true;
            _Context.SaveChanges();
        }
        public void Insert(OrderNotification entity)
        {
            _Context = new MobileMartEntities();
            _Context.OrderNotifications.Add(entity);
            _Context.SaveChanges();
        }
    }
}
