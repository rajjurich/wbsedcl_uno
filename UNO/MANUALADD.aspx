<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/ModuleMain.master"
    CodeBehind="MANUALADD.aspx.cs" Inherits="UNO.MANUALADD" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">--%>
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/Style.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript">

        function ValidateSave() {

            var first = "";
            var count = 0;
            var emp = document.getElementById('EmployeeHdn').value;
            var com = document.getElementById('ComapnyHdn').value;
            var loc = document.getElementById('LocationHdn').value;
            var div = document.getElementById('DivisionHdn').value;
            var dep = document.getElementById('DepartmentHdn').value;
            var sft = document.getElementById('ShiftHdn').value;
            if (ChkServerSideValidation()) {
                if (emp == "" && com == "" && loc == "" && div == "" && dep == "" && sft == "") {

                    var msg = confirm("No Entity Selected,Are you Sure To continue?");
                    if (msg == false) {
                        return false;
                    }
                    else {
                        if (!ChkServerSideValidation()) {
                            return false;
                        }
                        else return true;
                    }
                }
            }
            else {
                return false;
            }

        }

        function ChkServerSideValidation() {
            if (document.getElementById('<%=txtfromDate.ClientID%>').value == '') {
                document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please enter From Date';
                return false;
            }
            else if (document.getElementById('<%=txtToDate.ClientID%>').value == '') {
                document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please enter To Date';
                return false;
            }
            if (!CompareDates(document.getElementById('<%=txtfromDate.ClientID%>'), document.getElementById('<%=txtToDate.ClientID%>'))) {
                document.getElementById('<%= lblmsg.ClientID %>').innerHTML = "To Date should not be less than From date";
                return false;
            }
            else if (document.getElementById('<%=frm_time.ClientID%>').value == '' && document.getElementById('<%=To_Time.ClientID%>').value == '') {
                document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please Enter In Time or Out Time';
                return false;
            }


            if (document.getElementById('<%=frm_time.ClientID%>').value != "") {
                if (!(ValidateTime(document.getElementById('<%=frm_time.ClientID%>').value))) {
                    document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please enter valid From Time';
                    return false;
                }
            }
            if (document.getElementById('<%=To_Time.ClientID%>').value != "") {
                if (!(ValidateTime(document.getElementById('<%=To_Time.ClientID%>').value))) {
                    document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please enter valid To Time';
                    return false;
                }
            }
            if ($("#<%=ddlReason.ClientID%> option:selected").val() == 'Select One') {
                document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please select Reason Id';
                return false;
            }

            return true;

        }


        function isValidateIssueDate(oSrc, args) {

            if (!CompareDates(document.getElementById('txtfromDate'), document.getElementById('txtToDate'))) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }


        }


        function Reset(Errmsg) {
            $("#<%=RbdEmp.ClientID %>,#<%=RbdCompany.ClientID %>,#<%=RbdLocation.ClientID %>,#<%=RbdDivision.ClientID %>,#<%=RbdDepartment.ClientID %>,#<%=RbdCategory.ClientID %>").find('input').prop('disabled', false);
            $("#RbdEmp_0,#RbdCompany_0,#RbdLocation_0,#RbdDivision_0,#RbdDepartment_0,#RbdCategory_0").prop("checked", true);
            $("#EmployeeHdn,#ComapnyHdn,#LocationHdn,#DivisionHdn,#DepartmentHdn,#ShiftHdn").prop("value", "");

            document.getElementById('RbdEmp_1').checked == true;
            document.getElementById(Errmsg).innerHTML = "";
            //Show
            document.getElementById('btnOK').style.display = "none";
            document.getElementById('Button4').style.display = "none";
            document.getElementById('LstCategory').style.display = "none";
            document.getElementById('Btntd').style.display = "none";
            document.getElementById('LstEntitySelected').style.display = "none";
            //hide
            document.getElementById('LstEmployee').style.display = "none";
            document.getElementById('LstCompany').style.display = "none";
            document.getElementById('LstLocation').style.display = "none";
            document.getElementById('LstDivision').style.display = "none";
            document.getElementById('LstDepartment').style.display = "none";

            document.getElementById('LstEntitySelected').options.length = 0;
            document.getElementById('<%=txtfromDate.ClientID%>').value = '';
            document.getElementById('<%=txtToDate.ClientID%>').value = '';
            document.getElementById('<%=frm_time.ClientID%>').value = '';
            document.getElementById('<%=To_Time.ClientID%>').value = '';
            document.getElementById('<%=txt_Remarks.ClientID%>').value = '';
            //            location.reload();
            return false;
        }


        function sameDate() {
            var fdate = document.getElementById('<%= txtfromDate.ClientID %>').value;
            if (fdate != "") {
                var splitfdate = fdate.split('/');
                var concateFdate = splitfdate[1] + "/" + splitfdate[0] + "/" + splitfdate[2];
                var frmDate = new Date(concateFdate);
                var todate = splitfdate[0] + "/" + splitfdate[1] + "/" + splitfdate[2];
                document.getElementById('<%= txtToDate.ClientID %>').value = todate;
            }
        }


        function chkDate() {

            try {
                var fdate = document.getElementById('<%= txtfromDate.ClientID %>').value;
                if (fdate != "") {
                    var splitfdate = fdate.split('/');
                    var concateFdate = splitfdate[1] + "/" + splitfdate[0] + "/" + splitfdate[2];
                    var frmDate = new Date(concateFdate);
                    var tdate = document.getElementById('<%= txtToDate.ClientID %>').value;
                    var splitTdate = tdate.split('/');
                    var concateTdate = splitTdate[1] + "/" + splitTdate[0] + "/" + splitTdate[2];
                    var toDate = new Date(concateTdate);

                    if (frmDate > toDate)
                        sameDate();
                }
            }
            catch (e) {
                alert(e);
            }
        }

    </script>
    <style type="text/css">
        #form1
        {
        }
        .style38
        {
            font-size: 9pt;
            font-family: verdana;
            color: #515456;
            border: 0px solid #CCCCCC;
            text-align: left;
            padding-left: 4px;
            width: 35%;
            height: 25px;
        }
        .tableclass
        {
            width: 88%;
        }
        
        body
        {
            color: #515456;
        }
    </style>
    <h3 class="heading" style="margin-bottom: 0px;">
        Manual Attendance Entry</h3>
    <table class="TableClass" style="width: 70%; height: 75px; margin-left: 15%;">
        <tr>
            <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;
                width: 1%;">
                <table style="height: 75px; width: 102px;">
                    <tr>
                        <td class="style29">
                            &nbsp;Employee
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" class="style29">
                            <asp:RadioButtonList ID="RbdEmp" runat="server" RepeatLayout="Flow" CssClass="radiobutton"
                                ClientIDMode="Static">
                                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                <asp:ListItem Value="1">Select</asp:ListItem>
                                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="EmployeeHdn" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnEmp" runat="server" ClientIDMode="Static" />
            </td>
            <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;
                width: 7%">
                <table style="height: 75px;">
                    <tr>
                        <td>
                            Company
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:RadioButtonList ID="RbdCompany" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                ClientIDMode="Static">
                                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                <asp:ListItem Value="1">Select</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="ComapnyHdn" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnComp" runat="server" ClientIDMode="Static" />
            </td>
            <td style="width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                border-bottom: lightsteelblue thin solid; font-weight: bold;">
                <table style="height: 75px;">
                    <tr>
                        <td>
                            &nbsp;Location
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:RadioButtonList ID="RbdLocation" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                ClientIDMode="Static">
                                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                <asp:ListItem Value="1">Select</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="LocationHdn" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnLocation" runat="server" ClientIDMode="Static" />
            </td>
            <td style="width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
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
                            <asp:RadioButtonList ID="RbdDivision" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                ClientIDMode="Static">
                                <asp:ListItem Value="0" Selected="True" onclick="showDivision()">All</asp:ListItem>
                                <asp:ListItem Value="1">Select</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="DivisionHdn" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnDiv" runat="server" ClientIDMode="Static" />
            </td>
            <td style="width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                border-bottom: lightsteelblue thin solid; font-weight: bold;">
                <table style="height: 75px;" id="TABLE2">
                    <tr>
                        <td>
                            &nbsp;Department
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:RadioButtonList ID="RbdDepartment" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                ClientIDMode="Static">
                                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                <asp:ListItem Value="1">Select</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="DepartmentHdn" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnDept" runat="server" ClientIDMode="Static" />
            </td>
            <td style="width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                border-bottom: lightsteelblue thin solid; font-weight: bold;">
                <table style="height: 75px;" id="TABLE1">
                    <tr>
                        <td>
                            &nbsp;Category
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:RadioButtonList ID="RbdCategory" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                ClientIDMode="Static">
                                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                <asp:ListItem Value="1">Select</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="ShiftHdn" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnCat" runat="server" ClientIDMode="Static" />
            </td>
        </tr>
    </table>
    <div id="detail" style="display: none;" align="center">
        <table style="width: 60%; margin-left: 15%;">
            <tr>
                <td>
                    <asp:TextBox ID="txtSearchEmp" runat="server" placeholder="Search" Style="display: none"
                        ClientIDMode="Static" Width="250px"></asp:TextBox>
                    <asp:TextBox ID="txtCompany" runat="server" placeholder="Search" Style="display: none"
                        Width="250px"></asp:TextBox>
                    <asp:TextBox ID="txtLocation" runat="server" placeholder="Search" Style="display: none"
                        Width="250px"></asp:TextBox>
                    <asp:TextBox ID="txtDivision" runat="server" placeholder="Search" Style="display: none"
                        Width="250px"></asp:TextBox>
                    <asp:TextBox ID="txtDepartment" runat="server" placeholder="Search" Style="display: none"
                        Width="250px"></asp:TextBox>
                    <asp:TextBox ID="txtShift" runat="server" placeholder="Search" Style="display: none"
                        Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAvailableData" runat="server" Text="Available Data"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblSeletcedData" runat="server" Text="Selected Data"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ListBox ID="LstEmployee" runat="server" ClientIDMode="Static" Style="display: none"
                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                        Width="250px" Height="119px"></asp:ListBox>
                    <asp:ListBox ID="lstEmployDummy" runat="server" ClientIDMode="Static" Style="display: none"
                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                        Width="250px" Height="119px"></asp:ListBox>
                    <asp:ListBox ID="LstCompany" runat="server" ClientIDMode="Static" Style="display: none"
                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                        Width="250px" Height="119px"></asp:ListBox>
                    <asp:ListBox ID="LstLocation" runat="server" ClientIDMode="Static" Style="display: none"
                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                        Width="250px" Height="119px"></asp:ListBox>
                    <asp:ListBox ID="LstDivision" runat="server" ClientIDMode="Static" Style="display: none"
                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                        Width="250px" Height="119px"></asp:ListBox>
                    <asp:ListBox ID="LstDepartment" runat="server" ClientIDMode="Static" Style="display: none"
                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                        Width="250px" Height="119px"></asp:ListBox>
                    <asp:ListBox ID="LstCategory" runat="server" ClientIDMode="Static" Style="display: none"
                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                        Width="250px" Height="119px"></asp:ListBox>
                    <asp:ListBox ID="LstCompanyDummy" runat="server" ClientIDMode="Static" Style="display: none"
                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                        Width="250px" Height="119px"></asp:ListBox>
                    <asp:ListBox ID="LstLocationDummy" runat="server" ClientIDMode="Static" Style="display: none"
                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                        Width="250px" Height="119px"></asp:ListBox>
                    <asp:ListBox ID="LstDivisionDummy" runat="server" ClientIDMode="Static" Style="display: none"
                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                        Width="250px" Height="119px"></asp:ListBox>
                    <asp:ListBox ID="LstDepartmentDummy" runat="server" ClientIDMode="Static" Style="display: none"
                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                        Width="250px" Height="119px"></asp:ListBox>
                    <asp:ListBox ID="LstCategoryDummy" runat="server" ClientIDMode="Static" Style="display: none"
                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                        Width="250px" Height="119px"></asp:ListBox>
                </td>
                <td class="TDClassControl" id="Btntd" clientidmode="Static" style="width: 2%; display: none;"
                    runat="server">
                    <table style="height: 100px">
                        <tr>
                            <td class="TDClassControl">
                                <input id="cmdEntityAllRight" class="ButtonControl" value="&gt;&gt;" style="width: 40px"
                                    type="button" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TDClassControl">
                                <input id="Button1" class="ButtonControl" value="&gt;" style="width: 40px" type="button"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style38">
                                <input id="Button3" class="ButtonControl" value="&lt;" style="width: 40px" type="button"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TDClassControl">
                                <input id="Button2" class="ButtonControl" value="&lt;&lt;" style="width: 40px" type="button"
                                    runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:ListBox ID="LstEntitySelected" runat="server" ClientIDMode="Static" Style="display: none"
                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                        Width="250px" Height="119px"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" class="TDClassControl" style="text-align: center; padding-right: 44px;">
                    <input id="btnOK" class="ButtonControl" style="margin-left: 4%; display: none;" name="Ok"
                        type="button" value="OK" runat="server" />
                    <input id="Button4" class="ButtonControl" style="display: none;" name="Close" type="button"
                        value="Close" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div align="center" style="width: 65%; margin-left: 22%; text-align: center">
        <table id="Table1" runat="server" class="tableclass" width="100%">
            <tr>
                <td>
                </td>
            </tr>
            <tr style="height: 35px;">
                <td class="TDClassLabel">
                    &nbsp;From Date <font color="red">*</font>
                </td>
                <td class="TDClassControl">
                    <asp:TextBox ID="txtfromDate" runat="server" ClientIDMode="Static" MaxLength="10"
                        onchange="chkDate()" onblur="sameDate()"></asp:TextBox>
                    <asp:CalendarExtender ID="txtfromDate_CalendarExtender" TargetControlID="txtfromDate"
                        PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <%--  <asp:RequiredFieldValidator ID="ReqtxtfromDate" runat="server" ControlToValidate="txtfromDate"
                        Display="none" ErrorMessage="Please enter From Date" SetFocusOnError="True" ForeColor="Red"
                        ValidationGroup="Add"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="VCEtxtfromDate" runat="server" TargetControlID="ReqtxtfromDate"
                        PopupPosition="right">
                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                    <%--    <asp:CustomValidator ID="CustomValidatortxtfromDate" runat="server" ClientValidationFunction="isValidateIssueDate"
                        ControlToValidate="txtToDate" Display="none" ErrorMessage="To Date should not be same or less than from date"
                        ForeColor="Red" SetFocusOnError="True"></asp:CustomValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerCusttxtfromDate" runat="server" TargetControlID="CustomValidatortxtfromDate"
                        PopupPosition="left">
                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                </td>
                <td class="TDClassLabel">
                    &nbsp;To Date <font color="red">*</font>
                </td>
               
                <td class="TDClassControl">
                    <asp:TextBox ID="txtToDate" runat="server" ClientIDMode="Static" MaxLength="10" onchange="chkDate()"></asp:TextBox>
                    <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                    <asp:CalendarExtender ID="calFrom" TargetControlID="txtToDate" PopupButtonID="txtshiftStartDate"
                        runat="server" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <%--   <asp:RequiredFieldValidator ID="ReqtxtToDate" runat="server" ControlToValidate="txtToDate"
                        Display="none" ErrorMessage="Please enter To Date" SetFocusOnError="True" ForeColor="Red"
                        ValidationGroup="Add"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="VCEtxtToDate" runat="server" TargetControlID="ReqtxtToDate"
                        PopupPosition="left">
                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td class="TDClassLabel" valign="top">
                    In Time&nbsp;
                </td>
                <td class="TDClassControl" valign="top">
                    <asp:TextBox ID="frm_time" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="5" onkeypress="findspace(event)" onkeyup="fnColon(this,event)" TabIndex="2"></asp:TextBox>
                </td>
                <td class="TDClassLabel" valign="top">
                    Out Time
                </td>
               <td class="TDClassControl" valign="top">
                    <asp:TextBox ID="To_Time" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="5" onkeypress="findspace(event)" onkeyup="fnColon(this,event)" TabIndex="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel" valign="top">
                    Reason ID<font color="red">*</font> <font color="red"></font>
                </td>
                <td class="TDClassControl" valign="top">
                    <asp:DropDownList ID="ddlReason" runat="server" Width="170px" TabIndex="4" ClientIDMode="Static"
                        class="chosen-select">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="ReqddlReason" runat="server" ControlToValidate="ddlReason"
                        Display="none" ErrorMessage="Please enter Reason ID" SetFocusOnError="True" InitialValue="Select One"
                        ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="VCEddlReason" runat="server" TargetControlID="ReqddlReason"
                        PopupPosition="right">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
                <td class="TDClassLabel" valign="top">
                    Remarks
                </td>
                <td class="TDClassControl" valign="top">
                    <asp:TextBox ID="txt_Remarks" CssClass="TextControl" runat="server" MaxLength="50"
                        TextMode="MultiLine" ClientIDMode="Static" onkeyDown="checkLength(this,'50');"
                        onkeyUp="checkLength(this,'50');" Style="resize: none;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel" style="display: none">
                    Sanct id
                </td>
                <td class="TDClassControl" style="display: none">
                    <asp:DropDownList ID="ddSanctionedID" runat="server" CssClass="ComboControl" TabIndex="4"
                        ClientIDMode="Static">
                        <asp:ListItem Value="09056789">xyz</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 35px;">
                <td align="center" colspan="4">
                    <asp:Button ID="CmdOk" runat="server" CssClass="ButtonControl" Text="Save" TabIndex="8"
                        OnClick="CmdOk_Click" OnClientClick=" return ValidateSave();" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" TabIndex="9"
                        OnClick="btnReset_Click" />
                    <asp:Button ID="CmdCancel" runat="server" Text="Cancel" CssClass="ButtonControl"
                        TabIndex="9" CausesValidation="False" OnClick="CmdCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
    <table id="table3" runat="server" cellpadding="3" cellspacing="3" width="100%">
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text=""></asp:Label>
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
