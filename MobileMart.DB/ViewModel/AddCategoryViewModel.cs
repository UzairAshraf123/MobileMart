﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MobileMart.DB.ViewModel
{
   public class AddCategoryViewModel
    {
        [Required]
        [Display(Name ="Sub-Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Category Image")]
        public HttpPostedFileBase CategoryImage { get; set; }

        [Required(ErrorMessage = "You must select a category.")]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }
    }
}
