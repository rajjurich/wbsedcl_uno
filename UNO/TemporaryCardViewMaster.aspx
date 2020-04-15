<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemporaryCardViewMaster.aspx.cs"
    MasterPageFile="~/ModuleMain.master" Inherits="UNO.TemporaryCardViewMaster" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            var gridView = $get('<%= this.gvTempCard.ClientID %>');

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

        function chkDateRange(source, args) {

            var startDate = document.getElementById('<%=txtIssueDateAdd.ClientID %>').value;
            var EndDate = args.Value;
            //Year,month,date
            var strt = new Date(startDate.substring(6, 10), parseInt(startDate.substring(3, 5)) - 1, startDate.substring(0, 2));
            var End = new Date(EndDate.substring(6, 10), parseInt(EndDate.substring(3, 5)) - 1, EndDate.substring(0, 2));

            if (startDate == EndDate) {
                args.IsValid = true;
            }
            else if (strt < End) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }

        }


        function chkEditDateRange(source, args) {

            var startDate = document.getElementById('<%=txtIssueDateEdit.ClientID %>').value;
            var EndDate = args.Value;
            //Year,month,date
            var strt = new Date(startDate.substring(6, 10), parseInt(startDate.substring(3, 5)) - 1, startDate.substring(0, 2));
            var End = new Date(EndDate.substring(6, 10), parseInt(EndDate.substring(3, 5)) - 1, EndDate.substring(0, 2));

            if (startDate == EndDate) {
                args.IsValid = true;
            }
            else if (strt < End) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }

        }

    </script>
    <style>
        .textBold
        {
            text-align: center;
            vertical-align: middle;
            font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;
            font-weight: bold;
            color: Black;
            font-size: x-large;
        }
        
        .watermark
        {
            color: Gray;
            font-size: xx-small;
            height: 17px;
            width: 120px;
            border-radius: 15px;
            margin-right: 10px;
        }
        .searchTextBox
        {
            height: 17px;
            width: 120px;
            font-size: xx-small;
            border-radius: 15px;
            margin-right: 10px;
        }
        .DivEmpDetails
        {
            text-align: center;
            width: 95%; /*border: 1px solid #333333;*/
            border-radius: 15px;
            background-color: #47A3DA;
            margin: 10px 10px 10px 10px;
            padding: 10px 10px 10px 10px; /*min-height: 200px;*/ /*font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;*/
            box-shadow: 10px 10px 5px #888888;
        }
        .gvHeader
        {
            background-color: transparent;
            border: 0px solid #66B7F5;
            max-height: 29px;
            height: 29px;
            min-height: 29px;
        }
        gvAlternateRow
        {
        }
        .gvRow
        {
            border-bottom: 1px solid #C3C3C3;
            max-height: 26px;
            height: 26px;
            min-height: 26px;
        }
        .gvPager
        {
            vertical-align: bottom;
        }
        .center
        {
            text-align: center;
        }
    </style>
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
                <asp:Label ID="lblHead" runat="server" Text="Temporary Card View" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div class="DivEmpDetails">
        <table style="width: 100%;">
            <tr>
                <td style="width: 50%; text-align: left;">
                    <asp:Button runat="server" ID="ButtonAdd" Text="New" CssClass="ButtonControl" OnClick="ButtonAdd_Click" />
                    <asp:Button runat="server" ID="ButtonDelete" Text="Delete" CssClass="ButtonControl"
                        OnClick="ButtonDelete_Click" />
                </td>
                <td style="width: 50%; text-align: right;">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                        OnClick="btnSearch_Click" />
                    <asp:TextBox ID="txtCompanyID" onkeydown="return (event.keyCode!=13);" runat="server"
                        Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCompanyID" runat="server" TargetControlID="txtCompanyID"
                        WatermarkText="Search by Card Code" WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    <asp:TextBox ID="txtCompanyName" onkeydown="return (event.keyCode!=13);" runat="server"
                        Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCompanyName" runat="server" TargetControlID="txtCompanyName"
                        WatermarkText="Search by Employee Code" WatermarkCssClass="watermark">
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
                            <asp:GridView ID="gvTempCard" runat="server" AutoGenerateColumns="False" Width="100%"
                                GridLines="None" AllowPaging="True" OnPageIndexChanging="gvCompany_PageIndexChanging"
                                OnRowCommand="gvCompany_RowCommand">
                                <RowStyle CssClass="gvRow" />
                                <HeaderStyle CssClass="gvHeader" />
                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                <PagerStyle CssClass="gvPager" />
                                <EmptyDataTemplate>
                                    <div>
                                        <span>No Cards found.</span>
                                    </div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Select" SortExpression="Select" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" SortExpression="Edit" ItemStyle-Width="5%" HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                Text="Edit" ForeColor="#3366FF" CommandArgument='<%#Eval("TC_TMP_CARD_ID")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TC_TMP_CARD_ID" HeaderText="Temporary Card Code" SortExpression="TC_TMP_CARD_ID"
                                        ItemStyle-Width="14%">
                                        <%--  <ItemStyle HorizontalAlign="Left" Width="10%"  />--%>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TC_EMPLOYEE_ID" HeaderText="Employee Code" ItemStyle-Wrap="true"
                                        SortExpression="TC_EMPLOYEE_ID">
                                        <ItemStyle Wrap="True" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TC_ISSUEDT" HeaderText="Issue Date" ItemStyle-Width="10%"
                                        ItemStyle-Wrap="true" SortExpression="TC_ISSUEDT">
                                        <ItemStyle Wrap="True" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TC_RETURNDT" HeaderText="Return Date" SortExpression="TC_RETURNDT"
                                        ItemStyle-Width="10%">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TC_REASON_ID" HeaderText="Reason Code" ItemStyle-Width="10%"
                                        SortExpression="TC_REASON_ID">
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
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
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
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <%--    <table>
            <tr>
            <td style="text-align:center;" align="center">
                <asp:Label ID="lblMessages" runat="server" Text="" Visible="false"></asp:Label>
            </td>
            </tr>
            
            </table>--%>
            <div style="text-align: center">
                <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--   <asp:AsyncPostBackTrigger ControlID="ButtonAdd" />--%>
            <asp:AsyncPostBackTrigger ControlID="ButtonDelete" />
            <asp:AsyncPostBackTrigger ControlID="gvTempCard" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnSubmitAdd" />
            <asp:AsyncPostBackTrigger ControlID="btnSubmitEdit" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlAddTC" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="table4" runat="server" width="100%" height="90%" border="0" cellpadding="0"
                    cellspacing="5" class="TableClass">
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            Temporary Card Code :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtTempCardIDAdd" runat="server" CssClass="TextControl" MaxLength="10"
                                TabIndex="1" Style="text-transform: uppercase;" ClientIDMode="Static" ValidationGroup="Add"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_empid_add" runat="server" ControlToValidate="txtTempCardIDAdd"
                                Display="None" ErrorMessage="Please enter Code." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="rfvtxtPlannedStartDateEdit_ValidatorCalloutExtender"
                                runat="server" PopupPosition="Right" TargetControlID="rfv_empid_add">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            Employee ID :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtEmpCdAdd" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="10" TabIndex="2" ValidationGroup="Add"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_empcode_add" runat="server" ControlToValidate="txtEmpCdAdd"
                                Display="None" ErrorMessage="Please enter Code." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                PopupPosition="Right" TargetControlID="rfv_empcode_add">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            Issue Date :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtIssueDateAdd" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                onKeyPress="javascript: return false" MaxLength="15" TabIndex="3" ValidationGroup="Add"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtIssueDateAdd"
                                PopupPosition="Right" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfv_RequestDate_add" runat="server" ControlToValidate="txtIssueDateAdd"
                                Display="None" ErrorMessage="Please select Date" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                PopupPosition="Right" TargetControlID="rfv_RequestDate_add">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            Return Date :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtReturnDateAdd" runat="server" CssClass="TextControl" TabIndex="4"
                                onKeyPress="javascript: return false " ClientIDMode="Static" ValidationGroup="Add"></asp:TextBox>
                          <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReturnDateAdd"
                                Display="Dynamic" ErrorMessage="Please select Date" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>--%>
                            <%-- <asp:CompareValidator runat="server"  Display="None" ForeColor="Red" ValidationGroup="Add" id="cmpNumbers" controltovalidate="txtReturnDateAdd" controltocompare="txtIssueDateAdd" operator="GreaterThan" type="Date" errormessage="Return Date less than Issue date" />--%>
                            <asp:CustomValidator ID="cmpNumbers" runat="server" ErrorMessage="Return Date  is less than Issue date"
                                ValidationGroup="Add" ControlToValidate="txtReturnDateAdd" ForeColor="Red" Display="None"
                                ClientValidationFunction="chkDateRange"></asp:CustomValidator>
                            <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtReturnDateAdd"
                                Format="dd/MM/yyyy" PopupPosition="Right">
                            </ajaxToolkit:CalendarExtender>
                            <%--<asp:RequiredFieldValidator ID="rfv_ReturnDate_add" runat="server" ControlToValidate="txtReturnDateAdd"
                                Display="None" ErrorMessage="Please select Date" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3"
                                    runat="server" PopupPosition="Right" TargetControlID="rfv_ReturnDate_add">--%>
                            <%-- </ajaxToolkit:ValidatorCalloutExtender>--%>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                PopupPosition="Right" TargetControlID="cmpNumbers">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            Reason For Issue :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;" colspan="3">
                            <asp:DropDownList ID="ddlReasonAdd" runat="server" ClientIDMode="Static" TabIndex="5"
                                ValidationGroup="Add" class="chosen-select">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv_reason_add" runat="server" ControlToValidate="ddlReasonAdd"
                                Display="None" ErrorMessage="Please select Reason" ForeColor="Red" InitialValue="0"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                PopupPosition="Right" TargetControlID="rfv_reason_add">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnSubmitAdd" runat="server" CssClass="ButtonControl" TabIndex="6"
                                Text="Save" OnClick="btnSaveAdd_Click" ValidationGroup="Add" />
                            <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" TabIndex="7"
                                Text="Cancel" OnClick="btnCancelAdd_Click" />
                            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Label ID="lblErrorAdd" runat="server" Visible="False" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSubmitAdd" />
                <%--<asp:AsyncPostBackTrigger ControlID="ButtonAdd"/>--%>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button1" runat="server" TabIndex="17" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddTC" runat="server" TargetControlID="Button1"
        PopupControlID="pnlAddTC" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancelAdd">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlEditTC" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table id="table1" runat="server" width="100%" height="90%" border="0" cellpadding="0"
                    cellspacing="5" class="TableClass">
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            Temporary Card Code :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtTempCardIDEdit" runat="server" CssClass="TextControl" MaxLength="10"
                                TabIndex="1" Style="text-transform: uppercase;" ClientIDMode="Static" ValidationGroup="Edit"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_empid_Edit" runat="server" ControlToValidate="txtTempCardIDEdit"
                                Display="Dynamic" ErrorMessage="Please enter Code." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Edit"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            Employee ID :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtEmpCdEdit" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="10" TabIndex="12" ValidationGroup="Edit" Enabled="False"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_empcode_Edit" runat="server" ControlToValidate="txtEmpCdEdit"
                                Display="Dynamic" ErrorMessage="Please enter Code." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Edit"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            Issue Date :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtIssueDateEdit" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                onKeyPress="javascript: return false " MaxLength="15" TabIndex="7" ValidationGroup="Edit"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtIssueDateEdit"
                                Format="dd/MM/yyyy" PopupPosition="Right">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfv_RequestDate_Edit" runat="server" ControlToValidate="txtIssueDateEdit"
                                Display="Dynamic" ErrorMessage="Please select Date" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Edit"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            Return Date :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtReturnDateEdit" runat="server" CssClass="TextControl" TabIndex="7"
                                onKeyPress="javascript: return false " MaxLength="15" ClientIDMode="Static" ValidationGroup="Edit"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtReturnDateEdit"
                                Format="dd/MM/yyyy" PopupPosition="Right">
                            </ajaxToolkit:CalendarExtender>
                           <%-- <asp:RequiredFieldValidator ID="rfv_ReturnDate_Edit" runat="server" ControlToValidate="txtReturnDateEdit"
                                Display="Dynamic" ErrorMessage="Please select Date" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Edit"></asp:RequiredFieldValidator>--%>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Return Date  is less than Issue date"
                                ValidationGroup="Edit" ControlToValidate="txtReturnDateEdit" ForeColor="Red"
                                Display="None" ClientValidationFunction="chkEditDateRange"></asp:CustomValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            Reason For Issue :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;" colspan="3">
                            <asp:DropDownList ID="ddlReasonEdit" runat="server" ClientIDMode="Static" TabIndex="6"
                                ValidationGroup="Edit">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv_reason_edit" runat="server" ControlToValidate="ddlReasonEdit"
                                Display="Dynamic" ErrorMessage="Please select Reason" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Edit" InitialValue="Select One"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnSubmitEdit" runat="server" CssClass="ButtonControl" TabIndex="16"
                                Text="Save" OnClick="btnSaveEdit_Click" />
                            <asp:Button ID="btnCancelEdit" runat="server" CssClass="ButtonControl" TabIndex="17"
                                Text="Cancel" OnClick="btnCancelEdit_Click" />
                            <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Center">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center;">
                            <asp:Label ID="lblErrorEdit" runat="server" Visible="False" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSubmitEdit" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:LinkButton ID="lnkDummyEdit" runat="server" Style="display: none;">edit</asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="mpeEditTC" runat="server" TargetControlID="lnkDummyEdit"
        PopupControlID="pnlEditTC" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancelEdit">
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
