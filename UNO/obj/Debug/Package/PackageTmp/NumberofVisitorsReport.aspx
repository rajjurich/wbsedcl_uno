<%@ Page Language="C#" MasterPageFile="~/ModuleMain.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="NumberofVisitorsReport.aspx.cs" Inherits="UNO.NumberofVisitorsReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            $("select" + "#" + "<%=ddlVisitorType.ClientID%>").prop('selectedIndex', 0);
            $("select" + "#" + "<%=ddlNationality.ClientID%>").prop('selectedIndex', 0);     
         
            $('[id *= "<%=chkOptionalColumns.ClientID%>"]').find('input[type="checkbox"]').each(function () {
                $(this).attr("checked", false);
            });
            return false;
        }

        
    </script>
    <table cellspacing="0" class="MenuTable">
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <h1 class="heading" style='text-align: center; font-family: tahoma; font-size: x-large;'>
        Number of Visitors</h1>
    <div align="center">
        <asp:Panel runat="server" ID="HeadPanel">
            <table class="TableClass" style="width: 90%; height: 75px;">
                <tr>
                    <td class="tdStyle">
                        <table width="40%">
                            <tr>
                                <td colspan="2" style="height: 26px; text-align: left">
                                    <asp:Label ID="Label4" runat="server" Text="Visitor Type:"></asp:Label>
                                    <asp:DropDownList ID="ddlVisitorType" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdStyle">
                        <table width="40%">
                            <tr>
                                <td colspan="2" style="height: 26px; text-align: left">
                                    <asp:Label ID="Label3" runat="server" Text="Nationality:"></asp:Label>
                                    <asp:DropDownList ID="ddlNationality" runat="server">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem>Indian</asp:ListItem>
                                        <asp:ListItem>Other</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdStyle">
                        <table width="40%">
                            <tr>
                                <td colspan="2" style="height: 26px; text-align: left">
                                    <asp:Label ID="Label1" runat="server" Text="From Date:"></asp:Label>
                                    <asp:TextBox ID="txtFromDate" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                        ValidationGroup="ADD" Height="19px" Width="96px" onkeyPress="javascript: return false"></asp:TextBox>
                                  
                                    <%--<ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                        Enabled="true" TargetControlID="txtFromDate" WatermarkCssClass="watermarked"
                                        WatermarkText="-- Select Date --">
                                    </ajaxToolkit:TextBoxWatermarkExtender>--%>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                        PopupButtonID="txtFromDate" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdStyle">
                        <table width="40%">
                            <tr>
                                <td colspan="2" style="height: 26px; text-align: left">
                                    <asp:Label ID="Label2" runat="server" Text="To Date:"></asp:Label>
                                    <asp:TextBox ID="txtToDate" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                        ValidationGroup="ADD" MaxLength="5" Height="16px" Width="96px" onkeyPress="javascript: return false"></asp:TextBox>
                                    <%--<ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                        Enabled="true" TargetControlID="txtToDate" WatermarkCssClass="watermarked" WatermarkText="-- Select Date --">
                                    </ajaxToolkit:TextBoxWatermarkExtender>--%>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                        PopupButtonID="txtToDate" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdStyle" align="center">
                        <table style="height: 30px; width: 123px;">
                            <tr>
                                <td>
                                    <asp:Button ID="btnView" class="ButtonControl" runat="server" Text="View" Style="width: 60%"
                                        CausesValidation="true" ClientIDMode="Static" OnClick="View_Click" />
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
            <table width="90%">
                <tr>
                    <td style="border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                        border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                        font-weight: bold; color: Black;" colspan="5">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="height: 26px; text-align: left">
                                    <asp:Label ID="Label5" runat="server" Text="Optional Columns:"></asp:Label>
                                    <asp:CheckBoxList ID="chkOptionalColumns" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                        CssClass="chkOptionalcheckbox">
                                    </asp:CheckBoxList>
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
                    Width="90px" Height="23px" OnClientClick="navigateToUrl('NumberofVisitorsReport.aspx');return false;" />
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <asp:Panel ID="viewer" runat="server" Height="350px" BorderStyle="Solid" BorderColor="#0066FF"
                    BorderWidth="3px" Width="100%" Visible="false">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="350px"
                        Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                        WaitMessageFont-Size="14pt" BorderStyle="None">
                    </rsweb:ReportViewer>
                </asp:Panel>
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
            width: 20%;
            font-weight: bold;
            color: Black;
        }
        .chkOptionalcheckbox input[type="checkbox"]
        {
            margin-right: 2px;
        }
    </style>
</asp:Content>
