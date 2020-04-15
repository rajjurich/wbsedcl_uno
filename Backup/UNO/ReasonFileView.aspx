<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ReasonFileView.aspx.cs" Inherits="UNO.ReasonFileView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--  <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">--%>
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css" />
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script type="text/javascript" language="javascript">


        function validateSave() {
            var ddlType = document.getElementById('<%= cmbReason_Type.ClientID %>');
            
            var txtId = document.getElementById('<%= txtReason_ID.ClientID %>');
            var txtDesc = document.getElementById('<%= txtReason_Description.ClientID %>');

            var popUp = document.getElementById('<%= mpeAddreason.ClientID %>');

            var lbl = document.getElementById('<%= lblAddmsg.ClientID %>');

            if (ddlType.selectedIndex == 0) {
                lbl.innerHTML = "Please Select Reason Type. ";
                return false;
            }
            if (txtId.value == "") {
                lbl.innerHTML = "Please Enter ID. ";
                return false;
            }
            if (txtDesc.value == "") {
                lbl.innerHTML = "Please Enter Description. ";
                return false;
            }

            lbl.innerHTML = "";
            return true;
        }



        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvReason.ClientID %>');

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

        function resetVal() {
            $('select[name^="ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolder1$cmbReason_Type"]').focus();
            document.getElementById('<%=lblAddmsg.ClientID%>').innerHTML = "";
        }

        function ResetAll() {
            $('#' + ["<%=textreasonname.ClientID%>", "<%=textreasonid.ClientID%>"].join(', #')).prop('value', "");
            document.getElementById('<%=textreasonname.ClientID%>').focus();
            document.getElementById('<%=textreasonid.ClientID%>').focus();
            document.getElementById('<%=lblMessages.ClientID%>').innerHTML = "";
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvReason.ClientID%>').focus();
           // return false;
        }

    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Reason Master" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
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
                            <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click"
                                OnClientClick="return resetVal();" />
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                                OnClick="btnDelete_Click" />
                        </td>
                        <td style="width: 50%; text-align: right;">
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                                OnClientClick="return ResetAll();" onclick="btnReset_Click" />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;"
                                CssClass="ButtonControl" OnClick="btnSearch_Click" />
                            <asp:TextBox ID="textreasonname" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtzonename" runat="server" TargetControlID="textreasonname"
                                WatermarkText="Description" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:TextBox ID="textreasonid" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twezoneid" runat="server" TargetControlID="textreasonid"
                                WatermarkText="ID" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="3">
                            <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                                <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvReason" runat="server" AutoGenerateColumns="false" Width="100%"
                                        GridLines="None" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvReason_PageIndexChanging"
                                        OnRowCommand="gvReason_RowCommand">
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
                                            <asp:TemplateField HeaderText="Select" AccessibleHeaderText="Select" SortExpression="Select"
                                                ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="DeleteRows" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" AccessibleHeaderText="Edit" SortExpression="Edit"
                                                ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit3" CommandArgument='<%# Eval("Rec_Id") %>'
                                                        ForeColor="#3366FF">Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Reason_ID" HeaderText="ID" SortExpression="Reason_ID">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Reason_Description" HeaderText="Description" SortExpression="Reason_Description">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Rsn_Type" HeaderText="Reason Type" SortExpression="Rsn_Type">
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
                <%-- <asp:PostBackTrigger ControlID="btnAdd" />--%>
                <asp:PostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="gvReason" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                <%--  <asp:PostBackTrigger ControlID="btnSubmitAdd" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnModifySave" />
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Panel ID="pnlAddReason" runat="server" CssClass="PopupPanel" Style="width: 45%">
        <asp:UpdatePanel ID="updatepanel3" runat="server">
            <ContentTemplate>
                <table id="table8" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass" width="100%">
                    <tr>
                        <td class="TDClassLabel" width="30%" align="left">
                            Reason Type :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:DropDownList ID="cmbReason_Type" runat="server" TabIndex="1" ClientIDMode="Static"
                                ValidationGroup="Add" class="chosen-select" Width="220px">
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="rfvReason_Type" runat="server" ControlToValidate="cmbReason_Type"
                                Display="None" ErrorMessage="Please select Reason Type." ForeColor="Red" InitialValue="0"
                                SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                TargetControlID="rfvReason_Type" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            Reason ID:<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" >
                            <asp:TextBox CssClass="TextControl" ID="txtReason_ID" MaxLength="15" runat="server"
                                onkeypress="return IsAlphanumericWithoutspace(event)" Style="text-transform: capitalize;"
                                Width="220px" ClientIDMode="Static" TabIndex="2" ValidationGroup="Add" onblur="this.value=this.value.toUpperCase();"
                                onkeyup="this.value=this.value.toUpperCase();"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvReasonid" runat="server" ControlToValidate="txtReason_ID"
                                Display="None" ErrorMessage="Please enter Reason Id." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                TargetControlID="rfvReasonid" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:TextBox CssClass="TextControl" ID="txtReason_Description" MaxLength="50" runat="server"
                                Width="220px" ClientIDMode="Static" TabIndex="3" ValidationGroup="Add" Style="text-transform: capitalize;"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvReason_description" runat="server" ControlToValidate="txtReason_Description"
                                ErrorMessage="Please enter Description." ForeColor="Red" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                TargetControlID="rfvReason_description" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding-top: 4%">
                            <asp:Button ID="btnSubmitAdd" runat="server" Text="Save" CssClass="ButtonControl"
                                ValidationGroup="Add" OnClick="btnSubmitAdd_Click" OnClientClick="return validateSave()" TabIndex="4" />
                            <asp:Button ID="btnCancelAdd" runat="server" Text="Cancel" CssClass="ButtonControl"
                                OnClick="btnCancelAdd_Click" TabIndex="5" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div align="center">
                                <asp:Label ID="lblAddmsg" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="tempAdd" runat="server" Style="display: none" />
    <asp:Button ID="tempCancel" runat="server" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddreason" runat="server" TargetControlID="tempAdd"
        PopupControlID="pnlAddReason" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="tempCancel">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlModifyReason" runat="server" CssClass="PopupPanel" Style="width: 45%">
        <asp:UpdatePanel ID="updatepanel4" runat="server">
            <ContentTemplate>
                <table id="table7" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                    <tr>
                        <td class="TDClassLabel" width="30%">
                            Reason Type :
                        </td>
                        <td class="TDClassControl" >
                            <asp:DropDownList ID="cmbModifyReason_Type" runat="server" CssClass="ComboControl"
                                TabIndex="3" ClientIDMode="Static" Enabled="false" Width="220px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvModifyReason_Type" runat="server" ControlToValidate="cmbModifyReason_Type"
                                Display="None" ErrorMessage="Please select Reason Type." ForeColor="Red" InitialValue="0"
                                SetFocusOnError="True" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                TargetControlID="rfvModifyReason_Type" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            Reason ID:
                        </td>
                        <td class="TDClassControl" >
                            <asp:TextBox CssClass="TextControl" ID="txtModifyReasonId" MaxLength="15" runat="server"
                                onkeypress="return IsAlphanumericWithoutspace(event)" Width="220px" ClientIDMode="Static"
                                TabIndex="1" Height="21px" Style="text-transform: capitalize;" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvModifyReason_ID" runat="server" ControlToValidate="txtModifyReasonId"
                                Display="None" ErrorMessage="Please enter Reason Id." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                TargetControlID="rfvModifyReason_ID" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" >
                            Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:TextBox CssClass="TextControl" ID="txtModifyReasonDesc" MaxLength="50" runat="server"
                                Width="220px" ClientIDMode="Static" TabIndex="2" Style="text-transform: capitalize;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvModifyReasonDesc" runat="server" ControlToValidate="txtModifyReasonDesc"
                                Display="None" ErrorMessage="Please enter Description." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                TargetControlID="rfvModifyReasonDesc" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding-top: 4%">
                            <asp:Button ID="btnModifySave" runat="server" Text="Save" CssClass="ButtonControl"
                                OnClick="btnModifySave_Click" ValidationGroup="Edit" />
                            <asp:Button ID="btnModifyCancel" runat="server" Text="Cancel" CssClass="ButtonControl"
                                OnClick="btnModifyCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div align="center">
                                <asp:Label ID="lblEditMsg" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="btnModify" runat="server" Style="display: none" CssClass="ButtonControl"
        Font-Bold="true" TabIndex="5" Text="Modify" />
    <ajaxToolkit:ModalPopupExtender ID="mpEditReason" runat="server" TargetControlID="btnModify"
        PopupControlID="pnlModifyReason" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnModifyCancel">
    </ajaxToolkit:ModalPopupExtender>
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
