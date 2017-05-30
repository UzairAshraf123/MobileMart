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
        public ActionResult Index(string message)
        {
            try
            {
                ViewBag.Message = message;
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
                model.TotalOrders = dashboardBL.GetOrders();
                model.TotalSale = dashboardBL.GetTotalSale();
                model.NewCustomers = dashboardBL.GetNewCustomers();
                return View(model);
            }
            catch
            {
                return RedirectToAction("Page404", "Error", new { message = "Something went wrong While processing your request. Please check your request." });
            }
            
        }
        [HttpGet]
        public ActionResult Account(string Message)
        {
            try
            {
                ViewBag.Message = Message;
                return View(new AdminBL().GetAdminInformation(User.Identity.GetUserId()));
            }
            catch
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }
        [HttpPost]
        public ActionResult Account(AdminProfileViewModel viewModel)
        {
            try
            {
                viewModel.AspNetID = User.Identity.GetUserId();
                new AdminBL().UpateAdmin(viewModel);
                return View(new AdminBL().GetAdminInformation(User.Identity.GetUserId()));
            }
            catch
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
            }
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
            catch
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
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
            catch 
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
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
            try
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
                else
                {
                    return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }

        [HttpPost]
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
                else
                {
                    return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }

        public ActionResult DisplayShop(int? ownerID)
        {
            try
            {
                if (ownerID != null)
                { 
                    AdminBL BL = new AdminBL();
                    var owner = BL.ShopAndOwnerByOwnerID(Convert.ToInt32(ownerID));
                    return View(owner);
                }
                else
                {
                    return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
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
            catch 
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
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
            catch
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
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
            catch 
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
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
            catch 
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }

        public ActionResult OrderDetails(int? orderID)
        {
            try
            {
                if (orderID !=null)
                {
                    var adminBL = new AdminBL();
                    var model = new OrderViewModel();
                    model.Order = adminBL.GetOrderByID(Convert.ToInt32(orderID));
                    model.OrderDetail = adminBL.GetOrderDetailByOrderID(Convert.ToInt32(orderID));
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }

        [HttpPost]
        public bool DeleteShop(string UserID)
        {
            try
            {
                if (UserID != null)
                {
                    AdminBL BL = new AdminBL();
                    BL.DeleteUser(UserID);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
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
                return RedirectToAction("DisplayAllShops","Admin", new { message = "Something went wrong While processing your request. Please check your request." });
            }
            catch
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
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
            catch 
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }

        [HttpPost]
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
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
            }
            catch
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
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
                    ViewBag.message = "Company Successfully Added.";
                    return View();
                }
                ViewBag.message = "Something went wrong While processing your request. Please check your request.";
                return View();
            }
            catch 
            {
                ViewBag.message = "Company Successfully Added.";
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
            catch
            {
                return RedirectToAction("AddCategory", "Admin");
            }
        }

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
                else if (id !=null)
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
            catch
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }

        [HttpPost]
        public ActionResult DisplayAllProduct(ProductReporViewModel viewModel)
        {
            try
            {
                var model = new ProductReporViewModel();
                AdminBL adminBL = new AdminBL();
                model.Products = adminBL.GetProductsByRange(viewModel.From, viewModel.To);
                return View(model);
            }
            catch 
            {
                return RedirectToAction("Index", "Admin", new { message = "Something went wrong While processing your request. Please check your request." });
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
