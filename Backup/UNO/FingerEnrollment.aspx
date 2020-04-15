<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="FingerEnrollment.aspx.cs" Inherits="UNO.FingerEnrollment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <link href="Styles/Modelpop.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Style1.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function WriteOnCard() {
            try {
                var EmployeeCode = document.getElementById("<%= txtEmpCd.ClientID %>").value;
                $.ajax({
                    url: "FingerEnrollment.aspx/GetISOTemplate",
                    type: "POST",
                    dataType: "json",
                    data: "{'EmployeeCode':'" + EmployeeCode + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {


                        if (msg.d == "False") {
                            alert("Can not write on card,employee not enrolled or card is already issued.");
                        }
                        else {
                            alert(msg.d);
                            var xmlDoc = $.parseXML(msg.d);
                            var xml = $(xmlDoc);
                            var Finger_Template = xml.find("Table1");

                            var Template1 = $(Finger_Template).find("ISOTemplate1").text().toUpperCase();
                            var Template2 = $(Finger_Template).find("ISOTemplate2").text().toUpperCase();
                            var ActivationDate = $(Finger_Template).find("ActivationDate").text();
                            ActivationDate = parseInt(ActivationDate, 10).toString(16).toUpperCase();
                            var ExpiryDate = $(Finger_Template).find("ExpiryDate").text();
                            ExpiryDate = parseInt(ExpiryDate, 10).toString(16).toUpperCase();
                            var AadharNo = $(Finger_Template).find("AadharNo").text();
                            var CenterCode = $(Finger_Template).find("CenterCode").text();
                            var LocationCode = $(Finger_Template).find("LocationCode").text();



                            var objReadCard;
                            var DataInitialize;
                            var cardCSNR = "";
                            objReadCard = new ActiveXObject("ContactlessCardRW.Card");
                            DataInitialize = objReadCard.Initialise();
                            if (DataInitialize != "") {
                                alert("Omnikey reader not connected or Error in card Initialization");
                                return false;
                            }
                            var data;
                            data = objReadCard.ConnectToCard();
                            if (data != "") {
                                alert("Error in connecting to card");
                                return false;
                            }

                            var WriteData = "";
                            var EmployeeCode = document.getElementById("<%= txtEmpCd.ClientID %>").value;
                            var EmployeeCodePrefix = EmployeeCode.substring(0, 2);
                            var EmpCode = EmployeeCode.substring(2, EmployeeCode.length);
                            var EmpName = document.getElementById("<%= txtEmpName.ClientID %>").value.rpad(" ", 16);
                            WriteData = ConvertStringToHex(EmployeeCodePrefix) + ConvertStringToHex(EmpCode).toUpperCase() + ConvertStringToHex(EmpName).toUpperCase() + ActivationDate + ExpiryDate + "00" + "01" + "02" + (Template1.length).toString(16).lpad("0", 4).toUpperCase() + (Template2.length).toString(16).lpad("0", 4).toUpperCase() + AadharNo.lpad("0", 12) + "0000" + CenterCode + LocationCode + Template1 + Template2;


                            alert(WriteData);
                            var Sector = 35;
                            for (var i = 0, j = 176; i < 16 && Sector < 40; i++, j++) {
                                if (i == 15) {
                                    data = objReadCard.Authenticate(j, "FFFFFFFFFFFF", 96);
                                    if (data != "") {
                                        alert("Unable to Authenticate Block " + j);
                                        return false;
                                    }
                                    var key = "FFFFFFFFFFFF" + "FF078069" + "FFFFFFFFFFFF";
                                    key = objReadCard.Convert(key);
                                    data = objReadCard.WriteData(key, parseInt(j, 10).toString(16).toUpperCase());
                                    if (data != "") {
                                        alert("Card Error: Unable to write in the card.Sector " + Sector + " Block " + j);
                                        return false;
                                    }
                                    alert("Key Sector " + Sector + " Block " + j + " : " + key);
                                    i = 0 - 1;
                                    Sector = Sector + 1;
                                }
                                else {
                                    var strWriteData = WriteData.substring(0, 32).rpad("0", 32);
                                    WriteData = WriteData.substring(32, WriteData.length);
                                    alert("Data Sector " + Sector + " Block " + j + " : " + strWriteData);
                                    data = objReadCard.Authenticate(j, "FFFFFFFFFFFF", 96);
                                    if (data != "") {
                                        alert("Unable to Authenticate Block " + j);
                                        return false;
                                    }
                                    strWriteData = objReadCard.Convert(strWriteData);
                                    data = objReadCard.WriteData(strWriteData, parseInt(j, 10).toString(16).toUpperCase());
                                    if (data != "") {
                                        alert("Card Error: Unable to write in the card.Sector " + Sector + " Block " + j);
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                });
            }
            catch (ex) {
                alert(ex.Message);
            }
        }

        function ConvertStringToHex(strg) {
            try {
                var hex, i;
                var str = strg;
                var result = "";
                for (i = 0; i < str.length; i++) {
                    hex = str.charCodeAt(i).toString(16);
                    result += hex;
                }
                return result;
            }
            catch (ex) {
                alert(ex.Message + " ConvertStringToHex");
            }
        }

        function dateFromISO(s) {
            s = s.split(/\D/);
            return new Date(Date.UTC(s[0], --s[1] || '', s[2] || '', s[3] || '', s[4] || '', s[5] || '', s[6] || ''))
        }

        //pads right
        String.prototype.rpad = function (padString, length) {
            var str = this;
            while (str.length < length)
                str = str + padString;
            return str;
        }

        String.prototype.lpad = function (padString, length) {
            var str = this;
            while (str.length < length)
                str = padString + str;
            return str;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/jscript">

        function GetTemplate() {
            var objReadCard;
            var DataInitialize;
            objReadCard = new ActiveXObject("ContactlessCardRW.Card");
            alert(objReadCard);
            DataInitialize = objReadCard.Initialise();
            if (DataInitialize != "") {
                alert("Omnikey reader not connected or Error in card Initialization");
                return false;
            }
            var data;
            data = objReadCard.ConnectToCard();
            if (data != "") {
                alert("Error in connecting to card");
                return false;
            }
            var datatable = "";
            var txtEmpCd = document.getElementById('<%=txtEmpCd.ClientID%>').value;
            alert(txtEmpCd);
            $.ajax({
                url: "FingerEnrollment.aspx/GetTemplate",
                type: "POST",
                dataType: "json",
                data: "{'txtEmpCd':'" + txtEmpCd + "'}",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {


                    if (msg.d == "False") {
                        alert("Can not write on card,employee not enrolled or card is already issued.");
                    }
                    else {
                        alert(msg.d);
                        var xmlDoc = $.parseXML(msg.d);
                        var xml = $(xmlDoc);
                        var Finger_Template = xml.find("Table1");

                        $.each(Finger_Template, function () {
                            var customer = $(this);
                            var TempMin1 = $(this).find("TempMin1").text();
                            var TempMin2 = $(this).find("TempMin2").text();
                            var Format_Type = $(this).find("Format_Type").text();
                            var ISOFingerImg1 = $(this).find("ISOFingerImg1").text();
                            var ISOFingerImg2 = $(this).find("ISOFingerImg2").text();

                            if (Format_Type == "Native") {
                                var ArrTempMin1 = new Array();
                                var j = 0;
                                for (var i = 0; i < 256; i += 32) {
                                    var str = TempMin1.substring(i, i + 32);
                                    if (str == "") { str = "00" }
                                    ArrTempMin1.push(PadBytes(str, 32));
                                    j = j + 1;

                                }
                                alert("hi");
                                for (var i = 0; i < ArrTempMin1.length; i++) {
                                    var WriteData = ArrTempMin1[i];
                                    data = objReadCard.Authenticate(44, "FFFFFFFFFFFF", 96);
                                    alert(data);
                                    if (data != "") {
                                        alert("Unable to Authenticate Block 44");
                                        return false;
                                    }
                                    WriteData = objReadCard.Convert(WriteData);
                                    data = objReadCard.WriteData(WriteData, "2C");
                                    if (data != "") {
                                        alert("Card Error: Unable to write in the card.Sector 18 Block 72");
                                        return false;
                                    }
                                }
                                alert("End");
                                var ArrTempMin2 = new Array();
                                var j1 = 0;
                                for (var i = 0; i < 256; i += 32) {
                                    var str = TempMin2.substring(i, i + 32);
                                    if (str == "") { str = "00" }
                                    ArrTempMin2.push(PadBytes(str, 32));
                                    alert(ArrTempMin2[j1]);
                                    j1 = j1 + 1;
                                }
                                //Card Writing



                            }
                            if (Format_Type == "ISO") {
                                var ArrISOFingerImg1 = new Array();
                                var j = 0;
                                for (var i = 0; i < 256; i += 32) {
                                    alert(i);
                                    ArrISOFingerImg1.push(ISOFingerImg1.substring(i, i + 32));
                                    alert(ArrISOFingerImg1[j]);
                                    j = j + 1;
                                }
                                var ArrISOFingerImg2 = new Array();
                                var j1 = 0;
                                for (var i = 0; i < 256; i += 32) {
                                    alert(i);
                                    ArrISOFingerImg2.push(ISOFingerImg2.substring(i, i + 32));
                                    alert(ArrISOFingerImg2[j1]);
                                    j1 = j1 + 1;
                                }
                            }
                            if (Format_Type == "Both") {
                                var ArrTempMin1 = new Array();
                                var j = 0;
                                for (var i = 0; i < 256; i += 32) {
                                    alert(i);
                                    ArrTempMin1.push(TempMin1.substring(i, i + 32));
                                    alert(ArrTempMin1[j]);
                                    j = j + 1;
                                }
                                var ArrTempMin2 = new Array();
                                var j1 = 0;
                                for (var i = 0; i < 256; i += 32) {
                                    alert(i);
                                    ArrTempMin2.push(TempMin2.substring(i, i + 32));
                                    alert(ArrTempMin2[j1]);
                                    j1 = j1 + 1;
                                }
                                var ArrISOFingerImg1 = new Array();
                                var j2 = 0;
                                for (var i = 0; i < 256; i += 32) {
                                    alert(i);
                                    ArrISOFingerImg1.push(ISOFingerImg1.substring(i, i + 32));
                                    alert(ArrISOFingerImg1[j2]);
                                    j2 = j2 + 1;
                                }
                                var ArrISOFingerImg2 = new Array();
                                var j3 = 0;
                                for (var i = 0; i < 256; i += 32) {
                                    alert(i);
                                    ArrISOFingerImg2.push(ISOFingerImg2.substring(i, i + 32));
                                    alert(ArrISOFingerImg2[j3]);
                                    j3 = j3 + 1;
                                }

                            }

                        });
                    }

                },
                error: function () { alert(arguments[2]); }
            });


        };
        function PadBytes(val, num) {
            try {
                for (var i = val.length; i < num; i++) {
                    val = val + '0';
                }
                return val;
            }
            catch (ex) {
                alert(ex.Message);
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function executeCommands(inputparms) {
            var oShell = new ActiveXObject("Shell.Application");
            // var commandtoRun = "D:\\jasmin\\SAC Demo Web app\\NativeTemplate\\NativeTemplate\\bin\\Debug\\NativeTemplate.exe";
            var commandtoRun = document.getElementById('RootPath').value;
            var commandParms = document.getElementById('hdnparm').value;
            var ret = oShell.ShellExecute(commandtoRun, commandParms, "", "open", "1");

        }

        function Validate() {
            //         if (document.getElementById('txtUserID').value == "") {
            //             alert("Please enter user id.");
            //             return false
            //         }
            //         if (document.getElementById('txtPwd').value == "") {
            //             alert("Please enter Password");
            //             return false
            //         }
            //         return true;
            if (!Page_ClientValidate())
                return;
        }
    </script>
    <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"
        CombineScripts="false">
    </ajaxToolkit:ToolkitScriptManager>--%>
    <h3 class="heading">
        Finger Enrollment</h3>
    <table id="table1" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
        class="TableClass">
        <tr>
            <td align="right" style="width: 33%" class="LinkControl">
                <asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="EnrollmentView.aspx">Back to View Mode</asp:HyperLink>
            </td>
        </tr>
    </table>
    <asp:Panel ID="EmpDetails" ClientIDMode="Static" runat="server" Width="95%" Height="200px"
        BorderWidth="1" BorderColor="Gray" HorizontalAlign="Center" align="Center" CssClass="srcColor">
        <table id="table2" runat="server" width="80%">
            <tr>
                <td class="TDClassLabel">
                    Employee Code :
                </td>
                <td class="TDClassControl">
                    <asp:TextBox CssClass="TextControl" ClientIDMode="Static" ID="txtEmpCd" runat="server"
                        OnKeyPress="return IsNumber(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    Card Code :
                </td>
                <td class="TDClassControl">
                    <asp:TextBox CssClass="TextControl" ClientIDMode="Static" ID="txtCardCd" runat="server"
                        OnKeyPress="return IsNumber(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    Employee Name :
                </td>
                <td class="TDClassControl">
                    <asp:TextBox CssClass="TextControl" ClientIDMode="Static" ID="txtEmpName" runat="server"
                        OnKeyPress="return IsNumber(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    Department :
                </td>
                <td class="TDClassControl">
                    <asp:TextBox CssClass="TextControl" ClientIDMode="Static" ID="txtDept" MaxLength="3"
                        runat="server" OnKeyPress="return IsNumber(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    Designation :
                </td>
                <td class="TDClassControl">
                    <asp:TextBox CssClass="TextControl" ClientIDMode="Static" ID="txtDesignation" MaxLength="3"
                        runat="server" OnKeyPress="return IsNumber(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel">
                    Select Template Format :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl">
                    <asp:DropDownList CssClass="ComboControl" ClientIDMode="Static" ID="ddlFormat" runat="server">
                        <asp:ListItem>Native</asp:ListItem>
                        <asp:ListItem>ISO</asp:ListItem>
                        <asp:ListItem>Both</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvTemplate" runat="server" ErrorMessage="Please Select Template Format"
                        ControlToValidate="ddlFormat" SetFocusOnError="True" Display="Dynamic" ForeColor="Red"
                        ValidationGroup="Ctlrdetails"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <br />
        <table id="tbl" runat="server" width="80%">
            <tr>
                <td style="width: 50%">
                </td>
                <td>
                    <asp:Button ID="btnEnroll" runat="server" ClientIDMode="Static" Text="Enroll" Width="95%"
                        CssClass="ButtonControl" OnClick="btnEnroll_Click" CausesValidation="false" />
                </td>
                <td>
                    <asp:Button ID="btnVerify" runat="server" ClientIDMode="Static" Text="Verify" Width="95%"
                        CssClass="ButtonControl" OnClick="btnVerify_Click" />
                </td>
                <td>
                    <asp:Button ID="btnWrite" runat="server" Text="Write On Card" Width="100%" CssClass="ButtonControl"
                        OnClientClick="WriteOnCard();" />
                    <%--<asp:Button ID="btnWrite" runat="server" ClientIDMode="Static" Text="Write On Card" OnClientClick="GetTemplate();"
                        Width="100%" CssClass="ButtonControl" OnClick="btnWrite_Click" />--%>
                </td>
                <%-- <td style="width:20%"></td>   --%>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="CancelButton" DropShadow="true" PopupControlID="Panel1" PopupDragHandleControlID="Panel3"
        TargetControlID="btnTest">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none"
        Width="500px">
        <div>
            <%-- <p style="text-align: center">--%>
            <table align="center">
                <caption>
                    <h3 class="heading">
                        Place Master Card
                    </h3>
                    <tr>
                        <td class="TDClassLabel">
                            User ID :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserID" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="6" Width="150px" onkeypress="return IsAlphanumericWithoutspace(event)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserID"
                                Display="Dynamic" ErrorMessage="Please enter User ID" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="validate"></asp:RequiredFieldValidator>
                            <br />
                            <asp:RegularExpressionValidator ID="RegExp1" runat="server" ForeColor="Red" ControlToValidate="txtUserID"
                                ErrorMessage="User Id length must be 6 characters" ValidationExpression="^[a-zA-Z0-9\s]{6}$">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            Password:<label class="CompulsaryLabel">*</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPwd" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="6" TextMode="Password" Width="150px" onkeypress="return IsAlphanumericWithoutspace(event)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPwd"
                                Display="Dynamic" ErrorMessage="Please enter Password" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="validate"></asp:RequiredFieldValidator>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPwd"
                                ErrorMessage="Password length must be 6 characters" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9\s]{6}$">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="OkButton" runat="server" Text="OK" OnClick="OkButton_Click" CssClass="ButtonControl"
                                Width="30%" OnClientClick='return Validate()' />
                            <asp:Button ID="CancelButton" runat="server" Text="Cancel" CssClass="ButtonControl"
                                Width="30%" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </caption>
            </table>
            <%-- </p>--%>
        </div>
    </asp:Panel>
    &nbsp;
    <br />
    <div>
    </div>
    <asp:HiddenField ID="hdnparm" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="RootPath" ClientIDMode="Static" runat="server" />
    <asp:Button ID="btnTest" runat="server" ClientIDMode="Static" Text="Test" Width="100%"
        Style="display: none" />
</asp:Content>
