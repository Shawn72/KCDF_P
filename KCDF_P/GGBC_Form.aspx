<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GGBC_Form.aspx.cs" MasterPageFile="Site.Master" Inherits="KCDF_P.GGBC_Form" %>
<asp:Content ID="newLoanApplocation" ContentPlaceHolderID="MainContent" runat="server">
    <div id="lblLoanApplicationsr" class="alert tab-bg-info fade in">
        <h3  style="font-weight:bold; color:#fff; text-align:center;">KCDF Data Collection Tool</h3>
    </div>
    <section class="panel">
    <div class="panel panel-primary">  
        <header class="panel-heading tab-bg-info">          
         <asp:Menu ID="LoanAppMenu" Orientation="Horizontal"  StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server" OnMenuItemClick="LoanAppMenu_OnMenuItemClick">
            <Items>              
                <asp:MenuItem Text="GGBC Data" Value="0" runat="server"/>                 
            </Items>
        </asp:Menu>
        </header>
            <div class="panel-body">
                <div class="tab-content">
                <div class="row">
                    <asp:Label ID="lblErrMsg" runat="server" CssClass="text-left hidden"></asp:Label> 
                    <span class="text-center form-control text-danger"><small><%=lblErrMsg.Text %></small></span> 
                 </div>
                 <br/>                    
                    <asp:MultiView ID="loansMultiview" runat="server" ActiveViewIndex="0">                    
                      
                            <%-- REGISTRATION FORM --%>
                            <asp:View runat="server" ID="salaryDView">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="control-label form-control text-danger" style="font-weight: bold;"><span class="badge badge-danger">1</span>  Basic Information:</label> 
                                    </div>
                                    <br />                                    
                                    <div class="col-md-6">
                                 <div class="form-group">
                                        <asp:Label ID="lblstudentname" runat="server"  CssClass="control-label label">Student Name:</asp:Label>
                                       
                                            <asp:TextBox ID="Txtstudentname" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:TextBox>
                                        </div>
                        
                                    <div class="form-group">
                                    <asp:Label ID="Lblemailaddress" runat="server"  CssClass="control-label label">Email Address:</asp:Label>
                                        <asp:TextBox runat="server" ID="Txtemailaddress" CssClass="form-control" onkeypress="return allowOnlyNumber(event);" />   
                                     </div>
                        
                                    <div class="form-group">
                                        <asp:Label ID="Lblehomestaedlocation" runat="server"  CssClass="control-label label">Homestead Location:</asp:Label>
                                            <asp:TextBox runat="server" ID="Txthomesteadlocation" CssClass="form-control" onkeypress="return allowOnlyNumber(event);" />   
                                             </div>
                        
                                    <div class="form-group">
                                        <asp:Label ID="Lblsubcounty" runat="server"  CssClass="control-label label">Sub-county:</asp:Label>
                                            <asp:TextBox runat="server" ID="Txtsubcounty" CssClass="form-control" onkeypress="return allowOnlyNumber(event);" /> 
                                        </div>
                                     </div>
                                
                                    <div class="form-group">
                                        <asp:Label ID="lblnearestsecodary" runat="server"  CssClass="control-label label">Nearest Secondary/ primary School:</asp:Label>
                                        
                                            <asp:TextBox runat="server" ID="Txtnearestshl" CssClass="form-control" />              
                                            
                                    </div>
                        
                                    <div class="form-group">
                                        <asp:Label ID="lblnationalID" runat="server"  CssClass="control-label label">national ID No.:</asp:Label>
                                         <asp:TextBox runat="server" ID="IntboxnationalID" CssClass="form-control" />              
                                        </div> 
                        
                                    <div class="form-group">
                                        <asp:Label ID="Lblphonecontacts" runat="server"  CssClass="control-label label">Phone Contacts:</asp:Label>
                                        <asp:TextBox runat="server" ID="txtBoxphonecontacts" CssClass="form-control"/>              
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Lblcounty" runat="server"  CssClass="control-label label">County:</asp:Label>
                                        <asp:TextBox runat="server" ID="TextBoxcounty" CssClass="form-control"/>              
                                    </div>
                                                                       
                                    <div class="form-group">
                                        <asp:Label ID="Lblhighschoolname" runat="server"  CssClass="control-label label">High School Name:</asp:Label>
                                        <asp:TextBox runat="server" ID="TextBoxhighschoolname" CssClass="form-control"/>              
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Lbluniversityname" runat="server"  CssClass="control-label label">University Name:</asp:Label>
                                        <asp:TextBox runat="server" ID="TextBoxuniversityname" CssClass="form-control"/>              
                                    </div>
                                </div>
                                 <div class="form-group">
                                        <asp:Label ID="Lblyearofcompletion" runat="server"  CssClass="control-label label">Year Of Completion:</asp:Label>
                                        <asp:TextBox runat="server" ID="TextBoxyearofcompltion" CssClass="form-control"/>              
                                    </div>
                                <div class="form-group">
                                        <asp:Label ID="Lblcoursename" runat="server"  CssClass="control-label label">Course Name:</asp:Label>
                                        <asp:TextBox runat="server" ID="TextBoxcoursename" CssClass="form-control" onkeypress="return allowOnlyNumber(event);"/>              
                                    </div>
                                <div class="form-group">
                                        <asp:Label ID="Lblyearandmonthofgraduation" runat="server"  CssClass="control-label label">Year And Month Of Graduation:</asp:Label>
                                        <asp:TextBox runat="server" ID="yearandmonthofgraduation" CssClass="form-control" onkeypress="return allowOnlyNumber(event);"/>              
                                    </div>
                                <div class="form-group">
                                        <asp:Label ID="Lblexpectedgraduationyear" runat="server"  CssClass="control-label label">Expected Graduation Year:</asp:Label>
                                        <asp:TextBox runat="server" ID="TextBoxexpectedgraduationyear" CssClass="form-control" onkeypress="return allowOnlyNumber(event);"/>              
                                    </div>
                                <br/>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="control-label form-control text-danger" style="font-weight: bold;"><span class="badge badge-danger">2</span> Scholarship Details:</label> 
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="lbllgrad" runat="server"  CssClass="control-label label">GGBC Graduate category:</asp:Label>
                                         <div class="form-group">
                                         <asp:CheckBox ID="chkboxAI" runat="server"  CssClass="form-control" Text="2014"/>
                                     </div>
                                         <div class="form-group">
                                           <asp:CheckBox ID="chkboxATEBank" runat="server" CssClass="form-control" Text="2015" />
                                         </div>  
                                                
                                         <div class="form-group">
                                           <asp:CheckBox ID="chkboxExpected" runat="server" CssClass="form-control" Text="Expected" />
                                         </div>  
                                                   
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server"  CssClass="control-label label">GGBC scolarship category:</asp:Label>
                                         <div class="form-group">
                                         <asp:CheckBox ID="chkboxAnglinv" runat="server"  CssClass="form-control" Text="Angel Investor"/>
                                     </div>

                                         <div class="form-group">
                                           <asp:CheckBox ID="chkboxatebnk" runat="server" CssClass="form-control" Text="ATE Bank" />
                                         </div>  
                                                
                                         <div class="form-group">
                                           <asp:CheckBox ID="chkboxcti" runat="server" CssClass="form-control" Text="Citi" />
                                         </div>
                                            
                                        <div class="form-group">
                                           <asp:CheckBox ID="chkboxdelltte" runat="server" CssClass="form-control" Text="Deloitte" />
                                         </div>

                                          <div class="form-group">
                                           <asp:CheckBox ID="chkboxEqtbnk" runat="server" CssClass="form-control" Text="Equity Bank" />
                                         </div>

                                          <div class="form-group">
                                           <asp:CheckBox ID="chkboxgnmtr" runat="server" CssClass="form-control" Text="General Motors" />
                                         </div>

                                          <div class="form-group">
                                           <asp:CheckBox ID="chkboxkpmg" runat="server" CssClass="form-control" Text="KPMG" />
                                         </div> 

                                          <div class="form-group">
                                           <asp:CheckBox ID="chkboxknylw" runat="server" CssClass="form-control" Text="Kenya law" />
                                         </div>
           
                                          <div class="form-group">
                                           <asp:CheckBox ID="chkboxmstrcrdfnd" runat="server" CssClass="form-control" Text="Master Card Foundation" />
                                         </div>

                                          <div class="form-group">
                                           <asp:CheckBox ID="chkboxmsnt" runat="server" CssClass="form-control" Text="Monsanto" />
                                         </div>
                                        
                                          <div class="form-group">
                                           <asp:CheckBox ID="chkboxstdchbnk" runat="server" CssClass="form-control" Text="Standard Chartered Bank" />
                                         </div>
                                      
                                          <div class="form-group">
                                           <asp:CheckBox ID="chkboxsgpm" runat="server" CssClass="form-control" Text="Surgipharm" />
                                         </div>  
                                             
                                          <div class="form-group">
                                           <asp:CheckBox ID="chkboxusaid" runat="server" CssClass="form-control" Text="USAID" />
                                         </div>
                                               
                                          <div class="form-group">
                                           <asp:CheckBox ID="chkboxzepre" runat="server" CssClass="form-control" Text="ZEP-Re" />
                                         </div>

                                          <div class="form-group">
                                           <asp:CheckBox ID="chkboxothr" runat="server" CssClass="form-control" Text="Other" />
                                         </div>
                                    </div>
                                                                        
                                    <div class="form-group">
                                    <div class="col-md-12">
                                          <div class="row">
                                               <asp:Label ID="Label2" runat="server"  CssClass="control-label label">Have you access any of the following programs meant to improve employment skills?:</asp:Label>
                                       
                                         </div>
                                    </div>
                                    <div class="row"></div><br />
                                    <div class="row">
                                        <div class="col-md-4">                                         
                                           <div class="form-group">
                                                <asp:Label ID="Label3" runat="server"  CssClass="control-label label">Program:</asp:Label>
                                           </div>
                                           <div class="form-group">
                                         <asp:CheckBox ID="Chkboxictt" runat="server"  CssClass="form-control" Text="ICT Training"/>
                                          </div>
                                         <div class="form-group">
                                           <asp:CheckBox ID="chkboxattchmt" runat="server" CssClass="form-control" Text="Attachment" />
                                         </div>        
                                         <div class="form-group">
                                           <asp:CheckBox ID="Chkboxwrtrn" runat="server" CssClass="form-control" Text="Work Readiness Training" />
                                         </div>
                                             <div class="form-group">
                                           <asp:CheckBox ID="Chkboxentt" runat="server" CssClass="form-control" Text="Entrepreneurship Training" />
                                         </div>
                                             <div class="form-group">
                                           <asp:CheckBox ID="Chkboxfltg" runat="server" CssClass="form-control" Text="Financial Literacy Training" />
                                         </div>
                                             <div class="form-group">
                                           <asp:CheckBox ID="ckboxother" runat="server" CssClass="form-control" Text="Others" />
                                         </div>
                                        </div>

                                        <div class="col-md-8">
                                               <div class="form-group">
                                                <asp:Label ID="Label4" runat="server"  CssClass="control-label label">Specify Year:</asp:Label>
                                           </div>
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="chckbox2012" runat="server"  CssClass="form-control" Text="2012"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="chckbox2013" runat="server"  CssClass="form-control" Text="2013"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="chckbox2014" runat="server"  CssClass="form-control" Text="2014"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="chckbox2015" runat="server"  CssClass="form-control" Text="2015"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="chckbox_other" runat="server"  CssClass="form-control" Text="Other"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox2012_" runat="server"  CssClass="form-control" Text="2012"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox2013_" runat="server"  CssClass="form-control" Text="2013"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox2014_" runat="server"  CssClass="form-control" Text="2014"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox2015_" runat="server"  CssClass="form-control" Text="2015"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkboxother_" runat="server"  CssClass="form-control" Text="Other"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                             <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox12" runat="server"  CssClass="form-control" Text="2012"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox13" runat="server"  CssClass="form-control" Text="2013"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox14" runat="server"  CssClass="form-control" Text="2014"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox15" runat="server"  CssClass="form-control" Text="2015"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkboxother2" runat="server"  CssClass="form-control" Text="Other"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row"> <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox2012" runat="server"  CssClass="form-control" Text="2012"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox2013" runat="server"  CssClass="form-control" Text="2013"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox2014" runat="server"  CssClass="form-control" Text="2014"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox2015" runat="server"  CssClass="form-control" Text="2015"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkboxother" runat="server"  CssClass="form-control" Text="Other"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                             <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbx2012" runat="server"  CssClass="form-control" Text="2012"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chekbx2013" runat="server"  CssClass="form-control" Text="2013"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbx2014" runat="server"  CssClass="form-control" Text="2014"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbx2015" runat="server"  CssClass="form-control" Text="2015"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbxothr" runat="server"  CssClass="form-control" Text="Other"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                             <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox212" runat="server"  CssClass="form-control" Text="2012"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox213" runat="server"  CssClass="form-control" Text="2013"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox214" runat="server"  CssClass="form-control" Text="2014"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkbox215" runat="server"  CssClass="form-control" Text="2015"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="Chkboxoher" runat="server"  CssClass="form-control" Text="Other"/>
                                                </div>
                                             </div>
                                          </div>        
                                    </div>
                                     <%--  new row--%>
                                      <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <asp:Label ID="Label5" runat="server"  CssClass="control-label label">Have you access the following government facilities?:</asp:Label>
                                            </div>
                                         </div>
                                     </div>  
                                     
                                     <br />
                                     <br />
                                     <br/>
                                 </div>
                                    <br/>
                                    <br/>
                                </div>
                         </div> 
                        </asp:View>
                        </asp:MultiView>
                    </div>
                    </div>  
                                                                
                                
       
        </div>      
     </section>
        <script>
            function allowOnlyNumber(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            }
        </script>    
</asp:Content>
