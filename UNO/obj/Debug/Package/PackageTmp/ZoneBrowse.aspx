<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ZoneBrowse.aspx.cs" Inherits="UNO.ZoneBrowse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function ValidateRDListBox(sender, args) {
            var options = document.getElementById("lstSReader").options;
            if (options.length == 0) {
                sender.innerHTML = "Please select reader(s).";
                args.IsValid = false;
            }
            else { args.IsValid = true; }
        }

        function ValidateModifyRDListBox(sender, args) {
            var options = document.getElementById("ModifyLstSREADER").options;
            if (options.length == 0) {
                sender.innerHTML = "Please select reader(s).";
                args.IsValid = false;
            }
            else { args.IsValid = true; }
        }

    </script>
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvZone.ClientID %>');

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

            $('#' + ["<%=textzonename.ClientID%>", "<%=textzoneid.ClientID%>"].join(', #')).prop('value', "");
            document.getElementById('<%=lblMessages.ClientID%>').innerHTML = "";
            document.getElementById('<%=textzonename.ClientID%>').focus();
            document.getElementById('<%=textzoneid.ClientID%>').focus();
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvZone.ClientID%>').focus();

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
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Zone Master" ForeColor="RoyalBlue" Font-Size="20px"
                    Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <asp:Panel ID="Panel2" runat="server" DefaultButton="btnSearch">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%; text-align: left;">
                            <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click" />
                            <asp:Button runat="server" ID="dummybtnAdd" Text="New" CssClass="ButtonControl" Style="display: none" />
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                                OnClick="btnDelete_Click" />
                            <asp:Button runat="server" ID="Button1" Text="Delete" CssClass="ButtonControl" Style="display: none"
                                OnClick="btnDelete_Click" />
                        </td>
                        <td style="width: 50%; text-align: right;">
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                                OnClientClick="return ResetAll();" />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;"
                                CssClass="ButtonControl" OnClick="btnSearch_Click" />
                            <asp:TextBox ID="textzonename" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtzonename" runat="server" TargetControlID="textzonename"
                                WatermarkText="Zone description" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:TextBox ID="textzoneid" MaxLength="10" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twezoneid" runat="server" TargetControlID="textzoneid"
                                WatermarkText="Zone ID" WatermarkCssClass="watermark">
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
                                    <asp:GridView ID="gvZone" runat="server" AutoGenerateColumns="false" Width="100%"
                                        GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="gvZone_RowCommand"
                                        OnPageIndexChanging="gvZone_PageIndexChanging" OnRowDataBound="gvZone_RowDataBound">
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
                                            <asp:TemplateField HeaderText="Select" SortExpression="Select" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="DeleteRows" runat="server"  ClientIDMode="Static"/>
                                                    <asp:HiddenField ID="hdnFlag" runat="server" Value='<%#Eval("ZONE_ID")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" AccessibleHeaderText="Edit" SortExpression="Edit"
                                                ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Modify" CommandArgument='<%# Eval("ZONE_ID") %>'
                                                        ForeColor="#3366FF">Edit</asp:LinkButton>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ZONE_ID" HeaderText="ID" SortExpression="ZONE_ID" ItemStyle-Width="10%">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ZONE_DESCRIPTION" HeaderText="Description" SortExpression="ZONE_DESCRIPTION"
                                                ItemStyle-Width="30%">
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
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Button ID="btnDummyAdd" Style="display: none" runat="server" Text="Button" />
    <asp:Button ID="btnDummyModify" Style="display: none" runat="server" Text="Button" />
    <asp:Panel ID="pnlAddZone" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="table2" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                    <tr>
                        <td style="text-align: right">
                            Zone Id :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="height: 10px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtzoneid" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                            MaxLength="10" ReadOnly="True" Width="174px"></asp:TextBox>
                                    </td>
                                    <%--style="text-transform: uppercase;" --%>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox CssClass="TextControl" ID="txtdescription" MaxLength="20" runat="server"
                                            Width="167px" ClientIDMode="Static" Style="text-transform: capitalize;" TabIndex="1"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rcvZoneDescription" runat="server" ControlToValidate="txtdescription"
                                            ErrorMessage="Please enter Description." ForeColor="Red" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                            TargetControlID="rcvZoneDescription" PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top">
                            Reader :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td style="text-align: left;">
                                        Available Readers
                                    </td>
                                    <td>
                                    </td>
                                    <td style="text-align: left;">
                                        Selected
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lstAReader" runat="server" Height="90px" Width="280px" ForeColor="Black" 
                                            Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple"
                                            TabIndex="2"></asp:ListBox>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdALLRDRRight" runat="server" Text="&gt;&gt;" Width="39px" CssClass="ButtonControl"
                                                        CausesValidation="False" OnClick="cmdALLALRight_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdReaderRight" runat="server" Text="&gt;" Width="39px" CssClass="ButtonControl"
                                                        CausesValidation="False" OnClick="cmdReaderRight_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdReaderLeft" runat="server" Text="&lt;" Width="39px" CssClass="ButtonControl"
                                                        CausesValidation="False" OnClick="cmdReaderLeft_Click" />
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
                                    <td>
                                        <asp:ListBox ID="lstSReader" runat="server" Width="280px" ForeColor="Black" Font-Names="Courier New"
                                            CssClass="TextControl" Height="90px" ClientIDMode="Static" SelectionMode="Multiple"
                                            CausesValidation="True"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                    <td>
                                        <asp:CustomValidator ID="cvLstReader" runat="server" ControlToValidate="lstSReader"
                                            ErrorMessage="Please select reader(s)." ForeColor="Red" Display="None" ValidateEmptyText="True"
                                            ClientValidationFunction="ValidateRDListBox" ValidationGroup="Add"></asp:CustomValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceLstReader" runat="server" TargetControlID="cvLstReader"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                            <table class="TableClass">
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <asp:Button ID="btnSubmitAdd" runat="server" CssClass="ButtonControl" Text="Save"
                                            ValidationGroup="Add" OnClick="btnSubmitAdd_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" Text="Cancel"
                                            OnClick="btnCancelAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%--<tr>
    

	<td class = "UAPTDClassLabel" style="height: 10px"> 
        <asp:Button ID="CmdOk" runat="server" Text="Save" CssClass="ButtonControl" 
            onclick="CmdOk_Click" />
        </td>
	<td class = "TDClassControl" style="height: 10px">
        <asp:Button ID="CmdCancel" runat="server" Text="Cancel" 
            CssClass="ButtonControl" onclick="CmdCancel_Click" 
            CausesValidation="False" />
        </td>	
	</tr>

	<tr>
	<td class = "UAPTDClassLabel" style="height: 10px" colspan="2" align="center"> 
   <div id = "messageDiv" class = "MessageClass">    

    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
 </div> 
    </td>
	
	
	</tr>--%>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdReaderRight" />
                <asp:AsyncPostBackTrigger ControlID="cmdReaderLeft" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeAddZone" runat="server" TargetControlID="dummybtnAdd"
        PopupControlID="pnlAddZone" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancelAdd">
    </ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="mpeModifyZone" runat="server" TargetControlID="btnDummyModify"
        PopupControlID="pnlModify" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnModifyCancelZone">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlModify" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="updatepnlZone" runat="server">
            <ContentTemplate>
                <table class="TableClass">
                    <tr>
                        <td style="text-align: right">
                            Zone Id :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="height: 10px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtModifyZoneID" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                            MaxLength="15" ReadOnly="True" Width="174px"></asp:TextBox>
                                    </td>
                                    <%--style="text-transform: uppercase;" --%>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox CssClass="TextControl" ID="txtModifyZoneDesc" MaxLength="20" runat="server"
                                            Width="167px" ClientIDMode="Static" Style="text-transform: capitalize;" TabIndex="1"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rcvModifyZonedesc" runat="server" ControlToValidate="txtModifyZoneDesc"
                                            ErrorMessage="Please enter Description." ForeColor="Red" Display="None" ValidationGroup="Modify"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                            TargetControlID="rcvModifyZonedesc" PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Reader :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td style="text-align: left;">
                                        Available Readers
                                    </td>
                                    <td>
                                    </td>
                                    <td style="text-align: left;">
                                        Selected
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="ModifyLstAREADER" runat="server" Height="90px" Width="100%" ForeColor="Black"
                                            Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple"
                                            TabIndex="2"></asp:ListBox>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdModifyALLReaderRight" runat="server" Text="&gt;&gt;" Width="39px"
                                                        CssClass="ButtonControl" CausesValidation="False" OnClick="cmdModifyALLReaderRight_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdModifyReaderRight" runat="server" Text="&gt;" CssClass="ButtonControl"
                                                        CausesValidation="False" OnClick="cmdModifyReaderRight_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdModifyReaderLeft" runat="server" Text="&lt;" CssClass="ButtonControl"
                                                        CausesValidation="False" OnClick="cmdModifyReaderLeft_Click" />
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
                                    <td>
                                        <asp:ListBox ID="ModifyLstSREADER" runat="server" Width="280px" ForeColor="Black"
                                            Font-Names="Courier New" CssClass="TextControl" Height="90px" ClientIDMode="Static"
                                            SelectionMode="Multiple" CausesValidation="True"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                    <td>
                                        <asp:CustomValidator ID="cvModifylstReader" runat="server" ControlToValidate="ModifyLstSREADER"
                                            ErrorMessage="Please select reader(s)." ForeColor="Red" Display="None" ValidateEmptyText="True"
                                            ClientValidationFunction="ValidateModifyRDListBox" ValidationGroup="Modify"></asp:CustomValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                            TargetControlID="cvModifylstReader" PopupPosition="BottomRight">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                            <table class="TableClass">
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <asp:Button ID="btnModifySaveZone" runat="server" CssClass="ButtonControl" Text="Save"
                                            OnClick="btnModifySaveZone_Click" ValidationGroup="Modify" />
                                        &nbsp;
                                        <asp:Button ID="btnModifyCancelZone" runat="server" CssClass="ButtonControl" Text="Cancel"
                                            OnClick="btnModifyCancelZone_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <%--   <asp:Panel ID="ConfirmPanel" runat="server" Width="50%" CssClass="PopupPanel">
            <table style="width:100% ">
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="Label4" runat="server" Text="Do you want to delete the record ? "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnOk" CssClass="ButtonControl" runat="server" Text="Yes" style="float:right" 
                            onclick="btnOk_Click"/> 
                    </td>
                    <td>
                        <asp:Button ID="btnNo" CssClass="ButtonControl" runat="server" Text="No" onclick="btnNo_Click" />
                    </td>
                </tr>
            </table>

        </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="mpeConfirmPanel" runat="server" Enabled="True" BackgroundCssClass="modalBackground"
        TargetControlID="Button1" PopupControlID="ConfirmPanel" >
    </ajaxToolkit:ModalPopupExtender>--%>
</asp:Content>
