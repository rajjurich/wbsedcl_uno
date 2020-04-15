<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="UserAccessPermissionEdit.aspx.cs" Inherits="UNO.UserAccessPermissionEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function returnviewmode() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";
            window.location = "UserAccessPermissionBrowse.aspx";
        }
        function clearFunction() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";

        }

        function ValidateALListBox(sender, args) {
            var options = document.getElementById("<%=lstSAL.ClientID%>").options;
            if (options.length > 0) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }
        function ValidateALListBoxCount(sender, args) {
            var options = document.getElementById("<%=lstSAL.ClientID%>").options;
            if (options.length > 4) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }



        function handleAdd() {
            if (!Page_ClientValidate())
                return;

            var msg = confirm("Save Record?");

            if (msg == false) {
                return false;
            }
        }
    </script>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="table2" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                class="TableClass">
                <tr>
                    <td colspan="2" align="center" class="style23">
                        <h3 class="heading">
                            Employee Access Config</h3>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right" class="LinkControl">
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" OnClick="LinkButton1_Click"
                            class="LinkControl">Back to view mode</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="UAPTDClassLabel" style="height: 10px">
                        Entity :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px">
                        <asp:DropDownList ID="cmbEntity" runat="server" CssClass="ComboControl" AutoPostBack="True"
                            Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="UAPTDClassLabel">
                        Entity Specification :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="style25">
                        <table width="100%" style="height: 100px">
                            <tr>
                                <td class="TDClassControl">
                                    <asp:ListBox ID="lstAEntity" runat="server" Height="100px" Width="280px" ForeColor="Black"
                                        Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" Enabled="False">
                                    </asp:ListBox>
                                </td>
                                <td class="style11">
                                    <table>
                                        <tr>
                                            <td class="TDClassControl">
                                                <asp:Button ID="cmdEntityAllRight" runat="server" Text="&gt;&gt;" Width="19px" CssClass="ButtonControl"
                                                    CausesValidation="False" Visible="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TDClassControl">
                                                <asp:Button ID="cmdEntityRight" runat="server" Text="&gt;" Width="19px" CssClass="ButtonControl"
                                                    CausesValidation="False" Visible="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TDClassControl">
                                                <asp:Button ID="cmdEntityLeft" runat="server" Text="&lt;" Width="19px" CssClass="ButtonControl"
                                                    CausesValidation="False" Visible="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TDClassControl">
                                                <asp:Button ID="cmdEntityAllLeft" runat="server" Text="&lt;&lt;" Width="19px" CssClass="ButtonControl"
                                                    CausesValidation="False" Visible="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="TDClassControl">
                                    <asp:ListBox ID="lstSEntity" runat="server" CssClass="TextControl" Height="100px"
                                        Width="280px" ClientIDMode="Static" SelectionMode="Multiple" Visible="False">
                                    </asp:ListBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="UAPTDClassLabel" style="height: 10px">
                        Access Level :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px">
                        <table width="100%" style="height: 112px">
                            <tr>
                                <td class="style17">
                                    Available
                                </td>
                                <td class="style11">
                                </td>
                                <td>
                                    Selected
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassControl">
                                    <asp:ListBox ID="lstAAL" runat="server" Height="100px" Width="280px" ForeColor="Black"
                                        Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple">
                                    </asp:ListBox>
                                </td>
                                <td class="style11">
                                    <table>
                                        <tr>
                                            <td class="TDClassControl">
                                                <asp:Button ID="cmdALLALRight" runat="server" Text="&gt;&gt;" Width="19px" CssClass="ButtonControl"
                                                    CausesValidation="False" OnClick="cmdALLALRight_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TDClassControl">
                                                <asp:Button ID="cmdALRight" runat="server" Text="&gt;" Width="19px" CssClass="ButtonControl"
                                                    CausesValidation="False" OnClick="cmdALRight_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TDClassControl">
                                                <asp:Button ID="cmdALLeft" runat="server" Text="&lt;" Width="19px" CssClass="ButtonControl"
                                                    CausesValidation="False" OnClick="cmdALLeft_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TDClassControl">
                                                <asp:Button ID="cmdALLALLeft" runat="server" Text="&lt;&lt;" Width="19px" CssClass="ButtonControl"
                                                    CausesValidation="False" OnClick="cmdALLALLeft_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="TDClassControl">
                                    <asp:ListBox ID="lstSAL" runat="server" Height="100px" Width="280px" ForeColor="Black"
                                        Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple"
                                        AutoPostBack="True" CausesValidation="True"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style17">
                                </td>
                                <td class="style11">
                                </td>
                                <td>
                                    <asp:CustomValidator ID="CVAL" runat="server" ControlToValidate="lstSAL" Display="Dynamic"
                                        ForeColor="Red" ValidateEmptyText="True" ErrorMessage="Please select access level(s)."
                                        ClientValidationFunction="ValidateALListBox"></asp:CustomValidator>
                                    <br />
                                    <asp:CustomValidator ID="CVAL2" runat="server" ControlToValidate="lstSAL" Display="Dynamic"
                                        ForeColor="Red" ValidateEmptyText="True" ErrorMessage="You can select maximum 4 AL(s)."
                                        ClientValidationFunction="ValidateALListBoxCount"></asp:CustomValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <%--<tr>
    

	<td class = "UAPTDClassLabel" style="height: 10px"> 
        <asp:Button ID="CmdOk" runat="server" Text="Save" CssClass="ButtonControl" 
            onclick="CmdOk_Click" />
        </td>
	<td class = "TDClassControl" style="height: 10px">
        <asp:Button ID="CmdCancel" runat="server" Text="Cancel" 
            CssClass="ButtonControl" onclick="CmdCancel_Click" 
            CausesValidation="False" />
        </td>	
	</tr>

	<tr>
	<td class = "UAPTDClassLabel" style="height: 10px" colspan="2" align="center"> 
   <div id = "messageDiv" class = "MessageClass">    

    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
 </div> 
    </td>
	
	
	</tr>--%>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table id="table3" runat="server" cellpadding="3" cellspacing="3" width="95%">
        <tr>
            <td style="width: 70%">
            </td>
            <td>
                <asp:Button ID="CmdOk" runat="server" CssClass="ButtonControl" Text="Save" OnClientClick="return handleAdd()"
                    Width="100%" OnClick="CmdOk_Click" />
            </td>
            <td>
                <asp:Button ID="CmdCancel" runat="server" Text="Cancel" Width="100%" CssClass="ButtonControl"
                    TabIndex="54" CausesValidation="False" OnClick="CmdCancel_Click" />
                <%--   <asp:Button ID="btnCancel1" runat="server" CssClass="ButtonControl" 
                    onclick="btnCancel_Click" Text="Cancel" Width="80%" />--%>
            </td>
        </tr>
    </table>
    <table class="MessageContainerTable" width="98.8%">
        <tr>
            <td colspan="4" align="center" valign="middle">
                <div id="messageDiv" runat="server" clientidmode="Static" class="MessageClass">
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
