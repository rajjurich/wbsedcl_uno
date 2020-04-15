<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="VEH_Controller.aspx.cs" Inherits="UNO.VEH_Controller" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .cssVEh
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
            <asp:Label ID="lblHead" runat="server" Text="Controller" ForeColor="RoyalBlue" Font-Size="20px"
                Width="100%" CssClass="heading">
            </asp:Label>
        </asp:TableCell>
    </asp:TableRow>
    <div class="DivEmpDetails">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: left; width: 30%;">
                    <asp:Button ID="btnNew" runat="server" Text="New" CssClass="ButtonControl" OnClick="btnNew_Click" />
                    <asp:Button ID="btndel" runat="server" Text="Delete" CssClass="ButtonControl" ValidationGroup="Search"
                        OnClick="btndel_Click" />
                </td>
                <td style="width: 65%; color: #003300; text-align: center;">
                    <asp:TextBox ID="txtControllerName" runat="server" Style="float: right; text-align: center;"
                        onkeydown="return (event.keyCode!=13);" CssClass="searchTextBox" Width="162px"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server"
                        TargetControlID="txtControllerName" WatermarkText="Search by Controller Description "
                        WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    <asp:TextBox ID="txtControllerIDSerch" runat="server" Style="float: right; text-align: center;"
                        onkeydown="return (event.keyCode!=13);" CssClass="searchTextBox" Width="162px"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server"
                        TargetControlID="txtControllerIDSerch" WatermarkText="Search by Controller ID "
                        WatermarkCssClass="watermark">
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
                            <asp:GridView ID="gvController" runat="server" AutoGenerateColumns="false" Width="100%"
                                GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="gvController_RowCommand">
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
                                <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                            <%--       <asp:LinkButton ID="lnkDelete" runat="server" ForeColor="#3366FF" CommandName="Remove"
                                                CommandArgument='<%#Eval("ID")%>'>Delete</asp:LinkButton>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkModify" runat="server" ForeColor="#3366FF" CommandName="Modify"
                                                CommandArgument='<%#Eval("ID")%>'>Edit</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" ForeColor="#3366FF" CommandName="Remove"
                                                CommandArgument='<%#Eval("ID")%>'>Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Controller Status">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnlStatus" runat="server" ForeColor="#3366FF" CommandName="ChangeStatus"
                                                CommandArgument='<%#Eval("ID")%>' Text='<%#Eval("ControllerStatus")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="controllerID" HeaderText="Controller ID" />
                                    <asp:BoundField DataField="controllerIp" HeaderText="Controller IP" />
                                    <asp:BoundField DataField="location" HeaderText="Location" />
                                    <%--<asp:BoundField DataField="controllerDescription" HeaderText="Controller Description" />--%>
                                    <asp:TemplateField HeaderText="Complaint Description" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" Width="11%" />
                                        <ItemTemplate>
                                              <div style="width: 180px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                        <%# Eval("controllerDescription")%>
                                                    </div>
                                            <%--<div style="width: 180px; overflow: hidden; text-overflow: ellipsis; white-space: normal;
                                                white-space: nowrap;">
                                                <asp:Label ID="lbl_edit_remark" ToolTip='<%# Eval("controllerDescription")%>' runat="server"><%# Eval("controllerDescription")%></asp:Label>
                                            </div>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Controller Reinitialise">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnlReinit" runat="server" ForeColor="#3366FF" CommandName="Reinit"
                                                CommandArgument='<%#Eval("ID")%>' Text="Reinitialise"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                        </Animations>top: 3172px;
                    </ajaxToolkit:UpdatePanelAnimationExtender>--%>
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanelmsg" runat="server">
        <ContentTemplate>
            <div style="width: 100%; height: 2%;" align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblMsg" runat="server" Style="color: Red;"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlAddController" runat="server" Width="57%" Height="312px" Style="background-color: White;
        border: 5px solid #000000; border-radius: 25px; color: Black;">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <div style="width: 69%; background-color: White; position: absolute; border-radius: 10px;
                    left: 12%; height: 265px;">
                    <table style="width: 100%;">
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
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Controller Status"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:DropDownList ID="ddlControllerStatusAdd" runat="server" Height="20px" Width="149px">
                                    <asp:ListItem Value="Enabled">Enabled</asp:ListItem>
                                    <asp:ListItem Value="Disabled">Disabled</asp:ListItem>
                                </asp:DropDownList>
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
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Controller IP"></asp:Label>
                            </td>
                            <td>
                                <span style="color: Red; font-size: medium;">*</span>
                                <asp:TextBox ID="txtAddControllerIp" ValidationGroup="add" MaxLength="15" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Controller IP here"
                                    ControlToValidate="txtAddControllerIp" Display="None" ValidationGroup="add"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                    TargetControlID="RequiredFieldValidator1" PopupPosition="Right">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtAddControllerIp"
                                    Display="None" ValidationGroup="add" ValidationExpression="^(([01]?[0-9]?[0-9]|2([0-4][0-9]|5[0-5]))\.){3}([01]?[0-9]?[0-9]|2([0-4][0-9]|5[0-5]))$"
                                    runat="server" ErrorMessage="Please Enter Valid IP Address"></asp:RegularExpressionValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                    TargetControlID="RegularExpressionValidator1" PopupPosition="Right">
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
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Controller Location"></asp:Label>
                            </td>
                            <td>
                                <span style="color: Red; font-size: medium;">*</span>
                                <asp:TextBox ID="txtAddControllerLoc" ValidationGroup="add" MaxLength="15" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Controller Location here"
                                    ControlToValidate="txtAddControllerLoc" Display="None" ValidationGroup="add"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                    TargetControlID="RequiredFieldValidator2" PopupPosition="Right">
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
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Controller ID"></asp:Label>
                            </td>
                            <td>
                                <span style="color: Red; font-size: medium;">*</span>
                                <asp:TextBox ID="txtAddControllerID" MaxLength="18" runat="server" ValidationGroup="add"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Controller ID here"
                                    ControlToValidate="txtAddControllerID" Display="None" ValidationGroup="add"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                    TargetControlID="RequiredFieldValidator3" PopupPosition="Right">
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
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Controller Description"></asp:Label>
                            </td>
                            <td colspan="2">
                                &nbsp;
                                <asp:TextBox ID="txtControllerDescriptionAdd" MaxLength="150" runat="server" Height="43px"
                                    Width="262px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
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
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                                <asp:Button ID="btnAddController" runat="server" CssClass="ButtonControl" ValidationGroup="add"
                                    Text="Save" OnClick="btnAddController_Click" />
                                <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" Text="Cancel"
                                    OnClick="btnCancelAdd_Click" />
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
                                <asp:Label ID="lbl_Msg_Add" runat="server" Style="color: Red;"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNew" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelAdd" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="tempbtn" runat="server" Text="Dummy" Style="display: none;" />
    <asp:Button ID="tempbtnCancel" runat="server" Text="Dummy" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddController" runat="server" BackgroundCssClass="cssVEh"
        Enabled="true" PopupControlID="pnlAddController" TargetControlID="tempbtn">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlEditController" runat="server" Width="57%" Height="312px" Style="background-color: White;
        border: 5px solid #000000; border-radius: 25px; color: Black;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div style="width: 69%; background-color: White; position: absolute; border-radius: 10px;
                    left: 12%; height: 265px;">
                    <table style="width: 100%;">
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
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Controller Status"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:DropDownList ID="ddlEditStatus" runat="server" Height="20px" Width="149px">
                                    <asp:ListItem Value="Enabled">Enabled</asp:ListItem>
                                    <asp:ListItem Value="Disabled">Disabled</asp:ListItem>
                                </asp:DropDownList>
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
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="Controller IP"></asp:Label>
                            </td>
                            <td>
                                <span style="color: Red; font-size: medium;">*</span>
                                <asp:TextBox ID="txtEdiControllerIp" ValidationGroup="edit" MaxLength="15" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Controller IP Here"
                                    ControlToValidate="txtEdiControllerIp" Display="None" ValidationGroup="edit"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                    TargetControlID="RequiredFieldValidator5" PopupPosition="Right">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtEdiControllerIp"
                                    Display="None" ValidationGroup="edit" ValidationExpression="^(([01]?[0-9]?[0-9]|2([0-4][0-9]|5[0-5]))\.){3}([01]?[0-9]?[0-9]|2([0-4][0-9]|5[0-5]))$"
                                    runat="server" ErrorMessage="Please Enter Valid IP Address"></asp:RegularExpressionValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                    TargetControlID="RegularExpressionValidator2" PopupPosition="Right">
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
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="Controller Location"></asp:Label>
                            </td>
                            <td>
                                <span style="color: Red; font-size: medium;">*</span>
                                <asp:TextBox ID="txtEditControllerLoc" ValidationGroup="edit" MaxLength="15" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter Controller Location Here"
                                    ControlToValidate="txtEditControllerLoc" Display="None" ValidationGroup="edit"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                    TargetControlID="RequiredFieldValidator6" PopupPosition="Right">
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
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label16" runat="server" Text="Controller ID"></asp:Label>
                            </td>
                            <td>
                                <span style="color: Red; font-size: medium;">*</span><asp:TextBox ID="txtEditControllerID"
                                    MaxLength="18" runat="server" ValidationGroup="edit"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Enter Controller ID Here"
                                    ControlToValidate="txtEditControllerID" Display="None" ValidationGroup="edit"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                    TargetControlID="RequiredFieldValidator7" PopupPosition="Right">
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
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label18" runat="server" Text="Controller Description"></asp:Label>
                            </td>
                            <td colspan="2">
                                &nbsp;
                                <asp:TextBox ID="txtEditControllerDescrption" MaxLength="150" runat="server" Height="43px"
                                    Width="262px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
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
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                                <asp:Button ID="btnUpdate" runat="server" CssClass="ButtonControl" ValidationGroup="edit"
                                    Text="Save" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnUpdateCancel" runat="server" CssClass="ButtonControl" Text="Cancel"
                                    OnClick="btnUpdateCancel_Click" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnUpdateCancel" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="btntest" runat="server" Text="Dummy" Style="display: none;" />
    <asp:Button ID="btnD" runat="server" Text="Dummy" Style="display: none;" />
    <asp:Button ID="btndummyEdit" runat="server" Text="Dummy" Style="display: none;" />
    <asp:Button ID="btndummyEdit2" runat="server" Text="Dummy" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="mpeEditController" runat="server" BackgroundCssClass="cssVEh"
        Enabled="true" PopupControlID="pnlEditController" TargetControlID="btndummyEdit">
    </ajaxToolkit:ModalPopupExtender>
    <%--      <asp:Panel ID="pnlEnabled" runat="server" 
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
                <asp:Button ID="btnOK" runat="server" Text="OK" CssClass="ButtonControl" onclick="btnOK_Click" 
                       />

       
                </td>
            </tr>
        </table>
        </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnOK" />
        </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
         <asp:Button ID="btnDummy6" runat="server" Style="display: none;" Text="test" />     
    <ajaxToolkit:ModalPopupExtender ID="mpeNonChekedMsg" runat="server" Enabled="True" BackgroundCssClass="modalBackground"
        TargetControlID="btnDummy6" PopupControlID="pnlNonChekedEntry" 
        >
    </ajaxToolkit:ModalPopupExtender>--%>
    <asp:Panel ID="pnlDeleteController" runat="server" Style="background-color: White;
        border: 5px solid #000000; border-radius: 25px; color: Black;" Width="30%" Height="20%">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <table width="100%" style="margin-top: 15%;">
                    <tr>
                        <td style="text-align: center;">
                            <asp:Label ID="Label9" runat="server" Text="Do you want delete this record"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnDelete" runat="server" Text="Yes" CssClass="ButtonControl" OnClick="btnDelete_Click" />
                            <asp:Button ID="btnDelCancel" runat="server" Text="No" CssClass="ButtonControl" OnClick="btnDelCancel_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="btnDelCancel" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button5" runat="server" Style="display: none;" Text="test" />
    <asp:Button ID="Button6" runat="server" Text="Yes" Style="display: none;" />
    <asp:Button ID="Button7" runat="server" Text="No" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="mpeDelController" runat="server" Enabled="True"
        BackgroundCssClass="cssVEh" TargetControlID="Button5" PopupControlID="pnlDeleteController"
        OkControlID="Button7">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
