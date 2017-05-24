using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.Repository;
using MobileMart.DB.ViewModel;
namespace MobileMart.BL
{
    public class BaseControllerBL
    {
        public IEnumerable<CategoryDetalViewModel> GetCategory()
        {
            var categoryRepo = new CategoryRepository();
            return categoryRepo.GetCategory().Select(s=> new CategoryDetalViewModel
            {
                CategoryID = s.CategoryID,
                CategoryName = s.CategoryName,
                ParentCategoryID  = s.ParentCategory,
            });
        }
        public IEnumerable<CompanyDetailViewModel> GetCompany()
        {
            var companyRepo = new CompanyRepository();
            return companyRepo.GetCompany().Select(s=> new CompanyDetailViewModel
            {
                CompanyId = s.CompanyID,
                CompanyName = s.CompanyName,
            });
        }
    }
}
