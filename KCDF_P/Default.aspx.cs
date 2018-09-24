using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;

namespace KCDF_P
{
    public partial class Default : System.Web.UI.Page
    {
        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
               new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"],
                   ConfigurationManager.AppSettings["DOMAIN"])
        };
        public readonly string strSQLConn = @"Server=" + ConfigurationManager.AppSettings["DB_INSTANCE"] + ";Database=" +
                                  ConfigurationManager.AppSettings["DB_NAME"] + "; User ID=" +
                                  ConfigurationManager.AppSettings["DB_USER"] + "; Password=" +
                                  ConfigurationManager.AppSettings["DB_PWD"] + "; MultipleActiveResultSets=true";

        public string Company_Name = "KCDF";
       
        protected void Page_Load(object sender, EventArgs e)
        {
            NoCache();
        }

        public void NoCache()
        {
            Response.CacheControl = "private";
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }
        //protected override void CreateChildControls()
        //{
        //    base.CreateChildControls();
        //    ctrlGoogleReCaptcha.PublicKey = "6LdK7j4UAAAAAJaWiKryMXWxVcwuDAyjEb_Kr204";
        //    ctrlGoogleReCaptcha.PrivateKey = "6LdK7j4UAAAAAC1ovoMUpMxXODnYYsWaebjMbbf0";
        // }
        static string EncryptP(string mypass)
        {
            //encryptpassword:
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(mypass));
                return Convert.ToBase64String(data);
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            cptCaptcha.ValidateCaptcha(txtCaptcha.Text.Trim());
            if (cptCaptcha.UserValidated)
            {
                string userName = txtUsername.Text.Trim().Replace("'", "");
                string userPassword = txtPassword.Text.Trim().Replace("'", "");

                string mypassencrypt = EncryptP(userPassword);

                if (string.IsNullOrWhiteSpace(userPassword))
                {
                    lblError.Text = "Password Empty!";
                    KCDFAlert.ShowAlert("Password Empty!");
                    return;
                }

                try
                {
                    //if (nav.studentsRegister.Where(r => r.Username == userName && r.Activated == true && r.Password == mypassencrypt).FirstOrDefault() != null)
                    //{
                    //    //Change the Session called "Logged" value into "Yes"
                    //    Session["Logged"] = "Yes";
                    //    Session["username"] = userName;
                    //    Session["pwd"] = userPassword;
                    //    rememberMeYeah(userName, userPassword);
                    //    Session["reportformUser"] = "iamStudent";
                    //    CheckStudentsProfile(userName);
                    //    Response.Redirect("~/Dashboard.aspx");

                    //}
                    //else if (nav.studentsRegister.Where(r => r.Username == userName && r.Activated == false && r.Password == userPassword).FirstOrDefault() != null)
                    //{
                    //    //HotelFactory.ShowAlert("You Ain't Active!!");
                    //    lblError.Text = "Account not active, please activate!";
                    //    return;
                    //}
                    //else
                    //{
                    //    lblError.Text = "Authentication failed!";
                    //    return;
                    //}
                    ValidateScholar(userName, userPassword);
                }
                catch (Exception exception)
                {
                    lblError.Text = exception.Message;
                    return;
                }
            }
            else
            {
                lblError.Text = "Invalid Captcha. Try again!";
            }

        }

        protected string NewPassword()
        {
            var nPwd = "";
            var rdmNumber = new Random();
            nPwd = rdmNumber.Next(1000, 1999).ToString();
            return nPwd;
        }
        
        protected void btnSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("Account/Register.aspx");
        }
        
        protected void ddlUserType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string ddlUsrT = ddlUserType.SelectedItem.Text;

            switch (ddlUsrT)
            {
                case "Student":
                    LoginViews.SetActiveView(studentView);
                    break;

                case "Grantee":
                    LoginViews.SetActiveView(granteeView);
                    break;

                case "Consultant":
                    LoginViews.SetActiveView(viewConsultant);
                    break;
            }
          
        }

        protected void btnGrantLogin_OnClick(object sender, EventArgs e)
        {
            captchaGrantee.ValidateCaptcha(txtCaptcha2.Text.Trim());
            if (captchaGrantee.UserValidated)
            {
                string userName = txtorgUsn.Text.Trim().Replace("'", "");
                string userPassword = txtorgPsw.Text.Trim().Replace("'", "");
                string enP = EncryptP(userPassword);

                //try
                //{
                if (string.IsNullOrWhiteSpace(userPassword))
                {
                    lblError.Text = "Password Empty!";
                    KCDFAlert.ShowAlert("Password Empty!");
                    return;
                }
                    //OK
                    CheckExistsSql(userName, userPassword);
                //}
                //catch (Exception exception)
                //{
                //    lblError.Text = exception.Message;
                //    return;
                //}
            }
            else
            {
                lblError.Text = "Invalid Captcha. Try again!";
            }
            
        }
        public void LoginFn(string eml, string pass)
        {
            string SQLRQST = @"SELECT [Organization Username], Password from [" + Company_Name + "$Grantees] WHERE [Organization Username]=@usnM AND Password=@passAssD";
            SqlConnection con = new SqlConnection(strSQLConn);
            SqlCommand command = new SqlCommand(SQLRQST, con);
            command.Parameters.AddWithValue("@usnM",eml);
            command.Parameters.AddWithValue("@passAssD", pass);
            con.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                if ((eml == dr["Organization Username"].ToString()) && (pass== dr["Password"].ToString()))
                {
                    //System.Web.UI.HtmlControls.HtmlGenericControl lblMastersession =
                    //(System.Web.UI.HtmlControls.HtmlGenericControl) Master.FindControl("lblSessionUsername");
                    //lblMastersession.InnerText = eml;
                    //Change the Session called "Logged" value into "Yes"
                    Session["Logged"] = "Yes";
                    Session["username"] = eml;
                    Session["pwd"] = pass;
                    Session["reportformUser"] = "iamGrantee";


                    FormsAuthentication.GetAuthCookie(eml, true);
                    FormsAuthentication.SetAuthCookie(eml, true);

                    rememberMeYeah(eml, pass);
                    CheckUserProfileUpdates(eml);
                   //Response.Redirect("~/Grantee_Dashboard.aspx");
                   break;
                }
            }

        }

        protected void CheckExistsSql(string myUserName, string passMyass)
        {
            var iamThere = nav.grantees_Register.ToList().Where(usN => usN.Organization_Username == myUserName).SingleOrDefault();
            
            if (iamThere==null)
            {
              KCDFAlert.ShowAlert("Username not registered yet!"); 
              return; 
            }
            else
            {
                GetGranteePassword(myUserName, passMyass);
                // KCDFAlert.ShowAlert("I see u");
            }

        }

        private void CheckIfActive(string usnUsername, string passWord )
        {

            try
            {
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;

                if (sup.FnCheckIfActive_Grantee(usnUsername))
                {
                    // KCDFAlert.ShowAlert("Active Member");
                    GetGranteePassword(usnUsername, passWord);
                    LoginFn(usnUsername, passWord);
                    return;
                }

            }
            catch(Exception ex)
            {
              KCDFAlert.ShowAlert(ex.Message);  
            }

        }

        private void GetGranteePassword(string mYusNm, string myPassWd)
        {
            var myPass =
                  nav.grantees_Register.ToList()
                      .Where(or => or.Organization_Username == mYusNm)
                      .Select(p => p.Password)
                      .SingleOrDefault();
            if (myPass != EncryptP(myPassWd))
            {
               KCDFAlert.ShowAlert("Wrong Password!");
                lblError.Text = "Wrong Password!!";
                return;
            }
            else
            {
                ValidateGrantee(mYusNm, myPassWd);
            }
        }

        protected void lnkBtnResetPassword_OnClick(object sender, EventArgs e)
        {
          //for scholarships
            Session["resetPassword"] = "scholarship";
          //  KCDFAlert.ShowAlert(Session["resetPassword"].ToString());
            LoginViews.SetActiveView(viewiForgotItP);

        }

        protected void lnkBtnOrgResetP_OnClick(object sender, EventArgs e)
        {
            //for organization
            Session["resetPassword"] = "grant";
            //KCDFAlert.ShowAlert(Session["resetPassword"].ToString());
            LoginViews.SetActiveView(viewiForgotItP);
        }

        protected void btnResetUrPss_OnClick(object sender, EventArgs e)
        {
            var sessionSet = Session["resetPassword"].ToString();
            switch (sessionSet)
            {
                case "scholarship":
                    ResetmyScholarshipPass();
                    break;
                case "grant":
                    ResetmyGranteePassword();
                    break;
                case "consultant":
                    ResetmyConsultantPassword();
                    break;
            }

        }

        protected void ResetmyGranteePassword()
        {
            var myEmailIs = txtIforgotPassword.Text.Trim();

           try
            {
                var nPassword = NewPassword();
                var CompEmail = WSConfig.ObjNav.FnGranteePasswordReset(myEmailIs, EncryptP(nPassword));
                if (WSConfig.MailFunction(string.Format("Dear Member,\n Your New password is: {0}",nPassword), CompEmail,
                    "Grantee Portal password reset successful") && !String.IsNullOrEmpty(CompEmail))
                {
                    KCDFAlert.ShowAlert("A New Password has been generated and sent to your registered E-mail Address.");
                       
                }
             }
            catch (Exception exception)
            {
                KCDFAlert.ShowAlert(exception.Message);
            }
         }

        protected void ResetmyScholarshipPass()
        {
            var myEmailIs = txtIforgotPassword.Text.Trim();

            try
            {
                var nPassword = NewPassword();
                var CompEmail = WSConfig.ObjNav.FnStudentPasswordReset(myEmailIs, EncryptP(nPassword));
                if (WSConfig.MailFunction(string.Format("Dear Member,\n Your New password is: {0}", nPassword), CompEmail,
                    "Scholarship Portal password reset successful") && !String.IsNullOrEmpty(CompEmail))
                {
                    KCDFAlert.ShowAlert("A New Password has been generated and sent to your registered E-mail Address.");
                }
            }
            catch (Exception exception)
            {
                KCDFAlert.ShowAlert(exception.Message);
            }
        }

        protected void ResetmyConsultantPassword()
        {
            var myEmailIs = txtIforgotPassword.Text.Trim();

            try
            {
                var nPassword = NewPassword();
                var CompEmail = WSConfig.ObjNav.FnConsultantPasswordReset(myEmailIs, EncryptP(nPassword));
                if (WSConfig.MailFunction(string.Format("Dear Member,\n Your New password is: {0}", nPassword), CompEmail,
                    "Grantee Portal password reset successful") && !String.IsNullOrEmpty(CompEmail))
                {
                    KCDFAlert.ShowAlert("A New Password has been generated and sent to your registered E-mail Address.");

                }
            }
            catch (Exception exception)
            {
                KCDFAlert.ShowAlert(exception.Message);
            }
        }

        protected void btnbacktomyRoots_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void rememberMeYeah(string cookieuser, string cookiepasswd)
        {
            if (chkRememberMe.Checked || checkRemMegrant.Checked || chckMeConsult.Checked)
            {
                Response.Cookies["username"].Expires = DateTime.Now.AddDays(30);
                Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(30);
            }
            else
            {
                Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(-1);

            }
            Response.Cookies["username"].Value = cookieuser;
            Response.Cookies["pwd"].Value = cookiepasswd;
        }

        protected void lnkBtnResetPCons_OnClick(object sender, EventArgs e)
        {
            //for consultant
            Session["resetPassword"] = "consultant";
            //KCDFAlert.ShowAlert(Session["resetPassword"].ToString());
            LoginViews.SetActiveView(viewiForgotItP);
        }

        protected void btnLogConsultant_OnClick(object sender, EventArgs e)
        {
            cptCaptcha.ValidateCaptcha(txtCaptcha3.Text.Trim());
            if (cptCaptcha.UserValidated)
            {
                try
                {

                    string userName = txtConsUsernameIS.Text.Trim().Replace("'", "");
                    string userPassword = txtConsPasswordIS.Text.Trim().Replace("'", "");

                    if (string.IsNullOrWhiteSpace(userPassword))
                    {
                        lblError.Text = "Password Empty!";
                        KCDFAlert.ShowAlert("Password Empty!");
                        return;
                    }
                    if (nav.myConsultants.Where(r => r.Organization_Username == userName && r.Active == true && r.Password == EncryptP(userPassword)).FirstOrDefault() != null)
                    {
                        //Change the Session called "Logged" value into "Yes"
                        Session["Logged"] = "Yes";
                        Session["username"] = userName;
                        Session["pwd"] = EncryptP(userPassword);
                        rememberMeYeah(userName, EncryptP(userPassword));
                        Session["reportformUser"] = "iamConsult";
                        GetConsultantNoSession(userName);
                        FormsAuthentication.GetAuthCookie(userName, true);
                        FormsAuthentication.SetAuthCookie(userName, true);
                        Response.Redirect("~/Consultancy_Page.aspx");

                    }
                    else if (nav.myConsultants.Where(r => r.Organization_Username == userName && r.Active == false && r.Password == EncryptP(userPassword)).FirstOrDefault() != null)
                    {
                        lblError.Text = "Account not active, please activate!";
                        return;
                    }
                    else
                    {
                        lblError.Text = "Authentication failed!";
                        return;
                    }
                }
                catch (Exception exception)
                {
                    lblError.Text = exception.Message;
                    return;
                }
            }
            else
            {
                lblError.Text = "Invalid Captcha. Try again!";
            }

        }

        protected void CheckStudentsProfile(string userN)
        {
            try
            {
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                bool amupdated = sup.FnCheckScholarProfileUpdated(userN);
                switch (amupdated)
                {
                    case true:
                        //FormsAuthentication.RedirectFromLoginPage(userN, chkRememberMe.Checked);
                       Response.Redirect("~/Dashboard.aspx");
                        break;

                    case false:
                       // FormsAuthentication.RedirectFromLoginPage(userN, chkRememberMe.Checked);
                        Response.Redirect("Account/Add_Students_Profile.aspx");
                        break;
                }


            }
            catch (Exception e)
            {
            }
        }
    
        protected void CheckUserProfileUpdates(string userN)
        {
            try
            {
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                bool amupdated = sup.FnCheckGranteeProfileUpdated(userN);
                switch (amupdated)
                {
                    case true:
                        FormsAuthentication.RedirectFromLoginPage(userN, chkRememberMe.Checked);
                        Response.Redirect("~/Grantee_Dashboard.aspx");
                        break;

                    case false:
                        FormsAuthentication.RedirectFromLoginPage(userN, chkRememberMe.Checked);
                        Response.Redirect("Account/Add_Grantee_Profile.aspx");
                        break;
                }


            }
            catch (Exception e)
            {
            }
        }

        protected void ValidateGrantee(string UserName, string Password)
        {
            try
            {
                var myUID = "";

                using (SqlConnection con = new SqlConnection(strSQLConn))
                {
                
                using (SqlCommand cmd = new SqlCommand("Validate_UserGrantee"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", UserName);
                        cmd.Parameters.AddWithValue("@Password", EncryptP(Password));
                        cmd.Connection = con;

                        con.Open();
                        myUID = Convert.ToString(cmd.ExecuteScalar());
                        con.Close();

                    }
                    switch (myUID)
                    {
                        case "invalid":
                            KCDFAlert.ShowAlert("Aunthentication Failed!");
                            lblError.Text = "Aunthentication Failed!";
                            break;
                        case "deactivated":
                            KCDFAlert.ShowAlert("Account has not been activated!");
                            lblError.Text = "Account has not been activated";
                            break;
                        default:
                            Session.Clear();
                            FormsAuthentication.RedirectFromLoginPage(UserName, chkRememberMe.Checked);
                            Session["Logged"] = "Yes";
                            Session["username"] = UserName;
                            Session["pwd"] = Password;
                            Session["reportformUser"] = "iamGrantee";
                            GetGranteeNoSession(UserName);

                            FormsAuthentication.GetAuthCookie(UserName, false);
                            FormsAuthentication.SetAuthCookie(UserName, false);
                            rememberMeYeah(UserName, Password);
                            CheckUserProfileUpdates(UserName);
                            break;
                    }
                }

            }
            catch (Exception ex)
            {

                KCDFAlert.ShowAlert(ex.Message);
            }

        }


        protected void ValidateScholar(string UserName, string Password)
        {
            try
            {
                var myUID = "";

                using (SqlConnection con = new SqlConnection(strSQLConn))
                {

                    using (SqlCommand cmd = new SqlCommand("Validate_UserScholar"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", UserName);
                        cmd.Parameters.AddWithValue("@Password", EncryptP(Password));
                        cmd.Connection = con;

                        con.Open();
                        myUID = Convert.ToString(cmd.ExecuteScalar());
                        con.Close();

                    }
                    switch (myUID)
                    {
                        case "invalid":
                            KCDFAlert.ShowAlert("Aunthentication Failed!");
                            lblError.Text = "Aunthentication Failed!";
                            break;
                        case "deactivated":
                            KCDFAlert.ShowAlert("Account has not been activated!");
                            lblError.Text = "Account has not been activated";
                            break;
                        default:
                            Session.Clear();
                            FormsAuthentication.RedirectFromLoginPage(UserName, chkRememberMe.Checked);
                            //Change the Session called "Logged" value into "Yes"
                            Session["Logged"] = "Yes";
                            Session["username"] = UserName;
                            FormsAuthentication.GetAuthCookie(UserName, true);
                            FormsAuthentication.SetAuthCookie(UserName, true);

                            Session["pwd"] = Password;
                            GetStudentNoSession(UserName);
                            rememberMeYeah(UserName, Password);
                            Session["reportformUser"] = "iamStudent";
                            CheckStudentsProfile(UserName);
                            break;
                    }
                }

            }
            catch (Exception ex)
            {

                KCDFAlert.ShowAlert(ex.Message);
            }

        }

        protected void GetGranteeNoSession(string username)
        {
            var objGrantees = nav.grantees_Register.Where(r => r.Organization_Username == username).FirstOrDefault();

            if (objGrantees != null)
            {
                Session["grant_no"] = objGrantees.No;
                Session["userNotify"] = 1;
            }

        }

        protected void GetConsultantNoSession(string username)
        {
            var objCons = nav.myConsultants.Where(r => r.Organization_Username == username).FirstOrDefault();

            if (objCons != null)
            {
                Session["myRNo"] = objCons.Organization_Registration_No;
                Session["consultant_no"] = objCons.No;
                Session["userNotify"] = 2;
            }

        }

        protected void GetStudentNoSession(string username)
        {
            var objStudents = nav.studentsRegister.Where(r => r.Username == username).FirstOrDefault();

            if (objStudents != null)
            {
                Session["student_no"] = objStudents.No;
                Session["userNotify"] = 3;
            }

        }
    }
    
}
