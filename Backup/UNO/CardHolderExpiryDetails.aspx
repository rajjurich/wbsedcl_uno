<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" CodeBehind="CardHolderExpiryDetails.aspx.cs" Inherits="UNO.CardHolderExpiryDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div style="height:70px">
    </div>
    <div>
      
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:Panel ID="Header" runat="server">
        <div id="Content" style="margin-left:35% ;border:1px solid;width: 350px;">
            <table id="tblHead" cellspacing="3px" style="text-align:center; height: 93px; width: 350px;">
            <tr>
                <td>
                    <asp:RadioButton ID="rdbtnEmployeeWise" runat="server" Text="Employee Wise  " GroupName='B'/>
                    &nbsp;&nbsp;&nbsp;
                </td>   
                <td>
                    <asp:RadioButton ID="rdbtnReaderise" runat="server" Text="Reader Wise" GroupName='B'/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="style37" style="text-align:center">
                    <asp:RadioButton ID="rdbtnEmpCode" runat="server" Text="Employee Code  " 
                        oncheckedchanged="rdbtnEmpCode_CheckedChanged" GroupName="a" 
                        AutoPostBack="True"/>
                    &nbsp;&nbsp;
                </td>         
                <td class="style1">                
                   <asp:TextBox ID="txtEmpCode" runat="server" Width="113px"></asp:TextBox>
                </td>
                <td style="text-align:center" class="style37">
                    <asp:Button ID="btnView" runat="server" Text="View" onclick="btnView_Click" CssClass="ButtonControl"/>
                </td>
            </tr>
            <tr>
                <td class="style37" style="text-align:center">
                    <asp:RadioButton ID="rdbtnEmpName" runat="server" Text="Employee Name" 
                        oncheckedchanged="rdbtnEmpName_CheckedChanged" GroupName="a" 
                        AutoPostBack="True" /> 
                    &nbsp;&nbsp; 
                </td>         
                <td class="style1">                
                   <asp:TextBox ID="txtName" runat="server" Width="113px"></asp:TextBox>

                </td>
                <td style="text-align:center">
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" 
                        onclick="btnReset_Click"/>
                </td>
            </tr>
            <tr>
                <td>From: <asp:TextBox ID="txtFrom" runat="server" Width="105px"></asp:TextBox>
                <asp:CalendarExtender ID="txtfromDate_CalendarExtender" 
             TargetControlID="txtFrom" PopupButtonID="txtshiftStartDate"
                            runat="server" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                </td>
                <td>TO: <asp:TextBox ID="txtTo" runat="server" Width="105px"></asp:TextBox>
                <asp:CalendarExtender ID="txtToDate_CalendarExtender" 
                  TargetControlID="txtTo" PopupButtonID="txtshiftStartDate"
                            runat="server" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                </td>
                <td>
                </td>
            </tr>
            
            </table>
        </div>
        </asp:Panel>
        
      
        <asp:Panel ID="Body" runat="server">
        <div style="margin-left:10%; margin-top:1%; border:1px solid black;Width:72%">             
          
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" >
            </rsweb:ReportViewer>
            <rsweb:ReportViewer ID="ReportViewer2" runat="server" Width="100%" >
            </rsweb:ReportViewer>
        </div>
        </asp:Panel>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>



    </div>
</asp:Content>
