using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;

namespace MobileMart.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly MobileMartEntities _context;
        public CompanyRepository()
        {
            _context = new MobileMartEntities();
        }
        public IEnumerable<Company> GetCompany()
        {
            return  _context.Companies.ToList();
        }
        public Company GetByID(int? companyID)
        {
            return GetCompany().Where(s => s.CompanyID == companyID).FirstOrDefault();
        }
        public void Delete(int id)
        {
            var DeleteCompany= _context.Companies.Where(s => s.CompanyID == id).FirstOrDefault();
            _context.Companies.Remove(DeleteCompany);
            _context.SaveChanges();
        }

        public void insert(Company Entity)
        {
            _context.Companies.Add(Entity);
            _context.SaveChanges();
        }
    }
}
