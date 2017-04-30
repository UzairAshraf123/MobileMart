using MobileMart.DB.Model;
using MobileMart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.BL
{
    public class AjaxBL
    {
        public IEnumerable<Category> GetSubCategoryByID(int categoryID)
        {
            var categoryRepo = new CategoryRepository();
            return categoryRepo.GetSubCategoryByCategoryID(categoryID);
        }
    }
}
