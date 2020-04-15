<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpenseEntry.aspx.cs" Inherits="UNO.ExpenseEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .gridview
        {
            border: 5px solid #059EDC;
            border-radius: 15px;
            padding: 5px 5px 5px 5px;
            background-color: #059EDC;
        }
        .body
        {
            font-family: Arial;
            font-size: small;
            height: 400px;
            width: 400px;
        }
        .display
        {
            display: none;
        }
    </style>
    <script>
        function validNumber(sender) {
            try {
                var intRegex = /^\d+$/;
                var number = sender.value;
                if (!intRegex.test(number)) {
                    number = number.substring(0, number.length - 1);
                    sender.value = number;
                }
            }
            catch (ex) {
                alert(ex.Message);
            }
        }
    </script>
</head>
<body class="body">
    <form id="form1" runat="server">
    <div style="width: 450px; height: 450px; max-height: 450px; max-width: 450px; overflow: auto;">
        <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvExpense_RowDataBound"
            OnRowCommand="gvExpense_RowCommand" GridLines="None">
            <EmptyDataTemplate>
                <span>No Expense Recorded Yet.</span>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="gridview">
                            <div>
                                <table>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Label ID="lblFlagExpense" runat="server" Text="NEW" Style="display: none;"></asp:Label>
                                            <asp:Label ID="lblExpenseID" runat="server" Text="0" Style="display: none;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span><b>Purpose: </b></span>
                                        </td>
                                        <td colspan="5" style="padding: 0px 10px 0px 0px;">
                                            <asp:TextBox ID="txtPurpose" runat="server" TextMode="MultiLine" Style="width: 100%;
                                                max-width: 250px; min-width: 250px;" MaxLength="150"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtPurpose" runat="server" ErrorMessage="Enter Purpose"
                                                ControlToValidate="txtPurpose" Display="Dynamic" ValidationGroup="Submit" Enabled="true"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span><b>Mode: </b></span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMode" runat="server" OnSelectedIndexChanged="ddlMode_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Text="Owned" Value="Owned"></asp:ListItem>
                                                <asp:ListItem Text="By Air" Value="By Air"></asp:ListItem>
                                                <asp:ListItem Text="By Rail" Value="By Rail"></asp:ListItem>
                                                <asp:ListItem Text="By Road" Value="By Road"></asp:ListItem>
                                                <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <span><b>KM: </b></span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtKM" runat="server" Style="width: 50px;" MaxLength="5" onkeyup="validNumber(this);"></asp:TextBox>
                                            <asp:CustomValidator ID="cvtxtKM" runat="server" ErrorMessage="Entered Kilometers are not valid."
                                                Display="Dynamic" ValidationGroup="Submit" Enabled="true" OnServerValidate="cvtxtKM_OnServerValidate"
                                                ForeColor="Red"></asp:CustomValidator>
                                        </td>
                                        <td>
                                            <span><b>Amount: </b></span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount" runat="server" Style="width: 50px;" MaxLength="6" onkeyup="validNumber(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtAmount" runat="server" ErrorMessage="Enter Amount"
                                                ControlToValidate="txtAmount" Display="Dynamic" ValidationGroup="Submit" Enabled="true"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revrfvtxtAmount" runat="server" ErrorMessage="Not valid Amount"
                                                ControlToValidate="txtAmount" Display="Dynamic" ForeColor="Red" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span><b>Billable: </b></span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlBillable" runat="server">
                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" style="text-align: right;">
                                            <span><b>Paid By: </b></span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlPaidBy" runat="server">
                                                <asp:ListItem Text="Client" Value="Client"></asp:ListItem>
                                                <asp:ListItem Text="Self" Value="Self"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="text-align: center;">
                                <asp:GridView ID="gvEvidance" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvEvidance_RowDataBound"
                                    OnRowCommand="gvEvidance_RowCommand" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="display" ItemStyle-CssClass="display">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFlag" runat="server" Text="NEW" Style="display: none;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="display" ItemStyle-CssClass="display">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtExpenseID" runat="server" Style="display: none;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Number">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBillNo" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtBillNo" runat="server" ErrorMessage="Enter Bill No"
                                                    ControlToValidate="txtBillNo" Display="Dynamic" ValidationGroup="Submit" Enabled="true"></asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtDescription" runat="server" ErrorMessage="Enter Description"
                                                    ControlToValidate="txtDescription" Display="Dynamic" ValidationGroup="Submit"
                                                    Enabled="true"></asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDeleteEvidance" runat="server" CommandName="DeleteEvidance"
                                                    CommandArgument='<%#Eval("RES_EXP_ROWID")%>'>Delete</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Button ID="btnAddEvidance" runat="server" Text="Add Evidance" CommandName="AddEvidance"
                                    CommandArgument='<%#Eval("RES_EXP_ROWID")%>' />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDeleteExpense" runat="server" CommandName="DeleteExpense"
                            CommandArgument='<%#Eval("RES_EXP_ROWID")%>'>Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnAddExpense" runat="server" Text="Add Expense" OnClick="btnAddExpense_Click" />
    </div>
    <asp:Button ID="btnSubmitExpense" runat="server" Text="Submit" OnClick="btnSubmitExpense_Click"
        ValidationGroup="Submit" />
    <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblError" runat="server" Text="" Visible="false"></asp:Label>
    </form>
</body>
</html>
