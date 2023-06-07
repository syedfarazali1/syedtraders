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
    public class SalariesController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();

        // GET: Salaries
        public ActionResult Index()
        {
            if (Session["BranchID"] != null)
            {

            
            int id = Convert.ToInt32(Session["BranchID"].ToString());

            return View(db.Salaries.Where(x => x.IsDeleted == 0 && x.BranchID == id).ToList());
            }
            else
            {
                return RedirectToAction("Login","Accounts");
            }
        }

        // GET: Salaries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // GET: Salaries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalaryID,BranchID,EmployeeID,BasicSalary,CommissionAmount,AdvanceAmount,LoanAmount,NetSalary,SalaryMonth,CreationDate,CreatedByUserID,IsDeleted")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                if (Session["BranchID"] != null)
                {


                    int id = Convert.ToInt32(Session["BranchID"].ToString());
                    salary.BranchID = id;
                    salary.IsDeleted = 0;
                    db.Salaries.Add(salary);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }

            }

            return View(salary);
        }

        // GET: Salaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalaryID,BranchID,EmployeeID,BasicSalary,CommissionAmount,AdvanceAmount,LoanAmount,NetSalary,SalaryMonth,CreationDate,CreatedByUserID,IsDeleted")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                if (Session["BranchID"] != null)
                {


                    int id = Convert.ToInt32(Session["BranchID"].ToString());
                    salary.BranchID = id;
                    db.Entry(salary).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }

            }
            return View(salary);
        }

        // GET: Salaries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salary obj = db.Salaries.Find(id);
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
