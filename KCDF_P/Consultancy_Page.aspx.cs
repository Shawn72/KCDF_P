using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;

namespace KCDF_P
{
    public partial class Consultancy_Page : System.Web.UI.Page
    {
        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
             new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                 ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };
        public  static readonly string strSQLConn = @"Server=" + ConfigurationManager.AppSettings["DB_INSTANCE"] + ";Database=" +
                                  ConfigurationManager.AppSettings["DB_NAME"] + "; User ID=" +
                                  ConfigurationManager.AppSettings["DB_USER"] + "; Password=" +
                                  ConfigurationManager.AppSettings["DB_PWD"] + "; MultipleActiveResultSets=true";

        public static string Company_Name = "KCDF TEST NEW";


        [STAThread]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");

                }
                loadProfPic();
                returnConsultancy();
                getPostaCodes();
                checkSessX();
            }
            LoadMYProfile();
        }

        public class DropdownTest
        {
            public string postaCode { get; set;}
            public string postaTown { get; set; }
        }

        public void checkSessX()
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

        protected ConsultantClass returnConsultancy()
        {
            return new ConsultantClass(Session["username"].ToString());
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
                    profPic.ImageUrl = "ProfilePics/Consultants/" + pic;
                    HttpResponse.RemoveOutputCacheItem("/Consultancy_Page.aspx");
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

        protected void rdoBtnListYesNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["yesOrno"] = rdoBtnListYesNo.SelectedIndex;
            int selindex = Convert.ToInt32(Session["yesOrno"].ToString());
            switch (selindex)
            {
                case 0:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "IfYesIts()", true);
                    //KCDFAlert.ShowAlert("clicked: "+ selindex);
                    break;
                default:
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('I Haven't Consulted Yet!');", true);
                    break;
            }

        }

        protected void rdBtnStatus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["kcdfPstatus"] = rdBtnStatus.SelectedItem.Text;
        }

        protected void ddlPostalCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var pCode = ddlPostalCode.SelectedItem.Text;
            var postaTown =
                nav.list_myPosta.ToList()
                    .Where(cd => cd.Postal_Code == pCode)
                    .Select(pT => pT.Postal_Town)
                    .SingleOrDefault();
            txtPostalTown.Text = postaTown;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "pageLoad();", true);
        }

        //protected void OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string message = ddlFruits.SelectedItem.Text + " - " + ddlFruits.SelectedItem.Value;
        //    ScriptManager.RegisterStartupScript((sender as Control), this.GetType(), "alert", "alert('" + message + "');", true);
           
        //}

        protected void getPostaCodes()
        {
            try
            {
                var posta = nav.list_myPosta.ToList();
                ddlPostalCode.DataSource = posta;
                ddlPostalCode.DataTextField = "Postal_Code";
                ddlPostalCode.DataValueField = "Postal_Code";
                ddlPostalCode.DataBind();
                ddlPostalCode.Items.Insert(0, "--Select Postal Code--");

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('Your Postal code is not updated, Please update!!!');", true);

            }

        }

        [WebMethod]
        public static List<DropdownTest> GetPostalCodes()
        {
            DataTable dt = new DataTable();
            List<DropdownTest> postas = new List<DropdownTest>();

            string query = @"SELECT [Postal Code], [Postal Town]  FROM [" + Company_Name + "$Postal Codes]";
            string constr = strSQLConn;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            postas.Add(new DropdownTest
                            {
                                postaCode = dt.Rows[i]["Postal Code"].ToString(),
                                postaTown = dt.Rows[i]["Postal Town"].ToString()
                             }
                           );
                        }
                    }
                    con.Close();
                    return postas;
                }
            }
        }

        protected void EditConsultant()
        {
            try
            {
                var usnM = Session["username"].ToString();
                var regiNM = ConsultantClass.IDNoReg;
                var namecontactPs = TextBxcont.Text;
                var currPostn = TextBoposition.Text;
                var postTow = txtPostalTown.Text;
                var PhoneN = TextBoxphone.Text;
                var siteonWeb = TextBoxweb.Text;
                var postaAddress = TextBxpostalAdd.Text;
                var postCode = "";
                bool kcdfb4 = false;
                int statusofProject = 0;

                int TorF = rdoBtnListYesNo.SelectedIndex;
                switch (TorF)
                {
                    case 0:
                        kcdfb4 = true;
                        break;
                    case 1:
                        kcdfb4 = false;
                        statusofProject = 0;
                        break;
                    default:
                        KCDFAlert.ShowAlert("Please select Yes or No!");
                        break;
                }

                int statS = rdBtnStatus.SelectedIndex;
                switch (statS)
                {
                    case 0:
                        statusofProject = Convert.ToInt32(rdBtnStatus.SelectedValue);
                        break;
                    case 1:
                        statusofProject = Convert.ToInt32(rdBtnStatus.SelectedValue);
                        break;
                    case 2:
                        statusofProject = Convert.ToInt32(rdBtnStatus.SelectedValue);
                        break;
                    default:
                        statusofProject =0;
                        break;
                }
                if (ddlPostalCode.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('Please select Postal Code!!!');", true);
                    ddlPostalCode.Focus();
                    return;
                }
                else
                {
                    postCode = ddlPostalCode.SelectedItem.Text;
                }

                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                if (sup.FnEditConsultant(usnM, regiNM, PhoneN, namecontactPs, currPostn, postCode, postTow, postaAddress, kcdfb4, statusofProject, siteonWeb)==true)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itsABitch", "alert('Your Profile Edited Successfully!!');", true);
                    LoadMYProfile();
                }

            }
            catch (Exception Ec)
            {
                KCDFAlert.ShowAlert(Ec.Message);
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            EditConsultant();
        }

        protected void LoadMYProfile()
        {
            var myusername = Session["username"].ToString();
            var regIdN = ConsultantClass.IDNoReg;

            var edPrF =
                nav.myConsultants.ToList()
                    .Where(pc => pc.Organization_Username == myusername && pc.Organization_Registration_No == regIdN);

            TextBxcont.Text = edPrF.Select(cn => cn.Contact_Person_FullName).SingleOrDefault();
            TextBoposition.Text = edPrF.Select(pt => pt.Contact_Person_Position).SingleOrDefault();
            TextBxpostalAdd.Text = edPrF.Select(ad => ad.Postal_Address).SingleOrDefault();
            txtMyPostaIs.Text = edPrF.Select(pad => pad.Postal_Code).SingleOrDefault();
            txtPostalTown.Text = edPrF.Select(pTn => pTn.Postal_Town).SingleOrDefault();
            TextBoxphone.Text = edPrF.Select(pn => pn.Phone_Number).SingleOrDefault();
            TextBoxweb.Text = edPrF.Select(wb => wb.Website_Address).SingleOrDefault();

            bool yoN = edPrF.Select(yon => Convert.ToBoolean(yon.ConsultedForKCDFB4)).SingleOrDefault();
            switch (yoN)
            {
                case true:
                    rdoBtnListYesNo.SelectedIndex =0;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "IfYesIts()", true);
                    break;
                case false:
                    rdoBtnListYesNo.SelectedIndex=1;
                    break;
            }
            var statusIs = edPrF.Select(st =>st.Statusof_Project_Consulted).SingleOrDefault();
            switch (statusIs)
            {
                case "Never Consulted":
                    rdBtnStatus.SelectedIndex = -1;
                    break;
                case "Ongoing":
                    rdBtnStatus.SelectedIndex=0;
                    break;
                case "Complete":
                    rdBtnStatus.SelectedIndex = 1;
                    break;
                case "Terminated":
                    rdBtnStatus.SelectedIndex = 2;
                    break;
            }
        }
    }

}