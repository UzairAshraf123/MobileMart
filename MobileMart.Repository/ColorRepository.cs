using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;

namespace MobileMart.Repository
{
    public class ColorRepository : IColorRepository
    {
        private readonly MobileMartEntities _context;
        public ColorRepository()
        {
            _context = new MobileMartEntities();
        }
        public IEnumerable<Color> GetColors()
        {
            return _context.Colors.ToList(); 
        }
    }
}
