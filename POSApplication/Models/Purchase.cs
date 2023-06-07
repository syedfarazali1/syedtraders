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
    
    public partial class Purchase
    {
        public int PurchaseID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> IsTransferFromBranch { get; set; }
        public Nullable<int> TransferFromBranchID { get; set; }
        public string PoNumber { get; set; }
        public string PoDate { get; set; }
        public string PoReference { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> PaymentMode { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<int> TaxPer { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> PayableAmount { get; set; }
        public Nullable<int> isPaid { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<decimal> Balance { get; set; }
    }
}