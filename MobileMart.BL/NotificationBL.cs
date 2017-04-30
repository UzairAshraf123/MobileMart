﻿using MobileMart.DB.Model;
using MobileMart.DB.ViewModel;
using MobileMart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.BL
{
    public class NotificationBL
    {
        public NotificationViewModel GetUnSeenNotification()
        {
            var customerRepo = new CustomerRepository();
            var customerNR = new CustomerNotificationRepository();
            var orderNR = new OrderNotificationRepository();
            var productRepo = new ProductRepository();
            var productNR = new ProductNotificationRepository();
            var shopRepo = new ShopRepository();
            var shopNR = new ShopNotificationRepository();
            //var supplierNR = new SupplierNotificationRepository();

            var list = new NotificationViewModel();

            IEnumerable<CustomerNotification> customerNotification = customerNR.GetUnSeen();
            List<CustomerNotificationViewModel> customerNL = customerNotification.Select(i => new CustomerNotificationViewModel
            {
                CustomerID = i.CusotmerID,
                CustomerNotificationDescription = i.Description,
                CustomerNotificationID = i.CustomerNotificationID,
                CustomerNotificationIsSeen = i.IsSeen,
                CustomerNotificationURL = i.URL,
                CustomerImage = customerRepo.Get().Where(s => s.CustomerID == i.CusotmerID).Select(s=> s.ProfilePicture).FirstOrDefault(),
            }).ToList();

            IEnumerable<OrderNotification> orderNotification = orderNR.GetUnSeen();
            List<OrderNotificationViewModel> orderNL = orderNotification.Select(i => new OrderNotificationViewModel
            {
                OrderID = i.OrderID,
                OrderNotificationDescription = i.Description,
                OrderNotificationID = i.OrderNotificationID,
                OrderNotificationIsSeen = i.IsSeen,
                OrderNotificationURL = i.URL
            }).ToList();

            IEnumerable<ProductNotification> productNotification = productNR.GetUnSeen();
            List<ProductNotificationViewModel> productNL = productNotification.Select(i => new ProductNotificationViewModel
            {
                ProductID = i.ProductID,
                ProductNotificationDescription = i.Description,
                ProductNotificationID = i.ProductNotificationID,
                ProductNotificationIsSeen = i.IsSeen,
                ProductNotificationURL = i.URL,
                ProductImage = productRepo.Get().Where(s => s.ProductID == i.ProductID).Select(s => s.ProductImage1).FirstOrDefault(),
            }).ToList();

            IEnumerable<ShopNotification> shopNotification = shopNR.GetUnSeen();
            List<ShopNotificationViewModel> shopNL = shopNotification.Select(i => new ShopNotificationViewModel
            {
                ShopID = i.ShopID,
                ShopNotificationDescription = i.Description,
                ShopNotificationID = i.ShopNotificationID,
                ShopNotificationIsSeen = i.IsSeen,
                ShopNotificationURL = i.URL,
                ShopLogo = shopRepo.Get().Where(s => s.ShopID == i.ShopID).Select(s => s.ShopLogo).FirstOrDefault(),
            }).ToList();

            //IEnumerable<SupplierNotification> supplierNotification = supplierNR.GetUnSeen();
            //List<SupplierNotificationViewModel> supplierNL = supplierNotification.Select(i => new SupplierNotificationViewModel
            //{
            //    SupplierID = i.SupplierID,
            //    SupplierNotificationDescription = i.Description,
            //    SupplierNotificationID = i.SupplierNotificationID,
            //    SupplierNotificationIsSeen = i.IsSeen,
            //    SupplierNotificationURL = i.URL
            //}).ToList();

            list.CustomerNotificationList = customerNL;
            list.OrderNotificationList = orderNL;
            list.ProductNotificationList = productNL;
            list.ShopNotificationList = shopNL;
            //list.SupplierNotificationList = supplierNL;
            return list;
        }

        public void ChangeIsSeenState(SeenNotifications model)
        {
            for (int i = 1; i < model.OrderArray.Count(); i++)
            {
                ChangeOrderSeenStatus(model.OrderArray[i]);
            }
            for (int i = 1; i < model.CustomerArray.Count(); i++)
            {
                ChangeCustomerSeenStatus(model.CustomerArray[i]);
            }
            for (int i = 1; i < model.ProductArray.Count(); i++)
            {
                ChangeProductSeenStatus(model.ProductArray[i]);
            }
            for (int i = 1; i < model.ShopArray.Count(); i++)
            {
                ChangeShopSeenStatus(model.ShopArray[i]);
            }
            //for (int i = 1; i < model.SupplierArray.Count(); i++)
            //{
            //    ChangeSupplierSeenStatus(model.SupplierArray[i]);
            //}
        }
        public void ChangeCustomerSeenStatus(int customerID)
        {
            var customerNR = new CustomerNotificationRepository();
            customerNR.ChangeIsSeenByID(customerID);
        }
        public void ChangeOrderSeenStatus(int orderID)
        {
            var orderNR = new OrderNotificationRepository();
            orderNR.ChangeIsSeenByID(orderID);
        }
        public void ChangeProductSeenStatus(int productID)
        {
            var productNR = new ProductNotificationRepository();
            productNR.ChangeIsSeenByID(productID);
        }
        //public void ChangeSupplierSeenStatus(int supplierID)
        //{
        //    var supplierNR = new SupplierNotificationRepository();
        //    supplierNR.ChangeIsSeenByID(supplierID);
        //}
        public void ChangeShopSeenStatus(int shopID)
        {
            var shopNR = new ShopNotificationRepository();
            shopNR.ChangeIsSeenByID(shopID);
        }
        
        public CustomerDetailViewModel GetCustomerDetail(int? customerID)
        {
            var customerRepo = new CustomerRepository();
            var customer = customerRepo.GetCustomerByID(customerID);

            var customerVM = new CustomerDetailViewModel();
            customerVM.CustomerID = customer.CustomerID;
            customerVM.FirstName = customer.FirstName ;
            customerVM.LastName = customer.LastName ;
            customerVM.DOB = customer.DOB ;
            customerVM.Address1 = customer.Address1 ;
            customerVM.ProfilePicturePath = customer.ProfilePicture ;
            customerVM.Mobile = customer.PhoneNo ;
            customerVM.CreatedON = customer.CreatedOn ;
            customerVM.Email = customer.Email;

            var cityRepo = new CityRepository();
            var city = cityRepo.GetCityByID(customer.CityID);
            customerVM.City = city.name;
            var stateRepo = new StateRepository();
            var state = stateRepo.GetStateByID(city.state_id);
            customerVM.State = state.name;
            var countryRepo = new CountryRepository();
            customerVM.Country = countryRepo.GetCountryByID(state.country_id).name;
            return customerVM;
        }

        public CompleteOrderDetail GetOrderAndItsDetailByID(int? orderID)
        {
            var orderRepo = new OrderRepository();
            var order = orderRepo.GetByID(orderID);
            var orderDetailRepo = new OrderDetailRepository();
            var orderDetail = orderDetailRepo.GetByOrderID(orderID);
            var customerRepo = new CustomerRepository();
            var customer = customerRepo.GetCustomerByID(order.CustomerID);
            var productRepo = new ProductRepository();
            List<Product> productList = new List<Product>();
            int? quantity = 0;
            foreach (var item in orderDetail)
            {
                productList.Add(productRepo.GetProductByID(item.ProductID));
                quantity = quantity + item.Quantity;
            }

            return new CompleteOrderDetail()
            {
                CustomerID = order.CustomerID,
                OrderBy = customer.FirstName,
                paymentMethod = "PayPal",
                CreatedOn = order.CreatedOn,
                Tax = order.Tax,
                Shipping = order.Shipping,
                SubTotal = order.SubTotal,
                Total = order.Total,
                Products = productList,
                Quantity = quantity
            };
        }

        public ProductDetailViewModel GetProductDetail(int? productID)
        {
            var productRepo = new ProductRepository();
            var product = productRepo.GetProductByID(productID);
            var shopRepo = new ShopRepository();
            var shop = shopRepo.GetShopByProductID(productID);
            return new ProductDetailViewModel()
            {
                ProductName = product.ProductName,
                ProductImage1 = product.ProductImage1,
                ProductImage2 = product.ProductImage2,
                ProductImage3 = product.ProductImage3,
                ProductImage4 = product.ProductImage4,
                Price = product.Price,
                Color = product.ProductColor,
                ProductShop = shop,
                ProductDetail = product.ProductDetails
            };
        }

        public ShopDetailViewModel GetShopDetail(int? shopID)
        {
            var shopRepo = new ShopRepository();
            var shop = shopRepo.GetShopByID(shopID);
            var country = new CountryRepository().GetCountryByID(shop.CountryID);
            var state = new StateRepository().GetStateByID(shop.StateID);
            var city = new CityRepository().GetCityByID(shop.CityID);
            return new ShopDetailViewModel()
            {
                ShopName = shop.ShopName,
                ShopAddress = shop.ShopAddress,
                Logo = shop.ShopLogo,
                Country = country.name,
                State = state.name,
                City = city.name,
                CreatedON = shop.CreatedOn,
            };
        }
    }
}