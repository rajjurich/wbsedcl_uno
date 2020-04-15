<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" EnableEventValidation="true"
    AutoEventWireup="true" CodeBehind="TEClientMasterFile.aspx.cs" Inherits="UNO.TEClientMasterFile" %>

<%@ Register Assembly="DemoGrid" Namespace="DemoGrid.GridViewControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #DOB
        {
            width: 115px;
        }
        #DOB0
        {
            width: 115px;
        }
        .style38
        {
            font-size: 9pt;
            font-family: verdana;
            color: #515456;
            border: 0px solid #CCCCCC;
            text-align: left;
            padding-left: 4px;
            width: 30%;
            height: 10px;
        }
        .style39
        {
            font-size: 9pt;
            font-family: verdana;
            color: #515456;
            border: 0px solid #CCCCCC;
            text-align: right;
            padding-left: 4px;
            width: 35%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='./calendar.css' title='calendar'>
    <script language="javascript" src="calendar.js"></script>
    <script language="javascript" type="text/javascript">
        //window.onload = display;

        //*****************************Supriya26062013************************************

        function ValidateData5() {
            //        if (!(validateString(document.getElementById('Cmp_id'), "Please enter Client Code."))) {
            //            return false
            //        }
            //        if (!(validateString(document.getElementById('Cmp_desc'), "Please enter Client Description."))) {
            //            return false
            //        }
            if (handleAdd() == false) {
                return false
            }
        }

        function Confirmationbox() {
            var result = confirm('Are you sure you want to delete selected User(s)?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }

        function handleAdd() {
            if (!Page_ClientValidate())
                return;

            var msg = confirm('Save Record?');

            if (msg == false) {
                return false;
            }
        }

        //********************************Supriya18062013*****************************************
        function setFocus(Target) {
            if (window.event.keyCode == "13")
                document.getElementById(Target).focus();
        }
        //********************************Supriya18062013*****************************************



        function returnviewmode() {
            document.getElementById('LblMsg').innerHTML = "&nbsp;&nbsp;";
            window.location = "ClientFileView.aspx";
        }
        function HideLabel(labelID) {
            setTimeout("HideLabelHelper('" + labelID + "');", 3000);
        }

        function HideLabelHelper(labelID) {
            document.getElementById(labelID).style.display = "none";
        }
        function IsAlphanumericN(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) || (charCode >= 48 && charCode <= 57) ||
			(charCode >= 97 && charCode <= 122) || (charCode == 8) || (charCode == 32) || (charCode == 44) || (charCode == 45) || (charCode == 47)) {
                return true
            }
            else {
                return false
            }
        }

    </script>
    <asp:Panel ID="Client_File" Clientmode="static" runat="server" Width="100%" ScrollBars="None"
        ClientIDMode="Static">
        <br />
        <table id="table3" runat="server" width="100%" height="10%" border="0" cellpadding="0"
            cellspacing="0">
            <tr>
                <td>
                    <asp:Label ID="label2" runat="server" Text="Client Master" ForeColor="RoyalBlue"
                        Width="100%" Height="20px" CssClass="heading">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="~/TEClientMasterView.aspx"
                        ForeColor="Blue">Back to View Mode</asp:HyperLink>
                </td>
            </tr>
        </table>
        <br />
        <table id="tablen" runat="server" width="100%" height="90%"  class="rounded-corners">
            <tr>
                <td width="25%" class="style39">
                    &nbsp;Client ID :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="style38">
                    <asp:TextBox ID="txtClientID" runat="server" TabIndex="0" onkeypress="return IsAlphanumericWithoutspace(event)"
                        CssClass="TextControl" MaxLength="8" Style="text-transform: uppercase;" ClientIDMode="Static"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1_empid" runat="server" ControlToValidate="txtClientID"
                        Display="Dynamic" ErrorMessage="Please enter Client Id." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="25%" class="TDClassLabel" style="height: 10px">
                    &nbsp;Name :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="style38">
                    <asp:TextBox ID="txtClientName" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="50" onkeypress="return IsAlphanumeric(event)" Style="text-transform: capitalize;"
                        TabIndex="3"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtClientName"
                        Display="Dynamic" ErrorMessage="Please enter Client Name." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
                <td class="TDClassLabel" style="height: 10px">
                    &nbsp;Description :
                </td>
                <td class="TDClassControl" style="height: 10px">
                    <asp:TextBox ID="txtClientDesc" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="50" onkeypress="return IsAlphanumeric(event)" Style="text-transform: capitalize;"
                        TabIndex="1"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr width="100%">
                <td class="TDClassLabel" style="height: 10px">
                    &nbsp;Site Address :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="style38">
                    <asp:TextBox ID="txtClientAddress" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        Height="50px" MaxLength="50" onkeypress="return IsAlphanumericN(event)" Style="text-transform: capitalize;"
                        TabIndex="4" TextMode="MultiLine"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClientAddress"
                        Display="Dynamic" ErrorMessage="Please enter Client Site Address." ForeColor="Red"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
                <td class="TDClassLabel" style="height: 10px">
                    &nbsp;Home Address :
                </td>
                <td class="TDClassControl" style="height: 10px">
                    <asp:TextBox ID="txtClientHOAddress" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        Height="50px" MaxLength="50" onkeypress="return IsAlphanumericN(event)" Style="text-transform: capitalize;"
                        TabIndex="5" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr width="100%">
                <td class="TDClassLabel" style="height: 10px" width="50%">
                    Org. Phone Number 1:<label class="CompulsaryLabel">*</label>
                </td>
                <td class="style38">
                    <asp:TextBox ID="txtClientPh1" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="12" onkeypress="return IsNumeric(event)" Style="text-transform: uppercase;"
                        TabIndex="6"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtClientPh1"
                        Display="Dynamic" ErrorMessage="Please enter Client Phone Number." ForeColor="Red"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
                <td class="TDClassLabel" style="height: 10px" width="50%">
                    Org. Phone Number 2 :
                </td>
                <td class="TDClassControl" style="height: 10px" width="50%">
                    <asp:TextBox ID="txtClientPh2" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="12" onkeypress="return IsNumeric(event)" Style="text-transform: uppercase;"
                        TabIndex="7"></asp:TextBox>
                </td>
            </tr>
            <tr width="100%">
                <td class="TDClassLabel" style="height: 10px">
                    Contact Person 1 :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="style38">
                    <asp:TextBox ID="txtContPerson1" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="50" onkeypress="return IsAlphanumeric(event)" Style="text-transform: capitalize;"
                        TabIndex="8"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContPerson1"
                        Display="Dynamic" ErrorMessage="Please enter Client Person." ForeColor="Red"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
                <td class="TDClassLabel" style="height: 10px">
                    Contact Person 2 :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl" style="height: 10px">
                    <asp:TextBox ID="txtContPerson2" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="50" onkeypress="return IsAlphanumeric(event)" Style="text-transform: capitalize;"
                        TabIndex="9"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtContPerson2"
                        Display="Dynamic" ErrorMessage="Please enter Client Person." ForeColor="Red"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr width="100%">
                <td class="TDClassLabel" style="height: 10px" width="50%">
                    &nbsp;&nbsp; Phone Number 1 :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="style38">
                    <asp:TextBox ID="txtContFPerNm1" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="12" onkeypress="return IsNumeric(event)" Style="text-transform: uppercase;"
                        TabIndex="10"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtContFPerNm1"
                        Display="Dynamic" ErrorMessage="Please enter Client Phone Number." ForeColor="Red"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
                <td class="TDClassLabel" style="height: 10px" width="50%">
                    &nbsp;Phone Number 1 :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl" style="height: 10px" width="50%">
                    <asp:TextBox ID="txtContSPerNm1" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="12" onkeypress="return IsNumeric(event)" Style="text-transform: uppercase;"
                        TabIndex="11"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtContSPerNm1"
                        Display="Dynamic" ErrorMessage="Please enter Client Phone Number." ForeColor="Red"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel" style="height: 10px" width="50%">
                    Phone Number 2 :&nbsp;&nbsp;
                </td>
                <td class="style38">
                    <asp:TextBox ID="txtContFPerNm2" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="12" onkeypress="return IsNumeric(event)" Style="text-transform: uppercase;"
                        TabIndex="12"></asp:TextBox>
                </td>
                <td class="TDClassLabel" style="height: 10px" width="50%">
                    Phone Number 2 :&nbsp;&nbsp;
                </td>
                <td class="TDClassControl" style="height: 10px" width="50%">
                    <asp:TextBox ID="txtContSPerNm2" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="12" onkeypress="return IsNumeric(event)" Style="text-transform: uppercase;"
                        TabIndex="13"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table id="table5" runat="server" width="100%" height="90%" border="0" cellpadding="0"
            cellspacing="0" class="TableClass">
            <tr>
                <td colspan="4" align="right" style="height: 60px">
                    <asp:Button ID="btnSave" runat="server" CssClass="ButtonControl" TabIndex="14" Text="Save"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="ButtonControl" TabIndex="15"
                        Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />
                    <br />
                    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                        <asp:Label ID="LblMsg" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
</asp:Content>
