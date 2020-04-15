<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ZoneEdit.aspx.cs" Inherits="UNO.ZoneEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function returnviewmode() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";
            window.location = "ZoneBrowse.aspx";
        }
        function clearFunction() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";

        }
        function handleAdd() {
            if (!Page_ClientValidate())
                return;

            var msg = confirm("Save Record?");

            if (msg == false) {
                return false;
            }
        }

        function ValidateRDListBox(sender, args) {
            var options = document.getElementById("<%=lstSReader.ClientID%>").options;
            if (options.length == 0) {
                sender.innerHTML = "Please select reader(s).";
                args.IsValid = false;
            }
            else { args.IsValid = true; }
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
                            Zone</h3>
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
                        Zone Id :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px">
                        <table>
                            <tr>
                                <td class="TDClassControl">
                                    <asp:TextBox CssClass="TextControl" ID="txtzoneid" MaxLength="15" runat="server"
                                        Style="text-transform: uppercase;" Width="174px" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="style20">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtzoneid"
                                        ErrorMessage="Zone id is required." ForeColor="Red"></asp:RequiredFieldValidator>
                                    <br>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtzoneid"
                                        Display="dynamic" ErrorMessage="Please enter 1- 15 valid characters." ValidationExpression="[0-9a-zA-Z]{1,15}"
                                        ForeColor="Red" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="UAPTDClassLabel" style="height: 10px">
                        Description :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px">
                        <table>
                            <tr>
                                <td class="TDClassControl">
                                    <asp:TextBox CssClass="TextControl" ID="txtdescription" Style="text-transform: capitalize;"
                                        MaxLength="20" runat="server" Width="167px" ClientIDMode="Static" TabIndex="1"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdescription"
                                        ErrorMessage="Please enter Description." ForeColor="Red"></asp:RequiredFieldValidator>
                                    <br>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtdescription"
                                        Display="dynamic" ErrorMessage="Special characters are not allowed." ValidationExpression="[0-9a-zA-Z' ']{1,20}"
                                        ForeColor="Red" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="UAPTDClassLabel" style="height: 10px">
                        Reader :<label class="CompulsaryLabel">*</label>
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
                                    <asp:ListBox ID="lstAReader" runat="server" Height="90px" Width="280px" ForeColor="Black"
                                        Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple"
                                        TabIndex="2"></asp:ListBox>
                                </td>
                                <td class="style11">
                                    <table>
                                        <tr>
                                            <td class="TDClassControl">
                                                <asp:Button ID="cmdReaderRight" runat="server" Text="&gt;" Width="19px" CssClass="ButtonControl"
                                                    CausesValidation="False" OnClick="cmdReaderRight_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TDClassControl">
                                                <asp:Button ID="cmdReaderLeft" runat="server" Text="&lt;" Width="19px" CssClass="ButtonControl"
                                                    CausesValidation="False" OnClick="cmdReaderLeft_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="TDClassControl">
                                    <asp:ListBox ID="lstSReader" runat="server" Height="90px" Width="280px" ForeColor="Black"
                                        Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple">
                                    </asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                                <td>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="lstSReader"
                                        ErrorMessage="Please select reader(s)." ForeColor="Red" ClientValidationFunction="ValidateRDListBox"
                                        Display="Dynamic" ValidateEmptyText="True"></asp:CustomValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <%--<tr>
    

	<td class = "UAPTDClassLabel" style="height: 10px"> 
        <asp:Button ID="CmdOk" runat="server" Text="Ok" CssClass="ButtonControl" 
            onclick="CmdOk_Click" />
        </td>
	<td class = "TDClassControl" style="height: 10px">
        <asp:Button ID="CmdCancel" runat="server" Text="Cancel" 
            CssClass="ButtonControl" onclick="CmdCancel_Click" 
            CausesValidation="False" />
        </td>	
	</tr>
                --%>
                <%--<tr>
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
                <asp:Button ID="CmdOk" runat="server" CssClass="ButtonControl" OnClick="CmdOk_Click"
                    Text="Save" OnClientClick="return handleAdd()" Width="100%" />
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
