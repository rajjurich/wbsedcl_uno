<%@ Page Language="C#" MasterPageFile="~/ModuleMain.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="VisitorAccessMovementReport.aspx.cs" Inherits="UNO.VisitorAccessMovementReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="HeadPanel" ClientIDMode="Static" Width="100%" Style="padding-left: 7%;">
        <h1 class="heading" style='text-align: center; font-family: Tahoma; font-size: x-large;'>
            Visitor access / movement for main gate and second level</h1>
        <table class="TableClass" cellspacing="1" align="center" width="100%" style="padding-top: 1%;">
            <tr>
                <td colspan="7" class="TDClassForButton" style="height: 15px">
                    <asp:Panel runat="server" ID="Panel2">
                        <table class="TableClass" style="width: 100%; height: 25px; padding-left: 3%;">
                            <tr>
                                <td class="tdStyle" style="display: none;">
                                    &nbsp Personnel Type: &nbsp
                                    <asp:DropDownList ID="ddlPersonnelType" runat="server" AutoPostBack="true">
                                        <asp:ListItem Value="E" Text="Employee" />
                                        <asp:ListItem Value="NE" Text="Non Employee" />
                                    </asp:DropDownList>
                                </td>
                                <td style="border: thin solid lightsteelblue; text-align: left; width: 25%; font-weight: bold;
                                    color: Black; height: 75px; width: 9%;">
                                    &nbsp Status : &nbsp
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem Value="3" Text="ALL" />
                                        <asp:ListItem Value="0" Text="Granted" />
                                        <asp:ListItem Value="1" Text="Denied" />
                                    </asp:DropDownList>
                                </td>
                                <td style="border: thin solid lightsteelblue; text-align: left; width: 25%; font-weight: bold;
                                    color: Black; height: 75px; width: 9%;">
                                    &nbsp Level : &nbsp
                                    <asp:DropDownList ID="ddlLevel" runat="server">
                                        <asp:ListItem Value="0" Text="ALL" />
                                        <asp:ListItem Value="P" Text="Peripheral" />
                                        <asp:ListItem Value="S" Text="Second Level" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="TDClassForButton" style="height: 12px">
                    <asp:Panel runat="server" ID="HeadPnl">
                        <table class="TableClass" style="width: 100%; height: 75px; padding-left: 3%;">
                            <tr>
                                <td class="tdStyle">
                                    <table style="height: 75px;">
                                        <tr>
                                            <td>
                                                &nbsp;Employee
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="rdbEmployee" runat="server" RepeatLayout="Flow" CssClass="radiobutton"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="EmployeeHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 75px;">
                                        <tr>
                                            <td>
                                                Centre&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbCompany" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ComapnyHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ComapnyHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 75px;">
                                        <tr>
                                            <td>
                                                &nbsp;Unit
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbLocation" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('LocationHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="LocationHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 75px;">
                                        <tr>
                                            <td>
                                                &nbsp;Entity
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbDivision" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('DivisionHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="DivisionHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 75px;">
                                        <tr>
                                            <td>
                                                &nbsp;Group
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbDepartment" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('DepartmentHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="DepartmentHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 75px; display: none;">
                                        <tr>
                                            <td>
                                                &nbsp;Shift
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbShift" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ShiftHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ShiftHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 75px;">
                                        <tr>
                                            <td>
                                                &nbsp;Division
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbtnGrade" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('GradeHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="GradeHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 75px;">
                                        <tr>
                                            <td>
                                                &nbsp;Section
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbtnGroup" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('GroupHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="GroupHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 75px;">
                                        <tr>
                                            <td>
                                                &nbsp;Reader
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rblReader" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ReaderHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ReaderHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 75px;">
                                        <tr>
                                            <td>
                                                &nbsp;Zone
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rblZone" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ZoneHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ZoneHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 75px; width: 10%;">
                                        <tr>
                                            <td style="text-align: left">
                                                From Date
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtFromDate" onkeyPress="javascript: return false" runat="server"
                                                    ClientIDMode="Static" Height="18px" Width="111px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtfromDate_CalendarExtender1" TargetControlID="txtFromDate"
                                                    PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                To Date
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtToDate" runat="server" onkeyPress="javascript: return false"
                                                    Height="16px" Width="112px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtToDate"
                                                    PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 70px; width: 100px">
                                        <tr>
                                            <td>
                                                <asp:Button ID="View" class="ButtonControl" Style="width: 80%" runat="server" Text="View"
                                                    ClientIDMode="Static" OnClick="View_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button4" runat="server" Width="80%" CssClass="ButtonControl" Text="Reset"
                                                    ClientIDMode="Static" OnClientClick=" return Reset();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="Button" class="ButtonControl" style="width: 80%" value="Close" id="Button5"
                                                    onclick='fnCloseReport()' />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    &nbsp; &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" colspan="7" style="vertical-align: middle">
                    <asp:Panel ID="Panel1" runat="server" ClientIDMode="Static" Style="display: none">
                        <table style="width: 400px">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="HeadLbl" runat="server" Text="" Font-Bold="True" Font-Size="Medium"
                                        ClientIDMode="Static"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtEmployeeSearch" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtCompany" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtLocation" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtDivision" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtDepartment" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtShift" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtGrade" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtGroup" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtReader" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtZone" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="LstTitle" runat="server" BackColor="AliceBlue" BorderStyle="Double"
                                        ClientIDMode="Static" Font-Bold="True" Font-Names="Courier New" ForeColor="Black"
                                        Style="text-align: left; font-weight: bold; font-family: 'Courier New', Monospace;"
                                        Width="100%"></asp:Label>
                                    <asp:ListBox ID="lstEmployee" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple"
                                        Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstCompany" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="5" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstLocation" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="5" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstDivision" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="5" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstDepartment" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="5" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstShift" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="5" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    &nbsp;
                                    <asp:ListBox ID="lstGrade" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="5" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstGroup" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="5" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstReader" runat="server" ClientIDMode="Static" Font-Bold="true"
                                        Font-Name="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple" Style="display: none"
                                        Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstZone" runat="server" ClientIDMode="Static" Font-Bold="true" Font-Name="Courier New"
                                        ForeColor="Black" Rows="5" SelectionMode="Multiple" Style="display: none" Width="100%">
                                    </asp:ListBox>
                                    <asp:ListBox ID="lstEmployeeDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstCompanyDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstLocationDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstDivisionDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstDepartmentDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstShiftDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstGradeDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstGroupDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstReaderDummy" runat="server" ClientIDMode="Static" Font-Bold="true"
                                        Font-Name="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple" Style="display: none"
                                        Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstZoneDummy" runat="server" ClientIDMode="Static" Font-Bold="true"
                                        Font-Name="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple" Style="display: none"
                                        Width="100%"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 37px; vertical-align: bottom; width: 170px;" colspan="2">
                                    <input id="Button1" class="ButtonControl" name="Ok" style="width: 71px" type="button"
                                        value="OK" runat="server" />
                                    &nbsp;&nbsp;
                                    <input id="Button2" class="ButtonControl" name="Cancel" style="width: 71px" type="button"
                                        value="Cancel" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table align="center" style="width: 90%; margin-left: 39px;">
        <tr>
            <td style="text-align: right" class="style28" colspan="5">
                <div align="right" style="width: 90%;">
                    <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="ButtonControl" Visible="False"
                        Width="90px" Height="23px" OnClick="btnClose_Click" />
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <div align="center" style="width: 100%;">
                    <asp:Panel ID="viewer" runat="server" Height="380px" BorderStyle="Solid" BorderColor="#0066FF"
                        BorderWidth="3px" Width="81%" Visible="false">
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="100%"
                            Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" BorderStyle="None">
                        </rsweb:ReportViewer>
                    </asp:Panel>
                </div>
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
            font-weight: bold;
            color: Black;
        }
        .TextControl
        {
        }
    </style>
    <link rel='stylesheet' href='./calendar.css' title='calendar'>
    <script language="javascript" src="Scripts/calendar.js"></script>
    <!--In Progress UI CSS -->
    <link href="ProgressBar/CSS/container.css" rel="stylesheet" type="text/css" />
    <!--In Progress UI Dependencies -->
    <script src="ProgressBar/Scripts/utilities.js" type="text/javascript"></script>
    <script src="ProgressBar/Scripts/container-min.js" type="text/javascript"></script>
    <script src="ProgressBar/Scripts/InProgress.js" type="text/javascript"></script>
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function ResetHdn(str) {
            document.getElementById(str).value = "";

        }

        function fnCloseReport() {
            navigateToUrl('Uno_Dashboard.aspx');
        }


        function Reset() {
            var yyyy = new Date().getFullYear().toString();
            var mm = (new Date().getMonth() + 1).toString();
            var dd = new Date().getDate().toString();
            var Month = (mm[1] ? mm : "0" + mm[0]);
            var ToDate = (dd[1] ? dd : "0" + dd[0]);

            $('#' + ["<%=txtFromDate.ClientID%>"].join(', #')).prop('value', "01" + "/" + Month + "/" + yyyy);
            $('#' + ["<%=txtToDate.ClientID%>"].join(', #')).prop('value', ToDate + "/" + Month + "/" + yyyy);
            $("select" + "#" + "<%=ddlStatus.ClientID%>").prop('selectedIndex', 0);
            $("select" + "#" + "<%=ddlLevel.ClientID%>").prop('selectedIndex', 0);
            $('#' + ['<%=ddlStatus.ClientID%>', '<%=ddlLevel.ClientID%>'].join(', #')).prop('disabled', false);
            $('#' + ['<%=EmployeeHdn.ClientID%>', '<%=ComapnyHdn.ClientID%>', '<%=ShiftHdn.ClientID%>', '<%=GradeHdn.ClientID%>', '<%=GroupHdn.ClientID%>', '<%=LocationHdn.ClientID%>', '<%=DivisionHdn.ClientID%>', '<%=DepartmentHdn.ClientID%>', '<%=ReaderHdn.ClientID%>', '<%=ZoneHdn.ClientID%>'].join(', #')).prop('value', "");
            $('#' + ['rdbEmployee_0', 'rdbComapny_0', 'rdbLocation_0', 'rdbDivision_0', 'rdbDepartment_0', 'rdbShift_0', 'rdbtnGroup_0', 'rdbtnGrade_0', 'rblReader_0', 'rblZone_0'].join(', #')).prop('checked', true);
            $('#' + ['rdbEmployee', 'rdbComapny', 'rdbLocation', 'rdbDivision', 'rdbDepartment', 'rdbShift', 'rdbtnGroup', 'rdbtnGrade', 'rblReader', 'rblZone'].join(', #')).find('input').prop('disabled', false);
            return false;
        }
    
    </script>
</asp:Content>
