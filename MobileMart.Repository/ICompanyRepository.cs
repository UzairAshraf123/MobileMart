using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
   public interface ICompanyRepository
    {
        void insert(Company Entity);
        void Delete(int id);
        IEnumerable<Company> GetCompany();
    }
}
