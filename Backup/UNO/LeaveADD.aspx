<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ModuleMain.master"
    CodeBehind="LeaveADD.aspx.cs" Inherits="UNO.LeaveADD" Culture="en-GB" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <title></title>
    <link href="Styles/Style.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript">
        var OkFlag = "False";




        function isValidateIssueDate(oSrc, args) {

            if (!CompareDates(document.getElementById('txtfromDate'), document.getElementById('txtToDate'))) {
                //alert("Post dated Reports can not be viewed");
                // document.getElementById('txtholidaydate').select();
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }


        }
        function CompareDates(d1, d2) {
            var start = d1.value.toUpperCase();
            var end = d2.value.toUpperCase();
            var arrDate = start.split('/');
            var arrDate1 = end.split('/');
            var d1 = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
            var d2 = new Date(arrDate1[2], arrDate1[1] - 1, arrDate1[0]);

            if (d2 < d1) {
                document.getElementById('<%= CustomValidatortxtfromDate.ClientID %>').innerHTML = "To Date should not be less than From date"

                return false;
            }

            return true;
        }




        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);

            if (textBox.value.length > maxLength - 1) {
                if (window.event)//IE
                {
                    e.returnValue = false;
                    return false;
                }
                else//Firefox
                    e.preventDefault();
            }

        }

        function ValidateSave() {

            var first = "";
            var count = 0;
            var emp = document.getElementById('<%=EmployeeHdn.ClientID%>').value;
            var com = document.getElementById('<%=ComapnyHdn.ClientID%>').value;
            var loc = document.getElementById('<%=LocationHdn.ClientID %>').value;
            var div = document.getElementById('<%=DivisionHdn.ClientID %>').value;
            var dep = document.getElementById('<%=DepartmentHdn.ClientID %>').value;
            var sft = document.getElementById('<%=ShiftHdn.ClientID%>').value;
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
                document.getElementById('<%=lblMessage.ClientID%>').innerHTML = 'Please enter From Date';
                return false;
            }
             if (document.getElementById('<%=txtToDate.ClientID%>').value == '') {
                document.getElementById('<%=lblMessage.ClientID%>').innerHTML = 'Please enter To Date';
                return false;
            }
            if (!CompareDates(document.getElementById('<%=txtfromDate.ClientID%>'), document.getElementById('<%=txtToDate.ClientID%>'))) {
                document.getElementById('<%= lblMessage.ClientID %>').innerHTML = "To Date should not be less than From date";
                return false;
            }
            if ($("#<%=ddleaveType.ClientID%> option:selected").val() == 'Select One') {
                document.getElementById('<%=lblMessage.ClientID%>').innerHTML = 'Please select Leave Code';
                return false;
            }

            if ($("#<%=ddlReason.ClientID%> option:selected").val() == 'Select One') {
                document.getElementById('<%=lblMessage.ClientID%>').innerHTML = 'Please select Reason Id';
                return false;
            }
            return true;
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
        Leave Entry</h3>
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
                                ClientIDMode="Static" OnSelectedIndexChanged="RbdEmp_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                <asp:ListItem Value="1">Select</asp:ListItem>
                                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="EmployeeHdn" runat="server" ClientIDMode="Static" />
            </td>
            <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;
                width: 7%">
                <table style="height: 75px;">
                    <tr>
                        <td>
                            Company&nbsp;
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
            </td>
            <td style="width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
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
                            <asp:RadioButtonList ID="RbdDivision" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                ClientIDMode="Static">
                                <asp:ListItem Value="0" Selected="True" onclick="showDivision()">All</asp:ListItem>
                                <asp:ListItem Value="1">Select</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="DivisionHdn" runat="server" ClientIDMode="Static" />
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
            </td>
        </tr>
    </table>
    <div id="detail" style="display: none">
        <table id="table" style="width: 60%; margin-left: 15%;" style="display: none">
            <tr>
                <td>
                    <asp:TextBox ID="txtSearchEmp" runat="server" placeholder="Search" Style="display: none"
                        ClientIDMode="Static" Width="250px"></asp:TextBox>
                    <asp:TextBox ID="txtCompany" runat="server" placeholder="Search" Width="250px"></asp:TextBox>
                    <asp:TextBox ID="txtLocation" runat="server" placeholder="Search" Width="250px"></asp:TextBox>
                    <asp:TextBox ID="txtDivision" runat="server" placeholder="Search" Width="250px"></asp:TextBox>
                    <asp:TextBox ID="txtShift" runat="server" placeholder="Search" Width="250px"></asp:TextBox>
                    <asp:TextBox ID="txtDepartment" runat="server" placeholder="Search" Width="250px"></asp:TextBox>
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
                <td class="TDClassControl" id="Btntd" clientidmode="Static" style="width: 14%; display: none;"
                    runat="server">
                    <table style="height: 100px">
                        <tr>
                            <td class="TDClassControl">
                                <input id="cmdEntityAllRight" class="ButtonControl" runat="server" value="&gt;&gt;"
                                    type="button" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TDClassControl">
                                <input id="Button1" class="ButtonControl" value=" &gt; " runat="server" type="button" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style38">
                                <input id="Button3" class="ButtonControl" value=" &lt; " runat="server" type="button" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TDClassControl">
                                <input id="Button2" class="ButtonControl" value="&lt;&lt;" type="button" runat="server" />
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
        </table>
    </div>
    <table runat="server" cellspacing="5" class="tableclass" style="margin-left: 10%;
        width: 76%;">
        <tr>
            <td colspan="4" class="TDClassControl" style="text-align: center; padding-right: 44px;">
                <input id="btnOK" class="ButtonControl" style="width: 7%; margin-left: 4%; display: none;"
                    name="Ok" type="button" value="OK" runat="server" />
                <input id="Button4" class="ButtonControl" style="width: 7%; display: none;" name="Close"
                    type="button" value="Close" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="TDClassLabel">
                &nbsp;From Date <font color="red">*</font>
            </td>
            <td class="TDClassControl">
                <asp:TextBox ID="txtfromDate" runat="server" ClientIDMode="Static" MaxLength="10"
                    ValidationGroup="Add"></asp:TextBox>
                <asp:CalendarExtender ID="txtfromDate_CalendarExtender" TargetControlID="txtfromDate"
                    PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="retxtfromDate" runat="server" ControlToValidate="txtfromDate"
                    Display="none" ErrorMessage="Please enter From Date" SetFocusOnError="True" ForeColor="Red"
                    ValidationGroup="Add"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="vcertxtfromDate" runat="server" TargetControlID="retxtfromDate"
                    PopupPosition="Right">
                </ajaxToolkit:ValidatorCalloutExtender>
                <asp:CustomValidator ID="CustomValidatortxtfromDate" runat="server" ClientValidationFunction="isValidateIssueDate"
                    ControlToValidate="txtToDate" Display="none" ErrorMessage="To Date should not be same or less than from date"
                    ForeColor="Red" SetFocusOnError="True" ValidationGroup="Add"></asp:CustomValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="vcerCusttxtfromDate" runat="server" TargetControlID="CustomValidatortxtfromDate"
                    PopupPosition="left">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
            <td class="TDClassLabel">
                &nbsp;To Date <font color="red">*</font>
            </td>
            <td class="TDClassControl">
                <asp:TextBox ID="txtToDate" runat="server" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                <asp:CalendarExtender ID="calFrom" TargetControlID="txtToDate" PopupButtonID="txtshiftStartDate"
                    runat="server" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="ReqtxtToDate" runat="server" ControlToValidate="txtToDate"
                    Display="none" ErrorMessage="Please enter To Date" SetFocusOnError="True" ForeColor="Red"
                    ValidationGroup="Add"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="VCEtxtToDate" runat="server" TargetControlID="ReqtxtToDate"
                    PopupPosition="left">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td class="TDClassLabel">
                Leave Code <font color="red">*</font>&nbsp;
            </td>
            <td class="TDClassControl">
                <asp:DropDownList ID="ddleaveType" runat="server" class="chosen-select" TabIndex="4"
                    Width="173px" ClientIDMode="Static">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="ReqddleaveType" runat="server" ControlToValidate="ddleaveType"
                    Display="none" ErrorMessage="Please enter Leave Code" SetFocusOnError="True"
                    InitialValue="Select One" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="VCEddleaveType" runat="server" TargetControlID="ReqddleaveType"
                    PopupPosition="Right">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
            <td class="TDClassLabel">
                Reason Code <font color="red">*</font>
            </td>
            <td class="TDClassControl">
                <asp:DropDownList ID="ddlReason" runat="server" class="chosen-select" Width="173px"
                    TabIndex="4" ClientIDMode="Static">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="ReqddlReason" runat="server" ControlToValidate="ddlReason"
                    Display="none" ErrorMessage="Please enter Reason Code" SetFocusOnError="True"
                    InitialValue="Select One" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="VCEddlReason" runat="server" TargetControlID="ReqddlReason"
                    PopupPosition="left">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td class="TDClassLabel" style="display: none">
                Sanctioned ID
            </td>
            <td class="TDClassControl" style="display: none">
                <asp:DropDownList ID="ddSanctionedID" runat="server" class="chosen-select" TabIndex="4"
                    Width="173px" ClientIDMode="Static">
                    <asp:ListItem Value="09056789">xyz</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="TDClassLabel" valign="top" >
                Remarks
            </td>
            <td class="TDClassControl">
                <asp:TextBox ID="txt_Remarks" CssClass="TextControl" runat="server" Style="resize:none;Width:173px;"
                    TabIndex="3" Height="50px" MaxLength="50" TextMode="MultiLine" onkeyDown="checkLength(this,'50');"
                    onkeyUp="checkLength(this,'50');"></asp:TextBox>
            </td>
            <td class="TDClassLabel">
                <asp:RadioButton ID="rdbtnHalf" runat="server" Text="Half Day" GroupName="A" />
            </td>
            <td class="TDClassControl">
                <asp:RadioButton ID="rdbtnFull" runat="server" Text="Full Day" GroupName="A" />
            </td>
        </tr>
    </table>
    <table id="table3" runat="server" width="76%" align="center">
        <tr>
            <td style="width: 7%">
            </td>
            <td>
            </td>
            <td align="right">
                <asp:Button ID="CmdOk" runat="server" CssClass="ButtonControl" Text="Save" 
                    TabIndex="8" OnClick="CmdOk_Click" CausesValidation="true"  OnClientClick="return ValidateSave();" />
                <asp:Button ID="btnReset" runat="server" CssClass="ButtonControl" Text="Reset" TabIndex="8"
                    OnClientClick="location.reload();" />
            </td>
            <td align="left">
                <asp:Button ID="CmdCancel" runat="server" Text="Cancel" CssClass="ButtonControl"
                    TabIndex="9" CausesValidation="False" OnClick="CmdCancel_Click" />
            </td>
        </tr>
    </table>
    <div style="width: 100%; text-align: center">
        <asp:Label ID="lblMessage" runat="server" Text="" Style="color: red"></asp:Label>
    </div>
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
