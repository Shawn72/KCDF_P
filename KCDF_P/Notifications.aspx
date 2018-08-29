<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="KCDF_P.Notifications" MasterPageFile="~/Gran_Master.Master" %>

<asp:Content ID="notifiedContent" ContentPlaceHolderID="MainContent" runat="server">
   <div class="panel-body" style="font-family:Trebuchet MS">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="row">
                <div class="col-md-12">
                    <h4 style="align-content:center; font-family:Trebuchet MS; color:#0094ff">My Notification Panel</h4><br /></div>
            </div>
        <div class="form-horizontal">
            
                 <header class="panel-heading tab-bg-info">
                <asp:Menu ID="myNotificationTabs" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server" OnMenuItemClick="myNotificationTabs_OnMenuItemClick">
                    <Items>
                        <asp:MenuItem Text="Pending Tasks |" Value="0" runat="server" />
                        <asp:MenuItem Text="Completed Tasks |" Value="1" runat="server" />
                    </Items>
                </asp:Menu>
            </header>
        <asp:Label ID="lbError" runat="server" ForeColor="#FF3300" CssClass="text-left hidden"></asp:Label>
        <section class="panel">

            <div class="panel panel-primary">
                <div class="col-md-3"></div>
                <div class="col-md-6">
                    <asp:Label ID="lblError" runat="server" ForeColor="#FF3300" CssClass="text-left hidden"></asp:Label>
                    <span class="text-center text-danger"><small><%=lblError.Text %></small></span>
                </div>
                <div class="col-md-3"></div>
                <br/>
                <asp:MultiView ID="notifyPMultiview" runat="server" ActiveViewIndex="0">
                
                    <asp:View runat="server" ID="allNotications">
                         <div class="form-horizontal">
                            <br/>
                             <div class="row">
                                <div class="col-md-12">
                                    <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger"></span>My Notifications</label> 
                                </div>
                                <br /> 
                                 <div class="col-md-12">
                                    <asp:GridView ID="tblPendingTasks" runat="server" 
                                        CssClass="table table-condensed table-responsive table-bordered footable" 
                                        Width="100%" AutoGenerateSelectButton="false" EmptyDataText="No Tasks Found!" 
                                        AlternatingRowStyle-BackColor="#C2D69B" DataKeyNames="Entry_No"  AllowSorting="True">
                                        <Columns>
                                            <asp:BoundField DataField="Entry_No" HeaderText="S/No:" />
                                            <asp:BoundField DataField="Project" HeaderText="Project Name" />
                                            <asp:BoundField DataField="Condition" HeaderText="Task" />
                                            <asp:BoundField DataField="Due_Date" HeaderText="Due Date:" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="Condition_FullFilled" HeaderText="Task Completed" />
                                              <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkReact" Text="Act on Task" CommandArgument='<%# Eval("Entry_No") %>' CommandName="lnkReact" runat="server" OnClick="lnkReact_OnClick"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <SelectedRowStyle BackColor="#259EFF" BorderColor="#FF9966" />
                                    </asp:GridView> 
                                 </div> 
                                  
                             </div>
                            

                         </div>
                     </asp:View>
                    
                    <asp:View runat="server" ID="pendingTasksView">
                        <div class="form-horizontal">
                           <div class="row">
                                <div class="col-md-12">
                                    <label class="form-control alert alert-info" style="font-weight: bold;"><span class="badge alert-danger"></span>Completed Tasks</label> 
                                </div>
                                    <br />  
                               <div class="col-md-12">
                                   <asp:GridView ID="tblCompletedTasks" runat="server" 
                                        CssClass="table table-condensed table-responsive table-bordered footable" 
                                        Width="100%" AutoGenerateSelectButton="false" EmptyDataText="No Completed Tasks Found!" 
                                        AlternatingRowStyle-BackColor="#C2D69B" DataKeyNames="Entry_No"  AllowSorting="True">
                                        <Columns>
                                            <asp:BoundField DataField="Entry_No" HeaderText="S/No:" />
                                            <asp:BoundField DataField="Project" HeaderText="Project Name" />
                                            <asp:BoundField DataField="Condition" HeaderText="Task" />
                                            <asp:BoundField DataField="Due_Date" HeaderText="Due Date:" DataFormatString="{0:dd/MM/yyyy}"/>
                                            <asp:BoundField DataField="Condition_FullFilled" HeaderText="Task Completed" />
                                        </Columns>
                                        <SelectedRowStyle BackColor="#259EFF" BorderColor="#FF9966" />
                                    </asp:GridView>

                                   </div>
                               
                            </div> 
                        </div>

                    </asp:View>

                 </asp:MultiView>
                
            </div>

         </section>
       </div>
     </div>
</asp:Content>
