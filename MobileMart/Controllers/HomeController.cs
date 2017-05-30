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

        public ActionResult Index(string message)
        {
            var model = BL.GetHomeDetails();
            ViewBag.Companies = BL.GetCompanies();
            ViewBag.Message = message;
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
        
        public ActionResult ProductDetail(int? ID)
        {
            try
            {
                if (ID != null)
                {
                    HomeBL BL = new HomeBL();
                    var detail = BL.Detail(ID);
                    return View(detail);
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { message = "Bad Request..." });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { message = "Bad Request..." });
            }
        }

        public ActionResult Shop(int? shopID,int? page)
        {
            try
            {
                if (shopID != 0)
                {
                    var shopBL = new ShopBL();
                    var shop = shopBL.GetShopDetail(Convert.ToInt32(shopID));
                    var products = new ShopKeeperBL().GetProduct(Convert.ToInt32(shopID)).Where(s=> s.IsActive == true);
                    var pager = new Pager(products.Count(), page);
                    shop.Pager = pager;
                    shop.ProductDetail = products.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);
                    return View(shop);
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { message = "Bad Request..." });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { message = "Bad Request..." });
            }
        }

        public ActionResult MyProfile(int? customerID, string message)
        {
            try
            {
                if (customerID !=null)
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
                else
                {
                    return RedirectToAction("Index", "Home", new { message = "Bad Request..." });
                }
            }
            catch 
            {
                return RedirectToAction("Index", "Home", new { message = "Bad Request..." });
            }        
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

        public ActionResult MyOrdersDetails(int? orderID)
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
                    return RedirectToAction("Index", "Home", new { message = "Bad Request..." });
                }
            }
            catch 
            {
                return RedirectToAction("Index", "Home", new { message = "Bad Request..." });
            }
        }

        public ActionResult WishList(int? page)
        {
            try
            {
                if (User.Identity.GetCustomerID() > 0)
                {
                    var wishList = new HomeBL().GetMyWishList(User.Identity.GetCustomerID());
                    var pager = new Pager(wishList.Count(), page);
                    var model = new WishListViewModel()
                    {
                        Pager = pager,
                        WishList = wishList
                    };
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { message = "Something went wrong While processing your request. Please check your request." });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }

        public ActionResult AllProducts(int? page)
        {
            try
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
            catch
            {
                return RedirectToAction("Index", "Home", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }

        //public ActionResult Page(int? page)
        //{
        //    var dummyItems = Enumerable.Range(1, 150).Select(x => "Item " + x);
        //    var pager = new Pager(dummyItems.Count(), page);

        //    var viewModel = new PagerViewModel
        //    {
        //        Items = dummyItems.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
        //        Pager = pager
        //    };

        //    return View(viewModel);
        //}

        public ActionResult Companies()
        {
            try
            {
                return View(new HomeBL().GetCompanies());
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }

        public ActionResult ProductsByBrand(int? companyID)
        {
            try
            {
                if (companyID != null)
                {
                    ViewBag.Brand = new HomeBL().GetCompanyByID(companyID);
                    return View(new HomeBL().GetProductByCompanyID(companyID));
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { message = "Bad Request..." });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }

        public ActionResult NewProducts()
        {
            return View(new HomeBL().GetNewProducts());
        }

        public ActionResult FeaturedProducts(int? page)
        {
            try
            {
                var products = new HomeBL().GetAllProducts();
                var pager = new Pager(products.Count(), page);
                var model = new FeaturedProductsViewModel()
                {
                    Pager = pager,
                    Products = products.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize)
                };
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }

        public ActionResult NewItems(int? categoryID)
        {
            try
            {
                if (categoryID!=null)
                {
                    return View(new HomeBL().GetNewTablets(categoryID));
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { message = "Something went wrong While processing your request. Please check your request." });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { message = "Something went wrong While processing your request. Please check your request." });
            }

        }
     
        public ActionResult ProductBySubCategory(int? categoryID,int? subCategoryID)
        {
            try
            {
                if (categoryID !=null && subCategoryID!=null)
                {
                    return View(new HomeBL().GetNewTabletsByCategory(categoryID, subCategoryID));
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { message = "Something went wrong While processing your request. Please check your request." });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }

        public ActionResult OldProducts()
        {
            try
            {
                return View(new HomeBL().GetOldItems());
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }

        public ActionResult OldItems(int? categoryID)
        {
            try
            {
                if (categoryID != null)
                {
                    return View(new HomeBL().GetOldItemsByCategoryID(categoryID));
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { message = "Something went wrong While processing your request. Please check your request." });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }

        public ActionResult ProductsByCategories(int? categoryID, int? subCategoryID)
        {
            try
            {
                if (categoryID != null && subCategoryID != null)
                {
                    return View(new HomeBL().GetOldItemsByCategories(categoryID, subCategoryID));
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { message = "Something went wrong While processing your request. Please check your request." });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { message = "Something went wrong While processing your request. Please check your request." });
            }
        }
    }
}