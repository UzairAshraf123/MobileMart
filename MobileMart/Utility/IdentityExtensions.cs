using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using MobileMart.BL;

namespace MobileMart.Utility
{
    public static class IdentityExtensions
    {
        public static int GetShopID(this IIdentity identity)
        {
            if (identity.GetUserId() != "")
            {
                var shop = new ShopKeeperBL().GetShopByAspID(identity.GetUserId());
                if (shop != 0)
                {
                    return shop;
                }
                return 0;
            }
            return 0;
        }
    
        public static int GetCustomerID(this IIdentity identity)
        {
            if (identity.GetUserId() != "")
            {
                var customerID = new CustomerBL().GetCustomerIDByAspID(identity.GetUserId());
                if (customerID != 0)
                {
                    return customerID;
                }
                return 0;
            }
            return 0;
        }
    
    }
}