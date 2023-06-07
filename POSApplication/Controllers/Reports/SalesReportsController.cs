﻿using POSApplication.Models;
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
            ViewBag.list = dbContext.GetSalesReport(Invoice, startDate, endDate, CustomerId);

            return View();
        }
        [HttpGet]
        public ActionResult InvoiceDetails(int PoNum)
        {
            //ViewBag.list = dbContext.GetPurchaseReport(PoNumber, startDate, endDate, supID);
            int PoNums = Convert.ToInt32(PoNum);
            ViewBag.list = dbContext.GetSalesReportDetails(PoNums);
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