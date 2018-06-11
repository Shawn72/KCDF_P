using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;


namespace KCDF_P
{
    public partial class EditMyProject : System.Web.UI.Page
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

        public string Company_Name = "KCDF TEST NEW";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                GetSessionDatafromDashbord();
                myCountyIs();

            }

        }

        protected void GetSessionDatafromDashbord()
        {
            lblPrjNm.Text= Session["projectname"].ToString();
            lblProjNo.Text= Session["projectidnumber"].ToString();
            lblRefNo.Text=Session["prjCallrefNo"].ToString();
            loadEditPrj(lblProjNo.Text);
        }
        protected void btnProjEdit_OnClick(object sender, EventArgs e)
        {
            int projectlength = 0;
            DateTime projTDt;
            string county;
            string constituency;
            var pstartD = txtDateofStart.Value.Trim();
            if (string.IsNullOrWhiteSpace(pstartD))
            {
                KCDFAlert.ShowAlert("Please Select project start date!");
                return;
            }
            else
            {
                projTDt = DateTime.Parse(pstartD);
            }
            if (ddlSelCounty.SelectedIndex == 0)
            {
                KCDFAlert.ShowAlert("Please choose the county!");
                return;
            }
            else
            {
                county = ddlSelCounty.SelectedItem.Text;
            }
            if (ddlConstituency.SelectedIndex==0)
            {
              KCDFAlert.ShowAlert("Please select your subcounty");  
                return;
            }
            else
            {
                constituency = ddlConstituency.SelectedItem.Text;
            }
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

        protected void loadEditPrj(string projNumber)
        {
            var ldPrj = nav.projectOverview.ToList().Where(sn => sn.No == projNumber);
            TextBoxtitle.Text = ldPrj.Select(t => t.Project_Title).SingleOrDefault();

            var dtB = ldPrj.Select(dtoB => dtoB.Project_Start_Date).SingleOrDefault().ToString();
            DateTime dt1 = DateTime.Parse(dtB);
            txtDateofStart.Value = dt1.ToShortDateString();
           // ddlSelCounty.SelectedItem.Text = ldPrj.Select(c => c.County).SingleOrDefault();
           // ddlConstituency.SelectedItem.Text = ldPrj.Select(cn => cn.Constituency).SingleOrDefault();
            txtAreaTargetSettmnt.Text = ldPrj.Select(ta => ta.Urban_Settlement_Target).SingleOrDefault();
            ddlMonths.SelectedItem.Text = ldPrj.Select(mt => mt.Project_Length_Months).SingleOrDefault().ToString();
            ddlEstScale.SelectedItem.Text = ldPrj.Select(es => es.Grant_Scale).SingleOrDefault();
            TextBoxcost.Text = ldPrj.Select(cs => cs.Total_Project_Cost_KES).SingleOrDefault().ToString();
            TextBoxcont.Text = ldPrj.Select(cd => cd.Your_Cash_Contribution_KES).SingleOrDefault().ToString();
            TextBoxrequested.Text = ldPrj.Select(rq => rq.Requested_KCDF_Amount_KES).SingleOrDefault().ToString();

        }

        protected void myCountyIs()
        {
            var mycounty = nav.mycountyIs.ToList();
            ddlSelCounty.DataSource = mycounty;
            ddlSelCounty.DataTextField = "County_Name";
            ddlSelCounty.DataValueField = "County_Code";
            ddlSelCounty.DataBind();
            ddlSelCounty.Items.Insert(0, "--Select your County--");

            //ddlmycountyIS.DataSource = mycounty;
            //ddlmycountyIS.DataTextField = "County_Name";
            //ddlmycountyIS.DataValueField = "County_Code";
            //ddlmycountyIS.DataBind();
            //ddlmycountyIS.Items.Insert(0, "--Select your County--");
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
                    ddlConstituency.Enabled = true;
                    ddlConstituency.DataSource = subCnty;
                    ddlConstituency.DataTextField = "Sub_County_Name";
                    ddlConstituency.DataValueField = "Sub_County_Name";
                    ddlConstituency.DataBind();
                    ddlConstituency.Items.Insert(0, "--Select your Sub County--");
                    KCDFAlert.ShowAlert(sbCntysplit00);
                    break;
            }

        }

    }
}