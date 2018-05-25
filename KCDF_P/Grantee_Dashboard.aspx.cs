using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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

        public string Company_Name = "KCDF TEST NEW";
        [STAThread]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");

                }
                returnGrantee();
                loadMyProjects();
               // loadAllcountries();
                loadProfPic();
                clearCache();
            }

        }
        protected Grantees returnGrantee()
        {
            return new Grantees(Session["username"].ToString());
        }


        protected void clearCache()
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        protected void loadMyProjects()
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
        protected void loadProfPic()
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
                    profPic.ImageUrl = "ProfilePics/" + pic;
                    HttpResponse.RemoveOutputCacheItem("/Grantee_Dashboard.aspx");
                    // KCDFAlert.ShowAlert("ProfilePics/ "+pic);
                }
            }
            catch (Exception ex)
            {


            }

        }
        protected void btnUploadPic_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
                loadMyProjects();
                KCDFAlert.ShowAlert("Project Deleted Successfully!");
            }
            catch (Exception ex)
            {
               KCDFAlert.ShowAlert(ex.Message); 
            }
          
        }

        protected void lnkEdit_OnClick(object sender, EventArgs e)
        {
            string edit_id = (sender as LinkButton).CommandArgument;
            var prjnm =
                nav.projectOverview.ToList().Where(i => i.No == edit_id).Select(pn => pn.Project_Name).SingleOrDefault();
            var approvedyeah =
                nav.projectOverview.ToList()
                    .Where(a => a.No == edit_id)
                    .Select(ast => ast.Submission_Status)
                    .SingleOrDefault();

            switch (approvedyeah)
            {
                case "Approved":
                    KCDFAlert.ShowAlert("You cannot Edit an Appproved submission!!");
                    break;

                case "Pending Approval":
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openEditProj();", true);
                    lblPrjNm.Text = prjnm;
                    lblProjNo.Text = edit_id;
                    loadEditPrj(edit_id);
                    break;

                case "Incomplete":
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openEditProj();", true);
                    lblPrjNm.Text = prjnm;
                    lblProjNo.Text = edit_id;
                    loadEditPrj(edit_id);
                    break;
            }
        }

        protected void refreSH()
        {
            HttpResponse.RemoveOutputCacheItem("/Grantee_Dashboard.aspx");
            //  Response.Redirect(Request.RawUrl);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
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

        protected void loadEditPrj(string projNumber)
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
                string uploadsFolder = Request.PhysicalApplicationPath + "Uploaded Documents/" + Grantees.No + @"\";
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
        
        //protected void loadAllcountries() 
        //{
        //    var myList = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
        //        .Select(c => new RegionInfo(c.Name).EnglishName)
        //        .Distinct().OrderBy(s => s).ToList();
        //    ddlCountry.DataSource = myList;
        //    ddlCountry.DataBind();
        //}
    }
}