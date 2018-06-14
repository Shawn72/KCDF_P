using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using KCDF_P.NavOData;

namespace KCDF_P
{
    public class ConsultantClass
    {
        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
                   new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                       ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };

        public static  string No { get; set; }
        public static string fullname { get; set; }
        public static string contaCFName { get; set; }
        public static string contPposition { get; set; }
        public static string Email { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string MobileNo { get; set; }
        public static string IDNoReg { get; set; }
        public ConsultantClass(string username)
        {
            var objCons = nav.myConsultants.Where(r => r.Organization_Username == username);
            foreach (var objConsult in objCons)
            {
                No = objConsult.No;
                fullname = objConsult.Organization_Name;
                contaCFName = objConsult.Contact_Person_FullName;
                contPposition = objConsult.Contact_Person_Position;
                Email = objConsult.Organization_Email;
                Username = objConsult.Organization_Username;
                Password = objConsult.Password;
                MobileNo = objConsult.Phone_Number;
                IDNoReg = objConsult.Organization_Registration_No;
            }
        }
    }
}