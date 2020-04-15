<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ModuleMain.master"
    CodeBehind="Controller_Master.aspx.cs" Inherits="UNO.Controller_Master" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="center" style="width: 100%;text-align:center;">
        <tr>
            <td>
                <h2>
                    Controller Report</h2>
            </td>
        </tr>
    </table>
    <table align="center">
        <tr>
            <td style:align="left">
            </td>
        </tr>
    </table>
    <table align="center" style="width:100%;">
        <tr>
            <td style="text-align:center; padding-left:50%;">
                <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>--%>
                <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="ButtonControl" OnClick="Close_Click"
                    Width="71px" Height="23px" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center;" class="style24">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="543px" Font-Names="Verdana"
                    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                    WaitMessageFont-Size="14pt" BorderStyle="Solid" Style="margin-left: 25%;">
                    <LocalReport ReportPath="">
                    </LocalReport>
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .ButtonControl
        {
        }
        .style24
        {
            width: 453px;
        }
    </style>
</asp:Content>
