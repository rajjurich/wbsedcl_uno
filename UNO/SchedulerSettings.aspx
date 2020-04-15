<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="SchedulerSettings.aspx.cs" Inherits="UNO.SchedularSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript">
        //        function ValidateData() {           
        //            if (!(validateString(document.getElementById('txtScheduleDesc'), "Please enter Scheduler Description."))) {
        //                return false
        //            }         
        //            if (!(validateString(document.getElementById('txtScheduleTime'), "Please enter Scheduler Time."))) {
        //                return false
        //            }

        //            if(document.getElementById('ddlScheduleType').value == "Select One") {
        //                alert("Please select Scheduler Task Type.");
        //                return false
        //            }
        //            if (document.getElementById('ddlFrequency').value == "Select One") {
        //                alert("Please select Scheduler Frequency.");
        //                return false
        //            }

        //            if (handleAdd() == false) {
        //                return false
        //            }
        //        }

        function clearFunction(x) {
            document.getElementById(x).innerHTML = "&nbsp;&nbsp;";
            //            resetall();
        }

        function handleAdd() {


            if (!Page_ClientValidate('Scheduler'))
                return;
            var msg = confirm("Save Record?")
            if (msg == false) {
                return false;
            }

        }

        //            function resetall() {
        //                document.getElementById('txtScheduleDesc').value = '';
        //                document.getElementById('txtScheduleTime').value = '';
        //                document.getElementById('ddlScheduleType').value = "Select One";
        //                document.getElementById('ddlFrequency').value = "Select One";
        //                document.getElementById(x).innerHTML = "&nbsp;&nbsp;";
        //            }  
        

    </script>
    <h3 class="heading">
        Scheduler Settings</h3>
    <table id="table1" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
        class="TableClass">
        <tr>
            <td align="right" style="width: 33%" class="LinkControl">
                <asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="ScheduleSettingView.aspx">Back to View Mode</asp:HyperLink>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Settings" runat="server" ScrollBars="Auto" Width="95%" Height="200px"
        BorderWidth="1" BorderColor="Gray" HorizontalAlign="Center" align="Center" CssClass="srcColor">
        <br />
        <table id="table2" runat="server" width="100%" class="TableClass">
            <tr>
                <td class="TDClassLabel">
                    Scheduler Description :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl">
                    <asp:TextBox CssClass="TextControl" ID="txtScheduleDesc" ClientIDMode="Static" MaxLength="20"
                        runat="server" OnKeyPress="return IsAlphanumeric(event)"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvSchDesc" runat="server" ErrorMessage="Please Enter Scheduler Description."
                        ControlToValidate="txtScheduleDesc" SetFocusOnError="True" Display="Dynamic"
                        ForeColor="Red" ValidationGroup="Scheduler"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    Scheduler Task Type :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl">
                    <asp:DropDownList ID="ddlScheduleType" runat="server" ClientIDMode="Static" CssClass="ComboControl" />
                    <br />
                    <asp:RequiredFieldValidator ID="rfvSchType" runat="server" ErrorMessage="Please select Scheduler Task Type."
                        ControlToValidate="ddlScheduleType" SetFocusOnError="True" Display="Dynamic"
                        InitialValue="Select One" ForeColor="Red" ValidationGroup="Scheduler"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    Scheduler Frequency :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl">
                    <asp:DropDownList ID="ddlFrequency" runat="server" ClientIDMode="Static" CssClass="ComboControl" />
                    <br />
                    <asp:RequiredFieldValidator ID="rfvSchFrq" runat="server" ErrorMessage="Please select Scheduler Frequency."
                        ControlToValidate="ddlFrequency" SetFocusOnError="True" Display="Dynamic" InitialValue="Select One"
                        ForeColor="Red" ValidationGroup="Scheduler"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    Frequency Time(Sec) :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl">
                    <asp:TextBox CssClass="TextControl" ID="txtScheduleTime" ClientIDMode="Static" Width="20%"
                        runat="server" MaxLength="5" OnKeyPress="return IsNumber(event)"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvfrqTime" runat="server" ErrorMessage="Please Enter Frequency Time."
                        ControlToValidate="txtScheduleTime" SetFocusOnError="True" Display="Dynamic"
                        ForeColor="Red" ValidationGroup="Scheduler"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <table id="table3" runat="server" cellpadding="1" cellspacing="1" width="95%">
            <tr>
                <td style="width: 80%">
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="ButtonControl" OnClick="btnSave_Click"
                        OnClientClick="return handleAdd()" Text="Save" Width="90%" />
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" CssClass="ButtonControl" Text="Cancel"
                        Width="80%" OnClick="btnCancel_Click" />
                    <%--<input type="Button" class= "ButtonControl" value= "Cancel" id="btnCancel" style="width:80%" onclick="return btnCancel_onclick()" />--%>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table class="MessageContainerTable" width="98.8%">
        <tr>
            <td colspan="4" align="center" valign="middle">
                <div id="messageDiv" runat="server" clientidmode="Static" class="MessageClass">
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
