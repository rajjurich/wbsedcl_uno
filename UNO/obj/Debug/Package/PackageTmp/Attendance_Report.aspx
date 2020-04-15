<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Attendance_Report.aspx.cs"
    Inherits="UNO.Attendance_Report1" MasterPageFile="~/ModuleMain.master" EnableEventValidation="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <br />
    <center>
        <div style="width: 100%; text-align: center">
            <h1 class="heading" style='text-align: center; font-family: tahoma; font-size: x-large;
                width: 100%;'>
                Employee Attendance Report</h1>
            <br />
            <div style="width: 100%; text-align: center;" runat="server" id="DivSearch" align="center">
                <table class="TableClass" cellspacing="1"  runat="server" id="ShowTable"
                    style="display: none;text-align:center;" width="100%" align="center">
                    <tr>
                        <td colspan="7" class="TDClassForButton" style="height: 12px; text-align: center;">
                            <asp:Panel runat="server" ID="HeadPnl">
                                <table class="TableClass" style="width: 100%; height: 75px; padding-left: 0%" align="center">
                                    <tr>
                                        <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;"
                                            class="style26">
                                            <table style="height: 75px; width: 142px;">
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
                                                            <%--<asp:ListItem Value="2" >Enter Code</asp:ListItem> --%>
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
                                            <table style="height: 75px; width: 70px;">
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
                                                        Division
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
                                        <td style="width: 10%; height: 75px; text-align: left; display: none; border-right: lightsteelblue thin solid;
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
                                                            <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ShiftHdn')">&nbsp;All</asp:ListItem>
                                                            <asp:ListItem Value="1" onclick="show6()">&nbsp;Select</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:HiddenField ID="ShiftHdn" runat="server" ClientIDMode="Static" />
                                        </td>
                                        <td style="width: 10%; height: 75px; text-align: left; display: none; border-right: lightsteelblue thin solid;
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
                                        <td style="width: 20%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                            border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                            border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                            <table style="width: 150px;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlFromMonth" runat="server" class="chosen-select" Width="140px"
                                                            ClientIDMode="Static">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlFromYear" runat="server" class="chosen-select" Width="140px"
                                                            ClientIDMode="Static">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--  NEW CALENDER--%>
                                        </td>
                                        <td style="width: 10%; height: 75px; text-align: left; display: none; border-right: lightsteelblue thin solid;
                                            border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                            border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                            <%--  NEW CALENDER--%>
                                            <table>
                                                <tr>
                                                    <td colspan="2" style="height: 26px; text-align: left">
                                                        &nbsp; Date
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input runat="server" type="text" name='From_Date' id='txtCalendarFrom' maxlength="10"
                                                            readonly="readonly" />
                                                    </td>
                                                    <td>
                                                        <img src='images/calendar.gif' onmouseover="fnInitCalendar(this, 'txtCalendarFrom', 'style=calendar.css,close=true')">
                                                        </img>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input type="text" name='To_Date' id='txtCalendarTo' maxlength="10" readonly="readonly" />
                                                    </td>
                                                    <td>
                                                        <img src='images/calendar.gif' onmouseover="fnInitCalendar(this, 'txtCalendarTo', 'style=calendar.css,close=true')">
                                                        </img>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--  NEW CALENDER--%>
                                        </td>
                                        <td style="display: none; border: thin solid lightsteelblue; text-align: left; font-weight: bold;
                                            display: none" class="style25">
                                            <table id="table4" runat="server" style="text-align: right; vertical-align: top">
                                                <tr>
                                                    <td colspan="2" style="height: 26px; text-align: left">
                                                        <asp:CheckBox ID="TimeCheckBox" runat="server" onclick="ShowTime(this)" Text="Check for  Timings"
                                                            Width="125px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Time_Panel" Style="visibility: hidden" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 65px; height: 25px">
                                                                        From :
                                                                    </td>
                                                                    <td align="left" style="height: 25px">
                                                                        <asp:TextBox ID="From_Time" runat="server" onkeyup="fnColon(this,event)" onkeypress="findspace(event)"
                                                                            Width="50px" MaxLength="5" Height="15px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 65px; height: 22px">
                                                                        To :
                                                                    </td>
                                                                    <td align="left" style="height: 22px">
                                                                        <asp:TextBox ID="To_Time" runat="server" onkeyup="fnColon(this,event)" onkeypress="findspace(event)"
                                                                            Width="50px" MaxLength="5" Height="15px">00:00</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="text-align: center; border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                                            border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                                            font-weight: bold;">
                                            <table style="height: 50px; width: 150px;">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="View" class="ButtonControl" Style="width: 80%" runat="server" Text="View"
                                                            ClientIDMode="Static" OnClientClick='return ValidationReport()' OnClick="View_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="Button4" runat="server" Width="80%" CssClass="ButtonControl" OnClick="btnReset_Click"
                                                            Text="Reset" ClientIDMode="Static" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%-- <asp:Button ID="Button5" runat="server" CssClass="ButtonControl" Text="Close" OnClick="Button5_Click" />--%>
                                                        <asp:Button ID="btnClosePage" class="ButtonControl" Style="width: 80%" Text="Close"
                                                            runat="server" OnClick="btnClosePage_Click" />
                                                        <%--      <input type ="button" class= "ButtonControl" style="width:80%"  value= "Close"   runat="server"    />--%>
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
                        <td align="center" colspan="7" style="vertical-align: middle;">
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
                                            <asp:ListBox ID="ListBox13" runat="server" ClientIDMode="Static" Font-Bold="True"
                                                Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                                Style="display: none" Width="100%"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="height: 37px; vertical-align: bottom;">
                                        </td>
                                        <td align="center" style="height: 37px; vertical-align: bottom; width: 170px;">
                                            <input id="Button1" class="ButtonControl" name="Ok" style="width: 71px" type="button"
                                                value="OK" runat="server" />&nbsp;&nbsp;
                                            <input id="Button2" class="ButtonControl" name="Cancel" style="width: 71px" type="button"
                                                value="Cancel" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </center>
    <%--</asp:Panel>--%>
    <table align="center" style="width: 90%; margin-left: 5%;">
        <tr>
            <td colspan="5" align="right" style="height: 44px">
                <asp:HiddenField ID="DateHdn" runat="server" ClientIDMode="Static" />
                &nbsp;<asp:TextBox ID="EmpCode" MaxLength="8" onkeypress="return fnCharAlphaNumeric(event)"
                    onblur="fnDisplayEmployeeName()" runat="server" Visible="False"></asp:TextBox>
                <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="ButtonControl" Visible="False"
                    OnClick="btnClose_Click" />
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 100%;" class="style28">
                <asp:Panel ID="viewer" runat="server" Height="410px" BorderStyle="Solid" BorderColor="#0066FF"
                    BorderWidth="3px" Width="100%" Visible="false">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Font-Names="Verdana"
                        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                        WaitMessageFont-Size="14pt" BorderStyle="None" Visible="False">
                    </rsweb:ReportViewer>
                </asp:Panel>
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
            <%--  <asp:Label ID="HeadLbl" runat="server" Font-Bold="True" Font-Size="Medium"  ClientIDMode="Static"></asp:Label> --%><%--	<td></td>--%>
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
    <script src="Scripts/Choosen/chosen.jquery.js" type="text/javascript"></script>
    <script src="Scripts/Choosen/docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "95%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style25
        {
            height: 75px;
            width: 5%;
        }
        .style26
        {
            height: 75px;
            width: 9%;
        }
        .TableClass
        {
            height: 259px;
            width: 93%;
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
        #txtCalendarTo
        {
            width: 108px;
        }
    </style>
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">



        function fnDisplayEmployeeName() {

            //Nandkishor
            var xEmp_code = document.getElementById('ctl00_PrescientAuthorContent_EmpCode').value;
            var list5 = document.getElementById('ctl00_PrescientAuthorContent_ListBox1');
            var blnEmployeeExists = false

            for (var i = 0; i < list5.options.length; i++) {
                if (list5.options[i].value == xEmp_code) {
                    document.getElementById('ctl00_PrescientAuthorContent_lblEmployeeName').innerHTML = list5.options[i].text.substring(0, 20);
                }
            }
            //Nandkishor
        }



        function ResetHdn(str) {
            document.getElementById(str).value = "";

        }


     

    </script>
    <script type="text/javascript" language="javascript">


        function ValidationReport() {

            var formName = document.aspnetForm;
            if (document.getElementById('ddlFromMonth').value == "0") {
                alert("Please enter Month.");
                document.getElementById('ddlFromMonth').focus();
                return false;
            }


            if (document.getElementById('ddlFromYear').value == "0") {
                alert("Please enter Year.");
                document.getElementById('ddlFromYear').focus();
                return false;
            }

        }
	 
    </script>
</asp:Content>
