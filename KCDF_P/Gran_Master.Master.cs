﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KCDF_P
{
    public partial class Gran_Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkBtnChangeP_OnClick(object sender, EventArgs e)
        {
            Session["userType"] = "grantee";
            Response.Redirect("PasswordReset.aspx");
        }
    }
}