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
    public class ProcessorsController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();

        // GET: Processors
        public ActionResult Index()
        {
            return View(db.Processors.ToList());
        }

        // GET: Processors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Processor processor = db.Processors.Find(id);
            if (processor == null)
            {
                return HttpNotFound();
            }
            return View(processor);
        }

        // GET: Processors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Processors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProcessorID,ProcessorName")] Processor processor)
        {
            if (ModelState.IsValid)
            {
                db.Processors.Add(processor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(processor);
        }

        // GET: Processors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Processor processor = db.Processors.Find(id);
            if (processor == null)
            {
                return HttpNotFound();
            }
            return View(processor);
        }

        // POST: Processors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProcessorID,ProcessorName")] Processor processor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(processor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(processor);
        }

        // GET: Processors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Processor processor = db.Processors.Find(id);
            if (processor == null)
            {
                return HttpNotFound();
            }
            return View(processor);
        }

        // POST: Processors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Processor processor = db.Processors.Find(id);
            db.Processors.Remove(processor);
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
