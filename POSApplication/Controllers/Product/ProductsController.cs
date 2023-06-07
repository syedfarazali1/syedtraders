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
    public class ProductsController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();
        private IRepo<Product> ui;
        public ProductsController()
        {
            this.ui = new Utility<Product>();

        }
        string path = "~/assets/img/ProductImages";
        // GET: Products
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["BranchID"].ToString());
            ViewBag.cat = db.ProductCategories.Where(x =>x.BranchID == id && x.IsDeleted == 0).ToList();
            ViewBag.Brands = db.Brands.ToList();
            ViewBag.Model = db.Models.ToList();
            ViewBag.Generation = db.Generations.ToList();
            ViewBag.Ram = db.Rams.ToList();
            ViewBag.HardDisk = db.HardDisks.ToList();
            ViewBag.Processor = db.Processors.ToList();
            var Products = db.Products.Where(x => x.BranchID == id && x.IsDeleted == 0).ToList();


            return View(Products);  
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["BranchID"] != null)
            {
                int id = Convert.ToInt32(Session["BranchID"].ToString());

                ViewBag.ProdcutCategory = db.ProductCategories.Where(x => x.IsDeleted == 0 && x.BranchID == id).ToList();
                ViewBag.brand = db.Brands.ToList();
                ViewBag.Model = db.Models.ToList();
                ViewBag.Generation = db.Generations.ToList();
                ViewBag.Ram = db.Rams.ToList();
                ViewBag.HardDisk = db.HardDisks.ToList();
                ViewBag.Processor = db.Processors.ToList();
                return View();
            }

            else
            {
                return RedirectToAction("Login", "Accounts");
            }            
        }

        [HttpPost]
        public ActionResult barcodeCheck(Product product)
        {
            var a = product.BarCode;
            var check = db.Products.Where(x => x.BarCode == a).FirstOrDefault();
            if (check == null)
            {
                return Json("ok", JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json("exist", JsonRequestBehavior.AllowGet);

            }
        }
        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product product, HttpPostedFileBase img)
        {

                string filename = ui.SaveImage(img, path);
            if (filename == "File Type Error")
            {
                ViewBag.error = "Incorrect Datatype";
                return View(product);
            }
            else if (filename == "Error")
            {
                return View();
            }
            else
            {

            
            int id = Convert.ToInt32(Session["BranchID"].ToString());
                product.CreationDate = DateTime.Now;
                product.BranchID = id;
                product.ProductImage = filename;
                product.IsDeleted = 0;
                if (product.BarCode == null)
                {
                    product.BarCode = "0";
                }
 
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,BranchID,ProdcutCategoryID,ProductName,BrandNameID,ModelID,GenerationID,RamID,HardDiskID,ProcessorID,ProductImage,CreationDate,IsDeleted")] Product product,HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
              string filename =  ui.SaveImage(img, path);

                if (filename == "File Type Error")
                {
                    ViewBag.error = "Incorrect Datatype";
                    return View(product);
                }
                else if (filename == "Error")
                {
                    return View();
                }
                else
                {
                    product.CreationDate = DateTime.Now;
                    product.IsDeleted = 0;
                    int id = Convert.ToInt32(Session["BranchID"].ToString());
                    product.BranchID = id;
                    product.ProductImage = filename;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product obj = db.Products.Find(id);
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
