<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="AccessControlManualBackUp.aspx.cs" Inherits="UNO.AccessControlManualBackUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .cssVEh
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <%--<link href="Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        $(function () {
            $("[id*=btnSubmit]").click(function () {
                //var obj = {};
                //obj.TillDate = $.trim($("[id*=txtTillDate]").val());
                //obj.age = $.trim($("[id*=txtAge]").val());
                $.ajax({
                    type: "POST",
                    url: "AccessControlManualBackUp.aspx/AccessControlArchival",
                    data: '{TillDate: "' + $("#<%=txtTillDate.ClientID%>").value + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        alert(r.d);
                    }
                });
                return false;
            });
        });
         
    </script>
    
    <div id="div1" style="display: none; height: 40px; width: 40px;position:absolute; top:40%;left:40%;">
                                        <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                                    </div>
    <table id="table2" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
        class="TableClass">
        <tr>
            <td colspan="2" align="center" class="style23">
                <h3 class="heading">
                    Access Control Manual Backup Process</h3>
            </td>
        </tr>
        <tr>
           
            <td style="height: 10px;">
             <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 10px;">
            </td>
        </tr>
        <tr>
            <td class="TDClassLabel" style="height: 10px; width: 50%">
                Select Till Date for Archiving:<label class="CompulsaryLabel">*</label>
            </td>
            <td class="TDClassControl" style="height: 10px; width: 50%; margin-left: 40px;">
                <asp:TextBox ID="txtTillDate" runat="server" ClientIDMode="Static" MaxLength="10"
                    CssClass="TextControl" onKeyPress="javascript: return false"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calFrmDate" runat="server" TargetControlID="txtTillDate"
                    PopupButtonID="DOB" Format="dd/MM/yyyy" Enabled="True">
                </ajaxToolkit:CalendarExtender>
                <br />
                <asp:RequiredFieldValidator ID="rfvTillDate" runat="server" ControlToValidate="txtTillDate"
                    Display="None" ErrorMessage="Please enter Till Date for Archiving" SetFocusOnError="True"
                    ForeColor="Red" ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                    TargetControlID="rfvTillDate" Enabled="True">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td style="height: 10px;">
            </td>
        </tr>
        <tr>
            <td style="height: 10px;">
            </td>
        </tr>
        <tr>
            <td align="center" class="style23">
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="ButtonControl" Text="Submit" 
                    ValidationGroup="validateofficial" />
            </td>
        </tr>
        <tr>
            <td align="center" class="style23">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
