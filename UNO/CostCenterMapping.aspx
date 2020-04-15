<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="CostCenterMapping.aspx.cs" Inherits="UNO.CostCenterMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CancelImport() {
            $find('mpeBAddZone').hide();
            $('#' + ["<%=lblMessages.ClientID%>", "<%=lblBulkErrorMessage.ClientID%>"].join(', #')).prop('innerHTML', "");
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnMain" runat="server">
        <ContentTemplate>
            <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                        <asp:Label ID="lblHead" runat="server" Text="Cost Center Mapping" ForeColor="RoyalBlue"
                            Font-Size="20px" Width="100%" CssClass="heading">
                        </asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <center>
                <div>
                    <asp:UpdatePanel ID="upnlMessage" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass=""></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="DivEmpDetails">
                    <table style="width: 100%;" border="0">
                        <tr>
                            <td style="width: 50%; text-align: left;">
                                <asp:Button runat="server" ID="btnBulkUpload" Text="Bulk Upload" CssClass="ButtonControl"
                                    Enabled="true" />
                            </td>
                        </tr>
                        <tr>
                            <table id="costCenterMappingTable">
                                <thead>
                                    <tr>
                                        <th>
                                            Sr
                                        </th>
                                        <th>
                                            Company
                                        </th>
                                        <th>
                                            Head Quarter
                                        </th>
                                        <th>
                                            Head Quarter Code
                                        </th>
                                        <th>
                                            Zone
                                        </th>
                                        <th>
                                            Zone Code
                                        </th>
                                        <th>
                                            Region
                                        </th>
                                        <th>
                                            Region Code
                                        </th>
                                        <th>
                                            Profit Center
                                        </th>
                                        <th>
                                            Profit Center Code
                                        </th>
                                        <th>
                                            Cost Center Code
                                        </th>
                                        <th>
                                            Cost Center
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                        </tr>
                    </table>
                </div>
            </center>
            <asp:Panel ID="pnlAddZone" runat="server" CssClass="PopupPanel">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table id="table2" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                            <tr>
                                <td>
                                    <table class="TableClass" width="100%">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td class="heading">
                                                    <asp:Label ID="lblBulkUploadAdd" runat="server" Text="Upload Cost Center Mapping"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px; text-align: center;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px; text-align: left; color: Black;">
                                                    <b>Download Template : </b>
                                                    <asp:Button ID="btnDownloadExcel" runat="server" Text="Click Here" BackColor="White"
                                                        OnClick="btnDownloadExcel_Click" CssClass="ButtonControl" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px; text-align: center;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 50px; text-align: center;">
                                                    <b>Please Select Excel File: </b>
                                                    <asp:FileUpload ID="fileuploadExcel" runat="server" />
                                                    <asp:RequiredFieldValidator ID="rfvFileupload" runat="server" ErrorMessage="required"
                                                        ForeColor="Red" ControlToValidate="fileuploadExcel" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:Button ID="btnImport" runat="server" CssClass="ButtonControl" OnClick="btnImport_Click"
                                                        Text="Save" ValidationGroup="Add" />
                                                    <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" TabIndex="17"
                                                        Text="Cancel" OnClientClick="return CancelImport();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblBulkErrorMessage" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        </td> </tr> </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnImport" />
                        <asp:PostBackTrigger ControlID="btnDownloadExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Button ID="Button1" Style="display: none" runat="server" Text="Button" />
            <ajaxToolkit:ModalPopupExtender ID="mpeAddZone" runat="server" TargetControlID="btnBulkUpload"
                PopupControlID="pnlAddZone" BackgroundCssClass="modalBackground" Enabled="true"
                BehaviorID="mpeBAddZone" CancelControlID="Button1">
            </ajaxToolkit:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#costCenterMappingTable').DataTable({
                "columns": [
                                  { "data": "RowNum", "orderable": false, "searchable": false }
                                , { "data": "Company" }
                                , { "data": "HeadQuarter" }
                                , { "data": "HeadQuarterCode" }
                                , { "data": "Zone" }
                                , { "data": "ZoneCode" }
                                , { "data": "Region" }
                                , { "data": "RegionCode" }
                                , { "data": "ProfitCenter" }
                                , { "data": "ProfitCenterCode" }
                                , { "data": "CostCenterCode" }
                                , { "data": "CostCenter" }
                                ]
                            , bServerSide: true
                            , sAjaxSource: 'filter/CostCenterService.asmx/GetCostCenterMapping'
                            , sServerMethod: 'POST'
            });
        });
       
    </script>
</asp:Content>
