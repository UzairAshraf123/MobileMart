using MobileMart.DB.ViewModel;
using MobileMart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.BL
{
    public class ShopBL
    {
        public ShopWithProductsViewModel GetShopDetail(int shopID)
        {
            var shopRepo = new ShopRepository();
            var productRepo = new ProductRepository();
            var supplierRepo = new SupplierRepository();

            var shop = shopRepo.GetShopByID(shopID);
            var suppliers = supplierRepo.GetSupplierByShopID(shopID);
            var products = productRepo.GetProduct(shopID);

            var viewModel = new ShopWithProductsViewModel();
            viewModel.SupplierDetail = suppliers.Select(s => new DisplaySupplierViewModel
            {
                SupplierID = s.SupplierID,
                SupplierName = s.SupplierName,
                SupplierAddress = s.SupplierAddress,
                Supplierno = s.SupplierContact
            }).ToList();
            viewModel.ProductDetail = products.Select(s => new ProductDetailViewModel
            {
                ProductID = s.ProductID,
                ProductName = s.ProductName,
                ProductDetail = s.ProductDetails,
                ProductImage1 = s.ProductImage1,
                ProductImage2 = s.ProductImage2,
                ProductImage3 = s.ProductImage4,
                ProductImage4 = s.ProductImage4,
                Color = s.ProductColor,
                Price = s.Price,
                Company = s.Company.CompanyName
            }).ToList();
            var countryRepo = new CountryRepository();
            var stateRepo = new StateRepository();
            var cityRepo = new CityRepository();
            var shopVM = new ShopDetailViewModel();
            var ownerRepo = new OwnerRepository();
            var owner = ownerRepo.GetOwnerByID(shop.OwnerID);
            shopVM.ShopName = shop.ShopName;
            shopVM.ShopAddress = shop.ShopAddress;
            shopVM.Logo = shop.ShopLogo;
            shopVM.CreatedON = shop.CreatedOn;
            shopVM.Country = countryRepo.GetCountryByID(shop.CountryID).name;
            shopVM.State = stateRepo.GetStateByID(shop.StateID).name;
            shopVM.City = cityRepo.GetCityByID(shop.CityID).name;
            shopVM.OwnerName = owner.OwnerName;
            shopVM.Mobile = owner.OwnerContact;
            viewModel.ShopDetail = shopVM;
            return viewModel;
        }
    }
}
