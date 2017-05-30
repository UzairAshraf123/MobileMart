using MobileMart.BL;
using MobileMart.DB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileMart.Utility;
namespace MobileMart.Controllers
{
    public class NotificationController : AdminBaseController
    {
        // GET: Notification
        public ActionResult CustomerDetail(int? customerID)
        {
            try
            {
                if (customerID != null)
                {
                    var notificationBL = new NotificationBL();
                    var model = notificationBL.GetCustomerDetail(customerID);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your notification request. Please check your request." });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your notification request. Please check your request." });
            }
           
        }

        public ActionResult OrderDetail(int? orderID)
        {
            if (orderID != null)
            {
                var notificationBL = new NotificationBL();
                var model = notificationBL.GetOrderAndItsDetailByID(orderID);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

        public ActionResult ProductDetail(int? productID)
        {
            try
            {
                if (productID != null)
                {
                    var notificationBL = new NotificationBL();
                    var model = notificationBL.GetProductDetail(productID);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your notification request. Please check your request." });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your notification request. Please check your request." });
            }
            
        }

        public ActionResult ShopDetail(int? shopID)
        {
            try
            {
                if (shopID != null)
                {
                    var notificationBL = new NotificationBL();
                    var model = notificationBL.GetShopDetail(shopID);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your notification request. Please check your request." });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your notification request. Please check your request." });
            }

        }

        [HttpPost]
        public JsonResult GetNotifications()
        {
            var bl = new NotificationBL();
            var temp = bl.GetUnSeenNotification();
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetNotificatiosForShop()
        {
            var bl = new NotificationBL();
            var temp = bl.NotificatiosForShop(User.Identity.GetShopID());
            return Json(temp, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CustomerNotifications()
        {
            try
            {
                var notificationBL = new NotificationBL();
                var model = notificationBL.GetCustomerNotifications();
                return View(model);
            }
            catch 
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your notification request. Please check your request." });
            }
        }
        public ActionResult ProductNotifications()
        {
            try
            {
                var notificationBL = new NotificationBL();
                var model = notificationBL.GetProductNotifications();
                return View(model);
            }
            catch 
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your notification request. Please check your request." });
            }

        }
        public ActionResult ShopNotifications()
        {
            try
            {
                var notificationBL = new NotificationBL();
                var model = notificationBL.GetShopNotifications();
                return View(model);
            }
            catch 
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your notification request. Please check your request." });
            }
        }
        public ActionResult OrderNotifications()
        {
            var notificationBL = new NotificationBL();
            var model = notificationBL.GetOrderNotifications();
            return View(model);
        }
        [HttpPost]
        public void ChangeIsSeenState(SeenNotifications model)
        {
            var bl = new NotificationBL();
            bl.ChangeIsSeenState(model);
        }

        public ActionResult DeleteCustomer(int? CustomerID)
        {
            if (CustomerID > 0)
            {
                var customerBL = new CustomerBL();
                customerBL.DeleteCustomerByID(CustomerID);
            }
            return RedirectToAction("Index", "Admin");
        }

    }
}