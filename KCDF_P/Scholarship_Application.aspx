<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Scholarship_Application.aspx.cs" MasterPageFile="Applications.Master" Inherits="KCDF_P.Grant_Application" %>

<asp:Content ID="studentRegistrationForm" ContentPlaceHolderID="MainContent" runat="server">
<div class="panel-body" style="font-family:Trebuchet MS">
 <div class="row">
            <div class="col-md-12">
            <h4 style="align-content:center; font-family:Trebuchet MS; color:#0094ff">Scholarship Application Form</h4><br /></div>
    </div>
    
<div class="form-horizontal">
<div class="form-group">
    <asp:Label runat="server"  CssClass="col-md-3 control-label">Select Scholarship:</asp:Label>
        <div class="col-md-6">
            <asp:DropDownList ID="ddlScolarshipType" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                    data-live-search="true" AppendDataBoundItems="true" AutoPostBack="True">
            </asp:DropDownList>
        </div> 
</div>
</div>
<br/>
    
 <%
        var selProj = ddlScolarshipType.SelectedItem.Text;
        var proFtures = nav.call_for_Proposal.ToList().Where(bf => bf.Project == selProj);
        var itswhats = proFtures.Select(f => f.Basic_Features).SingleOrDefault();

        if (itswhats == false)
        {
    %>
 <header class="panel-heading tab-bg-info">
         <asp:Menu ID="scholarshipDataCollection" Orientation="Horizontal"  StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server" OnMenuItemClick="scholarshipDataCollection_OnMenuItemClick">
            <Items>
                <asp:MenuItem Text="Applicant's Profile |" Value="0" Selected="true" runat="server"/>
                <asp:MenuItem Text="My Referees |" Value="1" runat="server"/>
                <%-- <asp:MenuItem Text="Public Participation & Advocacy |" Value="2" runat="server"/> --%>
                <%-- <asp:MenuItem Text="Community Environmental Project |" Value="4" runat="server"/> --%>
                <asp:MenuItem Text="Scholarstic Support |" Value="2" runat="server"/> 
                <asp:MenuItem Text="Work Plan And Budget |" Value="3" runat="server"/>
                <asp:MenuItem Text="Attach Documents |" Value="4" runat="server"/> 
                <asp:MenuItem Text="Bank Details |" Value="5" runat="server"/>
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
                <asp:MenuItem Text="My Referees |" Value="1" runat="server"/>
                <%--<asp:MenuItem Text="Scholarstic Support |" Value="3" runat="server"/>--%>
                <asp:MenuItem Text="Work Plan And Budget |" Value="2" runat="server"/>
                <asp:MenuItem Text="Attachments |" Value="3" runat="server"/>
                <asp:MenuItem Text="Bank Details |" Value="4" runat="server"/>
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
            <div class="form-horizontal">
            <br/>
            <div class="row">
                <div class="col-md-12">
                    <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">1</span> Your Personal Basic Information:</label> 
                </div>
            </div>
            <br/>
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
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLname"
                                CssClass="text-danger" ErrorMessage="Lastname field is required!"></asp:RequiredFieldValidator>           
                        </div> 
                </div> 
        
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
                        <asp:TextBox runat="server" ID="txtPhoneNo" CssClass="form-control" required="True" onkeypress="return validate4N(event)" ondrop="return false;" onpaste="return false;" Enabled="False"/>              
                               
                         </div> 
                    </div> 
          
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">ID/Passport Number:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtIDNo" CssClass="form-control" required="True" TextMode="Number" onkeypress="return validateID(event)" Enabled="False"/>              
                
                        </div> 
                </div>

                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Gender:</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="lstGender" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="False">
                           <asp:ListItem>..Select Gender..</asp:ListItem>
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                           </asp:DropDownList>              
                        </div> 
                </div>

                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Date of Birth:</asp:Label>
                    <div class="col-md-6">
                       <div class="input-group date">
                           <input type="text" class="form-control" id="dateofBirth" runat="server"/><span class="input-group-addon"><i class="glyphicon glyphicon-th"></i></span>
                       </div>      
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
                        </div> 
                </div>
              
               <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Parent/Guardian Email:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtGuardianEmail" CssClass="form-control" Enabled="False" />              
                        </div> 
                </div>
              
               <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Parent/Guardian Address:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtGuardianAddress" CssClass="form-control" Enabled="False" style="text-transform:uppercase"/>              
                        </div> 
                </div>
                
            </div>
              
         </asp:View>

          <asp:View runat="server" ID=MyrefsView>
              <div class="form-horizontal">
                <div class="row">
                    <div class="col-md-12">
                        <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">3</span> My Referees</label> 
                    </div>
                </div>
              <br/>
                 <asp:GridView ID="tblRefs" runat="server" CssClass="table table-condensed" Width="100%" AutoGenerateSelectButton="false" 
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
               
              </div>
          </asp:View>

          

          <asp:View runat="server" ID="scholarSupport">
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
                <asp:Label runat="server" CssClass="col-md-3 control-label">Detailed description of items:</asp:Label>
                    <div class="col-md-6">
                       <asp:TextBox id="txtAreaItemDecription" TextMode="multiline" Columns="70" Width="100%" Rows="5" runat="server" />
                    </div>
                </div>
                  
                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Year of study:</asp:Label>
                    <div class="col-md-6">
                            <asp:DropDownList ID="txtYearofStudie" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="True">
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
                    </div>
                </div>
                  
                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Rank:</asp:Label>
                     <div class="col-md-6">
                            <asp:DropDownList ID="ddlRank" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="True">
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
               <div class="col-md-3"></div>
                </div>
                <br/>
                </div>
                  <div class="col-md-6">
                    <asp:GridView ID="grdViewScholarS" runat="server" CssClass="table table-condensed" Width="100%" AutoGenerateSelectButton="false" 
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
                    <asp:GridView ID="tblWorkplan" runat="server" CssClass="table table-condensed" Width="100%" AutoGenerateSelectButton="false" 
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
                <div class="row">
                    <div class="col-md-12">
                        <p class="form-control alert alert-info" style="font-weight: bold;"> Scholarship Budget.</p> 
                    </div>
                </div>
                 <br/>
                
                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Total Budget:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtBudgetTotal" CssClass="form-control" TextMode="Number" Enabled="True"/>               
                    </div>
                </div>
                   
                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Date Required:</asp:Label>
                   <div class="col-md-6">
                       <div class="input-group date">
                           <input type="text" class="form-control" id="txtWorkplanDate" runat="server"/><span class="input-group-addon"><i class="glyphicon glyphicon-th"></i></span>
                       </div>      
                    </div> 
                </div>
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
                                <label class="control-label form-control alert-danger" style="font-weight: bold;">College Financials</label>
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
                    <asp:GridView ID="gridViewUploads" runat="server" CssClass="table table-striped table-advance table-hover" GridLines="None" EmptyDataText="No files uploaded" OnRowDeleting="gridViewUploads_OnRowDeleting" AutoGenerateColumns="False" DataKeyNames="Id" AlternatingRowStyle-BackColor="#C2D69B"
                        OnRowDataBound="gridViewUploads_OnRowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="S/No:" />
                            <asp:BoundField DataField="Document_Kind" HeaderText="Document" />
                            <asp:BoundField DataField="Document_Name" HeaderText="File Name" />
                            <asp:BoundField DataField="Document_type" HeaderText="File Type" />
                            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                        </Columns>
                    </asp:GridView>
                    </div>
                </div>
          </asp:View>
          <asp:View runat="server" ID="bankDetails">
           <div class="form-horizontal">
            <br/>
               <div class="row">
                    <div class="col-md-12">
                        <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">4</span> Please provide your bank details here.  </label> 
                    </div>
                </div>
            <br/>
               <div class="row">
                    <div class="col-md-12">
                        <p class="form-control alert alert-info" style="font-weight: bold;"> Your University bank details </p> 
                    </div>
                </div>
            <br/>
               <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Univerity/College:</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlUniversity" runat="server" CssClass="form-control" AutoPostBack="true">
                                <asp:ListItem>..Select Univeristy..</asp:ListItem>
                                <asp:ListItem>KU</asp:ListItem>
                                <asp:ListItem>UoN</asp:ListItem>
                            </asp:DropDownList>         
                        </div> 
                </div>
               
               <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Bank:</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlBankUni" runat="server" CssClass="form-control" AutoPostBack="true">
                                <asp:ListItem>..Select Bank..</asp:ListItem>
                                <asp:ListItem>Equity</asp:ListItem>
                                <asp:ListItem>BoA</asp:ListItem>
                            </asp:DropDownList>         
                        </div> 
                </div>
               
               <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Bank Branch:</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlbankBranchUni" runat="server" CssClass="form-control" AutoPostBack="true">
                                <asp:ListItem>..Select Branch..</asp:ListItem>
                                <asp:ListItem>KU</asp:ListItem>
                                <asp:ListItem>UoN</asp:ListItem>
                            </asp:DropDownList>         
                        </div> 
                </div>

               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Account Name:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtUniAccName" CssClass="form-control" Enabled="True"/>               
                    </div>
                </div>
               
               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Account Number:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtUniAccNumber" CssClass="form-control" TextMode="Number" Enabled="True"/>               
                    </div>
                </div>
               
               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">University Admission Number:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtRegNumber" CssClass="form-control" Enabled="True"/>               
                    </div>
                </div>
               
               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">ID Number:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtIDNumber" CssClass="form-control" Enabled="True"/>               
                    </div>
                </div>

             <br/>
               <div class="row">
                    <div class="col-md-12">
                        <p class="form-control alert alert-info" style="font-weight: bold;"> Your Personal bank details </p> 
                    </div>
                </div>
             <br/>
               <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Bank:</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlPersonaBank" runat="server" CssClass="form-control" AutoPostBack="true">
                                <asp:ListItem>..Select Bank..</asp:ListItem>
                                <asp:ListItem>Equity</asp:ListItem>
                                <asp:ListItem>BoA</asp:ListItem>
                            </asp:DropDownList>         
                        </div> 
                </div>
               
               <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Bank Branch:</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlPersonaBranch" runat="server" CssClass="form-control" AutoPostBack="true">
                                <asp:ListItem>..Select Branch..</asp:ListItem>
                                <asp:ListItem>KU</asp:ListItem>
                                <asp:ListItem>UoN</asp:ListItem>
                            </asp:DropDownList>         
                        </div> 
                </div>
               
               <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Bank Branch:</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="true">
                                <asp:ListItem>..Select Branch..</asp:ListItem>
                                <asp:ListItem>KU</asp:ListItem>
                                <asp:ListItem>UoN</asp:ListItem>
                            </asp:DropDownList>         
                        </div> 
                </div>

               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Account Name:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtYourAccNAme" CssClass="form-control" Enabled="True"/>               
                    </div>
                </div>
               
               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Account Number:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtYourAccNumber" CssClass="form-control" TextMode="Number" Enabled="True"/>               
                    </div>
                </div>
               
               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">ID Number:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtYourIDNo" CssClass="form-control" TextMode="Number" Enabled="True"/>               
                    </div>
                </div>
               
            </div>

          </asp:View>
        
        </asp:MultiView>
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

</script>
</div>
</asp:Content>
