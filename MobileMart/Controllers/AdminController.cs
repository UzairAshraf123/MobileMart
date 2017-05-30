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
            var model = new AdminIndexViewModel();
            var dashboardBL = new DashboardBL();
            model.NewOrdersCount = dashboardBL.GetNewOrders();
            model.AllOrdersCount = dashboardBL.GetAllOrdersCount();
            model.NewProductsCount = dashboardBL.GetNewProductsCount();
            model.AllProductsCount = dashboardBL.GetAllProductsCount();
            model.NewCustomersCount = dashboardBL.GetNewCustomersCount();
            model.AllCustomersCount = dashboardBL.GetAllCustomersCount();
            model.NewShopsCount = dashboardBL.GetNewShopsCount();
            model.AllShopsCount = dashboardBL.GetAllShopsCount();
            model.Orders = dashboardBL.TodaysOrders();
            model.TotalOrders =  dashboardBL.GetOrders();
            model.TotalSale = dashboardBL.GetTotalSale();
            model.NewCustomers = dashboardBL.GetNewCustomers();
            return View(model);
        }
        [HttpGet]
        public ActionResult Account(string Message)
        {
            ViewBag.Message = Message;
            return View(new AdminBL().GetAdminInformation(User.Identity.GetUserId()));
        }
        [HttpPost]
        public ActionResult Account(AdminProfileViewModel viewModel)
        {
            viewModel.AspNetID = User.Identity.GetUserId();
            new AdminBL().UpateAdmin(viewModel);
            return View(new AdminBL().GetAdminInformation(User.Identity.GetUserId()));
        }
        [AllowAnonymous]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AllCustomers(string customers)
        {
            try
            {
                if (customers == "new")
                {
                    var model = new CustomersReport();
                    model.Customers = adminBL.GetAllCustomers().Where(w => w.CreatedON >= DateTime.Now.AddDays(-7)).OrderByDescending(s => s.CreatedON); ;
                    return View(model);
                }
                else
                {
                    var model = new CustomersReport();
                    model.Customers = adminBL.GetAllCustomers().OrderByDescending(s => s.CreatedON);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Page404", "Error", new { message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult AllCustomers(CustomersReport viewModel)
        {
            try
            {
                var customers = adminBL.GetCustomersByRange(viewModel.From, viewModel.To).OrderByDescending(s => s.CreatedON);
                viewModel.Customers = null;
                viewModel.Customers = customers;
                return View(viewModel);
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

        [HttpGet]
        public ActionResult DisplayAllShops(string shops)
        {
            try
            {
                if (shops == "new")
                {
                    var model = new ShopReportViewModel();
                    model.Shops = adminBL.GetAllShops().Where(w => w.ShopCreatedOn >= DateTime.Now.AddDays(-7)).OrderByDescending(s => s.ShopCreatedOn); ;
                    return View(model);
                }
                else { 
                    var viewModel = new ShopReportViewModel();
                    viewModel.Shops = adminBL.GetAllShops().OrderByDescending(s => s.ShopCreatedOn);
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Page404", "Error", new { message = ex.Message });
            }
        }
        
        [HttpPost]
        public ActionResult DisplayAllShops(ShopReportViewModel model)
        {
            try
            {
                AdminBL BL = new AdminBL();
                model.Shops = BL.GetAllShopsByRange(model.From, model.To).OrderByDescending(s => s.ShopCreatedOn);
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Page404", "Error", new { message = ex.Message });
            }
        }
        
        [HttpGet]
        public ActionResult DisplayOrders(string orders)
        {
            try
            {
                var model = new OrderReportViewModel();
                if (orders == "new")
                {
                    model.Orders = adminBL.GetAllOrders().Where(w => w.CreatedOn >= DateTime.Now.AddDays(-7)).OrderByDescending(s => s.CreatedOn); ;
                    return View(model);
                }
                else
                {
                    model.Orders = adminBL.GetAllOrders();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Page404", "Error", new { message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult DisplayOrders(OrderReportViewModel viewModel)
        {
            try
            {
                var adminBL = new AdminBL();
                var model = new OrderReportViewModel();
                model.Orders = adminBL.GetAllOrdersByRange(viewModel.From, viewModel.To);
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
        [HttpGet]
        public ActionResult DisplayAllProduct(int? id,string products)
        {
            try
            {
                var model = new ProductReporViewModel();
                if (products == "new")
                {

                    model.Products = adminBL.GetProducts().Where(w => w.CreatedOn >= DateTime.Now.AddDays(-7)).OrderByDescending(s => s.CreatedOn); ;
                    return View(model);
                }
                else if (id > 0)
                {

                    model.Products = adminBL.GetProductByID(id);
                    return View(model);
                }
                else
                {
                    model.Products = adminBL.GetProducts();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Admin", new { message = ex.Message });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DisplayAllProduct(ProductReporViewModel viewModel)
        {
            try
            {
                var model = new ProductReporViewModel();
                AdminBL adminBL = new AdminBL();
                model.Products = adminBL.GetProductsByRange(viewModel.From, viewModel.To);
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Admin", new { message = ex.Message });
            }
        }

        [HttpPost]
        public bool DeleteCustomer(string aspID)
        {
            if (aspID != null)
            {
                adminBL.DeleteCustomerByID(aspID);
                return true;
            }
            else
            {
                return false;
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
