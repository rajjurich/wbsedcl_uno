<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="LeaveRuleNew.aspx.cs" Inherits="UNO.LeaveRuleNew" %>

<%--<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="Leave_Data_Upload.aspx.cs" Inherits="UNO.Leave_Data_Upload" %>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript">
        function Isdigit(e) {
            var keyCode = e.which ? e.which : e.keyCode;
            var result = (keyCode >= 48 && keyCode <= 57);
            if (keyCode >= 48 && keyCode <= 57)
                return result;
            else {
                if (keyCode == 8 || keyCode == 46 || keyCode == 9) //9-tab,8-backspace,46-delete
                    return true;
                else
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
            var gridView = $get('<%= this.gvLeaveData.ClientID %>');

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


        var myVals = new Array();
        function CacheValues() {
            var l = document.getElementById('lstEmployees');

            for (var i = 0; i < l.options.length; i++) {
                myVals[i] = l.options[i].text;
            }
        }
        function SearchList() {

            var l = document.getElementById('lstEmployees');
            var tb = document.getElementById('txtSearchBox');

            l.options.length = 0;

            if (tb.value == "") {
                for (var i = 0; i < myVals.length; i++) {
                    l.options[l.options.length] = new Option(myVals[i]);
                }
            }
            else {


                for (var i = 0; i < myVals.length; i++) {
                    if (myVals[i].toLowerCase().indexOf(tb.value.toLowerCase()) != -1) {
                        l.options[l.options.length] = new Option(myVals[i]);
                    }
                    else {
                        // do nothing
                    }
                }
            }
        }

        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }

//        function CloseAdd() {
//            $find('mpeBAddNewEntry').hide();
//            return false;
//        }
        function EditClose() {
            $find('mpeBModifyZone').hide();
            return false;
        }

    </script>
    <script language="javascript" type="text/javascript">

        function clearFunction(x) {
            document.getElementById(x).innerHTML = "&nbsp;&nbsp;";
        }


        function isDecimalNum(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode == 46) {
                var inputValue = $("#inputfield").val()
                if (inputValue.indexOf('.') < 1) {
                    return true;
                }
                return false;
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
      

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Leave Rule" ForeColor="RoyalBlue" Font-Size="20px"
                    Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <table style="width: 100%;" border="0">
                <tr>
                    <td style="width: 50%; text-align: left;">
                        <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click"
                            Enabled="true" />
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                            ClientIDMode="Static" OnClick="btnDelete_Click" />
                    </td>
                    <td style="width: 50%; text-align: right;">
                        <asp:Button runat="server" ID="cmdReset" Text="Reset" Style="float: right; margin-left: 4px;"
                            CssClass="ButtonControl" OnClick="cmdReset_Click" />
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                            OnClick="btnSearch_Click" />
                        <asp:TextBox ID="txtSearchLeaveCode" runat="server" Style="float: right;" CssClass="searchTextBox"
                            onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtzonename" runat="server" TargetControlID="txtSearchLeaveCode"
                            WatermarkText="LR Code" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:TextBox ID="txtSearchCategoryID" runat="server" Style="float: right;" CssClass="searchTextBox"
                            onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twezoneid" runat="server" TargetControlID="txtSearchCategoryID"
                            WatermarkText="Category ID" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <asp:Panel ID="pnlgvLeaveData" runat="server" Visible="true">
                        <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="3">
                            <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                                <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvLeaveData" runat="server" AutoGenerateColumns="false" Width="100%"
                                        GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="gvLeaveData_RowCommand"
                                        OnPageIndexChanging="gvLeaveData_PageIndexChanging" OnRowDataBound="gvLeaveData_RowDataBound">
                                        <RowStyle CssClass="gvRow" />
                                        <HeaderStyle CssClass="gvHeader" />
                                        <AlternatingRowStyle BackColor="#F0F0F0" />
                                        <PagerStyle CssClass="gvPager" />
                                        <EmptyDataTemplate>
                                            <div>
                                                <span>No Records found.</span>
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" AccessibleHeaderText="Select" SortExpression="Select"
                                                ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="DeleteRows" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" AccessibleHeaderText="Edit" SortExpression="Edit"
                                                ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Modify" CommandArgument='<%# Eval("LR_REC_ID") %>'
                                                        ForeColor="#3366FF">Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LR_REC_ID" Visible="false" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("LR_REC_ID") %>'></asp:Label>
                                                    <asp:Label ID="lblLeaveID" runat="server" Text='<%# Eval("LeaveID") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdnFlag" runat="server" Value='<%#Eval("Flag") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="LR_CODE" HeaderText="LR Code" SortExpression="LR_CODE"
                                                ItemStyle-Width="10%">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LR_CATEGORYID" HeaderText="Category ID" SortExpression="LR_CATEGORYID"
                                                ItemStyle-Width="15%">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LeaveID" HeaderText="Leave ID" SortExpression="LeaveID"
                                                ItemStyle-Width="15%">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LR_ALLOTMENT" HeaderText="Leave Allotment" SortExpression="LR_ALLOTMENT"
                                                ItemStyle-Width="10%">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LR_ACCUMULATION" HeaderText="Leave Accumulation" SortExpression="LR_ACCUMULATION"
                                                ItemStyle-Width="15%">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LR_DAYS" HeaderText="Leaves Calculated On" SortExpression="LR_DAYS"
                                                ItemStyle-Width="15%">
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
                    </asp:Panel>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Button ID="btnDummyAdd" Style="display: none" runat="server" Text="Button" />
    <asp:Button ID="btnDummyModify" Style="display: none" runat="server" Text="Button" />
    <asp:Button ID="btnDummyAddNewEntry" Style="display: none" runat="server" Text="Button" />
    <asp:Button runat="server" ID="tempbtn" Style="display: none;" />
    <asp:Panel ID="pnlAddNewEntry" runat="server" CssClass="PopupPanel" Width="60%">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table class="TableClass">
                    <tr>
                        <td>
                            <table class="TableClass" border="0">
                                <tr>
                                    <td class="heading" colspan="4" style="display: none">
                                        <asp:Label ID="lblLeaveSingleEntry" runat="server" Visible="false" Text="Add Leave Rule">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td align="left" style="padding-left: 20%;">
                                        <asp:Label ID="lblLRCODE" runat="server" Text="Leave Rule Code :   " Style="vertical-align: top;
                                            font-weight: bolder"></asp:Label>
                                    </td>
                                    <td style="width: 50%;">
                                        <asp:TextBox ID="txtLRCODE" runat="server" Height="23px" ClientIDMode="Static" Width="157px"></asp:TextBox>
                                        <%--     <asp:RequiredFieldValidator ID="rfvtxtLRCODE" runat="server" ErrorMessage="Please enter leave rule code."
                                            ControlToValidate="txtLRCODE" Display="None" ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtLRCODE" runat="server" TargetControlID="rfvtxtLRCODE"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 20%;">
                                        <asp:Label ID="lblCategory" runat="server" Text="Category ID :" Style="vertical-align: top;
                                            font-weight: bolder"></asp:Label><font color="red">*</font>
                                    </td>
                                    <td style="width: 50%;">
                                        <asp:DropDownList ID="ddlCategory" runat="server" ClientIDMode="Static" Font-Bold="True"
                                            Font-Names="Courier New" ForeColor="Black" SelectionMode="Single" Style="width: 200px;">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCategory" runat="server" ErrorMessage="Please select category id."
                                            ControlToValidate="ddlCategory" Display="None" ValidationGroup="AddNew" InitialValue="Select One"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceddlCategory" runat="server" TargetControlID="rfvddlCategory"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 20%;">
                                        <asp:Label ID="lblLeaveCode" runat="server" Text="Leave Code :" Style="vertical-align: top;
                                            font-weight: bolder;"></asp:Label><font color="red">*</font>
                                    </td>
                                    <td style="width: 50%;">
                                        <asp:DropDownList Width="200px" ID="ddlLeaveCode" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select leave code."
                                            ControlToValidate="ddlLeaveCode" Display="None" ValidationGroup="AddNew" InitialValue="Select One"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                            TargetControlID="RequiredFieldValidator1" PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 20%;">
                                        <asp:Label ID="Label3" runat="server" Text="Minimum Days Allowed :" Style="vertical-align: top;
                                            font-weight: bolder"></asp:Label><font color="red">*</font>
                                    </td>
                                    <td style="width: 50%;">
                                        <asp:TextBox runat="server" ID="txtMinDaysAllow" Width="75px" MaxLength="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMinDaysAllow" runat="server" ErrorMessage="Please select minimum days."
                                            ControlToValidate="txtMinDaysAllow" Display="None" ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceMinDaysAllow" runat="server" TargetControlID="rfvMinDaysAllow"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMinDaysAllow"
                                            Display="None" ValidationGroup="AddNew" ErrorMessage="Please enter valid integer or decimal number with 2 decimal places."
                                            ForeColor="Red" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                            TargetControlID="RegularExpressionValidator3" PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtMinDaysAllow" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 20%;">
                                        <asp:Label ID="Label7" runat="server" Text="Maximum Days Allowed :" Style="vertical-align: top;
                                            font-weight: bolder"></asp:Label><font color="red">*</font>
                                    </td>
                                    <td style="width: 50%;">
                                        <asp:TextBox runat="server" ID="txtMaxDaysAllowed" Width="75px" MaxLength="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtMaxDaysAllowed" runat="server" ErrorMessage="Please select minimum days."
                                            ControlToValidate="txtMaxDaysAllowed" Display="None" ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                            TargetControlID="rfvtxtMaxDaysAllowed" PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:RegularExpressionValidator ID="regvtxtMaxDaysAllowed" runat="server" ControlToValidate="txtMaxDaysAllowed"
                                            Display="None" ValidationGroup="AddNew" ErrorMessage="Please enter valid integer or decimal number with 2 decimal places."
                                            ForeColor="Red" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                            TargetControlID="regvtxtMaxDaysAllowed" PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtMaxDaysAllowed" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 20%;">
                                        <asp:Label ID="Label4" runat="server" Text="Allotment Type :" Style="vertical-align: top;
                                            font-weight: bolder"></asp:Label><font color="red">*</font>
                                    </td>
                                    <td style="width: 50%;">
                                        <asp:DropDownList ID="ddlAllotmentType" runat="server" AutoPostBack="true" ForeColor="Black"
                                            SelectionMode="Single" Style="width: 200px;" OnSelectedIndexChanged="ddlAllotmentType_SelectedIndexChanged">
                                            <asp:ListItem Text="Select One" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Yearly" Value="Y"></asp:ListItem>
                                            <asp:ListItem Text="Monthly" Value="M"></asp:ListItem>
                                        </asp:DropDownList>
                                        <div style="float: right; padding-right: 27px;">
                                            <asp:RadioButtonList ID="rblDdlAllotmentType" runat="server" Visible="false" RepeatDirection="Horizontal"
                                                Style="padding-top: 1px" CellPadding="1">
                                                <asp:ListItem Text="Year End" Value="YE" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Pro Data" Value="PR" style="padding-left: 5px"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <asp:RequiredFieldValidator ID="rfvAllotmentType" runat="server" ErrorMessage="Please select allotment type"
                                            ControlToValidate="ddlAllotmentType" Display="None" ValidationGroup="AddNew"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceAllotmentType" runat="server" TargetControlID="rfvAllotmentType"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 20%;">
                                        <asp:Label ID="lblLeaveAllotment" runat="server" Text="Allotted Days :" Style="font-weight: bolder;"></asp:Label><font
                                            color="red">*</font>
                                    </td>
                                    <td style="width: 50%;">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtLeaveAllotment" runat="server" MaxLength="3" Width="75px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtLeaveAllotment" runat="server" ErrorMessage="Please enter allotment"
                                                    ControlToValidate="txtLeaveAllotment" Display="None" ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtLeaveAllotment" runat="server" TargetControlID="rfvtxtLeaveAllotment"
                                                    PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtLeaveAllotment"
                                                    Display="None" ValidationGroup="AddNew" ErrorMessage="Please enter valid integer or decimal number with 2 decimal places."
                                                    ForeColor="Red" ValidationExpression="((\d+)((\.\d{1,1})?))$"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                    TargetControlID="RegularExpressionValidator1" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                    FilterType="Numbers" TargetControlID="txtLeaveAllotment" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtLeaveAllotment" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 20%;">
                                        <asp:Label ID="lblLeaveAccumulation" runat="server" Text="Accumulation Limit :" Style="font-weight: bolder;"></asp:Label><font
                                            color="red">*</font>
                                    </td>
                                    <td style="width: 50%;">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtLeaveAccumulation" runat="server" MaxLength="3" Width="75px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtLeaveAccumulation" runat="server" ErrorMessage="Please enter accumulation"
                                                    ControlToValidate="txtLeaveAccumulation" Display="None" ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtLeaveAccumulation" runat="server"
                                                    TargetControlID="rfvtxtLeaveAccumulation" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <%--   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtLeaveAccumulation"
                                                    Display="None" ValidationGroup="AddNew" ErrorMessage="Please enter valid integer or decimal number with 2 decimal places."
                                                    ForeColor="Red" ValidationExpression="((\d+)((\.\d{1,1})?))$"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                    TargetControlID="RegularExpressionValidator2" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterType="Numbers" TargetControlID="txtLeaveAccumulation" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtEditAccumulation" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 20%;">
                                        <asp:Label ID="Label1" runat="server" Text="Leave are calculated on :" Style="vertical-align: top;
                                            font-weight: bolder;"></asp:Label><font color="red">*</font>
                                    </td>
                                    <td style="width: 50%;">
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:RadioButtonList ID="rdbDays" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbDays_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Working Days" Selected="True" Value="W"></asp:ListItem>
                                                    <asp:ListItem Text="Running Days" Value="R" style="padding-left: 5px"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="rdbDays" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList Width="200px" ID="ddlDays" runat="server" AutoPostBack="true" Visible="false"
                                                    OnSelectedIndexChanged="ddlDays_SelectedIndexChanged">
                                                    <asp:ListItem Text="None" Value="N"> </asp:ListItem>
                                                    <asp:ListItem Text="Prefixed Only" Value="P"> </asp:ListItem>
                                                    <asp:ListItem Text="Suffixed Only" Value="S"> </asp:ListItem>
                                                    <asp:ListItem Text="Prefixed & Suffixed" Value="B"> </asp:ListItem>
                                                    <asp:ListItem Text="Prefixed Or Suffixed" Value="O"> </asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RadioButtonList ID="rblValue" runat="server" AutoPostBack="true" Visible="false"
                                                    RepeatDirection="Horizontal" Style="padding-top: 1px">
                                                    <asp:ListItem Text="Greater" Selected="True" Value="G"></asp:ListItem>
                                                    <asp:ListItem Text="Lesser" Value="L" style="padding-left: 5px"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlDays" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 20px;" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center;">
                                        <asp:Button ID="btnSubmitNewEntry" runat="server" CssClass="ButtonControl" Text="Save"
                                            ValidationGroup="AddNew" OnClick="btnSubmitNewEntry_Onclick" />
                                        &nbsp;
                                        <asp:Button ID="btnAddCancelEntry" runat="server" CssClass="ButtonControl" 
                                            Text="Cancel" onclick="btnAddCancelEntry_Click"
                                             />
                                            <%-- OnClientClick="return CloseAdd();"--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 5px;" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center;">
                                        <asp:Label ID="lblErrorSingleEntry" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="pnlModify" runat="server" CssClass="PopupPanel" Width="60%">
        <asp:UpdatePanel ID="updatepnlEditLeaveData" runat="server">
            <ContentTemplate>
                <table class="TableClass" border="0">
                    <tr>
                        <td class="heading" colspan="4" align="center" style="display: none">
                            <asp:Label ID="lblEditLeave" runat="server" Text="Edit Leave">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                            <%--<asp:Label ID="lblID" runat="server"  ClientIDMode="Static" Width="157px" ></asp:Label>--%>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td align="left" style="padding-left: 20%;">
                            <asp:Label ID="lblEditLRCODE" runat="server" Text="Leave Rule Code :   " Style="vertical-align: top;
                                font-weight: bolder"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtEditLRCODE" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-left: 20%;">
                            <asp:Label ID="lblEditCategory" runat="server" Text="Category ID :   " Style="vertical-align: top;
                                font-weight: bolder"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtEditCategory" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-left: 20%;">
                            <asp:Label ID="lblEditLeaveCode" runat="server" Text="Leave Code :   " Style="vertical-align: top;
                                font-weight: bolder"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtEditLeaveCode" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-left: 20%;">
                            <asp:Label ID="Label5" runat="server" Text="Minimum Days Allowed :" Style="vertical-align: top;
                                font-weight: bolder"></asp:Label><font color="red">*</font>
                        </td>
                        <td style="width: 50%;">
                            <asp:TextBox runat="server" ID="txtEditMinDaysAllow" Width="75px" MaxLength="5" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtEditMinDaysAllow" runat="server" ErrorMessage="Please select minimum days."
                                ControlToValidate="txtEditMinDaysAllow" Display="None" ValidationGroup="ModifyAdd"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtEditMinDaysAllow" runat="server"
                                TargetControlID="rfvtxtEditMinDaysAllow" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtEditMinDaysAllow"
                                Display="None" ValidationGroup="ModifyAdd" ErrorMessage="Please enter valid integer or decimal number with 2 decimal places."
                                ForeColor="Red" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                TargetControlID="RegularExpressionValidator4" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtEditMinDaysAllow" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-left: 20%;">
                            <asp:Label ID="Label8" runat="server" Text="Maximum Days Allowed :" Style="vertical-align: top;
                                font-weight: bolder"></asp:Label><font color="red">*</font>
                        </td>
                        <td style="width: 50%;">
                            <asp:TextBox runat="server" ID="txtEditMaxDaysAllow" Width="75px" MaxLength="5"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtEditMaxDaysAllow" runat="server" ErrorMessage="Please select minimum days."
                                ControlToValidate="txtEditMaxDaysAllow" Display="None" ValidationGroup="ModifyAdd"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                TargetControlID="rfvtxtEditMaxDaysAllow" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="regtxtEditMaxDaysAllow" runat="server" ControlToValidate="txtEditMaxDaysAllow"
                                Display="None" ValidationGroup="ModifyAdd" ErrorMessage="Please enter valid integer or decimal number with 2 decimal places."
                                ForeColor="Red" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                TargetControlID="regtxtEditMaxDaysAllow" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtEditMinDaysAllow" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-left: 20%;">
                            <asp:Label ID="Label6" runat="server" Text="Allotment Type :" Style="vertical-align: top;
                                font-weight: bolder"></asp:Label><font color="red">*</font>
                        </td>
                        <td style="width: 50%;">
                            <asp:DropDownList ID="ddlEditAllotmentType" runat="server" Font-Bold="True" Font-Names="Courier New"
                                AutoPostBack="true" ForeColor="Black" SelectionMode="Single" Style="width: 200px;"
                                OnSelectedIndexChanged="ddlEditAllotmentType_SelectedIndexChanged">
                                <asp:ListItem Text="Select One" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Yearly" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="Monthly" Value="M"></asp:ListItem>
                            </asp:DropDownList>
                            <div style="float: right; padding-right: 27px;">
                                <asp:RadioButtonList ID="rblDdlEditAllotmentType" runat="server" Visible="false"
                                    RepeatDirection="Horizontal" Style="padding-top: 1px" CellPadding="1">
                                    <asp:ListItem Text="Year End" Value="YE" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Pro Data" Value="PR" style="padding-left: 5px"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <asp:RequiredFieldValidator ID="rfvddlEditAllotmentType" runat="server" ErrorMessage="Please select allotment type."
                                ControlToValidate="ddlEditAllotmentType" Display="None" ValidationGroup="ModifyAdd"
                                InitialValue="0"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceddlEditAllotmentType" runat="server"
                                TargetControlID="rfvddlEditAllotmentType" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-left: 20%;">
                            <asp:Label ID="lblEditLeaveAllotment" runat="server" Text="Allotted Days :   " Style="vertical-align: top;
                                font-weight: bolder"></asp:Label><font color="red">*</font>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtEditLeaveAllotmentAmount" runat="server" ClientIDMode="Static"
                                        Width="75px" MaxLength="6" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtEditLeaveAllotmentAmount" runat="server" ErrorMessage="Please enter leave allotment"
                                        ControlToValidate="txtEditLeaveAllotmentAmount" Display="None" ValidationGroup="ModifyAdd"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtEditLeaveAllotmentAmount" runat="server"
                                        TargetControlID="rfvtxtEditLeaveAllotmentAmount" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="RegularEditLeaveAllotmentAmount" runat="server"
                                        ControlToValidate="txtEditLeaveAllotmentAmount" Display="None" ValidationGroup="ModifyAdd"
                                        ErrorMessage="Please enter numeric value" ForeColor="Red" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                         <%--ValidationExpression="^\d+$"--%>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                        TargetControlID="RegularEditLeaveAllotmentAmount" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtEditLeaveAllotmentAmount" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-left: 20%;">
                            <asp:Label ID="lblEditAccumulation" runat="server" Text="Accumulation Limit:   "
                                Style="vertical-align: top; font-weight: bolder"></asp:Label><font color="red">*</font>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtEditAccumulation" runat="server" ClientIDMode="Static" Width="75px"
                                        MaxLength="6" ></asp:TextBox>
                                       <%-- onkeypress="return Isdigit(event)"--%>
                                    <asp:RequiredFieldValidator ID="rfvtxtEditAccumulation" runat="server" ErrorMessage="Please enter accumulation"
                                        ControlToValidate="txtEditAccumulation" Display="None" ValidationGroup="ModifyAdd"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtEditAccumulation" runat="server"
                                        TargetControlID="rfvtxtEditAccumulation" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="RegulartxtEditAccumulation6" runat="server" ControlToValidate="txtEditAccumulation"
                                        Display="None" ValidationGroup="ModifyAdd" ErrorMessage="Please enter numeric value"
                                        ForeColor="Red" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                        TargetControlID="RegulartxtEditAccumulation6" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtEditAccumulation" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-left: 20%;">
                            <asp:Label ID="Label2" runat="server" Text="Leave are calculated on :" Style="vertical-align: top;
                                font-weight: bolder;"></asp:Label><font color="red">*</font>
                        </td>
                        <td style="width: 50%;">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:RadioButtonList ID="rdlEditDays" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdlEditDays_SelectedIndexChanged"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Working Days" Selected="True" Value="W"></asp:ListItem>
                                        <asp:ListItem Text="Running Days" Value="R" style="padding-left: 5px"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rdlEditDays" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList Width="200px" ID="ddlEditRule" runat="server" Font-Names="Courier New"
                                        AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlEditRule_SelectedIndexChanged">
                                        <asp:ListItem Text="None" Value="N"> </asp:ListItem>
                                        <asp:ListItem Text="Prefixed Only" Value="P"> </asp:ListItem>
                                        <asp:ListItem Text="Suffixed Only" Value="S"> </asp:ListItem>
                                        <asp:ListItem Text="Prefixed & Suffixed" Value="B"> </asp:ListItem>
                                        <asp:ListItem Text="Prefixed Or Suffixed" Value="O"> </asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RadioButtonList ID="rblEditValue" runat="server" AutoPostBack="true" Visible="false"
                                        RepeatDirection="Horizontal" Style="padding-top: 1px" CellPadding="1">
                                        <asp:ListItem Text="Greater" Selected="True" Value="G"></asp:ListItem>
                                        <asp:ListItem Text="Lesser" Value="L" style="padding-left: 5px"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlDays" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnModifySaveLeave" runat="server" CssClass="ButtonControl" Text="Save"
                                OnClick="btnModifySaveLeave_Click" ValidationGroup="ModifyAdd" />
                            &nbsp;
                            <asp:Button ID="btnModifyCancelLeave" runat="server" CssClass="ButtonControl" Text="Cancel"
                                OnClientClick="return EditClose();" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button1" Style="display: none" runat="server" Text="Button" />
    <ajaxToolkit:ModalPopupExtender ID="mpeModifyZone" runat="server" TargetControlID="btnDummyModify"
        PopupControlID="pnlModify" BackgroundCssClass="modalBackground" Enabled="true"
        BehaviorID="mpeBModifyZone" CancelControlID="btnModifyCancelLeave">
    </ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="mpeAddNewEntry" runat="server" TargetControlID="btnAdd"
        BehaviorID="mpeBAddNewEntry" PopupControlID="pnlAddNewEntry" BackgroundCssClass="modalBackground"
        Enabled="true">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
