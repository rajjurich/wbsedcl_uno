<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="CategoryLeaveRelation.aspx.cs" Inherits="UNO.CategoryLeaveRelation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvCategoryLeave.ClientID %>');

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
                <asp:Label ID="lblHead" runat="server" Text="Catagory-Leave Relation View" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div class="DivEmpDetails">
        <table style="width: 100%;">
            <tr>
                <td style="width: 50%; text-align: left;">
                    <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" 
                        onclick="btnAdd_Click" />
                    <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl" />
                </td>
                <td style="width: 50%; text-align: right;">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl" />
                    <asp:TextBox ID="txtLeave" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtLeave" runat="server" TargetControlID="txtLeave"
                        WatermarkText="Search by Leave" WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    <asp:TextBox ID="txtEntity" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtEntity" runat="server" TargetControlID="txtEntity"
                        WatermarkText="Search by Entity" WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                    <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                        <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvCategoryLeave" runat="server" AutoGenerateColumns="false" Width="100%"
                                GridLines="None" AllowPaging="true" PageSize="10">
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
                                    <asp:TemplateField HeaderText="Select" SortExpression="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDelete" runat="server" Width="100px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" SortExpression="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Width="100px" ForeColor="#3366FF">Edit</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ELR_REC_ID" HeaderText="ID" SortExpression="ID" ItemStyle-Width="70px" />
                                    <asp:BoundField DataField="ELR_LEAVE_ID" HeaderText="Leave" SortExpression="Leave"
                                        ItemStyle-Width="200px" />
                                    <asp:BoundField DataField="ELR_ENTITY_ID" HeaderText="Entity" SortExpression="Entity"
                                        ItemStyle-Width="200px" />
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
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
                        TargetControlID="UpdatePanel2">
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
            <div>
                <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
