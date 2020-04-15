<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TE_ActivityAssignment.aspx.cs"
    Inherits="UNO.TE_ActivityAssignment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .display
        {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="min-height: 900px; min-width: 900px; overflow: auto;">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <center><span style="width: 100%; text-align: center; font-family: 'Open Sans Condensed', sans-serif;
            font-size: large; font-weight: bold;">Activity Assignment</span></center>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:GridView ID="gvAssignment" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvAssignment_RowDataBound"
                        OnRowCommand="gvAssignment_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="display" ItemStyle-CssClass="display">
                                <ItemTemplate>
                                    <asp:Label ID="lblFlag" runat="server" Text="NEW"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="display" ItemStyle-CssClass="display">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Resource Type">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlResourceType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlResourceType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlEmployee" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Planned Start Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPlannedStartDate" runat="server" ValidationGroup="Submit"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="caltxtPlannedStartDate" runat="server" TargetControlID="txtPlannedStartDate"
                                        PopupButtonID="txtPlannedStartDate" Format="dd-MM-yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvtxtPlannedStartDate" runat="server" ErrorMessage="Please Select Planned Start Date"
                                        ControlToValidate="txtPlannedStartDate" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtPlannedStartDate" runat="server"
                                        TargetControlID="rfvtxtPlannedStartDate" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="revtxtPlannedStartDate" runat="server" ErrorMessage="Start Date is not valid"
                                        ControlToValidate="txtPlannedStartDate" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                                        Display="None" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtPlannedStartDate" runat="server"
                                        TargetControlID="revtxtPlannedStartDate" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Planned End Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPlannedEndDate" runat="server" ValidationGroup="Submit"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="caltxtPlannedEndDate" runat="server" TargetControlID="txtPlannedEndDate"
                                        PopupButtonID="txtPlannedEndDate" Format="dd-MM-yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvtxtPlannedEndDate" runat="server" ErrorMessage="Please Select Planned End Date"
                                        ControlToValidate="txtPlannedEndDate" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtPlannedEndDate" runat="server"
                                        TargetControlID="rfvtxtPlannedEndDate" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="revtxtPlannedEndDate" runat="server" ErrorMessage="End Date is not valid"
                                        ControlToValidate="txtPlannedEndDate" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                                        Display="None" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtPlannedEndDate" runat="server"
                                        TargetControlID="revtxtPlannedEndDate" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CompareValidator ID="cvtxtPlannedEndDate" runat="server" ErrorMessage="End Date cannot be greater than Start Date"
                                        ControlToValidate="txtPlannedEndDate" ControlToCompare="txtPlannedStartDate"
                                        Type="Date" Operator="GreaterThanEqual" Display="None" ValidationGroup="Submit"></asp:CompareValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcecvtxtPlannedEndDate" runat="server"
                                        TargetControlID="cvtxtPlannedEndDate" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hours Per Day">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtHours" runat="server" ValidationGroup="Submit" onblur="return validTime(this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtHours" runat="server" ErrorMessage="Please Enter Hours"
                                        ControlToValidate="txtHours" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtHours" runat="server" TargetControlID="rfvtxtHours"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Remove">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <asp:Button ID="btnAdd" runat="server" Text="Add New Resource" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="lblError" runat="server" Text="" Visible="false"></asp:Label>
    </form>
</body>
</html>
