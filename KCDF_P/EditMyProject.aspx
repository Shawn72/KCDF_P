<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditMyProject.aspx.cs" Inherits="KCDF_P.EditMyProject" MasterPageFile="~/Account/AddGranteeProfile_Master.Master" %>
<asp:Content ID="EditProfileContent" ContentPlaceHolderID="MainContent" runat="server">
<%--<meta http-equiv="refresh" content="200;url=Add_Grantee_Profile.aspx"> --%>
<div class="panel-body" style="font-family:Trebuchet MS">
    <div class="col-md-12">
        <h4 style="align-content:center; font-family:Trebuchet MS; color:#0094ff">Edit Your Project Details</h4><br />
    </div>
    <br/>
<div class="row">
    <strong>Edit Project :</strong><asp:Label runat="server" ID="lblProjNo">&nbsp;:</asp:Label>
&nbsp;&nbsp;<strong><asp:Label runat="server" ID="lblPrjNm"></asp:Label></strong>
&nbsp;&nbsp;<strong><asp:Label runat="server" ID="lblRefNo"></asp:Label></strong>
</div>
<br/>
   
 <section class="panel">
 <div class="panel panel-primary">
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
                            
    <%--  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-3 control-label">County:</asp:Label>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlmycountyIS" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="True" 
                        OnSelectedIndexChanged="ddlmycountyIS_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    </div>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlmycountyIS" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>--%>


    <div class="form-group">
        <asp:Label runat="server" CssClass="col-md-3 control-label">Constituency:</asp:Label>
        <div class="col-md-6">
            <asp:DropDownList ID="ddlConstituency" runat="server" class="selectpicker form-control" data-live-search-style="begins" data-live-search="true" AppendDataBoundItems="False" Enabled="False"></asp:DropDownList>
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
        <%--<div class="col-md-3" >
            <asp:Button runat="server" ID="btnRefreshScript" Enabled="False" CssClass="btn btn-primary pull-left btn-sm" Text="Refresh" OnClick="btnRefreshScript_OnClick"/>
        </div>--%>
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
                <asp:Button ID="btnUpdatePOverview" runat="server" Text="Update Project Overview" CssClass="btn btn-primary pull-left btn-sm" OnClick="btnProjEdit_OnClick" Enabled="True"/>
            </div>
        </div>
        <div class="col-md-3"></div>
    </div>
    <br/>

</div>
   </div>
</section>
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
                <%--document.getElementById('<%=btnRefreshScript.ClientID%>').disabled = false;--%>
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