<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Consultancy_Page.aspx.cs" Inherits="KCDF_P.Consultancy_Page" MasterPageFile="~/Consultants.Master" %>
<%@Import namespace="System.IO" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="KCDF_P" %>
<%@ Import Namespace="KCDF_P.NAVWS" %>
<asp:Content ID="userDashBd" ContentPlaceHolderID="MainContent" runat="server">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <div class="row" style="height: 20px">&nbsp;</div>
     <div class="col-md-12">
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <h4>Session Idle:&nbsp;<span id="secondsIdle"></span>&nbsp;Seconds.</h4>
    <asp:LinkButton ID="lnkFake" runat="server" />
    <asp:ModalPopupExtender ID="mpeTimeout" BehaviorID="mpeTimeout" runat="server" PopupControlID="pnlPopup"
        TargetControlID="lnkFake" OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modalBackground"
        OnOkScript="ResetSession()">
    </asp:ModalPopupExtender>

    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="panel panel-default">
            Session Expiring!
        </div>
        <div class="body">
            Your Session will expire in&nbsp;<span id="seconds"></span>&nbsp;seconds.<br />
            Do you want to reset?
        </div>
        <div class="footer" align="right">
            <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="yes" />
            <asp:Button ID="btnNo" runat="server" Text="No" CssClass="no" />
        </div>
    </asp:Panel>
     </div>
    <div class="row">
    <asp:Label ID="lblUsernameIS" runat="server" Visible="False"></asp:Label>
        <div class="col-md-4">
            <div class="panel panel-default">
                <div class="panel-heading text-danger"><i class="fa fa-user"></i><strong style="font-family:Trebuchet MS">Welcome, <%=ConsultantClass.Username%></strong></div>
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
                            <td>Consultant Name: </td>
                            <td><%=ConsultantClass.fullname %></td>
                        </tr>
                        <tr>
                            <td>Email: </td>
                            <td><%=ConsultantClass.Email %></td>
                        </tr>
                        
                         <tr>
                            <td>Reg No/Id No: </td>
                            <td><%=ConsultantClass.IDNoReg %></td>
                        </tr>
                    </table>   
                   <%-- <asp:Button runat="server" ID="copyTest" OnClick="copyTest_OnClick" Text="Copy Files"/>--%>
                </div>
            </div>
        </div>
             
       <%-- <div class="col-md-8">
        <asp:GridView ID="tblMyProjects" runat="server" CssClass="table table-condensed table-responsive table-bordered footable" Width="100%" AutoGenerateSelectButton="false" 
            EmptyDataText="No projects Found!" OnRowDeleting="tblMyProjects_OnRowDeleting" DataKeyNames="No"
             AlternatingRowStyle-BackColor = "#C2D69B" AllowSorting="True">
        <Columns>
            <asp:BoundField DataField="No" HeaderText="S/No:"/>
            <asp:BoundField DataField="Project_Name" HeaderText="Project" />
            <asp:BoundField DataField="Project_Start_Date" HeaderText="Project Start Year" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="Total_Project_Cost_KES" HeaderText="Total Project Cost" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Your_Cash_Contribution_KES" HeaderText="Organization Contribution" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Requested_KCDF_Amount_KES" HeaderText="Requested Grant Amount"  DataFormatString="{0:N2}"/>
            <asp:BoundField DataField="Approval_Status" HeaderText="Approval Status"/>
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" Text="Edit" CommandArgument='<%# Eval("No") %>' CommandName="lnkEdit" runat="server" OnClick="lnkEdit_OnClick"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" HeaderText="Actions" />
        </Columns>
        <SelectedRowStyle BackColor="#259EFF" BorderColor="#FF9966" /> 
            </asp:GridView>      
       </div>--%>

        
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
          
<script type="text/javascript">
        function SessionExpireAlert(timeout) {
            var seconds = timeout / 1000;
            document.getElementsByName("secondsIdle").innerHTML = seconds;
            document.getElementsByName("seconds").innerHTML = seconds;
            setInterval(function () {
                seconds--;
                document.getElementById("seconds").innerHTML = seconds;
                document.getElementById("secondsIdle").innerHTML = seconds;
            }, 1000);
            setTimeout(function () {
                //Show Popup before 20 seconds of timeout.
                $find("mpeTimeout").show();
            }, timeout - 20 * 1000);
            setTimeout(function () {
                window.location = "Expired.aspx";
            }, timeout);
        };
        function ResetSession() {
            //Redirect to refresh Session.
            window.location = window.location.href;
        }
    </script>        

<script runat="server">
    protected void btnUploadMe_OnClick(object sender, EventArgs e)
    {
        string uploadsFolder = Request.PhysicalApplicationPath + "ProfilePics\\Consultants\\";
        string ext = Path.GetExtension(FileUpload.PostedFile.FileName);
        string filenameO = ConsultantClass.Username + DateTime.Now.Millisecond.ToString()+ ext;
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
    //protected void ToPNG(string imgFormat)
    //{
    //    string extn = imgFormat;
    //    string uploadsFolder = Request.PhysicalApplicationPath + "ProfilePics\\";
    //    string filenameO = Students.Username + extn;
    //    System.Drawing.Image image = System.Drawing.Image.FromFile(uploadsFolder + filenameO);
    //    image.Save(uploadsFolder +Students.Username +".png", System.Drawing.Imaging.ImageFormat.Png);
    //    KCDFAlert.ShowAlert("Picture: " + filenameO + " uploaded successfully");
    //    File.Delete(filenameO);
    //    Page.Response.Redirect(Page.Request.Url.ToString(), true);
    //}

    //protected void DeleteDups()
    //{
    //    string namepart = Session["username"].ToString();
    //    DirectoryInfo filepath = new DirectoryInfo(Server.MapPath("~/ProfilePics/"));
    //    FileInfo[] flInf = filepath.GetFiles("*" + namepart + ".");
    //    foreach (FileInfo gotcha in flInf.OrderByDescending(fil=>fil.CreationTime).Skip(1))
    //    {
    //        gotcha.Delete();

    //    }

    //}

    protected void refreSH()
    {
        HttpResponse.RemoveOutputCacheItem("/Consultancy_Page.aspx");
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
</div>
    <div class="container">
	<div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">

		<div class="panel panel-default">
			<div class="panel-heading" role="tab" id="headingOne">
				<h4 class="panel-title">
					<a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
						<i class="short-full glyphicon glyphicon-plus"></i>Edit Consultant Profile
					</a>
				</h4>
			</div>
			<div id="collapseOne" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
				<div class="panel-body">
             
               <div class="form-horizontal">
            <br/>
            <div class="col-md-12">
                <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">1</span>Contact Information of applying organization:</label> 
            </div>
             <br />
                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Name of Contact Person:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="TextBxcont" CssClass="form-control" style="text-transform:uppercase" />  
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="This Field is mandatory"
                                ControlToValidate="TextBxcont" runat="server" ForeColor="Red" Display="Dynamic" />             
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">His or Her current position:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="TextBoposition" CssClass="form-control"  Enabled="True" />  
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="This Field is mandatory"
                                ControlToValidate="TextBoposition" runat="server" ForeColor="Red" Display="Dynamic" />            
                        </div> 
                </div>
                   
                <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Postal Address:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="TextBxpostalAdd" CssClass="form-control" TextMode="Number"  Enabled="True" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="This Field is mandatory"
                                ControlToValidate="TextBxpostalAdd" runat="server" ForeColor="Red" Display="Dynamic" />               
                        </div> 
                </div>
                   
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Postal Code:</asp:Label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlPostalCode" runat="server"  CssClass="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true"  
                                AutoPostBack="True"  ViewStateMode="Enabled"
                                 OnSelectedIndexChanged="ddlPostalCode_OnSelectedIndexChanged">
                            </asp:DropDownList>

                            <%--<asp:DropDownList ID="ddlPosta" runat="server" CssClass="form-control"></asp:DropDownList>--%>

                       </div> 
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtMyPostaIs" CssClass="form-control" Enabled="False" placeholder="My Postal Code" />              
                     </div>
                </div>
                    
                <div class="form-group">
                <asp:Label runat="server"  CssClass="col-md-3 control-label">Town:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtPostalTown" CssClass="form-control" style="text-transform:uppercase" Enabled="False" />              
                    </div> 
                </div>
                </ContentTemplate>
                    <%-- <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlPostalCode" EventName="SelectedIndexChanged" />
                    </Triggers>--%>
            </asp:UpdatePanel>
                   <br />
                   
                <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Phone:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="TextBoxphone" CssClass="form-control" TextMode="Number"  Enabled="True" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="This Field is mandatory"
                                ControlToValidate="TextBoxphone" runat="server" ForeColor="Red" Display="Dynamic" />              
                        </div> 
                </div>
                   
                <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Website Address if applicable:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="TextBoxweb" CssClass="form-control"  Enabled="True" />              
                        </div> 
                </div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Have you Consulted for KCDF before?</asp:Label><span class="required">*</span>
                        <div class="col-md-6">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Please answer approriately!<br />"
                                ControlToValidate="rdoBtnListYesNo" runat="server" ForeColor="Red" Display="Dynamic" />
                                <asp:RadioButtonList ID="rdoBtnListYesNo" runat="server"
                                        OnSelectedIndexChanged="rdoBtnListYesNo_OnSelectedIndexChanged" 
                                    AutoPostBack="True" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="0" Selected="False"></asp:ListItem>
                                <asp:ListItem Text="No" Value="1" Selected="False"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div> 
                </div>
                <div id="idKCDFyes" style="display:none">
                        <div class="form-group">
                        <asp:Label runat="server" CssClass="col-md-3 control-label">If Yes what is the status of the KCDF Project?</asp:Label><span class="required">*</span>
                            <div class="col-md-6">
                                    <asp:RadioButtonList ID="rdBtnStatus" runat="server" OnSelectedIndexChanged="rdBtnStatus_OnSelectedIndexChanged">
                                    <asp:ListItem Text="Ongoing" Value="1" Selected="False"></asp:ListItem>
                                    <asp:ListItem Text="Completed" Value="2" Selected="False"></asp:ListItem>
                                    <asp:ListItem Text="Terminated" Value="3" Selected="False"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div> 
                    </div>
                </div>
                <br />
                    </ContentTemplate>
                </asp:UpdatePanel> 

                   
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                <ContentTemplate>
               <div class="row">
                  <div class="col-md-3"></div>
                    <div class="col-md-6">
                      <div class="form-group">
                        <asp:Button ID="btnSave" runat="server" Text="Update Profile" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnSave_OnClick" CausesValidation="True" />          
                     </div>
                    </div> 
                <div class="col-md-3"></div>
                </div>
                  </ContentTemplate>
                   <Triggers>
                      <asp:AsyncPostBackTrigger ControlID = "btnSave" EventName = "Click" />
                   </Triggers>
                </asp:UpdatePanel>
               <br/>                                 
               
             </div>
              <br/>
                    	 
                </div>
			</div>
		</div>

		<div class="panel panel-default">
			<div class="panel-heading" role="tab" id="headingTwo">
				<h4 class="panel-title">
					<a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
						<i class="short-full glyphicon glyphicon-plus"></i>Apply for Consultancy
					</a>
				</h4>
			</div>
			<div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
				<div class="panel-body">
					Here you go with the content of accordion's heading 2. You may place textual information, images etc. here.
				</div>
			</div>
		</div>
	</div>
</div>
<script type="text/javascript">
        function SessionExpireAlert(timeout) {
            var seconds = timeout / 1000;
            document.getElementsByName("secondsIdle").innerHTML = seconds;
            document.getElementsByName("seconds").innerHTML = seconds;
            setInterval(function () {
                seconds--;
                document.getElementById("seconds").innerHTML = seconds;
                document.getElementById("secondsIdle").innerHTML = seconds;
            }, 1000);
            setTimeout(function () {
                //Show Popup before 20 seconds of timeout.
                $find("mpeTimeout").show();
            }, timeout - 20 * 1000);
            setTimeout(function () {
                window.location = "Expired.aspx";
            }, timeout);
        };
        function ResetSession() {
            //Redirect to refresh Session.
            window.location = window.location.href;
        }
    </script>  
    
<script type="text/javascript" >
      function IfYesIts() {
           var ifYsId = document.getElementById("idKCDFyes");
           ifYsId.style.display = "block";
      }
    </script> 
    
<script>
    function pageLoad() {
        $('.selectpicker').selectpicker(); 
    }
</script>

<%--<script type="text/javascript">
       $document.ready(function() {
            $.ajax({
                type: "POST",
                url: "Consultancy_Page.aspx/GetPostalCodes",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var ddlPostals = $("[id*=ddlPosta]"); //$("[id*=ddlPosta]");
                    ddlPostals.empty().append('<option selected="selected" value="0">Please select</option>');
                    $.each(r.d, function (key, value) {
                        ddlPostals.append($("<option></option>").val(value.postaCode).html(value.postaTown));
                       // alert(ddlPostals.val);
                    });
                }
            });
        });
    </script>--%>
 
</asp:Content>