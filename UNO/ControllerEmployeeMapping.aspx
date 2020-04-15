<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ControllerEmployeeMapping.aspx.cs" Inherits="UNO.ControllerEmployeeMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnMain" runat="server">
        <ContentTemplate>
            <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                        <asp:Label ID="lblHead" runat="server" Text="Controller Employee Mapping" ForeColor="RoyalBlue"
                            Font-Size="20px" Width="100%" CssClass="heading">
                        </asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <center>
                <div class="DivEmpDetails">
                    <table style="width: 100%;" border="0">
                        <tr>
                            <table id="ControllerEmployeeMappingTable">
                                <thead>
                                    <tr>
                                        <th>
                                            Sr
                                        </th>
                                        <th>
                                            Employee Code
                                        </th>
                                        <th>
                                            Employee Name
                                        </th>
                                        <th>
                                            Controller Id
                                        </th>
                                        <th>
                                            Controller Description
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                        </tr>
                    </table>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ControllerEmployeeMappingTable').DataTable({
                "columns": [
                                  { "data": "RowNum", "orderable": false, "searchable": false }
                                , { "data": "EmpCode" }
                                , { "data": "EmpName" }
                                , { "data": "ControllerId" }
                                , { "data": "ControllerDescription" }
                                ]
                            , bServerSide: true
                            , sAjaxSource: 'filter/ControllerEmployeeService.asmx/GetControllerEmployeeMapping'
                            , sServerMethod: 'POST'
            });
        });
       
    </script>
</asp:Content>
