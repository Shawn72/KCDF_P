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
    public partial class Report_Form : System.Web.UI.Page
    {
        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
                new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };

        public string UserNo;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "private";
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");

                }
                SwitchAllmyProjects();
                GetmeYears();
                LoadList();
                SetMySessionItemView();
                //loadUploads();

            }
        }

        protected void LoadList()
        {
            ddlQuarter.Items.Add("--Select Quarter--");
            ddlQuarter.Items.Add("Jan - March");
            ddlQuarter.Items.Add("April - June");
            ddlQuarter.Items.Add("July - Sept");
            ddlQuarter.Items.Add("Oct - Dec");
        }

        protected void ddlAccountType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selProj = ddlAccountType.SelectedItem.Text.Trim();
            int slVal = ddlAccountType.SelectedIndex;
            switch (slVal)
            {
                case 0:
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything",
                        "alert('Select valid project from dropdownlist!');", true);
                    txtPrefNo.Text = "";
                    break;
                default:
                    txtPrefNo.Text = ddlAccountType.SelectedValue;
                    //  loadUploads();
                    break;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "pageLoad();", true);
        }

        protected void SwitchAllmyProjects()
        {
            var getsession = Session["reportformUser"].ToString();
            switch (getsession)
            {
                case "iamGrantee":
                    MyGrantProjects();
                    UserNo =
                        nav.grantees_Register.ToList()
                            .Where(u => u.Organization_Username == Session["username"].ToString())
                            .Select(u => u.No)
                            .SingleOrDefault();
                    switchUserView.SetActiveView(amGrantee);
                    break;
                case "iamStudent":
                    MyScholarProjects();
                    UserNo =
                        nav.studentsRegister.ToList()
                            .Where(u => u.Username == Session["username"].ToString())
                            .Select(u => u.No)
                            .SingleOrDefault();
                    switchUserView.SetActiveView(amScholar);
                    break;
                case "iamConsult":
                    MyConsultProjects();
                    UserNo =
                        nav.myConsultants.ToList()
                            .Where(u => u.Organization_Username == Session["username"].ToString())
                            .Select(u => u.No)
                            .SingleOrDefault();
                    switchUserView.SetActiveView(amConsult);
                    break;

            }

        }

        protected void MyGrantProjects()
        {
            var mygrants = nav.projectOverview.ToList().Where(u => u.Username == Session["username"].ToString());
            ddlAccountType.Items.Clear();

            ddlAccountType.DataSource = mygrants;
            ddlAccountType.DataTextField = "Project_Name";
            ddlAccountType.DataValueField = "Call_Ref_Number";
            ddlAccountType.DataBind();
            ddlAccountType.Items.Insert(0, "--Select project--");

        }

        protected void MyScholarProjects()
        {
            var mygrants =
                nav.scholarshipApplications.ToList().Where(u => u.Student_Username == Session["username"].ToString());
            ddlAccountType.Items.Clear();

            ddlAccountType.DataSource = mygrants;
            ddlAccountType.DataTextField = "Scholarship_Title";
            ddlAccountType.DataValueField = "Call_Reference_Number";
            ddlAccountType.DataBind();
            ddlAccountType.Items.Insert(0, "--Select Scolarship--");

        }

        protected void MyConsultProjects()
        {
            var mygrants =
                nav.myConsultations.ToList().Where(u => u.Consultant_Username == Session["username"].ToString());
            ddlAccountType.Items.Clear();

            ddlAccountType.DataSource = mygrants;
            ddlAccountType.DataTextField = "Project_Name";
            ddlAccountType.DataValueField = "Project_RefNo";
            ddlAccountType.DataBind();
            ddlAccountType.Items.Insert(0, "--Select Project--");

        }

        protected void ddlYears_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["yearofAwd"] = ddlYears.SelectedItem.Text;
        }

        protected void GetmeYears()
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

        protected bool SaveAttachment(string usernumber, string filName, string extension, string docKind,
            string callRefNo, string attachedBlob)
        {
            var usaname = Session["username"].ToString();
            int granttype = 0;
            var userNumber = "";
            var myyearP = "";
            var myquarter = "";
            var prjct = "";

            if (ddlAccountType.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything",
                    "alert('Please Select Project first');", true);
                return false;
            }
            else
            {
                prjct = ddlAccountType.SelectedItem.Text;
            }

            if (ddlYears.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "alert('Please Choose Year');",
                    true);

                return false;
            }
            else
            {
                myyearP = ddlYears.SelectedItem.Text;
            }

            if (ddlQuarter.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything",
                    "alert('Please Choose Quarter');", true);
                return false;
            }
            else
            {
                myquarter = ddlQuarter.SelectedItem.Text;
            }

            var sessx = Session["reportformUser"].ToString();
            switch (sessx)
            {
                case "iamGrantee":
                    granttype = 0;
                    userNumber =
                        nav.grantees_Register.ToList()
                            .Where(u => u.Organization_Username == Session["username"].ToString())
                            .Select(u => u.No)
                            .SingleOrDefault();
                    break;
                case "iamStudent":
                    granttype = 1;
                    userNumber =
                        nav.studentsRegister.ToList()
                            .Where(u => u.Username == Session["username"].ToString())
                            .Select(u => u.No)
                            .SingleOrDefault();
                    break;
                case "iamConsult":
                    granttype = 2;
                    userNumber =
                        nav.myConsultants.ToList()
                            .Where(u => u.Organization_Username == Session["username"].ToString())
                            .Select(u => u.No)
                            .SingleOrDefault();
                    break;
            }


            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);

            string docType = "";
            if ((extension == ".jpg") || (extension == ".jpeg") || (extension == ".png"))
            {
                docType = "Picture";
            }
            else if ((extension == ".pdf"))
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
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            if (
                sup.FnAttachReportForms(userNumber, docType, filName, granttype, docKind, usaname, prjct, callRefNo,
                    attachedBlob, myyearP, myquarter) == true)
            {
                // KCDFAlert.ShowAlert("Document: " + filName + " uploaded and Saved successfully!");
                // loadUploads();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything",
                    "alert('Document: '" + filName + "'successfully uploaded!');", true);
                return true;
            }
            return false;

            //}
            //catch (Exception r)
            //{
            //    KCDFAlert.ShowAlert(r.Message);
            //}
        }


        protected void btnUploadNarrative_OnClick(object sender, EventArgs e)
        {
            //try
            //{
            const string documentKind = "Narrative";
            var refNoIs = txtPrefNo.Text;
            var usnn = "";

            var sessx = Session["reportformUser"].ToString();
            switch (sessx)
            {
                case "iamGrantee":
                    usnn = Grantees.No;
                    break;
                case "iamStudent":
                    usnn = Students.No;
                    break;
                case "iamConsult":
                    usnn = ConsultantClass.No;
                    break;
            }


            string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + usnn + @"\";
            string fileName = Path.GetFileName(FileUploadNarrative.PostedFile.FileName);
            string ext = Path.GetExtension(FileUploadNarrative.PostedFile.FileName);
            if (!Directory.Exists(uploadsFolder))
            {
                //if the folder doesnt exist create it
                Directory.CreateDirectory(uploadsFolder);
            }

            if (FileUploadNarrative.PostedFile.ContentLength > 5000000)
            {
                KCDFAlert.ShowAlert("Select a file less than 5MB!");
                return;
            }
            if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") ||
                (ext == ".doc") || (ext == ".xls") || (ext == ".xlsx"))
            {

                string filenames = usnn + "_" + fileName;
                FileUploadNarrative.SaveAs(uploadsFolder + filenames);

                //file path to read file
                string filePath = uploadsFolder + filenames;
                FileStream file = File.OpenRead(filePath);
                byte[] buffer = new byte[file.Length];
                file.Read(buffer, 0, buffer.Length);
                file.Close();
                string attachedDoc = Convert.ToBase64String(buffer);

                if (SaveAttachment(UserNo, filenames, ext, documentKind, refNoIs, attachedDoc) == true)
                {
                    KCDFAlert.ShowAlert("Document uploaded!");
                    lblNarr.InnerText = "Uploaded!";
                    Session.Remove("typeoftask");
                }
                else
                {
                    KCDFAlert.ShowAlert("not uploaded!");
                    lblNarr.InnerText = "Not Uploaded!";
                }

                //KCDFAlert.ShowAlert(filenames);
                // loadUploads();
            }
            else
            {
                KCDFAlert.ShowAlert("File Format is : " + ext +
                                    "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

            }
            if (!FileUploadNarrative.HasFile)
            {
                KCDFAlert.ShowAlert("Select Document before uploading");
                return;
            }

            //}
            //catch (Exception ex)
            //{
            //    KCDFAlert.ShowAlert("Unkown Error Occured!");
            //    // KCDFAlert.ShowAlert(ex.Message);
            //}
        }

        protected void btnUploadFinancial_OnClick(object sender, EventArgs e)
        {
            //try
            //{
            const string documentKind = "Financial";
            var refNoIs = txtPrefNo.Text;
            var usnn = "";

            var sessx = Session["reportformUser"].ToString();
            switch (sessx)
            {
                case "iamGrantee":
                    usnn = Grantees.No;
                    break;
                case "iamStudent":
                    usnn = Students.No;
                    break;
                case "iamConsult":
                    usnn = ConsultantClass.No;
                    break;
            }


            string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + usnn + @"\";
            string fileName = Path.GetFileName(FileUploadFinancial.PostedFile.FileName);
            string ext = Path.GetExtension(FileUploadFinancial.PostedFile.FileName);
            if (!Directory.Exists(uploadsFolder))
            {
                //if the folder doesnt exist create it
                Directory.CreateDirectory(uploadsFolder);
            }

            if (FileUploadFinancial.PostedFile.ContentLength > 5000000)
            {
                KCDFAlert.ShowAlert("Select a file less than 5MB!");
                return;
            }
            if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") ||
                (ext == ".doc") || (ext == ".xls") || (ext == ".xlsx"))
            {

                string filenames = usnn + "_" + fileName;
                FileUploadFinancial.SaveAs(uploadsFolder + filenames);

                //file path to read file
                string filePath = uploadsFolder + filenames;
                FileStream file = File.OpenRead(filePath);
                byte[] buffer = new byte[file.Length];
                file.Read(buffer, 0, buffer.Length);
                file.Close();
                string attachedDoc = Convert.ToBase64String(buffer);

                if (SaveAttachment(UserNo, filenames, ext, documentKind, refNoIs, attachedDoc) == true)
                {
                    KCDFAlert.ShowAlert("Document uploaaded!");
                    lblFin.InnerText = "Uploaded!";
                    Session.Remove("typeoftask");
                }
                else
                {
                    lblFin.InnerText = "Not Uploaded!";
                    KCDFAlert.ShowAlert("Not uploaaded!");
                }

            }
            else
            {
                KCDFAlert.ShowAlert("File Format is : " + ext +
                                    "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

            }
            if (!FileUploadFinancial.HasFile)
            {
                KCDFAlert.ShowAlert("Select Document before uploading");
                return;
            }

            //}
            //catch (Exception ex)
            //{
            //    KCDFAlert.ShowAlert("Unkown Error Occured!");
            //    // KCDFAlert.ShowAlert(ex.Message);
            //}
        }

        protected void btnUploadData_OnClick(object sender, EventArgs e)
        {
            //try
            //{
            const string documentKind = "Data";
            var refNoIs = txtPrefNo.Text;
            var usnn = "";

            var sessx = Session["reportformUser"].ToString();
            switch (sessx)
            {
                case "iamGrantee":
                    usnn = Grantees.No;
                    break;
                case "iamStudent":
                    usnn = Students.No;
                    break;
                case "iamConsult":
                    usnn = ConsultantClass.No;
                    break;
            }


            string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + usnn + @"\";
            string fileName = Path.GetFileName(FileUploadData.PostedFile.FileName);
            string ext = Path.GetExtension(FileUploadData.PostedFile.FileName);
            if (!Directory.Exists(uploadsFolder))
            {
                //if the folder doesnt exist create it
                Directory.CreateDirectory(uploadsFolder);
            }

            if (FileUploadData.PostedFile.ContentLength > 5000000)
            {
                KCDFAlert.ShowAlert("Select a file less than 5MB!");
                return;
            }
            if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") ||
                (ext == ".doc") || (ext == ".xls") || (ext == ".xlsx"))
            {

                string filenames = usnn + "_" + fileName;
                FileUploadData.SaveAs(uploadsFolder + filenames);

                //file path to read file
                string filePath = uploadsFolder + filenames;
                FileStream file = File.OpenRead(filePath);
                byte[] buffer = new byte[file.Length];
                file.Read(buffer, 0, buffer.Length);
                file.Close();
                string attachedDoc = Convert.ToBase64String(buffer);

                if (SaveAttachment(UserNo, filenames, ext, documentKind, refNoIs, attachedDoc) == true)
                {
                    KCDFAlert.ShowAlert("File Uploaded!");
                    lblData.InnerText = "Uploaded!";
                    Session.Remove("typeoftask");
                }
                else
                {
                    lblData.InnerText = "Not Uploaded!";
                    KCDFAlert.ShowAlert("Not Uploaded!");
                }
            }
            else
            {
                KCDFAlert.ShowAlert("File Format is : " + ext +
                                    "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

            }
            if (!FileUploadData.HasFile)
            {
                KCDFAlert.ShowAlert("Select Document before uploading");
                return;
            }

            //}
            //catch (Exception ex)
            //{
            //    KCDFAlert.ShowAlert("Unkown Error Occured!");
            //    // KCDFAlert.ShowAlert(ex.Message);
            //}
        }

        protected void btnUploadComm_OnClick(object sender, EventArgs e)
        {
            //try
            //{
            const string documentKind = "Community Report";
            var refNoIs = txtPrefNo.Text;
            var usnn = "";

            var sessx = Session["reportformUser"].ToString();
            switch (sessx)
            {
                case "iamGrantee":
                    usnn = Grantees.No;
                    break;
                case "iamStudent":
                    usnn = Students.No;
                    break;
                case "iamConsult":
                    usnn = ConsultantClass.No;
                    break;
            }


            string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + usnn + @"\";
            string fileName = Path.GetFileName(FileUploadComm.PostedFile.FileName);
            string ext = Path.GetExtension(FileUploadComm.PostedFile.FileName);
            if (!Directory.Exists(uploadsFolder))
            {
                //if the folder doesnt exist create it
                Directory.CreateDirectory(uploadsFolder);
            }

            if (FileUploadComm.PostedFile.ContentLength > 5000000)
            {
                KCDFAlert.ShowAlert("Select a file less than 5MB!");
                return;
            }
            if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") ||
                (ext == ".doc") || (ext == ".xls") || (ext == ".xlsx"))
            {

                string filenames = usnn + "_" + fileName;
                FileUploadComm.SaveAs(uploadsFolder + filenames);

                //file path to read file
                string filePath = uploadsFolder + filenames;
                FileStream file = File.OpenRead(filePath);
                byte[] buffer = new byte[file.Length];
                file.Read(buffer, 0, buffer.Length);
                file.Close();
                string attachedDoc = Convert.ToBase64String(buffer);

                SaveAttachment(UserNo, filenames, ext, documentKind, refNoIs, attachedDoc);
                appComm.InnerText = "Uploaded!";

                // loadUploads();
            }
            else
            {
                KCDFAlert.ShowAlert("File Format is : " + ext +
                                    "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

            }
            if (!FileUploadComm.HasFile)
            {
                KCDFAlert.ShowAlert("Select Document before uploading");
                return;
            }

            //}
            //catch (Exception ex)
            //{
            //    KCDFAlert.ShowAlert("Unkown Error Occured!");
            //    // KCDFAlert.ShowAlert(ex.Message);
            //}
        }

        protected void btnScholReport_OnClick(object sender, EventArgs e)
        {
            //try
            //{
            const string documentKind = "Scholarship Report";
            var refNoIs = txtPrefNo.Text;
            var usnn = "";

            var sessx = Session["reportformUser"].ToString();
            switch (sessx)
            {
                case "iamGrantee":
                    usnn = Grantees.No;
                    break;
                case "iamStudent":
                    usnn = Students.No;
                    break;
                case "iamConsult":
                    usnn = ConsultantClass.No;
                    break;
            }


            string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + usnn + @"\";
            string fileName = Path.GetFileName(FileUploadSchRepo.PostedFile.FileName);
            string ext = Path.GetExtension(FileUploadSchRepo.PostedFile.FileName);
            if (!Directory.Exists(uploadsFolder))
            {
                //if the folder doesnt exist create it
                Directory.CreateDirectory(uploadsFolder);
            }

            if (FileUploadSchRepo.PostedFile.ContentLength > 5000000)
            {
                KCDFAlert.ShowAlert("Select a file less than 5MB!");
                return;
            }
            if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") ||
                (ext == ".doc") || (ext == ".xls") || (ext == ".xlsx"))
            {

                string filenames = usnn + "_" + fileName;
                FileUploadSchRepo.SaveAs(uploadsFolder + filenames);

                //file path to read file
                string filePath = uploadsFolder + filenames;
                FileStream file = File.OpenRead(filePath);
                byte[] buffer = new byte[file.Length];
                file.Read(buffer, 0, buffer.Length);
                file.Close();
                string attachedDoc = Convert.ToBase64String(buffer);

                SaveAttachment(UserNo, filenames, ext, documentKind, refNoIs, attachedDoc);
                lblSchRepo.InnerText = "Uploaded!";

                // loadUploads();
            }
            else
            {
                KCDFAlert.ShowAlert("File Format is : " + ext +
                                    "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

            }
            if (!FileUploadSchRepo.HasFile)
            {
                KCDFAlert.ShowAlert("Select Document before uploading");
                return;
            }

            //}
            //catch (Exception ex)
            //{
            //    KCDFAlert.ShowAlert("Unkown Error Occured!");
            //    // KCDFAlert.ShowAlert(ex.Message);
            //}
        }

        protected void btnConsulRepo_OnClick(object sender, EventArgs e)
        {
            //try
            //{
            var documentKind = "Consultancy Report";
            var refNoIs = txtPrefNo.Text;
            var usnn = "";

            var sessx = Session["reportformUser"].ToString();
            switch (sessx)
            {
                case "iamGrantee":
                    usnn = Grantees.No;
                    break;
                case "iamStudent":
                    usnn = Students.No;
                    break;
                case "iamConsult":
                    usnn = ConsultantClass.No;
                    break;
            }


            string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + usnn + @"\";
            string fileName = Path.GetFileName(FileUploadConsulRepo.PostedFile.FileName);
            string ext = Path.GetExtension(FileUploadConsulRepo.PostedFile.FileName);
            if (!Directory.Exists(uploadsFolder))
            {
                //if the folder doesnt exist create it
                Directory.CreateDirectory(uploadsFolder);
            }

            if (FileUploadConsulRepo.PostedFile.ContentLength > 5000000)
            {
                KCDFAlert.ShowAlert("Select a file less than 5MB!");
                return;
            }
            if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") ||
                (ext == ".doc") || (ext == ".xls") || (ext == ".xlsx"))
            {

                string filenames = usnn + "_" + fileName;
                FileUploadConsulRepo.SaveAs(uploadsFolder + filenames);

                //file path to read file
                string filePath = uploadsFolder + filenames;
                FileStream file = File.OpenRead(filePath);
                byte[] buffer = new byte[file.Length];
                file.Read(buffer, 0, buffer.Length);
                file.Close();
                string attachedDoc = Convert.ToBase64String(buffer);

                SaveAttachment(UserNo, filenames, ext, documentKind, refNoIs, attachedDoc);
                lblCons.InnerText = "Uploaded!";

                // loadUploads();
            }
            else
            {
                KCDFAlert.ShowAlert("File Format is : " + ext +
                                    "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

            }
            if (!FileUploadConsulRepo.HasFile)
            {
                KCDFAlert.ShowAlert("Select Document before uploading");
                return;
            }

            //}
            //catch (Exception ex)
            //{
            //    KCDFAlert.ShowAlert("Unkown Error Occured!");
            //    // KCDFAlert.ShowAlert(ex.Message);
            //}
        }

        protected void SetMySessionItemView()
        {
            var typeOpt = Session["typeoftask"].ToString();
            switch (typeOpt)
            {
                case "Narrative":
                    myNarrative.Visible = true;
                    break;

                case "Financial":
                    myFinancial.Visible = true;
                    break;

                case "Data":
                    myData.Visible = true;
                    break;

                case "Indicator Matrix":

                    break;

                case "POCA Tool":

                    break;

                case "Other":

                    break;
            }
        }
    }
}