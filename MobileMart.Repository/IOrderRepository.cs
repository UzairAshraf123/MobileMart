using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public interface IOrderRepository
    {
        void Insert(Order entity);
        IEnumerable<Order> Get();
        Order GetByID(int? orderID);
        int InsertAndReturnID(Order entity);
    }
}
