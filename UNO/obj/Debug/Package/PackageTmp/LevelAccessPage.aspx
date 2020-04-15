<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="LevelAccessPage.aspx.cs" Inherits="UNO.LevelAccessPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function xyz() {
            $(function () {
                $("[id*=imgOrdersShow]").each(function () {
                    if ($(this)[0].src.indexOf("minus") != -1) {
                        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                        $(this).next().remove();
                    }
                });
                $("[id*=imgProductsShow]").each(function () {
                    if ($(this)[0].src.indexOf("minus") != -1) {
                        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                        $(this).next().remove();
                    }
                });
            });
        }
    </script>
    <style type="text/css">
        @media (max-height:1080)
        {
        
        }
        
        @media (max-height:1024)
        {
        
        }
        
        @media (max-height:800)
        {
        
        }
        
        @media (max-height:768)
        {
        
        }
    </style>
    <style type="text/css">
        body
        {
            /*font-family: Arial;
            font-size: 10pt;*/
        }
        .Grid td
        {
            width: 8%;
            color: black;
            font-size: 10pt;
            line-height: 200%;
            font-weight: bold;
            text-align: center;
        }
        .Grid th
        {
            background-color: #47a3da;
            color: White;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
        }
        .ChildGrid td
        {
            width: 15%;
            color: black;
            font-size: 10pt;
            font-weight: bold;
            line-height: 200%;
            text-align: center;
        }
        .ChildGrid th
        {
            background-color: #47a3da;
            color: White;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
        }
        .Nested_ChildGrid td
        {
            width: 10%;
            color: black;
            font-size: 10pt;
            font-weight: bold;
            line-height: 200%;
        }
        .Nested_ChildGrid th
        {
            background-color: #47a3da;
            color: White;
            font-size: 10pt;
            text-align: center;
            line-height: 200%;
        }
        .Grid
        {
            width: 100%;
            background-color: #48a4db;
        }
        
        .ChildGrid
        {
            width: 100%;
            background-color: #88c4e8;
        }
        
        .Nested_ChildGrid
        {
            width: 100%;
            background-color: #c8e4f4;
        }
        .DivGrid
        {
            /*top: 5%;*/
        }
        
        .style37
        {
            width: 254px;
        }
        .style38
        {
            width: 167px;
        }
        
        
        .style39
        {
            width: 133px; /*right:170px;	 	position:absolute;*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Levels" ForeColor="RoyalBlue" Font-Size="20px"
                    Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div class="DivGrid">
        <table style="width: 70%; margin-left: 15%;">
            <tr>
                <td class="style39">
                    &nbsp;
                </td>
                <td class="style37">
                    &nbsp;
                </td>
                <td class="style38">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;
                    <asp:Label ID="Label2" runat="server" Text="Level Code :"></asp:Label><span style="color: Red;">*</span>
                </td>
                <td align="left">
                    &nbsp;
                    <asp:TextBox ID="txtLevelCode" runat="server" ValidationGroup="Add" MaxLength="50"
                        onkeydown="return (event.keyCode!=13);" Style="text-transform: uppercase; text-align: left"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtLevelCode" runat="server" ErrorMessage="Please Enter Level Code"
                        ValidationGroup="Add" Display="None" ControlToValidate="txtLevelCode"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtLevelCode" runat="server" TargetControlID="rfvtxtLevelCode"
                        PopupPosition="Right">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                        FilterType="Numbers,Custom,LowercaseLetters,UppercaseLetters" TargetControlID="txtLevelCode"
                        ValidChars="-ABC" />
                </td>
                <td align="right">
                    &nbsp;
                    <asp:Label ID="Label1" runat="server" Text="Level Descripton :"></asp:Label><span
                        style="color: Red;">*</span>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtLevelDescrip" runat="server" ValidationGroup="Add" MaxLength="50"
                        onkeydown="return (event.keyCode!=13);" Style="text-transform: capitalize;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtLevelDescrip" runat="server" ErrorMessage="Please Enter Description."
                        ControlToValidate="txtLevelDescrip" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtLevelDescrip" runat="server" TargetControlID="rfvtxtLevelDescrip"
                        PopupPosition="Right">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="max-height: 400px; width: 80%; text-align: center; margin-left: 10%;
                    border: 2px solid black; overflow: scroll; border-radius: 5px">
                    <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" CssClass="Grid"
                        ShowHeader="false" DataKeyNames="modules" BorderStyle="None" OnRowDataBound="gvCustomers_RowDataBound"
                        Height="100%">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgOrdersShow" runat="server" OnClick="Show_Hide_OrdersGrid"
                                        ImageUrl="~/images/plus.png" CommandArgument="Show" />
                                    <asp:Panel ID="pnlOrders" runat="server" Visible="false" Style="position: relative">
                                        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" PageSize="5"
                                            ShowHeader="false" AllowPaging="false" CssClass="ChildGrid" PagerStyle-HorizontalAlign="Center"
                                            PagerStyle-ForeColor="Black" PagerStyle-Font-Bold="true" DataKeyNames="ID" OnRowDataBound="gvOrders_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgProductsShow" runat="server" OnClick="Show_Hide_ProductsGrid"
                                                            ImageUrl="~/images/plus.png" CommandArgument="Show" />
                                                        <asp:Panel ID="pnlProducts" runat="server" Visible="false" Style="position: relative">
                                                            <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" PageSize="2"
                                                                ShowHeader="false" AllowPaging="false" DataKeyNames="ID" PagerStyle-HorizontalAlign="Center"
                                                                PagerStyle-ForeColor="Black" PagerStyle-Font-Bold="true" CssClass="Nested_ChildGrid">
                                                                <Columns>
                                                                    <asp:BoundField ItemStyle-Width="500px" DataField="name" />
                                                                    <asp:TemplateField HeaderText="Operation">
                                                                        <ItemTemplate>
                                                                            <%--<asp:Button runat="server" Text="Edit" ID="testub" />--%>
                                                                            <asp:CheckBox ID="CheckBox3" runat="server" OnCheckedChanged="CheckBox3_CheckedChanged"
                                                                                AutoPostBack="true" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField ItemStyle-Width="500px" DataField="name" />
                                                <asp:TemplateField HeaderText="Operation">
                                                    <ItemTemplate>
                                                        <%--<asp:Button runat="server" Text="Edit" ID="testub" />--%>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" OnCheckedChanged="CheckBox2_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField ItemStyle-Width="55%" DataField="modules" />
                            <asp:TemplateField HeaderText="Operation">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged"
                                        AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <center>
            <table>
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Save" ID="btnSave" OnClick="btnSave_Click" Width="69px"
                            ValidationGroup="Add" CssClass="ButtonControl" />
                    </td>
                    <td>
                        <asp:Button runat="server" Text="Cancel" ID="btnCancel" OnClick="btnCancel_Click"
                            Width="69px" CssClass="ButtonControl" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; color: Red">
                        <asp:Label ID="lblMessages" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </center>
    </div>
</asp:Content>
