<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFiles_Grants.aspx.cs" Inherits="KCDF_P.UploadFiles" MasterPageFile="~/Gran_Master.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblErrMsg" runat="server" CssClass="text-left hidden" Visible="false"></asp:Label> 
    <span class="text-center text-danger"><small><%=lblErrMsg.Text %></small></span> 

    <div class="row">
        <div class="col-md-12">
            <label class="control-label form-control alert-info" style="font-weight: bold;">Download KCDF files here.</label>
        </div>
       <br/>
    <hr />
    </div>
    
    <div class="row">
    <div class="col-md-12">
      <p class="form-control alert alert-info" style="font-weight: bold;"> KCDF Grants Files</p> 
    </div>
        
    <asp:MultiView runat="server" ID="multiVDlds">
        <asp:View runat="server" ID="GranteesViewId">
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
        </asp:View>
        <asp:View runat="server" ID="scholarshipViewId">
              <div class="col-md-12">
                <asp:GridView ID="gridViewScholarUploads" runat="server" CssClass="table table-striped table-advance table-hover" GridLines="None" AutoGenerateColumns="false" EmptyDataText = "No files available for download">
                    <Columns>
                        <asp:BoundField DataField="Text" HeaderText="File Name" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload2" Text = "Download" CommandArgument = '<%# Eval("Value") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </asp:View>
    </asp:MultiView>
    </div>
   
</asp:Content>