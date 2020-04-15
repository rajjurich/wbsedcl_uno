<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="TemporaryCardMaster.aspx.cs" Inherits="UNO.TemporaryCardMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="Scripts/calendar.js"></script>
    <link rel='stylesheet' href="Styles/calendar.css" title='calendar'>
    <%--<script runat="server">
        protected void btnDoWork_Click(object sender, EventArgs e)
        {
            /// do something long lasting
            System.Threading.Thread.Sleep(300);
        }
</script>--%>
    <script language="javascript" type="text/javascript">
        //window.onload = display;


        function fnSetDateFormat(oDateFormat) {
            oDateFormat['FullYear']; 	//Example = 2007
            oDateFormat['Year']; 		//Example = 07
            oDateFormat['FullMonthName']; //Example = January
            oDateFormat['MonthName']; 	//Example = Jan
            oDateFormat['Month']; 		//Example = 01
            oDateFormat['Date']; 		//Example = 01
            oDateFormat['FullDay']; 		//Example = Sunday
            oDateFormat['Day']; 			//Example = Sun
            oDateFormat['Hours']; 		//Example = 01
            oDateFormat['Minutes']; 		//Example = 01
            oDateFormat['Seconds']; 		//Example = 01

            var sDateString;

            //Example = 01/01/00  dd/mm/yy
            //sDateString = oDateFormat['Date'] +"/"+ oDateFormat['Month'] +"/"+ oDateFormat['Year'];		

            //Example = 01/01/0000  dd/mm/yyyy
            //Commented by
            sDateString = oDateFormat['Date'] + "/" + oDateFormat['Month'] + "/" + oDateFormat['FullYear'];

            //Example = 0000-01-01 yyyy/mm/dd
            //sDateString = oDateFormat['FullYear'] +"-"+ oDateFormat['Month'] +"-"+ oDateFormat['Date'];

            //Example = Jan-01-0000 Mmm/dd/yyyy
            //sDateString = oDateFormat['MonthName'] +"-"+ oDateFormat['Date'] +"-"+ oDateFormat['FullYear'];

            // sDateString = oDateFormat['Month'] + "/" + oDateFormat['Date'] + "/" + oDateFormat['FullYear'];

            return sDateString;
        }

        function ValidateData5() {
            //            if (!(validateString(document.getElementById('txtID'), "Please enter ID."))) {
            //                return false
            //            }
            //            if (!(validateString(document.getElementById('txtDesc'), "Please enter Description."))) {
            //                return false
            //            }
            if (handleAdd() == false) {
                return false
            }
        }

        function CheckComp() {
            var str = null;
            var value = null;
            var n = null;
            var m = null;
            document.getElementById('txtDesc').value = "";
            str = "PR" + document.getElementById('txtDesc').value;
            if (document.getElementById('ChkWO').checked) {

                value = "WC";
                if (str.match(value)) {

                    //                 n = str.replace(value,"");
                }
                else {
                    str = str + value;
                }
                document.getElementById('txtDesc').value = str;
            }
            else {
                if (!document.getElementById('ChkWO').checked) {
                    value = "WC";
                    if (str.match(value)) {
                        m = str.search(value);
                        if (str.length == 2) {
                            str = "";
                        }
                        else {
                            m = str.search(value);
                            var u = str.substr(m, 2);
                            str = str.replace(u, '');
                        }
                    }
                    document.getElementById('txtDesc').value = str;
                }
            }
            if (document.getElementById('ChkPL').checked) {

                value = "PC";
                if (str.match(value)) {

                    //                 n = str.replace(value,"");
                }
                else {
                    str = str + value;
                }
                document.getElementById('txtDesc').value = str;
            }
            else {
                if (!document.getElementById('ChkPL').checked) {
                    value = "PC";
                    if (str.match(value)) {
                        m = str.search(value);
                        if (str.length == 2) {
                            str = "";
                        }
                        else {
                            m = str.search(value);
                            var u = str.substr(m, 2);
                            str = str.replace(u, '');
                        }
                    }
                    document.getElementById('txtDesc').value = str;
                }
            }
            if (document.getElementById('ChkHO').checked) {

                value = "HC";
                if (str.match(value)) {

                    //                 n = str.replace(value,"");
                }
                else {
                    str = str + value;
                }
                document.getElementById('txtDesc').value = str;
            }
            else {
                if (!document.getElementById('ChkHO').checked) {
                    value = "HC";
                    if (str.match(value)) {
                        m = str.search(value);
                        if (str.length == 2) {
                            str = "";
                        }
                        else {
                            m = str.search(value);
                            var u = str.substr(m, 2);
                            str = str.replace(u, '');
                        }
                    }
                    document.getElementById('txtDesc').value = str;
                }
            }

            if (document.getElementById('ChkHO').checked && document.getElementById('ChkWO').checked && document.getElementById('ChkPL').checked) {
                document.getElementById('txtDesc').value = "ALL";

            }
            if (!document.getElementById('ChkHO').checked && !document.getElementById('ChkWO').checked && !document.getElementById('ChkPL').checked) {
                document.getElementById('txtDesc').value = "PR";

            }
        }

        function HideCtrl(ctrl, timer) {
            var ctry_array = ctrl.split(",");
            var num = 0, arr_length = ctry_array.length;
            while (num < arr_length) {
                if (document.getElementById(ctry_array[num])) {
                    setTimeout('document.getElementById("' + ctry_array[num] + '").style.display = "none";', timer);
                } s
                num += 1;
            }
            return false;
        }
        function returnviewmode() {
            document.getElementById('Label1').innerHTML = "&nbsp;&nbsp;";
            window.location = "WorkDayviewMaster.aspx";
        }
        function DltConfirmationbox() {
            var result = confirm('Are you sure you want to delete selected User(s)?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }


        function clearFunction() {
            // document.getElementById(x).innerHTML = "&nbsp;&nbsp;";
            //            document.getElementById('lstFile').value = "Select One";
            document.getElementById('txtTempCardID').value = "";
            document.getElementById('txtEmpCd').value = "";
            document.getElementById('txtIssueDate').value = "";
            document.getElementById('txtReturnDate').value = "";
            document.getElementById('ddlReason').value = "Select One";


        }

        function handleAdd() {
            if (!Page_ClientValidate())
                return;

            var msg = confirm('Save Record?');

            if (msg == false) {
                return false;
            }
        }
        function HideLabel(labelID) {
            setTimeout("HideLabelHelper('" + labelID + "');", 3000);
        }
        function HideLabelHelper(labelID) {
            document.getElementById(labelID).style.display = "none";
        }
        function HideLabelHelper(labelID) {
            document.getElementById(labelID).style.display = "none";
        }
    </script>
    <br />
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Temporary Card" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <asp:Table ID="Table1" runat="server" HorizontalAlign="Right" TabIndex="7">
        <asp:TableRow>
            <asp:TableCell>
                <asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="~/TemporaryCardViewMaster.aspx"
                    ForeColor="Blue">Back to view Mode</asp:HyperLink>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <br />
    <asp:Panel ID="DoorConfig" ClientIDMode="Static" runat="server" Width="95%" BorderWidth="1"
        BorderColor="Gray" HorizontalAlign="Center" align="Center" CssClass="srcColor">
        <br />
        <table id="TblInfo" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
            class="TableClass">
            <tr>
                <td class="TDClassLabel" align="right" style="width:50%;">
                    <asp:Label ID="LblID" runat="server" Font-Bold="true">Temporary Card Code :</asp:Label><label
                        class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl" align="right" style="width:50%;">
                    <asp:TextBox ID="txtTempCardID" Style="text-transform: uppercase" ClientIDMode="Static"
                        runat="server" Font-Bold="true" MaxLength="8" onkeypress="return IsAlphanumericWithoutspace(event)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1_empid" runat="server" ControlToValidate="txtTempCardID"
                        Display="Dynamic" ErrorMessage="Please enter Code." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel" align="right">
                    <asp:Label ID="Label2" runat="server" Font-Bold="true">Employee Code :</asp:Label><label
                        class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl" align="right">
                    <asp:TextBox ID="txtEmpCd" Style="text-transform: uppercase" ClientIDMode="Static"
                        runat="server" Font-Bold="False" MaxLength="8" onkeypress="return IsAlphanumericWithoutspace(event)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpCd"
                        Display="Dynamic" ErrorMessage="Please enter Code." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel" align="right">
                    <asp:Label ID="Label3" runat="server" Font-Bold="true">Issue Date :</asp:Label><label
                        class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl" align="right">
                    <asp:TextBox ID="txtIssueDate" runat="server" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                    <img onmouseover="fnInitCalendar(this, 'txtIssueDate', 'style=calendar.css,close=true')"
                        src="images/calendar.gif" />
                    <asp:RequiredFieldValidator ID="rfvRequestDate" runat="server" ControlToValidate="txtIssueDate"
                        Display="Dynamic" ErrorMessage="Please enter Date" ForeColor="Red" SetFocusOnError="True"
                        ValidationGroup="ODdetails"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel" align="right">
                    <asp:Label ID="Label4" runat="server" Font-Bold="true">Return Date :</asp:Label><label
                        class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl" align="right">
                    <asp:TextBox ID="txtReturnDate" runat="server" ClientIDMode="Static" MaxLength="10"></asp:TextBox><img
                        onmouseover="fnInitCalendar(this, 'txtReturnDate', 'style=calendar.css,close=true')"
                        src="images/calendar.gif" />
                    <asp:RequiredFieldValidator ID="rfvRequestDate0" runat="server" ControlToValidate="txtReturnDate"
                        Display="Dynamic" ErrorMessage="Please enter Date" ForeColor="Red" SetFocusOnError="True"
                        ValidationGroup="ODdetails"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel" align="right">
                    <asp:Label ID="Label5" runat="server" Font-Bold="true">Reason For Issue :</asp:Label><label
                        class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl" align="right">
                    <asp:DropDownList ID="ddlReason" runat="server" ClientIDMode="Static" CssClass="ComboControl">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1_empid11" runat="server" ControlToValidate="ddlReason"
                        Display="Dynamic" ErrorMessage="Please select Reason" ForeColor="Red" InitialValue="Select One"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <table id="TblBtn" runat="server" cellpadding="3" cellspacing="3" width="95%">
            <tr>
                <td style="text-align: center; width:100%;">
                    <asp:Button ID="BtnAdd" Width="60px" Font-Bold="true" Text="Save" OnClick="BtnAdd_Click"
                        TabIndex="5" OnClientClick="return ValidateData5()" runat="server" CssClass="ButtonControl" />
                    &nbsp;
                    <asp:Button ID="BtnCancel" Width="60px" Font-Bold="true" Text="Cancel" TabIndex="6"
                        OnClick="BtnCancel_Click" runat="server" CssClass="ButtonControl" CausesValidation="False" />
                </td>
                <%-- <td></td>--%>
            </tr>
        </table>
        <asp:Panel runat="server" HorizontalAlign="Center">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Black" TabIndex="7"></asp:Label></asp:Panel>
    </asp:Panel>
    <br />
</asp:Content>
