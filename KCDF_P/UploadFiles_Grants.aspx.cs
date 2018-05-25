using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Net;
using System.Web.UI.WebControls;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;

namespace KCDF_P
{
    public partial class UploadFiles : System.Web.UI.Page
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
                Response.Redirect("/Default.aspx");
            }
            loadView();
            loadUploads();
        }
      
        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }

        protected void loadView()
        {
            multiVDlds.SetActiveView(GranteesViewId);
        }
        protected void loadUploads()
        {
            try
            {
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/Downloads_Grantees/"));
                List<ListItem> files = new List<ListItem>();
                foreach (string filePath in filePaths)
                {
                    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                }
                gridViewUploads.DataSource = files;
                gridViewUploads.DataBind();
            }
            catch (Exception ex)
            {

                KCDFAlert.ShowAlert("No Uploads!");
            }
           
        }
       
    }

}