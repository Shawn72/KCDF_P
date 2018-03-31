using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;

namespace KCDF_P.Account
{
    public partial class Register : Page
    {
        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
             new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                 ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                   // Response.Redirect("~/Default.aspx");

                }
               
            }
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            string fname = txtFirstname.Text.Trim();
            string mname = txtMiddlename.Text.Trim();
            string lname = txtLastname.Text.Trim();
            string email = txtEmail.Text.Trim();
            string username = txtUserName.Text.Trim();
            string passW1 = txtPassword1.Text.Trim();
            string confirPwd = txtPassConfirmed.Text.Trim();
            string activationCode = Guid.NewGuid().ToString();


            if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(passW1) || string.IsNullOrEmpty(confirPwd))
            {
                lblError.Text = "Please fill all the empty required fields!";
                return;
            }
            else if (passW1 != confirPwd)
            {
                lblError.Text = "Password Mismatch, try again!";
                txtPassword1.Text = "";
                txtPassConfirmed.Text = "";
            }
            //else if (usname==username) {
            //    lblError.Text = "Username is already taken, please choose another";
            //}
            else
            {
                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                    Portals sup = new Portals();
                    sup.Credentials = credentials;
                    sup.PreAuthenticate = true;
                    sup.FnCreateAccount(fname, mname, lname, email, username, confirPwd, activationCode);

                    KCDFAlert.ShowAlert("Your account succcessfully created, proceed to activation from your email");

                    using (MailMessage mm = new MailMessage("ngutumbuvi8@gmail.com", email))
                    {
                        string ActivationUrl = string.Empty;
                        ActivationUrl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/Activated.aspx?ActivationCode=" + activationCode + "";

                        //Click here to activate your account
                        //var contactUsUrl = Page.ResolveClientUrl("~/Account/Register.aspx");

                        mm.Subject = "KCDF Account Activation";
                        string body = "Hello " + username + ",";
                        body += "<br /><br />Please click the following link to activate your account";

                        // body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("/Account/Register.aspx", "/Account/Activated.aspx?ActivationCode=" + activationCode) + "'>Click here to activate your account.</a>";

                        body += "<br />" + ActivationUrl + "";
                        body += "<br /><br />Thanks";
                        mm.Body = body;
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("ngutumbuvi8@gmail.com", "ngutu12345");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);

                        KCDFAlert.ShowAlert("Activation link has been send to your email");
                    }

                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    return;
                }
            }

        }

        protected void btnCreateAcc_OnClick(object sender, EventArgs e)
        {
            string orgname = txtOrgName.Text.Trim();
            string orgmail = txtOrgEmail.Text.Trim();
            string userNm = txtOrgUsername.Text.Trim();
            string passMe1 = txtPasswordOne.Text.Trim();
            string passMe = txtPassConfir.Text.Trim();
            string activationCode = Guid.NewGuid().ToString();

            if (passMe1 != passMe)
            {
                lblError.Text = "Password Mismatch, try again!";
                txtPassword1.Text = "";
                txtPassConfirmed.Text = "";
            }
            else
            {
                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                    Portals sup = new Portals();
                    sup.Credentials = credentials;
                    sup.PreAuthenticate = true;
                    sup.FnAddGrantee(orgname, orgmail, userNm, passMe, activationCode);

                    KCDFAlert.ShowAlert("Your account succcessfully created, proceed to activation from your email");

                    using (MailMessage mm = new MailMessage("ngutumbuvi8@gmail.com", orgmail))
                    {
                        string ActivationUrl = string.Empty;
                        ActivationUrl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/Activated_Grantees.aspx?ActivationCode=" + activationCode + "";

                        //Click here to activate your account
                        //var contactUsUrl = Page.ResolveClientUrl("~/Account/Register.aspx");

                        mm.Subject = "KCDF Account Activation";
                        string body = "Hello " + userNm + ",";
                        body += "<br /><br />Please click the following link to activate your account";

                        // body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("/Account/Register.aspx", "/Account/Activated.aspx?ActivationCode=" + activationCode) + "'>Click here to activate your account.</a>";

                        body += "<br />" + ActivationUrl + "";
                        body += "<br /><br />Thanks";
                        mm.Body = body;
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("ngutumbuvi8@gmail.com", "ngutu12345");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);

                        KCDFAlert.ShowAlert("Activation link has been send to your email");
                    }

                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    return;
                }
            }


        }
        protected void ddlAccountType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selType = ddlAccountType.SelectedItem.Text;
            if (selType== "Scholarship Account")
            {
                accuontTypeViews.SetActiveView(scholarView);
            }
            else if (selType== "Grantee Account")
            {
                accuontTypeViews.SetActiveView(grantsView);
            }
            {
                
            }
        }
    }
}
