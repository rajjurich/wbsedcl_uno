<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" CodeBehind="AccessLevelReport.aspx.cs" Inherits="UNO.AccessLevelReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css">
        .style37
        {
            width: 111px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
      
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:Panel ID="Header" runat="server">
        <div id="Content" style="margin-left:38% ;border:0px solid;width: 250px;">
            <table id="tblHead" style="text-align:center; height: 93px; width: 250px;">
            <tr>
                <td class="style37" style="text-align:center">
                   Description
                </td>         
                <td class="style1">                
              <%--     <asp:TextBox ID="txtDescription" runat="server" Width="113px"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddDescription" runat="server"  class="chosen-select">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="Reqdescription" runat="server" ControlToValidate="ddDescription"
                    Display="none" ErrorMessage="Please enter Description" SetFocusOnError="True"
                    InitialValue="" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>

                <ajaxToolkit:ValidatorCalloutExtender ID="VCEddleaveType" runat="server" TargetControlID="Reqdescription"
                    PopupPosition="Right">
                </ajaxToolkit:ValidatorCalloutExtender>

                </td>
                <td style="text-align:center" class="style37">
                    <asp:Button ID="btnView" runat="server" ValidationGroup="Add" Text="View" onclick="btnView_Click" CssClass="ButtonControl"/>
                </td>    
                <td style="text-align:center">
                    <asp:Button ID="btnReset" runat="server" Text="Reset" Visible="false" CssClass="ButtonControl" 
                        onclick="btnReset_Click"/>
                </td>
            
            </tr>
      
            </table>
        </div>
        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>

      
        <asp:Panel ID="Body" runat="server">
        <div style="margin-left:15%; margin-top:1%; border:1px solid black;Width:70%">             
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
            </rsweb:ReportViewer>    
        </div>
        </asp:Panel>
        </div>
        <script src="Scripts/Choosen/chosen.jquery.js" type="text/javascript"></script>
    <script src="Scripts/Choosen/docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "95%" }
            //            chosen:showing_dropdown
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
  </script>
</asp:Content>
