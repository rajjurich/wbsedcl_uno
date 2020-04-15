<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="AutoScheduledBackupConfig.aspx.cs" Inherits="UNO.AutoScheduledBackupConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .cssVEh
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <%--<link href="Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ValidateData() {

            if (handleAdd() == false) {
                return false
            }
        }

        function handleAdd() {

            if (!Page_ClientValidate("AddNew")) {
              
                return;
            }
            var msg = confirm("Save Record?");
            if (msg == false) {
                return false;
            }
        }
        function returnviewmode() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";
            window.location = "TADashboard.aspx";
        }



        function clearFunctionMessageDiv() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";

        }

        function ValidateDays(sender, args) {

            if (document.getElementById("<%= ChkSunday.ClientID %>").checked == false &&
                        document.getElementById("<%= ChkMonday.ClientID %>").checked == false &&
                        document.getElementById("<%= ChkTuesday.ClientID %>").checked == false &&
                        document.getElementById("<%= ChkWednesday.ClientID %>").checked == false &&
                        document.getElementById("<%= ChkThursday.ClientID %>").checked == false &&
                        document.getElementById("<%= ChkFriday.ClientID %>").checked == false &&
                        document.getElementById("<%= ChkSaturday.ClientID %>").checked == false
                    ) {
                sender.innerHTML = "Please select Days(s).";
                args.IsValid = false;

            }
            else {
                args.IsValid = true;
            }
        }

        function findspace(evnt) {

            var keyASCII = (evnt.which) ? evnt.which : event.keyCode;
            var keyValue = String.fromCharCode(keyASCII);

            if (!(keyASCII >= '48' && keyASCII <= '57')) {
                window.event.keyCode = 0;
            }
        }
        function fnColon(ctrl, e) {
            var unicode = e.keyCode
            if (unicode != 8) {
                if (ctrl.getAttribute && ctrl.value.length == 2) {
                    ctrl.value = ctrl.value + ":";
                }
            }
        }
        function reset() {
            document.getElementById("txtPassword").text = "";
            document.getElementById("txtPasswordConfirm").text = "";
        }
        function showDirectory() {
            var popupWidth = 270;
            var popupHeight = 250;
            var left = (screen.width / 2) - (popupWidth / 2);
            var top = (screen.height / 2) - (popupHeight / 2);
            var txtSQLBackupPath = '<%=txtSQLBackupPath.ClientID %>';
            window.open('DirectoryPopup.aspx?txtSQLBackupPathID=' + txtSQLBackupPath + '', 'BackupPath', 'toolbar=no, location=no, directories=no,status=no, menubar=no, scrollbars=YES, resizable=0, copyhistory=no, width=' + popupWidth + ', height=' + popupHeight + ', top=' + top + ', left=' + left);
            return false;
        }
        function showDirectoryManual() {
            var popupWidth = 270;
            var popupHeight = 250;
            var left = (screen.width / 2) - (popupWidth / 2);
            var top = (screen.height / 2) - (popupHeight / 2);
            var txtSQLBackupPath = '<%=txtmanuPath.ClientID %>';
            window.open('DirectoryPopup.aspx?txtSQLBackupPathID=' + txtSQLBackupPath + '', 'BackupPath', 'toolbar=no, location=no, directories=no,status=no, menubar=no, scrollbars=YES, resizable=0, copyhistory=no, width=' + popupWidth + ', height=' + popupHeight + ', top=' + top + ', left=' + left);
            return false;
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
                            Auto-scheduled backup Configuration</h3>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px;">
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px;">
                    </td>
                </tr>
                <tr>
                    <td class="TDClassLabel" style="height: 10px; width: 50%">
                        Password :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px; width: 50%; margin-left: 40px;">
                        <asp:TextBox ID="txtPassword" runat="server" MaxLength="50" CssClass="TextControl"
                            TabIndex="1" TextMode="Password">
                        </asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassword"
                            Display="None" ErrorMessage="Please enter password." ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                        <%--//changes made by shrinth on 12/Sept/2014 Start--%>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtPassword" runat="server" TargetControlID="RequiredFieldValidator4"
                            PopupPosition="Right">
                        </ajaxToolkit:ValidatorCalloutExtender>
                        <%--//changes made by shrinth on 12/Sept/2014 END--%>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px;">
                    </td>
                </tr>
                <tr>
                    <td class="TDClassLabel" style="height: 10px; width: 50%">
                        Confirm Password :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px; width: 50%; margin-left: 40px;">
                        <asp:TextBox ID="txtPasswordConfirm" runat="server" MaxLength="100" CssClass="TextControl"
                            TextMode="Password" ClientIDMode="Static" TabIndex="2" ValidationGroup="AddNew"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPasswordConfirm"
                            Display="None" ErrorMessage="Please enter password." ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password not matched"
                            ControlToCompare="txtPassword" ControlToValidate="txtPasswordConfirm" Display="None"></asp:CompareValidator>
                        <%--//changes made by shrinth on 12/Sept/2014 Start--%>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtSqlServerName" runat="server" TargetControlID="RequiredFieldValidator2"
                            PopupPosition="Right">
                        </ajaxToolkit:ValidatorCalloutExtender>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                            TargetControlID="CompareValidator1" PopupPosition="Right">
                        </ajaxToolkit:ValidatorCalloutExtender>
                        <%--//changes made by shrinth on 12/Sept/2014 END--%>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px;">
                    </td>
                </tr>
                <%-- changes in alignment by Shrinith 12/Sept/2014   Start--%>
                <tr>
                    <td class="TDClassLabel" style="height: 10px; width: 50%">
                        Select Days of week :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px; width: 50%; margin-left: 40px;">
                        <table>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="ChkSunday" runat="server" Text="Sunday" ClientIDMode="Static" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="ChkMonday" runat="server" Text="Monday" ClientIDMode="Static" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="ChkTuesday" runat="server" Text="Tuesday" ClientIDMode="Static" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="ChkWednesday" runat="server" Text="Wednesday" ClientIDMode="Static" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="ChkThursday" runat="server" Text="Thursday" ClientIDMode="Static" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="ChkFriday" runat="server" Text="Friday" ClientIDMode="Static" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="ChkSaturday" runat="server" Text="Saturday" ClientIDMode="Static" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <asp:CustomValidator ID="CVDays" runat="server" ErrorMessage="Please select Day(s)."
                            ForeColor="Red" Display="None" ClientValidationFunction="ValidateDays" ValidateEmptyText="True"
                            ValidationGroup="AddNew"></asp:CustomValidator>
                        <%--//changes made by shrinth on 12/Sept/2014 Start--%>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vcecvdays" runat="server" TargetControlID="CVDays"
                            PopupPosition="Right">
                        </ajaxToolkit:ValidatorCalloutExtender>
                        <%--//changes made by shrinth on 12/Sept/2014 END--%>
                    </td>
                </tr>
                <%--changes in alignment by Shrinith 12/Sept/2014 End--%>
                <tr>
                    <td style="height: 10px;">
                    </td>
                </tr>
                <tr>
                    <td class="TDClassLabel" style="height: 10px; width: 50%">
                        Start Time :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px; width: 50%; margin-left: 40px;">
                        <asp:TextBox ID="txtStartTime" runat="server" CssClass="TextControl" MaxLength="5"
                            onkeyup="fnColon(this,event)" onkeypress="findspace(event)" ClientIDMode="Static"
                            TabIndex="4" ValidationGroup="AddNew"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FTBEtxtComplaintCategory" runat="server"
                            FilterType="Numbers,Custom" TargetControlID="txtStartTime" ValidChars=":" />
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1_empid5" runat="server" ControlToValidate="txtStartTime"
                            Display="None" ErrorMessage="Please enter Start time." ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                        <%--//changes made by shrinth on 12/Sept/2014 Start--%>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vectxtStartTime" runat="server" TargetControlID="RequiredFieldValidator1_empid5"
                            PopupPosition="Right">
                        </ajaxToolkit:ValidatorCalloutExtender>
                        <%--//changes made by shrinth on 12/Sept/2014 END--%>
                        <asp:RegularExpressionValidator ID="regextxtSessionTime" runat="server" ControlToValidate="txtStartTime"
                            ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="AddNew"></asp:RegularExpressionValidator>
                        <%--//changes made by shrinth on 12/Sept/2014 Start--%>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtStartTime2" runat="server" TargetControlID="regextxtSessionTime"
                            PopupPosition="Right">
                        </ajaxToolkit:ValidatorCalloutExtender>
                        <%--//changes made by shrinth on 12/Sept/2014 END--%>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px;">
                    </td>
                </tr>
                <tr>
                    <td class="TDClassLabel" style="height: 10px; width: 50%">
                        SQL Backup Path :<label class="CompulsaryLabel">*</label>
                    </td>
                    <td class="TDClassControl" style="height: 10px; width: 50%; margin-left: 40px;">
                        <asp:TextBox ID="txtSQLBackupPath" runat="server" MaxLength="100" CssClass="TextControl"
                            ClientIDMode="Static" TabIndex="5" ValidationGroup="AddNew">
                        </asp:TextBox>
                        <asp:Button ID="btnBrowse" runat="server" CssClass="ButtonControl" Text="Browse"
                            TabIndex="6" OnClientClick="showDirectory();" />
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSQLBackupPath"
                            Display="None" ErrorMessage="Please enter SQL Backup Location." ForeColor="Red"
                            SetFocusOnError="True" ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                        <%--//changes made by shrinth on 12/Sept/2014 Start--%>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtSQLBackupPath" runat="server" TargetControlID="RequiredFieldValidator5"
                            PopupPosition="Right">
                        </ajaxToolkit:ValidatorCalloutExtender>
                        <%--//changes made by shrinth on 12/Sept/2014 END--%>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px;">
                    </td>
                </tr>
                <tr>
                    <td class="TDClassLabel" style="height: 10px; width: 50%">
                        Enter Email Id :<label class="CompulsaryLabel"></label>
                    </td>
                    <td class="TDClassControl" style="height: 10px; width: 50%; margin-left: 40px;">
                        <asp:TextBox ID="txtMailId" runat="server" MaxLength="100" CssClass="TextControl"
                            ClientIDMode="Static" TabIndex="7">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px;">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="CmdCancel" />
        </Triggers>
    </asp:UpdatePanel>
    <table id="table3" runat="server" cellpadding="3" cellspacing="3" width="100%">
        <tr>
            <td style="text-align: right; width: 45%;">
                <asp:Button ID="CmdOk" runat="server" CssClass="ButtonControl" OnClick="CmdOk_Click"
                    Text="Save" OnClientClick="return handleAdd()" TabIndex="8" ValidationGroup="AddNew" />
            </td>
            <td style="text-align: left; width: 55%;">
                <asp:Button ID="CmdCancel" runat="server" Text="Reset" CssClass="ButtonControl" TabIndex="9"
                    CausesValidation="False" OnClick="CmdCancel_Click" OnClientClick="reset()" />
                <asp:Button ID="btnDelete" runat="server" CssClass="ButtonControl" OnClick="btnDelete_Click"
                    Text="Delete" TabIndex="10" ValidationGroup="AddNew" OnClientClick="javascript:return confirm('Are you sure?,you want to delete the Auto Backup configuration');" />
                <asp:Button ID="btnManualClick" runat="server" Text="Manual Backup" CssClass="ButtonControl"
                    TabIndex="1" CausesValidation="False" OnClick="btnManualClick_Click" />
            </td>
        </tr>
    </table>
    <table class="MessageContainerTable" width="98.8%" style="margin-top: 1%">
        <tr>
            <td colspan="4" align="center" valign="middle">
                <div id="messageDiv" runat="server" clientidmode="Static" class="MessageClass" visible="false"
                    style="color: Red;">
                </div>
                <div id="messageDiv1" runat="server" clientidmode="Static" style="color: red;" class="MessageClass"
                    visible="false">
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlAddEmployee" runat="server" CssClass="PopupPanel" Width="40%">
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            SQL Backup Path :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%; margin-left: 40px;">
                            <asp:TextBox ID="txtmanuPath" runat="server" MaxLength="100" CssClass="TextControl"
                                ClientIDMode="Static" TabIndex="5" ValidationGroup="Confirm" >
                            </asp:TextBox>
                            <asp:Button ID="Button1" runat="server" CssClass="ButtonControl" Text="Browse" TabIndex="6"
                                OnClientClick="showDirectoryManual();" />
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtmanuPath"
                                Display="None" ErrorMessage="Please enter SQL Backup Location." ForeColor="Red"
                                SetFocusOnError="True" ValidationGroup="Confirm"></asp:RequiredFieldValidator>
                            <%--//changes made by shrinth on 12/Sept/2014 Start--%>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                TargetControlID="RequiredFieldValidator1" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <%--//changes made by shrinth on 12/Sept/2014 END--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Enter Email Id :<label class="CompulsaryLabel"></label>
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%; margin-left: 40px;">
                            <asp:TextBox ID="txtManualMailId" runat="server" MaxLength="100" CssClass="TextControl"
                                ClientIDMode="Static" TabIndex="7">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 45%;">
                            <asp:Button ID="btnSaveManualBackup" runat="server" CssClass="ButtonControl" Text="Save"
                                TabIndex="8" OnClick="btnSaveManualBackup_Click" ValidationGroup="Confirm" />
                        </td>
                        <td style="text-align: left; width: 55%;">
                            <asp:Button ID="btncancel" runat="server" CssClass="ButtonControl" Text="Cancel"
                                TabIndex="10" OnClick="btncancel_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button7" runat="server" Style="display: none;" Text="test" />
    <asp:Button ID="btntempadd" runat="server" Text="Yes" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="mpeManualBackup" runat="server" BackgroundCssClass="cssVEh"
        Enabled="true" PopupControlID="pnlAddEmployee" TargetControlID="Button7" OkControlID="btntempadd">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
