<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="LeaveAppEdit.aspx.cs" Inherits="UNO.LeaveAppEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">--%>
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css" />
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/Validation1.js"></script>
    <script language="javascript" src="calendar.js"></script>
    <link rel='stylesheet' href='./calendar.css' title='calendar'>
    <script language="javascript" type="text/javascript">
        //        function ValidateData() {           
        //            if (!(validateString(document.getElementById('txtScheduleDesc'), "Please enter Scheduler Description."))) {
        //                return false
        //            }         
        //            if (!(validateString(document.getElementById('txtScheduleTime'), "Please enter Scheduler Time."))) {
        //                return false
        //            }

        //            if (handleAdd() == false) {
        //                return false
        //            }z
        //        }



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

        //        function CompareDates(d1, d2) {
        //            var start = d1.value.toUpperCase();
        //            var end = d2.value.toUpperCase();
        //            var arrDate = start.split('/');
        //            var arrDate1 = end.split('/');
        //            var d1 = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
        //            var d2 = new Date(arrDate1[2], arrDate1[1] - 1, arrDate1[0]);
        //            if (d2 <= d1) {
        //                //alert("End date must be lessthan startdate");
        //                return false;
        //            }
        //            return true;
        //        }

        //        function isValidateIssueDate(oSrc, args) {

        //            if (!CompareDates(document.getElementById('txtholidaydate'), document.getElementById('txtholidayswap'))) {
        //                //alert("Post dated Reports can not be viewed");
        //                // document.getElementById('txtholidaydate').select();
        //                args.IsValid = false;
        //            }
        //            else 
        //            {
        //                args.IsValid = true;
        //            }
        //            
        //	
        //        }

        //       

        //        function clearFunction2() {
        //            document.getElementById('txtHolidayid').value = ""
        //            document.getElementById('txtHolidayDesc').value = ""
        //            document.getElementById('ddlHolidayType').value = "Select One"
        //            document.getElementById('txtholidaydate').value = ""
        //            document.getElementById('txtholidayswap').value = ""
        //            document.getElementById('txtHolidayid').focus();
        //        }
        //        function resetcontrols() {
        //            document.getElementById('txtHolidayid').value = ""
        //            document.getElementById('txtHolidayDesc').value = ""
        //            document.getElementById('ddlHolidayType').value="Select One" 
        //            document.getElementById('txtholidaydate').value = ""
        //            document.getElementById('txtholidayswap').value = ""
        //            document.getElementById('messagediv').InnerHtml = ""
        //            document.getElementById('txtHolidayid').focus();
        //        }




        function findspace(evnt) {

            var keyASCII = (evnt.which) ? evnt.which : event.keyCode;
            var keyValue = String.fromCharCode(keyASCII);

            if (!(keyASCII >= '48' && keyASCII <= '57')) {
                window.event.keyCode = 0;
            }
        }
        function fnColon(ctrl, e) {
            var unicode = e.keyCode
            if (unicode != 8) {
                if (ctrl.getAttribute && ctrl.value.length == 2) {
                    ctrl.value = ctrl.value + ":";
                }
            }
        }
        function wash(anInput) {
            if (anInput.value == anInput.defaultValue) anInput.value = '';
        }
        function checkWash(anInput) {
            if (anInput.value == '') anInput.value = anInput.defaultValue;
        }

        function trim(str) {
            while (str.substring(0, 1) == ' ') {
                str = str.substring(1, str.length);
            }
            while (str.substring(str.length - 1, str.length) == ' ') {
                str = str.substring(0, str.length - 1);
            }
            return str;
        }

        function keypress() {
            //            alert("hi");
            document.getElementById('Empdata').style.display = "block";
            //            document.getElementById('Empdata1').style.display = "block";
        }
        function ListBox1_DoubleClick() {
            document.forms[0].ListBox1Hidden.value = "doubleclicked";
            document.forms[0].submit();
        }

        function ListBox2_DoubleClick() {
            document.forms[0].ListBox2Hidden.value = "doubleclicked";
            document.forms[0].submit();
        }

        function onCheck(sender, ctrl, type) {
            lst = document.getElementById(ctrl);
            lst.selectedIndex = -1;
            var search_value = sender.value.toUpperCase();

            search_value = trim(search_value);
            search_value = search_value.split(' ').join('_');
            var len = search_value.length;
            if (type == 'DESC') {
                for (var i = 0; i < lst.options.length; i++) {
                    var Rval = trim(lst.options[i].text.toUpperCase().substr(0, len));
                    var l = Rval.length;
                    for (var j = 0; j < l; j++) {
                        if (Rval.charCodeAt(j) == 160)
                        { Rval = Rval.replaceAt(j, "_"); }
                    }

                    if (Rval == search_value) {
                        lst.options[i].selected = true;
                        break;
                    }
                }
            }
            if (type == 'CODE') {
                for (var i = 0; i < lst.options.length; i++) {
                    if (lst.options[i].value.toUpperCase().substr(0, len) == search_value) {
                        lst.options[i].selected = true;
                        break;
                    }
                }
            }

        }

        //        function editmode() {
        //            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";
        //            document.getElementById('txtholidaydate').select();
        //         
        //        }


        //        function returnviewmode() {
        //            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";
        //            window.location = "HolidayView.aspx";
        //        }
        //        function clearFunction(x) {
        //            document.getElementById('txtHolidayid').value = ""
        //            document.getElementById('txtHolidayDesc').value = ""
        //            document.getElementById('ddlHolidayType').value = "Select One"
        //            document.getElementById('txtholidaydate').value = ""
        //            document.getElementById('txtholidayswap').value = ""
        //            document.getElementById(x).innerHTML = "&nbsp;&nbsp;"
        //            document.getElementById('txtHolidayid').focus();
        //            }





        function clearMessageDiv(x) {



            document.getElementById(x).innerHTML = "&nbsp;&nbsp;";


        }

        function handleAdd() {



            if (document.getElementById('ddlModeType').value == "N") {

                ValidatorEnable(document.getElementById('RequiredFieldValidator1'), true);

                if (!Page_ClientValidate())

                    return;
            }
            else if (document.getElementById('ddlModeType').value == "I") {


                //document.getElementById('RequiredFieldValidator1').style.display = "none";
                ValidatorEnable(document.getElementById('RequiredFieldValidator1'), false);

                if (!Page_ClientValidate()) {

                    return;
                }
            }

            else if (document.getElementById('ddlModeType').value == "O") {


                //document.getElementById('RequiredFieldValidator1').style.display = "none";
                ValidatorEnable(document.getElementById('RequiredFieldValidator1'), true);

                if (!Page_ClientValidate()) {

                    return;
                }
            }

            //                else if (document.getElementById('ddlModeType').value == "I") {
            //                    if (!Page_ClientValidate())

            //                        return;
            //                }
            //                if (document.getElementById('ddlModeType').value == "N") {

            //                    if (!Page_ClientValidate('null'))

            //                        return;
            //                }

            var msg = confirm("Save Record?");

            if (msg == false) {
                return false;
            }
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
        function ShowSingledate() {
            var rb = document.getElementById('rbtDate');
            var inputs = rb.getElementsByTagName('input');
            var selected;
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].checked) {
                    selected = inputs[i];
                    if (selected.value == "0") {
                        // document.getElementById('<%=TxtLeaveTodt.ClientID%>').value =
                        document.getElementById('<%=TxtLeaveTodt.ClientID%>').disabled = true;
                    }
                    else {

                        document.getElementById('<%=TxtLeaveTodt.ClientID%>').disabled = false;
                    }
                    break;
                }
            }
        }
           
    </script>
    <h3 id="lbltitle" runat="server" class="heading">
        Leave Request</h3>
    <table id="table1" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
        class="TableClass">
        <tr>
            <td align="right" style="width: 33%" class="LinkControl">
                <asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="~/LeaveAppView.aspx"
                    ForeColor="Blue" Visible="false">Back to View Mode</asp:HyperLink>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Settings" runat="server" Width="95%" CssClass="srcColor">
        <table id="table2" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
            class="TableClass">
            <tr>
                <td class="TDClassLabel" style="text-align: Right; width: 25%">
                    &nbsp;Employee Code :
                    <label class="CompulsaryLabel">
                        *</label>
                </td>
                <td class="TDClassLabel" style="text-align: left">
                    <%--<asp:DropDownList ID ="ddlEmpCd" ClientIDMode="Static" CssClass="ComboControl" 
                     runat = "server" Width="100%" 
                            onselectedindexchanged="ddlEmpCd_SelectedIndexChanged"></asp:DropDownList>
                     <br />
                         <asp:RequiredFieldValidator ID="rfvEmpcd" runat="server" 
                             ErrorMessage="Please select Employee Code." ControlToValidate="ddlEmpCd" 
                             SetFocusOnError="True" Display="Dynamic" InitialValue = "Select One" ForeColor="Red" 
                             ValidationGroup="ODdetails"></asp:RequiredFieldValidator> --%>
                    <asp:TextBox ID="txtEmployeeCode" runat="server" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
            </tr>
            <tr>
                <td style="height: 15px;">
                </td>
            </tr>
            <tr id="Empdata" valign="top">
                <td class="TDClassLabel">
                    Employee Name:<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassLabel" style="text-align: left" valign="top">
                    <input type="hidden" name="ListBox1Hidden" />
                    <asp:TextBox ID="txtEmployeeName" runat="server" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                </td>
                </td>
            </tr>
            <tr>
                <td style="height: 15px;">
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    <%--  <asp:RadioButtonList ID="rbtDate" runat="server" ClientIDMode="Static" CssClass="radiobutton" 
                        RepeatLayout="Flow">
                        <asp:ListItem onclick="ShowSingledate()" Selected="True" Value="0" >Single Date</asp:ListItem>
                        <asp:ListItem onclick="ShowSingledate()" Value="1" >Date Range</asp:ListItem>
                    </asp:RadioButtonList>--%>
                    From Date :<font color="red">*</font>
                </td>
                <%--  <td class="style41">--%>
                <td class="TDClassControl">
                    <asp:TextBox ID="txtLeaveFrDt" runat="server" MaxLength="10" />
                    <ajaxToolkit:CalendarExtender ID="calLeaveFrDt" TargetControlID="txtLeaveFrDt" PopupButtonID="txtLeaveFrDt"
                        runat="server" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="height: 15px;">
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    To Date :<font color="red">*</font>
                </td>
                <td class="TDClassControl">
                    <asp:TextBox ID="TxtLeaveTodt" runat="server" MaxLength="10" />
                    <ajaxToolkit:CalendarExtender ID="CalLeaveTodt" TargetControlID="TxtLeaveTodt" PopupButtonID="TxtLeaveTodt"
                        runat="server" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="height: 15px;">
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    Leave Type :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl">
                    <asp:DropDownList ID="ddlLAType" runat="server" class="chosen-select" Width="173px"
                        ClientIDMode="Static">
                        <asp:ListItem Value="0">Select One</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlLAType"
                        Display="Dynamic" ErrorMessage="Please select Leave Type" ForeColor="Red" InitialValue="Select One"
                        ValidationGroup="Validate" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="height: 15px;">
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    Leave Reason :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl">
                    <asp:DropDownList ID="ddlReasonType" runat="server" ClientIDMode="Static" Width="173px"
                        class="chosen-select" Height="20px">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1_empid11" runat="server" ControlToValidate="ddlReasonType"
                        ValidationGroup="Validate" Display="Dynamic" ErrorMessage="Please select Reason"
                        ForeColor="Red" InitialValue="Select One" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="height: 15px;">
                </td>
            </tr>
            <tr id="frmtm" runat="server">
                <td class="TDClassLabel">
                    &nbsp;Remarks :
                    <label class="CompulsaryLabel">
                        *</label>
                </td>
                <td id="tdtextfrm_Time" runat="server" clientidmode="Static" class="TDClassControl">
                    <%-- <asp:Panel ID="pnlfrmtime"  runat="server"   align="Center"  >--%>
                    <asp:TextBox ID="txt_Remarks" runat="server" TextMode="MultiLine" Style="resize: none"
                        ClientIDMode="Static" Width="173px" onkeyDown="checkLength(this,'150');" onkeyUp="checkLength(this,'150');"
                        onkeypress="checkLength(this,'150');"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 15px;">
                </td>
            </tr>
            <tr id="Tr1" runat="server">
                <td class="TDClassLabel" style="display: none">
                    &nbsp;Sanction ID :
                    <label class="CompulsaryLabel">
                        *</label>
                </td>
                <td id="td1" runat="server" clientidmode="Static" class="TDClassControl" style="display: none">
                    <%-- <asp:Panel ID="pnlfrmtime"  runat="server"   align="Center"  >--%>
                    <asp:DropDownList ID="ddlSanctionCode" runat="server" ClientIDMode="Static" class="chosen-select">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 15px;">
                </td>
            </tr>
        </table>
        <table class="TableClass" id="table3" runat="server" cellpadding="3" cellspacing="3"
            width="95%">
            <tr>
                <td style="width: 7%">
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="ButtonControl" Style="float: right"
                        Text="Save" TabIndex="6" OnClick="btnSave_Click" ValidationGroup="Validate" />
                </td>
                <td>
                    <asp:Button ID="Btnclear" runat="server" Text="Cancel" CssClass="ButtonControl" TabIndex="7"
                        CausesValidation="False" OnClick="Btnclear_Click" />
                    <%--   <asp:Button ID="btnCancel1" runat="server" CssClass="ButtonControl" 
                    onclick="btnCancel_Click" Text="Cancel" Width="80%" />--%>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table class="MessageContainerTable" width="98.8%">
        <tr>
            <td colspan="4" align="center" valign="middle">
                <div id="messageDiv" runat="server" clientidmode="Static" class="MessageClass">
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </div>
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
