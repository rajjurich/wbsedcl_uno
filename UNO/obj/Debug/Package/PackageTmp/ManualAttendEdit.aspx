<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ManualAttendEdit.aspx.cs" Inherits="UNO.ManualAttendEdit" %>

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
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript">
        function sameDate() {
            var fdate = document.getElementById('<%= txtFromDate.ClientID %>').value;
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
                var fdate = document.getElementById('<%= txtFromDate.ClientID %>').value;
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



        function handleAdd() {
         
            if (document.getElementById('<%=txtFromDate.ClientID%>').value == '') {
                document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please enter From Date';
                return false;
            }
            else if (document.getElementById('<%=txtToDate.ClientID%>').value == '') {
                document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please enter To Date';
                return false;
            }
            if (!CompareDates(document.getElementById('<%=txtFromDate.ClientID%>'), document.getElementById('<%=txtToDate.ClientID%>'))) {
                document.getElementById('<%= lblmsg.ClientID %>').innerHTML = "To Date should not be less than From date";
                return false;
            }
            else if (document.getElementById('<%=frm_time.ClientID%>').value == '' && document.getElementById('<%=To_Time.ClientID%>').value=='') {
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
            if ($("#<%=ddlReasonType.ClientID%> option:selected").val() == 'Select One') {
                document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please select Reason';
                return false;
            }
            document.getElementById('<%=lblmsg.ClientID%>').innerHTML = "";
            return true;
            
        }

    </script>
    <h3 id="lbltitle" runat="server" class="heading">
        Manual Attendance</h3>
    <table id="table1" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
        class="TableClass">
        <tr>
            <td align="right" style="width: 33%" class="LinkControl">
                <asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="~/ManualAttendView.aspx"
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
                <td class="TDClassLabel" style="text-align: left;">
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
                <td style="height: 10px;">
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
                <td style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    From Date :<label class="CompulsaryLabel">*</label>
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" onKeyPress="javascript: return false "
                        onKeydown="javascript: return false " onchange="chkDate()" onblur="sameDate()"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                        PopupButtonID="txtFromDate" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    To Date :
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" onKeyPress="javascript: return false "
                        onKeydown="javascript: return false " onchange="chkDate()"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                        PopupButtonID="txtToDate" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <%-- <tr>
                <td class="TDClassLabel">
                    Swipe Date
                </td>
                <td class="TDClassControl">
                    <asp:TextBox ID="MA_SWIPEDATE" runat="server" MaxLength="10" />
                    <img onmouseover="fnInitCalendar(this, 'txtCalendarFrom', 'style=calendar.css,close=true')"
                        src="images/calendar.gif" id="imgfrmCalendar" name="imgfrmCalendar"></img></img>
                </td>
            </tr>--%>
            <tr>
                <td style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel" style="text-align: Right;">
                    From Time :
                </td>
                <td class="TDClassControl" style="text-align: left">
                    <asp:TextBox ID="frm_time" runat="server"  CssClass="TextControl"
                        MaxLength="5" onkeypress="findspace(event)" onkeyup="fnColon(this,event)" TabIndex="4"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FTEfrm_time" runat="server" FilterType="Custom,Numbers
" TargetControlID="frm_time" ValidChars=":" />
                    <br />
                   
                </td>
                <%--<asp:DropDownList ID ="ddlEmpCd" ClientIDMode="Static" CssClass="ComboControl" 
                     runat = "server" Width="100%" 
                            onselectedindexchanged="ddlEmpCd_SelectedIndexChanged"></asp:DropDownList>
                     <br />
                         <asp:RequiredFieldValidator ID="rfvEmpcd" runat="server" 
                             ErrorMessage="Please select Employee Code." ControlToValidate="ddlEmpCd" 
                             SetFocusOnError="True" Display="Dynamic" InitialValue = "Select One" ForeColor="Red" 
                             ValidationGroup="ODdetails"></asp:RequiredFieldValidator> --%>
            </tr>
            <tr>
                <td style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    To Time :
                </td>
                <td class="TDClassControl">
                    <asp:TextBox ID="To_Time" runat="server"  CssClass="TextControl"
                        MaxLength="5" onkeypress="findspace(event)" onkeyup="fnColon(this,event)" TabIndex="4"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FTETo_Time" runat="server" FilterType="Custom,Numbers
" TargetControlID="To_Time" ValidChars=":" />
                </td>
            </tr>
            <tr>
                <td style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    Select Reason :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl">
                    <asp:DropDownList ID="ddlReasonType" runat="server" ClientIDMode="Static" Width="173px"
                        Height="20px">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1_empid11" runat="server" ControlToValidate="ddlReasonType"
                        Display="Dynamic" ErrorMessage="Please select Reason" ForeColor="Red" InitialValue="Select One"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="height: 10px;">
                </td>
            </tr>
            <tr style="display: none;">
                <td class="TDClassLabel">
                    Sanctioned Employee Code :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl">
                    <asp:TextBox ID="txtSanctnedCode" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="5" onkeypress="findspace(event)" onkeyup="fnColon(this,event)" TabIndex="4"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="height: 10px;">
                </td>
            </tr>
            <tr style="display: none;">
                <td class="TDClassLabel">
                    &nbsp;Remarks :
                </td>
                <td id="tdtextfrm_Time" runat="server" clientidmode="Static" class="TDClassControl"
                    style="height: 10px; width: 50%">
                    <%-- <asp:Panel ID="pnlfrmtime"  runat="server"   align="Center"  >--%>
                    <asp:TextBox ID="txt_Remarks" runat="server" TextMode="MultiLine" Style="resize: none"
                        ClientIDMode="Static" Width="173px" onkeyDown="checkLength(this,'50');" onkeyUp="checkLength(this,'50');"
                        onkeypress="checkLength(this,'50');" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <%--<td class="TDClassLabel">
                    Swipe Type :<label class="CompulsaryLabel">*</label>
                </td>--%>
                <td class="TDClassControl">
                    <asp:DropDownList ID="ddlModeType" onchange="ShowMode()" runat="server" ClientIDMode="Static"
                        CssClass="ComboControl" Visible="false">
                        <asp:ListItem Value="0">Select One</asp:ListItem>
                        <asp:ListItem Value="I">IN</asp:ListItem>
                        <asp:ListItem Value="O">OUT</asp:ListItem>
                        <asp:ListItem Value="N">Both</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlModeType"
                        Display="Dynamic" ErrorMessage="Please select Swipe Type" ForeColor="Red" InitialValue="0"
                        SetFocusOnError="True" ClientIDMode="Static"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <table class="TableClass" id="table3" runat="server" cellpadding="3" cellspacing="3"
            width="95%">
            <tr>
                <td style="width: 7%">
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="ButtonControl" OnClientClick="return handleAdd()"
                        Text="Save" TabIndex="6" OnClick="btnSave_Click" Style="float: right" />
                </td>
                <td>
                    <asp:Button ID="Btnclear" runat="server" Text="Cancel" CssClass="ButtonControl" TabIndex="7"
                        CausesValidation="False" OnClick="Btnclear_Click" />
                    <%--   <asp:Button ID="btnCancel1" runat="server" CssClass="ButtonControl" 
                    onclick="btnCancel_Click" Text="Cancel" Width="80%" />--%>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text=""></asp:Label>
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
