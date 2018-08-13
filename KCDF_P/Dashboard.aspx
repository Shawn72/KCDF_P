<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" MasterPageFile="~/Site.Master" Inherits="KCDF_P.Dashboard" %>
<%@Import namespace="System.IO" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="KCDF_P" %>
<%@ Import Namespace="KCDF_P.NAVWS" %>
<asp:Content ID="userDashBd" ContentPlaceHolderID="MainContent" runat="server">
<%@ OutputCache NoStore="true" Duration="1" VaryByParam="*"   %>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="panel-body" style="font-family:Trebuchet MS">
    <div class="row" style="height: 20px">&nbsp;</div>
    <div class="row">
        <div class="col-md-5">
            <div class="panel panel-default">
                <div class="panel-heading text-danger"><i class="fa fa-user"></i><strong style="font-family:Trebuchet MS">Welcome, <%=Students.Username%></strong></div>
                <div class="panel-body">
                    <div class="row" tabindex="0px">
                        <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <asp:Image ID="profPic" runat="server"  class="img-circle img-responsive" />
                           <%-- <img src="ProfilePics/<%=Students.Username %>.png" class="img-circle img-responsive" alt="Upload Picture"/>--%>
                            <asp:Button  ID="btnUploadPic" runat="server" CssClass="btn btn-primary center btn-sm" OnClick="btnUploadPic_OnClick" Text="Change Picture" CausesValidation="False" UseSubmitBehavior="False" ></asp:Button>
                        </div>
                        <div class="col-md-3"></div>                        
                    </div>
                    <div class="row" style="height: 20px">&nbsp;</div>
                    <table class="table table-bordered table-condensed table-striped" style="font-family:Trebuchet MS">
                        <tr>
                            <th colspan="2" style="font-size: large" ><i class="glyphicon glyphicon-user"></i> Basic Information</th>
                        </tr>
                         <tr>
                            <td>Full Name: </td>
                            <td><%=Students.Name %></td>
                        </tr>
                        <tr>
                            <td>Date of Birth: </td>
                            <td><%=Students.doB %></td>
                        </tr>
                        
                         <tr>
                            <td>KCDF Number: </td>
                            <td><%=Students.No %></td>
                        </tr>
                       
                       
                         <tr>
                            <td>ID Number: </td>
                             <td><%=Students.IDNo %></td>
                        </tr>
                         <tr>
                            <td>Gender: </td>
                             <td><%=Students.gender %></td>
                        </tr>
                    </table>   
                   
                </div>
            </div>
        </div>
        <div class="col-md-3">
             <table class="table table-bordered table-condensed table-striped" style="font-family:Trebuchet MS">
                        <tr>
                            <th colspan="2" style="font-size: large" ><i class="glyphicon glyphicon-education"></i> Education Information</th>
                        </tr>
                         <tr>
                            <td>Primary school: </td>
                            <td><%=Students.primo %></td>
                        </tr>
                        <tr>
                            <td>Secondary School:</td>
                            <td><%=Students.seco %></td>
                        </tr>
                                              
                        <tr>
                            <td>University/College: </td>
                            <td><%=Students.univ %></td>
                        </tr>
                       <tr>
                         <td>Course: </td>
                            <td><%=Students.course %></td>
                        </tr>
                        <tr>
                            <td>Year of Admission: </td>
                            <td><%=Students.yearofAd %></td>
                        </tr>
                        <tr>
                            <td>Year of Study: </td>
                            <td><%=Students.yrofstudy %></td>
                        </tr>
                        <tr>
                            <td>Year of Completion: </td>
                            <td><%=Students.yrofCompltn %></td>
                        </tr>
                    </table> 
               </div>

        <div class="col-md-3">
                 <table class="table table-bordered table-condensed table-striped" style="font-family:Trebuchet MS">
                    <tr>
                        <th colspan="2" style="font-size: large" ><i class="glyphicon glyphicon-list-alt"></i> Contact Information</th>
                    </tr>

                    <tr>
                        <td>Email:</td>
                        <td><%=Students.Email %></td>
                    </tr>
                                              
                    <tr>
                        <td>Phone No: </td>
                        <td><%=Students.MobileNo %></td>
                    </tr>
                        
                        <tr>
                        <td>Residence: </td>
                        <td><%=Students.Address %></td>
                    </tr>
                </table> 
              </div>
        
        <div class="modal fade" id="pageUploadlink">
		<div class="modal-dialog" runat="server">
			<div class="modal-content" runat="server">
                <div class="panel-heading" style="text-align:left; background: #00bfff; color: #f0f8ff">Upload Profile Picture Here</div>
			<div class="modal-header" runat="server">
					<strong>Choose your Favourite pic</strong>
					<button type="button" class="close" data-dismiss="modal">&times;</button>
				</div>
			<div class="modal-body">
                    <table class="table table-bordered">
					<tr><td colspan="4">
						<label>Choose picture from your Computer</label>
						 <asp:FileUpload ID="FileUpload" runat="server" CssClass="btn btn-success pull-left btn-sm" Visible="True" placeholder="Choose Pic"/>
						</td>
					</tr>                                     
                    </table>		
			</div>
                <div class="modal-footer">
                <asp:Button runat="server" Text="Upload Picture" CssClass="btn btn-primary"  ID="btnUploadMe" OnClick="btnUploadMe_OnClick" />
                </div>
		</div>
	</div>

    </div>
        
        <script runat="server">
       protected void btnUploadMe_OnClick(object sender, EventArgs e)
       {
           string uploadsFolder = Request.PhysicalApplicationPath + "ProfilePics\\Scholarship\\";
           string ext = Path.GetExtension(FileUpload.PostedFile.FileName);
           string filenameO = Students.Username+ DateTime.Now.Millisecond.ToString() + ext;
           if (FileUpload.PostedFile.ContentLength>1000000)
           {
               KCDFAlert.ShowAlert("Select a file less than 1MB!");
               return;
           }
           if ((ext == ".jpeg") || (ext == ".jpg") || (ext == ".png"))
           {
               FileUpload.SaveAs(uploadsFolder + filenameO);
               saveProfToNav(uploadsFolder + filenameO, filenameO);
               refreSH();
           }
           else
           {
               KCDFAlert.ShowAlert("File Format is: " +ext+"Allowed picture formats are: JPG, JPEG, PNG only!");

           }
           if (!FileUpload.HasFile)
           {
               KCDFAlert.ShowAlert("Select Picture before uploading");
               return;
           }
       }
       protected void refreSH()
       {
           HttpResponse.RemoveOutputCacheItem("/Dashboard.aspx");
           // Response.Redirect(Request.RawUrl);
           Page.Response.Redirect(Page.Request.Url.ToString(), true);
       }
       protected void ToPNG(string imgFormat)
       {
           //string extn = imgFormat;
           //string uploadsFolder = Request.PhysicalApplicationPath + "ProfilePics\\";
           //string filenameO = Students.Username + extn;
           //System.Drawing.Image image = System.Drawing.Image.FromFile(uploadsFolder + filenameO);
           //image.Save(uploadsFolder +Students.Username +".png", System.Drawing.Imaging.ImageFormat.Png);
           //KCDFAlert.ShowAlert("Picture: " + filenameO + " uploaded successfully");
           //  File.Delete(filenameO);
           //  Response.Redirect("Dashboard.aspx");
       }

       protected void DeleteDups()
       {
           //string namepart = Session["username"].ToString();
           //DirectoryInfo filepath = new DirectoryInfo(Server.MapPath("~/ProfilePics/Scholarship/"));
           //FileInfo[] flInf = filepath.GetFiles("*" + namepart + ".");
           //foreach (FileInfo gotcha in flInf.OrderByDescending(fil=>fil.CreationTime).Skip(1))
           //{
           //    gotcha.Delete();

           //}
       }

       protected void saveProfToNav(string piclink, string fileNme)
       {
           var usrnm = Session["username"].ToString();
           var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
           Portals sup = new Portals();
           sup.Credentials = credentials;
           sup.PreAuthenticate = true;
           sup.FnSaveProfP(usrnm, piclink,fileNme);
           KCDFAlert.ShowAlert("Picture saved!");
       }

   </script>
    
  <script type="text/javascript">
    function openModal() {
        $('#pageUploadlink').modal('show');
    }
</script>
<script type="text/javascript">
 function forceReload() {
//give it a new name each time you need to do this
        var cookieName = 'refreshv1';
//check client can use cookies
        if ($.cookies.test()) {
            //get the cookie
            var c = $.cookies.get(cookieName);
            //if it doesn't exist this is their first time and they need the refresh
            if (c == null) {
                //set cookie so this happens only once
                $.cookies.set(cookieName, true, { expires: 7 });
                //do a "hard refresh" of the page, clearing the cache
                location.reload(true);
            }
        }
    }
</script>    
        
       
   </div>
    
    <div class="row">
        <div class="form-horizontal">
           
         <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
             <ContentTemplate>
                  <label class="form-control alert alert-info" style="font-weight: bold;">My Scholarships Applications</label> 
        
                <asp:GridView ID="tblmyApplications" runat="server" 
                    CssClass="table table-condensed table-responsive table-bordered footable" Width="100%" 
                    AutoGenerateSelectButton="false" 
                    EmptyDataText="No Applications yet!" DataKeyNames="No"
                    AlternatingRowStyle-BackColor = "#C2D69B" AllowSorting="True" OnRowDeleting="tblmyApplications_OnRowDeleting">
                <Columns>
                    <asp:BoundField DataField="No" HeaderText="S/No:"/>
                    <asp:BoundField DataField="Scholarship_Title" HeaderText="Scholarship Name" />
                    <asp:BoundField DataField="Date_of_Application" HeaderText="Application Date" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Approval_Status" HeaderText="Approval Status"/>
                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" HeaderText="Actions" />
                </Columns>
                <SelectedRowStyle BackColor="#259EFF" BorderColor="#FF9966" /> 
                </asp:GridView>   
         </ContentTemplate>
       </asp:UpdatePanel>
   </div>
    </div>
 </div>
</asp:Content>