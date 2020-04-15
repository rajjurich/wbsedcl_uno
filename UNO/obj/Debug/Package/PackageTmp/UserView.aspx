<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="UserView.aspx.cs" Inherits="UNO.UserView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">--%>
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvUsers.ClientID %>');

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

        function ResetAll() {

            $('#' + ["<%=txtLevelID.ClientID%>", "<%=txtUserID.ClientID%>"].join(', #')).prop('value', "");
            $('#' + ["<%=txtLevelID.ClientID%>"]).focus();
            $('#' + ["<%=txtUserID.ClientID%>"]).focus();
            document.getElementById('<%=lblMessages.ClientID%>').innerHTML = "";
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvUsers.ClientID%>').focus();
            //return false;
        }

    </script>
    <!--[if IE]>
<style>
    .DivEmpDetails {
                     text-align: center;
            width: 95%;
            border: 1px solid #333333;
            border-radius: 15px;
            margin: 10px 10px 10px 10px;
            padding: 10px 10px 10px 10px;
            background-color:#53AEF3;
            font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;
    }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnMain" runat="server">
        <ContentTemplate>
            <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                        <asp:Label ID="lblHead" runat="server" Text="User View" ForeColor="RoyalBlue" Font-Size="20px"
                            Width="100%" CssClass="heading">
                        </asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <center>
                <div class="DivEmpDetails">
                    <asp:Panel ID="Panel3" runat="server" DefaultButton="btnSearch">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 50%; text-align: left;">
                                    <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click" />
                                    <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                                        OnClick="btnDelete_Click" />
                                </td>
                                <td style="width: 50%; text-align: right;">
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                                        OnClientClick="return ResetAll();" onclick="btnReset_Click" />
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;"
                                        CssClass="ButtonControl" OnClick="btnSearch_Click" />
                                    <asp:TextBox ID="txtLevelID" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtLevelID" runat="server" TargetControlID="txtLevelID"
                                        WatermarkText="Level Name" WatermarkCssClass="watermark">
                                    </ajaxToolkit:TextBoxWatermarkExtender>
                                    <asp:TextBox ID="txtUserID" MaxLength="12" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtUserID" runat="server" TargetControlID="txtUserID"
                                        WatermarkText="User ID" WatermarkCssClass="watermark">
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
                                            <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" Width="100%"
                                                GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="gvUsers_RowCommand"
                                                OnRowDataBound="gvUsers_RowDataBound">
                                                <RowStyle CssClass="gvRow" />
                                                <HeaderStyle CssClass="gvHeader" />
                                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                                <PagerStyle CssClass="gvPager" />
                                                <EmptyDataTemplate>
                                                    <div>
                                                        <span>No Users found.</span>
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select" SortExpression="Select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" />
                                                            <asp:HiddenField ID="hdnLevelCode" runat="server" Value='<%#Eval("LevelCode")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ShowHeader="False" SortExpression="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                                Text="Edit" ForeColor="#3366FF" CommandArgument='<%#Eval("UserID")%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="UserID" HeaderText="User ID" SortExpression="User ID">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LevelName" HeaderText="Level Name" SortExpression="Level Name">
                                                        <ItemStyle HorizontalAlign="Center" />
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
                                           <%-- <asp:PostBackTrigger ControlID="btnAdd" />
                                            <asp:PostBackTrigger ControlID="btnDelete" />--%>
                                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                                          <%--  <asp:PostBackTrigger ControlID="btnSubmitAdd" />--%>
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
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div>
                            <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </center>
            <asp:Button ID="btnDummyEdit" runat="server" Text="dummy" Style="display: none;" />
            <asp:Panel ID="pnlAddUser" runat="server" CssClass="PopupPanel">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table id="table2" runat="server" border="0" cellpadding="0" cellspacing="5" class="TableClass">
                            <tr>
                                <td style="text-align: right;">
                                    User ID :<label class="CompulsaryLabel">*</label>
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox CssClass="TextControl" ID="txtUserNameAdd" onkeypress="return IsAlphanumeric(event)"
                                        ClientIDMode="Static" MaxLength="15" runat="server" TabIndex="1" ValidationGroup="Add"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtUserNameAdd" runat="server" ErrorMessage="Please enter User Name"
                                        ControlToValidate="txtUserNameAdd" SetFocusOnError="True" Display="None" ForeColor="Red"
                                        ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtUserNameAdd" runat="server" TargetControlID="rfvtxtUserNameAdd"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    Password :<label class="CompulsaryLabel">*</label>
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox CssClass="TextControl" ID="txtPasswordAdd" ClientIDMode="Static" MaxLength="8"
                                        runat="server" OnKeyPress="return IsAlphanumeric(event)" TabIndex="2" TextMode="Password"
                                        ValidationGroup="Add"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtPasswordAdd" runat="server" ControlToValidate="txtPasswordAdd"
                                        Display="None" ErrorMessage="Please enter password" ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtPasswordAdd" runat="server" TargetControlID="rfvtxtPasswordAdd"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    Confirm Password :<label class="CompulsaryLabel">*</label>
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtConfirmPasswordAdd" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                        MaxLength="8" OnKeyPress="return IsAlphanumeric(event)" TabIndex="2" TextMode="Password"
                                        ValidationGroup="Add"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtConfirmPasswordAdd" runat="server" ControlToValidate="txtConfirmPasswordAdd"
                                        Display="None" ErrorMessage="Please enter confirm password" ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtConfirmPasswordAdd" runat="server"
                                        TargetControlID="rfvtxtConfirmPasswordAdd" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CompareValidator ID="covtxtConfirmPasswordAdd" runat="server" ErrorMessage="Confirm Password Does not Match"
                                        ControlToValidate="txtConfirmPasswordAdd" ControlToCompare="txtPasswordAdd" Type="String"
                                        Display="None" Operator="Equal" ValidationGroup="Add"></asp:CompareValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcecovtxtConfirmPasswordAdd" runat="server"
                                        TargetControlID="covtxtConfirmPasswordAdd" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    Employee ID :
                                </td>
                                <td style="padding-left: 10px;">
                                    <%--   <asp:TextBox ID="txtEmpCodeAdd" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="10" OnKeyPress="return IsAlphanumeric(event)" TabIndex="2" ValidationGroup="Add"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlEmpCodeAdd" runat="server" class="chosen-select" Width="173px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    Level :<label class="CompulsaryLabel">*</label>
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:DropDownList ID="ddlLevelTypeAdd" runat="server" ClientIDMode="Static" Width="175px"
                                        ValidationGroup="Add" class="chosen-select">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlLevelTypeAdd" runat="server" ControlToValidate="ddlLevelTypeAdd"
                                        Display="None" ErrorMessage="Please select level" ForeColor="Red" InitialValue="0"
                                        SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlLevelTypeAdd" runat="server" TargetControlID="rfvddlLevelTypeAdd"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <asp:Button ID="btnSubmitAdd" runat="server" Text="Save" CssClass="ButtonControl"
                                        ValidationGroup="Add" OnClick="btnSubmitAdd_Click" />
                                    <asp:Button ID="btnCancelAdd" runat="server" Text="Cancel" CssClass="ButtonControl"
                                        OnClick="btnCancelAdd_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <asp:Label ID="lblErrorAdd" runat="server" Text="Label" Visible="false" CssClass="ErrorLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Button ID="btnadddummy" runat="server" Style="display: none;"></asp:Button>
            <ajaxToolkit:ModalPopupExtender ID="mpeAddUser" runat="server" TargetControlID="btnadddummy"
                PopupControlID="pnlAddUser" BackgroundCssClass="modalBackground" Enabled="true"
                CancelControlID="btnCancelAdd">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlEditUser" runat="server" CssClass="PopupPanel">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <table id="table1" runat="server" border="0" cellpadding="0" cellspacing="5" class="TableClass">
                            <tr>
                                <td style="text-align: right;">
                                    User ID :<label class="CompulsaryLabel">*</label>
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox CssClass="TextControl" ID="txtUserNameEdit" onkeypress="return IsAlphanumeric(event)"
                                        ClientIDMode="Static" MaxLength="15" runat="server" TabIndex="1" ValidationGroup="Edit"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtUserNameEdit" runat="server" ErrorMessage="Please enter User Name"
                                        ControlToValidate="txtUserNameEdit" SetFocusOnError="True" Display="None" ForeColor="Red"
                                        ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtUserNameEdit" runat="server" TargetControlID="rfvtxtUserNameEdit"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    Password :<label class="CompulsaryLabel">*</label>
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox CssClass="TextControl" ID="txtPasswordEdit" ClientIDMode="Static" MaxLength="8"
                                        runat="server" OnKeyPress="return IsAlphanumeric(event)" TabIndex="2" 
                                        ValidationGroup="Edit"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtPasswordEdit" runat="server" ControlToValidate="txtPasswordEdit"
                                        Display="None" ErrorMessage="Please enter password" ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtPasswordEdit" runat="server" TargetControlID="rfvtxtPasswordEdit"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    Confirm Password :<label class="CompulsaryLabel">*</label>
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtConfirmPasswordEdit" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                        MaxLength="8" OnKeyPress="return IsAlphanumeric(event)" TabIndex="2" 
                                        ValidationGroup="Edit"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtConfirmPasswordEdit" runat="server" ControlToValidate="txtConfirmPasswordEdit"
                                        Display="None" ErrorMessage="Please enter confirm password" ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtConfirmPasswordEdit" runat="server"
                                        TargetControlID="rfvtxtConfirmPasswordEdit" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CompareValidator ID="covtxtConfirmPasswordEdit" runat="server" ErrorMessage="Confirm Password Does not Match"
                                        ControlToValidate="txtConfirmPasswordEdit" ControlToCompare="txtPasswordEdit"
                                        Type="String" Display="None" Operator="Equal" ValidationGroup="Edit"></asp:CompareValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcecovtxtConfirmPasswordEdit" runat="server"
                                        TargetControlID="covtxtConfirmPasswordEdit" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    Employee ID :
                                </td>
                                <td style="padding-left: 10px;">
                                    <%--<asp:TextBox ID="txtEmpCodeEdit" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="10" OnKeyPress="return IsAlphanumeric(event)" TabIndex="2" Width="133px"
                                ValidationGroup="Edit"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlEmployeeIDEdit" runat="server" class="chosen-select" Width="173px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    Level :<label class="CompulsaryLabel">*</label>
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:DropDownList ID="ddlLevelTypeEdit" runat="server" CssClass="ComboControl" ClientIDMode="Static"
                                        ValidationGroup="Edit" class="chosen-select">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlLevelTypeEdit" runat="server" ControlToValidate="ddlLevelTypeEdit"
                                        Display="None" ErrorMessage="Please select level" ForeColor="Red" InitialValue="0"
                                        SetFocusOnError="True" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlLevelTypeEdit" runat="server"
                                        TargetControlID="rfvddlLevelTypeEdit" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <asp:Button ID="btnSaveEdit" runat="server" Text="Save" CssClass="ButtonControl"
                                        ValidationGroup="Edit" OnClick="btnSaveEdit_Click" />
                                    <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel" CssClass="ButtonControl"
                                        OnClick="btnCancelEdit_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <asp:Label ID="lblErrorEdit" runat="server" Text="Label" Visible="false" CssClass="ErrorLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="mpeEditUser" runat="server" TargetControlID="btnDummyEdit"
                PopupControlID="pnlEditUser" BackgroundCssClass="modalBackground" Enabled="true"
                CancelControlID="btnCancelEdit">
            </ajaxToolkit:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="Scripts/Choosen/chosen.jquery.js" type="text/javascript"></script>
    <script src="Scripts/Choosen/docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "95%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
    </script>
</asp:Content>
