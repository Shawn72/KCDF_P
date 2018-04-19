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
        public string strSQLConn = @"Server=" + ConfigurationManager.AppSettings["DB_INSTANCE"] + ";Database=" +
                                  ConfigurationManager.AppSettings["DB_NAME"] + "; User ID=" +
                                  ConfigurationManager.AppSettings["DB_USER"] + "; Password=" +
                                  ConfigurationManager.AppSettings["DB_PWD"] + "; MultipleActiveResultSets=true";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {

            }
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

                if (string.IsNullOrEmpty(userPassword) && string.IsNullOrEmpty(userName))
                {
                    lblError.Text = "Username or Password Empty!";
                    return;
                }

                try
                {
                    if (nav.studentsRegister.Where(r => r.Username == userName && r.Activated == true && r.Password == userPassword).FirstOrDefault() != null)
                    {
                        Session["username"] = userName;
                        Session["pwd"] = userPassword;
                      //  Students cust = new Students(userName);
                        Response.Redirect("~/Dashboard.aspx");

                    }
                    else if (nav.studentsRegister.Where(r => r.Username == userName && r.Activated == false && r.Password == userPassword).FirstOrDefault() != null)
                    {
                        //HotelFactory.ShowAlert("You Ain't Active!!");
                        lblError.Text = "Account not active, please activate!";
                        return;
                    }
                    else if (nav.studentsRegister.Where(r => r.Email == userName && r.Activated == true && r.Password == userPassword).FirstOrDefault() != null)
                    {
                        Session["username"] = userName;
                        Session["pwd"] = userPassword;
                        Response.Redirect("~/Dashboard.aspx");
                    }
                    else
                    {
                        lblError.Text = "Authentication failed!";
                        return;
                    }
                }
                catch (Exception exception)
                {
                   // lblError.Text = exception.Message;
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
            if (ddlUsrT == "Student")
            {
                LoginViews.SetActiveView(studentView);
            }
            if (ddlUsrT == "Grantee")
            {
                LoginViews.SetActiveView(granteeView);
            }
        }

        protected void btnGrantLogin_OnClick(object sender, EventArgs e)
        {
            captchaGrantee.ValidateCaptcha(txtCaptcha2.Text.Trim());
            if (captchaGrantee.UserValidated)
            {
                string userName = txtorgUsn.Text.Trim().Replace("'", "");
                string userPassword = txtorgPsw.Text.Trim().Replace("'", "");

                if (string.IsNullOrEmpty(userPassword) && string.IsNullOrEmpty(userName))
                {
                    lblError.Text = "Username or Password Empty!";
                    return;
                }

                try
                {
                   // var navusn = nav.grantees_Register.Where()

                    if (nav.grantees_Register.Where(r => r.Organization_Username == userName && r.Activated == true && r.Password == userPassword).FirstOrDefault() != null)
                    {
                        Session["username"] = userName;
                        Session["pwd"] = userPassword;
                        //  Students cust = new Students(userName);
                        Response.Redirect("~/Grantee_Dashboard.aspx");

                    }
                    else if (nav.grantees_Register.Where(r => r.Organization_Username == userName && r.Activated == false && r.Password == userPassword).FirstOrDefault() != null)
                    {
                        //HotelFactory.ShowAlert("You Ain't Active!!");
                        lblError.Text = "Account not active, please active!";
                        return;
                    }
                    else if (nav.grantees_Register.Where(r => r.Email == userName && r.Activated == true && r.Password == userPassword).FirstOrDefault() != null)
                    {
                        Session["username"] = userName;
                        Session["pwd"] = userPassword;
                        Response.Redirect("~/Grantee_Dashboard.aspx");
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

        private bool MyValidationFunction(string myusername, string mypassword)
        {
            bool boolReturnValue = false;
            string SQLRQST = @"SELECT No_, Password from [United Women Sacco Ltd$Members Register]";
            SqlConnection con = new SqlConnection(strSQLConn);
            SqlCommand command = new SqlCommand(SQLRQST, con);
            SqlDataReader Dr;
            try
            {
                con.Open();
                Dr = command.ExecuteReader();
                while (Dr.Read())
                {
                    if ((myusername == Dr["No_"].ToString()) && (mypassword == Dr["Password"].ToString()))
                    {
                        boolReturnValue = true;
                        break;
                    }
                    if (string.IsNullOrWhiteSpace(Dr["Password"].ToString()))
                    {
                        boolReturnValue = false;
                    }
                }
                Dr.Close();
            }
            catch (SqlException ex)
            {
                KCDFAlert.ShowAlert("Authentication failed!" + ex.Message);

            }
            return boolReturnValue;
        }
    }
}