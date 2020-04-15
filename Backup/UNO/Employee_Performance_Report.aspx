<%@ Page Language="C#" MasterPageFile="~/ModuleMain.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="Employee_Performance_Report.aspx.cs" Inherits="MMWebReports.Employee_Performance_Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <link rel='stylesheet' href='./calendar.css' title='calendar'>
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/calendar.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13)) { return false; }
        }
        document.onkeypress = stopRKey;
    </script>
    <script type="text/javascript" language="javascript">

        function fnCharAlphaNumeric(evnt) {

            var charCode = (evnt.which) ? evnt.which : event.keyCode
            if ((charCode >= 97 && charCode <= 122) || (charCode >= 65 && charCode <= 90) || (charCode >= 48 && charCode <= 57) ||

			 (charCode == 8)) {
                return true
            }
            else {
                return false
            }
        }
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

        function ShowPrint() {
            document.getElementById('Print').style.display = "inline";
        }

        function HidePrint() {
            document.getElementById('Print').style.display = "none";
        }

        function FillDate(str) {
            if (str.value == "")
                str.value = document.getElementById('DateHdn').value;
        }

        function ResetHdn(str) {
            document.getElementById(str).value = "";

        }

    </script>
    <script type="text/javascript" language="javascript">

        function OnShow(action) {
            if (ValidateData()) {
                var formName = document.aspnetForm;
                //var sBody = getRequestBody(formName, "SHOW");
                var sBody = "ACTION=SHOW";
                var url = "Employee_Performance_Report.aspx?" + sBody;
                formName.action = url;
                formName.submit();
            }
        }
        function OnPrint() {
            msg = confirm('Do you really want to Print this Report? ');
            if (msg == false)
                return;
            var formName = document.aspnetForm;
            var sBody = getRequestBody(formName, "PRINT");
            var url = "Employee_Performance_Report.aspx?" + sBody;
            formName.action = url;
            formName.submit();
        }
        function HandleAction(formAction, recomputeParentControlName) {
            var formName = document.aspnetForm;
            var sBody = getRequestBody(formName, formAction);

            if (formAction == 'SHOW') {
                if (!ValidateData())
                    return;
            }
            httpRequest = getXMLHTTPRequest();
            httpRequest.onreadystatechange = function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var doc;
                        doc = httpRequest.responseXML;
                        if (doc) {
                            if (formAction.search(/RECOMPUTE/) != -1) {
                            }
                            else {
                                updateReport(httpRequest, 'reportDiv');
                                if (!getMessageString(doc, 'messageDiv', 'messageDiv'))
                                    resetAllControls();
                            }
                        }
                    }
                }
            }
            httpRequest.open('POST', 'Employee_Performance_Report.aspx', true);
            httpRequest.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            httpRequest.send(sBody);
        }

        function ValidationReport() {

            var formName = document.aspnetForm;
            if (document.getElementById('txtCalendarFrom').value == "") {
                alert("Please enter From Date.");
                document.getElementById('txtCalendarFrom').focus();
                return false;
            }


            if (document.getElementById('txtCalendarTo').value == "") {
                alert("Please enter To Date.");
                document.getElementById('txtCalendarTo').focus();
                return false;
            }



            var strFrom = document.getElementById('txtCalendarFrom').value;
            var strTo = document.getElementById('txtCalendarTo').value;

            if (CompareDates(strTo, strFrom) == true) {

                alert("From Date must be less than or equals to To Date");
                document.getElementById('txtCalendarTo').focus();
                return false;
            }


        }

     
        function CompareDates(d1, d2) {


            //		     var start = d1.value.toUpperCase();
            //		     var end = d2.value.toUpperCase();

            var start = d1;
            var end = d2;


            var arrDate = start.split('/');
            var arrDate1 = end.split('/');
            var d1 = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
            var d2 = new Date(arrDate1[2], arrDate1[1] - 1, arrDate1[0]);

            if (d1 >= d2) {

                //alert("End date must be lessthan startdate");
                return false
            }
            return true
        }

  

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
    <h1 class="heading" style='text-align: center; font-family: tahoma; font-size: x-large;'>
        Employee Performance Report</h1>
    <asp:Panel runat="server" ID="HeadPanel" ClientIDMode="Static" Width="100%">
        <table class="TableClass" cellspacing="1" align="center" width="100%">
         
            <tr>
                <td colspan="7" class="TDClassForButton" style="height: 12px">
                    <asp:Panel runat="server" ID="HeadPnl">
                        <%--<table class="TableClass" style="width: 45%; height: 75px"> --%>
                        <table class="TableClass" style="width: 100%; height: 75px; margin-left: 4%;">
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
                                <td style="width: 15%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
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
                                <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px;" id="TABLE3">
                                        <tr>
                                            <td>
                                                Shift
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
                                            <td style="text-align: left">
                                                From Date
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCalendarFrom" onkeyPress="javascript: return false" runat="server"
                                                    ClientIDMode="Static" Height="18px" Width="111px" onkeydown="javascript: return false"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtfromDate_CalendarExtender1" TargetControlID="txtCalendarFrom"
                                                    PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtfromDate1" runat="server" ErrorMessage="Please Select From Date"
                                                    ControlToValidate="txtCalendarFrom" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtfromDate1" runat="server" TargetControlID="rfvtxtfromDate1"
                                                    PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                To Date
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCalendarTo" runat="server" onkeyPress="javascript: return false"
                                                    Height="16px" Width="112px" onkeydown="javascript: return false"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtCalendarTo"
                                                    PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtToDate" runat="server" ErrorMessage="Please Select To Date"
                                                    ControlToValidate="txtCalendarTo" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                    TargetControlID="rfvtxtToDate" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                    </table>
                                    <%--  NEW CALENDER--%>
                                </td>
                                <td style="display: none; border: thin solid lightsteelblue; text-align: left; font-weight: bold;"
                                    class="style25">
                                    <table id="table4" runat="server" style="text-align: right; vertical-align: top;">
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
                                    <table style="height: 50px">
                                        <tr>
                                            <td>
                                                <asp:Button ID="View" class="ButtonControl" Style="width: 80%" runat="server" Text="View"
                                                    ClientIDMode="Static" OnClick="View_Click" />
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
    </asp:Panel>
    <table align="center" style="width: 90%; margin-left: 39px;">
        <tr>
            <td style="text-align: right" class="style28" colspan="5">
                <div align="right" style="width: 91%;">
                    <%--  <asp:Label ID="HeadLbl" runat="server" Font-Bold="True" Font-Size="Medium"  ClientIDMode="Static"></asp:Label> --%>
                    <asp:HiddenField ID="DateHdn" runat="server" ClientIDMode="Static" />
                    &nbsp;<asp:TextBox ID="EmpCode" MaxLength="8" onkeypress="return fnCharAlphaNumeric(event)"
                        onblur="fnDisplayEmployeeName()" runat="server" Visible="False"></asp:TextBox>
                    <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="ButtonControl" OnClick="Close_Click"
                        Visible="False" />
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
                        BorderWidth="3px" Width="88%" Visible="false">
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
            width: 108px;
        }
        #txtCalendarTo
        {
            width: 108px;
        }
    </style>
</asp:Content>
