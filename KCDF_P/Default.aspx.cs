using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
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

        public string Company_Name = "KCDF TEST NEW";
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        //protected override void CreateChildControls()
        //{
        //    base.CreateChildControls();
        //    ctrlGoogleReCaptcha.PublicKey = "6LdK7j4UAAAAAJaWiKryMXWxVcwuDAyjEb_Kr204";
        //    ctrlGoogleReCaptcha.PrivateKey = "6LdK7j4UAAAAAC1ovoMUpMxXODnYYsWaebjMbbf0";
        // }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            cptCaptcha.ValidateCaptcha(txtCaptcha.Text.Trim());
            if (cptCaptcha.UserValidated)
            {
                string userName = txtUsername.Text.Trim().Replace("'", "");
                string userPassword = txtPassword.Text.Trim().Replace("'", "");

                if (string.IsNullOrWhiteSpace(userPassword))
                {
                    lblError.Text = "Password Empty!";
                    KCDFAlert.ShowAlert("Password Empty!");
                    return;
                }

                try
                {
                    if (nav.studentsRegister.Where(r => r.Username == userName && r.Activated == true && r.Password == userPassword).FirstOrDefault() != null)
                    {
                        Session["username"] = userName;
                        Session["pwd"] = userPassword;
                        rememberMeYeah(userName, userPassword);
                        Response.Redirect("~/Dashboard.aspx");

                    }
                    else if (nav.studentsRegister.Where(r => r.Username == userName && r.Activated == false && r.Password == userPassword).FirstOrDefault() != null)
                    {
                        //HotelFactory.ShowAlert("You Ain't Active!!");
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
            }
          
        }

        protected void btnGrantLogin_OnClick(object sender, EventArgs e)
        {
            captchaGrantee.ValidateCaptcha(txtCaptcha2.Text.Trim());
            if (captchaGrantee.UserValidated)
            {
                string userName = txtorgUsn.Text.Trim().Replace("'", "");
                string userPassword = txtorgPsw.Text.Trim().Replace("'", "");

                try
                {
                    if (string.IsNullOrWhiteSpace(userPassword))
                {
                    lblError.Text = "Password Empty!";
                    KCDFAlert.ShowAlert("Password Empty!");
                    return;
                }
                CheckExistsSql(userName, userPassword);



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
                if ((eml == dr["Organization Username"].ToString()) && (pass == dr["Password"].ToString()))
                {
                    //System.Web.UI.HtmlControls.HtmlGenericControl lblMastersession =
                    //    (System.Web.UI.HtmlControls.HtmlGenericControl) Master.FindControl("lblSessionUsername");

                    //lblMastersession.InnerText = eml;
                    Grantees.SessionUsername = eml;
                    Session["username"] = Grantees.SessionUsername;
                    Session["pwd"] = pass;
                    rememberMeYeah(eml, pass);
                    Response.Redirect("~/Grantee_Dashboard.aspx");
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
                CheckIfActive(myUserName, passMyass);
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
            if (myPass!= myPassWd)
            {
               KCDFAlert.ShowAlert("Aunthentication Failed!");
                lblError.Text = "Aunthentication Failed!";
                return;
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
            }

        }

        protected void ResetmyGranteePassword()
        {
            var myEmailIs = txtIforgotPassword.Text.Trim();

           try
            {
                var nPassword = NewPassword();
                var CompEmail = WSConfig.ObjNav.FnGranteePasswordReset(myEmailIs, nPassword);
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

        protected void ResetmyScholarshipPass()
        {
            var myEmailIs = txtIforgotPassword.Text.Trim();

            try
            {
                var nPassword = NewPassword();
                var CompEmail = WSConfig.ObjNav.FnStudentPasswordReset(myEmailIs, nPassword);
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

        protected void btnbacktomyRoots_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void rememberMeYeah(string cookieuser, string cookiepasswd)
        {
            if (chkRememberMe.Checked || checkRemMegrant.Checked)
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
        
    }
}
