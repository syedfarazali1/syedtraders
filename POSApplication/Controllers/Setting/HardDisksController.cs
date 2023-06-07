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
    public class HardDisksController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();

        // GET: HardDisks
        public ActionResult Index()
        {
            return View(db.HardDisks.ToList());
        }

        // GET: HardDisks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HardDisk hardDisk = db.HardDisks.Find(id);
            if (hardDisk == null)
            {
                return HttpNotFound();
            }
            return View(hardDisk);
        }

        // GET: HardDisks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HardDisks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HardDiskID,HardDiskName")] HardDisk hardDisk)
        {
            if (ModelState.IsValid)
            {
                db.HardDisks.Add(hardDisk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hardDisk);
        }

        // GET: HardDisks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HardDisk hardDisk = db.HardDisks.Find(id);
            if (hardDisk == null)
            {
                return HttpNotFound();
            }
            return View(hardDisk);
        }

        // POST: HardDisks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HardDiskID,HardDiskName")] HardDisk hardDisk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hardDisk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hardDisk);
        }

        // GET: HardDisks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HardDisk hardDisk = db.HardDisks.Find(id);
            if (hardDisk == null)
            {
                return HttpNotFound();
            }
            return View(hardDisk);
        }

        // POST: HardDisks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HardDisk hardDisk = db.HardDisks.Find(id);
            db.HardDisks.Remove(hardDisk);
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
