using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;

namespace KCDF_P
{
    public partial class Grantee_Dashboard : System.Web.UI.Page
    {
        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
              new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                  ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };
        public readonly string strSQLConn = @"Server=" + ConfigurationManager.AppSettings["DB_INSTANCE"] + ";Database=" +
                                  ConfigurationManager.AppSettings["DB_NAME"] + "; User ID=" +
                                  ConfigurationManager.AppSettings["DB_USER"] + "; Password=" +
                                  ConfigurationManager.AppSettings["DB_PWD"] + "; MultipleActiveResultSets=true";

        public string CompanyName = "KCDF";
        [STAThread]
        protected void Page_Load(object sender, EventArgs e)
        {
            NoCache();
            if (!IsPostBack)
            {
                
                if (!this.Page.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }

                //Check if the user is logged in or not
                if (Session["Logged"].Equals("No"))
                {
                    //Redirect the user to the Login.aspx
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    CheckSessX();
                    GetMemberDetails();
                    LoadMyProjects();
                    MyCountyIs();
                    LoadProfPic();
                    //fetchPicture();
                    ClearCache();
                    lblUsernameIS.Text = Convert.ToString(Session["username"]);
                    LblSessionfromMAster();
                    MyReporting();
                    GetMyMatrix();
                    OnlyShawn();
                }
                
            }

        }

        protected void OnlyShawn()
        {
           var sean = Session["seanonly"].ToString();
            switch (sean)
            {
               case "andre":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CodesaBitch", "crackDIV()", true);
                    break;
                case "50Cent":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CodesaBitch", "crackDIV()", true);
                    break;
                default:
                break;     
            }
            
        }
        protected void GetMemberDetails()
        {
            var objGrantees = nav.grantees_Register.Where(r => r.Organization_Username == User.Identity.Name).FirstOrDefault();

            if (objGrantees != null)
            {
                orgName.InnerHtml = objGrantees.Organization_Name;
                orgEmail.InnerHtml = objGrantees.Email;
                orgNumber.InnerHtml = objGrantees.No;
                Session["grant_no"] = objGrantees.No;
            }
            

        }
        public void NoCache()
        {
            Response.CacheControl = "private";
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        public void StoreToDatabase(string fileNme)
        {

            // file path to read file
            string filePath = fileNme;

            // declare and initialize FileStream object
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // declare and initialize BinaryReader object
            BinaryReader reader = new BinaryReader(stream);

            // Read bytes  from the file
            byte[] file = reader.ReadBytes((int)stream.Length);
            // always Remember  to close the handles, 
            // or resources remain locked
            reader.Close();
            stream.Close();

        }
        protected void LblSessionfromMAster()
        {
            System.Web.UI.WebControls.Label lblMastersession =
                (System.Web.UI.WebControls.Label)Master.FindControl("lblSessionUsername");

            lblMastersession.Text = lblUsernameIS.Text;
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

        protected void ClearCache()
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        protected void LoadMyProjects()
        {
            try
            {
                var prjcts = nav.projectOverview.ToList().Where(us => us.Username.Equals(Session["username"].ToString()));
                tblMyProjects.AutoGenerateColumns = false;
                tblMyProjects.DataSource = prjcts;
                tblMyProjects.DataBind();

                tblMyProjects.UseAccessibleHeader = true;
                tblMyProjects.HeaderRow.TableSection = TableRowSection.TableHeader;

                TableCellCollection cells = tblMyProjects.HeaderRow.Cells;
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
                tblMyProjects.EmptyDataText = "No project data found";
            }

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
                    profPic.ImageUrl = "ProfilePics/Grantees/" + pic;
                    HttpResponse.RemoveOutputCacheItem("/Grantee_Dashboard.aspx");
                    // KCDFAlert.ShowAlert("ProfilePics/ "+pic);
                }

                //string picture = string.Empty;
                //WSConfig.ObjNav.FnRetrieveSavedPic(Session["username"].ToString(), ref picture);

                //if (picture == string.Empty)
                //{
                //    KCDFAlert.ShowAlert("No Profile Photo yet!");
                //    return;
                //}
                //else
                //{
                //    byte[] buffer = Convert.FromBase64String(picture);

                //    FileStream file = File.Create("ProfilePics/Grantees/ "  + Grantees.No + ".png");

                //    file.Write(buffer, 0, buffer.Length);
                //    file.Close();
                //    profPic.ImageUrl = "ProfilePics/Grantees/" + Grantees.No + ".png";
                //    KCDFAlert.ShowAlert(picture);

                //    FileStream file = File.OpenRead(filePath);
                //    byte[] buffer = new byte[file.Length];
                //    file.Read(buffer, 0, buffer.Length);
                //    file.Close();
                //    string attachedDoc = Convert.ToBase64String(buffer);
                //    saveProfToNav(uploadsFolder + filenameO, filenameO, attachedDoc);
                //}

            }
            catch (Exception ex)
            {
            }
          }

        protected void fetchPicture()
        {
            var grntNo = Session["grant_no"];
            string sqlrqst = @"SELECT No, ProfilePhoto from [" + CompanyName + "$Grantees] WHERE No=@usnM";

            SqlConnection con = new SqlConnection(strSQLConn);
            SqlCommand command = new SqlCommand(sqlrqst, con);
            command.Parameters.AddWithValue("@usnM", grntNo);
            SqlDataAdapter da = new SqlDataAdapter(command);
            SqlCommandBuilder cbd = new SqlCommandBuilder();
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Open();
            byte[] data = (byte[])(ds.Tables[0].Rows[0]["ProfilePhoto"]);
            MemoryStream mem = new MemoryStream(data);
            System.Drawing.Image image = System.Drawing.Image.FromStream(mem);
            profPic.ImageUrl = image.ToString();

        }
        protected void btnUploadPic_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        System.Drawing.Image ConvertBinarytoImage(byte[]data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return System.Drawing.Image.FromStream(ms);

            }
        }
        
        protected void MyCountyIs()
        {
            var mycounty = nav.mycountyIs.ToList();
            ddlSelCountry.DataSource = mycounty;
            ddlSelCountry.DataTextField = "County_Name";
            ddlSelCountry.DataValueField = "County_Code";
            ddlSelCountry.DataBind();
            ddlSelCountry.Items.Insert(0, "--Select your County--");
        }
        protected void ddlSelCountry_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            int selIndex = ddlSelCountry.SelectedIndex;
            switch (selIndex)
            {
                case 0:
                    KCDFAlert.ShowAlert("Invalid County selection");
                    break;
                default:
                    var sbCntysplit00 = ddlSelCountry.SelectedValue;
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

        protected void tblMyProjects_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            try
            {
                var del_id = tblMyProjects.DataKeys[e.RowIndex].Values[0].ToString();
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                sup.FnDeleteProject(del_id);
                LoadMyProjects();
                KCDFAlert.ShowAlert("Project Deleted Successfully!");
            }
            catch (Exception ex)
            {
               KCDFAlert.ShowAlert(ex.Message); 
            }
          
        }

        protected void lnkEdit_OnClick(object sender, EventArgs e)
        {
            try
            {
            string edit_id = (sender as LinkButton).CommandArgument;
            var prjnm =
                nav.projectOverview.ToList().Where(i => i.No == edit_id).Select(pn => pn.Project_Name).SingleOrDefault();
                var CallRefNo =
                    nav.projectOverview.ToList().Where(i => i.No == edit_id).Select(pn => pn.Call_Ref_Number).SingleOrDefault();

                var approvedyeah =
                nav.projectOverview.ToList()
                    .Where(a => a.No == edit_id)
                    .Select(ast => ast.Approval_Status)
                    .SingleOrDefault();

            switch (approvedyeah)
            {
                case "Approved":
                    KCDFAlert.ShowAlert("You cannot Edit an Appproved application!!");
                    break;

                case "Pending Approval":
                     KCDFAlert.ShowAlert("Your application is pending approval, you cannot edit");
                       
                    break;

                case "Open":
                    Session["projectname"] = prjnm;
                    Session["projectidnumber"] = edit_id;
                    Session["prjCallrefNo"] = CallRefNo;
                    Response.Redirect("EditMyProject.aspx");
                    break;
              }
            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert("Error Loading!");
            }
        }

        protected void btnProjEdit_OnClick(object sender, EventArgs e)
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
            string projectNm = lblProjNo.Text;
            
            if (ddlMonths.SelectedItem.Text == "..Select project Length..")
            {
                KCDFAlert.ShowAlert("Select valid project duration!");
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
            sup.FnEditProject(projectNm, county, constituency, urbantarget,
                projectlength, projCost, contrib, kcdffunds, projTDt, scale);
              
            KCDFAlert.ShowAlert("Data Updated Successfully!");
          
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
            catch (Exception ex)
            {

                KCDFAlert.ShowAlert(ex.Message);
            }
        }

        protected void LoadEditPrj(string projNumber)
        {
            var ldPrj = nav.projectOverview.ToList().Where(sn => sn.No == projNumber);
            TextBoxtitle.Text = ldPrj.Select(t => t.Project_Title).SingleOrDefault();

            var dtB = ldPrj.Select(dtoB => dtoB.Project_Start_Date).SingleOrDefault().ToString();
            DateTime dt1 = DateTime.Parse(dtB);
            txtDateofStart.Value = dt1.ToShortDateString();
            ddlSelCountry.SelectedItem.Text = ldPrj.Select(c => c.County).SingleOrDefault();
            ddlConstituency.SelectedItem.Text = ldPrj.Select(cn => cn.Constituency).SingleOrDefault();
            txtAreaTargetSettmnt.Text = ldPrj.Select(ta => ta.Urban_Settlement_Target).SingleOrDefault();
            ddlMonths.SelectedItem.Text = ldPrj.Select(mt => mt.Project_Length_Months).SingleOrDefault().ToString();
            ddlEstScale.SelectedItem.Text = ldPrj.Select(es => es.Grant_Scale).SingleOrDefault();
            TextBoxcost.Text = ldPrj.Select(cs => cs.Total_Project_Cost_KES).SingleOrDefault().ToString();
            TextBoxcont.Text = ldPrj.Select(cd => cd.Your_Cash_Contribution_KES).SingleOrDefault().ToString();
            TextBoxrequested.Text = ldPrj.Select(rq => rq.Requested_KCDF_Amount_KES).SingleOrDefault().ToString();

        }

        protected void copyTest_OnClick(object sender, EventArgs e)
        {
            CopyFilesToDir();
        }

        protected void CopyFilesToDir()
        {
            // Impersonate, automatically release the impersonation.
            using (new Impersonator("KCDFFOUNDATION", @"KCDFFOUNDATION\Administrator", "Admin987654321"))
            {
                //try
                //{
                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents/" + Session["grant_no"] + @"\";
                string uriUploads = new Uri(uploadsFolder).LocalPath;

                string destPath = @"http://192.168.0.249:801/";
                //string uriPath = @"E:\AdvancedPortals\KCDF_P\KCDF_P\All Uploads\";
                string uriPath = new Uri(destPath).LocalPath;
                foreach (string dirPath in Directory.GetDirectories(uriUploads, " * ",
                    SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(uriUploads, uriPath));

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(uriUploads, "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(uriUploads, uriPath), true);
                KCDFAlert.ShowAlert("Copied from " + uriUploads + " to " + uriPath);
                //}
                //catch (Exception em)
                //{
                //  KCDFAlert.ShowAlert(em.Message);  

                //}

            }
        }
   
     
        protected void btnValidateInfo_OnClick(object sender, EventArgs e)
        {
            try
            {
                var tobevalidated = Session["edit_id"].ToString();
                //KCDFAlert.ShowAlert(tobevalidated);var prj = ddlAccountType.SelectedItem.Text;
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
                      //  btnFinalSubmit.Enabled = true;
                        btnValidateInfo.Enabled = false;
                        hdnTxtValidit.Value = "isValid";
                        KCDFAlert.ShowAlert("All Uploads available, you can submit your application: " + hdnTxtValidit.Value);
                        break;

                    case false:
                        txtValidate.Text = "PLEASE COMPLETE THE APPLICATION FIRST";
                        txtValidate.BackColor = Color.Red;
                        txtValidate.ForeColor = Color.GhostWhite;
                      //  btnFinalSubmit.Enabled = false;
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
                //btnFinalSubmit.Enabled = false;
            }
        }
       protected void lnkEditMe_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSubmit();", true);
            Session["edit_id"] = (sender as LinkButton).CommandArgument;
            var validMe = Session["edit_id"].ToString();
            lblProjNb.Text = validMe;

        }

        protected void lnkConfirm_OnClick(object sender, EventArgs e)
        {
            var subStatus = hdnTxtValidit.Value;
            var projectRfN = (sender as LinkButton).CommandArgument;
            switch (subStatus)
            {
                case "isValid":
                    SubmitProject(projectRfN);
                    break;
                case "isInValid":
                    KCDFAlert.ShowAlert("You can't submit this application, because you have not uploaded all documents!");
                    break;
                default:
                    KCDFAlert.ShowAlert("Please Confirm attachments first! " + hdnTxtValidit.Value);
                    break;
            }


        }
        protected void SubmitProject(string projNo)
        {
            try
            {
                var usNm = Session["username"].ToString();

                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                bool isSubmitted = sup.FnFinalSubmission(usNm, projNo);

                switch (isSubmitted)
                {
                    case true:
                        KCDFAlert.ShowAlert("Your Application is Successfully submitted!" + isSubmitted);
                        //send Email Here
                        SendEmail(usNm, projNo);
                       // loadIncompleteApplication();
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

        protected void SendEmail(string myUsername, string projNumber)
        {
            var ldPrj = nav.projectOverview.ToList().Where(sn => sn.No == projNumber);
            var projectRefNo = ldPrj.Select(t => t.Call_Ref_Number).SingleOrDefault();
            var pName = ldPrj.Select(t => t.Project_Title).SingleOrDefault();

            using (MailMessage mm = new MailMessage("kcdfportal@gmail.com", orgEmail.InnerText))
            {

                mm.Subject = "KCDF Application Submission";
                string body = "Dear " + myUsername + ",";
                body += "<br /><br />You have successfully applied for KCDF Project, "+ pName + ": "+ projectRefNo + "" ;
                body += "<br /><br />Thank you for choosing KCDF";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("kcdfportal@gmail.com", "Kcdfportal@4321*~");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                // KCDFAlert.ShowAlert("Activation link has been send to your email");
            }
        }

        protected void MyReporting()
        {
            var repo = nav.reportingDocs.ToList().Where(r => r.Username == Session["username"].ToString());

            gridviewUploadedRepos.AutoGenerateColumns = false;
            gridviewUploadedRepos.DataSource = repo;
            gridviewUploadedRepos.DataBind();
        }

        protected void GetMyMatrix()
        {
            var matrx = nav.pocasnMAtrix.ToList().Where(o => o.Username == Session["username"].ToString());
            gridMatrixPocas.AutoGenerateColumns = false;
            gridMatrixPocas.DataSource = matrx;
            gridMatrixPocas.DataBind();
        }
        protected void lnkReupload_OnClick(object sender, EventArgs e)
        {
            var entryId = (sender as LinkButton).CommandArgument;
            TaskType(entryId);
        }

        protected void lnkReuploadMatr_OnClick(object sender, EventArgs e)
        {
            var entryId = (sender as LinkButton).CommandArgument;
            TaskTypeinMatrix(entryId);
        }
        protected void TaskType(string taskentryNo)
        {
            Session["fullfillTask"] = 0;
            var typeOpt =
                nav.reportingDocs.ToList().Where(n => n.No == taskentryNo).Select(op => op.Document_Kind).SingleOrDefault();

            switch (typeOpt)
            {
                case "NARRATIVE":
                    Session["typeoftask"] = "Narrative";
                    Response.Redirect("Report_Form.aspx");
                    break;

                case "FINANCIAL":
                    Session["typeoftask"] = "Financial";
                    Response.Redirect("Report_Form.aspx");
                    break;

                case "DATA":
                    Session["typeoftask"] = "Data";
                    Response.Redirect("Report_Form.aspx");
                    break;

                case "INDICATOR MATRIX":
                    Session["typeoftask"] = "Indicator Matrix";
                    Response.Redirect("UploadFiles_Grants.aspx");
                    break;

                case "POCA TOOL":
                    Session["typeoftask"] = "POCA Tool";
                    Response.Redirect("UploadFiles_Grants.aspx");
                    break;

                default:
                    Session["typeoftask"] = "Other";
                    Response.Redirect("Report_Form.aspx");
                    break;

            }

        }

        protected void TaskTypeinMatrix(string taskentryNo)
        {
            Session["fullfillTask"] = 0;

            var typeOpt =
                nav.pocasnMAtrix.ToList().Where(n => n.Id == taskentryNo).Select(op => op.Document_Kind).SingleOrDefault();

            switch (typeOpt)
            {
                case "INDICATOR MATRIX":
                    Session["typeoftask"] = "Indicator Matrix";
                    Response.Redirect("UploadFiles_Grants.aspx");
                    break;

                case "POCA TOOL":
                    Session["typeoftask"] = "POCA Tool";
                    Response.Redirect("UploadFiles_Grants.aspx");
                    break;

                default:
                    Session["typeoftask"] = "Other";
                    Response.Redirect("Report_Form.aspx");
                    break;

            }

        }

        protected void btnDecrypt_OnClick(object sender, EventArgs e)
        {
           KCDFAlert.ShowAlert(Decrypt(txtpassword.Text.Trim()));
        }

       

        public string Decrypt(string cipher)
        {
           string key = "A!9HHhi%XjjYY4YP2@Nob009X";

            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
    }
}