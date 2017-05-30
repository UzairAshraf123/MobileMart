using MobileMart.DB.ViewModel;
using MobileMart.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileMart.Utility;

namespace MobileMart.Controllers
{
    [Authorize(Roles = "ShopKeeper")]
    public class ShopOwnerController : ShopOwnerBaseController
    {
        // GET: ShopOwner
        ShopKeeperBL shopBL = new ShopKeeperBL();
        public ActionResult Index(string message)
        {
            try
            {
                ViewBag.Message = message;
                var model = new ShopIndexViewModel();
                var shopBL = new ShopBL();
                var shopID = User.Identity.GetShopID();
                model.NewOrdersCount = shopBL.GetNewOrders(shopID);
                model.AllOrdersCount = shopBL.GetAllOrdersCount(shopID);
                model.NewProductsCount = shopBL.GetNewProductsCount(shopID);
                model.AllProductsCount = shopBL.GetAllProductsCount(shopID);
                model.Orders = shopBL.TodaysOrders(shopID);
                model.TotalOrders = new ShopKeeperBL().GetOrdersByShopID(shopID);
                model.TotalSale = new ShopBL().GetTotalSaleOfShop(shopID);
                return View(model);
            }
            catch
            {
                return RedirectToAction("Page404", "Error", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }

        public ActionResult Account(int? shopID,string Message)
        {
            if (shopID != null)
            {
                if (Message == "ChangePasswordSuccess")
                {
                    ViewBag.Message = "Owner Updated Successfully...";
                }
                else
                {
                    ViewBag.Message = Message;
                }
                var countries = new AdminBL().GetCountries().Select(s => new
                {
                    Text = s.name,
                    Value = s.id
                }).ToList();
                ViewBag.CountryDropDown = new SelectList(countries, "Value", "Text");
                return View(shopBL.GetShopAndOwnerByShopID(shopID));
            }
            else
            {
                return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }
        [HttpPost]
        public ActionResult UpdateShop(DisplayShopViewModel viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    
                    new ShopBL().UpdateShop(viewModel);
                    return RedirectToAction("Account", "ShopOwner", new { shopID = viewModel.ShopID });
                }
                return RedirectToAction("Account", "ShopOwner", new { shopID = viewModel.ShopID,Message = "Something went wrong while processing your request..." });
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return RedirectToAction("Account", "ShopOwner", new { shopID = viewModel.ShopID,Message = ex.Message});
            }
        }
        // Shopkeeper Login
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        // add company 
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
                    ViewBag.Added = shopBL.AddCompany(viewmodel);
                    return View(); ;
                }
                return View();
            }
            catch 
            {
                ViewBag.message = "Something went wrong while processing your request...";
                return View();
            }
        }

        //Add Supplier
        public ActionResult AddSupplier(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public ActionResult AddSupplier(AddSupplierViewModel viewmodel)
        {
            try
            {
                if (viewmodel != null)
                {
                    viewmodel.ShopID = User.Identity.GetShopID();
                    return RedirectToAction("AddProduct", "ShopOwner", new { SupplierID = shopBL.AddSupplier(viewmodel), message ="Supplier Added..." });
                }
                return View();
            }
            catch 
            {
                ViewBag.message = "Something went wrong while processing your request...";
                return RedirectToAction("AddSupplier", "ShopOwner");
            }
        }

        //Add product
        [HttpGet]
        public ActionResult AddProduct(string message)
        {
            try
            {
                ViewBag.Message = message;
                if (Request.QueryString["SupplierID"] != null && Request.QueryString["SupplierID"] != "")
                {
                    var companies = shopBL.GetCompany().Select(s => new
                    {
                        id = s.CompanyID,
                        text = s.CompanyName
                    }).ToList();

                    var categories = shopBL.GetCategory().Select(s => new
                    {
                        id = s.CategoryID,
                        text = s.CategoryName
                    }).ToList();
                    ViewBag.supplierID = int.Parse(Request.QueryString["SupplierID"].ToString());
                    ViewBag.CompanyDropdown = new SelectList(companies, "id", "text");
                    ViewBag.CategoryDropDown = new SelectList(categories, "id", "text", "");
                    return View(new AddProductViewModel { IsOld = false });
                }
                return RedirectToAction("AddSupplier", "ShopOwner",new { message = "Bad Request..."});
            }
            catch 
            {
                return RedirectToAction("AddProduct", "ShopOwner",new { message = "Something went wrong while processing your request...."});
            }
        }

        [HttpPost]
        public ActionResult AddProduct(AddProductViewModel viewmodel)
        {
            try
            {
                viewmodel.ShopID = User.Identity.GetShopID();
                shopBL.AddProduct(viewmodel);
                return RedirectToAction("DisplayProduct");
            }
            catch 
            {
                return RedirectToAction("AddProduct", "ShopOwner", new { message = "Something went wrong while processing your request...." });
            }
        }

        //Display Supplier
        public ActionResult DisplaySupplier()
        {
            try
            {
                var shopID = User.Identity.GetShopID();
                return View(shopBL.GetSupplier(shopID));
            }
            catch 
            {
                return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong while processing your request. Please try again." });
            }
        }

        public ActionResult FeatureProducts()
        {
            try
            {
                var model = new ProductReporViewModel();
                var shopID = User.Identity.GetShopID();
                model.Products = shopBL.GetProduct(shopID).Where(w => w.IsFeatured == true).OrderByDescending(s => s.CreatedOn); ;
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong while processing your request. Please try again." });
            }
            
        }
        //Display Product
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult DisplayProduct(string products)
        {
            try
            {
                var model = new ProductReporViewModel();
                var shopID = User.Identity.GetShopID();
                if (products == "new")
                {
                    model.Products = shopBL.GetProduct(shopID).Where(w => w.CreatedOn >= DateTime.Now.AddDays(-7)).OrderByDescending(s => s.CreatedOn); ;
                    return View(model);
                }
                else
                {
                    model.Products = shopBL.GetProduct(shopID).OrderByDescending(s => s.CreatedOn);
                    return View(model);
                }
            }
            catch
            {
                return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong while processing your request. Please try again." });
            }
        }

        //Display Product According to range
        [HttpPost]
        public ActionResult DisplayProduct(ProductReporViewModel viewmodel)
        {
            try
            {
                var shopID = User.Identity.GetShopID();
                var model = new ProductReporViewModel();
                var products = shopBL.GetProductsByRange(shopID, viewmodel.From, viewmodel.To).OrderByDescending(s => s.CreatedOn);
                model.Products = null;
                model.Products = products;
                return View(model);
            }
            catch 
            {
                return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong while processing your request. Please try again." });
            }
        }

        //Display Orders
        public ActionResult DisplayOrder(string orders)
        {
            try
            {
                var shopID = User.Identity.GetShopID();

                if (orders =="new")
                {
                    var model = new OrderReportViewModel();
                    model.Orders = shopBL.GetOrdersByShopID(shopID).Where(w=> w.CreatedOn >= DateTime.Now.AddDays(-7)).OrderByDescending(s => s.CreatedOn); ;
                    return View(model);
                }
                else
                { 
                    var model = new OrderReportViewModel();
                    model.Orders = shopBL.GetOrdersByShopID(shopID).OrderByDescending(s => s.CreatedOn);
                    return View(model);
                }
            }
            catch 
            {
                return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong while processing your request. Please try again." });
            }
        }
        //Orders According to Range
        [HttpPost]
         public ActionResult DisplayOrder(OrderReportViewModel viewModel)
        {
            try
            {
                var shopID = User.Identity.GetShopID();
                var model = new OrderReportViewModel();
                model.Orders = shopBL.GetOrdersByRange(shopID, viewModel.From, viewModel.To).OrderByDescending(s => s.CreatedOn);
                return View(model);
            }
            catch 
            {
                return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong while processing your request. Please try again." });
            }
        }
        //Display Order detail by orderID
        public ActionResult OrderDetails(int? orderID)
        {
            try
            {
                if (orderID != 0)
                {
                    var adminBL = new AdminBL();
                    var model = new OrderViewModel();
                    model.Order = adminBL.GetOrderByID(Convert.ToInt32(orderID));
                    model.OrderDetail = adminBL.GetOrderDetailByOrderID(Convert.ToInt32(orderID));
                    return View(model);
                }
                return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong while processing your request. Please try again." });
            }
            catch 
            {
                return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong while processing your request. Please try again." });
            }
        }

        //Delete Supplier
        [Authorize(Roles = "ShopKeeper")]
        public bool DeleteSupplier(int? id)
        {
            if (id != null)
            {
                shopBL.DeleteSupplier(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        //Update Product
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult EditProduct(int? id)
        {
            try
            {
                if (id !=null)
                {
                    var companies = shopBL.GetCompany().Select(s => new
                    {
                        id = s.CompanyID,
                        text = s.CompanyName
                    }).ToList();
                    var categories = shopBL.GetCategory().Select(s => new
                    {
                        id = s.CategoryID,
                        text = s.CategoryName
                    }).ToList();

                    ViewBag.CompanyDropdown = new SelectList(companies, "id", "text");
                    ViewBag.CategoryDropDown = new SelectList(categories, "id", "text");
                    var shopID = User.Identity.GetShopID();
                    var product = shopBL.UpdteProductlist(Convert.ToInt32(id), shopID);
                    return View(product);
                }
                else
                {
                    return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong while processing your request. Please try again." });
                }
            }
            catch  
            {
                return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong while processing your request. Please try again." });
            }
        }

        [HttpPost]
        public ActionResult EditProduct(EditProductViewModel viewmodel)
        {
            try
            {
                viewmodel.ShopID = User.Identity.GetShopID();
                shopBL.UpdateProduct(viewmodel);
                return RedirectToAction("DisplayProduct");
            }
            catch
            {
                return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong while processing your request. Please try again." });
            }
        }

        //Edit Supplier
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult EditSupplier(int? id)
        {
            try
            {
                if (id!=null)
                {
                    var supplier = shopBL.UpdteSupplierlist(Convert.ToInt32(id));
                    return View(supplier);
                }
                else
                {
                    return RedirectToAction("Index", "ShopOwner", new { message = "Bad Request..." });
                }
            }
            catch 
            {
                return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong while processing your request. Please try again." });
            }
        }

        [HttpPost]
        public ActionResult EditSupplier(EditSupplierViewModel viewmodel)
        {
            try
            {
                shopBL.UpdateSupplier(viewmodel);
                return RedirectToAction("DisplaySupplier");
            }
            catch
            {
                return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong while processing your request. Please try again." });
            }
        }

        //IsActive Product
        public bool IsActive(int productID)
        {
            var shopID = User.Identity.GetShopID();
            var IsActive = shopBL.IsActive(productID, shopID);
            if (IsActive == true)
            {
                IsActive = false;
                return shopBL.ChangeProductStateTo(productID, IsActive);

            }
            else
            {
                IsActive = true;
                return shopBL.ChangeProductStateTo(productID, IsActive);
            }
        }
        public bool MakeFeature(int productID)
        {
            var shopID = User.Identity.GetShopID();
            var isFeature = shopBL.IsFeature(productID, shopID);
            if (isFeature == true)
            {
                isFeature = false;
                return shopBL.ChangeFeatureState(productID, isFeature);

            }
            else
            {
                isFeature = true;
                return shopBL.ChangeFeatureState(productID, isFeature);
            }
        }
        //Add category
        public ActionResult AddCategory(string message)
        {
            try
            {
                ViewBag.Message = message;
                var categories = new ShopKeeperBL().GetCategory().Select(s => new
                {
                    id = s.CategoryID,
                    text = s.CategoryName
                }).ToList();
                ViewBag.CategoryDropDown = new SelectList(categories, "id", "text", "");
                return View();
            }
            catch
            {
                return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong while processing your request. Please try again." });
            }

        }

        [HttpPost]
        public ActionResult AddCategory(AddCategoryViewModel viewmodel)
        {
            try
            {
                if (viewmodel != null)
                {
                    AdminBL BL = new AdminBL();
                    BL.AddSubCategory(viewmodel);
                    return RedirectToAction("AddCategory", "ShopOwner", new{ message = "Category Added successfully..."});
                }
                return View();
            }
            catch
            {
                return RedirectToAction("Index", "ShopOwner", new { message = "Something went wrong while processing your request. Please try again." });
            }
        }


    }
}
