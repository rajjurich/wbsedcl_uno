<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="GatepassRegistration.aspx.cs" Inherits="UNO.GatepassRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function navigateToUrl(url) {
            var f = document.createElement("FORM");
            f.action = url;

            var indexQM = url.indexOf("?");
            if (indexQM >= 0) {
                // the URL has parameters => convert them to hidden form inputs
                var params = url.substring(indexQM + 1).split("&");
                for (var i = 0; i < params.length; i++) {
                    var keyValuePair = params[i].split("=");
                    var input = document.createElement("INPUT");
                    input.type = "hidden";
                    input.name = keyValuePair[0];
                    input.value = keyValuePair[1];
                    f.appendChild(input);
                }
            }

            document.body.appendChild(f);
            f.submit();
        }
    </script>
    <script type="text/javascript" language="javascript">




        function CallAutoScan(visitorid, connection) {
            try {
                var url = "";
                //            if (!window.location.origin) {
                //                // url = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '') + "/unotest14/ScanDocument.aspx";
                //                url = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '') + "/ScanDocument.aspx";
                //                url = url.replace(/ /g, ',');
                //            }
                //            else {
                //               // url = window.location.origin + "/unotest14/ScanDocument.aspx";
                //                url = window.location.origin + "/ScanDocument.aspx"; // For Save Document
                //                url = url.replace(/ /g, ',');
                //            }
                url = window.location.href
                var filename = url.substr(url.lastIndexOf("/") + 1);
                url = url.replace(filename, "ScanDocument.aspx");


                //  connection = connection.replace(/ /g, ',');
                var connection = document.getElementById('<%= hdConn.ClientID %>').value;

                //alert(connection);

                var empCode = visitorid;
                var WshShell = new ActiveXObject("WScript.Shell");
                var Return = WshShell.Run("C:/Visitor/ScanDocument/AutoScan.exe " + empCode + " " + connection + " " + url, 1, true);
                // return false;
                //alert(Return);
                $("#ContentPlaceHolder1_ContentPlaceHolder1_btnSearch").click();

                SomeFunction();
                //this.location.reload(true);
                //location.href = location.href;
            }
            catch (ex) {
                alert(ex.Message);
            }
        }

        function SomeFunction() {

            // document.getElementById("ContentPlaceHolder1_ContentPlaceHolder1_btnSearch").disabled = true;
            $('<%= btnSearch.ClientID %>>').click();
            //javascript: __doPostBack('ContentPlaceHolder1_ContentPlaceHolder1_btnSearch', '');
        }

        function poponload(_id) {
            testwindow = window.open("VMSNewGP_Print.aspx?ID=" + _id, "mywindow", "location=1,status=1,scrollbars=1,width=500,height=500,top=200,left=400");

            return false;

            testwindow.moveTo(0, 0);

        }
        function PrintPanel() {

            var panel = document.getElementById("<%=pnlVisitor.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }


    </script>
    <script type="text/javascript" language="javascript">

        function FingerTemplate(RequestId, connstring) {
            try {
                //   navigateToUrl("GatepassRegistration.aspx");

                var url = "";
                //            if (!window.location.origin) {
                //                url = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '') + "/unotest14/ScanDocument.aspx";
                //                url = url.replace(/ /g, ',');
                //            }
                //            else {
                //                url = window.location.origin + "/unotest14/ScanDocument.aspx"; // For delete document
                //                url = url.replace(/ /g, ',');
                //            }
                url = window.location.href;
                var filename = url.substr(url.lastIndexOf("/") + 1);
                url = url.replace(filename, "ScanDocument.aspx");

                var empCode = document.getElementById("<%=hdfEmpcode.ClientID %>").value;
                // connstring = connstring.replace(/ /g, ',');

                var connstring = document.getElementById('<%= hdConn.ClientID %>').value;
                //                alert(connstring);

                var WshShell = new ActiveXObject("WScript.Shell");
                var Return = WshShell.Run("C:/Visitor/Identification/VMS_Enrollment.exe " + RequestId + " " + url + " " + connstring + " " + empCode, 1, true);
                SomeFunction();
                //   return false;
                //  __doPostBack("<%=gvGatePass.UniqueID %>", "");
            }
            catch (ex) {
                alert(ex.Message);
            }
        }


        function Sign(EmpCode, Mode) {
            try {

                var gridView = document.getElementById('<%= this.gvGatePass.ClientID %>');
                gridView.disabled = true;
                gridView.style.cursor = 'wait';

                var ExePath = "C:\\Visitor\\";

                var gridViewControls = gridView.getElementsByTagName("a");

                for (i = 0; i < gridViewControls.length; i++) {
                    gridViewControls[i].disabled = true;
                    gridViewControls[i].style.cursor = 'wait';
                }
                if (Mode == "Edit") { 




                }
                else {
                    var PageName = document.getElementById('<%=hdnPageName.ClientID%>').value;
                    var UserId = document.getElementById('<%=hdnUserId.ClientID%>').value;
                    var WshShell = new ActiveXObject("WScript.Shell");

                    var Return = WshShell.Run(ExePath + "SignBio\\NativeTemplate.exe " + EmpCode + " ISO Enrollment Sign" + " " + ' "' + "0" + '" ' + " " + ' "' + PageName + '" ' + " " + ' "' + UserId + '" ""', 1, true);
                    gridView.disabled = false;
                    gridView.style.cursor = 'default';
                    for (i = 0; i < gridViewControls.length; i++) {
                        gridViewControls[i].disabled = false;
                        gridViewControls[i].style.cursor = 'default';
                    }

                }
                SomeFunction();
                //this.location.reload(true);
                //location.href = location.href;
                //$("#ContentPlaceHolder1_ContentPlaceHolder1_btnSearch").click();
            }
            catch (ex) {
                alert(ex.Message);

            }
        }



    </script>
    <script type="text/javascript" language="javascript">

        function WebCam(Visitor, connstring, localIPs) {
            try {
                var url = "";
                //            if (!window.location.origin) {
                //                url = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '') + "/unotest14/SavePhoto.aspx";
                //                url = url.replace(/ /g, ',');
                //                localIPs = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
                //            }
                //            else {
                //                url = window.location.origin + "/unotest14/SavePhoto.aspx"; // For delete document
                //                url = url.replace(/ /g, ',');
                //                localIPs = window.location.origin;
                //            }
                if (Visitor == '')
                    return false;

                url = window.location.href;
                var filename = url.substr(url.lastIndexOf("/") + 1);
                url = url.replace(filename, "SavePhoto.aspx");


                //connstring = connstring.replace(/ /g, ',');
                var connstring = document.getElementById('<%= hdConn.ClientID %>').value;

                var empCode = document.getElementById("<%=hdfEmpcode.ClientID %>").value;

                var WshShell = new ActiveXObject("WScript.Shell");
                var Return = WshShell.Run("C:/Visitor/WebCam/WinFormCharpWebCam.exe " + Visitor + " " + url + " " + connstring + " " + empCode + " " + localIPs, 1, true);
                //alert(Return);
                //  return false;
                //  __doPostBack("<%=gvGatePass.UniqueID %>", "");
                SomeFunction();
                //this.location.reload(true);
                //location.href = location.href;
            }
            catch (ex) {
                alert(ex.Message);
            }
        }


    </script>
    <script type="text/javascript">
        function WriteTemplateOnCard() {
            //debugger;
            try {

                var MasterKey = "";
                var DosKey = "";
                var ServerFlag = false;
                var KeyA = "";
                var KeyB = "";

                var ECode = document.getElementById("<%= txtVisitor.ClientID %>").innerHTML;
                var EName = document.getElementById("<%= txtVisitorName.ClientID %>").innerHTML;
                var Template1 = document.getElementById("<%= hdnFinger1.ClientID %>").value;
                var Template2 = document.getElementById("<%= hdFinger2.ClientID %>").value;

                var requestid = document.getElementById("<%= lblRequestID.ClientID %>").innerHTML;

                var ActivationDate = document.getElementById("<%= hdnactivationdate.ClientID %>").value;
                //ActivationDate = parseInt(ActivationDate, 10).toString(16).toUpperCase();

                ActivationDate = ActivationDate.replace(/\//g, "");

                var ExpiryDate = document.getElementById("<%= hdnexpirydate.ClientID %>").value;
                //ExpiryDate = parseInt(ExpiryDate, 10).toString(16).toUpperCase();
                ExpiryDate = ExpiryDate.replace(/\//g, "");

                var ActivationTime = document.getElementById("<%= txtStartTime.ClientID %>").innerHTML;
                ActivationTime = ActivationTime.replace(/:/g, "");

                var ExpiryTime = document.getElementById("<%= txtToTime.ClientID %>").innerHTML;
                ExpiryTime = ExpiryTime.replace(/:/g, "");
                var CenterCode = "B8";
                var LocationCode = document.getElementById("<%= ddlLocation.ClientID %>").value;
                var AadharNo = "00";
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

                cardCSNR = objReadCard.CSNR.replace(/ /g, "").substring(0, 8);
                cardCSNR = ReverseCSNR(cardCSNR);
                var CSNRExist = false;
                $.ajax({
                    url: "GatepassRegistration.aspx/CheckCSNR",
                    type: "POST",
                    dataType: "json",
                    data: "{'CSNR':'" + cardCSNR + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d == "True") {

                            CSNRExist = true;
                        }
                        else {
                            alert("Please add card to inventory before personalisation");
                            CSNRExist = false;
                            return false;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });
                if (CSNRExist == false) {

                    return;
                }


                $.ajax({
                    url: "GatepassRegistration.aspx/GenerateKeyWithoutEmployeeType",
                    type: "POST",
                    dataType: "json",
                    data: "{'CSNR':'" + cardCSNR + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d == "") {
                            alert("Key Generation Failed.");
                            ServerFlag = false;
                        }
                        else {
                            var GenKeys = msg.d.split(",");
                            KeyA = GenKeys[0];
                            KeyB = GenKeys[1]
                            ServerFlag = true;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });
                if (ServerFlag == false) {
                    return false;
                }

                var WriteData = "";
                var EmployeeCode = ECode;
                var EmployeeCodePrefix = EmployeeCode.substr(0, 2);
                var EmpCode = EmployeeCode.substr(2, EmployeeCode.length);
                var EmpName = EName.rpad(" ", 32);
                var Priviledge = "";
                if (document.getElementById("<%= chkBiometricBypass.ClientID %>").checked) {
                    Priviledge = Priviledge + "1";
                }
                else {
                    Priviledge = Priviledge + "0";
                }
                if (document.getElementById("<%= chkHolidayBypass.ClientID %>").checked) {
                    Priviledge = Priviledge + "1";
                }
                else {
                    Priviledge = Priviledge + "0";
                }
                if (document.getElementById("<%= chkAntipassBackBypass.ClientID %>").checked) {
                    Priviledge = Priviledge + "1";
                }
                else {
                    Priviledge = Priviledge + "0";
                }
                if (document.getElementById("<%= chkTimezoneBypass.ClientID %>").checked) {
                    Priviledge = Priviledge + "1";
                }
                else {
                    Priviledge = Priviledge + "0";
                }
                if (document.getElementById("<%= chkPinBypass.ClientID %>").checked) {
                    Priviledge = Priviledge + "1";
                }
                else {
                    Priviledge = Priviledge + "0";
                }
                Priviledge = Priviledge.rpad("0", 8);
                WriteData = ConvertStringToHex(EmployeeCodePrefix) + ConvertStringToHex(EmpCode).toUpperCase() + ConvertStringToHex(EmpName).toUpperCase() + ActivationDate + ActivationTime + ExpiryDate + ExpiryTime + parseInt(Priviledge, 2).toString(16).lpad("0", 2).toUpperCase() + "03" + "02" + (Template1.length / 2).toString(16).lpad("0", 4).toUpperCase() + (Template2.length / 2).toString(16).lpad("0", 4).toUpperCase() + AadharNo.lpad("0", 12) + CenterCode + LocationCode + "00000000" + "000000000000000000" + "01" + Template1 + Template2;

                //alert(WriteData);
                var Sector = 32;
                for (var i = 0, j = 128; i < 16 && Sector < 36; i++, j++) {
                    if (i == 15) {
                        data = objReadCard.Authenticate(j, "FFFFFFFFFFFF", 96);
                        if (data != "") {
                            alert("Unable to Authenticate Block " + j);
                            return false;
                        }
                        //var key = "FFFFFFFFFFFF" + "FF078069" + "FFFFFFFFFFFF";
                        var key = KeyA + "78778800" + KeyB;
                        //alert(key);
                        key = objReadCard.Convert(key);
                        data = objReadCard.WriteData(key, parseInt(j, 10).toString(16).toUpperCase());
                        if (data != "") {
                            alert("Card Error: Unable to write in the card.Sector " + Sector + " Block " + j);
                            return false;
                        }
                        //alert("Key Sector " + Sector + " Block " + j + " : " + key);
                        i = 0 - 1;
                        Sector = Sector + 1;
                    }
                    else {
                        var strWriteData = WriteData.substring(0, 32).rpad("0", 32);
                        WriteData = WriteData.substring(32, WriteData.length);
                        //alert("Data Sector " + Sector + " Block " + j + " : " + strWriteData);
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

                alert("Card is Personalised");

                var empcode = document.getElementById("<%= txtVisitor.ClientID %>").innerHTML;
                var Password = "";
                var cardno = cardCSNR;
                // Database Updation Start

                //alert("{'EmpCode':'" + empcode + "','CSNR':'" + cardno + "','RequestID':'" + requestid + "'}");

                var UpdateDatabase = false;
                $.ajax({
                    url: "GatepassRegistration.aspx/CompletePersonalisation",
                    type: "POST",
                    dataType: "json",
                    //data: "{'EmpCode':'" + empcode + "','pin':'" + pin + "','CSNR':'" + cardCSNR + "','Password':'" + Password + "'}",
                    //data: "{'EmpCode':'" + empcode + "','CSNR':'" + cardno + "','RequestID':" + requestid + "}",
                    data: "{'EmpCode':'" + empcode + "','CSNR':'" + cardno + "','RequestID':'" + requestid + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        //  alert(msg.d);
                        if (msg.d == "True") {
                            UpdateDatabase = true;

                        }
                        else {
                            alert("Error In Updating Database.");
                            UpdateDatabase = false;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });
                if (UpdateDatabase == false) {
                    return false;
                }

                __doPostBack("<%=gvGatePass.UniqueID %>", "");


            }
            catch (ex) {
                alert(ex.Message);
            }
        }

        function ReverseCSNR(csnr) {
            try {
                var temp = "";
                for (var i = 0; i < 4; i++) {
                    //temp = temp + csnr[csnr.length - 2] + csnr[csnr.length - 1];
                    temp = temp + csnr.substr(csnr.length - 2, 1) + csnr.substr(csnr.length - 1, 1);
                    csnr = csnr.substring(0, csnr.length - 2);
                }
                return temp;
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


        function navigateToUrl(url) {
            var f = document.createElement("FORM");
            f.action = url;

            var indexQM = url.indexOf("?");
            if (indexQM >= 0) {
                // the URL has parameters => convert them to hidden form inputs
                var params = url.substring(indexQM + 1).split("&");
                for (var i = 0; i < params.length; i++) {
                    var keyValuePair = params[i].split("=");
                    var input = document.createElement("INPUT");
                    input.type = "hidden";
                    input.name = keyValuePair[0];
                    input.value = keyValuePair[1];
                    f.appendChild(input);
                }
            }

            document.body.appendChild(f);
            f.submit();
        }

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        //Added by pooja yadav
        function ResetAll() {
            $('#' + ["<%=txtName.ClientID%>", "<%=txtMobile.ClientID%>", "<%=txtVisitorSearch.ClientID%>"].join(', #')).prop('value', "");
            document.getElementById('<%=txtName.ClientID%>').focus();
            document.getElementById('<%=txtVisitorSearch.ClientID%>').focus();
            document.getElementById('<%=txtMobile.ClientID%>').focus();
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvGatePass.ClientID%>').focus();
            return false;
        }

    </script>
    <%-- <script type="text/javascript">
        if ("ActiveXObject" in window) { }
        else { alert("This page will work only in IE 8 & 9"); window.history.back(); }
      
    </script>--%>
    <style type="text/css">
        .removeBorder
        {
            border: none;
        }
        .labelStyle
        {
            padding-left: 1%;
            font-weight: bold;
            font-size: 12px;
        }
        .dropdownStyle
        {
            padding-left: 1%;
            font-weight: bold;
            font-size: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdfEmpcode" runat="server" />
    <asp:HiddenField ID="hdConn" runat="server" />
    <asp:HiddenField ID="hdnPageName" runat="server" />
    <asp:HiddenField ID="hdnUserId" runat="server" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <br />
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Gatepass Registration & Issue" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <asp:Panel ID="Panel3" runat="server" DefaultButton="btnSearch">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%; text-align: left;">
                        </td>
                        <td style="width: 50%; text-align: right;">
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                                TabIndex="5" OnClick="btnReset_Click" />
                            <asp:Button ID="btnSearch" CssClass="ButtonControl" runat="server" Text="Search"
                                TabIndex="4" Style="float: right; margin-right: 3px;" OnClick="btnSearch_Click" />
                            <asp:TextBox ID="txtVisitorSearch" runat="server" TabIndex="3" Style="float: right;"
                                CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtVisitorSearch" runat="server" TargetControlID="txtVisitorSearch"
                                WatermarkText="Visitor ID" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:TextBox ID="txtName" runat="server" TabIndex="2" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtName" runat="server" TargetControlID="txtName"
                                WatermarkText="Visitor Name" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:TextBox ID="txtMobile" runat="server" TabIndex="1" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                TargetControlID="txtMobile" WatermarkText="Visitor Mobile No" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                            <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                                <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvGatePass" runat="server" AutoGenerateColumns="false" Width="100%"
                                        GridLines="None" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvGatePass_PageIndexChanging"
                                        OnRowCommand="gvGatePass_RowCommand" 
                                        OnRowDataBound="gvGatePass_RowDataBound" 
                                        >
                                        <RowStyle CssClass="gvRow" />
                                        <HeaderStyle CssClass="gvHeader" />
                                        <AlternatingRowStyle BackColor="#F0F0F0" />
                                        <PagerStyle CssClass="gvPager" />
                                        <EmptyDataTemplate>
                                            <div>
                                                <span>No Records found.</span>
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Request ID" SortExpression="RequestID" HeaderStyle-CssClass="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReq" runat="server" Font-Bold="true" Text='<%#Eval("RequestID") %>'
                                                        ForeColor="#3366FF"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Visitor ID" SortExpression="Visitor ID" HeaderStyle-CssClass="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitorID" runat="server" Font-Bold="true" Text='<%#Eval("Visitor_id") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Visitor_Name" HeaderText="Visitor Name" SortExpression="Visitor Name">
                                                <ItemStyle HorizontalAlign="Center" Wrap="true" Font-Bold="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="VisitorCompany" HeaderText="Visitor Company" SortExpression="Visitor Company">
                                                <ItemStyle HorizontalAlign="Center" Wrap="true" Font-Bold="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="mobileNo" HeaderText="Mobile No" SortExpression="Mobile No">
                                                <ItemStyle HorizontalAlign="Center" Wrap="true" Font-Bold="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Approval_Authority_Name" HeaderText="Authority Name" SortExpression="Authority Name">
                                                <ItemStyle HorizontalAlign="Center" Wrap="true" Font-Bold="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="From_Date" HeaderText="Valid From" SortExpression="Valid From">
                                                <ItemStyle HorizontalAlign="Center" Wrap="true" Font-Bold="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="To_Date" HeaderText="Valid To" SortExpression="Valid To">
                                                <ItemStyle HorizontalAlign="Center" Wrap="true" Font-Bold="true" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Finger" SortExpression="Finger" HeaderStyle-CssClass="center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkFinger" runat="server" CausesValidation="False" CommandName="Finger"
                                                        CommandArgument='<%#Eval("RequestID") %>' Text="Capture" Font-Bold="true" ForeColor="#3366FF"
                                                        OnClick="UpdateGrid"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Photo" SortExpression="Photo" HeaderStyle-CssClass="center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkPhoto" runat="server" CausesValidation="False" CommandName="Photo"
                                                        CommandArgument='<%#Eval("Visitor_ID") %>' Text="Capture" Font-Bold="true" ForeColor="#3366FF"
                                                        OnClick="UpdateGrid"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Signature">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSign" runat="server" Text='<%#Eval("RequestID")%>' Visible="false"></asp:Label>
                                                    <asp:LinkButton ID="lnkSignCapture" runat="server" CommandName="AddSign" CommandArgument='<%#Eval("Visitor_ID") %>'
                                                        Text="Capture" Font-Bold="true" ForeColor="#3366FF" Visible="false"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkSignEdit" runat="server" CommandName="EditSign" CommandArgument='<%#Eval("Visitor_ID") %>'
                                                        Text="Modify" Font-Bold="true" ForeColor="#3366FF" Visible="false"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Scan Document" SortExpression="Scan Document" HeaderStyle-CssClass="center">
                                                <%--<ItemTemplate>
                                                    <asp:LinkButton ID="lnkScanDocumnent" runat="server" CausesValidation="False" CommandName="ScanDocumnent"
                                                        CommandArgument='<%#Eval("Visitor_ID") %>' Text="Capture" Font-Bold="true" ForeColor="#3366FF"></asp:LinkButton>
                                                </ItemTemplate>--%>

                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkScanDocumnent" runat="server" CausesValidation="False" CommandName="ScanDocumnent"
                                                        CommandArgument='<%#Eval("RequestID") %>' Text="Capture" Font-Bold="true" ForeColor="#3366FF"></asp:LinkButton>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Personalize Card" SortExpression="Personalize Card"
                                                Visible="true" HeaderStyle-CssClass="center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkWriteOnCard" runat="server" CausesValidation="False" CommandName="WriteOnCard"
                                                        CommandArgument='<%#Eval("RequestID") %>' Text="Personalize Card" Font-Bold="true"
                                                        ForeColor="#3366FF"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Visitor Card No" SortExpression="Visitor Card" HeaderStyle-CssClass="center"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitorCardNO" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Paper Pass" SortExpression="Paper Pass" ItemStyle-Width="10%"
                                                HeaderStyle-CssClass="center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkPaperPass" runat="server" CausesValidation="False" CommandName="PaperPass"
                                                        CommandArgument='<%#Eval("RequestID") %>' Text="Print" Font-Bold="true" ForeColor="#3366FF"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="text-align: left; width: 15%;">
                                                        <span>Go To :</span><asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ChangePage">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:Button ID="btnPrevious" CssClass="ButtonControl" runat="server" Text="Previous"
                                                            OnClick="gvPrevious" />
                                                        <asp:Label ID="lblShowing" runat="server" Text="Showing "></asp:Label>
                                                        <asp:Button ID="btnNext" CssClass="ButtonControl" runat="server" Text="Next" OnClick="gvNext" />
                                                    </td>
                                                    <td style="text-align: right; width: 15%;">
                                                        <asp:Label ID="lblTotal" runat="server" Text="Total Records"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </PagerTemplate>
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:AsyncPostBackTrigger ControlID="btnSearch" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                            <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
                                TargetControlID="UpdatePanel2">
                                <Animations>
                        <OnUpdating>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="btnSearch" Enabled="false" />
                                <ScriptAction Script="onUpdating();" />  
                                <FadeOut Duration="1.0" Fps="24" minimumOpacity=".5" />
                            </Parallel> 
                        </OnUpdating>
                        <OnUpdated>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="btnSearch" Enabled="true" />
                                <ScriptAction Script="onUpdated();" /> 
                                <FadeIn Duration="1.0" Fps="24"  minimumOpacity=".5"/>
                            </Parallel> 
                        </OnUpdated>
                                </Animations>
                            </ajaxToolkit:UpdatePanelAnimationExtender>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div style="text-align: center">
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gvGatePass" />
                <%--<asp:AsyncPostBackTrigger ControlID="btnSearch" />--%>
                <%--    <asp:AsyncPostBackTrigger ControlID="BtnSave" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnPersonalDetailSave" />
            </Triggers>
        </asp:UpdatePanel>
        <div style="width: 95%; text-align: left;">
            <table width="32%">
                <tr>
                    <td>
                        <div style="background-color: Red; border: 1 Black; width: 10px; height: 10px;">
                        </div>
                    </td>
                    <td>
                        Card Personalization Pending
                    </td>
                    <td>
                        <div style="background-color: Green; border: 1 Black; width: 10px; height: 10px;">
                        </div>
                    </td>
                    <td>
                        Card Personalization Completed
                    </td>
                </tr>
            </table>
        </div>
    </center>
    <asp:Button ID="Button1" runat="server" Style="display: none" CssClass="ButtonControl"
        Font-Bold="true" TabIndex="5" Text="Modify" />
    <asp:Panel ID="pnlWriteOnCard" runat="server" CssClass="PopupPanel" Style="width: 55%">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="table1" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                    class="TableClass">
                    <tr>
                        <td colspan="6">
                            <asp:Label ID="Label1" runat="server" Text="Personalize Card" ForeColor="RoyalBlue"
                                Font-Size="20px" Width="100%" CssClass="heading">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
                <table runat="server" width="100%" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                    <tr style="border: 1;">
                        <td style="display: none;">
                            <asp:Label ID="lblRequestID" runat="server" Text="Request ID :"></asp:Label>
                        </td>
                        <td style="width: 20%;" class="labelStyle">
                            <asp:Label ID="lblVisitor" runat="server" Text="Visitor ID :"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 20%;">
                            <asp:Label ID="txtVisitor" runat="server"></asp:Label>
                        </td>
                        <td style="width: 20%;" class="labelStyle">
                            <asp:Label ID="lblVisitorN" runat="server" Text="Visitor Name :"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 20%;">
                            <asp:Label ID="txtVisitorName" runat="server"></asp:Label>
                        </td>
                        <td rowspan="4" style="width: 20%;">
                            <asp:Image ID="imgEmployeeImage" ImageUrl="~/Handler1.ashx" runat="server" Width="100%"
                                Height="108px" Style="text-align: center; padding-left: 15%; padding-right: 15%;"
                                ClientIDMode="Static" BorderWidth="1px" ImageAlign="Middle" />
                        </td>
                    </tr>
                    <tr>
                        <td class="labelStyle">
                            <asp:Label ID="lblStartDt" runat="server" Text="From Date :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:Label ID="txtStartDate" runat="server"></asp:Label>
                        </td>
                        <td class="labelStyle">
                            <asp:Label ID="lblStT" runat="server" Text="From Time :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:Label ID="txtStartTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelStyle">
                            <asp:Label ID="lblToDt" runat="server" Text="To Date :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:Label ID="txtToDate" runat="server"></asp:Label>
                        </td>
                        <td class="labelStyle">
                            <asp:Label ID="lblTDt" runat="server" Text="To Time :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:Label ID="txtToTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelStyle">
                            <asp:Label ID="lblNationlity" runat="server" Text="Nationality :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:Label ID="txtNationality" runat="server"></asp:Label>
                        </td>
                        <td class="labelStyle">
                            <asp:Label ID="lblPassport" runat="server" Text="Passport No :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:Label ID="txtPassport" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelStyle">
                            <asp:Label ID="lblCompany" runat="server" Text="Company Name :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:Label ID="txtCompany" runat="server"></asp:Label>
                        </td>
                        <td class="labelStyle">
                            <asp:Label ID="lblVisitorCompany" runat="server" Text="Company Address :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:Label ID="txtVisitorCompAddress" runat="server"></asp:Label>
                        </td>
                        <td rowspan="2" style="width: 20%;">
                            <asp:Image ID="ImageSignPerso" runat="server" Height="51px" Width="100%" ClientIDMode="Static"
                                BorderWidth="1px" ImageAlign="Middle" TabIndex="5" AlternateText="Signature Not Found"
                                Style="text-align: center; padding-left: 15%; padding-right: 15%;" ImageUrl="~/EmpImage/signature.JPG" />
                        </td>
                    </tr>
                    <tr>
                        <td class="labelStyle">
                            <asp:Label ID="lblCity" runat="server" Text="City :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:Label ID="txtCity" runat="server"></asp:Label>
                        </td>
                        <td class="labelStyle">
                            <asp:Label ID="lblLocation" runat="server" Text="Location :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="ComboControl">
                                <asp:ListItem Selected="True" Value="CB" Text="SAC"></asp:ListItem>
                                <%--   <asp:ListItem Value="CD" Text="DES"></asp:ListItem>
                                <asp:ListItem Value="CE" Text="BOPAL"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="labelStyle">
                            <asp:Label ID="lblPriviledge" Visible="false" runat="server" Text="Priviledge :"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:CheckBox ID="chkBiometricBypass" runat="server" Text="Biometric Bypass" Style="margin-left: 10px;
                                display: none;" /><br />
                            <asp:CheckBox ID="chkHolidayBypass" runat="server" Text="Holiday Bypass" Style="margin-left: 10px;
                                display: none;" /><br />
                            <asp:CheckBox ID="chkAntipassBackBypass" runat="server" Text="AntipassBack Bypass"
                                Style="margin-left: 10px; display: none;" /><br />
                            <asp:CheckBox ID="chkTimezoneBypass" runat="server" Text="Timezone Bypass" Style="margin-left: 10px;
                                display: none;" /><br />
                            <asp:CheckBox ID="chkPinBypass" runat="server" Text="PIN Bypass" Style="margin-left: 10px;
                                display: none;" />
                            <%--<asp:DropDownList ID="ddlPriviledge" runat="server" CssClass="ComboControl" TabIndex="4"
                                Enabled="true" ValidationGroup="CardPerso">
                                <asp:ListItem Value="01" Selected="True">Biometric Bypass</asp:ListItem>
                                <asp:ListItem Value="02">Holiday Bypass</asp:ListItem>
                                <asp:ListItem Value="03">AntipassBack Bypass</asp:ListItem>
                                <asp:ListItem Value="04">Timezone Bypass</asp:ListItem>
                                <asp:ListItem Value="05">PIN Bypass</asp:ListItem>
                            </asp:DropDownList>--%>
                        </td>
                        <%-- <td class="labelStyle">
                        </td>
                        <td class="dropdownStyle">
                        </td>--%>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdnFinger1" runat="server" />
                            <asp:HiddenField ID="hdFinger2" runat="server" />
                            <asp:HiddenField ID="hdnactivationdate" runat="server" />
                            <asp:HiddenField ID="hdnexpirydate" runat="server" />
                        </td>
                    </tr>
                </table>
                <table runat="server" width="100%" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                    <tr>
                        <td style="text-align: center; padding-top: 1%">
                            <asp:Button ID="btnPersonalDetailSave" runat="server" CssClass="ButtonControl" Font-Bold="true"
                                TabIndex="4" Text="Click To Personalise" ValidationGroup="Modify" 
                                OnClientClick="return WriteTemplateOnCard();" 
                                 />
                                 <%--onclick="btnPersonalDetailSave_Click"--%>
                            &nbsp;
                            <asp:Button ID="btnPersonalDetailCancel" runat="server" CausesValidation="False"
                                CssClass="ButtonControl" Font-Bold="true" TabIndex="5" Text="Cancel" OnClick="btnPersonalDetaiCancel_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="btnModify" runat="server" Style="display: none" CssClass="ButtonControl"
        Font-Bold="true" TabIndex="5" Text="Modify" />
    <ajaxToolkit:ModalPopupExtender ID="mpWriteOnCard" runat="server" TargetControlID="btnModify"
        PopupControlID="pnlWriteOnCard" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnPersonalDetailCancel">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlCamera" runat="server" CssClass="PopupPanel" Width="60%" Height="70%">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                </table>
                <iframe src="WebCamVisitor.aspx" width="100%" height="400px">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="center">
                                <u>Live Camera</u>
                            </td>
                            <td>
                            </td>
                            <td align="center">
                                <u>Captured Picture</u>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="Div1">
                                </div>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Image ID="Image1" runat="server" Style="visibility: hidden; width: 320px; height: 240px" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Button ID="Button2" Text="Capture" runat="server" OnClientClick="return Capture();" />
                    <br />
                    <span id="Span1"></span></iframe>
                </td></tr>
                <tr>
                    <td>
                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnCaptureClose_Click" />
                    </td>
                </tr>
                <td>
                </td>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="btntemp" runat="server" Style="display: none" CssClass="ButtonControl"
        Font-Bold="true" TabIndex="5" Text="Modify" />
    <ajaxToolkit:ModalPopupExtender ID="mpeCamera" runat="server" TargetControlID="btntemp"
        PopupControlID="pnlCamera" BackgroundCssClass="modalBackground" Enabled="true">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlVisitor" CssClass="PopupPanel" runat="server" Width="70%" Height="90%"
        ScrollBars="Vertical">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="innerPnlVisitor" ForeColor="Black" Width="100%" runat="server">
                    <asp:Panel ID="Panel1" runat="server" BorderWidth="1" Width="100%">
                        <table id="Table2" runat="server" border="1" style="color: Black; border-color: Black;
                            border: 1 solid; width: 100%; font-family: Times New Roman">
                            <tr>
                                <td colspan="6" style="text-align: center; font-size: 12px">
                                    <center>
                                        <table style="font-weight: bold">
                                            <tr>
                                                <td style="text-align: center">
                                                    भारत सरकार / Goverment of India
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">
                                                    अंतरिक्ष विभाग / Department of Space
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">
                                                    अंतरिक्ष अनुप्रयोग केंद्र / Space Applications Center (SAC)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">
                                                    अहमदाबाद / Ahmedabad -380015
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding-top: 1%">
                                                    आगंतुक पास / Visitor's Pass/Card and Biometric 
                                                </td>
                                            </tr>
                                        </table>
                                    </center>
                                </td>
                                <td rowspan="11" style="width: 29%;">
                                    <center>
                                        <table style="font-size: 12px">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <center>
                                                        <asp:Image ID="imgVisitor" runat="server" Style="width: 100px; height: 100px;" />
                                                    </center>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <p style="text-align: center; font-size: 9px; margin-bottom: 0px">
                                                        I am aware that SAC id declared as prohibited area under official secret ACT 1923,and
                                                        I will abide by security instructions followed in the center.
                                                    </p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-top: 0px">
                                                    <p style="text-align: center; font-size: 9px; margin-top: 0px">
                                                        मुझे ज्ञात है कि सैक को सरकारी गोपनीय अधिनियम १९२३ के अधीन निषिद्ध क्षेत्र घोषित
                                                        किया हुआ है, तथा मै केंद्र मे लागू सुरक्षा नियमो का पालन करूँगा |</p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding-top: 10%; font-size: 11px; border-bottom: 1 Black;">
                                                    <div>
                                                        <asp:Image ID="imgSign" runat="server" Height="30px" Style="text-align: center;"
                                                            Width="89px" ClientIDMode="Static" BorderWidth="1px" ImageAlign="Middle" TabIndex="5"
                                                            AlternateText="Signature Not Found" ImageUrl="~/EmpImage/signature.JPG" />
                                                    </div>
                                                    ------------------------------------------ <b>आगंतुक के हस्ताक्षर</b>
                                                    <div>
                                                        Signature of Visitor</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; padding-left: 10%; padding-top: 3%; font-size: 11px">
                                                    <b>
                                                        <ul>
                                                            <li>कार्ड अहस्तान्तरणीय है
                                                                <div style="font-size: 12px">
                                                                    Card is non transferable.</div>
                                                            </li>
                                                            <li>मात्र निर्धारित क्षेत्रों के लिए वैध है
                                                                <div style="font-size: 12px">
                                                                    Valid for the identified
                                                                </div>
                                                                <div>
                                                                    areas only.</div>
                                                            </li>
                                                            <li>मुलाक़ात के बाद कार्ड स्वागत
                                                                <div>
                                                                    कक्ष को लौटा दें</div>
                                                                <div style="font-size: 12px; margin: 0px 0px 0px 0px">
                                                                    Return the card at the
                                                                    <div>
                                                                        reception after visit.</div>
                                                                </div>
                                                            </li>
                                                        </ul>
                                                    </b>
                                                </td>
                                            </tr>
                                        </table>
                                    </center>
                                </td>
                            </tr>
                            <tr style="font-size: 12px">
                                <td style="padding-left: 2px">
                                    आगंतुक आईडी/ Visitor ID
                                </td>
                                <td style="padding-left: 2px; width: 10%">
                                    <asp:Label ID="lblVisitorIDPass" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width: 10%; padding-left: 2px">
                                    प्रवेश पत्र सं. / Gate-Pass No
                                </td>
                                <td style="padding-left: 2px">
                                    <asp:Label ID="lblGatePassNo" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width: 10%; padding-left: 2px">
                                    आगंतुक कार्ड सं./ Visitor Card No.
                                </td>
                                <td style="padding-left: 2px; width: 10%;">
                                    <%--<asp:TextBox ID="lblVisitorCardNo" runat="server" MaxLength="12"></asp:TextBox>--%>
                                    <asp:Label ID="lblVisitorCardNo" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr style="font-size: 12px">
                                <td style="padding-left: 2px">
                                    आगंतुक / Visitor
                                </td>
                                <td style="padding-left: 2px; width: 20%" colspan="5">
                                    <asp:Label ID="lblVisitorType" runat="server" Text=""></asp:Label>
                                </td>
                                <%-- <td style="width: 17%; padding-left: 2px">
                                    प्रवेश पत्र सं. / Gate-Pass No
                                </td>
                                <td style="padding-left: 2px">
                                    <asp:Label ID="lblGatePassNo" runat="server" Text=""></asp:Label>
                                </td>--%>
                            </tr>
                            <tr style="font-size: 12px">
                                <td style="padding-left: 2px">
                                    नाम / Name
                                </td>
                                <td colspan="5" style="padding-left: 2px">
                                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr style="font-size: 12px">
                                <td style="padding-left: 2px">
                                    पदनाम / Designation
                                </td>
                                <td colspan="5" style="padding-left: 2px">
                                    <asp:Label ID="lblDesg" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr style="font-size: 12px">
                                <td style="padding-left: 2px">
                                    नागरिकता / Nationality
                                </td>
                                <td style="padding-left: 2px" colspan="5">
                                    <asp:Label ID="lblNation" runat="server" Text=""></asp:Label>
                                </td>
                                <%--<td style="width: 20%; padding-left: 2px">
                                    आगंतुक कार्ड सं./ Visitor Card No.
                                </td>
                                <td style="padding-left: 2px">
                                    <asp:TextBox ID="lblVisitorCardNo" runat="server" MaxLength="12"></asp:TextBox>
                                </td>--%>
                            </tr>
                            <tr style="font-size: 12px">
                                <td style="padding-left: 2px">
                                    कंपनी / Company
                                </td>
                                <td colspan="5" style="padding-left: 2px">
                                    <asp:Label ID="lblcomapny" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr style="font-size: 12px">
                                <td style="width: 23%; padding-left: 2px">
                                    पता एवं संपर्क नं. / Address & Contact No
                                </td>
                                <td colspan="5" style="padding-left: 2px">
                                    <asp:Label ID="lblAdd" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr style="font-size: 12px;">
                                <td style="padding-left: 2px">
                                    जिससे मिलना है / Official to be visited
                                </td>
                                <td colspan="3" style="padding-left: 2px">
                                    <asp:Label ID="lblOfficial" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="padding-left: 2px" colspan="2">
                                    <asp:Label ID="lblExtn" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr style="font-size: 12px">
                                <td style="padding-left: 2px">
                                    मुलाकात का उद्देश्य / Purpose of Visit
                                </td>
                                <td colspan="5" style="padding-left: 2px">
                                    <asp:Label ID="lblPurposeOfVisit" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr style="font-size: 12px; padding-left: 2px">
                                <td style="padding-left: 2px">
                                    <span style="color: rgb(0, 0, 0); font-family: &quot Times New Roman &quot; font-size: 12px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">
                                    आवुकएस्कन्द / Approval By 
                                </td>
                                <td colspan="5" style="padding-left: 2px">
                                    <asp:Label ID="lblApprovalId" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr style="font-size: 12px; padding-left: 2px">
                                <td style="padding-left: 2px">
                                    से वैध / Valid From
                                </td>
                                <td colspan="5" style="padding-left: 2px">
                                    <asp:Label ID="lblValidFrom" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr style="font-size: 12px; padding-left: 2px">
                                <td style="padding-left: 2px">
                                    तक वैध / Valid Till
                                </td>
                                <td colspan="5" style="padding-left: 2px">
                                    <asp:Label ID="lblValidTo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr style="font-size: 12px">
                                <td colspan="4" style="padding-left: 2px">
                                    Additional Info : 
                                    <asp:Label ID="lblAddInfo" runat="server" Text=""></asp:Label>
                                </td>
                                <td colspan="2" style="text-align: center; padding-left: 2px">
                                    <asp:Image ID="myBarCode" runat="server" Visible="false"></asp:Image>
                                </td>
                                <td style="font-size: 11px; padding-left: 2px">
                                    <br />
                                    <br />
                                    <br />
                                    <b>मुलाकाती अधिकारी के हस्ताक्षर
                                        <div>
                                            Signature of Officer Visited
                                        </div>
                                        <br />
                                        निकास समय / Out Time : </b>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <center>
                        <span style="border-bottom: 1px solid; font-size: 15px; display:none; font-family: Times New Roman;
                            color: Black">CARD ENTRY</span></center>
                               <asp:HiddenField  ID="hdnRequestID" runat="server" />
                </asp:Panel>
                <table runat="server" id="Table3" style="width: 100%; text-align: center">
                    <tr>
                        <td style="text-align: center;">
                            <input type="button" class="ButtonControl" id="btnPrint" value="Print" onclick="printOnClick()" />
                            &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                CssClass="ButtonControl" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button3" runat="server" Style="display: none" CssClass="ButtonControl"
        Font-Bold="true" TabIndex="5" Text="Modify" />
    <ajaxToolkit:ModalPopupExtender ID="mpePrintgp" runat="server" TargetControlID="Button3"
        CancelControlID="btnCancel" PopupControlID="pnlVisitor" BackgroundCssClass="modalBackground"
        Enabled="true">
    </ajaxToolkit:ModalPopupExtender>
    <script src="Scripts/Choosen/chosen.jquery.js" type="text/javascript"></script>
    <script src="Scripts/Choosen/docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "95%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
        function printOnClick() {
            // var div = document.getElementById('pnlVisitor');



            //alert(document.getElementById("<%= hdnRequestID.ClientID %>").value);
            /*Make entry to Visitor Access*/
            $.ajax({
                url: "GatepassRegistration.aspx/InsertVisitorAccessData",
                type: "POST",
                dataType: "json",
                data: "{'strRequestID':'" + document.getElementById("<%= hdnRequestID.ClientID %>").value + "'}",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    if (msg.d == "True") {

                    }
                },
                error: function () { alert("Error While Inserting in Visitor Access."); }
            });
            /*Make entry to Visitor Access*/



            var lblVisitorCardNo = document.getElementById("<%= lblVisitorCardNo.ClientID %>").value;
            //alert(lblVisitorCardNo);
            if (lblVisitorCardNo != "") {
                var divElements = document.getElementById("<%= innerPnlVisitor.ClientID %>").innerHTML;
                //Get the HTML of whole page

                var oldPage = document.body.innerHTML;

                //Reset the page's HTML with div's HTML only
                document.body.innerHTML =
              "<html><head><title></title></head><body>" +
              divElements + "</body>";
                divElements = divElements.replace('Welcome to UNO', '');
                //alert(divElements);
                //Print Page
                debugger;
                $("a").each(function () {
                    $(this).css("display", "none");
                });
                document.title = '';
                //document.querySelector('footer').style = 'display: none'
                window.print();

                //Restore orignal HTML
                document.body.innerHTML = oldPage;
            }
            else {
                alert("Please enter visitor no.");
                document.getElementById("<%= lblVisitorCardNo.ClientID %>").focus();
            }

        }

    </script>
</asp:Content>
