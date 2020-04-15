<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="HierarchyManagerView.aspx.cs" Inherits="UNO.HierarchyManagerView" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvEmpMgrHier.ClientID %>');

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
        function returnviewmode() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";

        }
        function clearFunction() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";

        }

        function ResetAll() {
            $('#' + ["<%=txtCompanyId.ClientID%>", "<%=txtCompanyName.ClientID%>"].join(', #')).prop('value', "");
            $('#' + ["<%=txtCompanyId.ClientID%>"]).focus();
            $('#' + ["<%=txtCompanyName.ClientID%>"]).focus();
            document.getElementById('<%=lblErrorEdit.ClientID%>').innerHTML = "";
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvEmpMgrHier.ClientID%>').focus();
          //  return false;
        }
        function AssignName() {
            var SelectedVal = $('#' + '<%=lstManager.ClientID %>').find('option:selected').text();
            $('#' + '<%=txtManagerName.ClientID%>').val(SelectedVal);
            return false;
        }
       
    </script>
    <style type="text/css">
        .display
        {
            display: none;
        }
        .style38
        {
        }
        
        .style39
        {
            width: 35px;
        }
        .style41
        {
            width: 142px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnMain" runat="server">
        <ContentTemplate>
            <div align="center">
                <asp:Label ID="Label1" runat="server" Text="Employee Hierarchy Configuration View"
                    ForeColor="RoyalBlue" Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </div>
            <div class="DivEmpDetails">
                <table style="width: 100%;" border="0">
                    <tr>
                        <td style="text-align: left; width: 5%;">
                            <asp:Button runat="server" ID="btnAdd" Text="New" Width="90%" CssClass="ButtonControl"
                                OnClick="btnAdd_Click" />
                        </td>
                        <td style="text-align: left; width: 13%;">
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" Width="40%" CssClass="ButtonControl"
                                OnClick="btnDelete_Click" />
                            <asp:Button runat="server" ID="btnUpload" Width="40%" Text="Upload" CssClass="ButtonControl"
                                OnClick="btnUpload_Click" Enabled="true" />
                        </td>
                        <td style="text-align: right; width: 40%;">
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                                OnClientClick="return ResetAll();" OnClick="btnReset_Click" />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;"
                                CssClass="ButtonControl" OnClick="btnSearch_Click" />
                            <asp:TextBox ID="txtCompanyName" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtUserID" runat="server" TargetControlID="txtCompanyName"
                                WatermarkText="Name" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:TextBox ID="txtCompanyId" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtLevelID" runat="server" TargetControlID="txtCompanyId"
                                WatermarkText="Employee ID" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #EFF8FE; padding: 0px;" colspan="3">
                            <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                                <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvEmpMgrHier" runat="server" AutoGenerateColumns="false" Width="100%"
                                        GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="gvEmpMgrHier_RowCommand">
                                        <RowStyle CssClass="gvRow" />
                                        <HeaderStyle CssClass="gvHeader" />
                                        <AlternatingRowStyle BackColor="#F0F0F0" />
                                        <PagerStyle CssClass="gvPager" />
                                        <EmptyDataTemplate>
                                            <div>
                                                <span>No records found.</span>
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" SortExpression="Select" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ShowHeader="true" SortExpression="Edit" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Modify" CommandArgument='<%#Eval("Rowid")%>'
                                                        CausesValidation="False" Text="Edit" ForeColor="#3366FF"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="empid" HeaderText="Employee ID" ItemStyle-Width="15%">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EmpName" HeaderText="Name">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Mgr_id" HeaderText="Manager ID">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ManagerName" HeaderText="Manager Name">
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
                                        <SortedAscendingHeaderStyle ForeColor="White" />
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
            <div align="center">
                <asp:Label ID="lblErrorEdit" runat="server" CssClass="ErrorLabel" Text=""></asp:Label>
            </div>
            <div style="text-align: center;">
                <asp:Label ID="lbltext" runat="server" Text="Text" Visible="false" CssClass="ErrorLabel"></asp:Label>
            </div>
            <center>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            Visible="false" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" BorderWidth="1" BorderStyle="Solid">
                        </rsweb:ReportViewer>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </center>
            <div>
                <asp:UpdatePanel ID="up1" runat="server">
                    <ContentTemplate>
                        <ajaxToolkit:ModalPopupExtender ID="mpeEmpMgrHierarchy" runat="server" TargetControlID="Button1"
                            PopupControlID="pnlEdit" BackgroundCssClass="modalBackground" Enabled="true"
                            OkControlID="Button1" CancelControlID="btnCancelAdd">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlEdit" runat="server" CssClass="PopupPanel">
                            <table style="width: 95%">
                                </tr>
                                <tr>
                                    <td class="style41">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style41" align="right">
                                        <asp:Label ID="lblEmpId" runat="server" Font-Bold="True" Text="Employee ID:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmpID" runat="server" Style="margin-left: 1px" ValidationGroup="Edit"
                                            Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style41">
                                        &nbsp;
                                    </td>
                                    <td class="style39">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style41" align="right">
                                        <asp:Label ID="lblEmpName" runat="server" Font-Bold="True" Text="Employee Name :"></asp:Label>
                                    </td>
                                    <td class="style39">
                                        <asp:TextBox ID="txtEmployeeName" runat="server" Style="margin-left: 1px" ValidationGroup="Edit"
                                            Enabled="False" Width="241px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style41">
                                        &nbsp;
                                    </td>
                                    <td class="style39">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style41" align="right">
                                        <asp:Label ID="lblDescriptionEdit1" runat="server" Font-Bold="True" Text="Select Manager :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtManagerName" runat="server" ClientIDMode="Static" Height="22px"
                                            onkeyup="return SearchList();" Width="244px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style41">
                                        &nbsp;
                                    </td>
                                    <td class="style39">
                                        <asp:ListBox ID="lstManager" runat="server" ClientIDMode="Static" Font-Bold="True"
                                            Font-Names="Courier New" ForeColor="Black" Height="101px" Rows="10" SelectionMode="Single"
                                            Width="244px" AutoPostBack="True" class="chosen-select" onchange=" return AssignName();">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="style38" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="style38" colspan="2">
                                        &nbsp;
                                        <asp:Button ID="Button1" runat="server" Style="display: none;" Text="test" />
                                        <asp:Button ID="btnSubmitAdd" runat="server" CssClass="ButtonControl" OnClick="btnSubmitAdd_Click"
                                            Text="Save" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" Text="Cancel" />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div align="center">
                                            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Text=""></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:AsyncPostBackTrigger ControlID = "" />--%>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <asp:Panel ID="pnlUpload" runat="server" CssClass="PopupPanel" Style="height: 35%;
                width: 50%">
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
                                                    <asp:Label ID="lblUpload" runat="server" Text="Employee Hierarchy Upload">
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
                                                    <asp:Button ID="btnDownloadExcel" runat="server" Text="Click Here" BackColor="White"
                                                        CssClass="ButtonControl" OnClick="btnDownloadExcel_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px; text-align: center;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 50px; text-align: center;">
                                                    <b>Please Select Excel File: </b>
                                                    <asp:FileUpload ID="fileUpload" runat="server" />
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
                                                    <asp:Button ID="Button5" runat="server" CssClass="ButtonControl" TabIndex="17" Text="Cancel"
                                                        OnClick="btnCancelLeaveCut_Click" CausesValidation="False" ValidationGroup="AddLeaveCut" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblUploadCutError" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
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
                        <asp:PostBackTrigger ControlID="btnDownloadExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Button ID="Button4" Style="display: none" runat="server" Text="Button" />
            <ajaxToolkit:ModalPopupExtender ID="mpeUpload" runat="server" TargetControlID="Button4"
                PopupControlID="pnlUpload" BackgroundCssClass="modalBackground" Enabled="true">
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
