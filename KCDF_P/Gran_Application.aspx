﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gran_Application.aspx.cs" MasterPageFile="~/Gran_Master.Master"Inherits="KCDF_P.Gran_Application" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.5.7.123, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ OutputCache NoStore="true" Duration="1" VaryByParam="*"   %>
<asp:Content ID="OrganizationRegistrationForm" ContentPlaceHolderID="MainContent" runat="server">
  <%--   <div class="col-md-12">
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
     </div>--%>
        <div class="panel-body" style="font-family:Trebuchet MS">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="row">
                <div class="col-md-12">
                    <h4 style="align-content:center; font-family:Trebuchet MS; color:#0094ff">Manage Grants Applications</h4><br /></div>
            </div>
            <div class="form-horizontal">
                         
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">KCDF Project:</asp:Label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlAccountType" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlAccountType_OnSelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-5">
                        <asp:TextBox runat="server" ID="txtPrefNo" CssClass="form-control" Enabled="False" placeholder="Project Reference No"></asp:TextBox>
                    </div>
                </div>
            </div>
            <br/>
            <%
            var selProj = ddlAccountType.SelectedValue;
            var proFtures = nav.call_for_Proposal.ToList().Where(bf => bf.Call_Ref_Number == selProj);
            var itswhats = proFtures.Select(f => f.Basic_Features).SingleOrDefault();

            if (itswhats == false)
            {
             %>
            <header class="panel-heading tab-bg-info">
                <asp:Menu ID="OrgInfoMenu" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server" OnMenuItemClick="studentInfoMenu_OnMenuItemClick">
                    <Items>
                        <asp:MenuItem Text="Applicant Information |" Value="0" runat="server" />
                        <asp:MenuItem Text="KCDF Downloads |" Value="1" runat="server" />
                        <asp:MenuItem Text="Grants Management |" Value="2" runat="server" />
                        <asp:MenuItem Text="Project Overview |" Value="3" runat="server" />
                        <asp:MenuItem Text="Target Information |" Value="4" runat="server" />
                        <asp:MenuItem Text="Upload Project Documents |" Value="5" runat="server" />
                    </Items>
                </asp:Menu>
            </header>
            <% }
        else if (itswhats == true)
        {
         %>
            <header class="panel-heading tab-bg-info">
                <asp:Menu ID="orgInfoMenu2" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server" OnMenuItemClick="orgInfoMenu2_OnMenuItemClick">
                    <Items>
                        <asp:MenuItem Text="Applicant Information |" Value="0" runat="server" />
                        <asp:MenuItem Text="KCDF Downloads |" Value="1" runat="server" />
                        <asp:MenuItem Text="Project Overview |" Value="2" runat="server" />
                        <asp:MenuItem Text="Target Information |" Value="3" runat="server" />
                        <asp:MenuItem Text="Upload Project Documents |" Value="4" runat="server" />
                    </Items>
                </asp:Menu>
            </header>
            <% } %>
        <asp:Label ID="lbError" runat="server" ForeColor="#FF3300" CssClass="text-left hidden"></asp:Label>
        <section class="panel">
            <div class="panel panel-primary">
            
            <div class="col-md-3"></div>
            <div class="col-md-6">
                <asp:Label ID="lblError" runat="server" ForeColor="#FF3300" CssClass="text-left hidden"></asp:Label>
                <span class="text-center text-danger"><small><%=lblError.Text %></small></span>
            </div>
            <div class="col-md-3"></div>
                
                <br/>
                <asp:Label ID="lblUsernameIS" runat="server" Visible="False"></asp:Label>
                <div class="col-md-12">
                     <div class="form-group">
                       <label class="control-label alert alert-info col-md-3" style="font-weight: bold;"><span class="badge alert-danger">1</span>Registration of applying organization:</label> 
                     <%--   <asp:Label runat="server" CssClass="col-md-3 control-label">Registration No:</asp:Label>--%>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtRegNo" CssClass="form-control" style="text-transform:uppercase" Enabled="False" />
                        </div>
                      <br/>
                  </div>
                </div>
                 
                <br/>
                <asp:MultiView ID="orgPMultiview" runat="server" ActiveViewIndex="0">
                
                    <asp:View runat="server" ID="myApplicantInfo">
                         <div class="form-horizontal">
                        <br/>
                        <div class="col-md-12">
                            <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">2</span>Contact Information of applying organization:</label> 
                        </div>
                         <br />
                           
                            <div class="form-group">
                            <asp:Label runat="server" CssClass="col-md-3 control-label">Name of Contact Person:</asp:Label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="TextBxcont" CssClass="form-control" style="text-transform:uppercase" Enabled="False" />               
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Organization Name:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="txOrgname" CssClass="form-control" style="text-transform:uppercase" Enabled="False"/>               
                                    </div>
                                </div>

                            <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">His or Her current position:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="TextBoposition" CssClass="form-control"  Enabled="False" />              
                                    </div> 
                            </div>
                   
                            <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Postal Address:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="TextBxpostal" CssClass="form-control" TextMode="Number"  Enabled="False" />              
                                    </div> 
                            </div>
                   
                            <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Postal Code:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                    </div> 
                            </div>
                   
                            <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Town:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="txtPostalTown" CssClass="form-control" style="text-transform:uppercase" Enabled="False" />              
                                    </div> 
                            </div>
                   
                            <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Phone:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="TextBoxphone" CssClass="form-control"  Enabled="False" />              
                                    </div> 
                            </div>

                             <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Email Address:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="txEmailAdd" CssClass="form-control" Enabled="False" />              
                                    </div> 
                             </div>
                   
                            <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Website Address if applicable:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="TextBoxweb" CssClass="form-control"  Enabled="False" />              
                                    </div> 
                            </div>
                        <div class="col-md-12">
                            <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">3</span>Type of applying organization :</label> 
                        </div>
                         <br /> 
                             <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label"><span class="badge alert-danger">4</span>Type of Organization:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:CheckBoxList ID="checkIFNgO"  RepeatDirection="Horizontal" runat="server" >
                                            <asp:ListItem Text="Government" Value="GV" Enabled="False" />
                                            <asp:ListItem Text="Non-Government" Value="NGV" Enabled="False" />
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="nonProfitOR" runat="server" RepeatDirection="Horizontal">
                                             <asp:ListItem Text="Non-Profit" Value="NP" Enabled="False"/>
                                            <asp:ListItem Text="Profit Making" Value="PM" Enabled="False" />
                                        </asp:CheckBoxList>
                                      <%-- <asp:TextBox ID="txtNGO" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>--%>
                                </div> 
                            </div>

                                <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label"><span class="badge alert-danger">5</span>Type Of Registration:</asp:Label>
                                    <div class="col-md-5">
                                       <asp:TextBox ID="txtTypeofOrg" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3">
                                          <asp:Button ID="btnGoNext1" runat="server" Text="Next Tab" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnGoNext1_OnClick" CausesValidation="False" UseSubmitBehavior="False" />          
                                     </div>
                                </div>
                         </div>
                         <br/>
                    </asp:View>
                
                    <asp:View runat="server" ID="viewUploads">
                        <div class="form-horizontal">
                        <div class="row">
                        <div class="col-md-12">
                          <p class="form-control alert alert-info" style="font-weight: bold;"> Download KCDF Grant files here</p> 
                        </div>
                            <br/>
                            <div class="col-md-12">
                                 <asp:GridView ID="gridVGrantsDownloads" runat="server" CssClass="table table-striped table-advance table-hover" GridLines="None" AutoGenerateColumns="false" EmptyDataText = "No files available for download">
                                    <Columns>
                                        <asp:BoundField DataField="Text" HeaderText="File Name" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("Value") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                           
                            </div>
                          </div>
                        <div class="col-md-12 form-group">
                            <asp:Button ID="btnNext2" runat="server" Text="Next Tab" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnNext2_OnClick" CausesValidation="False" UseSubmitBehavior="False" />          
                        </div>
                    </asp:View>
                    
                    <asp:View runat="server" ID="grantsMgt">
                        <div class="form-horizontal">
                            <div class="row">
                                <div class="col-md-12">
                                    <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">1</span>Grants Management History.</label>
                                </div>
                                <br/>
                                
                                <div class="col-md-6">
                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                         <ContentTemplate>
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Have you Received KCDF funding before?</asp:Label><span class="required">*</span>
                                            <div class="col-md-6">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Please answer approriately!<br />"
                                        ControlToValidate="rdoBtnListYesNo" runat="server" ForeColor="Red" Display="Dynamic" />
                                                    <asp:RadioButtonList ID="rdoBtnListYesNo" runat="server" OnSelectedIndexChanged="rdoBtnListYesNo_OnSelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Text="Yes" Value="0" Selected="False"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="1" Selected="False"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div> 
                                    </div>

                                    <div id="idKCDFyes" style="display:none">
                                         <div class="form-group">
                                            <asp:Label runat="server" CssClass="col-md-3 control-label">If Yes what is the status of the grant?</asp:Label><span class="required">*</span>
                                                <div class="col-md-6">
                                                        <asp:RadioButtonList ID="rdBtnStatus" runat="server" OnSelectedIndexChanged="rdBtnStatus_OnSelectedIndexChanged">
                                                        <asp:ListItem Text="Ongoing" Value="0" Selected="False"></asp:ListItem>
                                                        <asp:ListItem Text="Completed" Value="1" Selected="False"></asp:ListItem>
                                                        <asp:ListItem Text="Terminated" Value="2" Selected="False"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div> 
                                        </div>
                                    </div>

                                     <div class="col-md-12">
                                        <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">2</span>History of grants received: You Can Add More than one grants history</label>
                                    </div>
                                    <br/>
                                    
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Name of the Donor:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtDonor" CssClass="form-control" style="text-transform:uppercase" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Grant Amount Provided:</asp:Label>
                                        <div class="col-md-6">
                                         <label id="lblCharLeft2" title=""></label>
                                        <br/>
                                            <asp:TextBox runat="server" ID="txtAmount" CssClass="form-control" required="True" TextMode="Number" onKeyUp="javascript:CheckView2(this, 9);" onChange="javascript:CheckView2(this, 9);"/>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Year of Award:</asp:Label>
                                        <div class="col-md-6">
                                            <div class="input-group">
                                                 <asp:DropDownList ID="ddlYears" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlYears_OnSelectedIndexChanged">
                                                </asp:DropDownList><span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                       
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Objective of grant :</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextObj" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">County (ies) targeted:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddltargetCounty" runat="server" class="selectpicker form-control" data-live-search-style="begins"
                                                 data-live-search="true" AppendDataBoundItems="true" 
                                                AutoPostBack="True" OnSelectedIndexChanged="ddltargetCounty_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:TextBox id="txtAreaCounties" TextMode="multiline" 
                                                    Columns="10" Width="100%" Rows="2" runat="server" 
                                                    placeholder="Counties Here" Visible="False" 
                                                    ReadOnly="False" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Type of beneficiaries reached/targeted:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlBenType" runat="server" class="selectpicker form-control" data-live-search-style="begins"
                                                 data-live-search="true" AppendDataBoundItems="true" 
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlBenType_OnSelectedIndexChanged">
                                                <asp:ListItem Selected="True">--Choose Beneficiary--</asp:ListItem>
                                                <asp:ListItem>Children</asp:ListItem>
                                                <asp:ListItem>Adults</asp:ListItem>
                                                <asp:ListItem>Teachers</asp:ListItem>
                                                <asp:ListItem>Farmers</asp:ListItem>
                                                <asp:ListItem>Youth</asp:ListItem>
                                                <asp:ListItem>Pastoral Community</asp:ListItem>
                                                <asp:ListItem>Persons with Disability</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:TextBox id="TextTypeBeneficiary" TextMode="multiline" 
                                                    Columns="10" Width="100%" Rows="2" runat="server" 
                                                    placeholder="Beneficiaries here" Visible="False" 
                                                    ReadOnly="False" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">No. of beneficiaries reached/targeted:</asp:Label>
                                        <div class="col-md-6">
                                             <label id="lblCharLeft3" title=""></label>
                                            <br/>
                                            <asp:TextBox runat="server" ID="TextNoBeneficiary" CssClass="form-control" onKeyUp="javascript:CheckView3(this, 5);" onChange="javascript:CheckView3(this, 5);"/>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Project Status:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlPrjStatus" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="true" AutoPostBack="True">
                                                <asp:ListItem Selected="True">..Select Project Status..</asp:ListItem>
                                                <asp:ListItem>Complete</asp:ListItem>
                                                <asp:ListItem>Ongoing</asp:ListItem>
                                                <asp:ListItem>Terminated</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br/>
                                        </ContentTemplate>
                                     </asp:UpdatePanel>
                                    
                                     <div class="row">
                                     <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                         <ContentTemplate>
                                   
                                        <div class="col-md-3"></div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <asp:Button ID="btnSaveGrantHisto" runat="server" Text="Save Grants History"
                                                     CssClass="btn btn-primary pull-left btn-sm" OnClick="btnSaveGrantHisto_OnClick"
                                                     CausesValidation="True"/>
                                            </div>
                                    </div>
                                     </ContentTemplate>
                                           <Triggers>
                                                <asp:PostBackTrigger ControlID = "btnSaveGrantHisto"/>
                                            </Triggers>
                                     </asp:UpdatePanel>
                                         
                                    <div class="col-md-3">
                                            <asp:Button ID="btnNext3" runat="server" Text="Next Tab" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnNext3_OnClick" CausesValidation="False" UseSubmitBehavior="False" />          
                                     </div>
                                   </div>
                                </div>
                         <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                <ContentTemplate>
                                <div class="col-md-6">
                                    <asp:GridView ID="tblGrantsManager" runat="server" 
                                        CssClass="table table-condensed table-responsive table-bordered footable" 
                                        Width="100%" AutoGenerateSelectButton="false" EmptyDataText="No Grant History Found!" 
                                        AlternatingRowStyle-BackColor="#C2D69B" DataKeyNames="No" OnRowDeleting="tblGrantsManager_OnRowDeleting"
                                         AllowSorting="True">
                                        <Columns>
                                            <asp:BoundField DataField="No" HeaderText="S/No:" />
                                            <asp:BoundField DataField="Project_Name" HeaderText="Project Name" />
                                            <asp:BoundField DataField="Name_Of_the_Donor" HeaderText="Donor" />
                                            <asp:BoundField DataField="Amount_Provided" HeaderText="Grant Amount Provided:" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="Project_Status" HeaderText="Project Status:" />
                                            <asp:CommandField ShowDeleteButton="True" ButtonType="Button"/>
                                        </Columns>
                                        <SelectedRowStyle BackColor="#259EFF" BorderColor="#FF9966" />
                                    </asp:GridView>
                                </div>
                                </ContentTemplate>
                        </asp:UpdatePanel>
                                    
                            </div>
                        </div>
                        <br/>
                    </asp:View>

                    <asp:View runat="server" ID="Projectoverview">
                        <div class="form-horizontal">
                            <div class="col-md-12">
                                <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">*</span>Project Title:</label>
                            </div>
                            <br />
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Proposed short title of your project:</asp:Label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="TextBoxtitle" CssClass="form-control" style=" text-transform:uppercase" />
                                </div>
                            </div>
                            
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                  <div class="form-group">
                                    <asp:Label runat="server"  CssClass="col-md-3 control-label"><span class="badge alert-danger">*</span>Proposal Alignment to call Objectives as per project guidelines</asp:Label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlObjectives" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                                    data-live-search="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlObjectives_OnSelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <label style=" font-weight: bold;">You can select several objectives</label>
                                        </div> 
                                       <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox id="txtAreaObjs" TextMode="multiline" 
                                                    Columns="10" Width="100%" Rows="2" runat="server" Visible="False" ReadOnly="False"
                                                    CssClass="form-control" MaxLength="250" ForeColor="Red" />
                                            </div>
                                       </div>
                                 
                                    </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            <br/> 

                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Project Start Date:</asp:Label>
                                <div class="col-md-6">
                                    <div class="input-group date">
                                        <input type="text" id="txtDateofStart" runat="server" class="form-control" /><span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="form-control alert alert-info" style=" font-weight: bold;"><span class="badge alert-danger">2</span>Project Area:</label>
                            </div>
                            <br />
                            
                             <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-3 control-label">County:</asp:Label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSelCounty" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSelCounty_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                     <div class="col-md-2">
                                        <asp:TextBox runat="server" ID="txtCounty" CssClass="form-control"  Enabled="False" placeholder="County?" />              
                                     </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-3 control-label">Constituency:</asp:Label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlConstituency" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="False"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox runat="server" ID="txtSubCounty" CssClass="form-control"  Enabled="False" placeholder="County?" />              
                                     </div>
                                </div>

                                <div class="form-group" style="display: none;">
                                    <asp:Label runat="server" CssClass="col-md-3 control-label">Description of areas where you plan to implement your project<br/> <b>(Max: 250 Chars)</b></asp:Label>
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-3"></div>
                                             <div class="col-md-6">
                                               <label id="lblCharleftTextarea" title=""></label>
                                             </div>
                                           <div class="col-md-3"></div>
                                        </div>
                                        <asp:TextBox id="txtAreaTargetSettmnt" TextMode="multiline" Columns="100"
                                             Width="100%" Rows="5" runat="server" onKeyUp="javascript:CheckTextArea(this, 250);" onChange="javascript:CheckTextArea(this, 250);" />
                                    </div>
                                </div>
                                </ContentTemplate>
                        </asp:UpdatePanel>
                            <div class="col-md-12">
                                <label class="form-control alert alert-info" style=" font-weight: bold;"><span class="badge alert-danger">3</span>Project Duration:</label>
                            </div>
                            <br />
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Expected length of your project (max is 24 months):</asp:Label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlMonths" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="true">
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
                                <div class="col-md-2">
                                    <asp:TextBox runat="server" ID="txtLength" CssClass="form-control"  Enabled="False" placeholder="project length" />              
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">4</span>	Requested resources :</label>
                            </div>
                            <br />
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Please Select an estimated scale of the grant funding needed for the implementation of the proposed project :</asp:Label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlEstScale" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="true">
                                        <asp:ListItem Selected="True">--Select Estimated Scale--</asp:ListItem>
                                        <asp:ListItem>KES 1,000,001 to 2,000,000</asp:ListItem>
                                        <asp:ListItem>KES 2,000,001 to 3,000,000</asp:ListItem>
                                        <asp:ListItem>KES 3,000,001 to 4,000,000</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox runat="server" ID="txtScale" CssClass="form-control"  Enabled="False" placeholder="Scale" />              
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="form-control alert alert-info" style="font-weight: bold;}"><span class="badge alert-danger">5</span>Breakdown of the cost of your proposed project in KES:Refer to Application contribution <a href="http://www.kcdf.or.ke/index.php/work/call-for-proposal">Guidelines</a></label>
                            </div>
                            <br />
                            
                            <div class="row">
                                <div class="col-md-3"></div>
                                 <div class="col-md-6">
                                   <label id="lblCharLeft" title=""></label>
                                 </div>
                               <div class="col-md-3"></div>
                            </div>
                           <br/>

                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Total project cost in cash -(KES):</asp:Label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="TextBoxcost" CssClass="form-control" TextMode="Number" onKeyUp="javascript:Check(this, 9);" onChange="javascript:Check(this, 9);"/>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Your CASH contribution in cash -(KES):</asp:Label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="TextBoxcont" CssClass="form-control" TextMode="Number" onKeyUp="javascript:ValidatemeSpecialCase(this);" onChange="javascript:ValidatemeSpecialCase(this);" Enabled="True"  />
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Amount requested from KCDF in cash –(KES):</asp:Label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="TextBoxrequested" CssClass="form-control" TextMode="Number" onKeyUp="javascript:Check(this, 9);" onChange="javascript:Check(this, 9);" Enabled="False" />
                                </div>
                            </div>
                            <br />
                            
                             <div class="row">
                             <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                <ContentTemplate>
                                   
                                        <div class="col-md-3"></div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <asp:Button ID="btnUpdatePOverview" runat="server" Text="Save Information" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnUpdatePOverview_OnClick" Enabled="false"/>
                                            </div>
                                        </div>

                                     
                                 </ContentTemplate>
                               <Triggers>
                                  <asp:PostBackTrigger ControlID = "btnUpdatePOverview"/>
                               </Triggers>
                            </asp:UpdatePanel>
                            <div class="col-md-3">
                                    <asp:Button ID="btnNext4" runat="server" Text="Next Tab" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnNext4_OnClick" CausesValidation="False" UseSubmitBehavior="False" />          
                              </div>
                         </div>
                        <br/>
                        </div>
                    </asp:View>

                    <asp:View ID="targetGroup" runat="server">
                        <div class="form-horizontal">
                            <div class="col-md-12">
                                <p>In this section, you are required to indicate the population that you intend to reach through the project for which you have asked KCDF to support: </p>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Households</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxhse" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ErrorMessage="This textfield is mandatory"
                                         ControlToValidate="TextBoxschl" runat="server" ForeColor="Red" Display="Dynamic" />
                                             </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Schools:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxschl" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ErrorMessage="This textfield is mandatory"
                                         ControlToValidate="TextBoxschl" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Grass-root organizations:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxorg" CssClass="form-control" Placeholder="Number Targeted" TextMode="Number" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="This textfield is mandatory"
                                         ControlToValidate="TextBoxorg" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                             <%--    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                    <ContentTemplate>
                                    <div class="form-group">
                                            <asp:Button ID="testBtn" runat="server" Text="Test Fields" CssClass="btn btn-primary pull-left btn-sm" OnClick="testBtn_OnClick"  OnSubmitBehavior="False" CausesValidation="False"/>
                                        </div>
                                         </ContentTemplate>
                                   <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID = "testBtn"/>
                                   </Triggers>
                                </asp:UpdatePanel>--%>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Youth ( aged 18 – 35 years):</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxyth" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="This textfield is mandatory"
                                         ControlToValidate="TextBoxyth" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Women:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBowmn" CssClass="form-control" Placeholder="Number Targeted" TextMode="Number" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBowmn" runat="server" ForeColor="Red" Display="Dynamic" /> 
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Men:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxmn" CssClass="form-control" Placeholder="Number Targeted" TextMode="Number" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxmn" runat="server" ForeColor="Red" Display="Dynamic" /> 
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Children (0-6 years old):</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBcldold" CssClass="form-control" Placeholder="Number Tageted" TextMode="Number" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBcldold" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Children ( 7- 14 years old):</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxold" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxold" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Children (15-17 years old):</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxren" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxren" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Orphans and Vulnerable Children:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxorph" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxorph" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">People with Terminal Illness:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxill" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxill" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Marginalized and pastoral population:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxmarg" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxmarg" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                            </div><br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Drug users:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxdrg" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxdrg" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Commercial Sex Workers:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxsxwrkr" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxsxwrkr" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                            </div><br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Teachers:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxtchr" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxtchr" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Farmers:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxfarmr" CssClass="form-control" placeholder="Number Target" TextMode="Number" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxfarmr" runat="server" ForeColor="Red" Display="Dynamic" />
                                          </div>
                                    </div>
                                </div>
                            </div><br />

                            <div class="row">
                                <div class="col-md-3"></div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <asp:Button ID="btnSaveTarget" runat="server" Text="Save Target Information" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnSaveTarget_Click" />
                                   </div>
                                </div>
                                 <div class="col-md-3">
                                         <asp:Button ID="btnNext5" runat="server" Text="Next Tab" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnNext5_OnClick" CausesValidation="False" UseSubmitBehavior="False" />          
                                 </div>
                            </div>
                            <br />
                        </div>
                    </asp:View>

                    <asp:View runat="server" ID="uploadDocs">
                        <asp:Label ID="lblErrMsg" runat="server" CssClass="text-left hidden" Visible="false"></asp:Label>
                        <span class="text-center text-danger"><small><%=lblErrMsg.Text %></small></span>
                        <div class="row">
                            <div class="col-md-12">
                                <label class="control-label form-control alert-info" style="font-weight: bold">Select Documents from your computer to upload</label>
                            </div>
                            <br/>

                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <label class="control-label form-control alert-info" style=" font-weight: bold;">Application Form</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:FileUpload ID="FileUpload" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="UploadFile" />
                                        </div>
                                        <div class="col-md-2">
                                            <label style=" color: green; font-weight:bold" runat="server" id="appFm"></label>
                                        </div>
                                    </div>
                                    <br/>
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <label class="control-label form-control alert-info" style="font-weight: bold;">Proposed Budget</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:FileUpload ID="FileUploadProjectBudget" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="btnProjectBudget" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnProjectBudget_OnClick" />
                                        </div>
                                        <div class="col-md-2">
                                            <label style="color: green; font-weight:bold" runat="server" id="lblPB"></label>
                                        </div>
                                    </div>
                                    <br/>
                                </div>
                            </div>
                            
                             <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <label class="control-label form-control alert-info" style=" font-weight:bold;">Bill of Quantities (Optional)</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:FileUpload ID="FileUploadtheBill" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="btnBillofQTY" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnBillofQTY_OnClick" />
                                        </div>
                                        <div class="col-md-2">
                                            <label style="color: green; font-weight:bold" runat="server" id="lblBill"></label>
                                        </div>
                                    </div>
                                    <br/>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <label class="control-label form-control alert-info" style="font-weight: bold;">Registration Certificate</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:FileUpload ID="FileUploadID" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="btnUploadID" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadID_OnClick" />
                                        </div>
                                        <div class="col-md-2">
                                            <label style="color: green; font-weight:bold" runat="server" id="lblID"></label>
                                        </div>
                                    </div>
                                    <br/>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <label class="control-label form-control alert-info" style="font-weight: bold;">Organizational Constitution/Strategy </label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:FileUpload ID="FileUploadConst" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="btnUploadConstitution" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadConstitution_OnClick" />
                                        </div>
                                         <div class="col-md-2">
                                            <label style="color: green; font-weight:bold" runat="server" id="lblOC"></label>
                                        </div>
                                    </div>
                                    <br/>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <label class="control-label form-control alert-info" style="font-weight: bold;">Ordinary /committee members List:  </label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:FileUpload ID="FileUploadList" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="btnUploadList" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadList_OnClick" />
                                        </div>
                                        <div class="col-md-2">
                                            <label style="color: green; font-weight: bold;" runat="server" id="lblUL"></label>
                                        </div>
                                    </div>
                                    <br/>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <label class="control-label form-control alert-info" style="font-weight: bold;">Recent Financial Report  </label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:FileUpload ID="FileUploadFinRePo" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="btnFinReport" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnFinReport_OnClick" />
                                        </div>
                                        <div class="col-md-2">
                                            <label style="color: green; font-weight: bold;" runat="server" id="lblFR"></label>
                                        </div>
                                    </div>
                                    <br/>
                                </div>
                            </div>
                            
                             <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <label class="control-label form-control alert-info" style=" font-weight: bold;">Audited Accounts (optional)  </label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:FileUpload ID="FileUploadAudit" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="btnAudit" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnAudit_OnClick" />
                                        </div>
                                         <div class="col-md-2">
                                            <label style="color: green; font-weight:bold" runat="server" id="lblAudit"></label>
                                        </div>
                                    </div>
                                    <br/>
                                </div>
                            </div>

                            <br/>
                            <hr />
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <p class="form-control alert alert-info" style="font-weight: bold;"> My Uploaded Documents</p>
                            </div>

                            <div class="col-md-12">
                                <asp:GridView ID="gridViewUploads" runat="server" CssClass="table table-striped table-advance table-hover footable"
                                     GridLines="None" EmptyDataText="No files uploaded"
                                     OnRowDeleting="gridViewUploads_OnRowDeleting" AutoGenerateColumns="False"
                                     DataKeyNames="Id, Grant_No" AlternatingRowStyle-BackColor="#C2D69B" AllowSorting="True">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="S/No:" />
                                        <asp:BoundField DataField="Document_Kind" HeaderText="Document" />
                                        <asp:BoundField DataField="Document_Name" HeaderText="File Name" />
                                        <asp:BoundField DataField="Document_type" HeaderText="File Type" />
                                         <asp:BoundField DataField="Grant_No" HeaderText="Grant Number" />
                                        <asp:BoundField DataField="Project_Name" HeaderText="Grant Name"/>
                                        <%--<asp:CommandField ShowDeleteButton="True" ButtonType="Button" />--%>
                                    </Columns>
                                </asp:GridView>
                                </div>
                            </div>
                        <div class="col-md-12">
                            <div class="form-group">
                              <asp:Button ID="btnNext6" runat="server" Text="Go to Submit" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnNext6_OnClick" CausesValidation="False" UseSubmitBehavior="False" />          
                           </div>
                        </div>
                                    
                    </asp:View>

                    </asp:MultiView>
                </div>
             
            </section>

            <script type="text/javascript">
                //Function to allow only numbers to textbox
                function validate4N(key) {
                    //getting key code of pressed key
                    var keycode = (key.which) ? key.which : key.keyCode;
                    var phn = document.getElementById('txtPhoneNo');
                    //comparing pressed keycodes
                    if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                        return false;
                    } else {
                        //Condition to check textbox contains ten numbers or not
                        if (id.value.length < 15) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                }

                function validateID(key) {
                    //getting key code of pressed key
                    var keycode = (key.which) ? key.which : key.keyCode;
                    var id = document.getElementById('txtIDNo');
                    //comparing pressed keycodes
                    if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                        return false;
                    } else {
                        //Condition to check textbox contains ten numbers or not
                        if (id.value.length < 15) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                }
            </script>

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

            <script type="text/javascript">
                $(function() {
                    $('#TextBoxcost').on('onkeyup', function () {
                        var x = $('#TextBoxcost').val();
                        $('#TextBoxcont').val(0.5 * (x));
                        
                    });
                });

                function addCommas(x) {
                    var parts = x.toString().split(".");
                    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    return parts.join(".");
                }

                function Check(textBox, maxLength) {
                    document.getElementById("lblCharLeft").innerHTML = maxLength - textBox.value.length + " characters left";
                    if (textBox.value.length > maxLength) {
                        document.getElementById("lblCharLeft").style.color = "red";
                        textBox.value = textBox.value.substr(0, maxLength);
                        document.getElementById("lblCharLeft").innerHTML = maxLength - textBox.value.length + " characters left";
                    }
                    else if (textBox.value.length < maxLength) {
                        document.getElementById("lblCharLeft").style.color = "Black";
                    }
                    else {
                        document.getElementById("lblCharLeft").style.color = "red";
                    }

                }
                function getKCDFContribution()
                {
                    var _txt1 = document.getElementById('<%= TextBoxcost.ClientID %>').value;
                    var _txt2 = document.getElementById('<%= TextBoxcont.ClientID %>').value;

                    var _minused = _txt1 - _txt2;
                       
                    if (!isNaN(_txt1) && !isNaN(_txt2)) {
                        document.getElementById('<%= TextBoxrequested.ClientID %>').value = _minused;
                    } else if (_minused<1) {
                        document.getElementById("lblCharLeft").style.color = "red";
                        document.getElementById("lblCharLeft").innerHTML = "Invalid Entry!, Click Refresh";
                        document.getElementById('<%= TextBoxrequested.ClientID %>').value = "";
                        document.getElementById('<%= TextBoxrequested.ClientID %>').style.color = "red";
                    }
                }

                function ValidatemeSpecialCase(textBox) {
                    var maxLength = document.getElementById('<%= TextBoxcost.ClientID %>').value.length;
                    document.getElementById("lblCharLeft").innerHTML = maxLength - textBox.value.length + " characters left";
                    var subs = maxLength - textBox.value.length;
                    if (subs === 0) {
                        //document.getElementById("lblCharLeft").innerHTML = "Input error!";
                        document.getElementById("lblCharLeft").style.color = "red";
                        document.getElementById('<%= TextBoxrequested.ClientID %>').disabled = true;
                    }
                    if (textBox.value.length > maxLength) {
                        document.getElementById("lblCharLeft").style.color = "red";
                        textBox.value = textBox.value.substr(0, maxLength);
                        document.getElementById("lblCharLeft").innerHTML = maxLength - textBox.value.length + " characters left";
                    }
                    else if (textBox.value.length < maxLength) {
                        document.getElementById("lblCharLeft").style.color = "Black";
                    }
                    else {
                        document.getElementById("lblCharLeft").style.color = "red";
                    }
                    getKCDFContribution();
                }

                function CheckView2(textBox, maxLength) {
                    document.getElementById("lblCharLeft2").innerHTML = maxLength - textBox.value.length + " characters left";
                    if (textBox.value.length > maxLength) {
                        document.getElementById("lblCharLeft2").style.color = "red";
                        textBox.value = textBox.value.substr(0, maxLength);
                        document.getElementById("lblCharLeft2").innerHTML = maxLength - textBox.value.length + " characters left";
                    }
                    else if (textBox.value.length < maxLength) {
                        document.getElementById("lblCharLeft2").style.color = "Black";
                    }
                    else {
                        document.getElementById("lblCharLeft2").style.color = "red";
                    }
                }

                function CheckView3(textBox, maxLength) {
                    document.getElementById("lblCharLeft3").innerHTML = maxLength - textBox.value.length + " characters left";
                    if (textBox.value.length > maxLength) {
                        document.getElementById("lblCharLeft3").style.color = "red";
                        textBox.value = textBox.value.substr(0, maxLength);
                        document.getElementById("lblCharLeft3").innerHTML = maxLength - textBox.value.length + " characters left";
                    }
                    else if (textBox.value.length < maxLength) {
                        document.getElementById("lblCharLeft3").style.color = "Black";
                    }
                    else {
                        document.getElementById("lblCharLeft3").style.color = "red";
                    }
                }

                function CheckView4(textBox, maxLength) {
                    document.getElementById("lblCharLeft4").innerHTML = maxLength - textBox.value.length + " characters left";
                    if (textBox.value.length > maxLength) {
                        document.getElementById("lblCharLeft4").style.color = "red";
                        textBox.value = textBox.value.substr(0, maxLength);
                        document.getElementById("lblCharLeft4").innerHTML = maxLength - textBox.value.length + " characters left";
                    }
                    else if (textBox.value.length < maxLength) {
                        document.getElementById("lblCharLeft4").style.color = "Black";
                    }
                    else {
                        document.getElementById("lblCharLeft4").style.color = "red";
                    }
                }
            </script>
            
            <script>
            function ConfirmDelete() {
                    var Delet_Confirm = confirm("Do you really want to delete this record ?");
                    if (Delet_Confirm == true) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }

            </script>
            
            <script type="text/javascript" >
                function IfYesIts() {
                    var ifYsId = document.getElementById("idKCDFyes");
                    ifYsId.style.display = "block";
                }
                function pageLoad() {
                    $('.selectpicker').selectpicker();
                } 
                function ClearOverviewFields() {
                    document.getElementById('<%= txtDonor.ClientID %>').value = '';
                    document.getElementById('<%= txtAmount.ClientID %>').value = '';
                    document.getElementById('<%= TextObj.ClientID %>').value = '';
                    document.getElementById('<%= txtAreaCounties.ClientID %>').value = '';
                    document.getElementById('<%= TextTypeBeneficiary.ClientID %>').value = '';
                    document.getElementById('<%= TextNoBeneficiary.ClientID %>').value = '';
                }
                function ErrorLabel1() {
                    document.getElementById('<%= appFm.ClientID %>').style.color = 'Red';
                }
                function ErrorLabel2() {
                    document.getElementById('<%= lblPB.ClientID %>').style.color = 'Red';
                }
                function ErrorLabel3() {
                     document.getElementById('<%= lblBill.ClientID %>').style.color = 'Red';
                }
                function ErrorLabel3() {
                    document.getElementById('<%= lblID.ClientID %>').style.color = 'Red';
                }
                function ErrorLabel4() {
                    document.getElementById('<%= lblOC.ClientID %>').style.color = 'Red';
                }
                function ErrorLabel5() {
                     document.getElementById('<%= lblUL.ClientID %>').style.color = 'Red';
                }
                 function ErrorLabel6() {
                     document.getElementById('<%= lblFR.ClientID %>').style.color = 'Red';
                }
                function ErrorLabel7() {
                     document.getElementById('<%= lblAudit.ClientID %>').style.color = 'Red';
                }
            </script> 
         
            <script>
                function CheckTextArea(textArea, maxLength) {
                    document.getElementById("lblCharleftTextarea").innerHTML = maxLength - textArea.value.length + " characters left";
                    if (textArea.value.length > maxLength) {
                        document.getElementById("lblCharleftTextarea").style.color = "red";
                        textArea.value = textArea.value.substr(0, maxLength);
                        document.getElementById("lblCharleftTextarea").innerHTML = maxLength - textArea.value.length + " characters left";
                    }
                    else if (textArea.value.length < maxLength) {
                        document.getElementById("lblCharleftTextarea").style.color = "Black";
                    }
                    else {
                        document.getElementById("lblCharleftTextarea").style.color = "red";
                    }
                }
            </script>
            <script>
                function TesctEmpty() {
                    if (document.getElementById('<%= TextBxcont.ClientID %>').value == "" ||
                        document.getElementById('<%= txtRegNo.ClientID %>').value == "" ||
                        document.getElementById('<%= txOrgname.ClientID %>').value == "" ||
                        document.getElementById('<%= TextBoposition.ClientID %>').value == "" ||
                        document.getElementById('<%= TextBoxphone.ClientID %>').value == "" ||
                        document.getElementById('<%= TextBoxorg.ClientID %>').value == "") {
                        alert("Please complete your user profile before proceeding!");
                        window.location.href = "/Account/Add_Grantee_Profile.aspx";
                    }
                }
                
            </script>
        </div>
    </asp:Content>