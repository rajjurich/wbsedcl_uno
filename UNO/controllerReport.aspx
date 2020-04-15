<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="controllerReport.aspx.cs" Inherits="UNO.controllerReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            color: Black;
        }
    </style>
    <script src="Scripts/Validation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: 10px">
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Header" runat="server">
                    <div id="Content" style="margin-left: 30%; border: 1px solid; width: 374px;" align="center">
                        <table id="tblHead" style="text-align: center; height: 93px; width: 465px;">
                            <tr>
                                <td class="style1" align="left">
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rdbtnControlId" runat="server" GroupName="A" AutoPostBack="True"
                                        OnCheckedChanged="rdbtnControlId_CheckedChanged" />Controller ID
                                </td>
                                <td class="style1">
                                    <%--   <asp:DropDownList ID="ddlCobntrolId" runat="server">
                </asp:DropDownList>
                                    --%>
                                    <asp:TextBox ID="txtControlId" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="style1">
                                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" CssClass="ButtonControl" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style1" align="left">
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rdbtnControlName" runat="server" GroupName="A" AutoPostBack="True"
                                        OnCheckedChanged="rdbtnControlName_CheckedChanged" />Controller Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtControlName" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnReset" runat="server" Text="Close" OnClientClick="navigateToUrl('Uno_Dashboard.aspx');return false;"
                                        CssClass="ButtonControl" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="height: 14px;">
        </div>
        <div align="center" style="width: 100%;">
            <asp:Panel ID="Panel1" runat="server" Height="410px" BorderStyle="Solid" BorderColor="#0066FF"
                BorderWidth="3px" Width="70%" Visible="true">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                    InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                    BorderStyle="None" Visible="False" Width="100%">
                </rsweb:ReportViewer>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
