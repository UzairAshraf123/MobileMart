using Microsoft.AspNet.Identity;
using MobileMart.BL;
using MobileMart.DB.ViewModel;
using MobileMart.Utility;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MobileMart.Controllers
{
    public class HomeController : Controller
    {
        HomeBL BL = new HomeBL();

        public ActionResult Index()
        {
            var model = BL.GetHomeDetails();
            return View(model);
        }

        public ActionResult RegisterAndLogin()
        {
            AdminBL adminBL = new AdminBL();
            var countries = adminBL.GetCountries().Select(s => new
            {
                Text = s.name,
                Value = s.id
            }).ToList();
            ViewBag.CountryDropDown = new SelectList(countries, "Value", "Text");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UserProfile(int ID)
        {
            HomeBL BL = new HomeBL();
            var profile = BL.ShopDetails(ID);
            return View(profile);
        }
        
        public ActionResult ProductDetail(int? ID)
        {
            
            if (ID > 0)
            {
                HomeBL BL = new HomeBL();
                var detail = BL.Detail(ID);
                return View(detail);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Shop(int shopID)
        {
            try
            {
                if (shopID > 0)
                {
                    var shopBL = new ShopBL();
                    return View(shopBL.GetShopDetail(shopID));
                }
                return View("Index");
            }
            catch(Exception ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }

        public ActionResult MyProfile(int customerID ,string message)
        {
            var homeBL = new HomeBL();
            var countries = homeBL.GetCountries().Select(s => new
            {
                Text = s.name,
                Value = s.id
            }).ToList();
            
            ViewBag.CountryDropDown = new SelectList(countries, "Value", "Text");
            var model = homeBL.GetCustomerDetailByID(customerID);

            if (message == "ChangePasswordSuccess")
            {
                ViewBag.StatusMessage = "Password changed successfully...";
            }
            else if (message == "Error")
            {
                ViewBag.ErrorMessage = "Something went wrong While processing your request. Please check your request.";
            }
            model.CustomerOrders = homeBL.GetMyOrder(User.Identity.GetCustomerID());
            model.WishList = homeBL.GetMyWishList(User.Identity.GetCustomerID());

            return View(model);
        }

        [HttpPost]
        public ActionResult MyProfile(CustomerDetailViewModel viewModel)
        {
            var homeBL = new HomeBL();
            viewModel.AspNetUserID = User.Identity.GetUserId();
            viewModel.CustomerID = User.Identity.GetCustomerID();
            homeBL.UpdateCustomerProfile(viewModel);
            return RedirectToAction("MyProfile","Home" , new { customerID = viewModel.CustomerID});
        }

        public ActionResult MyOrdersDetails(int orderID)
        {
            try
            {
                if (orderID > 0)
                {
                    var adminBL = new AdminBL();
                    var model = new OrderViewModel();
                    model.Order = adminBL.GetOrderByID(orderID);
                    model.OrderDetail = adminBL.GetOrderDetailByOrderID(orderID);
                    return View(model);
                }
                return RedirectToAction("MyProfile", "Home",new {customerID = User.Identity.GetCustomerID() , message = "Error" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Page404", "Error", new { message = ex.Message });
            }
        }

        public ActionResult WishList()
        {
            if (User.Identity.GetCustomerID()>0)
            {
                var model =  new HomeBL().GetMyWishList(User.Identity.GetCustomerID());
                return View(model);
            }
            ViewBag.ErrorMessage = "Something went wrong While processing your request. Please check your request.";
            return View();
        }
    }
}