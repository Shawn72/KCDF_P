<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_Form.aspx.cs" Inherits="KCDF_P.Report_Form" MasterPageFile="ReportForm_Master.Master" %>
<asp:Content ID="ReportingForm" ContentPlaceHolderID="MainContent" runat="server">
  <%@ OutputCache NoStore="true" Duration="1" VaryByParam="*"   %>
        <div class="panel-body" style="font-family:Trebuchet MS">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="row">
                <div class="col-md-12">
                    <h4 style="align-content:center; font-family:Trebuchet MS; color:#0094ff">Manage Grants Applications</h4><br /></div>
                </div>
            <div class="form-horizontal">
                         
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">KCDF Project:</asp:Label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlAccountType" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlAccountType_OnSelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-5">
                        <asp:TextBox runat="server" ID="txtPrefNo" CssClass="form-control" Enabled="False" placeholder="Project Reference No"></asp:TextBox>
                    </div>
                </div>
            </div>
            <br/>
                <div class="col-md-3"></div>
                <div class="col-md-6">
                     <asp:Label ID="lblError" runat="server" ForeColor="#FF3300" CssClass="text-left hidden"></asp:Label>
                            <span class="text-center text-danger"><small><%=lblError.Text %></small></span>
                  </div>
                <div class="col-md-3"></div>
                <br/>
        
        <section class="panel">
            <div class="panel panel-primary">
                <div class="form-horizontal">
                    
                     <div class="form-group col-md-12">
                       <label class="control-label alert alert-info" style="font-weight: bold;">Upload reporting Documents</label>
                    </div>
                    <br/>
                
                       <div class="form-group">
                            <asp:Label runat="server" CssClass="col-md-3 control-label">Select Year:</asp:Label>
                            <div class="col-md-6">
                                <div class="input-group">
                                        <asp:DropDownList ID="ddlYears" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlYears_OnSelectedIndexChanged">
                                    </asp:DropDownList><span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                </div>
                            </div>
                        </div>

                     <div class="form-group">
                            <asp:Label runat="server" CssClass="col-md-3 control-label">Select Quarter:</asp:Label>
                            <div class="col-md-6">
                                <div class="input-group">
                                        <asp:DropDownList ID="ddlQuarter" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="true" >
                                    
                                        </asp:DropDownList><span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                </div>
                            </div>
                        </div>
                    
                        <div class="row">
                        <div class="col-md-12">
                            <label class="control-label alert-info" style="font-weight: bold;">Select Documents from your computer to upload</label>
                             <br/>
                        </div>
                        <br/>
                               
                         <asp:MultiView ID="switchUserView" runat="server">
                             
                                <asp:View runat="server" ID="amGrantee">
                                     <div class="col-md-12" runat="server" ID="myNarrative" Visible="false">
                                              <div class="form-group">
                                            <div class="col-md-4">
                                                <label class="control-label alert-info" style="font-weight: bold;">Narrative</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:FileUpload ID="FileUploadNarrative" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                <asp:Button ID="btnUploadNarrative" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadNarrative_OnClick" />
                                            </div>
                                            <div class="col-md-2">
                                                <label style="color: green; font-weight:bold" runat="server" id="lblNarr"></label>
                                            </div>
                                        </div>
                                        <br/>
                                     </div>
                                    
                                     <div class="col-md-12" runat="server" ID="myFinancial" Visible="False">
                                              <div class="form-group">
                                            <div class="col-md-4">
                                                <label class="control-label alert-info" style="font-weight: bold;">Financial</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:FileUpload ID="FileUploadFinancial" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                <asp:Button ID="btnUploadFinancial" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadFinancial_OnClick" />
                                            </div>
                                            <div class="col-md-2">
                                                <label style="color: green; font-weight:bold" runat="server" id="lblFin"></label>
                                            </div>
                                        </div>
                                        <br/>
                                     </div>

                                   <div class="col-md-12" runat="server" ID="myData" Visible="False">
                                           <div class="form-group">
                                                <div class="col-md-4">
                                                    <label class="control-label alert-info" style="font-weight: bold;">Data</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:FileUpload ID="FileUploadData" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                    <asp:Button ID="btnUploadData" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadData_OnClick" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label style="color: green; font-weight:bold" runat="server" id="lblData"></label>
                                                </div>
                                            </div>
                                            <br/>
                                 </div> 
                                </asp:View>

                                <asp:View runat="server" ID="amScholar">
                                    <div class="col-md-12">
                                             <div class="form-group">
                                                <div class="col-md-4">
                                                    <label class="control-label alert-info" style="font-weight: bold;">Community Report/Giveback Report</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:FileUpload ID="FileUploadComm" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                    <asp:Button ID="btnUploadComm" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadComm_OnClick" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label style="color: green; font-weight:bold" runat="server" id="appComm"></label>
                                                </div>
                                            </div>
                                            <br/>
                                           
                                    </div>
                            
                                    <div class="col-md-12">
                                             <div class="form-group">
                                                <div class="col-md-4">
                                                    <label class="control-label alert-info" style="font-weight: bold;">Scholarship Report</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:FileUpload ID="FileUploadSchRepo" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                    <asp:Button ID="btnScholReport" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File"  OnClick="btnScholReport_OnClick"/>
                                                </div>
                                                <div class="col-md-2">
                                                    <label style="color: green; font-weight:bold" runat="server" id="lblSchRepo"></label>
                                                </div>
                                            </div>
                                            <br/>
                                    </div>
                                </asp:View>

                                <asp:View runat="server" ID="amConsult">
                                     <div class="col-md-12">
                                             <div class="form-group">
                                                <div class="col-md-4">
                                                    <label class="control-label alert-info" style="font-weight: bold;">Consultancy Report</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:FileUpload ID="FileUploadConsulRepo" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                    <asp:Button ID="btnConsulRepo" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnConsulRepo_OnClick" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label style="color: green; font-weight:bold" runat="server" id="lblCons"></label>
                                                </div>
                                            </div>
                                            <br/>
                                            
                                    </div>
                                </asp:View>

                         </asp:MultiView>
                            
                        </div>

                </div>
            </div>
          </section>
     </div>
    <script>
        function pageLoad() {
            $('.selectpicker').selectpicker();
        }
    </script>
</asp:Content>

