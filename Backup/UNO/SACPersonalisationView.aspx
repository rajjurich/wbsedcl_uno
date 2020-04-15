<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="SACPersonalisationView.aspx.cs" Inherits="UNO.SACPersonalisationView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvPersonalisation.ClientID %>');

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Card Personalization" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div class="DivEmpDetails">
     <asp:Panel ID="Panel3" runat="server" DefaultButton="btnSearch">

        <table style="width: 100%;">
            <tr>
                <td style="width: 33%; text-align: left;">
                    <asp:Button runat="server" ID="btnModify" Text="Personalise" CssClass="ButtonControl"
                        OnClick="btnModify_Click" />
                    <%--<asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                        OnClick="btnDelete_Click" />--%>
                </td>
                <td style="width: 33%; text-align: center;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <span style="color: Black; font-weight: bold;">Employees:</span>
                            <asp:DropDownList ID="ddlMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMode_SelectedIndexChanged">
                                <asp:ListItem Text="All" Value="All" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Personalised" Value="Personalised"></asp:ListItem>
                                <asp:ListItem Text="Non-Personalised" Value="NonPersonalised"></asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 33%; text-align: right;">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" 
                        CssClass="ButtonControl" onclick="btnSearch_Click" />
                    <asp:TextBox ID="txtCompanyID"  runat="server"
                        Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCompanyID" runat="server" TargetControlID="txtCompanyID"
                        WatermarkText="Search by Employee ID" WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    <asp:TextBox ID="txtCompanyName"  runat="server"
                        Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCompanyName" runat="server" TargetControlID="txtCompanyName"
                        WatermarkText="Search by Employee Name" WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="3">
                    <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                        <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvPersonalisation" runat="server" AutoGenerateColumns="False" Width="100%"
                                GridLines="None" AllowPaging="True" OnPageIndexChanging="gvPersonalisation_PageIndexChanging"
                                OnRowCommand="gvPersonalisation_RowCommand">
                                <RowStyle CssClass="gvRow" />
                                <HeaderStyle CssClass="gvHeader" />
                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                <PagerStyle CssClass="gvPager" />
                                <EmptyDataTemplate>
                                    <div>
                                        <span>No Employees found.</span>
                                    </div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Edit" SortExpression="Edit" ItemStyle-Width="15%"
                                        HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                Text="Select" ForeColor="#3366FF" CommandArgument='<%#Eval("EPD_EMPID")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EPD_EMPID" HeaderText="Employee Code" SortExpression="EPD_EMPID">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="Employee Name" SortExpression="Name">
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
                            <asp:AsyncPostBackTrigger ControlID="ddlMode" />
                            <asp:AsyncPostBackTrigger ControlID="btnModify" />
                            <asp:AsyncPostBackTrigger ControlID="gvPersonalisation" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
                        TargetControlID="UpdatePanel2">
                        <Animations>
                        <OnUpdating>
                            <Parallel duration="0">
                                <ScriptAction Script="onUpdating();" />  
                                <FadeOut Duration="1.0" Fps="24" minimumOpacity=".5" />
                            </Parallel> 
                        </OnUpdating>
                        <OnUpdated>
                            <Parallel duration="0">
                                <ScriptAction Script="onUpdated();" /> 
                                <FadeIn Duration="1.0" Fps="24"  minimumOpacity=".5"/>
                            </Parallel> 
                        </OnUpdated>
                        </Animations>
                    </ajaxToolkit:UpdatePanelAnimationExtender>
                </td>
            </tr>
        </table>
    </asp:Panel>
    
    </div>
</asp:Content>
