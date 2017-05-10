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
    public class ShopOwnerController : ShopOwnerBaseController
    {
        // GET: ShopOwner
        ShopKeeperBL shopBL = new ShopKeeperBL();
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult Index()
        {
            return View();
        }
        
        // Shopkeeper Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        
        // add company 
        [Authorize(Roles = "ShopKeeper")]
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
                shopBL.AddCompany(viewmodel);
                return RedirectToAction("AddCompany");
            }
            return View();
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return RedirectToAction("AddCompany", "ShopOwner");
            }
        }

        //Add Supplier
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult AddSupplier()
        {
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
                return RedirectToAction("AddProduct", "ShopOwner", new { SupplierID = shopBL.AddSupplier(viewmodel) });
            }
            return View();
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return RedirectToAction("AddSupplier", "ShopOwner");
            }
        }

        //Add product
        [Authorize(Roles = "ShopKeeper")]
        [HttpGet]
        public ActionResult AddProduct()
        {
            try
            {
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
                return RedirectToAction("AddSupplier", "ShopOwner");
            }
            catch(Exception ex)
            {
                ViewBag.message = ex.Message;
                return RedirectToAction("AddProduct", "ShopOwner");
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
            catch(Exception ex)
            {
                ViewBag.message = ex.Message;
                return RedirectToAction("AddProduct", "ShopOwner");
            }
        }

        //Display Supplier
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult DisplaySupplier()
        {
            try
            { 
            var shopID = User.Identity.GetShopID();
            return View(shopBL.GetSupplier(shopID));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Page404", "Error", new { message = ex.Message });
            }
        }

        //Display Product
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult DisplayProduct()
        {
            try
            { 
            var shopID = User.Identity.GetShopID();
            return View(shopBL.GetProduct(shopID).OrderByDescending(s => s.CreatedOn));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Page404", "Error", new { message = ex.Message });
            }
        }

        //Display Orders
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult DisplayOrder()
        {
            try
            { 
            var shopID = User.Identity.GetShopID();
            var model = shopBL.GetOrdersByShopID(shopID).OrderByDescending(s => s.CreatedOn);
            return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Page404", "Error", new { message = ex.Message });
            }
        }

        //Display Order detail by orderID
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
        public ActionResult EditProduct(int id)
        {
            try
            { 
            if (id > 0) {
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
                var product = shopBL.UpdteProductlist(id, shopID);
                return View(product);
            }
            else
            {
                return View("DisplayProduct");
            }
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return RedirectToAction("EditProduct", "ShopOwner");
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
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return RedirectToAction("EditProduct", "ShopOwner");
            }
        }
        
        //Edit Supplier
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult EditSupplier(int id)
        {
            try
            { 
            var supplier = shopBL.UpdteSupplierlist(id);
            return View(supplier);
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return RedirectToAction("EditSupplier", "ShopOwner");
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
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return RedirectToAction("EditSupplier", "ShopOwner");
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
       
    }
}
 