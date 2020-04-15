<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="WeeklyOffView.aspx.cs" Inherits="UNO.WeeklyOffView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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


        function HideWeeksList() {
            document.getElementById('rdoUserDefSelction').style.display = "none";
        }


        function ShowWeeksList() {
            document.getElementById('rdoUserDefSelction').style.display = "inline";

        }
        function HideWeeksListEdit() {
            document.getElementById('rdoUserDefSelctionEdit').style.display = "none";
        }


        function ShowWeeksListEdit() {
            document.getElementById('rdoUserDefSelctionEdit').style.display = "inline";

        }

        function ValidateSave(txtWeekOffCode, lblAddError, rdoSelectionWay, rdoUserDefSelction) {

            if (document.getElementById(txtWeekOffCode).value == '') {
                document.getElementById(lblAddError).innerHTML = "Please enter Week Off Code.";
                return false;
            }
            else if ($('#' + rdoSelectionWay + ' input:checked').val() == "3") {
                if ($('#' + rdoUserDefSelction + ' input:checked').val() == null) {
                    document.getElementById(lblAddError).innerHTML = "Please Select atlease one value";
                    return false;
                }


            }
            document.getElementById(lblAddError).innerHTML = "";
            return true;
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
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Weekend / Weekly Off" ForeColor="RoyalBlue"
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
                            OnClick="btnDelete_Click" />
                    </td>
                    <td style="width: 50%; text-align: right;">
                        <asp:Button runat="server" ID="cmdReset" Text="Reset" Style="float: right;" CssClass="ButtonControl"
                            OnClick="cmdReset_Click" />
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;"
                            CssClass="ButtonControl" OnClick="btnSearch_Click" />
                        <asp:TextBox ID="textreasonname" runat="server" Style="float: right;" Visible="false"
                            CssClass="searchTextBox" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtzonename" runat="server" TargetControlID="textreasonname"
                            WatermarkText="Description" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:TextBox ID="textreasonid" runat="server" Style="float: right;" CssClass="searchTextBox"
                            onkeydown="return (event.keyCode!=13);"></asp:TextBox>
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
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit3" CommandArgument='<%# Eval("MWK_CD") %>'
                                                    ForeColor="#3366FF">Edit</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MWK_CD" HeaderText="ID" SortExpression="MWK_CD">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MWK_DAY" HeaderText="WEEK DAY" SortExpression="MWK_DAY">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MWK_OFF" HeaderText="WEEKEND /WEEK OFF" SortExpression="MWK_OFF">
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
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAdd" />
                <asp:PostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="gvReason" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                <asp:PostBackTrigger ControlID="btnSubmitAdd" />
                <asp:AsyncPostBackTrigger ControlID="btnModifySave" />
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Panel ID="pnlAddReason" runat="server" CssClass="PopupPanel" Style="width: 55%">
        <asp:UpdatePanel ID="updatepanel3" runat="server">
            <ContentTemplate>
                <table id="table8" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                    <tr>
                        <td class="TDClassLabel">
                            Week Off Code :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:TextBox CssClass="TextControl" ID="txtWeekOffCode" MaxLength="6" runat="server"
                                Width="167px" ClientIDMode="Static" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                        </td>
                        <td class="TDClassControl">
                            <asp:RadioButtonList ID="rdoWOOrWEND" runat="server" CssClass="radiobutton" ClientIDMode="Static"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" Selected="True">Week Off</asp:ListItem>
                                <asp:ListItem Value="1" style="padding-left: 17px;">Week End</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            Day :
                        </td>
                        <td class="TDClassControl">
                            <asp:DropDownList ID="ddlDay" runat="server" Width="173px" TabIndex="3" ClientIDMode="Static">
                                <asp:ListItem Value="1" Text="Sunday"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Monday"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Tuesday"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Wednesday"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Thursday"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Friday"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Saturday"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                        </td>
                        <td class="TDClassControl">
                            <asp:RadioButtonList ID="rdoSelectionWay" runat="server" CssClass="radiobutton" ClientIDMode="Static"
                                RepeatDirection="Horizontal" CellPadding="3" CellSpacing="3">
                                <asp:ListItem Value="0" Selected="True" onclick="HideWeeksList()">All</asp:ListItem>
                                <asp:ListItem Value="1" onclick="HideWeeksList()" style="padding-left: 8px;">Even</asp:ListItem>
                                <asp:ListItem Value="2" onclick="HideWeeksList()" style="padding-left: 8px;">Odd</asp:ListItem>
                                <asp:ListItem Value="3" onclick="ShowWeeksList()" style="padding-left: 8px;">User Defined</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                        </td>
                        <td class="TDClassControl">
                            <asp:CheckBoxList ID="rdoUserDefSelction" runat="server" CssClass="radiobutton" ClientIDMode="Static"
                                RepeatDirection="Vertical">
                                <asp:ListItem Value="1">1st Week </asp:ListItem>
                                <asp:ListItem Value="2">2nd Week</asp:ListItem>
                                <asp:ListItem Value="3">3rd Week</asp:ListItem>
                                <asp:ListItem Value="4">4th Week</asp:ListItem>
                                <asp:ListItem Value="5">5th Week</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding-top: 4%">
                            <asp:Button ID="btnSubmitAdd" runat="server" Text="Save" CssClass="ButtonControl"
                                OnClick="btnSubmitAdd_Click" />
                            <asp:Button ID="btnCancelAdd" runat="server" Text="Cancel" CssClass="ButtonControl"
                                OnClick="btnCancelAdd_Click" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Label ID="lblAddError" runat="server" CssClass="ErrorLabel" Text=""></asp:Label>
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
    <asp:Panel ID="pnlModifyReason" runat="server" CssClass="PopupPanel" Style="width: 55%">
        <asp:UpdatePanel ID="updatepanel4" runat="server">
            <ContentTemplate>
                <table id="table7" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                    <%--   <tr>
                        <td class="TDClassLabel">
                            Reason ID:<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:TextBox CssClass="TextControl" ID="txtModifyReasonId" MaxLength="15" runat="server"
                                onkeypress="return IsAlphanumericWithoutspace(event)" Width="174px" ClientIDMode="Static"
                                TabIndex="1"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvModifyReason_ID" runat="server" ControlToValidate="txtModifyReasonId"
                                Display="None" ErrorMessage="Please enter Reason Id." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                TargetControlID="rfvModifyReason_ID" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:TextBox CssClass="TextControl" ID="txtModifyReasonDesc" MaxLength="50" runat="server"
                                Width="167px" ClientIDMode="Static" TabIndex="2"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvModifyReasonDesc" runat="server" ControlToValidate="txtModifyReasonDesc"
                                Display="None" ErrorMessage="Please enter Description." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                TargetControlID="rfvModifyReasonDesc" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            Reason Type :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:DropDownList ID="cmbModifyReason_Type" runat="server" CssClass="ComboControl"
                                TabIndex="3" ClientIDMode="Static">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvModifyReason_Type" runat="server" ControlToValidate="cmbModifyReason_Type"
                                Display="None" ErrorMessage="Please select Reason Type." ForeColor="Red" InitialValue="0"
                                SetFocusOnError="True" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                TargetControlID="rfvModifyReason_Type" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>

                    --%>
                    <tr>
                        <td class="TDClassLabel">
                            Week Off Code :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:TextBox CssClass="TextControl" ID="txtWeekOffCodeEdit" MaxLength="6" runat="server"
                                Width="167px" ClientIDMode="Static" TabIndex="2" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtWeekOffCodeEdit" runat="server" ControlToValidate="txtWeekOffCodeEdit"
                                Display="None" ErrorMessage="Please enter Week Off Code." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="WeekOffGroupEdit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                TargetControlID="rfvtxtWeekOffCodeEdit" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr style="height: 1%;">
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                        </td>
                        <td class="TDClassControl">
                            <asp:RadioButtonList ID="rdoWOOrWENDEdit" runat="server" CssClass="radiobutton" ClientIDMode="Static"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Week Off</asp:ListItem>
                                <asp:ListItem Value="1" style="padding-left: 17px;">Week End</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr style="height: 1%;">
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            Day :
                        </td>
                        <td class="TDClassControl">
                            <asp:DropDownList ID="ddlDayEdit" runat="server" CssClass="ComboControl" TabIndex="3"
                                ClientIDMode="Static" ValidationGroup="Add" Width="173px">
                                <asp:ListItem Value="1" Text="Sunday"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Monday"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Tuesday"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Wednesday"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Thursday"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Friday"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Saturday"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 1%;">
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                        </td>
                        <td class="TDClassControl">
                            <asp:RadioButtonList ID="rdoSelectionWayEdit" runat="server" CssClass="radiobutton"
                                ClientIDMode="Static" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" onclick="HideWeeksListEdit()">All</asp:ListItem>
                                <asp:ListItem Value="1" onclick="HideWeeksListEdit()" style="padding-left: 8px;">Even</asp:ListItem>
                                <asp:ListItem Value="2" onclick="HideWeeksListEdit()" style="padding-left: 8px;">Odd</asp:ListItem>
                                <asp:ListItem Value="3" onclick="ShowWeeksListEdit()" style="padding-left: 8px;">User Defined</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr style="height: 1%;">
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                        </td>
                        <td class="TDClassControl">
                            <asp:CheckBoxList ID="rdoUserDefSelctionEdit" runat="server" CssClass="radiobutton"
                                ClientIDMode="Static" RepeatDirection="Vertical">
                                <asp:ListItem Value="1">1st Week </asp:ListItem>
                                <asp:ListItem Value="2">2nd Week</asp:ListItem>
                                <asp:ListItem Value="3">3rd Week</asp:ListItem>
                                <asp:ListItem Value="4">4th Week</asp:ListItem>
                                <asp:ListItem Value="5">5th Week</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr style="height: 1%;">
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding-top: 4%">
                            <asp:Button ID="btnModifySave" runat="server" Text="Save" CssClass="ButtonControl"
                                OnClick="btnModifySave_Click" />
                            <asp:Button ID="btnModifyCancel" runat="server" Text="Cancel" CssClass="ButtonControl"
                                OnClick="btnModifyCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Label ID="lblEditError" runat="server" CssClass="ErrorLabel" Text=""></asp:Label>
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
