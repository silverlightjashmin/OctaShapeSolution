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
    
    public partial class Card_RequestType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Card_RequestType()
        {
            this.Card_RequestDetail = new HashSet<Card_RequestDetail>();
        }
    
        public int Request_Id { get; set; }
        public string Request_Name { get; set; }
        public Nullable<decimal> Request_Charge { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Card_RequestDetail> Card_RequestDetail { get; set; }
    }
}
