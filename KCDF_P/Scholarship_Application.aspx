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
    
 <%
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
 <% } %>
 <asp:Label ID="lblError" runat="server" ForeColor="#FF3300" CssClass="text-left hidden"></asp:Label>
 <section class="panel">
 <div class="panel panel-primary">
 <span class="text-center text-danger"><small><%=lblError.Text %></small></span> 

      <asp:MultiView ID="profileMultiview" runat="server" ActiveViewIndex="0">
          <asp:View runat="server" ID="personalDView">
               
         </asp:View>

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

          <asp:View runat="server" ID="scholarSupport">
             
          </asp:View>

          <asp:View runat="server" ID="workplanView">
               <div class="form-horizontal">
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
              </div>
          </asp:View>
          
          <asp:View runat="server" ID="AttachDocs">
             <asp:Label ID="lblErrMsg" runat="server" CssClass="text-left hidden" Visible="false"></asp:Label>
            <span class="text-center text-danger"><small><%=lblErrMsg.Text %></small></span>
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
                                <label class="control-label form-control alert-info" style="font-weight: bold;">College Financials</label>
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
                <div class="col-md-4"></div> 
                 <div class="col-md-4 form-group">
                      <asp:Button ID="btnGoNext5" runat="server" Text="Next Tab" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnGoNext5_OnClick" CausesValidation="False" UseSubmitBehavior="False"  />          
                 </div>
                 <div class="col-md-4"></div>
          </asp:View>

          <asp:View runat="server" ID="bankDetails">
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
          </asp:View>
        
          <asp:View runat="server" ID="finalSubmit">
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
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ErrorMessage="Required field!"
                     ControlToValidate="txtGuardianAddress" runat="server" ForeColor="Red" Display="Dynamic" />  
                        </div><span class="required">*</span>
                        <div class="col-md-2">
                             <asp:Button ID="btnSubmitApplication" runat="server" Text="Submit Application" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnSubmitApplication_OnClick" CausesValidation="False" />          
                         </div>
                </div>
               
              </div>

                <br />
          </asp:View>
        </asp:MultiView>
     <br/>
  </div>

 </section>
<section class="panel">
    <div class="panel panel-primary">
        <div class="container">
           <asp:Wizard ID="Wizard1" runat="server" BackColor="#EFF3FB" BorderColor="#B5C7DE"
                BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" Width="75%"  ActiveStepIndex="0" OnFinishButtonClick="OnFinish">
                <StepStyle Font-Size="0.8em" ForeColor="#333333" />
                    <WizardSteps>
                    <asp:WizardStep runat="server" Title="Personal Information" StepType="Auto">
                        <br/>
                         <fieldset>
                            <legend>Personal Information</legend>
                        <br/>
                        
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
                            <%-- <div class="col-md-4"></div> 
                             <div class="col-md-4 form-group">
                                  <asp:Button ID="btnGoNext1" runat="server" Text="Next Tab" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnGoNext1_OnClick" CausesValidation="False" UseSubmitBehavior="False" />          
                             </div>
                             <div class="col-md-4"></div>  --%>         
                        </div>
                      
                          </fieldset>
                          </asp:WizardStep>
                        
                            <asp:WizardStep runat="server" Title="Scholarstic Support" StepType="Auto">
                             <fieldset>
                                <legend>Scholarstic Support</legend>

                            <div class="form-horizontal">
                            <br/>
                            <div class="row">
                                <div class="col-md-12">
                                    <p class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">4</span> Please give a breakdown of the total cost of your course. Please rank the item(s) in priority you would like this application to support. Remember that the priority for this fund is study tuition </p> 
                                </div>
                            </div>
                            <br/>
                            <div class="row">
                                <div class="col-md-6">
                            <div class="form-group">
                            <asp:Label runat="server" CssClass="col-md-3 control-label">Description of items:</asp:Label>
                                <div class="col-md-6">
                                    <asp:TextBox id="txtAreaItemDecription" TextMode="multiline" Columns="70" Width="100%" Rows="5" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ErrorMessage="Required field!"
                                    ControlToValidate="txtAreaItemDecription" runat="server" ForeColor="Red" Display="Dynamic" />  
                                </div>
                            </div>
                  
                            <div class="form-group">
                            <asp:Label runat="server" CssClass="col-md-3 control-label">Year of study:</asp:Label>
                                <div class="col-md-6">
                                        <asp:DropDownList ID="txtYearofStudie" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                                data-live-search="true" AppendDataBoundItems="False">
                                            <asp:ListItem Selected="True">..Select Year of Study..</asp:ListItem>
                                            <asp:ListItem>First Year</asp:ListItem>
                                            <asp:ListItem>Second Year</asp:ListItem>
                                            <asp:ListItem>Third Year</asp:ListItem>
                                            <asp:ListItem>Fourth Year</asp:ListItem>
                                        </asp:DropDownList>
                                    </div> 
                            </div>
                  
                            <div class="form-group">
                            <asp:Label runat="server" CssClass="col-md-3 control-label">Cost (KES):</asp:Label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server"  TextMode="Number" ID="txtCost" CssClass="form-control" Enabled="True" required="True"/> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ErrorMessage="Please provide Cost!/>"
                                    ControlToValidate="txtCost" runat="server" ForeColor="Red" Display="Dynamic" />               
                                </div><span class="required">*</span>
                            </div>
                  
                            <div class="form-group">
                            <asp:Label runat="server" CssClass="col-md-3 control-label">Rank:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlRank" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                                data-live-search="true" AppendDataBoundItems="False">
                                        <asp:ListItem>..Select Rank..</asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        </asp:DropDownList>
                                </div> 
                            </div>
                                <div class="row">
                                <div class="col-md-3"></div>
                                <div class="col-md-6">
                                <div class="form-group">
                                <asp:Button ID="btnAddSupport" runat="server" Text="Add Scholastic Support" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnAddSupport_OnClick" CausesValidation="False" />          
                                </div>
                            </div> 
                            <div class="col-md-3 form-group">
                                <asp:Button ID="btnGoNext3" runat="server" Text="Next Tab" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnGoNext3_OnClick" CausesValidation="False" UseSubmitBehavior="False" />          
                                </div>
                            </div>
                            <br/>
                            </div>
                                <div class="col-md-6">
                                <asp:GridView ID="grdViewScholarS" runat="server" CssClass="table table-condensed footable" Width="100%" AutoGenerateSelectButton="false" 
                                    EmptyDataText="No Scholarstic Support data Found!" DataKeyNames="No" OnRowDeleting="grdViewScholarS_OnRowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="No" HeaderText="S/No:"/>
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                    <asp:BoundField DataField="Cost_KES" HeaderText="Cost (KES):" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="Rank_Priority" HeaderText="Priority (Rank):" />
                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                </Columns>
                                <SelectedRowStyle BackColor="#259EFF" BorderColor="#FF9966" /> 
                                    </asp:GridView>
                                    </div>
                                </div>
                          </div>
                    </fieldset>
                    </asp:WizardStep>

                    </WizardSteps>
         </asp:Wizard>

           
            
            
            
              <div>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
            <td><asp:Button ID="btnstart" runat="server" Text="Launch Wizard..." 
                    onclick="btnstart_Click" /></td>
            </tr>
            <tr>
                <td align="center" style="padding-top:50px;">
                    <table cellpadding="0" cellspacing="0" border="0" width="90%">
                        <tr>
                            <td align="center">
                                <asp:Wizard ID="Wizard2" runat="server" BackColor="#EFF3FB" BorderColor="#B5C7DE"
                                    BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" Width="75%"  ActiveStepIndex="0" OnFinishButtonClick="OnFinish">
                                    <StepStyle Font-Size="0.8em" ForeColor="#333333" />
                                    <WizardSteps>
                                        <asp:WizardStep runat="server" Title="Personal Information" StepType="Auto">
                                            <fieldset>
                                                <legend>Personal Information</legend>
                                                <div style="height: 350px; text-align: center;">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="50%">
                                                        <tr>
                                                            <td style="padding-top: 10px;">
                                                                First Name:
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtFirstName" runat="server" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-top: 10px;">
                                                                Last Name:
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtLastName" runat="server" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-top: 10px;">
                                                                Address1:
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtAddress1" runat="server" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-top: 10px;">
                                                                Addres2:
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtAddress2" runat="server" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="padding-top: 10px;">
                                                            <td style="padding-top: 10px;">
                                                                City:
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtCity" runat="server" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="padding-top: 10px;">
                                                            <td style="padding-top: 10px;">
                                                                State:
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtState" runat="server" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="padding-top: 10px;">
                                                            <td style="padding-top: 10px;">
                                                                Country:
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtCountry" runat="server" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="padding-top: 10px;">
                                                            <td style="padding-top: 10px;">
                                                                Mobile:
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtMobile" runat="server" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="padding-top: 10px;">
                                                            <td style="padding-top: 10px;">
                                                                Email Id:
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtEmailId" runat="server" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </fieldset></asp:WizardStep>
                                        <asp:WizardStep runat="server" Title="Career Information" StepType="Auto">
                                            <fieldset>
                                                <legend>Career Infomation</legend>
                                                <div style="height: 350px; text-align: center;">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="50%">
                                                        <tr>
                                                            <td style="padding-top: 10px;">
                                                                Qualification:
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtQualification" runat="server" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-top: 10px;">
                                                                Year of Passing:
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtYearofPass" runat="server" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-top: 10px;">
                                                                Number of years Experience:
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtexperience" runat="server" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-top: 10px;">
                                                                Expertise:
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtExpertise" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </fieldset></asp:WizardStep>
                                        <asp:WizardStep runat="server" ID="Projects" StepType="Auto">
                                            <fieldset>
                                                <legend>Projects</legend>
                                                <div style="height: 350px;">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="90%">
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="gvProjects" runat="server" CellPadding="4" ForeColor="#333333"
                                                                    GridLines="None" Width="90%">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <EditRowStyle BackColor="#2461BF" />
                                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </fieldset></asp:WizardStep>
                                        <asp:WizardStep runat="server" Title="Optional" StepType="Finish">
                                            <fieldset>
                                                <legend>Optional</legend>
                                                <div style="height: 350px;">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="50%">
                                                        <tr>
                                                            <td style="padding-top: 10px;">
                                                                <asp:CheckBox ID="chkNewsLetters" runat="server" />
                                                            </td>
                                                            <td style="padding-top: 10px; text-align: left;">
                                                                Would like to recieve the news letters
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </fieldset></asp:WizardStep>
                                    </WizardSteps>
                                    <SideBarButtonStyle BackColor="#507CD1" Font-Names="Verdana" ForeColor="White" />
                                    <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
                                        BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
                                    <SideBarStyle BackColor="#507CD1" Font-Size="0.9em" Width="20%" VerticalAlign="Top" />
                                    <HeaderStyle BackColor="#284E98" BorderColor="#EFF3FB" BorderStyle="Solid" BorderWidth="2px"
                                        Font-Bold="True" Font-Size="0.9em" ForeColor="White" HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        Employee Profle
                                    </HeaderTemplate>
                                </asp:Wizard>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
          
        </div>

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
        var applyYeah = document.getElementById("idApplyforConsulting");
        idIs.style.display = "block";
        applyYeah.style.display = "none";
    }
</script> 

</div>
    <script src="https://code.jquery.com/jquery-compat-3.0.0-alpha1.js"></script>
    <script>       
        $(document).ready(function () {

            var navListItems = $('div.setup-panel div a'),
                    allWells = $('.setup-content'),
                    allNextBtn = $('.nextBtn');

            allWells.hide();

            navListItems.click(function (e) {
                e.preventDefault();
                var $target = $($(this).attr('href')),
                        $item = $(this);

                if (!$item.hasClass('disabled')) {
                    navListItems.removeClass('btn-primary').addClass('btn-default');
                    $item.addClass('btn-primary');
                    allWells.hide();
                    $target.show();
                    $target.find('input:eq(0)').focus();
                }
            });

            allNextBtn.click(function () {
                var curStep = $(this).closest(".setup-content"),
                    curStepBtn = curStep.attr("id"),
                    nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().next().children("a"),
                    curInputs = curStep.find("input[type='text'],input[type='url']"),
                    isValid = true;

                $(".form-group").removeClass("has-error");
                for (var i = 0; i < curInputs.length; i++) {
                    if (!curInputs[i].validity.valid) {
                        isValid = false;
                        $(curInputs[i]).closest(".form-group").addClass("has-error");
                    }
                }

                if (isValid)
                    nextStepWizard.removeAttr('disabled').trigger('click');
            });

            $('div.setup-panel div a.btn-primary').trigger('click');
        });
     </script>
</asp:Content>
