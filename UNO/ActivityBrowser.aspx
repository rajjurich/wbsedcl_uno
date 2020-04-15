<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ActivityBrowser.aspx.cs" Inherits="UNO.ActivityBrowser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvActivity.ClientID %>');

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
                <asp:Label ID="lblHead" runat="server" Text="Activity Browser" ForeColor="RoyalBlue" Font-Size="20px"
                    Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div class="DivEmpDetails">
    <asp:Panel ID="Panel2" runat="server" DefaultButton="btnSearch">
     
        <table style="width: 100%;">
            <tr>
                <td style="text-align: left; width: 5%;">
                 <asp:Button ID="CmdPause" runat="server" Text="Pause" CssClass="ButtonControl" OnClick="CmdPause_Click"
                   />
                </td>
                <td style="text-align: center; width: 40%; color: #003300; display: inline;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right;">
                                <span>Select Controller :</span>
                            </td>
                            <td style="text-align: left; padding-left: 5px;">
                                <asp:DropDownList ID="cmbContrller" runat="server" CssClass="ComboControl" AutoPostBack="True"
                                    OnSelectedIndexChanged="cmbContrller_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align: right; width: 40%;">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                        OnClick="btnSearch_Click" />
                    
                    <asp:TextBox ID="txtLevelID" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtLevelID" runat="server" TargetControlID="txtLevelID"
                        WatermarkText="Search by  Event Description" WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>

                    <asp:TextBox ID="txtUserID" runat="server" MaxLength="12" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtUserID" runat="server" TargetControlID="txtUserID"
                        WatermarkText="Search by Controller" WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>

                </td>
            </tr>
            <tr>
                <td style="background-color: #EFF8FE; padding: 0px;" colspan="3">
                    <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                        <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvActivity" runat="server" AutoGenerateColumns="false" Width="100%"
                                GridLines="None" AllowPaging="true" PageSize="10" 
                               OnRowDataBound="gvActivity_RowDataBound">
                                <RowStyle CssClass="gvRow" />
                                <HeaderStyle CssClass="gvHeader" />
                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                <PagerStyle CssClass="gvPager" />
                                <EmptyDataTemplate>
                                    <div>
                                        <span>No Record found.</span>
                                    </div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="DATETIME" HeaderText="Date Time" SortExpression="DATETIME">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CTLR_DESCRIPTION" HeaderText="Controller" SortExpression="CTLR_DESCRIPTION">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EVENT_DESC" HeaderText="Event Description" SortExpression="EVENT_DESC">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="STATUS" HeaderText="Status" SortExpression="STATUS"></asp:BoundField>
                                    <asp:BoundField DataField="RETRY" HeaderText="Retry Count" SortExpression="RETRY">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="USER_ID" HeaderText="User" SortExpression="USER_ID"></asp:BoundField>
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
                                <SortedAscendingHeaderStyle ForeColor="White" />
                            </asp:GridView>
                        </ContentTemplate>
                         <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ctlTimer" EventName="Tick" />
        </Triggers>
                    </asp:UpdatePanel>
                    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
                        TargetControlID="UpdatePanel1">
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
        </asp:Panel>


            <asp:Timer runat="server" ID="ctlTimer" Interval="3000" OnTick="OnTimerIntervalElapse" />
    </div>
</asp:Content>
