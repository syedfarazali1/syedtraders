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
    
    public partial class Expens
    {
        public int ExpensesID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> ExpensesTypeID { get; set; }
        public string ExpensesDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> ExpensesByID { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> CreatedByUserID { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<decimal> ExpensesAmount { get; set; }
    }
}