﻿using System;
using System.Configuration;
using System.Linq;
using System.Net;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;

namespace KCDF_P.Account
{
    public partial class Activated : System.Web.UI.Page
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

                    var activatem = nav.activationquery.ToList().Where(r => r.Activation_Code == activationCode);
                    string activatemyASS = activatem.Select(r => r.Activation_Code).SingleOrDefault().ToString();
                    sup.FnActivateAc(activatemyASS);
                    ltMessage.Text = "Your Acount has been activated successfully.";
                }
                catch (Exception ex)
                {
                     //ltMessage.Text = ex.Message;
                   ltMessage.Text = "Error in activation, Please try again later!!";
                    return;
                }


            }

        }
    }
}