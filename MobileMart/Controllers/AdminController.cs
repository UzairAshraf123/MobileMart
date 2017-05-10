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
    [Authorize(Roles = "Admin")]
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

        public ActionResult AllCustomers()
        {
            try
            {
                IEnumerable<DisplayAllCustomers> model = adminBL.GetAllCustomers();
                return View(model.OrderByDescending(s => s.CreatedON));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Page404", "Error", new { message = ex.Message });
            }
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
            try
            {
                if (viewModel != null)
                {
                    AdminBL adminBL = new AdminBL();
                    adminBL.CreateShop(viewModel);
                    return RedirectToAction("DisplayShop", "Admin", new { ownerID = viewModel.OwnerID });
                }
                return RedirectToAction("CreateShop", "Admin");
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return RedirectToAction("CreateShop", "Admin");
            }
        }

        public ActionResult DisplayShop(int ownerID)
        {
            try
            {
                AdminBL BL = new AdminBL();
                var owner = BL.ShopAndOwnerByOwnerID(ownerID);
                return View(owner);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Page404", "Error", new { message = ex.Message });
            }
        }

        public ActionResult DisplayAllShops()
        {
            try
            {
                AdminBL BL = new AdminBL();
                IEnumerable<DisplayShopViewModel> shop = BL.GetAllShops();
                return View(shop.OrderByDescending(s => s.ShopCreatedOn));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Page404", "Error", new { message = ex.Message });
            }
        }

        public ActionResult DisplayOrders()
        {
            try
            {
                var adminBL = new AdminBL();
                IEnumerable<DisplayOrderViewModel> model = adminBL.GetAllOrders();
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Page404", "Error", new { message = ex.Message });
            }
        }

        public ActionResult OrderDetails(int orderID)
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
                return RedirectToAction("DisplayOrders", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Page404", "Error", new { message = ex.Message });
            }
        }
        [HttpPost]
        public bool DeleteShop(string UserID)
        {
            if (UserID != null)
            {
                AdminBL BL = new AdminBL();
                BL.DeleteUser(UserID);
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult EditOwner(int? ownerID)
        {
            try
            {
                if (ownerID != null)
                {
                    AdminBL BL = new AdminBL();
                    var owner = BL.GetOwnerByID(ownerID);
                    return View(owner);
                }
                return View("DisplayAllShops");
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return RedirectToAction("EditOwner", "Admin");
            }
        }

        [HttpGet]
        public ActionResult EditShop(int? ownerID)
        {
            try
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
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return RedirectToAction("EditOwner", "Admin");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditShop(CreateShopViewModel viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    AdminBL adminBL = new AdminBL();
                    adminBL.UpdateEditedShop(viewModel);
                    return RedirectToAction("DisplayShop", "Admin", new { ownerID = viewModel.OwnerID });
                }
                return RedirectToAction("CreateShop", "Admin");
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return RedirectToAction("EditOwner", "Admin");
            }
        }

        public ActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCompany(AddCompanyViewModel viewmodel)
        {
            try
            {
                if (viewmodel != null)
                {
                    AdminBL BL = new AdminBL();
                    BL.AddCompany(viewmodel);
                    return RedirectToAction("AddCompany", "Admin");
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }

        //Add category
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(AddCategoryViewModel viewmodel)
        {
            try
            {
                if (viewmodel != null)
                {
                    AdminBL BL = new AdminBL();
                    BL.AddCategory(viewmodel);
                    return RedirectToAction("AddCategory", "Admin");
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.massage = ex.Message;
                return RedirectToAction("AddCategory", "Admin");
            }
        }

        [AllowAnonymous]
        public ActionResult DisplayAllProduct(int? id)
        {
            try
            {
                AdminBL adminBL = new AdminBL();
                if (id > 0)
                {
                    var product = adminBL.GetProductByID(id);
                    return View(product);
                }
                return View(adminBL.GetProducts());
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Admin", new { message = ex.Message });
            }
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

    }

}
