<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Leave_Data_Upload.aspx.cs" Inherits="UNO.Leave_Data_Upload" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="Leave_Data_Upload.aspx.cs" Inherits="UNO.Leave_Data_Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
        
        .hidden-field
        {
            display: none;
        }
    </style>
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script>

        function ValidateRDListBox(sender, args) {
            var options = document.getElementById("lstSReader").options;
            if (options.length == 0) {
                sender.innerHTML = "Please select reader(s).";
                args.IsValid = false;
            }
            else { args.IsValid = true; }


            var sel = document.getElementbyId("lstSReader");
            var listLength = sel.options.length;
            for (var i = 0; i < listLength; i++) {
                if (sel.options[i].selected)
                    //document.getElementById("list2").add(new Option(sel.options[i].value));
            } 



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
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvLeaveData.ClientID %>');

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
    <script language="javascript" type="text/javascript">


        var myVals = new Array();
        function CacheValues() {
            var l = document.getElementById('lstEmployees');

            for (var i = 0; i < l.options.length; i++) {
                myVals[i] = l.options[i].text;
            }
        }
        function SearchList() {

            var l = document.getElementById('lstEmployees');
            var tb = document.getElementById('txtSearchBox');

            l.options.length = 0;

            if (tb.value == "") {
                for (var i = 0; i < myVals.length; i++) {
                    l.options[l.options.length] = new Option(myVals[i]);
                }
            }
            else {


                for (var i = 0; i < myVals.length; i++) {
                    if (myVals[i].toLowerCase().indexOf(tb.value.toLowerCase()) != -1) {
                        l.options[l.options.length] = new Option(myVals[i]);
                    }
                    else {
                        // do nothing
                    }
                }
            }
        }

        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }


    </script>
    <%--<script language="javascript" type="text/javascript">

        function ValidateShiftListBoxEdit(sender, args) {

//            var options = document.getElementById("<%=lstLeaveType.ClientID%>").options;
//          
//            if (options.length == 0) {
//                sender.innerHTML = "Please select Leave Type.";
//                args.IsValid = false;
//            }
//            else
            //            { args.IsValid = true; }

            var options = document.getElementById("<%=lstLeaveType.ClientID%>").options;
                        if (options.length == 0) {
                            sender.innerHTML = "Please select Leave Type.";
                            args.IsValid = false;
                        }
                        else {
                            args.IsValid = true;
                        }


        }

    </script>--%>
    <script language="javascript" type="text/javascript">

        function clearFunction(x) {
            document.getElementById(x).innerHTML = "&nbsp;&nbsp;";
        }

        function handleDelete() {

            if (!validateCheckBoxes())
                return false;

            var msg = confirm("Record(s) marked for Deletion. Continue? ");
            if (msg == false) {
                clearGridCheckBoxes();
                return false;
            }
        }

        function validateCheckFocus() {

            var isValid = false;

            var gridView = document.getElementById('<%= gvLeaveData.ClientID %>');

            var inputs = gridView.getElementsByTagName('input');

            for (var i = 0; i < inputs.length; i++) {

                var elementRef = inputs[i];

                if ((elementRef.type == 'checkbox') && (elementRef.checked == true)) {
                    document.getElementById('btnDelete').focus();
                    isValid = true;

                    return true;


                }
            }

        }

        function clearGridCheckBoxes() {
            var gridRef = document.getElementById('<%= gvLeaveData.ClientID %>');
            var inputElementArray = gridRef.getElementsByTagName('input');
            var cntchk = 0;
            for (var i = 0; i < inputElementArray.length; i++) {
                var elementRef = inputElementArray[i];

                if ((elementRef.type == 'checkbox')) {
                    elementRef.checked = false;
                }
            }
        }


        function validateCheckBoxes() {

            var isValid = false;

            var gridView = document.getElementById('<%= gvLeaveData.ClientID %>');

            var inputs = gridView.getElementsByTagName('input');

            for (var i = 0; i < inputs.length; i++) {

                var elementRef = inputs[i];

                if ((elementRef.type == 'checkbox') && (elementRef.checked == true)) {
                    isValid = true;

                    return true;


                }
            }
            alert("Please select record");
            return false;
        }
        function isDecimalNum(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode == 46) {
                var inputValue = $("#inputfield").val()
                if (inputValue.indexOf('.') < 1) {
                    return true;
                }
                return false;
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
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
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Leave Data Entry" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <table style="width: 100%;" border="0">
                <tr>
                    <td style="width: 50%; text-align: left;">
                        <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click"
                            Enabled="true" />
                        <%--  <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                            OnClick="btnDelete_Click" />--%>
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                            OnClientClick="return handleDelete()" ClientIDMode="Static" OnClick="btnDelete_Click" />
                        <asp:Button runat="server" ID="btnBulkUpload" Text="Bulk Upload" CssClass="ButtonControl"
                            OnClick="btnBulkUpload_Click" Enabled="true" />
                        <asp:Button runat="server" ID="btnLvCut" Text="Leave Cut" CssClass="ButtonControl"
                            OnClick="btnLvCut_Click" Enabled="true" />
                    </td>
                    <td style="width: 50%; text-align: right;">
                        <asp:Button runat="server" ID="cmdReset" Text="Reset" Style="float: right; margin-left: 4px;"
                            CssClass="ButtonControl" OnClick="cmdReset_Click" />
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                            OnClick="btnSearch_Click" />
                        <asp:TextBox ID="txtSearchLeaveCode" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtzonename" runat="server" TargetControlID="txtSearchLeaveCode"
                            WatermarkText="Leave Code" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:TextBox ID="txtSearchEmployeeid" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twezoneid" runat="server" TargetControlID="txtSearchEmployeeid"
                            WatermarkText="Employee ID" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <asp:Panel ID="pnlgvLeaveData" runat="server" Visible="true">
                        <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="3">
                            <div id="updateProgressDiv" style="display: none;">
                                <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvLeaveData" runat="server" AutoGenerateColumns="false" Width="100%"
                                        GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="gvLeaveData_RowCommand"
                                        OnPageIndexChanging="gvLeaveData_PageIndexChanging">
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
                                                ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Modify" CommandArgument='<%# Eval("LV_REC_ID") %>'
                                                        ForeColor="#3366FF">Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="LV_REC_ID" HeaderText="Sr No." SortExpression="Sr No."
                                                ItemStyle-Width="4%" Visible="True" ItemStyle-CssClass="hidden-field" HeaderStyle-CssClass="hidden-field"  >
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LV_EMP_ID" HeaderText="Emp ID" SortExpression="Emp ID"
                                                ItemStyle-Width="10%">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LV_LEAVE_ID" HeaderText="Leave Code" SortExpression="Leave Code"
                                                ItemStyle-Width="10%">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LV_OPENINGBAL" HeaderText="Leave Opening Balance" SortExpression="Leave Opening Balance"
                                                ItemStyle-Width="14%">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LV_ALLOTMENT" HeaderText="Leave Allotment" SortExpression="Leave Allotment"
                                                ItemStyle-Width="14%">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LV_AVAILABLE" HeaderText="Leave Available" SortExpression="Leave Availabe"
                                                ItemStyle-Width="10%">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LV_AVAILED" HeaderText="Leave Availed" SortExpression="Leave Availed"
                                                ItemStyle-Width="10%">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LV_ENCASHED" HeaderText="Leave Encashed" SortExpression="Leave Encashed"
                                                ItemStyle-Width="10%">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LV_CUT" HeaderText="Leave Cut" SortExpression="Leave Cut"
                                                ItemStyle-Width="8%">
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
                    </asp:Panel>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="150px" Width="600px"
                        Visible="false">
                    </rsweb:ReportViewer>
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                </div>
                <table class="MessageContainerTable" width="98.8%">
                    <tr>
                        <td colspan="4" align="center" valign="middle">
                            <div id="messageDiv" runat="server" clientidmode="Static" class="MessageClass">
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Button ID="btnDummyAdd" Style="display: none" runat="server" Text="Button" />
    <asp:Button ID="btnDummyModify" Style="display: none" runat="server" Text="Button" />
    <asp:Button ID="btnDummyAddNewEntry" Style="display: none" runat="server" Text="Button" />
    <asp:Button runat="server" ID="tempbtn" Style="display: none;" />
    <asp:Panel ID="pnlAddZone" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="table2" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%"
                    class="TableClass">
                    <tr>
                        <td>
                            <table class="TableClass" width="100%">
                                <table border="0" width="100%">
                                    <tr>
                                        <td class="heading">
                                            <asp:Label ID="lblBulkUploadAdd" runat="server" Text="Leave Bulk Upload">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px; text-align: center;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px; text-align: left; color: Black;">
                                            <b>Download Template : </b>
                                            <asp:Button ID="btnDownloadExcel" runat="server" Text="Click Here" BackColor="White"
                                                OnClick="btnDownloadExcel_Click" CssClass="ButtonControl" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px; text-align: center;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 50px; text-align: center;">
                                            <b>Please Select Excel File: </b>
                                            <asp:FileUpload ID="fileuploadExcel" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:Button ID="btnImport" runat="server" CssClass="ButtonControl" OnClick="btnImport_Click"
                                                Text="Save" ValidationGroup="Add" />
                                            <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" TabIndex="17"
                                                Text="Cancel" OnClick="btnCancelAdd_Click" CausesValidation="False" ValidationGroup="Add" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblBulkErrorMessage" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </table>
                        </td>
                    </tr>
                </table>
                </td> </tr> </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnImport" />
                <asp:PostBackTrigger ControlID="btnDownloadExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="pnlLeaveCut" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <table id="table1" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%"
                    class="TableClass">
                    <tr>
                        <td>
                            <table class="TableClass" width="100%">
                                <table border="0" width="100%">
                                    <tr>
                                        <td class="heading">
                                            <asp:Label ID="lblLeave" runat="server" Text="Leave Cut Bulk Upload">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px; text-align: center;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px; padding-left: 10%; text-align: left; color: Black;">
                                            <b>Download Template : </b>
                                            <asp:Button ID="btnDownloadLeaveCutExcel" runat="server" Text="Click Here" BackColor="White"
                                                CssClass="ButtonControl" OnClick="btnDownloadLeaveCutExcel_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px; text-align: center;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 50px; text-align: center;">
                                            <b>Please Select Excel File: </b>
                                            <asp:FileUpload ID="fileLeaveUpload" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:Button ID="btnImportLeaveCut" runat="server" CssClass="ButtonControl" Text="Save"
                                                ValidationGroup="AddLeaveCut" OnClick="btnImportLeaveCut_Click" />
                                            <%--<asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" Text="Cancel" ValidationGroup="Add" 
                                            OnClick="btnCancelAdd_Click" CausesValidation="False" />--%>
                                            <asp:Button ID="Button5" runat="server" CssClass="ButtonControl" TabIndex="17" Text="Cancel"
                                                OnClick="btnCancelLeaveCut_Click" CausesValidation="False" ValidationGroup="AddLeaveCut" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblLeaveCutError" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </table>
                        </td>
                    </tr>
                </table>
                </td> </tr> </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnImportLeaveCut" />
                <asp:PostBackTrigger ControlID="btnDownloadLeaveCutExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="pnlAddNewEntry" runat="server" CssClass="PopupPanel" Style="height: auto;
        width: auto">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table class="TableClass">
                    <tr>
                        <td>
                            <table class="TableClass" border="0">
                                <tr>
                                    <td class="heading">
                                        <asp:Label ID="lblLeaveSingleEntry" Visible="false" runat="server" Text="Add Leave">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <%-- multiple class="chosen-select"--%>
                                    <td>
                                        <asp:Label ID="lblEmp" runat="server" Text="Select Employee :   " Style="vertical-align: top;
                                            font-weight: bolder"></asp:Label><font color="red">*</font>
                                        <asp:TextBox ID="txtSearchBox" onkeyup="return SearchList();" runat="server" Height="23px"
                                            ClientIDMode="Static" Width="157px" Visible="false"></asp:TextBox>
                                        &nbsp&nbsp&nbsp&nbsp;&nbsp;
                                        <asp:ListBox ID="lstEmployees" runat="server" ClientIDMode="Static" Font-Bold="True"
                                            Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Single" Width="400px"
                                            Height="85px" AutoPostBack="True" class="chosen-select" OnSelectedIndexChanged="lstEmployees_SelectedIndexChanged">
                                        </asp:ListBox>
                                        <asp:RequiredFieldValidator ID="rfvlstEmployees" runat="server" ErrorMessage="Please select Employee."
                                            ControlToValidate="lstEmployees" Display="None" ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcelstEmployees" runat="server" TargetControlID="rfvlstEmployees"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLeaveCode" runat="server" Text="Select Leave Code :" Style="vertical-align: top;
                                            font-weight: bolder"></asp:Label><font color="red">*</font> &nbsp&nbsp;
                                        <asp:ListBox ID="lstLeaveType" runat="server" ClientIDMode="Static" Font-Bold="True"
                                            Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Single" Width="400px"
                                            Height="37px" AutoPostBack="True" class="chosen-select" OnSelectedIndexChanged="lstLeaveType_SelectedIndexChanged">
                                        </asp:ListBox>
                                        <asp:RequiredFieldValidator ID="rfvlstLeaveType" runat="server" ErrorMessage="Please select Leave Type."
                                            ControlToValidate="lstLeaveType" Display="None" ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcelstLeaveType" runat="server" TargetControlID="rfvlstLeaveType"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLeaveOpeningBal" runat="server" Text="Leave Opening Balance :"
                                            Style="font-weight: bolder; padding-right: 5px;"></asp:Label><font color="red">*</font>
                                        <asp:TextBox ID="txtLeaveOpeningBal" runat="server" Width="47px" MaxLength="8" Style="padding-right: 5px;"
                                            onkeypress="return isDecimalNum(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtLeaveOpeningBal" runat="server" ErrorMessage="Please enter opening balance"
                                            ControlToValidate="txtLeaveOpeningBal" Display="None" ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtLeaveOpeningBal" runat="server" TargetControlID="rfvtxtLeaveOpeningBal"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:RegularExpressionValidator ID="Regex2" runat="server" ValidationExpression="((\d+)+(\.\d+))$"
                                            ErrorMessage="Please enter valid decimal number with any decimal places." Display="None"
                                            ValidationGroup="AddNew" ControlToValidate="txtLeaveOpeningBal" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                            TargetControlID="Regex2" PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:Label ID="lblLeaveAllotment" runat="server" Text="Leave Allotment :" Style="font-weight: bolder;
                                            padding-right: 5px; padding-left: 5px;"></asp:Label><font color="red">*</font>
                                        <asp:TextBox ID="txtLeaveAllotment" runat="server" Width="47px" MaxLength="8" onkeypress="return isDecimalNum(event)"></asp:TextBox>
                                        <%--     <asp:RequiredFieldValidator ID="rfvtxtLeaveAllotment" runat="server" ErrorMessage="Please enter leave allotment"
                                            ControlToValidate="txtLeaveAllotment" Display="None" ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtLeaveAllotment" runat="server" TargetControlID="rfvtxtLeaveAllotment"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="((\d+)+(\.\d+))$"
                                            ErrorMessage="Please enter valid decimal number with any decimal places." Display="None"
                                            ValidationGroup="AddNew" ControlToValidate="txtLeaveAllotment" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                            TargetControlID="RegularExpressionValidator1" PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                                        <asp:Label ID="lblLeaveAvailable" runat="server" Text="Leave Available :" Style="font-weight: bolder;
                                            padding-right: 5px; padding-left: 5px;"></asp:Label><font color="red">*</font>
                                        <asp:TextBox ID="txtLeaveAvailable" runat="server" Width="47px" MaxLength="8" onkeypress="return isDecimalNum(event)"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ID="rfvtxtLeaveAvailable" runat="server" ErrorMessage="Please enter leave available"
                                            ControlToValidate="txtLeaveAvailable" Display="None" ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtLeaveAvailable" runat="server" TargetControlID="rfvtxtLeaveAvailable"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="((\d+)+(\.\d+))$"
                                            ErrorMessage="Please enter valid decimal number with any decimal places." Display="None"
                                            ValidationGroup="AddNew" ControlToValidate="txtLeaveAvailable" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                            TargetControlID="RegularExpressionValidator2" PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>
                                            <asp:Label ID="lblLeaveAlloted" runat="server" Text="Max Leave Allotment:" Style="margin-left: 38%"></asp:Label>
                                            <asp:Label ID="lblLeaveValue" runat="server" Text=""></asp:Label>
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="visibility: hidden;">
                                        <asp:Label ID="lblLeaveAvailed" runat="server" Text="Leave Availed :" Style="font-weight: bolder;
                                            padding-right: 34px;"></asp:Label>
                                        <asp:TextBox ID="txtLeaveAvailed" runat="server" Width="50px"></asp:TextBox>
                                        &nbsp&nbsp&nbsp&nbsp;
                                        <asp:Label ID="lblLeaveEncashed" runat="server" Text="Leave Encashed :" Style="font-weight: bolder;
                                            padding-right: 6px;"></asp:Label>
                                        <asp:TextBox ID="txtLeaveEncashed" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <asp:Button ID="btnSubmitNewEntry" runat="server" CssClass="ButtonControl" Text="Save"
                                            ValidationGroup="AddNew" OnClick="btnSubmitNewEntry_Onclick" />
                                        &nbsp;
                                        <asp:Button ID="btnAddCancelEntry" runat="server" CssClass="ButtonControl" Text="Cancel"
                                            ValidationGroup="AddNew" CausesValidation="false" OnClick="btnAddCancelEntry_Onclick" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 5px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <asp:Label ID="lblErrorSingleEntry" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <asp:GridView ID="GridView1" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                                    RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
                                    runat="server" AutoGenerateColumns="false" AllowPaging="true">
                                    <Columns>
                                    </Columns>
                                </asp:GridView>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lstEmployees" />
                <asp:PostBackTrigger ControlID="lstLeaveType" />
                <asp:PostBackTrigger ControlID="btnSubmitNewEntry" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="pnlModify" runat="server" CssClass="PopupPanel" Style="height: auto;
        width: auto">
        <asp:UpdatePanel ID="updatepnlEditLeaveData" runat="server">
            <ContentTemplate>
                <table class="TableClass" border="0">
                    <tr>
                        <td class="heading" colspan="4" align="center" style="display: none">
                            <asp:Label ID="lblEditLeave" runat="server" Text="Edit Leave">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                            <%--<asp:Label ID="lblID" runat="server"  ClientIDMode="Static" Width="157px" ></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblEditEmployee" runat="server" Text="Employee :   " Style="vertical-align: top;
                                font-weight: bolder"></asp:Label><font color="red">*</font>
                        </td>
                        <td>
                            <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                            <asp:TextBox ID="txtEditEmployeeName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblEditLeaveType" runat="server" Text="Leave Type :   " Style="vertical-align: top;
                                font-weight: bolder"></asp:Label><font color="red">*</font>
                        </td>
                        <td>
                            <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                            <asp:TextBox ID="txtEditLeaveCode" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblEditOpeningBal" runat="server" Text="Leave Opening Balance :   "
                                Style="vertical-align: top; font-weight: bolder"></asp:Label><font color="red">*</font>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditLeaveOpeningBal" runat="server" ClientIDMode="Static" Width="75px"
                                MaxLength="8"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtEditLeaveOpeningBal" runat="server" ErrorMessage="Please enter leave opening balance"
                                ControlToValidate="txtEditLeaveOpeningBal" Display="None" ValidationGroup="ModifyAdd"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtEditLeaveOpeningBal" runat="server"
                                TargetControlID="rfvtxtEditLeaveOpeningBal" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revtxtEditLeaveOpeningBal" runat="server" ValidationExpression="((\d+)+(\.\d+))$"
                                ErrorMessage="Please enter valid decimal number with any decimal places." Display="None"
                                ValidationGroup="ModifyAdd" ControlToValidate="txtEditLeaveOpeningBal" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                TargetControlID="revtxtEditLeaveOpeningBal" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblEditLeaveAllotment" runat="server" Text="Leave Allotment :   "
                                Style="vertical-align: top; font-weight: bolder"></asp:Label><font color="red">*</font>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditLeaveAllotmentAmount" runat="server" ClientIDMode="Static"
                                Width="75px" MaxLength="8"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtEditLeaveAllotmentAmount" runat="server" ErrorMessage="Please enter leave allotment"
                                ControlToValidate="txtEditLeaveAllotmentAmount" Display="None" ValidationGroup="ModifyAdd"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtEditLeaveAllotmentAmount" runat="server"
                                TargetControlID="rfvtxtEditLeaveAllotmentAmount" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revtxtEditLeaveAllotmentAmount" runat="server"
                                ValidationExpression="((\d+)+(\.\d+))$" ErrorMessage="Please enter valid decimal number with any decimal places."
                                Display="None" ValidationGroup="ModifyAdd" ControlToValidate="txtEditLeaveAllotmentAmount" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                TargetControlID="revtxtEditLeaveAllotmentAmount" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblEditLeaveAvailable" runat="server" Text="Leave Available :   "
                                Style="vertical-align: top; font-weight: bolder"></asp:Label><font color="red">*</font>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditLeaveAvailableAmount" runat="server" ClientIDMode="Static"
                                Width="75px" MaxLength="8"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtEditLeaveAvailableAmount" runat="server" ErrorMessage="Please enter available amount"
                                ControlToValidate="txtEditLeaveAvailableAmount" Display="None" ValidationGroup="ModifyAdd"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtEditLeaveAvailableAmount" runat="server"
                                TargetControlID="rfvtxtEditLeaveAvailableAmount" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revtxtEditLeaveAvailableAmount" runat="server"
                                ValidationExpression="((\d+)+(\.\d+))$" ErrorMessage="Please enter valid decimal number with any decimal places."
                                Display="None" ValidationGroup="ModifyAdd" ControlToValidate="txtEditLeaveAvailableAmount" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                TargetControlID="revtxtEditLeaveAvailableAmount" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lbltxtEditLeaveAvailed" runat="server" Text="Leave Availed :   " Style="vertical-align: top;
                                font-weight: bolder" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditLeaveAvailed" runat="server" ClientIDMode="Static" Width="75px"
                                MaxLength="8" Visible="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtEditLeaveAvailed" runat="server" ErrorMessage="Please enter leave availed"
                                ControlToValidate="txtEditLeaveAvailed" Display="None" ValidationGroup="ModifyAdd"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtEditLeaveAvailed" runat="server"
                                TargetControlID="rfvtxtEditLeaveAvailed" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblEditLeaveEncashed" runat="server" Text="Leave Encashed :   " Style="vertical-align: top;
                                font-weight: bolder" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditLeaveEncashed" runat="server" ClientIDMode="Static" Width="75px"
                                MaxLength="8" Visible="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtEditLeaveEncashed" runat="server" ErrorMessage="Please enter leave encashed"
                                ControlToValidate="txtEditLeaveEncashed" Display="None" ValidationGroup="ModifyAdd"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtEditLeaveEncashed" runat="server"
                                TargetControlID="rfvtxtEditLeaveEncashed" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblEditLeaveCut" runat="server" Text="Leave Cut :   " Style="vertical-align: top;
                                font-weight: bolder" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditLeaveCut" runat="server" ClientIDMode="Static" Width="75px"
                                MaxLength="8" Visible="false"></asp:TextBox>
                            <%--  <asp:RequiredFieldValidator ID="rfvtxtEditLeaveCut" runat="server" ErrorMessage="Please enter leave cut"
                                        ControlToValidate="txtEditLeaveCut" Display="None" ValidationGroup="ModifyAdd"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtEditLeaveCut" runat="server" TargetControlID="rfvtxtEditLeaveCut"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnModifySaveLeave" runat="server" CssClass="ButtonControl" Text="Save"
                                OnClick="btnModifySaveLeave_Click" ValidationGroup="ModifyAdd" />
                            &nbsp;
                            <asp:Button ID="btnModifyCancelLeave" runat="server" CssClass="ButtonControl" Text="Cancel"
                                OnClick="btnModifyCancelLeave_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button1" Style="display: none" runat="server" Text="Button" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddZone" runat="server" TargetControlID="btnBulkUpload"
        PopupControlID="pnlAddZone" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="Button1">
    </ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="mpeModifyZone" runat="server" TargetControlID="btnDummyModify"
        PopupControlID="pnlModify" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnModifyCancelLeave">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Button ID="Button2" Style="display: none" runat="server" Text="Button" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddNewEntry" runat="server" TargetControlID="Button2"
        PopupControlID="pnlAddNewEntry" BackgroundCssClass="modalBackground" Enabled="true">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Button ID="Button4" Style="display: none" runat="server" Text="Button" />
    <ajaxToolkit:ModalPopupExtender ID="mpeLeaveCut" runat="server" TargetControlID="Button4"
        PopupControlID="pnlLeaveCut" BackgroundCssClass="modalBackground" Enabled="true">
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
