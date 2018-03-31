using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;

namespace KCDF_P
{
    public partial class Grants_Uploads : System.Web.UI.Page
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
            loadUploads();
        }
        protected void UploadFile(object sender, EventArgs e)
        {
            try
            {
                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + "\\";
                string fileName = Path.GetFileName(FileUpload.PostedFile.FileName);
                string ext = Path.GetExtension(FileUpload.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (FileUpload.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") ||(ext == ".xlsx"))
                {
                    string filename = Grantees.No + "_" + fileName;
                    FileUpload.SaveAs(uploadsFolder + filename);
                    CopyFilesToDir();
                    loadUploads();
                    saveAttachment(filename, ext);
                    KCDFAlert.ShowAlert("Document: " + filename + " uploaded and Saved successfully!");

                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext + "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

                }
                if (!FileUpload.HasFile)
                {
                    KCDFAlert.ShowAlert("Select Document before uploading");
                    return;
                }

            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert("Unkown Error Occured!");
            }

        }

        protected void CopyFilesToDir()
        {
            string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + "\\";
            string destPath = Request.PhysicalApplicationPath + "All Uploads\\";

            foreach (string dirPath in Directory.GetDirectories(uploadsFolder, " * ",
              SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(uploadsFolder, destPath));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(uploadsFolder, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(uploadsFolder, destPath), true);
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(filePath);
            //delete from the common folder
            //string commonF = Request.PhysicalApplicationPath + "All Uploads\\";
            //File.Delete(commonF);
            //Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void saveAttachment(string filName, string extension)
        {
            var usNo = nav.grantees_Register.ToList().Where(usr => usr.Organization_Username == Session["username"].ToString()).Select(nu=>nu.No).SingleOrDefault();
           // var prjct =;
            string fullFPath = Request.PhysicalApplicationPath + "All Uploads\\"+ Grantees.No +"\\"+ filName;
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            int granttype = 1;
            string docType ="";
            if ((extension==".jpg") || (extension == ".jpeg") || (extension == ".png") )
            {
                docType = "Picture";
            }
            else
            if ((extension == ".pdf"))
            {
                docType = "PDF";
            }
            if ((extension == ".doc") || (extension == ".docx"))
            {
                docType = "Word Document";
            }
            if (extension == ".xlsx")
            {
                docType = "Excel Document";
            }

            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
           // sup.FnAttachment(usNo, docType, fullFPath, filName, granttype, docType, Session["username"].ToString());

        }

        protected void loadUploads()
        {
            try
            {
                //string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploaded Documents/" + Grantees.No + "/"));
                //List<ListItem> files = new List<ListItem>();
                //foreach (string filePath in filePaths)
                //{
                //    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                //}

              //  gridViewUploads.DataSource = files;
                gridViewUploads.DataBind();
            }
            catch (Exception ex)
            {
               KCDFAlert.ShowAlert("You have not uploaded documents yet!"); 
               
            }
            
        }

    }
}