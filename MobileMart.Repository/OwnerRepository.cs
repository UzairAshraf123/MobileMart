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
        public void Edit(Owner entity)
        {
            _context = new MobileMartEntities();
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

        }
        public Owner GetOwnerByID(int? ID)
        {
            _context = new MobileMartEntities();
            var owner = _context.Owners.Where(o => o.OwnerID == ID).FirstOrDefault();
            return owner;
        }
    }
}
