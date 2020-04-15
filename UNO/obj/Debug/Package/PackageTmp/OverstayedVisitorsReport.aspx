<%@ Page Language="C#" MasterPageFile="~/ModuleMain.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="OverstayedVisitorsReport.aspx.cs" Inherits="UNO.OverstayedVisitorsReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellspacing="0" class="MenuTable">
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <h1 class="heading" style='text-align: center; font-family: tahoma; font-size: x-large;'>
        Overstayed Visitors
    </h1>
    <div align="center">
        <asp:Panel runat="server" ID="HeadPanel">
            <table class="TableClass" style="width: 70%; height: 75px; margin-left: 4%;">
                <tr>
                    <td class="tdStyle">
                        <table width="20%">
                            <tr>
                                <td colspan="2" style="height: 26px; text-align: left">
                                    <asp:Label ID="Label1" runat="server" Text="From Date:"></asp:Label>
                                    <asp:TextBox ID="txtFromDate" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                        ValidationGroup="ADD" Height="19px" Width="96px" onkeyPress="javascript: return false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFVFromDate" runat="server" ControlToValidate="txtFromDate"
                                        ErrorMessage="Plz. enter From Date." ForeColor="Red" ValidationGroup="ADD" Display="None"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                        TargetControlID="RFVFromDate" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                        PopupButtonID="txtFromDate" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdStyle">
                        <table width="20%">
                            <tr>
                                <td colspan="2" style="height: 26px; text-align: left">
                                    <asp:Label ID="Label2" runat="server" Text="To Date:"></asp:Label>
                                    <asp:TextBox ID="txtToDate" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                        ValidationGroup="ADD" MaxLength="5" Height="16px" Width="96px" onkeyPress="javascript: return false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RfVtxtToDate" runat="server" ControlToValidate="txtToDate"
                                        ErrorMessage="Plz. enter To date." ForeColor="Red" ValidationGroup="ADD" Display="None"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                        TargetControlID="RfVtxtToDate" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                        PopupButtonID="txtToDate" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdStyle">
                        <table width="20%">
                            <tr>
                                <td colspan="2" style="height: 26px; text-align: left">
                                    <asp:Label ID="Label3" runat="server" Text="Time:"></asp:Label>
                                    <asp:TextBox ID="txtTime" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                        ValidationGroup="ADD" MaxLength="5" Height="16px" Width="96px" onkeyup="fnColon(this,event)"
                                        onkeypress="findspace(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFVtxtTime" runat="server" ControlToValidate="txtTime"
                                        ErrorMessage="Please enter time." ForeColor="Red" ValidationGroup="ADD" Display="None"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCEtxtTime" runat="server" TargetControlID="RFVtxtTime"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="revtxtTime" runat="server" ControlToValidate="txtTime"
                                        ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="ADD"></asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtTime" runat="server" TargetControlID="revtxtTime"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdStyle" align="center">
                        <table style="height: 30px; width: 123px;">
                            <tr>
                                <td>
                                    <asp:Button ID="btnView" class="ButtonControl" runat="server" Text="View" Style="width: 60%"
                                        ValidationGroup="ADD" CausesValidation="true" ClientIDMode="Static" OnClick="View_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnReset" runat="server" CssClass="ButtonControl" Text="Reset" Style="width: 60%"
                                        ClientIDMode="Static" OnClientClick="return Reset();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="Button" class="ButtonControl" value="Close" id="Close" style="width: 60%"
                                        onclick="navigateToUrl('Uno_Dashboard.aspx');return false;" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <table align="center" style="width: 90%; margin-left: 39px;">
        <tr>
            <td colspan="5" align="right" style="height: 30px">
                <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="ButtonControl" Visible="False"
                    Width="90px" Height="23px" OnClientClick="navigateToUrl('OverstayedVisitorsReport.aspx');return false;" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="viewer" runat="server" Height="100%" BorderStyle="Solid" BorderColor="#0066FF"
                    BorderWidth="3px" Width="100%" Visible="false" >
                    <%--Style="overflow: auto;"--%>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="380px"
                        Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                        WaitMessageFont-Size="14pt" BorderStyle="None">
                    </rsweb:ReportViewer>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table class="MessageContainerTable" width="98.8%">
        <tr>
            <td colspan="4" align="center" valign="middle">
                <asp:Label ID="MessageLabel" runat="server" CssClass="ErrMessageStyle"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .tdStyle
        {
            border-right: lightsteelblue thin solid;
            border-top: lightsteelblue thin solid;
            border-left: lightsteelblue thin solid;
            border-bottom: lightsteelblue thin solid;
            width: 5%;
            font-weight: bold;
            color: Black;
        }
        .TextControl
        {
        }
    </style>
    <script language="javascript" src="Scripts/calendar.js" type="text/javascript"></script>
    <!--In Progress UI CSS -->
    <link href="ProgressBar/CSS/container.css" rel="stylesheet" type="text/css" />
    <!--In Progress UI Dependencies -->
    <script src="ProgressBar/Scripts/utilities.js" type="text/javascript"></script>
    <script src="ProgressBar/Scripts/container-min.js" type="text/javascript"></script>
    <script src="ProgressBar/Scripts/InProgress.js" type="text/javascript"></script>
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function Reset() {

            var yyyy = new Date().getFullYear().toString();
            var mm = (new Date().getMonth() + 1).toString();
            var dd = new Date().getDate().toString();
            var Month = (mm[1] ? mm : "0" + mm[0]);
            var ToDate = (dd[1] ? dd : "0" + dd[0]);

            $('#' + ["<%=txtFromDate.ClientID%>"].join(', #')).prop('value', "01" + "/" + Month + "/" + yyyy);
            $('#' + ["<%=txtToDate.ClientID%>"].join(', #')).prop('value', ToDate + "/" + Month + "/" + yyyy);
            $('#' + ["<%=txtTime.ClientID%>"].join(', #')).prop('value', "17:00");
            return false;
        }
     
     
    </script>
</asp:Content>
