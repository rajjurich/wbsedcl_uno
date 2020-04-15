<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Page Language="C#" MasterPageFile="~/ModuleMain.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="AccessReport_HeadCount.aspx.cs" Inherits="UNO.AccessReport_HeadCount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <link rel='stylesheet' href='./calendar.css' title='calendar'>
    <script language="javascript" src="Scripts/calendar.js"></script>
    <script language="javascript" type="text/javascript">
        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13)) { return false; }
        }
        document.onkeypress = stopRKey;
    </script>
    <script type="text/javascript" language="javascript">


        function ResetHdn(str) {
            document.getElementById(str).value = "";

        }



        function DispText() {
            //  document.getElementById('ctl00_ProjectContent_DDO_pnl').style.display="inline";     
            document.getElementById('Panel1').style.display = "inline";
            document.getElementById('_ListBox1').style.display = "none";
            document.getElementById('ListBox2').style.display = "none";
            document.getElementById('ListBox3').style.display = "none";
            document.getElementById('ListBox4').style.display = "none";
            document.getElementById('ListBox5').style.display = "none";
            document.getElementById('ListBox6').style.display = "none";
            //document.getElementById('ListBox7').style.display="none";     

            common();
            document.getElementById('EmpCode').style.display = "inline";
            document.getElementById('EmpCode').value = "";
            document.getElementById('EmpCode').focus();
            document.getElementById('HeadLbl').innerHTML = "";
            //document.getElementById('LstTitle').innerHTML="&nbsp;Enter Employee Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            // document.getElementById('ctl00_ProjectContent_LstTitle').style.display="none";
            document.getElementById('LstTitle').innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Enter Employee Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp";
        }




        function show9() {

            document.getElementById("<%=txtReader.ClientID%>").style.display = "inline";
            document.getElementById('Panel1').style.display = "inline";
            document.getElementById('ListBox1').style.display = "none";
            document.getElementById('ListBox2').style.display = "none";

            document.getElementById('ListBox3').style.display = "none";
            document.getElementById('ListBox4').style.display = "none";
            document.getElementById('ListBox5').style.display = "none";
            document.getElementById('ListBox6').style.display = "none";
            document.getElementById('lstGroup').style.display = "none";
            document.getElementById('lstGrade').style.display = "none";
            document.getElementById('lstReader').style.display = "inline";
            document.getElementById('lstZone').style.display = "none";
            document.getElementById('<%=Button1.ClientID%>').style.display = "inline";
            document.getElementById('<%=Button2.ClientID%>').style.display = "inline";
            common();
            List1('lstReader', 'ReaderHdn');
            document.getElementById('HeadLbl').innerHTML = "Select Readers";
            document.getElementById('LstTitle').innerHTML = "Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Description";

            document.getElementById("<%=txtEmployeeSearch.ClientID%>").style.display = "none";
            document.getElementById("<%=txtCompany.ClientID%>").style.display = "none";
            document.getElementById("<%=txtLocation.ClientID%>").style.display = "none";
            document.getElementById("<%=txtDivision.ClientID%>").style.display = "none";
            document.getElementById("<%=txtDepartment.ClientID%>").style.display = "none";
            document.getElementById("<%=txtShift.ClientID%>").style.display = "none";

            document.getElementById("<%=txtGroup.ClientID%>").style.display = "none";
            document.getElementById("<%=txtGrade.ClientID%>").style.display = "none";
            document.getElementById("<%=txtReader.ClientID%>").style.display = "inline";
            document.getElementById("<%=txtZone.ClientID%>").style.display = "none";

        }

        function show10() {
            document.getElementById('Panel1').style.display = "inline";
            document.getElementById('ListBox1').style.display = "none";
            document.getElementById('ListBox2').style.display = "none";

            document.getElementById('ListBox3').style.display = "none";
            document.getElementById('ListBox4').style.display = "none";
            document.getElementById('ListBox5').style.display = "none";
            document.getElementById('ListBox6').style.display = "none";
            document.getElementById('lstGroup').style.display = "none";
            document.getElementById('lstGrade').style.display = "none";
            document.getElementById('lstReader').style.display = "none";
            document.getElementById('lstZone').style.display = "inline";

            common();
            List1('lstReader', 'ReaderHdn');
            document.getElementById('HeadLbl').innerHTML = "Select Section";
            document.getElementById('LstTitle').innerHTML = "Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Description";

            document.getElementById("<%=txtEmployeeSearch.ClientID%>").style.display = "none";
            document.getElementById("<%=txtCompany.ClientID%>").style.display = "none";
            document.getElementById("<%=txtLocation.ClientID%>").style.display = "none";
            document.getElementById("<%=txtDivision.ClientID%>").style.display = "none";
            document.getElementById("<%=txtDepartment.ClientID%>").style.display = "none";
            document.getElementById("<%=txtShift.ClientID%>").style.display = "none";

            document.getElementById("<%=txtGroup.ClientID%>").style.display = "none";
            document.getElementById("<%=txtGrade.ClientID%>").style.display = "none";
            document.getElementById("<%=txtReader.ClientID%>").style.display = "none";
            document.getElementById("<%=txtZone.ClientID%>").style.display = "inline";

        }




        function common() {
            //        toggleDisabled(document.getElementById("HeadPnl"));


            document.getElementById('RadioButtonList1_0').disabled = true;
            document.getElementById('RadioButtonList1_1').disabled = true;
            document.getElementById('RadioButtonList2_0').disabled = true;
            document.getElementById('RadioButtonList2_1').disabled = true;
            document.getElementById('RadioButtonList3_0').disabled = true;
            document.getElementById('RadioButtonList3_1').disabled = true;
            document.getElementById('RadioButtonList4_0').disabled = true;
            document.getElementById('RadioButtonList4_1').disabled = true;
            document.getElementById('RadioButtonList5_0').disabled = true;
            document.getElementById('RadioButtonList5_1').disabled = true;
            document.getElementById('RadioButtonList6_0').disabled = true;
            document.getElementById('RadioButtonList6_1').disabled = true;

            document.getElementById('rdbtnGrade_0').disabled = true;
            document.getElementById('rdbtnGrade_1').disabled = true;

            document.getElementById('rdbtnGroup_0').disabled = true;
            document.getElementById('rdbtnGroup_1').disabled = true;


            document.getElementById('rblReader_0').disabled = true;
            document.getElementById('rblReader_1').disabled = true;

            document.getElementById('rblZone_0').disabled = true;
            document.getElementById('rblZone_1').disabled = true;

            // document.getElementById('txtCalendarFrom').disabled = true;
            document.getElementById('View').disabled = true;
            document.getElementById('Button4').disabled = true;
            document.getElementById('Button5').disabled = true;

            document.getElementById("<%=ddlPersonnelType.ClientID%>").disabled = true;

            document.getElementById("<%=DropDownList1.ClientID%>").disabled = true;
            document.getElementById("<%=ddlTA.ClientID%>").disabled = true;



        }

        function EnableControls() {
            document.getElementById('RadioButtonList1_0').disabled = false;
            document.getElementById('RadioButtonList1_1').disabled = false;
            document.getElementById('RadioButtonList2_0').disabled = false;
            document.getElementById('RadioButtonList2_1').disabled = false;
            document.getElementById('RadioButtonList3_0').disabled = false;
            document.getElementById('RadioButtonList3_1').disabled = false;
            document.getElementById('RadioButtonList4_0').disabled = false;
            document.getElementById('RadioButtonList4_1').disabled = false;
            document.getElementById('RadioButtonList5_0').disabled = false;
            document.getElementById('RadioButtonList5_1').disabled = false;
            document.getElementById('RadioButtonList6_0').disabled = false;
            document.getElementById('RadioButtonList6_1').disabled = false;

            document.getElementById('rdbtnGrade_0').disabled = false;
            document.getElementById('rdbtnGrade_1').disabled = false;
            document.getElementById('rdbtnGroup_0').disabled = false;
            document.getElementById('rdbtnGroup_1').disabled = false;

            document.getElementById('rblReader_0').disabled = false;
            document.getElementById('rblReader_1').disabled = false;
            document.getElementById('rblZone_0').disabled = false;
            document.getElementById('rblZone_1').disabled = false;

            // document.getElementById('txtCalendarFrom').disabled = false;
            document.getElementById('View').disabled = false;
            document.getElementById('Button4').disabled = false;
            document.getElementById('Button5').disabled = false;
            document.getElementById('Panel1').style.display = "none";

            document.getElementById('HeadLbl').innerHTML = "";
        }

        function DisableControls() {
            document.getElementById('RadioButtonList2_0').disabled = true;
            document.getElementById('RadioButtonList2_1').disabled = true;
            document.getElementById('RadioButtonList3_0').disabled = true;
            document.getElementById('RadioButtonList3_1').disabled = true;
            document.getElementById('RadioButtonList4_0').disabled = true;
            document.getElementById('RadioButtonList4_1').disabled = true;
            document.getElementById('RadioButtonList5_0').disabled = true;
            document.getElementById('RadioButtonList5_1').disabled = true;
            document.getElementById('RadioButtonList2_0').checked = true;
            document.getElementById('RadioButtonList3_0').checked = true;
            document.getElementById('RadioButtonList4_0').checked = true;
            document.getElementById('RadioButtonList5_0').checked = true;
            document.getElementById('RadioButtonList6_0').checked = true;
            document.getElementById('RadioButtonList1_0').disabled = false;
            document.getElementById('RadioButtonList1_1').disabled = false;
            document.getElementById('RadioButtonList6_0').disabled = true;
            document.getElementById('RadioButtonList6_1').disabled = true;

            document.getElementById('rdbtnGrade_0').disabled = true;
            document.getElementById('rdbtnGrade_1').disabled = true;
            document.getElementById('rdbtnGroup_0').disabled = true;
            document.getElementById('rdbtnGroup_1').disabled = true;
            document.getElementById('rblReader_0').disabled = true;
            document.getElementById('rblReader_1').disabled = true;
            document.getElementById('rblZone_0').disabled = true;
            document.getElementById('rblZone_1').disabled = true;


        }

        function EnableRestControls() {
            document.getElementById('RadioButtonList1_0').disabled = false;
            document.getElementById('RadioButtonList1_1').disabled = false;
            document.getElementById('RadioButtonList2_0').disabled = false;
            document.getElementById('RadioButtonList2_1').disabled = false;
            document.getElementById('RadioButtonList3_0').disabled = false;
            document.getElementById('RadioButtonList3_1').disabled = false;
            document.getElementById('RadioButtonList4_0').disabled = false;
            document.getElementById('RadioButtonList4_1').disabled = false;
            document.getElementById('RadioButtonList5_0').disabled = false;
            document.getElementById('RadioButtonList5_1').disabled = false;
            document.getElementById('RadioButtonList6_0').disabled = false;
            document.getElementById('RadioButtonList6_1').disabled = false;
        }



        function fnCloseReport() {
            navigateToUrl('Uno_Dashboard.aspx');
        }



        function List1(str1, str2) {
            list = document.getElementById(str1);
            hdn = document.getElementById(str2).value;

            for (var i = 0; i < list.options.length; i++) {
                if (hdn.indexOf("'" + list.options[i].value + "'") != -1)
                    list.options[i].selected = true;
                else
                    list.options[i].selected = false;

            }

        }


    </script>
    <script type="text/javascript" language="javascript">

		
		
    </script>
    <asp:Panel runat="server" ID="HeadPanel" ClientIDMode="Static" Width="100%" Style="padding-left: 7%;">
        <h1 class="heading" style='text-align: center; font-family: Tahoma; font-size: x-large;'>
            Employees On Premise Report</h1>
        <table class="TableClass" cellspacing="1" align="center" width="100%" style="padding-top: 1%;">
            <tr>
                <td colspan="7" class="TDClassForButton" style="height: 15px">
                    <asp:Panel runat="server" ID="Panel2">
                        <table class="TableClass" style="width: 100%; height: 25px; padding-left: 3%;">
                            <tr style="display: none;">
                                <td style="border: thin solid lightsteelblue; text-align: left; width: 30%; font-weight: bold;"
                                    class="style26">
                                    &nbsp Personnel Type: &nbsp
                                    <asp:DropDownList ID="ddlPersonnelType" runat="server" AutoPostBack="true">
                                        <asp:ListItem Value="E" Text="Employee" />
                                        <asp:ListItem Value="NE" Text="Non Employee" />
                                    </asp:DropDownList>
                                </td>
                                <td style="border: thin solid lightsteelblue; display: none; text-align: left; width: 25%;
                                    font-weight: bold;" class="style26">
                                    &nbsp Status : &nbsp
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem Value="3" Text="ALL" />
                                        <asp:ListItem Value="0" Text="Granted" />
                                        <asp:ListItem Value="1" Text="Denied" />
                                    </asp:DropDownList>
                                </td>
                                <td style="border: thin solid lightsteelblue; display: none; text-align: left; width: 25%;
                                    font-weight: bold;" class="style26">
                                    &nbsp Level : &nbsp
                                    <asp:DropDownList ID="ddlTA" runat="server">
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
                <td colspan="7" class="TDClassForButton" style="height: 12px">
                    <asp:Panel runat="server" ID="HeadPnl">
                        <table class="TableClass" style="width: 100%; height: 75px; padding-left: 3%;">
                            <tr>
                                <td style="border: thin solid lightsteelblue; text-align: left; width: 10%; font-weight: bold;"
                                    class="style26">
                                    <table style="height: 75px; width: 100%;">
                                        <tr>
                                            <td class="style29">
                                                &nbsp;Employee
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left" class="style29">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow" CssClass="radiobutton"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="EmployeeHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="border: thin solid lightsteelblue; width: 10%; text-align: left; font-weight: bold;"
                                    class="style30">
                                    <table style="height: 75px; width: 100%;">
                                        <tr>
                                            <td>
                                                Company&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ComapnyHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ComapnyHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px; width: 100%;">
                                        <tr>
                                            <td>
                                                &nbsp;Location
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="RadioButtonList3" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('LocationHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="LocationHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="height: 75px; text-align: left; width: 10%; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px; width: 100%;">
                                        <tr>
                                            <td>
                                                &nbsp;Division
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="RadioButtonList4" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('DivisionHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="DivisionHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px; width: 100%;" id="TABLE2">
                                        <tr>
                                            <td>
                                                &nbsp;Department
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="RadioButtonList5" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('DepartmentHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="DepartmentHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="height: 75px; text-align: left; width: 10%; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold; display: none">
                                    <table style="height: 75px; width: 100%;" id="TABLE3">
                                        <tr>
                                            <td>
                                                &nbsp;Shift
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="RadioButtonList6" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ShiftHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ShiftHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="height: 75px; text-align: left; width: 10%; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px; width: 100%;" id="TABLE1">
                                        <tr>
                                            <td>
                                                &nbsp;Grade
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
                                <td style="height: 75px; text-align: left; width: 10%; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px; width: 100%;" id="TABLE3">
                                        <tr>
                                            <td>
                                                &nbsp;Group
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
                                <td style="height: 75px; text-align: left; width: 10%; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px; width: 100%;" id="TABLE5">
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
                                                    <asp:ListItem Value="1" onclick="show9()">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ReaderHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="height: 75px; text-align: left; width: 10%; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold; display: none;">
                                    <table style="height: 75px; width: 100%;" id="TABLE6">
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
                                                    <asp:ListItem Value="1" onclick="show10()">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ZoneHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px;" id="TABLE7">
                                        <tr>
                                            <td>
                                                &nbsp;Designation
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbtnDesignation" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('DesignationHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="DesignationHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold; display: none;">
                                    <%--  old Calender --%>
                                    <%--  NEW CALENDER--%>
                                    <table>
                                        <tr>
                                            <td style="text-align: left">
                                                From Date
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCalendarFrom" runat="server" ClientIDMode="Static" Height="18px"
                                                    Width="111px"></asp:TextBox>
                                                <%--  <ajaxToolkit:CalendarExtender ID="txtfromDate_CalendarExtender1" TargetControlID="txtCalendarFrom"
                                                    PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtfromDate1" runat="server" ErrorMessage="Please Select From Date"
                                                    ControlToValidate="txtCalendarFrom" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtfromDate1" runat="server" TargetControlID="rfvtxtfromDate1"
                                                    PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                To Date
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtToDate" runat="server" Height="16px" Width="112px"></asp:TextBox>
                                                <%--  <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtToDate"
                                                    PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtToDate" runat="server" ErrorMessage="Please Select To Date"
                                                    ControlToValidate="txtToDate" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvtxtToDate"
                                                    PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>--%>
                                            </td>
                                        </tr>
                                    </table>
                                    <%--  NEW CALENDER--%>
                                </td>
                                <td style="display: none; border: thin solid lightsteelblue; text-align: left; font-weight: bold;"
                                    class="style25">
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
                                    <table style="height: 50px; width: 100px">
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
                                    <asp:TextBox ID="txtZone" runat="server" placeholder="Search" Width="100%" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtDesignation" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="LstTitle" runat="server" BackColor="AliceBlue" BorderStyle="Double"
                                        ClientIDMode="Static" Font-Bold="True" Font-Names="Courier New" ForeColor="Black"
                                        Style="text-align: left; font-weight: bold; font-family: 'Courier New', Monospace;"
                                        Width="100%"></asp:Label>
                                    <asp:ListBox ID="ListBox1" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple"
                                        Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox2" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="5" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox3" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="5" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox4" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="5" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox5" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="5" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox6" runat="server" ClientIDMode="Static" Font-Bold="True"
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
                                     <asp:ListBox ID="lstDesignation" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox7" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox8" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox9" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox10" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox11" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="5" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox12" runat="server" ClientIDMode="Static" Font-Bold="True"
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
                                    <asp:ListBox ID="lstDesignationDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <%--     <td align="center" style="height: 37px; vertical-align: bottom;">
                                  
                                </td>--%>
                                <td align="center" style="height: 37px; vertical-align: bottom; width: 170px;" colspan="2">
                                    <input id="Button1" class="ButtonControl" name="Ok" style="width: 71px" type="button"
                                        value="OK" runat="server" onclick="OkClick();" />
                                    &nbsp;&nbsp;
                                    <input id="Button2" class="ButtonControl" name="Cancel" style="width: 71px" type="button"
                                        value="Cancel" runat="server" onclick="CancelClick();" />
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
            <td style="text-align: right" class="style28" colspan="5">
                <div align="center" style="width: 100%;">
                    <asp:Panel ID="viewer" runat="server" Height="410px" BorderStyle="Solid" BorderColor="#0066FF"
                        BorderWidth="3px" Width="72%" Visible="false">
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                            BorderStyle="None" Visible="False" Width="100%">
                        </rsweb:ReportViewer>
                    </asp:Panel>
                    <asp:HiddenField ID="DateHdn" runat="server" ClientIDMode="Static" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Close" Text="Close" Visible="false" runat="server" CssClass="ButtonControl"
                        OnClientClick="return CloseFun()" />
                    &nbsp;&nbsp;
                </div>
            </td>
            <%-- <td>
                &nbsp;
            </td>--%>
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
        }
    </style>
</asp:Content>
