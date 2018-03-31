using System;
using System.Collections.Generic;
using System.Configuration;
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
                readData();
                loadRefs();
                readEducBgData();
                loadScholarData();
                loadProjectWorkplan();
                getScholarship();
            }

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
            dateofBirth.Value = dt1.ToShortDateString();
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
            txtCollege.Text = unv;
            txtDegree.Text = faclty;
            txtYearofStudy.Text = yoS;
            txtAdmittedWhen.Text = yoAd.ToShortDateString();
            txtYearofCompltn.Text = yoC.ToShortDateString();
           
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
            ddlScolarshipType.DataValueField = "Project";
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
            var studnumber = nav.studentsRegister.ToList().Where(sN => sN.Username == userNm).Select(no => no.No).SingleOrDefault();
            string objtv = txtObj.Text.Trim();
            string actvty = txtActivity.Text.Trim();
            string actvitytarget = txtTargets.Text.Trim();
            string meanofVer = txtMeansofVer.Text.Trim();
            int timeFrme = 0;
            decimal amnt = 0;
            string scholname = ddlScolarshipType.SelectedItem.Text;
            if (!string.IsNullOrEmpty(txtTimeFrame.Text))
            {
                timeFrme= Int32.Parse(txtTimeFrame.Text);
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
            naws.FnAddWorkplanItem(userNm, studnumber, objtv, actvty, actvitytarget, meanofVer, amnt, timeFrme, scholname);
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
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
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
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            sup.FnDelWorkplan(del_id);
            loadProjectWorkplan();
            KCDFAlert.ShowAlert("Workplan deleted successfully");
        }
    }
}