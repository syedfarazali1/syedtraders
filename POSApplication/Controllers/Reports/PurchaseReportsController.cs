using POSApplication.App_Classes;
using POSApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApplication.Controllers.Reports
{
    public class PurchaseReportsController : Controller
    {
        PosintofsaleEntities dbContext = new PosintofsaleEntities();
        // GET: Purchase

        [HttpGet]
        public ActionResult Invoice()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Invoice(DateTime? startDate, DateTime? endDate, int supID = 0, int PoNumber = 0)
        {
            if (Session["BranchID"] != null)
            {

            string BranchID = Session["BranchID"].ToString();
            ViewBag.list = dbContext.GetPurchaseReport(PoNumber, startDate, endDate, supID, BranchID);
            if (PoNumber != null || PoNumber > 0)
            {
                ViewBag.PoNum = PoNumber;
            }

            return View();

            }
            else
            {
                return RedirectToAction("Login", "Accounts");
            }
        }
        [HttpGet]
        public ActionResult InvoiceDetails(int PoNum)
        {
            //ViewBag.list = dbContext.GetPurchaseReport(PoNumber, startDate, endDate, supID);
            int PoNums = Convert.ToInt32(PoNum);
            ViewBag.list= dbContext.GetPurchaseReportDetails(PoNums);
           var data = dbContext.GetPurchaseReportDetailsTotalAmounts(PoNums).FirstOrDefault();
            ViewBag.GrandTotal = data.GrandTotal;
            ViewBag.PayableAmount = data.PayableAmount;
            ViewBag.TaxAmount = data.TaxAmount;
            ViewBag.TotalAmount = data.TotalAmount;
            ViewBag.TotalBance = data.TotalBance;
            ViewBag.TotalQuntity = data.TotalQuntity;
            ViewBag.PoNum = PoNum;
            
            return View();
        }
    
    }
}