<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="AcsPointBrowse.aspx.cs" Inherits="UNO.AcsPointBrowse" %>

<%@ Register Assembly="DemoGrid" Namespace="DemoGrid.GridViewControl" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--        <link href="Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function returnviewmode() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";
            window.location = "AcsPointBrowse.aspx";
        }
        function clearFunction() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";

        }

        function handleDelete() {
            if (!validateCheckBoxes())
                return false;
            //          return CheckOne(document.getElementById('gvHolidayView'));
            // return  validateCheckBoxes();
            //          if (!Page_ClientValidate())
            //              return;


            var msg = confirm("Record(s) marked for Deletion. Continue? ");
            if (msg == false) {
                clearGridCheckBoxes();
                return false;
            }
        }

        function clearGridCheckBoxes() {
            var gridRef = document.getElementById('<%= Grid.ClientID %>');
            var inputElementArray = gridRef.getElementsByTagName('input');
            var cntchk = 0;
            for (var i = 0; i < inputElementArray.length; i++) {
                var elementRef = inputElementArray[i];

                if ((elementRef.type == 'checkbox')) {
                    elementRef.checked = false;
                }
            }
        }

        function CheckedFocus() {

            var isValid = false;
            var gridView = document.getElementById('<%= Grid.ClientID %>');
            var inputs = gridView.getElementsByTagName('input');
            for (var i = 0; i < inputs.length; i++) {
                var elementRef = inputs[i];

                if ((elementRef.type == 'checkbox') && (elementRef.checked == true)) {
                    isValid = true;

                }
            }
            if (isValid == true)
            { document.getElementById('btnDelete').focus(); }
            else
            { document.getElementById('btnAdd').focus(); }

        }


        function validateCheckBoxes() {

            var isValid = false;

            var gridView = document.getElementById('<%= Grid.ClientID %>');

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
    <table id="Table1" width="100%" border="0" runat="server">
        <tr>
            <td colspan="3" align="center">
                <h3 class="heading">
                    Access Point View
                </h3>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;" class="LinkControl" colspan="3">
                <asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="~/AccessDashboard.aspx"
                    ForeColor="Blue">Back to Access Management Dash Board</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td width="15%">
                <asp:Button runat="server" ID="btnAdd" Text="New" Width="90%" CssClass="ButtonControl"
                    OnClick="btnAdd_Click" ClientIDMode="Static" TabIndex="1" />
            </td>
            <td width="15%">
                <asp:Button runat="server" ID="btnDelete" Text="Delete" Width="90%" CssClass="ButtonControl"
                    OnClick="btnDelete_Click" OnClientClick="return handleDelete()" ClientIDMode="Static"
                    TabIndex="2" />
            </td>
            <td width="55%" align="right" class="LinkControl">
            </td>
        </tr>
    </table>
    <table id="Table4" width="100%" border="0" runat="server">
        <tr>
            <td style="height: 10px; width: 15%; text-align: right">
                Id :
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
    <table style="width: 100%; height: 335px;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <br />
                <asp:Panel ID="pnlGrdView" runat="server" ScrollBars="Auto" Width="100%" Height="230px"
                    CssClass="srcColor">
                    <cc1:GridView ID="Grid" runat="server" AutoGenerateColumns="False" Width="98%" CssClass="tablestyle"
                        AllowSorting="True" AllowPaging="True" OnDataBound="Grid_DataBound" OnPageIndexChanging="Grid_PageIndexChanging"
                        CellPadding="0" OnRowDataBound="Grid_RowDataBound" OnRowUpdating="Grid_RowUpdating"
                        OnPreRender="Grid_PreRender" OnSelectedIndexChanged="Grid_SelectedIndexChanged"
                        OnSorting="Grid_Sorting" DataKeyNames="AP_ID" ClientIDMode="Static" PageSize="5"
                        GridLines="None">
                        <HeaderStyle CssClass="headerstyle" />
                        <RowStyle CssClass="rowstyle" Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <EmptyDataRowStyle BackColor="#edf5ff" Height="300px" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="altrowstyle" Wrap="True" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select" AccessibleHeaderText="Select" SortExpression="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="DeleteRows" runat="server" Onclick="CheckedFocus()" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" AccessibleHeaderText="Edit" SortExpression="Edit">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("AP_ID", "EditAcsPoint.aspx?id={0}") %>'
                                        Text="Edit" ForeColor="#3366FF"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AP_ID" HeaderText="ID" SortExpression="ID">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AP_DESCRIPTION" HeaderText="Description" SortExpression="Description">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AP_TYPE" HeaderText="Type" SortExpression="Type"></asp:BoundField>
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
                        <SortedAscendingHeaderStyle ForeColor="White" />
                    </cc1:GridView>
                </asp:Panel>
                <div style="margin-top: 5px">
                    <table>
                        <tr>
                            <td>
                                <asp:DataPager ID="pager" runat="server" PagedControlID="Grid">
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
                            <td>
                                <asp:Label ID="lblPageCount" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table class="MessageContainerTable" width="98.8%">
                    <tr>
                        <td colspan="4" align="center" valign="middle">
                            <div id="messageDiv" runat="server" clientidmode="Static" class="MessageClass">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
