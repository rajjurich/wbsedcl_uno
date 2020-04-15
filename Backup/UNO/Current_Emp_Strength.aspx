<%@ Page Language="C#" MasterPageFile="~/ModuleMain.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="Current_Emp_Strength.aspx.cs" Inherits="UNO.Current_Emp_Strength1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/calendar.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function fnCloseReport() {
            navigateToUrl('Uno_Dashboard.aspx');
        }
       
    </script>
    <table cellspacing="0" class="MenuTable">
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <h1 class="heading" style='text-align: center; font-family: Tahoma; font-size: x-large;'>
        <asp:Label ID="lblHeader" runat="server" Text="Current Employees strength Report"></asp:Label></h1>
    <div>
        <center>
            <asp:Panel runat="server" ID="HeadPanel" ClientIDMode="Static" Width="80%">
                <table class="TableClass" cellspacing="1" align="center" width="80%">
                    <tr style="display: none;">
                        <td style="padding-left: 3%">
                            Personnel Type:
                            <asp:DropDownList ID="ddlPersonnelType" runat="server" AutoPostBack="true">
                                <asp:ListItem Value="E" Text="Employee" />
                                <asp:ListItem Value="NE" Text="Non Employee" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" class="TDClassForButton" style="height: 12px">
                            <asp:Panel runat="server" ID="HeadPnl">
                                <table class="TableClass" style="width: 100%; height: 75px; padding-left: 3%;">
                                    <tr>
                                        <td style="border: thin solid lightsteelblue; display: none; text-align: left; font-weight: bold;"
                                            class="style26">
                                            <table style="height: 75px; width: 100px;">
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
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:HiddenField ID="EmployeeHdn" runat="server" ClientIDMode="Static" />
                                        </td>
                                        <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;"
                                            class="style30">
                                            <table style="height: 75px; width: 100px;">
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
                                            <table style="height: 75px; width: 105px;">
                                                <tr>
                                                    <td>
                                                        &nbsp;Location
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
                                        <td style="height: 75px; text-align: left; border-right: lightsteelblue thin solid;
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
                                            border-bottom: lightsteelblue thin solid; font-weight: bold; display: none">
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
                                            <table style="height: 75px;" id="TABLE3">
                                                <tr>
                                                    <td>
                                                        Group
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
                                            display: none; border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                            border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                            <table>
                                                <tr>
                                                    <td style="text-align: left">
                                                        From Date
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtCalendarFrom" onkeyPress="javascript: return false" runat="server"
                                                            ClientIDMode="Static" Height="18px" Width="111px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="txtfromDate_CalendarExtender1" TargetControlID="txtCalendarFrom"
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
                                        <td style="text-align: center; border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                                            border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                                            font-weight: bold;">
                                            <table style="height: 50px; width: 100px">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="View" class="ButtonControl" Style="width: 80%" runat="server" Text="View"
                                                            ClientIDMode="Static" OnClick="View_Click" ValidationGroup="Add" />
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
                                            <asp:TextBox ID="txtEmployeeSearch" runat="server" placeholder="Search" onkeyup="FilterEmployee()"
                                                Width="100%"></asp:TextBox>
                                            <asp:TextBox ID="txtCompany" runat="server" placeholder="Search" onkeyup="FilterCompany()"
                                                Width="100%"></asp:TextBox>
                                            <asp:TextBox ID="txtLocation" runat="server" placeholder="Search" onkeyup="FilterLocation()"
                                                Width="100%"></asp:TextBox>
                                            <asp:TextBox ID="txtDivision" runat="server" placeholder="Search" onkeyup="FilterDivision()"
                                                Width="100%"></asp:TextBox>
                                            <asp:TextBox ID="txtDepartment" runat="server" placeholder="Search" onkeyup="FilterDept()"
                                                Width="100%"></asp:TextBox>
                                            <asp:TextBox ID="txtShift" runat="server" placeholder="Search" onkeyup="FilterShift()"
                                                Width="100%"></asp:TextBox>
                                            <asp:TextBox ID="txtGrade" runat="server" placeholder="Search" onkeyup="FilterDept()"
                                                Width="100%"></asp:TextBox>
                                            <asp:TextBox ID="txtGroup" runat="server" placeholder="Search" onkeyup="FilterShift()"
                                                Width="100%"></asp:TextBox>
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
                    <td style="text-align: right" class="style28" colspan="5">
                        <div align="right" style="width: 86%;">
                            &nbsp;<asp:TextBox ID="EmpCode" MaxLength="8" onkeypress="return fnCharAlphaNumeric(event)"
                                onblur="fnDisplayEmployeeName()" runat="server" Visible="False"></asp:TextBox>
                            <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="ButtonControl" OnClick="Close_Click"
                                Visible="False" Width="90px" Height="23px" />
                        </div>
                    </td>
                </tr>
                <tr style="height: 15px;">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style28">
                        <div align="center" style="width: 100%;">
                            <asp:Panel ID="viewer" runat="server" Height="410px" BorderStyle="Solid" BorderColor="#0066FF"
                                BorderWidth="3px" Width="73%" Visible="false">
                                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Font-Names="Verdana"
                                    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                                    WaitMessageFont-Size="14pt" BorderStyle="None" Visible="False">
                                </rsweb:ReportViewer>
                            </asp:Panel>
                            <asp:HiddenField ID="DateHdn" runat="server" ClientIDMode="Static" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Close" Text="Close" Visible="false" runat="server" CssClass="ButtonControl"
                                OnClientClick="return CloseFun()" />
                            &nbsp;&nbsp;
                        </div>
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
        </center>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style26
        {
            height: 75px;
            width: 9%;
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
        }
    </style>
</asp:Content>
