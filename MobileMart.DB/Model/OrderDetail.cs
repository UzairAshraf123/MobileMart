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
    
    public partial class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> ColorID { get; set; }
        public Nullable<int> TotalAmount { get; set; }
        public Nullable<int> PaymentID { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
