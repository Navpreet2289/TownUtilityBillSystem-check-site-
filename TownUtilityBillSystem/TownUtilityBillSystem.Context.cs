﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TownUtilityBillSystem
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TownUtilityBillSystemEntities : DbContext
    {
        public TownUtilityBillSystemEntities()
            : base("name=TownUtilityBillSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    

        public virtual DbSet<ADDRESS> ADDRESS { get; set; }
        public virtual DbSet<BILL> BILL { get; set; }
        public virtual DbSet<BUILDING> BUILDING { get; set; }
        public virtual DbSet<CUSTOMER> CUSTOMER { get; set; }
        public virtual DbSet<CUSTOMER_TYPE> CUSTOMER_TYPE { get; set; }
        public virtual DbSet<FLAT_PART> FLAT_PART { get; set; }
        public virtual DbSet<IMAGEBUILDING> IMAGEBUILDING { get; set; }
        public virtual DbSet<IMAGEUTILITY> IMAGEUTILITY { get; set; }
        public virtual DbSet<INDEX> INDEX { get; set; }
        public virtual DbSet<METER> METER { get; set; }
        public virtual DbSet<METER_ITEM> METER_ITEM { get; set; }
        public virtual DbSet<METER_TYPE> METER_TYPE { get; set; }
        public virtual DbSet<STREET> STREET { get; set; }
        public virtual DbSet<TEMPERATURE> TEMPERATURE { get; set; }
        public virtual DbSet<TOWN> TOWN { get; set; }
        public virtual DbSet<UNIT> UNIT { get; set; }
        public virtual DbSet<UTILITY> UTILITY { get; set; }

        public virtual DbSet<IMAGENEWS> IMAGENEWS { get; set; }
        public virtual DbSet<NEWS> NEWS { get; set; }
        public virtual DbSet<NEWSCHAPTER> NEWSCHAPTER { get; set; }

    }
}