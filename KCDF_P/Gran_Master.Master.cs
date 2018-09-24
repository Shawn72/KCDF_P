using System;
using System.Configuration;
using System.Net;
using System.Web.Providers.Entities;

using KCDF_P.NavOData;
using KCDF_P.NAVWS;

namespace KCDF_P
{
    public partial class Gran_Master : System.Web.UI.MasterPage
    {
        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
              new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                  ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            // getURL();
            GetNotification();
        }

        //protected void GetMemberDetails()
        //{
        //    var memberdetails = Nav.MemberList.Where(r => r.No == Session["username"].ToString()).FirstOrDefault();

        //    if (memberdetails != null)
        //    {
        //        memberName.InnerHtml = memberdetails.Name;
        //    }

        //}

        protected void lnkBtnChangeP_OnClick(object sender, EventArgs e)
        {
            Session["userType"] = "grantee";
            Response.Redirect("~/PasswordReset.aspx");
        }

        protected void lnkbtnEdit_OnClick(object sender, EventArgs e)
        {
            // Response.Redirect("Account/Add_Grantee_Profile.aspx");
            Server.Transfer("~/Account/Add_Grantee_Profile.aspx");
            
        }
        protected void lnkDashboard_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Grantee_Dashboard.aspx");
        }

        protected void lnkLogout_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Logout.aspx");
            //Response.ApplyAppPathModifier("~/Default.aspx");
        }

        protected void GetNotification()
        {
            //int nots = nav.tasks.ToList().Where(n => n.User_Number == Grantees.No).Count();

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            int nots = sup.FnCountGrantsNotifications(Session["username"].ToString());
            lblNots.Text= nots.ToString();
        }
    }
}