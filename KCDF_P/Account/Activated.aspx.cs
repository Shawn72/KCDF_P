using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        public readonly string strSQLConn = @"Server=" + ConfigurationManager.AppSettings["DB_INSTANCE"] + ";Database=" +
                                ConfigurationManager.AppSettings["DB_NAME"] + "; User ID=" +
                                ConfigurationManager.AppSettings["DB_USER"] + "; Password=" +
                                ConfigurationManager.AppSettings["DB_PWD"] + "; MultipleActiveResultSets=true";

        public string Company_Name = "KCDF";
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
                    string myusername = Request.QueryString["username"];

                    var activatem = nav.activationquery.ToList().Where(r => r.Activation_Code == activationCode);
                    string activatemyASS = activatem.Select(r => r.Activation_Code).SingleOrDefault().ToString();
                    if (sup.FnActivateAc(activatemyASS) == true)
                    {
                        ActivatedfromDB(activationCode);
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
            ActivationUrl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Default.aspx";
            Response.Redirect(ActivationUrl);
        }

        public void ActivatedfromDB(string actiVCode)
        {
            using (SqlConnection con = new SqlConnection(strSQLConn))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM UserActivation WHERE ActivationCode = @ActivationCode"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ActivationCode", actiVCode);
                        cmd.Connection = con;
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();
                        if (rowsAffected == 1)
                        {
                            ltMessage.Text = "Your Acount has been activated successfully.";
                        }
                        else
                        {
                            ltMessage.Text = "Invalid Activation code.";
                        }
                    }
                }
            }
        }
    }
}