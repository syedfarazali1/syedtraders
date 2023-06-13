using POSApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApplication.Controllers.Reports
{
    public class StockReportsController : Controller
    {
        PosintofsaleEntities db = new PosintofsaleEntities();
        [HttpGet]
        public ActionResult GetReport()
        {
            if (Session["BranchID"] != null)
            {
                int id = Int32.Parse(Session["BranchID"].ToString());
                ViewBag.Product = db.Products.Where(x => x.BranchID == id).ToList();
                var list = db.GetStockReportReport(null, id.ToString());
                ViewBag.list = list;
            }

             return View();
        }

        [HttpPost]
        public ActionResult GetReport(String ProductID)
        {
            if (Session["BranchID"] != null)
            {
                if (ProductID != "-1")
                {
                    int id = Int32.Parse(Session["BranchID"].ToString());
                    ViewBag.Product = db.Products.Where(x => x.BranchID == id).ToList();
                    var list = db.GetStockReportReport(ProductID, id.ToString());
                    ViewBag.list = list;
                }
                else
                {
                    int id = Int32.Parse(Session["BranchID"].ToString());
                    ViewBag.Product = db.Products.Where(x => x.BranchID == id).ToList();
                    var list = db.GetStockReportReport(null, id.ToString());
                    ViewBag.list = list;
                }

                
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Accounts");
            }
        }


    }
}