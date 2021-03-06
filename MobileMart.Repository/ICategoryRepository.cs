﻿using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public interface ICategoryRepository
    {
        void insert(Category entity);
        void delete(int id);
        IEnumerable<Category> GetCategory();
        IEnumerable<Category> GetSubCategoryByCategoryID(int categoryID);
    }
}
