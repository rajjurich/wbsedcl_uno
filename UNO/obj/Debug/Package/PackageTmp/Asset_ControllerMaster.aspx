<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="Asset_ControllerMaster.aspx.cs" Inherits="UNO.Asset_ControllerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <script type="text/javascript">
   
      function IsNumber(evt) 
         {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57))
                  {
                    alert("Allow Only Numbers");
                    return false;
                  }
          }--%>
        }
    </script>

    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                        <asp:Label ID="lblHead" runat="server" Text="Asset Controller Master" ForeColor="RoyalBlue"
                            Font-Size="20px" Width="100%" CssClass="heading">
                        </asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <center>
                <div class="DivEmpDetails">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%; text-align: left;">
                                <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click" />
                                <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                                    ValidationGroup="Search" OnClick="btnDelete_Click" OnClientClick="return confirm('Record(s) marked for Deletion. Continue? ');" />
                            </td>
                            <td style="width: 50%; text-align: right;">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                                    OnClick="btnReset_Click" />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;"
                                    CssClass="ButtonControl" OnClick="btnSearch_Click" />
                                <asp:TextBox ID="txtControllerDescription" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="WetxtControllerDescription" runat="server"
                                    TargetControlID="txtControllerDescription" WatermarkText="Description" WatermarkCssClass="watermark">
                                </ajaxToolkit:TextBoxWatermarkExtender>
                                <asp:TextBox ID="txtControllerCode" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="WetxtControllerCode" runat="server" TargetControlID="txtControllerCode"
                                    WatermarkText="Code " WatermarkCssClass="watermark">
                                </ajaxToolkit:TextBoxWatermarkExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                                <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                                    <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvController" runat="server" AutoGenerateColumns="false" Width="100%"
                                            GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="gvController_RowCommand"
                                            OnRowDataBound="gvController_RowDataBound">
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
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="DeleteRows" runat="server" />
                                                        <asp:HiddenField Value='<%#Eval("IsDelete") %>' ID="hdnIsDelete" runat="server" Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkButtonId" runat="server" CommandArgument='<%#Eval("CtlrID") %>'
                                                            CommandName="Modify" ForeColor="#3366FF" Text="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CtlrCode" HeaderText="Controller Code" />
                                                <asp:BoundField DataField="CtlrDesc" HeaderText="Controller Description" />
                                                <asp:BoundField DataField="CtlrIP" HeaderText="Controller IP" />
                                                <asp:BoundField DataField="LocationID" HeaderText="Location" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowID" runat="server" Text='<%#Eval("CtlrID")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
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
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div>
                            <asp:Label ID="lblMessages" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                        <asp:AsyncPostBackTrigger ControlID="gvController" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                </asp:UpdatePanel>
            </center>
            <asp:Panel ID="pnlAddCall" runat="server" CssClass="PopupPanel">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%" cellpadding="2" cellspacing="2">
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Size="Large" Visible="false"
                                        Text="New Asset Controller Master"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Controller Code: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNewControllerCode" runat="server" MaxLength="5" onkeypress="return IsNumber(event)"
                                        TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtNewControllerCode" runat="server" ValidationGroup="Add"
                                        ControlToValidate="txtNewControllerCode" ErrorMessage="Please Enter Controller Code"
                                        Display="None"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revtxtNewControllerCode" runat="server" ValidationGroup="Add"
                                        ControlToValidate="txtNewControllerCode" ErrorMessage="Please enter only number!"
                                        ValidationExpression="^[0-9]*$" Display="None"></asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtNewControllerCode" runat="server"
                                        TargetControlID="rfvtxtNewControllerCode" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="revvcetxtNewControllerCode" runat="server"
                                        TargetControlID="revtxtNewControllerCode" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td style="padding-left: 3%">
                                    Controller Description:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNewControllerDescription" runat="server" TabIndex="2" MaxLength="30"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Controller IP: <font color="red">*</font>&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNewControllerIP" runat="server" MaxLength="15" TabIndex="3"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtNewControllerIP" runat="server" ErrorMessage="Please Enter Controller IP"
                                        ValidationGroup="Add" ControlToValidate="txtNewControllerIP" Display="None"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtNewControllerIP" runat="server" TargetControlID="rfvtxtNewControllerIP"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ValidationExpression="\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"
                                        ID="revtxtNewControllerIP" runat="server" Display="None" ValidationGroup="Add"
                                        ErrorMessage="Invalid IP" ControlToValidate="txtNewControllerIP"></asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtNewControllerIP" runat="server"
                                        TargetControlID="revtxtNewControllerIP" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td style="padding-left: 3%">
                                    Location ID: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="ComboControl" Width="100%"
                                        TabIndex="4">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddLocation" runat="server" ControlToValidate="ddlLocation"
                                        Display="none" ErrorMessage="Please Select Location" ForeColor="Red" InitialValue="Select Location"
                                        SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddLocation" runat="server" PopupPosition="Right"
                                        TargetControlID="rfvddLocation">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Button ID="btnSaveNew" runat="server" CssClass="ButtonControl" Text="Save" ValidationGroup="Add"
                                        TabIndex="5" OnClick="btnSaveNew_Click" />
                                    <asp:Button ID="btnCancelNew" runat="server" CausesValidation="false" CssClass="ButtonControl"
                                        TabIndex="6" Text="Cancel" OnClick="btnCancelNew_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Label ID="lblPnlNew" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSaveNew" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="mpeAddCall" runat="server" TargetControlID="Button2"
                PopupControlID="pnlAddCall" BackgroundCssClass="modalBackground" Enabled="true"
                CancelControlID="btnCancelNew">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlEdit" runat="server" CssClass="PopupPanel">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%" cellpadding="2" cellspacing="2">
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" Visible="false"
                                        Text="Edit Asset Controller Master"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Controller Code: <font color="red">*</font>
                                </td>
                                <td>
                                    <%--<asp:Label ID="lblEditControllerCode" runat="server"></asp:Label>--%>
                                    <asp:TextBox ID="lblEditControllerCode" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="padding-left: 3%">
                                    Controller Description:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEditControllerDescription" runat="server" TabIndex="1" MaxLength="30"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Controller IP: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEditControllerIP" runat="server" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtEditControllerIP" runat="server" ErrorMessage="Please Enter Controller IP"
                                        ValidationGroup="Edit" ControlToValidate="txtEditControllerIP" Display="None"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtEditControllerIP" runat="server"
                                        TargetControlID="rfvtxtEditControllerIP" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ValidationExpression="\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"
                                        ID="revtxtEditControllerIP" runat="server" Display="None" ValidationGroup="Edit"
                                        ErrorMessage="Invalid IP" ControlToValidate="txtEditControllerIP"></asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtEditControllerIP" runat="server"
                                        TargetControlID="revtxtEditControllerIP" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td style="padding-left: 3%">
                                    Location ID: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEditLocation" runat="server" CssClass="ComboControl" Width="100%"
                                        TabIndex="3">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlEditLocation" runat="server" ControlToValidate="ddlEditLocation"
                                        Display="none" ErrorMessage="Please Select Location" ForeColor="Red" InitialValue="Select Location"
                                        SetFocusOnError="True" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vceddlEditLocation" runat="server" PopupPosition="Right"
                                        TargetControlID="rfvddlEditLocation">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Button ID="btnSaveEdit" runat="server" CssClass="ButtonControl" Text="Save"
                                        ValidationGroup="Edit" OnClick="btnSaveEdit_Click" />
                                    <asp:Button ID="btnCancelEdit" runat="server" CausesValidation="false" CssClass="ButtonControl"
                                        Text="Cancel" OnClick="btnCancelEdit_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Label ID="Label2" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSaveNew" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="mpeEdit" runat="server" TargetControlID="Button4"
                PopupControlID="pnlEdit" BackgroundCssClass="modalBackground" Enabled="true"
                CancelControlID="btnCancelEdit">
            </ajaxToolkit:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
