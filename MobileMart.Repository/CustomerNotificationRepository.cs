using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;
using System.Data.Entity;

namespace MobileMart.Repository
{
    public class CustomerNotificationRepository : ICustomerNotificationRepository
    {
        private MobileMartEntities _Context;
        public List<CustomerNotification> Get()
        {
            _Context = new MobileMartEntities();
            return _Context.CustomerNotifications.ToList();
        }
        public List<CustomerNotification> GetUnSeen()
        {
            _Context = new MobileMartEntities();
            var list =  _Context.CustomerNotifications.Where(s=>s.IsSeen == false).ToList();
            return list;
        }
        public void ChangeIsSeenByID(int customerID)
        {
            _Context = new MobileMartEntities();
            var info = _Context.CustomerNotifications.Where(s => s.CustomerNotificationID == customerID).FirstOrDefault();
            info.IsSeen = true;
            _Context.SaveChanges();
        }
        public void Insert(CustomerNotification entity)
        {
            _Context = new MobileMartEntities();
            _Context.CustomerNotifications.Add(entity);
            _Context.SaveChanges();
        }
    }
}
