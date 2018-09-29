using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCDF_P.NAVWS;

namespace KCDF_P
{
    public partial class Consultants : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetNotification();
        }
        protected void lnkBtnChangeP_OnClick(object sender, EventArgs e)
        {
            Session["userType"] = "Consultant";
            Response.Redirect("PasswordReset.aspx");
        }

        protected void lnkbtnEdit_OnClick(object sender, EventArgs e)
        {
            // Response.Redirect("Account/Add_Grantee_Profile.aspx");
           // Server.Transfer("~/Account/Add_Grantee_Profile.aspx");
        }
       protected void LnkchnagetoProfile_OnClick(object sender, EventArgs e)
        {
            Session.Remove("toprofile");
            Session["toprofile"] = 1;
            Response.Redirect("Consultancy_Page.aspx");
        }

        protected void lnkBtntoDashd_OnClick(object sender, EventArgs e)
        {
            Session.Remove("toprofile");
            Session["toprofile"] = 0;
            Response.Redirect("Consultancy_Page.aspx");
        }

        protected void lnkBtnCompleted_OnClick(object sender, EventArgs e)
        {
            Session.Remove("completed");
            Session["completed"] = 0;
            Response.Redirect("/Notifications.aspx");
        }
        protected void lnkBtnPends_OnClick(object sender, EventArgs e)
        {
            Session.Remove("completed");
            Session["completed"] = 1;
            Response.Redirect("/Notifications.aspx");
        }
        protected void GetNotification()
        {
            //int nots = nav.tasks.ToList().Where(n => n.User_Number == Grantees.No).Count();
            int userType = Convert.ToInt32(Session["usertype"]);
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            int nots = sup.FnCountGrantsNotifications(Session["username"].ToString(), userType);
            lblNots.Text = nots.ToString();
        }
    }
}