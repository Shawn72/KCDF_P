﻿using System;
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
    public partial class PasswordReset : System.Web.UI.Page
    {
        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
             new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                 ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("~/Default.aspx");

            }
           // KCDFAlert.ShowAlert(Session["userType"].ToString());
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var imWhoIam = Session["userType"].ToString();
            switch (imWhoIam)
            {
                case "student":
                    ChangeStudentPassword();
                    break;
                   
                case "grantee":
                    ChangeGranteePassword();
                    break;
            }
        }

        public void ChangeStudentPassword()
        {
            if (string.IsNullOrWhiteSpace(txtPasswordOld.Text.Trim()) && string.IsNullOrWhiteSpace(txtPasswordNew.Text.Trim()) &&
               string.IsNullOrWhiteSpace(txtPasswordConfirm.Text))
            {
                KCDFAlert.ShowAlert("You must fill-in all the fields to continue");
                return;
            }
            if (txtPasswordNew.Text.Trim() != txtPasswordConfirm.Text.Trim())
            {
                KCDFAlert.ShowAlert("New password is not matching the confirmed password field");
                lblError.Text = "Password Mismatch, please try again!";
                return;
            }
            else
            {
                try
                {
                    if (WSConfig.ObjNav.FnStudent_PasswordChange(Session["username"].ToString(),
                        txtPasswordOld.Text.Trim(),
                        txtPasswordConfirm.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myFunctionLoadScolars();",
                            true);
                    }
                    else
                    {
                        KCDFAlert.ShowAlert("Password could not be changed, kindly contact ICT Admin for assistance");
                    }
                }
                catch (Exception exception)
                {
                    KCDFAlert.ShowAlert(exception.Message);
                }
            }
        }

        protected void ChangeGranteePassword()
        {
            if (string.IsNullOrWhiteSpace(txtPasswordOld.Text.Trim()) && string.IsNullOrWhiteSpace(txtPasswordNew.Text.Trim()) &&
                string.IsNullOrWhiteSpace(txtPasswordConfirm.Text))
            {
                KCDFAlert.ShowAlert("You must fill-in all the fields to continue");
                return;
            }
            if (txtPasswordNew.Text.Trim() != txtPasswordConfirm.Text.Trim())
            {
                KCDFAlert.ShowAlert("New password is not matching the confirmed password field");
                lblError.Text = "Password Mismatch, please try again!";
                return;
            }
            else
            {
                try
                {
                    if (WSConfig.ObjNav.FnGrantee_PasswordChange(Session["username"].ToString(),
                        txtPasswordOld.Text.Trim(),
                        txtPasswordConfirm.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myFunctionLoadGrantees();",
                            true);
                    }
                    else
                    {
                        KCDFAlert.ShowAlert("Password could not be changed, kindly contact ICT Admin for assistance");
                    }
                }
                catch (Exception exception)
                {
                    KCDFAlert.ShowAlert(exception.Message);
                }
            }
        }
    }
}