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
        public Shop GetShopByID(int? shopID)
        {
            _context = new MobileMartEntities();
            return _context.Shops.Where(s => s.ShopID == shopID).FirstOrDefault();
        }
        public Shop GetShopByNotificationID(int shopNotificationID)
        {
            _context = new MobileMartEntities();
            var shop = _context.Shops.Where(s => s.ShopNotifications.Any(w => w.ShopNotificationID == shopNotificationID)).FirstOrDefault();
            return shop;
        }
        public Shop GetShopByProductID(int? productID)
        {
            _context = new MobileMartEntities();
            var shop = _context.Shops.Where(s => s.Suppliers.Any(w => w.Products.Any(e => e.ProductID == productID))).FirstOrDefault();
            return shop;
        }
       
        public void Insert(Shop entity)
        {
            _context = new MobileMartEntities();
            _context.Shops.Add(entity);
            _context.SaveChanges();
        }
        public int InsertAndGetID(Shop entity)
        {
            _context = new MobileMartEntities();
            _context.Shops.Add(entity);
            _context.SaveChanges();
            return entity.ShopID;
        }

        public void Delete(int? ID)
        {
            _context = new MobileMartEntities();
            var shop = GetShopByOwnerID(ID);
            _context.Shops.Remove(shop);
            _context.SaveChanges();
        }

        public void Edit(Shop entity)
        {
            _context = new MobileMartEntities();
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

        }
        public Shop GetShopByOwnerID(int? ID)
        {
            _context = new MobileMartEntities();
            var shop = _context.Shops.Where(s => s.OwnerID == ID).FirstOrDefault();
            return shop;
        }

        public int GetShopIDByOwnerID(int id)
        {
            _context = new MobileMartEntities();
            return _context.Shops.FirstOrDefault(s => s.OwnerID == id).ShopID;
        }
        public void Update(Shop entity)
        {
            _context = new MobileMartEntities();
            var shop = _context.Shops.FirstOrDefault(s => s.OwnerID == entity.OwnerID);
            shop.OwnerID = entity.OwnerID;
            shop.ShopID = entity.ShopID;
            shop.ShopName = entity.ShopName;
            shop.ShopAddress = entity.ShopAddress;
            shop.ShopLogo = entity.ShopLogo;
            shop.CountryID = entity.CountryID;
            shop.StateID = entity.StateID;
            shop.CityID = entity.CityID;
            shop.CreatedOn = entity.CreatedOn;
            _context.SaveChanges();
        }
    }
}
