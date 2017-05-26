using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;

namespace MobileMart.Repository
{
    public interface IOwnerRepository
    {
        void Insert(Owner entity);
        void Delete(int? ID);
        void Update(Owner entity);
        IEnumerable<Owner> Get();
        Owner GetOwnerByID(int? ID);
        Owner GetOwnerByShopID(int? shopID);
        int GetOwnerIDByUserID(string userID);
    }
}
