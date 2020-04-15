<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ESS_TA_OD_Edit.aspx.cs" Inherits="UNO.ESS_TA_OD_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style45
        {
            text-align: right;
        }
        .style45
        {
            font-family: Verdana;
            font-size: 9pt;
            color: #515456; /*border: 1px solid #CCCCCC;*/
            text-align: right;
            padding-right: 4px;
            width: 11%;
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


//        function ShowMode() {
//            if (document.getElementById('ddlModeType').value == "I") {
//                document.getElementById('frmtime').style.display = "inline";


//                document.getElementById('totime').style.display = "none";




//            }

//            else if (document.getElementById('ddlModeType').value == "O") {

//                document.getElementById('frmtime').style.display = "none";


//                document.getElementById('totime').style.display = "inline";

//                //                document.getElementById('RequiredFieldValidator5').disabled = true;




//            }

//            else if (document.getElementById('ddlModeType').value == "N") {


//                document.getElementById('frmtime').style.display = "inline";
//                document.getElementById('totime').style.display = "inline";


//            }

//        }


        function clearMessageDiv(x) {



            document.getElementById(x).innerHTML = "&nbsp;&nbsp;";


        }


 function CompareDates(d1, d2) {
            var start = d1.value.toUpperCase();
            var end = d2.value.toUpperCase();



            var arrDate = start.split('/');
            var arrDate1 = end.split('/');
            var d1 = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
            var d2 = new Date(arrDate1[2], arrDate1[1] - 1, arrDate1[0]);






            //1 year =31,536,000,000 milli seconds
            //  if ((d2 - d1) > '31622400000') {
            if ((d2 - d1) > '2628000000') {


                document.getElementById('<%= CustomValidatorM_Lab_From.ClientID %>').innerHTML = "Difference between two dates should not be greater than a Month"

                return false;
            }


            else if (d2 < d1) {
                document.getElementById('<%= CustomValidatorM_Lab_From.ClientID %>').innerHTML = "To Date should not be less than From date"

                return false;
            }

            return true;
        }

        function isValidateIssueDate(oSrc, args) {

            if (!CompareDates(document.getElementById('txtCalendarFrom'), document.getElementById('txtCalendarTo'))) {
                //alert("Post dated Reports can not be viewed");
                // document.getElementById('txtholidaydate').select();
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }

        }

        function ShowSingledate() {

            if (document.getElementById('rbtDate_0').checked == true) {
                document.getElementById('txtCalendarTo').style.display = "none";

                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator3.ClientID %>'), false);

                //                document.getElementById('imgToCalendar').style.display = "none";

            }
            else if (document.getElementById('rbtDate_1').checked == true) {


                document.getElementById('txtCalendarTo').style.display = "inline";

                //                document.getElementById('imgToCalendar').style.display = "inline";



            }
        }

        function handleAdd() {

            if (document.getElementById('rbtDate_1').checked == true) {
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator3.ClientID %>'), true);
            }

            if (!Page_ClientValidate())

                return;

            var msg = confirm("Save Record?");

            if (msg == false) {
                return false;
            }
        }
           
    </script>
    <h3 id="lbltitle" runat="server" class="heading">
        OD Request</h3>
    <table id="table1" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
        class="TableClass">
        <tr>
            <td align="right" style="width: 33%" class="LinkControl">
                <asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="~/ESS_TA_OD_View.aspx"
                    ForeColor="Blue">Back to View Mode</asp:HyperLink>
            </td>
        </tr>
    </table>
    <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Settings" runat="server" ScrollBars="Auto" Width="95%" CssClass="srcColor">
                <table id="table2" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                    class="TableClass">
                    <tr>
                        <td class="TDClassLabel">
                            <asp:RadioButtonList ID="rbtDate" runat="server" ClientIDMode="Static" CssClass="radiobutton"
                                RepeatLayout="Flow">
                                <asp:ListItem onclick="ShowSingledate()" Selected="True" Value="0">Single Date</asp:ListItem>
                                <asp:ListItem onclick="ShowSingledate()" Value="1">Date Range</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="TDClassControl">
                            <asp:TextBox ID='txtCalendarFrom' runat="server" MaxLength="10" 
                                ClientIDMode="Static" />
                            <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtCalendarFrom"
                                PopupButtonID="txtCalendarFrom" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <%--<img onmouseover="fnInitCalendar(this, 'txtCalendarFrom', 'style=calendar.css,close=true')"
                        src="images/calendar.gif" id="imgfrmCalendar" name="imgfrmCalendar"></img>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCalendarFrom"
                                Display="Dynamic" ErrorMessage="Please enter From Date" SetFocusOnError="True"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                            <%--<asp:CustomValidator ID="CustomValidatorDeposit_Date0" runat="server" 
                                ClientValidationFunction="isValidateDDMMYYYYDate" 
                                ControlToValidate="txtCalendarFrom" Display="Dynamic" 
                                ErrorMessage="Please enter valid Date" ForeColor="Red" SetFocusOnError="True"></asp:CustomValidator>--%>
                                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                            ControlToValidate="txtCalendarFrom" 
                            ErrorMessage="Please enter date in dd/mm/yyyy format" ForeColor="Red" 
                            
                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>

                            <br />
                            <asp:TextBox ID='txtCalendarTo' runat="server" MaxLength="10" 
                                ClientIDMode="Static" />
                            <ajaxToolkit:CalendarExtender ID="calTo" runat="server" TargetControlID="txtCalendarTo"
                                PopupButtonID="txtCalendarTo" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <%--<img onmouseover="fnInitCalendar(this, 'txtCalendarTo', 'style=calendar.css,close=true')"
                            src="images/calendar.gif" name="imgToCalendar"></img>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCalendarTo"
                                Display="Dynamic" ErrorMessage="Please enter To Date" SetFocusOnError="True"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidatorM_Lab_From" runat="server" 
                                ClientValidationFunction="isValidateIssueDate" 
                                ControlToValidate="txtCalendarTo" Display="Dynamic" 
                                ErrorMessage="To Date should not be same or less than from date" 
                                ForeColor="Red" SetFocusOnError="True"></asp:CustomValidator>
                                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="txtCalendarTo" 
                            ErrorMessage="Please enter date in dd/mm/yyyy format" ForeColor="Red" 
                            
                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>

                            <%--<asp:CustomValidator ID="CustomValidatorDeposit_Date1" runat="server" 
                                ClientValidationFunction="isValidateDDMMYYYYDate" 
                                ControlToValidate="txtCalendarTo" Display="Dynamic" 
                                ErrorMessage="Please enter valid Date" ForeColor="Red" SetFocusOnError="True"></asp:CustomValidator>--%>
                        </td>
                    </tr>
                    <%--<asp:DropDownList ID ="ddlEmpCd" ClientIDMode="Static" CssClass="ComboControl" 
                     runat = "server" Width="100%" 
                            onselectedindexchanged="ddlEmpCd_SelectedIndexChanged"></asp:DropDownList>
                     <br />
                         <asp:RequiredFieldValidator ID="rfvEmpcd" runat="server" 
                             ErrorMessage="Please select Employee Code." ControlToValidate="ddlEmpCd" 
                             SetFocusOnError="True" Display="Dynamic" InitialValue = "Select One" ForeColor="Red" 
                             ValidationGroup="ODdetails"></asp:RequiredFieldValidator> --%>
                    <tr>
                        <td class="style45" style="text-align: Right;">
                            OD Code :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="style42" style="text-align: left">
                            &nbsp;<asp:DropDownList ID="ddlODType" runat="server" ClientIDMode="Static" CssClass="ComboControl"
                                Height="20px">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlODType"
                                Display="Dynamic" ErrorMessage="Please enter OD type" ForeColor="Red" SetFocusOnError="True"
                                ClientIDMode="Static" InitialValue="Select One"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            Select Reason :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:DropDownList ID="ddlReasonType" runat="server" ClientIDMode="Static" CssClass="ComboControl"
                                Height="20px">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1_empid11" runat="server" ControlToValidate="ddlReasonType"
                                Display="Dynamic" ErrorMessage="Please select Reason" ForeColor="Red" InitialValue="Select One"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="frmtm" runat="server">
                        <td class="TDClassLabel">
                            &nbsp;Remarks :&nbsp;
                        </td>
                        <td id="tdtextfrm_Time" runat="server" clientidmode="Static" class="TDClassControl"
                            style="height: 10px; width: 50%">
                            <%-- <asp:Panel ID="pnlfrmtime"  runat="server"   align="Center"  >--%>
                            <asp:TextBox ID="txt_Remarks" runat="server" TextMode="MultiLine" 
                                ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="TableClass" id="table3" runat="server" cellpadding="3" cellspacing="3"
                    width="95%">
                    <tr>
                        <td style="width: 70%">
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonControl" OnClientClick="return handleAdd()"
                                Text="Save" Width="100%" TabIndex="6" OnClick="btnSave_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Btnclear" runat="server" Text="Cancel" Width="100%" CssClass="ButtonControl"
                                TabIndex="7" CausesValidation="False" OnClick="Btnclear_Click" />
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
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
