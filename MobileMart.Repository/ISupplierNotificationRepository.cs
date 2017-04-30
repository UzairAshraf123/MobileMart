using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public interface ISupplierNotificationRepository
    {
        List<SupplierNotification> Get();
        void ChangeIsSeenByID(int supplierID);
        void Insert(SupplierNotification entity);
    }
}
