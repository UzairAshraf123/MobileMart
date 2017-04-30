﻿using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public interface IShopNotificationRepository
    {
        List<ShopNotification> Get();
        void ChangeIsSeenByID(int shopID);
    }
}