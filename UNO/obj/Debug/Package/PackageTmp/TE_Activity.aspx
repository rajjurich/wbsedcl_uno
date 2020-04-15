<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="TE_Activity.aspx.cs" Inherits="UNO.TE_Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script type="text/javascript" src="riceBox/jquery-1.4.4.js"></script>--%>
    <script type="text/javascript" src="Scripts/Ricebox/ricePhoto1.js">  </script>
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
    <script>
        function validTime(sender) {
            try {
                var intRegex = /^\d+$/;
                var time = sender.value;
                var seperator = time.indexOf(":");
                var Hours = 0;
                var Mins = 0;
                if (time == "") {
                    return true;
                }
                if (seperator == -1) {
                    alert("Time entered is not in valid format");
                    sender.focus();
                    return false;
                }
                else {

                    Hours = parseInt(time.substring(0, seperator));
                    Mins = parseInt(time.substring(seperator + 1, time.length));
                    if (!intRegex.test(Hours)) {
                        alert("Not a valid Time1");
                        sender.focus();
                        return false;
                    }
                    else if (!intRegex.test(Mins)) {
                        alert(Mins);
                        alert("Not a valid Time");
                        sender.focus();
                        return false;
                    }
                    else if (Hours > 24) {
                        alert("hours should be less than 24");
                        sender.focus();
                        return false;
                    }
                    else if (Mins > 60) {
                        alert("Mins should be less than 60");
                        sender.focus();
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            }
            catch (ex) {
                alert(ex.Message);
            }
        }
    </script>
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
                            font-size: large; font-weight: bold;">Activity </span>
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
                        <asp:Label ID="lblWBS" runat="server" Text="WBS : "></asp:Label>
                        <asp:DropDownList ID="ddlWBS" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWBS_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvActivity" runat="server" AutoGenerateColumns="false" OnRowCommand="gvActivity_RowCommand"
                                    OnRowDataBound="gvActivity_RowDataBound" CssClass="gridview">
                                    <EmptyDataTemplate>
                                        <span>No activities Found </span>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Assign">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkAssignActivity" runat="server" rel="ricePhoto[frame]" title="<b>Activity Assignment</b>">Assign</asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="display" ItemStyle-CssClass="display" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                   <%--     <asp:BoundField DataField="PlannedStartDate" HeaderText="Planned Start Date" HeaderStyle-CssClass="display"
                                            ItemStyle-CssClass="display" />
                                        <asp:BoundField DataField="PlannedEndDate" HeaderText="Planned End Date" HeaderStyle-CssClass="display"
                                            ItemStyle-CssClass="display" />--%>
                                        <asp:BoundField DataField="Progress" HeaderText="Progress" />
                                        <asp:BoundField DataField="TotalManHours" HeaderText="Hours" HeaderStyle-CssClass="display"
                                            ItemStyle-CssClass="display" />
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Button ID="btnAdd" runat="server" Text="Add Activity" OnClick="btnAdd_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:Button ID="btnDummyAdd" runat="server" Text="Dummy" Style="display: none;" />
    <asp:Button ID="btnDummyPopUpOK" runat="server" Text="Dummy" Style="display: none;" />
    <asp:Button ID="btnDummyEdit" runat="server" Text="Dummy" Style="display: none;" />
    <asp:Panel ID="plnAddActivity" runat="server" Style="background-color: White; border: 5px solid #000000;
        border-radius: 25px; color: Black;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <center><span style="width: 100%; text-align: center; font-family: 'Open Sans Condensed', sans-serif;
                    font-size: large; font-weight: bold;">Add New Activity</span></center>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblDescriptionAdd" runat="server" Text="Description : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescriptionAdd" runat="server" TextMode="MultiLine" ValidationGroup="Add"
                                Style="min-width: 450px; max-width: 450px; min-height: 50px; max-height: 50px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtDescriptionAdd" runat="server" ErrorMessage="Please Enter Description"
                                ControlToValidate="txtDescriptionAdd" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtDescriptionAdd" runat="server"
                                TargetControlID="rfvtxtDescriptionAdd" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvResource" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvResource_RowDataBound"
                                OnRowCommand="gvResource_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Resource Type">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblResourceTypeAdd" runat="server" Text="Resource Type : "></asp:Label>--%>
                                            <asp:DropDownList ID="ddlResourceTypeAdd" runat="server" ValidationGroup="Add">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlResourceTypeAdd" runat="server" ErrorMessage="Please Select Resource Type"
                                                ControlToValidate="ddlResourceTypeAdd" Display="None" InitialValue="0" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlResourceTypeAdd" runat="server"
                                                TargetControlID="rfvddlResourceTypeAdd" PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Planned Start Date">
                                        <ItemTemplate>
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
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="Planned End Date">
                                        <ItemTemplate>
                                            
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
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Hours Per day">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtHoursAdd" runat="server" ValidationGroup="Add" onblur="return validTime(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtHoursAdd" runat="server" ErrorMessage="Please Enter Hours"
                                                ControlToValidate="txtHoursAdd" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtHoursAdd" runat="server" TargetControlID="rfvtxtHoursAdd"
                                                PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDeleteResourceAdd" runat="server" CommandName="RemoveResource">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Button ID="btnAddResource" runat="server" Text="Add Resource" OnClick="btnAddResource_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnAddNewActivity" runat="server" Text="Add New Activity" ValidationGroup="Add"
                                OnClick="btnAddNewActivity_Click" />
                            <asp:Button ID="btnCancelAdd" runat="server" Text="Cancel" OnClick="btnCancelAdd_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeAddActivity" runat="server" TargetControlID="btnDummyAdd"
        PopupControlID="plnAddActivity" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancelAdd">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlOKPopUp" runat="server" Style="background-color: White; border: 5px solid #000000;
        border-radius: 25px; color: Black; padding: 15px 15px 15px 15px;">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Label ID="lblMessageOKPopUp" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                            <asp:Button ID="btnOKPopUp" runat="server" Text="OK" OnClick="btnOKPopUp_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeOKPopUp" runat="server" TargetControlID="btnDummyPopUpOK"
        PopupControlID="pnlOKPopUp" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnOKPopUp">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlEditActivity" runat="server" Style="background-color: White; border: 5px solid #000000;
        border-radius: 25px; color: Black;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <center><span style="width: 100%; text-align: center; font-family: 'Open Sans Condensed', sans-serif;
                    font-size: large; font-weight: bold;">Modify Activity</span></center>
                <asp:Label ID="lblActivityIdEdit" runat="server" Text="" Style="display: none;"></asp:Label>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblDescriptionEdit" runat="server" Text="Description : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescriptionEdit" runat="server" TextMode="MultiLine" ValidationGroup="Edit"
                                Style="min-height: 50px; max-height: 50px; min-width: 450px; max-width: 450px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvResourceEdit" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvResourceEdit_RowDataBound"
                                OnRowCommand="gvResourceEdit_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="display" ItemStyle-CssClass="display">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFlagEdit" runat="server" Text="NEW"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="display" ItemStyle-CssClass="display">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResourceID" runat="server" Text="0"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Resource Type">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblResourceTypeAdd" runat="server" Text="Resource Type : "></asp:Label>--%>
                                            <asp:DropDownList ID="ddlResourceTypeEdit" runat="server" ValidationGroup="Edit">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlResourceTypeEdit" runat="server" ErrorMessage="Please Select Resource Type"
                                                ControlToValidate="ddlResourceTypeEdit" Display="None" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlResourceTypeEdit" runat="server"
                                                TargetControlID="rfvddlResourceTypeEdit" PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Planned Start Date">
                                        <ItemTemplate>
                                            
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
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="Planned End Date">
                                        <ItemTemplate>
                                          
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
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Hours Per day">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtHoursEdit" runat="server" ValidationGroup="Edit"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtHoursEdit" runat="server" ErrorMessage="Please Enter Hours"
                                                ControlToValidate="txtHoursEdit" Display="None" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtHoursEdit" runat="server" TargetControlID="rfvtxtHoursEdit"
                                                PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDeleteResourceEdit" runat="server" CommandName="RemoveResource">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Button ID="btnAddResourceEdit" runat="server" Text="Add Resource" OnClick="btnAddResourceEdit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnEditActivity" runat="server" Text="Modify" ValidationGroup="Edit"
                                OnClick="btnEditActivity_Click" />
                            <asp:Button ID="btnEditCancel" runat="server" Text="Cancel" 
                                onclick="btnEditCancel_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeEditActivity" runat="server" TargetControlID="btnDummyEdit"
        PopupControlID="pnlEditActivity" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnEditCancel">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Label ID="lblError" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
