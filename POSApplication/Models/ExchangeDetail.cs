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
    
    public partial class ExchangeDetail
    {
        public int ExchangeDetailID { get; set; }
        public Nullable<int> ExchangeID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> BrandID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> Generation { get; set; }
        public Nullable<int> HardDiskID { get; set; }
        public Nullable<int> HDDAvailable { get; set; }
        public Nullable<int> RamID { get; set; }
        public Nullable<int> RamAvailable { get; set; }
        public Nullable<int> ProcessorID { get; set; }
        public string Screen { get; set; }
        public Nullable<int> ScreenAvailable { get; set; }
        public string Keboard { get; set; }
        public Nullable<int> KeyboardAvailable { get; set; }
        public string Charger { get; set; }
        public Nullable<int> ChargerAvailable { get; set; }
        public string Board { get; set; }
        public Nullable<int> BoardAvailable { get; set; }
    }
}
