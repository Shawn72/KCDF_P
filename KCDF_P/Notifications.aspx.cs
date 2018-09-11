using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCDF_P.NavOData;
using Microsoft.Ajax.Utilities;

namespace KCDF_P
{
    public partial class Notifications : System.Web.UI.Page
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
                //    if (Session["username"] == null)
                //    {
                //        Response.Redirect("~/Default.aspx");
                //    }

                GetMeAllNotifics();
            }
        }

        protected void myNotificationTabs_OnMenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            notifyPMultiview.ActiveViewIndex = index;
        }

        protected void GetMeAllNotifics()
        {
            try
            {
                var myNots = nav.tasks.ToList().Where(un => un.User_Number == Grantees.No && un.Condition_FullFilled == false);
                tblPendingTasks.AutoGenerateColumns = false;
                tblPendingTasks.DataSource = myNots;
                tblPendingTasks.DataBind();



                var myNotsSorted = nav.tasks.ToList().Where(un => un.User_Number == Grantees.No && un.Condition_FullFilled==true);
                tblCompletedTasks.AutoGenerateColumns = false;
                tblCompletedTasks.DataSource = myNotsSorted;
                tblCompletedTasks.DataBind();
            }
            catch (Exception xc)
            {
                
            }
            
        }

        protected void lnkReact_OnClick(object sender, EventArgs e)
        {
            var entryId = (sender as LinkButton).CommandArgument;
            TaskType(entryId);
        }

    protected void TaskType(string taskentryNo)
        {
            var typeOpt =
                nav.tasks.ToList().Where(n => n.Entry_No == taskentryNo).Select(op => op.Task_Type).SingleOrDefault();

            switch (typeOpt)
            {
                case "Narrative":
                    Session["typeoftask"] = "Narrative";
                    Session["tasknumber"] = taskentryNo;
                    Response.Redirect("Report_Form.aspx");
                    break;

                case "Financial":
                    Session["typeoftask"] = "Financial";
                    Session["tasknumber"] = taskentryNo;
                    Response.Redirect("Report_Form.aspx");
                    break;

                case "Data":
                    Session["typeoftask"] = "Data";
                    Session["tasknumber"] = taskentryNo;
                    Response.Redirect("Report_Form.aspx");
                    break;

                case "Indicator Matrix":
                    Session["typeoftask"] = "Indicator Matrix";
                    Session["tasknumber"] = taskentryNo;
                    Response.Redirect("UploadFiles_Grants.aspx");
                    break;

                case "POCA Tool":
                    Session["typeoftask"] = "POCA Tool";
                    Session["tasknumber"] = taskentryNo;
                    Response.Redirect("UploadFiles_Grants.aspx");
                    break;

                case "Other":
                    Session["typeoftask"] = "Other";
                    Session["tasknumber"] = taskentryNo;
                    Response.Redirect("Report_Form.aspx");
                    break;

            }

        }
    }
}