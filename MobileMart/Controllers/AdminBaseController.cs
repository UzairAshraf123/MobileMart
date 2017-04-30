using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileMart.BL;
using MobileMart.DB.Model;
using MobileMart.DB.ViewModel;
namespace MobileMart.Controllers
{
    public class AdminBaseController : Controller
    {
       public AdminBaseController()
        {
            AdminBL adminBL = new AdminBL();

            AdminLayoutViewModel data = new AdminLayoutViewModel();
            data.Customers  = adminBL.AllCustomers();
            data.Shops = adminBL.AllShops();
            data.products = adminBL.GetProduct();
            ViewBag.LayoutData = data;
        }
    }
}