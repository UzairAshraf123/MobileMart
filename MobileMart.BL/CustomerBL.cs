using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.Repository;
namespace MobileMart.BL
{
    public class CustomerBL
    {
        public int GetCustomerIDByAspID(string aspID)
        { 
            return new CustomerRepository().GetCustomerID(aspID);
        }
        public void DeleteCustomerByID(int? customerID)
        {
            var customerRepo = new CustomerRepository();
            customerRepo.Delete(customerID);
        }
    }
}
