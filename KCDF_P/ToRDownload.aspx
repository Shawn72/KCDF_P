<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToRDownload.aspx.cs" Inherits="KCDF_P.ToRDownload" MasterPageFile="~/Registration.Master"%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblErrMsg" runat="server" CssClass="text-left hidden" Visible="false"></asp:Label> 
    <span class="text-center text-danger"><small><%=lblErrMsg.Text %></small></span> 

    <div class="row">
        <div class="col-md-12">
            <label class="control-label form-control alert-info" style="font-weight: bold;">Download , Read and understand TOR before applying</label>
        </div>
       <br/>
    <hr />
    </div>
    
    <div class="row">
    <div class="col-md-12">
      <p class="form-control alert alert-info" style="font-weight: bold;"> Terms of Reference</p> 
    </div>
        <div class="col-md-12">
        <asp:GridView ID="gridViewUploads" runat="server" CssClass="table table-striped table-advance table-hover" GridLines="None" AutoGenerateColumns="false" EmptyDataText = "No files available for download">
            <Columns>
                <asp:BoundField DataField="Text" HeaderText="File Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("Value") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>        
    </div>
   
</asp:Content>