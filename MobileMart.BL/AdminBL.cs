using MobileMart.DB.ViewModel;
using MobileMart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;
namespace MobileMart.BL
{
    public class AdminBL
    {
        public void CreateOwner(CreateOwnerViewModel model)
        {
            IOwnerRepository ownerRepo = new OwnerRepository();
            Owner entity = new Owner();
            entity.AspNetUserID = model.UserID;
            entity.OwnerName = model.OwnerName;
            entity.OwnerContact = model.Mobile;
            entity.OwnerPicture = model.ProfilePhotoPath;
            entity.CreatedOn = model.CreatedOn;
            ownerRepo.Insert(entity);
        }

        public void CreateShop(CreateShopViewModel model)
        {
            IShopRepository ownerRepo = new ShopRepository();
            Shop entity = new Shop();
            entity.OwnerID = model.OwnerID;
            entity.ShopName = model.ShopName;
            entity.ShopAddress = model.ShopAddress;
            entity.ShopLogo = model.ShopLogo;
            entity.CountryID = model.Country;
            entity.StateID = model.State;
            entity.CityID = model.City;
            entity.CreatedOn = model.CreatedOn;
            ownerRepo.Insert(entity);
        }

        public int GetOwnerByUserID(string userID)
        {
            IOwnerRepository ownerRepo = new OwnerRepository();
            var owner = ownerRepo.Get().Where(s => s.AspNetUserID == userID).Select(s => s.OwnerID).FirstOrDefault();
            return owner;
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
        public DisplayShopViewModel GetShopByOwnerID(int ownerID)
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
            viewmodel.Profile = owner.OwnerPicture;

            viewmodel.ShopID = shop.ShopID;
            viewmodel.ShopName = shop.ShopName;
            viewmodel.ShopAddress = shop.ShopAddress;
            viewmodel.ShopLogo = shop.ShopLogo;
            viewmodel.ShopCreatedOn = shop.CreatedOn;

            return viewmodel;
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
                viewmodel.OwnerID = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).OwnerID;
                viewmodel.OwnerName = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).OwnerName;

               // viewmodel.Contact = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).OwnerContact;
               // viewmodel.OwnerCreatedOn = ownerRepo.Get().FirstOrDefault(s => s.OwnerID == item.OwnerID).CreatedOn;

                viewmodel.ShopID = item.ShopID;
                viewmodel.ShopName = item.ShopName;
                viewmodel.ShopAddress = item.ShopAddress;
                viewmodel.ShopLogo = item.ShopLogo;
                viewmodel.ShopCreatedOn = item.CreatedOn;

                list.Add(viewmodel);
            }

            return list;


            }

        public string DeleteShop(int? ID)
        {
            IOwnerRepository ownerRepo = new OwnerRepository();
            IShopRepository shopRepo = new ShopRepository();

            ownerRepo.Delete(ID);
            shopRepo.Delete(ID);
            return "shop deleted";


        }
        public EditShopViewModel EditShopView(int? ID)
        {
            IShopRepository shopRepo = new ShopRepository();
            var Shop = shopRepo.GetShopByID(ID);
            var EShop = new EditShopViewModel();
            EShop.OwnerID = Shop.OwnerID;
            EShop.ShopID = Shop.ShopID;
            EShop.ShopAddress = Shop.ShopAddress;
            EShop.ShopName = Shop.ShopName;
            EShop.ShopLogo = Shop.ShopLogo;

            return EShop;
        }

        public string EditShop (EditShopViewModel ViewModel)
        {
            IOwnerRepository OwnerRepo = new OwnerRepository();
            IShopRepository ShopRepo = new ShopRepository();

            Shop shop = new Shop();
            Owner owner = new Owner();
            owner.OwnerID = ViewModel.OwnerID;
            owner.OwnerName = ViewModel.OwnerName;
            owner.OwnerPicture = ViewModel.ProfilePhotoPath;
            owner.OwnerContact = ViewModel.Mobile;
            

            shop.ShopID = ViewModel.ShopID;
            shop.ShopName = ViewModel.ShopName;
            shop.ShopAddress = ViewModel.ShopAddress;
            shop.ShopLogo = ViewModel.ShopLogo;
            shop.CountryID = ViewModel.Country;
            shop.StateID = ViewModel.State;
            shop.CityID = ViewModel.City;

            OwnerRepo.Edit(owner);
            ShopRepo.Edit(shop);

            return "Profile Updated";
        }
    }
}
