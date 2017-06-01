using MobileMart.DB.Model;
using MobileMart.DB.ViewModel;
using MobileMart.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MobileMart.BL
{
    public class HomeBL
    {
        public List<IndexViewModel> GetHomeDetails()
        {
            IProductRepository productRepo = new ProductRepository();
            IShopRepository Shop = new ShopRepository();
            IOwnerRepository Owner = new OwnerRepository();

            List<IndexViewModel> list = new List<IndexViewModel>();

            var products = productRepo.Get().ToList();
            var shops = Shop.Get().ToList();
            var owners = Owner.Get().ToList();

            foreach (var item in products.Where(s=> s.IsActive == true))
            {
                IndexViewModel viewmodel = new IndexViewModel();
                viewmodel.ProductID = item.ProductID;
                viewmodel.ProductName = item.ProductName;
                viewmodel.Quantity = item.Quantity;
                viewmodel.OwnerID = owners.FirstOrDefault().OwnerID;
                viewmodel.OwnerName = owners.FirstOrDefault().OwnerName;
                viewmodel.ShopID = shops.FirstOrDefault().ShopID;
                viewmodel.ShopName = shops.FirstOrDefault().ShopName;
                viewmodel.ProductImage = item.ProductImage1;
                viewmodel.Price = item.Price;
                viewmodel.ProductDetail = item.ProductDetails;
                viewmodel.Company = item.Company.CompanyName;
                viewmodel.IsActive = item.IsActive;
                viewmodel.IsFeature = item.IsFeature;
                viewmodel.CreatedOn = item.CreatedOn;
                list.Add(viewmodel);
            }
            return list;
        }

        public IEnumerable<IndexViewModel> GetAllProducts()
        {
            IProductRepository productRepo = new ProductRepository();
            IShopRepository Shop = new ShopRepository();
            IOwnerRepository Owner = new OwnerRepository();

            List<IndexViewModel> list = new List<IndexViewModel>();

            var products = productRepo.Get().ToList();
            var shops = Shop.Get().ToList();
            var owners = Owner.Get().ToList();

            foreach (var item in products.Where(s => s.IsActive == true))
            {
                IndexViewModel viewmodel = new IndexViewModel();
                viewmodel.ProductID = item.ProductID;
                viewmodel.ProductName = item.ProductName;
                viewmodel.OwnerID = owners.FirstOrDefault().OwnerID;
                viewmodel.OwnerName = owners.FirstOrDefault().OwnerName;
                viewmodel.ShopID = shops.FirstOrDefault().ShopID;
                viewmodel.ShopName = shops.FirstOrDefault().ShopName;
                viewmodel.ProductImage = item.ProductImage1;
                viewmodel.Price = item.Price;
                viewmodel.ProductDetail = item.ProductDetails;
                viewmodel.Company = item.Company.CompanyName;
                viewmodel.IsActive = item.IsActive;
                viewmodel.IsFeature = item.IsFeature;
                viewmodel.CreatedOn = item.CreatedOn;
                list.Add(viewmodel);
            }
            return list;
        }

        public ProductDetailViewModel Detail(int? ProductID)
        {
            IProductRepository Product = new ProductRepository();
            IShopRepository Shop = new ShopRepository();

            var products = Product.Get().Where(p => p.ProductID == ProductID).FirstOrDefault();
            var shops = Shop.Get().FirstOrDefault();

            ProductDetailViewModel viewmodel = new ProductDetailViewModel();
            viewmodel.ProductID = products.ProductID;
            viewmodel.ProductName = products.ProductName;
            viewmodel.Price = products.Price;
            viewmodel.ProductDetail = products.ProductDetails;
            viewmodel.ProductImage1 = products.ProductImage1;
            viewmodel.ProductImage2 = products.ProductImage2;
            viewmodel.ProductImage3 = products.ProductImage3;
            viewmodel.ProductImage4 = products.ProductImage4;
            viewmodel.Quantity = products.Quantity;
            return viewmodel;

        }

        public DisplayShopViewModel ShopDetails(int? ShopID)
        {
            IShopRepository shopRepo = new ShopRepository();
            IOwnerRepository OwnerRepo = new OwnerRepository();
            IProductRepository prodRepo = new ProductRepository();

            var shop = shopRepo.Get().FirstOrDefault();
            var owner = OwnerRepo.Get().FirstOrDefault();
            var product = prodRepo.Get().FirstOrDefault();

            DisplayShopViewModel viewModel = new DisplayShopViewModel();

            viewModel.OwnerID = owner.OwnerID;
            viewModel.OwnerName = owner.OwnerName;
            viewModel.OwnerProfilePath = owner.OwnerPicture;
            viewModel.Contact = owner.OwnerContact;

            viewModel.ShopID = shop.ShopID;
            viewModel.ShopName = shop.ShopName;
            viewModel.ShopAddress = shop.ShopAddress;
            viewModel.ShopCreatedOn = shop.CreatedOn;

            return viewModel;
        }

        public List<DisplayProductViewModel> ShopProducts(int? ShopID)
        {
            IShopRepository shopRepo = new ShopRepository();
            IProductRepository prodRepo = new ProductRepository();
            var shop = shopRepo.Get().Where(s => s.ShopID == ShopID);
            var product = prodRepo.Get().ToList();

            List<DisplayProductViewModel> list = new List<DisplayProductViewModel>();

            foreach (var item in product)
            {
                DisplayProductViewModel vm = new DisplayProductViewModel();
                vm.ProductID = item.ProductID;
                vm.ProductName = item.ProductName;
                vm.ProductImage1 = item.ProductImage1;
                vm.price = item.Price;
                list.Add(vm);
            }
            return list;

        }

        public CustomerDetailViewModel GetCustomerDetailByID(int? customerID)
        {
            var customerRepo = new CustomerRepository();
            var customer = customerRepo.GetCustomerByID(customerID);
            var viewModel = new CustomerDetailViewModel();
            viewModel.FirstName = customer.FirstName;
            viewModel.LastName = customer.LastName;
            viewModel.Address1 = customer.Address1;
            viewModel.DOB = customer.DOB;
            viewModel.Email = customer.Email;
            viewModel.CreatedON = customer.CreatedOn;
            viewModel.City = customer.city.name;
            viewModel.State = customer.city.state.name;
            viewModel.Country = customer.city.state.country.name;
            viewModel.Mobile = customer.PhoneNo;
            viewModel.ProfilePicturePath = customer.ProfilePicture;
            return viewModel;
        }

        public IEnumerable<country> GetCountries()
        {
            ICountryRepository countryRepo = new CountryRepository();
            return countryRepo.Get();
        }

        public void CreateCustomer(CustomerRegisterViewModel model)
        {
            var customerRepo = new CustomerRepository();
            Customer customer = new Customer();

            var fileName = Path.GetFileNameWithoutExtension(model.ProfilePicture.FileName);
            fileName += DateTime.Now.Ticks + Path.GetExtension(model.ProfilePicture.FileName);
            var basePath = "~/Content/Customer/" + model.AspNetUserID + "/ProfilePhoto/";
            var path = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName);
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/Customer/" + model.AspNetUserID + "/ProfilePhoto/"));
            model.ProfilePicture.SaveAs(path);

            customer.ProfilePicture = basePath + fileName;
            customer.AspNetUserID = model.AspNetUserID;
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Address1 = model.Address;
            customer.Email = model.Email;
            customer.CreatedOn = DateTime.Now;
            customer.PhoneNo = model.Mobile;
            customer.IsActive = true;
            customer.CityID = model.City;
            customer.DOB = model.DOB;
            var customerID = customerRepo.InsertAndGetID(customer);

            var customerNR = new CustomerNotificationRepository();
            var customerNE = new CustomerNotification();
            customerNE.CusotmerID = customerID;
            customerNE.Description = "New Customer added.";
            customerNE.IsSeen = false;
            customerNE.URL = "/Notification/CustomerDetail?customerID=" + customerID;
            customerNE.Timestamp = DateTime.Now;
            customerNR.Insert(customerNE);
        }

        public void UpdateCustomerProfile(CustomerDetailViewModel viewModel)
        {
            var customerRepo = new CustomerRepository();
            var entity = new Customer();
            if (viewModel.ProfilePicture != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(viewModel.ProfilePicture.FileName);
                fileName += DateTime.Now.Ticks + Path.GetExtension(viewModel.ProfilePicture.FileName);
                var basePath = "~/Content/Customer/" + viewModel.AspNetUserID + "/ProfilePhoto/";
                var path = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName);
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/Customer/" + viewModel.AspNetUserID + "/ProfilePhoto/"));
                viewModel.ProfilePicture.SaveAs(path);
                entity.ProfilePicture = basePath + fileName;
            }
            else
            {
                entity.ProfilePicture = viewModel.ProfilePicturePath;
            }
            entity.FirstName = viewModel.FirstName;
            entity.LastName = viewModel.LastName;
            entity.Address1 = viewModel.Address1;
            entity.CityID = viewModel.CityID;
            entity.DOB = viewModel.DOB;
            entity.PhoneNo = viewModel.Mobile;
            entity.CustomerID = viewModel.CustomerID;

            customerRepo.Edit(entity);
        }

        public IEnumerable<DisplayOrderViewModel> GetMyOrder(int? customerID)
        {
            var orderRepo = new OrderRepository();
            var orders = orderRepo.GetByCustomerID(customerID);
            return orders.Select(s => new DisplayOrderViewModel
            {
                CreatedOn = s.CreatedOn,
                OrderID = s.OrderID,
                CustomerName = s.Customer.FirstName,
                Shipping = s.Shipping,
                SubTotal = s.SubTotal,
                Tax = s.Tax,
                Total = s.Total,
            });
        }

        public void AddToWishList(int? productID , int? customerID)
        {
            var wishListRepository = new WishListRepository();
            var entity = new WishList();
            entity.ProductID = productID;
            entity.CustomerID = customerID;
            wishListRepository.Insert(entity);
        }
        public IEnumerable<DisplayWishListViewModel> GetMyWishList(int? customerID)
        {
            var wishListRepository = new WishListRepository();
            var wishList = wishListRepository.Get();
            return wishList.Where(w=> w.CustomerID == customerID).Select(s => new DisplayWishListViewModel
            {
                CustomerID = s.CustomerID,
                Product = GetProductByID(s.ProductID),
                ProductID = s.ProductID,
                WishListID = s.WishListID
            });
        }

        public ProductDetailViewModel GetProductByID(int? productID)
        {
            IProductRepository productRepo = new ProductRepository();
            ProductDetailViewModel viewmodel = new ProductDetailViewModel();
            var item = productRepo.GetProductByID(productID);
            viewmodel.ProductName = item.ProductName;
            viewmodel.ProductImage1 = item.ProductImage1;
            viewmodel.ProductImage2 = item.ProductImage2;
            viewmodel.ProductImage3 = item.ProductImage3;
            viewmodel.ProductImage4 = item.ProductImage4;
            viewmodel.ProductDetail = item.ProductDetails;
            viewmodel.Color = item.ProductColor;
            viewmodel.Price = item.Price;
            return viewmodel;
        }

        public IEnumerable<CompanyDetailViewModel> GetCompanies()
        {
            return new CompanyRepository().GetCompany().Select(s=> new CompanyDetailViewModel
            {
                CompanyId = s.CompanyID,
                CompanyName = s.CompanyName,
                Logo = s.CompanyLogo,
            }); 
        }

        public IEnumerable<IndexViewModel> GetProductByCompanyID(int? companyID)
        {
            return new ProductRepository().Get().Where(s => s.CompanyID == companyID).Select(w => new IndexViewModel
            {
                Category = w.Category.CategoryName,
                Color = w.ProductColor,
                Company = w.Company.CompanyName,
                IsActive = w.IsActive,
                IsOld = w.IsOld,
                Price = w.Price,
                ProductDetail = w.ProductDetails,
                ProductID = w.ProductID,
                ProductImage = w.ProductImage1,
                ProductName = w.ProductName,
                ShopID = w.Supplier.ShopID,
                ShopName = w.Supplier.Shop.ShopName
            });
        }

        public IEnumerable<IndexViewModel> GetNewProducts()
        {
            return new ProductRepository().Get().Where(s => s.IsOld == false && s.IsActive == true &&  s.CreatedOn >= DateTime.Now.AddDays(-7) && s.Quantity > 0).Select(w => new IndexViewModel
            {
                Category = w.Category.CategoryName,
                Color = w.ProductColor,
                Company = w.Company.CompanyName,
                IsActive = w.IsActive,
                IsOld = w.IsOld,
                Price = w.Price,
                ProductDetail = w.ProductDetails,
                ProductID = w.ProductID,
                ProductImage = w.ProductImage1,
                ProductName = w.ProductName,
                ShopID = w.Supplier.ShopID
            });
        }

        public CompanyDetailViewModel GetCompanyByID(int? companyID)
        {
            var company =  new CompanyRepository().GetByID(companyID);
            var viewModel = new CompanyDetailViewModel();
            viewModel.CompanyId = company.CompanyID;
            viewModel.CompanyName = company.CompanyName;
            viewModel.Logo = company.CompanyLogo;
            return viewModel;
        }

        public IEnumerable<IndexViewModel> GetNewTablets(int? categoryID)
        {
            return new ProductRepository().Get().Where(s => s.SubCategoryID == categoryID && s.IsOld == false && s.IsActive == true).Select(w => new IndexViewModel
            {
                Category = w.Category.CategoryName,
                Color = w.ProductColor,
                Company = w.Company.CompanyName,
                IsActive = w.IsActive,
                IsOld = w.IsOld,
                Price = w.Price,
                ProductDetail = w.ProductDetails,
                ProductID = w.ProductID,
                ProductImage = w.ProductImage1,
                ProductName = w.ProductName,
                ShopID = w.Supplier.ShopID
            });
        }
        
        public IEnumerable<IndexViewModel> GetNewTabletsByCategory(int? categoryID, int? subCategoryID)
        {
            return new ProductRepository().Get().Where(s => s.CategoryID == categoryID && s.SubCategoryID == subCategoryID && s.IsOld == false && s.IsActive == true).Select(w => new IndexViewModel
            {
                Category = w.Category.CategoryName,
                Color = w.ProductColor,
                Company = w.Company.CompanyName,
                IsActive = w.IsActive,
                IsOld = w.IsOld,
                Price = w.Price,
                ProductDetail = w.ProductDetails,
                ProductID = w.ProductID,
                ProductImage = w.ProductImage1,
                ProductName = w.ProductName,
                ShopID = w.Supplier.ShopID
            });
        }

        public IEnumerable<IndexViewModel> GetOldItems()
        {
            return new ProductRepository().Get().Where(s => s.IsOld == true && s.IsActive == true).Select(w => new IndexViewModel
            {
                Category = w.Category.CategoryName,
                Color = w.ProductColor,
                Company = w.Company.CompanyName,
                IsActive = w.IsActive,
                IsOld = w.IsOld,
                Price = w.Price,
                ProductDetail = w.ProductDetails,
                ProductID = w.ProductID,
                ProductImage = w.ProductImage1,
                ProductName = w.ProductName,
                ShopID = w.Supplier.ShopID
            });
        }

        public IEnumerable<IndexViewModel> GetOldItemsByCategoryID(int? categoryID)
        {
            return new ProductRepository().Get().Where(s =>s.SubCategoryID==categoryID && s.IsOld == true && s.IsActive == true).Select(w => new IndexViewModel
            {
                Category = w.Category.CategoryName,
                Color = w.ProductColor,
                Company = w.Company.CompanyName,
                IsActive = w.IsActive,
                IsOld = w.IsOld,
                Price = w.Price,
                ProductDetail = w.ProductDetails,
                ProductID = w.ProductID,
                ProductImage = w.ProductImage1,
                ProductName = w.ProductName,
                ShopID = w.Supplier.ShopID
            });
        }

        public IEnumerable<IndexViewModel> GetOldItemsByCategories(int? categoryID, int? subCategoryID)
        {
            return new ProductRepository().Get().Where(s => s.CategoryID == categoryID && s.SubCategoryID == subCategoryID && s.IsOld == true && s.IsActive == true).Select(w => new IndexViewModel
            {
                Category = w.Category.CategoryName,
                Color = w.ProductColor,
                Company = w.Company.CompanyName,
                IsActive = w.IsActive,
                IsOld = w.IsOld,
                Price = w.Price,
                ProductDetail = w.ProductDetails,
                ProductID = w.ProductID,
                ProductImage = w.ProductImage1,
                ProductName = w.ProductName,
                ShopID = w.Supplier.ShopID
            });
        }
        
    }
}
