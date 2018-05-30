<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gran_Application.aspx.cs" MasterPageFile="~/Gran_Master.Master"Inherits="KCDF_P.Gran_Application" %>
    <asp:Content ID="OrganizationRegistrationForm" ContentPlaceHolderID="MainContent" runat="server">
        <%--<meta http-equiv="refresh" content="120;url=Gran_Application.aspx">--%>
        <div class="panel-body" style="font-family:Trebuchet MS">
            <div class="row">
                <div class="col-md-12">
                    <h4 style="align-content:center; font-family:Trebuchet MS; color:#0094ff">Manage Grants Applications</h4><br /></div>
            </div>
            <div class="form-horizontal">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">KCDF Project:</asp:Label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="ddlAccountType" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlAccountType_OnSelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <br/>
            <%
        var selProj = ddlAccountType.SelectedItem.Text;
        var proFtures = nav.call_for_Proposal.ToList().Where(bf => bf.Project == selProj);
        var itswhats = proFtures.Select(f => f.Basic_Features).SingleOrDefault();

        if (itswhats == false)
        {
         %>
            <header class="panel-heading tab-bg-info">
                <asp:Menu ID="OrgInfoMenu" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server" OnMenuItemClick="studentInfoMenu_OnMenuItemClick">
                    <Items>
                        <asp:MenuItem Text="KCDF Downloads |" Value="0" runat="server" />
                        <asp:MenuItem Text="Applicant Information |" Value="1" runat="server" />
                        <asp:MenuItem Text="Grants Management |" Value="2" runat="server" />
                        <asp:MenuItem Text="Project Overview |" Value="3" runat="server" />
                        <asp:MenuItem Text="Target Information |" Value="4" runat="server" />
                        <asp:MenuItem Text="Upload Project Documents |" Value="5" runat="server" />
                        <asp:MenuItem Text="Submit Application |" Value="6" runat="server" />
                    </Items>
                </asp:Menu>
            </header>
        <% }
        else if (itswhats == true)
        {
         %>
            <header class="panel-heading tab-bg-info">
                <asp:Menu ID="orgInfoMenu2" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server" OnMenuItemClick="orgInfoMenu2_OnMenuItemClick">
                    <Items>
                        <asp:MenuItem Text="KCDF Downloads |" Value="0" runat="server" />
                        <asp:MenuItem Text="Applicant Information |" Value="1" runat="server" />
                        <asp:MenuItem Text="Project Overview |" Value="2" runat="server" />
                        <asp:MenuItem Text="Target Information |" Value="3" runat="server" />
                        <asp:MenuItem Text="Upload Project Documents |" Value="4" runat="server" />
                        <asp:MenuItem Text="Submit Application |" Value="5" runat="server" />
                    </Items>
                </asp:Menu>
            </header>
            <% } %>
        <asp:Label ID="lbError" runat="server" ForeColor="#FF3300" CssClass="text-left hidden"></asp:Label>
        <section class="panel">
            <div class="panel panel-primary">
                <span class="text-center text-danger"><small><%=lbError.Text %></small></span>
                <br/>
                  <div class="form-group">
                        <asp:Label runat="server" CssClass="col-md-3 control-label">Organization Registration No:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtRegNo" CssClass="form-control" style="text-transform:uppercase" Enabled="False" />
                        </div>
                      <br/>
                  </div>
                <br/>
                <asp:MultiView ID="orgPMultiview" runat="server" ActiveViewIndex="0">
                
                    <asp:View runat="server" ID="viewUploads">
                        <div class="form-horizontal">
                        <div class="row">
                        <div class="col-md-12">
                          <p class="form-control alert alert-info" style="font-weight: bold;"> Download KCDF Grant files here</p> 
                        </div>
                            <br/>
                            <div class="col-md-12">
                                 <asp:GridView ID="gridVGrantsDownloads" runat="server" CssClass="table table-striped table-advance table-hover" GridLines="None" AutoGenerateColumns="false" EmptyDataText = "No files available for download">
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
                          </div>
                    </asp:View>

                    <asp:View runat="server" ID="myApplicantInfo">
                         <div class="form-horizontal">
                        <br/>
                        <div class="col-md-12">
                            <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">1</span>Contact Information of applying organization:</label> 
                        </div>
                         <br />
                            <div class="form-group">
                            <asp:Label runat="server" CssClass="col-md-3 control-label">Name of Contact Person:</asp:Label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="TextBxcont" CssClass="form-control" style="text-transform:uppercase" Enabled="False" />               
                                </div>
                            </div>

                               <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Organization Name:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="txOrgname" CssClass="form-control" style="text-transform:uppercase" Enabled="False"/>               
                                    </div>
                                </div>

                            <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">His or Her current position:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="TextBoposition" CssClass="form-control"  Enabled="False" />              
                                    </div> 
                            </div>
                   
                            <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Postal Address:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="TextBxpostal" CssClass="form-control" TextMode="Number"  Enabled="False" />              
                                    </div> 
                            </div>
                   
                            <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Postal Code:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div> 
                            </div>
                   
                            <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Town:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="txtPostalTown" CssClass="form-control" style="text-transform:uppercase" Enabled="False" />              
                                    </div> 
                            </div>
                   
                            <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Phone:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="TextBoxphone" CssClass="form-control"  Enabled="False" />              
                                    </div> 
                            </div>

                             <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Email Address:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="txEmailAdd" CssClass="form-control" Enabled="False" />              
                                    </div> 
                             </div>
                   
                            <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Website Address if applicable:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="TextBoxweb" CssClass="form-control"  Enabled="False" />              
                                    </div> 
                            </div>
                        <div class="col-md-12">
                            <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">2</span>Type of applying organization :</label> 
                        </div>
                         <br /> 
                             <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Non -Governmental:</asp:Label>
                                    <div class="col-md-6">
                                       <asp:TextBox ID="txtNGO" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                </div> 
                            </div>

                             <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Non -Profitable:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtNonProfit" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                </div> 
                            </div>
                               <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Proposal Alignment to call Objectives</asp:Label>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlObjectives" runat="server"  class="selectpicker form-control" data-live-search-style="begins"
                                                data-live-search="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlObjectives_OnSelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div> 
                                   <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:TextBox id="txtAreaObjs" TextMode="multiline" Columns="10" Width="100%" Rows="2" runat="server" placeholder="Objectives Here" Visible="False" ReadOnly="True" />
                                        </div>
                                   </div>
                                </div>
                   
                             <div class="form-group">
                                <asp:Label runat="server"  CssClass="col-md-3 control-label">Type Of Registration:</asp:Label>
                                    <div class="col-md-6">
                                       <asp:TextBox ID="txtTypeofOrg" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                    </div>
                                 <div class="col-md-3"><asp:Button runat="server" ID="btnSaveObjectives" Text="Save Information" CausesValidation="False" OnClick="btnSaveObjectives_OnClick" CssClass="btn btn-primary pull-left btn-sm"/></div> 
                            </div>
                           <br/>                                 
               
                         </div>
                         <br/>
                    </asp:View>

                    <asp:View runat="server" ID="grantsMgt">
                        <div class="form-horizontal">
                            <div class="row">
                                <div class="col-md-12">
                                    <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">1</span>Grants Management History.</label>
                                </div>
                                <br/>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Have you Received KCDF funding before?</asp:Label><span class="required">*</span>
                                            <div class="col-md-6">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Please answer approriately!<br />"
                                        ControlToValidate="rdoBtnListYesNo" runat="server" ForeColor="Red" Display="Dynamic" />
                                                    <asp:RadioButtonList ID="rdoBtnListYesNo" runat="server" OnSelectedIndexChanged="rdoBtnListYesNo_OnSelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Text="Yes" Value="0" Selected="False"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="1" Selected="False"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div> 
                                    </div>
                                    <div id="idKCDFyes" style="display:none">
                                         <div class="form-group">
                                            <asp:Label runat="server" CssClass="col-md-3 control-label">If Yes what is the status of the grant?</asp:Label><span class="required">*</span>
                                                <div class="col-md-6">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Please answer approriately!<br />"
                                                         ControlToValidate="rdBtnStatus" runat="server" ForeColor="Red" Display="Dynamic" />
                                                        <asp:RadioButtonList ID="rdBtnStatus" runat="server" OnSelectedIndexChanged="rdBtnStatus_OnSelectedIndexChanged">
                                                        <asp:ListItem Text="Ongoing" Value="0" Selected="False"></asp:ListItem>
                                                        <asp:ListItem Text="Completed" Value="1" Selected="False"></asp:ListItem>
                                                        <asp:ListItem Text="Terminated" Value="2" Selected="False"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div> 
                                        </div>
                                    </div>
                                     <div class="col-md-12">
                                        <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">2</span>History of grants received</label>
                                    </div>
                                    <br/>
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Name of the Donor:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtDonor" CssClass="form-control" style="text-transform:uppercase" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Grant Amount Provided:</asp:Label>
                                        <div class="col-md-6">
                                         <label id="lblCharLeft2" title=""></label>
                                        <br/>
                                            <asp:TextBox runat="server" ID="txtAmount" CssClass="form-control" required="True" TextMode="Number" onKeyUp="javascript:CheckView2(this, 9);" onChange="javascript:CheckView2(this, 9);"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Year of Award:</asp:Label>
                                        <div class="col-md-6">
                                            <div class="input-group date">
                                                <input type="text" id="yrofAward" runat="server" class="form-control" /><span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Objective of grant :</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextObj" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">County (ies) targeted:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddltargetCounty" runat="server" class="selectpicker form-control" data-live-search-style="begins"
                                                 data-live-search="true" AppendDataBoundItems="true" 
                                                AutoPostBack="True" OnSelectedIndexChanged="ddltargetCounty_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:TextBox id="txtAreaCounties" TextMode="multiline" Columns="10" Width="100%" Rows="2" runat="server" placeholder="Counties Here" Visible="False" ReadOnly="True" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Type of beneficiaries reached/targeted:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextTypeBeneficiary" CssClass="form-control" style="text-transform:uppercase" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">No. of beneficiaries reached/targeted:</asp:Label>
                                        <div class="col-md-6">
                                             <label id="lblCharLeft3" title=""></label>
                                            <br/>
                                            <asp:TextBox runat="server" ID="TextNoBeneficiary" CssClass="form-control" onKeyUp="javascript:CheckView3(this, 5);" onChange="javascript:CheckView3(this, 5);"/>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Year of funding:</asp:Label>
                                        <div class="col-md-6">
                                            <div class="input-group date">
                                                <input type="text" id="yrofFunding" runat="server" class="form-control" /><span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Grant amount received:</asp:Label>
                                        <div class="col-md-6">
                                            <label id="lblCharLeft4" title=""></label>
                                            <br/>
                                            <asp:TextBox runat="server" ID="TextAmount" CssClass="form-control" onKeyUp="javascript:CheckView4(this, 9);" onChange="javascript:CheckView4(this, 9);" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Intervention supported:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextIntspprt" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-3 control-label">Project Status:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlPrjStatus" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="true" AutoPostBack="True">
                                                <asp:ListItem Selected="True">..Select Project Status..</asp:ListItem>
                                                <asp:ListItem>Complete</asp:ListItem>
                                                <asp:ListItem>Ongoing</asp:ListItem>
                                                <asp:ListItem>Terminated</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3"></div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Button ID="btnSaveGrantHisto" runat="server" Text="Update Grants History" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnSaveGrantHisto_OnClick" />
                                            </div>
                                        </div>
                                        <div class="col-md-3"></div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <asp:GridView ID="tblGrantsManager" runat="server" CssClass="table table-condensed table-responsive table-bordered footable" Width="100%" AutoGenerateSelectButton="false" EmptyDataText="No Grant History Found!" AlternatingRowStyle-BackColor="#C2D69B" DataKeyNames="No" OnRowDeleting="tblGrantsManager_OnRowDeleting"
                                        OnRowDataBound="tblGrantsManager_OnRowDataBound" AllowSorting="True">
                                        <Columns>
                                            <asp:BoundField DataField="No" HeaderText="S/No:" />
                                            <asp:BoundField DataField="Project_Name" HeaderText="Project Name" />
                                            <asp:BoundField DataField="Name_Of_the_Donor" HeaderText="Donor" />
                                            <asp:BoundField DataField="Grant_Amount_Recieved" HeaderText="Grant Amount Received:" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="Project_Status" HeaderText="Project Status:" />
                                            <asp:CommandField ShowDeleteButton="True" ButtonType="Button"/>
                                        </Columns>
                                        <SelectedRowStyle BackColor="#259EFF" BorderColor="#FF9966" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <br/>

                    </asp:View>

                    <asp:View runat="server" ID="Projectoverview">
                        <div class="form-horizontal">
                            <div class="col-md-12">
                                <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">1</span>Project Title:</label>
                            </div>
                            <br />
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Proposed short title of your project:</asp:Label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="TextBoxtitle" CssClass="form-control" style="text-transform:uppercase" />
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Project Start Date:</asp:Label>
                                <div class="col-md-6">
                                    <div class="input-group date">
                                        <input type="text" id="txtDateofStart" runat="server" class="form-control" /><span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">2</span>Project Area:</label>
                            </div>
                            <br />
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">County:</asp:Label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlSelCounty" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSelCounty_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Constituency:</asp:Label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlConstituency" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="False"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Description of areas where you plan to implement your project</asp:Label>
                                <div class="col-md-6">
                                    <asp:TextBox id="txtAreaTargetSettmnt" TextMode="multiline" Columns="100" Width="100%" Rows="5" runat="server" class="max" />
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">3</span>Project Duration:</label>
                            </div>
                            <br />
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Expected length of your project (max is 24 months):</asp:Label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlMonths" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="true">
                                        <asp:ListItem Selected="True">..Select project Length..</asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                        <asp:ListItem>24</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">4</span>	Requested resources :</label>
                            </div>
                            <br />
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Please Select an estimated scale of the grant funding needed for the implementation of the proposed project :</asp:Label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlEstScale" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="true">
                                        <asp:ListItem Selected="True">..Select Estimated Scale..</asp:ListItem>
                                        <asp:ListItem>KES 1,000,001 to 2,000,000</asp:ListItem>
                                        <asp:ListItem>KES 2,000,001 to 3,000,000</asp:ListItem>
                                        <asp:ListItem>KES 3,000,001 to 4,000,000</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">5</span>Breakdown of the cost of your proposed project in KES:Refer to Application contribution <a href="http://www.kcdf.or.ke/index.php/work/call-for-proposal">Guidelines</a></label>
                            </div>
                            <br />
                            
                            <div class="row">
                                <div class="col-md-3"></div>
                                 <div class="col-md-6">
                                   <label id="lblCharLeft" title=""></label>
                                 </div>
                               <div class="col-md-3"></div>
                            </div>
                           <br/>

                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Total project cost in cash -(KES):</asp:Label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="TextBoxcost" CssClass="form-control" TextMode="Number" onKeyUp="javascript:Check(this, 9);" onChange="javascript:Check(this, 9);"/>
                                </div>
                                <div class="col-md-3" >
                                    <asp:Button runat="server" ID="btnRefreshScript" Enabled="False" CssClass="btn btn-primary pull-left btn-sm" Text="Refresh" OnClick="btnRefreshScript_OnClick"/>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Your CASH contribution in cash -(KES):</asp:Label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="TextBoxcont" CssClass="form-control" TextMode="Number" onKeyUp="javascript:ValidatemeSpecialCase(this);" onChange="javascript:ValidatemeSpecialCase(this);" Enabled="True"  />
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-md-3 control-label">Amount requested from KCDF in cash –(KES):</asp:Label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="TextBoxrequested" CssClass="form-control" TextMode="Number" onKeyUp="javascript:Check(this, 9);" onChange="javascript:Check(this, 9);" Enabled="False" />
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-3"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnUpdatePOverview" runat="server" Text="Update Project Overview" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnUpdatePOverview_OnClick" Enabled="false"/>
                                    </div>
                                </div>
                                <div class="col-md-3"></div>
                            </div>
                            <br/>

                        </div>
                    </asp:View>

                    <asp:View ID="targetGroup" runat="server">
                        <div class="form-horizontal">
                            <div class="col-md-12">
                                <p>In this section, you are required to indicate the population that you intend to reach through the project for which you have asked KCDF to support: </p>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Households</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxhse" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ErrorMessage="This textfield is mandatory"
                                         ControlToValidate="TextBoxschl" runat="server" ForeColor="Red" Display="Dynamic" />
                                             </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Schools:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxschl" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ErrorMessage="This textfield is mandatory"
                                         ControlToValidate="TextBoxschl" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Grass-root organizations:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxorg" CssClass="form-control" Placeholder="Number Targeted" TextMode="Number" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="This textfield is mandatory"
                                         ControlToValidate="TextBoxorg" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Youth ( aged 18 – 35 years):</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxyth" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="This textfield is mandatory"
                                         ControlToValidate="TextBoxyth" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Women:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBowmn" CssClass="form-control" Placeholder="Number Targeted" TextMode="Number" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBowmn" runat="server" ForeColor="Red" Display="Dynamic" /> 
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Men:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxmn" CssClass="form-control" Placeholder="Number Targeted" TextMode="Number" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxmn" runat="server" ForeColor="Red" Display="Dynamic" /> 
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Children (0-6 years old):</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBcldold" CssClass="form-control" Placeholder="Number Tageted" TextMode="Number" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBcldold" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Children ( 7- 14 years old):</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxold" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxold" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Children (15-17 years old):</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxren" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxren" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Orphans and Vulnerable Children:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxorph" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxorph" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">People with Terminal Illness:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxill" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxill" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Marginalized and pastoral population:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxmarg" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxmarg" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                            </div><br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Drug users:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxdrg" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxdrg" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Commercial Sex Workers:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxsxwrkr" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxsxwrkr" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                            </div><br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Teachers:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxtchr" CssClass="form-control" placeholder="Number Targeted" TextMode="Number" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxtchr" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-6 control-label">Farmers:</asp:Label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="TextBoxfarmr" CssClass="form-control" placeholder="Number Target" TextMode="Number" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ErrorMessage="This field is mandatory"
                                     ControlToValidate="TextBoxfarmr" runat="server" ForeColor="Red" Display="Dynamic" />
                                          </div>
                                    </div>
                                </div>
                            </div><br />

                            <div class="row">
                                <div class="col-md-9"></div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnSaveTarget" runat="server" Text="Save Target Information" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnSaveTarget_Click" />
                                    </div>
                                </div>
                                <%-- <div class="col-md-3"></div>--%>
                            </div>
                            <br />
                        </div>
                    </asp:View>

                    <asp:View runat="server" ID="uploadDocs">
                        <asp:Label ID="lblErrMsg" runat="server" CssClass="text-left hidden" Visible="false"></asp:Label>
                        <span class="text-center text-danger"><small><%=lblErrMsg.Text %></small></span>
                        <div class="row">
                            <div class="col-md-12">
                                <label class="control-label form-control alert-info" style="font-weight: bold;">Select Documents from your computer to upload</label>
                            </div>
                            <br/>

                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-5">
                                            <label class="control-label form-control alert-info" style="font-weight: bold;">Project Proposal</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:FileUpload ID="FileUpload" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="UploadFile" />
                                        </div>
                                    </div>
                                    <br/>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-5">
                                            <label class="control-label form-control alert-danger" style="font-weight: bold;">Registration Certificate</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:FileUpload ID="FileUploadID" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="btnUploadID" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadID_OnClick" />
                                        </div>
                                    </div>
                                    <br/>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-5">
                                            <label class="control-label form-control alert-info" style="font-weight: bold;">Organizational Constitution </label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:FileUpload ID="FileUploadConst" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="btnUploadConstitution" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadConstitution_OnClick" />
                                        </div>
                                    </div>
                                    <br/>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-5">
                                            <label class="control-label form-control alert-info" style="font-weight: bold;">Ordinary /committee members List:  </label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:FileUpload ID="FileUploadList" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="btnUploadList" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnUploadList_OnClick" />
                                        </div>
                                    </div>
                                    <br/>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-5">
                                            <label class="control-label form-control alert-info" style="font-weight: bold;">Recent Financial Report  </label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:FileUpload ID="FileUploadFinRePo" runat="server" CssClass="btn btn-success pull-left btn-sm" /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="btnFinReport" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload File" OnClick="btnFinReport_OnClick" />
                                        </div>
                                    </div>
                                    <br/>
                                </div>
                            </div>

                            <br/>
                            <hr />
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <p class="form-control alert alert-info" style="font-weight: bold;"> My Uploaded Documents</p>
                            </div>

                            <div class="col-md-12">
                                <asp:GridView ID="gridViewUploads" runat="server" CssClass="table table-striped table-advance table-hover footable"
                                     GridLines="None" EmptyDataText="No files uploaded"
                                     OnRowDeleting="gridViewUploads_OnRowDeleting" AutoGenerateColumns="False"
                                     DataKeyNames="Id, Grant_No" AlternatingRowStyle-BackColor="#C2D69B"
                                    OnRowDataBound="gridViewUploads_OnRowDataBound" AllowSorting="True">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="S/No:" />
                                        <asp:BoundField DataField="Document_Kind" HeaderText="Document" />
                                        <asp:BoundField DataField="Document_Name" HeaderText="File Name" />
                                        <asp:BoundField DataField="Document_type" HeaderText="File Type" />
                                         <asp:BoundField DataField="Grant_No" HeaderText="Grant Number" />
                                        <asp:BoundField DataField="Project_Name" HeaderText="Grant Name"/>
                                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                    </Columns>
                                </asp:GridView>
                                </div>
                            </div>
                    </asp:View>

                    <asp:View ID="finalSubmit" runat="server">
                        <div class="form-horizontal">
                            <div class="col-md-12">
                                <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger">1</span>Validate and submit your application:</label>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-12">
                                <asp:GridView ID="gridSubmitApps" runat="server" CssClass="table table-condensed footable" Width="100%" AutoGenerateSelectButton="false"
                                     EmptyDataText="No unsubmitted application" AlternatingRowStyle-BackColor="#C2D69B" DataKeyNames="No"
                                    >
                                    <Columns>
                                        <asp:BoundField DataField="No" HeaderText="S/No:" />
                                        <asp:BoundField DataField="Project_Name" HeaderText="Project Name" />
                                        <asp:BoundField DataField="Submission_Status" HeaderText="Submission Status" />
                                         <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEditMe" Text="Confirm Attachments/Submit" CommandArgument='<%# Eval("No") %>' CommandName="lnkEditMe" runat="server" OnClick="lnkEditMe_OnClick"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <SelectedRowStyle BackColor="#259EFF" BorderColor="#FF9966" />
                                </asp:GridView>
                            </div>
                            </div>
                        </div>
                    </asp:View>

                    </asp:MultiView>
                </div>
            
              <div class="modal fade" id="pageApplications" data-backdrop="static">
		        <div class="modal-dialog" runat="server">
			        <div class="modal-content" runat="server">
                            <div class="panel-heading" style="text-align:left; background: #00bfff; color: #f0f8ff">Submit your projects</div>
			            <div class="modal-header" runat="server">
					            <strong>Confirm Submission for Project:</strong>&nbsp;&nbsp;<asp:Label runat="server" ID="lblProjNb">&nbsp;</asp:Label>
					            <button type="button" class="close" data-dismiss="modal">&times;</button>
				            </div>
			            <div class="modal-body">
                        <div class="form-horizontal">
                             <div class="row">
                                <div class="col-md-3">
                                    <asp:Button ID="btnValidateInfo" runat="server" Text="Click to Validate" CssClass="btn btn-primary pull-right btn-sm" OnClick="btnValidateInfo_OnClick" />&nbsp;&nbsp;

                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="txtValidate" CssClass="form-control" Enabled="False" /> &nbsp;&nbsp;
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnFinalSubmit" runat="server" Text="Final Submission" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnFinalSubmit_OnClick" Enabled="False" />
                                </div>
                            </div>
                        </div>
                        </div>
                    </div>
                </div>
                </div>
            </section>

            <script type="text/javascript">
                //Function to allow only numbers to textbox
                function validate4N(key) {
                    //getting key code of pressed key
                    var keycode = (key.which) ? key.which : key.keyCode;
                    var phn = document.getElementById('txtPhoneNo');
                    //comparing pressed keycodes
                    if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                        return false;
                    } else {
                        //Condition to check textbox contains ten numbers or not
                        if (id.value.length < 15) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                }

                function validateID(key) {
                    //getting key code of pressed key
                    var keycode = (key.which) ? key.which : key.keyCode;
                    var id = document.getElementById('txtIDNo');
                    //comparing pressed keycodes
                    if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                        return false;
                    } else {
                        //Condition to check textbox contains ten numbers or not
                        if (id.value.length < 15) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                }
            </script>

            <script type="text/javascript">
                $(function() {
                    $('#TextBoxcost').on('onkeyup', function () {
                        var x = $('#TextBoxcost').val();
                        $('#TextBoxcont').val(0.5 * (x));
                        
                    });
                });

                function addCommas(x) {
                    var parts = x.toString().split(".");
                    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    return parts.join(".");
                }

                function openModalSubmit() {
                    $('#pageApplications').modal('show');
                }

                function Check(textBox, maxLength) {
                    document.getElementById("lblCharLeft").innerHTML = maxLength - textBox.value.length + " characters left";
                    if (textBox.value.length > maxLength) {
                        document.getElementById("lblCharLeft").style.color = "red";
                        textBox.value = textBox.value.substr(0, maxLength);
                        document.getElementById("lblCharLeft").innerHTML = maxLength - textBox.value.length + " characters left";
                    }
                    else if (textBox.value.length < maxLength) {
                        document.getElementById("lblCharLeft").style.color = "Black";
                    }
                    else {
                        document.getElementById("lblCharLeft").style.color = "red";
                    }

                }
                function getKCDFContribution()
                {
                    var _txt1 = document.getElementById('<%= TextBoxcost.ClientID %>').value;
                    var _txt2 = document.getElementById('<%= TextBoxcont.ClientID %>').value;

                    var _minused = _txt1 - _txt2;
                       
                    if (!isNaN(_txt1) && !isNaN(_txt2)) {
                        document.getElementById('<%= TextBoxrequested.ClientID %>').value = _minused;
                    } else if (_minused<1) {
                        document.getElementById("lblCharLeft").style.color = "red";
                        document.getElementById("lblCharLeft").innerHTML = "Invalid Entry!, Click Refresh";
                        document.getElementById('<%= TextBoxrequested.ClientID %>').value = "";
                        document.getElementById('<%= TextBoxrequested.ClientID %>').style.color = "red";
                        document.getElementById('<%=btnRefreshScript.ClientID%>').disabled = false;
                    }
                }

                function ValidatemeSpecialCase(textBox) {
                    var maxLength = document.getElementById('<%= TextBoxcost.ClientID %>').value.length;
                    document.getElementById("lblCharLeft").innerHTML = maxLength - textBox.value.length + " characters left";
                    var subs = maxLength - textBox.value.length;
                    if (subs === 0) {
                        document.getElementById("lblCharLeft").innerHTML = "Input error!";
                        document.getElementById("lblCharLeft").style.color = "red";
                        document.getElementById('<%= TextBoxrequested.ClientID %>').disabled = true;
                    }
                    if (textBox.value.length > maxLength) {
                        document.getElementById("lblCharLeft").style.color = "red";
                        textBox.value = textBox.value.substr(0, maxLength);
                        document.getElementById("lblCharLeft").innerHTML = maxLength - textBox.value.length + " characters left";
                    }
                    else if (textBox.value.length < maxLength) {
                        document.getElementById("lblCharLeft").style.color = "Black";
                    }
                    else {
                        document.getElementById("lblCharLeft").style.color = "red";
                    }
                    getKCDFContribution();
                }

                function CheckView2(textBox, maxLength) {
                    document.getElementById("lblCharLeft2").innerHTML = maxLength - textBox.value.length + " characters left";
                    if (textBox.value.length > maxLength) {
                        document.getElementById("lblCharLeft2").style.color = "red";
                        textBox.value = textBox.value.substr(0, maxLength);
                        document.getElementById("lblCharLeft2").innerHTML = maxLength - textBox.value.length + " characters left";
                    }
                    else if (textBox.value.length < maxLength) {
                        document.getElementById("lblCharLeft2").style.color = "Black";
                    }
                    else {
                        document.getElementById("lblCharLeft2").style.color = "red";
                    }
                }

                function CheckView3(textBox, maxLength) {
                    document.getElementById("lblCharLeft3").innerHTML = maxLength - textBox.value.length + " characters left";
                    if (textBox.value.length > maxLength) {
                        document.getElementById("lblCharLeft3").style.color = "red";
                        textBox.value = textBox.value.substr(0, maxLength);
                        document.getElementById("lblCharLeft3").innerHTML = maxLength - textBox.value.length + " characters left";
                    }
                    else if (textBox.value.length < maxLength) {
                        document.getElementById("lblCharLeft3").style.color = "Black";
                    }
                    else {
                        document.getElementById("lblCharLeft3").style.color = "red";
                    }
                }

                function CheckView4(textBox, maxLength) {
                    document.getElementById("lblCharLeft4").innerHTML = maxLength - textBox.value.length + " characters left";
                    if (textBox.value.length > maxLength) {
                        document.getElementById("lblCharLeft4").style.color = "red";
                        textBox.value = textBox.value.substr(0, maxLength);
                        document.getElementById("lblCharLeft4").innerHTML = maxLength - textBox.value.length + " characters left";
                    }
                    else if (textBox.value.length < maxLength) {
                        document.getElementById("lblCharLeft4").style.color = "Black";
                    }
                    else {
                        document.getElementById("lblCharLeft4").style.color = "red";
                    }
                }
            </script>
            
            <script>
                jQuery.fn.getNum = function () {
                    var val = $.trim($(this).val());
                    if (val.indexOf(',') > -1) {
                        val = val.replace(',', '.');
                    }
                    var num = parseFloat(val);
                    var num = num.toFixed(2);
                    if (isNaN(num)) {
                        num = '';
                    }
                    return num;
                }

                $(function() {

                    $('<%= TextBoxcost.ClientID %>').blur(function () {
                        $(this).val($(this).getNum());
                    });
                });

                function ConfirmDelete() {
                    var Delet_Confirm = confirm("Do you really want to delete this record ?");
                    if (Delet_Confirm == true) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
              
            </script>
            
   <script type="text/javascript" >
      function IfYesIts() {
           var ifYsId = document.getElementById("idKCDFyes");
           ifYsId.style.display = "block";
      }
    </script> 
           
        </div>
    </asp:Content>