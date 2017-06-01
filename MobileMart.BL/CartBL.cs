using MobileMart.DB.Model;
using MobileMart.DB.ViewModel;
using MobileMart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.BL
{
    public class CartBL
    {
        public ProductDetailViewModel GetProductByID(int productID)
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
            viewmodel.Quantity = item.Quantity;
            return viewmodel;
        }
        public decimal GetCartTotal(IEnumerable<CartSessionViewModel> cartList)
        {
            if (cartList != null)
            {
                int quantityCount = 0;
                decimal total = 0;
                foreach (var item in cartList)
                {
                    quantityCount = item.Quantity + quantityCount;
                    var price = item.ProductDetail.Price * item.Quantity;
                    total = price + total;
                }
                return total;
            }
            return 0;
        }
    }
}
