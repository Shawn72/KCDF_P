using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace KCDF_P
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start(Object sender, EventArgs e)
        {
            //Define new 3 Sessions
            //The first Session "Logged" which is an indicator to the status of the user
            Session["Logged"] = "No";
            //The second Session "User" stores the name of the current user
            Session["username"] = "";
        }
    }
}