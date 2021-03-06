﻿<%@ Page Title="Register" Language="C#" MasterPageFile="~/Registration.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="KCDF_P.Account.Register" %>
<%@ Import Namespace="KCDF_P" %>
<%@ OutputCache NoStore="true" Duration="1" VaryByParam="*"   %>
<asp:Content ID="registerMe" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="panel-body" style="font-family:Trebuchet MS">
        <div class="row">
            <div class="col-md-3"></div>
             <div class="col-md-6">
             <p align="center"><h4 style="align-content:center; font-family:Trebuchet MS; color:#0094ff">Please create an account with us</h4></p><hr /></div>
            <div class="col-md-3"></div>
     </div>

            <div class="col-md-3"></div>
            <div class="col-md-6">
                <asp:Label ID="lblError" runat="server" ForeColor="#FF3300" CssClass="text-left hidden"></asp:Label>
                <span class="text-center text-danger"><small><%=lblError.Text %></small></span>
            </div>
            <div class="col-md-3"></div>

      <div class="form-horizontal">
        <div class="form-group">
            <asp:Label runat="server"  CssClass="col-md-3 control-label">Account Type:</asp:Label>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlAccountType" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                            data-live-search="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAccountType_OnSelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Selected="True">--Select Account Type--</asp:ListItem>
                    </asp:DropDownList>
                </div> 
                
        </div>
      </div>
      <br/>
    <asp:MultiView runat="server" ID="accuontTypeViews">
  
        <asp:View runat="server" ID="scholarView">
            <div class="form-horizontal">
                <div class="col-md-2"></div>
                  <div class="form-group col-md-8">
                    <asp:LinkButton ID="lnkAddExistingAcc" runat="server" OnClick="lnkAddExistingAcc_OnClick" CausesValidation="False">Click here if you have ever submitted your data to KCDF before</asp:LinkButton>
                   </div>
                <div class="col-md-2"></div>

               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">First Name:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtFirstname" CssClass="form-control" required="True" placeholder="Your firstname" />              
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="First name is mandatory"
                    ControlToValidate="txtFirstname" runat="server" ForeColor="Red" Display="Dynamic" />
                    </div><span class="required">*</span>
                </div>
               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Middle Name:</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtMiddlename" CssClass="form-control" placeholder="Your middlename" />              
                </div> 
            </div>
         
               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Last Name:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtLastname" CssClass="form-control" required ="true" placeholder="Your lastname" /> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Last name is mandatory"
                    ControlToValidate="txtLastname" runat="server" ForeColor="Red" Display="Dynamic" />             
                    </div><span class="required">*</span>
                </div> 
           
               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Email:</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" required="True" TextMode="Email" placeholder="Your email address" onkeypress="ValidatemyEmail()"/>              
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Email is mandatory"
                    ControlToValidate="txtEmail" runat="server" ForeColor="Red" Display="Dynamic" /> 
                    <span id="validemail"></span>
                </div><span class="required">*</span>
               </div>
                
                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Date of Birth:</asp:Label>
                    <div class="col-md-6">
                       <div class="input-group date" id="dpcker">
                           <input type="text" class="form-control" id="dateofBirth" runat="server"/><span class="input-group-addon"><i class="glyphicon glyphicon-th"></i></span>
                       </div>     
                    </div><span class="required">*</span> 
                </div>
                
                   <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                          <ContentTemplate>
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="col-md-3 control-label">Gender:</asp:Label><span class="required">*</span>
                                <div class="col-md-6">
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Please select your Gender.<br />"
                            ControlToValidate="rdoBtnListGender" runat="server" ForeColor="Red" Display="Dynamic" />
                                     <asp:RadioButtonList ID="rdoBtnListGender" runat="server" OnSelectedIndexChanged="rdoBtnListGender_OnSelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Text="Male" Value="0" Selected="False"></asp:ListItem>
                                        <asp:ListItem Text="Female" Value="1" Selected="False"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div> 
                        </div>
                     </ContentTemplate>
                </asp:UpdatePanel>

                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">ID No/Reg Number:</asp:Label>
                        <div class="col-md-6">
                            <span id="message"></span> &nbsp;<asp:Label runat="server" ID="lblStatus" Visible="False"></asp:Label>
                            <asp:TextBox runat="server" ID="txtIDorRegNo" CssClass="form-control" required="True"  placeholder="Enter your ID or Registration Number"/>              
                                    
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="ID or Reg Number is mandatory"
                                ControlToValidate="txtIDorRegNo" runat="server" ForeColor="Red" Display="Dynamic" /> 
                        </div><span class="required">*</span> 
                </div>

                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Username:</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" required="True" placeholder="Choose username" />              
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="Unique username is mandatory"
                                ControlToValidate="txtUserName" runat="server" ForeColor="Red" Display="Dynamic" /> 
                     </div><span class="required">*</span> 
               </div>
           
                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Password:</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtPassword1" CssClass="form-control" required="True" TextMode="Password" placeholder="Your password"/>              
                             
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ErrorMessage="This field is mandatory"
                                ControlToValidate="txtPassword1" runat="server" ForeColor="Red" Display="Dynamic" />
                </div><span class="required">*</span> 
              </div>
            
                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Confirm Password:</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtPassConfirmed" CssClass="form-control" required="True" TextMode="Password" placeholder="Confirm password"/>              
                           
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ErrorMessage="Password field is mandatory"
                                ControlToValidate="txtPassConfirmed" runat="server" ForeColor="Red" Display="Dynamic" />
                </div><span class="required">*</span>
              </div>
            <div class="col-md-3"></div>
            <div class="col-md-4">
                <div class="form-group">            
                    <asp:Button ID="btnReg" runat="server" Text="Create Account" CssClass="btn btn-primary pull-right btn-sm" OnClick="CreateUser_Click" OnClientClick="Confirm()"/>          
                </div>
            </div>
            <div class="col-md-3"></div>
            </div>
        </asp:View>

        <asp:View runat="server" ID="grantsView">
            <div class="form-horizontal">
                <div class="col-md-2"></div>
                 <div class="form-group col-md-8">
                    <asp:LinkButton ID="lnkbtnEx2" runat="server" OnClick="lnkAddExistingAcc_OnClick">Click here if you have ever submitted your data to KCDF before</asp:LinkButton>
                   </div>
                <div class="col-md-2"></div>

			         <div class="form-group">
                        <asp:Label runat="server" CssClass="col-md-3 control-label">Organization Name:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtOrgName" CssClass="form-control" required="True" placeholder="Organization Name" />              
                        </div><span class="required">*</span> 
                       </div>
               
                    <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Organization Email:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtOrgEmail" CssClass="form-control" placeholder="Organization Email" />              
                        </div><span class="required">*</span> 
                    </div>
               
                    <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Organization Username:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtOrgUsername" CssClass="form-control"  required ="true" placeholder="Choose a unique username" />              
                    </div><span class="required">*</span> 
                    </div>
               
              <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Password:</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtPasswordOne" CssClass="form-control" required="True" TextMode="Password" placeholder="Your password"/>              
                </div><span class="required">*</span> 
               </div>
          
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Confirm Password:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtPassConfir" CssClass="form-control" required="True" TextMode="Password" placeholder="Confirm password"/>              
                    </div><span class="required">*</span> 
                    </div> 
                 
                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-4">
                        <div class="form-group">            
                    <asp:Button ID="btnCreateAcc" runat="server" Text="Create Account" CssClass="btn btn-primary pull-right btn-sm" OnClick="btnCreateAcc_OnClick" />          
                </div> 
                    </div>
                    <div class="col-md-3"></div>
                </div> 	
			    
               </div>
        </asp:View>

        <asp:View runat="server" ID="regConsults">
             <div class="form-horizontal">
                 <div class="col-md-2"></div>
                  <div class="form-group col-md-8">
                    <asp:LinkButton ID="lnkBtnEx3" runat="server" OnClick="lnkAddExistingAcc_OnClick" CausesValidation="False" UseSubmitBehaviour="False">Click here if you have ever submitted your data to KCDF before</asp:LinkButton>
                   </div>
                 <div class="col-md-2"></div>

                  <div class="form-group">
                        <div class="col-md-3">
                        </div>
                        &nbsp;
                        <div class="col-md-6">
                            <div class="input-group">
                               <input type="checkbox" id="readTOR" onclick="ifItsAccepted()"/>
                                <label for="readTOR">Read and Accept our <a href="../ToRDownload.aspx">Terms of Reference</a>&nbsp; first
                               </label>
                            </div>
                           </div>
                       </div>
                 <div id="iAccepted" style="display:none">
			        <div class="form-group">
                        <asp:Label runat="server" CssClass="col-md-3 control-label">Name:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtConsName" CssClass="form-control" required="True" placeholder="Organization Name" MaxLength="250" />              
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ErrorMessage="Fullname field is mandatory"
                    ControlToValidate="txtConsName" runat="server" ForeColor="Red" Display="Dynamic" /> 
                        </div><span class="required">*</span> 
                       </div>
               
                    <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Email:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtConsEmail" CssClass="form-control" placeholder="Organization Email" onkeypress="ValidatemyConsEmail()" />              
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ErrorMessage="Email is mandatory"
                    ControlToValidate="txtConsEmail" runat="server" ForeColor="Red" Display="Dynamic" /> 
                    <span id="validemailCons"></span>
                        </div><span class="required">*</span> 
                    </div>
                 
                    <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Registration Number/ID Number:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtConsIDReg" CssClass="form-control"  required ="true" placeholder="Unique ID" />              
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ErrorMessage="Reg or ID No field is mandatory"
                    ControlToValidate="txtConsIDReg" runat="server" ForeColor="Red" Display="Dynamic" /> 
                    </div><span class="required">*</span> 
                    </div>
               
                    <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label"> Username:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtConsUsername" CssClass="form-control"  required ="true" placeholder="Choose a unique username" />              
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ErrorMessage="Username field is mandatory"
                    ControlToValidate="txtConsUsername" runat="server" ForeColor="Red" Display="Dynamic" /> 
                    </div><span class="required">*</span> 
                    </div>
               
                    <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Password:</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtConsPass1" CssClass="form-control" required="True" TextMode="Password" placeholder="Your password"/>              
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ErrorMessage="Password field is mandatory"
                    ControlToValidate="txtConsPass1" runat="server" ForeColor="Red" Display="Dynamic" /> 
                </div><span class="required">*</span> 
               </div>
          
                    <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Confirm Password:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtConsPass2" CssClass="form-control" required="True" TextMode="Password" placeholder="Confirm password"/>              
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ErrorMessage="Confirm password field is mandatory"
                    ControlToValidate="txtConsPass2" runat="server" ForeColor="Red" Display="Dynamic" /> 
                    </div><span class="required">*</span> 
                    </div> 
                 
                    <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-4">
                        <div class="form-group">            
                    <asp:Button ID="btnSaveConsultant" runat="server" Text="Create Consultants Account" CssClass="btn btn-primary pull-right btn-sm" OnClick="btnSaveConsultant_OnClick" OnClientClick="Confirm()"/>          
                     </div> 
                    </div>
                    <div class="col-md-3"></div>
                </div> 	
                 </div>
			    
               </div>
        </asp:View>
        
        <asp:View runat="server" ID="viewExistingAc">
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-3"></div>
                    <div class="col-md-6">
                        <p style="font-weight: bold">If you had submitted your data to KCDF earlier, activate your account here</p>
                    </div>
                    <div class="col-md-3"></div>
                </div>

               
                    <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Email Address:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtExEmail" CssClass="form-control" placeholder="Email Address you provided" />              
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ErrorMessage="this field is mandatory"
                    ControlToValidate="txtExEmail" runat="server" ForeColor="Red" Display="Dynamic" /> 
                        </div><span class="required">*</span> 
                    </div>
               
                    <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Create Username:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtExUsername" CssClass="form-control"  required ="true" placeholder="Choose a unique username" />              
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ErrorMessage="this field is mandatory"
                    ControlToValidate="txtExUsername" runat="server" ForeColor="Red" Display="Dynamic" /> 
                    </div><span class="required">*</span> 
                    </div>
               
              <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Password:</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtXPass1" CssClass="form-control" required="True" TextMode="Password" placeholder="Your password"/>              
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ErrorMessage="this field is mandatory"
                    ControlToValidate="txtXPass1" runat="server" ForeColor="Red" Display="Dynamic" /> 
                     </div><span class="required">*</span> 
               </div>
          
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Confirm Password:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtXPass2" CssClass="form-control" required="True" TextMode="Password" placeholder="Confirm password"/>              
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ErrorMessage="this field is mandatory"
                    ControlToValidate="txtXPass2" runat="server" ForeColor="Red" Display="Dynamic" /> 
                    </div><span class="required">*</span> 
                    </div> 
                 
                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-4">
                        <div class="form-group">            
                    <asp:Button ID="btnCreateXAccount" runat="server" Text="Get Account" CssClass="btn btn-primary pull-right btn-sm" OnClick="btnCreateXAccount_OnClick" />          
                </div> 
                    </div>
                    <div class="col-md-3"></div>
                </div> 	
			    
               </div>
        </asp:View>
    </asp:MultiView>

   <script type="text/javascript">
    function IsValidEmail(email) {
        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        return expr.test(email);
    };
    function ValidatemyEmail() {
        var isValid = $("#validemail");
        var myEmail = document.getElementById('<%=txtEmail.ClientID%>').value;
            if (!IsValidEmail(myEmail)) {
                isValid.css("color", "red");
                isValid.html("Email is Invalid!");
            }
            else {
                isValid.css("color", "green");
                isValid.html("Email is valid");
            }
        }
    </script>
        
    <script type="text/javascript">
   
    function ValidatemyConsEmail() {
        var isValid = $("#validemailCons");
        var myEmail = document.getElementById('<%=txtConsEmail.ClientID%>').value;
            if (!IsValidEmail(myEmail)) {
                isValid.css("color", "red");
                isValid.html("Email is Invalid!");
            }
            else {
                isValid.css("color", "green");
                isValid.html("Email is valid");
            }
        }
    </script>

    <script type="text/javascript" >
        function ifItsAccepted() {
            var myCheckBoxAss = document.getElementById("readTOR").checked;
            if (myCheckBoxAss == true) {
                alert('I have Read the Terms');
                var ifYsId = document.getElementById("iAccepted");
                ifYsId.style.display = "block";

            } else if (myCheckBoxAss == false) {
                alert("I did'nt read the Terms");
                var ifYsId = document.getElementById("iAccepted");
                ifYsId.style.display = "none";
            }
        }    

    </script>         

  </div>
    
</asp:Content>



 