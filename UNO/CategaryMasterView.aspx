<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="CategaryMasterView.aspx.cs" Inherits="UNO.CategaryMasterView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">--%>
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script>


        function CheckLength_ID(evnt) {

            var varCatDesc = document.getElementById('<%= textCategory.ClientID %>').value;
            if (varCatDesc.length > 20) {
                FilterType = "ID";
                document.getElementById('<%=lblMessages.ClientID%>').innerHTML = "Limit Exceeded ";
                MTimer();
                return false;
            }

        }

        function fnCheck() {
            if (document.getElementById("Erl_Alwd").value == "00:00") {
                Erl_Alwd == "";
            }
        }

        function IsAlphanumericN(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) || (charCode >= 48 && charCode <= 57) ||
			(charCode >= 97 && charCode <= 122) || (charCode == 8) || (charCode == 32)) {
                return true
            }
            else {
                return false
            }
        }

        function NotAll() {

            var read = document.getElementById('ChkAllowed').removeAttribute("readonly", 0);


        }


        function CheckExtraHours(source, args) {
            if (document.getElementById('ChkAllowed').checked == true) {


                if (document.getElementById('Bef_Shft').value.length <= 0) {

                    args.IsValid = false;

                }

                else {

                    args.IsValid = true;

                }


            }

            return;


        }

        function ChkF() {
            if (document.getElementById('ChkAllowed').checked == true) {
                document.getElementById('Aft_shft').disabled = false;
                document.getElementById('Bef_Shft').disabled = false;

                document.getElementById('ErlD').disabled = false;
                document.getElementById('LtD').disabled = false;


            }
            else {
                document.getElementById('Aft_shft').disabled = true;
                document.getElementById('Bef_Shft').disabled = true;
                document.getElementById('Aft_shft').value = "";
                document.getElementById('Bef_Shft').value = "";
                document.getElementById('ErlD').checked = false;
                document.getElementById('LtD').checked = false;

                document.getElementById('ErlD').disabled = true;
                document.getElementById('LtD').disabled = true;
            }
        }

        function CheckExtraHoursaft(source, args) {
            if (document.getElementById('ChkAllowed').checked == true) {


                if (document.getElementById('Aft_shft').value.length <= 0) {

                    args.IsValid = false;

                }

                else {

                    args.IsValid = true;

                }
            }

            return;
        }
        function Clear() {
            document.getElementById('Cat_id').value = "";
            document.getElementById('Cat_desc').value = "";
            document.getElementById('Erl_Alwd').value = "";
            document.getElementById('Lt_Alwd').value = "";
            document.getElementById('Label3').value = "";
            document.getElementById('ChkAllowed').checked = false;
            document.getElementById('Bef_Shft').value = "";
            document.getElementById('Aft_shft').value = "";
            //            document.getElementById('Comp_Sts').value = "";
            document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = "";

            document.getElementById('ChkW1').checked = false;
            document.getElementById('ChkW2').checked = false;
            document.getElementById('ChkHO').checked = false;
            document.getElementById('ChkWD').checked = false;
            document.getElementById('ErlD').checked = false;
            document.getElementById('LtD').checked = false;

        }

        function Chk() {
            var str = '';
            var value = null;
            str = document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value;

            if (document.getElementById('ChkW1').checked) {
                value = "-W1";
                if (str.match(value)) {
                    //                 n = str.replace(value,"");
                }
                else {
                    str = str + value;
                }
                alert(document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value);
                if (document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value == '') {
                    alert(str);
                    var val = str.substr(1, str.length);
                    alert(val);
                    document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = val;
                }
                else {
                    document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
                }
            }
            else {
                if (!document.getElementById('ChkW1').checked) {
                    value = "-W1";
                    if (str.match(value)) {
                        m = str.search(value);
                        if (str.length == 2) {
                            str = "";
                        }
                        else {
                            m = str.search(value);
                            var u = str.substr(m, 3);
                            str = str.replace(u, '');
                        }
                    }
                    //                    document.getElementById('Comp_Sts').value = str;
                    document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
                }
            }
            if (document.getElementById('ChkW2').checked) {

                value = "-W2";
                if (str.match(value)) {

                    //                n = str.replace(value, "");
                }
                else {
                    str = str + value;
                }
                //                document.getElementById('Comp_Sts').value = str;
                document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
            }
            else {
                if (!document.getElementById('ChkW2').checked) {

                    value = "-W2";
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
                    //                    document.getElementById('Comp_Sts').value = str;
                    document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
                }
            }
            if (document.getElementById('ChkHO').checked) {

                value = "-HO";
                if (str.match(value)) {

                    //                n = str.replace(value, "");
                }
                else {
                    str = str + value;
                }
                document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
            }

            else {
                if (!document.getElementById('ChkHO').checked) {
                    value = "-HO";
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
                    //                    document.getElementById('Comp_Sts').value = str;
                    document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
                }
            }
            if (document.getElementById('ChkWD').checked) {

                value = "-WD";
                if (str.match(value)) {
                }
                else {
                    str = str + value;
                }
                //                document.getElementById('Comp_Sts').value = str;
                document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
            }
            else {
                if (!document.getElementById('ChkWD').checked) {
                    value = "-WD";
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
                    document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
                }
            }
        }


        function CheckComp() {
            var str = null;
            var value = null;
            var n = null;
            var m = null;

            //            str = document.getElementById('Comp_Sts').value;

            str = document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value;
            if (document.getElementById('ChkW1').checked) {

                value = "W1";
                if (str.match(value)) {

                    //                 n = str.replace(value,"");
                }
                else {
                    str = str + value;
                }
                //                document.getElementById('Comp_Sts').value = str;

                document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
            }
            else {
                if (!document.getElementById('ChkW1').checked) {
                    value = "W1";
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
                    //                    document.getElementById('Comp_Sts').value = str;
                    document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
                }
            }
            if (document.getElementById('ChkW2').checked) {

                value = "W2";
                if (str.match(value)) {

                    //                n = str.replace(value, "");
                }
                else {
                    str = str + value;
                }
                //                document.getElementById('Comp_Sts').value = str;
                document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
            }
            else {
                if (!document.getElementById('ChkW2').checked) {

                    value = "W2";
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
                    //                    document.getElementById('Comp_Sts').value = str;
                    document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
                }
            }
            if (document.getElementById('ChkHO').checked) {

                value = "HO";
                if (str.match(value)) {

                    //                n = str.replace(value, "");
                }
                else {
                    str = str + value;
                }
                document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
            }

            else {
                if (!document.getElementById('ChkHO').checked) {
                    value = "HO";
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
                    //                    document.getElementById('Comp_Sts').value = str;
                    document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
                }
            }
            if (document.getElementById('ChkWD').checked) {

                value = "WD";
                if (str.match(value)) {
                }
                else {
                    str = str + value;
                }
                //                document.getElementById('Comp_Sts').value = str;
                document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
            }
            else {
                if (!document.getElementById('ChkWD').checked) {
                    value = "WD";
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
                    document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_Comp_Sts').value = str;
                }
            }

        }




        function findspace(evnt) {

            var keyASCII = (evnt.which) ? evnt.which : event.keyCode;
            var keyValue = String.fromCharCode(keyASCII);

            if (!(keyASCII >= '48' && keyASCII <= '57')) {
                window.event.keyCode = 0;
            }
        }
        function fnColon(ctrl, e) {
            var unicode = e.keyCode
            if (unicode != 8) {
                if (ctrl.getAttribute && ctrl.value.length == 2) {
                    ctrl.value = ctrl.value + ":";
                }
            }
        }
  
    </script>
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvCategory.ClientID %>');

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
    <style type="text/css">
        .style37
        {
            height: 33px;
        }
        .Display
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnMain" runat="server">
        <ContentTemplate>
            <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                        <asp:Label ID="lblHead" runat="server" Text="Category Specific Rules" ForeColor="RoyalBlue"
                            Font-Size="20px" Width="100%" CssClass="heading">
                        </asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <center>
                <div class="DivEmpDetails">
                    <asp:Panel ID="panel1" runat="server" DefaultButton="btnSearch">
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
                                    <asp:TextBox ID="textCategory" runat="server" Style="float: right;" onkeypress="return CheckLength_ID(event)"
                                        onkeydown="return (event.keyCode!=13);" CssClass="searchTextBox"></asp:TextBox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtzonename" runat="server" TargetControlID="textCategory"
                                        WatermarkText="ID" WatermarkCssClass="watermark">
                                    </ajaxToolkit:TextBoxWatermarkExtender>
                                    <asp:TextBox ID="textreasonid" runat="server" Style="float: right; display: none;"
                                        onkeydown="return (event.keyCode!=13);" CssClass="searchTextBox"></asp:TextBox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="twezoneid" runat="server" TargetControlID="textreasonid"
                                        WatermarkText="Search by Reason ID" WatermarkCssClass="watermark" Enabled="false">
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
                                            <asp:GridView ID="gvCategory" runat="server" AutoGenerateColumns="false" Width="100%"
                                                GridLines="None" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvCategory_PageIndexChanging"
                                                OnRowCommand="gvCategory_RowCommand" OnRowDataBound="gvCategory_RowDataBound">
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
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit3" CommandArgument='<%# Eval("CAT_ROW_ID") %>'
                                                                ForeColor="#3366FF">Edit</asp:LinkButton>
                                                            <asp:HiddenField ID="hdnFlag" runat="server" Value='<%#Eval("Flag") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CAT_ROW_ID" HeaderStyle-CssClass="Display" SortExpression="ID"
                                                        ItemStyle-CssClass="Display"></asp:BoundField>
                                                    <asp:BoundField DataField="CAT_CATEGORY_ID" HeaderText="ID" SortExpression="ID">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CAT_EARLY_GOING" HeaderText="Early Going" SortExpression="Early Going">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CAT_LATE_COMING" HeaderText="Late Coming" SortExpression="Late Coming">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CAT_EXHRS_BEFORE_SHIFT_HRS" HeaderText="ExHrs Before Shift"
                                                        SortExpression="Before Shift Hours"></asp:BoundField>
                                                    <asp:BoundField DataField="CAT_EXHRS_AFTER_SHIFT_HRS" HeaderText="ExHrs After Shift"
                                                        SortExpression="After Shift Hours"></asp:BoundField>
                                                    <asp:BoundField DataField="CAT_COMPENSATORYOFF_CODE" HeaderText="Compensatory off Code"
                                                        SortExpression="Compensatory off Code"></asp:BoundField>
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
                        <%-- <asp:AsyncPostBackTrigger ControlID="btnAdd" />--%>
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                        <asp:AsyncPostBackTrigger ControlID="gvCategory" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <%--    <asp:AsyncPostBackTrigger ControlID="BtnSave" />--%>
                    </Triggers>
                </asp:UpdatePanel>
            </center>
            <asp:Panel ID="pnlAddCategory" runat="server" CssClass="PopupPanel">
                <asp:UpdatePanel ID="updatepanel4" runat="server">
                    <ContentTemplate>
                        <table id="tblcat" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%"
                            class="TableClass">
                            <tr>
                                <td class="TDClassLabel">
                                    Select Category:<label class="CompulsaryLabel">*</label>
                                </td>
                                <td class="TDClassControl" style="height: 10px; width: 70%">
                                    <asp:DropDownList ID="ddlCategory" runat="server" class="chosen-select" Visible="false"
                                        Width="200px">
                                    </asp:DropDownList>
                                    <%--<asp:Label ID = "lblCategory" runat ="server" VIsible = "false"></asp:Label>--%>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvCategoryID" runat="server" ControlToValidate="ddlCategory"
                                        ErrorMessage="Please Select Category Type." ForeColor="Red" Display="Dynamic"
                                        SetFocusOnError="True" ValidationGroup="Add" InitialValue="Select One"></asp:RequiredFieldValidator>
                                    <%--<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                TargetControlID="rfvCategoryID" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassLabel" valign="top">
                                    Grace Timing :
                                </td>
                                <td class="TDClassControl" style="width: 70%">
                                    <table id="Table3" runat="server">
                                        <tr>
                                            <td align="left">
                                                Early Going allowed by :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Erl_Alwd" runat="server" MaxLength="5" placeholder="24 Hrs Format"
                                                    onkeyup="fnColon(this,event)" ClientIDMode="Static" TabIndex="3"></asp:TextBox>
                                                <%-- <ajaxToolkit:TextBoxWatermarkExtender ID="TBEErl_Alwd" runat="server" TargetControlID="Erl_Alwd"
                                            WatermarkText="24 Hrs Format" WatermarkCssClass="watermark">
                                        </ajaxToolkit:TextBoxWatermarkExtender>--%>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FTEErl_Alwd" runat="server" FilterType="Numbers,Custom"
                                                    TargetControlID="Erl_Alwd" ValidChars=":" />
                                                <br />
                                                <asp:RegularExpressionValidator ID="revEarlyAllowed" runat="server" ControlToValidate="Erl_Alwd"
                                                    ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                                    Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Add"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vceEarlyAllowed" runat="server" TargetControlID="revEarlyAllowed"
                                                    PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Late Coming allowed by :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Lt_Alwd" runat="server" placeholder="24 Hrs Format" MaxLength="5"
                                                    onkeyup="fnColon(this,event)" ClientIDMode="Static" TabIndex="4"></asp:TextBox>
                                                <%-- <ajaxToolkit:TextBoxWatermarkExtender ID="TWELt_Alwd" runat="server" TargetControlID="Lt_Alwd"
                                            WatermarkText="24 Hrs Format" WatermarkCssClass="watermark">
                                        </ajaxToolkit:TextBoxWatermarkExtender>--%>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FTELt_Alwd" runat="server" FilterType="Numbers,Custom"
                                                    TargetControlID="Lt_Alwd" ValidChars=":" />
                                                <br />
                                                <asp:RegularExpressionValidator ID="revLateAllowed" runat="server" ControlToValidate="Lt_Alwd"
                                                    ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                                    Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Add"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                    TargetControlID="revLateAllowed" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassLabel" valign="top">
                                    Extra Hours :
                                </td>
                                <td class="TDClassControl" style="width: 70%">
                                    <table id="Table5" runat="server">
                                        <tr>
                                            <td colspan="2">
                                                <label id="Label1" runat="server" clientidmode="Static">
                                                    If Extra Allowed</label>
                                                <asp:CheckBox ID="ChkAllowed" runat="server" TabIndex='5' ClientIDMode="Static" onclick="ChkF()"
                                                    Checked="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style37">
                                                Extra Hrs Allowed(Before shift Hours)&gt;= :
                                            </td>
                                            <td class="style37">
                                                <asp:TextBox ID="Bef_Shft" runat="server" placeholder="24 Hrs Format" MaxLength="5"
                                                    onkeyup="fnColon(this,event)" ClientIDMode="Static" TabIndex="6"></asp:TextBox>
                                                <%--                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TWEBef_Shft" runat="server" TargetControlID="Bef_Shft"
                                            WatermarkText="24 Hrs Format" WatermarkCssClass="watermark">
                                        </ajaxToolkit:TextBoxWatermarkExtender>--%>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FTEBef_Shft" runat="server" FilterType="Numbers,Custom"
                                                    TargetControlID="Bef_Shft" ValidChars=":" />
                                                <asp:CustomValidator ID="cvBeforeShift" runat="server" ErrorMessage="Enter Before shift Hours"
                                                    Display="None" ControlToValidate="Bef_Shft" SetFocusOnError="true" ForeColor="Red"
                                                    ClientValidationFunction="CheckExtraHours" ValidateEmptyText="true" ValidationGroup="Add"></asp:CustomValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                    TargetControlID="cvBeforeShift" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="rebeforeShift" runat="server" ControlToValidate="Bef_Shft"
                                                    ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                                    Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Add"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                    TargetControlID="rebeforeShift" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style38">
                                                Extra Hrs Allowed(After shift Hours)&lt;= :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Aft_shft" runat="server" placeholder="24 Hrs Format" MaxLength="5"
                                                    onkeyup="fnColon(this,event)" ClientIDMode="Static" TabIndex="7"></asp:TextBox>
                                                <%--<ajaxToolkit:TextBoxWatermarkExtender ID="TWEAft_shft" runat="server" TargetControlID="Aft_shft"
                                            WatermarkText="24 Hrs Format" WatermarkCssClass="watermark">--%>
                                                </ajaxToolkit:TextBoxWatermarkExtender>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FTEAft_shft" runat="server" FilterType="Numbers,Custom"
                                                    TargetControlID="Aft_shft" ValidChars=":" />
                                                <asp:CustomValidator ID="cvAfterShift" runat="server" ErrorMessage="Enter After shift Hours"
                                                    Display="None" ControlToValidate="Aft_shft" SetFocusOnError="true" ForeColor="Red"
                                                    ClientValidationFunction="CheckExtraHoursaft" ValidateEmptyText="true" ValidationGroup="Add">
                                                </asp:CustomValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                    TargetControlID="cvAfterShift" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="revAfterShift" runat="server" ControlToValidate="Aft_shft"
                                                    ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                                    Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Add">
                                                </asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                    TargetControlID="revAfterShift" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassLabel" valign="top">
                                    Compensatory Off :
                                </td>
                                <td style="width: 70%">
                                    <table id="Table6" runat="server" width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="Comp_Sts" runat="server" CssClass="TextControl" Enabled="false"
                                                    MaxLength="8" TabIndex='8' onkeypress="return NotAll()"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="ChkW1" runat="server" onClick="return CheckComp()" ClientIDMode="Static"
                                                    TabIndex='9' />
                                                Weekend
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChkW2" runat="server" onclick="return CheckComp()" ClientIDMode="Static"
                                                    TabIndex='10' />
                                                Weekoff
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="ChkHO" runat="server" onclick="return CheckComp()" ClientIDMode="Static"
                                                    TabIndex='11' />
                                                Holiday
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChkWD" runat="server" onclick="return CheckComp()" ClientIDMode="Static"
                                                    TabIndex='12' />
                                                WeekDay
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassLabel" valign="top">
                                    Deduct From Extra Hours :
                                </td>
                                <td class="TDClassControl" style="width: 70%">
                                    <table id="Table7" runat="server" width="100%">
                                        <tr>
                                            <td>
                                                Early Going Hours :
                                                <asp:CheckBox ID="ErlD" runat="server" TabIndex='13' ClientIDMode="Static" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Late Coming Hours :
                                                <asp:CheckBox ID="LtD" runat="server" TabIndex='14' ClientIDMode="Static" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="BtnSave" runat="server" CssClass="ButtonControl" Text="Save" TabIndex='15'
                                        ValidationGroup="Add" OnClick="BtnSave_Click" OnClientClick="" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="ButtonControl" Text="Cancel"
                                        TabIndex='16' CausesValidation="False" OnClick="btnCancel_Click" />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <asp:Label ID="lblMessageAdd" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnSave" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Button ID="Button1" runat="server" CssClass="ButtonControl" Text="Save" Style="display: none;" />
            <ajaxToolkit:ModalPopupExtender ID="mpeAddCategory" runat="server" TargetControlID="Button1"
                PopupControlID="pnlAddCategory" BackgroundCssClass="modalBackground" Enabled="true"
                CancelControlID="btnCancel">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlMessage" runat="server" CssClass="popupPannel" Style="min-width: 150px;
                min-height: 50px;">
                <table style="width: 100%; height: 100%; min-height: 100%; max-width: 100%;">
                    <tr>
                        <td style="padding-bottom: 20px;">
                            <asp:Label ID="lblMessage" runat="server" Text="All categories are configured" Style="font-family: Arial;
                                font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnMessageOK" runat="server" Text="OK" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="Button2" runat="server" Text="OK" Style="display: none;" />
            <ajaxToolkit:ModalPopupExtender ID="mpeMessage" runat="server" TargetControlID="Button2"
                PopupControlID="pnlMessage" OkControlID="btnMessageOK" BackgroundCssClass="modalBackground">
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
