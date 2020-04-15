<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ODAppEdit.aspx.cs" Inherits="UNO.ODAppEdit" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--
   <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
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
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>    
    <script language="javascript" src="calendar.js"></script>
    <link rel='stylesheet' href='./calendar.css' title='calendar'>
    <script language="javascript" type="text/javascript">
      
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
           
    </script>
    <h3 id="lbltitle" runat="server" class="heading">
        OD Request</h3>
    <table id="table1" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
        class="TableClass">
        <tr>
            <td align="right" style="width: 33%" class="LinkControl">
                <asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="~/ODAppView.aspx"
                    Visible="false" ForeColor="Blue">Back to View Mode</asp:HyperLink>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Settings" runat="server" Width="95%" CssClass="srcColor">
        <table id="table2" runat="server" width="100%" border="0" cellpadding="1" cellspacing="5"
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
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
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
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    <asp:RadioButtonList ID="rbtDate" runat="server" ClientIDMode="Static" CssClass="radiobutton"
                        RepeatLayout="Flow" CellSpacing="10">
                        <asp:ListItem onclick="ShowSingledate()" Selected="True" Value="0">Single Date<br /></asp:ListItem>
                        <asp:ListItem onclick="ShowSingledate()" Value="1">Date Range</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="style41">
                    <asp:TextBox ID="txtLeaveFrDt" runat="server" MaxLength="10" onKeyPress="javascript: return false " />
                    <ajaxToolkit:CalendarExtender ID="caltxtLeaveFrDt" runat="server" TargetControlID="txtLeaveFrDt"
                        PopupButtonID="txtLeaveFrDt" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RegularExpressionValidator ID="revtxtLeaveFrDt" runat="server" ErrorMessage="Not a valid date"
                        ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                        ControlToValidate="txtLeaveFrDt" Display="None"></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtLeaveFrDt" runat="server" TargetControlID="revtxtLeaveFrDt"
                        PopupPosition="Right">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <%--<img onmouseover="fnInitCalendar(this, 'txtCalendarFrom', 'style=calendar.css,close=true')"
                        src="images/calendar.gif" id="imgfrmCalendar" name="imgfrmCalendar"></img></img>--%>
                    <br />
                    <br />
                    <asp:TextBox ID="TxtLeaveTodt" runat="server" MaxLength="10" onKeyPress="javascript: return false " />
                    <ajaxToolkit:CalendarExtender ID="calTxtLeaveTodt" runat="server" TargetControlID="TxtLeaveTodt"
                        PopupButtonID="TxtLeaveTodt" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RegularExpressionValidator ID="revTxtLeaveTodt" runat="server" ErrorMessage="Not a valid date"
                        ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                        ControlToValidate="TxtLeaveTodt" Display="None"></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerevTxtLeaveTodt" runat="server" TargetControlID="revTxtLeaveTodt"
                        PopupPosition="Right">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <%-- <img onmouseover="fnInitCalendar(this, 'txtCalendarFrom', 'style=calendar.css,close=true')"
                        src="images/calendar.gif" id="img1" name="imgfrmCalendar"></img></img>--%>
                </td>
            </tr>
            <tr style="display: none;">
                <td class="TDClassLabel">
                    Leave Type :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl">
                    <asp:DropDownList ID="ddlODType" runat="server" Width="173px" ClientIDMode="Static">
                        <asp:ListItem Value="0">Select One</asp:ListItem>
                        <asp:ListItem Value="I">IN</asp:ListItem>
                        <asp:ListItem Value="O">OUT</asp:ListItem>
                        <asp:ListItem Value="N">Both</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlODType"
                        Display="None" ErrorMessage="Please select OD Type" ForeColor="Red" InitialValue="0"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vceRequiredFieldValidator1" runat="server"
                        TargetControlID="RequiredFieldValidator1" PopupPosition="Right">
                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                </td>
            </tr>
            <tr>
               
              
                    <td colspan="2">
                    </td>
                </tr>
                <tr style="display: none;"> <td class="TDClassLabel"> Leave Reason :<label class="CompulsaryLabel">*</label>
                </td> <td class="TDClassControl"> <asp:DropDownList ID="ddlReasonType" runat="server"
                Width="173px" ClientIDMode="Static" Height="20px"> </asp:DropDownList> <br /> <%--
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1_empid11" runat="server"
                ControlToValidate="ddlReasonType" Display="None" ErrorMessage="Please select Reason"
                ForeColor="Red" InitialValue="Select One" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="vceRequiredFieldValidator1_empid11" runat="server"
                TargetControlID="RequiredFieldValidator1_empid11" PopupPosition="Right"> </ajaxToolkit:ValidatorCalloutExtender>--%>
                </td>
            </tr>
            <tr id="frmtm" runat="server">
                <td class="TDClassLabel">
                    &nbsp;Remarks :
                </td>
                <td id="tdtextfrm_Time" runat="server" clientidmode="Static" class="TDClassControl">
                    <%-- <asp:Panel ID="pnlfrmtime"  runat="server"   align="Center"  >--%>
            <asp:TextBox ID="txt_Remarks" runat="server" Style="width: 200px;
                    height: 60px; max-height: 60px; min-height: 60px; max-width: 200px; min-width: 200px;"
                    TabIndex="3" Height="50px" MaxLength="50" TextMode="MultiLine" onkeyDown="checkLength(this,'50');"
                    onkeyUp="checkLength(this,'50');"></asp:TextBox> </td> </tr> <tr id="Tr1" runat="server">
            <td class="TDClassLabel" style="display: none"> &nbsp;Sanction ID : <label class="CompulsaryLabel">
            *</label> </td> <td id="td1" runat="server" clientidmode="Static" class="TDClassControl"
            style="display: none"> <%-- <asp:Panel ID="pnlfrmtime" runat="server" align="Center"
            >--%> <asp:DropDownList ID="ddlSanctionCode" runat="server" ClientIDMode="Static"
            CssClass="ComboControl"> </asp:DropDownList> </td> </tr>
        </table>
        <table class="TableClass" id="table3" runat="server" cellpadding="3" cellspacing="3"
            width="95%" style="text-align:center;">
            <tr>
               
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" CssClass="ButtonControl" OnClientClick="return handleAdd()"
                        Text="Save" TabIndex="6" OnClick="btnSave_Click" />
                           <asp:Button ID="Btnclear" runat="server" Text="Cancel" CssClass="ButtonControl" TabIndex="7"
                        CausesValidation="False" OnClick="Btnclear_Click" />
                </td>
                
            </tr>
            <tr style="height:30px;">
            <td colspan="2">
              <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
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
