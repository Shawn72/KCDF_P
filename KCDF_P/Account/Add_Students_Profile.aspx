<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Students_Profile.aspx.cs" MasterPageFile="../Site.Master" Inherits="KCDF_P.Account.Add_Students_Profile" %>
<%@ Register Assembly="EditableDropDownList" Namespace="EditableControls" TagPrefix="editable" %>
<asp:Content ID="studentRegistrationForm" ContentPlaceHolderID="MainContent" runat="server">
<div class="panel-body" style="font-family:Trebuchet MS">
    <div class="row">
            <div class="col-md-12">
            <h4 style="align-content:center; font-family:Trebuchet MS; color:#0094ff">Manage Profile</h4><br /></div>
    </div>
 <header class="panel-heading tab-bg-info">
         <asp:Menu ID="studentInfoMenu" Orientation="Horizontal"  StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server" OnMenuItemClick="studentInfoMenu_OnMenuItemClick">
            <Items>
                <asp:MenuItem Text="Personal Information |" Value="0" Selected="true" runat="server"/>
                <asp:MenuItem Text="Education Background |" Value="1" runat="server"/> 
                <asp:MenuItem Text="Add Referees |" Value="2" runat="server"/>
            </Items>
        </asp:Menu>

</header>
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
                    <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">1</span> Your Personal Information:</label> 
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
                            <asp:TextBox runat="server" ID="txtResidence" CssClass="form-control" style="text-transform:uppercase" />              
                        </div> 
                </div>
      
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Mobile Number:</asp:Label>
                       <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtPhoneNo" CssClass="form-control" required="True" onkeypress="return validate4N(event)" ondrop="return false;" onpaste="return false;"/>            
                       </div> 
                    </div> 
          
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">ID/Passport Number:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtIDNo" CssClass="form-control" required="True" TextMode="Number" onkeypress="return validateID(event)"/>              
                
                        </div> 
                </div>

                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Gender:</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="lstGender" runat="server" CssClass="form-control">
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
                           <input type="text" id="dateOFBirth" runat="server" class="form-control"/><span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                       </div>      
                    </div> 
                </div>
             
                <div class="row">
                  <div class="col-md-3"></div>
                    <div class="col-md-6">
                  <div class="form-group">
                    <asp:Button ID="btnEditProf" runat="server" Text="Update Data" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnEditProf_Click" CausesValidation="False" />          
                 </div>
              </div> 
               <div class="col-md-3"></div>
                </div>
                <br/>
                   
              </div>
              
         </asp:View>
          
          <asp:View runat="server" ID="edubackGrd">
            <div class="form-horizontal">
            <div class="row">
                <div class="col-md-12">
                    <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">2</span> Your Education Background:</label> 
                </div>
             </div>
            <br/>
                 <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Primary School:</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlPrimo" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true">
                                <asp:ListItem Selected="True">..Select Primary School..</asp:ListItem>
                                <asp:ListItem>Nairobi Primary School</asp:ListItem>
                                <asp:ListItem>Kamukunji P. School</asp:ListItem>
                                <asp:ListItem>Queen Elizabeth P. School</asp:ListItem>
                                <asp:ListItem>King James P. School</asp:ListItem>
                            </asp:DropDownList>
                        </div> 
                </div>
                 <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Secondary:</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlSeco" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true">
                                <asp:ListItem Selected="True">..Select Secondary School..</asp:ListItem>
                                <asp:ListItem>St. Charles Lwanga School</asp:ListItem>
                                <asp:ListItem>Alliance School</asp:ListItem>
                                <asp:ListItem>Eastleigh Sec School</asp:ListItem>
                                <asp:ListItem>Kenya High</asp:ListItem>
                            </asp:DropDownList>
                        </div> 
                </div>
               <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Name of University/College:</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlUnivCollg" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true">
                                <asp:ListItem Selected="True">..Select University/College..</asp:ListItem>
                                <asp:ListItem>Kenyatta Uni</asp:ListItem>
                                <asp:ListItem>University of Waterloo</asp:ListItem>
                                <asp:ListItem>JKUAT</asp:ListItem>
                            </asp:DropDownList>
                        </div> 
                </div>
              
               <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Faculty & Degree of Study:</asp:Label>
                       <div class="col-md-6">
                            <asp:DropDownList ID="ddlDegree" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="True">
                                <asp:ListItem Selected="True">..Select Course/Degree Programme..</asp:ListItem>
                                <asp:ListItem>Bsc. Computer Science</asp:ListItem>
                                <asp:ListItem>BSc. Software Engineering</asp:ListItem>
                                <asp:ListItem>BBIT</asp:ListItem>
                                <asp:ListItem>Information Technology</asp:ListItem>
                            </asp:DropDownList>
                        </div> 
                </div>
              
               <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Year of Study:</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlYearofStudy" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
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
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Year of Admission:</asp:Label>
                    <div class="col-md-6">
                       <div class="input-group date">
                           <input type="text" id="txtYrofAdmsn" runat="server" class="form-control"/><span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                       </div>      
                    </div> 
                </div>
              
               <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Year of Completion:</asp:Label>
                        <div class="col-md-6">
                       <div class="input-group date">
                           <input type="text" id="txtYrofCompltn" runat="server" class="form-control"/><span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                       </div>      
                    </div> 
                </div>
              
               <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Parent/Guardian Phone:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtGuardianPhone" CssClass="form-control" onkeypress="return allowOnlyNumber(event);" ondrop="return false;" onpaste="return false;" required="True"/>              
                        </div> 
                </div>
              
               <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Parent/Guardian Email:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtGuardianEmail" CssClass="form-control" TextMode="Email" />              
                        </div> 
                </div>
              
               <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Parent/Guardian Address:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtGuardianAddress" CssClass="form-control" style="text-transform:uppercase" />              
                        </div> 
                </div>
                 <div class="row">
                  <div class="col-md-3"></div>
                    <div class="col-md-6">
                  <div class="form-group">
                    <asp:Button ID="btnEditEductn" runat="server" Text="Update Data" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnEditEductn_OnClick" CausesValidation="False" />          
                 </div>
              </div> 
               <div class="col-md-3"></div>
                </div>
                <br/>
                  
               </div>
                            
          </asp:View>
          
          <asp:View runat="server" ID="addReferees">
              <div class="form-horizontal">
              <div class="row">
                <div class="col-md-12">
                    <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">3</span> Add Referees</label> 
                </div>
                </div>
            <br/>
              <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Firstname:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtrefFname" CssClass="form-control" style="text-transform:uppercase" required="True" />              
                        </div> 
                </div>
              
              <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Middlename:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="refMidName" CssClass="form-control" style="text-transform:uppercase" />              
                        </div> 
                </div>
              
              <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Lastname:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="refLName" CssClass="form-control" style="text-transform:uppercase"  required="True"/>              
                        </div> 
                </div>
              
              <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Email:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="refEmail" CssClass="form-control" required="True" TextMode="Email" />              
                        </div> 
                </div>
              
              <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Phone Number:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="refMobile" CssClass="form-control" style="text-transform:uppercase" required="True"/>              
                        </div> 
                </div>
                <div class="row">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-6">
                        <div class="form-group">
                            <asp:Button ID="btnAddRefer" runat="server" Text="Add Referee" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnAddRefer_OnClick" CausesValidation="False" />          
                        </div>
                     </div>
                    <div class="col-sm-3"></div>
                </div>

            
                <asp:GridView ID="tblRefs" runat="server" CssClass="table table-condensed" Width="100%" AutoGenerateSelectButton="True" 
                 EmptyDataText="No Referees Found!" OnSelectedIndexChanged="tblRefs_OnSelectedIndexChanged" >
                <Columns>
                    <asp:BoundField DataField="No" HeaderText="Ref No." />
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
        </asp:MultiView>
     </div>
       
   </section>
<script type="text/javascript" src="/Scripts/ControlValidater.js"></script>
</div>
</asp:Content>
