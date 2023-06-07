using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace POSApplication.Controllers
{
    interface IRepo <T> where T : class
    {
         IEnumerable<T> Get();
        void InsertData(T model);
        void Savechanges(T model);
        T GetById(int modelid);
        void Update(T model);

        void Delete(int modelid);

        string GetSQLInject(string txtString);

        string SaveImage(HttpPostedFileBase Img, string path);

        void SendEmail(string host, string sendermail, string password, string recievermail, string subject, string body);
    }
}
