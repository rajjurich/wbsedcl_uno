<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ControllerView.aspx.cs" Inherits="UNO.ControllerView1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <%-- <link href="Styles/default.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvController.ClientID %>');

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

        function HideControl() {

            document.getElementById('divReader').style.display = "none";
            document.getElementById('divAccess').style.display = "none";
            document.getElementById('lblReader').style.display = "none";
            document.getElementById('lblAccess').style.display = "none";          

        }

        function ShowControl() {
            document.getElementById('divReader').style.display = "block";
            document.getElementById('divAccess').style.display = "block";
            document.getElementById('lblReader').style.display = "block";
            document.getElementById('lblAccess').style.display = "block";           

        }


        function findQuote(evnt) {

            var keyASCII = (evnt.which) ? evnt.which : event.keyCode;
            var keyValue = String.fromCharCode(keyASCII);
            if (keyASCII == '39') {
                window.event.keyCode = 0;
            }
        }

        function HidePanel() {
            document.getElementById('pnlAddCtrl').style.display = "none";

        }
        function validateIP() {
            var ip = document.getElementById('txtCtlrIp').value;
            var str = ip.split('.');
            if (str[0] == 0) {
                alert('Please Enter Valid IP');
            }

        }

        function ResetAll() {

            $('#' + ["<%=txtCtlrID.ClientID%>", "<%=txtCtlrDesc.ClientID%>"].join(', #')).prop('value', "");
            document.getElementById('<%=lblMessages.ClientID%>').innerHTML = "";
            document.getElementById('<%=txtCtlrDesc.ClientID%>').focus();
            document.getElementById('<%=txtCtlrID.ClientID%>').focus();
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvController.ClientID%>').focus();

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
                <asp:Label ID="lblHead" runat="server" Text="Controller View" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
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
                            <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClientClick="HideControl()"
                                OnClick="btnAdd_Click" />
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                                OnClick="btnDelete_Click" />
                        </td>
                        <td style="width: 50%; text-align: right;">
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                                OnClientClick="return ResetAll();" />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;margin-right:3px;" CssClass="ButtonControl"
                                OnClick="btnSearch_Click" />
                            <asp:TextBox ID="txtCtlrDesc" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCtlrDesc" runat="server" TargetControlID="txtCtlrDesc"
                                WatermarkText="Description" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:TextBox ID="txtCtlrID" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCtlrID" runat="server" TargetControlID="txtCtlrID"
                                WatermarkText="Controller ID" WatermarkCssClass="watermark">
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
                                        OnPageIndexChanging="gvController_PageIndexChanging">
                                        <RowStyle CssClass="gvRow" />
                                        <HeaderStyle CssClass="gvHeader" />
                                        <AlternatingRowStyle BackColor="#F0F0F0" />
                                        <PagerStyle CssClass="gvPager" />
                                        <EmptyDataTemplate>
                                            <div>
                                                <span>No Controller found.</span>
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" SortExpression="Select" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" SortExpression="Edit" ItemStyle-Width="5%" HeaderStyle-CssClass="center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                        Text="Edit" ForeColor="#3366FF" CommandArgument='<%#Eval("CTLR_ID")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CTLR_ID" HeaderText="ID" SortExpression="ID" ItemStyle-Width="5%">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CTLR_DESCRIPTION" HeaderText="Description" ItemStyle-Wrap="true"
                                                ItemStyle-Width="10%" SortExpression="Description"></asp:BoundField>
                                            <asp:BoundField DataField="CTLR_TYPE" HeaderText="Type" ItemStyle-Width="10%" ItemStyle-Wrap="true"
                                                SortExpression="Type"></asp:BoundField>
                                            <asp:BoundField DataField="CTLR_IP" HeaderText="IP" SortExpression="IP" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="CTLR_EVENTS_STORED" HeaderText="Stored Events" ItemStyle-Width="5%"
                                                SortExpression="Stored Events" />
                                            <asp:BoundField DataField="CTLR_CURRENT_USER_CNT" HeaderText="User Count" ItemStyle-Width="5%"
                                                SortExpression="Current User Count" />
                                            <asp:TemplateField HeaderText="Controller Reinitialise">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlReinit" runat="server" ForeColor="#3366FF" CommandName="Reinit"
                                                        CommandArgument='<%#Eval("CTLR_ID")%>' Text="Reinitialise"></asp:LinkButton>
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
            </asp:Panel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="gvController" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                <asp:AsyncPostBackTrigger ControlID="ddlAPB" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlCtlrType" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmitAdd" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmitEdit" />
                <asp:AsyncPostBackTrigger ControlID="ddlAntipass" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Panel ID="pnlAddCtrl" runat="server" CssClass="PopupPanel" Style="display: block;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table id="table4" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                    class="TableClass">
                    <tr>
                        <td style="text-align: right;">
                            Controller ID :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtControllerId" runat="server" CssClass="TextControl" MaxLength="3"
                                Width="100%" TabIndex="1" ClientIDMode="Static" onkeypress="return IsNumber(event)"
                                ValidationGroup="Add"></asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please Enter Valid Value"
                                Type="Integer" Display="None" MinimumValue="2" MaximumValue="999" ControlToValidate="txtControllerId"
                                ValidationGroup="Add"></asp:RangeValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="rangeValidator" runat="server" TargetControlID="RangeValidator1"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="rfvCtlrId" runat="server" ControlToValidate="txtControllerId"
                                Display="None" ErrorMessage="Please enter controller Id." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceCtlrId" runat="server" TargetControlID="rfvCtlrId"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revCtlrid" ValidationExpression="\d+" runat="server"
                                ControlToValidate="txtControllerId" SetFocusOnError="true" Enabled="False"></asp:RegularExpressionValidator>
                        </td>
                        <td style="text-align: right;">
                            Controller Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtControllerDesc" runat="server" CssClass="TextControl" MaxLength="50"
                                Width="100%" onkeypress="findQuote(event)" TabIndex="2" ClientIDMode="Static"
                                ValidationGroup="Add"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCtlrDesc" runat="server" ControlToValidate="txtControllerDesc"
                                Display="None" ErrorMessage="Please enter Controller Description." ForeColor="Red"
                                SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceCtlrDesc" runat="server" TargetControlID="rfvCtlrDesc"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Controller Type :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList ID="ddlCtlrType" runat="server" ClientIDMode="Static" TabIndex="3"
                                Width="100%" ValidationGroup="Add" AutoPostBack="true" OnSelectedIndexChanged="ddlCtlrType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCTLRType" runat="server" ErrorMessage="Please select Controller Type."
                                ControlToValidate="ddlCtlrType" SetFocusOnError="True" Display="None" InitialValue="Select One"
                                ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceCtlrType" runat="server" TargetControlID="rfvCTLRType"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td style="text-align: right;">
                            Controller IP :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtCtlrIp" runat="server" CssClass="TextControl" MaxLength="15"
                                onblur="validateIP()" Width="100%" TabIndex="4" ClientIDMode="Static" ValidationGroup="Add"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCtlrIp" runat="server" ErrorMessage="Please enter Controller IP"
                                ControlToValidate="txtCtlrIp" SetFocusOnError="True" Display="None" ForeColor="Red"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ValidationGroup="Add" ValidationExpression="\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"
                                ID="revCtlrIp" runat="server" ForeColor="Red" ErrorMessage="Invalid Controller IP !"
                                ControlToValidate="txtCtlrIp" Display="None" SetFocusOnError="True"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcrCtlrIp" runat="server" TargetControlID="rfvCtlrIp"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceIPaddr" runat="server" TargetControlID="revCtlrIp"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Mac ID:
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtMacId" runat="server" CssClass="TextControl" MaxLength="17" TabIndex="5"
                                Width="100%" Enabled="false" Style="text-transform: capitalize;" ClientIDMode="Static"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            Incoming Port :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox CssClass="TextControl" ID="txtInPort" Text="4365" ReadOnly="true" TabIndex="6"
                                Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Firmware Version No :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox CssClass="TextControl" ID="txtFirmwareNo" MaxLength="6" runat="server"
                                Width="100%" Enabled="false" TabIndex="8"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            Outgoing Port :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox CssClass="TextControl" ID="txtOutPort" Text="4365" ReadOnly="true" TabIndex="7"
                                Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Hardware Version No :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox CssClass="TextControl" ID="txtHardwareNo" MaxLength="6" runat="server"
                                Width="100%" Enabled="false" TabIndex="9"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            Local Antipassback :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlAntipass" Width="100%"
                                TabIndex="10" OnSelectedIndexChanged="ddlAntipass_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="N">No</asp:ListItem>
                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            APB Schedule :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlAPB" Width="100%" Enabled="false"
                                TabIndex="11" AutoPostBack="True" OnSelectedIndexChanged="ddlAPB_SelectedIndexChanged">
                                <asp:ListItem Value="T" Selected="True">Timed</asp:ListItem>
                                <asp:ListItem Value="M">Midnight</asp:ListItem>
                                <asp:ListItem Value="H">Hard</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;">
                            &nbsp;Local Antipassback Time(Min) :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox CssClass="TextControl" ID="txtAPBTimed" ClientIDMode="Static" MaxLength="5"
                                Width="100%" Enabled="false" onkeypress="return IsNumber(event)" runat="server"
                                TabIndex="12"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Authentication Mode :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList ClientIDMode="Static" ID="ddlAuthMode" Width="100%" TabIndex="13"
                                runat="server" ValidationGroup="Add">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAuthMode" runat="server" ErrorMessage="Please select Authentication Mode."
                                ControlToValidate="ddlAuthMode" SetFocusOnError="True" Display="None" InitialValue="Select One"
                                ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceAuthMode" runat="server" TargetControlID="rfvAuthMode"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td style="text-align: right;">
                            Template On Card :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:CheckBox ID="chkAuthMode" runat="server" ClientIDMode="Static" TabIndex="14"
                                Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Time Attendance :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:CheckBox ID="chkTAADD" runat="server" ClientIDMode="Static" TabIndex="8" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PnlInfo" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblReader" runat="server" Text="Reader Details" ForeColor="RoyalBlue"
                                    ClientIDMode="Static" Font-Size="15px" Width="100%" CssClass="heading" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlRdr" runat="server" Visible="false">
                        <table width="100%" border="0" cellpadding="1" cellspacing="1" class="gvHeader">
                            <tr>
                                <td style="width: 5%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                                    Delete
                                </td>
                                <td style="width: 8%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                                    Reader ID
                                </td>
                                <td style="width: 20%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                                    Reader Description
                                </td>
                                <td style="width: 22%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                                    Reader Type
                                </td>
                                <td style="width: 15%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                                    Reader Mode
                                </td>
                                <td style="width: 15%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                                    Passes From
                                </td>
                                <td style="width: 20%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                                    Passes To
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div id="divReader" clientidmode="Static" style="height: 100px; overflow: auto;">
                        <table id="table5" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                            <tr>
                                <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvReader" runat="server" AutoGenerateColumns="False" Width="100%"
                                                GridLines="None" AllowPaging="false">
                                                <RowStyle CssClass="gvRow" />
                                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                                <PagerStyle CssClass="gvPager" />
                                                <Columns>
                                                    <asp:TemplateField SortExpression="Delete Reader" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkDelete" runat="server" AutoPostBack="true" OnCheckedChanged="chkDeleteChanged" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtReaderId" runat="server" Width="100%" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Reader Description" ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtReaderDesc" runat="server" Width="100%" MaxLength="25"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="22%">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlReaderType" runat="server" Width="100%" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlReaderMode" runat="server" Width="100%" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlPassesFrom" runat="server" Width="100%" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlPassesTo" runat="server" Width="100%" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--<table id="table6" runat="server"  border="0" cellpadding="0"
                    cellspacing="0" class="TableClass">
        <tr>
        <td align="right">
         <asp:Button ID="btnAddReader" runat="server" CssClass="ButtonControl" TabIndex="16" ClientIDMode="Static"
                                Text="+" Font-Bold="true"  ValidationGroup="Add" />
        </td>
        </tr>
        </table>      
        </table>--%>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblAccess" runat="server" Text="Access Point Details" ForeColor="RoyalBlue"
                                    ClientIDMode="Static" Font-Size="15px" Width="100%" CssClass="heading" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlRdr2" runat="server" Visible="true">
                        <table width="100%" border="0" cellpadding="1" cellspacing="1" class="gvHeader">
                            <tr>
                                <%--        <td style="width:10%; color :#47A3DA; font-size=20px;font-weight:bolder;background-color:#EFF8FE" >
                                AP ID
                            </td>--%>
                                <td style="width: 10%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                                    Reader ID
                                </td>
                                <td style="width: 20%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                                    Reader
                                </td>
                                <td style="width: 15%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                                    Door
                                </td>
                                <td style="width: 20%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                                    Door Lock Type
                                </td>
                                <td style="width: 17%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE;">
                                    Door Open Duration(sec)
                                </td>
                                <td style="width: 18%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE;">
                                    Door Feedback Duration(sec)
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div id="divAccess" clientidmode="Static" style="height: 100px; overflow: auto;">
                        <table id="tblAccess" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                            <tr>
                                <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvAccesspoint" runat="server" AutoGenerateColumns="false" Width="100%"
                                                GridLines="None" AllowPaging="false">
                                                <RowStyle CssClass="gvRow" />
                                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                                <Columns>
                                                    <%--<asp:TemplateField HeaderText="Delete Access" SortExpression="Delete Access" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDelete" runat="server" AutoPostBack="true"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                                    <%--  <asp:TemplateField HeaderText="Access Description">
                                    <ItemTemplate>                        
                                        <asp:TextBox ID="txtAccessdesc" runat="server" Width="50px" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                                    <asp:TemplateField ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtReaderId" runat="server" ReadOnly="true" Width="100%" Style="text-align: center"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtReaderdesc" ReadOnly="true" runat="server" Width="100%"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlDoor" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlDoor_SelectedIndexChanged" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlLocktype" runat="server" Width="100%" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="17%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOpenDur" Text="5" runat="server" MaxLength="2" Width="100%" Style="text-align: center"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="18%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFeedbackDur" Text="5" runat="server" MaxLength="2" Width="100%"
                                                                Style="text-align: center"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <%--<table id="table2" runat="server"  border="0" cellpadding="0"
                    cellspacing="0" class="TableClass">
        <tr>
        <td align="right">
         <asp:Button ID="btnAddAccesspoint" runat="server" CssClass="ButtonControl" TabIndex="16" ClientIDMode="Static"
                                Text="+" Font-Bold="true"  ValidationGroup="Add" />
        </td>
        </tr>
        </table>   --%>
                <table id="table3" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnSubmitAdd" runat="server" CssClass="ButtonControl" TabIndex="15"
                                ClientIDMode="Static" Text="Save" ValidationGroup="Add" OnClick="btnSubmitAdd_Click" />
                            <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" TabIndex="16"
                                ClientIDMode="Static" Text="Cancel" OnClick="btnCancelAdd_Click" />
                            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                <asp:Label ID="lbl1" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeAddCtrl" runat="server" TargetControlID="btnAdd"
        PopupControlID="pnlAddCtrl" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancelAdd">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlEditCtrl" runat="server" CssClass="PopupPanel" Style="display: block;">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <table id="table1" runat="server" width="100%" height="90%" border="0" cellpadding="0"
                    cellspacing="0" class="TableClass">
                    <tr>
                        <td style="text-align: right;">
                            Controller ID :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtCID1" runat="server" Enabled="false" CssClass="TextControl" ReadOnly="true"
                                ClientIDMode="Static"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            Controller Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtCDesc" runat="server" CssClass="TextControl" ClientIDMode="Static"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtCDesc" runat="server" ControlToValidate="txtCDesc"
                                Display="None" ErrorMessage="Please enter Controller Description." ForeColor="Red"
                                SetFocusOnError="True" ValidationGroup="edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                TargetControlID="rfvtxtCDesc" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Controller Type :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList ID="ddlCType" Enabled="true" runat="server" ClientIDMode="Static"
                                Width="59%" OnSelectedIndexChanged="ddlCType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;">
                            Controller IP :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtCIP1" runat="server" CssClass="TextControl" MaxLength="15" TabIndex="1"
                                ClientIDMode="Static" ValidationGroup="Edit" onblur="validateIP()"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCIP" runat="server" ErrorMessage="Please enter Controller IP"
                                ControlToValidate="txtCIP1" SetFocusOnError="True" Display="None" ForeColor="Red"
                                ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ValidationGroup="Edit" ValidationExpression="\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"
                                ID="revCIP" runat="server" ForeColor="Red" ErrorMessage="Invalid Controller IP !"
                                ControlToValidate="txtCIP1" Display="None" SetFocusOnError="True"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceCIP" runat="server" TargetControlID="rfvCIP"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceCIP1" runat="server" TargetControlID="revCIP"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Mac ID:
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtCMacId" runat="server" CssClass="TextControl" MaxLength="17"
                                Enabled="false" TabIndex="1" Style="text-transform: capitalize;" ClientIDMode="Static"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            Incoming Port :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox CssClass="TextControl" Text="4365" ReadOnly="true" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Firmware Version No :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox CssClass="TextControl" ID="txtFirmwareVersionNo" MaxLength="6" runat="server"
                                Enabled="false" TabIndex="2"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            Outgoing Port :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox CssClass="TextControl" Text="4365" ReadOnly="true" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Hardware Version No :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox CssClass="TextControl" ID="txtHardwareVersionNo" MaxLength="6" runat="server"
                                Enabled="false" TabIndex="3"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            Local Antipassback :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlCAPB" Width="66%" TabIndex="4">
                                <asp:ListItem Value="N">No</asp:ListItem>
                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            APB Schedule :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlAPBSchedule" Width="59%"
                                TabIndex="5">
                                <asp:ListItem Value="T" Selected="True">Timed</asp:ListItem>
                                <asp:ListItem Value="M">Midnight</asp:ListItem>
                                <asp:ListItem Value="H">Hard</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;">
                            Local Antipassback Time(Min) :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox CssClass="TextControl" ID="txtABPTime" ClientIDMode="Static" MaxLength="6"
                                runat="server" TabIndex="7"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Authentication Mode :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList ClientIDMode="Static" ID="ddlAuthenticationMode" Width="59%" TabIndex="7"
                                runat="server" ValidationGroup="Edit">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvMode" runat="server" ErrorMessage="Please select Authentication Mode."
                                ControlToValidate="ddlAuthenticationMode" SetFocusOnError="True" Display="None"
                                InitialValue="Select One" ForeColor="Red" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcemode" runat="server" TargetControlID="rfvMode"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td style="text-align: right;">
                            Template On Card :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:CheckBox ID="chkTempOnCard" runat="server" ClientIDMode="Static" TabIndex="8" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Time Attendance :
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:CheckBox ID="chkTA" runat="server" ClientIDMode="Static" TabIndex="8" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Reader Details" ForeColor="RoyalBlue"
                                ClientIDMode="Static" Font-Size="15px" Width="100%" CssClass="heading" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" class="gvHeader">
                    <tr>
                        <td style="width: 5%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                            Active
                        </td>
                        <td style="width: 8%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                            Reader ID
                        </td>
                        <td style="width: 20%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                            Reader Description
                        </td>
                        <td style="width: 22%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                            Reader Type
                        </td>
                        <td style="width: 15%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                            Reader Mode
                        </td>
                        <td style="width: 15%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                            Passes From
                        </td>
                        <td style="width: 20%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                            Passes To
                        </td>
                    </tr>
                </table>
                <div id="div1" clientidmode="Static" style="overflow: auto; height: 100px;">
                    <table id="table2" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                        <tr>
                            <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="6">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvEditReader" runat="server" AutoGenerateColumns="False" Width="100%"
                                            GridLines="None" AllowPaging="true">
                                            <RowStyle CssClass="gvRow" />
                                            <%-- <HeaderStyle CssClass="gvHeader" ForeColor="#47A3DA" />--%>
                                            <AlternatingRowStyle BackColor="#F0F0F0" />
                                            <PagerStyle CssClass="gvPager" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDel" runat="server" OnCheckedChanged="chkEditChanged" AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReaderId" runat="server" Width="100%" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Reader Description" ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReaderDesc" ReadOnly="false" runat="server" Width="100%" MaxLength="25"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="22%">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlReaderType" runat="server" Width="100%" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlReaderMode" runat="server" Width="100%" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlPassesFrom" runat="server" Width="100%" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlPassesTo" runat="server" Width="100%" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Access Point (AP) Details" ForeColor="RoyalBlue"
                                ClientIDMode="Static" Font-Size="15px" Width="100%" CssClass="heading" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" class="gvHeader">
                    <tr>
                        <td style="width: 10%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                            AP ID
                        </td>
                        <td style="width: 10%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                            Reader ID
                        </td>
                        <td style="width: 20%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                            Reader
                        </td>
                        <td style="width: 15%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                            Door
                        </td>
                        <td style="width: 20%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE">
                            Door Lock Type
                        </td>
                        <td style="width: 12%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE;">
                            Door Open Duration(sec)
                        </td>
                        <td style="width: 13%; color: #47A3DA; font-size=20px; font-weight: bolder; background-color: #EFF8FE;">
                            Door Feedback Duration(sec)
                        </td>
                    </tr>
                </table>
                <div id="div2" clientidmode="Static" style="overflow: auto; height: 100px;">
                    <table id="Table6" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                        <tr>
                            <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvEditAccessPoint" runat="server" AutoGenerateColumns="false" Width="100%"
                                            GridLines="None" AllowPaging="true">
                                            <RowStyle CssClass="gvRow" />
                                            <HeaderStyle CssClass="gvHeader" ForeColor="#47A3DA" />
                                            <AlternatingRowStyle BackColor="#F0F0F0" />
                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="Delete Access" SortExpression="Delete Access" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDel" runat="server" AutoPostBack="true"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                                <%--  <asp:TemplateField HeaderText="Access Description">
                                    <ItemTemplate>                        
                                        <asp:TextBox ID="txtAccessdesc" runat="server" Width="50px" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                                <asp:TemplateField ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAPID" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReaderId" runat="server" ReadOnly="true" Width="100%" Style="text-align: center"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReaderdesc" ReadOnly="true" runat="server" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlDoor" runat="server" Width="100%" OnSelectedIndexChanged="ddlDoorChanged"
                                                            AutoPostBack="true" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlLocktype" runat="server" Width="100%" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="12%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOpenDur" Text="5" runat="server" MaxLength="2" Width="100%" Style="text-align: center"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="13%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtFeedbackDur" Text="5" runat="server" MaxLength="2" Width="100%"
                                                            Style="text-align: center"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
                <table id="table7" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnSubmitEdit" runat="server" CssClass="ButtonControl" TabIndex="9"
                                ClientIDMode="Static" Text="Save" ValidationGroup="Edit" OnClick="btnSubmitEdit_Click" />
                            <asp:Button ID="btnCancelEdit" runat="server" CssClass="ButtonControl" TabIndex="10"
                                ClientIDMode="Static" Text="Cancel" OnClientClick="HidePanel()" OnClick="btnCancelEdit_Click" />
                            <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Center">
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:LinkButton ID="lnkDummyEdit" runat="server" Style="display: none;">edit</asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="mpeEditCtrl" runat="server" TargetControlID="lnkDummyEdit"
        PopupControlID="pnlEditCtrl" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancelEdit">
    </ajaxToolkit:ModalPopupExtender>
    <%-- <asp:Panel ID="ConfirmPanel" runat="server" Width="50%" CssClass="PopupPanel">
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

        </asp:Panel>--%>
    <%-- <ajaxToolkit:ModalPopupExtender ID="mpeConfirmPanel" runat="server" Enabled="True" BackgroundCssClass="modalBackground"
        TargetControlID="Button1" PopupControlID="ConfirmPanel" >
    </ajaxToolkit:ModalPopupExtender>--%>
</asp:Content>
