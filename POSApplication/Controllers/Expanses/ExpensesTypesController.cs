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
    public class ExpensesTypesController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();

        // GET: ExpensesTypes
        public ActionResult Index()
        {
            return View(db.ExpensesTypes.Where(x => x.IsDeleted == "0").ToList());
        }

        // GET: ExpensesTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpensesType expensesType = db.ExpensesTypes.Find(id);
            if (expensesType == null)
            {
                return HttpNotFound();
            }
            return View(expensesType);
        }

        // GET: ExpensesTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpensesTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpensesTypeID,ExpensesTypeName,IsDeleted")] ExpensesType expensesType)
        {
            if (ModelState.IsValid)
            {
                db.ExpensesTypes.Add(expensesType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expensesType);
        }

        // GET: ExpensesTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpensesType expensesType = db.ExpensesTypes.Find(id);
            if (expensesType == null)
            {
                return HttpNotFound();
            }
            return View(expensesType);
        }

        // POST: ExpensesTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpensesTypeID,ExpensesTypeName,IsDeleted")] ExpensesType expensesType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expensesType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expensesType);
        }

        // GET: ExpensesTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpensesType expensesType = db.ExpensesTypes.Find(id);
            if (expensesType == null)
            {
                return HttpNotFound();
            }
            return View(expensesType);
        }

        // POST: ExpensesTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpensesType obj = db.ExpensesTypes.Find(id);
            obj.IsDeleted = "1";
            db.Entry(obj).State = EntityState.Modified;
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
