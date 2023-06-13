using CrystalDecisions.CrystalReports.Engine;
using POSApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApplication.Controllers.Purchases
{
    public class PuchaseController : Controller
    {
        Purchase puchase = new Purchase();
        PurchaseDetail purchaseDetail = new PurchaseDetail();
        PosintofsaleEntities db = new PosintofsaleEntities();
        List<puchaseaddtocartclass> li = new List<puchaseaddtocartclass>();

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
                ViewBag.Supplier = db.Suppliers.Where(x => x.BranchID == id && x.IsDeleted == 0 && x.IsActive == 0).ToList();
                ViewBag.PaymentMode = db.PaymentModes.ToList();
                ViewBag.PaymentMethod = db.PaymentMethods.ToList();
                ViewBag.product = db.Products.Where(x => x.BranchID == id).ToList();
                ViewBag.category = db.ProductCategories.Where(x => x.BranchID == id && x.IsDeleted == 0).ToList();
                ViewBag.Supplier = db.Suppliers.Where(x => x.BranchID == id).ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Accounts");


            }
        }
        [HttpPost]
        public ActionResult Create(Purchase p, string InvoicePrint)

        { 

            int? BranchID = Convert.ToInt32(Session["BranchID"].ToString());
            int CreatedBy = Convert.ToInt32(Session["UserID"].ToString());
            int? IsTransferFromBranch = 0;
            int? TransferFromBranchID = 0;
            string PoNumber = Convert.ToString(p.PoNumber);
            int? SupplierID = Convert.ToInt32(p.SupplierID);
            string PoDate = p.PoDate;
            string PoReference = p.PoReference;
            decimal PayableAmount = Convert.ToDecimal(p.PayableAmount);
            int? TotalAmount = Convert.ToInt32(p.TotalAmount);

            var a = db.usp_Add_Purchase(BranchID, IsTransferFromBranch, TransferFromBranchID, PoNumber, PoDate, PoReference, SupplierID, p.PaymentMode, TotalAmount,p.TaxPer , p.TaxAmount, PayableAmount, p.Balance, CreatedBy);
            if (a.ToString() != "0")
            {
                PurchaseDetail pd = new PurchaseDetail();


                List<puchaseaddtocartclass> li2 = Session["cart"] as List<puchaseaddtocartclass>;

                foreach (var item in li2)
                {
                    pd.PurchaseID = Int32.Parse(p.PoNumber);
                    pd.ProdcutCategoryID = Convert.ToInt32(item.categoryid);
                    pd.ProductID = Convert.ToInt32(item.productid);
                    pd.Quantity = Convert.ToInt32(item.quantity);
                    pd.UnitPrice = Convert.ToDecimal(item.unitPrice);
                    pd.Amount = Convert.ToDecimal(item.amount);
                    pd.StockQuantity = Convert.ToInt32(item.quantity);

                    var result = db.usp_Add_PurchaseDetails(pd.PurchaseID, pd.ProdcutCategoryID, pd.ProductID, pd.Quantity, pd.UnitPrice, pd.Amount, pd.StockQuantity);


                }
                if (InvoicePrint != null)
                {

                    Session["TotalAmount"] = p.TotalAmount;
                    Session["TexAmount"] = p.TaxAmount;
                    Session["TotalAmountwithText"] = p.TaxAmount + p.TotalAmount;
                    Session["PoDate"] = p.PoDate;
                    Session["PoNumber"] = p.PoNumber;
                    Session["Balance"] = p.Balance;
                    Session["PayableAmount"] = p.PayableAmount;
                    Session["SupplierId"] = null;
                    return RedirectToAction("PrintPage");


                }


            }
            Session["TotalAmount"] = null;
            Session["cart"] = null;
            Session["PoDate"] = null;
            Session["PoNumber"] = null;
            Session["Balance"] = null;
            Session["PayableAmount"] = null;
            Session["SupplierId"] = null;
            Session["TexAmount"] = null;
            Session["TotalAmountwithText"] = null;
            return RedirectToAction("Create");
        }
        [HttpGet]
        public ActionResult PrintPage()
        {
            try
            {
                if (Session["TotalAmount"] != null || Session["cart"] != null || Session["PoDate"] != null || Session["PoNumber"] != null|| Session["Balance"] != null|| Session["PayableAmount"] != null )
                {


                    List<puchaseaddtocartclass> li2 = Session["cart"] as List<puchaseaddtocartclass>;
                    ViewBag.Invoice = li2;
                    ViewBag.Total = Session["TotalAmount"];
                    ViewBag.Date = Session["PoDate"];
                    ViewBag.InvoiceNumber = Session["PoNumber"];
                    ViewBag.Balance = Session["Balance"];
                    ViewBag.PayableAmount = Session["PayableAmount"];
                    ViewBag.TexAmount = Session["TexAmount"];
                    ViewBag.TotalAmountwithText = Session["TotalAmountwithText"];
                    Purchase p = new Purchase();
                    
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
                    rd.Load(Path.Combine(Server.MapPath("~/Reports"), "PurchaseInvoiceReport.rpt"));
                    List<puchaseaddtocartclass> list = Session["cart"] as List<puchaseaddtocartclass>;
                
                    foreach (var item in list)
                    {
                        string productname = db.Products.Where(x => x.ProductID == item.productid).Select(x => x.ProductName).FirstOrDefault();


                        dt.Rows.Add(productname, item.quantity, item.unitPrice, item.amount);
                    
                    

                    }
                string PoDate = Session["PoDate"].ToString();
                string TotalAmount = Session["TotalAmount"].ToString();
                string PoNumber = Session["PoNumber"].ToString();
                int? id = Int32.Parse(PoNumber);
                var data = db.GetPurchaseReportDetailsTotalAmounts(id).FirstOrDefault();
                Decimal? GrandTotal = data.GrandTotal;
                Decimal? PayableAmount = data.PayableAmount;
                Decimal? TaxAmount = data.TaxAmount;
                Decimal? TotalBalance = data.TotalBance;
                Decimal? TotalQuntity = data.TotalQuntity;
                Decimal? spTotal = data.TotalAmount;
                



                rd.SetDataSource(dt);
                rd.SetParameterValue("GrandTotal", GrandTotal);
                rd.SetParameterValue("PayableAmount", PayableAmount);
                rd.SetParameterValue("TaxAmount", TaxAmount);
                rd.SetParameterValue("TotalBalance", TotalBalance);
                rd.SetParameterValue("Date", PoDate);
                rd.SetParameterValue("InvoiceNum", PoNumber);
                rd.SetParameterValue("Total", TotalAmount);
             

                Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    Stream st = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    st.Seek(0, SeekOrigin.Begin);
                Session["TotalAmount"] = null;
                Session["cart"] = null;
                Session["PoDate"] = null;
                Session["PoNumber"] = null;
                Session["Balance"] = null;
                Session["PayableAmount"] = null;
                Session["SupplierId"] = null;
                Session["TexAmount"] = null;
                Session["TotalAmountwithText"] = null;
                return File(st, "application/pdf", "PurchaseInvoice.pdf");
                }
                catch (Exception msg)
                {

                    throw;
                }




        }

        [HttpPost]
        public ActionResult AddTOCart(PurchaseDetail pd, string SupplierId)
        {
            try
            {

                var msg = "";
                PurchaseDetail dta = new PurchaseDetail();


                if (pd.ProdcutCategoryID > 0)
                {
                    if (pd.ProductID > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(pd.UnitPrice.ToString()))
                        {
                            if (!string.IsNullOrWhiteSpace(pd.Quantity.ToString()))
                            {
                                string productname = db.Products.Where(x => x.ProductID == pd.ProductID).Select(x => x.ProductName).FirstOrDefault();

                                puchaseaddtocartclass c = new puchaseaddtocartclass();
                                c.categoryid = pd.ProdcutCategoryID;
                                c.productid = pd.ProductID;
                                c.ProdcutName = productname;
                                c.unitPrice = Convert.ToDecimal(pd.UnitPrice);
                                c.quantity = pd.Quantity;
                                c.amount = Convert.ToDecimal(c.quantity) * Convert.ToDecimal(c.unitPrice);

                                //supplier id add krne he dropdown mai agr cart null na ho
                                // Session["supplierId"] == 0;
                                List<puchaseaddtocartclass> listcart = Session["cart"] as List<puchaseaddtocartclass>;
                                if (listcart == null)
                                {
                                    li.Add(c);
                                    Session["cart"] = li;
                                    Session["SupplierId"] = SupplierId;

                                }

                                else
                                {
                                    List<puchaseaddtocartclass> li2 = Session["cart"] as List<puchaseaddtocartclass>;
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
                                            li.Add(c);
                                            Session["cart"] = li;
                                            Session["SupplierId"] = SupplierId;
                                        }
                                    }
                                    if (flag == 0)
                                    {
                                        li2.Add(c);
                                    }
                                    Session["cart"] = li2;
                                    Session["SupplierId"] = SupplierId;
                                }

                                ViewBag.error = "Product addedd successfully";
                                return Json(Session["cart"], JsonRequestBehavior.AllowGet);

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
            catch (Exception)
            {

                throw;
            }
            return Json("Error", JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult CalculateTotalAmount()
        {
            Decimal? TotalAmount = 0;
            List<puchaseaddtocartclass> li3 = Session["cart"] as List<puchaseaddtocartclass>;
            if (li3 != null)
            {


                if (Session["cart"] == null)
                {
                    TotalAmount = 0;
                }
                else
                {


                    List<puchaseaddtocartclass> li2 = Session["cart"] as List<puchaseaddtocartclass>;
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
            List<puchaseaddtocartclass> li = Session["cart"] as List<puchaseaddtocartclass>;
            if (li != null)
            {

                int pid = product.ProductID;
                List<puchaseaddtocartclass> li2 = Session["cart"] as List<puchaseaddtocartclass>;
                puchaseaddtocartclass c = li2.Where(x => x.productid == pid).FirstOrDefault();
                li2.Remove(c);
                Session["cart"] = li2;
                decimal? amount = 0;

                foreach (var item in li2)
                {
                    amount += item.amount;
                }
                Session["cart"] = li2;
                if (li2.Count != 0)
                {
                    return Json(Session["cart"], JsonRequestBehavior.AllowGet);

                }
                else
                {
                    Session["SupplierId"] = null;
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
            List<puchaseaddtocartclass> li2 = Session["cart"] as List<puchaseaddtocartclass>;
            if (li2 != null)
            {


                if (li2.Count > 0)
                {

                    return Json(Session["cart"], JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Session["SupplierId"] = null;
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
        public ActionResult calculatePONum()
        {
            int id = Int32.Parse(Session["BranchID"].ToString());
            int PoNum;
            int val = 0;
            try
            {
                 PoNum = db.Purchases.Where(x => x.BranchID == id).ToList().Select(x => x.PurchaseID).Max();
                val = PoNum + 1;

            }
            catch (Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                {
                    val = 1;

                    
                }


            }

            return Json(val, JsonRequestBehavior.AllowGet);




        }
        [HttpGet]
        public ActionResult SupplierId()
        {
            var SupplierId = Session["SupplierId"];
            if (SupplierId == null)
            {
                SupplierId = -1;
            }
            return Json(SupplierId, JsonRequestBehavior.AllowGet);

        }

    }
}