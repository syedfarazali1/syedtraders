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
    
    public partial class PaymentVoucherCustomer
    {
        public int PaymentVoucherCustomersID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> SalesID { get; set; }
        public Nullable<int> PaymetMethodID { get; set; }
        public Nullable<int> BankID { get; set; }
        public string ChequeNumber { get; set; }
        public string PayOrderNumber { get; set; }
        public Nullable<int> IsPDC { get; set; }
        public string PDCDate { get; set; }
        public Nullable<int> IsPdcClear { get; set; }
        public Nullable<decimal> Debit { get; set; }
        public Nullable<decimal> Credit { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<int> Condition { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> CreatedByUserID { get; set; }
        public Nullable<int> TransactionTypeID { get; set; }
        public Nullable<int> IsDeleted { get; set; }
    }
}
