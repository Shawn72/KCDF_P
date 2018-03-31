using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;

namespace KCDF_P.Account
{
    public partial class Add_Students_Profile : System.Web.UI.Page
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
                readData();
                readEducBgData();
                loadRefs();

            }
        }

        protected void CustomddlPrimoDataBound()
        {
            ddlPrimo.Items.Insert(0, new ListItem("..Select Primary School..", ""));
            ddlPrimo.SelectedIndex = 0;
        }
        protected void CustomddlSecoDataBound()
        {
            ddlSeco.Items.Insert(0, new ListItem("..Select Secondary School..", ""));
            ddlSeco.SelectedIndex = 0;
        }
        protected void CustomddlUnivCollgDataBound()
        {
            ddlUnivCollg.Items.Insert(0, new ListItem("..Select University/College..", ""));
            ddlUnivCollg.SelectedIndex = 0;
        }
        protected void editProfile()
        {
            try
            {
            var MobileString = txtPhoneNo.Text.Trim();
            var mobileBuilder = new StringBuilder(MobileString);
            mobileBuilder.Remove(0, 1); //Trim one character from position 1
            mobileBuilder.Insert(0, "+254"); // replace position 0 with +254
            MobileString = mobileBuilder.ToString();

            int gentype = 0;
            string gender = lstGender.SelectedItem.Text;

            if (gender.Equals("..Select Gender.."))
            {
                KCDFAlert.ShowAlert("Please select a valid gender type!");
                return;
                
            }
            else if (gender.Equals("Male"))
            {
                gentype = 0;
            }
            else
            {
                gentype = 1;
            }
            var dtofBirth = dateOFBirth.Value.Trim();
            DateTime dTOfBth = Convert.ToDateTime(dtofBirth);

            string usname = Session["username"].ToString();
            string fname = txtfNname.Text.Trim();
            string mname = txtMname.Text.Trim();
            string lname = txtLname.Text.Trim();
            string idno = txtIDNo.Text.Trim();
            string resid = txtResidence.Text.Trim();

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
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
                    sup.FnRegisterStudent(fname, mname, lname, idno, resid, MobileString, usname, gentype, dTOfBth);
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
            // txtPhoneNo.Text = studData.Select(pn => pn.Phone_Number).SingleOrDefault().ToString();
                txtfNname.Text = studData.Select(fn => fn.First_name).SingleOrDefault().ToString();
                txtMname.Text = studData.Select(mn => mn.Middle_name).SingleOrDefault().ToString();
                txtLname.Text = studData.Select(ln => ln.Last_name).SingleOrDefault().ToString();
                txtResidence.Text = studData.Select(r => r.Residence).SingleOrDefault().ToString();
                txtIDNo.Text = studData.Select(id => id.ID_No).SingleOrDefault().ToString();
                txtEmailAdd.Text = studData.Select(em => em.Email).SingleOrDefault().ToString();
                var dtB = studData.Select(dtoB => dtoB.Date_of_Birth).SingleOrDefault().ToString();
                DateTime dt1 = DateTime.Parse(dtB);
                dateOFBirth.Value = dt1.ToShortDateString();
                var gent = studData.Select(g => g.Gender).SingleOrDefault().ToString();
                if (gent == "Male")
                {
                    lstGender.SelectedIndex = 1;
                }
                else
                {
                    lstGender.SelectedIndex = 2;
                }
            
            
        }

       

        protected void studentInfoMenu_OnMenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            profileMultiview.ActiveViewIndex = index;
        }

        protected void btnEditEductn_OnClick(object sender, EventArgs e)
        {
            editEducation();
        }

        protected void editEducation()
        {
            string usname = Session["username"].ToString();
            string primo = ddlPrimo.Text.Trim();
            string seco = ddlSeco.Text.Trim();
            string uni = ddlUnivCollg.Text.Trim();
            string course = ddlDegree.Text.Trim();
            string YroStd = ddlYearofStudy.Text.Trim();

            var YoAdm = txtYrofAdmsn.Value.Trim();
            DateTime yearOfAdmn = DateTime.Parse(YoAdm);
            var YoComptn = txtYrofCompltn.Value.Trim();
            DateTime yrofCompln = DateTime.Parse(YoComptn);

            string grdEmail = txtGuardianEmail.Text.Trim();
            string grdAddr = txtGuardianAddress.Text.Trim();

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
                return;
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
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                sup.FnEducatBg(usname, primo, seco, uni, yearOfAdmn, YroStd,  yrofCompln, grdPhne, grdEmail, grdAddr, course);
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
            var pry = edctnData.Select(pr => pr.Primary_School).SingleOrDefault();
            var secsh = edctnData.Select(se => se.Secondary_School).SingleOrDefault();
            var unv = edctnData.Select(un => un.University_or_College).SingleOrDefault();
            var faclty = edctnData.Select(fc => fc.Course).SingleOrDefault();
            var yoS = edctnData.Select(yos => yos.Year_of_Study).Single();
            var yoAd = edctnData.Select(yoa => Convert.ToDateTime(yoa.Year_of_Admission)).SingleOrDefault();
            var yoC = edctnData.Select(yoc => Convert.ToDateTime(yoc.Year_of_Completion)).SingleOrDefault();
            txtGuardianEmail.Text = edctnData.Select(em => em.Parent_or_Guardian_Email).SingleOrDefault();
            txtGuardianAddress.Text = edctnData.Select(ad => ad.Parent_or_Guardian_Address).SingleOrDefault();

            if (string.IsNullOrEmpty(pry))
            {
                ddlPrimo.SelectedIndex = 0;
            }
            else
            {
                ddlPrimo.SelectedItem.Text = pry;
            }
            if (string.IsNullOrEmpty(secsh))
            {
                ddlSeco.SelectedIndex = 0;
            }
            else
            {
                ddlSeco.SelectedItem.Text = secsh;
            }
            if (string.IsNullOrEmpty(unv))
            {
                ddlUnivCollg.SelectedIndex = 0;
            }
            else
            {
                ddlUnivCollg.SelectedItem.Text = unv;
            }
            if (string.IsNullOrEmpty(faclty))
            {
                ddlDegree.SelectedIndex = 0;
            }
            else
            {
                ddlDegree.SelectedItem.Text = faclty;
            }
            if (string.IsNullOrEmpty(yoS))
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
                DateTime dtAd = DateTime.Parse(yoAd.ToString());
                txtYrofAdmsn.Value = dtAd.ToShortDateString();
            }

            if ((yoC.ToString().Equals(null)))
            {
                txtYrofCompltn.Value = yoC.ToShortDateString();
            }
            else
            {
                DateTime dtCmp = DateTime.Parse(yoC.ToString());
                txtYrofCompltn.Value = dtCmp.ToShortDateString();
            }
        }

        protected void btnAddRefer_OnClick(object sender, EventArgs e)
        {
            string usernaMe = Session["username"].ToString();
            var studNo =
                nav.studentsRegister.ToList()
                    .Where(s => s.Username == Session["username"].ToString())
                    .Select(sn => sn.No)
                    .SingleOrDefault();

            string refFname = txtrefFname.Text.Trim();
            string refmname = refMidName.Text.Trim();
            string refLname = refLName.Text.Trim();
            string refemail = refEmail.Text.Trim();
            var ferPhneNo = refMobile.Text.Trim();
            var mobileBuilder = new StringBuilder(ferPhneNo);
            mobileBuilder.Remove(0, 1); //Trim one character from position 1
            mobileBuilder.Insert(0, "+254"); // replace position 0 with +254
            ferPhneNo = mobileBuilder.ToString();
            try
            {
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
               ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals nws = new Portals();
                nws.Credentials = credentials;
                nws.PreAuthenticate = true;

                nws.FnAddReferee(usernaMe,studNo, refFname, refmname, refLname, ferPhneNo, refemail);
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
                Portals nws = new Portals();
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
    }
}