<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFiles.aspx.cs" Inherits="KCDF_P.UploadFiles" MasterPageFile="Site.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblErrMsg" runat="server" CssClass="text-left hidden" Visible="false"></asp:Label> 
    <span class="text-center text-danger"><small><%=lblErrMsg.Text %></small></span> 
    <div class="row">
        <div class="col-md-12">
            <label class="control-label form-control alert-info" style="font-weight: bold;">Select Documents from your computer to upload</label>
        </div>
        <br/>
        <div class="col-md-12">
             <asp:FileUpload ID="FileUpload" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp; <asp:Button ID="btnUpload" runat="server"  CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="UploadFile" />
        </div>
       <br/>
        <hr />
    </div>
    
    <div class="row">
    <div class="col-md-12">
      <p class="form-control alert alert-info" style="font-weight: bold;"> My Uploaded Documents</p> 
    </div>
  
    <div class="col-md-12">
        <asp:GridView ID="gridViewUploads" runat="server" CssClass="table table-striped table-advance table-hover" GridLines="None" AutoGenerateColumns="false" EmptyDataText = "No files uploaded">
            <Columns>
                <asp:BoundField DataField="Text" HeaderText="File Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("Value") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID = "lnkDelete" Text = "Delete" CommandArgument = '<%# Eval("Value") %>' runat = "server" OnClick = "DeleteFile" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </div>
   
</asp:Content>