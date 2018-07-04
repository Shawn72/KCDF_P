using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KCDF_P
{
    public partial class ToRDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           LoadUploads();
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
        protected void LoadUploads()
        {
            try
            {
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/ToRUploaded/"));
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