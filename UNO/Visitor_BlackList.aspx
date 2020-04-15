<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    UICulture="en-GB" CodeBehind="Visitor_BlackList.aspx.cs" Inherits="UNO.VMS_BlackList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .cssVEh
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .hideCol
        {
            display: none;
        }
        .style42
        {
            height: 40px;
            width: 201px;
            margin: 0px;
            padding: 0px;
        }
        
        
        .TDClassLabel1vaibhav
        {
            font-family: Verdana;
            font-size: 9pt;
            color: #515456;
            border: 0px solid #CCCCCC;
            border-width: 0px;
            text-align: right;
            padding-right: 4px;
            width: 22%;
        }
        .Display
        {
            display: none;
        }
        .style46
        {
            font-family: Verdana;
            font-size: 9pt;
            color: #515456;
            border: 0px solid #CCCCCC;
            text-align: right;
            padding-right: 4px;
            width: 210px;
        }
    </style>
    <script type="text/javascript" src="Scripts/Validation.js"></script>
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            var updateProgressDiv = $get('updateProgressDiv');
            updateProgressDiv.style.display = '';
            var gridView = $get('<%= this.gvBlackList.ClientID %>');
            var gridViewBounds = Sys.UI.DomElement.getBounds(gridView);
            var updateProgressDivBounds = Sys.UI.DomElement.getBounds(updateProgressDiv);
            var x = gridViewBounds.x + Math.round(gridViewBounds.width / 2) - Math.round(updateProgressDivBounds.width / 2);
            var y = gridViewBounds.y + Math.round(gridViewBounds.height / 2) - Math.round(updateProgressDivBounds.height / 2);
            Sys.UI.DomElement.setLocation(updateProgressDiv, x, y);
        }
        function blacklistcheck() {
            debugger;
            var from = document.getElementById("<%=txtFrmDate.ClientID %>");
            var to = document.getElementById("<%=txtTDate.ClientID %>");
            var reason = document.getElementById("<%=txtReason.ClientID %>");
            var val_msg = document.getElementById("val_msg");
            if (from.value == "") {

                document.getElementById("val_msg").innerHTML = "Please Select From Date.";
    return false;
}

   if (to.value=="") {
       document.getElementById("val_msg").innerHTML = "Please Select To Date.";
    return false;
    
}
if (reason.value == "") {
    document.getElementById("val_msg").innerHTML = "Please Enter Reason.";
    return false;

}
debugger


var dateFields = from.value.split('/')

var dateFields2 = to.value.split('/')
var d1 = new Date()
var d2 = new Date()
d1 = from.value;
d2 = to.value;
if (d2 < d1) {
  
    return false;

}
            if (from.value!="" && to.value!="" && reason.value!="") {
                var s = confirm('Are you sure you want to Blacklist?');
                return s;
            }
            else {
                val_msg.value = "";
             
                return true;
            }
       
        
        }
        function onUpdated() {
            var updateProgressDiv = $get('updateProgressDiv');
            updateProgressDiv.style.display = 'none';
        }
    </script>
    <script type="text/javascript">
        function CompareDates(d1, d2) {
            var start = d1.value.toUpperCase();
            var end = d2.value.toUpperCase();
            var arrDate = start.split('/');
            var arrDate1 = end.split('/');
            var d1 = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
            var d2 = new Date(arrDate1[2], arrDate1[1] - 1, arrDate1[0]);
            if (d2 < d1) {
                document.getElementById('<%= CustomValidatorM_Lab_From.ClientID %>').innerHTML = "To Date should not be less than From date"

                return false;
            }
            return true;
        }

        function isValidateIssueDate(oSrc, args) {

            if (!CompareDates(document.getElementById('<%= txtFrmDate.ClientID %>'), document.getElementById('<%= txtTDate.ClientID %>'))) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }

        }
        function isValidateEditDate(oSrc, args) {

            if (!CompareEditDates(document.getElementById('<%=txtFromdateEdit.ClientID %>'), document.getElementById('<%=txtToDateEdit.ClientID %>'))) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }

        }
        function CompareEditDates(d1, d2) {
            var start = d1.value.toUpperCase();
            var end = d2.value.toUpperCase();
            var arrDate = start.split('/');
            var arrDate1 = end.split('/');
            var d1 = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
            var d2 = new Date(arrDate1[2], arrDate1[1] - 1, arrDate1[0]);
            if (d2 < d1) {
                document.getElementById('<%= CustomValidator1.ClientID %>').innerHTML = "To Date should not be less than From date"

                return false;
            }
            return true;
        }
   
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Black List Visitors" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <asp:Panel runat="server" ID="BlackPanel" DefaultButton="btnSearch">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 20%; text-align: left;">
                            <asp:Button ID="btnNew" runat="server" Text="New" CssClass="ButtonControl" ValidationGroup="Search"
                                OnClick="btnNew_Click" />
                            <asp:Button ID="Button4" runat="server" Text="Delete" CssClass="ButtonControl" ValidationGroup="Search"
                                OnClick="Button4_Click" />
                        </td>
                        <td style="width: 30%; text-align: left;">
                            <asp:DropDownList ID="ddlStatus" Style="display: none;" runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                AutoPostBack="True">
                                <asp:ListItem Value="A">All</asp:ListItem>
                                <asp:ListItem Value="W">Non-BlackListed</asp:ListItem>
                                <asp:ListItem Value="B">BlackListed</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 50%; text-align: right;">
                            <asp:Button ID="btnReset" runat="server" Text="Reset" Style="float: right;" CssClass="ButtonControl"
                                ValidationGroup="Search" onclick="btnReset_Click1"  />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;margin-right:3px;" CssClass="ButtonControl"
                                ValidationGroup="Search" OnClick="btnSearch_Click" />
                            <asp:TextBox ID="txtMobileNumber" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                TargetControlID="txtMobileNumber" WatermarkText="Mobile Number" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:TextBox ID="txtCompanyName" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                TargetControlID="txtCompanyName" WatermarkText="Company Name" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:TextBox ID="txtVisitorId" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtVisitorId" runat="server" TargetControlID="txtVisitorId"
                                WatermarkText="Visitor Id" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="3">
                            <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                                <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvBlackList" runat="server" AutoGenerateColumns="false" Width="100%"
                                        GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="gvBlackList_RowCommand"
                                        OnRowDataBound="gvBlackList_RowDataBound">
                                        <RowStyle CssClass="gvRow" />
                                        <HeaderStyle CssClass="gvHeader" />
                                        <AlternatingRowStyle BackColor="#F0F0F0" />
                                        <PagerStyle CssClass="gvPager" />
                                        <EmptyDataTemplate>
                                            <div>
                                                <span>No Records Found.</span>
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False" SortExpression="Delete">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" SortExpression="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkModify" runat="server" CausesValidation="False" CommandName="Modify"
                                                        Text="Edit" ForeColor="#3366FF" CommandArgument='<%#Eval("AllVisitorID")+","+ Eval("Visitor_Name")+","+Eval("RowID")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AllVisitorID" HeaderText="Visitor Id" SortExpression="ID">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Visitor_Name" HeaderText="Name" SortExpression="ECode">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="VisitorCompany" HeaderText="Company" SortExpression="Name">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="mobileNo" HeaderText="Mobile Number" SortExpression="LeaveCode">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Black List" ShowHeader="False" SortExpression="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="NonBlackList"
                                                        Text="Non-Black List" ForeColor="#3366FF" CommandArgument='<%#Eval("AllVisitorID")+","+Eval("RowID")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="History" ShowHeader="False" SortExpression="Edit">
                                                <ItemTemplate>
                                                    <%-- <asp:Label ID="lblBalckListed" runat="server" Text='<%#Eval("BlackListed")%>'></asp:Label>--%>
                                                    <asp:LinkButton ID="lblBalckListed" runat="server" CausesValidation="False" CommandName="View"
                                                        Text="View" ForeColor="#3366FF" CommandArgument='<%#Eval("AllVisitorID")%>'></asp:LinkButton>
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
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gvBlackList" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeBlackList" runat="server" TargetControlID="Button2"
        PopupControlID="pnlBlackList" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancelNew">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlBlackList" CssClass="PopupPanel" runat="server" Width="30%">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table id="Table3" width="100%">
                    <tr>
                        <td style="text-align: center; font-weight: bold; padding-bottom: 2%">
                            Do you want to non-blacklist the visitor ?
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; padding-top: 2%">
                            <asp:Button ID="btnsaveNew" runat="server" Text="yes" CssClass="ButtonControl" OnClick="btnsaveNew_Click" />
                            <asp:Button ID="btnCancelNew" runat="server" Text="No" CssClass="ButtonControl" OnClick="btnCancelNew_Click1" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeView" runat="server" TargetControlID="Button1"
        PopupControlID="pnlView" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnOk">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlView" CssClass="PopupPanel" runat="server">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="Table1">
                    <tr>
                        <td colspan="5" style="text-align: center; padding-bottom: 2%">
                            <asp:Label ID="Label1" runat="server" Text="Black Listed Details" Font-Bold="True"
                                Font-Size="Large"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Visitor Id:
                        </td>
                        <td>
                            <asp:TextBox ID="lblVId" Enabled="false" runat="server" Text=""></asp:TextBox>
                            <%--<asp:Label ID="lblVId" runat="server" Text=""></asp:Label>--%>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            Visitor Name:
                        </td>
                        <td>
                            <asp:TextBox ID="lblVName" Enabled="false" runat="server" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Designation:
                        </td>
                        <td>
                            <asp:TextBox ID="lblVDesig" Enabled="false" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            Company Name:
                        </td>
                        <td>
                            <asp:TextBox ID="lblVCompany" Enabled="false" runat="server" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="padding-top: 2%">
                            <asp:GridView ID="gvPopUp" runat="server" AutoGenerateColumns="False" Width="100%"
                                AllowSorting="True" AllowPaging="true" ClientIDMode="Static" PageSize="10" GridLines="None"
                                OnPageIndexChanging="gvPopUp_PageIndexChanging">
                                <RowStyle CssClass="gvRow" />
                                <HeaderStyle CssClass="headerstyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                <PagerStyle CssClass="gvPager" />
                                <EmptyDataRowStyle BackColor="#edf5ff" Height="100px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <div>
                                        <span>No Records found.</span>
                                    </div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="Blacklist_Date" HeaderText="BlackListed Date" SortExpression="Name">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="blacklisted_from_date" HeaderText="From Date" SortExpression="Name">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="blackListed_To_date" HeaderText="To Date" SortExpression="Name">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Blacklist_Reason" HeaderText="Reason" SortExpression="Name">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center; padding-top: 2%">
                            <asp:Button ID="btnOk" runat="server" CssClass="ButtonControl" Text="Cancel" OnClick="btnOk_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSerchInner" CssClass="PopupPanel"
        Width="80%" Style="height: auto;">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
             <asp:TableRow ID="TableRow2" runat="server">
                    <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                        <asp:Label ID="Label3" runat="server" Text="Visitor To Be Blacklisted" ForeColor="RoyalBlue"
                            Font-Size="20px" Width="100%" CssClass="heading">
                        </asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <table style="width: 100%; padding: 1px;">
                    <tr runat="server" id="sectionGV3">
                        <td>
                            <fieldset style="width: 98%; border-radius: 10px;">
                                <legend style="color: Green; padding-left: 5px; padding-right: 5px; font-weight: bold;">
                                    Visitor To Be Blacklisted :</legend>
                                <div style="border: 0px groove lightgray; max-width: 100%; max-height: 200px; height: auto;
                                    width: 100%; overflow: auto; border-radius: 10px;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="width: 60%; text-align: right;">
                                                <asp:Button ID="btnSerchInner" runat="server" Text="Search" Style="float: right;"
                                                    CssClass="ButtonControl" OnClick="btnSerchInner_Click" />
                                                <asp:TextBox ID="txtEmpName" runat="server" Style="float: right;"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender321" runat="server"
                                                    TargetControlID="txtEmpName" WatermarkText="Visitor Name" WatermarkCssClass="">
                                                </ajaxToolkit:TextBoxWatermarkExtender>
                                                <asp:TextBox ID="txtVehicleNo" runat="server" Style="float: right;"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender32" runat="server"
                                                    TargetControlID="txtVehicleNo" WatermarkText="Visitor ID" WatermarkCssClass="">
                                                </ajaxToolkit:TextBoxWatermarkExtender>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="gvVisitorPopup" runat="server" AutoGenerateColumns="false" Width="100%"
                                        OnRowCommand="gvVisitorPopup_RowCommand">
                                        <EmptyDataTemplate>
                                            <div style="text-align: center">
                                                <span>No Records.</span>
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" HeaderStyle-BackColor="lightgray" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblVisitorID" CommandName="BlackList" runat="server" ForeColor="Blue"
                                                        CommandArgument='<%#Eval("AllVisitorID")+","+ Eval("Visitor_Name")%>'  Text="Blacklist"></asp:LinkButton>
                                                    <%--          <asp:CheckBox ID="chkControllerEdit" runat="server"/>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AllVisitorID" HeaderText="Visitor ID" HeaderStyle-BackColor="lightgray"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Visitor_Name" HeaderText="Visitor Name" HeaderStyle-BackColor="lightgray"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="mobileNo" HeaderText="Mobile No." HeaderStyle-BackColor="lightgray"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </td>
                    </tr>
                    <tr runat="server" id="Section1" visible="false">
                        <td>
                            <fieldset style="width: 98%; border-radius: 10px;">
                                <legend style="color: Green; padding-left: 5px; padding-right: 5px; font-weight: bold;">
                                    Blacklist Details :</legend>
                                <table id="table2" runat="server" height="90%" border="0" cellpadding="0" width="100%"
                                    cellspacing="0">
                                    <tr id="Tr4" runat="server">
                                        <td id="Td14" class="style46" runat="server" style="text-align: left; font-weight: bold;">
                                            Visitor ID :&nbsp;
                                        </td>
                                        <td id="Td15" style="height: 10px; width: 200px;" class="style42" runat="server">
                                            <%--          <asp:DropDownList ID="ddlEntity" runat="server" class="chosen-select" AutoPostBack="true"
                                ValidationGroup="add" Width="174px" onselectedindexchanged="ddlEntity_SelectedIndexChanged" 
                             >
                                
                            </asp:DropDownList>--%>
                                            <asp:TextBox ID="txtVisitorIDpopup" onKeyPress="javascript: return false;" runat="server"
                                                Enabled="false" Width="174px"></asp:TextBox>
                                            <br />
                                        </td>
                                        <td id="Td16" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                            font-weight: bold;">
                                            Visitor Name.
                                        </td>
                                        <td id="Td19" runat="server" class="style42">
                                            <%--       <asp:DropDownList ID="ddlVehicle" runat="server" 
                                ValidationGroup="add" Width="174px" 
                             >
                                
                            </asp:DropDownList>--%>
                                            <asp:TextBox ID="txtVisitorNamePopup" onKeyPress="javascript: return false;" runat="server"
                                                Enabled="false" Width="174px"></asp:TextBox>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr id="Tr2" runat="server">
                                        <td id="Td8" class="style46" runat="server" style="text-align: left; font-weight: bold;">
                                            From Date :<label class="CompulsaryLabel">*</label>
                                        </td>
                                        <td id="Td9" runat="server" class="style42">
                                            <asp:TextBox ID="txtFrmDate" runat="server" Width="174px" onKeyPress="javascript: return false;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="cetxtFrmDate" runat="server" TargetControlID="txtFrmDate"
                                                PopupButtonID="txtFrmDate" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select From Date"
                                                ValidationGroup="Add" ControlToValidate="txtFrmDate" Display="None"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                TargetControlID="RequiredFieldValidator1" PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFrmDate"
                                                Display="None" ValidationGroup="Add" ErrorMessage="Please enter date in dd/mm/yyyy format"
                                                ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                TargetControlID="RegularExpressionValidator2" PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            <br />
                                        </td>
                                        <td id="Td11" class="TDClassLabel1vaibhav" style="height: 10px; text-align: left;
                                            font-weight: bold;" runat="server">
                                            To Date :
                                            <label class="CompulsaryLabel">
                                            </label>
                                        </td>
                                        <td id="Td12" runat="server" class="style42">
                                            <asp:TextBox ID="txtTDate" runat="server" Width="174px" onKeyPress="javascript: return false;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtTDate"
                                                PopupButtonID="txtTDate" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:CustomValidator ID="CustomValidatorM_Lab_From" runat="server" ClientValidationFunction="isValidateIssueDate"
                                                ValidationGroup="Add" ControlToValidate="txtTDate" Display="None" ErrorMessage="To Date should not be less than From date"
                                                ForeColor="Red" SetFocusOnError="True"></asp:CustomValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                TargetControlID="CustomValidatorM_Lab_From" PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTDate"
                                                Display="None" ValidationGroup="Add" ErrorMessage="Please enter date in dd/mm/yyyy format"
                                                ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                TargetControlID="RegularExpressionValidator1" PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            &nbsp;<br />
                                        </td>
                                    </tr>
                                    <tr id="Tr1" runat="server">
                                        <td id="Td1" class="style46" runat="server" style="text-align: left; font-weight: bold;">
                                            Reason :     <label class="CompulsaryLabel">*
                                            </label>
                                        </td>
                                        <td id="Td2" style="height: 10px;" class="style42" runat="server" colspan="3">
                                            <%--          <asp:DropDownList ID="ddlEntity" runat="server" class="chosen-select" AutoPostBack="true"
                                ValidationGroup="add" Width="174px" onselectedindexchanged="ddlEntity_SelectedIndexChanged" 
                             >
                                
                            </asp:DropDownList>--%>
                                            <asp:TextBox ID="txtReason" Width="534px" runat="server" TextMode="MultiLine" Style="max-width: 534px;
                                                min-width: 534px; min-height: 40px; max-height: 40px" CssClass="TextControl"
                                                TabIndex="3" Height="50px" MaxLength="150" onkeyDown="checkLength(this,'150');"
                                                onkeyUp="checkLength(this,'150');"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter reason"
                                                ValidationGroup="Add" ControlToValidate="txtReason" Display="None"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                TargetControlID="RequiredFieldValidator3" PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            <br />
                                            <%--       <asp:DropDownList ID="ddlVehicle" runat="server" 
                                ValidationGroup="add" Width="174px" 
                             >
                                
                            </asp:DropDownList>--%>
                                            <br />
                                        </td>
                                    </tr>

                                     <tr>
                    <td colspan="4"  style="text-align:center;" >
                    <label id="val_msg" style="color:Red;font-size:12px;"></label>

                      </td>
                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td rowspan="2">
                        </td>
                    </tr>

                   
                </table>
                <table id="table4" runat="server" width="100%" border="0" cellpadding="3" cellspacing="3">
                    <tr id="Tr3" runat="server" visible="false">
                        <td id="Td13" align="center" runat="server">
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonControl" Text="Save"  OnClick="btnSave_Click" 
                                ValidationGroup="Add" OnClientClick="return blacklistcheck();"   />
                           <%--<asp:Button OnClientClick="return confirm('save data?');" ID="Button6" CausesValidation="true" runat="server" 
                            Text="Button"  OnClick="btnSave_Click" />--%>
                            <asp:Button ID="btnCancel" runat="server" CssClass="ButtonControl" Text="Cancel"
                                OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
                <table runat="server" id="tblClose" width="100%" border="0" cellpadding="3" cellspacing="3">
                    <tr id="Tr7" runat="server">
                        <td id="Td24" align="center" runat="server">
                            <asp:Button ID="Button5" runat="server" Text="Close" CssClass="ButtonControl" OnClick="Button5_Click" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td align="center" class="style39">
                            <asp:Label ID="Label2" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button3" runat="server" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeNewNonBlacklisted" runat="server" TargetControlID="Button3"
        BehaviorID="ModalBehaviourNew" PopupControlID="Panel1" BackgroundCssClass="modalBackground"
        Enabled="true">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlEditBlackListedVisitor" runat="server" CssClass="PopupPanel" Width="80%"
        Style="height: auto;">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr runat="server" id="Tr5">
                        <td>
                            <fieldset style="width: 98%; border-radius: 10px;">
                                <legend style="color: Green; padding-left: 5px; padding-right: 5px; font-weight: bold;">
                                    Blaklist&nbsp;Details :</legend>
                                <table id="table5" runat="server" height="90%" border="0" cellpadding="0" width="100%"
                                    cellspacing="0">
                                    <tr id="Tr6" runat="server">
                                        <td id="Td3" class="style46" runat="server" style="text-align: left; font-weight: bold;">
                                            Visitor ID :&nbsp;
                                        </td>
                                        <td id="Td4" style="height: 10px; width: 200px;" class="style42" runat="server">
                                            <%--          <asp:DropDownList ID="ddlEntity" runat="server" class="chosen-select" AutoPostBack="true"
                                ValidationGroup="add" Width="174px" onselectedindexchanged="ddlEntity_SelectedIndexChanged" 
                             >
                                
                            </asp:DropDownList>--%>
                                            <asp:TextBox ID="txtVisitorIdEdit" onKeyPress="javascript: return false;" runat="server"
                                                Enabled="false" Width="174px"></asp:TextBox>
                                            <br />
                                        </td>
                                        <td id="Td5" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                            font-weight: bold;">
                                            Visitor Name.
                                        </td>
                                        <td id="Td6" runat="server" class="style42">
                                            <%--       <asp:DropDownList ID="ddlVehicle" runat="server" 
                                ValidationGroup="add" Width="174px" 
                             >
                                
                            </asp:DropDownList>--%>
                                            <asp:TextBox ID="txtVisitorNameEdit" onKeyPress="javascript: return false;" runat="server"
                                                Enabled="false" Width="174px"></asp:TextBox>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr id="Tr8" runat="server">
                                        <td id="Td7" class="style46" runat="server" style="padding-right: 50px; text-align: left;
                                            font-weight: bold;">
                                            From Date :<label class="CompulsaryLabel">*</label>
                                        </td>
                                        <td id="Td10" runat="server" class="style42">
                                            <asp:TextBox ID="txtFromdateEdit" runat="server" Width="174px" onKeyPress="javascript: return false;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromdateEdit"
                                                PopupButtonID="txtFromdateEdit" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select From Date"
                                                ValidationGroup="Edit" ControlToValidate="txtFromdateEdit" Display="None"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                TargetControlID="RequiredFieldValidator2" PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtFromdateEdit"
                                                Display="None" ValidationGroup="Edit" ErrorMessage="Please enter date in dd/mm/yyyy format"
                                                ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                TargetControlID="RegularExpressionValidator3" PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            <br />
                                        </td>
                                        <td id="Td17" class="TDClassLabel1vaibhav" style="height: 10px; text-align: left;
                                            font-weight: bold;" runat="server">
                                            To Date :
                                            <label class="CompulsaryLabel">
                                            </label>
                                        </td>
                                        <td id="Td18" runat="server" class="style42">
                                            <asp:TextBox ID="txtToDateEdit" runat="server" Width="174px" onKeyPress="javascript: return false;" ValidationGroup="Edit"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDateEdit"
                                                PopupButtonID="txtToDateEdit" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="isValidateEditDate"
                                                ValidationGroup="Edit" ControlToValidate="txtToDateEdit" Display="None" ErrorMessage="To Date should not be less than From date"
                                                ForeColor="Red" SetFocusOnError="True"></asp:CustomValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                                TargetControlID="CustomValidator1" PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtToDateEdit"
                                                Display="None" ValidationGroup="Edit" ErrorMessage="Please enter date in dd/mm/yyyy format"
                                                ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                                TargetControlID="RegularExpressionValidator4" PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            &nbsp;<br />
                                        </td>
                                    </tr>
                                    <tr id="Tr9" runat="server">
                                        <td id="Td20" class="style46" runat="server" style="text-align: left; font-weight: bold;">
                                            Reason :&nbsp;     <label class="CompulsaryLabel">*
                                            </label>
                                        </td>
                                        <td id="Td21" style="height: 10px;" class="style42" runat="server" colspan="3">
                                            <%--          <asp:DropDownList ID="ddlEntity" runat="server" class="chosen-select" AutoPostBack="true"
                                ValidationGroup="add" Width="174px" onselectedindexchanged="ddlEntity_SelectedIndexChanged" 
                             >
                                
                            </asp:DropDownList>--%>
                                            <asp:TextBox ID="txtReasonEdit" Width="534px" runat="server" TextMode="MultiLine"
                                                Style="max-width: 534px; min-width: 534px; min-height: 40px; max-height: 40px"
                                                CssClass="TextControl" TabIndex="3" Height="50px" MaxLength="150" onkeyDown="checkLength(this,'150');"
                                                onkeyUp="checkLength(this,'150');" ValidationGroup="Edit"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter reason"
                                                ValidationGroup="Edit" ControlToValidate="txtReasonEdit" Display="None"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                TargetControlID="RequiredFieldValidator4" PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            <br />
                                            <%--       <asp:DropDownList ID="ddlVehicle" runat="server" 
                                ValidationGroup="add" Width="174px" 
                             >
                                
                            </asp:DropDownList>--%>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                                <table id="table6" runat="server" width="100%" border="0" cellpadding="3" cellspacing="3">
                                    <tr id="Tr10" runat="server">
                                        <td id="Td22" align="center" runat="server">
                                            <asp:Button ID="btnSaveEdit" runat="server" CssClass="ButtonControl" ValidationGroup="Edit"
                                                Text="Save" OnClick="btnSaveEdit_Click" />
                                            <asp:Button ID="Button7" runat="server" CssClass="ButtonControl" Text="Cancel" OnClick="Button7_Click" />
                                            <asp:HiddenField ID="hdnRowID" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td rowspan="2">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Btntemp5" runat="server" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeNewNonBlacklistedEdit" runat="server" TargetControlID="Btntemp5"
        PopupControlID="pnlEditBlackListedVisitor" BackgroundCssClass="modalBackground"
        Enabled="true">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
