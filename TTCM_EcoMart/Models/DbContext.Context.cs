﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TTCM_EcoMart.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Beta_E_CommerceEntities1 : DbContext
    {
        public Beta_E_CommerceEntities1()
            : base("name=Beta_E_CommerceEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblAccount> tblAccounts { get; set; }
        public virtual DbSet<tblBill> tblBills { get; set; }
        public virtual DbSet<tblCategory> tblCategories { get; set; }
        public virtual DbSet<tblImage> tblImages { get; set; }
        public virtual DbSet<tblPayment> tblPayments { get; set; }
        public virtual DbSet<tblProduct> tblProducts { get; set; }
        public virtual DbSet<tblSale> tblSales { get; set; }
        public virtual DbSet<tblWishlist> tblWishlists { get; set; }
        public virtual DbSet<tblWishlist_details> tblWishlist_details { get; set; }
        public virtual DbSet<tblBill_details> tblBill_details { get; set; }
    }
}
