using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using KCDF_P.NavOData;

    public class Students
    {

        public static string No { get; set; }
        public static string fName { get; set; }
        public static string mName { get; set; }
        public static string lName { get; set; }
        public static string Username { get; set; }
        public static string MobileNo { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string Name;
        public static string IDNo { get; set; }
        public static string Address { get; set; }
        public static string doB { get; set; } 
        public static string gender { get; set; }
        public static  string primo { get; set; }
        public static  string seco { get; set; }
        public static string univ { get; set; }
        public static string yearofAd { get; set; }
        public static string yrofCompltn { get; set; }
        public static string yrofstudy { get; set; }
        public static string course { get; set; }
        public static string filename;
        public static string ext;
        public static string imgname;


        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
                    new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                        ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };

        public Students(string user)
        {
        
            var objStudents = nav.studentsRegister.Where(r => r.Username == user);
            foreach (var objStudent in objStudents)
            {
                No = objStudent.No;
                fName = objStudent.First_name;
                mName = objStudent.Middle_name;
                lName = objStudent.Last_name;
                Email = objStudent.Email;
                Username = objStudent.Username;
                Password = objStudent.Password;
                MobileNo = objStudent.Phone_Number;
                IDNo = objStudent.ID_No;
                Address = objStudent.Residence;
                DateTime db = Convert.ToDateTime(objStudent.Date_of_Birth);
                doB = db.ToShortDateString();
                gender = objStudent.Gender;
                primo = objStudent.Primary_School;
                seco = objStudent.Secondary_School;
                univ = objStudent.University_or_College;
                DateTime yoa = Convert.ToDateTime(objStudent.Year_of_Admission);
                yearofAd = yoa.ToShortDateString();
                DateTime yroc = Convert.ToDateTime(objStudent.Year_of_Completion);
                yrofCompltn = yroc.ToShortDateString();
                yrofstudy = objStudent.Year_of_Study;
                course = objStudent.Course;
            }
               Name = fName + " " + mName + " " + lName;

     }
}