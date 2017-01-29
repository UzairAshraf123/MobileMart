using MobileMart.BL;
using MobileMart.DB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileMart.Controllers
{
    
    public class AdminController : Controller
    {
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

        [Authorize(Roles = "Admin")]
        public ActionResult CreateOwner()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateShop(string userID)
        {
            if (userID != null)
            {
                AdminBL adminBL = new AdminBL();
                var countries = adminBL.GetCountries().Select(s => new
                {
                    Text = s.name,
                    Value = s.id
                }).ToList();
                ViewBag.CountryDropDown = new SelectList(countries, "Value", "Text"); 
                ViewBag.OwnerID = adminBL.GetOwnerByUserID(userID);
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
                return View("Index");
            }
            return View();
        }
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
    }
}