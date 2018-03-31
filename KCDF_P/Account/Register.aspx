<%@ Page Title="Register" Language="C#" MasterPageFile="~/Registration.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="KCDF_P.Account.Register" %>
<asp:Content ID="registerMe" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel-body" style="font-family:Trebuchet MS">
        <div class="row">
            <div class="col-md-3"></div>
             <div class="col-md-6">
             <p align="center"><h4 style="align-content:center; font-family:Trebuchet MS; color:#0094ff">Please create an account with us</h4></p><hr /></div>
            <div class="col-md-3"></div>
     </div>
<asp:Label ID="lblError" runat="server" ForeColor="#FF3300" CssClass="text-left hidden"></asp:Label>
 <span class="text-center text-danger"><small><%=lblError.Text %></small></span> 
      <div class="form-horizontal">
        <div class="form-group">
            <asp:Label runat="server"  CssClass="col-md-3 control-label">Account Type:</asp:Label>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlAccountType" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                            data-live-search="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAccountType_OnSelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Selected="True">..Select Account Type..</asp:ListItem>
                        <asp:ListItem>Scholarship Account</asp:ListItem>
                        <asp:ListItem>Grantee Account</asp:ListItem>
                    </asp:DropDownList>
                </div> 
        </div>
      </div>
      <br/>
    <asp:MultiView runat="server" ID="accuontTypeViews">
        <asp:View runat="server" ID="scholarView">
            <div class="form-horizontal">
               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">First Name:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtFirstname" CssClass="form-control" required="True" placeholder="Your firstname" />              
                    </div> 
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
                    </div> 
                </div> 

           
               <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Email:</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" required="True" TextMode="Email" placeholder="Your email address"/>              
                </div> 
               </div>
         
              <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Username:</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" required="True" placeholder="Choose username" />              
                </div> 
               </div>
           
             <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Password:</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtPassword1" CssClass="form-control" required="True" TextMode="Password" placeholder="Your password"/>              
                </div> 
              </div>
            
              <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Confirm Password:</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtPassConfirmed" CssClass="form-control" required="True" TextMode="Password" placeholder="Confirm password"/>              
                </div>
              </div>
            <div class="col-md-3"></div>
            <div class="col-md-4">
                <div class="form-group">            
                    <asp:Button ID="btnReg" runat="server" Text="Create Account" CssClass="btn btn-primary pull-right btn-sm" OnClick="CreateUser_Click"/>          
                </div>
            </div>
            <div class="col-md-3"></div>
            </div>
        </asp:View>

        <asp:View runat="server" ID="grantsView">
            
            <div class="form-horizontal">
			         <div class="form-group">
                        <asp:Label runat="server" CssClass="col-md-3 control-label">Organization Name:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtOrgName" CssClass="form-control" required="True" placeholder="Organization Name" />              
                        </div> 
                       </div>
               
                    <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Organization Email:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtOrgEmail" CssClass="form-control" placeholder="Organization Email" />              
                        </div> 
                    </div>
               
                    <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Organization Username:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtOrgUsername" CssClass="form-control"  required ="true" placeholder="Choose a unique username" />              
                    </div> 
                    </div>
               
              <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">Password:</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtPasswordOne" CssClass="form-control" required="True" TextMode="Password" placeholder="Your password"/>              
                </div> 
               </div>
          
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">Confirm Password:</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtPassConfir" CssClass="form-control" required="True" TextMode="Password" placeholder="Confirm password"/>              
                    </div> 
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
    </asp:MultiView>

  </div>
</asp:Content>



 