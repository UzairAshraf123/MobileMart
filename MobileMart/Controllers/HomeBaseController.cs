using MobileMart.BL;
using MobileMart.DB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileMart.Controllers
{
    public class HomeBaseController : Controller
    {
        // GET: HomeBase
        public HomeBaseController()
        {
            var bl = new BaseControllerBL();
            var viewModel = new HomeLayoutViewModel();
            viewModel.Category = bl.GetCategory();
            viewModel.Company = bl.GetCompany();
            ViewBag.LayoutData = viewModel;
        }
    }
}