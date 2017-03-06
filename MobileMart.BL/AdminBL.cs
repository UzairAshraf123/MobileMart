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
            ownerRepo.Insert(entity);
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
        public void CreateCustomer(RegisterViewModel model)
        {
            ICustomerRepository customerRepo = new CustomerRepository();
            Customer customer = new Customer();
            customer.ProfilePicture = model.ProfilePicture;
            customer.AspNetUserID = model.AspNetUserID;
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Address1 = model.Address;
            customer.Email = model.Email;
            customer.CreatedOn = model.CreatedOn;
            customer.PhoneNo = model.Mobile;
            customer.DOB = model.DOB;

            customerRepo.Insert(customer);
        }
        public DisplayShopViewModel ShopAndOwnerByOwnerID(int ownerID)
        {
            IShopRepository shopRepo = new ShopRepository();
            IOwnerRepository ownerRepo = new OwnerRepository();
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

        //public string DeleteShop(int? ID)
        //{
        //    IOwnerRepository ownerRepo = new OwnerRepository();
        //    IShopRepository shopRepo = new ShopRepository();

        //    ownerRepo.Delete(ID);
        //    shopRepo.Delete(ID);
        //    return "shop deleted";
        //}
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
        //public EditShopViewModel EditShopView(int? ID)
        //{
        //    IShopRepository shopRepo = new ShopRepository();
        //    var Shop = shopRepo.GetShopByOwnerID(ID);
        //    var EShop = new EditShopViewModel();
        //    EShop.OwnerID = Shop.OwnerID;
        //    EShop.ShopID = Shop.ShopID;
        //    EShop.ShopAddress = Shop.ShopAddress;
        //    EShop.ShopName = Shop.ShopName;
        //    EShop.ShopLogo = Shop.ShopLogo;
        //    return EShop;
        //}

        public void InsertEditedOwner(EditShopViewModel viewModel)
        {
            IOwnerRepository OwnerRepo = new OwnerRepository();
            Owner owner = new Owner();
            owner.OwnerID = viewModel.OwnerID;
            owner.OwnerName = viewModel.OwnerName;
            owner.OwnerPicture = viewModel.ProfilePhotoPath;
            owner.OwnerContact = viewModel.Mobile;
            owner.CreatedOn = viewModel.CreatedOn;
            OwnerRepo.Update(owner);
        }

        public void UpdateEditedShop(CreateShopViewModel viewModel)
        {
            IShopRepository shopRepo = new ShopRepository();
            Shop shopEntity = new Shop();
            shopEntity.OwnerID = viewModel.OwnerID;
            shopEntity.ShopID = viewModel.ShopID;
            shopEntity.ShopName = viewModel.ShopName;
            shopEntity.ShopAddress = viewModel.ShopAddress;
            //shopEntity.ShopLogo = viewModel.ShopLogo;
            shopEntity.CountryID = viewModel.Country;
            shopEntity.StateID = viewModel.State;
            shopEntity.CityID = viewModel.City;
            shopEntity.CreatedOn = viewModel.CreatedOn;
            shopRepo.Update(shopEntity);
        }
    }
}
