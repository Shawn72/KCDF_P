using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using KCDF_P.NavOData;
   
    public class Grantees
    {
        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
                  new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                      ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };
        public static string No { get; set; }
        public static string OrgUsername { get; set; }
        public static string MobileNo { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string orgname { get; set; }

        public static string SessionUsername;
        public Grantees(string username)
        {
            var objGrantees = nav.grantees_Register.Where(r => r.Organization_Username == username);
            foreach (var objGrantee in objGrantees)
            {
                No = objGrantee.No;
                Email = objGrantee.Email;
                orgname = objGrantee.Organization_Name;
                Password = objGrantee.Password;
                OrgUsername = objGrantee.Organization_Username;

            }

        }
    }
