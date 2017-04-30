using MobileMart.BL;
using MobileMart.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileMart.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        public JsonResult StatesByCountryID(int id)
        {
            AdminBL bl = new AdminBL();
            List<SelectListItem> list = new List<SelectListItem>();
            var states = bl.GetStatesByCountryId(id).Select(s => new
            {
                Text = s.name,
                Id = s.id
            }).ToList();
            var state = new SelectList(states, "Id", "Text");
            return Json(new { state }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CitesByStateID(int id)
        {
            AdminBL bl = new AdminBL();
            List<SelectListItem> list = new List<SelectListItem>();
            var cities = bl.GetCitiesByStateId(id).Select(s => new
            {
                Text = s.name,
                Id = s.id
            }).ToList();
            var city = new SelectList(cities, "Id", "Text");
            return Json(new { city }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchSupplier(string Prefix, int ShopID)
        {
            var supplier = new ShopKeeperBL().GetSuppliersByShopID(ShopID);
            Prefix = Prefix.ToLower();
            //Searching records from list using LINQ query  
            var searchedUsers = supplier.Where(w => w.SupplierName.ToLower().StartsWith(Prefix)).Select(s => new
            {
                Route = "/ShopOwner/AddProduct?SupplierID=" + s.SupplierID,
                Name = s.SupplierName
            });

            return Json(searchedUsers, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SearchProduct(string Prefix)
        {
            var product = new ShopKeeperBL().GetProduct(User.Identity.GetShopID());
            Prefix = Prefix.ToLower();
            //Searching records from list using LINQ query  
            var searchedUsers = product.Where(w => w.ProductName.ToLower().StartsWith(Prefix)).Select(s => new
            {
                Name = s.ProductName
            });

            return Json(searchedUsers, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public JsonResult AddItemInCart()
        //{
        //    return Json( ,JsonRequestBehavior.AllowGet);
        //}
    }
}