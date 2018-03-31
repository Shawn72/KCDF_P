using System;
using System.Drawing;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCDF_P.NavOData;

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
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");

                }
                returnCustomer();
                loadProfPic();

                // checkUser();
                // checkExtension();

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
                    profPic.ImageUrl = "ProfilePics/"+pic;
                   // KCDFAlert.ShowAlert("ProfilePics/ "+pic);
                }
            }
            catch (Exception ex)
            {
                
              
            }
           
        }
        protected Students returnCustomer()
        {
            return new Students(Session["username"].ToString());
        }
        protected void checkUser()
        {
            var edituser = nav.studentsRegister.ToList().Where(r => r.Username == Session["username"].ToString()).Select(r => r.First_name).SingleOrDefault();
            //HotelFactory.ShowAlert("User: "+edituser);

            if (string.IsNullOrEmpty(edituser))
            {
                KCDFAlert.ShowAlert("null user");
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

        }

        protected void checkExtension()
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
            string filenameO = Students.Username + extn;
            System.Drawing.Image image = System.Drawing.Image.FromFile(uploadsFolder + filenameO);
            image.Save(uploadsFolder + "dre2.png", System.Drawing.Imaging.ImageFormat.Png);
            KCDFAlert.ShowAlert("Converted to png");
        }
        
    }
}