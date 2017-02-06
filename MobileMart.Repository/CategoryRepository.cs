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
        public void delete(int id)
        {
            var deletecategory = _context.Categories.Where(s => s.CategoryID == id).FirstOrDefault();
            _context.Categories.Remove(deletecategory);
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetCategory()
        {
            return _context.Categories.ToList();
        }

        public void insert(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }
    }
}
