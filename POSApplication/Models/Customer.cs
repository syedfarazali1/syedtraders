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
    
    public partial class Customer
    {
        public int CustomerID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public string CustomerName { get; set; }
        public string ContactNumber { get; set; }
        public string CustomerReference { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<int> IsDeleted { get; set; }
    }
}