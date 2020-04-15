<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="AdminAnnouncement.aspx.cs" Inherits="UNO.AdminAnnouncement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvAnnouncement.ClientID %>');

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
    <script type="text/javascript">

        function CompareDates(d1, d2) {
            var start = d1.value.toUpperCase();
            var end = d2.value.toUpperCase();
            var arrDate = start.split('/');
            var arrDate1 = end.split('/');
            d1 = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
            d2 = new Date(arrDate1[2], arrDate1[1] - 1, arrDate1[0]);
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

            if (!CompareDates(document.getElementById('<%= txtFrmDate.ClientID %>'), document.getElementById('<%= txtTDate.ClientID %>'))) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }

        }
        function CompareDates1(d1, d2) {
            var start = d1.value.toUpperCase();
            var end = d2.value.toUpperCase();
            var arrDate = start.split('/');
            var arrDate1 = end.split('/');
            d1 = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
            d2 = new Date(arrDate1[2], arrDate1[1] - 1, arrDate1[0]);
            //1 year =31,536,000,000 milli seconds
            //  if ((d2 - d1) > '31622400000') {
            if ((d2 - d1) > '2628000000') {
                document.getElementById('<%= CustomValidator1.ClientID %>').innerHTML = "Difference between two dates should not be greater than a Month"
                return false;
            }
            else if (d2 < d1) {
                document.getElementById('<%= CustomValidator1.ClientID %>').innerHTML = "To Date should not be less than From date"

                return false;
            }
            return true;
        }

        function isValidateIssueDate1(oSrc, args) {

            if (!CompareDates1(document.getElementById('<%= txtFrmDateEdit.ClientID %>'), document.getElementById('<%= txtTDateEdit.ClientID %>'))) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Announcement" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%; text-align: left;">
                        <asp:Button runat="server" ID="btnNew" Text="New" CssClass="ButtonControl" OnClick="New" />
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                            OnClick="btnDelete_Click" />
                    </td>
                    <td style="width: 50%; text-align: right;">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                            OnClick="btnSearch_Click" ValidationGroup="Search" />
                        <asp:TextBox ID="txtTodate" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCallStatus" runat="server" TargetControlID="txtTodate"
                            WatermarkText="Search by To Date" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTodate"
                            PopupButtonID="txtTodate" Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtTodate"
                            Display="None" ValidationGroup="Search" ErrorMessage="Please enter date in dd/mm/yyyy format"
                            ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                            TargetControlID="RegularExpressionValidator3" PopupPosition="left">
                        </ajaxToolkit:ValidatorCalloutExtender>
                        <asp:TextBox ID="txtFromDate" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWEtxtCallDate" runat="server" TargetControlID="txtFromDate"
                            WatermarkText="Search by From Date" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <ajaxToolkit:CalendarExtender ID="caltxtCallDate" runat="server" TargetControlID="txtFromDate"
                            PopupButtonID="txtFromDate" Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtFromDate"
                            Display="None" ValidationGroup="Search" ErrorMessage="Please enter date in dd/mm/yyyy format"
                            ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                            TargetControlID="RegularExpressionValidator4" PopupPosition="Right">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                        <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                            <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvAnnouncement" runat="server" AutoGenerateColumns="false" Width="100%"
                                    GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="gvAnnouncement_RowCommand">
                                    <RowStyle CssClass="gvRow" />
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                    <PagerStyle CssClass="gvPager" />
                                    <EmptyDataTemplate>
                                        <div>
                                            <span>No Record found.</span>
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" SortExpression="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ShowHeader="False" SortExpression="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                    CommandArgument='<%#Eval("intID") %>' Text="Edit" ForeColor="#3366FF"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intID" HeaderText="ID" SortExpression="ID" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="varHeadLine" HeaderText="HeadLine Content" SortExpression="ECode"
                                            ItemStyle-Wrap="true">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dtFromDate" HeaderText="From Date" SortExpression="Name">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dtToDate" HeaderText="To Date" SortExpression="FromDate">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowID" runat="server" Text='<%#Eval("intID")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
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
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
                            TargetControlID="UpdatePanel2">
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
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <%-- <asp:AsyncPostBackTrigger ControlID="btnAdd" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="gvAnnouncement" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                <%--   <asp:AsyncPostBackTrigger ControlID="btnSubmitAdd" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmitEdit" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <%-- NEW--%>
    <asp:Panel ID="Panel1" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            Headline Content :
                        </td>
                        <td>
                            <textarea id="txtContent" cols="60" rows="7" runat="server"></textarea>
                            <%--    <asp:TextBox ID="txtContent" runat="server"></asp:TextBox>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContent"
                                Display="none" ErrorMessage="Please Enter Content" SetFocusOnError="True" ForeColor="Red"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                TargetControlID="RequiredFieldValidator2" PopupPosition="right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            From Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtFrmDate" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFrmDate"
                                PopupButtonID="txtFrmDate" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="Reqfrm_time" runat="server" ControlToValidate="txtFrmDate"
                                Display="none" ErrorMessage="Please Enter From Date" SetFocusOnError="True" ForeColor="Red"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                TargetControlID="Reqfrm_time" PopupPosition="right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFrmDate"
                                Display="None" ValidationGroup="Add" ErrorMessage="Please enter date in dd/mm/yyyy format"
                                ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                TargetControlID="RegularExpressionValidator2" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            To Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTDate" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtTDate"
                                PopupButtonID="txtTDate" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <asp:CustomValidator ID="CustomValidatorM_Lab_From" runat="server" ClientValidationFunction="isValidateIssueDate"
                                ValidationGroup="Add" ControlToValidate="txtTDate" Display="None" ErrorMessage="To Date should not be less than From date"
                                ForeColor="Red" SetFocusOnError="True"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                TargetControlID="CustomValidatorM_Lab_From" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTDate"
                                Display="None" ValidationGroup="Add" ErrorMessage="Please enter date in dd/mm/yyyy format"
                                ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                TargetControlID="RegularExpressionValidator1" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonControl" Text="Save" ValidationGroup="Add"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="ButtonControl" Text="Cancel"
                                OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
     <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddCall" runat="server" TargetControlID="Button2"
        PopupControlID="Panel1" BackgroundCssClass="modalBackground" Enabled="true" CancelControlID="btnCancel">
    </ajaxToolkit:ModalPopupExtender>
    <%-- Edit--%>
    <asp:Panel ID="Panel2" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            Headline Content :
                        </td>
                        <td>
                            <textarea id="txtContentEdit" cols="60" rows="7" runat="server"></textarea>
                            <%--     <asp:TextBox ID="txtContentEdit" runat="server"></asp:TextBox>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtContentEdit"
                                Display="none" ErrorMessage="Please Enter From Date" SetFocusOnError="True" ForeColor="Red"
                                ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server"
                                TargetControlID="RequiredFieldValidator1" PopupPosition="right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            From Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtFrmDateEdit" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtFrmDateEdit"
                                PopupButtonID="txtFrmDateEdit" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="Reqfrm_timeEdit" runat="server" ControlToValidate="txtFrmDateEdit"
                                Display="none" ErrorMessage="Please Enter From Date" SetFocusOnError="True" ForeColor="Red"
                                ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                TargetControlID="Reqfrm_timeEdit" PopupPosition="right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtFrmDateEdit"
                                Display="None" ValidationGroup="Edit" ErrorMessage="Please enter date in dd/mm/yyyy format"
                                ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                TargetControlID="RegularExpressionValidator5" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            To Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTDateEdit" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtTDateEdit"
                                PopupButtonID="txtTDateEdit" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="isValidateIssueDate1"
                                ValidationGroup="Edit" ControlToValidate="txtTDateEdit" Display="None" ErrorMessage="To Date should not be less than From date"
                                ForeColor="Red" SetFocusOnError="True"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                TargetControlID="CustomValidator1" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtTDateEdit"
                                Display="None" ValidationGroup="Edit" ErrorMessage="Please enter date in dd/mm/yyyy format"
                                ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                TargetControlID="RegularExpressionValidator6" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnEditSave" runat="server" CssClass="ButtonControl" Text="Save"
                                ValidationGroup="Edit" OnClick="btnEditSave_Click" />
                            <asp:Button ID="btnEditCancel" runat="server" CssClass="ButtonControl" Text="Cancel"
                                OnClick="btnEditCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="mpeEditContent" runat="server" TargetControlID="Button1"
        PopupControlID="Panel2" BackgroundCssClass="modalBackground" Enabled="true" CancelControlID="btnEditCancel">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
