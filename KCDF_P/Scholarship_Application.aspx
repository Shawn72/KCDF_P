<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Scholarship_Application.aspx.cs" MasterPageFile="~/Applications.Master" Inherits="KCDF_P.Grant_Application" %>
<%@ OutputCache NoStore="true" Duration="1" VaryByParam="*"   %>
<asp:Content ID="studentRegistrationForm" ContentPlaceHolderID="MainContent" runat="server">
<div class="panel-body" style="font-family:Trebuchet MS">
  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <div class="row">
    <div class="col-md-12">
    <h4 style="align-content:center; font-family:Trebuchet MS; color:#0094ff">Scholarship Application Form</h4><br /></div>
</div>
    
<div class="form-horizontal">
<div class="form-group">
    <asp:Label runat="server"  CssClass="col-md-3 control-label">Select Scholarship:</asp:Label>
        <div class="col-md-6">
            <asp:DropDownList ID="ddlScolarshipType" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                    data-live-search="true" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlScolarshipType_OnSelectedIndexChanged">
            </asp:DropDownList>
        </div> 
</div>
</div>
<br/>
    
<%-- <%
     var selProj = ddlScolarshipType.SelectedValue;
        var proFtures = nav.call_for_Proposal.ToList().Where(bf => bf.Call_Ref_Number == selProj);
        var itswhats = proFtures.Select(f => f.Basic_Features).SingleOrDefault();

        if (itswhats == false)
        {
    %>
 <header class="panel-heading tab-bg-info">
         <asp:Menu ID="scholarshipDataCollection" Orientation="Horizontal"  StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server" OnMenuItemClick="scholarshipDataCollection_OnMenuItemClick">
            <Items>
                <asp:MenuItem Text="Applicant's Profile |" Value="0" Selected="true" runat="server"/>
                <asp:MenuItem Text="Scholarstic Support |" Value="1" runat="server"/> 
                <asp:MenuItem Text="Work Plan And Budget |" Value="2" runat="server"/>
                <asp:MenuItem Text="Attach Documents |" Value="3" runat="server"/> 
                <asp:MenuItem Text="Bank Details |" Value="4" runat="server"/>
                <asp:MenuItem Text="Final Submit |" Value="5" runat="server"/>
            </Items>
        </asp:Menu>
</header>
  <% }
        else if (itswhats == true)
        {
    %>
<header class="panel-heading tab-bg-info">
         <asp:Menu ID="scholarMenu2" Orientation="Horizontal"  StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server" OnMenuItemClick="scholarMenu2_OnMenuItemClick">
            <Items>
                <asp:MenuItem Text="Applicant's Profile |" Value="0" Selected="true" runat="server"/>
                <asp:MenuItem Text="Work Plan And Budget |" Value="1" runat="server"/>
                <asp:MenuItem Text="Attachments |" Value="2" runat="server"/>
                <asp:MenuItem Text="Bank Details |" Value="3" runat="server"/>
                <asp:MenuItem Text="Final Submit |" Value="4" runat="server"/>
            </Items>
        </asp:Menu>
</header>    
 <% } %>--%>
 <asp:Label ID="lblError" runat="server" ForeColor="#FF3300" CssClass="text-left hidden"></asp:Label>
 <section class="panel">
 <div class="panel panel-primary">
 <span class="text-center text-danger"><small><%=lblError.Text %></small></span> 
     
<asp:Label ID="lblErrMsg" runat="server" CssClass="text-left hidden" Visible="false"></asp:Label>
<span class="text-center text-danger"><small><%=lblErrMsg.Text %></small></span>

      <asp:MultiView ID="profileMultiview" runat="server">
          <asp:View runat="server" ID="applicationForm">
          
            <table width="100%">
            <tr>
                <td width="100%">
                    <asp:Wizard ID="Wizard1" runat="server" BackColor="#EFF3FB" BorderColor="#B5C7DE"
                        BorderWidth="1px" Font-Names="Tebuchet MS" Font-Size="0.9em" Width="100%"  ActiveStepIndex="0" OnFinishButtonClick="OnFinish">
                        <StepStyle Font-Size="0.9em" ForeColor="#333333" />
                        <WizardSteps>
                        
                            <asp:WizardStep runat="server" Title="Applicant's Information" StepType="Auto">
                                <fieldset>
                                    <legend>Applicant's Information</legend>
                                    <div style="height: Auto; text-align: center;">
                                        <div class="form-horizontal">
                                            <br/>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">1</span> Your Personal Basic Information:</label> 
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                <asp:Label runat="server" CssClass="col-md-3 control-label">First Name:</asp:Label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox runat="server" ID="txtfNname" CssClass="form-control" style="text-transform:uppercase" Enabled="False"/> 
                                      
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Middle Name:</asp:Label>
                                                        <div class="col-md-6">
                                                            <asp:TextBox runat="server" ID="txtMname" CssClass="form-control" style="text-transform:uppercase" Enabled="False" />              
                                                        </div> 
                                                </div>   
          
                                                <div class="form-group">
                                                    <asp:Label runat="server" AssociatedControlID="txtLname" CssClass="col-md-3 control-label">Last Name:</asp:Label>
                                                        <div class="col-md-6">
                                                            <asp:TextBox runat="server" ID="txtLname" CssClass="form-control" style="text-transform:uppercase" Enabled="False"/>   
                                                        </div> 
                                                </div> 

                    
                                                    <div class="row">
                                                    <div class="col-md-12">
                                                        <p class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">2</span> Create An Application.</p> 
                                                    </div>
                                                    </div>

                                                        <br/>
                                                        <div class="form-group">
                                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Total Budget:</asp:Label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox runat="server" ID="txtBudgetTotal" CssClass="form-control" TextMode="Number" Enabled="True"/> 
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ErrorMessage="Required field!"
                                                                ControlToValidate="txtBudgetTotal" runat="server" ForeColor="Red" Display="Dynamic" />               
                                                            </div><span class="required">*</span>
                                                        </div>
                                                
                                                     <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                                     <ContentTemplate>
                                                        <div class="form-group">
                                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Date Required:</asp:Label>
                                                            <div class="col-md-4">
                                                               <div class="input-group date">
                                                                    <input type="text" class="form-control" id="txtWorkplanDate" runat="server"/><span class="input-group-addon"><i class="glyphicon glyphicon-th"></i></span>
                                                                </div>      
                                                            </div> 

                                                            <div class="col-md-2">
                                                                <asp:Button ID="btnSaveApplication" runat="server" Text="Create Scholarship Application" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnSaveApplication_OnClick" CausesValidation="True" Enabled="False" />          
                                                            </div>
                                                       
                                                        </div>
                                                        <div class="form-group">
                                                            <div id="idAlreadyApplied" style="display:none">
                                                            <div class="col-md-12">
                                                                <label class="form-control alert alert-danger" style="font-weight: bold;">You have already Applied for  this project!</label>
                                                            </div>
                                                            </div> 
                                                        </div>

                                                        </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID = "btnSaveApplication" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel> 
                                                  
                                                    <br/>  
                                                </div>
                                        
                                            </div>
                                </fieldset>
                            </asp:WizardStep>

                            <asp:WizardStep runat="server" Title="Scholarstic Support" StepType="Auto">
                                <fieldset>
                                    <legend>Scholarstic Support</legend>
                                    <div style="height: Auto; text-align: center; width: Auto;">
                                       <div class="form-horizontal">
                                        <br/>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p class="form-control alert alert-info" style="font-weight: bold;">
                                                    <span class="badge alert-danger">4</span> Please give a breakdown of the total cost of your course.
                                                </p> 
                                            </div>
                                        </div>
                                        <br/>
                                        <div class="row">
                                          <div class="col-md-4">
                                            <div class="form-group">
                                            <asp:Label runat="server" CssClass="col-md-3 control-label">Year of study:</asp:Label>
                                                <div class="col-md-6">
                                                       <asp:TextBox runat="server" ID="lblYearofStdy" CssClass="form-control" Enabled="False"></asp:TextBox>
                                                 </div> 
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                             <asp:GridView ID="grdViewScholarS" runat="server" CssClass="table table-condensed footable" Width="100%" AutoGenerateSelectButton="false" 
                                            EmptyDataText="No Scholarstic Support data Found!" DataKeyNames="No" OnRowDeleting="grdViewScholarS_OnRowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="No" HeaderText="S/No:"/>
                                            <asp:BoundField DataField="Year" HeaderText="Year" />
                                            <asp:BoundField DataField="Tuition" HeaderText="Tuition Fee (KES):" DataFormatString="{0:N2}"/>
                                            <asp:BoundField DataField="Academic" HeaderText="Accomodation Fee (KES):" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="Upkeep" HeaderText="Upkeep Fee (KES):" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="Scholarstic_Material" HeaderText="Scholarstic Materials (KES):" DataFormatString="{0:N2}" />
                                            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                        </Columns>
                                        <SelectedRowStyle BackColor="#259EFF" BorderColor="#FF9966" /> 
                                            </asp:GridView>
                                         </div>
                                         </div>

                                           <div class="row">
                                            <div class="col-md-12">
                                               <div ID="idSecod" runat="server" Visible="false">
                                                    <asp:GridView ID="gridViewSecoDLevel" runat="server" AutoGenerateColumns="false" Width="70%"
                                                AlternatingRowStyle-BackColor="#C2D69B" HeaderStyle-BackColor="#a2dcc7" ShowFooter="true">
                                                <Columns>
                                                     <asp:TemplateField  HeaderText="Year">
                                                        <ItemTemplate>
                                                            <%# Eval("Year") %>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                            <asp:DropDownList ID="ddlForm" runat="server"
                                                                              AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="-1">Choose Class</asp:ListItem>
                                                                    <asp:ListItem Value="0">Form 1</asp:ListItem>
                                                                    <asp:ListItem Value="1">Form 2</asp:ListItem>
                                                                    <asp:ListItem Value="2">Form 3</asp:ListItem>
                                                                    <asp:ListItem Value="3">Form 4</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField  HeaderText="Term">
                                                        <ItemTemplate>
                                                            <%# Eval("Semester") %>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                            <asp:DropDownList ID="ddlTerm" runat="server"
                                                                              AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="-1">Choose Term</asp:ListItem>
                                                                    <asp:ListItem Value="0">Term 1</asp:ListItem>
                                                                    <asp:ListItem Value="1">Term 2</asp:ListItem>
                                                                    <asp:ListItem Value="2">Term 3</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                
                                                    <asp:TemplateField HeaderText="Tuition">
                                                        <ItemTemplate>
                                                            <%# Eval("Tuition") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtTuition" runat="server" TextMode="Number" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accomodation">
                                                        <ItemTemplate>
                                                            <%# Eval("Academic") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtAcademic" runat="server" TextMode="Number"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Upkeep">
                                                        <ItemTemplate>
                                                            <%# Eval("Upkeep") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtUpkeep" runat="server" TextMode="Number" ></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Scholarstic Material">
                                                        <ItemTemplate>
                                                            <%# Eval("Scholarstic_Material") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtScholarMat" runat="server" TextMode="Number" ></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField>
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAddSecLevel" runat="server" Text="Add" OnClick="btnAddSecLevel_OnClick" CommandName = "Footer" CssClass="btn btn-primary pull-left btn-sm" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <AlternatingRowStyle BackColor="#C2D69B" />
                                                <EmptyDataTemplate>
                                                    <tr style="background-color: #b1e8e9;">
                                                         <th scope="col">
                                                            Year
                                                        </th>
                                                        <th scope="col">
                                                            Term
                                                        </th>
                                                        <th scope="col">
                                                            Tuition
                                                        </th>
                                                        <th scope="col">
                                                            Accomodation
                                                        </th>
                                                        <th scope="col">
                                                            Upkeep
                                                        </th>
                                                        <th scope="col">
                                                            Scholarstic Material
                                                        </th>
                                                        <th scope="col">
                   
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                             <asp:DropDownList ID="ddlForm" runat="server"
                                                                              AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="-1">Choose Class</asp:ListItem>
                                                                    <asp:ListItem Value="0">Form 1</asp:ListItem>
                                                                    <asp:ListItem Value="1">Form 2</asp:ListItem>
                                                                    <asp:ListItem Value="2">Form 3</asp:ListItem>
                                                                    <asp:ListItem Value="3">Form 4</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                             <asp:DropDownList ID="ddlTerm" runat="server"
                                                                              AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="-1">Choose Term</asp:ListItem>
                                                                    <asp:ListItem Value="0">Term 1</asp:ListItem>
                                                                    <asp:ListItem Value="1">Term 2</asp:ListItem>
                                                                    <asp:ListItem Value="2">Term 3</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTuition" runat="server" TextMode="Number"  />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAcademic" runat="server" TextMode="Number" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtUpkeep" runat="server" TextMode="Number" />
                                                        </td>
                                                         <td>
                                                            <asp:TextBox ID="txtScholarMat" runat="server" TextMode="Number" />
                                                        </td>
                                                        <td>
                                                             <asp:Button ID="btnAddSecLevel" runat="server" Text="Add" OnClick="btnAddSecLevel_OnClick" CommandName = "Footer" CssClass="btn btn-primary pull-left btn-sm" />
                                                        </td>
                                                    </tr>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                                   <br/>
                                                </div> 
                                              <div ID="idHighEd" runat="server" Visible="False">
                                                     <asp:GridView ID="gridViewAddThings" runat="server" AutoGenerateColumns="false" Width="80%"
                                                AlternatingRowStyle-BackColor="#C2D69B" HeaderStyle-BackColor="#a2dcc7" ShowFooter="true">
                                                <Columns>
                                                     <asp:TemplateField  HeaderText="Year">
                                                        <ItemTemplate>
                                                            <%# Eval("Year") %>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                            <asp:DropDownList ID="ddlYearIS" runat="server"
                                                                              AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="-1">Choose Year</asp:ListItem>
                                                                    <asp:ListItem Value="0">First Year</asp:ListItem>
                                                                    <asp:ListItem Value="1">Second Year</asp:ListItem>
                                                                    <asp:ListItem Value="2">Third Year</asp:ListItem>
                                                                    <asp:ListItem Value="3">Fourth Year</asp:ListItem>
                                                                    <asp:ListItem Value="4">Fifth Year</asp:ListItem>
                                                                    <asp:ListItem Value="5">Sixth Year</asp:ListItem>
                                                                    <asp:ListItem Value="6">Seventh Year</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField  HeaderText="Semester">
                                                        <ItemTemplate>
                                                            <%# Eval("Semester") %>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                            <asp:DropDownList ID="ddlSem" runat="server"
                                                                              AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="-1">Choose Sem</asp:ListItem>
                                                                    <asp:ListItem Value="0">Semester 1</asp:ListItem>
                                                                    <asp:ListItem Value="1">Semester 2</asp:ListItem>
                                                                    <asp:ListItem Value="2">Semester 3</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                
                                                    <asp:TemplateField HeaderText="Tuition">
                                                        <ItemTemplate>
                                                            <%# Eval("Tuition") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtTuition" runat="server" TextMode="Number" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accomondation">
                                                        <ItemTemplate>
                                                            <%# Eval("Academic") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtAcademic" runat="server" TextMode="Number"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Upkeep">
                                                        <ItemTemplate>
                                                            <%# Eval("Upkeep") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtUpkeep" runat="server" TextMode="Number" ></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Scholarstic Material">
                                                        <ItemTemplate>
                                                            <%# Eval("Scholarstic_Material") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtScholarMat" runat="server" TextMode="Number" ></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField>
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_OnClick" CommandName = "Footer" CssClass="btn btn-primary pull-left btn-sm" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <AlternatingRowStyle BackColor="#C2D69B" />
                                                <EmptyDataTemplate>
                                                    <tr style="background-color: #b1e8e9;">
                                                         <th scope="col">
                                                            Year
                                                        </th>
                                                        <th scope="col">
                                                            Semester
                                                        </th>
                                                        <th scope="col">
                                                            Tuition
                                                        </th>
                                                        <th scope="col">
                                                            Accomodation
                                                        </th>
                                                        <th scope="col">
                                                            Upkeep
                                                        </th>
                                                        <th scope="col">
                                                            Scholarstic Material
                                                        </th>
                                                        <th scope="col">
                   
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                             <asp:DropDownList ID="ddlYearIS" runat="server"
                                                                              AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="-1">Choose Year</asp:ListItem>
                                                                    <asp:ListItem Value="0">First Year</asp:ListItem>
                                                                    <asp:ListItem Value="1">Second Year</asp:ListItem>
                                                                    <asp:ListItem Value="2">Third Year</asp:ListItem>
                                                                    <asp:ListItem Value="3">Fourth Year</asp:ListItem>
                                                                    <asp:ListItem Value="4">Fifth Year</asp:ListItem>
                                                                    <asp:ListItem Value="5">Sixth Year</asp:ListItem>
                                                                    <asp:ListItem Value="6">Seventh Year</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                             <asp:DropDownList ID="ddlSem" runat="server"
                                                                              AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="-1">Choose Sem</asp:ListItem>
                                                                    <asp:ListItem Value="0">Semester 1</asp:ListItem>
                                                                    <asp:ListItem Value="1">Semester 2</asp:ListItem>
                                                                    <asp:ListItem Value="2">Semester 3</asp:ListItem>

                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTuition" runat="server" TextMode="Number"  />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAcademic" runat="server" TextMode="Number" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtUpkeep" runat="server" TextMode="Number" />
                                                        </td>
                                                         <td>
                                                            <asp:TextBox ID="txtScholarMat" runat="server" TextMode="Number" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_OnClick" CommandName = "EmptyDataTemplate" CssClass="btn btn-primary pull-left btn-sm" />
                                                        </td>
                                                    </tr>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                              </div>
                                              <br/> 

                                           </div>
                                           </div>

                                        </div>
                                    </div>
                                </fieldset>
                            </asp:WizardStep>

                            <asp:WizardStep runat="server" Title="Attach Documents" StepType="Auto">
                                <fieldset>
                                    <legend>Attach Documents</legend>
                                    <div style="height: Auto; text-align: center; width: Auto;">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label class="control-label form-control alert-info" style="font-weight: bold;">Select Documents from your computer to upload</label>
                                            </div>
                                            <br/>

                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-md-5">
                                                            <label class="control-label form-control alert-info" style="font-weight: bold;">Scholarship Form</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:FileUpload ID="FileUploadSDoc" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                            <asp:Button ID="btnUploadSDc" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadSDc_OnClick"/>
                                                        </div>
                                                    </div>
                                                    <br/>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-md-5">
                                                            <label class="control-label form-control alert-info" style="font-weight: bold;">Admission Letter, Fee Structure (Attach in One Document)</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:FileUpload ID="FileUploadCF" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                            <asp:Button ID="btnUploadCF" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadCF_OnClick"/>
                                                        </div>
                                                    </div>
                                                    <br/>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-md-5">
                                                            <label class="control-label form-control alert-info" style="font-weight: bold;">National ID /or Student ID </label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:FileUpload ID="FileUploadSNID" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                            <asp:Button ID="btnUploadNID" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadNID_OnClick" />
                                                        </div>
                                                    </div>
                                                    <br/>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-md-5">
                                                            <label class="control-label form-control alert-info" style="font-weight: bold;">Passport Photo  </label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:FileUpload ID="FileUploadPhoto" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                            <asp:Button ID="btnUploadPhoto" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadPhoto_OnClick" />
                                                        </div>
                                                    </div>
                                                    <br/>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-md-5">
                                                            <label class="control-label form-control alert-info" style="font-weight: bold;">Guardian’s Concurrence Letter  </label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:FileUpload ID="FileUploadGurdLeter" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                            <asp:Button ID="btnUploadGuardLeter" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadGuardLeter_OnClick"/>
                                                        </div>
                                                    </div>
                                                    <br/>
                                                </div>
                                            </div>
                
                                             <div class="col-md-12">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-md-5">
                                                            <label class="control-label form-control alert-info" style="font-weight: bold;"> Dean/ Departmental Chair Testimonial  </label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:FileUpload ID="FileUploadDeansTest" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                            <asp:Button ID="btnUploadDeansTest" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadDeansTest_OnClick" />
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
                                                <asp:GridView ID="gridmyViewUploads" runat="server" CssClass="table table-striped table-advance table-hover footable"
                                                GridLines="None" EmptyDataText="No files uploaded"
                                                OnRowDeleting="gridmyViewUploads_OnRowDeleting" AutoGenerateColumns="False"
                                                DataKeyNames="Id, Scholarship_No" AlternatingRowStyle-BackColor="#C2D69B" AllowSorting="True">
                                                    <Columns>
                                                        <asp:BoundField DataField="Id" HeaderText="S/No:" />
                                                        <asp:BoundField DataField="Document_Kind" HeaderText="Document" />
                                                        <asp:BoundField DataField="Document_Name" HeaderText="File Name" />
                                                        <asp:BoundField DataField="Document_type" HeaderText="File Type" />
                                                        <asp:BoundField DataField="Scholarship_No" HeaderText="Scholarship Number" />
                                                        <%--<asp:CommandField ShowDeleteButton="True" ButtonType="Button" />--%>
                                                    </Columns>
                                                </asp:GridView>
                                                </div>
                                            </div>

                                    </div>
                                </fieldset>
                            </asp:WizardStep>

                            <asp:WizardStep runat="server" Title="Submit Application" StepType="Finish">
                                <fieldset>
                                    <legend>Submit Application</legend>
                                    <div style="height: Auto; text-align: center; width: Auto;">
                                        <div class="form-horizontal">
                                   <div class="row">
                                    <div class="col-md-12">
                                        <p class="form-control alert alert-info" style="font-weight: bold;"> Preview Filled Information before submitting.</p> 
                                        </div>
                                    </div>
                                     <br/>
                  
                                   <div class="form-group">
                                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Email Address:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtEmailAdd" CssClass="form-control" Enabled="False" />              
                                        </div> 
                                    </div>

                                <div class="form-group">
                                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Residence:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtResidence" CssClass="form-control" style="text-transform:uppercase" Enabled="False" />              
                        
                                        </div> 
                                </div>
      
                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-3 control-label">Mobile Number:</asp:Label>
                                       <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="txtPhoneNo" CssClass="form-control" required="True" Enabled="False"/>              
                               
                                         </div> 
                                    </div> 
          
                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-3 control-label">ID/Passport/Admission No:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtIDNo" CssClass="form-control" required="True" Enabled="False"/>              
                
                                        </div> 
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-3 control-label">Gender:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="lstGender" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="False">
                                           <asp:ListItem>--Select Gender--</asp:ListItem>
                                                <asp:ListItem>Male</asp:ListItem>
                                                <asp:ListItem>Female</asp:ListItem>
                                           </asp:DropDownList>              
                                        </div> 
                                </div>

                                <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Date of Birth:</asp:Label>
                                    <div class="col-md-6">
                                       <asp:TextBox ID="dateofBirth" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>   
                                    </div> 
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                            <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">2</span> Your Education Background, Guardian And Referees:</label> 
                                     </div>
                                </div>
                                <br/>

                               <div class="form-group">
                                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Name of University/College:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtCollege" CssClass="form-control" style="text-transform:uppercase" Enabled="False" />              
                        
                                        </div> 
                                </div>
              
                               <div class="form-group">
                                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Faculty & Degree of Study:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtDegree" CssClass="form-control" style="text-transform:uppercase" Enabled="False"/>              
                        
                                        </div> 
                                </div>
              
                               <div class="form-group">
                                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Year of Study:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtYearofStudy" CssClass="form-control" style="text-transform:uppercase" Enabled="False"/>              
                        
                                        </div> 
                                </div>

                               <div class="form-group">
                                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Year of Admission:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtAdmittedWhen" CssClass="form-control" style="text-transform:uppercase" Enabled="False"/>              
                        
                                        </div> 
                                </div>
              
                               <div class="form-group">
                                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Year of Completion:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtYearofCompltn" CssClass="form-control" style="text-transform:uppercase" Enabled="False" />              
                        
                                        </div> 
                                </div>
              
                               <div class="form-group">
                                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Parent/Guardian Phone:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtGuardianPhone" CssClass="form-control" style="text-transform:uppercase" Enabled="False" />              
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ErrorMessage="Required field!"
                                     ControlToValidate="txtGuardianPhone" runat="server" ForeColor="Red" Display="Dynamic" />  
                                        </div><span class="required">*</span>
                                </div>
              
                               <div class="form-group">
                                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Parent/Guardian Email:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtGuardianEmail" CssClass="form-control" Enabled="False" />              
                                        </div> 
                                </div>
              
                               <div class="form-group">
                                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Parent/Guardian Address:</asp:Label>
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtGuardianAddress" CssClass="form-control" Enabled="False" style="text-transform:uppercase"/>
                                        </div>
                                </div>
               
                              </div>

                                <br />
                                    </div>
                                </fieldset>
                            </asp:WizardStep>
                        </WizardSteps>
                        <SideBarButtonStyle Font-Names="Tebuchet MS" ForeColor="#fed189" />
                        <NavigationButtonStyle BackColor="White" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="Tebuchet MS" Font-Size="0.9em" ForeColor="#284E98" />
                        <SideBarStyle BackColor="#00a367" Font-Size="1.0em" Width="20%" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#00a367" BorderColor="#EFF3FB" BorderStyle="Solid" BorderWidth="2px"
                            Font-Bold="True" Font-Size="0.9em" ForeColor="#fed189" HorizontalAlign="Center" />
                        <HeaderTemplate>
                            Scholarship Application Form
                        </HeaderTemplate>

                        <StartNavigationTemplate>
                         <table cellpadding="3" cellspacing="3">
                             <tr>
                                 <td>
                                     <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                         CausesValidation="false" 
                                         OnClientClick="return confirm('Are you sure you want to cancel');"
                                         CssClass="btn btn-primary pull-left btn-sm"  OnClick="btnCancel_OnClick"
                                      />
                                 </td>
                                 <td>
                                     <asp:Button ID="btnNext" runat="server" Text="Next >>"
                                         CausesValidation="true" 
                                         CommandName="MoveNext" CssClass="btn btn-primary pull-left btn-sm"
                                      />&nbsp;
                                 </td>
                             </tr>
                         </table>
                     </StartNavigationTemplate>
                     <StepNavigationTemplate>
                         <table cellpadding="3" cellspacing="3">
                             <tr>
                                 <td>
                                     <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                         CausesValidation="false" 
                                         OnClientClick="return confirm('Are you sure you want to cancel');" 
                                         CssClass="btn btn-primary pull-left btn-sm"  OnClick="btnCancel_OnClick"
                                      />
                                 </td>
                                 <td>
                                     <asp:Button ID="btnPrevious" runat="server" Text="<< Previous"
                                         CausesValidation="false" 
                                         CommandName="MovePrevious" 
                                         CssClass="btn btn-primary pull-left btn-sm"
                                      />&nbsp;
                                      <asp:Button ID="btnNext" runat="server" Text="Next >>"
                                         CausesValidation="true" 
                                         CommandName="MoveNext" 
                                          CssClass="btn btn-primary pull-left btn-sm"
                                      />
                                 </td>
                             </tr>
                         </table>
                     </StepNavigationTemplate>
                     <FinishNavigationTemplate>
                         <table cellpadding="3" cellspacing="3">
                             <tr>
                                 <td>
                                     <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                         CausesValidation="false" 
                                         OnClientClick="return confirm('Are you sure you want to cancel');" 
                                         CssClass="btn btn-primary pull-left btn-sm"  OnClick="btnCancel_OnClick"
                                      />
                                 </td>
                                 <td>
                                      <asp:Button ID="btnFinish" runat="server" Text="Submit Application"
                                         CausesValidation="true" 
                                         CommandName="MoveComplete" 
                                          CssClass="btn btn-primary pull-left btn-sm" OnClick="btnSubmitApplication_OnClick" 
                                      />
                                 </td>
                             </tr>
                         </table>
                     </FinishNavigationTemplate>

                    </asp:Wizard>
                </td>
            </tr>
        </table> 

          <%--<asp:View runat="server" ID=MyrefsView>
              <div class="form-horizontal">
                <div class="row">
                    <div class="col-md-12">
                        <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">3</span> My Referees</label> 
                    </div>
                </div>
              <br/>
                 <asp:GridView ID="tblRefs" runat="server" CssClass="table table-condensed footable" Width="100%" AutoGenerateSelectButton="false" 
                 EmptyDataText="No Referees Found!" OnSelectedIndexChanged="tblRefs_OnSelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="firstname" HeaderText="First Name" />
                    <asp:BoundField DataField="secondname" HeaderText="Middle Name:" />
                    <asp:BoundField DataField="lastname" HeaderText="Last Name:" />
                    <asp:BoundField DataField="phoneNumber" HeaderText="Phone Number:" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                           <asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_OnClick" >Delete Referee
                           </asp:LinkButton>
                        </ItemTemplate>                        
                    </asp:TemplateField>
                </Columns>
                <SelectedRowStyle BackColor="#259EFF" BorderColor="#FF9966" /> 
                 </asp:GridView>
                  
                 <div class="col-md-4"></div> 
                 <div class="col-md-4 form-group">
                      <asp:Button ID="btnGoNext2" runat="server" Text="Next Tab" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnSubmitApplication_OnClick" CausesValidation="True" />          
                 </div>
                 <div class="col-md-4"></div> 
               
              </div>
          </asp:View>--%>
          <asp:View runat="server" ID="workplanView">
               <%--<div class="form-horizontal">
              <br/>
                <div class="row">
                    <div class="col-md-12">
                        <p class="form-control alert alert-info" style="font-weight: bold;"> Workplan And Budget Requested for</p> 
                    </div>
                </div>
                <div class="row">
                <div class="col-md-6">
                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Objective:</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtObj" CssClass="form-control" Enabled="True"/>               
                </div>
                </div>
                 
                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Activity:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtActivity" CssClass="form-control" Enabled="True"/>               
                    </div>
                </div>
                   
                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Activity Targets:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtTargets" CssClass="form-control" Enabled="True"/>               
                    </div>
                </div>
                   
                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Means of Verification:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtMeansofVer" CssClass="form-control" Enabled="True"/>               
                    </div>
                </div>
                   
                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Time Frame (Days):</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtTimeFrame" CssClass="form-control" Enabled="True" TextMode="Number"/>               
                    </div>
                </div>
                   
                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Amount:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtAmount" CssClass="form-control" TextMode="Number" Enabled="True"/>               
                    </div>
                </div>
                <div class="row">
                  <div class="col-md-3"></div>
                    <div class="col-md-6">
                      <div class="form-group">
                        <asp:Button ID="btnAddWorkplan" runat="server" Text="Add Workplan Item" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnAddWorkplan_OnClick" CausesValidation="False" />          
                      </div>
                   </div> 
               <div class="col-md-3"></div>
                </div>
               <br/>
                </div>
                 <div class="col-md-6">
                    <asp:GridView ID="tblWorkplan" runat="server" CssClass="table table-condensed footable" Width="100%" AutoGenerateSelectButton="false" 
                     EmptyDataText="No Workplan data Found!" DataKeyNames="No" OnRowDeleting="tblWorkplan_OnRowDeleting">
                        <Columns>
                            <asp:BoundField DataField="No" HeaderText="S/No:"/>
                            <asp:BoundField DataField="Objective" HeaderText="Objective" />
                            <asp:BoundField DataField="Activity" HeaderText="Activity" />
                            <asp:BoundField DataField="Activity_Targets" HeaderText="Activity Targets:" />
                            <asp:BoundField DataField="Time_Frame_Days" HeaderText="Time Frame" />
                            <asp:BoundField DataField="Amount_KES" HeaderText="Amount (KES):" DataFormatString="{0:N2}" />
                            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                        </Columns>
                        <SelectedRowStyle BackColor="#259EFF" BorderColor="#FF9966" /> 
                     </asp:GridView>
                     </div>
                 </div>
              <br/>
                 <div class="col-md-4"></div> 
                 <div class="col-md-4 form-group">
                      <asp:Button ID="btnGoNext4" runat="server" Text="Next Tab" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnGoNext4_OnClick" CausesValidation="False" UseSubmitBehavior="False"  />          
                 </div>
                 <div class="col-md-4"></div>
              </div>--%>
          </asp:View>
          <%--<asp:View runat="server" ID="bankDetails">
           <div class="form-horizontal">
            <br/>
               <div class="row">
                    <div class="col-md-12">
                        <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">4</span> Please provide your bank details here.</label> 
                    </div>
                </div>
            <br/>
               <div class="row">
                    <div class="col-md-12">
                        <p class="form-control alert alert-info" style="font-weight: bold;"> Your University bank details </p> 
                    </div>
                </div>
            <br/>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
                       <div class="form-group">
                            <asp:Label runat="server" CssClass="col-md-3 control-label">Univerity/College:</asp:Label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlUniversity" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                            data-live-search="true" AppendDataBoundItems="true" AutoPostBack="False">
                                        <asp:ListItem>--Select Univeristy--</asp:ListItem>
                                    </asp:DropDownList>         
                                </div> 
                            <div class="col-md-2">
                                <asp:TextBox runat="server" ID="txtMyColleg" CssClass="form-control" Enabled="False" placeholder="My College?" />              
                             </div>
                        </div>
               
                       <div class="form-group">
                            <asp:Label runat="server" CssClass="col-md-3 control-label">Bank:</asp:Label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlBankUni" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                            data-live-search="true" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlBankUni_OnSelectedIndexChanged">
                                        <asp:ListItem>--Select Bank--</asp:ListItem>
                                    </asp:DropDownList>         
                                </div> 
                           <div class="col-md-2">
                                <asp:TextBox runat="server" ID="txtMyBank" CssClass="form-control" Enabled="False" placeholder="My College Bank?" />              
                             </div>
                        </div>
               
                       <div class="form-group">
                            <asp:Label runat="server" CssClass="col-md-3 control-label">Bank Branch:</asp:Label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlbankBranchUni" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                            data-live-search="true" AppendDataBoundItems="true" AutoPostBack="True">
                                        <asp:ListItem>--Select Branch--</asp:ListItem>
                                    </asp:DropDownList>         
                                </div> 
                           <div class="col-md-2">
                                <asp:TextBox runat="server" ID="txtBnkBrnch" CssClass="form-control" Enabled="False" placeholder="College bank branch?" />              
                             </div>
                        </div>
                  </ContentTemplate>
               </asp:UpdatePanel>
             <br/>
               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Account Name:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtUniAccName" CssClass="form-control" Enabled="True"/>   
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Please provide Account Name!/>"
                     ControlToValidate="txtUniAccName" runat="server" ForeColor="Red" Display="Dynamic" />            
                    </div><span class="required">*</span>
                  
                </div>
               
               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Account Number:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtUniAccNumber" CssClass="form-control" TextMode="Number" Enabled="True"/>   
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Please provide Account Number!/>"
                     ControlToValidate="txtUniAccNumber" runat="server" ForeColor="Red" Display="Dynamic" />            
                    </div><span class="required">*</span>
                </div>
               
               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">University Admission Number:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtRegNumber" CssClass="form-control" Enabled="True"/>    
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Please provide Admission Number!/>"
                     ControlToValidate="txtRegNumber" runat="server" ForeColor="Red" Display="Dynamic" />           
                    </div><span class="required">*</span>
                </div>
               
               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">ID Number:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtIDNumber" CssClass="form-control" Enabled="True"/> 
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Please provide Admission Number!/>"
                     ControlToValidate="txtIDNumber" runat="server" ForeColor="Red" Display="Dynamic" />               
                    </div><span class="required">*</span>
                   <div class="col-md-3">
                       <asp:Button ID="btnEditUniDetails" runat="server" Text="Save Details" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnEditUniDetails_OnClick" CausesValidation="True" />          
                   </div>

                </div>
             <br/>
               <div class="row">
                    <div class="col-md-12">
                        <p class="form-control alert alert-info" style="font-weight: bold;"> Your Personal bank details </p> 
                    </div>
                </div>
             <br/>
               <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                   <div class="form-group">
                        <asp:Label runat="server" CssClass="col-md-3 control-label">Bank:</asp:Label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlPersonaBank" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                        data-live-search="true" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlPersonaBank_OnSelectedIndexChanged">
                                    <asp:ListItem>--Select Bank--</asp:ListItem>
                                </asp:DropDownList>         
                            </div> 
                       <div class="col-md-2">
                                <asp:TextBox runat="server" ID="txtmyPsBank" CssClass="form-control" Enabled="False" placeholder="My bank?" />              
                             </div>
                    </div>
               
                   <div class="form-group">
                        <asp:Label runat="server" CssClass="col-md-3 control-label">Bank Branch:</asp:Label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlPersonaBranch" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                        data-live-search="true" AppendDataBoundItems="true" AutoPostBack="True">
                                    <asp:ListItem>--Select Branch--</asp:ListItem>
                                </asp:DropDownList>         
                            </div> 
                             <div class="col-md-2">
                                 <asp:TextBox runat="server" ID="txtMyBbranch" CssClass="form-control" Enabled="False" placeholder="My Bank Branch?" />              
                            </div>
                    </div>

                   <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Account Name:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtYourAccNAme" CssClass="form-control" Enabled="True"/>  
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Please provide Account Name!/>"
                         ControlToValidate="txtYourAccNAme" runat="server" ForeColor="Red" Display="Dynamic" />               
                        </div><span class="required">*</span>
                    </div>
               
                   <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Account Number:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtYourAccNumber" CssClass="form-control" TextMode="Number" Enabled="True"/> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="Please provide Account Number!/>"
                     ControlToValidate="txtYourAccNumber" runat="server" ForeColor="Red" Display="Dynamic" />              
                    </div><span class="required">*</span>
                </div>
                      
                    </ContentTemplate>
               </asp:UpdatePanel>
               <br/>
               <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                   <ContentTemplate>
                       <div class="form-group">
                        <asp:Label runat="server" CssClass="col-md-3 control-label">ID Number:</asp:Label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtYourIDNo" CssClass="form-control" TextMode="Number" Enabled="True"/> 
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ErrorMessage="Please provide ID Number!/>"
                             ControlToValidate="txtYourIDNo" runat="server" ForeColor="Red" Display="Dynamic" />                   
                            </div><span class="required">*</span>
                            <div class="col-md-3">
                               <asp:Button ID="btnAddPersonalBankDs" runat="server" Text="Save Details" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnAddPersonalBankDs_OnClick" CausesValidation="True" />          
                           </div>
                        </div>
                    </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID = "btnAddPersonalBankDs" />
                        </Triggers>
             </asp:UpdatePanel>
                  <br/>
               
                 <div class="col-md-4"></div> 
                 <div class="col-md-4 form-group">
                      <asp:Button ID="btnGoNext6" runat="server" Text="Next Tab" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnGoNext6_OnClick" CausesValidation="False" />          
                 </div>
                 <div class="col-md-4"></div>
            </div>
          </asp:View>--%>

         </asp:View>
        </asp:MultiView>
     <br/>
  </div>

</section>

<script type="text/javascript">
    //Function to allow only numbers to textbox
    function validate4N(key)
    {
        //getting key code of pressed key
        var keycode = (key.which) ? key.which : key.keyCode;
        var phn = document.getElementById('txtPhoneNo');
        //comparing pressed keycodes
        if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
            return false;
        }
        else {
            //Condition to check textbox contains ten numbers or not
            if (id.value.length < 15) {
                return true;
            }
            else {
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
        }
        else {
            //Condition to check textbox contains ten numbers or not
            if (id.value.length <15) {
                return true;
            }
            else {
                return false;
            }
        }
    }
    function pageLoad() {
        $('.selectpicker').selectpicker();
    }
    function AlreadyIn() {
        var idIs = document.getElementById("idAlreadyApplied");
        idIs.style.display = "block";
    }

    function ShowSecID() {
        var idSec = document.getElementById("idSecod");
       // var idHigh = document.getElementById("idHighEd");
        idSec.style.display = "block";
        // idHigh.style.display = "none";
        alert("chosen");
    }
    function ShowHighLevelID() {
       // var idSec = document.getElementById("idSecod");
        var idHigh = document.getElementById("idHighEd");
       // idSec.style.display = "none";
        idHigh.style.display = "block";
    }

</script> 
<script type="text/javascript">
$('.input-group.date').datepicker({
    format: "mm/dd/yy",
    maxViewMode: 3,
    todayBtn: true,
    clearBtn: true,
    autoclose: true,
    calendarWeeks: true,
    toggleActive: true
});
</script>

</div>
</asp:Content>
