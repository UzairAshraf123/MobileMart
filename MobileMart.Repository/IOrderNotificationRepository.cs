using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public interface IOrderNotificationRepository
    {
        List<OrderNotification> Get();
        void ChangeIsSeenByID(int orderID);
        void Insert(OrderNotification entity);
    }
}
