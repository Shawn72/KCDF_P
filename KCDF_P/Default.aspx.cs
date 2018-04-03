using System;
using System.Collections.Generic;
using System.Configuration;
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
       

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //    string userName = txtEmployeeNo.Text.Trim().Replace("'", "");
            //    string userPassword = idNo.Text.Trim().Replace("'", "");

            //    var idcheck = nav.MemberList.Where(r => r.No == userName).Select(k=>k.ID_No).FirstOrDefault();
            //    var emalcheck = nav.MemberList.Where(r => r.No == userName).Select(k => k.E_Mail).FirstOrDefault();
            //    var phonecheck = nav.MemberList.Where(r => r.No == userName).Select(k => k.Phone_No).FirstOrDefault();

            //    if (string.IsNullOrEmpty(userPassword) && string.IsNullOrEmpty(userName))
            //    {
            //        lblError.Text = "Member No or National ID Empty!";
            //        btnSubmit.Visible = true;
            //        btnBack.Visible = true;
            //        btnSignup.Visible = false;
            //        btnLogin.Visible = false;
            //        MultiView1.SetActiveView(View2);
            //        return;

            //    }
            //    else
            //    {
            //        try
            //        {
            //            var nPassword = NewPassword();
            //            var CompEmail = WSConfig.ObjNav.FnUpdatePassword(txtEmployeeNo.Text.Trim(), idNo.Text.Trim(), nPassword);
            //            if (WSConfig.MailFunction(string.Format("Dear Sacco Member,\n Your New password is: {0}", nPassword), CompEmail,
            //                "Portal password reset successful") && !String.IsNullOrEmpty(CompEmail))
            //            {
            //                SACCOFactory.ShowAlert(
            //                    "A New Password has been generated and sent to your Personal mail and Mobile Phone.Kindly use to it to login to your Member portal");
            //                btnSubmit.Visible = false;
            //                txtEmployeeNo.Enabled = false;
            //                idNo.Enabled = false;
            //                btnBack.Visible = false;
            //                MultiView1.SetActiveView(View1);
            //                btnSignup.Visible = true;
            //                btnLogin.Visible = true;
            //                lblError.Text = "";
            //            }
            //            else if (idcheck != userPassword)
            //            {
            //                SACCOFactory.ShowAlert(
            //                   "Your Password could not be reset. Member number does not match your ID number!");
            //                btnSubmit.Visible = true;
            //                btnBack.Visible = true;
            //                btnSignup.Visible = false;
            //                btnLogin.Visible = false;
            //                MultiView1.SetActiveView(View2);
            //            }
            //            else if (string.IsNullOrEmpty(emalcheck) && phonecheck!=null)
            //            {
            //                SACCOFactory.ShowAlert(
            //                  "Your Password was send to your Phone Number");
            //                btnSubmit.Visible = false;
            //                txtEmployeeNo.Enabled = false;
            //                idNo.Enabled = false;
            //                btnBack.Visible = false;
            //                MultiView1.SetActiveView(View1);
            //                btnSignup.Visible = true;
            //                btnLogin.Visible = true;
            //                lblError.Text = "";
            //            }

            //        }
            //        catch (Exception exception)
            //        {
            //            SACCOFactory.ShowAlert(exception.Message);
            //        }
            //    }
            //    btnSubmit.Visible = true;
            //    btnBack.Visible = true;
            //    btnSignup.Visible = false;
            //    btnLogin.Visible = false;
            //    MultiView1.SetActiveView(View2);

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
    }
}