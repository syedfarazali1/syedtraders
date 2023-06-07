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
    public class EmployeeLoansController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();

        // GET: EmployeeLoans
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["BranchID"].ToString());

            return View(db.EmployeeLoans.Where(x => x.BranchID == id).ToList());
        }

        // GET: EmployeeLoans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeLoan employeeLoan = db.EmployeeLoans.Find(id);
            if (employeeLoan == null)
            {
                return HttpNotFound();
            }
            return View(employeeLoan);
        }

        // GET: EmployeeLoans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeLoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeLoanID,BranchID,EmployeeID,LoanDate,LoanAmount,InstallmentAmount,PaidInstallment,numberOfInstallment,CreationDate,IsPaid")] EmployeeLoan employeeLoan)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["BranchID"].ToString());
                employeeLoan.BranchID = id;
                db.EmployeeLoans.Add(employeeLoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeeLoan);
        }

        // GET: EmployeeLoans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeLoan employeeLoan = db.EmployeeLoans.Find(id);
            if (employeeLoan == null)
            {
                return HttpNotFound();
            }
            return View(employeeLoan);
        }

        // POST: EmployeeLoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeLoanID,BranchID,EmployeeID,LoanDate,LoanAmount,InstallmentAmount,PaidInstallment,numberOfInstallment,CreationDate,IsPaid")] EmployeeLoan employeeLoan)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["BranchID"].ToString());
                employeeLoan.BranchID = id;
                db.Entry(employeeLoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeLoan);
        }

        // GET: EmployeeLoans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeLoan employeeLoan = db.EmployeeLoans.Find(id);
            if (employeeLoan == null)
            {
                return HttpNotFound();
            }
            return View(employeeLoan);
        }

        // POST: EmployeeLoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeLoan employeeLoan = db.EmployeeLoans.Find(id);
            db.EmployeeLoans.Remove(employeeLoan);
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
