using System;
using System.Configuration;
using System.Net;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;

namespace KCDF_P
{
    public partial class Notifications1 : System.Web.UI.MasterPage
    {
        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
              new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                  ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            GetNotification();
        }
       
        protected void lnkDashboard_OnClick(object sender, EventArgs e)
        {
            int sexc = Convert.ToInt32(Session["userNotify"]);
            switch (sexc)
            {
                case 1:
                    Response.Redirect("~/Grantee_Dashboard.aspx");
                    break;
                case 2:
                    Response.Redirect("~/Consultancy_Page.aspx");
                    break;
                case 3:
                    Response.Redirect("~/Dashboard.aspx");
                    break;
            }
           
        }

        protected void lnkLogout_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Logout.aspx");
            //Response.ApplyAppPathModifier("~/Default.aspx");
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
    }
}
