<%@ Page Language="C#" MasterPageFile="~/ModuleMain.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="Daily_Arrival_Report.aspx.cs" Inherits="MMWebReports.Daily_Arrival_report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <link rel='stylesheet' href='./calendar.css' title='calendar'>
    <script language="javascript" src="Scripts/calendar.js"></script>
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function ResetHdn(str) {
            document.getElementById(str).value = "";

        }


    </script>
    <script type="text/javascript" language="javascript">

        //Added by Pooja yadav
        function ValidationReport(strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSection, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, txtCalendarFrom) {

            if (document.getElementById(txtCalendarFrom).value == "" || document.getElementById(txtCalendarFrom).value == "-- Select Date --") {
                alert("Please enter Date.");
                document.getElementById(txtCalendarFrom).focus();
                return false
            }


            if (document.getElementById(strEmployee).value == "") {
                document.getElementById(LstEmployee).style.display = "none";
                document.getElementById(RbdEmp + '_0').checked = true;
            }

            if (document.getElementById(strcomany).value == "") {
                document.getElementById(LstCompany).style.display = "none";
                document.getElementById(RbdCompany + '_0').checked = true;
            }

            if (document.getElementById(strlocation).value == "") {
                document.getElementById(LstLocation).style.display = "none";
                document.getElementById(RbdLocation + '_0').checked = true;
            }

            if (document.getElementById(strdivision).value == "") {
                document.getElementById(LstDivision).style.display = "none";
                document.getElementById(RbdDivision + '_0').checked = true;
            }

            if (document.getElementById(strdepartment).value == "") {
                document.getElementById(LstDepartment).style.display = "none";
                document.getElementById(RbdDepartment + '_0').checked = true;
            }

            if (document.getElementById(strShift).value == "") {
                document.getElementById(LstShift).style.display = "none";
                document.getElementById(RbdShift + '_0').checked = true;
            }
            if (strSec != "") {

                if (document.getElementById(strSec).value == "") {
                    document.getElementById(LstSection).style.display = "none";
                    document.getElementById(RbdSection + '_0').checked = true;
                }

            }
            return true;

        }



        function fnCloseReport() {
            navigateToUrl("Uno_Dashboard.aspx");
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
        Daily Arrival Report</h1>
    <asp:Panel runat="server" ID="HeadPanel" ClientIDMode="Static" Width="100%">
        <table class="TableClass" cellspacing="1" align="center" width="100%">
            <tr>
                <td colspan="7" class="TDClassForButton" style="height: 12px">
                    <asp:Panel runat="server" ID="HeadPnl">
                        <%--<table class="TableClass" style="width: 45%; height: 75px"> --%>
                        <table class="TableClass" style="width: 100%; height: 25px; padding-left: 3%; display: none;">
                            <tr>
                                <td style="border: thin solid lightsteelblue; text-align: left; width: 30%; font-weight: bold;"
                                    class="style26">
                                    &nbsp Personnel Type: &nbsp
                                    <asp:DropDownList ID="ddlPersonnelType" runat="server" AutoPostBack="true">
                                        <asp:ListItem Value="E" Text="Employee" />
                                        <asp:ListItem Value="NE" Text="Non Employee" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table class="TableClass" style="width: 100%; height: 75px; margin-left: 4%;">
                            <tr>
                                <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;">
                                    <table style="height: 75px; width: 102px;">
                                        <tr>
                                            <td class="style29">
                                                &nbsp;Employee
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left" class="style29">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow" CssClass="radiobutton"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                    <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="EmployeeHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;"
                                    class="style30">
                                    <table style="height: 75px;">
                                        <tr>
                                            <td>
                                                Company&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ComapnyHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ComapnyHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px;">
                                        <tr>
                                            <td>
                                                Location
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="RadioButtonList3" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('LocationHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="LocationHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px;">
                                        <tr>
                                            <td>
                                                &nbsp;Division
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="RadioButtonList4" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('DivisionHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="DivisionHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px;" id="TABLE2">
                                        <tr>
                                            <td>
                                                Department
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="RadioButtonList5" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('DepartmentHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="DepartmentHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px;" id="TABLE1">
                                        <tr>
                                            <td>
                                                Grade
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbtnGrade" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('GradeHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="GradeHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px; width: 95px;">
                                        <tr>
                                            <td>
                                                &nbsp;Group
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbtnGroup" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('GroupHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="GroupHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px;" id="TABLE3">
                                        <tr>
                                            <td>
                                                &nbsp;Shift
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="RadioButtonList6" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ShiftHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ShiftHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold; vertical-align: top;">
                                    <table style="height: 75px;" id="TABLE5">
                                        <tr>
                                            <td style="vertical-align: bottom; height: 28px">
                                                &nbsp; Sort
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:CheckBox ID="TypeCheckBox" runat="server" Text=" Employee&nbsp;Wise" Width="114px" />
                                                &nbsp; &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table>
                                        <tr>
                                            <td colspan="2" style="height: 26px; text-align: left">
                                                &nbsp; Date
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCalendarFrom" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                                    ValidationGroup="ADD" MaxLength="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvshiftstartdate" runat="server" ControlToValidate="txtCalendarFrom"
                                                    ErrorMessage="Plz. enter shift date." ForeColor="Red" ValidationGroup="ADD" Display="None"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                    TargetControlID="rfvshiftstartdate" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                    Enabled="true" TargetControlID="txtCalendarFrom" WatermarkCssClass="watermarked"
                                                    WatermarkText="-- Select Date --">
                                                </ajaxToolkit:TextBoxWatermarkExtender>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCalendarFrom"
                                                    PopupButtonID="txtCalendarFrom" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <%--<input type="text" name='From_Date' id='txtCalendarFrom' maxlength="10" readonly="readonly" />--%>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="text-align: center; border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                                    border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                                    font-weight: bold;">
                                    <table style="height: 50px">
                                        <tr>
                                            <td>
                                                <asp:Button ID="View" class="ButtonControl" Style="width: 80%" runat="server" Text="View"
                                                    ValidationGroup="ADD" CausesValidation="true" ClientIDMode="Static" OnClick="View_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button4" runat="server" Width="80%" CssClass="ButtonControl" OnClick="Button4_Click"
                                                    Text="Reset" ClientIDMode="Static" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%-- <asp:Button ID="Button5" runat="server" CssClass="ButtonControl" Text="Close" OnClick="Button5_Click" />--%>
                                                <input type="Button" class="ButtonControl" style="width: 80%" value="Close" id="Button5"
                                                    onclick='fnCloseReport()' runat="server" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    &nbsp; &nbsp;&nbsp;
                    <%--	<input type ="Button" class= "ButtonControl" value= "Print" id="Print" onclick='OnPrint("PRINT")' />
                    --%>
                </td>
            </tr>
            <div runat="server" id="d1">
            </div>
            <tr>
                <td align="center" colspan="7" style="vertical-align: middle">
                    <asp:Panel ID="Panel1" runat="server" ClientIDMode="Static" Style="display: none">
                        <table style="width: 400px">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="HeadLbl" runat="server" Text="" Font-Bold="True" Font-Size="Medium"
                                        Width="100%" ClientIDMode="Static"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtEmployeeSearch" runat="server" placeholder="Search" o Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtCompany" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtLocation" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtDivision" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtDepartment" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtShift" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtGrade" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtGroup" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="LstTitle" runat="server" BackColor="AliceBlue" BorderStyle="Double"
                                        ClientIDMode="Static" Font-Bold="True" Font-Names="Courier New" ForeColor="Black"
                                        Style="text-align: left; font-weight: bold; font-family: 'Courier New', Monospace;"
                                        Width="100%"></asp:Label>
                                    <asp:ListBox ID="ListBox1" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox2" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox3" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox4" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox5" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox6" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    &nbsp;
                                    <asp:ListBox ID="lstGrade" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstGroup" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox7" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox8" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox9" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox10" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox11" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox12" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstGradeDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstGroupDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 37px; vertical-align: bottom;">
                                </td>
                                <td align="center" style="height: 37px; vertical-align: bottom; width: 170px;">
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
            <td colspan="5" align="right" style="height: 30px">
                <asp:HiddenField ID="DateHdn" runat="server" ClientIDMode="Static" />
                <%--  <asp:Label ID="HeadLbl" runat="server" Font-Bold="True" Font-Size="Medium"  ClientIDMode="Static"></asp:Label> --%>
                &nbsp;<asp:Button ID="btnClose" Text="Close" runat="server" CssClass="ButtonControl"
                    OnClick="Close_Click" Visible="False" Width="90px" Height="23px" />
            </td>
        </tr>
        <tr>
            <td style="text-align: right" class="style28">
                <asp:Panel ID="viewer" runat="server" Height="410px" BorderStyle="Solid" BorderColor="#0066FF"
                    BorderWidth="3px" Width="100%" Visible="false">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Font-Names="Verdana"
                        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                        WaitMessageFont-Size="14pt" BorderStyle="None" Visible="False">
                    </rsweb:ReportViewer>
                </asp:Panel>
                <input type="button" class="ButtonControl" style="width: 120px; visibility: hidden;"
                    value="Export To Excel" id="Excel_Export_Button" onclick='OnExcelExportDownLoad("EXCELDOWNLOAD")' />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Close" Text="Close" Visible="false" runat="server" CssClass="ButtonControl"
                    OnClientClick="return CloseFun()" />
                &nbsp;&nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style28">
                &nbsp; &nbsp; &nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <table align="center">
        <tr>
            <%--	<td></td>--%>
            <%--<td></td>--%>
            <td align="left" class="style27">
                <asp:HyperLink ID="Back_to_Module_Dashboard" CssClass="Back_LogoutLink" runat="server"
                    NavigateUrl="../../BaseModule/UI/BaseModule_Dashboard.aspx" Visible="False">Back to TK Web Reports Dashboard</asp:HyperLink>
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
        .style25
        {
            height: 75px;
            width: 5%;
        }
        .style26
        {
            height: 75px;
            width: 7%;
        }
        .TableClass
        {
            height: 259px;
            width: 93%;
            color: Black;
        }
        .style27
        {
            width: 40%;
        }
        .style28
        {
            width: 100%;
        }
        .style29
        {
            width: 117px;
        }
        .style30
        {
            height: 75px;
            width: 134px;
        }
        #txtCalendarFrom
        {
            width: 108px;
        }
    </style>
</asp:Content>
