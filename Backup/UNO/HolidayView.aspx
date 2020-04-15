<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="HolidayView.aspx.cs" Inherits="UNO.HolidayView" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">--%>
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
        
        .divOverflow
        {
            max-height: 200px !important;
            overflow: auto !important;
            display: block !important;
        }
        .divHide
        {
            display: none !important;
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
            var gridView = $get('<%= this.gvHoliday.ClientID %>');

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
        function ResetAll() {
            $('#' + ["<%=txtHolidayName.ClientID%>", "<%=txtHolidayID.ClientID%>"].join(', #')).prop('value', "");
            document.getElementById('<%=txtHolidayName.ClientID%>').focus();
            document.getElementById('<%=txtHolidayID.ClientID%>').focus();
            document.getElementById('<%=lblMessages.ClientID%>').innerHTML = "";
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvHoliday.ClientID%>').focus();
            return false;
        }

        function ValidateLocation(GridView) {
            var GrdLoc;
            if (GridView == "A")
                GrdLoc = document.getElementById('<%=gvHolidayLocationsAdd.ClientID%>');
            else
                GrdLoc = document.getElementById('<%=gvHolidayLocationsEdit.ClientID%>');
            for (var i = 1; i <= GrdLoc.rows.length - 1; i++) {
                var OptionalCheck = GrdLoc.rows[i].cells[3].children[0].checked;
                if (OptionalCheck == true)
                    GrdLoc.rows[i].cells[0].children[0].checked = true;
            }
        }

        function ValidateOptional(GridView) {
            var GrdLoc;
            if (GridView == "A")
                GrdLoc = document.getElementById('<%=gvHolidayLocationsAdd.ClientID%>');
            else
                GrdLoc = document.getElementById('<%=gvHolidayLocationsEdit.ClientID%>');
            for (var i = 1; i <= GrdLoc.rows.length - 1; i++) {
                var LocationCheck = GrdLoc.rows[i].cells[0].children[0].checked;
                var OptionalCheck = GrdLoc.rows[i].cells[3].children[0].checked;
                if (LocationCheck == false)
                    if (OptionalCheck)
                        GrdLoc.rows[i].cells[3].children[0].checked = false;
            }
        }

        function ValidateSave(txtHolidayID, txtDescription, ddlHolidayType, txtHolidayDate, txtSwapDate, chkSelectLocation, gvHolidayLocations, lblError) {


            var count = 0;
            if (document.getElementById(txtHolidayID).value.trim() == '') {
                document.getElementById(lblError).innerHTML = "Please enter Holiday Id";
                return false;
            }
            if (document.getElementById(txtDescription).value.trim() == '') {
                document.getElementById(lblError).innerHTML = "Please enter Holiday Description";
                return false;
            }
            if (document.getElementById(ddlHolidayType).value.trim() == '0') {
                document.getElementById(lblError).innerHTML = "Please select Holiday type";
                return false;
            }
            if (document.getElementById(txtHolidayDate).value.trim() == '') {
                document.getElementById(lblError).innerHTML = "Please enter Holiday Date";
                return false;
            }

            if (document.getElementById(txtSwapDate).value != undefined) {
                var start = document.getElementById(txtHolidayDate).value.toUpperCase();
                var end = document.getElementById(txtSwapDate).value.toUpperCase();
                var arrDate = start.split('/');
                var arrDate1 = end.split('/');
                var date1 = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
                var date2 = new Date(arrDate1[2], arrDate1[1] - 1, arrDate1[0]);
                if (date1 >= date2) {
                    document.getElementById(lblError).innerHTML = "Swap Date should not be same or less than holiday date";
                    return false;
                }
            }

            if ($('#' + chkSelectLocation + ' input:checked').val() == 'S') {
                if (document.getElementById(gvHolidayLocations) != undefined)
                    var gvLocation = document.getElementById(gvHolidayLocations);
                for (var i = 1; i <= gvLocation.rows.length - 1; i++) {
                    if (gvLocation.rows[i].cells[0].children[0].checked) {
                        count = 1;
                        break;
                    }
                }
                if (count == 0) {
                    document.getElementById(lblError).innerHTML = "Please select atleast one location";
                    return false;
                }
            }

            document.getElementById(lblError).innerHTML = "";
            return true;


        }

        function ddlHolidayTypeOnChange(ddlHolidayType, chkSelectLocation, gvHolidayLocations, AddOrEdit) {

            if (document.getElementById(ddlHolidayType).value.trim() == 'N') {
                $('#' + [chkSelectLocation].join(', #')).find('input').prop('disabled', true);
                document.getElementById(chkSelectLocation).enabled = false;
                document.getElementById(chkSelectLocation + '_0').checked = true;

                var gvLocation = document.getElementById(gvHolidayLocations);
                for (var i = 1; i <= gvLocation.rows.length - 1; i++) {
                    gvLocation.rows[i].cells[0].children[0].checked = false;
                    gvLocation.rows[i].cells[3].children[0].checked = false;
                }

                if (AddOrEdit == 'A') {

                    $("#divgvHolidayLocationsAdd").removeClass("divOverflow");
                    $("#divgvHolidayLocationsAdd").addClass("divHide");
                }

                else {
                    $("#divgvHolidayLocationsEdit").removeClass("divOverflow");
                    $("#divgvHolidayLocationsEdit").addClass("divHide");

                }

            }
            else {
                $('#' + [chkSelectLocation].join(', #')).find('input').prop('disabled', false);
            }
            if ($('#' + chkSelectLocation + ' input:checked').val() == 'S') {

                var gvLocation = document.getElementById(gvHolidayLocations);
                for (var i = 1; i <= gvLocation.rows.length - 1; i++) {
                    gvLocation.rows[i].cells[0].children[0].checked = false;
                    gvLocation.rows[i].cells[3].children[0].checked = false;
                }

                if (AddOrEdit == 'A') {
                    $("#divgvHolidayLocationsAdd").removeClass("divHide");
                    $("#divgvHolidayLocationsAdd").addClass("divOverflow");
                }
                else {
                    $("#divgvHolidayLocationsEdit").removeClass("divHide");
                    $("#divgvHolidayLocationsEdit").addClass("divOverflow");
                }

            }
            else {

                if (AddOrEdit == 'A') {
                    $("#divgvHolidayLocationsAdd").removeClass("divOverflow");
                    $("#divgvHolidayLocationsAdd").addClass("divHide");

                }
                else {
                    $("#divgvHolidayLocationsEdit").removeClass("divOverflow");
                    $("#divgvHolidayLocationsEdit").addClass("divHide");
                }


            }
        }

        function CloseAddPopup() {
            $find('mpeBAddHolidays').hide();           
            return false;
        }
        function CloseEditPopup() {
            $find('mpeBEditHolidays').hide();
            return false;
        }
                   

    </script>
    <!--[if IE]>
<style>
    .DivEmpDetails {
                     text-align: center;
            width: 95%;
            border: 1px solid #333333;
            border-radius: 15px;
            margin: 10px 10px 10px 10px;
            padding: 10px 10px 10px 10px;
            background-color:#53AEF3;
            font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;
    }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Holiday View" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <asp:Panel ID="Panel3" runat="server" DefaultButton="btnSearch">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%; text-align: left;">
                            <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click" />
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                                OnClick="btnDelete_Click" />
                        </td>
                        <td style="width: 50%; text-align: right;">
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                                OnClientClick="return ResetAll();" onclick="btnReset_Click" />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;"
                                CssClass="ButtonControl" OnClick="btnSearch_Click" />
                            <asp:TextBox ID="txtHolidayName" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtHolidayName" runat="server" TargetControlID="txtHolidayName"
                                WatermarkText="Description" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:TextBox ID="txtHolidayID" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtHolidayID" runat="server" TargetControlID="txtHolidayID"
                                WatermarkText="ID" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                            <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                                <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvHoliday" runat="server" AutoGenerateColumns="false" Width="100%"
                                        GridLines="None" AllowPaging="true" PageSize="10" 
                                        OnRowCommand="gvHoliday_RowCommand" onrowdatabound="gvHoliday_RowDataBound">
                                        <RowStyle CssClass="gvRow" />
                                        <HeaderStyle CssClass="gvHeader" />
                                        <AlternatingRowStyle BackColor="#F0F0F0" />
                                        <PagerStyle CssClass="gvPager" />
                                        <EmptyDataTemplate>
                                            <div>
                                                <span>No Holidays found.</span>
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" SortExpression="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                                    <asp:HiddenField ID="hdnTdayFlag" runat="server" Value='<%#Eval("Flag")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" SortExpression="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                        Text="Edit" ForeColor="#3366FF" CommandArgument='<%#Eval("HOLIDAY_ID")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="HOLIDAY_ID" HeaderText="Holiday ID" SortExpression="Holiday ID">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="HOLIDAY_DESCRIPTION" HeaderText="Holiday Description"
                                                SortExpression="Holiday Description">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="HOLIDAY_TYPE" HeaderText="Holiday Type" SortExpression="Holiday Type">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Holiday Date" ShowHeader="true" SortExpression="Holiday Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHOLIDAY_DATE" runat="server" Text='<%#Eval("HOLIDAYDATE")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="HOLIDAYSWAP" HeaderText="Holiday Swap" SortExpression="Holidate Swap">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
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
                                </ContentTemplate>
                                <Triggers>
                                    <%--  <asp:PostBackTrigger ControlID="btnAdd" />--%>
                                    <asp:PostBackTrigger ControlID="btnDelete" />
                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                                    <asp:PostBackTrigger ControlID="btnSubmitAdd" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
                                TargetControlID="UpdatePanel1">
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
            </asp:Panel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Button ID="btnDummyEdit" runat="server" Text="Button" Style="display: none;" />
    <asp:Panel ID="pnlAddHoliday" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table id="table2" runat="server" width="100%" border="0" cellpadding="0" cellspacing="5"
                    class="TableClass">
                    <tr>
                        <td style="text-align: right;">
                            Holiday Id :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtHolidayIDAdd" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="8" Style="text-transform: uppercase;" TabIndex="1"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                FilterType="Numbers,Custom,LowercaseLetters,UppercaseLetters" TargetControlID="txtHolidayIDAdd"
                                ValidChars="-" />
                        </td>
                        <td style="text-align: right">
                            Holiday Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtDescriptionAdd" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="20" Style="text-transform: capitalize;" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Holiday Type :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList ID="ddlHolidayTypeAdd" runat="server" ClientIDMode="Static" Width="150px"
                                TabIndex="3" AutoPostBack="false" />
                        </td>
                        <td style="text-align: right">
                            Holiday Date&nbsp;:<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtHolidayDateAdd" runat="server" CssClass="TextControl" ClientIDMode="Static"
                                onKeyPress="javascript: return false "></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calHolidayDateAdd" runat="server" TargetControlID="txtHolidayDateAdd"
                                PopupButtonID="txtHolidayDateAdd" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Holiday Swap Date:
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtSwapDateAdd" runat="server" CssClass="TextControl" ClientIDMode="Static"
                                onKeyPress="javascript: return false "></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calSwapDateAdd" runat="server" TargetControlID="txtSwapDateAdd"
                                PopupButtonID="txtSwapDateAdd" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td style="text-align: right">
                            Location :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:RadioButtonList ID="chkSelectLocation" runat="server" RepeatDirection="Horizontal"
                                ClientIDMode="Static" AutoPostBack="false">
                                <asp:ListItem Value="A" Selected="True">All</asp:ListItem>
                                <asp:ListItem Value="S">Select Location</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="max-height: 200px; overflow: auto;" id="divgvHolidayLocationsAdd" runat="server"
                                clientidmode="Static">
                                <asp:GridView ID="gvHolidayLocationsAdd" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="false" AllowSorting="false" Width="100%">
                                    <RowStyle CssClass="gvRow" />
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                    <PagerStyle CssClass="gvPager" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" SortExpression="Select">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="ValidateOptional('A')" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="OCE_ID" HeaderText="Location Code" SortExpression="Location Code">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OCE_DESCRIPTION" HeaderText="Location Name" SortExpression="Location Name">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Optional" SortExpression="Select">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="optionalChk" runat="server" ClientIDMode="Static" Onclick="ValidateLocation('A')" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnSubmitAdd" runat="server" CssClass="ButtonControl" TabIndex="16"
                                Text="Save" OnClick="btnSubmitAdd_Click" />
                            <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" TabIndex="17"
                                Text="Cancel" OnClientClick="return CloseAddPopup();" />
                            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Label ID="lblErrorAdd" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="chkSelectLocation" />
                <asp:AsyncPostBackTrigger ControlID="gvHolidayLocationsAdd" />
                <asp:AsyncPostBackTrigger ControlID="ddlHolidayTypeAdd" />
                <%--<asp:AsyncPostBackTrigger ControlID="btnAdd" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="btnCancelDummy" runat="server" Text="Button" Style="display: none;" />
    <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddHolidays" runat="server" TargetControlID="Button1"
        PopupControlID="pnlAddHoliday" BackgroundCssClass="modalBackground" Enabled="true"
        BehaviorID="mpeBAddHolidays" CancelControlID="btnCancelDummy">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlEditHolidays" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table id="table1" runat="server" width="100%" border="0" cellpadding="0" cellspacing="5"
                    class="TableClass">
                    <tr>
                        <td style="text-align: right;">
                            Holiday Id :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtHolidayIDEdit" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="8" Style="text-transform: capitalize;" TabIndex="1"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                FilterType="Numbers,Custom,LowercaseLetters,UppercaseLetters" TargetControlID="txtHolidayIDEdit"
                                ValidChars="-" />
                        </td>
                        <td style="text-align: right">
                            Holiday Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtDescriptionEdit" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="20" Style="text-transform: capitalize;" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Holiday Type :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList ID="ddlHolidayTypeEdit" runat="server" ClientIDMode="Static" CssClass="ComboControl"
                                TabIndex="3" AutoPostBack="false" />
                        </td>
                        <td style="text-align: right">
                            Holiday Date&nbsp;:<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtHolidayDateEdit" runat="server" CssClass="TextControl" ClientIDMode="Static"
                                onKeyPress="javascript: return false "></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calHolidayDateEdit" runat="server" TargetControlID="txtHolidayDateEdit"
                                PopupButtonID="txtHolidayDateEdit" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Holiday Swap Date:
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtSwapDateEdit" runat="server" CssClass="TextControl" ClientIDMode="Static"
                                onKeyPress="javascript: return false "></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calSwapDateEdit" runat="server" TargetControlID="txtSwapDateEdit"
                                PopupButtonID="txtSwapDateEdit" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td style="text-align: right">
                            Location :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:RadioButtonList ID="chkSelectLocationEdit" runat="server" RepeatDirection="Horizontal"
                                ClientIDMode="Static" AutoPostBack="false">
                                <asp:ListItem Value="A" Selected="True">All</asp:ListItem>
                                <asp:ListItem Value="S">Select Location</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="max-height: 200px; overflow: auto;" id="divgvHolidayLocationsEdit" runat="server"
                                clientidmode="Static">
                                <asp:GridView ID="gvHolidayLocationsEdit" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="false" AllowSorting="false" Width="100%">
                                    <RowStyle CssClass="gvRow" />
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                    <PagerStyle CssClass="gvPager" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" SortExpression="Select">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="ValidateOptional('E')" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="OCE_ID" HeaderText="Location Code" SortExpression="Location Code">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OCE_DESCRIPTION" HeaderText="Location Name" SortExpression="Location Name">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Optional" SortExpression="Select">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="optionalChk" runat="server" ClientIDMode="Static" Onclick="ValidateLocation('E')" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnSubmitEdit" runat="server" CssClass="ButtonControl" TabIndex="16"
                                Text="Save" OnClick="btnSubmitEdit_Click" />
                            <asp:Button ID="btnCancelEdit" runat="server" CssClass="ButtonControl" TabIndex="17"
                                Text="Cancel" OnClientClick="return CloseEditPopup();" />
                            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                <asp:Label ID="lab" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Label ID="lblErrorEdit" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlHolidayTypeEdit" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeEditHolidays" runat="server" TargetControlID="btnDummyEdit"
        PopupControlID="pnlEditHolidays" BackgroundCssClass="modalBackground" Enabled="true"
        BehaviorID="mpeBEditHolidays" CancelControlID="btnCancelEdit">
    </ajaxToolkit:ModalPopupExtender>
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
