//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FashionShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public account()
        {
            this.infomationAccounts = new HashSet<infomationAccount>();
            this.carts = new HashSet<cart>();
            this.Orders = new HashSet<Order>();
        }
    
        public string email { get; set; }
        public string pwd { get; set; }
        public Nullable<System.DateTime> timecreate { get; set; }
        public string identityCard { get; set; }
        public Nullable<bool> isVerified { get; set; }
        public Nullable<bool> isAdmin { get; set; }
        public string hoTen { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<infomationAccount> infomationAccounts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart> carts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
