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
    
    public partial class Export_perm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Export_perm()
        {
            this.Export_product = new HashSet<Export_product>();
        }
    
        public int perm_id { get; set; }
        public Nullable<int> supplier_id { get; set; }
        public Nullable<int> store_id { get; set; }
        public Nullable<System.DateTime> perm_date { get; set; }
    
        public virtual Store Store { get; set; }
        public virtual Supplier Supplier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Export_product> Export_product { get; set; }
    }
}
