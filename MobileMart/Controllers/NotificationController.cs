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
            if (customerID != null)
            {
                var notificationBL = new NotificationBL();
                var model = notificationBL.GetCustomerDetail(customerID);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
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
            if (productID > 0)
            {
                var notificationBL = new NotificationBL();
                var model = notificationBL.GetProductDetail(productID);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Admin");               
            }
        }

        public ActionResult ShopDetail(int? shopID)
        {
            if (shopID > 0)
            {
                var notificationBL = new NotificationBL();
                var model = notificationBL.GetShopDetail(shopID);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
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
            var notificationBL = new NotificationBL();
            var model = notificationBL.GetCustomerNotifications();
            return View(model);
        }
        public ActionResult ProductNotifications()
        {
            var notificationBL = new NotificationBL();
            var model = notificationBL.GetProductNotifications();
            return View(model);
        }
        public ActionResult ShopNotifications()
        {
            var notificationBL = new NotificationBL();
            var model = notificationBL.GetShopNotifications();
            return View(model);
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