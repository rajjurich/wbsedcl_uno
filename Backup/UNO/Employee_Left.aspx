<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="Employee_Left.aspx.cs" Inherits="UNO.Employee_Left" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css" />
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
        .ajax__validatorcallout_popup_table
        {
            left: 20px;
            top: 0px;
        }
        
        #ContentPlaceHolder1_ContentPlaceHolder1_vcelstEmployees_popupTable
        {
            left: 20px;
            top: 0px;
        }
    </style>
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript">

        function ValidateInput() {
            if ($("#<%= lstEmployees.ClientID %> option:selected").val() == "" || $("#<%= lstEmployees.ClientID %>").val() == null || $("#<%= lstEmployees.ClientID %> option:selected").val() == "Select") {
                $("#<%= lstEmployees.ClientID %>").focus();
                //                $("#<%= lblErrorSingleEntry.ClientID %>").css("display", "block");
                //                $("#<%= lblErrorSingleEntry.ClientID %>").innerHTML = "Please select Employee";
                alert('Please select Employee');
                return false;
            }

            if ($("#<%= txtCalendarFrom.ClientID %>").val() == "") {
                $("#<%= txtCalendarFrom.ClientID %>").focus();
                //                $("#<%= lblErrorSingleEntry.ClientID %>").css("display", "block");
                //                $("#<%= lblErrorSingleEntry.ClientID %>").innerHTML = "Please enter From Date";
                alert('Please enter From Date');
                return false;
            }

            if ($("#<%= ddlReasonAdd.ClientID %> option:selected").val() == "Select") {
                $("#<%= ddlReasonAdd.ClientID %>").focus();
                //                $("#<%= lblErrorSingleEntry.ClientID %>").css("display", "block");
                //                $("#<%= lblErrorSingleEntry.ClientID %>").innerHTML = "Please select Reason";
                alert('Please select Reason');
                return false;
            }

            return true;
        }

        function CancelImport() {
            $find('mpeBAddZone').hide();
            $('#' + ["<%=lblMessages.ClientID%>", "<%=lblBulkErrorMessage.ClientID%>"].join(', #')).prop('innerHTML', "");
            return false;
        }

        function CancelModify() {
            $find('mpeBModifyZone').hide();
            return false;
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
        function ResetAll() {
            $('#' + ["<%=txtSearchEmployeeid.ClientID%>"].join(', #')).prop('value', "");
            document.getElementById('<%=txtSearchEmployeeid.ClientID%>').focus();
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvLeaveData.ClientID%>').focus();
           // return false;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnMain" runat="server">
        <ContentTemplate>
            <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                        <asp:Label ID="lblHead" runat="server" Text="Employee Left" ForeColor="RoyalBlue"
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
                                <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                                    ClientIDMode="Static" OnClick="btnDelete_Click" />
                                <asp:Button runat="server" ID="btnBulkUpload" Text="Bulk Upload" CssClass="ButtonControl"
                                    OnClick="btnBulkUpload_Click" Enabled="true" />
                            </td>
                            <td style="width: 50%; text-align: right;">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                                    OnClientClick="return ResetAll();" OnClick="btnReset_Click" />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;"
                                    CssClass="ButtonControl" OnClick="btnSearch_Click" />
                                <asp:TextBox ID="txtSearchLeaveCode" runat="server" Style="float: right;" CssClass="searchTextBox"
                                    Visible="False"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtzonename" runat="server" TargetControlID="txtSearchLeaveCode"
                                    WatermarkText="Search by Employee ID" WatermarkCssClass="watermark">
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
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Modify" CommandArgument='<%# Eval("EL_ColumnID") %>'
                                                                ForeColor="#3366FF">Edit</asp:LinkButton>
                                                            <asp:HiddenField ID="hdnColId" runat="server" Value='<%#Eval("EL_ColumnID") %>' />
                                                             <asp:HiddenField ID="hdnEmpID" runat="server" Value='<%#Eval("EL_EMP_ID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EL_EMP_ID" HeaderText="Employee ID" SortExpression="Emp ID"
                                                        ItemStyle-Width="25%">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EL_LEFT_DATE" HeaderText="RETIREMENT DATE" SortExpression="RETIREMENT DATE"
                                                        ItemStyle-Width="25%">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EL_REASONID" HeaderText="REASON" SortExpression="REASON"
                                                        ItemStyle-Width="25%">
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
                                </td>
                            </asp:Panel>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:UpdatePanel ID="upnlMessage" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div>
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="150px" Width="600px"
                                Visible="false">
                            </rsweb:ReportViewer>
                        </div>
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
                        <table id="table2" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                            <tr>
                                <td>
                                    <table class="TableClass" width="100%">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td class="heading">
                                                    <asp:Label ID="lblBulkUploadAdd" runat="server" Text="Employee Left Bulk Upload"></asp:Label>
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
                                                        Text="Cancel" OnClientClick="return CancelImport();" />
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
            <asp:Panel ID="pnlAddNewEntry" runat="server" CssClass="PopupPanel">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <table class="TableClass">
                            <tr>
                                <td>
                                    <table class="TableClass" border="0">
                                        <tr>
                                            <td class="heading" colspan="2" align="center">
                                                <asp:Label ID="lblLeaveSingleEntry" runat="server" Text="Add Employee">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblEmp" runat="server" Text="Select Employee :  "></asp:Label>
                                                <label class="CompulsaryLabel" style="padding-right: 20px;">
                                                    *</label>
                                                <asp:TextBox ID="txtSearchBox" onkeyup="return SearchList();" runat="server" Height="23px"
                                                    ClientIDMode="Static" Width="157px" Visible="false"></asp:TextBox>
                                                <asp:ListBox ID="lstEmployees" runat="server" ClientIDMode="Static" Font-Names="Courier New"
                                                    ForeColor="Black" Rows="10" SelectionMode="Single" Width="200px" Height="85px"
                                                    AutoPostBack="false" class="chosen-select"></asp:ListBox>
                                                <%--<asp:RequiredFieldValidator ID="rfvlstEmployees" runat="server" ErrorMessage="Please select Employee."
                                                    ControlToValidate="lstEmployees" Display="None" ValidationGroup="ADD">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcelstEmployees" runat="server" TargetControlID="rfvlstEmployees"
                                                    PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblLeaveCode" runat="server" Text="Select Left Date :"></asp:Label>
                                                <label class="CompulsaryLabel" style="padding-right: 25px;">
                                                    *</label>
                                                <asp:TextBox ID="txtCalendarFrom" onKeyPress="javascript: return false " runat="server"
                                                    ClientIDMode="Static" CssClass="TextControl" MaxLength="10" Width="23%"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCalendarFrom"
                                                    PopupButtonID="txtCalendarFrom" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <%--<asp:RequiredFieldValidator ID="RFVtxtCalendarFrom" runat="server" ErrorMessage="Please select Left Date."
                                                    ControlToValidate="txtCalendarFrom" Display="None" ValidationGroup="ADD"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                    TargetControlID="RFVtxtCalendarFrom" PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Select Reason :"></asp:Label>
                                                <label class="CompulsaryLabel" style="padding-right: 25px;">
                                                    *</label>
                                                <asp:DropDownList ID="ddlReasonAdd" runat="server" ClientIDMode="Static" class="chosen-select"
                                                    Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="visibility: hidden;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Button ID="btnSubmitNewEntry" runat="server" CssClass="ButtonControl" Text="Save"
                                                    ValidationGroup="ADD" OnClick="btnSubmitNewEntry_Onclick" CausesValidation="true"
                                                    OnClientClick="return ValidateInput();" />
                                                &nbsp;
                                                <asp:Button ID="btnAddCancelEntry" runat="server" CssClass="ButtonControl" Text="Cancel"
                                                    CausesValidation="false" OnClick="btnAddCancelEntry_Onclick" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblErrorSingleEntry" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
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
            <asp:Panel ID="pnlModify" runat="server" CssClass="PopupPanel">
                <asp:UpdatePanel ID="updatepnlEditLeaveData" runat="server">
                    <ContentTemplate>
                        <table class="TableClass" border="0">
                            <tr>
                                <td class="heading" colspan="2" align="center">
                                    <asp:Label ID="lblEditLeave" runat="server" Text="Edit Employee"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                    <%--<asp:Label ID="lblID" runat="server"  ClientIDMode="Static" Width="157px" ></asp:Label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblEditEmployee" runat="server" Text="Employee:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="txtEditEmployeeName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblEditLeaveType" runat="server" Text="Select Left Date :" Style="vertical-align: top;"></asp:Label>
                                    <label class="CompulsaryLabel">
                                        *</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" ClientIDMode="Static" onKeyPress="javascript: return false "
                                        CssClass="TextControl" MaxLength="10" Width="35%"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox1"
                                        PopupButtonID="TextBox1" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RFVTextBox1" runat="server" ErrorMessage="Please select Left Date."
                                        ControlToValidate="TextBox1" Display="None" ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                        TargetControlID="RFVTextBox1" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label2" runat="server" Text="Select Reason :" Style="vertical-align: top;"></asp:Label>
                                    <label class="CompulsaryLabel">
                                        *</label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReasonEdit" runat="server" ClientIDMode="Static" class="chosen-select"
                                        Width="200px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="visibility: hidden;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;" colspan="2">
                                    <asp:Button ID="btnModifySaveLeave" runat="server" CssClass="ButtonControl" OnClick="btnModifySaveLeave_Click"
                                        Text="Save" ValidationGroup="ModifyAdd" />
                                    &nbsp;
                                    <asp:Button ID="btnModifyCancelLeave" runat="server" CssClass="ButtonControl" Text="Cancel"
                                        OnClientClick="return CancelModify();" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 10px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;" colspan="2">
                                    <asp:Label ID="lblEditError" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
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
                BehaviorID="mpeBAddZone" CancelControlID="Button1">
            </ajaxToolkit:ModalPopupExtender>
            <ajaxToolkit:ModalPopupExtender ID="mpeModifyZone" runat="server" TargetControlID="btnDummyModify"
                PopupControlID="pnlModify" BackgroundCssClass="modalBackground" Enabled="true"
                CancelControlID="btnModifyCancelLeave" BehaviorID="mpeBModifyZone">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Button ID="Button2" Style="display: none" runat="server" Text="Button" />
            <ajaxToolkit:ModalPopupExtender ID="mpeAddNewEntry" runat="server" TargetControlID="Button2"
                PopupControlID="pnlAddNewEntry" BackgroundCssClass="modalBackground" Enabled="true">
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
