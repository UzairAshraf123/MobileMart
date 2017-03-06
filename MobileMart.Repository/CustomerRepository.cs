using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;
using System.Data.Entity;

namespace MobileMart.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private MobileMartEntities _context;

        public IEnumerable<Customer> Get()
        {
            _context = new MobileMartEntities();
            return _context.Customers.ToList();
            
        }

        public void Insert(Customer entity)
        {
            _context = new MobileMartEntities();
            _context.Customers.ToList();
            _context.SaveChanges();
        }

        public Customer GetCustomerByID(int? ID)
        {
            _context = new MobileMartEntities();
            return _context.Customers.Where(w => w.CustomerID == ID).FirstOrDefault();
        }

        public void Delete(int? ID)
        {
            _context = new MobileMartEntities();
            var customer = GetCustomerByID(ID);
            _context.Customers.Remove(customer);
        }

        public void Edit(Customer entity)
        {
            _context = new MobileMartEntities();
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
