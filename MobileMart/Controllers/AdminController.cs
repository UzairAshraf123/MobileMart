using Microsoft.AspNet.Identity;
using MobileMart.BL;
using MobileMart.DB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MobileMart.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MobileMart.Controllers
{
    
    public class AdminController : Controller
    {
        AdminBL adminBL = new AdminBL();
        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateOwner(int? ownerID)
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateShop(string userID)
        {
            AdminBL adminBL = new AdminBL();
            if (userID != null)
            {
                var countries = adminBL.GetCountries().Select(s => new
                {
                    Text = s.name,
                    Value = s.id
                }).ToList();
                ViewBag.OwnerID = adminBL.GetOwnerIDByUserID(userID);
                ViewBag.UserID = userID;
                ViewBag.CountryDropDown = new SelectList(countries, "Value", "Text"); 
                return View();
            }
            return RedirectToAction("CreateOwner", "Admin");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateShop(CreateShopViewModel viewModel)
        {
            if (viewModel != null)
            {
                AdminBL adminBL = new AdminBL();
                adminBL.CreateShop(viewModel);
                return RedirectToAction("DisplayShop","Admin", new {ownerID = viewModel.OwnerID });
            }
            return RedirectToAction("CreateShop", "Admin");
        }

        public ActionResult DisplayShop(int ownerID)
        {
            AdminBL BL = new AdminBL();
            var owner = BL.GetShopByOwnerID(ownerID);
            return View(owner);
        }

        public ActionResult DisplayAllShops()
        {
                AdminBL BL = new AdminBL();
                var shop = BL.GetAllShops();
                return View(shop);
            
        }

        public ActionResult DeleteShop (int? ID)
        {
            AdminBL BL = new AdminBL();
           string status = BL.DeleteShop(ID);
            return RedirectToAction("DisplayAllShops", "Admin");
        }

        [HttpGet]
        public ActionResult EditOwner(int? ownerID)
        {
            AdminBL BL = new AdminBL();
            var Owner = BL.EditShopView(ID);
            return View(Owner);
        }

        public ActionResult EditShop (EditShopViewModel ViewModel)
        {
            AdminBL BL = new AdminBL();
            string status = BL.EditShop(ViewModel);
            return RedirectToAction("DisplayAllShops", "Admin");
        }

       

        [HttpGet]
        public ActionResult EditShop(int? ownerID)
        {
            AdminBL adminBL = new AdminBL();
            if (ownerID != null)
            {
                var shop = adminBL.GetShopByOwnerID(ownerID);
                var countries = adminBL.GetCountries().Select(s => new
                {
                    Text = s.name,
                    Value = s.id
                }).ToList();
                ViewBag.OwnerID = ownerID;
                ViewBag.CountryDropDown = new SelectList(countries, "Value", "Text");
                return View(shop);
            }
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditShop(CreateShopViewModel viewModel)
        {
            if (viewModel != null)
            {
                AdminBL adminBL = new AdminBL();
                adminBL.UpdateEditedShop(viewModel);
                return RedirectToAction("DisplayShop", "Admin", new { ownerID = viewModel.OwnerID });
            }
            return RedirectToAction("CreateShop", "Admin");
        }
    }
}
