using POSApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace POSApplication.Controllers
{
    public class Utility <T> : IRepo<T> where T : class
    {
        PosintofsaleEntities db;
        private IDbSet<T> TableName;
        public Utility()
        {
            db = new PosintofsaleEntities();
            TableName = db.Set<T>();
        }

        public IEnumerable<T> Get()
        {
            return TableName.ToList();
        }

        public void InsertData(T model)
        {
            TableName.Add(model);

        }
        public void Savechanges(T model)
        {
            db.SaveChanges();
        }

        public T GetById(int modelid)
        {
            return TableName.Find(modelid);
        }
        public void Update(T model)
        {
            db.Entry(model).State = EntityState.Modified;
        }
        public void Delete(int modelid)
        {
            T model = TableName.Find(modelid);
            TableName.Remove(model);

        }
        public string GetSQLInject(string txtString)
        {
            txtString = txtString.ToLower();
            string sqlstr = txtString.Replace("--", "").Replace("'", "").Replace("/*", "").Replace(";", "").Replace(" OR ", "").Replace(" or ", "").Replace(" oR ", "").Replace(" Or ", "").Replace(";--", "").Replace("*/", "").Replace("@@", "");
            sqlstr = sqlstr.Replace("begin", "").Replace("create", "").Replace("cursor", "").Replace("declare", "").Replace("delete", "").Replace("drop", "").Replace("end", "").Replace("exec", "").Replace("execute", "").Replace("fetch", "");
            sqlstr = sqlstr.Replace("insert", "").Replace("kill", "").Replace("sys", "").Replace("sysobjects", "").Replace("syscolumns", "").Replace("alter", "").Replace("1=1", "").Replace("#", "");
            return sqlstr;
        }
        public string SaveImage(HttpPostedFileBase Img, string path)
        {
            try
            {
                Random rd = new Random();
                string extension1 = Path.GetExtension(Img.FileName);
                if ((extension1 == ".png" || extension1 == ".jpg"))
                {

                    string _fileName = "imgs_" + rd.Next(100, 100000) + DateTime.Now.ToString("HHmmssddMMYYYY") + Path.GetExtension(Img.FileName);
                    string _Path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(path), _fileName);
                    Img.SaveAs(_Path);


                    return _fileName;

                }
                else
                {
                    return "File Type Error";


                }
            }
            catch (Exception)
            {
                return "Error";

                throw;
            }

        }

        public void SendEmail(string host, string sendermail, string password, string recievermail, string subject, string body)
        {
            host = "smtp.outlook.com";
            sendermail = "syedfarazali066@outlook.com";
            password = "Muslimsr1";
            subject = "Email checking";
            body = "Dear this is checking mail body";
            recievermail = "syedfarazali066@outlook.com";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = host;
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential(sendermail, password);
            smtp.EnableSsl = true;

            MailMessage msg = new MailMessage();
            msg.Subject = subject;
            msg.Body = body;
            string toemail = recievermail;
            msg.To.Add(toemail);
            string formmail = sendermail;
            msg.From = new MailAddress(formmail);
            smtp.Send(msg);
        }
    }
}