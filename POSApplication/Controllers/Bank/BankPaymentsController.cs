using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using POSApplication.Models;

namespace POSApplication.Controllers.Bank
{
    public class BankPaymentsController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();

        // GET: BankPayments
        public ActionResult Index()
        {
            var a = db.BankPayments.ToList();
            return View(a);
        }

        // GET: BankPayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankPayment bankPayment = db.BankPayments.Find(id);
            if (bankPayment == null)
            {
                return HttpNotFound();
            }
            return View(bankPayment);
        }

        // GET: BankPayments/Create
        public ActionResult Create()
        {
            if (Session["BranchID"] != null)
            {
                int BrancID = Int32.Parse(Session["BranchID"].ToString());
                ViewBag.bankaccounts = db.BankAccounts.Where(x => x.BrancID == BrancID).ToList();
                ViewBag.customer = db.Customers.Where(x => x.BranchID == BrancID).ToList();
                ViewBag.Bank = db.Banks.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Accounts");
            }

        }
        
        public ActionResult LoadSales(int ID)
        {
            var logs = db.Sales.Where(x => x.CustomerID == ID).ToList();
            if (logs.Count > 0)
            {
                return Json(logs, JsonRequestBehavior.AllowGet);
            }
            return Json("ERROR", JsonRequestBehavior.AllowGet);

        }
        // POST: BankPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BankPaymentID,BranchID,BankAccountID,CustomerID,SalesID,BankID,CheqNumber,PdcDate,IsClear")] BankPayment bankPayment)
        {
            if (ModelState.IsValid)
            {
                db.BankPayments.Add(bankPayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bankPayment);
        }

        // GET: BankPayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankPayment bankPayment = db.BankPayments.Find(id);
            if (bankPayment == null)
            {
                return HttpNotFound();
            }
            return View(bankPayment);
        }

        // POST: BankPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BankPaymentID,BranchID,BankAccountID,CustomerID,SalesID,BankID,CheqNumber,PdcDate,IsClear")] BankPayment bankPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bankPayment);
        }

        // GET: BankPayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankPayment bankPayment = db.BankPayments.Find(id);
            if (bankPayment == null)
            {
                return HttpNotFound();
            }
            return View(bankPayment);
        }

        // POST: BankPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankPayment bankPayment = db.BankPayments.Find(id);
            db.BankPayments.Remove(bankPayment);
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
