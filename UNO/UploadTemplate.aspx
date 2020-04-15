<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="UploadTemplate.aspx.cs" Inherits="UNO.UploadTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function handleAdd() {
            var msg = confirm("Save Record?");

            if (msg == false) {
                return false;
            }
        }
        function clearFunction() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";

        }

        function AllCtlrSelected() {
            alert('hi');
            if (document.getElementById('chkAllCtlr').checked == true) {
                document.getElementById('lstACntrl').disabled = true;
                document.getElementById('lstSCntrl').disabled = true;
                document.getElementById('btnCntrlRight').disabled = true;
                document.getElementById('btnCntrlLeft').disabled = true;
            }
            else {
                document.getElementById('lstACntrl').disabled = false;
                document.getElementById('lstSCntrl').disabled = false;
                document.getElementById('btnCntrlRight').disabled = false;
                document.getElementById('btnCntrlLeft').disabled = false;

            }
        }

        function AllEmpSelected() {
            if (document.getElementById('chkAllEmp').checked == true) {
                document.getElementById('lstAEmp').disabled = true;
                document.getElementById('lstSEmp').disabled = true;
                document.getElementById('btnEmpRight').disabled = true;
                document.getElementById('btnEmpLeft').disabled = true;
            }
            else {
                document.getElementById('lstAEmp').disabled = false;
                document.getElementById('lstSEmp').disabled = false;
                document.getElementById('btnEmpRight').disabled = false;
                document.getElementById('btnEmpLeft').disabled = false;

            }
        }    



    </script>
    <table id="table2" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
        class="TableClass">
        <tr>
            <td colspan="3" align="center">
                <h3 class="heading">
                    Upload Finger Template
                </h3>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right" class="LinkControl">
                <asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="~/AccessDashboard.aspx" Visible="false"
                    ForeColor="Blue">Back to Access Management Dash Board</asp:HyperLink>
            </td>
        </tr>
    </table>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Details" ClientIDMode="Static" runat="server" Width="95%" Height="350px"
                BorderWidth="1" BorderColor="Gray" HorizontalAlign="Center" align="Center" CssClass="srcColor">
                <table id="table1" runat="server" width="100%">
                    <%--<tr>
	<td class = "TDClassLabel" > Select Controller :<label class="CompulsaryLabel">*</label> </td>
	<td class = "TDClassControl" colspan="3" >
		<asp:DropDownList ID="ddlController" ClientIDMode="Static" runat="server" 
            Width="20%" CssClass="ComboControl"  AutoPostBack = "true">     
            <asp:ListItem Value = "S">Single</asp:ListItem>
            <asp:ListItem Value = "M">Multiple</asp:ListItem>
            <asp:ListItem Value = "A">All</asp:ListItem>
            </asp:DropDownList>            
        <br />
          <asp:RequiredFieldValidator ID="rfvCTLR" runat="server" 
            ErrorMessage="Please select Controller." ControlToValidate="ddlController" 
            SetFocusOnError="True" Display="Dynamic" InitialValue = "Select One" ForeColor="Red" 
            ValidationGroup="Details"></asp:RequiredFieldValidator>           
	</td>     
</tr> --%>
                    <tr>
                        <td style="height: 30px">
                        </td>
                        <td style="height: 30px" colspan="2" align="center">
                            <asp:RadioButtonList ID="rbSelect" runat="server" Width="80%" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="rbSelect_SelectedIndexChanged">
                                <asp:ListItem Value="U" Selected="True">Upload</asp:ListItem>
                                <asp:ListItem Value="D">Delete</asp:ListItem>
                                <asp:ListItem Value="R">Record Count</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2" align="left">
                            <asp:CheckBox Text="Select All" runat="server" ID="chkAllCtlr" AutoPostBack="true"
                                ClientIDMode="Static" OnCheckedChanged="chkAllCtlr_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                            Select Controller :
                        </td>
                        <td style="height: 10px">
                            <table width="80%" style="height: 112px">
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
                                    <td>
                                        <asp:ListBox ID="lstACntrl" runat="server" Height="100px" Width="280px" ForeColor="Black"
                                            Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple"
                                            AutoPostBack="True" CausesValidation="True"></asp:ListBox>
                                    </td>
                                    <td class="style11">
                                        <table>
                                            <tr>
                                                <td>
                                                    &nbsp;<asp:Button ID="btnCntrlRight" runat="server" Text="&gt;" Width="19px" CssClass="ButtonControl"
                                                        ClientIDMode="Static" OnClick="btnCntrlRight_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;<asp:Button ID="btnCntrlLeft" runat="server" Text="&lt;" Width="19px" CssClass="ButtonControl"
                                                        ClientIDMode="Static" CausesValidation="False" OnClick="btnCntrlLeft_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstSCntrl" runat="server" Height="100px" Width="280px" ForeColor="Black"
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
                                        <%--<asp:CustomValidator ID="CVContrl" runat="server" 
              ControlToValidate="lstSCntrl" 
               Display="Dynamic" ForeColor="Red"
              ValidateEmptyText="True"  ClientValidationFunction="ValidateCntrlListBox"></asp:CustomValidator>--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%--<tr>
	<td class = "TDClassLabel" > Select Employee :<label class="CompulsaryLabel">*</label> </td>
	<td class = "TDClassControl" colspan="3" >
		<asp:DropDownList ID="ddlEmployee" ClientIDMode="Static" runat="server" 
            Width="20%" CssClass="ComboControl"  AutoPostBack = "true" >     
            <asp:ListItem Value = "S">Single</asp:ListItem>
            <asp:ListItem Value = "M">Multiple</asp:ListItem>
            <asp:ListItem Value = "A">All</asp:ListItem>
         </asp:DropDownList>
        <br />
          <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" 
            ErrorMessage="Please select Employee." ControlToValidate="ddlEmployee" 
            SetFocusOnError="True" Display="Dynamic" InitialValue = "Select One" ForeColor="Red" 
            ValidationGroup="Details"></asp:RequiredFieldValidator>           
	</td>     
</tr>--%>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2" align="left">
                            <asp:CheckBox ID="chkAllEmp" Text="Select All" runat="server" ClientIDMode="Static"
                                AutoPostBack="true" OnCheckedChanged="chkAllEmp_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                            Select Employee :
                        </td>
                        <td style="height: 10px">
                            <table width="80%" style="height: 112px">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server">Available</asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server">Selected</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lstAEmp" runat="server" Height="100px" Width="280px" ForeColor="Black"
                                            Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple"
                                            TabIndex="4"></asp:ListBox>
                                    </td>
                                    <td class="style11">
                                        <table width="20px">
                                            <tr>
                                                <td>
                                                    &nbsp;<asp:Button ID="btnEmpRight" runat="server" Text="&gt;" Width="19px" CssClass="ButtonControl"
                                                        ClientIDMode="Static" OnClick="btnEmpRight_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;<asp:Button ID="btnEmpLeft" runat="server" Text="&lt;" Width="19px" CssClass="ButtonControl"
                                                        ClientIDMode="Static" OnClick="btnEmpLeft_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstSEmp" runat="server" Height="100px" Width="280px" ForeColor="Black"
                                            Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple"
                                            AutoPostBack="True" CausesValidation="True"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <%--  <asp:CustomValidator ID="CVEmp" runat="server" 
              ControlToValidate="lstSEmp" 
              Display="Dynamic" ForeColor="Red"
              ValidateEmptyText="True" ClientValidationFunction = "ValidateEmpListBox"></asp:CustomValidator>--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table id="table3" runat="server" cellpadding="3" cellspacing="3" width="95%">
        <tr>
            <td style="width: 70%">
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonControl" Text="Save" OnClientClick="return handleAdd()"
                    Width="100%" TabIndex="9" OnClick="btnSave_Click" />
            </td>
            <td>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100%" CssClass="ButtonControl"
                    TabIndex="10" CausesValidation="False" OnClick="btnCancel_Click" />
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
