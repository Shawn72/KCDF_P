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
            if (!IsPostBack)
            {
                loadView();
                loadUploads();
                LoadMyMatrixUploads();
                GetProjects();
            }
            
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
        protected void CopyFilesToDir()
        {
            try
            {
                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + @"\";
                string destPath = @"\\192.168.0.250\All Uploads\";

                foreach (string dirPath in Directory.GetDirectories(uploadsFolder, " * ",
                  SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(uploadsFolder, destPath));

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(uploadsFolder, "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(uploadsFolder, destPath), true);
            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert(ex.Message);

            }
        }

        protected void loadUploads()
        {
            try
            {
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/POCAMatrix/"));
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

        protected void UploadFile(object sender, EventArgs e)
        {
            try
            {
                var documentKind = "POCA TOOL";
                var refNoIs = txtPrefNo.Text;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + @"\";

                // string uploadsFolder = @"\\KCDFSVR\All_Portal_Uploaded\";  //@"\\192.168.0.249\All_Portal_Uploaded\";
                string fileName = Path.GetFileName(FileUploadPOCA.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadPOCA.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (FileUploadPOCA.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".xls") || (ext == ".xlsx"))
                {
                    string filename = Grantees.No + "_" + fileName;
                    FileUploadPOCA.SaveAs(uploadsFolder + filename);
                    // file path to read file
                    string filePath = uploadsFolder + filename;

                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKind, refNoIs, attachedDoc);
                    LoadMyMatrixUploads();
                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext + "; - Allowed picture formats are: XLS XLSX only!");

                }
                if (!FileUploadPOCA.HasFile)
                {
                    KCDFAlert.ShowAlert("Select Document before uploading");
                    return;
                }

            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert("Unkown Error Occured!");
                // KCDFAlert.ShowAlert(ex.Message);
            }

        }

        protected void GetProjects()
        {
            var projs = nav.call_for_Proposal.ToList().Where(pty => pty.Proposal_Type == "Grant");
            ddlAccountType.DataSource = projs;
            ddlAccountType.DataTextField = "Project";
            ddlAccountType.DataValueField = "Call_Ref_Number";
            ddlAccountType.DataBind();
            ddlAccountType.Items.Insert(0, "--Select Project--");
        }

        protected void btnUploadMatrix_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKind = "INDICATOR MATRIX";
                var refNoIs = txtPrefNo.Text;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + @"\";
                string fileName = Path.GetFileName(FileUploadMatrix.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadMatrix.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (FileUploadMatrix.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xlsx"))
                {
                    string filename = Grantees.No + "_" + fileName;
                    FileUploadMatrix.SaveAs(uploadsFolder + filename);
                    // file path to read file
                    string filePath = uploadsFolder + filename;

                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKind, refNoIs, attachedDoc);
                    LoadMyMatrixUploads();
                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext + "; - Allowed picture formats are: XLS XLSX only!");
                }
                if (!FileUploadMatrix.HasFile)
                {
                    KCDFAlert.ShowAlert("Select Document before uploading");
                    return;
                }

            }
            catch (Exception ex)
            {
                // KCDFAlert.ShowAlert("Unkown Error Occured!");
                KCDFAlert.ShowAlert(ex.Message);
            }
        }

        protected void ddlAccountType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int slVal = ddlAccountType.SelectedIndex;
            switch (slVal)
            {
                case 0:
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "alert('Select valid project from dropdownlist!');", true);
                    txtPrefNo.Text = "";
                    break;
                default:
                    txtPrefNo.Text = ddlAccountType.SelectedValue;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "pageLoad();", true);
                    break;
            }
           
        }
        
        protected void saveAttachment(string filName, string extension, string docKind, string callRefNo, string attachedBLOB)
        {
            var usNo = nav.grantees_Register.ToList().Where(usr => usr.Organization_Username == Session["username"].ToString()).Select(nu => nu.No).SingleOrDefault();
            var usaname = Session["username"].ToString();
            var prjct = ddlAccountType.SelectedItem.Text;

            // string fullFPath = Request.PhysicalApplicationPath + "All Uploads\\" + Grantees.No + @"\" + filName;

            string navfilePath = @"D:\All_Portal_Uploaded\" + filName;

            // string uriPath = new Uri(navfilePath).LocalPath;
            // string urI =;

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            int granttype = 1;
            string docType = "";
            if ((extension == ".jpg") || (extension == ".jpeg") || (extension == ".png"))
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
            //try
            //{

            int slVal = ddlAccountType.SelectedIndex;
            switch (slVal)
            {
                case 0:
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "alert('Select valid project from dropdownlist!');", true);
                    txtPrefNo.Text = "";
                    break;
                default:
                    Portals sup = new Portals();
                    sup.Credentials = credentials;
                    sup.PreAuthenticate = true;
                    if (sup.FnAttachMatrixx(usNo, docType, navfilePath, filName, granttype, docKind, usaname, prjct, callRefNo, attachedBLOB) == true)
                    {
                     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "alert('Document: "+ filName + " uploaded and Saved successfully!');", true);
                    }
                    break;
               }

            //}
            //catch (Exception r)
            //{
            //    KCDFAlert.ShowAlert(r.Message);
            //}
          }

        protected void LoadMyMatrixUploads()
        {
            try
            {
                var userNM = Session["username"].ToString();
                var openP =
                    nav.projectOverview.ToList().Where(up => up.Username == userNM && up.Approval_Status == "Open").Select(pn => pn.No).SingleOrDefault();

                //KCDFAlert.ShowAlert(openP);
                var upsFiles = nav.myPOCAMatrixxUploads.ToList().Where(un => un.Username == userNM && un.Grant_No == openP);
                grViewMyDocs.AutoGenerateColumns = false;
                grViewMyDocs.DataSource = upsFiles;
                grViewMyDocs.DataBind();

                grViewMyDocs.UseAccessibleHeader = true;
                grViewMyDocs.HeaderRow.TableSection = TableRowSection.TableHeader;
                TableCellCollection cells = grViewMyDocs.HeaderRow.Cells;
                cells[0].Attributes.Add("data-class", "expand");
                cells[2].Attributes.Add("data-hide", "phone,tablet");
                cells[3].Attributes.Add("data-hide", "phone,tablet");
                cells[4].Attributes.Add("data-hide", "phone, tablet");
            }
            catch (Exception ex)
            {
                // KCDFAlert.ShowAlert("You have not uploaded documents yet!");
                // KCDFAlert.ShowAlert("You have not uploaded documents yet!");
                //KCDFAlert.ShowAlert(ex.Message);
                grViewMyDocs.EmptyDataText = "No Uploads found!";
            }
        }

    }

}