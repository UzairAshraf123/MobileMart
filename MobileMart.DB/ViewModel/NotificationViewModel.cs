using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class NotificationViewModel
    {
        public IEnumerable<CustomerNotificationViewModel> CustomerNotificationList { get; set; }
        public IEnumerable<OrderNotificationViewModel> OrderNotificationList { get; set; }
        //public List<SupplierNotificationViewModel> SupplierNotificationList { get; set; }
        public IEnumerable<ProductNotificationViewModel> ProductNotificationList { get; set; }
        public IEnumerable<ShopNotificationViewModel> ShopNotificationList { get; set; }
    }
    public class CustomerNotificationViewModel
    {
        public int CustomerNotificationID { get; set; }

        public int? CustomerID { get; set; }

        [Display(Name ="Notification Description")]
        public string CustomerNotificationDescription { get; set; }

        [Display(Name = "Customer Detail")]
        public string CustomerNotificationURL { get; set; }

        public bool? CustomerNotificationIsSeen { get; set; }

        [Display(Name = "Customer Image")]
        public string CustomerImage { get; set; }

        [Display(Name = "Time")]
        public DateTime? CreatedON { get; set; }
    }
    public class OrderNotificationViewModel
    {
        public int OrderNotificationID { get; set; }

        public int? OrderID { get; set; }

        [Display(Name = "Notification Descrption")]
        public string OrderNotificationDescription { get; set; }

        [Display(Name = "Order Detail")]
        public string OrderNotificationURL { get; set; }

        public bool? OrderNotificationIsSeen { get; set; }

        [Display(Name = "Time")]
        public DateTime? CreatedON { get; set; }
    }
    //public class SupplierNotificationViewModel
    //{
    //    public int SupplierNotificationID { get; set; }

    //    public int? SupplierID { get; set; }

    //    public string SupplierNotificationDescription { get; set; }

    //    public string SupplierNotificationURL { get; set; }

    //    public bool? SupplierNotificationIsSeen { get; set; }
    //}
    public class ProductNotificationViewModel
    {
        public int ProductNotificationID { get; set; }

        public int? ProductID { get; set; }

        [Display(Name = "Notification Description")]
        public string ProductNotificationDescription { get; set; }

        [Display(Name ="Product Detail")]
        public string ProductNotificationURL { get; set; }

        public bool? ProductNotificationIsSeen { get; set; }

        [Display(Name ="Product Image")]
        public string ProductImage { get; set; }


        [Display(Name = "Time")]
        public DateTime? CreatedON { get; set; }
    }
    public class ShopNotificationViewModel
    {
        public int ShopNotificationID { get; set; }

        public int? ShopID { get; set; }

        [Display(Name ="Notification Description")]
        public string ShopNotificationDescription { get; set; }

        [Display(Name ="Shop Detail")]
        public string ShopNotificationURL { get; set; }

        public bool? ShopNotificationIsSeen { get; set; }

        [Display(Name ="Shop Logo")]
        public string ShopLogo { get; set; }

        [Display(Name = "Time")]
        public DateTime? CreatedON { get; set; }
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
