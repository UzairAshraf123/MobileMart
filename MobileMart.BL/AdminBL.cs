using MobileMart.DB.ViewModel;
using MobileMart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;
using System.IO;
using System.Web;

namespace MobileMart.BL
{
    public class AdminBL
    {
        public void CreateOwner(CreateOwnerViewModel model)
        {
            var fileName = Path.GetFileNameWithoutExtension(model.ProfilePhotoPath.FileName);
            fileName += DateTime.Now.Ticks + Path.GetExtension(model.ProfilePhotoPath.FileName);
            var basePath = "~/Content/ShopOwner/" + model.UserID + "/Profile/Images/";
            var path = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName);
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/ShopOwner/" + model.UserID + "/Profile/Images/"));
            model.ProfilePhotoPath.SaveAs(path);

            IOwnerRepository ownerRepo = new OwnerRepository();
            Owner entity = new Owner();
            entity.AspNetUserID = model.UserID;
            entity.OwnerName = model.OwnerName;
            entity.OwnerContact = model.Mobile;
            entity.OwnerPicture = basePath + fileName;
            entity.CreatedOn = model.CreatedOn;
            ownerRepo.Insert(entity);
        }

        public void CreateShop(CreateShopViewModel model)
        {
            IShopRepository ownerRepo = new ShopRepository();
            Shop entity = new Shop();

            var fileName = Path.GetFileNameWithoutExtension(model.ShopLogo.FileName);
            fileName += DateTime.Now.Ticks + Path.GetExtension(model.ShopLogo.FileName);
            var basePath = "~/Content/ShopOwner/" + model.UserID+"/"+ model.OwnerID +"/ShopLogo/";
            var path = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName);
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/ShopOwner/" + model.UserID + "/" + model.OwnerID + "/ShopLogo/"));
            model.ShopLogo.SaveAs(path);

            entity.OwnerID = model.OwnerID;
            entity.ShopName = model.ShopName;
            entity.ShopAddress = model.ShopAddress;
            entity.ShopLogo = basePath + fileName;
            entity.CountryID = model.Country;
            entity.StateID = model.State;
            entity.CityID = model.City;
            entity.CreatedOn = model.CreatedOn;
            var shopID = ownerRepo.InsertAndGetID(entity);

            var shopNR = new ShopNotificationRepository();
            var shopNE = new ShopNotification();

            shopNE.ShopID = shopID;
            shopNE.Description = "New Shop has been added.";
            shopNE.IsSeen = false;
            shopNE.Timestamp = DateTime.Now;
            shopNE.URL = "/Notification/ShopDetail?shopID=" + shopID;
            shopNR.Insert(shopNE);
        }

        public int GetOwnerIDByUserID(string userID)
        {
            IOwnerRepository ownerRepo = new OwnerRepository();
            return ownerRepo.GetOwnerIDByUserID(userID);
        }

        public IEnumerable<country> GetCountries()
        {
            ICountryRepository countryRepo = new CountryRepository();
            return countryRepo.Get();
        }

        public IEnumerable<state> GetStatesByCountryId(int id)
        {
            IStateRepository stateRepo = new StateRepository();
            return stateRepo.Get().Where(s => s.country_id == id).ToList();
        }

        public IEnumerable<city> GetCitiesByStateId(int id)
        {
            ICityRepository cityRepo = new CityRepository();
            return cityRepo.Get().Where(s => s.state_id == id).ToList();
        }

        public DisplayShopViewModel ShopAndOwnerByOwnerID(int ownerID)
        {
            IShopRepository shopRepo = new ShopRepository();
            IOwnerRepository ownerRepo = new OwnerRepository();
            IProductRepository product = new ProductRepository();
            var owner = ownerRepo.Get().Where(s => s.OwnerID == ownerID).FirstOrDefault();
            var shop = shopRepo.Get().Where(s => s.OwnerID == ownerID).FirstOrDefault();
            DisplayShopViewModel viewmodel = new DisplayShopViewModel();

            viewmodel.OwnerID = owner.OwnerID;
            viewmodel.OwnerName = owner.OwnerName;
            viewmodel.Contact = owner.OwnerContact;
            viewmodel.OwnerCreatedOn = owner.CreatedOn;
            viewmodel.OwnerProfilePath = owner.OwnerPicture;

            viewmodel.ShopID = shop.ShopID;
            viewmodel.ShopName = shop.ShopName;
            viewmodel.ShopAddress = shop.ShopAddress;
            viewmodel.ShopLogo = shop.ShopLogo;
            viewmodel.ShopCreatedOn = shop.CreatedOn;

            return viewmodel;
        }

        public CreateShopViewModel GetShopByOwnerID(int? ownerID)
        {
            IShopRepository shopRepo = new ShopRepository();
            var shop = shopRepo.GetShopByOwnerID(ownerID);
            CreateShopViewModel viewModel = new CreateShopViewModel();
            viewModel.OwnerID = shop.OwnerID;
            viewModel.ShopID = shop.ShopID;
            viewModel.ShopName = shop.ShopName;
            viewModel.ShopAddress = shop.ShopAddress;
            viewModel.GetLogo = shop.ShopLogo;
            viewModel.Country = shop.CountryID;
            viewModel.CreatedOn = shop.CreatedOn;

            return viewModel;
        }
        
        public List<DisplayShopViewModel> GetAllShops()
        {
            IShopRepository shopRepo = new ShopRepository();
            IOwnerRepository ownerRepo = new OwnerRepository();
            IProductRepository productRepo = new ProductRepository();
            var shop = shopRepo.Get();

            List<DisplayShopViewModel> list = new List<DisplayShopViewModel>();
            foreach (var item in shop)
            {
                DisplayShopViewModel viewmodel = new DisplayShopViewModel();
                viewmodel.UserID = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).AspNetUserID;
                viewmodel.OwnerID = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).OwnerID;
                viewmodel.OwnerName = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).OwnerName;
                viewmodel.OwnerProfilePath = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).OwnerPicture;
                viewmodel.Contact = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).OwnerContact;
                viewmodel.OwnerCreatedOn = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).CreatedOn;

                viewmodel.ShopID = item.ShopID;
                viewmodel.ShopName = item.ShopName;
                viewmodel.ShopAddress = item.ShopAddress;
                viewmodel.ShopLogo = item.ShopLogo;
                viewmodel.ShopCreatedOn = item.CreatedOn;
                viewmodel.productcount = productRepo.GetProduct(item.ShopID).Count();

                list.Add(viewmodel);
            }
            return list;
        }

        public IEnumerable<DisplayShopViewModel> GetAllShopsByRange(DateTime from, DateTime to)
        {
            var shop = new ShopRepository().Get().Where(s=>s.CreatedOn>=from && s.CreatedOn <= to);
            IEnumerable<DisplayShopViewModel> shopList = shop.Select(s => new DisplayShopViewModel
            {
                OwnerName = s.Owner.OwnerName,
                OwnerID = Convert.ToInt32(s.OwnerID),
                OwnerCreatedOn = s.Owner.CreatedOn,
                Contact = s.Owner.OwnerContact,
                UserID = s.Owner.AspNetUserID,
                OwnerProfilePath = s.Owner.OwnerPicture,
                ShopID = s.ShopID,
                ShopName = s.ShopName,
                ShopAddress = s.ShopAddress,
                ShopCreatedOn = s.CreatedOn,
                ShopLogo = s.ShopLogo,
                productcount =new ProductRepository().GetProduct(s.ShopID).Count()
            });
            return shopList;
        }

        public IEnumerable<DisplayAllCustomers> GetAllCustomers()
        {
            var customerRepo = new CustomerRepository();
            IEnumerable<Customer> customers = customerRepo.Get();
            List<DisplayAllCustomers> customerList = new List<DisplayAllCustomers>();
            foreach (var item in customers)
            {
                DisplayAllCustomers viewModel = new DisplayAllCustomers();
                viewModel.CustomerID = item.CustomerID;
                viewModel.FirstName = item.FirstName;
                viewModel.LastName = item.LastName;
                viewModel.Email = item.Email;
                viewModel.DOB = item.DOB;
                viewModel.ProfilePicturePath = item.ProfilePicture;
                viewModel.CreatedON = item.CreatedOn;
                viewModel.Address1 = item.Address1;
                viewModel.Address2 = item.Address2;
                viewModel.Mobile = item.PhoneNo;
                var city = GetCityByID(item.CityID);
                viewModel.City = city.name;
                var state = GetStateByID(city.state_id);
                viewModel.State = state.name;
                var country = GetCountryByID(state.country_id);
                viewModel.Country = country.name;
                viewModel.AspID = item.AspNetUserID;
                customerList.Add(viewModel);
            }
            return customerList;
        }

        public IEnumerable<DisplayAllCustomers> GetCustomersByRange(DateTime from, DateTime to)
        {
            return new CustomerRepository().Get().Where(s=> s.CreatedOn >= from && s.CreatedOn<= s.CreatedOn).Select(i=> new DisplayAllCustomers
            {
                CustomerID = i.CustomerID,
                Address1 = i.Address1,
                AspID = i.AspNetUserID,
                Mobile=i.PhoneNo,
                City = i.city.name,
                State = i.city.state.name,
                Country = i.city.state.country.name,
                CreatedON =i.CreatedOn,
                DOB=i.DOB,
                Email = i.Email,
                FirstName = i.FirstName,
                LastName = i.LastName,
                ProfilePicturePath = i.ProfilePicture
            });
        }

        private city GetCityByID(int? cityID)
        {
            ICityRepository cityRepo = new CityRepository();
            return cityRepo.GetCityByID(cityID);
        }

        private state GetStateByID(int? state)
        {
            IStateRepository stateRepo = new StateRepository();
            return stateRepo.GetStateByID(state);
        }

        private country GetCountryByID (int? countryID)
        {
            ICountryRepository countryRepo = new CountryRepository();
            return countryRepo.GetCountryByID(countryID); 
        }

        public void DeleteUser(string userID)
        {
            IAspNetUser Repo = new Repository.AspNetUser();
            Repo.DeleteUser(userID);            
        }

        public EditShopViewModel GetOwnerByID(int? ownerID)
        {
            IOwnerRepository ownerRepo = new OwnerRepository();
            EditShopViewModel ownerVM = new EditShopViewModel();

            var owner = ownerRepo.GetOwnerByID(ownerID);
            ownerVM.UserID = owner.AspNetUserID;
            ownerVM.OwnerName = owner.OwnerName;
            ownerVM.Mobile = owner.OwnerContact;
            ownerVM.ProfilePhotoPath = owner.OwnerPicture;
            return ownerVM;
        }

        public void InsertEditedOwner(EditShopViewModel viewModel)
        {
            IOwnerRepository OwnerRepo = new OwnerRepository();
            Owner owner = new Owner();
            if (viewModel.ProfilePhoto != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(viewModel.ProfilePhoto.FileName);
                fileName += DateTime.Now.Ticks + Path.GetExtension(viewModel.ProfilePhoto.FileName);
                var basePath = "~/Content/ShopOwner/" + viewModel.UserID + "/Profile/Images/";
                var path = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName);
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/ShopOwner/" + viewModel.UserID + "/Profile/Images/"));
                viewModel.ProfilePhoto.SaveAs(path);
                owner.OwnerPicture = basePath + fileName;
            }
            else
            {
                owner.OwnerPicture = viewModel.ProfilePhotoPath;
            }
            owner.OwnerID = viewModel.OwnerID;
            owner.OwnerName = viewModel.OwnerName;
            owner.OwnerContact = viewModel.Mobile;
            owner.CreatedOn = viewModel.CreatedOn;
            OwnerRepo.Update(owner);
        }

        public void UpdateOwner(DisplayShopViewModel viewModel)
        {
            IOwnerRepository OwnerRepo = new OwnerRepository();
            Owner owner = new Owner();
            if (viewModel.OwnerProfilePhoto != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(viewModel.OwnerProfilePhoto.FileName);
                fileName += DateTime.Now.Ticks + Path.GetExtension(viewModel.OwnerProfilePhoto.FileName);
                var basePath = "~/Content/ShopOwner/" + viewModel.UserID + "/Profile/Images/";
                var path = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName);
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/ShopOwner/" + viewModel.UserID + "/Profile/Images/"));
                viewModel.OwnerProfilePhoto.SaveAs(path);
                owner.OwnerPicture = basePath + fileName;
            }
            else
            {
                owner.OwnerPicture = viewModel.OwnerProfilePath;
            }
            owner.OwnerID = viewModel.OwnerID;
            owner.OwnerName = viewModel.OwnerName;
            owner.OwnerContact = viewModel.Contact;
            owner.CreatedOn = viewModel.OwnerCreatedOn;
            OwnerRepo.Update(owner);
        }
        public void UpdateEditedShop(CreateShopViewModel viewModel)
        {
            IShopRepository shopRepo = new ShopRepository();
            Shop shopEntity = new Shop();
            if (viewModel.ShopLogo != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(viewModel.ShopLogo.FileName);
                fileName += DateTime.Now.Ticks + Path.GetExtension(viewModel.ShopLogo.FileName);
                var basePath = "~/Content/ShopOwner/" + viewModel.UserID + "/" + viewModel.OwnerID + "/ShopLogo/";
                var path = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName);
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/ShopOwner/" + viewModel.UserID + "/" + viewModel.OwnerID + "/ShopLogo/"));
                viewModel.ShopLogo.SaveAs(path);
                shopEntity.ShopLogo = basePath + fileName;
            }
            else
            {
                shopEntity.ShopLogo = viewModel.GetLogo;
            }
            shopEntity.OwnerID = viewModel.OwnerID;
            shopEntity.ShopID = viewModel.ShopID;
            shopEntity.ShopName = viewModel.ShopName;
            shopEntity.ShopAddress = viewModel.ShopAddress;
            shopEntity.CountryID = viewModel.Country;
            shopEntity.StateID = viewModel.State;
            shopEntity.CityID = viewModel.City;
            shopEntity.CreatedOn = viewModel.CreatedOn;
            shopRepo.Update(shopEntity);
        }

        public int GetCustomerCount()
        {
            CustomerRepository Customer = new CustomerRepository();
            return Customer.Get().Count();
        }

        public int GetShopCount()
        {
            IShopRepository repo = new ShopRepository();
            return repo.Get().Count();
        }

        public int GetProductCount()
        {
            IProductRepository repo = new ProductRepository();
            return repo.Get().Count();
        }

        public string AddCompany(AddCompanyViewModel viewmodel)
        {
            ICompanyRepository companyRepo = new CompanyRepository();
            Company entity = new Company();
            var fileName1 = Path.GetFileNameWithoutExtension(viewmodel.CompanyLogo.FileName);
            fileName1 += DateTime.Now.Ticks + Path.GetExtension(viewmodel.CompanyLogo.FileName);
            var basePath = "~/Content/Campanies/" + viewmodel.CompanyName + "/Logo/";
            var path1 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName1);
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/Campanies/" + viewmodel.CompanyName + "/Logo/"));
            viewmodel.CompanyLogo.SaveAs(path1);

            entity.CompanyName = viewmodel.CompanyName;
            entity.CompanyLogo = basePath + fileName1;
            companyRepo.insert(entity);

            return "Company has been added successfully..";
        }

        public void AddCategory(AddCategoryViewModel viewmodel)
        {
            ICategoryRepository categoryrepo = new CategoryRepository();
            Category entity = new Category();
            if (viewmodel.CategoryImage != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(viewmodel.CategoryImage.FileName);
                fileName += DateTime.Now.Ticks + Path.GetExtension(viewmodel.CategoryImage.FileName);
                var basePath = "~/Content/Admin/Category/Images/";
                var path = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName);
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/Category/Product/Images/"));
                entity.CategoryImage = basePath + fileName;
            }
            entity.CategoryName = viewmodel.CategoryName;
            categoryrepo.insert(entity);

        }

        //public IEnumerable<DisplayProductViewModel> GetAllProducts()
        //{
        //    var productRepo = new ProductRepository();
        //    return productRepo.Get().Select(s => new DisplayProductViewModel
        //    {
        //        ProductName = s.ProductName,
        //        ProductImage1 = s.ProductImage1,
        //        ProductImage2 = s.ProductImage2,
        //        ProductImage3 = s.ProductImage3,
        //        ProductImage4 = s.ProductImage4,
        //        Color = s.ProductColor,
        //        price = s.Price,
        //        CreatedOn = s.CreatedOn,
        //        ProductDetail = s.ProductDetails
        //    });
        //}

        public IEnumerable<DisplayProductViewModel> GetProducts()
        {
            var productRepo = new ProductRepository();
            var Products = productRepo.Get().ToList();
            return (Allproduct(Products));
        }
        public IEnumerable<DisplayProductViewModel> GetProductsByRange(DateTime from, DateTime to)
        {
            return new ProductRepository().Get().Where(s => s.CreatedOn >= from && s.CreatedOn <= to).Select(w => new DisplayProductViewModel
            {
                CreatedOn = w.CreatedOn,
                Category = w.Category.CategoryName,
                Color = w.ProductColor,
                Company = w.Company.CompanyName,
                IMEI = w.IMEI,
                IsActive = w.IsActive,
                New = w.IsOld,
                price = w.Price,
                ProductDetail = w.ProductDetails,
                ProductID = w.ProductID,
                ProductImage1 = w.ProductImage1,
                ProductImage2 = w.ProductImage2,
                ProductImage3 = w.ProductImage3,
                ProductImage4 = w.ProductImage4,
                ProductName = w.ProductName,
            });
        }
        public IEnumerable<DisplayProductViewModel> GetProductByID(int? shopID)
        {
            var productRepo = new ProductRepository();
            var products = productRepo.GetProduct(shopID).ToList();
            return (Allproduct(products));
        }

        private IEnumerable<DisplayProductViewModel> Allproduct(IEnumerable<Product> products)
        {
            List<DisplayProductViewModel> viewmodellist = new List<DisplayProductViewModel>();
            var categoryRepo = new CategoryRepository();
            var companyRepo = new CompanyRepository();
            foreach (var item in products)
            {
                DisplayProductViewModel viewmodel = new DisplayProductViewModel();
                viewmodel.ProductID = item.ProductID;
                viewmodel.ProductName = item.ProductName;
                viewmodel.ProductImage1 = item.ProductImage1;
                viewmodel.ProductImage2 = item.ProductImage2;
                viewmodel.ProductImage3 = item.ProductImage3;
                viewmodel.ProductImage4 = item.ProductImage4;
                viewmodel.ProductDetail = item.ProductDetails;
                viewmodel.Color = item.ProductColor;
                viewmodel.CreatedOn = item.CreatedOn;
                viewmodel.price = item.Price;
                viewmodel.Company = item.Company.CompanyName;
                viewmodel.Category = item.Category.CategoryName;
                viewmodellist.Add(viewmodel);
            }
            return viewmodellist;

        }

        public string DeleteProduct(int? id)
        {
            var productRepo = new ProductRepository();
            productRepo.delete(id);
            return "product delete";
        }
        
        public void DeleteCustomerByID(string customerID)
        {
            new Repository.AspNetUser().DeleteUser(customerID);
        }

        public bool IsActive(int id)
        {
            var productRepo = new ProductRepository();
            var isactive = productRepo.Get().FirstOrDefault(s => s.ProductID == id).IsActive;
            return (isactive);
        }

        public bool ChangeProductStateTo(int productID, bool IsActive)
        {
            var productRepo = new ProductRepository();
            var entity = productRepo.Get().FirstOrDefault(s => s.ProductID == productID);
            entity.IsActive = IsActive;
            entity.ProductID = productID;
            return productRepo.ChangeActiveStatus(entity);
        }

        public IEnumerable<DisplayOrderViewModel> GetAllOrders()
        {
            var orderRepo = new OrderRepository();
            var customerRepo = new CustomerRepository();
            IEnumerable<DisplayOrderViewModel> orderList = orderRepo.Get().Select(s => new DisplayOrderViewModel
            {
                OrderID = s.OrderID,
                CustomerName = customerRepo.GetCustomerByID(s.CustomerID).FirstName,
                CreatedOn = s.CreatedOn,
                Tax = s.Tax,
                Total = s.Total,
                Shipping = s.Shipping,
                SubTotal = s.SubTotal,
            });
            return orderList;
        }

        public IEnumerable<DisplayOrderViewModel> GetAllOrdersByRange(DateTime from, DateTime to)
        {
            var orderRepo = new OrderRepository();
            var customerRepo = new CustomerRepository();
            IEnumerable<DisplayOrderViewModel> orderList = orderRepo.Get().Where(w => w.CreatedOn >= from && w.CreatedOn <= to).Select(s => new DisplayOrderViewModel
            {
                OrderID = s.OrderID,
                CustomerName = s.Customer.FirstName,
                CreatedOn = s.CreatedOn,
                Tax = s.Tax,
                Total = s.Total,
                Shipping = s.Shipping,
                SubTotal = s.SubTotal,
            });
            return orderList;
        }

        public int GetOrderCount()
        {
            var orderRepo = new OrderRepository();
            return orderRepo.Get().Count();
        }

        public IEnumerable<DisplayOrderDetailViewModel> GetOrderDetailByOrderID(int orderID)
        {
            var orderDetailRepo = new OrderDetailRepository();
            var productRepo = new ProductRepository();
            IEnumerable<DisplayOrderDetailViewModel> orderDetailList = orderDetailRepo.GetByOrderID(orderID).Select(s => new DisplayOrderDetailViewModel
            {
                OrderDetailID = s.OrderDetailID,
                OrderID = s.OrderID,
                Product = productRepo.GetProductByID(s.ProductID).ProductName,
                Quantity = s.Quantity,
                UnitPrice = s.UnitPrice,
            });
            return orderDetailList;
        }

        public DisplayOrderViewModel GetOrderByID(int orderID)
        {
            var orderRepo = new OrderRepository();
            var order = orderRepo.GetByID(orderID);
            var customerRepo = new CustomerRepository();
            var viewModel = new DisplayOrderViewModel();
            viewModel.CustomerName = customerRepo.GetCustomerByID(order.CustomerID).FirstName;
            viewModel.Tax = order.Tax;
            viewModel.Shipping = order.Shipping;
            viewModel.Total = order.Total;
            viewModel.SubTotal = order.SubTotal;
            viewModel.CreatedOn = order.CreatedOn;
            return viewModel;
        }

        public DisplayShopViewModel GetShopByID(int? shopID)
        {
            var shop = new ShopRepository().GetShopByID(shopID);
            return new DisplayShopViewModel()
            {
                ShopAddress = shop.ShopAddress,
                ShopID = shop.ShopID,
                ShopLogo = shop.ShopLogo,
                ShopName = shop.ShopName,
            };
        }
    }
}
