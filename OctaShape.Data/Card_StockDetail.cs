//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Card_StockDetail
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Tran_Date { get; set; }
        public string Description { get; set; }
        public Nullable<int> Reference_No { get; set; }
        public Nullable<int> Stock_Inward { get; set; }
        public Nullable<int> Stock_Outward { get; set; }
    
        public virtual Card_Received Card_Received { get; set; }
    }
}
