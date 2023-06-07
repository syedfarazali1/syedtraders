using POSApplication.App_Classes;
using POSApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApplication.Controllers.Sales
{
    public class SalesReturnController : Controller
    {
        PosintofsaleEntities db = new PosintofsaleEntities();

        List<salesaddtocart> sl2 = new List<salesaddtocart>();

        // GET: SalesReturn
        public ActionResult Create()
        {
            if(Session["BranchID"] != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Login", "Accounts");
            }
        }
        [HttpPost]
        public ActionResult GetSale(int SaleID)
        {
            if (Session["BranchID"] != null)
            {
                int brachid = Int32.Parse(Session["BranchID"].ToString());

                var query = from sale in db.Sales
                            join details in db.SalesDetails on sale.SalesID equals details.SalesID
                            join product in db.Products on details.ProductID equals product.ProductID
                            where sale.SalesID == SaleID && sale.IsReturn == 0 && details.IsReturn == 0 && sale.BranchID == brachid
                            select new
                            {
                                details.SalesDetailID,
                                details.SalesID,
                                product.ProductName,
                                details.Quantity,
                                details.UnitPrice,
                                details.Amount
                            };
                var result = query.ToList();

                if (result.Count > 0)
                {
                    Session["Saleid"] = SaleID;
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("Null", JsonRequestBehavior.AllowGet);
                }
             
            }
            else
            {

                return Json("Redirect", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult ReturnItem(SalesDetail sd)
        {
            var list = db.SalesDetails.Find(sd.SalesDetailID);
            list.IsReturn = 1;
            db.SaveChanges();

            return Json("Ok", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SalesReturn()
        {
            int SalesID = Int32.Parse(Session["Saleid"].ToString());
            var list = db.Sales.Find(SalesID);
            list.IsReturn = 1;
            db.SaveChanges();
            Session["Saleid"] = null;
            return Json("Sale Return Successfull", JsonRequestBehavior.AllowGet);
        }


    }
}