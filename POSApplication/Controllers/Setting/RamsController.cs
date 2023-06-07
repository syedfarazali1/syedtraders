using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using POSApplication.Models;

namespace POSApplication.Controllers
{
    public class RamsController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();

        // GET: Rams
        public ActionResult Index()
        {
            return View(db.Rams.ToList());
        }

        // GET: Rams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ram ram = db.Rams.Find(id);
            if (ram == null)
            {
                return HttpNotFound();
            }
            return View(ram);
        }

        // GET: Rams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RamID,RamName")] Ram ram)
        {
            if (ModelState.IsValid)
            {
                db.Rams.Add(ram);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ram);
        }

        // GET: Rams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ram ram = db.Rams.Find(id);
            if (ram == null)
            {
                return HttpNotFound();
            }
            return View(ram);
        }

        // POST: Rams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RamID,RamName")] Ram ram)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ram).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ram);
        }

        // GET: Rams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ram ram = db.Rams.Find(id);
            if (ram == null)
            {
                return HttpNotFound();
            }
            return View(ram);
        }

        // POST: Rams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ram ram = db.Rams.Find(id);
            db.Rams.Remove(ram);
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
