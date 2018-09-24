using System;
using System.Drawing;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCDF_P.NavOData;
using KCDF_P.NAVWS;

namespace KCDF_P
{
    public partial class Dashboard : System.Web.UI.Page
    {
        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"],
                          ConfigurationManager.AppSettings["DOMAIN"])
        };

       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                NoCache();
                if (!IsPostBack)
                {
                    if (Session["username"] == null)
                    {
                        Response.Redirect("~/Default.aspx");

                    }
                    GetMemberDetails();
                    LoadProfPic();
                    LoadMyApplications();
                }
            }
        }
        public void NoCache()
        {
            Response.CacheControl = "private";
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }
        protected void GetMemberDetails()
        {
            var objStudents = nav.studentsRegister.Where(r => r.Username == User.Identity.Name).FirstOrDefault();
            if (objStudents != null)
            {
                mykcdfNumber.InnerHtml = objStudents.No;
                string fName = objStudents.First_name;
                string  mName = objStudents.Middle_name;
                string lName = objStudents.Last_name;
                myEmail.InnerHtml = objStudents.Email;
                myphoneNo.InnerHtml = objStudents.Phone_Number;
                myidNo.InnerHtml = objStudents.ID_No;
                myaddress.InnerHtml = objStudents.Residence;
                DateTime db = Convert.ToDateTime(objStudents.Date_of_Birth);
                mydoB.InnerHtml = db.ToShortDateString();
                mygender.InnerHtml = objStudents.Gender;
                myprimo.InnerHtml = objStudents.Primary_School;
                myseco.InnerHtml = objStudents.Secondary_School;
                myuniver.InnerHtml = objStudents.University_or_College;
                DateTime yoa = Convert.ToDateTime(objStudents.Year_of_Admission);
                myyrofAdm.InnerHtml = yoa.ToShortDateString();
                DateTime yroc = Convert.ToDateTime(objStudents.Year_of_Completion);
                myyrofcompletion.InnerHtml = yroc.ToShortDateString();
                myyrofstudy.InnerHtml = objStudents.Year_of_Study;
                mycourse.InnerHtml = objStudents.Course;
                myfullname.InnerHtml = fName + " " + mName + " " + lName;
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
                   profPic.ImageUrl = "ProfilePics/Scholarship/" + pic;
                   HttpResponse.RemoveOutputCacheItem("/Dashboard.aspx");
                   
                }
            }
            catch (Exception ex)
            {
              
            }
           
        }

        protected void CheckExtension()
        {
            string namepart = Session["username"].ToString();
            string uploadsFolder = Request.PhysicalApplicationPath + "ProfilePics\\";
           
            DirectoryInfo filepath = new DirectoryInfo(uploadsFolder);

            var srcQ = filepath.GetFiles("*" + namepart, SearchOption.AllDirectories);
            foreach (var fln in srcQ.OrderByDescending(f=>f.CreationTime).Skip(1))
            {
              
            }

            //lblimg.Text = filepath.ToString();
            //FileInfo[] flInf = filepath.GetFiles("*" + namepart + ".");
            //foreach (FileInfo gotcha in flInf)
            //{
            //  string img = gotcha.FullName;

            //}
        }
        protected void btnUploadPic_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void ToPNG()
        {
            string extn = ".jpg";
            string uploadsFolder = Request.PhysicalApplicationPath + "ProfilePics\\";
            string filenameO = User.Identity.Name + extn;
            System.Drawing.Image image = System.Drawing.Image.FromFile(uploadsFolder + filenameO);
            image.Save(uploadsFolder + "dre2.png", System.Drawing.Imaging.ImageFormat.Png);
            KCDFAlert.ShowAlert("Converted to png");
        }

        protected void tblmyApplications_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var del_id = tblmyApplications.DataKeys[e.RowIndex].Values[0].ToString();
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                Portals sup = new Portals();
                sup.Credentials = credentials;
                sup.PreAuthenticate = true;
                if (sup.FnDeleteScholarship(del_id) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alert('Application Entry deleted successfully!');", true);
                    LoadMyApplications();
                }
               
            }
            catch (Exception ex)
            {
                //KCDFAlert.ShowAlert(ex.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alert('"+ ex.Message + "');", true);
                LoadMyApplications();
            }
        }

        protected void LoadMyApplications()
        {
            var myapps =
                nav.scholarshipApplications.ToList().Where(u => u.Student_Username == User.Identity.Name);
            tblmyApplications.AutoGenerateColumns = false;
            tblmyApplications.DataSource = myapps;
            tblmyApplications.DataBind();
        }
    }
}