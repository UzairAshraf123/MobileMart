using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public interface IShopRepository
    {
        IEnumerable<Shop> Get();
        void Insert(Shop entity);
        int InsertAndGetID(Shop entity);
        void Edit(Shop entity);
        void Delete(int? ID);
        Shop GetShopByOwnerID(int? ID);
        void Update(Shop entity);
        int GetShopIDByOwnerID(int ownerID);
        Shop GetShopByProductID(int? productID);
        Shop GetShopByID(int? shopID);
    }
}
