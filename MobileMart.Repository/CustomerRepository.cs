using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public class CustomerRepository:ICustomerRepository
    {
        private MobileMartEntities _context;

        public IEnumerable<Customer> Get()
        {
            _context = new MobileMartEntities();
            return _context.Customers.ToList();

        }
        public Customer GetCustomerByNotificationID(int? customerNotificationID)
        {
            _context = new MobileMartEntities();
            var customer = _context.Customers.Where(s => s.CustomerNotifications.Any(w => w.CustomerNotificationID == customerNotificationID)).FirstOrDefault();
            return customer;
        }

        public void Insert(Customer entity)
        {
            _context = new MobileMartEntities();
            _context.Customers.Add(entity);
            _context.SaveChanges();
        }

        public int InsertAndGetID(Customer entity)
        {
            _context = new MobileMartEntities();
            _context.Customers.Add(entity);
            _context.SaveChanges();
            return entity.CustomerID;
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
            _context.SaveChanges();
        }

        public void Edit(Customer entity)
        {
            _context = new MobileMartEntities();
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public int GetCustomerID(string aspID)
        {
            _context = new MobileMartEntities();
            return _context.Customers.FirstOrDefault(s => s.AspNetUserID == aspID).CustomerID;
        }
    }
}
