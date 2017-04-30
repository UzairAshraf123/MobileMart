using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;
using System.Data.Entity;

namespace MobileMart.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly MobileMartEntities _context;
        public SupplierRepository()
        {
            _context = new MobileMartEntities();
        }
        public void delete(int id)
        {
            var delete= _context.Suppliers.Where(s => s.SupplierID == id).FirstOrDefault();
            _context.Suppliers.Remove(delete);
            _context.SaveChanges();
        }

        public IEnumerable<Supplier> GetSupplier()
        {
            return _context.Suppliers.ToList();
        }
        public IEnumerable<Supplier> GetSupplierByID(int shopID)
        {
            return _context.Suppliers.Where(w=>w.ShopID == shopID).ToList();
        }

        public int InsertAndGetID(Supplier entity)
        {
            _context.Suppliers.Add(entity);
            _context.SaveChanges();
            int supplierID = entity.SupplierID;
            return supplierID;
        }

        public void update(Supplier entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
