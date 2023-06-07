using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApplication.App_Classes
{
    public class purchaseReporthelperclass
    {
        public string PoNumber { get; set; }
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        public string PoDate { get; set; }
        public string PoReference { get; set; }
        public string SupplierName { get; set; }
        public string PaymentMethodName { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}