using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
   public interface IProductRepository
    {
        void insert(Product entity);
        void delete(int? id);
        IEnumerable<Product> Get();
        IEnumerable<Product> GetProduct(int? shopID);
        void update(Product entity);
        Product GetProductByID(int? productID);
        int InsertAndGetID(Product entity);
        bool ChangeActiveStatus(Product entity);
        bool? ChangeFeatureStatus(Product entity);
        void MinusQuantityByID(int? productID, int? Quantity);
    }
}
