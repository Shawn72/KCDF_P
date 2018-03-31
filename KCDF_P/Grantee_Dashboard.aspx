<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Grantee_Dashboard.aspx.cs" MasterPageFile="Gran_Master.Master" Inherits="KCDF_P.Grantee_Dashboard" %>

<%@Import namespace="System.IO" %>
<%@Import namespace="System" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="KCDF_P" %>
<%@ Import Namespace="KCDF_P.NAVWS" %>
<asp:Content ID="userDashBd" ContentPlaceHolderID="MainContent" runat="server">

<meta http-equiv="refresh" content="180;url=Grantee_Dashboard.aspx"> 
    <div class="row" style="height: 20px">&nbsp;</div>
    <div class="row">
        <div class="col-md-4">
            <div class="panel panel-default">
                <div class="panel-heading text-danger"><i class="fa fa-user"></i><strong style="font-family:Trebuchet MS">Welcome, <%=Grantees.OrgUsername%></strong></div>
                <div class="panel-body">
                    <div class="row" tabindex="0px">
                        <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <asp:Image ID="profPic" runat="server"  class="img-circle img-responsive" />
                            <asp:Button  ID="btnUploadPic" runat="server" CssClass="btn btn-primary pull-right btn-sm" OnClick="btnUploadPic_OnClick" Text="Change Picture" ></asp:Button>
                        </div>
                       <div class="col-md-3"></div>
                    </div>
                    <div class="row" style="height: 20px">&nbsp;</div>
                    <table class="table table-bordered table-condensed table-striped" style="font-family:Trebuchet MS">
                        <tr>
                            <th colspan="2" style="font-size: large" ><i class="glyphicon glyphicon-user"></i> Basic Information</th>
                        </tr>
                         <tr>
                            <td>Organization Name: </td>
                            <td><%=Grantees.orgname %></td>
                        </tr>
                        <tr>
                            <td>Email: </td>
                            <td><%=Grantees.Email %></td>
                        </tr>
                        
                         <tr>
                            <td>KCDF Number: </td>
                            <td><%=Grantees.No %></td>
                        </tr>
                    </table>   
                   
                </div>
            </div>
        </div>
             
        <div class="col-md-8">
        <asp:GridView ID="tblMyProjects" runat="server" CssClass="table table-condensed" Width="100%" AutoGenerateSelectButton="false" 
            EmptyDataText="No projects Found!" OnRowDeleting="tblMyProjects_OnRowDeleting" DataKeyNames="No"
             AlternatingRowStyle-BackColor = "#C2D69B">
        <Columns>
            <asp:BoundField DataField="No" HeaderText="S/No:"/>
            <asp:BoundField DataField="Project_Name" HeaderText="Project" />
            <asp:BoundField DataField="Project_Start_Date" HeaderText="Project Start Year" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="Total_Project_Cost_KES" HeaderText="Total Project Cost" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Your_Cash_Contribution_KES" HeaderText="Organization Contribution" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Requested_KCDF_Amount_KES" HeaderText="Requested Grant Amount"  DataFormatString="{0:N2}"/>
            <asp:BoundField DataField="Submission_Status" HeaderText="Approval Status"/>
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" Text="Edit" CommandArgument='<%# Eval("No") %>' CommandName="lnkEdit" runat="server" OnClick="lnkEdit_OnClick"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" HeaderText="Actions" />
        </Columns>
        <SelectedRowStyle BackColor="#259EFF" BorderColor="#FF9966" /> 
            </asp:GridView>      
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
        
          
        <div class="modal fade" id="pageProjectEdit">
		<div class="modal-dialog" runat="server">
			<div class="modal-content" runat="server">
                <div class="panel-heading" style="text-align:left; background: #00bfff; color: #f0f8ff">Edit Projects Information</div>
			<div class="modal-header" runat="server">
					<strong>Edit Project: </strong><asp:Label runat="server" ID="lblPrjNm"></asp:Label>
					<button type="button" class="close" data-dismiss="modal">&times;</button>
				</div>
			<div class="modal-body">
                    <div class="form-horizontal">
                   <div class="col-md-12">
                      <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">1</span>Project Title:</label> 
                  </div>
               <br />
                 <div class="form-group">
                     <asp:Label runat="server"  CssClass="col-md-3 control-label">Proposed short title of your project:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="TextBoxtitle" CssClass="form-control" style="text-transform:uppercase" />              
                        </div> 
                  </div>
                    
                 <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Project Start Date:</asp:Label>
                        <div class="col-md-6">
                       <div class="input-group date">
                               <input type="text" id="txtDateofStart" runat="server" class="form-control"/><span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                           </div>      
                        </div> 
                     </div>

             <div class="col-md-12">
                <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">2</span>Project Area:</label> 
            </div>
               <br />
                   <div class="form-group">
                       <asp:Label runat="server"  CssClass="col-md-3 control-label">County:</asp:Label>
                         <div class="col-md-6">
                            <asp:DropDownList ID="ddlSelCountry" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true" >
                                <asp:ListItem Selected="True">..Select County..</asp:ListItem>
                                <asp:ListItem>Nairobi</asp:ListItem>
                                <asp:ListItem>Mombasa</asp:ListItem>
                                <asp:ListItem>Kitui</asp:ListItem>
                                <asp:ListItem>Kiambu</asp:ListItem>
                            </asp:DropDownList>
                        </div> 
                    </div>
                
                    <div class="form-group">
                        <asp:Label runat="server"  CssClass="col-md-3 control-label">Constituency:</asp:Label>
                            <div class="col-md-6">
                            <asp:DropDownList ID="ddlConstituency" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true" >
                                <asp:ListItem Selected="True">..Select County..</asp:ListItem>
                                <asp:ListItem>Dagoretti</asp:ListItem>
                                <asp:ListItem>Westlands</asp:ListItem>
                                <asp:ListItem>Kitui south</asp:ListItem>
                                <asp:ListItem>Ruiru</asp:ListItem>
                            </asp:DropDownList>
                        </div>  
                    </div>
                
                    <div class="form-group">
                        <asp:Label runat="server"  CssClass="col-md-3 control-label">Urban Settlement targeted (Can describe if applicable, the exact geographical areas where you plan to implement your project in no more than three sentences):</asp:Label>
                            <div class="col-md-6">
                                <asp:TextBox id="txtAreaTargetSettmnt" TextMode="multiline" Columns="100" Width="100%" Rows="5" runat="server" />
                            </div>
                    </div>
               
                    <div class="col-md-12">
                    <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">3</span>Project Duration:</label> 
                </div>
               <br />
                    <div class="form-group">
                        <asp:Label runat="server"  CssClass="col-md-3 control-label">Expected length of your project (max is 24 months):</asp:Label>
                             <div class="col-md-6">
                            <asp:DropDownList ID="ddlMonths" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true" >
                                <asp:ListItem Selected="True">..Select project Length..</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                            </asp:DropDownList>
                        </div> 
                    </div>

                    <div class="col-md-12">
                <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">4</span>	Requested resources :</label> 
            </div>
               <br />
                     <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Please Select an estimated scale of the grant funding needed for the implementation of the proposed project :</asp:Label>
                         <div class="col-md-6">
                            <asp:DropDownList ID="ddlEstScale" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true" >
                                <asp:ListItem Selected="True">..Select Estimated Scale..</asp:ListItem>
                                <asp:ListItem>KES 1,000,001 to 2,000,000</asp:ListItem>
                                <asp:ListItem>KES 2,000,001 to 3,000,000</asp:ListItem>
                                <asp:ListItem>KES 3,000,001 to 4,000,000</asp:ListItem>
                            </asp:DropDownList>
                        </div> 
                    </div>

                     <div class="col-md-12">
                       <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">5</span>Breakdown of the cost of your proposed project in KES :</label> 
                    </div>
               <br />
                    <div class="form-group">
                        <asp:Label runat="server"  CssClass="col-md-3 control-label">Total project cost in cash -(KES):</asp:Label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="TextBoxcost" CssClass="form-control" TextMode="Number"/>              
                            </div> 
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server"  CssClass="col-md-3 control-label">Your CASH contribution in cash -(KES):</asp:Label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="TextBoxcont" CssClass="form-control" TextMode="Number" />              
                            </div> 
                    </div>

                    <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Amount requested from KCDF in cash –(KES):</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="TextBoxrequested" CssClass="form-control" TextMode="Number" />              
                        </div>
                    </div>
              <br />
         
            <%--<div class="row">
                <div class="col-md-3"></div>
                <div class="col-md-6">
                    <div class="form-group">
                         <asp:Button ID="btnUpdatePOverview" runat="server" Text="Update Project Overview" CssClass="btn btn-primary pull-left btn-sm"/>          
                    </div>
                </div> 
                <div class="col-md-3"></div>
            </div>--%>
               <br/>      
           
             </div>
			</div>
                <div class="modal-footer">
                <asp:Button runat="server" Text="Update" CssClass="btn btn-primary"  ID="btnProjEdit" OnClick="btnProjEdit_OnClick"  />
                </div>
		</div>
	</div>

    </div>
    
   <script runat="server">
       protected void btnUploadMe_OnClick(object sender, EventArgs e)
       {
           string uploadsFolder = Request.PhysicalApplicationPath + "ProfilePics\\";
           string ext = Path.GetExtension(FileUpload.PostedFile.FileName);
           string filenameO = Grantees.OrgUsername + DateTime.Now.Millisecond.ToString()+ ext;
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
       protected void ToPNG(string imgFormat)
       {
           string extn = imgFormat;
           string uploadsFolder = Request.PhysicalApplicationPath + "ProfilePics\\";
           string filenameO = Students.Username + extn;
           System.Drawing.Image image = System.Drawing.Image.FromFile(uploadsFolder + filenameO);
           image.Save(uploadsFolder +Students.Username +".png", System.Drawing.Imaging.ImageFormat.Png);
           KCDFAlert.ShowAlert("Picture: " + filenameO + " uploaded successfully");
           File.Delete(filenameO);
           Page.Response.Redirect(Page.Request.Url.ToString(), true);
       }

       protected void DeleteDups()
       {
           string namepart = Session["username"].ToString();
           DirectoryInfo filepath = new DirectoryInfo(Server.MapPath("~/ProfilePics/"));
           FileInfo[] flInf = filepath.GetFiles("*" + namepart + ".");
           foreach (FileInfo gotcha in flInf.OrderByDescending(fil=>fil.CreationTime).Skip(1))
           {
               gotcha.Delete();

           }

       }

       protected void refreSH()
       {
           HttpResponse.RemoveOutputCacheItem("/Grantee_Dashboard.aspx");
         //  Response.Redirect(Request.RawUrl);
           Page.Response.Redirect(Page.Request.Url.ToString(), true);
       }

       protected void saveProfToNav(string piclink, string fileNme)
       {
           var usrnm = Session["username"].ToString();
           var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
           Portals sup = new Portals();
           sup.Credentials = credentials;
           sup.PreAuthenticate = true;
           sup.FnSaveProfP(usrnm, piclink,fileNme);
           refreSH();
           KCDFAlert.ShowAlert("Picture saved!");
          
       }

   </script>
    
  <script type="text/javascript">
    function openModal() {
        $('#pageUploadlink').modal('show');
    }
    function openEditProj() {
        $('#pageProjectEdit').modal('show');
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
</asp:Content>