using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Security.AccessControl;
using System.Security.Principal;
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
            if (!IsPostBack)
            {
                checkSessionExists();
                loadGrantsHistory();
                loadUploads();
                getProjects();
                loadIncompleteApplication();
                myCountyIs();
                loadGranteeUploads();
                loadApplicationInfo();
            }

        }
        protected void checkSessionExists()
        {
            var sessionIs = Session["username"];
            if (sessionIs == null)
            {
                Response.Redirect("/Default.aspx");
            }
        }


        protected void studentInfoMenu_OnMenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            orgPMultiview.ActiveViewIndex = index;
        }

      
        protected void btnSaveGrantHisto_OnClick(object sender, EventArgs e)
        {
            DateTime yearOfAward, yrOfFunds;
            string usnme = Session["username"].ToString();
            string donorname = txtDonor.Text.Trim();
            decimal amtprovided = Convert.ToDecimal(txtAmount.Text);

            var yoAwrd = yrofAward.Value.Trim();
            if (string.IsNullOrWhiteSpace(yoAwrd))
            {
                KCDFAlert.ShowAlert("Select a valid date!");
                yrofAward.Focus();
                return;
            }
            else
            {
                yearOfAward = DateTime.Parse(yoAwrd);
            }

            var yoFunding = yrofFunding.Value.Trim();
            if (string.IsNullOrWhiteSpace(yoFunding))
            {
                KCDFAlert.ShowAlert("Select a valid date!");
                yrofFunding.Focus();
                return;
            }
            else
            {
                yrOfFunds = DateTime.Parse(yoFunding);
            }

            string proj_name = ddlAccountType.SelectedItem.Text;
            string objGrnt = TextObj.Text.Trim();
            string trgtBenef = TextTypeBeneficiary.Text.Trim();
            int noOfBs = Convert.ToInt32(TextNoBeneficiary.Text);
            decimal grantAmnt = Convert.ToDecimal(TextAmount.Text);
            string interSupported = TextIntspprt.Text.Trim();
            string prjStatus = ddlPrjStatus.SelectedItem.Text;
            string tcountytrimmed  = txtAreaCounties.Text.Substring(0, txtAreaCounties.Text.Length - 1);

            if (ddltargetCounty.SelectedIndex==0)
            {
                KCDFAlert.ShowAlert("Please select target counties");
                return;
            }
            if (ddlPrjStatus.SelectedIndex == 0)
            {
                KCDFAlert.ShowAlert("Please select a valid project status");
                return;
            }

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;

            sup.FnGrantManager(usnme, donorname, amtprovided, yearOfAward, objGrnt, tcountytrimmed, trgtBenef, noOfBs, yrOfFunds, grantAmnt, interSupported, prjStatus, proj_name);
            KCDFAlert.ShowAlert("Data Saved Successfully!!");
            txtDonor.Text = "";
            txtAmount.Text = "";
            yrofAward.Value = "";
            yrofFunding.Value = "";
            TextObj.Text = "";
            ddltargetCounty.SelectedIndex = 0;
            TextTypeBeneficiary.Text = "";
            TextNoBeneficiary.Text = "";
            TextIntspprt.Text = "";
            ddlPrjStatus.SelectedIndex = 0;
            txtAreaCounties.Text = "";
            loadGrantsHistory();

        }
        protected void loadGrantsHistory()
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
            }
            catch (Exception ex)
            {
               // KCDFAlert.ShowAlert("No projects yet!");
                tblGrantsManager.EmptyDataText = "No grants management data found";
            }
           
        }

        protected void btnUpdatePOverview_OnClick(object sender, EventArgs e)
        {
            int projectlength = 0;
            DateTime projTDt;
            decimal projCost = 0;
            decimal contrib = 0;
            decimal kcdffunds = 0;
            var usn = Session["username"].ToString();

            string projTt = TextBoxtitle.Text.Trim();
            if (string.IsNullOrWhiteSpace(TextBoxtitle.Text))
            {
                KCDFAlert.ShowAlert("Fill provide the title!");
                TextBoxtitle.BorderColor = Color.OrangeRed;
                TextBoxtitle.Focus();
                return;
            }
            var pstartD = txtDateofStart.Value.Trim();
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

            try
            {
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            sup.FnProjectOverview(usn, county, constituency, projTt, urbantarget,
                projectlength, projCost, contrib, kcdffunds, projTDt, scale, projectNm);

            KCDFAlert.ShowAlert("Data saved Successfully!, KCDF Requested Amount : "+ kcdffunds);

            TextBoxtitle.Text = "";
            txtDateofStart.Value = "";
            ddlSelCounty.SelectedIndex = 0;
            ddlConstituency.SelectedIndex = 0;
            txtAreaTargetSettmnt.Text = "";
            ddlMonths.SelectedIndex = 0;
            ddlEstScale.SelectedIndex = 0;
            TextBoxcost.Text = "";
            TextBoxcont.Text = "";
            TextBoxrequested.Text = "";
            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert(ex.Message);
            }

        }
       
        protected void CopyFilesToDir()
        {
            //string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + @"\";
            //string destPath = @"\\192.168.0.250\All Uploads\";

            //foreach (string dirPath in Directory.GetDirectories(uploadsFolder, " * ",
            //  SearchOption.AllDirectories))
            //    Directory.CreateDirectory(dirPath.Replace(uploadsFolder, destPath));

            ////Copy all the files & Replaces any files with the same name
            //foreach (string newPath in Directory.GetFiles(uploadsFolder, "*.*",
            //    SearchOption.AllDirectories))
            //    File.Copy(newPath, newPath.Replace(uploadsFolder, destPath), true);
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

        protected void saveAttachment(string filName, string extension, string docKind)
        {
            var usNo = nav.grantees_Register.ToList().Where(usr => usr.Organization_Username == Session["username"].ToString()).Select(nu => nu.No).SingleOrDefault();
            var usaname = Session["username"].ToString();
            var prjct = ddlAccountType.SelectedItem.Text;

            // string fullFPath = Request.PhysicalApplicationPath + "All Uploads\\" + Grantees.No + @"\" + filName;

            string navfilePath = @"\\192.168.0.249\All_Portal_Uploaded\" + filName;

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

            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            sup.FnAttachment(usNo, docType, navfilePath, filName, granttype, docKind, usaname, prjct);

        }

       
        private void loadUploads()
        {
            try
            {
                var userNM = Session["username"].ToString();
                var openP =
                    nav.projectOverview.ToList().Where(up => up.Username == userNM && (up.Submission_Status == "Pending Approval" || up.Submission_Status=="Incomplete")).Select(pn=>pn.No).SingleOrDefault();

                KCDFAlert.ShowAlert(openP);
                var upsFiles = nav.myUploads.ToList().Where(un => un.Username == userNM && un.Grant_No==openP);
                gridViewUploads.AutoGenerateColumns = false;
                gridViewUploads.DataSource = upsFiles;
                gridViewUploads.DataBind();

                gridViewUploads.UseAccessibleHeader = true;
                gridViewUploads.HeaderRow.TableSection = TableRowSection.TableHeader;
                TableCellCollection cells = gridViewUploads.HeaderRow.Cells;
                cells[0].Attributes.Add("data-class", "expand");
                cells[2].Attributes.Add("data-hide", "phone,tablet");
                cells[3].Attributes.Add("data-hide", "phone,tablet");
                cells[4].Attributes.Add("data-hide", "phone, tablet");
            }
            catch (Exception ex)
            {
                // KCDFAlert.ShowAlert("You have not uploaded documents yet!");
                //KCDFAlert.ShowAlert(ex.Message);
                gridViewUploads.EmptyDataText = "No Uploads found!";

            }

        }
        protected void UploadFile(object sender, EventArgs e)
        {
            //+ Grantees.No + "\\"
            try
            {
                var documentKind = "Proposal";
                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + @"\";
                string fileName = Path.GetFileName(FileUpload.PostedFile.FileName);
                string ext = Path.GetExtension(FileUpload.PostedFile.FileName);
                //if (!Directory.Exists(uploadsFolder))
                //{
                //    //if the folder doesnt exist create it
                //    Directory.CreateDirectory(uploadsFolder);
                //}

                if (FileUpload.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xlsx"))
                {

                string filename = Grantees.No + "_" + fileName;
                //DirectoryInfo dInfo = new DirectoryInfo(uploadsFolder);
                //DirectorySecurity dSecurity = dInfo.GetAccessControl();
                //dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), 
                //    FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, 
                //    PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                //dInfo.SetAccessControl(dSecurity);

                FileUpload.SaveAs(uploadsFolder + filename);
                CopyFilesToDir();
                saveAttachment(filename, ext, documentKind);
                KCDFAlert.ShowAlert("Document: " + filename + " uploaded and Saved successfully!");
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
                // KCDFAlert.ShowAlert("Unkown Error Occured!");
                KCDFAlert.ShowAlert(ex.Message);
            }

        }
        
        protected void btnUploadID_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKind = "Registration Certificate";
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
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xlsx"))
                {
                    string filename = Grantees.No + "_" + fileName;
                    FileUploadID.SaveAs(uploadsFolder + filename);
                    CopyFilesToDir();
                    saveAttachment(filename, ext, documentKind);
                    KCDFAlert.ShowAlert("Document: " + filename + " uploaded and Saved successfully!");
                    loadUploads();
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
                KCDFAlert.ShowAlert(ex.Message);
            }
        }

        protected void btnUploadConstitution_OnClick(object sender, EventArgs e)
        {

            try
            {
                var documentKind = "Organizational Constitution ";
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
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xlsx"))
                {
                    string filename = Grantees.No + "_" + fileName;
                    FileUploadConst.SaveAs(uploadsFolder + filename);
                    CopyFilesToDir();
                    saveAttachment(filename, ext, documentKind);
                    KCDFAlert.ShowAlert("Document: " + filename + " uploaded and Saved successfully!");
                    loadUploads();
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
                // KCDFAlert.ShowAlert("Unkown Error Occured!");
                KCDFAlert.ShowAlert(ex.Message);
            }
        }

        protected void btnUploadList_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKindML = "Members List";
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
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xlsx"))
                {
                    string filename = Grantees.No + "_" + fileName;
                    FileUploadList.SaveAs(uploadsFolder + filename);
                    CopyFilesToDir();
                    saveAttachment(filename, ext, documentKindML);
                    KCDFAlert.ShowAlert("Document: " + filename + " uploaded and Saved successfully!");
                    loadUploads();
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
                // KCDFAlert.ShowAlert("Unkown Error Occured!");
                KCDFAlert.ShowAlert(ex.Message);
            }

        }

        protected void btnFinReport_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKindFR = "Financial Report ";
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
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xlsx"))
                {
                    string filename = Grantees.No + "_" + fileName;
                    FileUploadFinRePo.SaveAs(uploadsFolder + filename);
                    CopyFilesToDir();
                    saveAttachment(filename, ext, documentKindFR);
                    KCDFAlert.ShowAlert("Document: " + filename + " uploaded and Saved successfully!");
                    loadUploads();
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
                // KCDFAlert.ShowAlert("Unkown Error Occured!");
                KCDFAlert.ShowAlert(ex.Message);
            }

        }

       protected void getProjects()
       {
            var projs = nav.call_for_Proposal.ToList().Where(pty=>pty.Proposal_Type=="Grant");
            ddlAccountType.DataSource = projs;
            ddlAccountType.DataTextField = "Project";
            ddlAccountType.DataValueField = "Project";
            ddlAccountType.DataBind();
            ddlAccountType.Items.Insert(0, "--Select Project--");
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
                var uploadsGrantNo = nav.myUploads.ToList().Where(rU => rU.Id == Session["delMeID"].ToString() && rU.Username == Session["username"].ToString()).Select(gN => gN.Grant_No).SingleOrDefault();
                sup.FnChangeSubmitStatus(uploadsGrantNo);

                sup.FnDeleteUpload(Session["delMeID"].ToString());
                KCDFAlert.ShowAlert("Deleted Successfully!" + uploadsGrantNo + " &&" + del_id);
                loadUploads();
            }
           catch (Exception ex)
           {
           KCDFAlert.ShowAlert(ex.Message);
           }
            
        }


        protected void gridViewUploads_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string smth = e.Row.Cells[0].Text;
                foreach (Button button in e.Row.Cells[2].Controls.OfType<Button>())
                {
                    if (button.CommandName=="Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete" + smth + "?')){return false};";
                    }  
                }
            }
        }

        protected void ddltargetCounty_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            txtAreaCounties.Visible = true;
            string countyW = ddltargetCounty.SelectedItem.Text.Trim();
            AppendCounties(countyW);
            //string countiesminus = txtAreaCounties.Text;
            //KCDFAlert.ShowAlert(countiesminus.Remove(countiesminus.Length-1));
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
            loadGrantsHistory();
            KCDFAlert.ShowAlert("Grant Deleted Successfully!");
            
        }

        protected void tblGrantsManager_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[0].Text;
                foreach (Button button in e.Row.Cells[2].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                    }
                }
            }
        }

        protected void btnSaveTarget_Click(object sender, EventArgs e)
        {
            try
            {
            string proj = ddlAccountType.SelectedItem.Text;
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
            sup.FnSaveToGargetGroup(usnm, hsehlds, schls, org, yth, wmn, mn, chldold, old, ren, orph, ill, marg, drg,
                sxwrkr, tchr, frmr, proj);
            KCDFAlert.ShowAlert("Target group information saved successfully!");
            TextBoxhse.Text = "";
            TextBoxschl.Text = "";
            TextBoxorg.Text = "";
            TextBoxyth.Text = "";
            TextBowmn.Text = "";
            TextBowmn.Text = "";
            TextBoxmn.Text = "";
            TextBcldold.Text = "";
            TextBoxold.Text = "";
            TextBoxren.Text = "";
            TextBoxorph.Text = "";
            TextBoxill.Text = "";
            TextBoxmarg.Text = "";
            TextBoxdrg.Text = "";
            TextBoxsxwrkr.Text = "";
            TextBoxfarmr.Text = "";
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
                    KCDFAlert.ShowAlert("All Uploads available, you can submit your application");
                    txtValidate.Text = "ALL UPLOADS AVAILABLE";
                    txtValidate.ForeColor = Color.GhostWhite;
                    txtValidate.BackColor = Color.ForestGreen;
                    btnFinalSubmit.Enabled = true;
                    btnValidateInfo.Enabled = false;

                    break;

                    case false:
                    KCDFAlert.ShowAlert("No uploads yet!, you cannot submit anything");
                    txtValidate.Text = "PLEASE COMPLETE THE APPLICATION FIRST";
                    txtValidate.BackColor = Color.Red;
                    txtValidate.ForeColor = Color.GhostWhite;
                    btnFinalSubmit.Enabled = false;
                    sup.FnChangeSubmitStatus(tobevalidated);
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

        protected void checkApprovalStat(string projName)
        {
            var usnm = Session["username"].ToString();
            var chckIt = nav.projectOverview.ToList().Where(pu => pu.Username == usnm);
            var appStat = chckIt.ToList().Where(pns => pns.Project_Name == projName).Select(st => st.Approval_Status).ToString();

            switch (appStat)
            {
                case "Pending Approval":
                break;

                case "Approved":

                    break;
            }
        }

        protected void lnkEditMe_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSubmit();", true);
            Session["edit_id"] = (sender as LinkButton).CommandArgument;
            var validMe = Session["edit_id"].ToString();
            lblProjNb.Text = validMe;

        }

        private void loadIncompleteApplication()
        {
            try
            {
                var usn = Session["username"].ToString();
                var inComp = nav.projectOverview.ToList()
                    .Where(us => us.Username == usn && Convert.ToString(us.Submission_Status) == "Incomplete");
                gridSubmitApps.AutoGenerateColumns = false;
                gridSubmitApps.DataSource = inComp;
                gridSubmitApps.DataBind();

                gridSubmitApps.UseAccessibleHeader = true;
                gridSubmitApps.HeaderRow.TableSection = TableRowSection.TableHeader;
                TableCellCollection cells = gridSubmitApps.HeaderRow.Cells;
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
                gridSubmitApps.EmptyDataText = "No Application data found";
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
                    KCDFAlert.ShowAlert(sbCntysplit00);
                    break;
            }

        }

        protected void ddlAccountType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int slVal = ddlAccountType.SelectedIndex;
            switch (slVal)
            {
              case 0:
                    KCDFAlert.ShowAlert("Select valid project from dropdownlist");
                    btnUpdatePOverview.Enabled = false;
                    break;
            default:
                    btnUpdatePOverview.Enabled = true;
                    break;
            }
        }

        protected void btnRefreshScript_OnClick(object sender, EventArgs e)
        {
            TextBoxcost.Text = "";
            TextBoxcont.Text = "";
            TextBoxrequested.Text = "";
        }

      
        protected void rdoBtnListYesNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["yesOrno"] = rdoBtnListYesNo.SelectedIndex;
            int selindex = Convert.ToInt32(Session["yesOrno"].ToString());switch (selindex)
            {
                case 0:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "IfYesIts()", true);
                    //KCDFAlert.ShowAlert("clicked: "+ selindex);
                    break;
                default:
                    KCDFAlert.ShowAlert("Non KCDF grant!");
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
        }
        protected void AppendObjectives(string str2AppendObj)
        {
            txtAreaCounties.Text = txtAreaObjs.Text + str2AppendObj + ", ";
        }
        protected void btnSaveObjectives_OnClick(object sender, EventArgs e)
        {
            var txtArObjs = txtAreaObjs.Text.TrimEnd('?', '.', ',');
            KCDFAlert.ShowAlert(txtArObjs);
            //save objectives
            switch (txtArObjs)
            {
                case null:
                    KCDFAlert.ShowAlert("Please select Objectives!");
                    break;
                default:
                    //save in naVision
                    break;
            }
        }
        protected void loadApplicationInfo()
        {
            var granteeInfo =
                nav.grantees_Register.ToList()
                    .Where(n => n.Organization_Username.Equals(Session["username"].ToString()));
            TextBxcont.Text = granteeInfo.Select(co => co.Contact_Person).SingleOrDefault();
            TextBoposition.Text = granteeInfo.Select(po => po.Current_Position).SingleOrDefault();
            TextBxpostal.Text = granteeInfo.Select(pa => pa.Postal_Address).SingleOrDefault();
            txtPostalCode.Text = granteeInfo.Select(pc => pc.Postal_Code).SingleOrDefault();
            txtPostalTown.Text = granteeInfo.Select(ta => ta.Town).SingleOrDefault();
            TextBoxphone.Text = granteeInfo.Select(pn => pn.Phone).SingleOrDefault();
            txEmailAdd.Text = granteeInfo.Select(em => em.Email).SingleOrDefault();
            TextBoxweb.Text = granteeInfo.Select(wb => wb.Website).SingleOrDefault();;
            var ngO = granteeInfo.Select(ot => ot.NGO).SingleOrDefault();
            switch (ngO)
            {
                case true:
                    txtNGO.Text = "Non Governmental Organization";
                    break;
                case false:
                    txtNGO.Text = "Governmental Organization";
                    break;
                default:
                    txtNGO.Text = "Incomplete Information";
                    break;
            }
            var proftb = granteeInfo.Select(pft => pft.Profitable).SingleOrDefault();
            switch (proftb)
            {
                case true:
                    txtNonProfit.Text = "Profitable Organization";
                    break;
                case false:
                    txtNonProfit.Text = "Non-Profitable Organization";
                    break;
                default:
                    txtNonProfit.Text = "Incomplete Information";
                    break;
            }
            txtTypeofOrg.Text = granteeInfo.Select(rt => rt.Type_Of_Organization).SingleOrDefault();
           // var doR = granteeInfo.Select(dr => Convert.ToDateTime(dr.Date_Registered)).SingleOrDefault();
            txtRegNo.Text = granteeInfo.Select(rg => rg.Registration_No).SingleOrDefault();

        }
      }
    }
