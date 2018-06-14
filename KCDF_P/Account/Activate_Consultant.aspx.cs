using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;

namespace KCDF_P
{
    public partial class Activate_Consultant : System.Web.UI.Page
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
                try
                {

                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                    Portals sup = new Portals();
                    sup.Credentials = credentials;
                    sup.PreAuthenticate = true;
                    string activationCode = Request.QueryString["ActivationCode"]; Guid.Empty.ToString();

                    var activatem = nav.consActivationQuery.ToList().Where(r => r.Activation_Code == activationCode);
                    string activatemyASS = activatem.Select(r => r.Activation_Code).SingleOrDefault();
                    if (sup.FnActivateConsultant(activatemyASS) == true)
                    {
                        ltMessage.Text = "Your Acount has been activated successfully!";
                    }
                    else
                    {
                        ltMessage.Text = "Your Acount Could not be activated!";
                    }
                    
                }
                catch (Exception ex)
                {
                    //ltMessage.Text = ex.Message;
                    ltMessage.Text = "Error in activation, Please try again later!!";
                    return;
                }
            }
        }

        protected void lnkToHome_OnClick(object sender, EventArgs e)
        {
            string ActivationUrl = string.Empty;
            ActivationUrl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +"/Default.aspx";
            Response.Redirect(ActivationUrl);
        }
    }
}
