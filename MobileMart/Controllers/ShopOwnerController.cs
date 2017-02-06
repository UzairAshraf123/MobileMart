using MobileMart.DB.ViewModel;
using MobileMart.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileMart.Controllers
{
    public class ShopOwnerController : Controller
    {
        // GET: ShopOwner
        ShopKeeperBL shopBL = new ShopKeeperBL();
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
                shopBL.AddCategory(viewmodel);
                return RedirectToAction("AddCategory");
            }
            return View();
            }
        //Display Product
        public ActionResult DisplayProduct()
        {
            
            return View(shopBL.GetProduct());
        }
        //Add product
        [HttpGet]
        public ActionResult AddProduct()
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
            var colors = shopBL.GetColor().Select(s => new
            {
                id= s.ColorID,
                text=s.ColorName
            }).ToList();
            ViewBag.CompanyDropdown = new SelectList(companies, "id", "text");
            ViewBag.CategoryDropDown = new SelectList(categories, "id", "text");
            ViewBag.ColorDropDown = new SelectList(colors, "id", "text");
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(AddProductViewModel viewmodel)
        {
            if (viewmodel != null)
            {
                shopBL.AddProduct(viewmodel);
                return RedirectToAction("DisplayProduct");
            }
            return View();
        }
       //Delete Product
        public ActionResult Delete(int id)
        {
            string delete = shopBL.DeleteProduct(id);
            return RedirectToAction("DisplayProduct");
        }
        //Update Product
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
            var colors = shopBL.GetColor().Select(s => new
            {
                id = s.ColorID,
                text = s.ColorName
            }).ToList();
            ViewBag.CompanyDropdown = new SelectList(companies, "id", "text");
            ViewBag.CategoryDropDown = new SelectList(categories, "id", "text");
            ViewBag.ColorDropDown = new SelectList(colors, "id", "text");
            var product= shopBL.UpdteProduct(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(AddProductViewModel viewmodel)
        {
            shopBL.UpdateProduct(viewmodel);
            return RedirectToAction("DisplayProduct");
        }

    }
}