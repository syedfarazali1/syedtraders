//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POSApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Exchange
    {
        public int ExchangeID { get; set; }
        public string PO { get; set; }
        public string PoDate { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> PayableAmount { get; set; }
        public Nullable<int> IsPaid { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> IsDeleted { get; set; }
    }
}
