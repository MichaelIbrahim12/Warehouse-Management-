﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Company_storeEntities1 : DbContext
    {
        public Company_storeEntities1()
            : base("name=Company_storeEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Export_perm> Export_perm { get; set; }
        public virtual DbSet<Export_product> Export_product { get; set; }
        public virtual DbSet<Import_perm> Import_perm { get; set; }
        public virtual DbSet<Import_product> Import_product { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Product_unit> Product_unit { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<store_product> store_product { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Transfer_Product> Transfer_Product { get; set; }
    }
}
