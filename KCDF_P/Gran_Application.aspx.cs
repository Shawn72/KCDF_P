using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;
using Microsoft.Ajax.Utilities;

namespace KCDF_P
{
    public partial class Gran_Application : System.Web.UI.Page
    {
        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
                new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            NoCache();
           
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");

                }
                
                LoadGrantsHistory();
                getProjects();
                loadIncompleteApplication();
                myCountyIs();
               // loadObjectivesHere();
                loadGranteeUploads();
                loadApplicationInfo();
               // checkSessX();
                lblUsernameIS.Text = Convert.ToString(Session["username"]);
                lblSessionfromMAster();
                getmeYears();
                loadUploads();
                LoadCurrentProject();
                LoadMyTargets();
                checkSubmitYeS();
            }
        }
        protected void lblSessionfromMAster()
        {
            System.Web.UI.WebControls.Label lblMastersession =
                (System.Web.UI.WebControls.Label)Master.FindControl("lblSessionUsername");

            lblMastersession.Text = lblUsernameIS.Text;
            Session["username"] = lblMastersession.Text;
        }
        public void NoCache()
        {
            Response.CacheControl = "private";
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }
        public void checkSessX()
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
        protected void studentInfoMenu_OnMenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            orgPMultiview.ActiveViewIndex = index;
        }
        protected void btnSaveGrantHisto_OnClick(object sender, EventArgs e)
        {
            try
            {
            int noOfBs = 0;
            decimal amtprovided = 0;
            bool kcdfPorNot = false;
            string yearOfAward = "";
            string usnme = Session["username"].ToString();
            string donorname = txtDonor.Text.Trim();
            string proj_name = ddlAccountType.SelectedItem.Text;
            string objGrnt = TextObj.Text.Trim();
            string trgtBenef = TextTypeBeneficiary.Text.Trim();
            string prjStatus = ddlPrjStatus.SelectedItem.Text;
            var callNo = txtPrefNo.Text.Trim();
            string tcountytrimmed = txtAreaCounties.Text.Substring(0, txtAreaCounties.Text.Length - 1);

            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert('Please fill in this required field!');", true);
                txtAmount.BorderColor = Color.Red;
                txtAmount.Focus();
                return;
            }
            else
            {
              amtprovided = Convert.ToDecimal(txtAmount.Text);
            }

            if (ddlYears.SelectedIndex==0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert('Please select year of Award!');", true);
                ddlYears.Focus();
                return;
            }
            else
            {
                yearOfAward = ddlYears.SelectedItem.Text;
            }

            if (string.IsNullOrWhiteSpace(TextNoBeneficiary.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert('Please fill the number of beneficiaries field!');", true);
                TextNoBeneficiary.BorderColor=Color.Red;
                TextNoBeneficiary.Focus();
                return;
            }
            else
            {
                noOfBs = Convert.ToInt32(TextNoBeneficiary.Text);
            }
           
            if (ddltargetCounty.SelectedIndex==0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert('Please select target counties!');", true);
                ddltargetCounty.Focus();
                return;
            }
            if (ddlPrjStatus.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert('Please select a valid project status!');", true);
                ddlPrjStatus.Focus();
                return;
            }
            int TorF = rdoBtnListYesNo.SelectedIndex;
            switch (TorF)
            {
                case 0:
                    kcdfPorNot = true;
                    break;
                case 1:
                    kcdfPorNot = false;
                    break;
                default:
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert('Please select Yes or No!!');", true);
                    break;
            }

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            if (sup.FnGrantManager(usnme, donorname, amtprovided, yearOfAward, objGrnt, tcountytrimmed, trgtBenef,
                noOfBs, prjStatus, callNo, kcdfPorNot, proj_name))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert('Your Data Saved Successfully!!');", true);
                    
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "ClearOverviewFields();", true);

                    txtDonor.Text = "";
                txtAmount.Text = "";
                TextObj.Text = "";
                ddltargetCounty.SelectedIndex = 0;
                TextTypeBeneficiary.Text = "";
                TextNoBeneficiary.Text = "";
                ddlPrjStatus.SelectedIndex = 0;
                txtAreaCounties.Text = "";
                LoadGrantsHistory();
              }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert('Error!!');", true);
                }
             }
            catch (Exception er)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert('Please verify all the Inputs before saving!');", true);
            }

        }
        protected void LoadGrantsHistory()
        {
            try
            {
                var grnhisto =
               nav.grant_history.ToList().Where(us => us.Organization_Username == Session["username"].ToString());
                tblGrantsManager.AutoGenerateColumns = false;
                tblGrantsManager.DataSource = grnhisto;
                tblGrantsManager.DataBind();
               

                tblGrantsManager.UseAccessibleHeader = true;
                tblGrantsManager.HeaderRow.TableSection = TableRowSection.TableHeader;
                TableCellCollection cells = tblGrantsManager.HeaderRow.Cells;
                cells[0].Attributes.Add("data-class", "expand");
                cells[2].Attributes.Add("data-hide", "phone,tablet");
                cells[3].Attributes.Add("data-hide", "phone,tablet");
                cells[4].Attributes.Add("data-hide", "phone, tablet");
                cells[5].Attributes.Add("data-hide", "phone, tablet");
                UpdatePanel5.Update();
            }
            catch (Exception ex)
            {
               // KCDFAlert.ShowAlert("No projects yet!");
                tblGrantsManager.EmptyDataText = "No grants management data found";
            }
           
        }
        protected void LoadCurrentProject()
        {
            var myP =
                nav.projectOverview.ToList()
                    .Where(
                        k =>
                            k.Username == Session["username"].ToString() && k.Grant_Approval=="Open");
            TextBoxtitle.Text = myP.Select(t=>t.Project_Title).SingleOrDefault();
            var doR = myP.Select(dr => Convert.ToDateTime(dr.Project_Start_Date)).SingleOrDefault();
            txtDateofStart.Value = doR.ToShortDateString();

            ddlSelCounty.SelectedIndex = 0;
            ddlConstituency.SelectedIndex = 0;
            txtAreaTargetSettmnt.Text = myP.Select(t => t.Urban_Settlement_Target).SingleOrDefault();
            ddlMonths.SelectedIndex = 0;
            ddlEstScale.SelectedIndex = 0;
            Decimal costP = myP.Select(mk => Convert.ToDecimal(mk.Total_Project_Cost_KES)).SingleOrDefault();
            TextBoxcost.Text = costP.ToString();
            Decimal yayocontrib = myP.Select(t => Convert.ToDecimal(t.Your_Cash_Contribution_KES)).SingleOrDefault();
            TextBoxcont.Text = yayocontrib.ToString();
            Decimal reqA= myP.Select(t => Convert.ToDecimal(t.Requested_KCDF_Amount_KES)).SingleOrDefault();
            TextBoxrequested.Text = reqA.ToString();
            txtCounty.Text = myP.Select(k => k.County).SingleOrDefault();
            txtSubCounty.Text= myP.Select(k => k.Constituency).SingleOrDefault();
            int ddL = myP.Select(k => Convert.ToInt32(k.Project_Length_Months)).SingleOrDefault();
            txtLength.Text = ddL.ToString();
            txtScale.Text= myP.Select(k => k.Grant_Scale).SingleOrDefault();
            pickLoadObjectives();
        }
        protected void btnUpdatePOverview_OnClick(object sender, EventArgs e)
        {
            int projectlength = 0;
            DateTime projTDt;
            decimal projCost = 0;
            decimal contrib = 0;
            decimal kcdffunds = 0;
            
            var usn = Session["username"].ToString();
            var callNo = txtPrefNo.Text.Trim();
            string projTt = TextBoxtitle.Text.Trim();
            string objs = txtAreaObjs.Text;

            if (string.IsNullOrWhiteSpace(TextBoxtitle.Text))
            {
                KCDFAlert.ShowAlert("Fill provide the title!");
                TextBoxtitle.BorderColor = Color.OrangeRed;
                TextBoxtitle.Focus();
                return;
            }

            int selObj = ddlObjectives.SelectedIndex;
            switch (selObj)
            {
                case 0:
                    KCDFAlert.ShowAlert("Please select an objective");
                    ddlObjectives.Focus();
                    break;
                default:
                    //InsertObjective();
                    break;
            }

            string county = ddlSelCounty.SelectedItem.Text;
            if (ddlSelCounty.SelectedIndex==0)
            {
                KCDFAlert.ShowAlert("Select valid county");
                ddlSelCounty.BackColor = Color.OrangeRed;
                return;
            }
            string constituency = ddlConstituency.SelectedItem.Text;
            if (ddlConstituency.SelectedIndex == 0)
            {
                KCDFAlert.ShowAlert("Select valid subcounty");
                ddlConstituency.BorderColor = Color.OrangeRed;
                return;
            }
            string urbantarget = txtAreaTargetSettmnt.Text.Trim();
            if (string.IsNullOrWhiteSpace(urbantarget))
            {
                KCDFAlert.ShowAlert("Please fill target textArea!");
                txtAreaTargetSettmnt.BorderColor = Color.Red;
                txtAreaTargetSettmnt.Focus();
                return;
            }
            string scale = ddlEstScale.SelectedItem.Text;
            if (ddlEstScale.SelectedIndex == 0)
            {
                KCDFAlert.ShowAlert("Select valid estimated  scale");
                ddlEstScale.Focus();
                ddlEstScale.BorderColor = Color.OrangeRed;
                return;
            }
            if (string.IsNullOrWhiteSpace(TextBoxcost.Text))
            {
                KCDFAlert.ShowAlert("fill in the Cost Please!");
                TextBoxcost.BorderColor = Color.Red;
                TextBoxcost.Focus();
                return;
            }
            else
            {
                projCost = Convert.ToDecimal(TextBoxcost.Text);

            }
            if (string.IsNullOrWhiteSpace(TextBoxcont.Text))
            {
                KCDFAlert.ShowAlert("Please fill in your contribution!");
                TextBoxcont.BorderColor = Color.Red;
                TextBoxcont.Focus();
                return;
            }
            else
            {
                contrib = Convert.ToDecimal(TextBoxcont.Text);
                decimal kcdfthing = projCost - contrib;
                TextBoxrequested.Text = kcdfthing.ToString();
                kcdffunds = contrib;
            }
            string projectNm = ddlAccountType.SelectedItem.Text;
            if (ddlAccountType.SelectedIndex == 0)
            {
                KCDFAlert.ShowAlert("Select valid Project first!");
                ddlAccountType.Focus();
                ddlAccountType.BorderColor = Color.Red;
                return;
            }
            
            if (ddlMonths.SelectedIndex == 0)
            {
                KCDFAlert.ShowAlert("Select valid project duration!");
                ddlMonths.Focus();
                ddlMonths.BackColor = Color.OrangeRed;
                return;
            }
            else
            {
               projectlength = Convert.ToInt32(ddlMonths.SelectedItem.Text);
            }
            var pstartD = txtDateofStart.Value;
            if (string.IsNullOrWhiteSpace(pstartD))
            {
                KCDFAlert.ShowAlert("Select a valid date!");
                txtDateofStart.Focus();
                return;
            }
            else
            {
                projTDt = DateTime.Parse(pstartD);
            }

            try
            {
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
                if (sup.FnProjectOverview(usn, county, constituency, projTt,
                    projectlength, projCost, contrib, kcdffunds, projTDt, scale, projectNm, callNo,objs) == true)
                {
                    //KCDFAlert.ShowAlert("Data saved Successfully!, KCDF Requested Amount : " + kcdffunds);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert('Data saved Successfully!');", true);
                    LoadCurrentProject();

                }
               
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert(" + ex.Message + ");", true);
                KCDFAlert.ShowAlert(ex.Message);

            }

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
        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
        protected void refreSH()
        {
           Response.Redirect(Request.RawUrl);Page.Response.Redirect(Page.Request.Url.ToString(), true);
           // ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "forceReload()", true);
        }
        protected Boolean saveAttachment(string filName, string extension, string docKind, string callRefNo,string attachedBlob)
        {
            var usNo = nav.grantees_Register.ToList().Where(usr => usr.Organization_Username == Session["username"].ToString()).Select(nu => nu.No).SingleOrDefault();
            var usaname = Session["username"].ToString();
            var prjct = ddlAccountType.SelectedItem.Text;

            // string fullFPath = Request.PhysicalApplicationPath + "All Uploads\\" + Grantees.No + @"\" + filName;

            string navfilePath = @"D:\All_Portal_Uploaded\" + filName;

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
            try
            {
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                if (sup.FnAttachment(usNo, docType, navfilePath, filName, granttype, docKind, usaname, prjct, callRefNo, attachedBlob) == true)
                {
                    KCDFAlert.ShowAlert("Document: " + filName + " uploaded and Saved successfully!");
                   // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "alert('Document: '" + filName + "'successfully uploaded!');", true);
                    loadUploads();
                    loadIncompleteApplication();
                    return true;
                }

            }
            catch (Exception r)
            {
                KCDFAlert.ShowAlert(r.Message);
            }
            return false;
        }
        protected void loadUploads()
        {
            //try
            //{
                var userNM = Session["username"].ToString();
                var openP =
                    nav.projectOverview.ToList().Where(up => up.Username == userNM && up.Grant_Approval == "Open" && up.Call_Ref_Number==ddlAccountType.SelectedValue).Select(pn=>pn.No).SingleOrDefault();

                //KCDFAlert.ShowAlert(openP);
                var upsFiles = nav.myUploads.ToList().Where(un => un.Username == userNM && un.Grant_No==openP);
                gridViewUploads.AutoGenerateColumns = false;
                gridViewUploads.DataSource = upsFiles;
                gridViewUploads.DataBind();

                //gridViewUploads.UseAccessibleHeader = true;
                //gridViewUploads.HeaderRow.TableSection = TableRowSection.TableHeader;
                //TableCellCollection cells = gridViewUploads.HeaderRow.Cells;
                //cells[0].Attributes.Add("data-class", "expand");
                //cells[2].Attributes.Add("data-hide", "phone,tablet");
                //cells[3].Attributes.Add("data-hide", "phone,tablet");
                //cells[4].Attributes.Add("data-hide", "phone, tablet");
            //}
            //catch (Exception ex)
            //{
            //    // KCDFAlert.ShowAlert("You have not uploaded documents yet!");
            //    //KCDFAlert.ShowAlert(ex.Message);
            //    gridViewUploads.EmptyDataText = "No Uploads found!";

            //}

        }
        protected void UploadFile(object sender, EventArgs e)
        {
            try
            {
                var documentKind = "Application Form";
            var refNoIs = txtPrefNo.Text;

            string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + @"\";

                 // string uploadsFolder = @"\\KCDFSVR\All_Portal_Uploaded\";  //@"\\192.168.0.249\All_Portal_Uploaded\";
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
            if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xls") || (ext == ".xlsx"))
            {

            string filename = Grantees.No + "_" + fileName;
            FileUpload.SaveAs(uploadsFolder + filename);

            //file path to read file
            string filePath = uploadsFolder + filename;
            FileStream file = File.OpenRead(filePath);
            byte[] buffer = new byte[file.Length];
            file.Read(buffer, 0, buffer.Length);
            file.Close();
            string attachedDoc = Convert.ToBase64String(buffer);

            saveAttachment(filename, ext, documentKind, refNoIs,attachedDoc);
            appFm.InnerText = "Uploaded!";
            loadUploads();
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
                //KCDFAlert.ShowAlert("Unkown Error Occured!");
                // KCDFAlert.ShowAlert(ex.Message);
                appFm.InnerText = "Not Uploaded!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "ErrorLabel1();", true);
            }

        }
        protected void btnUploadID_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKind = "Registration Certificate";
                var refNoIs = txtPrefNo.Text;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + @"\";
                string fileName = Path.GetFileName(FileUploadID.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadID.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (FileUploadID.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xls") || (ext == ".xlsx"))
                {
                    string filename = Grantees.No + "_" + fileName;
                    FileUploadID.SaveAs(uploadsFolder + filename);
                    //file path to read file
                    string filePath = uploadsFolder + filename;
                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKind, refNoIs,attachedDoc);
                    loadUploads();
                    lblID.InnerText = "Uploaded!";
                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext + "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

                }
                if (!FileUploadID.HasFile)
                {
                    KCDFAlert.ShowAlert("Select Document before uploading");
                    return;
                }

             }
            catch (Exception ex)
            {
                // KCDFAlert.ShowAlert("Unkown Error Occured!");
                //KCDFAlert.ShowAlert(ex.Message);
                lblID.InnerText = "Not Uploaded!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "ErrorLabel3();", true);
            }
        }
        protected void btnUploadConstitution_OnClick(object sender, EventArgs e)
        {

            try
            {
                var documentKind = "Organizational Constitution ";
                var refNoIs = txtPrefNo.Text;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + @"\";
                string fileName = Path.GetFileName(FileUploadConst.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadConst.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (FileUploadConst.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xls") || (ext == ".xlsx"))
                {
                    string filename = Grantees.No + "_" + fileName;
                    FileUploadConst.SaveAs(uploadsFolder + filename);
                    //file path to read file
                    string filePath = uploadsFolder + filename;
                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKind, refNoIs,attachedDoc);
                    loadUploads();
                    lblOC.InnerText = "Uploaded!";
                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext + "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

                }
                if (!FileUploadConst.HasFile)
                {
                    KCDFAlert.ShowAlert("Select Document before uploading");
                    return;
                }

              }
            catch (Exception ex)
            {
                // KCDFAlert.ShowAlert("Unkown Error Occured! "+ ex.Message);
                // KCDFAlert.ShowAlert(ex.Message);
                lblOC.InnerText = "Not Uploaded!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "ErrorLabel4();", true);
            }
        }
        protected void btnUploadList_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKindML = "Members List";
                var refNoIs = txtPrefNo.Text;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + @"\";
                string fileName = Path.GetFileName(FileUploadList.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadList.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (FileUploadList.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xls") || (ext == ".xlsx"))
                {
                    string filename = Grantees.No + "_" + fileName;
                    FileUploadList.SaveAs(uploadsFolder + filename);
                    //file path to read file
                    string filePath = uploadsFolder + filename;
                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKindML, refNoIs,attachedDoc);
                    loadUploads();
                    lblUL.InnerText = "Uploaded!";
                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext + "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

                }
                if (!FileUploadList.HasFile)
                {
                    KCDFAlert.ShowAlert("Select Document before uploading");
                    return;
                }

            }
            catch (Exception ex)
            {
                // KCDFAlert.ShowAlert("Unkown Error Occured! "+ ex.Message);
                //KCDFAlert.ShowAlert(ex.Message);
                lblUL.InnerText = "Not Uploaded!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "ErrorLabel5();", true);
            }

        }
        protected void btnFinReport_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKindFR = "Financial Report ";
                var refNoIs = txtPrefNo.Text;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + @"\";
                string fileName = Path.GetFileName(FileUploadFinRePo.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadFinRePo.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (FileUploadFinRePo.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") ||(ext == ".xls")|| (ext == ".xlsx"))
                {
                    string filename = Grantees.No + "_" + fileName;
                    FileUploadFinRePo.SaveAs(uploadsFolder + filename);
                    //file path to read file
                    string filePath = uploadsFolder + filename;
                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKindFR, refNoIs,attachedDoc);
                    loadUploads();
                    lblFR.InnerText = "Uploaded!";
                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext + "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

                }
                if (!FileUploadFinRePo.HasFile)
                {
                    KCDFAlert.ShowAlert("Select Document before uploading");
                    return;
                }

            }
            catch (Exception ex)
            {
                // KCDFAlert.ShowAlert("Unkown Error Occured!"+ ex.Message);
                //KCDFAlert.ShowAlert(ex.Message);
                lblFR.InnerText = "Not Uploaded!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "ErrorLabel6();", true);
            }

        }
        protected void getProjects()
       {
            var projs = nav.call_for_Proposal.ToList().Where(pty=>pty.Proposal_Type=="Grant");
            ddlAccountType.DataSource = projs;
            ddlAccountType.DataTextField = "Project";
            ddlAccountType.DataValueField = "Call_Ref_Number";
            ddlAccountType.DataBind();
            ddlAccountType.Items.Insert(0, "--Select Project--");
        }
        protected void gridViewUploads_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           //try
           //{
               var del_id = gridViewUploads.DataKeys[e.RowIndex].Values[0].ToString();
                Session["delMeID"] = del_id;

                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                var uploadsGrantNo = nav.myUploads.ToList().Where(rU => rU.Id == Session["delMeID"].ToString() && rU.Username == Session["username"].ToString()).Select(gN => gN.Grant_No).SingleOrDefault();
                sup.FnChangeSubmitStatus(uploadsGrantNo, Session["username"].ToString());

               if (sup.FnDeleteUpload(Session["delMeID"].ToString())== true)
               {
                   //KCDFAlert.ShowAlert("Deleted Successfully!" + uploadsGrantNo + " &&" + del_id);
                   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "alert('Deleted Succesfully! '" + uploadsGrantNo + "'');", true);
                   loadUploads();
                   loadIncompleteApplication();
               }
           //}
           //catch (Exception ex)
           //{
           //KCDFAlert.ShowAlert(ex.Message);
           //}
        }
        protected void ddltargetCounty_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            txtAreaCounties.Visible = true;
            string countyW = ddltargetCounty.SelectedItem.Text.Trim();
            AppendCounties(countyW);
            //string countiesminus = txtAreaCounties.Text;
            //KCDFAlert.ShowAlert(countiesminus.Remove(countiesminus.Length-1));
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "pageLoad();", true);
        }
        protected void AppendCounties(string str2Append)
        {
              txtAreaCounties.Text = txtAreaCounties.Text + str2Append+", ";
        }
        protected void orgInfoMenu2_OnMenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            orgPMultiview.ActiveViewIndex = index;
            if (index == 2)
            {
                orgPMultiview.SetActiveView(Projectoverview);
            }
            if (index == 3)
            {
               orgPMultiview.SetActiveView(targetGroup);
            }
            if (index == 4)
            {
                orgPMultiview.SetActiveView(uploadDocs);
            }
            if (index == 5)
            {
                orgPMultiview.SetActiveView(finalSubmit);
            }
        }
        protected void tblGrantsManager_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var del_id = tblGrantsManager.DataKeys[e.RowIndex].Values[0].ToString();
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            sup.FnDeleteGrant(del_id);
            LoadGrantsHistory();
            KCDFAlert.ShowAlert("Grant Deleted Successfully!");

        }
        protected void LoadMyTargets()
        {
            var openpr =
                nav.projectOverview.ToList()
                    .Where(i => i.Username == Session["username"].ToString() && i.Grant_Approval == "Open")
                    .Select(r => r.Call_Ref_Number).SingleOrDefault();

            KCDFAlert.ShowAlert(openpr);

            var gvtarget =
                nav.target_Groups.ToList().Where(un => un.Organization_username == Session["username"].ToString() && un.ProjCallRefNo == openpr);
            int hs = gvtarget.Select(ttm => Convert.ToInt32(ttm.House_Holds)).SingleOrDefault();
            TextBoxhse.Text = Convert.ToString(hs);

            int sc = gvtarget.Select(ttm => Convert.ToInt32(ttm.Schools)).SingleOrDefault();
            TextBoxschl.Text = Convert.ToString(sc);

            int org = gvtarget.Select(ttm => Convert.ToInt32(ttm.Grass_root_organizations)).SingleOrDefault();
            TextBoxorg.Text = Convert.ToString(org);

            int yth = gvtarget.Select(ttm => Convert.ToInt32(ttm.Youth_aged_18_35_years)).SingleOrDefault();
            TextBoxyth.Text = Convert.ToString(yth);

            int wnm = gvtarget.Select(ttm => Convert.ToInt32(ttm.women)).SingleOrDefault();
            TextBowmn.Text = Convert.ToString(wnm);

            int mn = gvtarget.Select(ttm => Convert.ToInt32(ttm.Men)).SingleOrDefault();
            TextBoxmn.Text = Convert.ToString(mn);

            int blod = gvtarget.Select(ttm => Convert.ToInt32(ttm.Children_0_6_years_old)).SingleOrDefault();
            TextBcldold.Text = Convert.ToString(blod);

            int o7d = gvtarget.Select(ttm => Convert.ToInt32(ttm.Children_7_14_years_old)).SingleOrDefault();
            TextBoxold.Text = Convert.ToString(o7d);

            int o15d = gvtarget.Select(ttm => Convert.ToInt32(ttm.Children_15_17_years_old)).SingleOrDefault();
            TextBoxren.Text = Convert.ToString(o15d);

            int orp = gvtarget.Select(ttm => Convert.ToInt32(ttm.OrphansVulnerable_Children)).SingleOrDefault();
            TextBoxorph.Text = Convert.ToString(orp);

            int ill = gvtarget.Select(ttm => Convert.ToInt32(ttm.Terminal_Illness)).SingleOrDefault();
            TextBoxill.Text = Convert.ToString(ill);


            int marg = gvtarget.Select(ttm => Convert.ToInt32(ttm.Marginalizedpopulation)).SingleOrDefault();
            TextBoxmarg.Text = Convert.ToString(marg);

            int drg = gvtarget.Select(ttm => Convert.ToInt32(ttm.Drug_users)).SingleOrDefault();
            TextBoxdrg.Text = Convert.ToString(drg);

            int sexw = gvtarget.Select(ttm => Convert.ToInt32(ttm.Commercial_Sex_Workers)).SingleOrDefault();
            TextBoxsxwrkr.Text = Convert.ToString(sexw);

            int farm = gvtarget.Select(ttm => Convert.ToInt32(ttm.Farmers)).SingleOrDefault();
            TextBoxfarmr.Text = Convert.ToString(farm);

            int tchr = gvtarget.Select(ttm => Convert.ToInt32(ttm.Teachers)).SingleOrDefault();
            TextBoxtchr.Text= Convert.ToString(tchr);
        }
        protected void btnSaveTarget_Click(object sender, EventArgs e)
        {
            try
            {
            string proj = ddlAccountType.SelectedItem.Text;
            string prjRef = ddlAccountType.SelectedValue;
            var usnm = Session["username"].ToString();

                int hsehlds = 0;
                int schls = 0;
                if (TextBoxhse.Text == null)
                {
                    KCDFAlert.ShowAlert("Field required");
                    TextBoxhse.BackColor=Color.Red;
                }
                else
                {
                    hsehlds = Convert.ToInt32(TextBoxhse.Text);
                }
                if (TextBoxschl.Text == null)
                {
                    KCDFAlert.ShowAlert("Field required");
                    TextBoxschl.BackColor = Color.Red;
                }
                else
                {
                    schls = Convert.ToInt32(TextBoxschl.Text);
                }
                string org = TextBoxorg.Text;
                int yth = Convert.ToInt32(TextBoxyth.Text);
                int wmn = Convert.ToInt32(TextBowmn.Text);
                int mn = Convert.ToInt32(TextBoxmn.Text);
                int chldold = Convert.ToInt32(TextBcldold.Text);
                int old = Convert.ToInt32(TextBoxold.Text);
                int ren = Convert.ToInt32(TextBoxren.Text);
                int orph = Convert.ToInt32(TextBoxorph.Text);
                int ill = Convert.ToInt32(TextBoxill.Text);
                int marg = Convert.ToInt32(TextBoxmarg.Text);
                int drg = Convert.ToInt32(TextBoxdrg.Text);
                int sxwrkr = Convert.ToInt32(TextBoxsxwrkr.Text);
                int tchr = Convert.ToInt32(TextBoxtchr.Text);
                int frmr = Convert.ToInt32(TextBoxfarmr.Text);

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
                if (sup.FnSaveToGargetGroup(usnm, hsehlds, schls, org, yth, wmn, mn, chldold, old, ren, orph, ill, marg,
                    drg,sxwrkr, tchr, frmr, proj, prjRef) == true)
                {
                    KCDFAlert.ShowAlert("Target group information saved successfully!");
                    LoadMyTargets();

                }
                else
                {
                    KCDFAlert.ShowAlert("Error Occured Saving Info!");
                }
            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert(ex.Message);
            }
        }
        protected void btnValidateInfo_OnClick(object sender, EventArgs e)
        {
            try
            {
                var tobevalidated = Session["edit_id"].ToString();
                //KCDFAlert.ShowAlert(tobevalidated);
                var prj = ddlAccountType.SelectedItem.Text;
                var usNM = Session["username"].ToString();
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                bool myValid = sup.FnValidateSubmission(usNM, tobevalidated);
                switch (myValid)
                {
                    case true:
                    txtValidate.Text = "ALL UPLOADS AVAILABLE";
                    txtValidate.ForeColor = Color.GhostWhite;
                    txtValidate.BackColor = Color.ForestGreen;
                    btnFinalSubmit.Enabled = true;
                    btnValidateInfo.Enabled = false;
                    hdnTxtValidit.Value = "isValid";
                    KCDFAlert.ShowAlert("All Uploads available, you can submit your application: " + hdnTxtValidit.Value);
                    break;

                    case false:
                    txtValidate.Text = "PLEASE COMPLETE THE APPLICATION FIRST";
                    txtValidate.BackColor = Color.Red;
                    txtValidate.ForeColor = Color.GhostWhite;
                    btnFinalSubmit.Enabled = false;
                    hdnTxtValidit.Value = "isInValid";
                    sup.FnChangeSubmitStatus(tobevalidated, usNM);
                    KCDFAlert.ShowAlert("No uploads yet!, you cannot submit anything: " + hdnTxtValidit.Value);
                    break;
                }

                }
                    catch (Exception ex)
                    {
                        KCDFAlert.ShowAlert(ex.Message);
                        txtValidate.Text = "PLEASE UPLOAD ALL DOCUMENTS";
                        txtValidate.BackColor = Color.Red;
                        txtValidate.ForeColor = Color.GhostWhite;
                        btnFinalSubmit.Enabled = false;
                    }
                }
        protected void btnFinalSubmit_OnClick(object sender, EventArgs e)
        {
            try
            {
                var usNM = Session["username"].ToString();
                var projTtle = ddlAccountType.SelectedItem.Text;

                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                bool isSubmitted = sup.FnFinalSubmission(usNM, projTtle);

                switch (isSubmitted)
                {
                    case true:
                        KCDFAlert.ShowAlert("Your Application is Successfully submitted!" + isSubmitted);
                        loadIncompleteApplication();
                        break;

                    case false:
                        KCDFAlert.ShowAlert("Your Application could not submitted!" + isSubmitted);
                        break;

                }
            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert(ex.Message);
            }

        }
        protected void lnkEditMe_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSubmit();", true);
            Session["edit_id"] = (sender as LinkButton).CommandArgument;
            var validMe = Session["edit_id"].ToString();
            lblProjNb.Text = validMe;

        }

        protected void checkSubmitYeS()
        {
            var usn = Session["username"].ToString();
            var msubmitted =
                nav.projectOverview.ToList().Where(us => us.Username == usn && us.Grant_Approval == "Open")
                    .Select(k =>k.projectisSubmitted).SingleOrDefault();
            if (msubmitted == true)
            {
                btnSumbitFinal.Visible = false;
                btnSumbitFinal.Enabled = false;
            }
            else
            {
                btnSumbitFinal.Visible = true;
                btnSumbitFinal.Enabled = true;
            }
        }
        private void loadIncompleteApplication()
        {
            checkSubmitYeS();
            var usn = Session["username"].ToString();
            try
            {
                var inComp = nav.projectOverview.Where(us=> us.Username == usn && us.Grant_Approval == "Open"  && (us.projectisSubmitted==false)).ToList();
                gridSubmitApps.AutoGenerateColumns = false;
                gridSubmitApps.DataSource = inComp;
                gridSubmitApps.DataBind();
                
            }
            catch (Exception ex)
            {
               // KCDFAlert.ShowAlert(ex.Message);
                gridSubmitApps.EmptyDataText = "No Application data found!";
            }

        }
        protected void myCountyIs()
        {
            var mycounty = nav.mycountyIs.ToList();
            ddlSelCounty.DataSource = mycounty;
            ddlSelCounty.DataTextField = "County_Name";
            ddlSelCounty.DataValueField = "County_Code";
            ddlSelCounty.DataBind();
            ddlSelCounty.Items.Insert(0, "--Select your County--");

            ddltargetCounty.DataSource = mycounty;
            ddltargetCounty.DataTextField = "County_Name";
            ddltargetCounty.DataValueField = "County_Code";
            ddltargetCounty.DataBind();
            ddltargetCounty.Items.Insert(0, "--Select your County--");
           
        }
        protected void ddlSelCounty_OnSelectedIndexChanged(object sender, EventArgs e)
        {
           
            int selIndex = ddlSelCounty.SelectedIndex;
            switch (selIndex)
            {
                case 0:
                     KCDFAlert.ShowAlert("Invalid County selection");      
                    break;
                default:
                    var sbCntysplit00 = ddlSelCounty.SelectedValue;
                    //var sbcoutysplit = new StringBuilder(sbCntysplit00);
                    //sbcoutysplit.Remove(0, 2); //Trim two characters from position 1
                    //sbCntysplit00 = sbcoutysplit.ToString();
                    var subCnty = nav.mysubCountyIs.Where(sc => sc.County_Code == sbCntysplit00).ToList();
                    ddlConstituency.DataSource = subCnty;
                    ddlConstituency.DataTextField = "Sub_County_Name";
                    ddlConstituency.DataValueField = "Sub_County_Name";
                    ddlConstituency.DataBind();
                    ddlConstituency.Items.Insert(0, "--Select your Sub County--");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "pageLoad();", true);
                    KCDFAlert.ShowAlert(sbCntysplit00);
                    break;
            }

        }
        protected void ddlAccountType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selProj = ddlAccountType.SelectedItem.Text.Trim();
            int slVal = ddlAccountType.SelectedIndex;
            switch (slVal)
            {
              case 0:
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "alert('Select valid project from dropdownlist!');", true);
                    btnUpdatePOverview.Enabled = false;
                    txtPrefNo.Text = "";
                    break;
            default:
                    btnUpdatePOverview.Enabled = true;
                    loadObjectivesHere(selProj);
                    txtPrefNo.Text = ddlAccountType.SelectedValue;
                    loadUploads();
                    break;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "pageLoad();", true);
        }
        protected void rdoBtnListYesNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["yesOrno"] = rdoBtnListYesNo.SelectedIndex;
            int selindex = Convert.ToInt32(Session["yesOrno"].ToString());
            switch (selindex)
            {
                case 0:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "IfYesIts()", true);
                    //KCDFAlert.ShowAlert("clicked: "+ selindex);
                    break;
                default:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "alert('Non KCDF grant!')", true);
                    break;
            }
            
        }
        protected void rdBtnStatus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["kcdfPstatus"] = rdBtnStatus.SelectedItem.Text;
        }
        protected void loadGranteeUploads()
        {
            try
            {
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/Downloads_Grantees/"));
                List<ListItem> files = new List<ListItem>();
                foreach (string filePath in filePaths)
                {
                    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                }
                gridVGrantsDownloads.DataSource = files;
                gridVGrantsDownloads.DataBind();
            }
            catch (Exception ex)
            {

                KCDFAlert.ShowAlert("No Uploads!");
            }

        }
        protected void ddlObjectives_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Objectives from NaVISION
            txtAreaObjs.Visible = true;
            string obJs = ddlObjectives.SelectedItem.Text.Trim();
            AppendObjectives(obJs);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "pageLoad();", true);
        }
        protected void AppendObjectives(string str2AppendObj)
        {
            txtAreaObjs.Text = txtAreaObjs.Text + str2AppendObj + ", ";
        }
        protected void pickLoadObjectives()
        {
            var objctvs = nav.projectOverview.ToList()
                  .Where(oj => oj.Username.Equals(Session["username"].ToString())
                  && oj.Grant_Approval=="Open");

            if (objctvs.Equals(null))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert('You have not saved objectives for this project yet!');", true);
             }
            else
            {
                txtAreaObjs.Visible = true;
                txtAreaObjs.Text = objctvs.Select(obc => obc.Objectives).SingleOrDefault();
            }

        }
        protected void InsertObjective()
        {
            var prjNm = ddlAccountType.SelectedItem.Text;
            var callNo = txtPrefNo.Text.Trim();
            var txtArObjs = txtAreaObjs.Text.TrimEnd('?', '.', ',');
            int mytxtAreaLn = txtAreaObjs.MaxLength;
            int myobjlength = txtArObjs.Length;
            //save objectives
            switch (txtArObjs)
            {
                case null:
                    KCDFAlert.ShowAlert("Please select Objectives!");
                    break;
                default:
                    //save in naVision
                    if (myobjlength > mytxtAreaLn)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "alert('Maximum length of textarea input exceeded!, DELETE the last entry!')", true);
                        txtAreaObjs.Enabled = true;
                        return;
                    }
                    else
                    {
                        try
                        {
                            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                            Portals sup = new Portals();
                            sup.Credentials = credentials;
                            sup.PreAuthenticate = true;
                            if (sup.FnAddProjObjective(Session["username"].ToString(), prjNm, txtArObjs, callNo) == true)
                            {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "alert('Project Objectives Saved!')", true);
                               // pickLoad();
                            }
                            else
                            {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "alert('Error Occured!')", true);
                            }
                        }
                        catch (Exception exc)
                        {
                            KCDFAlert.ShowAlert(exc.Message);
                        }
                    }
                    break;
            }
        }
        protected void loadApplicationInfo()
        {
            var granteeInfo =nav.grantees_Register.ToList()
                .Where(n => n.Organization_Username.Equals(Session["username"].ToString()));
            TextBxcont.Text = granteeInfo.Select(co => co.Contact_Person).SingleOrDefault();
            TextBoposition.Text = granteeInfo.Select(po => po.Current_Position).SingleOrDefault();
            TextBxpostal.Text = granteeInfo.Select(pa => pa.Postal_Address).SingleOrDefault();
            txtPostalCode.Text = granteeInfo.Select(pc => pc.Postal_Code).SingleOrDefault();
            txtPostalTown.Text = granteeInfo.Select(ta => ta.Town).SingleOrDefault();
            TextBoxphone.Text = granteeInfo.Select(pn => pn.Phone).SingleOrDefault();
            txEmailAdd.Text = granteeInfo.Select(em => em.Email).SingleOrDefault();
            TextBoxweb.Text = granteeInfo.Select(wb => wb.Website).SingleOrDefault();
            txOrgname.Text = granteeInfo.Select(on => on.Organization_Name).SingleOrDefault();

            var ngO = granteeInfo.Select(ot => ot.NGO).SingleOrDefault();
            switch (ngO)
            {
                case true:
                    // txtNGO.Text = "Non Governmental Organization";
                    checkIFNgO.SelectedIndex = 1;
                    break;
                case false:
                    // txtNGO.Text = "Governmental Organization";
                    checkIFNgO.SelectedIndex =0;
                    break;
                default:
                   // txtNGO.Text = "Incomplete Information";
                    break;
            }
            var proftb = granteeInfo.Select(pft => pft.Profitable).SingleOrDefault();
            switch (proftb)
            {
                case true:
                    //txtNGO.Text = "Profitable Organization";
                    nonProfitOR.SelectedIndex = 1;
                    break;
                case false:
                    nonProfitOR.SelectedIndex = 0;
                    // txtNGO.Text = "Non-Profitable Organization";
                    break;
                default:
                   KCDFAlert.ShowAlert("Please go to edit type of your organization's information!");
                    break;
            }
            txtTypeofOrg.Text = granteeInfo.Select(rt => rt.Type_Of_Organization).SingleOrDefault();
           // var doR = granteeInfo.Select(dr => Convert.ToDateTime(dr.Date_Registered)).SingleOrDefault();
            txtRegNo.Text = granteeInfo.Select(rg => rg.Registration_No).SingleOrDefault();
         
        }
        protected void lnkConfirm_OnClick(object sender, EventArgs e)
        {
            var subStatus = hdnTxtValidit.Value;
            switch (subStatus)
            {
                case "isValid":
                    submitProject();
                    break;
                case "isInValid":
                    KCDFAlert.ShowAlert("You can't cubmit this application, because you have not uploaded all documents!");
                    break;
                default:
                    KCDFAlert.ShowAlert("Please Confirm attachemnts first! " + hdnTxtValidit.Value);
                    break;
            }


        }
        protected void submitProject()
        {
            try
            {
                var usNM = Session["username"].ToString();
                var projTtle = ddlAccountType.SelectedValue;

                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                bool isSubmitted = sup.FnFinalSubmission(usNM, projTtle);

                switch (isSubmitted)
                {
                    case true:
                        KCDFAlert.ShowAlert("Your Application is Successfully submitted!" + isSubmitted);
                        loadIncompleteApplication();
                        break;

                    case false:
                        KCDFAlert.ShowAlert("Your Application could not submitted!" + isSubmitted);
                        break;

                }
            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert(ex.Message);
            }

        }
        protected void loadObjectivesHere( string projName)
        {
            try
            {
                var objS = nav.project_objectives.Where(p => p.Project == projName).ToList();
                ddlObjectives.DataSource = objS;
                ddlObjectives.DataTextField = "Objective";
                ddlObjectives.DataValueField = "Objective";
                ddlObjectives.DataBind();
                ddlObjectives.Items.Insert(0, "--Select Objective--");
            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert(ex.Message);
            }
        }
        protected void ddlBenType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            TextTypeBeneficiary.Visible = true;
            string beneFic = ddlBenType.SelectedItem.Text.Trim();
            AppendBenefic(beneFic);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "pageLoad();", true);

        }
        protected void AppendBenefic(string str2Append)
        {
            TextTypeBeneficiary.Text = TextTypeBeneficiary.Text + str2Append + ", ";
        }
        protected void ddlYears_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["yearofAwd"] = ddlYears.SelectedItem.Text;
        }
        protected void getmeYears()
        {
            // Clear items:    
            ddlYears.Items.Clear();
            // Add default item to the list
            ddlYears.Items.Add("--Select Year--");
            // Start loop
            for (int i = 0; i < 20; i++)
            {
                // For each pass add an item
                // Add a number of years (negative, which will subtract) to current year
                ddlYears.Items.Add(DateTime.Now.AddYears(-i).Year.ToString());
            }
          
        }
        protected void btnProjectBudget_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKindFR = "Proposed Budget ";
                var refNoIs = txtPrefNo.Text;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + @"\";
                string fileName = Path.GetFileName(FileUploadProjectBudget.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadProjectBudget.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (FileUploadProjectBudget.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xls") || (ext == ".xlsx"))
                {
                    string filename = Grantees.No + "_" + fileName;
                    FileUploadProjectBudget.SaveAs(uploadsFolder + filename);
                    //file path to read file
                    string filePath = uploadsFolder + filename;
                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKindFR, refNoIs, attachedDoc);
                    loadUploads();
                    lblPB.InnerText = "Uploaded!";
                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext + "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

                }
                if (!FileUploadProjectBudget.HasFile)
                {
                    KCDFAlert.ShowAlert("Select Document before uploading");
                    return;
                }

            }
            catch (Exception ex)
            {
                //KCDFAlert.ShowAlert("Unkown Error Occured!" + ex.Message);
                //KCDFAlert.ShowAlert(ex.Message);
                lblPB.InnerText = "Not Uploaded!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "ErrorLabel2();", true);

            }

        }
        protected void btnBillofQTY_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKindFR = "Bill of Quantities ";
                var refNoIs = txtPrefNo.Text;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + @"\";
                string fileName = Path.GetFileName(FileUploadtheBill.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadtheBill.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (FileUploadtheBill.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xls") || (ext == ".xlsx"))
                {
                    string filename = Grantees.No + "_" + fileName;
                    FileUploadtheBill.SaveAs(uploadsFolder + filename);
                    //file path to read file
                    string filePath = uploadsFolder + filename;
                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKindFR, refNoIs, attachedDoc);
                    loadUploads();
                    lblBill.InnerText = "Uploaded!";
                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext + "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

                }
                if (!FileUploadtheBill.HasFile)
                {
                    KCDFAlert.ShowAlert("Select Document before uploading");
                    return;
                }

            }
            catch (Exception ex)
            {
                //  KCDFAlert.ShowAlert("Unkown Error Occured!" + ex.Message);
                //KCDFAlert.ShowAlert(ex.Message);
                lblBill.InnerText = "Not Uploaded!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "ErrorLabel3();", true);
            }
        }
        protected void btnAudit_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKindFR = "Audited Accounts";
                var refNoIs = txtPrefNo.Text;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + @"\";
                string fileName = Path.GetFileName(FileUploadAudit.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadAudit.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (FileUploadAudit.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xls") || (ext == ".xlsx"))
                {
                    string filename = Grantees.No + "_" + fileName;
                    FileUploadAudit.SaveAs(uploadsFolder + filename);
                    //file path to read file
                    string filePath = uploadsFolder + filename;
                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKindFR, refNoIs, attachedDoc);
                    loadUploads();
                    lblAudit.InnerText = "Uploaded!";
                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext + "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

                }
                if (!FileUploadAudit.HasFile)
                {
                    KCDFAlert.ShowAlert("Select Document before uploading");
                    return;
                }

            }
            catch (Exception ex)
            {
                //KCDFAlert.ShowAlert("Unkown Error Occured!" + ex.Message);
                //KCDFAlert.ShowAlert(ex.Message);
                lblAudit.InnerText = "Not Uploaded!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "ErrorLabel7();", true);
            }
        }
        protected void CheckUserProfile()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "TesctEmpty();", true);
        }
        protected void testBtn_OnClick(object sender, EventArgs e)
        {
            string mystr = "";
            WSConfig.ObjNav.FnFetchNavPicture(Session["username"].ToString(), ref mystr);

            byte[] buffer = Convert.FromBase64String(mystr);
            FileStream file = File.Create("E:\\AdvancedPortals\\KCDF_P\\KCDF_P\\testCopy" + Session["username"] + ".png");
            file.Write(buffer, 0, buffer.Length);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "alert('written');", true);
            file.Close();
        }

        protected void btnGoNext1_OnClick(object sender, EventArgs e)
        {
          orgPMultiview.SetActiveView(viewUploads);
        }

        protected void btnNext2_OnClick(object sender, EventArgs e)
        {
            orgPMultiview.SetActiveView(grantsMgt);
        }

        protected void btnNext3_OnClick(object sender, EventArgs e)
        {
            orgPMultiview.SetActiveView(Projectoverview);
        }

        protected void btnNext4_OnClick(object sender, EventArgs e)
        {
            orgPMultiview.SetActiveView(targetGroup);
        }

        protected void btnNext5_OnClick(object sender, EventArgs e)
        {
            orgPMultiview.SetActiveView(uploadDocs);
        }

        protected void btnNext6_OnClick(object sender, EventArgs e)
        {
          orgPMultiview.SetActiveView(finalSubmit);
        }
    }
    }
