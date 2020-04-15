<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="VEH_AdminOperations.aspx.cs" Inherits="UNO.VEH_AdminOperations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
  .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TableRow ID="TableRow1" runat="server">
        <asp:TableCell HorizontalAlign="Left" CssClass="CompulsaryLabel">
            <asp:Label ID="lblHead" runat="server" Text="To Be Enabled" ForeColor="RoyalBlue"
                Font-Size="20px" Width="100%" CssClass="heading">
            </asp:Label>
        </asp:TableCell>
    </asp:TableRow>
    <div class="DivEmpDetails">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: left; width: 5%;">
                    <asp:Button ID="btnEnabled" runat="server" Text="Enabled" 
                        CssClass="ButtonControl" onclick="btnEnabled_Click" 
                         />
                </td>
                <td style="width: 90%; color: #003300; text-align: center;">
                 <asp:TextBox ID="txtRFIDEnabled" runat="server" Style="float: right;text-align:center;" onkeydown = "return (event.keyCode!=13);" CssClass="searchTextBox" Width="162px"></asp:TextBox>
                     <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtRFIDEnabled"
                        WatermarkText="Search by RFID " WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>

                     <asp:TextBox ID="txtemployeeIDEnabled" runat="server" Style="float: right;text-align:center;" onkeydown = "return (event.keyCode!=13);"
                        CssClass="searchTextBox"  Width="162px"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtemployeeIDEnabled"
                        WatermarkText="Search by Employee ID " WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>

                     <asp:TextBox ID="txtVisitorIDEnabled" runat="server" Style="float: right;text-align:center;" onkeydown = "return (event.keyCode!=13);"
                        CssClass="searchTextBox"  Width="162px"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtVisitorIDEnabled"
                        WatermarkText="Search by visitorID " WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>

                    <asp:TextBox ID="txtVehregNoEnabled" runat="server" 
                        Style="float: right; margin-left: 55;text-align:center;" CssClass="searchTextBox" onkeydown = "return (event.keyCode!=13);"
                       Width="162px"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtVehregNoEnabled"
                        WatermarkText="Search by vehicle Registration No." WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
              
                </td>
                <td style="text-align: right; width: 5%;">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                        OnClick="btnSearch_Click" />
              </td>
            </tr>
            <tr>
                <td style="background-color: #EFF8FE; padding: 0px;" colspan="3">
                    <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                        <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvEnabled" runat="server" AutoGenerateColumns="false"  Width="100%"
                                GridLines="None" AllowPaging="true" PageSize="5" 
                                >
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
                                     <asp:TemplateField HeaderText="Enabled">
                                            <ItemTemplate>
                                                    <asp:CheckBox ID="chkEnabled" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                    <asp:BoundField DataField="EmployeeName_VistorName" HeaderText="Employee/Visitor" />
                                    <asp:BoundField DataField="userType" HeaderText="User Type" />
                                    <asp:BoundField DataField="entityId" HeaderText="Entity ID" />
                                    <asp:BoundField DataField="rfId" HeaderText="RFID" />
                                    <asp:BoundField DataField="vehicleName" HeaderText="Vehicle Name" />
                                    <asp:BoundField DataField="VehicleRegistrationNumber" HeaderText="Vehicle Registration Number" />
                                    <asp:BoundField DataField="rfidValidityDate" HeaderText="RFID Validity Date" />
                                
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
                    </asp:UpdatePanel>
                    <%-- <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
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
                    </ajaxToolkit:UpdatePanelAnimationExtender>--%>
                </td>
            </tr>
        </table>
    </div>
        <asp:TableRow ID="TableRow2" runat="server" HorizontalAlign="Center">
        <asp:TableCell HorizontalAlign="Left"  CssClass="CompulsaryLabel">
            <asp:Label ID="Label1" runat="server" Text="To Be Disabled" ForeColor="RoyalBlue"
                Font-Size="20px" Width="100%" CssClass="heading">
            </asp:Label>
        </asp:TableCell>
    </asp:TableRow>
    <div class="DivEmpDetails">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: left; width: 5%;">
                    <asp:Button ID="btnDisabled" runat="server" Text="Disabled" 
                        CssClass="ButtonControl" onclick="btnDisabled_Click" />
                </td>
                <td style="width: 0%; color: #003300; text-align: left;">
                    <%--<asp:Button ID="btnDel" runat="server" Text="Delete" CssClass="ButtonControl" />--%>
                </td>
                <td style="text-align: right; width: 80%;">
                    <asp:Button ID="btnSearchDisbled" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                        OnClick="btnSearchDisbled_Click" />
                    <asp:TextBox ID="txtRFIDDisabled" runat="server" Style="float: right;text-align:center;" CssClass="searchTextBox" Width="162px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox>
                     <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtRFIDDisabled"
                        WatermarkText="Search by RFID " WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>

                    <asp:TextBox ID="txtemployeeIDDisabled" runat="server" Style="float: right;text-align:center;" onkeydown = "return (event.keyCode!=13);"
                        CssClass="searchTextBox"  Width="162px"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtemployeeIDDisabled"
                        WatermarkText="Search by Employee ID " WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>

                     <asp:TextBox ID="txtVisitorIDDisabled" runat="server" Style="float: right;text-align:center;" onkeydown = "return (event.keyCode!=13);"
                        CssClass="searchTextBox"  Width="162px"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtVisitorIDDisabled"
                        WatermarkText="Search by visitorID " WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    
                    <asp:TextBox ID="txtVehregNoDisabled" runat="server" onkeydown = "return (event.keyCode!=13);"
                        Style="float: right; margin-left: 55;text-align:center;" CssClass="searchTextBox" 
                       Width="162px"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtVehregNoDisabled"
                        WatermarkText="Search by vehicle Registration No. " WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
                <td style="background-color: #EFF8FE; padding: 0px;" colspan="3">
                    <div id="Div1" style="display: none; height: 40px; width: 40px">
                        <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvDisabled" runat="server" AutoGenerateColumns="false"  Width="100%"
                                GridLines="None" AllowPaging="true" PageSize="5">
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
                                     <asp:TemplateField HeaderText="Disabled">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkDisabled" runat="server"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                  
                                   <asp:BoundField DataField="EmployeeName_VistorName" HeaderText="Employee/Visitor" />
                                    <asp:BoundField DataField="userType" HeaderText="User Type" />
                                    <asp:BoundField DataField="entityId" HeaderText="Entity ID" />
                                    <asp:BoundField DataField="rfId" HeaderText="RFID" />
                                    <asp:BoundField DataField="vehicleName" HeaderText="Vehicle Name" />
                                    <asp:BoundField DataField="VehicleRegistrationNumber" HeaderText="Vehicle Registration Number" />
                                    <asp:BoundField DataField="rfidValidityDate" HeaderText="RFID Validity Date" />
                                </Columns>
                                <PagerTemplate>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: left; width: 15%;">
                                                <span>Go To : </span>
                                                <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChangePageDisbled">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="gvPreviousDisbled"
                                                    CssClass="ButtonControl" />
                                                <asp:Label ID="lblShowing" runat="server" Text="Showing "></asp:Label>
                                                <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="gvNextDisbled" CssClass="ButtonControl" />
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
                    </asp:UpdatePanel>
                    <%-- <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
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
                    </ajaxToolkit:UpdatePanelAnimationExtender>--%>
                </td>
            </tr>
        </table>
    </div>
        <asp:Panel ID="pnlEnabled" runat="server" 
        Style="background-color: White;border: 5px solid #000000; border-radius: 25px; color: Black;" 
        Width="30%" Height="20%">
      <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
        <table width="100%" style="margin-top:15%;">
        <tr>
        <td style="text-align:center;"><asp:Label ID="Label19" runat="server" Text="Do you want to Enable This Record"></asp:Label> </td>
        </tr>
         <tr>
                <td align="center" colspan="2">
                <asp:Button ID="btnEnabledPnl" runat="server" Text="Yes" CssClass="ButtonControl" onclick="btnEnabledPnl_Click"/>
          <asp:Button ID="btn_cl" runat="server" Text="No" CssClass="ButtonControl" onclick="btn_cl_Click"/>
       
                </td>
            </tr>
        </table>
        </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnEnabled" />
        </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
         <asp:Button ID="btn_sub" runat="server" Style="display: none;" Text="test" />  
                 <asp:Button ID="Button1" runat="server" Style="display: none;" Text="test" />  
                <asp:Button ID="btndummy7" runat="server" Style="display: none;" Text="test" /> 
    <ajaxToolkit:ModalPopupExtender ID="mpeEnabled" runat="server" Enabled="True" 
        TargetControlID="btndummy7" PopupControlID="pnlEnabled" BackgroundCssClass="modalBackground" CancelControlID="Button1"
       >
    </ajaxToolkit:ModalPopupExtender>
     <asp:Panel ID="pnlDisabled" runat="server" 
        Style="background-color: White;border: 5px solid #000000; border-radius: 25px; color: Black;" 
        Width="30%" Height="20%">
      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
        <table width="100%" style="margin-top:15%;">
        <tr>
        <td style="text-align:center;"><asp:Label ID="Label2" runat="server" Text="Do you want Disable This Record"></asp:Label> </td>
        </tr>
         <tr>
                <td align="center" colspan="2">
                <asp:Button ID="btnDisabledPnl" runat="server" Text="Yes" CssClass="ButtonControl" onclick="btnDisabledPnl_Click"
                       />
          <asp:Button ID="btnCancel" runat="server" Text="No" CssClass="ButtonControl" 
                        onclick="btnCancel_Click"/>
       
                </td>
            </tr>
        </table>
        </ContentTemplate>
         <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnDisabled" />
        </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
         <asp:Button ID="btntemp" runat="server" Style="display: none;" Text="test" />     
            <asp:Button ID="Button3" runat="server" Style="display: none;" Text="test" />  
    <ajaxToolkit:ModalPopupExtender ID="mpeDisabled" runat="server" Enabled="True" BackgroundCssClass="modalBackground"
        TargetControlID="Button3" PopupControlID="pnlDisabled" CancelControlID="btntemp"
        >
    </ajaxToolkit:ModalPopupExtender>

         <asp:Panel ID="pnlNonChekedEntry" runat="server" 
        Style="background-color: White;border: 5px solid #000000; border-radius: 25px; color: Black;" 
        Width="30%" Height="20%">
      <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
        <table width="100%" style="margin-top:15%;">
        <tr>
        <td style="text-align:center;"><asp:Label ID="Label3" runat="server" Text="Please select records first"></asp:Label> </td>
        </tr>
         <tr>
                <td align="center" colspan="2">
                <asp:Button ID="Button2" runat="server" Text="OK" CssClass="ButtonControl" onclick="Button2_Click"
                       />

                </td>
            </tr>
        </table>
        </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Button2" />
        </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
         <asp:Button ID="btnDummy6" runat="server" Style="display: none;" Text="test" />     
    <ajaxToolkit:ModalPopupExtender ID="mpeNonChekedMsg" runat="server" Enabled="True" BackgroundCssClass="modalBackground"
        TargetControlID="btnDummy6" PopupControlID="pnlNonChekedEntry" 
        >
    </ajaxToolkit:ModalPopupExtender>
      
</asp:Content>
