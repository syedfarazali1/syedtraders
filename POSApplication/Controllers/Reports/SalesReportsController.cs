using POSApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApplication.Controllers.Reports
{
    public class SalesReportsController : Controller
    {
        PosintofsaleEntities dbContext = new PosintofsaleEntities();
        // GET: Purchase

        [HttpGet]
        public ActionResult Invoice()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Invoice(DateTime? startDate, DateTime? endDate, int CustomerId = 0, int Invoice = 0)
        {
            if (Session["BranchID"] != null)
            {
                string BranchID = Session["BranchID"].ToString();
                var list = dbContext.GetSalesReport(Invoice, startDate, endDate, CustomerId, BranchID);
                if (list != null)
                {
                    ViewBag.list = list;
                }

                if (Invoice != null || Invoice > 0)
                {
                    ViewBag.PoNum = Invoice;
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Accounts");
            }
        }
        [HttpGet]
        public ActionResult InvoiceDetails(int InvoiceNumber)
        {
            int PoNums = Convert.ToInt32(InvoiceNumber);
            ViewBag.list = dbContext.GetSalesReportDetails(InvoiceNumber);
            var data = dbContext.GetSaleReportDetailsTotalAmounts(InvoiceNumber).FirstOrDefault();
            ViewBag.GrandTotal = data.GrandTotal;
            ViewBag.PayableAmount = data.PayableAmount;
            ViewBag.TaxAmount = data.TaxAmount;
            ViewBag.TotalAmount = data.TotalAmount;
            ViewBag.TotalBance = data.TotalBance;
            ViewBag.TotalQuntity = data.TotalQuntity;
            ViewBag.PoNum = InvoiceNumber;

            return View();
        }
    }
}