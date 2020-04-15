<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="Asset_Master_Entry.aspx.cs" Inherits="UNO.Asset_Master_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/default.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="Label1" runat="server" Text="Asset Master" ForeColor="RoyalBlue" Font-Size="20px"
                    Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%; text-align: left;">
                        <asp:Button runat="server" ID="btnNew" Text="New" CssClass="ButtonControl" OnClick="btnNew_Click" />
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                            OnClick="btnDelete_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                            onclick="btnReset_Click" />
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;" CssClass="ButtonControl"
                            OnClick="btnSearch_Click" />
                        
                        <asp:TextBox ID="txtAssetDescription" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="WetxtControllerDescription" runat="server"
                            TargetControlID="txtAssetDescription" WatermarkText="Description"
                            WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:TextBox ID="txtAssetCode" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="WetxtControllerCode" runat="server" TargetControlID="txtAssetCode"
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
                                <asp:GridView ID="grdAsset" runat="server" AutoGenerateColumns="false" Width="100%"
                                    GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="grdAsset_RowCommand"
                                    OnRowDataBound="grdAsset_RowDataBound">
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
                                                <asp:CheckBox ID="DeleteRows" runat="server" />
                                                <asp:HiddenField Value='<%#Eval("IsDelete") %>' ID="hdnIsDelete" runat="server" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("Asset_Id") %>'
                                                    CommandName="Modify" ForeColor="#3366FF" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Asset_Code" HeaderText="Code" />
                                        <asp:BoundField DataField="Asset_Desc" HeaderText="Description" />
                                        <asp:BoundField DataField="Asset_Type" HeaderText="Type" />
                                        <asp:BoundField DataField="Asset_Make" HeaderText="Make" />
                                        <asp:BoundField DataField="Asset_Model" HeaderText="Model" />
                                        <asp:BoundField DataField="Asset_SrNo" HeaderText="Serial No." />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowID" runat="server" Text='<%#Eval("Asset_Id")%>' Visible="false"></asp:Label>
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
                <asp:AsyncPostBackTrigger ControlID="grdAsset" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Panel ID="pnlAddCall"  runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpPopUp" runat="server">
            <ContentTemplate>
                <div>
                    <table style="width: 100%" cellpadding="2" cellspacing="2">
                        <tr>
                            <td colspan="4" style="text-align: center">
                                <asp:Label ID="lblHead" runat="server" Text="Asset Entry" ForeColor="RoyalBlue" Font-Size="20px" Visible="false"
                                    Width="100%" CssClass="heading">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                               Asset Code: <font color="red">*</font>
                            </td>
                            <td width="28%">
                                <asp:TextBox ID="txtAstCode" runat="server" MaxLength="20" TabIndex="1" Width="100%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtAstCode" runat="server" ErrorMessage="Please Enter Asset Code"
                                    ValidationGroup="Add" ControlToValidate="txtAstCode" Display="None"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtAstCode" runat="server" TargetControlID="rfvtxtAstCode"
                                    PopupPosition="Right">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                            <td style="padding-left: 3%" width="20%">Description:<font color="red">*</font>
                            </td>
                            <td width="28%">
                                <asp:TextBox ID="txtAstDescription" runat="server" MaxLength="150" TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtAstDescription" runat="server" ErrorMessage="Please Enter Asset Description"
                                    ValidationGroup="Add" ControlToValidate="txtAstDescription" Display="None"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtAstDescription" runat="server" TargetControlID="rfvtxtAstDescription"
                                    PopupPosition="Right">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Type:<font color="red">*</font>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAstType" runat="server" TabIndex="3" Width="100%">
                                
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlAstType" runat="server" ControlToValidate="ddlAstType"
                                    Display="none" ErrorMessage="Please Select Type" ForeColor="Red" InitialValue="0"
                                    SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlAstType" runat="server" PopupPosition="Right"
                                    TargetControlID="rfvddlAstType">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                            <td style="padding-left: 3%">
                                Serial No.: <font color="red">*</font>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAstSrNo" runat="server" MaxLength="150"  TabIndex="4"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server" ControlToValidate="txtAstSrNo"
                                    Display="none" ErrorMessage="Please Enter Sr No." ForeColor="Red" InitialValue="0"
                                    SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vceSrNo" runat="server" PopupPosition="Right"
                                    TargetControlID="rfvSerialNo">
                                </ajaxToolkit:ValidatorCalloutExtender>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                Make:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAstMake" runat="server" MaxLength="150" TabIndex="5" Width="100%"></asp:TextBox>
                            </td>
                            <td style="padding-left: 3%">
                                Model:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAstModel" runat="server" MaxLength="150" TabIndex="6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center;">
                                <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="ButtonControl" ValidationGroup="Add"
                                    TabIndex="7" OnClick="btnSaveNew_Click" />
                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="false"
                                    CssClass="ButtonControl" TabIndex="8" OnClick="btnCancelNew_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center;">
                                <asp:Label ID="lblPnlNew" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddCall" runat="server" TargetControlID="Button2"
        PopupControlID="pnlAddCall" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancel">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlEdit" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <table style="width:100%;">
                        <tr>
                            <td colspan="4" >
                                <asp:Label ID="Label2" runat="server" Text="Asset Edit Entry" ForeColor="RoyalBlue" Visible="false"
                                    Font-Size="20px" Width="100%" CssClass="heading">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                Asset Code:
                            </td>
                            <td width="28%">
                                <%--<asp:Label ID="lblAssetCode" runat="server">
                                </asp:Label>--%>
                                <asp:TextBox ID="lblAssetCode" runat="server" Enabled="false" Width="100%" ></asp:TextBox>
                            </td>
                            <td style="padding-left: 5px; " width="20%">Description:<font color="red">*</font>
                            </td>
                            <td width="28%">
                                <asp:TextBox ID="txtEditDescription" runat="server" MaxLength="150" TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtEditDescription" runat="server" ErrorMessage="Please Enter Asset Description"
                                    ValidationGroup="Edit" ControlToValidate="txtEditDescription" Display="None"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtEditDescription" runat="server"
                                    TargetControlID="rfvtxtEditDescription" PopupPosition="Right">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Type:<font color="red">*</font>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEditType" runat="server" TabIndex="3" Width="100%">
                                  
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlEditType" runat="server" ControlToValidate="ddlEditType"
                                    Display="none" ErrorMessage="Please Select Type" ForeColor="Red" InitialValue="0"
                                    SetFocusOnError="True" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlEditType" runat="server" PopupPosition="Right"
                                    TargetControlID="rfvddlEditType">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                            <td style="padding-left: 5px">
                                Serial No.:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditSerialNo" runat="server" MaxLength="150" TabIndex="4"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Make:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditMake" runat="server" MaxLength="150" TabIndex="5" Width="100%"></asp:TextBox>
                            </td>
                            <td style="padding-left: 5px">
                                Model:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditModel" runat="server" MaxLength="150" TabIndex="6"></asp:TextBox>
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
                                <asp:Label ID="Label3" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeEdit" runat="server" TargetControlID="Button4"
        PopupControlID="pnlEdit" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancelEdit">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
