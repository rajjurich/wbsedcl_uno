<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="AccessLevelBrowse.aspx.cs" Inherits="UNO.AccessLevelBrowse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script>
        function ValidatelstSReaderAdd(sender, args) {
            try {
                var list = document.getElementById("<%=RBLZoneAdd.ClientID%>"); //Client ID of the radiolist
                var inputs = list.getElementsByTagName("input");
                var selected;
                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].checked) {
                        selected = inputs[i];
                        break;
                    }
                }
                if (selected) {
                    var options = document.getElementById("<%=lstSReaderAdd.ClientID%>").options;
                    if (selected.value == "R" && options.length == 0) {
                        args.IsValid = false;
                    }
                    else {
                        args.IsValid = true;
                    }
                }
                else {
                    args.IsValid = false;
                }
            }
            catch (ex) {
                alert(ex.Message);
            }
        }

        function ValidatelstSReaderEdit(sender, args) {
            try {
                var list = document.getElementById("<%=RBLZoneEdit.ClientID%>"); //Client ID of the radiolist
                var inputs = list.getElementsByTagName("input");
                var selected;
                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].checked) {
                        selected = inputs[i];
                        break;
                    }
                }
                if (selected) {
                    var options = document.getElementById("<%=lstSReaderEdit.ClientID%>").options;
                    if (selected.value == "R" && options.length == 0) {
                        args.IsValid = false;
                    }
                    else {
                        args.IsValid = true;
                    }
                }
                else {
                    args.IsValid = false;
                }
            }
            catch (ex) {
                alert(ex.Message);
            }
        }

        function focusSearch(e) {
            var code = e.keycode;
            //alert(code);
            if (e.keyCode == 13) {
                //alert('hello'); 
                document.getElementById("<%=btnSearch.ClientID%>").click();
                alert('hello');
            }

            //document.getElementById('btnSearch').focus = true;
        }

        function doClick(buttonName, e) {
            if (e.keyCode == 13) {
                //alert('hello'); 
                document.getElementById("<%=btnSearch.ClientID%>").click();
                alert('hello');
            }
        }

        function ResetAll() {
            $('#' + ["<%=txtID.ClientID%>", "<%=txtDescription .ClientID%>"].join(', #')).prop('value', "");
            document.getElementById('<%=lblMessages.ClientID%>').innerHTML = "";
            document.getElementById('<%=txtDescription .ClientID%>').focus();
            document.getElementById('<%=txtID.ClientID%>').focus();
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvAccessLevel.ClientID%>').focus();
            return false;
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
    .style37
    {
        width: 341px;
    }
    .style38
    {
        width: 329px;
    }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Access Level View" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
            <div class="DivEmpDetails">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%; text-align: left;">
                            <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click" />
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                                OnClick="btnDelete_Click" />
                        </td>
                        <td style="width: 50%; text-align: right;">
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                                OnClientClick="return ResetAll();" />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;margin-right:3px;" CssClass="ButtonControl"
                                OnClick="btnSearch_Click" />
                            <asp:TextBox ID="txtDescription" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtDescription" runat="server" TargetControlID="txtDescription"
                                WatermarkText="Description" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:TextBox ID="txtID" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>                          
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtID" runat="server" TargetControlID="txtID"
                                WatermarkText="ID" WatermarkCssClass="watermark">
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
                                    <asp:GridView ID="gvAccessLevel" runat="server" AutoGenerateColumns="false" Width="100%"
                                        GridLines="None" AllowPaging="true" PageSize="10" DataKeyNames="AL_ID" OnRowCommand="gvAccessLevel_RowCommand"
                                        OnRowDataBound="gvAccessLevel_RowDataBound">
                                        <RowStyle CssClass="gvRow" />
                                        <HeaderStyle CssClass="gvHeader" />
                                        <AlternatingRowStyle BackColor="#F0F0F0" />
                                        <PagerStyle CssClass="gvPager" />
                                        <EmptyDataTemplate>
                                            <div>
                                                <span>No Access Level found.</span>
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" AccessibleHeaderText="Select" SortExpression="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="DeleteRows" runat="server" Onclick="CheckedFocus()" />
                                                    <asp:Label ID="lblIsAllowed" runat="server" Text='<%#Eval("IsExists") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" AccessibleHeaderText="Edit" SortExpression="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Modify" Text="Edit" ForeColor="#3366FF"
                                                        CommandArgument='<%#Eval("AL_ID")%>'></asp:LinkButton>
                                                    <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("AL_ID", "AccessLevelEdit.aspx?id={0}") %>'
                                                Text="Edit" ForeColor="#3366FF"></asp:HyperLink>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AL_ID" HeaderText="ID" SortExpression="AL_ID">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AL_DESCRIPTION" HeaderText="Description" SortExpression="AL_DESCRIPTION">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TZ_DESCRIPTION" HeaderText="Time Zone" SortExpression="TZ_DESCRIPTION">
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
            </div>
        </asp:Panel>
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
    <asp:Button ID="btnDummyAdd" runat="server" Text="Dummy" Style="display: none;" />
    <asp:Button ID="btnDymmyEdit" runat="server" Text="Dummy" Style="display: none;" />
    <asp:Panel ID="pnlAddAccessLevel" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="table2" runat="server" border="0" cellpadding="0" cellspacing="5" class="TableClass">
                    <tr>
                        <td style="text-align: right;">
                            Access Level Id :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;" class="style38">
                            <asp:TextBox CssClass="TextControl" ID="txtalidAdd" MaxLength="15" runat="server"
                                Style="text-transform: capitalize;" Width="174px" ClientIDMode="Static" ReadOnly="True"
                                ValidationGroup="Add"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="padding-left: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;" class="style38">
                            <asp:TextBox CssClass="TextControl" ID="txtdescriptionAdd" Style="text-transform: capitalize;"
                                MaxLength="20" runat="server" Width="167px" ClientIDMode="Static" TabIndex="1"
                                ValidationGroup="Add"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtdescriptionAdd" runat="server" ControlToValidate="txtdescriptionAdd"
                                ErrorMessage="Please enter description." Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtdescriptionAdd" runat="server"
                                TargetControlID="rfvtxtdescriptionAdd" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revtxtdescriptionAdd" runat="server" ControlToValidate="txtdescriptionAdd"
                                Display="None" ErrorMessage="Special characters are not allowed." ValidationExpression="[0-9a-zA-Z' ']{1,20}"
                                ValidationGroup="Add" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtdescriptionAdd" runat="server"
                                TargetControlID="revtxtdescriptionAdd" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="padding-left: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Entity Type :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;" class="style38">
                            <asp:RadioButtonList ID="RBLZoneAdd" runat="server" CssClass="ComboControl" Width="80px"
                                AutoPostBack="True" TabIndex="2" ValidationGroup="Add" OnSelectedIndexChanged="RBLZoneAdd_SelectedIndexChanged">
                                <asp:ListItem Value="Z">Zone</asp:ListItem>
                                <asp:ListItem Value="R">Reader</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvRBLZoneAdd" runat="server" ErrorMessage="Plese Select entity type."
                                ControlToValidate="RBLZoneAdd" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvRBLZoneAdd" runat="server" TargetControlID="rfvRBLZoneAdd"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList ID="cmbZoneAdd" runat="server" AutoPostBack="True" CssClass="ComboControl"
                                TabIndex="3" ValidationGroup="Add" OnSelectedIndexChanged="cmbZoneAdd_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--   <asp:RequiredFieldValidator ID="rfvcmbZoneAdd" runat="server" ControlToValidate="cmbZoneAdd"
                                Display="None" ErrorMessage="Please select zone" InitialValue="-1" SetFocusOnError="True"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvcmbZoneAdd" runat="server" TargetControlID="rfvcmbZoneAdd"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                        </td>
                        <td style="padding-left: 10px;" class="style38">
                            <asp:Label ID="lblAvailableAdd" runat="server">Available</asp:Label>
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:Label ID="lblSelectedAdd" runat="server">Selected</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblZoneAdd" runat="server">Zone:</asp:Label>
                            <asp:Label ID="lblrederadd" Visible="true" Text="Reader :" runat="server"></asp:Label>
                            &nbsp;
                        </td>
                        <td style="padding-left: 10px;" class="style38">
                            <asp:ListBox ID="lstAReaderAdd" runat="server" Height="90px" Width="280px" ForeColor="Black"
                                Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple"
                                TabIndex="4" ValidationGroup="Add"></asp:ListBox>
                        </td>
                        <td style="text-align: right;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="cmdALLRDRRight" runat="server" Text="&gt;&gt;" Width="39px" CssClass="ButtonControl"
                                            CausesValidation="False" OnClick="cmdALLALRight_Click" />
                                    </td>
                                    <tr>
                                        <td>
                                            <asp:Button ID="cmdReaderRightAdd" runat="server" Text="&gt;" Width="39px" CssClass="ButtonControl"
                                                CausesValidation="False" TabIndex="5" OnClick="cmdReaderRightAdd_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="cmdReaderLeftAdd" runat="server" Text="&lt;" Width="39px" CssClass="ButtonControl"
                                                CausesValidation="False" TabIndex="6" OnClick="cmdReaderLeftAdd_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="cmdALLRDRLeft" runat="server" Text="&lt;&lt;" Width="39px" CssClass="ButtonControl"
                                                CausesValidation="False" OnClick="cmdALLALLeft_Click" />
                                        </td>
                                    </tr>
                            </table>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:ListBox ID="lstSReaderAdd" runat="server" Height="90px" Width="280px" ForeColor="Black"
                                Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple"
                                CausesValidation="True" TabIndex="7" ValidationGroup="Add"></asp:ListBox>
                            <asp:CustomValidator ID="vclstSReaderAdd" runat="server" ControlToValidate="lstSReaderAdd"
                                Display="None" ValidateEmptyText="True" ErrorMessage="Please select reader(s)."
                                ClientValidationFunction="ValidatelstSReaderAdd" ValidationGroup="Add"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcevclstSReaderAdd" runat="server" TargetControlID="vclstSReaderAdd"
                                PopupPosition="BottomRight">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            TimeZone :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;" class="style38">
                            <asp:DropDownList ID="cmbTimeZoneAdd" runat="server" CssClass="ComboControl" Width="171px"
                                AutoPostBack="True" ValidationGroup="Add" TabIndex="2">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvcmbTimeZoneAdd" runat="server" ControlToValidate="cmbTimeZoneAdd"
                                Display="None" ErrorMessage="Please select time zone" InitialValue="-1" SetFocusOnError="True"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvcmbTimeZoneAdd" runat="server" TargetControlID="rfvcmbTimeZoneAdd"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="padding-left: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnAccessLevelAdd" runat="server" CssClass="ButtonControl" Text="Save"
                                TabIndex="9" OnClick="btnAccessLevelAdd_Click" ValidationGroup="Add" />
                            <asp:Button ID="btnAccessLevelCancel" runat="server" Text="Cancel" CssClass="ButtonControl"
                                TabIndex="10" CausesValidation="False" OnClick="btnAccessLevelCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblErrorAdd" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeAddAccessLevel" runat="server" TargetControlID="btnDummyAdd"
        PopupControlID="pnlAddAccessLevel" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnAccessLevelCancel">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlEditAccessLevel" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table id="table1" runat="server" border="0" cellpadding="0" cellspacing="5" class="TableClass">
                    <tr>
                        <td style="text-align: right;">
                            Access Level Id :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;" class="style37">
                            <asp:TextBox CssClass="TextControl" ID="txtalidEdit" MaxLength="15" runat="server"
                                Style="text-transform: capitalize;" Width="174px" ClientIDMode="Static" ReadOnly="True"
                                ValidationGroup="Edit"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtalidEdit" runat="server" ControlToValidate="txtalidEdit"
                                Display="None" ErrorMessage="Access level id is required." ForeColor="Red" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtalidEdit" runat="server" TargetControlID="rfvtxtalidEdit"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revtxtalidEdit" runat="server" ControlToValidate="txtalidEdit"
                                Display="None" ErrorMessage="Please enter 1- 15 valid characters." ValidationExpression="[0-9a-zA-Z]{1,15}"
                                ValidationGroup="Edit" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtalidEdit" runat="server" TargetControlID="revtxtalidEdit"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="padding-left: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;" class="style37">
                            <asp:TextBox CssClass="TextControl" ID="txtdescriptionEdit" Style="text-transform: capitalize;"
                                MaxLength="20" runat="server" Width="167px" ClientIDMode="Static" TabIndex="1"
                                ValidationGroup="Edit"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtdescriptionEdit" runat="server" ControlToValidate="txtdescriptionEdit"
                                ErrorMessage="Please enter description." Display="None" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtdescriptionEdit" runat="server"
                                TargetControlID="rfvtxtdescriptionEdit" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revtxtdescriptionEdit" runat="server" ControlToValidate="txtdescriptionEdit"
                                Display="None" ErrorMessage="Special characters are not allowed." ValidationExpression="[0-9a-zA-Z' ']{1,20}"
                                ValidationGroup="Edit" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtdescriptionEdit" runat="server"
                                TargetControlID="revtxtdescriptionEdit" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="padding-left: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Entity Type :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;" class="style37">
                            <asp:RadioButtonList ID="RBLZoneEdit" runat="server" CssClass="ComboControl" Width="80px"
                                AutoPostBack="True" TabIndex="2" ValidationGroup="Edit" OnSelectedIndexChanged="RBLZoneEdit_SelectedIndexChanged">
                                <asp:ListItem Value="Z">Zone</asp:ListItem>
                                <asp:ListItem Value="R">Reader</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvRBLZoneEdit" runat="server" ErrorMessage="Plese Select entity type."
                                ControlToValidate="RBLZoneEdit" Display="None" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvRBLZoneEdit" runat="server" TargetControlID="rfvRBLZoneEdit"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList ID="cmbZoneEdit" runat="server" AutoPostBack="True" CssClass="ComboControl"
                                TabIndex="3" ValidationGroup="Edit" OnSelectedIndexChanged="cmbZoneEdit_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--     <asp:RequiredFieldValidator ID="rfvcmbZoneEdit" runat="server" ControlToValidate="cmbZoneEdit"
                                Display="None" ErrorMessage="Please select zone" InitialValue="-1" SetFocusOnError="True"
                                ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvcmbZoneEdit" runat="server"
                                TargetControlID="rfvcmbZoneEdit" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                        </td>
                        <td style="padding-left: 10px;" class="style37">
                            <asp:Label ID="lblAvailableEdit" runat="server">Available</asp:Label>
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:Label ID="lblSelectedEdit" runat="server">Selected</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <%-- <td style="text-align: right;">
                            Reader :<label class="CompulsaryLabel">*</label>
                        </td>--%>
                        <td style="text-align: right;">
                            <asp:Label ID="lblZoneEdit" runat="server">Zone:</asp:Label>
                            <asp:Label ID="lblReaderEdit" Visible="true" Text="Reader :" runat="server"></asp:Label>
                            <label runat="server" id="modifylbl" class="CompulsaryLabel">
                                *</label>
                        </td>
                        <td style="padding-left: 10px;" class="style37">
                            <asp:ListBox ID="lstAReaderEdit" Visible="true" runat="server" Height="90px" Width="280px"
                                ForeColor="Black" Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static"
                                SelectionMode="Multiple" TabIndex="4" ValidationGroup="Edit"></asp:ListBox>
                        </td>
                        <td style="text-align: right;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="cmdModifyALLReaderRight" runat="server" Text="&gt;" Width="39px"
                                            CssClass="ButtonControl" CausesValidation="False" OnClick="cmdModifyALLReaderRight_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="cmdReaderRightEdit" runat="server" Text="&gt;" Width="39px" CssClass="ButtonControl"
                                            CausesValidation="False" TabIndex="5" OnClick="cmdReaderRightEdit_Click" ValidationGroup="Edit" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="cmdReaderLeftEdit" runat="server" Text="&lt;" Width="39px" CssClass="ButtonControl"
                                            CausesValidation="False" TabIndex="6" OnClick="cmdReaderLeftEdit_Click" ValidationGroup="Edit" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="cmdALLModifyReaderLeft" runat="server" Text="&lt;&lt;" Width="39px"
                                            CssClass="ButtonControl" CausesValidation="False" OnClick="cmdALLModifyReaderLeft_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:ListBox ID="lstSReaderEdit" runat="server" Height="90px" Width="280px" ForeColor="Black"
                                Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple"
                                CausesValidation="True" TabIndex="7" ValidationGroup="Edit"></asp:ListBox>
                            <asp:CustomValidator ID="cvlstSReaderEdit" runat="server" ControlToValidate="lstSReaderEdit"
                                Display="None" ValidateEmptyText="True" ErrorMessage="Please select reader(s)."
                                ClientValidationFunction="ValidatelstSReaderEdit" ValidationGroup="Edit"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcecvlstSReaderEdit" runat="server" TargetControlID="cvlstSReaderEdit"
                                PopupPosition="BottomRight">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            TimeZone :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;" class="style37">
                            <asp:DropDownList ID="cmbTimeZoneEdit" runat="server" CssClass="ComboControl" Width="171px"
                                AutoPostBack="True" TabIndex="8" ValidationGroup="Edit">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvcmbTimeZoneEdit" runat="server" ControlToValidate="cmbTimeZoneEdit"
                                Display="None" ErrorMessage="Please select time zone" InitialValue="-1" SetFocusOnError="True"
                                ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvcmbTimeZoneEdit" runat="server" TargetControlID="rfvcmbTimeZoneEdit"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="padding-left: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnAccessLevelEdit" runat="server" CssClass="ButtonControl" Text="Save"
                                TabIndex="9" ValidationGroup="Edit" OnClick="btnAccessLevelEdit_Click" />
                            <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel" CssClass="ButtonControl"
                                TabIndex="10" CausesValidation="False" OnClick="btnCancelEdit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblErrorEdit" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeEditAccessLevel" runat="server" TargetControlID="btnDymmyEdit"
        PopupControlID="pnlEditAccessLevel" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancelEdit">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
