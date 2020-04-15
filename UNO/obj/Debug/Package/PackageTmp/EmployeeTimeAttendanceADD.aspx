<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ModuleMain.master"
    UICulture="en-GB" CodeBehind="EmployeeTimeAttendanceADD.aspx.cs" Inherits="UNO.EmployeeTimeAttendanceADD"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title></title>
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.PnlFilterListBox.ClientID%>');

            // get the bounds of both the gridview and the progress div
            var gridViewBounds = Sys.UI.DomElement.getBounds(gridView);
            var updateProgressDivBounds = Sys.UI.DomElement.getBounds(updateProgressDiv);

            //    do the math to figure out where to position the element (the center of the gridview)
            var x = gridViewBounds.x + Math.round(gridViewBounds.width / 2) - Math.round(updateProgressDivBounds.width / 2);
            var y = gridViewBounds.y + Math.round(gridViewBounds.height / 2) - Math.round(updateProgressDivBounds.height / 2);

            //    set the progress element to this position
            Sys.UI.DomElement.setLocation(updateProgressDiv, x, y);
        }

        function onUpdated() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it invisible
            updateProgressDiv.style.display = 'none';
        }
       

    </script>
    <script language="javascript" type="text/javascript">



        function MTimer() {
            var mT;
            mT = setTimeout(ClearMessage, 3000);
        }

        function ClearMessage() {

            document.getElementById('lblMsg').innerHTML = " ";
        }

        function RecSave(iCount) {
            document.getElementById('lblMsg').innerHTML = iCount + " Records(s) Saved.";
            MTimer();
            PageRefresh();
        }


        function IsAlphanumeric(evnt) {


            var charCode = (evnt.which) ? evnt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) || (charCode >= 48 && charCode <= 57) ||
			(charCode >= 97 && charCode <= 122) || (charCode == 8) || (charCode == 32)) {
                return true
            }
            else {
                return false
            }
        }

        function IsNumericSwipe(evnt) {

            var charCode = (evnt.which) ? evnt.which : event.keyCode
            if ((charCode >= 48 && charCode <= 57) || (charCode == 8) || (charCode == 32)) {
                return true
            }
            else {
                return false
            }
        }

        function PageRefresh() {

            // by vaibhav
            document.getElementById('RbdEmp_0').checked = false;
            document.getElementById('RbdCompany_0').checked = false;
            document.getElementById('RbdLocation_0').checked = false;
            document.getElementById('RbdDivision_0').checked = false;
            document.getElementById('RbdDepartment_0').checked = false;
            document.getElementById('RbdCategory_0').checked = false;

            document.getElementById('RbdEmp_1').checked = false;
            document.getElementById('RbdCompany_1').checked = false;
            document.getElementById('RbdLocation_1').checked = false;
            document.getElementById('RbdDivision_1').checked = false;
            document.getElementById('RbdDepartment_1').checked = false;
            document.getElementById('RbdCategory_1').checked = false;
            ///vaibhav end

            document.getElementById('RbdCompany_0').disabled = false;
            document.getElementById('RbdCompany_1').disabled = false;

            document.getElementById('RbdLocation_0').disabled = false;
            document.getElementById('RbdLocation_1').disabled = false;

            document.getElementById('RbdDivision_0').disabled = false;
            document.getElementById('RbdDivision_1').disabled = false;

            document.getElementById('RbdDepartment_0').disabled = false;
            document.getElementById('RbdDepartment_1').disabled = false;

            document.getElementById('RbdCategory_0').disabled = false;
            document.getElementById('RbdCategory_1').disabled = false;

            document.getElementById('txtshiftStartDate').value = "";
            document.getElementById('txtMinimumSwipe').value = "";
            document.getElementById('ddweekoff').selectedIndex = 0;
            document.getElementById('ddWeekend').selectedIndex = 0;
            document.getElementById('EmployeeHdn').value = "";
            document.getElementById('ComapnyHdn').value = "";
            document.getElementById('LocationHdn').value = "";
            document.getElementById('DivisionHdn').value = "";
            document.getElementById('DepartmentHdn').value = "";
            document.getElementById('ShiftHdn').value = "";

            document.getElementById('rblScheduleType_1').checked = true;


        }


        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }

        function backtoViewMode() {
            window.location = "EmployeeTimeAttendanceView.aspx"

        }

      


    </script>
    <script language="javascript" type="text/javascript">


        function pageloadShift() {


            document.getElementById('<%=lblShiftType.ClientID%>').style.display = "block";

            document.getElementById('<%=ddlShiftPattern.ClientID%>').style.display = "none";

            document.getElementById('<%=ddlShift.ClientID%>').style.display = "block"


        }

        //Added by Pooja Yadav
        function GetSelectedValue() {
            var list = document.getElementById("<% =rblScheduleType.ClientID %>");
            var rdbtnLstValues = list.getElementsByTagName("input");
            var Checkdvalue;
            for (var i = 0; i < rdbtnLstValues.length; i++) {
                if (rdbtnLstValues[parseInt(i)].checked) {
                    Checkdvalue = rdbtnLstValues[parseInt(i)].value;
                    break;
                }
            }
            if (Checkdvalue == "Pattern") {
                FillPattern();
                ckhForDaily();
            }
            else if (Checkdvalue == "Fixed") {
                FillFixed();
            }
        }

        function FillFixed() {

            document.getElementById('<%=lblShiftType.ClientID%>').style.display = "block";
            document.getElementById('<%=ddlShiftPattern.ClientID%>').style.display = "none"
            document.getElementById('<%=ddlShift.ClientID%>').style.display = "block";
            $("#<%= ddweekoff.ClientID%> ").prop("disabled", false);
            $("#<%= ddWeekend.ClientID%>").prop("disabled", false);
        }


        function FillPattern() {

            document.getElementById('<%=lblShiftType.ClientID%>').style.display = "block";
            document.getElementById('<%=ddlShiftPattern.ClientID%>').style.display = "block";
            document.getElementById('<%=ddlShift.ClientID%>').style.display = "none"
        }

        function ckhForDaily() {
            var value = $("#<%= ddlShiftPattern.ClientID%> option:selected").text();
            var seperateby = value.lastIndexOf("-");
            //gets only the extension
            var ddlVal = value.substring(seperateby);

            if (ddlVal.slice(-5) == 'DAILY') {
                $("#<%= ddweekoff.ClientID%> ").prop("disabled", true);
                $("#<%= ddWeekend.ClientID%>").prop("disabled", true);

            }
            else {
                $("#<%= ddweekoff.ClientID%> ").prop("disabled", false);
                $("#<%= ddWeekend.ClientID%>").prop("disabled", false);
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
            var list = document.getElementById("<% =rblScheduleType.ClientID %>");
            var rdbtnLstValues = list.getElementsByTagName("input");
            var Checkdvalue;
            for (var i = 0; i < rdbtnLstValues.length; i++) {
                if (rdbtnLstValues[parseInt(i)].checked) {
                    Checkdvalue = rdbtnLstValues[parseInt(i)].value;
                    break;
                }
            }

//            if (document.getElementById('<%=txtshiftStartDate.ClientID%>').value == '') {
//                document.getElementById('<%=lblMsg.ClientID%>').innerHTML = 'Please enter Shift Start Date';
//                return false;
//            }


             if (document.getElementById('<%=txtMinimumSwipe.ClientID%>').value == '') {
                document.getElementById('<%=lblMsg.ClientID%>').innerHTML = 'Please enter Minimum Swipe';
                return false;
            }
            if (Checkdvalue == "Pattern") {
                if ($("#<%=ddlShiftPattern.ClientID%> option:selected").val() == '0') {
                    document.getElementById('<%=lblMsg.ClientID%>').innerHTML = 'Please select shift';
                    return false;
                }

            }
            if (Checkdvalue == "Fixed") {
                if ($("#<%=ddlShift.ClientID%> option:selected").val() == '0') {
                    document.getElementById('<%=lblMsg.ClientID%>').innerHTML = 'Please select shift';
                    return false;
                }
            }
            document.getElementById('<%=lblMsg.ClientID%>').innerHTML = "";
            return true;
        }


    </script>
    <style type="text/css">
        body
        {
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblHederText" runat="server" Text="Attendance Configuration" CssClass="heading"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlFilter" runat="server">
        <table class="TableClass" style="width: 70%; height: 75px; margin-left: 15%;">
            <tr>
                <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;
                    width: 1%;">
                    <table style="height: 75px; width: 102px;">
                        <tr>
                            <td>
                                &nbsp;Employee
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
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
                </td>
                <td style="width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
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
    </asp:Panel>
    <asp:Panel ID="PnlFilterListBox" runat="server">
        <div id="detail" style="display: none">
            <table id="table" style="width: 70%; margin-left: 15%;" style="display: none">
                <tr>
                    <td>
                        <asp:TextBox ID="txtSearchEmp" runat="server" placeholder="Search" Style="display: none"
                            ClientIDMode="Static" Width="250px"></asp:TextBox>
                        <asp:TextBox ID="txtCompany" runat="server" placeholder="Search" Width="250px"></asp:TextBox>
                        <asp:TextBox ID="txtLocation" runat="server" placeholder="Search" Width="250px"></asp:TextBox>
                        <asp:TextBox ID="txtDivision" runat="server" placeholder="Search" Width="250px"></asp:TextBox>
                        <asp:TextBox ID="txtDepartment" runat="server" placeholder="Search" Width="250px"></asp:TextBox>
                        <asp:TextBox ID="txtShift" runat="server" placeholder="Search" Width="250px"></asp:TextBox>
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
                    <td id="Btntd" clientidmode="Static" style="display: none;" runat="server">
                        <table style="height: 100px">
                            <tr>
                                <td align="left">
                                    <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                    <input id="cmdEntityAllRight" class="ButtonControl" value="&gt;&gt;" style="width: 50px"
                                        type="button" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                    <input id="cmdEntityRight" class="ButtonControl" value="&gt;" style="width: 50px"
                                        type="button" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                    <input id="cmdEntityLeft" class="ButtonControl" value="&lt;" style="width: 50px"
                                        type="button" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                    <input id="cmdEntityAllLeft" class="ButtonControl" value="&lt;&lt;" style="width: 50px"
                                        type="button" runat="server" />
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
                    </td>
                    <td>
                        <asp:Button ID="btnOK" runat="server" Text="OK" class="ButtonControl" ClientIDMode="Static"
                            Style="width: 50px" OnClick="okclick1" ValidationGroup="xyz" />
                        <%--<input id="btnClose" class="ButtonControl" style="display: none; width: 50px" name="Close"
                        type="button" value="Close" onclick="closeclick()" />--%>
                        <asp:Button ID="btnClose" runat="server" Text="Close" class="ButtonControl" ClientIDMode="Static"
                            Style="width: 50px" OnClick="btnClose_Click1" ValidationGroup="xyz" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlConfigPart" runat="server" HorizontalAlign="Center" Style="margin-left: 15%"
        Width="107%">
        <table id="Table4" runat="server">
            <tr>
                <td class="TDClassLabel" >
                    <%--<asp:Label ID="Label1" runat="server" Text="Shift Start Date"></asp:Label>
                    <label class="CompulsaryLabel">
                        *</label>--%>
                        
                    <asp:Label ID="Label3" runat="server" Text="Week end"></asp:Label>
                    <label class="CompulsaryLabel">
                        *</label>
                
                </td>
                <td class="TDClassControl" >
                    <asp:TextBox ID="txtshiftStartDate" runat="server" ClientIDMode="Static" MaxLength="10"
                        onkeypress="return false" style="display:none;"></asp:TextBox>
                    
                    
                    <asp:DropDownList ID="ddWeekend" runat="server" TabIndex="4" ClientIDMode="Static"
                        Width="173px" OnSelectedIndexChanged="ddWeekend_SelectedIndexChanged">
                    </asp:DropDownList>
                   
                </td>
                <td class="TDClassLabel">
                    <%-- <asp:Label ID="llblShiftCode" runat="server" Text="Shift Code"></asp:Label>
                    <label class="CompulsaryLabel">
                        *</label><br />--%>
                    Week OFF<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl">
                    <%--<asp:TextBox ID="txtshiftcode" Width="50px" class="TextControl" runat="server" ontextchanged="txtshiftcode_TextChanged"></asp:TextBox>--%>
                    <asp:DropDownList ID="txtshiftcode" runat="server" CssClass="ComboControl" TabIndex="5"
                        Width="173px" ClientIDMode="Static" Visible="false">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddweekoff" Width="173px" runat="server" TabIndex="5" ClientIDMode="Static"
                        OnSelectedIndexChanged="ddweekoff_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    <asp:Label ID="lblMinimumswipeT" runat="server" Text="Minimum swipe"></asp:Label>
                    <label class="CompulsaryLabel">
                        *</label>
                </td>
                <td class="TDClassControl">
                    <asp:TextBox ID="txtMinimumSwipe" CssClass="TextControl" MaxLength="2" ClientIDMode="Static"
                        runat="server" OnTextChanged="txtMinimumSwipe_TextChanged1" onkeypress="return IsNumericSwipe(event)"></asp:TextBox>
                    <%--  <asp:RequiredFieldValidator ID="rfvMinimumSwipe" runat="server" ControlToValidate="txtMinimumSwipe"
                        ErrorMessage="Plz. enter min. swipe." Style="font-family: Verdana; font-size: 9pt;"
                        ForeColor="Red" Font-Size="Medium" ValidationGroup="ADD" Display="None"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                        TargetControlID="rfvMinimumSwipe" PopupPosition="Right">
                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                </td>
                <td class="TDClassLabel">
                    <asp:Label ID="Label2" runat="server" Text="Schedule Type"></asp:Label>
                    <label class="CompulsaryLabel">
                        *</label>
                </td>
                <td class="TDClassControl">
                    <asp:RadioButtonList ID="rblScheduleType" runat="server" CssClass="ComboControl"
                        TabIndex="4" ClientIDMode="Static" RepeatDirection="Horizontal" onchange="GetSelectedValue();"
                        onclick="GetSelectedValue();">
                        <asp:ListItem Value="Pattern">Pattern</asp:ListItem>
                        <asp:ListItem Value="Fixed" Selected="True">Fixed</asp:ListItem>
                    </asp:RadioButtonList>
                    <%--<asp:RequiredFieldValidator ID="rfvrblScheduleType" runat="server" ControlToValidate="rblScheduleType"
                        ErrorMessage="Plz. select shift type." ForeColor="Red" Style="font-family: Verdana;
                        font-size: 9pt;" Font-Size="Medium" ValidationGroup="ADD" Display="None"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerblScheduleType" runat="server" TargetControlID="rfvrblScheduleType"
                        PopupPosition="Right">
                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td class="TDClassLabel">
                    <asp:Label ID="lblShiftType" runat="server" Text="Select Shift :"></asp:Label>
                </td>
                <td class="TDClassControl">
                    <asp:DropDownList ID="ddlShiftPattern" runat="server" onchange="ckhForDaily();" onkeyup="ckhForDaily();"
                        onkeydown="ckhForDaily();" Width="173px">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlShift" runat="server" Width="173px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlshifttype" runat="server" Visible="false">
        <table style="width: 70%; margin-left: 15%;">
            <tr>
                <td colspan="2" style="width: 35%">
                </td>
                <td class="TDClassLabel" style="width: 200px">
                </td>
                <td>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table id="table3" runat="server" cellpadding="3" cellspacing="3" width="100%">
        <tr>
            <td>
            </td>
            <td align="center">
                <asp:Button ID="CmdOk" runat="server" CssClass="ButtonControl" Text="Save" TabIndex="8"
                    OnClick="CmdOk_Click" OnClientClick="return ValidateSave();" />
                <asp:Button ID="CmdCancel" runat="server" Text="Reset" CssClass="ButtonControl" TabIndex="9"
                    CausesValidation="False" OnClientClick="PageRefresh()" OnClick="CmdCancel_Click" />
                <input id="btnBak" type="button" class="ButtonControl" name="Back" onclick="backtoViewMode()"
                    value="Cancel" />
            </td>
            <td>
            </td>
        </tr>
    </table>
    <div class="DivEmpDetails">
        <table style="width: 100%;">
            <tr>
                <td style="background-color: #EFF8FE; padding: 0px;" colspan="3">
                <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                        <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                    </div>
                    <asp:UpdatePanel ID="upnGrid" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvEmpMgrAdd" runat="server" AutoGenerateColumns="false" Width="100%"
                                GridLines="None" AllowPaging="true" PageSize="10">
                                <RowStyle CssClass="gvRow" />
                                <HeaderStyle CssClass="gvHeader" />
                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                <PagerStyle CssClass="gvPager" />
                                <EmptyDataTemplate>
                                    <div>
                                        <span>No records found.</span>
                                    </div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="EOD_EMPID" HeaderText="Employee ID"></asp:BoundField>
                                    <asp:BoundField DataField="Empname" HeaderText="Employee Name"></asp:BoundField>
                                    <asp:BoundField DataField="EOD_DIVISION_ID" HeaderText="Division"></asp:BoundField>
                                </Columns>
                                <PagerTemplate>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: left; width: 15%;">
                                                <span>Go To : </span>
                                                <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChangePage">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="gvPrevious"
                                                    CssClass="ButtonControl" />
                                                <asp:Label ID="lblShowing" runat="server" Text="Showing "></asp:Label>
                                                <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="gvNext" CssClass="ButtonControl" />
                                            </td>
                                            <td style="text-align: right; width: 15%;">
                                                <asp:Label ID="lblTotal" runat="server" Text="Total Records"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </PagerTemplate>
                                <SortedAscendingHeaderStyle ForeColor="White" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnOK" />
                            <asp:AsyncPostBackTrigger ControlID="btnClose" />
                            <asp:AsyncPostBackTrigger ControlID="CmdOk" />                            
                        </Triggers>
                    </asp:UpdatePanel>
                    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
                        TargetControlID="upnGrid">
                        <Animations>
                        <OnUpdating>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="btnSearch" Enabled="false" />
                                <ScriptAction Script="onUpdating();" />  
                                <FadeOut Duration="1.0" Fps="24" minimumOpacity=".5" />
                            </Parallel> 
                        </OnUpdating>
                        <OnUpdated>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="btnSearch" Enabled="true" />
                                <ScriptAction Script="onUpdated();" /> 
                                <FadeIn Duration="1.0" Fps="24"  minimumOpacity=".5"/>
                            </Parallel> 
                        </OnUpdated>
                        </Animations>
                    </ajaxToolkit:UpdatePanelAnimationExtender>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="pnlErrorMessage" runat="server" HorizontalAlign="Center">
        <asp:Label ID="lblMsg" ClientIDMode="Static" CssClass="ErrorLabel" runat="server"
            ForeColor="Red" Style="font-family: Verdana; font-size: 9pt" Visible="true"></asp:Label>
    </asp:Panel>
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
