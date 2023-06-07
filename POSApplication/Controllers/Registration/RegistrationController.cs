using POSApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApplication.Controllers
{

    public class RegistrationController : Controller
    {
        PosintofsaleEntities db = new PosintofsaleEntities();
        private IRepo<User> injec;
        public RegistrationController()
        {
            this.injec = new Utility<User>();

        }

        #region User
        [Authorize(Roles = "User")]
        [OutputCache(NoStore = true, Duration = 0)]
        [HttpGet]
        public ActionResult User()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FillUsers()
        {
            int BranchID = Int32.Parse(Session["BranchID"].ToString());
            List<User> users = new List<User>();
            users = db.Users.Where(x => x.IsDeleted == 0 && x.IsActive == 0 && x.BranchID == BranchID).ToList();

            return Json(users, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AddUser(User us, HttpPostedFileBase UserImage)
        {
            if (UserImage != null)
            {
                us.UserImage = injec.SaveImage(UserImage, @"~\assets\img\UserImages\");

            }
            us.CreationDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
            us.IsActive = 0;
            us.IsDeleted = 0;
            

            string UserName = injec.GetSQLInject(us.UserName);
            string Password = injec.GetSQLInject(us.Password);
            if (UserName != "" && Password != "")
            {
                Role r = new Role();
                r.Role_Tittle = "User";
                var id = db.Users.Select(x => x.UserID).Max();
                int uid = id + 1;
                r.UserId = uid;
                us.BranchID = Int32.Parse(Session["BranchID"].ToString());
                db.Users.Add(us);
                db.Roles.Add(r);
                db.SaveChanges();
                return Json("Ok", JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json("Inject", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult UpdateUser(User user, HttpPostedFileBase UserImage)
        {
            if (UserImage != null)
            {
                user.UserImage = injec.SaveImage(UserImage, @"~\assets\img\UserImages\");

            }
            else
            {
                user.UserImage = db.Users.Where(x=>x.UserID == user.UserID).Select(x=>x.UserImage).FirstOrDefault();
            }
            user.CreationDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));

            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return Json("Ok", JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            User users = new User();
            users = db.Users.Where(x => x.IsDeleted == 0 && x.IsActive == 0 && x.UserID == user.UserID).FirstOrDefault();

            return Json(users, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Delete(User user)
        {

            var obj = db.Users.Where(x => x.UserID == user.UserID).FirstOrDefault();
            obj.IsDeleted = 1;
            db.Entry(obj).State = EntityState.Modified;

            db.SaveChanges();
            return Json("Delete Successfull", JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Suppliers
        public ActionResult Supplies()
        {
            return View();
        }

        public ActionResult loadsupplier()
        {

            int id = Convert.ToInt32(Session["BranchID"].ToString());


            var list = db.Suppliers.Where(x => x.IsDeleted == 0 && x.BranchID == id && x.IsActive == 0).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Addsupplier(Supplier supplier)
        {

            int id = Convert.ToInt32(Session["BranchID"].ToString());
            supplier.BranchID = id;
            supplier.CreationDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
            db.Suppliers.Add(supplier);
            db.SaveChanges();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Editsupplier(Supplier supplier)
        {
            var list = db.Suppliers.Where(x => x.SupplierID == supplier.SupplierID).FirstOrDefault();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Updatesupplier(Supplier supplier)
        {

            int id = Convert.ToInt32(Session["BranchID"].ToString());
            supplier.BranchID = id;
            supplier.CreationDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
            db.Entry(supplier).State = EntityState.Modified;
            db.SaveChanges();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Deletesupplier(Supplier supplier)
        {
            var obj = db.Suppliers.Find(supplier.SupplierID);
            obj.IsDeleted = 1;
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Customer
        [HttpGet]
        public ActionResult Customer()
        {


            return View();
        }
        public ActionResult Customer(Customer customer)
        {

            int id = Convert.ToInt32(Session["BranchID"].ToString());
            customer.BranchID = id;
            customer.CreationDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
            db.Customers.Add(customer);
            db.SaveChanges();
            return Json("Ok", JsonRequestBehavior.AllowGet);

        }
        public ActionResult LoadCustomer()
        {
            int id = Convert.ToInt32(Session["BranchID"].ToString());
            var cus = db.Customers.Where(x => x.IsDeleted == 0 && x.BranchID == id && x.IsActive == 0).ToList();
            return Json(cus, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Deletecustomer(Customer Customer)
        {
            var obj = db.Customers.Where(x => x.CustomerID == Customer.CustomerID).FirstOrDefault();
            obj.IsDeleted = 1;
            db.Entry(obj).State = EntityState.Modified;

            db.SaveChanges();
            return Json("Delete Successfull", JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public ActionResult EditCustomer(Customer Customer)
        {
            var obj = db.Customers.Where(x => x.CustomerID == Customer.CustomerID).FirstOrDefault();

            return Json(obj, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public ActionResult UpdateCustomer(Customer customer)
        {


            int id = Convert.ToInt32(Session["BranchID"].ToString());
            customer.BranchID = id;
            customer.CreationDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
            return Json("Ok", JsonRequestBehavior.AllowGet);

        }
        #endregion  

        #region Engineers
        [HttpGet]
        public ActionResult Engineers()
        {


            return View();
        }
        public ActionResult Engineers(Engineer engineer)
        {
            int id = Convert.ToInt32(Session["BranchID"].ToString());
            engineer.BranchID = id;
            engineer.CreationDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
            db.Engineers.Add(engineer);
            db.SaveChanges();
            return Json("Ok", JsonRequestBehavior.AllowGet);


        }
        public ActionResult LoadEngineers()
        {


            int id = Convert.ToInt32(Session["BranchID"].ToString());
            var cus = db.Engineers.Where(x => x.IsDeleted == 0 && x.BranchID == id && x.IsActive == 0).ToList();
            return Json(cus, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public ActionResult DeleteEngineers(Engineer Engineer)
        {
            var obj = db.Engineers.Where(x => x.EngineerID == Engineer.EngineerID).FirstOrDefault();
            obj.IsDeleted = 1;
            db.Entry(obj).State = EntityState.Modified;

            db.SaveChanges();
            return Json("Delete Successfull", JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public ActionResult EditEngineers(Engineer Engineer)
        {
            var obj = db.Engineers.Where(x => x.EngineerID == Engineer.EngineerID).FirstOrDefault();

            return Json(obj, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public ActionResult UpdateEngineers(Engineer engineer)
        {

            int id = Convert.ToInt32(Session["BranchID"].ToString());
            engineer.BranchID = id;
            engineer.CreationDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
            db.Entry(engineer).State = EntityState.Modified;
            db.SaveChanges();
            return Json("Ok", JsonRequestBehavior.AllowGet);



        }
        #endregion
    }
}