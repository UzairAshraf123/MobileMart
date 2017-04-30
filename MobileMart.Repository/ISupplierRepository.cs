using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
   public interface ISupplierRepository
    {
        int InsertAndGetID(Supplier entity);
        void delete(int id);
        void update(Supplier entity);
        IEnumerable<Supplier> GetSupplier();
        IEnumerable<Supplier> GetSupplierByID(int shopID);
    }
}
