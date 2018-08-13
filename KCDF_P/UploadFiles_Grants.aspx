<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFiles_Grants.aspx.cs" Inherits="KCDF_P.UploadFiles" MasterPageFile="~/Gran_Master.Master" %>
<%@ OutputCache NoStore="true" Duration="1" VaryByParam="*"   %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblErrMsg" runat="server" CssClass="text-left hidden" Visible="false"></asp:Label> 
    <span class="text-center text-danger"><small><%=lblErrMsg.Text %></small></span> 
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

     <div class="form-horizontal">
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                      <ContentTemplate>         
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="col-md-3 control-label">KCDF Project:</asp:Label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlAccountType" runat="server" class="selectpicker form-control" 
                                    data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="true" 
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlAccountType_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtPrefNo" CssClass="form-control" Enabled="False" placeholder="Project Reference No"></asp:TextBox>
                            </div>
                        </div>
                </ContentTemplate>
             </asp:UpdatePanel>
            </div>
            <br/>

    <div class="row">
        <div class="col-md-12">
            <label class="control-label form-control alert-info" style="font-weight: bold;">Download KCDF files here.</label>
        </div>
       <br/>
    <hr />
    </div>
    
    <div class="row">
    <div class="col-md-12">
      <p class="form-control alert alert-info" style="font-weight: bold;"> POCA Tool And Indicator Matrix</p> 
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
            <div class="row">
            <div class="col-md-12">
                <label class="control-label form-control alert-info" style="font-weight: bold;">Select Documents from your computer to upload</label>
            </div>
            <br/>

            <div class="col-md-12">
                <div class="row">
                    <div class="form-group">
                        <div class="col-md-5">
                            <label class="control-label form-control alert-info" style="font-weight: bold;">POCA Tool</label>
                        </div>
                        <div class="col-md-7">
                            <asp:FileUpload ID="FileUploadPOCA" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                            <asp:Button ID="btnUploadPOCA" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="UploadFile" />
                        </div>
                    </div>
                    <br/>
                </div>
            </div>

            <div class="col-md-12">
                <div class="row">
                    <div class="form-group">
                        <div class="col-md-5">
                            <label class="control-label form-control alert-info" style="font-weight: bold;">Indicator Matrix</label>
                        </div>
                        <div class="col-md-7">
                            <asp:FileUpload ID="FileUploadMatrix" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                            <asp:Button ID="btnUploadMatrix" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadMatrix_OnClick" />
                        </div>
                    </div>
                    <br/>
                </div>
            </div>
        </div>
        
         <div class="row">
            <div class="col-md-12">
                <p class="form-control alert alert-info" style="font-weight: bold;"> My Uploaded Documents</p>
            </div>

            <div class="col-md-12">
                <asp:GridView ID="grViewMyDocs" runat="server" CssClass="table table-striped table-advance table-hover footable"
                        GridLines="None" EmptyDataText="No files uploaded" AutoGenerateColumns="False"
                        DataKeyNames="Id, Grant_No" AlternatingRowStyle-BackColor="#C2D69B" AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="S/No:" />
                        <asp:BoundField DataField="Document_Kind" HeaderText="Document" />
                        <asp:BoundField DataField="Document_Name" HeaderText="File Name" />
                        <asp:BoundField DataField="Document_type" HeaderText="File Type" />
                            <asp:BoundField DataField="Grant_No" HeaderText="Grant Number" />
                        <asp:BoundField DataField="Project_Name" HeaderText="Grant Name"/>
                    </Columns>
                </asp:GridView>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
    </div>

    <script>
    function pageLoad() {
        $('.selectpicker').selectpicker();
    }
    </script>
</asp:Content>