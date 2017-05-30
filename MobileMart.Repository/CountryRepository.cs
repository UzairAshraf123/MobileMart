using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;

namespace MobileMart.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private MobileMartEntities _context;
        public IEnumerable<country> Get()
        {
            _context = new MobileMartEntities();
            return _context.countries.Where(w => w.id == 166).ToList();
        }

        public country GetCountryByID(int? countryID)
        {
            _context = new MobileMartEntities();
            return _context.countries.FirstOrDefault(s=>s.id == countryID);
        }
    }
}
