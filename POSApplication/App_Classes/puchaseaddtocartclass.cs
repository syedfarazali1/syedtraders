using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApplication.Controllers
{
    public class puchaseaddtocartclass
    {
        public int? productid { get; set; }
        public int? categoryid { get; set; }
        public string BarCode { get; set; }
        public string ProdcutName { get; set; }
        public int? quantity { get; set; }
        public decimal? unitPrice { get; set; }
        public decimal? amount { get; set; }
    }
}