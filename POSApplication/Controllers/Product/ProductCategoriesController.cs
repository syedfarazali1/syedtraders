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
    public class ProductCategoriesController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();

        // GET: ProductCategories
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["BranchID"].ToString());
            var list = db.ProductCategories.Where(x => x.IsDeleted == 0 && x.BranchID == id).ToList();
            return View(list);
        }

        // GET: ProductCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["BranchID"].ToString());
                productCategory.BranchID = id;
                productCategory.CreationDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
                productCategory.IsDeleted = 0;
                db.ProductCategories.Add(productCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productCategory);
        }
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductCategoryID,BranchID,CategoryName,CreationDate,IsDeleted")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                int BranchID = Convert.ToInt32(Session["BranchID"].ToString());
                productCategory.BranchID = BranchID;
                productCategory.CreationDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
                productCategory.IsDeleted = 0;
                db.Entry(productCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productCategory);
        }

        public ActionResult Delete(int? id)
        {
            ProductCategory productCategory = db.ProductCategories.Find(id);
            productCategory.IsDeleted = 1;
            db.Entry(productCategory).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

  }
}
