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
    
    public partial class BankPayment
    {
        public int BankPaymentID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> BankAccountID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> SalesID { get; set; }
        public Nullable<int> BankID { get; set; }
        public string CheqNumber { get; set; }
        public string PdcDate { get; set; }
        public string IsClear { get; set; }
    }
}
