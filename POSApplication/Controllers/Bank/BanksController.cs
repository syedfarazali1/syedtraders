﻿//using POSApplication.Models;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web.Mvc;

//namespace POSApplication.Controllers.Bank
//{
//    public class BanksController : Controller
//    {

//        private PosintofsaleEntities db = new PosintofsaleEntities();

//        // GET: Banks
//        public ActionResult Index()
//        {
//            return View(db.Banks.ToList());
//        }

//        // GET: Banks/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            var bank = db.Banks.Find(id);
//            if (bank == null)
//            {
//                return HttpNotFound();
//            }
//            return View(bank);
//        }

//        // GET: Banks/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Banks/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "BankID,BankName,IsDeleted")] Banks bank)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Banks.Add(bank);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(bank);
//        }

//        // GET: Banks/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            var bank = db.Banks.Find(id);
//            if (bank == null)
//            {
//                return HttpNotFound();
//            }
//            return View(bank);
//        }

//        // POST: Banks/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "BankID,BankName,IsDeleted")] Bank bank)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(bank).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(bank);
//        }

//        // GET: Banks/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            var bank = db.Banks.Find(id);
//            if (bank == null)
//            {
//                return HttpNotFound();
//            }
//            return View(bank);
//        }

//        // POST: Banks/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            var bank = db.Banks.Find(id);
//            db.Banks.Remove(bank);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
