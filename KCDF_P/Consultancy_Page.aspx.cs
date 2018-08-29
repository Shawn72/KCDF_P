using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Web.Security;

namespace KCDF_P
{
    public partial class Consultancy_Page : System.Web.UI.Page
    {
        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
             new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                 ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };
        public  static readonly string strSQLConn = @"Server=" + ConfigurationManager.AppSettings["DB_INSTANCE"] + ";Database=" +
                                  ConfigurationManager.AppSettings["DB_NAME"] + "; User ID=" +
                                  ConfigurationManager.AppSettings["DB_USER"] + "; Password=" +
                                  ConfigurationManager.AppSettings["DB_PWD"] + "; MultipleActiveResultSets=true";

        public static string Company_Name = "KCDF";


        [STAThread]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                NoCache();
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");

                }
                if (!IsPostBack)
                {
                    LoadProfPic();
                    ReturnConsultancy();
                    getPostaCodes();
                    CheckSessX();
                    getProjects();
                    LoadMYProfile();
                    LoadmyApplications();
                    loadUploads();
                }
               
            }
           
        }

        public void NoCache()
        {
            Response.CacheControl = "private";
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }
        public void CheckSessX()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!this.IsPostBack)
            {
                Session["Reset"] = true;
                Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
                int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
                ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
            }
        }
        protected ConsultantClass ReturnConsultancy()
        {
            return new ConsultantClass(User.Identity.Name);
            //return new ConsultantClass(Session["username"].ToString());
        }
        protected void LoadProfPic()
        {
            try
            {
                var pic =
               nav.profile_Pics.ToList()
                   .Where(sn => sn.Username == Session["username"].ToString())
                   .Select(l => l.Filename)
                   .SingleOrDefault();

                if (pic == null)
                {
                    KCDFAlert.ShowAlert("Upload a profile picture");
                }
                else
                {
                    profPic.ImageUrl = "ProfilePics/Consultants/" + pic;
                    HttpResponse.RemoveOutputCacheItem("/Consultancy_Page.aspx");
                    // KCDFAlert.ShowAlert("ProfilePics/ "+pic);
                }
            }
            catch (Exception ex)
            {

            }

        }
        protected void btnUploadPic_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalProfP();", true);
        }
        protected void rdoBtnListYesNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["yesOrno"] = rdoBtnListYesNo.SelectedIndex;
            int selindex = Convert.ToInt32(Session["yesOrno"].ToString());
            switch (selindex)
            {
                case 0:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "IfYesIts()", true);
                     break;
                default:
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('I Haven't Consulted Yet!');", true);
                    break;
            }

        }
        protected void rdBtnStatus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["kcdfPstatus"] = rdBtnStatus.SelectedItem.Text;
        }
        protected void ddlPostalCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var pCode = ddlPostalCode.SelectedItem.Text;
            var postaTown =
                nav.list_myPosta.ToList()
                    .Where(cd => cd.Postal_Code == pCode)
                    .Select(pT => pT.Postal_Town)
                    .SingleOrDefault();
            txtPostalTown.Text = postaTown;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "pageLoad();", true);
        }
        protected void getProjects()
        {
            var projs = nav.call_for_Proposal.ToList().Where(pty => pty.Proposal_Type == "Consultancy");
            ddlConsultProject.DataSource = projs;
            ddlConsultProject.DataTextField = "Project";
            ddlConsultProject.DataValueField = "Call_Ref_Number";
            ddlConsultProject.DataBind();
            ddlConsultProject.Items.Insert(0, "--Select Project--");

            ddlProjectApp.DataSource = projs;
            ddlProjectApp.DataTextField = "Project";
            ddlProjectApp.DataValueField = "Call_Ref_Number";
            ddlProjectApp.DataBind();
            ddlProjectApp.Items.Insert(0, "--Select Project--");
        }
        protected void getPostaCodes()
        {
            try
            {
                var posta = nav.list_myPosta.ToList();
                ddlPostalCode.DataSource = posta;
                ddlPostalCode.DataTextField = "Postal_Code";
                ddlPostalCode.DataValueField = "Postal_Code";
                ddlPostalCode.DataBind();
                ddlPostalCode.Items.Insert(0, "--Select Postal Code--");

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('Your Postal code is not updated, Please update!!!');", true);

            }

        }
        protected void EditConsultant()
        {
            try
            {
                var usnM = Session["username"].ToString();
                var regiNM = ConsultantClass.IDNoReg;
                var namecontactPs = TextBxcont.Text;
                var currPostn = TextBoposition.Text;
                var postTow = txtPostalTown.Text;
                var PhoneN = TextBoxphone.Text;
                var siteonWeb = TextBoxweb.Text;
                var postaAddress = TextBxpostalAdd.Text;
                var postCode = "";
                bool kcdfb4 = false;
                int statusofProject = 0;

                int TorF = rdoBtnListYesNo.SelectedIndex;
                switch (TorF)
                {
                    case 0:
                        kcdfb4 = true;
                        break;
                    case 1:
                        kcdfb4 = false;
                        statusofProject = 0;
                        break;
                    default:
                        KCDFAlert.ShowAlert("Please select Yes or No!");
                        break;
                }

                int statS = rdBtnStatus.SelectedIndex;
                switch (statS)
                {
                    case 0:
                        statusofProject = Convert.ToInt32(rdBtnStatus.SelectedValue);
                        break;
                    case 1:
                        statusofProject = Convert.ToInt32(rdBtnStatus.SelectedValue);
                        break;
                    case 2:
                        statusofProject = Convert.ToInt32(rdBtnStatus.SelectedValue);
                        break;
                    default:
                        statusofProject =0;
                        break;
                }
                if (ddlPostalCode.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('Please select Postal Code!!!');", true);
                    ddlPostalCode.Focus();
                    return;
                }
                else
                {
                    postCode = ddlPostalCode.SelectedItem.Text;
                }

                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                if (sup.FnEditConsultant(usnM, regiNM, PhoneN, namecontactPs, currPostn, postCode, postTow, postaAddress, kcdfb4, statusofProject, siteonWeb)==true)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('Your Profile Edited Successfully!!');", true);
                    LoadMYProfile();
                }

            }
            catch (Exception Ec)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('"+ Ec.Message + "');", true);

            }
        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            EditConsultant();
        }
        protected void LoadMYProfile()
        {
            var myusername = Session["username"].ToString();
            var regIdN = ConsultantClass.IDNoReg;

            var edPrF =
                nav.myConsultants.ToList()
                    .Where(pc => pc.Organization_Username == myusername && pc.Organization_Registration_No == regIdN);

            TextBxcont.Text = edPrF.Select(cn => cn.Contact_Person_FullName).SingleOrDefault();
            TextBoposition.Text = edPrF.Select(pt => pt.Contact_Person_Position).SingleOrDefault();
            TextBxpostalAdd.Text = edPrF.Select(ad => ad.Postal_Address).SingleOrDefault();
            txtMyPostaIs.Text = edPrF.Select(pad => pad.Postal_Code).SingleOrDefault();
            txtPostalTown.Text = edPrF.Select(pTn => pTn.Postal_Town).SingleOrDefault();
            TextBoxphone.Text = edPrF.Select(pn => pn.Phone_Number).SingleOrDefault();
            TextBoxweb.Text = edPrF.Select(wb => wb.Website_Address).SingleOrDefault();

            bool yoN = edPrF.Select(yon => Convert.ToBoolean(yon.ConsultedForKCDFB4)).SingleOrDefault();
            switch (yoN)
            {
                case true:
                    rdoBtnListYesNo.SelectedIndex =0;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "IfYesIts()", true);
                    break;
                case false:
                    rdoBtnListYesNo.SelectedIndex=1;
                    break;
            }
            var statusIs = edPrF.Select(st =>st.Statusof_Project_Consulted).SingleOrDefault();
            switch (statusIs)
            {
                case "Never Consulted":
                    rdBtnStatus.SelectedIndex = -1;
                    break;
                case "Ongoing":
                    rdBtnStatus.SelectedIndex=0;
                    break;
                case "Complete":
                    rdBtnStatus.SelectedIndex = 1;
                    break;
                case "Terminated":
                    rdBtnStatus.SelectedIndex = 2;
                    break;
            }
        }
        protected void ddlConsultProject_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selProj = ddlConsultProject.SelectedItem.Text.Trim();
            int slVal = ddlConsultProject.SelectedIndex;
            switch (slVal)
            {
                case 0:
                    txtPrefNo.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CodesaBitch", "alert('Select valid project from dropdownlist!');", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CodesaBitch", "pageLoad();", true);
                    break;
                default:
                    txtPrefNo.Text = ddlConsultProject.SelectedValue;
                    Session["projectIs"] = selProj;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CodesaBitch", "ApplyForConsult();", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CodesaBitch", "pageLoad();", true);
                    break;
            }
        }
        protected void btnApplyConsult_OnClick(object sender, EventArgs e)
        {
            CheckApplication(txtPrefNo.Text);
        }
        protected void CheckApplication(string refNoH)
        {
          
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
               ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            bool  isT= sup.FnCheckIfApplied(Session["username"].ToString(), refNoH);
            if (isT == false)
            {
                ApplyConsult();
            }
            else{
               // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CodesaBitch", "alert('Already Applied!');", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CodesaBitch", "AlreadyIn();", true);
            }
            
        }
        protected void ApplyConsult()
        {
           try
            {
           
            var userNme = Session["username"].ToString();
            var projctName = Session["projectIs"].ToString();
            var refNo = txtPrefNo.Text;
            var myRNo = ConsultantClass.IDNoReg;

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
             ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            if (sup.FnAddConsultantApplication(userNme, myRNo, projctName, refNo) == true)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('You have Applied for this project!,Keep checking status on your Dashboard');", true);
                LoadmyApplications();
            }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert(" + ex.Message + ");", true);
            }

        }
        protected void LoadmyApplications()
        {
            try
            {
                var prjcts = nav.myConsultations.ToList().Where(us => us.Consultant_Username.Equals(Session["username"].ToString()));
                tblmyApplications.AutoGenerateColumns = false;
                tblmyApplications.DataSource = prjcts;
                tblmyApplications.DataBind();

                tblmyApplications.UseAccessibleHeader = true;
                tblmyApplications.HeaderRow.TableSection = TableRowSection.TableHeader;

                TableCellCollection cells = tblmyApplications.HeaderRow.Cells;
                cells[0].Attributes.Add("data-class", "expand");
                cells[2].Attributes.Add("data-hide", "phone,tablet");
                cells[3].Attributes.Add("data-hide", "phone,tablet");
                cells[4].Attributes.Add("data-hide", "phone, tablet");
                cells[5].Attributes.Add("data-hide", "phone, tablet");
                cells[6].Attributes.Add("data-hide", "phone, tablet");

            }
            catch (Exception ex)
            {
                // KCDFAlert.ShowAlert("No projects yet!");
                tblmyApplications.EmptyDataText = "No project data found";
            }
        }
        protected void ddlProjectApp_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selProj = ddlProjectApp.SelectedItem.Text.Trim();
            int slVal = ddlProjectApp.SelectedIndex;
            switch (slVal)
            {
                case 0:
                    textRefNo.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CodesaBitch", "alert('Select valid project from dropdownlist!');", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CodesaBitch", "pageLoad();", true);
                    break;
                default:
                    textRefNo.Text = ddlProjectApp.SelectedValue;
                    Session["projectName"] = selProj;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CodesaBitch", "UploadsDIV();", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CodesaBitch", "pageLoad();", true);
                    loadUploads();
                    break;
            }
        }
        protected void saveAttachment(string filName, string extension, string docKind, string callRefNo, string AttachedBLOB)
        {
            var usNo = nav.myConsultants.ToList().Where(usr => usr.Organization_Username == Session["username"].ToString()).Select(nu => nu.No).SingleOrDefault();
            var usaname = Session["username"].ToString();
            var prjct = ddlProjectApp.SelectedItem.Text;

            // string fullFPath = Request.PhysicalApplicationPath + "All Uploads\\" + Grantees.No + @"\" + filName;

            string navfilePath = @"D:\All_Portal_Uploaded\" + filName;

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            int granttype = 2;
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
            try
            {
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                if (sup.FnAttachmentConsultant(usNo, docType, navfilePath, filName, granttype, docKind, usaname, prjct, callRefNo,AttachedBLOB) == true)
                {
                  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('Document: " + filName + " uploaded and Saved successfully!');", true);
                    getProjects();
                }

            }
            catch (Exception r)
            {
               // KCDFAlert.ShowAlert(r.Message);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('"+ r.Message +" ');", true);
            }
        }
        protected void btnUploadProposal_OnClick(object sender, EventArgs e)
        {
            //try
            //{
                var documentKind = "Consultancy Proposal";
                var refNoIs = textRefNo.Text;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + ConsultantClass.No + @"\";

                // string uploadsFolder = @"\\192.168.0.249\All_Portal_Uploaded\"; @"\\KCDFSVR\All_Portal_Uploaded\";  //
                string fileName = Path.GetFileName(FileUploadProposal.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadProposal.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (FileUploadProposal.PostedFile.ContentLength > 5000000)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('Select a file less than 5MB!');", true);
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xlsx"))
                {

                    string filename = ConsultantClass.No + "_" + fileName;
                    FileUploadProposal.SaveAs(uploadsFolder + filename);

                    //file path to read file
                    string filePath = uploadsFolder + filename;
                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKind, refNoIs, attachedDoc);
                    loadUploads();
                }
                else
                {
                     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('File Format is : " + ext + "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!');", true);
                }
                if (!FileUploadProposal.HasFile)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('Select Document before uploading!');", true);
                    return;
                }

            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('"+ ex.Message + "');", true);
            //    // KCDFAlert.ShowAlert(ex.Message);
            //}
         }
        protected void btnUploadBudget_OnClick(object sender, EventArgs e)
        {
            //try
            //{
                var documentKind = "Proposal Budget";
                var refNoIs = textRefNo.Text;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + ConsultantClass.No + @"\";
                string fileName = Path.GetFileName(FileUploadBudget.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadBudget.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (FileUploadBudget.PostedFile.ContentLength > 5000000)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('Select a file less than 5MB!');", true);
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xlsx"))
                {
                string filename = ConsultantClass.No + "_" + fileName;
                FileUploadBudget.SaveAs(uploadsFolder + filename);
                //file path to read file
                string filePath = uploadsFolder + filename;
                FileStream file = File.OpenRead(filePath);
                byte[] buffer = new byte[file.Length];
                file.Read(buffer, 0, buffer.Length);
                file.Close();
                string attachedDoc = Convert.ToBase64String(buffer);
                saveAttachment(filename, ext, documentKind, refNoIs,attachedDoc);
                loadUploads();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('File Format is : " + ext + "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!');", true);
                }
                if (!FileUploadBudget.HasFile)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('Select Document before uploading!');", true);
                    return;
                }

            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('" + ex.Message + "');", true);
            //}
            
        }

        private static void UploadFileToFTP(string src)
        {
            String sourcefilepath = src;
            String ftpurl = "ftp://192.168.0.249/Uploads/"; 
            String ftpusername = "Administrator"; // e.g. username
            String ftppassword = "Admin7654321"; // e.g. password
            try
            {
                string filename = Path.GetFileName(sourcefilepath);
                string ftpfullpath = ftpurl;
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                FileStream fs = File.OpenRead(sourcefilepath);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void loadUploads()
        {
            try
            {
                var userNM = Session["username"].ToString();
                var openP = nav.myConsultations.ToList().Where(up => up.Consultant_Username == userNM && up.Approval_Status == "Open").Select(pn => pn.No).SingleOrDefault();

                //KCDFAlert.ShowAlert(openP);
                var upsFiles = nav.myConsultsUploads.ToList().Where(un => un.Username == userNM && un.Scholarship_No == openP);
                gridViewUploads.AutoGenerateColumns = false;
                gridViewUploads.DataSource = upsFiles;
                gridViewUploads.DataBind();

                }
                  catch (Exception ex){
                        // KCDFAlert.ShowAlert("You have not uploaded documents yet!");
                        KCDFAlert.ShowAlert(ex.Message);
                        //gridViewUploads.EmptyDataText = "No Uploads found!";
            }
        }

        //protected void FtpUpload()
        //{
        //    //FTP Server URL.
        //    string ftp = "ftp://192.168.0.249/";

        //    //FTP Folder name. Leave blank if you want to upload to root folder.
        //    string ftpFolder = "";

        //    byte[] fileBytes = null;

        //    //Read the FileName and convert it to Byte array.
        //    string fileName = Path.GetFileName(FileUploadBudget.FileName);
        //    using (StreamReader fileStream = new StreamReader(FileUploadBudget.PostedFile.InputStream))
        //    {
        //        fileBytes = Encoding.UTF8.GetBytes(fileStream.ReadToEnd());
        //        fileStream.Close();
        //    }

        //    //try
        //    //{
        //        //Create FTP Request.
        //        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + fileName);
        //        request.Method = WebRequestMethods.Ftp.UploadFile;

        //        //Enter FTP Server credentials.
        //        request.Credentials = new NetworkCredential("Administrator", "Admin7654321");
        //        request.ContentLength = fileBytes.Length;
        //        request.UsePassive = true;
        //        request.UseBinary = true;
        //        request.ServicePoint.ConnectionLimit = fileBytes.Length;
        //        request.EnableSsl = false;

        //        using (Stream requestStream = request.GetRequestStream())
        //        {
        //            requestStream.Write(fileBytes, 0, fileBytes.Length);
        //            requestStream.Close();
        //        }

        //        FtpWebResponse response = (FtpWebResponse)request.GetResponse();

        //        //lblMessage.Text += fileName + " uploaded.<br />";
        //        KCDFAlert.ShowAlert("Uploaded");
        //        response.Close();
        //    //}
        //    //catch (WebException ex)
        //    //{
        //    //    KCDFAlert.ShowAlert((ex.Response as FtpWebResponse).StatusDescription);
        //    //}
        //}
        protected void CopyFilesToDir()
        {
            try
            {
                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + ConsultantClass.No + @"\";
                string destPathserver1 = @"\\192.168.0.250\All Uploads\";

                foreach (string dirPath in Directory.GetDirectories(uploadsFolder, " * ",
                  SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(uploadsFolder, destPathserver1));

                //Copy all the files & Replaces any files with the same name

                // string destPathserver2 = @"\\192.168.0.249\All_Portal_Uploaded\";
                foreach (string newPath in Directory.GetFiles(uploadsFolder, "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(uploadsFolder, destPathserver1), true);
            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert(ex.Message);

            }
        }
        protected void CopyFilesToDir2Test()
        {
            //try
            //{
                string uploadsFolder = @"\\192.168.0.250\All Uploads\";
                string destPathserver2 = @"\\192.168.0.249\All_Portal_Uploaded\";

                AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

                WindowsIdentity idnt = new WindowsIdentity(@"kcdfoundation.org\Administrator", "Admin7654321");

                WindowsImpersonationContext context = idnt.Impersonate();

                foreach (string dirPath in Directory.GetDirectories(uploadsFolder, " * ",
                  SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(uploadsFolder, destPathserver2));

                //Copy all the files & Replaces any files with the same name

                // string destPathserver2 = @"\\192.168.0.249\All_Portal_Uploaded\";
                foreach (string newPath in Directory.GetFiles(uploadsFolder, "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(uploadsFolder, destPathserver2), true);
                KCDFAlert.ShowAlert("copied");
                context.Undo();
            //}
            //catch (Exception ex)
            //{
            //    KCDFAlert.ShowAlert(ex.Message);

            //}
        }
        protected void gridViewUploads_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var del_id = gridViewUploads.DataKeys[e.RowIndex].Values[0].ToString();
                Session["delMeID"] = del_id;

                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;

                var uploadsGrantNo =
                    nav.myConsultsUploads.ToList()
                           .Where(rU => rU.Id == Session["delMeID"].ToString() 
                            && rU.Username == Session["username"].ToString())
                           .Select(gN => gN.Scholarship_No).SingleOrDefault();
                bool confirmsubmit =
                    nav.myConsultations.ToList()
                        .Where(ri => ri.No == uploadsGrantNo)
                        .Select(sb => Convert.ToBoolean(sb.Application_Submitted))
                        .SingleOrDefault();
                switch (confirmsubmit)
                {
                    case true:
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('You CANT delete Upload for a submitted project, Replace it by Uploading again!');", true);
                        break;
                    case false:
                        if (sup.FnDeleteUploadConsult(Session["delMeID"].ToString()) == true)
                        {
                            KCDFAlert.ShowAlert("Deleted Successfully!" + uploadsGrantNo + " &&" + del_id);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('Deleted Successfully!" + uploadsGrantNo + " &&" + del_id+"');", true);
                            loadUploads();
                        }
                        break;
                }

               
            }
            catch (Exception ex)
            {
                //KCDFAlert.ShowAlert(ex.Message);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('"+ ex.Message + "');", true);

            }

        }
        protected void btnValidateInfo_OnClick(object sender, EventArgs e)
        {
            try
            {
                var tobevalidated = Session["edit_id"].ToString();
                //KCDFAlert.ShowAlert(tobevalidated);
                var usNM = Session["username"].ToString();

                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                bool myValid = sup.FnValidatSubmitConsultant(usNM, tobevalidated);
                switch (myValid)
                {
                    case true:
                        txtValidate.Text = "ALL UPLOADS AVAILABLE";
                        txtValidate.ForeColor = Color.GhostWhite;
                        txtValidate.BackColor = Color.ForestGreen;
                        btnValidateInfo.Enabled = false;
                       // hdnTxtValidit.Value = "isValid";
                        Session["validOR"]= "isValid";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('All Uploads available, you can submit your application: " + hdnTxtValidit.Value+"');", true);
                        break;

                    case false:
                        txtValidate.Text = "PLEASE COMPLETE THE APPLICATION FIRST";
                        txtValidate.BackColor = Color.Red;
                        txtValidate.ForeColor = Color.GhostWhite;
                       // hdnTxtValidit.Value = "isInValid";
                        Session["validOR"] = "isInValid";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('No uploads yet!, you cannot submit anything: " + hdnTxtValidit.Value + "');", true);
                        break;
                }


            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert(ex.Message);
                txtValidate.Text = "PLEASE UPLOAD ALL DOCUMENTS";
                txtValidate.BackColor = Color.Red;
                txtValidate.ForeColor = Color.GhostWhite;
            }
        }
        protected void FinalSubmitApplication(string usname, string refNoPrj)
        {
            try
            {

                var approvedyeah =
                nav.myConsultations.ToList()
                    .Where(a => a.No == refNoPrj)
                    .Select(ast => ast.Approval_Status)
                    .SingleOrDefault();

                switch (approvedyeah)
                {
                    case "Approved":
                        KCDFAlert.ShowAlert("You cannot Submit an Appproved application!!");
                        break;

                    case "Declined":
                        KCDFAlert.ShowAlert("Your application was declined!, You cannot submit");
                        break;

                    case "Open":
                        var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                        ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                        Portals sup = new Portals();
                        sup.Credentials = credentials;
                        sup.PreAuthenticate = true;
                        bool isSubmitted = sup.FnSubmitConsultApp(usname, refNoPrj);

                        switch (isSubmitted)
                        {
                            case true:
                                KCDFAlert.ShowAlert("Your Application is Successfully submitted!" + isSubmitted);
                                LoadmyApplications();
                                Session.Remove("validOR");
                                break;

                            case false:
                                KCDFAlert.ShowAlert("Your Application could not submitted!" + isSubmitted);
                                Session.Remove("validOR");
                                break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert("Error Loading!");
            }
            
        }

        protected void IsAGoPostIt(string usNme, string refId)
        {
            var subStatus = Session["validOR"].ToString();
            switch (subStatus)
            {
                case "isValid":
                    FinalSubmitApplication(usNme, refId);
                    break;
                case "isInValid":
                    KCDFAlert.ShowAlert("You can't Submit this application, because you have not uploaded all documents!");
                    break;
                default:
                    KCDFAlert.ShowAlert("Please Confirm attachemnts first! " + subStatus);
                    break;
            }
        }
        protected void lnkConfirAtt_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSubmit();", true);
            Session["edit_id"] = (sender as LinkButton).CommandArgument;
            var validMe = Session["edit_id"].ToString();
            lblProjNb.Text = validMe;
        }

        protected void lnkConfirm_OnClick(object sender, EventArgs e)
        {
            try
            {
                string edit_id = (sender as LinkButton).CommandArgument;
                var usernameIS = Session["username"].ToString();
                IsAGoPostIt(usernameIS, edit_id);
            }
            catch (Exception rt)
            {
              //  KCDFAlert.ShowAlert(rt.Message);
                KCDFAlert.ShowAlert("Could not submit! Check if everything is uploaded!");
            }
        }

        protected void copyTest_OnClick(object sender, EventArgs e)
        {
           /// UploadFileToFTP();
        }
    }
}