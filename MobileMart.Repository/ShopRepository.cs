using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;
using System.Data.Entity;

namespace MobileMart.Repository
{
    public class ShopRepository : IShopRepository
    {
        private MobileMartEntities _context;

        public IEnumerable<Shop> Get()
        {
            _context = new MobileMartEntities();
            return _context.Shops.ToList();
        }

        public void Insert(Shop entity)
        {
            _context = new MobileMartEntities();
            _context.Shops.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int? ID)
        {
            _context = new MobileMartEntities();
            var shop = GetShopByID(ID);
            _context.Shops.Remove(shop);
            _context.SaveChanges(); 
        }

        public void Edit(Shop entity)
        {
            _context = new MobileMartEntities();
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

        }
        public Shop GetShopByID(int? ID)
        {
            _context = new MobileMartEntities();
            var shop = _context.Shops.Where(s => s.ShopID == ID).FirstOrDefault();
            return shop;
        }
    }
}
