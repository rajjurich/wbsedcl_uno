<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="TEClientMasterView.aspx.cs" Inherits="UNO.TEClientMasterView" %>

<%@ Register Assembly="DemoGrid" Namespace="DemoGrid.GridViewControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">



        function DltConfirmationbox() {

            if (!validateCheckBoxes())
                return false;

            var msg = confirm("Record(s) marked for Deletion. Continue?");
            if (msg == false) {
                clearGridCheckBoxes();
                return false;
            }
        }


        function clearGridCheckBoxes() {
            var gridRef = document.getElementById('<%= Grid.ClientID %>');
            var inputElementArray = gridRef.getElementsByTagName('input');
            var cntchk = 0;
            for (var i = 0; i < inputElementArray.length; i++) {
                var elementRef = inputElementArray[i];

                if ((elementRef.type == 'checkbox')) {
                    elementRef.checked = false;
                }
            }
        }
        function validateCheckBoxes() {

            var isValid = false;

            var gridView = document.getElementById('<%= Grid.ClientID %>');

            var inputs = gridView.getElementsByTagName('input');

            for (var i = 0; i < inputs.length; i++) {

                var elementRef = inputs[i];

                if ((elementRef.type == 'checkbox') && (elementRef.checked == true)) {
                    isValid = true;

                    return true;


                }
            }
            alert("Please select record");
            return false;
        }


        function validateCheckFocus() {

            var isValid = false;

            var gridView = document.getElementById('<%= Grid.ClientID %>');

            var inputs = gridView.getElementsByTagName('input');

            for (var i = 0; i < inputs.length; i++) {

                var elementRef = inputs[i];

                if ((elementRef.type == 'checkbox') && (elementRef.checked == true)) {
                    document.getElementById('btnDelete').focus();
                    isValid = true;

                    return true;


                }
            }


        }
        function HideLabel(labelID) {
            setTimeout("HideLabelHelper('" + labelID + "');", 3000);
        }
        function HideLabelHelper(labelID) {
            document.getElementById(labelID).style.display = "none";
        }



    
     

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <asp:Panel ID="Pnl" runat="server" Width="100%" CssClass="srcColor">
        <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                    <asp:Label ID="lblHead" runat="server" Text="Client Master View" ForeColor="RoyalBlue"
                        Font-Size="20px" Width="100%" Height="20px" CssClass="heading">
                    </asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <table id="tbl" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
            class="TableClass">
            <tr>
                <td align="right" style="width: 33%" class="LinkControl">
                    <asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="~/TimeExpenseDashboard.aspx"
                        ForeColor="Blue">Back to Time Expense DashBoard</asp:HyperLink>
                </td>
            </tr>
        </table>
        <br />
        <table id="Table1" width="100%" border="0" runat="server">
            <tr width="100%">
                <td width="15%">
                    <asp:Button runat="server" ID="btnNew" Text="New" Width="90%" CssClass="ButtonControl"
                        ClientIDMode="Static" OnClick="btnNew_Click" />
                </td>
                <td width="15%">
                    <asp:Button runat="server" ID="btnDelete" Text="Delete" Width="90%" CssClass="ButtonControl"
                        OnClick="btnDelete_Click" OnClientClick="return DltConfirmationbox()" ClientIDMode="Static" />
                </td>
                <td width="15%">
                    <asp:Button runat="server" ID="btnSearch" Text="Search" Width="90%" CssClass="ButtonControl" Visible="false" />
                </td>
                <td width="55%">
                </td>
            </tr>
        </table>
        <!-- bug 87 solved start --Swapnil -->
        <table id="Table4" width="100%" border="0" runat="server">
            <tr>
                <td style="height: 10px; width: 15%; text-align: right">
                    Client Id :
                </td>
                <td width="20%">
                    <asp:TextBox ID="txtCompanyId" runat="server" Width="95px" CssClass="TextBox"></asp:TextBox>
                </td>
                <td class="style37" style="text-align: right; width: 15%;">
                    Client Name :
                </td>
                <td style="width: 20%;">
                    <asp:TextBox ID="txtCompanyName" runat="server" Width="116px" CssClass="TextBox"></asp:TextBox>
                </td>
                <td width="15%">
                    <asp:Button runat="server" ID="cmdSearch" Text="Search" Width="90%" CssClass="ButtonControl"
                        ClientIDMode="Static" OnClick="cmdSearch_Click" />
                </td>
                <td width="15%">
                    <asp:Button runat="server" ID="cmdReset" Text="Reset" Width="90%" CssClass="ButtonControl"
                        OnClick="cmdReset_Click" />
                </td>
            </tr>
        </table>
        <!-- bug 87 solved End --Swapnil -->
        <table style="width: 100%" cellpadding="0" cellspacing="0">
            <tr width="100%">
                <td>
                    <br />
                    <div id="divGridView">
                        <cc1:GridView ID="Grid" runat="server" AutoGenerateColumns="False" Width="90%" CssClass="tablestyle"
                            AllowSorting="True" AllowPaging="True" OnDataBound="Grid_DataBound" OnPageIndexChanging="Grid_PageIndexChanging"
                            OnPageIndexChanged="Grid_PageIndexChanged" OnSorting="Grid_Sorting" CellPadding="0"
                            OnRowEditing="Grid_RowEditing1">
                            <HeaderStyle CssClass="headerstyle" />
                            <RowStyle CssClass="rowstyle" Wrap="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <EmptyDataRowStyle BackColor="#edf5ff" Height="300px" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <AlternatingRowStyle CssClass="altrowstyle" Wrap="True" />
                            <Columns>
                                <asp:TemplateField HeaderText="Select" SortExpression="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" ShowHeader="False" SortExpression="Edit">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                            Text="Update" ForeColor="#3366FF"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                            Text="Cancel" ForeColor="#3366FF"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                            Text="Edit" ForeColor="#3366FF"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CLIENT_ID" HeaderText="Client ID" SortExpression="CLIENT_ID">
                                    <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Client_DESC" HeaderText="Client Description" SortExpression="Client_DESC">
                                    <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CLIENT_NAME" HeaderText="Client Name" SortExpression="CLIENT_NAME">
                                    <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CLIENT_SITE_ADDRESS" HeaderText="Client Address" SortExpression="CLIENT_SITE_ADDRESS">
                                    <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CLIENT_PHONE1" HeaderText="Phone Number" SortExpression="CLIENT_PHONE1">
                                    <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CLIENT_CONTACT_PERSON1" HeaderText="First Contact Person"
                                    SortExpression="CLIENT_CONTACT_PERSON1">
                                    <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CLIENT_CONTPER1_PHONE1" HeaderText="First Contact Person's Number"
                                    SortExpression="CLIENT_CONTPER1_PHONE1">
                                    <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CLIENT_CONTACT_PERSON2" HeaderText="Second Contact Person"
                                    SortExpression="CLIENT_CONTACT_PERSON2">
                                    <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CLIENT_CONTPER2_PHONE1" HeaderText="Second Contact Person's Number"
                                    SortExpression="CLIENT_CONTPER2_PHONE1">
                                    <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                </asp:BoundField>
                            </Columns>
                            <RowStyle Wrap="true" Width="150px" />
                            <PagerTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: left">
                                            Page Size :
                                            <asp:DropDownList ID="ddPageSize" runat="server" EnableViewState="true" OnSelectedIndexChanged="ddPageSize_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Text="5"></asp:ListItem>
                                                <asp:ListItem Text="10"></asp:ListItem>
                                                <asp:ListItem Text="15"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblPageCount" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </PagerTemplate>
                        </cc1:GridView>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <table class="TableClass">
            <tr width="100%">
                <td>
                    <div style="margin-top: 5px">
                        <asp:DataPager ID="pager" runat="server" PagedControlID="Grid">
                            <Fields>
                                <asp:NextPreviousPagerField FirstPageText="&lt;&lt;" LastPageText="&gt;&gt;" NextPageText="&gt;"
                                    PreviousPageText="&lt;" ShowFirstPageButton="True" ShowNextPageButton="False"
                                    ButtonCssClass="datapager" />
                                <asp:NumericPagerField ButtonCount="10" NumericButtonCssClass="datapager" CurrentPageLabelCssClass="datapager" />
                                <asp:NextPreviousPagerField LastPageText="&gt;&gt;" NextPageText="&gt;" ShowLastPageButton="True"
                                    ShowPreviousPageButton="False" ButtonCssClass="datapager" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <table runat="server" id="tblLbl" align="center">
            <tr>
                <td align="center">
                    <asp:Label ID="LblMsg" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
</asp:Content>
