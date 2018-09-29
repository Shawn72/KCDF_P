using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using KCDF_P.NavOData;
using System.Security.Cryptography;
using System.Text;
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

        public static readonly string StrSqlConn = @"Server=" + ConfigurationManager.AppSettings["DB_INSTANCE"] + ";Database=" +
                                  ConfigurationManager.AppSettings["DB_NAME"] + "; User ID=" +
                                  ConfigurationManager.AppSettings["DB_USER"] + "; Password=" +
                                  ConfigurationManager.AppSettings["DB_PWD"] + "; MultipleActiveResultSets=true";

        public string CompanyName = "KCDF";
        protected void Page_Load(object sender, EventArgs e)
        {
            NoCache();
            if (!IsPostBack)
            {
               loadList();
            }
        }
        public void NoCache()
        {
            Response.CacheControl = "private";
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var regno = txtIDorRegNo.Text;

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
                CheckAdmissionNO(regno);

            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Operation Cancelled by user!')", true); // ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "alert", "alert('You clicked NO!')", true);
               
            }

        }

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
        protected void CreateStudentUser()
        {
            string fname = txtFirstname.Text.Trim();
            string mname = txtMiddlename.Text.Trim();
            string lname = txtLastname.Text.Trim();
            string email = txtEmail.Text.Trim();
            string username = txtUserName.Text.Trim();
            string passW1 = txtPassword1.Text.Trim();
            string confirPwd = txtPassConfirmed.Text.Trim();
            string activationCode = Guid.NewGuid().ToString();
            int gender = Convert.ToInt32(Session["gender"].ToString());
            string admNo = txtIDorRegNo.Text.Trim();

            string passEncrypt = EncryptP(confirPwd);

            DateTime doB;
            var leohii = DateTime.Today;

            var dofB = dateofBirth.Value.Trim();
            if (string.IsNullOrWhiteSpace(dofB))
            {
                KCDFAlert.ShowAlert("Select a valid date!");
                dateofBirth.Focus();
                return;
            }
            else
            {
                doB = DateTime.Parse(dofB);

            }

            if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(passW1) || string.IsNullOrEmpty(confirPwd))
            {
                lblError.Text = "Please fill all the empty required fields!";
                return;
            }
            if (passW1 != confirPwd)
            {
                lblError.Text = "Password Mismatch, try again!";
                KCDFAlert.ShowAlert("Password Mismatch, try again!");
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
                    if (sup.FnCreateAccount(fname, mname, lname, email, username, passEncrypt, activationCode, gender, doB,
                        admNo))
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "getmeTohomeConfirm()", true);

                        //call goback to login to load Default page for user

                        InsertToActivationDB(username, activationCode);

                        using (MailMessage mm = new MailMessage("kcdfportal@gmail.com", email))
                        {
                            string ActivationUrl = string.Empty;
                            ActivationUrl =
                                System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                                "/Account/Activated.aspx?ActivationCode=" + activationCode + "&username=" + username + "";

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
                            NetworkCredential NetworkCred = new NetworkCredential("kcdfportal@gmail.com", "Kcdfportal@4321*~");
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);
                            // KCDFAlert.ShowAlert("Activation link has been send to your email");
                        }
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
                    sup.FnAddGrantee(orgname, orgmail, userNm, EncryptP(passMe), activationCode);

                    KCDFAlert.ShowAlert("Your account succcessfully created, proceed to activation from your email");

                    InsertToActivationDB(userNm, activationCode);

                    using (MailMessage mm = new MailMessage("kcdfportal@gmail.com", orgmail))
                    {
                        string ActivationUrl = string.Empty;
                        ActivationUrl = 
                            System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) 
                            + "/Account/Activated_Grantees.aspx?ActivationCode=" + activationCode + "&username="+ userNm + "";

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
                        NetworkCredential NetworkCred = new NetworkCredential("kcdfportal@gmail.com", "Kcdfportal@4321*~");
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

        protected void loadList()
        {
            ddlAccountType.Items.Add("Scholarship Account");
            ddlAccountType.Items.Add("Grantee Account");
            ddlAccountType.Items.Add("Consultant Account");
        }

        protected void ddlAccountType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selType = ddlAccountType.SelectedItem.Text;

            switch (selType)
            {
                case "Scholarship Account":
                    accuontTypeViews.SetActiveView(scholarView);
                    Session["Xaccount"] = "scholarship";
                    break;

                case "Grantee Account":
                    accuontTypeViews.SetActiveView(grantsView);
                    Session["Xaccount"] = "grantee";
                    break;

                case "Consultant Account":
                    accuontTypeViews.SetActiveView(regConsults);
                    Session["Xaccount"] = "consultant";
                    break;
            }
        }

        protected void rdoBtnListGender_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["gender"] = rdoBtnListGender.SelectedIndex;
           // KCDFAlert.ShowAlert(Session["gender"].ToString());
        }

        protected void compareDates()
        {
            DateTime doB;
            var leohii = DateTime.Today;
            
            var dofB = dateofBirth.Value.Trim();
            if (string.IsNullOrWhiteSpace(dofB))
            {
                KCDFAlert.ShowAlert("Select a valid date!");
                dateofBirth.Focus();
                return;
            }
            else
            {
                doB = DateTime.Parse(dofB);

            }
          var myAge = (leohii - doB).TotalDays;
            if (myAge<18)
            {
              KCDFAlert.ShowAlert("PG 13 pls go to bed");  
            }
            else
            {
                KCDFAlert.ShowAlert("18 and above, greenlight");
            }
        }

        public bool CheckAdmissionNO(string RegNo)
        {
            bool status = false;
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;

            if (sup.FnVerifyStudentRegNo(RegNo)==true)
            {
               status = true;
               KCDFAlert.ShowAlert(status.ToString()+ "That Reg No is already taken");
                //Session["myStatusIs"] = status;
                //lblStatus.Text = Session["myStatusIs"].ToString();
            }
            else
            {
                status = false;
                //Session["myStatusIsFalse"] = status;
                //  KCDFAlert.ShowAlert(status.ToString());
                CreateStudentUser();
            }

            return status;

        }

        protected void checkIam_OnClick(object sender, EventArgs e)
        {
            CheckConsultant(txtConsIDReg.Text);
        }

        protected void btnSaveConsultant_OnClick(object sender, EventArgs e)
        {
            //save consultants
            var idnumber = txtConsIDReg.Text;

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
                CheckConsultant(idnumber);
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Operation Cancelled by user!')", true); 
                // ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "alert", "alert('You clicked NO!')", true);
            }
        }

        public bool CheckConsultant(string idNoReg)
        {
            bool status = false;
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;

            if (sup.FnVerifyConsultant(idNoReg) == true)
            {
                status = true;
                KCDFAlert.ShowAlert(status.ToString() + "That Reg No/ID No is already taken");
                //Session["myStatusIs"] = status;
                //lblStatus.Text = Session["myStatusIs"].ToString();
            }
            else
            {
                status = false;
                //Session["myStatusIsFalse"] = status;
               // KCDFAlert.ShowAlert(status.ToString()+"Not Taken");
                CreateAConsultant();
            }

            return status;
        }
        protected void CreateAConsultant()
        {
            string fullname = txtConsName.Text.Trim();
            string email = txtConsEmail.Text.Trim();
            string username = txtConsUsername.Text.Trim();
            string passW1 = txtConsPass1.Text.Trim();
            string confirPwd = txtConsPass2.Text.Trim();
            string activationCode = Guid.NewGuid().ToString();
            string regNo = txtConsIDReg.Text.Trim();
          

            if (string.IsNullOrWhiteSpace(fullname) ||
                string.IsNullOrWhiteSpace(regNo) || 
                string.IsNullOrWhiteSpace(email) || 
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(passW1) || 
                string.IsNullOrWhiteSpace(confirPwd))
            {
                lblError.Text = "Please fill all the empty required fields!";
                return;
            }
            if (passW1 != confirPwd)
            {
                lblError.Text = "Password Mismatch, try again!";
                KCDFAlert.ShowAlert("Password Mismatch, try again!");
                txtPassword1.Text = "";
                txtPassConfirmed.Text = "";
                return;
            }
            else
            {
                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                    Portals sup = new Portals();
                    sup.Credentials = credentials;
                    sup.PreAuthenticate = true;
                    if (sup.FnCreateConsultant(fullname, email, regNo, username, EncryptP(confirPwd), activationCode
                        ))
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "getmeTohomeConfirm()", true);

                        //call goback to login to load Default page for user

                        InsertToActivationDB(username, activationCode);

                        using (MailMessage mm = new MailMessage("kcdfportal@gmail.com", email))
                        {
                            string ActivationUrl = string.Empty;
                            ActivationUrl =
                                System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                                "/Account/Activate_Consultant.aspx?ActivationCode=" + activationCode + "&username=" + username + "";

                            //Click here to activate your account
                            //var contactUsUrl = Page.ResolveClientUrl("~/Account/Register.aspx");

                            mm.Subject = "KCDF Consultant Account Activation";
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
                            NetworkCredential NetworkCred = new NetworkCredential("kcdfportal@gmail.com", "Kcdfportal@4321*~");
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);
                            // KCDFAlert.ShowAlert("Activation link has been send to your email");
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    return;
                }
            }
           
        }
        public void InsertToActivationDB(string userName, string ActvCode)
        {
            using (SqlConnection con = new SqlConnection(StrSqlConn))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO UserActivation VALUES(@UserId, @ActivationCode)"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@UserId", userName);
                        cmd.Parameters.AddWithValue("@ActivationCode", ActvCode);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }

        protected void rdoBtnListYesNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Session["yesOrno"] = rdoBtnListYesNo.SelectedIndex;
            //int selindex = Convert.ToInt32(Session["yesOrno"].ToString());
            //switch (selindex)
            //{
            //    case 0:
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "IfAccepted()", true);
            //        //KCDFAlert.ShowAlert("clicked: "+ selindex);
            //        break;
            //    default:
            //        KCDFAlert.ShowAlert("Non KCDF grant!");
            //        break;
            //}

        }

        protected void btnCreateXAccount_OnClick(object sender, EventArgs e)
        {
            var myXtsession = Session["Xaccount"].ToString();
            switch (myXtsession)
            {
                case "scholarship":
                    GetScholarAccount();
                    break;
                case "grantee":
                    GetGranteeAccount();
                    break;
                case "consultant":
                    GetConsultantAccount();
                    break;

            }

        }

        protected void GetGranteeAccount()
        {
            string email = txtExEmail.Text.Trim();
            string username = txtExUsername.Text.Trim();
            string passW1 = txtXPass1.Text.Trim();
            string confirPwd = txtXPass2.Text.Trim();
            string activationCode = Guid.NewGuid().ToString();
            string regNo = txtConsIDReg.Text.Trim();


            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(passW1) ||
                string.IsNullOrWhiteSpace(confirPwd))
            {
                lblError.Text = "Please fill all the empty required fields!";
                return;
            }
            if (passW1 != confirPwd)
            {
                lblError.Text = "Password Mismatch, try again!";
                KCDFAlert.ShowAlert("Password Mismatch, try again!");
                txtPassword1.Text = "";
                txtPassConfirmed.Text = "";
                return;
            }
            else
            {
                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                    Portals sup = new Portals();
                    sup.Credentials = credentials;
                    sup.PreAuthenticate = true;
                    if (sup.FnAddXstingGrantee(email, username, EncryptP(confirPwd), activationCode))
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "getmeTohomeConfirm()", true);

                        //call goback to login to load Default page for user
                        InsertToActivationDB(username, activationCode);

                        using (MailMessage mm = new MailMessage("kcdfportal@gmail.com", email))
                        {
                            string ActivationUrl = string.Empty;
                            ActivationUrl =
                                System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                                "/Account/Activated_Grantees.aspx?ActivationCode=" + activationCode + "";

                            //Click here to activate your account
                            //var contactUsUrl = Page.ResolveClientUrl("~/Account/Register.aspx");

                            mm.Subject = "KCDF Grantee Account Activation";
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
                            NetworkCredential NetworkCred = new NetworkCredential("kcdfportal@gmail.com", "Kcdfportal@4321*~");
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);
                            // KCDFAlert.ShowAlert("Activation link has been send to your email");
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    KCDFAlert.ShowAlert(ex.Message);
                    return;
                }
            }
        }

        protected void GetConsultantAccount()
        {
            string email = txtExEmail.Text.Trim();
            string username = txtExUsername.Text.Trim();
            string passW1 = txtXPass1.Text.Trim();
            string confirPwd = txtXPass2.Text.Trim();
            string activationCode = Guid.NewGuid().ToString();

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(passW1) ||
                string.IsNullOrWhiteSpace(confirPwd))
            {
                lblError.Text = "Please fill all the empty required fields!";
                return;
            }
            if (passW1 != confirPwd)
            {
                lblError.Text = "Password Mismatch, try again!";
                KCDFAlert.ShowAlert("Password Mismatch, try again!");
                txtPassword1.Text = "";
                txtPassConfirmed.Text = "";
                return;
            }
            else
            {
                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                    Portals sup = new Portals();
                    sup.Credentials = credentials;
                    sup.PreAuthenticate = true;
                    if (sup.FnAddXstingConsult(email, username, EncryptP(confirPwd), activationCode))
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "getmeTohomeConfirm()", true);

                        //call goback to login to load Default page for user
                        InsertToActivationDB(username, activationCode);

                        using (MailMessage mm = new MailMessage("kcdfportal@gmail.com", email))
                        {
                            string ActivationUrl = string.Empty;
                            ActivationUrl =
                                System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                                "/Account/Activate_Consultant.aspx?ActivationCode=" + activationCode + "";

                            //Click here to activate your account
                            //var contactUsUrl = Page.ResolveClientUrl("~/Account/Register.aspx");

                            mm.Subject = "KCDF Grantee Account Activation";
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
                            NetworkCredential NetworkCred = new NetworkCredential("kcdfportal@gmail.com", "Kcdfportal@4321*~");
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);
                            // KCDFAlert.ShowAlert("Activation link has been send to your email");
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    KCDFAlert.ShowAlert(ex.Message);
                    return;
                }
              }
            
        }

        protected void GetScholarAccount()
        {
            string email = txtExEmail.Text.Trim();
            string username = txtExUsername.Text.Trim();
            string passW1 = txtXPass1.Text.Trim();
            string confirPwd = txtXPass2.Text.Trim();
            string activationCode = Guid.NewGuid().ToString();

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(passW1) ||
                string.IsNullOrWhiteSpace(confirPwd))
            {
                lblError.Text = "Please fill all the empty required fields!";
                return;
            }
            if (passW1 != confirPwd)
            {
                lblError.Text = "Password Mismatch, try again!";
                KCDFAlert.ShowAlert("Password Mismatch, try again!");
                txtPassword1.Text = "";
                txtPassConfirmed.Text = "";
                return;
            }
            else
            {
                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                    Portals sup = new Portals();
                    sup.Credentials = credentials;
                    sup.PreAuthenticate = true;
                    if (sup.FnAddXstingScholar(email, username, EncryptP(confirPwd), activationCode))
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "getmeTohomeConfirm()", true);

                        //call goback to login to load Default page for user
                        InsertToActivationDB(username, activationCode);

                        using (MailMessage mm = new MailMessage("kcdfportal@gmail.com", email))
                        {
                            string ActivationUrl = string.Empty;
                            ActivationUrl =
                                System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                                "/Account/Activated.aspx?ActivationCode=" + activationCode + "";

                            //Click here to activate your account
                            //var contactUsUrl = Page.ResolveClientUrl("~/Account/Register.aspx");

                            mm.Subject = "KCDF Grantee Account Activation";
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
                            NetworkCredential NetworkCred = new NetworkCredential("kcdfportal@gmail.com", "Kcdfportal@4321*~");
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);
                            // KCDFAlert.ShowAlert("Activation link has been send to your email");
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    KCDFAlert.ShowAlert(ex.Message);
                    return;
                }
            }
        }

        protected void lnkAddExistingAcc_OnClick(object sender, EventArgs e)
        {
            accuontTypeViews.SetActiveView(viewExistingAc);
        }
    }
}
