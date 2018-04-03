﻿using System;
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

namespace KCDF_P.Account
{
    public partial class Add_Grantee_Profile : System.Web.UI.Page
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
                loadApplicationInfo();
            }
            
        }
        protected void editProfile()
        {
            var MobileString = txPhoneNo.Text.Trim();
            var mobileBuilder = new StringBuilder(MobileString);
            mobileBuilder.Remove(0, 1); //Trim one character from position 1
            mobileBuilder.Insert(0, "+254"); // replace position 0 with +254
            MobileString = mobileBuilder.ToString();

            string usname = Session["username"].ToString();
            string coName = txOrgname.Text.Trim();
            string eAddr = txEmailAdd.Text.Trim();

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;

            // sup.FnRegisterStudent(fname, mname, lname, idno, resid, MobileString, usname, gentype);

            KCDFAlert.ShowAlert("Your account succcessfully Edited");

        }

        protected void btnEditProf_Click(object sender, EventArgs e)
        {
            editProfile();
        }

        protected void readData()
        {

            var granteeData = nav.grantees_Register.ToList().Where(r => r.Organization_Username == Session["username"].ToString());
            
            txPhoneNo.Text = granteeData.Select(pn => pn.Phone).SingleOrDefault().ToString();
            txOrgname.Text = granteeData.Select(fn => fn.Organization_Name).SingleOrDefault().ToString();
            txEmailAdd.Text = granteeData.Select(em => em.Email).SingleOrDefault().ToString();


        }

        protected void OrgInfoMenu_OnMenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            orgPMultiview.ActiveViewIndex = index;
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            bool ngo = false, notpartisan = false, nonprofit = false, legally = false;

            string usanm = Session["username"].ToString();
            string contactP = TextBxcont.Text.Trim();
            string currposition = TextBoposition.Text.Trim();
            string postaddress = TextBxpostal.Text.Trim();
            string postcode = ddlPostalCode.SelectedItem.Text;
            string tao = txtPostalTown.Text.Trim();
            string phoneNum = TextBoxphone.Text.Trim();
            string webs = TextBoxweb.Text.Trim();
            string registrationNum = TextBoxreg.Text.Trim();
            string physicAddre = txtPhysicallAddr.Text.Trim();
            string nonPartisanTxtA = txtAreaPartisan.Text.Trim();

            //string nonPtrimmed  = txtAreaPartisan.Text.Substring(0, txtAreaPartisan.Text.Length - 1);

            int nonG = ddlOrgType.SelectedIndex;
            if (nonG == 0)
            {
                KCDFAlert.ShowAlert("Select valid option!");
            }
            if (nonG == 1)
            {
                ngo = false;
            }
            if (nonG == 2)
            {
                ngo = true;
            }
            int nonPart = ddlnonPartisan.SelectedIndex;
            if (nonPart == 0)
            {
                KCDFAlert.ShowAlert("Select valid option!");
            }
            if (nonPart == 1)
            {
                notpartisan = false;
            }
            if (nonPart == 2)
            {
                notpartisan = true;
            }

            if (nonPart == 1 && nonPartisanTxtA == "")
            {
                KCDFAlert.ShowAlert("Please describe your partisanship");
                return;
            }

            int nonProfit = ddlNonProfitable.SelectedIndex;
            if (nonProfit == 0)
            {
                KCDFAlert.ShowAlert("Select valid option!");
            }
            if (nonProfit == 1)
            {
                nonprofit = false;
            }
            if (nonProfit == 2)
            {
                nonprofit = true;
            }

            int legal = ddlLegal.SelectedIndex;
            if (legal == 0)
            {
                KCDFAlert.ShowAlert("Select valid option!");
            }
            if (legal == 1)
            {
                legally = false;
            }
            if (legal == 2)
            {
                legally = true;
            }
            string regType = ddlRegtype.SelectedItem.Text;

            var YoReg = dateofReg.Value.Trim();
            DateTime yearOfAdmn = DateTime.Parse(YoReg);


            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            Portals sup = new Portals();
            sup.Credentials = credentials;
            sup.PreAuthenticate = true;
            sup.FnRegGranteeInfo(usanm, contactP, currposition, phoneNum,
                postaddress, postcode, tao, ngo, notpartisan, nonprofit,
                legally, physicAddre, regType, yearOfAdmn, webs, registrationNum, nonPartisanTxtA);
            KCDFAlert.ShowAlert("Organization Information updated successfully!");
            loadApplicationInfo();


        }

        protected void loadApplicationInfo()
        {
            var granteeInfo =
                nav.grantees_Register.ToList()
                    .Where(n => n.Organization_Username.Equals(Session["username"].ToString()));
            TextBxcont.Text = granteeInfo.Select(co => co.Contact_Person).SingleOrDefault();
            TextBoposition.Text = granteeInfo.Select(po => po.Current_Position).SingleOrDefault();
            TextBxpostal.Text = granteeInfo.Select(pa => pa.Postal_Address).SingleOrDefault();
            ddlPostalCode.SelectedItem.Text = granteeInfo.Select(pc => pc.Postal_Code).SingleOrDefault();
            txtPostalTown.Text = granteeInfo.Select(ta => ta.Town).SingleOrDefault();
            TextBoxphone.Text = granteeInfo.Select(pn => pn.Phone).SingleOrDefault();
            txPhoneNo.Text = granteeInfo.Select(pn => pn.Phone).SingleOrDefault();
            TextBoxweb.Text = granteeInfo.Select(wb => wb.Website).SingleOrDefault();
            txtPhysicallAddr.Text = granteeInfo.Select(pad => pad.Physical_Address).SingleOrDefault();
            var ngO = granteeInfo.Select(ot => ot.NGO).SingleOrDefault();

            if (ngO == false)
            {
                ddlOrgType.SelectedIndex = 1;
            }
            if (ngO == true)
            {
                ddlOrgType.SelectedIndex = 2;
            }
            if (ngO == null)
            {
                ddlOrgType.SelectedIndex = 0;
            }
            var partsn = granteeInfo.Select(pt => pt.Partisan).SingleOrDefault();
            if (partsn == true)
            {
                ddlnonPartisan.SelectedIndex = 2;
            }
            if (partsn == false)
            {
                ddlnonPartisan.SelectedIndex = 1;
                txtAreaPartisan.Visible = true;
                txtAreaPartisan.Text = granteeInfo.Select(p => p.Non_Partisan_Describe).SingleOrDefault();
            }
            var nonPr = granteeInfo.Select(np => np.Profitable).SingleOrDefault();
            if (nonPr == false)
            {
                ddlNonProfitable.SelectedIndex = 1;
            }
            if (nonPr == true)
            {
                ddlNonProfitable.SelectedIndex = 2;
            }
            var legl = granteeInfo.Select(lg => lg.Legally_registered).SingleOrDefault();
            if (legl == false)
            {
                ddlLegal.SelectedIndex = 1;

            }
            if (legl == true)
            {
                ddlLegal.SelectedIndex = 2;
            }
            ddlRegtype.SelectedItem.Text = granteeInfo.Select(rt => rt.Type_Of_Organization).SingleOrDefault();
            var doR = granteeInfo.Select(dr => Convert.ToDateTime(dr.Date_Registered)).SingleOrDefault();
            dateofReg.Value = doR.ToShortDateString();
            TextBoxreg.Text = granteeInfo.Select(rg => rg.Registration_No).SingleOrDefault();

        }

        protected void ddlnonPartisan_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int selVal = ddlnonPartisan.SelectedIndex;
            if (selVal == 0)
            {
                KCDFAlert.ShowAlert("Please select a valid choice");
            }
            else
            if (selVal == 1)
            {
                txtAreaPartisan.Visible = true;
            }
            else
            {
                txtAreaPartisan.Visible = false;
            }
        }

    }
}