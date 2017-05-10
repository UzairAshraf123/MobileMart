using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public interface ICustomerNotificationRepository
    {
        List<CustomerNotification> Get();
        IEnumerable<CustomerNotification> GetUnSeen();
        void ChangeIsSeenByID(int customerID);
        void Insert(CustomerNotification entity);
    }
}
