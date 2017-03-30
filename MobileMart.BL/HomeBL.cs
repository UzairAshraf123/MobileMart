using MobileMart.DB.ViewModel;
using MobileMart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.BL
{
  public  class HomeBL
    {
        public List<IndexViewModel> index()
        {
            IProductRepository productRepo = new ProductRepository();
            IShopRepository Shop = new ShopRepository();
            IOwnerRepository Owner = new OwnerRepository();

            List<IndexViewModel> list = new List<IndexViewModel>();

            var products = productRepo.Get().ToList();
            var shops = Shop.Get().ToList();
            var owners = Owner.Get().ToList();

            foreach (var item in products)
            {
                IndexViewModel viewmodel = new IndexViewModel();
                viewmodel.ProductID = item.ProductID;
                viewmodel.ProductName = item.ProductName;
                viewmodel.OwnerID = owners.FirstOrDefault().OwnerID;
                viewmodel.OwnerName = owners.FirstOrDefault().OwnerName;
                viewmodel.ShopID = shops.FirstOrDefault().ShopID;
                viewmodel.ShopName = shops.FirstOrDefault().ShopName;
                viewmodel.ProductImage = item.ProductImage1;

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
            viewModel.OwnerProfile = owner.OwnerPicture;
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
    }
}
