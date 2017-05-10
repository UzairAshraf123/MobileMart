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
            data.CustomerCount = adminBL.GetCustomerCount();
            data.ShopCount = adminBL.GetShopCount();
            data.ProductCount = adminBL.GetProductCount();
            data.OrderCount = adminBL.GetOrderCount();
            ViewBag.LayoutData = data;
        }
    }
}