using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class NotificationViewModel
    {
        public List<CustomerNotificationViewModel> CustomerNotificationList { get; set; }
        public List<OrderNotificationViewModel> OrderNotificationList { get; set; }
        public List<SupplierNotificationViewModel> SupplierNotificationList { get; set; }
        public List<ProductNotificationViewModel> ProductNotificationList { get; set; }
        public List<ShopNotificationViewModel> ShopNotificationList { get; set; }
    }
    public class CustomerNotificationViewModel
    {
        public int CustomerNotificationID { get; set; }

        public int? CustomerID { get; set; }

        public string CustomerNotificationDescription { get; set; }

        public string CustomerNotificationURL { get; set; }

        public bool? CustomerNotificationIsSeen { get; set; }

        public string CustomerImage { get; set; }
    }
    public class OrderNotificationViewModel
    {
        public int OrderNotificationID { get; set; }

        public int? OrderID { get; set; }

        public string OrderNotificationDescription { get; set; }

        public string OrderNotificationURL { get; set; }

        public bool? OrderNotificationIsSeen { get; set; }
    }
    public class SupplierNotificationViewModel
    {
        public int SupplierNotificationID { get; set; }

        public int? SupplierID { get; set; }

        public string SupplierNotificationDescription { get; set; }

        public string SupplierNotificationURL { get; set; }

        public bool? SupplierNotificationIsSeen { get; set; }
    }
    public class ProductNotificationViewModel
    {
        public int ProductNotificationID { get; set; }

        public int? ProductID { get; set; }

        public string ProductNotificationDescription { get; set; }

        public string ProductNotificationURL { get; set; }

        public bool? ProductNotificationIsSeen { get; set; }

        public string ProductImage { get; set; }
    }
    public class ShopNotificationViewModel
    {
        public int ShopNotificationID { get; set; }

        public int? ShopID { get; set; }

        public string ShopNotificationDescription { get; set; }

        public string ShopNotificationURL { get; set; }

        public bool? ShopNotificationIsSeen { get; set; }

        public string ShopLogo { get; set; }
    }

    public class SeenNotifications
    {
        public int[] CustomerArray { get; set; }
        public int[] OrderArray { get; set; }
        public int[] SupplierArray { get; set; }
        public int[] ProductArray { get; set; }
        public int[] ShopArray { get; set; }
    }
}
