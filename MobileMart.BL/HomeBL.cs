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
            IProductRepository Product = new ProductRepository();
            IShopRepository Shop = new ShopRepository();
            IOwnerRepository Owner = new OwnerRepository();

            List<IndexViewModel> list = new List<IndexViewModel>();

            var products = Product.GetProduct().ToList();
            var shops = Shop.Get().ToList();
            var owners = Owner.Get().ToList();

            foreach(var item in products)
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

    }
}
