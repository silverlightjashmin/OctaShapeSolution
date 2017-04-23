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
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
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
    
        public virtual DbSet<Card_Requested> Card_Requested { get; set; }
        public virtual DbSet<Card_Received> Card_Received { get; set; }
        public virtual DbSet<Card_ReceivedDetails> Card_ReceivedDetails { get; set; }
        public virtual DbSet<Card_RequestDetail> Card_RequestDetail { get; set; }
        public virtual DbSet<Card_RequestType> Card_RequestType { get; set; }
        public virtual DbSet<Card_StockDetail> Card_StockDetail { get; set; }
    
        public virtual ObjectResult<GetRequestData_Result> GetRequestData(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate, string userName)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRequestData_Result>("GetRequestData", startDateParameter, endDateParameter, userNameParameter);
        }
    }
}
