using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace KCDF_P
{
    public class KCDFAlert
    {
        public static void ShowAlert(Page currentPage, string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("');");
            currentPage.ClientScript.RegisterStartupScript(typeof(KCDFAlert), "showalert", sb.ToString(), true);
        }

        public static void ShowAlert(string message)
        {
            Page currentPage = HttpContext.Current.Handler as Page;
            if (currentPage != null)
                ShowAlert(currentPage, message);
        }
    }
}