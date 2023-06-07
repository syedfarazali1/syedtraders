using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApplication.App_Classes
{
    public class salesaddtocart
    {
        public string BarCode { get; set; }
        public int? SalesID { get; set; }
        public int? productid { get; set; }
        public int? categoryid { get; set; }
        public string ProdcutName { get; set; }
        public int? quantity { get; set; }
        public decimal? unitPrice { get; set; }
        public decimal? amount { get; set; }
    }
}