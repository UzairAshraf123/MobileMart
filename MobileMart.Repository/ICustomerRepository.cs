using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Get();
        Customer GetCustomerByNotificationID(int? customerNotificationID);
        void Insert(Customer entity);
        void Delete(int? ID);
        void Edit(Customer entity);
        Customer GetCustomerByID(int? ID);
    }
}
