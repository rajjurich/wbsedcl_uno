<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" CodeBehind="VEH_ReportofRFIDtagInventory.aspx.cs" Inherits="UNO.VEH_ReportofRFIDtagInventory" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
 .style28
        {
            width: 90%;
        }

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                                <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="RFID Tag Inventory" ForeColor="RoyalBlue" Font-Size="20px"
                    Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>


</ContentTemplate>
<Triggers>

</Triggers>

</asp:UpdatePanel>    



<table align="center" style="width: 100%;">
    <tr>
            <td align="center">
             Select Status : <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true"
                    onselectedindexchanged="ddlStatus_SelectedIndexChanged">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Issued</asp:ListItem>

                <asp:ListItem>New</asp:ListItem>
                <asp:ListItem>Kill</asp:ListItem>
                </asp:DropDownList>
              </td>
           
        </tr>
        <tr>
            <td align="center">
       <rsweb:reportviewer ID="ReportViewer1" runat="server" Width="62%" Font-Names="Verdana"
                    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                    WaitMessageFont-Size="14pt" BorderStyle="Solid">
    </rsweb:reportviewer>
            </td>
           
        </tr>
        <tr>
            <td align="center">
             
                <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
    </table>        


</asp:Content>
