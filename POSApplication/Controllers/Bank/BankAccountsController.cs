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
    public class BankAccountsController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();

        // GET: BankAccounts
        public ActionResult Index()
        {
            return View(db.BankAccounts.Where(x =>x.IsActive == 0 && x.IsDeleted == 0).ToList());
        }

        // GET: BankAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BankAccountID,BrancID,BankName,AccountTitle,AccountNumber,IBANNumber,BankBranch,IsActive,IsDeleted")] BankAccount bankAccount)
        {
            if (Session["BranchID"] != null)
            {

                if (ModelState.IsValid)
                {
                    bankAccount.BrancID = Int32.Parse(Session["BranchID"].ToString());
                    bankAccount.IsActive = 0;
                    db.BankAccounts.Add(bankAccount);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(bankAccount);
            }
            else
            {
                return RedirectToAction("Login", "Accounts");
            }

        }

        // GET: BankAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BankAccountID,BrancID,BankName,AccountTitle,AccountNumber,IBANNumber,BankBranch,IsActive,IsDeleted")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                bankAccount.BrancID = Int32.Parse(Session["BranchID"].ToString());
                db.Entry(bankAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankAccount bankAccount = db.BankAccounts.Find(id);
            bankAccount.IsDeleted = 1;
            db.Entry(bankAccount).State = EntityState.Modified;
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
