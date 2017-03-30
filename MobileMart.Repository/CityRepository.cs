using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;
namespace MobileMart.Repository
{
    public class CityRepository: ICityRepository
    {
        private MobileMartEntities _context;
        public IEnumerable<city> Get()
        {
            _context = new MobileMartEntities();
            return _context.cities.ToList();
        }

        public city GetCityByID(int? cityID)
        {
            _context = new MobileMartEntities();
            return _context.cities.FirstOrDefault(s => s.id == cityID);
        }
    }
}
