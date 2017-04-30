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
            customerNE.Description = "New Customer has been added.";
            customerNE.IsSeen = false;
            customerNE.URL = "/Notification/CustomerDetail?customerID=" + customerID;
            customerNE.Timestamp = DateTime.Now;
            customerNR.Insert(customerNE);
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
            viewmodel.OwnerProfile = owner.OwnerPicture;

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
            var shop = shopRepo.Get();

            List<DisplayShopViewModel> list = new List<DisplayShopViewModel>();
            foreach (var item in shop)
            {
                DisplayShopViewModel viewmodel = new DisplayShopViewModel();
                viewmodel.UserID = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).AspNetUserID;
                viewmodel.OwnerID = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).OwnerID;
                viewmodel.OwnerName = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).OwnerName;
                viewmodel.OwnerProfile = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).OwnerPicture;
                viewmodel.Contact = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).OwnerContact;
                viewmodel.OwnerCreatedOn = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).CreatedOn;

                viewmodel.ShopID = item.ShopID;
                viewmodel.ShopName = item.ShopName;
                viewmodel.ShopAddress = item.ShopAddress;
                viewmodel.ShopLogo = item.ShopLogo;
                viewmodel.ShopCreatedOn = item.CreatedOn;

                list.Add(viewmodel);
            }
            return list;
        }

        public IEnumerable<DisplayAllCustomers> GetAllCustomers()
        {
            var customerRepo = new CustomerRepository();
            var customers = customerRepo.Get();
            var viewModel = new DisplayAllCustomers();

            List<DisplayAllCustomers> customerList = new List<DisplayAllCustomers>();
            foreach (var item in customers)
            {
                viewModel.CustomerID = item.CustomerID;
                viewModel.FirstName = item.FirstName ;
                viewModel.LastName = item.LastName ;
                viewModel.Email = item.Email ;
                viewModel.DOB = item.DOB ;
                viewModel.ProfilePicturePath = item.ProfilePicture ;
                viewModel.CreatedON = item.CreatedOn ;
                viewModel.Address1 = item.Address1 ;
                viewModel.Address2 = item.Address2 ;
                var city = GetCityByID(item.CityID);
                viewModel.City = city.name ;
                var state = GetStateByID(city.state_id);
                viewModel.State = state.name ;
                var country = GetCountryByID(state.country_id);
                viewModel.Country = country.name ;
                viewModel.AspID = item.AspNetUserID ;
                customerList.Add(viewModel);
            }
            return customerList;
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

        public IEnumerable<Customer> AllCustomers()
        {
            CustomerRepository Customer = new CustomerRepository();
            return Customer.Get().ToList();
        }
        
        public IEnumerable<Shop> AllShops()
        {
            IShopRepository repo = new ShopRepository();
            return repo.Get().ToList();
        }

        public void AddCompany(AddCompanyViewModel viewModel)
        {
            ICompanyRepository companyrepo = new CompanyRepository();
            Company entity = new Company();
            entity.CompanyName = viewModel.CompanyName;
            entity.CompanyLogo = viewModel.CompanyLogo;
            companyrepo.insert(entity);
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
    }
}
