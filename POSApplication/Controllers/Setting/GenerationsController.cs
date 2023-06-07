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
    public class GenerationsController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();

        // GET: Generations
        public ActionResult Index()
        {
            return View(db.Generations.ToList());
        }

        // GET: Generations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Generation generation = db.Generations.Find(id);
            if (generation == null)
            {
                return HttpNotFound();
            }
            return View(generation);
        }

        // GET: Generations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Generations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenerationID,GenerationName")] Generation generation)
        {
            if (ModelState.IsValid)
            {
                db.Generations.Add(generation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(generation);
        }

        // GET: Generations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Generation generation = db.Generations.Find(id);
            if (generation == null)
            {
                return HttpNotFound();
            }
            return View(generation);
        }

        // POST: Generations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenerationID,GenerationName")] Generation generation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(generation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(generation);
        }

        // GET: Generations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Generation generation = db.Generations.Find(id);
            if (generation == null)
            {
                return HttpNotFound();
            }
            return View(generation);
        }

        // POST: Generations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Generation generation = db.Generations.Find(id);
            db.Generations.Remove(generation);
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
