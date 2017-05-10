using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public interface IWishListRepository
    {
        void Insert(WishList entity);
        IEnumerable<WishList> Get();
        IEnumerable<WishList> GetByCustomerID(int? customerID);
    }
}
