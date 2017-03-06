using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;

namespace MobileMart.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MobileMartEntities _context;
        public CategoryRepository()
        {
            _context = new MobileMartEntities();
        } 
        public IEnumerable<Category> GetCategory()
        {
            return _context.Categories.ToList();
        }
    }
}
