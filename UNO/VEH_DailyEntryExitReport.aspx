<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" CodeBehind="VEH_DailyEntryExitReport.aspx.cs" Inherits="UNO.UNO_Log.VEH_DailyEntryExitReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style type="text/css">
 .style28
        {
            width: 90%;
        }

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    


<table align="center" style="width: 90%; margin-left: 10%;">
    
        <tr>
            <td style="text-align: right" class="style28">
       <rsweb:reportviewer ID="ReportViewer1" runat="server" Width="80%" Font-Names="Verdana"
                    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                    WaitMessageFont-Size="14pt" BorderStyle="Solid">
</rsweb:reportviewer>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style28" align="center">
             
                <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
    </table>     



</asp:Content>
