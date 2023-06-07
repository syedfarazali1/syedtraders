using CrystalDecisions.CrystalReports.Engine;
using POSApplication.App_Classes;
using POSApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApplication.Controllers.Sales
{
    public class SalesController : Controller
    {
        Sale sales = new Sale();
        SalesDetail sd = new SalesDetail();
        PosintofsaleEntities db = new PosintofsaleEntities();

        List<salesaddtocart> sl2 = new List<salesaddtocart>();

        // GET: Puchase
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            if (Session["BranchID"] != null)
            {
                int id = Int32.Parse(Session["BranchID"].ToString());
                ViewBag.customer = db.Customers.Where(x => x.BranchID == id && x.IsDeleted == 0 && x.IsActive == 0).ToList();
                ViewBag.PaymentMode = db.PaymentModes.ToList();
                ViewBag.PaymentMethod = db.PaymentMethods.ToList();
                ViewBag.product = db.Products.Where(x => x.BranchID == id).ToList();
                ViewBag.category = db.ProductCategories.Where(x => x.BranchID == id && x.IsDeleted == 0).ToList();
                
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Accounts");


            }
        }
        [HttpPost]
        public ActionResult Create(Sale s, string InvoicePrint)
        {


            int? BranchID = Convert.ToInt32(Session["BranchID"].ToString());
            int CreatedBy = Convert.ToInt32(Session["UserID"].ToString());
            string InvoiceNumber = Convert.ToString(s.InvoiceNumber);
            int? Customer = Convert.ToInt32(s.CustomerID);
            string InvoiceDate = s.InvoiceDate;
            int? PaymentMode = s.PaymentMode;
            decimal? PayableAmount = Convert.ToDecimal(s.PayableAmount);
            decimal? totalAmount = Convert.ToDecimal(s.TotalAmount);
            int? TaxPer = Convert.ToInt32(s.TaxPer);
            decimal? TaxAmount = s.TaxAmount;
             
            var a = db.usp_Add_Sales(BranchID, InvoiceNumber, InvoiceDate, Customer, PaymentMode, totalAmount, TaxPer, TaxAmount, PayableAmount,s.Balance, CreatedBy);
            
            if (a != null)
            {
                SalesDetail sd = new SalesDetail();


                List<salesaddtocart> li2 = Session["Salescart"] as List<salesaddtocart>;

                foreach (var item in li2)
                {
                    sd.SalesID = Int32.Parse(s.InvoiceNumber);
                    sd.ProdcutCategoryID = Convert.ToInt32(item.categoryid);
                    sd.ProductID = Convert.ToInt32(item.productid);
                    sd.Quantity = Convert.ToInt32(item.quantity);
                    sd.UnitPrice = Convert.ToDecimal(item.unitPrice);
                    sd.Amount = Convert.ToDecimal(item.amount);


                    var result = db.usp_Add_SalesDetails(sd.SalesID, sd.ProdcutCategoryID, sd.ProductID, sd.Quantity, sd.UnitPrice, sd.Amount);

                    Session["ddlCustomer"] = null;
                }
                if (InvoicePrint != null)
                {

                    Session["SaleTotalAmount"] = s.TotalAmount;
                    Session["sTexAmount"] = s.TaxAmount;
                    Session["sTotalAmountwithText"] = s.TaxAmount + s.TotalAmount;
                    Session["sInvoiceDate"] = s.InvoiceDate;
                    Session["sInvoiceNumber"] = s.InvoiceNumber;
                    Session["sBalance"] = s.Balance;
                    Session["sPayableAmount"] = s.PayableAmount;
                    return RedirectToAction("PrintPage");


                }
                else
                {
                    Session["ddlCustomer"] = null;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult PrintPage()
        {
            try
            {
                if (Session["SaleTotalAmount"] != null ||
                    Session["Salescart"] != null || 
                    Session["sTexAmount"] != null ||
                    Session["sTotalAmountwithText"] != null ||
                    Session["sInvoiceDate"] != null || 
                    Session["sInvoiceNumber"] != null || 
                    Session["sBalance"] != null || 
                    Session["sPayableAmount"] != null)
                {


                    List<salesaddtocart> li2 = Session["Salescart"] as List<salesaddtocart>;
                    ViewBag.Invoice = li2;
                    ViewBag.Total = Session["SaleTotalAmount"];
                    ViewBag.Date = Session["sInvoiceDate"];
                    ViewBag.InvoiceNumber = Session["sInvoiceNumber"];
                    ViewBag.Balance = Session["sBalance"];
                    ViewBag.PayableAmount = Session["sPayableAmount"];
                    
                    return View();
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
            catch (Exception msg)
            {

                throw;
            }



        }
        [HttpPost]
        public ActionResult PrintPDF()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ProdcutName", typeof(String));
                dt.Columns.Add("quantity", typeof(Int32));
                dt.Columns.Add("unitPrice", typeof(Decimal));
                dt.Columns.Add("amount", typeof(Decimal));

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports"), "SalesInvoiceReport.rpt"));
                List<salesaddtocart> list = Session["Salescart"] as List<salesaddtocart>;
                foreach (var item in list)
                {
                    string productname = db.Products.Where(x => x.ProductID == item.productid).Select(x => x.ProductName).FirstOrDefault();


                    dt.Rows.Add(productname, item.quantity, item.unitPrice, item.amount);



                }

                string PoDate = Session["sInvoiceDate"].ToString();
                string TotalAmount = Session["SaleTotalAmount"].ToString();
                string PoNumber = Session["sInvoiceNumber"].ToString();



                rd.SetDataSource(dt);
                rd.SetParameterValue("Date", PoDate);
                rd.SetParameterValue("InvoiceNum", PoNumber);
                rd.SetParameterValue("Total", TotalAmount);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                Stream st = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                st.Seek(0, SeekOrigin.Begin);

                Session["SaleTotalAmount"] = null;
                Session["Salescart"] = null;
                Session["sInvoiceDate"] = null;
                Session["sInvoiceNumber"] = null;
                Session["sBalance"] = null;
                Session["sPayableAmount"] = null;
                Session["SupplierId"] = null;
                Session["sTexAmount"] = null;
                Session["sTotalAmountwithText"] = null;



                return File(st, "application/pdf", "SalesInvoice.pdf");
            }
            catch (Exception msg)
            {

                throw;
            }




        }

        [HttpPost]
        public ActionResult AddTOCart(SalesDetail sd, string ddlCustomer)
        {
            try
            {
                
                var msg = "";
                SalesDetail dta = new SalesDetail();


                if (sd.ProdcutCategoryID > 0)
                {
                    if (sd.ProductID > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(sd.UnitPrice.ToString()))
                        {
                            if (!string.IsNullOrWhiteSpace(sd.Quantity.ToString()))
                            {
                                string productname = db.Products.Where(x => x.ProductID == sd.ProductID).Select(x => x.ProductName).FirstOrDefault();
                                string BarCode = db.Products.Where(x => x.ProductID == sd.ProductID).Select(x => x.BarCode).FirstOrDefault();

                                salesaddtocart c = new salesaddtocart();
                                c.categoryid = sd.ProdcutCategoryID;
                                c.productid = sd.ProductID;
                                c.BarCode = BarCode;
                                c.ProdcutName = productname;
                                c.unitPrice = Convert.ToDecimal(sd.UnitPrice);
                                c.quantity = sd.Quantity;
                                c.amount = Convert.ToDecimal(c.quantity) * Convert.ToDecimal(c.unitPrice);

                                List<salesaddtocart> li = Session["Salescart"] as List<salesaddtocart>;
                                if (li == null)
                                {
                                    sl2.Add(c);
                                    Session["Salescart"] = sl2;
                                    Session["ddlCustomer"] = ddlCustomer;

                                }

                                else
                                {
                                    List<salesaddtocart> li2 = Session["Salescart"] as List<salesaddtocart>;
                                    int flag = 0;
                                    foreach (var item in li2)
                                    {
                                        if (item.productid == c.productid)
                                        {
                                            item.quantity += c.quantity;
                                            item.amount += c.amount;
                                            flag = 1;
                                        }

                                        else
                                        {
                                            sl2.Add(c);
                                            Session["Salescart"] = sl2;
                                            Session["ddlCustomer"] = ddlCustomer;
                                        }
                                    }
                                    if (flag == 0)
                                    {
                                        li2.Add(c);
                                    }
                                    Session["Salescart"] = li2;
                                    Session["ddlCustomer"] = ddlCustomer;
                                }

                                ViewBag.error = "Product addedd successfully";
                                return Json(Session["Salescart"], JsonRequestBehavior.AllowGet);

                            }
                            else
                            {
                                msg = "Invalid Quantity For The Selected Item";
                                ViewBag.error = msg;
                            }
                        }
                        else
                        {
                            msg = "Invalid UnitPrice For The Selected Item";
                            ViewBag.error = msg;
                        }
                    }

                    else
                    {

                        msg = "Please select product item.";
                        ViewBag.error = msg;
                    }
                }

                else
                {
                    msg = "Please select product category.";
                    ViewBag.error = msg;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Json("Error", JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult CalculateTotalAmount()
        {
            Decimal? TotalAmount = 0;
            List<salesaddtocart> li2 = Session["Salescart"] as List<salesaddtocart>;
            if (li2 != null)
            {

            if (li2.Count == 0)
            {
                TotalAmount = 0;
            }
            else
            {



                int flag = 0;
                foreach (var item in li2)
                {


                    TotalAmount += item.amount;
                    flag = 1;


                }
            }
            }

            return Json(TotalAmount, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult RemoveCart(Product product)
        {
            List<salesaddtocart> li = Session["Salescart"] as List<salesaddtocart>;
            if (li != null)
            {

                int pid = product.ProductID;
                List<salesaddtocart> li2 = Session["Salescart"] as List<salesaddtocart>;
                salesaddtocart c = li2.Where(x => x.productid == pid).FirstOrDefault();
                li2.Remove(c);
                Session["Salescart"] = li2;
                decimal? amount = 0;

                foreach (var item in li2)
                {
                    amount += item.amount;
                }
                Session["Salescart"] = li2;
                if (li2.Count != 0)
                {
                    return Json(Session["Salescart"], JsonRequestBehavior.AllowGet);

                }
                else
                {
                    Session["ddlCustomer"] = null;
                    return Json("NULL", JsonRequestBehavior.AllowGet);


                }

            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult onload()
        {
            List<salesaddtocart> li2 = Session["Salescart"] as List<salesaddtocart>;
            if (li2 != null)
            {


                if (li2.Count > 0)
                {

                    return Json(Session["Salescart"], JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Session["ddlCustomer"] = null;
                    return Json("Null", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("Null", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult LoadProduct(Product product)
        {
            if (Session["BranchID"] != null)
            {


                if (product != null)
                {
                    int id = Int32.Parse(Session["BranchID"].ToString());
                    var list = db.Products.Where(x => x.ProdcutCategoryID == product.ProdcutCategoryID && x.BranchID == id && x.IsDeleted == 0).ToList();
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Null", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("Login", "Accounts");
            }
        }
        [HttpGet]
        public ActionResult calculateInvoiceNum()
        {
            int InvoiceNum = 0;
            int id = Int32.Parse(Session["BranchID"].ToString());
        var list= db.Sales.Where(x => x.BranchID == id).ToList();
            if (list.Count > 0)
            {
                InvoiceNum = db.Sales.Where(x => x.BranchID == id).ToList().Select(x => x.SalesID).Max();

            }

            int val = InvoiceNum + 1;
            return Json(val, JsonRequestBehavior.AllowGet);


        }
        [HttpGet]
        public ActionResult GetCustomerID()
        {
            var Customer = Session["ddlCustomer"];
            if (Customer == null)
            {
                Customer = -1;
            }
            return Json(Customer, JsonRequestBehavior.AllowGet);

        }

    }
}