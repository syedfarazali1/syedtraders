using POSApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace POSApplication.Controllers
{
    [AllowAnonymous]
    public class AccountsController : Controller
    {
        private PosintofsaleEntities db = new PosintofsaleEntities();
        private IRepo<Product> injec;
        public AccountsController()
        {
            this.injec = new Utility<Product>();

        }


        // GET: Accounts
      
        public ActionResult Index()
        {

            return View();
        }


        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (returnUrl != null)
            {
                Session["returnUrl"] = returnUrl;
            }

            return View();
        }
        [HttpGet]
        public ActionResult LoadBranches()
        {
            var logs = db.Branches.ToList();
            if (logs.Count > 0)
            {
                return Json(logs, JsonRequestBehavior.AllowGet);
            }
            return Json("ERROR", JsonRequestBehavior.AllowGet);

        }
        public ActionResult Login(login log)
        {
          string Login =  injec.GetSQLInject(log.Login);
            string Password=  injec.GetSQLInject(log.Password);
            int branch = Convert.ToInt32(injec.GetSQLInject(Convert.ToString(log.branchID)));

            var logs = db.Users.Where(x => x.UserName == Login && x.Password == Password && x.BranchID == branch && x.IsDeleted == 0).FirstOrDefault();
            if (logs != null)      
            {
                FormsAuthentication.SetAuthCookie(logs.UserName,false);
                Session["BranchID"] = log.branchID.ToString();
                Session["UserID"] = logs.UserID;
               string host = "smtp.outlook.com";
                string sendermail = "syedfarazali066@outlook.com";
                string password = "Muslimsr1";
                string subject = "Email checking";
                string body = "Dear this is checking mail body";
                string recievermail = "syedfarazali066@outlook.com";

                //   injec.SendEmail(host,sendermail,password,recievermail,subject,body);
                // Get the return URL from the Referrer property
 
                
                if (Session["returnUrl"] != null)
                {
                    string returnUrl = Session["returnUrl"].ToString();
                    Session["returnUrl"] = null;
                    return Json(returnUrl, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("/Registration/Supplies", JsonRequestBehavior.AllowGet);
                }


            }
            else if (log.Login == "Admin" && log.Password == "Admin")
            {
                return Json("Admin Login Successfull", JsonRequestBehavior.AllowGet);
                
            }
            else
            {
                return Json("Login Failed", JsonRequestBehavior.AllowGet);

            }
        }
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            
            return RedirectToAction("Login");
        }

    }
}