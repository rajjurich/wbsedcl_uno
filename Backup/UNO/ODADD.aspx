<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ModuleMain.master"
    CodeBehind="ODADD.aspx.cs" Inherits="UNO.ODADD" Culture="en-GB" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--   <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">--%>
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
        .tdstyle
        {
            font-family: Verdana;
            font-size: 9pt;
            color: #515456;
            border: 0px solid #CCC;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <title></title>
    <link href="Styles/Style.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript">
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
                document.getElementById('<%=lblMessages.ClientID%>').innerHTML = 'Please enter From Date';
                return false;
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value == '') {
                document.getElementById('<%=lblMessages.ClientID%>').innerHTML = 'Please enter To Date';
                return false;
            }
            if (!CompareDates(document.getElementById('<%=txtfromDate.ClientID%>'), document.getElementById('<%=txtToDate.ClientID%>'))) {
                document.getElementById('<%= lblMessages.ClientID %>').innerHTML = "To Date should not be less than From date";
                return false;
            }

            if ($("#<%=ddlReason.ClientID%> option:selected").val() == 'Select One') {
                document.getElementById('<%=lblMessages.ClientID%>').innerHTML = 'Please select Reason Id';
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
        OD Entry</h3>
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
                                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
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
        <table style="width: 70%; margin-left: 15%;">
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
                <td class="TDClassControl" id="Btntd" clientidmode="Static" style="width: 14%; display: none;"
                    runat="server">
                    <table style="height: 100px">
                        <tr>
                            <td class="TDClassControl">
                                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                <input id="cmdEntityAllRight" class="ButtonControl" runat="server" value="&gt;&gt;"
                                    type="button" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TDClassControl">
                                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                <input id="Button1" class="ButtonControl" value=" &gt; " runat="server" type="button" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style38">
                                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                <input id="Button3" class="ButtonControl" value=" &lt; " runat="server" type="button" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TDClassControl">
                                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                <input id="Button2" class="ButtonControl" value="&lt;&lt;" runat="server" type="button" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:ListBox ID="LstEntitySelected" runat="server" ClientIDMode="Static" Style="display: none"
                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                        Width="250px" Height="119px"></asp:ListBox>
                </td>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="TDClassControl" style="text-align: center; padding-right: 44px;">
                        <input id="btnOK" class="ButtonControl" style="margin-left: 4%; display: none;" name="Ok"
                            type="button" value="OK" runat="server" />
                        <input id="Button4" class="ButtonControl" style="display: none;" name="Close" type="button"
                            value="Close" runat="server" />
                    </td>
                </tr>
        </table>
    </div>
    <table runat="server" class="tableclass" style="margin-left: 20%; width: 50%;" align="center">
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="TDClassLabel">
                &nbsp;From Date<font color="red">*</font>
            </td>
            <td class="TDClassControl">
                <asp:TextBox ID="txtfromDate" runat="server" ClientIDMode="Static" MaxLength="10"
                    onkeyPress="javascript: return false"></asp:TextBox>
                <asp:CalendarExtender ID="txtfromDate_CalendarExtender" TargetControlID="txtfromDate"
                    PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
            <td class="tdstyle">
                &nbsp;To Date<font color="red">*</font>
            </td>
            <td class="TDClassControl">
                <asp:TextBox ID="txtToDate" runat="server" ClientIDMode="Static" MaxLength="10" onkeyPress="javascript: return false"></asp:TextBox>
                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                <asp:CalendarExtender ID="calFrom" TargetControlID="txtToDate" PopupButtonID="txtshiftStartDate"
                    runat="server" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr style="height: 35px;">
            <td class="TDClassLabel">
                Reason Code<font color="red">*</font>
            </td>
            <td class="TDClassControl">
                <asp:DropDownList ID="ddlReason" runat="server" class="chosen-select" Width="173px"
                    TabIndex="4" ClientIDMode="Static">
                </asp:DropDownList>
            </td>
            <td class="TDClassLabel" style="display: none">
                Sanctioned ID
            </td>
            <td class="TDClassControl" style="display: none">
                <asp:DropDownList ID="ddSanctionedID" runat="server" CssClass="ComboControl" TabIndex="4"
                    ClientIDMode="Static">
                    <asp:ListItem Value="09056789">xyz</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="TDClassLabel">
                Remarks
            </td>
            <td class="TDClassControl">
                <asp:TextBox ID="txt_Remarks" CssClass="TextControl" runat="server" Style="width: 200px;
                    height: 60px; max-height: 60px; min-height: 60px; max-width: 200px; min-width: 200px;"
                    TabIndex="3" Height="50px" MaxLength="50" TextMode="MultiLine" onkeyDown="checkLength(this,'50');"
                    onkeyUp="checkLength(this,'50');"></asp:TextBox>
            </td>
            <td class="TDClassLabel">
                &nbsp;
            </td>
            <td class="TDClassControl">
                &nbsp;
            </td>
        </tr>
    </table>
    <table id="table3" runat="server" cellpadding="3" cellspacing="3" style="margin-left: 10%;
        width: 80%;" align="center">
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="CmdOk" runat="server" CssClass="ButtonControl" Text="Save" TabIndex="8"
                    OnClick="CmdOk_Click" OnClientClick="return ValidateSave();" />
                <asp:Button ID="btnReset" runat="server" CssClass="ButtonControl" Text="Reset" TabIndex="8"
                    OnClientClick="location.reload();" />
                <asp:Button ID="CmdCancel" runat="server" Text="Cancel" CssClass="ButtonControl"
                    TabIndex="9" CausesValidation="False" OnClick="CmdCancel_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center;">
                <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
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
