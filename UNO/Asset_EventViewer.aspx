<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" CodeBehind="Asset_EventViewer.aspx.cs" Inherits="UNO.Asset_EventViewer" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script type="text/javascript" language="javascript">


         function onUpdating() {
             // get the update progress div
             var updateProgressDiv = $get('updateProgressDiv');
             // make it visible
             updateProgressDiv.style.display = '';

             //  get the gridview element        
             var gridView = $get('<%= this.gvEvent.ClientID %>');

             // get the bounds of both the gridview and the progress div
             var gridViewBounds = Sys.UI.DomElement.getBounds(gridView);
             var updateProgressDivBounds = Sys.UI.DomElement.getBounds(updateProgressDiv);

             //    do the math to figure out where to position the element (the center of the gridview)
             var x = gridViewBounds.x + Math.round(gridViewBounds.width / 2) - Math.round(updateProgressDivBounds.width / 2);
             var y = gridViewBounds.y + Math.round(gridViewBounds.height / 2) - Math.round(updateProgressDivBounds.height / 2);

             //    set the progress element to this position
             Sys.UI.DomElement.setLocation(updateProgressDiv, x, y);
         }

         function highLightRow() {
             debugger;
             var tr = document.getElementById('<%= this.gvEvent.ClientID %>').getElementsByTagName("tr");

             for (var i = 0; i < tr.length; i++) {


                 var column = document.getElementById('<%= this.gvEvent.ClientID %>').getElementsByTagName("tr");
                 if (tr[6].innerHTML == "Access Granted") {
                     alert('dfgg');
                     
                 }
             }

             
         }

         $(document).ready(function () {
             HighLightRowsLessThanColumnValue('<%= this.gvEvent.ClientID %>', 7, "Access Granted");
         });

         function HighLightRowsLessThanColumnValue(gridviewID, columnIndex, value) {
             debugger;
             $("#" + gridviewID + " td:nth-child(" + columnIndex + ")").each(function () {
                 debugger;
                 if ($(this).text() == value) {
                     $(this).parent("tr").css("background-color", "Green");
                 }
                 if ($(this).text() == "Access Denied") {
                     $(this).parent("tr").css("background-color", "Red");
                 }
             });
         }

         function HightlightMyTable() {
             HighLightRowsLessThanColumnValue('<%= this.gvEvent.ClientID %>', 7, "Access Granted");
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
                <asp:Label ID="lblHead" runat="server" Text="Asset Event Viewer" ForeColor="RoyalBlue" Font-Size="20px"
                    Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <div class="DivEmpDetails">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: left; width: 5%;">
                    <asp:Button ID="CmdPause" runat="server" Text="Release" CssClass="ButtonControl"
                        OnClick="CmdPause_Click" />
                </td>
                <td style="text-align: center; width: 40%; color: #003300; display: inline;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right;">
                                <span>Select Event Type :</span>
                            </td>
                            <td style="text-align: left; padding-left: 5px;">
                                <asp:RadioButtonList ID="RBLDataType" runat="server" OnSelectedIndexChanged="RBL_SelectedIndexChanged"
                                    RepeatDirection="Horizontal" AutoPostBack="True" ForeColor="#003300" Width="100%">
                                    <asp:ListItem Value="A" Selected="True">All</asp:ListItem>
                                    <asp:ListItem Value="01">Card</asp:ListItem>
                                    <asp:ListItem Value="02">Alarm</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align: right; width: 40%;">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                        OnClick="btnSearch_Click" />
                    <asp:TextBox ID="txtLevelID" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtLevelID" runat="server" TargetControlID="txtLevelID"
                        WatermarkText="Search by Name" WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    
                    <asp:TextBox ID="txtUserID" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtUserID" runat="server" TargetControlID="txtUserID"
                        WatermarkText="Search by Employee ID" WatermarkCssClass="watermark">
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
                            <asp:GridView ID="gvEvent" runat="server" AutoGenerateColumns="false" Width="100%"
                                GridLines="None" AllowPaging="true" PageSize="20" DataKeyNames="Event_ID,Event_Type"
                           OnRowDataBound="gvEvent_RowDataBound">
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
                                    <asp:BoundField DataField="Event_Type" HeaderText="Type" SortExpression="Event_Type">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Event_Datetime" HeaderText="Datetime" SortExpression="Event_Datetime">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"></asp:BoundField>
                                    <asp:BoundField DataField="Event_Employee_Code" HeaderText="Emp Code" SortExpression="Event_Employee_Code">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Event_Card_Code" HeaderText="Card Code" SortExpression="Event_Card_Code">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Controller" HeaderText="Controller" SortExpression="Controller">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Event_Alarm_Type" HeaderText="A Type" SortExpression="Event_Alarm_Type">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Event_Alarm_Action" HeaderText="A Action" SortExpression="Event_Alarm_Action">
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

    </div>
    <asp:Timer runat="server" ID="ctlTimer" Interval="3000" OnTick="OnTimerIntervalElapse" />
     
</asp:Content>
