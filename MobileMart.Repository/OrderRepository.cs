using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public class OrderRepository: IOrderRepository
    {
        private readonly MobileMartEntities _context;
        public OrderRepository()
        {
            _context = new MobileMartEntities();
        }
        public IEnumerable<Order> Get()
        {
            return _context.Orders.ToList();
        }
        public Order GetByID(int? orderID)
        {
            return _context.Orders.Where(s=> s.OrderID == orderID).FirstOrDefault();
        }
        public void Insert(Order entity)
        {
            _context.Orders.Add(entity);
            _context.SaveChanges();
        }

        public int InsertAndReturnID(Order entity)
        {
            _context.Orders.Add(entity);
            _context.SaveChanges();
            return entity.OrderID;
        }

        
    }
}
