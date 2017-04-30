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
    [Authorize(Roles ="Admin")]
    public class AdminController : AdminBaseController
    {
        AdminBL adminBL = new AdminBL();
        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateOwner()
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
                return RedirectToAction("DisplayShop", "Admin", new { ownerID = viewModel.OwnerID });
            }
            return RedirectToAction("CreateShop", "Admin");
        }

        public ActionResult DisplayShop(int ownerID)
        {
            AdminBL BL = new AdminBL();
            var owner = BL.ShopAndOwnerByOwnerID(ownerID);
            return View(owner);
        }

        public ActionResult DisplayAllShops()
        {
                AdminBL BL = new AdminBL();
                var shop = BL.GetAllShops();
                return View(shop);
            
        }
        public ActionResult DisplayProductbyid(int? id)
        {
                AdminBL BL = new AdminBL();
                var product = BL.GetProductByID(id);
                return View(product);
        }
        public ActionResult DisplayProduct()
        {
            AdminBL BL = new AdminBL();
            var product = BL.GetProduct();
            return View(product);
        }
        public bool Delete(int? id)
        {
            if (id != null)
            {
                string delete = adminBL.DeleteProduct(id);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsActive(int productID)
        {
            var IsActive = adminBL.IsActive(productID);
            if (IsActive == true)
            {
                IsActive = false;
                return adminBL.ChangeProductStateTo(productID, IsActive);

            }
            else
            {
                IsActive = true;
                return adminBL.ChangeProductStateTo(productID, IsActive);
            }
        }
        public ActionResult AllCustomers()
        {
            return View(adminBL.GetAllCustomers());
        }

        public ActionResult DeleteShop (string UserID)
        {
            AdminBL BL = new AdminBL();
            BL.DeleteUser(UserID);
            return RedirectToAction("DisplayAllShops", "Admin");
        }

        [HttpGet]
        public ActionResult EditOwner(int? ownerID)
        {
            if (ownerID != null)
            {
                AdminBL BL = new AdminBL();
                var owner = BL.GetOwnerByID(ownerID);
                return View(owner);
            }
            return View("DisplayAllShops");
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

        public ActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCompany(AddCompanyViewModel viewmodel)
        {
            if (viewmodel != null)
            {
                AdminBL BL = new AdminBL();
                BL.AddCompany(viewmodel);
                return RedirectToAction("AddCompany" , "Admin");
            }
            return View();
        }
        
        //Add category
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(AddCategoryViewModel viewmodel)
        {

            if (viewmodel != null)
            {
                AdminBL BL = new AdminBL();
                BL.AddCategory(viewmodel);
                return RedirectToAction("AddCategory", "Admin");
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult DisplayAllProduct()
        {
            return View();
        }
    }
}
