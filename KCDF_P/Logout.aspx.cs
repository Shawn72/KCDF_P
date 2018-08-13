using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KCDF_P
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Session.Abandon();
            Session["Logged"] = "No";
            Session["username"] = "";
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");

            Response.Buffer = true;
            Response.CacheControl = "no-cache";
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Cache-Control", "no-cache");
            Response.CacheControl = "no-cache";
            Response.ExpiresAbsolute = new DateTime(1900, 1, 1);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

        }
    }
}