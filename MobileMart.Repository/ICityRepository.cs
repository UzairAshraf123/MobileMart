using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;
namespace MobileMart.Repository
{
    public interface ICityRepository
    {
        IEnumerable<city> Get();
        city GetCityByID(int? cityID);
    }
}
