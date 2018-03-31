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
using System.Web.UI.WebControls;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;

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
                if (Session["username"] == null)
                {
                    Response.Redirect("/Default.aspx");

                }
                loadGrantsHistory();
                loadUploads();
                getProjects();
            }

        }
   

      

       

        protected void studentInfoMenu_OnMenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            orgPMultiview.ActiveViewIndex = index;
        }

      
        protected void btnSaveGrantHisto_OnClick(object sender, EventArgs e)
        {
            string usnme = Session["username"].ToString();
            string donorname = txtDonor.Text.Trim();
            decimal amtprovided = Convert.ToDecimal(txtAmount.Text);
            var yoAwrd = yrofAward.Value.Trim();
            DateTime yearOfAward = DateTime.Parse(yoAwrd);

            var yoFunding = yrofFunding.Value.Trim();
            DateTime yrOfFunds = DateTime.Parse(yoFunding);

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
        protected void loadPastDonoryTable()
        {
            //var grnhisto =
            //    nav.grant_history.ToList().Where(us => us.Organization_Username == Session["username"].ToString());
            //tblGrantsManager.AutoGenerateColumns = false;
            //tblGrantsManager.DataSource = grnhisto;
            //tblGrantsManager.DataBind();
       }

     

        protected void loadGrantsHistory()
        {
            var grnhisto =
                nav.grant_history.ToList().Where(us => us.Organization_Username == Session["username"].ToString());
            tblGrantsManager.AutoGenerateColumns = false;
            tblGrantsManager.DataSource = grnhisto;
            tblGrantsManager.DataBind();
        }

        protected void btnUpdatePOverview_OnClick(object sender, EventArgs e)
        {
            int projectlength = 0;
            var usn = Session["username"].ToString();
            string projTt = TextBoxtitle.Text.Trim();
            var pstartD = txtDateofStart.Value.Trim();
            DateTime projTDt = DateTime.Parse(pstartD);
            string county = ddlSelCountry.SelectedItem.Text;
            string constituency = ddlConstituency.SelectedItem.Text;
            string urbantarget = txtAreaTargetSettmnt.Text.Trim();
           
            string scale = ddlEstScale.SelectedItem.Text;
            decimal projCost = Convert.ToDecimal(TextBoxcost.Text);
            decimal contrib = Convert.ToDecimal(TextBoxcont.Text);
            decimal kcdffunds = Convert.ToDecimal(TextBoxrequested.Text);
            string projectNm = ddlAccountType.SelectedItem.Text;

            if (ddlMonths.SelectedIndex == 0)
            {
                KCDFAlert.ShowAlert("Select valid project duration!");
                return;
            }
            else
            {
               projectlength = Convert.ToInt32(ddlMonths.SelectedItem.Text);
            }

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            sup.FnProjectOverview(usn, county, constituency, projTt, urbantarget,
                projectlength, projCost, contrib, kcdffunds, projTDt, scale, projectNm);

            KCDFAlert.ShowAlert("Data saved Successfully!");
            TextBoxtitle.Text = "";
            txtDateofStart.Value = "";
            ddlSelCountry.SelectedIndex = 0;
            ddlConstituency.SelectedIndex = 0;
            txtAreaTargetSettmnt.Text = "";
            ddlMonths.SelectedIndex = 0;
            ddlEstScale.SelectedIndex = 0;
            TextBoxcost.Text = "";
            TextBoxcont.Text = "";
            TextBoxrequested.Text = "";
           
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
            string fullFPath = Request.PhysicalApplicationPath + "All Uploads\\" + Grantees.No + "\\" + filName;
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
            sup.FnAttachment(usNo, docType, fullFPath, filName, granttype, docKind, usaname, prjct);

        }

       
        protected void loadUploads()
        {
           
            try
            {
            //    string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploaded Documents/" + Grantees.No + "/"));
            //    List<ListItem> files = new List<ListItem>();
            //    foreach (string filePath in filePaths)
            //    {
            //        files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            //    }
            //    gridViewUploads.DataSource = files;
                var upsFiles = nav.myUploads.ToList().Where(un => un.Username == Session["username"].ToString());
                gridViewUploads.AutoGenerateColumns = false;
                gridViewUploads.DataSource = upsFiles;
                gridViewUploads.DataBind();
            }
            catch (Exception ex)
            {
               // KCDFAlert.ShowAlert("You have not uploaded documents yet!");
               KCDFAlert.ShowAlert(ex.Message);

            }

        }
        protected void UploadFile(object sender, EventArgs e)
        {
            try
            {
                var documentKind = "Proposal";
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
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") || (ext == ".doc") || (ext == ".xlsx"))
                {
                    string filename = Grantees.No + "_" + fileName;
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
                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + "\\";
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
                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + "\\";
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
                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + "\\";
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
                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Grantees.No + "\\";
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
          
            var del_id = gridViewUploads.DataKeys[e.RowIndex].Values[0].ToString();
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            sup.FnDeleteUpload(del_id);
            KCDFAlert.ShowAlert("Deleted Successfully!");
            loadUploads();
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
                string smth = e.Row.Cells[0].Text;
                foreach (Button button in e.Row.Cells[2].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete" + smth + "?')){return false};";
                    }
                }
            }
        }

        protected void btnSaveTarget_Click(object sender, EventArgs e)
        {
           
            string proj = ddlAccountType.SelectedItem.Text;
            var usnm = Session["username"].ToString();
            int hsehlds = Convert.ToInt32(TextBoxhse.Text);
            int schls = Convert.ToInt32(TextBoxschl.Text);
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

        }
        protected void btnValidateInfo_OnClick(object sender, EventArgs e)
        {
            try
            {
                var usNM = Session["username"].ToString();
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                bool myValid = sup.FnValidateSubmission(usNM);
                if (myValid)
                {
                    txtValidate.Text = "ALL UPLOADS AVAILABLE";
                    txtValidate.ForeColor = Color.GhostWhite;
                    txtValidate.BackColor = Color.ForestGreen;
                    btnFinalSubmit.Enabled = true;
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
           
        }
    }
}