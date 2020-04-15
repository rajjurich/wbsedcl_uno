<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ScheduleSettingView.aspx.cs" Inherits="UNO.ScheduleSettingView" %>

<%@ Register Assembly="DemoGrid" Namespace="DemoGrid.GridViewControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function clearFunction(x) {
            document.getElementById(x).innerHTML = "&nbsp;&nbsp;";
        }

        function handleDelete() {

            if (!validateCheckBoxes())
                return false;

            var msg = confirm("Record(s) marked for Deletion. Continue?");
            if (msg == false) {
                clearGridCheckBoxes();
                return false;
            }
        }

        function clearGridCheckBoxes() {
            var gridRef = document.getElementById('<%= gvScheduleSettings.ClientID %>');
            var inputElementArray = gridRef.getElementsByTagName('input');
            var cntchk = 0;
            for (var i = 0; i < inputElementArray.length; i++) {
                var elementRef = inputElementArray[i];

                if ((elementRef.type == 'checkbox')) {
                    elementRef.checked = false;
                }
            }
        }
        function validateCheckFocus() {

            var isValid = false;

            var gridView = document.getElementById('<%= gvScheduleSettings.ClientID %>');

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
        function validateCheckBoxes() {

            var isValid = false;

            var gridView = document.getElementById('<%= gvScheduleSettings.ClientID %>');

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

    </script>
    <table id="table2" runat="server" width="100%" height="10%" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td>
                <h3 class="heading">
                    Scheduler Settings</h3>
            </td>
        </tr>
    </table>
    <table id="table3" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
        class="TableClass">
        <tr>
            <td align="right" style="width: 33%" class="LinkControl">
                <asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="~/AccessDashboard.aspx"
                    ForeColor="Blue">Back to Access Management Dash Board</asp:HyperLink>
            </td>
        </tr>
    </table>
    <table id="Table1" width="100%" border="0" runat="server">
        <tr width="100%">
            <td width="15%">
                <asp:Button runat="server" ID="btnAdd" Text="New" Width="90%" CssClass="ButtonControl"
                    OnClick="btnAdd_Click" />
            </td>
            <td width="15%">
                <asp:Button runat="server" ID="btnDelete" Text="Delete" Width="90%" CssClass="ButtonControl"
                    OnClick="btnDelete_Click" OnClientClick="return handleDelete()" />
            </td>
            <td width="15%">
                <asp:Button runat="server" ID="btnSearch" Text="Search" Width="90%" CssClass="ButtonControl"
                    Visible="false" />
            </td>
            <td width="55%">
            </td>
        </tr>
    </table>
    <table id="Table4" width="100%" border="0" runat="server">
        <tr>
            <td style="height: 10px; width: 15%; text-align: right">
                Task Type :
            </td>
            <td width="20%">
                <asp:TextBox ID="txtCompanyId" runat="server" Width="95px" CssClass="TextBox"></asp:TextBox>
            </td>
            <td class="style37" style="text-align: right; width: 15%;">
                Description :
            </td>
            <td style="width: 20%;">
                <asp:TextBox ID="txtCompanyName" runat="server" Width="116px" CssClass="TextBox"></asp:TextBox>
            </td>
            <td width="15%">
                <asp:Button runat="server" ID="cmdSearch" Text="Search" Width="90%" CssClass="ButtonControl"
                    ClientIDMode="Static" OnClick="cmdSearch_Click" />
            </td>
            <td width="15%">
                <asp:Button runat="server" ID="cmdReset" Text="Reset" Width="90%" CssClass="ButtonControl"
                    OnClick="cmdReset_Click" />
            </td>
        </tr>
    </table>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <br />
                        <asp:Panel ID="pnlGrdView" runat="server" ScrollBars="Auto" Width="100%" Height="170px"
                            HorizontalAlign="Center" align="Center" CssClass="srcColor">
                            <cc1:GridView ID="gvScheduleSettings" runat="server" AutoGenerateColumns="False"
                                Width="98%" AllowSorting="True" AllowPaging="true" OnDataBound="gvScheduleSettings_DataBound"
                                OnPageIndexChanging="gvScheduleSettings_PageIndexChanging" OnRowEditing="gvScheduleSettings_RowEditing"
                                BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" PageSize="5" GridLines="None">
                                <HeaderStyle CssClass="headerstyle" />
                                <RowStyle CssClass="rowstyle" Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <EmptyDataRowStyle BackColor="#edf5ff" Height="300px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="altrowstyle" Wrap="True" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Select" SortExpression="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="DeleteRows" Width="8%" ClientIDMode="Static" Onclick="validateCheckFocus()"
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" ShowHeader="False" SortExpression="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                Text="Edit" ForeColor="#3366FF"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SCHEDULER_TASK_TYPE" HeaderText="Scheduler Task Type"
                                        SortExpression="Scheduler Task Type">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SCHEDULER_DESCRIPTION" HeaderText="Scheduler Description"
                                        SortExpression="Controller Description">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SCHEDULER_FREQUENCY" HeaderText="Scheduler Frequency"
                                        SortExpression="Scheduler Frequency">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SCHEDULER_TIME" HeaderText="Scheduler Time" SortExpression="Scheduler Time">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                                <PagerTemplate>
                                    <%--<table width="100%">
                                        <tr>
                                            <td style="text-align: left">
                                                Page Size :
                                                <asp:DropDownList ID="ddPageSize" runat="server" EnableViewState="true" OnSelectedIndexChanged="ddPageSize_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Text="5"></asp:ListItem>
                                                    <asp:ListItem Text="10"></asp:ListItem>
                                                    <asp:ListItem Text="15"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblPageCount" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>--%>
                                </PagerTemplate>
                            </cc1:GridView>
                        </asp:Panel>
                        <div style="margin-top: 5px">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <asp:DataPager ID="pager" runat="server" PagedControlID="gvScheduleSettings">
                                            <Fields>
                                                <asp:NextPreviousPagerField FirstPageText="&lt;&lt;" LastPageText="&gt;&gt;" NextPageText="&gt;"
                                                    PreviousPageText="&lt;" ShowFirstPageButton="True" ShowNextPageButton="False"
                                                    ButtonCssClass="datapager" />
                                                <asp:NumericPagerField ButtonCount="10" NumericButtonCssClass="datapager" CurrentPageLabelCssClass="datapager" />
                                                <asp:NextPreviousPagerField LastPageText="&gt;&gt;" NextPageText="&gt;" ShowLastPageButton="True"
                                                    ShowPreviousPageButton="False" ButtonCssClass="datapager" />
                                            </Fields>
                                        </asp:DataPager>
                                        
                                    </td>
                                    <td style="width:70px; text-align:right;" >
                                        <asp:Label ID="lblPageCount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table class="MessageContainerTable" width="98.8%">
        <tr>
            <td colspan="4" align="center" valign="middle">
                <div id="messageDiv" runat="server" clientidmode="Static" class="MessageClass">
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
