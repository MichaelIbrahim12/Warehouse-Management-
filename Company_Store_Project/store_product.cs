//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Company_Store_Project
{
    using System;
    using System.Collections.Generic;
    
    public partial class store_product
    {
        public int store_id { get; set; }
        public int product_id { get; set; }
        public Nullable<int> quantity { get; set; }
        public string unit { get; set; }
        public Nullable<System.DateTime> production_date { get; set; }
        public Nullable<System.DateTime> expire_date { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
