<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Grantee_Profile.aspx.cs" MasterPageFile="~/Account/AddGranteeProfile_Master.Master" Inherits="KCDF_P.Account.Add_Grantee_Profile" %>
<asp:Content ID="OrganizationRegistrationForm" ContentPlaceHolderID="MainContent" runat="server">
<%--<meta http-equiv="refresh" content="200;url=Add_Grantee_Profile.aspx"> --%>
<div class="panel-body" style="font-family:Trebuchet MS">
    <div class="row">
        <div class="col-md-12">
                <h4 style="align-content:center; font-family:Trebuchet MS; color:#0094ff">Manage Profile</h4><br />
        </div>
    </div>
    <header class="panel-heading tab-bg-info">
             <asp:Menu ID="OrgInfoMenu" Orientation="Horizontal"  StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server" OnMenuItemClick="OrgInfoMenu_OnMenuItemClick">
                <Items>
                    <asp:MenuItem Text="Organization Profile |" Value="0" Selected="true" runat="server"/>
                    <asp:MenuItem Text="Applicant Information |" Value="1" runat="server"/>     
                </Items>
            </asp:Menu>
    </header>
 <section class="panel">
 <div class="panel panel-primary">
 
      <asp:MultiView ID="orgPMultiview" runat="server" ActiveViewIndex="0">
         <asp:View runat="server" ID="orgDView">
            <div class="form-horizontal">
            <br/>
            
            <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Organization Name:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txOrgname" CssClass="form-control" style="text-transform:uppercase" Enabled="False"/>               
                    </div>
                </div>
                
                <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Email Address:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txEmailAdd" CssClass="form-control" Enabled="False" />              
                        </div> 
                    </div>
      
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Mobile Number:</asp:Label>
                       <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txPhoneNo" CssClass="form-control"  Enabled="False"  />              
                               
                         </div> 
                    </div>
                      
              </div>
              <br/>
              
         </asp:View>
         
         <asp:View runat="server" ID="ContactInfoofApplyingOrg">
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
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">His or Her current position:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="TextBoposition" CssClass="form-control"  Enabled="True" />              
                        </div> 
                </div>
                   
                <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Postal Address:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="TextBxpostal" CssClass="form-control" TextMode="Number"  Enabled="True" />              
                        </div> 
                </div>
                   
                <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Postal Code:</asp:Label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlPostalCode" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true"  AutoPostBack="True" OnSelectedIndexChanged="ddlPostalCode_OnSelectedIndexChanged">
                            </asp:DropDownList>
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
                   
                 <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Physical Address:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtPhysicallAddr" CssClass="form-control" style="text-transform:uppercase" Enabled="True" />              
                        </div> 
                </div>
                   
                <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Phone:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="TextBoxphone" CssClass="form-control"  Enabled="True" />              
                        </div> 
                </div>
                   
                <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Website Address if applicable:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="TextBoxweb" CssClass="form-control"  Enabled="True" />              
                        </div> 
                </div>
            <div class="col-md-12">
                <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">2</span>Type of applying organization :</label> 
            </div>
             <br /> 
                 <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Non -Governmental:</asp:Label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlOrgType" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true">
                                <asp:ListItem Selected="True">..Select Organization Type..</asp:ListItem>
                                <asp:ListItem>NO</asp:ListItem>
                                <asp:ListItem>YES</asp:ListItem>
                            </asp:DropDownList>
                    </div> 
                      <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtOrgtype" CssClass="form-control" Enabled="False" placeholder="Organization type" />              
                     </div>
                </div>

                 <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Non -Partisan:</asp:Label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlnonPartisan" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlnonPartisan_OnSelectedIndexChanged" AutoPostBack="True" >
                               <asp:ListItem Selected="True">--Select Type--</asp:ListItem>
                               <asp:ListItem>NO</asp:ListItem>
                               <asp:ListItem>YES</asp:ListItem>
                            </asp:DropDownList>
                         </div> 
                      <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtpartsan" CssClass="form-control" Enabled="False" placeholder="partisan?" />              
                     </div>
                      <div class="col-md-3">
                      <div class="form-group">
                            <asp:TextBox id="txtAreaPartisan" CssClass="form-control" TextMode="multiline" Columns="8" Width="90%" Rows="2" runat="server" placeholder="Describe your partisan" Visible="False"/>
                        </div>
                     </div>

                </div>

                 <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Non -Profitable:</asp:Label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlNonProfitable" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true" >
                                <asp:ListItem Selected="True">--Select Type--</asp:ListItem>
                              <asp:ListItem>NO</asp:ListItem>
                              <asp:ListItem>YES</asp:ListItem>
                            </asp:DropDownList>
                    </div> 
                      <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtNgO" CssClass="form-control"  Enabled="False" placeholder="NGO?" />              
                     </div>
                </div>

                 <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Legally Registered:</asp:Label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlLegal" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true" >
                                <asp:ListItem Selected="True">--Choose--</asp:ListItem>
                                <asp:ListItem>NO</asp:ListItem>
                                <asp:ListItem>YES</asp:ListItem>
                            </asp:DropDownList>
                    </div> 
                     <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtlegalY" CssClass="form-control"  Enabled="False" placeholder="legal?" />              
                     </div>
                </div>
                   
                 <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Type Of Registration:</asp:Label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlRegtype" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true" >
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem>SHG</asp:ListItem>
                                <asp:ListItem>CBO</asp:ListItem>
                                <asp:ListItem>NGO</asp:ListItem>
                                <asp:ListItem>FBO</asp:ListItem>
                                <asp:ListItem>PSO</asp:ListItem>
                                <asp:ListItem>Trust</asp:ListItem>
                                <asp:ListItem>Society</asp:ListItem>
                                <asp:ListItem>Foundation</asp:ListItem>
                            </asp:DropDownList>
                    </div> 
                     <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtregtypeIs" CssClass="form-control"  Enabled="False" placeholder="registration type?" />              
                     </div>
                </div>

                 <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Registration Month/Year</asp:Label>
                    <div class="col-md-6">
                       <div class="input-group date">
                           <input type="text" class="form-control" id="dateofReg" runat="server"/><span class="input-group-addon"><i class="glyphicon glyphicon-th"></i></span>
                       </div>      
                    </div> 
                </div>

                 <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Registration Number:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="TextBoxreg" CssClass="form-control" style="text-transform:uppercase" Enabled="true" />              
                        </div> 
                </div>  
                   
               <div class="row">
                  <div class="col-md-3"></div>
                    <div class="col-md-6">
                      <div class="form-group">
                        <asp:Button ID="btnSave" runat="server" Text="Update Data" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnSave_OnClick"/>          
                     </div>
                    </div> 
                <div class="col-md-3"></div>
                </div>
               <br/>                                 
               
             </div>
              <br/>
          </asp:View>
        </asp:MultiView>
     </div>
</section>
</div>
</asp:Content>