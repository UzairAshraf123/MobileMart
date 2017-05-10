using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public interface IProductNotificationRepository
    {
        List<ProductNotification> Get();
        IEnumerable<ProductNotification> GetUnSeen();
        void ChangeIsSeenByID(int productID);
        void Insert(ProductNotification entity);
    }
}
