﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OctaShape.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OctaShape_Card_Entities : DbContext
    {
        public OctaShape_Card_Entities()
            : base("name=OctaShape_Card_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Card_Received> Card_Received { get; set; }
        public virtual DbSet<Card_RequestDetail> Card_RequestDetail { get; set; }
        public virtual DbSet<Card_Requested> Card_Requested { get; set; }
        public virtual DbSet<Card_RequestType> Card_RequestType { get; set; }
        public virtual DbSet<Card_StockDetail> Card_StockDetail { get; set; }
    }
}
