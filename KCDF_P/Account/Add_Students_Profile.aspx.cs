using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
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
        public static readonly string strSQLConn = @"Server=" + ConfigurationManager.AppSettings["DB_INSTANCE"] + ";Database=" +
                                  ConfigurationManager.AppSettings["DB_NAME"] + "; User ID=" +
                                  ConfigurationManager.AppSettings["DB_USER"] + "; Password=" +
                                  ConfigurationManager.AppSettings["DB_PWD"] + "; MultipleActiveResultSets=true";

        public static string Company_Name = "KCDF TEST NEW";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ReturnStudent();
                checkSessionExists();
                readData();
                readEducBgData();
                loadRefs();
                loadAllMySchools();
               
            }
        }

        protected void checkSessionExists()
        {
            var sessionIs = Session["username"];
            if (sessionIs==null)
            {
                Response.Redirect("/Default.aspx");
            }
            
        }
        protected Students ReturnStudent()
        {

            return new Students(Session["username"].ToString());
        }

        //protected void CustomddlPrimoDataBound()
        //{
        //    ddlPrimo.Items.Insert(0, new ListItem("..Select Primary School..", ""));
        //    ddlPrimo.SelectedIndex = 0;
        //}
        //protected void CustomddlSecoDataBound()
        //{
        //    ddlSeco.Items.Insert(0, new ListItem("..Select Secondary School..", ""));
        //    ddlSeco.SelectedIndex = 0;
        //}
        //protected void CustomddlUnivCollgDataBound()
        //{
        //    ddlUnivCollg.Items.Insert(0, new ListItem("..Select University/College..", ""));
        //    ddlUnivCollg.SelectedIndex = 0;
        //}
        protected void editProfile()
        {
            try
            {
                var MobileString = txtPhoneNo.Text.Trim();
                var mobileBuilder = new StringBuilder(MobileString);
                mobileBuilder.Remove(0, 1); //Trim one character from position 1
                mobileBuilder.Insert(0, "+254"); // replace position 0 with +254
                MobileString = mobileBuilder.ToString();

                var gentype = 0;
                var gender = lstGender.SelectedItem.Text;

                if (gender.Equals("..Select Gender.."))
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
                var dtofBirth = dateOFBirth.Value.Trim();
                var dTOfBth = Convert.ToDateTime(dtofBirth);

                var usname = Session["username"].ToString();
                var fname = txtfNname.Text.Trim();
                var mname = txtMname.Text.Trim();
                var lname = txtLname.Text.Trim();
                var idno = txtIDNo.Text.Trim();
                var resid = txtResidence.Text.Trim();
                int marks = Convert.ToInt32(txtMarks.Text);
                int ttMarks = Convert.ToInt32(txtTotalMarks.Text);
                var mygradeIs = Session["myGrade"].ToString();

                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                var sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;

                if (string.IsNullOrEmpty(idno))
                {
                    KCDFAlert.ShowAlert("Fill in ID Number!");
                }
                if (string.IsNullOrEmpty(MobileString))
                {
                    KCDFAlert.ShowAlert("Fill in Mobile Number!");
                }
                else
                {
                    sup.FnRegisterStudent(fname, mname, lname, idno, resid, MobileString, usname, gentype, dTOfBth,marks.ToString(),mygradeIs);
                    KCDFAlert.ShowAlert("Your account succcessfully Edited");
                }
            }
            catch (Exception ex)
            {
                KCDFAlert.ShowAlert("Select Valid Date");
            }
        }

        protected void btnEditProf_Click(object sender, EventArgs e)
        {
            editProfile();
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
            var marsk = studData.Select(mk => mk.KCPE_Marks).SingleOrDefault();
            var fGrade = studData.Select(grd => grd.KCSE_Grade).SingleOrDefault();
            var dtB = studData.Select(dtoB => dtoB.Date_of_Birth).SingleOrDefault().ToString();
            var dt1 = DateTime.Parse(dtB);
            dateOFBirth.Value = dt1.ToShortDateString();
                txtMarks.Text = marsk;
                rdoBtnListGrade.SelectedValue = fGrade;
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
            var usname = Session["username"].ToString();
            var primo = Session["primarySch"].ToString();
            var seco = ddlSeco.SelectedItem.Text.Trim();
            var uni = ddlUnivCollg.SelectedItem.Text.Trim();
            var course = txtDegree.Text.Trim();
            var YroStd = ddlYearofStudy.Text.Trim();

            var YoAdm = txtYrofAdmsn.Value.Trim();
            var yearOfAdmn = DateTime.Parse(YoAdm);
            var YoComptn = txtYrofCompltn.Value.Trim();
            var yrofCompln = DateTime.Parse(YoComptn);

            var grdEmail = txtGuardianEmail.Text.Trim();
            var grdAddr = txtGuardianAddress.Text.Trim();

            var grdPhne = txtGuardianPhone.Text.Trim();
            var mobileBuilder = new StringBuilder(grdPhne);
            mobileBuilder.Remove(0, 1); //Trim one character from position 1
            mobileBuilder.Insert(0, "+254"); // replace position 0 with +254
            grdPhne = mobileBuilder.ToString();

            try
            {
                if (yrofCompln < yearOfAdmn)
                {
                    KCDFAlert.ShowAlert("Admision Date should be later than Completion Date!");
                    txtYrofAdmsn.Value = "";
                    txtYrofCompltn.Value = "";
                    return;
                }
            }
            catch (Exception ec)
            {
                KCDFAlert.ShowAlert("Please select correct dates!");
            }

            try
            {
                if (txtYrofAdmsn.Value == "" || txtYrofCompltn.Value == "")
                {
                }
                //if (primo == "..Select Primary School..")
                //{
                //    KCDFAlert.ShowAlert("Please Select Valid Primary School!");
                //}
                //if (seco == "..Select Secondary School..")
                //{
                //    KCDFAlert.ShowAlert("Please Select Valid Secondary School!");
                //}
                //if (uni == "..Select University/College..")
                //{
                //    KCDFAlert.ShowAlert("Please Select Valid University/College!");
                //}
                //if (course == "..Select Course/Degree Programme..")
                //{
                //    KCDFAlert.ShowAlert("Please Select Valid Course!");
                //}
                //if (YroStd == "..Select Year of Study..")
                //{
                //    KCDFAlert.ShowAlert("Please Select Valid Year of study!");
                //}
                //if (string.IsNullOrEmpty(YoAdm.ToString()))
                //{
                //    KCDFAlert.ShowAlert("Choose valid year of Admision");
                //}
                //if (string.IsNullOrEmpty(YoComptn.ToString()))
                //{
                //    KCDFAlert.ShowAlert("Choose valid year of Completion");
                //}
                //if (string.IsNullOrEmpty(grdPhne))
                //{
                //    KCDFAlert.ShowAlert("Fill in your Guardian Mobile Number");
                //}
                //if (string.IsNullOrEmpty(grdAddr))
                //{
                //    KCDFAlert.ShowAlert("Fill in your Guardian Address");
                //}
                else
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                        ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                    var sup = new Portals();
                    sup.Credentials = credentials;
                    sup.PreAuthenticate = true;
                    sup.FnEducatBg(usname, primo, seco, uni, yearOfAdmn, YroStd, yrofCompln, grdPhne, grdEmail, grdAddr,
                        course);
                    KCDFAlert.ShowAlert("Your Education Data Updated Successfully");
                    readEducBgData();
                }
            }
            catch (Exception exc)
            {
                KCDFAlert.ShowAlert(exc.Message);
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

                txtSearch.Text = edctnData.Select(pr => pr.Primary_School).SingleOrDefault();
            var secsh = edctnData.Select(se => se.Secondary_School).SingleOrDefault();
            var unv = edctnData.Select(un => un.University_or_College).SingleOrDefault();
                txtDegree.Text = edctnData.Select(fc => fc.Course).SingleOrDefault();
            var yoS = edctnData.Select(yos => yos.Year_of_Study).Single();
            var yoAd = edctnData.Select(yoa => Convert.ToDateTime(yoa.Year_of_Admission)).SingleOrDefault();
            var yoC = edctnData.Select(yoc => Convert.ToDateTime(yoc.Year_of_Completion)).SingleOrDefault();
            txtGuardianEmail.Text = edctnData.Select(em => em.Parent_or_Guardian_Email).SingleOrDefault();
            txtGuardianAddress.Text = edctnData.Select(ad => ad.Parent_or_Guardian_Address).SingleOrDefault();
           

            if (string.IsNullOrWhiteSpace(secsh))
            {
                ddlSeco.SelectedIndex = 0;
            }
            else
            {
                ddlSeco.SelectedItem.Text = secsh;
            }
            if (string.IsNullOrWhiteSpace(unv))
            {
                ddlUnivCollg.SelectedIndex = 0;
            }
            else
            {
                ddlUnivCollg.SelectedItem.Text = unv;
            }
            if (string.IsNullOrWhiteSpace(yoS))
            {
                ddlYearofStudy.SelectedIndex = 0;
            }
            else
            {
                ddlYearofStudy.SelectedItem.Text = yoS;
            }
            if ((yoAd.ToString().Equals(null)))
            {
                txtYrofAdmsn.Value = yoAd.ToShortDateString();
            }
            else
            {
                var dtAd = DateTime.Parse(yoAd.ToString());
                txtYrofAdmsn.Value = dtAd.ToShortDateString();
            }

            if ((yoC.ToString().Equals(null)))
            {
                txtYrofCompltn.Value = yoC.ToShortDateString();
            }
            else
            {
                var dtCmp = DateTime.Parse(yoC.ToString());
                txtYrofCompltn.Value = dtCmp.ToShortDateString();
            }
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
                conn.ConnectionString = strSQLConn;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT [School Name],[Level of School] from [" + Company_Name + "$Schools List] WHERE [School Name] like @SearchText + '%' && [Level of School]=0";
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
            using (SqlConnection con = new SqlConnection(strSQLConn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText=  @"SELECT DISTINCT([School Name]) from [" + Company_Name +
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
            lblValues.Text =  myschoolIs;
            Session["primarySch"] = myschoolIs;
        }
    }
 }
