<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="AutoCardLookup.aspx.cs" Inherits="UNO.AutoCardLookup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
    <asp:Timer runat="server" ID="ctlTimer" Interval="2000" OnTick="OnTimerIntervalElapse" />
    <asp:UpdatePanel runat="server" ID="pnlUpdate">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ChkBuffer" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td colspan="4" align="center">
                        <h3 class="heading">
                            Auto Card Look Up
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
                                    <asp:DropDownList ID="cmbReaders" runat="server" CssClass="ComboControl" AutoPostBack="True"
                                        OnSelectedIndexChanged="cmbReaders_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassLabel" style="height: 10px">
                                    DateTime :
                                </td>
                                <td class="TDClassControl" style="height: 10px">
                                    <asp:Label ID="lblDateTime" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassLabel" style="height: 10px">
                                    Card No :
                                </td>
                                <td class="TDClassControl" style="height: 10px">
                                    <asp:Label ID="lblCardNo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassLabel" style="height: 10px">
                                    Name :
                                </td>
                                <td class="TDClassControl" style="height: 10px">
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassLabel" style="height: 10px">
                                    Card Status :
                                </td>
                                <td class="TDClassControl" style="height: 10px">
                                    <asp:Label ID="lblCardStatus" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px; padding-right: 5px;" align="right">
                                    <asp:CheckBox ID="ChkBuffer" runat="server" AutoPostBack="True" OnCheckedChanged="ChkBuffer_CheckedChanged"
                                        Text="Buffer" Style="font-size: 11px; font-weight: bold" />
                                </td>
                                <td align="left">
                                    <asp:Button ID="CmdNext" runat="server" CssClass="ButtonControl" Text="Next" Width="30%"
                                        OnClick="CmdNext_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <%-- <tr>
    <td >
    </td>
    <td colspan="3" width="75%">
        <table>
        <tr>
            <td class = "TDClassLabel" style="height: 10px"> <asp:CheckBox ID="chkIsBuffer" runat="server" Text="Buffer" /></td>
            <td align="left">
               <asp:Button ID="CmdNext" runat="server" CssClass="ButtonControl"  Text="Next"    Width="30%"  />
            </td>
        </tr>
        </table>
    </td>
    </tr>--%>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table class="MessageContainerTable" width="98.8%">
        <tr>
            <td colspan="4" align="center" valign="middle">
                <div id="messageDiv" runat="server" clientidmode="Static" class="MessageClass">
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
