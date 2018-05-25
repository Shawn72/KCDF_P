<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Students_Profile.aspx.cs" MasterPageFile="~/Site.Master" Inherits="KCDF_P.Account.Add_Students_Profile" %>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="First name is mandatory"
                    ControlToValidate="txtfNname" runat="server" ForeColor="Red" Display="Dynamic" />
                    </div><span class="required">*</span>
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
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="First name is mandatory"
                    ControlToValidate="txtLname" runat="server" ForeColor="Red" Display="Dynamic" />          
                        </div><span class="required">*</span> 
                </div> 
        
                <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Email Address:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtEmailAdd" CssClass="form-control" Enabled="False" TextMode="Email" onkeypress="ValidatemyEmail()"/> 
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Email is mandatory"
                             ControlToValidate="txtEmailAdd" runat="server" ForeColor="Red" Display="Dynamic" /> 
                    <span id="validemail"></span>             
                        </div> 
                    </div>

                <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Residence:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtResidence" CssClass="form-control" style="text-transform:uppercase" />       
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="First name is mandatory"
                              ControlToValidate="txtResidence" runat="server" ForeColor="Red" Display="Dynamic" />           
                        </div> <span class="required">*</span>
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
                            <asp:TextBox runat="server" ID="txtIDNo" CssClass="form-control" required="True" TextMode="Number" onkeypress="return validateID(event)" placeholder="ID/Passport/Admission Number"/>              
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="First name is mandatory"
                    ControlToValidate="txtIDNo" runat="server" ForeColor="Red" Display="Dynamic" />  
                        </div> <span class="required">*</span>
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
                       <div class="input-group date" id="dpcker">
                           <input type="text" class="form-control" id="dateOFBirth" runat="server"/><span class="input-group-addon"><i class="glyphicon glyphicon-th"></i></span>
                       </div>     
                    </div><span class="required">*</span> 
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
               <%--  <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Primary School:</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlPrimo" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div> 
                </div>--%>
                
                 <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Search your primary School:</asp:Label>
                    <div class="col-md-6">
                         <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" />
                         <asp:HiddenField ID="hfCustomerId" runat="server" />
                    </div>
                      <div class="col-md-3">
                         <asp:Button ID="btnSearchSQL" Text="Search" CssClass="btn btn-primary pull-left btn-sm" runat="server" OnClick="btnSearchSQL_OnClick" CausesValidation="False" UseSubmitBehavior="false" />
                      </div>
                  </div>

                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Select your primary school:</asp:Label>
                    <div class="col-md-6">
                <%--<asp:ListView ID="lvCustomers" runat="server" GroupPlaceholderID="groupPlaceHolder1"
                    ItemPlaceholderID="itemPlaceHolder1" OnPagePropertiesChanging="OnPagePropertiesChanging" OnSelectedIndexChanged="lvCustomers_OnSelectedIndexChanged" OnSelectedIndexChanging="lvCustomers_OnSelectedIndexChanging" DataKeyNames="School Name">
                    <LayoutTemplate>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <th>
                                    School Name
                                </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                            <tr>
                                <td colspan = "3">
                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvCustomers" PageSize="10">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonType="Link" />
                                            <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton = "false" />
                                        </Fields>
                                    </asp:DataPager>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                        </tr>
                    </GroupTemplate>
                    <ItemTemplate>
                        <tr class="item" runat="server">
                          <td>
                              <asp:LinkButton runat="server" 
                                ID="SelectButton" 
                                Text="Select"
                                CommandName="Select" />
                            </td>
                            <td>
                              <asp:Label runat="server" ID="NameLabel" Text='<%#Eval("School Name") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                     <SelectedItemTemplate>
                          <tr class="selection" runat="server">
                            <td>&nbsp;</td>
                            <td>
                              <asp:Label runat="server" ID="NameLabel" Text='<%#Eval("School Name") %>' />
                            </td>
                          </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>--%>
                        
                        <asp:GridView ID="grdViewMySchoolIs" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                                AutoGenerateColumns="false" CssClass="table table-condensed table-responsive table-bordered footable" Width="100%" AutoGenerateSelectButton="false" 
                             EmptyDataText="No projects Found!" DataKeyNames="School Name"  AlternatingRowStyle-BackColor = "#C2D69B" AllowSorting="True">
                                <Columns>
                                    <asp:BoundField DataField="School Name" HeaderText="Primary School Name"/>
                                       <asp:TemplateField HeaderText="Select your school">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" Text="Select" CommandArgument='<%# Eval("School Name") %>' CommandName="lnkSelect" runat="server" OnClick="lnkSelect_OnClick" CausesValidation="False" UseSubmitBehavior="false"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                </Columns>
                              <SelectedRowStyle BackColor="#259EFF" BorderColor="#FF9966" /> 
                            </asp:GridView>
                            <br />
                           <div id="myPrimaryIs" style="display: none;">
                               <label>My Primary School is:&nbsp;</label><asp:Label ID="lblValues" runat="server" Text=""></asp:Label>
                           </div>
                            
                        </div>
                    </div>
                
                 <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Marks:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtMarks" CssClass="form-control" required="True" TextMode="Number" onkeypress="return validateID(event)" placeholder="My Marks"/>
                    </div> <span class="required">*</span> 
                 </div>
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Out of:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtTotalMarks" CssClass="form-control" required="True" TextMode="Number" onkeypress="return validateID(event)" placeholder="Total Marks"/>
                    </div> <span class="required">*</span>
                </div>
                 <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Secondary:</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlSeco" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div> 
                </div>
                
                   <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Grade:</asp:Label><span class="required">*</span>
                        <div class="col-md-6">
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="Please select your Grade.<br />"
                    ControlToValidate="rdoBtnListGrade" runat="server" ForeColor="Red" Display="Dynamic" />
                             <asp:RadioButtonList ID="rdoBtnListGrade" runat="server" OnSelectedIndexChanged="rdoBtnListGrade_OnSelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Text="A." Value="A." Selected="False"></asp:ListItem>
                                <asp:ListItem Text="A-" Value="A-" Selected="False"></asp:ListItem>
                                <asp:ListItem Text="B+" Value="B+" Selected="False"></asp:ListItem>
                                <asp:ListItem Text="B." Value="B." Selected="False"></asp:ListItem>
                                <asp:ListItem Text="B-" Value="B-" Selected="False"></asp:ListItem>
                                <asp:ListItem Text="C+" Value="C+" Selected="False"></asp:ListItem>
                                <asp:ListItem Text="C." Value="C." Selected="False"></asp:ListItem>
                                <asp:ListItem Text="D+" Value="D+" Selected="False"></asp:ListItem>
                                <asp:ListItem Text="D." Value="D." Selected="False"></asp:ListItem>
                                <asp:ListItem Text="D-" Value="D-" Selected="False"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div> 
                </div>

               <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Name of University/College:</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlUnivCollg" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div> 
                </div>
              
               <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-3 control-label">Faculty & Degree of Study:</asp:Label>
                       <div class="col-md-6">
                           <asp:TextBox runat="server" ID="txtDegree" placeholder="Enter your Degree/Course Name" CssClass="form-control"></asp:TextBox>
                        </div><span class="required">*</span> 
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
                    </div></div><span class="required">*</span> 
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
                        </div><span class="required">*</span>  
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
                        </div><span class="required">*</span>  
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
     <script type="text/javascript">
        function IsValidEmail(email) {
            var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            return expr.test(email);
        };
        function ValidatemyEmail() {
            var isValid = $("#validemail");
           var myEmail = document.getElementById('<%=txtEmailAdd.ClientID%>').value;
            if (!IsValidEmail(myEmail)) {
                isValid.css("color", "red");
                isValid.html("Email is Invalid!");
            }
            else {
                isValid.css("color", "green");
                isValid.html("Email is valid");
            }
        }

         function whatSchool() {
             var myPrimois = document.getElementById("myPrimaryIs");
             myPrimois.style.display = "block";
         }
    </script>
  <%--  <script type="text/javascript">
    $(function () {
        $("[id$=txtSearch]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("Add_Students_Profile.aspx/GetMySchools") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function(item) {
                            return {
                                label: item.split('-')[0],
                                val: item.split('-')[1]
                            }
                        }));
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("[id$=hfCustomerId]").val(i.item.val);
            },
            minLength: 1
        });
    });  
</script>--%>
</div>
</asp:Content>
