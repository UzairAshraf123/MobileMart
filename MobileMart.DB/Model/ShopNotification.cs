//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MobileMart.DB.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShopNotification
    {
        public int ShopNotificationID { get; set; }
        public Nullable<int> ShopID { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public Nullable<System.DateTime> Timestamp { get; set; }
        public Nullable<bool> IsSeen { get; set; }
    
        public virtual Shop Shop { get; set; }
    }
}