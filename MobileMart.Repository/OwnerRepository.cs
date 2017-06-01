using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MobileMart.DB.Model;

namespace MobileMart.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private MobileMartEntities _context;

        public IEnumerable<Owner> Get()
        {
            _context = new MobileMartEntities();
            return _context.Owners.ToList();
        }

        public void Insert(Owner entity)
        {
            _context = new MobileMartEntities();
            _context.Owners.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int? ID)
        {
            _context = new MobileMartEntities();
            var owner = GetOwnerByID(ID);
            _context.Owners.Remove(owner);
           // _context.SaveChanges();
        }
        public void Update(Owner entity)
        {
            _context = new MobileMartEntities();
            var owner = _context.Owners.FirstOrDefault(s => s.OwnerID == entity.OwnerID);
            owner.OwnerID = entity.OwnerID;
            owner.OwnerName = entity.OwnerName;
            owner.OwnerContact = entity.OwnerContact;
            owner.CreatedOn = entity.CreatedOn;
            owner.OwnerPicture = entity.OwnerPicture;
            _context.SaveChanges();
        }
        public Owner GetOwnerByID(int? ID)
        {
            _context = new MobileMartEntities();
            return _context.Owners.FirstOrDefault(o => o.OwnerID == ID);
        }
        public int GetOwnerIDByUserID(string userID)
        {
            try
            {
                _context = new MobileMartEntities();
                return _context.Owners.FirstOrDefault(s => s.AspNetUserID == userID).OwnerID;
            } catch
            {
                return 0;
            }
            
        }

        public Owner GetOwnerByShopID(int? shopID)
        {
            _context = new MobileMartEntities();
            return _context.Owners.FirstOrDefault(s => s.Shops.Any(i => i.ShopID == shopID));
        }
    }
}
