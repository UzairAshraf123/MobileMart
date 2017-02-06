using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;

namespace MobileMart.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private MobileMartEntities _context;

        public void Insert(Customer entity)
        {
            _context = new MobileMartEntities();
            _context.Customers.ToList();
            _context.SaveChanges();
        }
    }
}
