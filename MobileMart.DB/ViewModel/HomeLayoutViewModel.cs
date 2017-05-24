using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class HomeLayoutViewModel
    {
        public IEnumerable<CategoryDetalViewModel> Category { get; set; }
        public IEnumerable<CompanyDetailViewModel> Company { get; set; }
    }
}
