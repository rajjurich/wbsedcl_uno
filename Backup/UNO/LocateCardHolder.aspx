<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="LocateCardHolder.aspx.cs" Inherits="UNO.LocateCardHolder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
       <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>

    <script language="javascript" type="text/javascript">

        function clearFunctionMessageDiv() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";

        }

    </script>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
    <asp:UpdatePanel runat="server" ID="pnlUpdate">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td colspan="4" align="center">
                        <h3 class="heading">
                            Locate Card Holder
                        </h3>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="right">
                        <asp:HyperLink ID="HyperLink1" CssClass="LinkControl" runat="server" NavigateUrl="~/AccessDashboard.aspx"
                            ForeColor="Blue">Back to Access Management Dash Board</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Image ID="ImgPhoto" runat="server" ImageUrl="~/EmpImage/default.png" />
                    </td>
                    <td colspan="3" width="75%">
                        <table>
                            <tr>
                                <td class="TDClassLabel" style="height: 10px">
                                    Select Criteria :
                                </td>
                                <td class="TDClassControl" style="height: 10px">
                                    <asp:DropDownList ID="cmbCriteria" runat="server" class="chosen-select" AutoPostBack="True"
                                        OnSelectedIndexChanged="cmbCriteria_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="cmbCriteria"
                                        Display="Dynamic" ErrorMessage="Please select Criteria." ForeColor="Red" InitialValue="-1"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassLabel" style="height: 10px">
                                    Employee/Card Code :
                                </td>
                                <td class="TDClassControl" style="height: 10px">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox CssClass="TextControl" ID="txtCode" MaxLength="20" runat="server" Width="167px"
                                                    ClientIDMode="Static" TabIndex="1"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="CmdSearch" runat="server" CssClass="ButtonControl" Text="Search"
                                                    OnClick="CmdSearch_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassLabel" style="height: 10px">
                                    Employee Code :
                                </td>
                                <td class="TDClassControl" style="height: 10px">
                                    <asp:Label ID="lblEmployeeCode" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassLabel" style="height: 10px">
                                    Card Code :
                                </td>
                                <td class="TDClassControl" style="height: 10px">
                                    <asp:Label ID="lblCardNo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassLabel" style="height: 10px">
                                    Card Holder Name :
                                </td>
                                <td class="TDClassControl" style="height: 10px">
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassLabel" style="height: 10px">
                                    Mobile No :
                                </td>
                                <td class="TDClassControl" style="height: 10px">
                                    <asp:Label ID="lblMobileNo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassLabel" style="height: 10px">
                                    Marital Status :
                                </td>
                                <td class="TDClassControl" style="height: 10px">
                                    <asp:Label ID="lblMaritalStatus" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassLabel" style="height: 10px">
                                    Last Reader swiped :
                                </td>
                                <td class="TDClassControl" style="height: 10px">
                                    <asp:Label ID="lblLastReaderswiped" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID = "cmbCriteria" />
        </Triggers>
    </asp:UpdatePanel>
    <table class="MessageContainerTable" width="98.8%">
        <tr>
            <td colspan="4" align="center" valign="middle">
                <div id="messageDiv" runat="server" clientidmode="Static" class="MessageClass">
                </div>
            </td>
        </tr>
    </table>

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
