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

        public int GetNewOrders(int shopID)
        {
            return new OrderRepository().Get().Where(s => s.OrderDetails.Any(i => i.Product.Supplier.Shop.ShopID == shopID) && s.CreatedOn >= DateTime.Now.AddDays(-7)).Count();
        }
        public int GetAllOrdersCount(int shopID)
        {
            return new OrderRepository().Get().Where(s => s.OrderDetails.Any(i => i.Product.Supplier.Shop.ShopID == shopID)).Count();
        }
        public int GetNewProductsCount(int shopID)
        {
            return new ProductRepository().Get().Where(s => s.Supplier.Shop.ShopID == shopID && s.CreatedOn >= DateTime.Now.AddDays(-7)).Count();
        }
        public int GetAllProductsCount(int shopID)
        {
            return new ProductRepository().Get().Where(s => s.Supplier.Shop.ShopID == shopID).Count();
        }
        public IEnumerable<DisplayOrderViewModel> TodaysOrders(int shopID)
        {
            return new OrderRepository().Get().Where(s => s.OrderDetails.Any(i => i.Product.Supplier.Shop.ShopID == shopID) && s.CreatedOn >= DateTime.Today).Select(w=> new DisplayOrderViewModel
            {
                OrderID = w.OrderID,
                CustomerName = new CustomerRepository().GetCustomerByID(w.CustomerID).FirstName,
                CreatedOn = w.CreatedOn,
                Tax = w.Tax,
                Total = w.Total,
                Shipping = w.Shipping,
                SubTotal = w.SubTotal,
            });
        }

        public void UpdateShop(DisplayShopViewModel viewModel)
        {
            IShopRepository shopRepo = new ShopRepository();
            Shop shopEntity = new Shop();
            if (viewModel.ShopLogo != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(viewModel.ShopImage.FileName);
                fileName += DateTime.Now.Ticks + Path.GetExtension(viewModel.ShopImage.FileName);
                var basePath = "~/Content/ShopOwner/" + viewModel.UserID + "/" + viewModel.OwnerID + "/ShopLogo/";
                var path = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName);
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/ShopOwner/" + viewModel.UserID + "/" + viewModel.OwnerID + "/ShopLogo/"));
                viewModel.ShopImage.SaveAs(path);
                shopEntity.ShopLogo = basePath + fileName;
            }
            else
            {
                shopEntity.ShopLogo = viewModel.ShopLogo;
            }
            shopEntity.OwnerID = viewModel.OwnerID;
            shopEntity.ShopID = viewModel.ShopID;
            shopEntity.ShopName = viewModel.ShopName;
            shopEntity.ShopAddress = viewModel.ShopAddress;
            shopEntity.CountryID = viewModel.Country;
            shopEntity.StateID = viewModel.State;
            shopEntity.CityID = viewModel.City;
            shopEntity.CreatedOn = DateTime.Now;
            shopRepo.Update(shopEntity);
        }
        public decimal? GetTotalSaleOfShop(int? shopID)
        {
            var orders= new OrderRepository().Get().Where(s => s.OrderDetails.Any(w => w.Product.Supplier.Shop.ShopID == shopID)).ToList();
            decimal? count = 0;
            foreach (var item in orders)
            {
                count = item.Total + count;
            }
            return count;
        }

    }
}
