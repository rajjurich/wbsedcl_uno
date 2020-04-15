<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="TE_Milestone.aspx.cs" Inherits="UNO.TE_Milestone" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Modelpop.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .gvdata
        {
            background-color: #EFFBFF;
            text-align: center;
            border-width: 0px;
            top: 111px;
            left: 11px;
        }
        .style6
        {
        }
        .style7
        {
            width: 41px;
        }
        .style10
        {
        }
        .style11
        {
        }
        .divWrapper
        {
            border-radius: 25px;
            border: 5px solid #059EDC;
            background-color: #059EDC;
            height: auto;
            margin-top: 0px;
        }
        .style18
        {
            width: 165px;
        }
        .style19
        {
            width: 267px;
        }
        .style20
        {
            width: 333px;
        }
        .style21
        {
            width: 1155px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="style6">
                &nbsp;
            </td>
            <td class="TDClassControl">
                &nbsp;
            </td>
            <td class="TDClassControl">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="50%">
        <tr>
            <td align="right">
                &nbsp;
            </td>
            <td>
                <%-- <asp:TemplateField HeaderStyle-BackColor ="#99CCFF"  HeaderText="Select">
                                        <ItemStyle HorizontalAlign="Center " />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="iDSelect" runat="server" Name ="Select" OnCheckedChanged="chkSelectChanged" AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
            </td>
            <td>
                <%--   <asp:GridView runat="server" ID="msView" AutoGenerateColumns="false" AllowPaging="true" 
                                CssClass="gvdata" Height="82px" width="100%" PageSize ="10" 
                                HorizontalAlign="Center" BorderWidth ="1px" 
                    BorderColor ="Black">
                            
                                <rowstyle   forecolor="DarkBlue" BorderStyle ="Solid"  BorderColor ="Black" BorderWidth ="1px"/>
                                <alternatingrowstyle backcolor="#99CCFF"  BorderStyle ="Solid"  BorderColor ="Black" BorderWidth ="1px"/>
                                <Columns>
                                    <asp:TemplateField HeaderStyle-BackColor ="#99CCFF"  HeaderText="Select">
                                        <ItemStyle HorizontalAlign="Center " />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="iDSelect" runat="server" Name ="Select" OnCheckedChanged="chkSelectChanged" AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                             <asp:BoundField DataField ="Description" ItemStyle-HorizontalAlign="Center"   HeaderText ="Milestone Description" HeaderStyle-BackColor ="#99CCFF" />
                                            <asp:BoundField DataField ="PlannedStartDate" ItemStyle-HorizontalAlign="Center"   HeaderText ="Milestone Start Date" HeaderStyle-BackColor ="#99CCFF" />
                                            <asp:BoundField DataField="PlannedEndDate"  ItemStyle-HorizontalAlign="Left"    HeaderText="Milestone End Date" HeaderStyle-BackColor ="#99CCFF"  />
                          <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Modify">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Remove">Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                                </Columns>
                            </asp:gridview>--%>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" style="height: 331px">
        <tr>
            <td>
                <div class="divWrapper">
                    <asp:Label ID="lblDescriptionEdit2" Style="left: 47%; position: absolute" runat="server"
                        Font-Bold="True" Text="Milestone " Font-Size="Large"></asp:Label>
                    <br />
                    Project Selection :
                    <asp:DropDownList ID="ddl_projectnames" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_projectnames_SelectedIndexChanged">
                        <asp:ListItem>Select..</asp:ListItem>
                    </asp:DropDownList>
                    <asp:GridView runat="server" ID="msView" AutoGenerateColumns="false" AllowPaging="true"
                        CssClass="gvdata" Height="68px" Width="100%" PageSize="10" BorderStyle="None"
                        HorizontalAlign="Center" EmptyDataText="No Milestones are found." OnRowCommand="msView_RowCommand"
                        OnPageIndexChanging="msView_PageIndexChanging">
                        <AlternatingRowStyle BackColor="#F0F0F0" />
                        <Columns>
                            <%-- <asp:TemplateField HeaderStyle-BackColor ="#99CCFF"  HeaderText="Select">
                                        <ItemStyle HorizontalAlign="Center " />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="iDSelect" runat="server" Name ="Select" OnCheckedChanged="chkSelectChanged" AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                            <asp:BoundField DataField="Description" ItemStyle-HorizontalAlign="Center" HeaderText="Milestone Description" />
                            <%--    <asp:BoundField DataField="PlannedStartDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MM-yyyy}"
                                HeaderText="Milestone Start Date" />
                            <asp:BoundField DataField="PlannedEndDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MM-yyyy}"
                                HeaderText="Milestone End Date" />--%>
                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Modify" CommandArgument='<%#Eval("ID")%>'>Edit</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Remove" CommandArgument='<%#Eval("ID")%>'>Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="btn_add" CssClass="button" Text="Add" Style="border-radius: 10px;"
                        Width="105px" runat="server" OnClick="btn_add_Click" Height="27px" ClientIDMode="Static" />
                </div>
                <%--
                <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                    Text="Edit Milestone Information"></asp:Label>--%>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblbaselinemsg" runat="server" Font-Size="Larger" ForeColor="Red"
                    Text="Project is Baseline Cant Modify" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <!--vaibhav code-->
    <asp:Panel ID="pnl_Edit" runat="server" Style="background-color: White; border: 2px solid silver;
        border-radius: 25px; color: Black;" Width="417px">
        <center style="width: 325px"><span style="width: 100%; text-align: center; font-family: 'Open Sans Condensed', sans-serif;
                    font-size: large; font-weight: bold;">Edit Milestone</span></center>
        <%--
                <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                    Text="Edit Milestone Information"></asp:Label>--%>
        <table style="width: 300px">
            <tr>
                <td class="style20">
                    &nbsp;
                </td>
                <td class="style21">
                    &nbsp;
                </td>
                <td style="font-weight: bold">
                    <asp:Label ID="lblproject_id" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                    <asp:Label ID="lblmilestone_id" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style20">
                    &nbsp;
                </td>
                <td class="style21">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Milestone Description:"></asp:Label>
                </td>
                <td class="style18">
                    <asp:TextBox ID="txtDescriptionEdit_pnl" runat="server" Style="margin-left: 1px"
                        TextMode="MultiLine" ValidationGroup="Edit"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtDescriptionEdit_pnl" runat="server" ControlToValidate="txtDescriptionEdit_pnl"
                        Display="None" ErrorMessage="Please Enter Description" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                        PopupPosition="Right" TargetControlID="rfvtxtDescriptionEdit_pnl">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <%--<tr>
                <td class="style20">
                    &nbsp;
                </td>
                <td class="style21">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Planned Start Date :"></asp:Label>
                </td>
                <td class="style18">
                    <asp:TextBox ID="txtPlannedStartDate_edit" runat="server" dataformatstring="{0:dd-MM-yyyy}"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                        PopupButtonID="txtPlannedStartDate_edit" TargetControlID="txtPlannedStartDate_edit">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="rfvtxtPlannedStartDate_edit" runat="server" ControlToValidate="txtPlannedStartDate_edit"
                        Display="None" ErrorMessage="Please Select Planned Start Date" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                        PopupPosition="Right" TargetControlID="rfvtxtPlannedStartDate_edit">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revtxtPlannedStartDate_edit" runat="server" ControlToValidate="txtPlannedStartDate_edit"
                        Display="None" ErrorMessage="Start Date is not valid" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                        PopupPosition="Right" TargetControlID="revtxtPlannedStartDate_edit">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>--%>
            <tr>
                <td class="style20">
                    &nbsp;
                </td>
                <td class="style21">
                    &nbsp;
                </td>
                <td class="style18">
                    &nbsp;
                </td>
            </tr>
            <%--<tr>
                <td class="style20">
                    &nbsp;
                </td>
                <td class="style21">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Planned End Date :"></asp:Label>
                </td>
                <td class="style18">
                    <asp:TextBox ID="txtPlannedEndDate_edit" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                        PopupButtonID="txtPlannedEndDate_edit" TargetControlID="txtPlannedEndDate_edit">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>--%>
            <%--<tr>
                <td class="style20">
                    &nbsp;
                </td>
                <td class="style21">
                    &nbsp;
                </td>
                <td class="style18">
                    <asp:RequiredFieldValidator ID="rfvtxtPlannedEndDate_edit" runat="server" ControlToValidate="txtPlannedEndDate_edit"
                        Display="None" ErrorMessage="Please Select Planned End Date" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                        PopupPosition="Right" TargetControlID="rfvtxtPlannedEndDate_edit">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revtxtPlannedEndDate_edit" runat="server" ControlToValidate="txtPlannedEndDate_edit"
                        Display="None" ErrorMessage="End Date is not valid" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                        PopupPosition="Right" TargetControlID="revtxtPlannedEndDate_edit">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="vctxtPlannedEndDate_edit" runat="server" ControlToCompare="txtPlannedStartDate_edit"
                        ControlToValidate="txtPlannedEndDate_edit" ErrorMessage="Plan End Date Should be greater than Start Date"
                        Operator="LessThan" Type="Date" ValidationGroup="Edit"></asp:CompareValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                        PopupPosition="Right" TargetControlID="vctxtPlannedEndDate_edit">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>--%>
            <tr>
                <td align="center" class="style20">
                    &nbsp;
                </td>
                <td align="center" colspan="2">
                    <asp:Button ID="Button2" runat="server" Style="display: none;" Text="test" />
                    <asp:Button ID="btnmodify_sub" runat="server" OnClick="btnmodify_sub_Click" Text="Modify" />
                    <asp:Button ID="Button4" runat="server" Text="Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Button ID="Button3" runat="server" Text="Cancel" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button3"
        PopupControlID="pnl_Edit" BackgroundCssClass="modalBackground" Enabled="true"
        OkControlID="Button1" CancelControlID="btnCancelAdd">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlAddProject" runat="server" Style="background-color: White; border: 2px solid silver;
        border-radius: 25px; color: Black;" Width="387px">
        <center><span style="width: 100%; text-align: center; font-family: 'Open Sans Condensed', sans-serif;
                    font-size: large; font-weight: bold;">Add Milestone</span></center>
        <table>
            <tr>
                <%-- <td class="style11" align="center" colspan="5">
                            <asp:Label ID="lblDescriptionEdit0" runat="server" Font-Bold="True" 
                                Text="Add Milestone "></asp:Label>
                        </td>--%>
            </tr>
            <tr>
                <td class="style11">
                    &nbsp;
                </td>
                <td class="style10">
                    &nbsp;
                </td>
                <td colspan="3" style="font-weight: bold">
                </td>
            </tr>
            <tr>
                <td class="style11">
                    &nbsp;
                </td>
                <td class="style10">
                    <asp:Label ID="lblDescriptionEdit" runat="server" Font-Bold="True" Text="Milestone Description :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDescriptionEdit" runat="server" Style="margin-left: 1px" TextMode="MultiLine"
                        ValidationGroup="Edit"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtDescriptionEdit" runat="server" ControlToValidate="txtDescriptionEdit"
                        Display="None" ErrorMessage="Please Enter Description" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtDescriptionEdit" runat="server"
                        PopupPosition="Right" TargetControlID="rfvtxtDescriptionEdit">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <%--<tr>
                <td class="style11">
                    &nbsp;
                </td>
                <td class="style10">
                    <asp:Label ID="lblPlannedStartDateAdd" runat="server" Font-Bold="True" Text="Planned Start Date :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPlannedStartDateAdd" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="caltxtPlannedStartDateAdd" runat="server" Format="dd-MM-yyyy"
                        PopupButtonID="txtPlannedStartDateAdd" TargetControlID="txtPlannedStartDateAdd">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="rfvtxtPlannedStartDateAdd" runat="server" ControlToValidate="txtPlannedStartDateAdd"
                        Display="None" ErrorMessage="Please Select Planned Start Date" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtPlannedStartDateAdd" runat="server"
                        PopupPosition="Right" TargetControlID="rfvtxtPlannedStartDateAdd">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revtxtPlannedStartDateAdd" runat="server" ControlToValidate="txtPlannedStartDateAdd"
                        Display="None" ErrorMessage="Start Date is not valid" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtPlannedStartDateAdd" runat="server"
                        PopupPosition="Right" TargetControlID="revtxtPlannedStartDateAdd">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
                <td class="style7">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>--%>
            <%--<tr>
                <td class="style11">
                    &nbsp;
                </td>
                <td class="style10">
                    <asp:Label ID="lblPlannedEndDateAdd0" runat="server" Font-Bold="True" Text="Planned End Date :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPlannedEndDateAdd" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="cetxtPlannedEndDateAdd" runat="server" Format="dd-MM-yyyy"
                        PopupButtonID="txtPlannedEndDateAdd" TargetControlID="txtPlannedEndDateAdd">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="rfvtxtPlannedEndDateAdd" runat="server" ControlToValidate="txtPlannedEndDateAdd"
                        Display="None" ErrorMessage="Please Select Planned End Date" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtPlannedEndDateAdd" runat="server"
                        PopupPosition="Right" TargetControlID="rfvtxtPlannedEndDateAdd">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator ID="revtxtPlannedEndDateAdd" runat="server" ControlToValidate="txtPlannedEndDateAdd"
                        Display="None" ErrorMessage="End Date is not valid" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtPlannedEndDateAdd" runat="server"
                        PopupPosition="Right" TargetControlID="revtxtPlannedEndDateAdd">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="vctxtPlannedEndDateAdd" runat="server" ControlToCompare="txtPlannedStartDateAdd"
                        ControlToValidate="txtPlannedEndDateAdd" Display="None" ErrorMessage="End Date cannot be greater than Start Date"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="Edit"></asp:CompareValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcevctxtPlannedEndDateAdd" runat="server"
                        PopupPosition="Right" TargetControlID="vctxtPlannedEndDateAdd">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
                <td class="style7">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>--%>
            <tr>
                <td class="style11">
                    &nbsp;
                </td>
                <td class="style10">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="style7">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" class="style11">
                    &nbsp;
                </td>
                <td align="center" colspan="3">
                    <asp:Button ID="Button1" runat="server" Text="test" Style="display: none;" />
                    <asp:Button ID="btnSubmitAdd" runat="server" OnClick="btnSubmitAdd_Click1" Text="Add Milestone" />
                    <asp:Button ID="btnCancelAdd" runat="server" Text="Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeAddProject" runat="server" TargetControlID="btn_add"
        PopupControlID="pnlAddProject" BackgroundCssClass="modalBackground" Enabled="true"
        OkControlID="Button1" CancelControlID="btnCancelAdd">
    </ajaxToolkit:ModalPopupExtender>
    <!--end-vaibhav code-->
    <%--<asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>--%>
    <asp:Panel ID="pnl_del_project" runat="server" Style="background-color: White; border: 2px solid silver;
        border-radius: 7px; color: Black;" Width="262px">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblDescriptionEdit1" runat="server" Font-Bold="True" Text="Delete Milestone "></asp:Label>
        <table>
            <tr>
                <td class="style11">
                    &nbsp;
                </td>
                <td class="style10">
                    <asp:Label ID="lblmilestone_id_delpnl" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                </td>
                <td style="font-weight: bold">
                </td>
            </tr>
            <tr>
                <td class="style11" colspan="3">
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtDescriptionEdit0" runat="server"
                        PopupPosition="Right" TargetControlID="rfvtxtDescriptionEdit">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Do You Want Delete this Record
                </td>
            </tr>
            <tr>
                <td class="style11" colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style11" align="center">
                    &nbsp;
                </td>
                <td align="center" colspan="2">
                    <asp:Button ID="btn_sub" runat="server" Style="display: none;" Text="test" />
                    <asp:Button ID="btn_del_submit" runat="server" Text="Yes" OnClick="btn_del_submit_Click" />
                    <asp:Button ID="btn_cl" runat="server" Text="No" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ModalPopupExtender ID="Pops_ModalPopupExtender" runat="server" Enabled="True"
        TargetControlID="btn_sub" DropShadow="true" PopupControlID="pnl_del_project"
        OkControlID="btn_cl" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <%--    <asp:ModalPopupExtender 
        ID="Pops_ModalPopupExtender" 
        runat="server" 
        Enabled="True" 
        TargetControlID="btnTemp"
        DropShadow="true"
        PopupControlID="Pops"
        OkControlID="btnclose" 
        BackgroundCssClass="modalBackground">
     </asp:ModalPopupExtender>--%>
    <asp:Button ID="btnTemp" runat="server" Text="" Style="display: none;" />
    <%--    <asp:ModalPopupExtender 
        ID="Pops_ModalPopupExtender" 
        runat="server" 
        Enabled="True" 
        TargetControlID="btnTemp"
        DropShadow="true"
        PopupControlID="Pops"
        OkControlID="btnclose" 
        BackgroundCssClass="modalBackground">
     </asp:ModalPopupExtender>--%>
    <table runat="server" id="tblLbl" align="center">
        <tr>
            <td align="center">
                <asp:Label ID="LblMsg" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
