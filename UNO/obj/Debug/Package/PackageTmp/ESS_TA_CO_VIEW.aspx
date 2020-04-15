<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ESS_TA_CO_VIEW.aspx.cs" Inherits="UNO.ESS_TA_CO_VIEW" Culture="en-GB"
    EnableEventValidation="false" %>

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
    <%--<link href="Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript">

        function clearFunction(x) {
            document.getElementById(x).innerHTML = "&nbsp;&nbsp;";
        }

        function handleDelete() {

            //if (!ValidateSave())
            //   return false;
            //          return CheckOne(document.getElementById('gvHolidayView'));
            // return  validateCheckBoxes();
            //          if (!Page_ClientValidate())
            //              return;


            var msg = confirm("Record(s) marked for Deletion. Continue? ");
            if (msg == false) {
                clearGridCheckBoxes();
                return false;
            }
        }

        function clearGridCheckBoxes() {
            var gridRef = document.getElementById('<%= gvLVDetails.ClientID %>');
            var inputElementArray = gridRef.getElementsByTagName('input');
            var cntchk = 0;
            for (var i = 0; i < inputElementArray.length; i++) {
                var elementRef = inputElementArray[i];

                if ((elementRef.type == 'checkbox')) {
                    elementRef.checked = false;
                }
            }
        }


        function ResetAll() {
            $('#' + ["<%=txtfromDate.ClientID%>"].join(', #')).prop('value', "");
            $("select" + "#" + "<%=cmbStatus.ClientID%>").prop('selectedIndex', 0);
            document.getElementById('<%=txtfromDate.ClientID%>').focus();
            document.getElementById('<%=cmdSearch.ClientID%>').click();
            document.getElementById('<%=gvLVDetails.ClientID%>').focus();
            return false;
        }

        function ValidateSave() {

            var lblErr = document.getElementById('<%=lblAddError.ClientID%>');
            var GrdCompOff = document.getElementById('<%=gvPopUp.ClientID%>');
            var frmDate = document.getElementById('<%=txtfromDate1.ClientID%>');
            var toDate = document.getElementById('<%=txtToDate1.ClientID%>');
            var Count = 0;

            if ($("#<%=ddlEmp.ClientID%> option:selected").val() == '0') {
                lblErr.innerHTML = 'Please select employee';
                return false;
            }
            if (frmDate.value == '') {
                lblErr.innerHTML = 'Please enter from date.';
                return false;
            }

            if (toDate.value == '') {
                lblErr.innerHTML = 'Please enter to date.';
                return false;
            }

            if (!CompareDates(frmDate, toDate)) {
                lblErr.innerHTML = "To Date should not be less than From date";
                return false;
            }

            if ($("#<%=ddlReasonType.ClientID%> option:selected").val() == '-1') {
                lblErr.innerHTML = 'Please select reason type';
                return false;
            }

            for (var i = 1; i <= GrdCompOff.rows.length - 1; i++) {

                if (GrdCompOff.rows[i].cells[0].children[0].checked) {
                    Count++;
                }
            }
            if (Count > 0) {
                lblErr.innerHTML = "";
            }
            else {
                lblErr.innerHTML = "Please select record.";
                return false;
            }

            var start = frmDate.value.toUpperCase();
            var end = toDate.value.toUpperCase();
            var arrDate = start.split('/');
            var arrDate1 = end.split('/');

            var dateDiff = arrDate1[0] - arrDate[0]
            if (!(dateDiff + 1 == Count)) {
                lblErr.innerHTML = "Please select records as per date selection";
                return false;
            }

        }

    </script>
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvLVDetails.ClientID %>');

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
    <script type="text/javascript" language="javascript">
        function onUpdatingPopUp() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDivPopUp');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvPopUp.ClientID %>');

            // get the bounds of both the gridview and the progress div
            var gridViewBounds = Sys.UI.DomElement.getBounds(gridView);
            var updateProgressDivBounds = Sys.UI.DomElement.getBounds(updateProgressDiv);

            //    do the math to figure out where to position the element (the center of the gridview)
            var x = gridViewBounds.x + Math.round(gridViewBounds.width / 2) - Math.round(updateProgressDivBounds.width / 2);
            var y = gridViewBounds.y + Math.round(gridViewBounds.height / 2) - Math.round(updateProgressDivBounds.height / 2);

            //    set the progress element to this position
            Sys.UI.DomElement.setLocation(updateProgressDiv, x, y);
        }

        function onUpdatedPopUp() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDivPopUp');
            // make it invisible
            updateProgressDiv.style.display = 'none';
        }
    </script>
    <h3 id="lbltitle" runat="server" class="heading">
        Comp Off View</h3>
    <asp:UpdatePanel ID="UPWrapper" runat="server">
        <ContentTemplate>
            <table id="table2" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                class="TableClass">
                <tr>
                    <td align="right" style="width: 33%" class="LinkControl">
                        <%-- <asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="~/TADashboard.aspx"
                    ForeColor="Blue">Back to Time Attendance  DashBoard</asp:HyperLink>--%>
                    </td>
                </tr>
            </table>
            <div class="DivEmpDetails">
                <table id="Table4" width="100%" border="0" runat="server">
                    <tr>
                        <td width="9%">
                            <asp:Button runat="server" ID="btnAdd" Text="New" Width="90%" CssClass="ButtonControl"
                                OnClick="btnAdd_Click1" />
                        </td>
                        <td width="9%">
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" Width="90%" CssClass="ButtonControl"
                                ClientIDMode="Static" OnClick="btnDelete_Click" />
                        </td>
                        <td width="18%">
                        </td>
                        <td width="10%" style="text-align: right; color: Black; display: none;">
                            &nbsp; Comp Off Status :
                            <%-- <asp:ListItem Value="NA">Not Applied</asp:ListItem>--%>
                        </td>
                        <td width="10%" style="text-align: right; color: Black; display: none">
                            &nbsp;
                            <asp:DropDownList ID="cmbStatus" runat="server" CssClass="ComboControl" AutoPostBack="True"
                                OnSelectedIndexChanged="cmbStatus_SelectedIndexChanged">
                                <asp:ListItem Value="N">Pending</asp:ListItem>
                                <asp:ListItem Value="A">Approved</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: right;">
                            <asp:TextBox ID="txtfromDate" runat="server" CssClass="searchTextBox" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtID" runat="server" TargetControlID="txtfromDate"
                                WatermarkText="Employee ID">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:Button runat="server" ID="cmdSearch" Text="Search" CssClass="ButtonControl"
                                OnClick="cmdSearch_Click" />
                            <asp:Button runat="server" ID="cmdReset" Text="Reset" CssClass="ButtonControl" OnClick="cmdReset_Click"
                                Visible="true" OnClientClick="return ResetAll();" />
                        </td>
                    </tr>
                </table>
                <table style="width: 100%" cellpadding="0" cellspacing="0" border="1">
                    <tr>
                        <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="3">
                            <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                                <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlGrdView" runat="server" ScrollBars="Auto" Width="100%">
                                        <asp:GridView ID="gvLVDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                            AllowSorting="True" AllowPaging="true" OnDataBound="gvLVDetails_DataBound" OnPageIndexChanging="gvLVDetails_PageIndexChanging"
                                            OnSorting="gvLVDetails_sorting" ClientIDMode="Static" OnRowCommand="gvLVDetails_RowCommand"
                                            OnRowDataBound="gvLVDetails_RowDataBound" PageSize="5" GridLines="None">
                                            <RowStyle CssClass="gvRow" />
                                            <HeaderStyle CssClass="gvHeader" />
                                            <AlternatingRowStyle BackColor="#F0F0F0" />
                                            <PagerStyle CssClass="gvPager" />
                                            <%--          <EmptyDataRowStyle BackColor="#edf5ff" Height="300px" VerticalAlign="Middle" HorizontalAlign="Center" />--%>
                                            <EmptyDataTemplate>
                                                <div>
                                                    <span>No Records found.</span>
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()"
                                                            Enabled="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <%--      <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                Text="Edit" ForeColor="#3366FF"></asp:LinkButton>--%>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit3"
                                                            CommandArgument='<%#Eval("CO_ROWID") %>' Text="Edit" ForeColor="#3366FF" Enabled="true"></asp:LinkButton>
                                                        <asp:HiddenField ID="hdnLeaveStatus" runat="server" Value='<%#Eval("CO_STATUS")%>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CO_ROWID" HeaderText="ID">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CO_EMPID" HeaderText="Employee Code">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LEAVECODE" HeaderText="Code">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CO_FROMDT" HeaderText="Comp Off Date">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ESS_CO_LEAVEAGANISTDATE" HeaderText="Comp off Against">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CO_STATUS" HeaderText="Leave Status" Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="width: 100%; background: #edf5ff;">
                                                    <tr>
                                                        <td style="text-align: left; width: 15%;">
                                                            <span>Go To : </span>
                                                            <asp:DropDownList ID="ddlPageNo1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChangePage_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="text-align: center;">
                                                            <asp:Button ID="btnPrevious1" runat="server" Text="Previous" CssClass="ButtonControl"
                                                                OnClick="gvPrevious1" />
                                                            <asp:Label ID="lblShowing1" runat="server" Text="Showing "></asp:Label>
                                                            <asp:Button ID="btnNext1" runat="server" Text="Next" OnClick="gvNext1" CssClass="ButtonControl" />
                                                        </td>
                                                        <td style="text-align: right; width: 15%;">
                                                            <asp:Label ID="lblTotal1" runat="server" Text="Total Records"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                                                        </td>
                                                    </tr>--%>
                                                </table>
                                            </PagerTemplate>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cmdSearch" />
                                    <asp:AsyncPostBackTrigger ControlID="cmdReset" />
                                    <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
                                TargetControlID="UpdatePanel">
                                <Animations>
                            <OnUpdating>
                                <Parallel duration="0">
                                    <EnableAction AnimationTarget="cmdSearch" Enabled="false" />
                                    <ScriptAction Script="onUpdating();" />  
                                    <FadeOut Duration="1.0" Fps="24" minimumOpacity=".5" />
                                </Parallel> 
                            </OnUpdating>
                            <OnUpdated>
                                <Parallel duration="0">
                                    <EnableAction AnimationTarget="cmdSearch" Enabled="true" />
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
            <div align="center">
                <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
            </div>
            <asp:Button runat="server" ID="tempbtn" Style="display: none;" />
            <div style="width: 70%; height: auto;">
                <asp:Panel ID="pnlAddCompOff" runat="server" CssClass="PopupPanel" Style="height: auto;
                    width: 50%">
                    <asp:UpdatePanel ID="UpAdd" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td class="heading" colspan="4">
                                        <asp:Label ID="lblCompOffAdd" runat="server" Text="Comp Off Request">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%; text-align: left;">
                                        <asp:Label ID="lblEmp" runat="server" Text="Employee : " Style="vertical-align: top;"></asp:Label>
                                        <label class="CompulsaryLabel">
                                            *</label>
                                    </td>
                                    <td colspan="3" style="text-align: left; height: 30px;">
                                        <asp:DropDownList ID="ddlEmp" runat="server" Class="chosen-select" AutoPostBack="true"
                                            Width="380px" Style="text-align: left" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 20%;">
                                        From Date:<label class="CompulsaryLabel">*</label>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtfromDate1" runat="server" ClientIDMode="Static" MaxLength="10"
                                            Width="100%" onkeyPress="javascript: return false"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtfromDate_CalendarExtender1" TargetControlID="txtfromDate1"
                                            PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvtxtfromDate1" runat="server" ErrorMessage="Please Select Date"
                                            ControlToValidate="txtfromDate1" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtfromDate1" runat="server" TargetControlID="rfvtxtfromDate1"
                                            PopupPosition="Left">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:RegularExpressionValidator ID="revtxtfromDate1" runat="server" ErrorMessage="Not a valid date"
                                            ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                                            ControlToValidate="txtfromDate1" Display="None"></asp:RegularExpressionValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtfromDate1" runat="server" TargetControlID="revtxtfromDate1"
                                            PopupPosition="Left">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td style="text-align: left; width: 15%;">
                                        To Date:<label class="CompulsaryLabel">*</label>
                                    </td>
                                    <td style="text-align: left; width: 40%;">
                                        <asp:TextBox ID="txtToDate1" runat="server" ClientIDMode="Static" MaxLength="10"
                                            Width="100%" onkeyPress="javascript: return false"></asp:TextBox>
                                        <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                        <ajaxToolkit:CalendarExtender ID="calFrom1" TargetControlID="txtToDate1" PopupButtonID="txtshiftStartDate"
                                            runat="server" Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvtxtToDate1" runat="server" ErrorMessage="Please Select Date"
                                            ControlToValidate="txtToDate1" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtToDate1" runat="server" TargetControlID="rfvtxtToDate1"
                                            PopupPosition="Left">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:RegularExpressionValidator ID="revtxtToDate1" runat="server" ErrorMessage="RegularExpressionValidator"
                                            ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                                            ControlToValidate="txtToDate1" Display="None" ValidationGroup="Add"></asp:RegularExpressionValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtToDate1" runat="server" TargetControlID="revtxtToDate1"
                                            PopupPosition="Left">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:CompareValidator ID="vctxtToDate1" runat="server" ErrorMessage="From Date should be less then To Date"
                                            ControlToCompare="txtfromDate1" ControlToValidate="txtToDate1" Type="Date" Operator="GreaterThanEqual"
                                            Display="None" ValidationGroup="Add"></asp:CompareValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcevctxtToDate1" runat="server" TargetControlID="vctxtToDate1"
                                            PopupPosition="Left">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        <asp:Label ID="lblReason" runat="server" ClientIDMode="Static" Text="Reason:"></asp:Label>
                                        <label class="CompulsaryLabel">
                                            *</label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlReasonType" runat="server" ClientIDMode="Static" class="chosen-select"
                                            Height="20px" Width="100%">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvReasonType" runat="server" ControlToValidate="ddlReasonType"
                                            Display="None" ErrorMessage="Please select Reason" ValidationGroup="Add" ForeColor="Red"
                                            InitialValue="Select One" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceddlReasonType" runat="server" TargetControlID="rfvReasonType"
                                            PopupPosition="Left">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lblRemark" runat="server" ClientIDMode="Static" Text="Remark:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtRemark" runat="server" Style="resize: none" TextMode="MultiLine"
                                            Width="100%" MaxLength="150" CStyle="resize: none" ClientIDMode="Static" onkeyDown="checkLength(this,'150');"
                                            onkeyUp="checkLength(this,'150');" onkeypress="checkLength(this,'150');"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="gvPopUp" runat="server" AutoGenerateColumns="False" Width="100%"
                                            AllowSorting="True" AllowPaging="true" OnPageIndexChanging="gvPopUp_PageIndexChanging"
                                            OnSorting="gvPopUp_sorting" OnRowEditing="gvPopUp_RowEditing" ClientIDMode="Static"
                                            PageSize="5" GridLines="None">
                                            <RowStyle CssClass="gvRow" />
                                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <AlternatingRowStyle BackColor="#F0F0F0" />
                                            <PagerStyle CssClass="gvPager" />
                                            <EmptyDataRowStyle BackColor="#edf5ff" Height="100px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                <div id="emptydata" runat="server">
                                                    <span>No Comp OFF Available.</span>
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select" SortExpression="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="SaveRows" runat="server" ClientIDMode="Static" Enabled="true" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Code" ShowHeader="True" Visible="true" SortExpression="Employee Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMPID" runat="server" Text='<%# Eval("TDAY_EMPID") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Code" ShowHeader="True" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLeaveCode" runat="server" Text='<%# Eval("LEAVECODE") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="TDAY_INTIME" HeaderText="In Time" SortExpression="TDAY_INTIME">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TDAY_OUTIME" HeaderText="Out Time" SortExpression="TDAY_OUTIME">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Available Dates" ShowHeader="True" Visible="true"
                                                    SortExpression="Available Dates">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromDT" runat="server" Text='<%# Eval("TDAY_DATE") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" ShowHeader="True" Visible="true" SortExpression="STATUS">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTATUS" runat="server" Text='<%# Eval("TDAY_STATUS") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
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
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center;">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center;">
                                        <asp:Button ID="btnSubmitAdd" runat="server" CssClass="ButtonControl" TabIndex="16"
                                            Text="Save" OnClick="btnSubmitAdd_Click" CausesValidation="True" OnClientClick="return ValidateSave();" />
                                        <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" TabIndex="17"
                                            Text="Cancel" OnClick="btnCancelAdd_Click" CausesValidation="False" />
                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div align="center">
                        <asp:Label ID="lblAddError" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                    </div>
                </asp:Panel>
            </div>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <br />
                        <ajaxToolkit:ModalPopupExtender ID="mpeAddCompOff" runat="server" TargetControlID="tempbtn"
                            PopupControlID="pnlAddCompOff" BackgroundCssClass="modalBackground" Enabled="true">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlModifyCommon" runat="server" CssClass="PopupPanel" Style="width: 65%">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table id="table5" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                                        class="TableClass">
                                        <tr>
                                            <td class="heading" colspan="4">
                                                <asp:Label ID="lblEditHeader" runat="server" Text="Comp Off Modify">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px;" colspan="1">
                                                <asp:Label ID="lblEditEmployee" runat="server" ClientIDMode="Static" Text="Employee ID"></asp:Label>
                                            </td>
                                            <td style="height: 10px;" colspan="3">
                                                <asp:Label ID="txtEditEmployee" runat="server" ClientIDMode="Static"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px;" colspan="1">
                                                <asp:Label ID="lblCompOffAgainstDate" runat="server" ClientIDMode="Static" Text="Comp-Off Against Date"></asp:Label>
                                            </td>
                                            <td style="height: 10px;" colspan="1">
                                                <asp:TextBox ID="txtLeaveDate" runat="server" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="height: 10px;" colspan="1">
                                                <asp:Label ID="lblEditFromDate" runat="server" ClientIDMode="Static" Text="Comp-Off Date"></asp:Label>
                                            </td>
                                            <td style="height: 10px;" colspan="1">
                                                <%--    <asp:TextBox ID="txtEditFromDate" runat="server"></asp:TextBox>--%>
                                                <asp:TextBox ID="txtEditFromDate" runat="server" ClientIDMode="Static" MaxLength="10"
                                                    ValidationGroup="Modify"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEditFromDate"
                                                    PopupButtonID="txtEditFromDate" runat="server" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtEditFromDate" runat="server" ErrorMessage="Please Select Date"
                                                    ControlToValidate="txtEditFromDate" Display="None" ValidationGroup="Modify"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                    TargetControlID="rfvtxtEditFromDate" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px;" colspan="1">
                                                <asp:Label ID="lblReasonEdit" runat="server" ClientIDMode="Static" Text="Reason"></asp:Label>
                                            </td>
                                            <td class="style38">
                                                &nbsp;<asp:DropDownList ID="ddlReasonEdit" runat="server" ClientIDMode="Static" class="chosen-select"
                                                    Height="20px">
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlReasonEdit"
                                                    Display="Dynamic" ErrorMessage="Please select Reason" ValidationGroup="Modify"
                                                    ForeColor="Red" InitialValue="Select One" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="height: 5px;" colspan="1">
                                                <asp:Label ID="lblRemarkEdit" runat="server" ClientIDMode="Static" Text="Remark"></asp:Label>
                                            </td>
                                            <td style="height: 10px;" colspan="1">
                                                <asp:TextBox ID="txtRemarkEdit" runat="server" Style="resize: none" TextMode="MultiLine"
                                                    MaxLength="150" CStyle="resize: none" ClientIDMode="Static" Width="173px" onkeyDown="checkLength(this,'150');"
                                                    onkeyUp="checkLength(this,'150');" onkeypress="checkLength(this,'150');"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtRemarkEdit" runat="server" ErrorMessage="Please Select Remark"
                                                    ControlToValidate="txtRemarkEdit" Display="None" ValidationGroup="Modify"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtRemarkEdit" runat="server" TargetControlID="rfvtxtRemarkEdit"
                                                    PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="text-align: center; padding-top: 2%">
                                                <asp:Button ID="btnModifySave" runat="server" CssClass="ButtonControl" Font-Bold="true"
                                                    TabIndex="4" Text="Save" ValidationGroup="Modify" OnClick="btnModifySave_Click" />
                                                <asp:Button ID="btnModifyCancel" runat="server" CausesValidation="False" CssClass="ButtonControl"
                                                    Font-Bold="true" TabIndex="5" Text="Cancel" OnClick="btnModifyCancel_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center;">
                                                <asp:Label ID="lblErrorEdit" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                        <asp:Button ID="btnModify" runat="server" Style="display: none" CssClass="ButtonControl"
                            Font-Bold="true" TabIndex="5" Text="Modify" />
                        <ajaxToolkit:ModalPopupExtender ID="mpModifyCommon" runat="server" TargetControlID="btnModify"
                            PopupControlID="pnlModifyCommon" BackgroundCssClass="modalBackground" Enabled="true"
                            CancelControlID="btnModify">
                        </ajaxToolkit:ModalPopupExtender>
                        <br />
                    </td>
                </tr>
            </table>
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
    <script src="Scripts/Choosen/chosen.jquery.js" type="text/javascript"></script>
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
    </div>
</asp:Content>
