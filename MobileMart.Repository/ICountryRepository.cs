using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;
namespace MobileMart.Repository
{
    public interface ICountryRepository
    {
        IEnumerable<country> Get();
        country GetCountryByID(int? countryID);
    }
}
