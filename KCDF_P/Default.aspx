<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MasterPageFile="~/Defaults.Master" Inherits="KCDF_P.Default" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

 <div class="container-fluid">
 <div class="row row-eq-height">					                    
            
<div class="col-md-3"></div>
<div class="col-md-6">
    <div class="home-form-container">

	<div class="section">
        <div class="row">
            <div class="col-md-4"></div>
                <div class="col-md-4">
                    <img class="img img-circle login-img" style="background-color:#fff;" src="siteimages/kcdflogo.jpg" />
                </div>
            <div class="col-md-4"></div>
        </div>
		<div class="mobile-form-toggle">
		<br>
    	    <div class="field-error">
                <div class="row">
                    <div class="col-md-12"> 
                            <asp:Label ID="lblError" runat="server" ForeColor="#FBF409" CssClass="text-left hidden"></asp:Label>  
                            <span class="text-center text-danger"><small><%=lblError.Text %></small></span>                                                        
                        <h2 class="text-center text-primary" style="font-weight:bold;">User Login </h2>                                
                      </div>
                  </div>
    		 </div>
		</div>
        <div class="row">
            <div class="col-md-6">
              <div class="form-horizontal">
                <div class="form-group">
                    <asp:Label runat="server"  CssClass="col-md-4 control-label">Login As:</asp:Label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlUserType" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                    data-live-search="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlUserType_OnSelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Selected="True">--Select User Type--</asp:ListItem>
                                <asp:ListItem>Student</asp:ListItem>
                                <asp:ListItem>Grantee</asp:ListItem>
                                <asp:ListItem>Consultant</asp:ListItem>
                            </asp:DropDownList>
                    </div> 
                </div>
              </div>
              <br/>
            </div>
             <div class="col-md-6">
                 <span class="pull-right"><asp:LinkButton runat="server" ID="lnkBtnSign" OnClick="btnSignup_Click" CausesValidation="False">Create an Account</asp:LinkButton></span>
             </div>
          </div>
            
        <div class="row">
              <asp:MultiView runat="server" ID="LoginViews">
                <asp:View runat="server" ID="studentView">
                    <div class="input-group">
                    <span class="input-group-addon"><i class="fa fa-user"></i> Username:</span>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                    </div>
                    <br />
                    <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-key"></i> Password:</span>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                    </div>
                    <br/>

                    <div class="input-group">
                        <asp:CheckBox ID="chkRememberMe" runat="server" />
                        <label>Remember Me</label>
                     </div>
                    <br/>
                   
                    <div style="transform:scale(1.0); -webkit-transform:scale(1.0);transform-origin:0 0;-webkit-transform-origin:0 0;">
                    <cc1:CaptchaControl ID="cptCaptcha" runat="server" 
                        CaptchaBackgroundNoise="Low" CaptchaLength="5" 
                        CaptchaHeight="60" CaptchaWidth="250" 
                        CaptchaLineNoise="None" CaptchaMinTimeout="10" 
                        CaptchaMaxTimeout="240" FontColor = "#529E00" />
                    </div>
                    <br/>
                    <div class="input-group">
                         <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control" placeholder="Enter the above Text"></asp:TextBox>
                          <br/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Required!" ControlToValidate = "txtCaptcha" ForeColor="red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-lg btn-block" OnClick="btnLogin_Click"/>&nbsp;&nbsp;
                        </div>
                        <div class="col-md-6">
                             <span class="pull-right"><asp:LinkButton runat="server" ID="lnkBtnResetPassword" OnClick="lnkBtnResetPassword_OnClick" CausesValidation="False">Reset Password</asp:LinkButton></span>
                         </div>
                    </div>  
                </asp:View>
                <asp:View runat="server" ID="granteeView">
                    <div class="input-group">
                    <span class="input-group-addon"><i class="fa fa-user"></i> Organization Username:</span>
                    <asp:TextBox ID="txtorgUsn" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                    </div>
                    <br />
                    <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-key"></i> Organization Password:</span>
                        <asp:TextBox ID="txtorgPsw" runat="server" CssClass="form-control" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                    </div>
                    <br/>
                    <div class="input-group">
                        <asp:CheckBox ID="checkRemMegrant" runat="server" />
                        <label>Remember Me</label>
                     </div>
                    <br/>
                    
                     <div style="transform:scale(1.0); -webkit-transform:scale(1.0);transform-origin:0 0;-webkit-transform-origin:0 0;">
                    <cc1:CaptchaControl ID="captchaGrantee" runat="server" 
                        CaptchaBackgroundNoise="Low" CaptchaLength="5" 
                        CaptchaHeight="60" CaptchaWidth="250" 
                        CaptchaLineNoise="None" CaptchaMinTimeout="10" 
                        CaptchaMaxTimeout="240" FontColor = "#529E00" />
                    </div>
                    <br/>
                    <div class="input-group">
                         <asp:TextBox ID="txtCaptcha2" runat="server" CssClass="form-control" placeholder="Enter the above Text"></asp:TextBox>
                          <br/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Required!" ControlToValidate = "txtCaptcha2" ForeColor="red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Button ID="btnGrantLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-lg btn-block" OnClick="btnGrantLogin_OnClick" />
                         </div>
                          <div class="col-md-6">
                             <span class="pull-right"><asp:LinkButton runat="server" ID="lnkBtnOrgResetP" OnClick="lnkBtnOrgResetP_OnClick" CausesValidation="False">Reset Password</asp:LinkButton></span>
                         </div>
                    </div>
                </asp:View>
                 <asp:View runat="server" ID="viewConsultant">
                    <div class="input-group">
                    <span class="input-group-addon"><i class="fa fa-user"></i> Consultant Username:</span>
                    <asp:TextBox ID="txtConsUsernameIS" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                    </div>
                    <br />
                    <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-key"></i>Consultant Password:</span>
                        <asp:TextBox ID="txtConsPasswordIS" runat="server" CssClass="form-control" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                    </div>
                    <br/>

                    <div class="input-group">
                        <asp:CheckBox ID="chckMeConsult" runat="server" />
                        <label>Remember Me</label>
                     </div>
                    <br/>
                   
                    <div style="transform:scale(1.0); -webkit-transform:scale(1.0);transform-origin:0 0;-webkit-transform-origin:0 0;">
                    <cc1:CaptchaControl ID="CaptchaControl1" runat="server" 
                        CaptchaBackgroundNoise="Low" CaptchaLength="5" 
                        CaptchaHeight="60" CaptchaWidth="250" 
                        CaptchaLineNoise="None" CaptchaMinTimeout="10" 
                        CaptchaMaxTimeout="240" FontColor = "#529E00" />
                    </div>
                    <br/>
                    <div class="input-group">
                         <asp:TextBox ID="txtCaptcha3" runat="server" CssClass="form-control" placeholder="Enter the above Text"></asp:TextBox>
                          <br/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Required!" ControlToValidate = "txtCaptcha3" ForeColor="red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Button ID="btnLogConsultant" runat="server" Text="Login" CssClass="btn btn-primary btn-lg btn-block" OnClick="btnLogConsultant_OnClick"/>&nbsp;&nbsp;
                        </div>
                        <div class="col-md-6">
                             <span class="pull-right"><asp:LinkButton runat="server" ID="lnkBtnResetPCons" OnClick="lnkBtnResetPCons_OnClick" CausesValidation="False" useSubmitBehaviour="False">Reset Password</asp:LinkButton></span>
                         </div>
                    </div>  
                </asp:View>
                
                <asp:View runat="server" ID="viewiForgotItP">
                    <div class="form-horizontal">
                              <label class="form-control alert" style="font-weight: bold;"><span class="badge alert-danger">+</span>Please enter the username you registered with to reset your password:</label>
                            <br/>
                     <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-user"></i> Registered Username:</span>
                          <asp:TextBox ID="txtIforgotPassword" runat="server" CssClass="form-control" placeholder="Enter your username"></asp:TextBox>
                    </div>
                    <br />
                        <div class="row">
                            <div class="col-md-6">
                              <asp:Button ID="btnResetUrPss" runat="server" Text="Reset Password" CssClass="btn btn-primary btn-sm btn-block" OnClick="btnResetUrPss_OnClick" />
                            </div>
                            <div class="col-md-6">
                                <asp:Button runat="server" ID="btnbacktomyRoots" Text="Back to Login" CssClass="btn btn-primary btn-sm btn-block" OnClick="btnbacktomyRoots_OnClick"/>
                            </div>
                        </div>
                    </div>
                </asp:View>  

            </asp:MultiView>
        </div>

	</div>

</div>

</div>
<div class="col-md-3"></div>
</div>

</div>
</asp:Content>