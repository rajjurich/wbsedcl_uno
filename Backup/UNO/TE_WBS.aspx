<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="TE_WBS.aspx.cs" Inherits="UNO.TE_WBS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .display
        {
            display: none;
        }
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .divWrapper
        {
            border-radius: 25px;
            border: 5px solid #059EDC;
            background-color: #059EDC;
        }
        .gridview
        {
            width: 100%;
            background-color: #EFFBFF;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>--%>
    <div style="padding: 25px 10px 10px 10px;">
        <div class="divWrapper">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center;">
                        <span style="width: 100%; text-align: center; font-family: 'Open Sans Condensed', sans-serif;
                            font-size: large; font-weight: bold;">WBS </span>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Label ID="lblProject" runat="server" Text="Project : "></asp:Label>
                        <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label ID="lblMilestone" runat="server" Text="Milestone : "></asp:Label>
                        <asp:DropDownList ID="ddlMilestone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMilestone_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvWBS" runat="server" AutoGenerateColumns="false" OnRowCommand="gvWBS_RowCommand"
                                    CssClass="gridview">
                                    <EmptyDataTemplate>
                                        <span>No WBS to display</span>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="display" ItemStyle-CssClass="display" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" HeaderStyle-CssClass="display"
                                            ItemStyle-CssClass="display" />
                                        <asp:BoundField DataField="MilestoneID" HeaderText="MilestoneID" HeaderStyle-CssClass="display"
                                            ItemStyle-CssClass="display" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                <%--        <asp:BoundField DataField="PlannedStartDate" HeaderText="Planned Start Date" />
                                        <asp:BoundField DataField="PlannedEndDate" HeaderText="Planned End Date" />--%>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkModify" runat="server" CommandName="Modify" CommandArgument='<%#Eval("ID")%>'>Edit</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Remove" CommandArgument='<%#Eval("ID")%>'>Delete</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Button ID="btnDummyAdd" runat="server" Text="Dummy" Style="display: none;" />
                                <asp:Button ID="btnDummyOKPopUp" runat="server" Text="Dummy" Style="display: none;" />
                                <asp:Button ID="btnDummyEdit" runat="server" Text="Dummy" Style="display: none;" />
                                <asp:Button ID="btnDummyRemove" runat="server" Text="Dummy" Style="display: none;" />
                                <asp:Panel ID="pnlAddWBS" runat="server" Style="background-color: White; border: 5px solid #000000;
                                    border-radius: 25px; color: Black;">
                                    <center><span style="width: 100%; text-align: center; font-family: 'Open Sans Condensed', sans-serif;
                    font-size: large; font-weight: bold;">Add New WBS</span></center>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDescriptionAdd" runat="server" Text="Description : "></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtDescriptionAdd" runat="server" TextMode="MultiLine" ValidationGroup="Add"
                                                    Style="min-height: 50px; max-height: 50px; min-width: 450px; max-width: 450px;"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtDescriptionAdd" runat="server" ErrorMessage="Please Enter Description"
                                                    Display="None" ControlToValidate="txtDescriptionAdd" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="cverfvtxtDescriptionAdd" runat="server"
                                                    TargetControlID="rfvtxtDescriptionAdd" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td>
                                                <asp:Label ID="lblPlannedStartDateAdd" runat="server" Text="Planned Start Date : "></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPlannedStartDateAdd" runat="server" ValidationGroup="Add"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="caltxtPlannedStartDateAdd" runat="server" TargetControlID="txtPlannedStartDateAdd"
                                                    PopupButtonID="txtPlannedStartDateAdd" Format="dd-MM-yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtPlannedStartDateAdd" runat="server" ErrorMessage="Please Select Planned Start Date"
                                                    ControlToValidate="txtPlannedStartDateAdd" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtPlannedStartDateAdd" runat="server"
                                                    TargetControlID="rfvtxtPlannedStartDateAdd" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="revtxtPlannedStartDateAdd" runat="server" ErrorMessage="Start Date is not valid"
                                                    ControlToValidate="txtPlannedStartDateAdd" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                                                    Display="None" ValidationGroup="Add"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtPlannedStartDateAdd" runat="server"
                                                    TargetControlID="revtxtPlannedStartDateAdd" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPlannedEndDateAdd" runat="server" Text="Planned End Date : "></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPlannedEndDateAdd" runat="server" ValidationGroup="Add"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="cetxtPlannedEndDateAdd" runat="server" TargetControlID="txtPlannedEndDateAdd"
                                                    PopupButtonID="txtPlannedEndDateAdd" Format="dd-MM-yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtPlannedEndDateAdd" runat="server" ErrorMessage="Please Select Planned End Date"
                                                    ControlToValidate="txtPlannedEndDateAdd" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtPlannedEndDateAdd" runat="server"
                                                    TargetControlID="rfvtxtPlannedEndDateAdd" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="revtxtPlannedEndDateAdd" runat="server" ErrorMessage="End Date is not valid"
                                                    ControlToValidate="txtPlannedEndDateAdd" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                                                    Display="None" ValidationGroup="Add"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtPlannedEndDateAdd" runat="server"
                                                    TargetControlID="revtxtPlannedEndDateAdd" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:CompareValidator ID="vctxtPlannedEndDateAdd" runat="server" ErrorMessage="End Date cannot be greater than Start Date"
                                                    ControlToValidate="txtPlannedEndDateAdd" ControlToCompare="txtPlannedStartDateAdd"
                                                    Type="Date" Operator="GreaterThanEqual" Display="None" ValidationGroup="Add"></asp:CompareValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcevctxtPlannedEndDateAdd" runat="server"
                                                    TargetControlID="vctxtPlannedEndDateAdd" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td colspan="4" style="text-align: center;">
                                                <asp:Button ID="btnAddNewWBS" runat="server" Text="Add New WBS" OnClick="btnAddNewWBS_Click" />
                                                <asp:Button ID="btnCancelAdd" runat="server" Text="Cancel" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <ajaxToolkit:ModalPopupExtender ID="mpeAddWBS" runat="server" TargetControlID="btnDummyAdd"
                                    PopupControlID="pnlAddWBS" BackgroundCssClass="modalBackground" Enabled="true"
                                    CancelControlID="btnCancelAdd">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlOKPopUp" runat="server" Style="background-color: White; border: 5px solid #000000;
                                    border-radius: 25px; color: Black; padding: 15px 15px 15px 15px;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMessageOKPopUp" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Button ID="btnOK" runat="server" Text="OK" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <ajaxToolkit:ModalPopupExtender ID="mpeOKPopUp" runat="server" TargetControlID="btnDummyOKPopUp"
                                    PopupControlID="pnlOKPopUp" BackgroundCssClass="modalBackground" Enabled="true"
                                    CancelControlID="btnOK">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlRemoveWBS" runat="server" Style="background-color: White; border: 5px solid #000000;
                                    border-radius: 25px; color: Black; padding: 15px 15px 15px 15px;">
                                    <asp:Label ID="lblWBSIDRemove" runat="server" Text="" Style="display: none;"></asp:Label>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMessageRemove" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Button ID="btnYesRemove" runat="server" Text="Yes" OnClick="btnYesRemove_Click" />
                                                <asp:Button ID="btnNoRemove" runat="server" Text="No" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <ajaxToolkit:ModalPopupExtender ID="mpeRemoveWBS" runat="server" TargetControlID="btnDummyRemove"
                                    PopupControlID="pnlRemoveWBS" BackgroundCssClass="modalBackground" Enabled="true"
                                    CancelControlID="btnNoRemove">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlEditWBS" runat="server" Style="background-color: White; border: 5px solid #000000;
                                    border-radius: 25px; color: Black;">
                                    <center><span style="width: 100%; text-align: center; font-family: 'Open Sans Condensed', sans-serif;
                    font-size: large; font-weight: bold;">Modify WBS</span></center>
                                    <asp:Label ID="lblWBSIDEdit" runat="server" Text="" Style="display: none;"></asp:Label>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDescriptionEdit" runat="server" Text="Description : "></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtDescriptionEdit" runat="server" TextMode="MultiLine" ValidationGroup="Edit"
                                                    Style="min-height: 50px; max-height: 50px; min-width: 450px; max-width: 450px;"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtDescriptionEdit" runat="server" ErrorMessage="Please Enter Description"
                                                    Display="None" ControlToValidate="txtDescriptionEdit" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtDescriptionEdit" runat="server"
                                                    TargetControlID="rfvtxtDescriptionEdit" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td>
                                                <asp:Label ID="lblPlannedStartDateEdit" runat="server" Text="Planned Start Date : "></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPlannedStartDateEdit" runat="server" ValidationGroup="Edit"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="caltxtPlannedStartDateEdit" runat="server" TargetControlID="txtPlannedStartDateEdit"
                                                    PopupButtonID="txtPlannedStartDateEdit" Format="dd-MM-yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtPlannedStartDateEdit" runat="server" ErrorMessage="Please Select Planned Start Date"
                                                    ControlToValidate="txtPlannedStartDateEdit" Display="None" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtPlannedStartDateEdit" runat="server"
                                                    TargetControlID="rfvtxtPlannedStartDateEdit" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="revtxtPlannedStartDateEdit" runat="server" ErrorMessage="Start Date is not valid"
                                                    ControlToValidate="txtPlannedStartDateEdit" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                                                    Display="None" ValidationGroup="Edit"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtPlannedStartDateEdit" runat="server"
                                                    TargetControlID="revtxtPlannedStartDateEdit" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPlannedEndDateEdit" runat="server" Text="Planned End Date : "></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPlannedEndDateEdit" runat="server" ValidationGroup="Edit"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="caltxtPlannedEndDateEdit" runat="server" TargetControlID="txtPlannedEndDateEdit"
                                                    PopupButtonID="txtPlannedEndDateEdit" Format="dd-MM-yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtPlannedEndDateEdit" runat="server" ErrorMessage="Please Select Planned End Date"
                                                    ControlToValidate="txtPlannedEndDateEdit" Display="None" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtPlannedEndDateEdit" runat="server"
                                                    TargetControlID="rfvtxtPlannedEndDateEdit" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="revtxtPlannedEndDateEdit" runat="server" ErrorMessage="End Date is not valid"
                                                    ControlToValidate="txtPlannedEndDateEdit" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                                                    Display="None" ValidationGroup="Edit"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtPlannedEndDateEdit" runat="server"
                                                    TargetControlID="revtxtPlannedEndDateEdit" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:CompareValidator ID="cvtxtPlannedEndDateEdit" runat="server" ErrorMessage="End Date cannot be greater than Start Date"
                                                    ControlToValidate="txtPlannedEndDateEdit" ControlToCompare="txtPlannedStartDateEdit"
                                                    Type="Date" Operator="GreaterThanEqual" Display="None" ValidationGroup="Edit"></asp:CompareValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcecvtxtPlannedEndDateEdit" runat="server"
                                                    TargetControlID="cvtxtPlannedEndDateEdit" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td colspan="4" style="text-align: center;">
                                                <asp:Button ID="btnSubmitEdit" runat="server" Text="Modify" OnClick="btnSubmitEdit_Click" />
                                                <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <ajaxToolkit:ModalPopupExtender ID="mpeEditWBS" runat="server" TargetControlID="btnDummyEdit"
                                    PopupControlID="pnlEditWBS" BackgroundCssClass="modalBackground" Enabled="true"
                                    CancelControlID="btnCancelEdit">
                                </ajaxToolkit:ModalPopupExtender>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvWBS" />
                                <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New WBS" OnClick="btnAdd_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:Label ID="lblError" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
