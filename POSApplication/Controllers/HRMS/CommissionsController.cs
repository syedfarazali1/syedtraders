using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using POSApplication.Models;

namespace POSApplication.Controllers.HRMS
{
    public class CommissionsController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();

        // GET: Commissions
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["BranchID"].ToString());
            return View(db.Commissions.Where(x =>x.BranchID == id).ToList());
        }

        // GET: Commissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commission commission = db.Commissions.Find(id);
            if (commission == null)
            {
                return HttpNotFound();
            }
            return View(commission);
        }

        // GET: Commissions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Commissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommissionID,BranchID,EmployeeID,SalesID,SalesDate,CommissionAmount,IsPaid")] Commission commission)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["BranchID"].ToString());
                commission.BranchID = id;
                db.Commissions.Add(commission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commission);
        }

        // GET: Commissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commission commission = db.Commissions.Find(id);
            if (commission == null)
            {
                return HttpNotFound();
            }
            return View(commission);
        }

        // POST: Commissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommissionID,BranchID,EmployeeID,SalesID,SalesDate,CommissionAmount,IsPaid")] Commission commission)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["BranchID"].ToString());
                commission.BranchID = id;
                db.Entry(commission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commission);
        }

        // GET: Commissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commission commission = db.Commissions.Find(id);
            if (commission == null)
            {
                return HttpNotFound();
            }
            return View(commission);
        }

        // POST: Commissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commission commission = db.Commissions.Find(id);
            db.Commissions.Remove(commission);
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
