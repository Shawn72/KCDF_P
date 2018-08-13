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
    public partial class Grant_Application : System.Web.UI.Page
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
                ReturnStudent();
                readData();
                loadRefs();
                readEducBgData();
                loadScholarData();
                loadProjectWorkplan();
                getScholarship();
                loadUploads();
                MyBankIs();
                MyUniCollege();
                MyCollegBankDs();
                MyPersonalBankDs();


                Wizard1.Visible = true;
                Wizard1.ActiveStepIndex = 0;

            }

        }
        protected void btnstart_Click(object sender, EventArgs e)
        {
            btnstart.Visible = false;
            Wizard1.Visible = true;
            Wizard1.ActiveStepIndex = 0;
        }
        protected void OnFinish(object sender, WizardNavigationEventArgs e)
        {
            WizardStepType t = Wizard1.WizardSteps[e.NextStepIndex].StepType;
            if (t == WizardStepType.Finish)
            {
                Wizard1.Visible = false;
            }
        }
        protected Students ReturnStudent()
        {

            return new Students(Session["username"].ToString());
        }

        protected void scholarshipDataCollection_OnMenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            profileMultiview.ActiveViewIndex = index;
        }

        protected void scholarMenu2_OnMenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            profileMultiview.ActiveViewIndex = index;

            if (index == 2)
            {
                profileMultiview.SetActiveView(workplanView);
            }
            if (index == 3)
            {
                profileMultiview.SetActiveView(AttachDocs);
            }
            if (index == 4)
            {
                profileMultiview.SetActiveView(bankDetails);
            }
        }

        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {

        }

        protected void tblRefs_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void loadRefs()
        {
            try
            {
                //var allmyrefs = nav.myReferees.ToList().Where(us => us.Username == Session["username"].ToString());
                //tblRefs.AutoGenerateColumns = false;
                //tblRefs.DataSource = allmyrefs;
                //tblRefs.DataBind();

            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert(ex.Message);
            }
        }

        protected void readData()
        {
            var studData = nav.studentsRegister.ToList().Where(r => r.Username == Session["username"].ToString());
            try
            {
                var ppNN = studData.Select(p => p.Phone_Number).SingleOrDefault();
                if (ppNN != null)
                {
                    var mobileBuilder = new StringBuilder(ppNN);
                    mobileBuilder.Remove(0, 4); //Trim four characters from position 1
                    mobileBuilder.Insert(0, "0"); // replace position +254 with 0
                    ppNN = mobileBuilder.ToString();
                    txtPhoneNo.Text = ppNN;
                }
            }
            catch (Exception ex)
            {
                txtPhoneNo.Text = "";
            }
            // txtPhoneNo.Text = studData.Select(pn => pn.Phone_Number).SingleOrDefault().ToString();
            txtfNname.Text = studData.Select(fn => fn.First_name).SingleOrDefault();
            txtMname.Text = studData.Select(mn => mn.Middle_name).SingleOrDefault();
            txtLname.Text = studData.Select(ln => ln.Last_name).SingleOrDefault();
            txtResidence.Text = studData.Select(r => r.Residence).SingleOrDefault();
            txtIDNo.Text = studData.Select(id => id.ID_No).SingleOrDefault();
            txtEmailAdd.Text = studData.Select(em => em.Email).SingleOrDefault();
            var dtB = studData.Select(dtoB => dtoB.Date_of_Birth).SingleOrDefault().ToString();
            DateTime dt1 = DateTime.Parse(dtB);
            dateofBirth.Text = dt1.ToShortDateString();
            var gent = studData.Select(g => g.Gender).SingleOrDefault();
            if (gent == "Male")
            {
                lstGender.SelectedIndex = 1;
            }
            else
            {
                lstGender.SelectedIndex = 2;
            }
        }

        protected void readEducBgData()
        {
            var edctnData = nav.studentsRegister.ToList().Where(r => r.Username == Session["username"].ToString());

            try
            {
                var ggNN = edctnData.Select(p => p.Parent_or_Guardian_Phone).SingleOrDefault();
                if (ggNN != null)
                {
                    var mobileBuilder = new StringBuilder(ggNN);
                    mobileBuilder.Remove(0, 4); //Trim four characters from position 1
                    mobileBuilder.Insert(0, "0"); // replace position +254 with 0
                    ggNN = mobileBuilder.ToString();
                    txtGuardianPhone.Text = ggNN;
                }
            }
            catch (Exception ex)
            {
                txtGuardianPhone.Text = "";
            }
            
            var unv = edctnData.Select(un => un.University_or_College).SingleOrDefault();
            var faclty = edctnData.Select(fc => fc.Course).SingleOrDefault();
            var yoS = edctnData.Select(yos => yos.Year_of_Study).Single();
            var yoAd = edctnData.Select(yoa => Convert.ToDateTime(yoa.Year_of_Admission)).SingleOrDefault();
            var yoC = edctnData.Select(yoc => Convert.ToDateTime(yoc.Year_of_Completion)).SingleOrDefault();
            txtGuardianEmail.Text = edctnData.Select(em => em.Parent_or_Guardian_Email).SingleOrDefault();
            txtGuardianAddress.Text = edctnData.Select(ad => ad.Parent_or_Guardian_Address).SingleOrDefault();
            txtCollege.Text = unv;
            txtDegree.Text = faclty;
            txtYearofStudy.Text = yoS;
            txtAdmittedWhen.Text = yoAd.ToShortDateString();
            txtYearofCompltn.Text = yoC.ToShortDateString();
            txtIDNo.Text = edctnData.Select(iD => iD.ID_No).SingleOrDefault();
        }
        
        protected void btnAddSupport_OnClick(object sender, EventArgs e)
        {
            var studentNo =
                nav.studentsRegister.ToList()
                    .Where(sn => sn.Username == Session["username"].ToString())
                    .Select(n => n.No)
                    .SingleOrDefault();
            var usnm = Session["username"].ToString();
            string scholname = ddlScolarshipType.SelectedItem.Text;
            string descr = txtAreaItemDecription.Text.Trim();
            string yrofStdy = txtYearofStudie.SelectedItem.Text;
            string ranked = ddlRank.SelectedItem.Text;
            decimal costSupport = 0;
            if (!string.IsNullOrEmpty(txtCost.Text))
            {
                costSupport = Convert.ToDecimal(txtCost.Text);
            }
            else
            {
                KCDFAlert.ShowAlert("Please Fill in Valid Cost!");
                return;
            }
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals naws = new Portals();
            naws.Credentials = credentials;
            naws.PreAuthenticate = true;
            naws.FnAddScholasticSupport(descr, studentNo, yrofStdy, costSupport, ranked, usnm, scholname);
            KCDFAlert.ShowAlert("Scholarstic Support Data Saved Successfully!");
            txtAreaItemDecription.Text = "";
            txtYearofStudie.SelectedIndex = 0;
            txtCost.Text = "";
            ddlRank.SelectedIndex = 0;
            loadScholarData();
        }

        protected void loadScholarData()
        {
            var lodScolar = nav.Scholar_Support.ToList().Where(usn => usn.Username == Session["username"].ToString());
            grdViewScholarS.AutoGenerateColumns = false;
            grdViewScholarS.DataSource = lodScolar;
            grdViewScholarS.DataBind();
        }

        protected void getScholarship()
        {
            var projs = nav.call_for_Proposal.ToList().Where(pty => pty.Proposal_Type == "Scholarship");
            ddlScolarshipType.DataSource = projs;
            ddlScolarshipType.DataTextField = "Project";
            ddlScolarshipType.DataValueField = "Call_Ref_Number";
            ddlScolarshipType.DataBind();
            ddlScolarshipType.Items.Insert(0, "--Select Scholarship--");
        }

        protected void loadProjectWorkplan()
        {
            var prjItem = nav.workplanQ.ToList().Where(un => un.Username == Session["username"].ToString());
            tblWorkplan.AutoGenerateColumns = false;
            tblWorkplan.DataSource = prjItem;
            tblWorkplan.DataBind();
        }

        protected void btnAddWorkplan_OnClick(object sender, EventArgs e)
        {
            var userNm = Session["username"].ToString();
            var studnumber =
                nav.studentsRegister.ToList().Where(sN => sN.Username == userNm).Select(no => no.No).SingleOrDefault();
            string objtv = txtObj.Text.Trim();
            string actvty = txtActivity.Text.Trim();
            string actvitytarget = txtTargets.Text.Trim();
            string meanofVer = txtMeansofVer.Text.Trim();
            int timeFrme = 0;
            decimal amnt = 0;
            string scholname = ddlScolarshipType.SelectedItem.Text;
            if (!string.IsNullOrEmpty(txtTimeFrame.Text))
            {
                timeFrme = Int32.Parse(txtTimeFrame.Text);
            }
            else
            {
                KCDFAlert.ShowAlert("Please fill in valid Time frame");
                return;
            }
            if (!string.IsNullOrEmpty(txtAmount.Text))
            {
                amnt = Convert.ToDecimal(txtAmount.Text);

            }
            else
            {
                KCDFAlert.ShowAlert("Please Fill in Valid Amount!");
                return;
            }
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals naws = new Portals();
            naws.Credentials = credentials;
            naws.PreAuthenticate = true;
            naws.FnAddWorkplanItem(userNm, studnumber, objtv, actvty, actvitytarget, meanofVer, amnt, timeFrme,
                scholname);
            KCDFAlert.ShowAlert("Workplan Item Saved Successfully!");
            loadProjectWorkplan();
            txtObj.Text = "";
            txtActivity.Text = "";
            txtTargets.Text = "";
            txtTargets.Text = "";
            txtMeansofVer.Text = "";
            txtTimeFrame.Text = "";
            txtAmount.Text = "";
        }

        protected void grdViewScholarS_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var del_id = grdViewScholarS.DataKeys[e.RowIndex].Values[0].ToString();
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            sup.FnDelSchlSupport(del_id);
            loadScholarData();
            KCDFAlert.ShowAlert("Scholarship data deleted successfully");
        }

        protected void tblWorkplan_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var del_id = tblWorkplan.DataKeys[e.RowIndex].Values[0].ToString();
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            sup.FnDelWorkplan(del_id);
            loadProjectWorkplan();
            KCDFAlert.ShowAlert("Workplan deleted successfully");
        }

        protected void loadUploads()
        {
            try
            {
                string userNM = Session["username"].ToString();

                var openPrj = nav.scholarshipApplications.ToList()
                    .Where(up => up.Student_Username == userNM && up.Approval_Status == "Open")
                    .Select(pn => pn.No).SingleOrDefault();
                
                var upsFiles =
                    nav.myScholaruploads.ToList().Where(un => un.Username == userNM && un.Scholarship_No == openPrj);
                gridmyViewUploads.AutoGenerateColumns = false;
                gridmyViewUploads.DataSource = upsFiles;
                gridmyViewUploads.DataBind();
            }
            catch (Exception ex)
            {
                // KCDFAlert.ShowAlert("You have not uploaded documents yet!");
                KCDFAlert.ShowAlert(ex.Message);

            }

        }

        protected void saveAttachment(string filName, string extension, string docKind, string callRefNo,string AttachedBlob)
        {
            var usNo =
                nav.studentsRegister.ToList()
                    .Where(usr => usr.Username == Session["username"].ToString())
                    .Select(nu => nu.No)
                    .SingleOrDefault();
            var usaname = Session["username"].ToString();
            var prjct = ddlScolarshipType.SelectedItem.Text;

            string navfilePath = @"D:\All_Portal_Uploaded\" + filName;

            // string fullFPath = Request.PhysicalApplicationPath + Students.No + @"\" + filName;
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            int granttype = 0;
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

            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            if (
                sup.FnAttachements_Scholarship(usNo, docType, navfilePath, filName, granttype, docKind, usaname, prjct,
                    callRefNo, AttachedBlob) == true)
            {
                KCDFAlert.ShowAlert("Attached!");
            }
            else
            {
                KCDFAlert.ShowAlert("Not Attached, Error!");
            }

        }

        protected void CopyFilesToDir()
        {
            //string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Students.No + @"\";
            //string destPath = @"\\192.168.0.250\All Uploads\";

            //foreach (string dirPath in Directory.GetDirectories(uploadsFolder, " * ",
            //  SearchOption.AllDirectories))
            //    Directory.CreateDirectory(dirPath.Replace(uploadsFolder, destPath));

            ////Copy all the files & Replaces any files with the same name
            //foreach (string newPath in Directory.GetFiles(uploadsFolder, "*.*",
            //    SearchOption.AllDirectories))
            //    File.Copy(newPath, newPath.Replace(uploadsFolder, destPath), true);
        }

        protected void gridmyViewUploads_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            var del_id = gridmyViewUploads.DataKeys[e.RowIndex].Values[0].ToString();
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            sup.FnDeleteUpload(del_id);
            KCDFAlert.ShowAlert("Deleted Successfully!");
            loadUploads();
        }

        protected void btnUploadSDc_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKind = "Scholarship Form";
                var schlRefNo = ddlScolarshipType.SelectedValue;
                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Students.No + @"\";
                string fileName = Path.GetFileName(FileUploadSDoc.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadSDoc.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }
                if (FileUploadSDoc.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") ||
                    (ext == ".doc") || (ext == ".xlsx"))
                {

                    string filename = Students.No + "_" + fileName;
                    FileUploadSDoc.SaveAs(uploadsFolder + filename);

                    //file path to read file
                    string filePath = uploadsFolder + filename;
                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKind, schlRefNo, attachedDoc);
                    loadUploads();

                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext +
                                        "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

                }
                if (!FileUploadSDoc.HasFile)
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

        protected void btnUploadCF_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKind = "College Financials";
                var schlRefNo = ddlScolarshipType.SelectedValue;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Students.No + @"\";
                string fileName = Path.GetFileName(FileUploadCF.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadCF.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }
                if (FileUploadCF.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") ||
                    (ext == ".doc") || (ext == ".xlsx"))
                {

                    string filename = Students.No + "_" + fileName;
                    FileUploadCF.SaveAs(uploadsFolder + filename);

                    //file path to read file
                    string filePath = uploadsFolder + filename;
                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKind, schlRefNo, attachedDoc);
                    loadUploads();

                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext +
                                        "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

                }
                if (!FileUploadCF.HasFile)
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

        protected void btnUploadNID_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKind = "National ID /or Student ID";
                var schlRefNo = ddlScolarshipType.SelectedValue;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Students.No + @"\";
                string fileName = Path.GetFileName(FileUploadSNID.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadSNID.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }
                if (FileUploadSNID.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") ||
                    (ext == ".doc") || (ext == ".xlsx"))
                {
                    string filename = Students.No + "_" + fileName;
                    FileUploadSNID.SaveAs(uploadsFolder + filename);

                    //file path to read file
                    string filePath = uploadsFolder + filename;
                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKind, schlRefNo, attachedDoc);
                    loadUploads();

                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext +
                                        "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

                }
                if (!FileUploadSNID.HasFile)
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

        protected void btnUploadPhoto_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKind = "Passport Photo";
                var schlRefNo = ddlScolarshipType.SelectedValue;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Students.No + @"\";
                string fileName = Path.GetFileName(FileUploadPhoto.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadPhoto.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }
                if (FileUploadPhoto.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") ||
                    (ext == ".doc") || (ext == ".xlsx"))
                {

                    string filename = Students.No + "_" + fileName;
                    FileUploadPhoto.SaveAs(uploadsFolder + filename);

                    //file path to read file
                    string filePath = uploadsFolder + filename;
                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKind, schlRefNo, attachedDoc);
                    loadUploads();

                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext +
                                        "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

                }
                if (!FileUploadPhoto.HasFile)
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

        protected void btnUploadGuardLeter_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKind = "Guardian Concurrence Letter";
                var schlRefNo = ddlScolarshipType.SelectedValue;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Students.No + @"\";
                string fileName = Path.GetFileName(FileUploadGurdLeter.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadGurdLeter.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }
                if (FileUploadGurdLeter.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") ||
                    (ext == ".doc") || (ext == ".xlsx"))
                {

                    string filename = Students.No + "_" + fileName;
                    FileUploadGurdLeter.SaveAs(uploadsFolder + filename);

                    //file path to read file
                    string filePath = uploadsFolder + filename;
                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKind, schlRefNo, attachedDoc);
                    loadUploads();

                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext +
                                        "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

                }
                if (!FileUploadGurdLeter.HasFile)
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

        protected void btnUploadDeansTest_OnClick(object sender, EventArgs e)
        {
            try
            {
                var documentKind = "Guardian Concurrence Letter";
                var schlRefNo = ddlScolarshipType.SelectedValue;

                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents\\" + Students.No + @"\";
                string fileName = Path.GetFileName(FileUploadDeansTest.PostedFile.FileName);
                string ext = Path.GetExtension(FileUploadDeansTest.PostedFile.FileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    //if the folder doesnt exist create it
                    Directory.CreateDirectory(uploadsFolder);
                }
                if (FileUploadDeansTest.PostedFile.ContentLength > 5000000)
                {
                    KCDFAlert.ShowAlert("Select a file less than 5MB!");
                    return;
                }
                if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png") || (ext == ".pdf") || (ext == ".docx") ||
                    (ext == ".doc") || (ext == ".xlsx"))
                {

                    string filename = Students.No + "_" + fileName;
                    FileUploadDeansTest.SaveAs(uploadsFolder + filename);

                    //file path to read file
                    string filePath = uploadsFolder + filename;
                    FileStream file = File.OpenRead(filePath);
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer, 0, buffer.Length);
                    file.Close();
                    string attachedDoc = Convert.ToBase64String(buffer);

                    saveAttachment(filename, ext, documentKind, schlRefNo, attachedDoc);
                    loadUploads();

                }
                else
                {
                    KCDFAlert.ShowAlert("File Format is : " + ext +
                                        "; - Allowed picture formats are: JPG, JPEG, PNG, PDF, DOCX, DOC, XLSX only!");

                }
                if (!FileUploadDeansTest.HasFile)
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

        protected void btnEditUniDetails_OnClick(object sender, EventArgs e)
        {
            string bankBrnch;
            string uniBank;
            string uniVNm;
            var accName = txtUniAccName.Text;
            var accNo = txtUniAccNumber.Text;
            var regNo = txtRegNumber.Text;
            var idNo = txtIDNumber.Text;

            if (ddlUniversity.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "alert('Please select College or University');", true);
                ddlUniversity.Focus();
                return;
            }
            else
            {
              uniVNm =ddlUniversity.SelectedItem.Text;
            }
            if (ddlBankUni.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "alert('Please select Bank!');", true);
                ddlBankUni.Focus();
                return;
            }
            else
            {
              uniBank = ddlBankUni.SelectedItem.Text;
            }
            if (ddlbankBranchUni.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "alert('Please select Bank Branch!');", true);
                ddlbankBranchUni.Focus();
                return;
            }
            else
            {
               bankBrnch = ddlbankBranchUni.SelectedItem.Text;
            }

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            if (sup.FnSaveUniBankDetails(uniVNm, uniBank, bankBrnch, Session["username"].ToString(), accNo,idNo, accName) ==true)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "alert('College Bank details Saved Successfully!');", true);
                MyCollegBankDs();
            }
         }

        protected void btnAddPersonalBankDs_OnClick(object sender, EventArgs e)
        {
            string stdbank;
            string bBranch;
            var accBName = txtYourAccNAme.Text;
            var accNumber = txtYourAccNumber.Text;
            var stdIdno = txtYourIDNo.Text;

            if (ddlPersonaBank.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "alert('Please select your Bank');", true);
                ddlPersonaBank.Focus();
                return;
            }
            else
            {
                stdbank = ddlPersonaBank.SelectedItem.Text;
            }
            if (ddlPersonaBranch.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "alert('Please select your Bank Branch');", true);
                ddlPersonaBranch.Focus();
                return;
            }
            else
            {
                bBranch = ddlPersonaBranch.SelectedItem.Text;
            }

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            if (sup.FnSavePersonalBankDetails(stdbank, bBranch, Session["username"].ToString(), accNumber, stdIdno, accBName) == true)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "alert('Your Personal Bank details Saved Successfully!');", true);
                MyPersonalBankDs();
            }

        }
        protected void MyBankIs()
        {
            var mybank = nav.myBanks.ToList();
            ddlBankUni.DataSource = mybank;
            ddlBankUni.DataTextField = "Bank_Name";
            ddlBankUni.DataValueField = "Bank_Code";
            ddlBankUni.DataBind();

            ddlPersonaBank.DataSource = mybank;
            ddlPersonaBank.DataTextField = "Bank_Name";
            ddlPersonaBank.DataValueField = "Bank_Code";
            ddlPersonaBank.DataBind();
        }

        protected void MyUniCollege()
        {
            var myUni = nav.my_unisOrCollegs.ToList();
            ddlUniversity.DataSource = myUni;
            ddlUniversity.DataTextField = "College_Name";
            ddlUniversity.DataValueField = "College_Name";
            ddlUniversity.DataBind();
        }

        protected void MyCollegBankDs()
        {
            var myuniBInf = nav.uniBanks.ToList().Where(un => un.Student_Username == Session["username"].ToString());
            txtMyColleg.Text = myuniBInf.Select(cl => cl.University_Name).SingleOrDefault();
            txtMyBank.Text = myuniBInf.Select(b => b.University_Bank_Name).SingleOrDefault();
            txtBnkBrnch.Text = myuniBInf.Select(bb => bb.University_Bank_Branch).SingleOrDefault();
            txtUniAccName.Text = myuniBInf.Select(an => an.Account_Name).SingleOrDefault();
            txtUniAccNumber.Text = myuniBInf.Select(no => no.University_Account_No).SingleOrDefault();
            txtRegNumber.Text = myuniBInf.Select(rg => rg.Student_No).SingleOrDefault();
            txtIDNumber.Text = myuniBInf.Select(id => id.Students_IDNo).SingleOrDefault();
        }

        protected void MyPersonalBankDs()
        {
            var myBInf = nav.studeBanks.ToList().Where(un => un.Student_Username == Session["username"].ToString());
            txtmyPsBank.Text = myBInf.Select(mb => mb.Student_Bank_Name).SingleOrDefault();
            txtMyBbranch.Text = myBInf.Select(br => br.Student_Bank_Branch).SingleOrDefault();
            txtYourAccNAme.Text = myBInf.Select(acn => acn.Account_Name).SingleOrDefault();
            txtYourAccNumber.Text = myBInf.Select(acno => acno.Student_Bank_Account).SingleOrDefault();
            txtYourIDNo.Text = myBInf.Select(mId => mId.Sudents_IdNo).SingleOrDefault();
        }

        protected void btnSaveApplication_OnClick(object sender, EventArgs e)
        {
            var admNo = txtIDNo.Text;
            var fname = txtfNname.Text.Trim();
            var mname = txtMname.Text.Trim();
            var lname = txtLname.Text.Trim();
            Decimal bugt;
            DateTime dateRequired;
            if (string.IsNullOrWhiteSpace(txtBudgetTotal.Text))
            {
                txtBudgetTotal.Focus();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert('Please enter total budget!');", true);
                return;
            }
            else
            {
                bugt = Convert.ToDecimal(txtBudgetTotal.Text);
            }

            var reqDat = txtWorkplanDate.Value.Trim();
            if (string.IsNullOrWhiteSpace(reqDat))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert('Select a valid date!');", true);
                txtWorkplanDate.Focus();
                return;
            }
            else
            {
                dateRequired = DateTime.Parse(reqDat);
            }

            var fullName = fname + " " + mname + " " + lname;
            DateTime todayIs = DateTime.Now;
            var myColleIs = txtCollege.Text;
            var sclRefNo = ddlScolarshipType.SelectedValue;
            if (ddlScolarshipType.SelectedIndex == 0)
            {
                KCDFAlert.ShowAlert("Select valid Scholarship first!");
                ddlScolarshipType.Focus();
                ddlScolarshipType.BorderColor = Color.Red;
                return;
            }
            //Save application here
            try
            {
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                if (sup.FnAddScholarship(sclRefNo, admNo, fullName, todayIs, myColleIs, Session["username"].ToString(), bugt,dateRequired) ==true)
                {
                   // KCDFAlert.ShowAlert("Application Saved Successfully! on :" );
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CodesaBitch", "alert('Application Saved Successfully! on :"+ todayIs + "');", true);
                }
               
            }
            catch (Exception Er)
            {
                //KCDFAlert.ShowAlert(Er.Message);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CodesaBitch", "AlreadyIn();", true);
            }
        }

        protected void ddlScolarshipType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int selVal = ddlScolarshipType.SelectedIndex;
            switch (selVal)
            {
                case 0:
                    KCDFAlert.ShowAlert("Select a valid Scholarship to continue!");
                    btnSaveApplication.Enabled = false;
                    break;
                default:
                    Session["theScholarship"] = ddlScolarshipType.SelectedItem.Text;
                    KCDFAlert.ShowAlert("You selected: " + Session["theScholarship"] + " Ref Number: " +
                                        ddlScolarshipType.SelectedValue);
                    btnSaveApplication.Enabled = true;
                    break;

            }
        }

        protected void ddlBankUni_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int selIndex = ddlBankUni.SelectedIndex;
            switch (selIndex)
            {
                case 0:
                    KCDFAlert.ShowAlert("Invalid Bank selection, please choose");
                    break;
                default:
                    var mybank = ddlBankUni.SelectedValue;

                    var mybranch = nav.myBankBranch.Where(sc => sc.Bank_Code == mybank).ToList();

                    ddlbankBranchUni.DataSource = mybranch;
                    ddlbankBranchUni.DataTextField = "Branch_Name";
                    ddlbankBranchUni.DataValueField = "Branch_Name";
                    ddlbankBranchUni.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "pageLoad();", true);
                    break;
            }
        }

        protected void ddlPersonaBank_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int selIndex = ddlPersonaBank.SelectedIndex;
            switch (selIndex)
            {
                case 0:
                    KCDFAlert.ShowAlert("Invalid Bank selection, please choose");
                    break;
                default:
                    var mybank = ddlPersonaBank.SelectedValue;

                    var mybranch = nav.myBankBranch.Where(sc => sc.Bank_Code == mybank).ToList();

                    ddlPersonaBranch.DataSource = mybranch;
                    ddlPersonaBranch.DataTextField = "Branch_Name";
                    ddlPersonaBranch.DataValueField = "Branch_Name";
                    ddlPersonaBranch.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "pageLoad();", true);
                    break;
            }
        }

        protected void btnSubmitApplication_OnClick(object sender, EventArgs e)
        {
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                     ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            if (sup.FnSumbitScholarshipApp(Session["username"].ToString(), ddlScolarshipType.SelectedValue) == true)
            {
                KCDFAlert.ShowAlert("Application Submitted Successfully! Keep checking your dashboard for Updates!");
            }
            else
            {
                KCDFAlert.ShowAlert("Please apply first before submitting!");
            }
        }
        protected void btnGoNext1_OnClick(object sender, EventArgs e)
        {
            profileMultiview.SetActiveView(scholarSupport);
        }

        protected void btnGoNext3_OnClick(object sender, EventArgs e)
        {
            profileMultiview.SetActiveView(workplanView);
        }

        protected void btnGoNext4_OnClick(object sender, EventArgs e)
        {
            profileMultiview.SetActiveView(AttachDocs);
        }

        protected void btnGoNext5_OnClick(object sender, EventArgs e)
        {
            profileMultiview.SetActiveView(bankDetails);
        }

        protected void btnGoNext6_OnClick(object sender, EventArgs e)
        {
            profileMultiview.SetActiveView(finalSubmit);
        }
    }
}