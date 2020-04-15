<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="WorkDayviewMaster.aspx.cs" Inherits="UNO.WorkDayviewMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript">

        function ValidateData5() {
            if (handleAdd() == false) {
                return false
            }
        }

        function CheckComp() {
            var str = null;
            var value = null;
            var n = null;
            var m = null;
            document.getElementById('txtDesc').value = "";
            str = "PR" + document.getElementById('txtDesc').value;
            if (document.getElementById('ChkWO').checked) {

                value = "WC";
                if (str.match(value)) {
                }
                else {
                    str = str + value;
                }
                document.getElementById('txtDesc').value = str;
            }
            else {
                if (!document.getElementById('ChkWO').checked) {
                    value = "WC";
                    if (str.match(value)) {
                        m = str.search(value);
                        if (str.length == 2) {
                            str = "";
                        }
                        else {
                            m = str.search(value);
                            var u = str.substr(m, 2);
                            str = str.replace(u, '');
                        }
                    }
                    document.getElementById('txtDesc').value = str;
                }
            }
            if (document.getElementById('ChkPL').checked) {

                value = "PC";
                if (str.match(value)) {
                }
                else {
                    str = str + value;
                }
                document.getElementById('txtDesc').value = str;
            }
            else {
                if (!document.getElementById('ChkPL').checked) {
                    value = "PC";
                    if (str.match(value)) {
                        m = str.search(value);
                        if (str.length == 2) {
                            str = "";
                        }
                        else {
                            m = str.search(value);
                            var u = str.substr(m, 2);
                            str = str.replace(u, '');
                        }
                    }
                    document.getElementById('txtDesc').value = str;
                }
            }
            if (document.getElementById('ChkHO').checked) {

                value = "HC";
                if (str.match(value)) {
                }
                else {
                    str = str + value;
                }
                document.getElementById('txtDesc').value = str;
            }
            else {
                if (!document.getElementById('ChkHO').checked) {
                    value = "HC";
                    if (str.match(value)) {
                        m = str.search(value);
                        if (str.length == 2) {
                            str = "";
                        }
                        else {
                            m = str.search(value);
                            var u = str.substr(m, 2);
                            str = str.replace(u, '');
                        }
                    }
                    document.getElementById('txtDesc').value = str;
                }
            }

            if (document.getElementById('ChkHO').checked && document.getElementById('ChkWO').checked && document.getElementById('ChkPL').checked) {
                document.getElementById('txtDesc').value = "ALL";

            }
            if (!document.getElementById('ChkHO').checked && !document.getElementById('ChkWO').checked && !document.getElementById('ChkPL').checked) {
                document.getElementById('txtDesc').value = "PR";

            }
        }

        function HideCtrl(ctrl, timer) {
            var ctry_array = ctrl.split(",");
            var num = 0, arr_length = ctry_array.length;
            while (num < arr_length) {
                if (document.getElementById(ctry_array[num])) {
                    setTimeout('document.getElementById("' + ctry_array[num] + '").style.display = "none";', timer);
                } s
                num += 1;
            }
            return false;
        }
        function returnviewmode() {
            document.getElementById('Label1').innerHTML = "&nbsp;&nbsp;";
            window.location = "WorkDayviewMaster.aspx";
        }
        function DltConfirmationbox() {
            var result = confirm('Are you sure you want to delete selected User(s)?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }


        function clearFunction() {
            // document.getElementById(x).innerHTML = "&nbsp;&nbsp;";
            //            document.getElementById('lstFile').value = "Select One";
            document.getElementById('txtID').value = "";
            document.getElementById('txtDesc').value = "";
            document.getElementById('ChkWO').checked = false;
            document.getElementById('ChkPL').checked = false;
            document.getElementById('ChkHO').checked = false;


        }

        function handleAdd() {
            if (!Page_ClientValidate())
                return;

            var msg = confirm('Save Record?');

            if (msg == false) {
                return false;
            }
        }
        function HideLabel(labelID) {
            setTimeout("HideLabelHelper('" + labelID + "');", 3000);
        }
        function HideLabelHelper(labelID) {
            document.getElementById(labelID).style.display = "none";
        }
        function HideLabelHelper(labelID) {
            document.getElementById(labelID).style.display = "none";
        }
    </script>
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvWorkDay.ClientID %>');

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
                <asp:Label ID="lblHead" runat="server" Text="Work Day" ForeColor="RoyalBlue" Font-Size="20px"
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
                        <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click" />
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                            OnClick="btnDelete_Click" />
                    </td>
                    <td style="width: 50%; text-align: right;">
                        <asp:Button runat="server" ID="cmdReset" Text="Reset" Style="float: right; margin-left: 4px;"
                            CssClass="ButtonControl" OnClick="cmdReset_Click" />
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                            OnClick="btnSearch_Click" />
                        <asp:TextBox ID="txtLevelID" runat="server" Style="float: right;" CssClass="searchTextBox"
                            onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                        <asp:TextBox ID="txtUserID" runat="server" Style="float: right;" CssClass="searchTextBox"
                            onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtUserID" runat="server" TargetControlID="txtUserID"
                            WatermarkText="ID" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtLevelID" runat="server" TargetControlID="txtLevelID"
                            WatermarkText="Description" WatermarkCssClass="watermark">
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
                                <asp:GridView ID="gvWorkDay" runat="server" AutoGenerateColumns="false" Width="100%"
                                    GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="gvWorkDay_RowCommand">
                                    <RowStyle CssClass="gvRow" />
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                    <PagerStyle CssClass="gvPager" />
                                    <EmptyDataTemplate>
                                        <div>
                                            <span>No Work Day found.</span>
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" SortExpression="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ShowHeader="False" SortExpression="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                    Text="Edit" ForeColor="#3366FF" CommandArgument='<%#Eval("VALUE")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CODE" HeaderText="ID" SortExpression="CODE">
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VALUE" HeaderText="Description" SortExpression="VALUE">
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" />
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
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Button ID="btnDummyAddEdit" runat="server" Text="Dummy" Style="display: none;" />
    <asp:Panel ID="pnlAddEdit" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="TblInfo" runat="server" width="100%" border="0" cellpadding="0" cellspacing="5"
                    class="TableClass">
                    <tr>
                        <td class="TDClassLabel" align="right">
                            <asp:Label ID="LblID" runat="server" Font-Bold="true">Work Day ID :</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" align="right">
                            <asp:TextBox ID="txtID" Style="text-transform: uppercase" ClientIDMode="Static" runat="server"
                                Font-Bold="true" MaxLength="8" onkeypress="return IsAlphanumericWithoutspace(event)"
                                ValidationGroup="Submit"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtID" runat="server" ControlToValidate="txtID"
                                Display="None" ErrorMessage="Please enter Code." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtID" runat="server" TargetControlID="rfvtxtID"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            <asp:Label ID="LblDesc" runat="server" Font-Bold="true">Description :</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:TextBox ID="txtDesc" runat="server" CssClass="TextControl" MaxLength="8" Font-Bold="true"
                                TabIndex='1' ClientIDMode="Static" ValidationGroup="Submit"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtDesc" runat="server" ControlToValidate="txtDesc"
                                Display="None" ErrorMessage="Please enter Description." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtDesc" runat="server" TargetControlID="rfvtxtDesc"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="ChkWO" runat="server" onclick="return CheckComp()" ClientIDMode="Static"
                                            Font-Bold="true" TabIndex='2' ValidationGroup="Submit" />
                                        Weekly Off + Weekend Off
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="ChkPL" runat="server" onclick="return CheckComp()" ClientIDMode="Static"
                                            Font-Bold="true" TabIndex='3' ValidationGroup="Submit" />
                                        Paid Leave
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="ChkHO" runat="server" onclick="return CheckComp()" ClientIDMode="Static"
                                            Font-Bold="true" TabIndex='4' ValidationGroup="Submit" />
                                        Holiday
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnSubmit" Width="60px" Font-Bold="true" Text="Save" TabIndex="5"
                                OnClientClick="return ValidateData5()" runat="server" CssClass="ButtonControl"
                                ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                            <asp:Button ID="BtnCancel" Width="60px" Font-Bold="true" Text="Cancel" TabIndex="6"
                                runat="server" CssClass="ButtonControl" CausesValidation="False" OnClick="BtnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Label ID="lblError" runat="server" Text="Label" Visible="false" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeAddEdit" runat="server" TargetControlID="btnDummyAddEdit"
        PopupControlID="pnlAddEdit" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="BtnCancel">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
