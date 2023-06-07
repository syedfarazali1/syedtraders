using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using POSApplication.Models;

namespace POSApplication.Controllers.Purchases
{
    public class PurchaseTransferToBranchController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();

        // GET: PurchaseTransferToBranch
        public ActionResult TransferToBranch()
        {
            return View();

        }
        [HttpPost]
        public ActionResult TransferToBranch(string PoNumber , string PoReference)
        {
            if (Session["BranchID"] != null)
            {

            
            var query = from purchase in db.Purchases
                        join purchaseDetail in db.PurchaseDetails on purchase.PurchaseID equals purchaseDetail.PurchaseID
                        join product in db.Products on purchaseDetail.ProductID equals product.ProductID where purchase.PoNumber == PoNumber || purchase.PoReference == PoReference
                        select new
                        {
                            ProductName =  product.ProductName,
                            Quantity = purchaseDetail.Quantity,
                            UnitPrice = purchaseDetail.UnitPrice,
                            Amount = purchaseDetail.Amount,
                            TotalAmount = purchase.TotalAmount
                        };

            var List = query.ToList();
            ViewBag.Total = query.Sum(x => x.Amount);

            return Json(List, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Login", "Accounts");
            }

        }


        public ActionResult Index()
        {
            return View(db.Purchases.ToList());
        }
        [HttpPost]
        public ActionResult Index(Purchase purchase)
        {
            return View(db.Purchases.ToList());
        }

        // GET: PurchaseTransferToBranch/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: PurchaseTransferToBranch/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurchaseTransferToBranch/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchaseID,BranchID,IsTransferFromBranch,TransferFromBranchID,PoNumber,PoDate,PoReference,SupplierID,PaymentMode,TotalAmount,TaxPer,TaxAmount,PayableAmount,isPaid,CreationDate,CreatedBy,IsDeleted")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Purchases.Add(purchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(purchase);
        }

        // GET: PurchaseTransferToBranch/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: PurchaseTransferToBranch/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchaseID,BranchID,IsTransferFromBranch,TransferFromBranchID,PoNumber,PoDate,PoReference,SupplierID,PaymentMode,TotalAmount,TaxPer,TaxAmount,PayableAmount,isPaid,CreationDate,CreatedBy,IsDeleted")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchase);
        }

        // GET: PurchaseTransferToBranch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: PurchaseTransferToBranch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
