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

            return View(shopBL.GetSupplier());
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
            if (viewmodel != null)
            {
                viewmodel.ShopID = User.Identity.GetShopID();
                return RedirectToAction("AddProduct" , "ShopOwner" , new {SupplierID = shopBL.AddSupplier(viewmodel)} );
            }
            return View();
        }
        //Delete Supplier
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult DeleteSupplier(int id)
        {
            shopBL.DeleteSupplier(id);
            return RedirectToAction("DisplaySupplier");
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
       //Delete Product
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult Delete(int id)
        {
            string delete = shopBL.DeleteProduct(id);
            return RedirectToAction("DisplayProduct");
        }
        //Update Product
        [Authorize(Roles = "ShopKeeper")]
        public ActionResult Edit(int id)
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
        public ActionResult Edit(AddProductViewModel viewmodel)
        {
            viewmodel.ShopID = User.Identity.GetShopID();
            shopBL.UpdateProduct(viewmodel);
            return RedirectToAction("DisplayProduct");
        }

        
    }
}