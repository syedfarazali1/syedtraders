using CrystalDecisions.CrystalReports.Engine;
using POSApplication.Models;
using POSApplication.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace POSApplication.Controllers
{
    public class PurchaseInvoiceController : Controller
    {
        PosintofsaleEntities db = new PosintofsaleEntities();
        // GET: PurchaseReport
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Index(string supplier)
        {
            return View();
        }
        public ActionResult GeneratePDF()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProdcutName", typeof(String));
            dt.Columns.Add("quantity", typeof(Int32));
            dt.Columns.Add("unitPrice", typeof(Decimal));
            dt.Columns.Add("amount", typeof(Decimal));

            // Generate HTML for the invoice
            string invoiceHtml = "<html><body><h2>Invoice</h2><p>Invoice details go here.</p></body></html>";

            // Create a MemoryStream to hold the PDF
            MemoryStream outputStream = new MemoryStream();

            // Create a TextWriter with UTF-8 encoding
            using (TextWriter writer = new StreamWriter(outputStream, Encoding.UTF8, 4096, true))
            {
                // Set the content type and encoding for the response
                Response.ContentType = "application/octet-stream"; 
                Response.ContentEncoding = Encoding.UTF8;

                // Create an HtmlTextWriter to render the HTML content
                using (HtmlTextWriter htmlWriter = new HtmlTextWriter(writer))
                {
                    // Render the HTML content into the writer
                    htmlWriter.Write(invoiceHtml);
                }

                // Flush the writer to ensure all content is written to the MemoryStream
                writer.Flush();

                // Set the position of the MemoryStream to the beginning
                outputStream.Position = 0;

                // Return the PDF file
                return File(outputStream, "application/pdf", "invoice"+DateTime.Now.ToString("g")+".pdf");

            }
        }
        }
}