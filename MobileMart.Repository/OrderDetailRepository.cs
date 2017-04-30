using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;

namespace MobileMart.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly MobileMartEntities _context;
        public OrderDetailRepository()
        {
            _context = new MobileMartEntities();
        }
        public IEnumerable<OrderDetail> Get()
        {
            return _context.OrderDetails.ToList();
        }
        public IEnumerable<OrderDetail> GetByOrderID(int? orderID)
        {
            return _context.OrderDetails.Where(s => s.OrderID == orderID).ToList();
        }
        public void Insert(OrderDetail entity)
        {
            _context.OrderDetails.Add(entity);
            _context.SaveChanges();
        }
    }
}
