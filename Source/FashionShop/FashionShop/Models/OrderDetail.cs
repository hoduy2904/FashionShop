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
    
    public partial class OrderDetail
    {
        public int id { get; set; }
        public Nullable<int> OrderID { get; set; }
        public string productID { get; set; }
        public Nullable<int> numberProduct { get; set; }
        public Nullable<double> price { get; set; }
        public string size { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
