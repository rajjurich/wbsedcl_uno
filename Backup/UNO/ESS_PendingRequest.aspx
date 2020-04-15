<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ESS_PendingRequest.aspx.cs" EnableEventValidation="false" Inherits="UNO.ESS_PendingRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DemoGrid" Namespace="DemoGrid.GridViewControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ValidateSearch() {

            var FromDate, ToDate;
            FromDate = document.getElementById('<%=txtFrom.ClientID%>');
            ToDate = document.getElementById('<%=txtTo.ClientID%>');
            if (FromDate.value == "") {
                alert('Please Enter From Date');
                document.getElementById('<%=txtFrom.ClientID%>').focus()
                return false;
            }
            if (ToDate.value == "") {
                alert('Please Enter To Date');
                document.getElementById('<%=txtTo.ClientID%>').focus()
                return false;
            }
            if (!CompareDates(FromDate, ToDate)) {
                alert("From Date must be less than or equals to To Date");
                document.getElementById('<%=txtFrom.ClientID%>').focus();
                return false;
            }
            return true;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Literal ID="heading" runat="server"><h3 class="heading" style="margin-bottom: 0px;">
        Applied Requests</h3></asp:Literal>
                    </td>
                    <td align="right" style="text-align: right">
                        <%--  <asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="~/ESS_Dashboard.aspx"
                    ForeColor="Blue">Back to Time Attendance  DashBoard</asp:HyperLink>--%>
                    </td>
                </tr>
            </table>
            <center>
                <div class="DivEmpDetails">
                    <table style="width: 100%; color: Black">
                        <tr>
                            <td style="float: left;">
                                <div id="hide" runat="server" clientidmode="Static">
                                    Entity Type
                                    <asp:DropDownList ID="ddlEntityType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEntityType_SelectedIndexChanged1">
                                        <asp:ListItem Value="1">Leave Application</asp:ListItem>
                                        <asp:ListItem Value="2">Manual Attendance</asp:ListItem>
                                        <asp:ListItem Value="3">OutDoor Duty</asp:ListItem>
                                        <asp:ListItem Value="4">Out-Pass</asp:ListItem>
                                        <asp:ListItem Value="5">Compensatory Off Application</asp:ListItem>
                                    </asp:DropDownList>
                                    Status
                                    <asp:DropDownList ID="ddlStatus" runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                        <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                        <%-- <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>        --%>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <div id="divSearch" runat="server">
                                    From Date
                                    <asp:TextBox ID="txtFrom" runat="server" ValidationGroup="a" onKeyPress="javascript: return false "
                                        onKeysown="javascript: return false "></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtfromDate_CalendarExtender" TargetControlID="txtFrom"
                                        PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvfromDate" runat="server" ControlToValidate="txtFrom"
                                        Display="none" ErrorMessage="Please enter From Date" SetFocusOnError="True" ForeColor="Red"
                                        ValidationGroup="a"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCEfrm_Date" runat="server" TargetControlID="rfvfromDate"
                                        PopupPosition="right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    To Date
                                    <asp:TextBox ID="txtTo" runat="server" ValidationGroup="a" onKeyPress="javascript: return false "
                                        onKeysown="javascript: return false "></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" TargetControlID="txtTo"
                                        PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvtoDate" runat="server" ControlToValidate="txtTo"
                                        Display="none" ErrorMessage="Please enter To Date" SetFocusOnError="True" ForeColor="Red"
                                        ValidationGroup="a"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                        TargetControlID="rfvtoDate" PopupPosition="right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </div>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="ButtonControl"
                                    OnClick="btnSearch_Click" OnClientClick=" return ValidateSearch();" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="5">
                                <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                                    <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="true" Width="100%"
                                            AllowPaging="True" PageSize="10" GridLines="None" OnPageIndexChanging="gvData_PageIndexChanging">
                                            <RowStyle CssClass="gvRow" />
                                            <PagerStyle ForeColor="Black" />
                                            <HeaderStyle CssClass="gvHeader" />
                                            <AlternatingRowStyle BackColor="#F0F0F0" />
                                            <EmptyDataTemplate>
                                                <div>
                                                    <span>No Records.</span>
                                                </div>
                                            </EmptyDataTemplate>
                                            <%-- <Columns>
                     
                      <asp:TemplateField HeaderText="Select" SortExpression="Select" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" ShowHeader="False" SortExpression="Edit" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                            Text="Edit" ForeColor="#3366FF"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="EmployeeCode" SortExpression="EmployeeCode">
                                    <ItemTemplate>
                                        <asp:Label ID="lblname" runat="server" Text='<%#Eval("Tday_empcde")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpCode" runat="server" Text='<%#Eval("name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="InTime" SortExpression="InTime">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFrmDt" runat="server" Text='<%#Eval("IN Time") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Out Date" SortExpression="Out Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblToDt" runat="server" Text='<%#Eval("OUT Time") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Late" SortExpression="Late">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStats" runat="server" Text='<%#Eval("Late By") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" SortExpression="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Work Hour" SortExpression="Work Hour">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMgrID" runat="server" Text='<%#Eval("Work Hr") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Extra Hour" SortExpression="Extra Hour">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExtraHr" runat="server" Text='<%#Eval("Extra Hr") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblleavecde" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                     </Columns>--%>
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
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="float: right">
                                <asp:ImageButton ID="imgExpotBtn" runat="server" ImageUrl="~/images/Excelicon.png"
                                    OnClick="imgExpotBtn_Click" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </div>
            </center>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgExpotBtn" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
