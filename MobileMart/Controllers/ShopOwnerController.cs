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
            if (viewmodel != null)
            {
                shopBL.AddCompany(viewmodel);
                return RedirectToAction("AddCompany");
            }
            return View();
        }
        //Display Supplier
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult DisplaySupplier()
        {
            var shopID = User.Identity.GetShopID();
            return View(shopBL.GetSupplier(shopID));
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
            viewmodel.shopID = User.Identity.GetShopID(); 
            if (viewmodel != null)
            {
                return RedirectToAction("AddProduct" , "ShopOwner" , new {SupplierID = shopBL.AddSupplier(viewmodel)} );
            }
            return View();
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
        //Edit Supplier
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult EditSupplier(int id)
        {
            var supplier = shopBL.UpdteSupplierlist(id);
            return View(supplier);
        }
        [HttpPost]
        public ActionResult EditSupplier(EditSupplierViewModel viewmodel)
        {
            shopBL.UpdateSupplier(viewmodel);
            return RedirectToAction("DisplaySupplier");
        }
        //Display Product
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult DisplayProduct()
        {
            var shopID = User.Identity.GetShopID();
            return View(shopBL.GetProduct(shopID));
        }
        //Add product
        [Authorize(Roles = "ShopKeeper")]
        [HttpGet]
        public ActionResult AddProduct()
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
                ViewBag.CategoryDropDown = new SelectList(categories, "id", "text");
                return View(new AddProductViewModel { IsOld = true });
            }
            return RedirectToAction("AddSupplier", "ShopOwner");
        }
        [HttpPost]
        public ActionResult AddProduct(AddProductViewModel viewmodel)
        {
            if (viewmodel != null)
            {
                viewmodel.ShopID = User.Identity.GetShopID();
                shopBL.AddProduct(viewmodel);
                return RedirectToAction("DisplayProduct");
            }
            return View();
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
        //Update Product
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult EditProduct(int id)
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
            var product= shopBL.UpdteProductlist(id , shopID);
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(EditProductViewModel viewmodel)
        {
            viewmodel.ShopID = User.Identity.GetShopID();
            shopBL.UpdateProduct(viewmodel);
            return RedirectToAction("DisplayProduct");
        }

        
    }
}