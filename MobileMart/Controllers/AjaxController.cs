using MobileMart.BL;
using MobileMart.DB.ViewModel;
using MobileMart.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MobileMart.Repository;
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

        [HttpPost]
        public JsonResult GetSubCategory(int categoryID)
        {
            var ajaxBL = new AjaxBL();
            var fetchedSubCategory = ajaxBL.GetSubCategoryByID(categoryID).Select(s => new
            {
                Text = s.CategoryName,
                Id = s.CategoryID
            }).ToList();
            var subCategory = new SelectList(fetchedSubCategory, "Id", "Text");
            return Json(new { subCategory }, JsonRequestBehavior.AllowGet);
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
        public JsonResult SearchProducts(string Prefix)
        {
            var products = new HomeBL().GetAllProducts();
            Prefix = Prefix.ToLower();
            //Searching records from list using LINQ query  
            var searchedProducts = products.Where(w => w.ProductName.ToLower().StartsWith(Prefix) || w.ProductName.Contains(Prefix)).Select(s => new
            {
                Route = "/Home/ProductDetail?ID=" + s.ProductID,
                Name = s.ProductName
            });
            return Json(searchedProducts, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public int AddToWishList(int? productID)
        {
            var homeBL = new HomeBL();
            homeBL.AddToWishList(productID, User.Identity.GetCustomerID());
            return User.Identity.GetCustomerWishList();
        }

        [HttpPost]
        public int DeleteItemFromWishList(int wishListID)
        {
            new WishListRepository().Delete(wishListID);
            return User.Identity.GetCustomerWishList();
        }
    }
}