using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;
using System.Data.Entity;

namespace MobileMart.Repository
{
    public class SupplierNotificationRepository : ISupplierNotificationRepository
    {
        private MobileMartEntities _Context;
        public List<SupplierNotification> Get()
        {
            _Context = new MobileMartEntities();
            return _Context.SupplierNotifications.ToList();
        }
        public List<SupplierNotification> GetUnSeen()
        {
            _Context = new MobileMartEntities();
            return _Context.SupplierNotifications.Where(s=>s.IsSeen == false).ToList();
        }
        public void ChangeIsSeenByID(int supplierID)
        {
            _Context = new MobileMartEntities();
            var info = _Context.SupplierNotifications.Where(s => s.SupplierNotificationID == supplierID).FirstOrDefault();
            info.IsSeen = true;
            _Context.SaveChanges();
        }
        public void Insert(SupplierNotification entity)
        {
            _Context = new MobileMartEntities();
            _Context.SupplierNotifications.Add(entity);
            _Context.SaveChanges();
        }
    }
}
