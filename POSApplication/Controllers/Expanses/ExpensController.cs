using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using POSApplication.Models;

namespace POSApplication.Controllers.Expanses
{
    public class ExpensController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();

        // GET: Expens
        public ActionResult Index()
        {
            ViewBag.Exptyle = db.ExpensesTypes.Where(x => x.IsDeleted == "0").ToList();
            return View(db.Expenses.ToList());
        }

        // GET: Expens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expens expens = db.Expenses.Find(id);
            if (expens == null)
            {
                return HttpNotFound();
            }
            return View(expens);
        }

        // GET: Expens/Create
        public ActionResult Create()
        {
            ViewBag.exptyle = db.ExpensesTypes.Where(x => x.IsDeleted == "0").ToList();
            return View();
        }

        // POST: Expens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpensesID,BranchID,ExpensesTypeID,ExpensesDate,Remarks,ExpensesByID,CreationDate,CreatedByUserID,IsDeleted,ExpensesAmount")] Expens expens)
        {
            if (ModelState.IsValid)
            {
                expens.BranchID = Int32.Parse(Session["BranchID"].ToString());
                expens.CreationDate = DateTime.Now;
                expens.CreatedByUserID = Int32.Parse(Session["UserID"].ToString());
                expens.IsDeleted = 0;
                db.Expenses.Add(expens);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expens);
        }

        // GET: Expens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expens expens = db.Expenses.Find(id);
            if (expens == null)
            {
                return HttpNotFound();
            }
            return View(expens);
        }

        // POST: Expens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpensesID,BranchID,ExpensesTypeID,ExpensesDate,Remarks,ExpensesByID,CreationDate,CreatedByUserID,IsDeleted,ExpensesAmount")] Expens expens)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expens).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expens);
        }

        // GET: Expens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expens expens = db.Expenses.Find(id);
            if (expens == null)
            {
                return HttpNotFound();
            }
            return View(expens);
        }

        // POST: Expens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expens expens = db.Expenses.Find(id);
            expens.IsDeleted = 1;
            db.Entry(expens).State = EntityState.Modified;
            
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
