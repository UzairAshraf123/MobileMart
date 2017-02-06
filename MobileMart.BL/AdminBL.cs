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

        public int GetOwnerByUserID(string userID)
        {

            //var owner = ownerRepo.Get().FirstOrDefault(s => s.AspNetUserID == userID).OwnerID
            IOwnerRepository ownerRepo = new OwnerRepository();
            var owner = ownerRepo.Get().FirstOrDefault(s => s.AspNetUserID == userID).OwnerID;
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

    }
}
