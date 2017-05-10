using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;

namespace MobileMart.Repository
{
    public class WishListRepository : IWishListRepository
    {
        private MobileMartEntities _context;
        public IEnumerable<WishList> Get()
        {
            _context = new MobileMartEntities();
            return _context.WishLists.ToList();
        }

        public void Insert(WishList entity)
        {
            _context = new MobileMartEntities();
            _context.WishLists.Add(entity);
            _context.SaveChanges();
        }
        public IEnumerable<WishList> GetByCustomerID(int? customerID)
        {
            return Get().Where(s => s.CustomerID == customerID).ToList();
        }
        public void Delete (int? wishListID)
        {
            _context = new MobileMartEntities();
            var wishlist = Get().Where(s => s.WishListID == wishListID).FirstOrDefault();
            _context.WishLists.Remove(wishlist);
            _context.SaveChanges();
        }
    }
}
