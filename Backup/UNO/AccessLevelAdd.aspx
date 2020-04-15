<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="AccessLevelAdd.aspx.cs" Inherits="UNO.AccessLevelAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<link href="Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function returnviewmode() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";
            //window.location = "AcsPointBrowse.aspx";
        }
        function clearFunction() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";

        }

        function PushAlert(msg) {
            alert(msg);

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
                    <td colspan="3" align="center">
                        <h3 class="heading">
                            Access Level
                        </h3>
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
                        Access Level Id :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px">
                        <table>
                            <tr>
                                <td class="TDClassControl">
                                    <asp:TextBox CssClass="TextControl" ID="txtalid" MaxLength="15" runat="server" Style="text-transform: capitalize;"
                                        Width="174px" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="style20">
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
                                        ErrorMessage="Please enter description." ForeColor="Red"></asp:RequiredFieldValidator>
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
                        Entity Type :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td style="height: 10px" align="left">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left" valign="top" class="TDClassControl">
                                    <asp:RadioButtonList ID="RBLZone" runat="server" CssClass="ComboControl" Width="80px"
                                        OnSelectedIndexChanged="RBLZone_SelectedIndexChanged" AutoPostBack="True" Height="100%"
                                        TabIndex="2">
                                        <asp:ListItem Value="Z">Zone</asp:ListItem>
                                        <asp:ListItem Value="R">Reader</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="left" valign="top" class="style2">
                                    <asp:Panel ID="PnlZone" runat="server" HorizontalAlign="Left" Width="263px">
                                        <table>
                                            <tr>
                                                <td class="UAPTDClassLabel">
                                                    <asp:Label ID="lblZone" runat="server">Zone:</asp:Label>
                                                </td>
                                                <td class="TDClassControl" style="height: 10px">
                                                    <asp:DropDownList ID="cmbZone" runat="server" AutoPostBack="True" CssClass="ComboControl"
                                                        OnSelectedIndexChanged="cmbZone_SelectedIndexChanged" Height="17px" Width="194px"
                                                        TabIndex="3">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="cmbZone"
                                                        Display="Dynamic" ErrorMessage="Please select zone" ForeColor="Red" InitialValue="-1"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="UAPTDClassLabel" style="height: 10px">
                    </td>
                    <td class="TDClassControl" style="height: 10px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Plese Select entity type."
                            ControlToValidate="RBLZone" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="UAPTDClassLabel" style="height: 10px">
                        Reader :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px">
                        <table width="100%" style="height: 112px">
                            <tr>
                                <td>
                                    <asp:Label ID="lblAvailable" runat="server">Available</asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblSelected" runat="server">Selected</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassControl">
                                    <asp:ListBox ID="lstAReader" runat="server" Height="90px" Width="280px" ForeColor="Black"
                                        Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple"
                                        TabIndex="4"></asp:ListBox>
                                </td>
                                <td class="style11">
                                    <table>
                                        <tr>
                                            <td>
                                                &nbsp;<asp:Button ID="cmdReaderRight" runat="server" Text="&gt;" Width="19px" CssClass="ButtonControl"
                                                    CausesValidation="False" OnClick="cmdReaderRight_Click" TabIndex="5" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;<asp:Button ID="cmdReaderLeft" runat="server" Text="&lt;" Width="19px" CssClass="ButtonControl"
                                                    CausesValidation="False" OnClick="cmdReaderLeft_Click" TabIndex="6" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="TDClassControl">
                                    <asp:ListBox ID="lstSReader" runat="server" Height="90px" Width="280px" ForeColor="Black"
                                        Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple"
                                        AutoPostBack="True" CausesValidation="True" TabIndex="7"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:CustomValidator ID="CVReaders" runat="server" ControlToValidate="lstSReader"
                                        Display="Dynamic" ForeColor="Red" ValidateEmptyText="True" ErrorMessage="Please select reader(s)."
                                        OnServerValidate="CVReaders_ServerValidate"></asp:CustomValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="UAPTDClassLabel" style="height: 10px">
                        TimeZone :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px">
                        &nbsp;<asp:DropDownList ID="cmbTimeZone" runat="server" CssClass="ComboControl" Width="171px"
                            AutoPostBack="True" TabIndex="8">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbTimeZone"
                            Display="Dynamic" ErrorMessage="Please select time zone" ForeColor="Red" InitialValue="-1"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <%--<tr>
    

	<td class = "UAPTDClassLabel" style="height: 10px"> 
        <asp:Button ID="CmdOk" runat="server" Text="Ok" CssClass="ButtonControl" onclick="CmdOk_Click" 
            />
        </td>
	<td class = "TDClassControl" style="height: 10px">
        <asp:Button ID="CmdCancel" runat="server" Text="Back" 
            CssClass="ButtonControl"  
            CausesValidation="False" onclick="CmdCancel_Click" />
        &nbsp;</td>	
	</tr>

	<tr>
	<td class = "UAPTDClassLabel" style="height: 10px" colspan="2" align="center"> 
   <div id = "messageDiv" class = "MessageClass">    

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
                    Text="Save" OnClientClick="return handleAdd()" Width="100%" TabIndex="9" />
            </td>
            <td>
                <asp:Button ID="CmdCancel" runat="server" Text="Cancel" Width="100%" CssClass="ButtonControl"
                    TabIndex="10" CausesValidation="False" OnClick="CmdCancel_Click" />
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
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style1
        {
            width: 177px;
        }
        .style2
        {
            width: 217px;
        }
    </style>
</asp:Content>
