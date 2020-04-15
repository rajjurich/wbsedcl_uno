<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="AcsPointAdd.aspx.cs" Inherits="UNO.AcsPointAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <link href="Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }


        function handleAdd() {
            if (!Page_ClientValidate())
                return;

            var msg = confirm("Save Record?");

            if (msg == false) {
                return false;
            }
        }
        function returnviewmode() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";
            //window.location = "AcsPointBrowse.aspx";
        }
        function clearFunction() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";

        }
        function ValidateRDListBox1(sender, args) {
            var options = document.getElementById("<%=lstSReader.ClientID%>").options;
            if (options.length > 0) {
                args.IsValid = true;
            }
            else {
                sender.innerHTML = "Please select reader(s).";
                args.IsValid = false;
            }
        }
        function ValidateAPCombo(sender, args) {
            var ddlReport = document.getElementById("<%=cmbAccessPointType.ClientID%>");
            var Value = ddlReport.options[ddlReport.selectedIndex].value;
            if (Value == "-1") {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }
        function ValidateRDListBox(sender, args) {
            var ddlReport = document.getElementById("<%=cmbAccessPointType.ClientID%>");
            var Value = ddlReport.options[ddlReport.selectedIndex].value;


            var options = document.getElementById("<%=lstSReader.ClientID%>").options;
            if (options.length == 0) {
                sender.innerHTML = "Please select reader(s).";
                args.IsValid = false;
            }

            if (Value == "OROD" && document.getElementById("<%=lstSReader.ClientID%>").options.length < 1) {
                sender.innerHTML = "Please select one reader";
                //document.getElementById("<%=lstSReader.ClientID%>").focus();
                args.IsValid = false;
            }
            else if (Value == "OROD" && document.getElementById("<%=lstSReader.ClientID%>").options.length > 1) {
                sender.innerHTML = "You can select only one reader";
                // document.getElementById("<%=lstSReader.ClientID%>").focus();
                args.IsValid = false;
            }

            else if (Value == "TROD" && document.getElementById("<%=lstSReader.ClientID%>").options.length < 2) {
                sender.innerHTML = "Please select two reader";
                //document.getElementById("<%=lstSReader.ClientID%>").focus();
                args.IsValid = false;
            }
            else if (Value == "TROD" && document.getElementById("<%=lstSReader.ClientID%>").options.length > 2) {
                sender.innerHTML = "Please select only two reader";
                // document.getElementById("<%=lstSReader.ClientID%>").focus();
                args.IsValid = false;
            }
            else if (Value == "ROND" && document.getElementById("<%=lstSReader.ClientID%>").options.length < 1) {
                sender.innerHTML = "Please select one reader";
                //document.getElementById("<%=lstSReader.ClientID%>").focus();
                args.IsValid = false;
            }
            else if (Value == "ROND" && document.getElementById("<%=lstSReader.ClientID%>").options.length > 1) {
                sender.innerHTML = "Please select only one reader";
                // document.getElementById("<%=lstSReader.ClientID%>").focus();
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }


        function checkDValidation(sender, args) {

            var ddlReport = document.getElementById("<%=cmbAccessPointType.ClientID%>");
            var Value = ddlReport.options[ddlReport.selectedIndex].value;

            if (Value == "OROD" && document.getElementById("<%=lstSDoor.ClientID%>").options.length < 1) {
                sender.innerHTML = "Please select one door";
                args.IsValid = false;
            }
            else if (Value == "OROD" && document.getElementById("<%=lstSDoor.ClientID%>").options.length > 1) {
                sender.innerHTML = "Please select only one door";
                args.IsValid = false;
            }
            else if (Value == "TROD" && document.getElementById("<%=lstSDoor.ClientID%>").options.length < 1) {
                sender.innerHTML = "Please select one door";
                args.IsValid = false;
            }
            else if (Value == "TROD" && document.getElementById("<%=lstSDoor.ClientID%>").options.length > 1) {
                sender.innerHTML = "Please select only one door";
                args.IsValid = false;
            }
            else if (Value == "ROND" && document.getElementById("<%=lstSDoor.ClientID%>").options.length > 0) {
                sender.innerHTML = "Pleasr do not select any door";
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }


        
    </script>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table id="table2" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                class="TableClass">
                <tr>
                    <td colspan="2" align="center" style="height: 10px">
                        <h3 class="heading">
                            Access Point
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
                    <td class="TDClassLabel" style="height: 10px">
                        Access Point Id :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px">
                        <table>
                            <tr>
                                <td class="TDClassControl">
                                    <asp:TextBox CssClass="TextControl" ID="txtaccesspointid" MaxLength="15" runat="server"
                                        Style="text-transform: uppercase;" Width="174px" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="style20">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="TDClassLabel" style="height: 10px">
                        Description :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px">
                        <table>
                            <tr>
                                <td class="TDClassControl">
                                    <asp:TextBox CssClass="TextControl" ID="txtdescription" MaxLength="20" Style="text-transform: capitalize;"
                                        onkeypress="return IsAlphanumeric(event)" runat="server" Width="167px" ClientIDMode="Static"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdescription"
                                        ErrorMessage="Please enter Description." ForeColor="Red"></asp:RequiredFieldValidator>
                                    <br>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtdescription"
                                        Display="dynamic" ErrorMessage="Special characters are not allowed." ValidationExpression="[0-9a-zA-Z' ']{1,20}"
                                        ForeColor="Red" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="TDClassLabel" style="height: 10px">
                        Controller :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px">
                        &nbsp;<asp:DropDownList ID="cmbContrller" runat="server" CssClass="ComboControl" AutoPostBack="True"
                            OnSelectedIndexChanged="cmbContrller_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="cmbContrller"
                            Display="Dynamic" ErrorMessage="Please select Controller." ForeColor="Red" InitialValue="-1"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="TDClassLabel">
                        Access Point Type :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl">
                        &nbsp;<asp:DropDownList ID="cmbAccessPointType" runat="server" CssClass="ComboControl"
                            AutoPostBack="True" OnSelectedIndexChanged="cmbAccessPointType_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:CustomValidator ID="CVtype" runat="server" ErrorMessage="Please select Access Point Type."
                            ClientValidationFunction="ValidateAPCombo" ForeColor="Red"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td class="TDClassLabel" style="height: 10px">
                        Reader :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px">
                        <table  style="height: 112px">
                            <tr>
                                <td class="style17" >
                                    Available
                                </td>
                                <td class="style11" style=" width:15px;">
                                </td>
                                <td>
                                    Selected
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassControl">
                                    <asp:ListBox ID="lstAReader" runat="server" Height="74px" Width="215px" CssClass="TextControl"
                                        ClientIDMode="Static" SelectionMode="Multiple"></asp:ListBox>
                                </td>
                                <td style="width:15px;">
                                    <table>
                                        <tr>
                                            <td class="TDClassControl">
                                                &nbsp;<asp:Button ID="cmdReaderRight" runat="server" OnClick="cmdReaderRight_Click"
                                                    Text="&gt;" Width="19px" CssClass="ButtonControl" CausesValidation="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TDClassControl">
                                                &nbsp;<asp:Button ID="cmdReaderLeft" runat="server" Text="&lt;" Width="19px" OnClick="cmdReaderLeft_Click"
                                                    CssClass="ButtonControl" CausesValidation="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="TDClassControl">
                                    <asp:ListBox ID="lstSReader" runat="server" Width="230px" CssClass="TextControl"
                                        Height="74px" ClientIDMode="Static" SelectionMode="Multiple" AutoPostBack="True"
                                        CausesValidation="True"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center">
                                    <asp:CustomValidator ID="CVLSTReader" runat="server" ControlToValidate="lstSReader"
                                        Display="Dynamic" ForeColor="Red" ValidateEmptyText="True" ClientValidationFunction="ValidateRDListBox"></asp:CustomValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="TDClassLabel" style="height: 10px">
                        Door :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px">
                        <table width="100%" style="height: 112px">
                            <tr>
                                <td class="style18">
                                    Available
                                </td>
                                <td class="style15">
                                </td>
                                <td>
                                    Selected
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClassControl">
                                    <asp:ListBox ID="lstADoor" runat="server" Height="74px" Width="211px" CssClass="TextControl"
                                        ClientIDMode="Static" SelectionMode="Multiple"></asp:ListBox>
                                </td>
                                <td class="style14">
                                    <table>
                                        <tr>
                                            <td class="TDClassControl">
                                                &nbsp;<asp:Button ID="CmdDoorRight" runat="server" OnClick="CmdDoorRight_Click" Text="&gt;"
                                                    Width="20px" CssClass="ButtonControl" CausesValidation="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TDClassControl">
                                                &nbsp;<asp:Button ID="CmdDoorLeft" runat="server" OnClick="CmdDoorLeft_Click" Text="&lt;"
                                                    Width="20px" CssClass="ButtonControl" CausesValidation="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="TDClassControl">
                                    <asp:ListBox ID="lstSDoor" runat="server" Width="230px" CssClass="TextControl" Height="74px"
                                        SelectionMode="Multiple" ClientIDMode="Static"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center">
                                    <asp:CustomValidator ID="CVDoor" runat="server" ForeColor="Red" ClientValidationFunction="checkDValidation"></asp:CustomValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
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
            width: 15px;
        }
        .style2
        {
            width: 217px;
        }
    </style>
</asp:Content>
