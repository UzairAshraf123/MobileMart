using Microsoft.AspNet.Identity;
using MobileMart.BL;
using MobileMart.DB.ViewModel;
using MobileMart.Services;
using MobileMart.Utility;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MobileMart.Controllers
{
    public class HomeController : HomeBaseController
    {
        HomeBL BL = new HomeBL();

        public ActionResult Index()
        {
            var model = BL.GetHomeDetails();
            ViewBag.Companies = BL.GetCompanies();
            return View(model);
        }
       
        public ActionResult RegisterAndLogin()
        {
            AdminBL adminBL = new AdminBL();
            var countries = adminBL.GetCountries().Select(s => new
            {
                Text = s.name,
                Value = s.id
            }).ToList();
            ViewBag.CountryDropDown = new SelectList(countries, "Value", "Text");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UserProfile(int ID)
        {
            HomeBL BL = new HomeBL();
            var profile = BL.ShopDetails(ID);
            return View(profile);
        }
        
        public ActionResult ProductDetail(int? ID)
        {
            
            if (ID > 0)
            {
                HomeBL BL = new HomeBL();
                var detail = BL.Detail(ID);
                return View(detail);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Shop(int shopID)
        {
            try
            {
                if (shopID > 0)
                {
                    var shopBL = new ShopBL();
                    return View(shopBL.GetShopDetail(shopID));
                }
                return View("Index");
            }
            catch(Exception ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }

        public ActionResult MyProfile(int customerID ,string message)
        {
            var homeBL = new HomeBL();
            var countries = homeBL.GetCountries().Select(s => new
            {
                Text = s.name,
                Value = s.id
            }).ToList();
            
            ViewBag.CountryDropDown = new SelectList(countries, "Value", "Text");
            var model = homeBL.GetCustomerDetailByID(customerID);

            if (message == "ChangePasswordSuccess")
            {
                ViewBag.StatusMessage = "Password changed successfully...";
            }
            else if (message == "Error")
            {
                ViewBag.ErrorMessage = "Something went wrong While processing your request. Please check your request.";
            }
            model.CustomerOrders = homeBL.GetMyOrder(User.Identity.GetCustomerID());
            model.WishList = homeBL.GetMyWishList(User.Identity.GetCustomerID());

            return View(model);
        }

        [HttpPost]
        public ActionResult MyProfile(CustomerDetailViewModel viewModel)
        {
            var homeBL = new HomeBL();
            viewModel.AspNetUserID = User.Identity.GetUserId();
            viewModel.CustomerID = User.Identity.GetCustomerID();
            homeBL.UpdateCustomerProfile(viewModel);
            return RedirectToAction("MyProfile","Home" , new { customerID = viewModel.CustomerID});
        }

        public ActionResult MyOrdersDetails(int orderID)
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
                return RedirectToAction("MyProfile", "Home",new {customerID = User.Identity.GetCustomerID() , message = "Error" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Page404", "Error", new { message = ex.Message });
            }
        }

        public ActionResult WishList(int? page)
        {
            if (User.Identity.GetCustomerID()>0)
            {
                var wishList =  new HomeBL().GetMyWishList(User.Identity.GetCustomerID());
                var pager = new Pager(wishList.Count(), page);
                var model = new WishListViewModel()
                {
                    Pager = pager,
                    WishList = wishList
                };
                return View(model);    
            }
            ViewBag.ErrorMessage = "Something went wrong While processing your request. Please check your request.";
            return View();
        }

        public ActionResult AllProducts(int? page)
        {
            var products = new HomeBL().GetAllProducts();
            var pager = new Pager(products.Count(), page);
            var model = new AllProductsViewModel()
            {
                Pager = pager,
                ProductsList = products.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize)
            };
            return View(model);
        }

        public ActionResult Page(int? page)
        {
            var dummyItems = Enumerable.Range(1, 150).Select(x => "Item " + x);
            var pager = new Pager(dummyItems.Count(), page);

            var viewModel = new PagerViewModel
            {
                Items = dummyItems.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager
            };

            return View(viewModel);
        }

        public ActionResult Companies()
        {
            return View(new HomeBL().GetCompanies());
        }

        public ActionResult ProductsByBrand(int? companyID)
        {
            ViewBag.Brand = new HomeBL().GetCompanyByID(companyID);
            return View(new HomeBL().GetProductByCompanyID(companyID));
        }

        public ActionResult NewProducts()
        {
            return View(new HomeBL().GetNewProducts());
        }

        public ActionResult NewItems(int? categoryID)
        {
            return View(new HomeBL().GetNewTablets(categoryID));
        }
     
        public ActionResult ProductBySubCategory(int? categoryID,int? subCategoryID)
        {
            return View(new HomeBL().GetNewTabletsByCategory(categoryID, subCategoryID));
        }

        public ActionResult OldProducts()
        {
            return View(new HomeBL().GetOldItems());
        }

        public ActionResult OldItems(int? categoryID)
        {
            return View(new HomeBL().GetOldItemsByCategoryID(categoryID));
        }

        public ActionResult ProductsByCategories(int? categoryID, int? subCategoryID)
        {
            return View(new HomeBL().GetOldItemsByCategories(categoryID, subCategoryID));
        }

       
    }
}