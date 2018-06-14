<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Activated_Grantees.aspx.cs" Inherits="KCDF_P.Account.Activated_Grantees" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Trebuchet MS;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <h2><asp:Literal ID="ltMessage" runat="server" /></h2>
    <asp:LinkButton ID="lnkToHome" runat="server" OnClick="lnkToHome_OnClick">Go to Login</asp:LinkButton>
    </form>
</body>
</html>

