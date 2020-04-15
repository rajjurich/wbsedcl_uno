<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="LeaveFileView.aspx.cs" Inherits="UNO.LeaveFileView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvLeave.ClientID %>');

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
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Leave File" ForeColor="RoyalBlue" Font-Size="20px"
                    Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div class="DivEmpDetails">
        <table style="width: 100%;">
            <tr>
                <td style="width: 50%; text-align: left;">
                    <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click" />
                    <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                        OnClick="btnDelete_Click" />
                </td>
                <td style="width: 50%; text-align: right;">
                    <asp:Button runat="server" ID="cmdReset" Text="Reset" Style="float: right; margin-left: 4px;"
                        CssClass="ButtonControl" OnClick="cmdReset_Click" />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                        OnClick="btnSearch_Click" />
                    <asp:TextBox ID="textLeavename" runat="server" Style="float: right;" CssClass="searchTextBox"
                        onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtzonename" runat="server" TargetControlID="textLeavename"
                        WatermarkText="Leave description" WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    <asp:TextBox ID="textLeaveid" runat="server" Style="float: right;" CssClass="searchTextBox"
                        onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twezoneid" runat="server" TargetControlID="textLeaveid"
                        WatermarkText="Leave ID" WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="3">
                    <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                        <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="false" Width="100%"
                                GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="gvLeave_RowCommand"
                                OnPageIndexChanging="gvLeave_PageIndexChanging" OnRowDataBound="gvLeave_RowDataBound">
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
                                    <asp:TemplateField HeaderText="Select" AccessibleHeaderText="Select" SortExpression="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="DeleteRows" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" AccessibleHeaderText="Edit" SortExpression="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Modify" CommandArgument='<%# Eval("Rec_ID") %>'
                                                ForeColor="#3366FF">Edit</asp:LinkButton>
                                            <asp:HiddenField ID="hdnFlag" runat="server" Value='<%#Eval("Flag") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Leave_ID" HeaderText="ID" SortExpression="Leave_ID">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Leave_Description" HeaderText="Description" SortExpression="Leave_Description">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Leave_IsPaid" HeaderText="Is Paid" SortExpression="Leave_IsPaid">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Leave_Group" HeaderText="Leave Group" SortExpression="Leave_Group">
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
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div style="width: 100%; text-align: center;">
                <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <asp:Button ID="btnDummyModify" Style="display: none" runat="server" Text="Button" />
    <asp:Panel ID="pnlAddLeave" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="table4" runat="server" width="100%" border="0" cellpadding="0" cellspacing="5"
                    class="TableClass">
                    <tr>
                        <td class="TDClassLabel" align="right">
                            <asp:Label ID="LblID" runat="server" ClientIDMode="Static">Leave ID</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" align="right">
                            <asp:TextBox ID="txtleaveID" ClientIDMode="Static" runat="server" MaxLength="8" TabIndex="2"
                                onkeypress="return IsAlphanumericWithoutspace(event)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtLeaveid" runat="server" ControlToValidate="txtleaveID"
                                Display="None" ErrorMessage="Please enter Id." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcertxtidAdd" runat="server" TargetControlID="rfvtxtLeaveid"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <%--<td> <asp:RequiredFieldValidator ID="ValID" runat="server" 
            ErrorMessage="Please enter ID" ControlToValidate="txtID" 
            SetFocusOnError="True" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator></td>--%>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            <asp:Label ID="LblDesc" runat="server" ClientIDMode="Static">Description</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:TextBox ID="txtleaveDescription" ClientIDMode="Static" runat="server" TabIndex="3"
                                MaxLength="20" onkeypress="return IsAlphanumeric(event)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtLeavedesc" runat="server" ControlToValidate="txtleaveDescription"
                                Display="None" ErrorMessage="Please enter Description." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcertxtdescAdd" runat="server" TargetControlID="rfvtxtLeavedesc"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            <asp:Label ID="lblReasonType" runat="server" ClientIDMode="Static"> Paid Leave</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:DropDownList ID="cmbPaidLeave" runat="server" ClientIDMode="Static" Width="173px"
                                TabIndex="3">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvPaidLeaveType" runat="server" ControlToValidate="cmbPaidLeave"
                                Display="None" ErrorMessage="Please select Paid Leave Type." ForeColor="Red"
                                InitialValue="-1" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                TargetControlID="rfvPaidLeaveType" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            <asp:Label ID="Label1" runat="server" ClientIDMode="Static"> Leave Group</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:DropDownList ID="cmbLeaveGroup" runat="server" ClientIDMode="Static" Width="173px"
                                TabIndex="3">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLeaveGroup" runat="server" ControlToValidate="cmbLeaveGroup"
                                Display="None" ErrorMessage="Please select Leave Group." ForeColor="Red" InitialValue="Select One"
                                SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                TargetControlID="rfvLeaveGroup" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding-top: 4%">
                            <asp:Button ID="BtnAddSave" runat="server" CssClass="ButtonControl" Font-Bold="true"
                                TabIndex="4" Text="Save" ValidationGroup="Add" OnClick="BtnAddSave_Click" />
                            &nbsp;
                            <asp:Button ID="BtnAddCancel" runat="server" CausesValidation="False" CssClass="ButtonControl"
                                Font-Bold="true" TabIndex="5" Text="Cancel" OnClick="BtnAddCancel_Click" />
                        </td>
                        <%-- <td></td>--%>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Label ID="lblErrorAdd" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="btnAdd"/>--%>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="btnAddDummy" runat="server" Text="Button" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddLeave" runat="server" TargetControlID="btnAddDummy"
        PopupControlID="pnlAddLeave" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="BtnAddCancel">
    </ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="mpeModifyLeave" runat="server" TargetControlID="btnDummyModify"
        PopupControlID="pnlModifyLeave" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnModifyCancel">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlModifyLeave" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table id="table1" runat="server" width="100%" border="0" cellpadding="0" cellspacing="5"
                    class="TableClass">
                    <tr>
                        <td class="TDClassLabel" align="right">
                            <asp:Label ID="Label2" runat="server" ClientIDMode="Static">Leave ID</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" align="right">
                            <asp:TextBox ID="txtModifyLeaveID" ClientIDMode="Static" runat="server" MaxLength="8"
                                TabIndex="2" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvModifyLeaveid" runat="server" ControlToValidate="txtModifyLeaveID"
                                Display="None" ErrorMessage="Please enter Id." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Modify"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                TargetControlID="rfvModifyLeaveid" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <%--<td> <asp:RequiredFieldValidator ID="ValID" runat="server" 
            ErrorMessage="Please enter ID" ControlToValidate="txtID" 
            SetFocusOnError="True" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator></td>--%>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            <asp:Label ID="Label3" runat="server" ClientIDMode="Static">Description</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:TextBox ID="txtModifyLeaveDesc" ClientIDMode="Static" runat="server" TabIndex="3"
                                MaxLength="20"  onkeypress="return IsAlphanumeric(event)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvleavedesc" runat="server" ControlToValidate="txtModifyLeaveDesc"
                                Display="None" ErrorMessage="Please enter Description." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Modify"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                TargetControlID="rfvleavedesc" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            <asp:Label ID="Label4" runat="server" ClientIDMode="Static"> Paid Leave</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:DropDownList ID="cmbModifyPaidLeave" runat="server" ClientIDMode="Static" CssClass="ComboControl"
                                Width="173px" TabIndex="3">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvModifyPaidLeave" runat="server" ControlToValidate="cmbModifyPaidLeave"
                                Display="None" ErrorMessage="Please select Paid Leave Type." ForeColor="Red"
                                InitialValue="-1" SetFocusOnError="True" ValidationGroup="Modify"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                TargetControlID="rfvModifyPaidLeave" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            <asp:Label ID="Label5" runat="server" ClientIDMode="Static"> Leave Group</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:DropDownList ID="cmbModifyLeaveGroup" runat="server" ClientIDMode="Static" CssClass="ComboControl"
                                Width="173px" TabIndex="3">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvModifyleaveGroup" runat="server" ControlToValidate="cmbModifyLeaveGroup"
                                Display="None" ErrorMessage="Please select Leave Group." ForeColor="Red" InitialValue="Select One"
                                SetFocusOnError="True" ValidationGroup="Modify"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                TargetControlID="rfvModifyleaveGroup" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding-top: 4%">
                            <asp:Button ID="btnModifySave" runat="server" CssClass="ButtonControl" Font-Bold="true"
                                TabIndex="4" Text="Save" ValidationGroup="Modify" OnClick="btnModifySave_Click" />
                            &nbsp;
                            <asp:Button ID="btnModifyCancel" runat="server" CausesValidation="False" CssClass="ButtonControl"
                                Font-Bold="true" TabIndex="5" Text="Cancel" OnClick="btnModifyCancel_Click" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Label ID="lblEditError" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
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
