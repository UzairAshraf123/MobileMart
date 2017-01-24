using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;

namespace MobileMart.Repository
{
    public class ShopRepository : IShopRepository
    {
        private MobileMartEntities _context;
        public void Create(Shop entity)
        {
            _context = new MobileMartEntities();
            _context.Shops.Add(entity);
            _context.SaveChanges();
        }
    }
}
