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
    public class EmployeeAdvancesController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();

        // GET: EmployeeAdvances
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["BranchID"].ToString());
            return View(db.EmployeeAdvances.Where(x => x.BranchID == id).ToList());
        }

        // GET: EmployeeAdvances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeAdvance employeeAdvance = db.EmployeeAdvances.Find(id);
            if (employeeAdvance == null)
            {
                return HttpNotFound();
            }
            return View(employeeAdvance);
        }

        // GET: EmployeeAdvances/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeAdvances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeAdvanceID,BranchID,EmployeeID,AdvanceDate,AdvanceAmount,IsPaid,IsDeleted")] EmployeeAdvance employeeAdvance)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["BranchID"].ToString());
                employeeAdvance.BranchID = id;
                employeeAdvance.IsDeleted = 0;
                db.EmployeeAdvances.Add(employeeAdvance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeeAdvance);
        }

        // GET: EmployeeAdvances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeAdvance employeeAdvance = db.EmployeeAdvances.Find(id);
            if (employeeAdvance == null)
            {
                return HttpNotFound();
            }
            return View(employeeAdvance);
        }

        // POST: EmployeeAdvances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeAdvanceID,BranchID,EmployeeID,AdvanceDate,AdvanceAmount,IsPaid,IsDeleted")] EmployeeAdvance employeeAdvance)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["BranchID"].ToString());
                employeeAdvance.BranchID = id;
                db.Entry(employeeAdvance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeAdvance);
        }

        // GET: EmployeeAdvances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeAdvance employeeAdvance = db.EmployeeAdvances.Find(id);
            if (employeeAdvance == null)
            {
                return HttpNotFound();
            }
            return View(employeeAdvance);
        }

        // POST: EmployeeAdvances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeAdvance obj = db.EmployeeAdvances.Find(id);
            obj.IsDeleted = 1;
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
