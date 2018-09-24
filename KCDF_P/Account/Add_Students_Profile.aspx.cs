using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;

namespace KCDF_P.Account
{
    public partial class Add_Students_Profile : Page
    {
        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
                new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };

        public static readonly string StrSqlConn = @"Server=" + ConfigurationManager.AppSettings["DB_INSTANCE"] +
                                                   ";Database=" +
                                                   ConfigurationManager.AppSettings["DB_NAME"] + "; User ID=" +
                                                   ConfigurationManager.AppSettings["DB_USER"] + "; Password=" +
                                                   ConfigurationManager.AppSettings["DB_PWD"] +
                                                   "; MultipleActiveResultSets=true";

        public static string CompanyName = "KCDF";

        protected void Page_Load(object sender, EventArgs e)
        {
            NoCache();
            if (!IsPostBack)
            {                
                CheckSessionExists();
                readData();
                readEducBgData();
                loadRefs();
                loadAllMySchools();
                myCountyIs();
            }
        }

        protected void CheckSessionExists()
        {
            try
            {
                var sessionIs = Convert.ToString(Session["username"]);
                if (string.IsNullOrWhiteSpace(sessionIs))
                {
                    Response.Redirect("/Default.aspx");
                }
            }
            catch (Exception)
            {
                Response.Redirect("/Default.aspx");
            }

        }
       
        public void NoCache()
        {
            Response.CacheControl = "private";
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        protected void EditProfile()
        {
            //try
            //{
            var MobileString = txtPhoneNo.Text.Trim();
            var mobileBuilder = new StringBuilder(MobileString);
            mobileBuilder.Remove(0, 1); //Trim one character from position 1
            mobileBuilder.Insert(0, "+254"); // replace position 0 with +254
            MobileString = mobileBuilder.ToString();

            DateTime dTOfBth;
            var gentype = 0;
            var usname = Session["username"].ToString();
            var fname = txtfNname.Text.Trim();
            var mname = txtMname.Text.Trim();
            var lname = txtLname.Text.Trim();
            var idno = txtIDNo.Text.Trim();
            var resid = txtResidence.Text.Trim();
            var gender = lstGender.SelectedItem.Text;

            var mycounty = "";
            var mysubcounty = "";

            if (ddlSelCounty.SelectedIndex == 0)
            {
                ddlSelCounty.Focus();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                    "alert('Please select your county!');", true);
                return;
            }
            else
            {
                mycounty = ddlSelCounty.SelectedItem.Text;
            }

            if (ddlConstituency.SelectedIndex == 0)
            {
                ddlConstituency.Focus();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                    "alert('Please select your subcounty!');", true);
                return;
            }
            else
            {
                mysubcounty = ddlConstituency.SelectedItem.Text;
            }

            if (lstGender.SelectedIndex == 0)
            {
                KCDFAlert.ShowAlert("Please select a valid gender type!");
                return;
            }
            if (gender.Equals("Male"))
            {
                gentype = 0;
            }
            else
            {
                gentype = 1;
            }
            var dtofBirth = dateOFBirth.Value;
            if (string.IsNullOrWhiteSpace(dtofBirth))
            {
                dateOFBirth.Focus();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                    "alert('Please select valid date');", true);
                return;
            }
            else
            {
                dTOfBth = DateTime.Parse(dtofBirth);
            }

            if (string.IsNullOrEmpty(idno))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                    "alert('Fill in ID Number!');", true);
                txtIDNo.Focus();
                txtIDNo.BorderColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(MobileString))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                    "alert('Fill in Mobile Number!');", true);
                txtPhoneNo.Focus();
                txtPhoneNo.BorderColor = Color.Red;
                return;
            }

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            var sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            if (
                sup.FnRegisterStudent(fname, mname, lname, idno, resid, MobileString, usname, gentype, dTOfBth, mycounty,
                    mysubcounty) == true)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                    "alert('Your account succcessfully Edited!');", true);
            }

            //}
            //catch (Exception ex)
            //{
            //    KCDFAlert.ShowAlert("Select Valid Date");
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert('Error!');", true);

            //}
        }

        protected void btnEditProf_Click(object sender, EventArgs e)
        {
            EditProfile();
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
            try
            {

                txtfNname.Text = studData.Select(fn => fn.First_name).SingleOrDefault();
                txtMname.Text = studData.Select(mn => mn.Middle_name).SingleOrDefault();
                txtLname.Text = studData.Select(ln => ln.Last_name).SingleOrDefault();
                txtResidence.Text = studData.Select(r => r.Residence).SingleOrDefault();
                txtIDNo.Text = studData.Select(id => id.ID_No).SingleOrDefault();
                txtEmailAdd.Text = studData.Select(em => em.Email).SingleOrDefault();
                txtCounty.Text = studData.Select(em => em.County).SingleOrDefault();
                txtSubCounty.Text = studData.Select(em => em.SubCounty).SingleOrDefault();
                var dtB = studData.Select(dtoB => dtoB.Date_of_Birth).SingleOrDefault().ToString();
                var dt1 = DateTime.Parse(dtB);
                dateOFBirth.Value = dt1.ToShortDateString();
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
            catch (Exception exr)
            {
                KCDFAlert.ShowAlert("Your Data is Incomplete, please fill all your information");
            }
        }

        protected void studentInfoMenu_OnMenuItemClick(object sender, MenuEventArgs e)
        {
            var index = Int32.Parse(e.Item.Value);
            profileMultiview.ActiveViewIndex = index;
        }

        protected void btnEditEductn_OnClick(object sender, EventArgs e)
        {
            editEducation();
        }

        protected void editEducation()
        {
            try
            {
                DateTime yearOfAdmn;
                DateTime yrofCompln;
                int marks = 0;
                int ttMarks = 0;
                string mygradeIs;
                string uni;
                string seco;
                string YroStd;
                var usname = Session["username"].ToString();
                var primo = lblValues.Text;
                var grdEmail = txtGuardianEmail.Text.Trim();
                var grdAddr = txtGuardianAddress.Text.Trim();
                var course = txtDegree.Text.Trim();
                int edlev;

                if (rdoBtnListEdLevel.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please select level of Education first');", true);
                    return;
                }
                else
                {
                    edlev = rdoBtnListEdLevel.SelectedIndex;
                }
                if (ddlSeco.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please select secondary school');", true);
                    return;
                }
                else
                {
                    seco = ddlSeco.SelectedItem.Text.Trim();
                }

                if (ddlUnivCollg.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please select College or University');", true);
                    return;
                }
                else
                {
                    uni = ddlUnivCollg.SelectedItem.Text.Trim();
                }

                if (rdoBtnListGrade.SelectedIndex == -1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please select a grade');", true);
                    return;
                }
                else
                {
                    mygradeIs = rdoBtnListGrade.SelectedValue;
                }

                if (ddlYearofStudy.SelectedIndex == 0)
                {
                    ddlYearofStudy.Focus();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please select an option');", true);
                    return;
                }
                else
                {
                    YroStd = ddlYearofStudy.SelectedItem.Text;
                }

                var YoAdm = txtYrofAdmsn.Value;
                if (string.IsNullOrWhiteSpace(YoAdm))
                {
                    txtYrofAdmsn.Focus();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please select valid date');", true);
                    return;
                }
                else
                {
                    yearOfAdmn = DateTime.Parse(YoAdm);
                }

                var YoComptn = txtYrofCompltn.Value;
                if (string.IsNullOrWhiteSpace(YoComptn))
                {
                    txtYrofCompltn.Focus();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please select valid date');", true);
                    return;
                }
                else
                {
                    yrofCompln = DateTime.Parse(YoComptn);
                }

                if (string.IsNullOrWhiteSpace(txtMarks.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please Fill in your marks');", true);
                    txtMarks.Focus();
                    txtMarks.BorderColor = Color.Red;
                    return;
                }
                else
                {
                    marks = Convert.ToInt32(txtMarks.Text);
                }
                if (string.IsNullOrWhiteSpace(txtTotalMarks.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please Fill in total marks');", true);
                    txtTotalMarks.Focus();
                    txtTotalMarks.BorderColor = Color.Red;
                    return;
                }
                else
                {
                    ttMarks = Convert.ToInt32(txtTotalMarks.Text);
                }

                var grdPhne = txtGuardianPhone.Text.Trim();
                var mobileBuilder = new StringBuilder(grdPhne);
                mobileBuilder.Remove(0, 1); //Trim one character from position 1
                mobileBuilder.Insert(0, "+254"); // replace position 0 with +254
                grdPhne = mobileBuilder.ToString();

                try
                {
                    if (yrofCompln < yearOfAdmn)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                            "alert('Admision Date should be later than Completion Date!');", true);
                        txtYrofAdmsn.Value = "";
                        txtYrofCompltn.Value = "";
                        return;
                    }
                }
                catch (Exception ec)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please select correct dates!');", true);

                }

                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                        ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                    var sup = new Portals();
                    sup.Credentials = credentials;
                    sup.PreAuthenticate = true;
                    if (sup.FnEducatBg(usname, primo, seco, uni, yearOfAdmn, YroStd, yrofCompln, grdPhne, grdEmail,
                        grdAddr, course, marks, ttMarks, mygradeIs, edlev) == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                            "alert('Your Education Data Updated Successfully!');", true);
                        readEducBgData();
                    }

                }
                catch (Exception exc)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('" + exc.Message + "');", true);
                }
            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert(ex.Message);
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

            try
            {

                var secsh = edctnData.Select(se => se.Secondary_School).SingleOrDefault();
                txtSecon.Text = secsh;
                txtCollg.Text = edctnData.Select(un => un.University_or_College).SingleOrDefault();
                txtDegree.Text = edctnData.Select(fc => fc.Course).SingleOrDefault();
                txtFaculty.Text = edctnData.Select(fc => fc.Course).SingleOrDefault();
                txtYearofStd.Text = edctnData.Select(yos => yos.Year_of_Study).Single();
                txtForm.Text= edctnData.Select(yos => yos.Year_of_Study).Single();
                var yoAd = edctnData.Select(yoa => Convert.ToDateTime(yoa.Year_of_Admission)).SingleOrDefault();
                var yoC = edctnData.Select(yoc => Convert.ToDateTime(yoc.Year_of_Completion)).SingleOrDefault();
                txtGuardianEmail.Text = edctnData.Select(em => em.Parent_or_Guardian_Email).SingleOrDefault();
                txtGuardianAddress.Text = edctnData.Select(ad => ad.Parent_or_Guardian_Address).SingleOrDefault();

                txtYoAdmn.Text = yoAd.ToShortDateString();
                txtSecoYofAdm.Text = yoAd.ToShortDateString();

                txtYofcomplt.Text = yoC.ToShortDateString();
                txtSecoEndYear.Text = yoC.ToShortDateString();

                int marsk = edctnData.Select(mk => Convert.ToInt32(mk.KCPE_Marks)).SingleOrDefault();
                var fGrade = edctnData.Select(grd => grd.KCSE_Grade).SingleOrDefault();
                int ttMks = edctnData.Select(ttm => Convert.ToInt32(ttm.KCPE_Total_Marks)).SingleOrDefault();
                txtMarks.Text = Convert.ToString(marsk);
                txtTotalMarks.Text = Convert.ToString(ttMks);
                rdoBtnListGrade.SelectedValue = fGrade;

                txtMyPrimo.Text = edctnData.Select(pr => pr.Primary_School).SingleOrDefault();


            }
            catch (Exception exr)
            {
                KCDFAlert.ShowAlert("Your Data is Incomplete, Please Fill all the data!");
            }
        }

        protected void btnAddRefer_OnClick(object sender, EventArgs e)
        {
            var usernaMe = Session["username"].ToString();
            var studNo =
                nav.studentsRegister.ToList()
                    .Where(s => s.Username == Session["username"].ToString())
                    .Select(sn => sn.No)
                    .SingleOrDefault();

            var refFname = txtrefFname.Text.Trim();
            var refmname = refMidName.Text.Trim();
            var refLname = refLName.Text.Trim();
            var refemail = refEmail.Text.Trim();
            var ferPhneNo = refMobile.Text.Trim();
            var mobileBuilder = new StringBuilder(ferPhneNo);
            mobileBuilder.Remove(0, 1); //Trim one character from position 1
            mobileBuilder.Insert(0, "+254"); // replace position 0 with +254
            ferPhneNo = mobileBuilder.ToString();
            try
            {
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                var nws = new Portals();
                nws.Credentials = credentials;
                nws.PreAuthenticate = true;

                nws.FnAddReferee(usernaMe, studNo, refFname, refmname, refLname, ferPhneNo, refemail);
                KCDFAlert.ShowAlert("Referee Added Successfully!");
                loadRefs();
                txtrefFname.Text = "";
                refMidName.Text = "";
                refLName.Text = "";
                refEmail.Text = "";
                refMobile.Text = "";

            }
            catch (Exception ep)
            {
                KCDFAlert.ShowAlert(ep.Message);
            }
        }

        protected void tblRefs_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ref_no"] = tblRefs.SelectedRow.Cells[1].Text;
        }

        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            var usnme = Session["username"].ToString();
            var refNo = Session["ref_no"].ToString();
            try
            {
            }
            catch (Exception ex)
            {
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                var nws = new Portals();
                nws.Credentials = credentials;
                nws.PreAuthenticate = true;

                nws.FnDeleteReferee(usnme, refNo);
                KCDFAlert.ShowAlert("Referee Deleted!");
                loadRefs();
            }
        }

        protected void loadRefs()
        {
            try
            {
                var allmyrefs = nav.myReferees.ToList().Where(us => us.Username == Session["username"].ToString());
                tblRefs.AutoGenerateColumns = false;
                tblRefs.DataSource = allmyrefs;
                tblRefs.DataBind();
            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert(ex.Message);
            }
        }

        protected void loadAllMySchools()
        {
            var secos = nav.list_Schools.Where(seco => seco.Level_of_School == "Secondary").ToList();
            var unicolleg = nav.my_unisOrCollegs.ToList();

            ddlSeco.DataSource = secos;
            ddlSeco.DataTextField = "School_Name";
            ddlSeco.DataValueField = "School_Name";
            ddlSeco.DataBind();
            ddlSeco.Items.Insert(0, "--Select Secondary School--");

            ddlUnivCollg.DataSource = unicolleg;
            ddlUnivCollg.DataTextField = "College_Name";
            ddlUnivCollg.DataValueField = "College_Name";
            ddlUnivCollg.DataBind();
            ddlUnivCollg.Items.Insert(0, "--Select University/College--");

        }

        protected void rdoBtnListGrade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["myGrade"] = rdoBtnListGrade.SelectedValue;
            KCDFAlert.ShowAlert("My Grade: " + Session["myGrade"]);
        }

        protected void btnSearchSQL_OnClick(object sender, EventArgs e)
        {
            //string schoolName = Request.Form[txtSearch.UniqueID];
            //string schooltype = Request.Form[hfCustomerId.UniqueID];
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: " + schoolName + "\\nID: " + schooltype + "');", true);
            var srchT = txtSearch.Text;
            fillMySchoolList(srchT);

        }

        [WebMethod]
        public static string[] GetMySchools(string prefix)
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = StrSqlConn;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT [School Name],[Level of School] from [" + CompanyName +
                                      "$Schools List] WHERE [School Name] like @SearchText + '%' && [Level of School]=0";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}-{1}", sdr["School Name]"], sdr["Level of School"]));
                        }
                    }
                    conn.Close();
                }
            }
            return customers.ToArray();
        }

        protected void fillMySchoolList(string searchT)
        {
            using (SqlConnection con = new SqlConnection(StrSqlConn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT DISTINCT([School Name]) from [" + CompanyName +
                                      "$Schools List] WHERE lower([School Name]) like @SearchText + '%' AND [Level of School]=0";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@SearchText", searchT);
                    cmd.Connection = con;
                    con.Open();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        grdViewMySchoolIs.DataSource = dt;
                        grdViewMySchoolIs.DataBind();
                    }
                    con.Close();
                }
            }

        }

        protected void lnkSelect_OnClick(object sender, EventArgs e)
        {
            string myschoolIs = (sender as LinkButton).CommandArgument;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "whatSchool()", true);
            lblValues.Text = myschoolIs;
            Session["primarySch"] = myschoolIs;
        }

        protected void tblRefs_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var del_id = tblRefs.DataKeys[e.RowIndex].Values[0].ToString();
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                if (sup.FnDeleteReferee(Session["username"].ToString(), del_id) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop",
                        "alert('Application Entry deleted successfully!');", true);
                    loadRefs();
                }

            }
            catch (Exception ex)
            {
                //KCDFAlert.ShowAlert(ex.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alert('" + ex.Message + "');", true);
                loadRefs();
            }
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
                    var subCnty = nav.mysubCountyIs.Where(sc => sc.County_Code == sbCntysplit00).ToList();
                    ddlConstituency.DataSource = subCnty;
                    ddlConstituency.DataTextField = "Sub_County_Name";
                    ddlConstituency.DataValueField = "Sub_County_Name";
                    ddlConstituency.DataBind();
                    ddlConstituency.Items.Insert(0, "--Select your Sub County--");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "pageLoad();", true);
                    //KCDFAlert.ShowAlert(sbCntysplit00);
                    break;
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
        }

        protected void btnNext1_OnClick(object sender, EventArgs e)
        {
            profileMultiview.SetActiveView(edubackGrd);
        }

        protected void btnNext2_OnClick(object sender, EventArgs e)
        {
            profileMultiview.SetActiveView(addReferees);
        }

        protected void btntoApplyF_OnClick(object sender, EventArgs e)
        {
        Response.Redirect("~/Scholarship_Application.aspx");
        }

        protected void rdoBtnListEdLevel_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["yesOrno"] = rdoBtnListEdLevel.SelectedIndex;
            int selindex = Convert.ToInt32(Session["yesOrno"].ToString());
            switch (selindex)
            {
                case 0:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "LevelWhatSeco()", true);
                    //KCDFAlert.ShowAlert("clicked: "+ selindex);
                    //btnEdSecLevel.Visible = true;
                    //btnEditEductn.Visible = false;
                    break;
                case 1:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "LevelWhatHigher()", true);
                    //btnEditEductn.Visible = true;
                    //btnEdSecLevel.Visible = false;
                    break;
            }
        }

        protected void btnEdSecLevel_OnClick(object sender, EventArgs e)
        {
            EditEducationSecLevel();
        }
        protected void EditEducationSecLevel()
        {
            try
            {
                DateTime yearOfAdmn;
                DateTime yrofCompln;
                int marks = 0;
                int ttMarks = 0;
                string mygradeIs;
                int optionIs=0;
                string uni="";
                string seco;
                string YroStd;
                var usname = Session["username"].ToString();
                var primo = lblValues.Text;
                var grdEmail = txtGuardianEmail.Text.Trim();
                var grdAddr = txtGuardianAddress.Text.Trim();

                //int edlev;

                //if (rdoBtnListEdLevel.SelectedIndex ==-1)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                //        "alert('Please select level of Education first');", true);
                   
                //    return;
                //}
                //else
                //{
                //    edlev = rdoBtnListEdLevel.SelectedIndex;
                //}

                int TorF = rdoBtnListEdLevel.SelectedIndex;
                switch (TorF)
                {
                    case 0:
                        optionIs = 0;
                        break;
                    case 1:
                        optionIs = 1;
                        break;
                    default:
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode", "alert('Please select level of Education first!');", true);
                        break;
                }

                if (ddlSeco.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please select secondary school');", true);
                    return;
                }
                else
                {
                    seco = ddlSeco.SelectedItem.Text.Trim();
                }

                if (ddlFormwhat.SelectedIndex == 0)
                {
                    ddlFormwhat.Focus();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please select class!');", true);
                    return;
                }
                else
                {
                    YroStd = ddlFormwhat.SelectedItem.Text;
                }

                var YoAdm = doAdm.Value;
                if (string.IsNullOrWhiteSpace(YoAdm))
                {
                    doAdm.Focus();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please select valid date');", true);
                    return;
                }
                else
                {
                    yearOfAdmn = DateTime.Parse(YoAdm);
                }

                var YoComptn = doEnd.Value;
                if (string.IsNullOrWhiteSpace(YoComptn))
                {
                    doEnd.Focus();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please select valid date');", true);
                    return;
                }
                else
                {
                    yrofCompln = DateTime.Parse(YoComptn);
                }

                if (string.IsNullOrWhiteSpace(txtMarks.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please Fill in your marks');", true);
                    txtMarks.Focus();
                    txtMarks.BorderColor = Color.Red;
                    return;
                }
                else
                {
                    marks = Convert.ToInt32(txtMarks.Text);
                }
                if (string.IsNullOrWhiteSpace(txtTotalMarks.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please Fill in total marks');", true);
                    txtTotalMarks.Focus();
                    txtTotalMarks.BorderColor = Color.Red;
                    return;
                }
                else
                {
                    ttMarks = Convert.ToInt32(txtTotalMarks.Text);
                }

                var grdPhne = txtGuardianPhone.Text.Trim();
                var mobileBuilder = new StringBuilder(grdPhne);
                mobileBuilder.Remove(0, 1); //Trim one character from position 1
                mobileBuilder.Insert(0, "+254"); // replace position 0 with +254
                grdPhne = mobileBuilder.ToString();

                try
                {
                    if (yrofCompln < yearOfAdmn)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                            "alert('Admision Date should be later than Completion Date!');", true);
                        doEnd.Value = "";
                        doAdm.Value = "";
                        return;
                    }
                }
                catch (Exception ec)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('Please select correct dates!');", true);

                }

                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                        ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                    var sup = new Portals();
                    sup.Credentials = credentials;
                    sup.PreAuthenticate = true;
                    if (sup.FnEducatBg(usname, primo, seco, uni , yearOfAdmn, YroStd, yrofCompln, grdPhne, grdEmail,
                        grdAddr, "", marks, ttMarks, "", optionIs) == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                            "alert('Your Education Data Updated Successfully!');", true);
                       // readEducBgData();
                    }


                }
                catch (Exception exc)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisBitchcode",
                        "alert('" + exc.Message + "');", true);
                }
            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert(ex.Message);
            }
        }
    }
}
