using System.Collections.Generic;
using MobileMart.DB.Model;

namespace MobileMart.BL
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Get();
        void Insert(Customer entity);
        Customer GetCustomerByID(int? ID);
        void Delete(int? ID);
        void Edit(Customer entity);
        int GetCustomerID(string aspID);
    }
}