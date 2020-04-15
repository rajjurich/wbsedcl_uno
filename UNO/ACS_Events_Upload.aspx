<%@ Page Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" CodeBehind="ACS_Events_Upload.aspx.cs" Inherits="UNO.ACS_Events_Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnDummyAdd" Style="display: none" runat="server" Text="Button" />
    <asp:Button ID="btnDummyModify" Style="display: none" runat="server" Text="Button" />
    <asp:Button ID="btnDummyAddNewEntry" Style="display: none" runat="server" Text="Button" />
    <asp:Button runat="server" ID="tempbtn" Style="display: none;" />
    <br />
    <asp:Panel ID="pnlAddZone" runat="server" CssClass="PopupPanel" style="width:100%">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="table2" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%" class="TableClass">
                    <tr>
                        <td class="heading">
                            <asp:Label ID="lblBulkUploadAdd" runat="server" Text="ACS Event Bulk Upload"></asp:Label>
                        </td>
                   </tr> 
                   <tr>
                        <td style="height: 50px; text-align: center;">
                                <b>Please Select File: </b><asp:FileUpload ID="fileuploadExcel" runat="server" />
                        </td>
                   </tr>
                   <tr>
                        <td style="text-align: center;">
                               <asp:Button ID="btnImport" runat="server" CssClass="ButtonControl" OnClick="btnImport_Click" Text="Save" ValidationGroup="Add" />
                               <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" TabIndex="17" Text="Cancel" OnClick="btnCancelAdd_Click" CausesValidation="False" ValidationGroup="Add" />
                        </td>
                   </tr>
                   <tr>
                         <td style="text-align: center;">
                              <asp:Label ID="lblBulkErrorMessage" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                          </td>
                   </tr>
               </table>                        
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnImport" />                
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    
    <asp:Button ID="Button1" Style="display: none" runat="server" Text="Button" />
    <ajaxToolkit:ModalPopupExtender ID="mpeModifyZone" runat="server" TargetControlID="btnDummyModify"
        PopupControlID="pnlModify" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnModifyCancelLeave">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Button ID="Button2" Style="display: none" runat="server" Text="Button" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddNewEntry" runat="server" TargetControlID="Button2"
        PopupControlID="pnlAddNewEntry" BackgroundCssClass="modalBackground" Enabled="true">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Button ID="Button4" Style="display: none" runat="server" Text="Button" />
    <ajaxToolkit:ModalPopupExtender ID="mpeLeaveCut" runat="server" TargetControlID="Button4"
        PopupControlID="pnlLeaveCut" BackgroundCssClass="modalBackground" Enabled="true">
    </ajaxToolkit:ModalPopupExtender>
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