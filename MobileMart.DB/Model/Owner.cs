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
    
    public partial class Owner
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Owner()
        {
            this.Shops = new HashSet<Shop>();
        }
    
        public int OwnerID { get; set; }
        public string AspNetUserID { get; set; }
        public string OwnerName { get; set; }
        public string OwnerContact { get; set; }
        public string OwnerPicture { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shop> Shops { get; set; }
    }
}
