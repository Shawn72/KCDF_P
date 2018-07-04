<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordReset.aspx.cs" MasterPageFile="~/PasswordReset_Master.Master" Inherits="KCDF_P.PasswordReset" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="height: 20px">&nbsp;</div>
    <div class="row">
        <div class="col-md-6">
             <asp:Label ID="lblError" runat="server" ForeColor="#FF3300" CssClass="text-center hidden"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading text-danger"><i class="fa fa-user"></i>Reset your Password</div>
                <span class="text-left text-danger"><small><%=lblError.Text %></small></span>
                <div class="form-group form-inline">
                  
                                 <table style="width: 100%;">
                                <tr><td>&nbsp;</td></tr> 
                                <tr>
                                    <td><small>Old Password</small></td>
                                    <td><asp:TextBox ID="txtPasswordOld" runat="server" CssClass="form-control" placeholder="Enter Old Password" TextMode="Password"></asp:TextBox> <br /></td>
                                </tr>
                               <tr>
                                    <td>&nbsp;</td>
                               </tr>
                                <tr>
                                    <td><small>New Password</small></td>
                                    <td><asp:TextBox ID="txtPasswordNew" runat="server" CssClass="form-control" placeholder="Enter New Password" TextMode="Password"></asp:TextBox> <br /></td>
                                </tr>
                                <tr>
                                     <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Confirm Password</td>
                                    <td style="margin-left: 80px"><asp:TextBox ID="txtPasswordConfirm" runat="server" CssClass="form-control" placeholder="Re-Enter New Password" TextMode="Password"></asp:TextBox> <br /></td>
                                </tr>
                                <tr>
                                     <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td style="margin-left: 80px">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" OnClick="btnSubmit_Click" Text="Submit" />
                                     </td>
                                </tr>
                            </table>
                    </div>
                </div>
            </div>
        </div>
      <script type="text/javascript">
        function myFunctionLoadScolars() {
        var txt;
        var r = confirm("Password Changed successfully!");
        if (r === true) {
            window.location.href = "Dashboard.aspx";
        } else {
            txt = "Cancelled!";
            }
        }

        function myFunctionLoadGrantees() {
            var txt;
            var r = confirm("Password Changed successfully!");
            if (r === true) {
                window.location.href = "Grantee_Dashboard.aspx";
            } else {
                txt = "Cancelled!";
            }
        }

        function myFunctionLoadConsultants() {
            var txt;
            var r = confirm("Password Changed successfully!");
            if (r === true) {
                window.location.href = "Consultancy_Page.aspx";
            } else {
                txt = "Cancelled!";
            }
        }
    </script>
 </asp:Content>
