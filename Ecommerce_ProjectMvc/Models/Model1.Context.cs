﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ecommerce_ProjectMvc.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Ecommerce_ProjectEntities : DbContext
    {
        public Ecommerce_ProjectEntities()
            : base("name=Ecommerce_ProjectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tbl_invoice> Tbl_invoice { get; set; }
        public virtual DbSet<Tbl_order> Tbl_order { get; set; }
        public virtual DbSet<Tbl_product> Tbl_product { get; set; }
        public virtual DbSet<Tbl_user> Tbl_user { get; set; }
        public virtual DbSet<Admin_Tbl> Admin_Tbl { get; set; }
        public virtual DbSet<ContactUS_Tbl> ContactUS_Tbl { get; set; }

        public System.Data.Entity.DbSet<Ecommerce_ProjectMvc.Models.MultiModel> MultiModels { get; set; }
    }
}
