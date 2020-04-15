<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="TE_Projects.aspx.cs" Inherits="UNO.TE_Projects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .gridview
        {
            width: 100%;
            background-color: #EFFBFF;
            text-align:center;
        }
        .divWrapper
        {
            border-radius: 25px;
            border: 5px solid #059EDC;
            background-color: #059EDC;
        }
        .display
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="padding: 25px 10px 10px 10px;">
        <ContentTemplate>
            <div class="divWrapper">
                <center><span style="width: 100%; text-align: center; font-family:'Open Sans Condensed', sans-serif; font-size:large; font-weight:bold;">Projects </span></center>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:GridView ID="gvProjects" runat="server" AutoGenerateColumns="false" OnRowCommand="gvProjects_RowCommand"
                                CssClass="gridview">
                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Baseline">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBaseLine" runat="server" CommandName="Baseline" CommandArgument='<%#Eval("ID")%>'>Baseline</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="display" ItemStyle-CssClass="display" />
                                    <asp:BoundField DataField="ProjectCode" HeaderText="Project Code" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                    <asp:BoundField DataField="PlannedStartDate" HeaderText="Planned Start Date" />
                                    <asp:BoundField DataField="PlannedEndDate" HeaderText="Planned End Date" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Modify" CommandArgument='<%#Eval("ID")%>'>Edit</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Remove" CommandArgument='<%#Eval("ID")%>'>Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Button ID="btnAddNewProject" runat="server" Text="Add New Project" OnClick="btnAddNewProject_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Button ID="btnDummyEdit" runat="server" Text="dummy" Style="display: none;" />
            <asp:Button ID="btnDummyAdd" runat="server" Text="dummy" Style="display: none;" />
            <asp:Button ID="btnDummyRemove" runat="server" Text="dummy" Style="display: none;" />
            <asp:Button ID="btnDummyOK" runat="server" Text="dummy" Style="display: none;" />
            <asp:Button ID="btnDummyBaseline" runat="server" Text="dummy" Style="display: none;" />
            <asp:Panel ID="pnlEditProject" runat="server" Style="background-color: White; border: 5px solid #000000;
                border-radius: 25px; color: Black;">
                <asp:Label ID="lblIdEdit" runat="server" Text="" Style="display: none;"></asp:Label>
                <center><span style="width: 100%; text-align: center; font-family: 'Open Sans Condensed', sans-serif;
                    font-size: large; font-weight: bold;">Modify Project</span></center>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblProjectCodeEdit" runat="server" Text="Project Code :"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDescriptionEdit" runat="server" Text="Description :"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDescriptionEdit" runat="server" TextMode="MultiLine" ValidationGroup="Edit"
                                Style="min-width: 450px; max-width: 450px; min-height: 50px; max-height: 50px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtDescriptionEdit" runat="server" ErrorMessage="Please Enter Description"
                                Display="None" ControlToValidate="txtDescriptionEdit" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtDescriptionEdit" runat="server"
                                TargetControlID="rfvtxtDescriptionEdit" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblClientEdit" runat="server" Text="Client :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlClientEdit" runat="server" ValidationGroup="Edit">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlClientEdit" runat="server" ErrorMessage="Please Select Client"
                                ControlToValidate="ddlClientEdit" Display="None" InitialValue="0" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlClientEdit" runat="server" TargetControlID="rfvddlClientEdit"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblDivisionEdit" runat="server" Text="Division :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDivisionEdit" runat="server" ValidationGroup="Edit">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlDivisionEdit" runat="server" ErrorMessage="Please Select Division"
                                ControlToValidate="ddlDivisionEdit" Display="None" InitialValue="0" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlDivisionEdit" runat="server" TargetControlID="rfvddlDivisionEdit"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLocationEdit" runat="server" Text="Location :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLocationEdit" runat="server" ValidationGroup="Edit"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtLocationEdit" runat="server" ErrorMessage="Please Enter Location"
                                ControlToValidate="txtLocationEdit" Display="None" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtLocationEdit" runat="server" TargetControlID="rfvtxtLocationEdit"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblStateEdit" runat="server" Text="State :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStateEdit" runat="server" ValidationGroup="Edit"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtStateEdit" runat="server" ErrorMessage="Please Enter State"
                                ControlToValidate="txtStateEdit" Display="None" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtStateEdit" runat="server" TargetControlID="rfvtxtStateEdit"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRemarksEdit" runat="server" Text="Remarks :"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtRemarksEdit" runat="server" TextMode="MultiLine" ValidationGroup="Edit"
                                Style="min-width: 450px; max-width: 450px; min-height: 50px; max-height: 50px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtRemarksEdit" runat="server" ErrorMessage="Please Enter Remarks"
                                ControlToValidate="txtRemarksEdit" Display="None" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtRemarksEdit" runat="server" TargetControlID="rfvtxtRemarksEdit"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPlannedStartDateEdit" runat="server" Text="Planned Start Date :"></asp:Label>
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
                            <asp:Label ID="lblPlannedEndDateEdit" runat="server" Text="Planned End Date :"></asp:Label>
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
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnSubmitEdit" runat="server" Text="Modify" ValidationGroup="Edit"
                                OnClick="btnSubmitEdit_Click" />
                            <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="mpeEditProject" runat="server" TargetControlID="btnDummyEdit"
                PopupControlID="pnlEditProject" BackgroundCssClass="modalBackground" Enabled="true"
                CancelControlID="btnCancelEdit">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlAddProject" runat="server" Style="background-color: White; border: 5px solid #000000;
                border-radius: 25px; color: Black;">
                <center><span style="width: 100%; text-align: center; font-family: 'Open Sans Condensed', sans-serif;
                    font-size: large; font-weight: bold;">Add New Project</span></center>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblProjectNameAdd" runat="server" Text="Project Name :"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtProjectCodeAdd" runat="server" ValidationGroup="Add" Width="360px"></asp:TextBox>
                            <%--<asp:DropDownList ID="ddlProjectNameAdd" runat="server" ValidationGroup="Add">
                            </asp:DropDownList>--%>
                            <asp:RequiredFieldValidator ID="rfvddlProjectNameAdd" runat="server" ErrorMessage="Please Select Project Name"
                                ControlToValidate="txtProjectCodeAdd" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlProjectNameAdd" runat="server"
                                TargetControlID="rfvddlProjectNameAdd" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDescriptionAdd" runat="server" Text="Description :"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDescriptionAdd" runat="server" TextMode="MultiLine" ValidationGroup="Add"
                                Style="min-width: 450px; max-width: 450px; min-height: 50px; max-height: 50px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtDescriptionAdd" runat="server" ErrorMessage="Please Enter Description"
                                Display="None" ControlToValidate="txtDescriptionAdd" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="cverfvtxtDescriptionAdd" runat="server"
                                TargetControlID="rfvtxtDescriptionAdd" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblClientAdd" runat="server" Text="Client :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlClientAdd" runat="server" ValidationGroup="Add">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlClientAdd" runat="server" ErrorMessage="Please Select Client"
                                ControlToValidate="ddlClientAdd" Display="None" InitialValue="0" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlClientAdd" runat="server" TargetControlID="rfvddlClientAdd"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblDivisionAdd" runat="server" Text="Division :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDivisionAdd" runat="server" ValidationGroup="Add">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlDivisionAdd" runat="server" ErrorMessage="Please Select Division"
                                ControlToValidate="ddlDivisionAdd" Display="None" InitialValue="0" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlDivisionAdd" runat="server" TargetControlID="rfvddlDivisionAdd"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLocationAdd" runat="server" Text="Location :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLocationAdd" runat="server" ValidationGroup="Add"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtLocationAdd" runat="server" ErrorMessage="Please Enter Location"
                                ControlToValidate="txtLocationAdd" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtLocationAdd" runat="server" TargetControlID="rfvtxtLocationAdd"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblStateAdd" runat="server" Text="State :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStateAdd" runat="server" ValidationGroup="Add"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtStateAdd" runat="server" ErrorMessage="Please Enter State"
                                ControlToValidate="txtStateAdd" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtStateAdd" runat="server" TargetControlID="rfvtxtStateAdd"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRemarksAdd" runat="server" Text="Remarks :"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtRemarksAdd" runat="server" TextMode="MultiLine" ValidationGroup="Add"
                                Style="min-width: 450px; max-width: 450px; min-height: 50px; max-height: 50px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtRemarksAdd" runat="server" ErrorMessage="Please Enter Remarks"
                                ControlToValidate="txtRemarksAdd" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtRemarksAdd" runat="server" TargetControlID="rfvtxtRemarksAdd"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPlannedStartDateAdd" runat="server" Text="Planned Start Date :"></asp:Label>
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
                            <asp:Label ID="lblPlannedEndDateAdd" runat="server" Text="Planned End Date :"></asp:Label>
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
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnSubmitAdd" runat="server" Text="Add Project" OnClick="btnSubmitAdd_Click"
                                ValidationGroup="Add" />
                            <asp:Button ID="btnCancelAdd" runat="server" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="mpeAddProject" runat="server" TargetControlID="btnDummyAdd"
                PopupControlID="pnlAddProject" BackgroundCssClass="modalBackground" Enabled="true"
                CancelControlID="btnCancelAdd">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlRemovePopUp" runat="server" Style="background-color: White; border: 5px solid #000000;
                border-radius: 25px; color: Black; padding: 15px 15px 15px 15px;">
                <asp:Label ID="lblProjectIdRemove" runat="server" Text="" Style="display: none;"></asp:Label>
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
            <ajaxToolkit:ModalPopupExtender ID="mpeRemovePopUp" runat="server" TargetControlID="btnDummyRemove"
                PopupControlID="pnlRemovePopUp" BackgroundCssClass="modalBackground" Enabled="true"
                CancelControlID="btnNoRemove">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlOKPopUp" runat="server" Style="background-color: White; border: 5px solid #000000;
                border-radius: 25px; color: Black; padding: 15px 15px 15px 15px;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Label ID="lblMessageGeneral" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                            <asp:Button ID="btnOk" runat="server" Text="OK" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="mpeOKPopUp" runat="server" TargetControlID="btnDummyOK"
                PopupControlID="pnlOKPopUp" BackgroundCssClass="modalBackground" Enabled="true"
                CancelControlID="btnOk">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlBaselinePopUp" runat="server" Style="background-color: White; border: 5px solid #000000;
                border-radius: 25px; color: Black; padding: 15px 15px 15px 15px;">
                <asp:Label ID="lblProjectIdBaseline" runat="server" Text="" Style="display: none;"></asp:Label>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Label ID="lblMessageBaseline" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;">
                            <asp:Button ID="btnYesBaseline" runat="server" Text="Yes" OnClick="btnYesBaseline_Click" />
                            <asp:Button ID="btnNoBaseline" runat="server" Text="No" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="mpeBaselinePopUp" runat="server" TargetControlID="btnDummyBaseline"
                PopupControlID="pnlBaselinePopUp" BackgroundCssClass="modalBackground" Enabled="true"
                CancelControlID="btnNoBaseline">
            </ajaxToolkit:ModalPopupExtender>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAddNewProject" />
            <asp:AsyncPostBackTrigger ControlID="gvProjects" />
            <asp:AsyncPostBackTrigger ControlID="btnSubmitAdd" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelAdd" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Label ID="lblError" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
