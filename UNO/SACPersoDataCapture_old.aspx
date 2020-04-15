<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="SACPersoDataCapture.aspx.cs" Inherits="UNO.SACPersoDataCapture" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvEmpDetails.ClientID %>');

            // get the bounds of both the gridview and the progress div
            var gridViewBounds = Sys.UI.DomElement.getBounds(gridView);
            var updateProgressDivBounds = Sys.UI.DomElement.getBounds(updateProgressDiv);

            //    do the math to figure out where to position the element (the center of the gridview)
            var x = gridViewBounds.x + Math.round(gridViewBounds.width / 2) - Math.round(updateProgressDivBounds.width / 2);
            var y = gridViewBounds.y + Math.round(gridViewBounds.height / 2) - Math.round(updateProgressDivBounds.height / 2);

            //    set the progress element to this position
            Sys.UI.DomElement.setLocation(updateProgressDiv, x, y);
        }

        function onUpdated() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it invisible
            updateProgressDiv.style.display = 'none';
        }
    </script>
    <script type="text/javascript">
        if ("ActiveXObject" in window) { }
        else { alert("This page will work only in IE 8 & 9"); window.history.back(); }
      
    </script>
    <script type="text/javascript">
        function Print(EmpCode, EmpName) {
            var WshShell = new ActiveXObject("WScript.Shell");
            var gridView = document.getElementById('<%= this.gvEmpDetails.ClientID %>');

            try {

                var Category = "0";
                var anchors = document.getElementsByTagName("a");
                for (var i = 0; i < anchors.length; i++) {
                    anchors[i].onclick = function () { return (false); };
                }
                gridView.disabled = true;
                gridView.style.cursor = 'wait';

                EmpName = EmpName.replace(/ /g, ',');

                var conn = document.getElementById('<%= hdConn.ClientID %>').value;

                Return = WshShell.Run("C:/CardDesign/KCards.exe " + EmpCode + " Print " + conn + " " + EmpName + " " + Category, 1, true);

                if (Return != "1") {
                    $(this).remove();
                }
                var anchors = document.getElementsByTagName("a");
                for (var i = 0; i < anchors.length; i++) {
                    anchors[i].onclick = function () { return (true); };
                }
                gridView.disabled = false;
                gridView.style.cursor = 'default';
                __doPostBack("<%=gvEmpDetails.UniqueID %>", "");
            }
            catch (e) {
                alert(e.Message);
                var anchors = document.getElementsByTagName("a");
                for (var i = 0; i < anchors.length; i++) {
                    anchors[i].onclick = function () { return (true); };
                }
                gridView.disabled = false;
                gridView.style.cursor = 'default';
            }
        }
    </script>

   <%-- <script type="text/javascript">

        function CallAutoScan(visitorid, reqid) {
            try {

                //                var url = "";
                //                //            if (!window.location.origin) {
                //                //                // url = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '') + "/unotest14/ScanDocument.aspx";
                //                //                url = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '') + "/ScanDocument.aspx";
                //                //                url = url.replace(/ /g, ',');
                //                //            }
                //                //            else {
                //                //               // url = window.location.origin + "/unotest14/ScanDocument.aspx";
                //                //                url = window.location.origin + "/ScanDocument.aspx"; // For Save Document
                //                //                url = url.replace(/ /g, ',');
                //                //            }
                //                url = window.location.href
                //                var filename = url.substr(url.lastIndexOf("/") + 1);
                //                url = url.replace(filename, "ScanDocument.aspx");
                //                var emp = "\"\"";

                //  connection = connection.replace(/ /g, ',');
                // var connection = document.getElementById('<%= hdConn.ClientID %>').value;
                alert(visitorid);
                var empid = visitorid;
                var WshShell = new ActiveXObject("WScript.Shell");
                var Return = WshShell.Run("C:/Parso/Image_Uploader/Image_Uploader.exe " + empid + " " + reqid, 1, true);
                // return false;


            }
            catch (ex) {
                alert(ex.Message);
            }
        }

        //C:/Parso/Image_Uploader/Image_Uploader.exe "
    
    </script>--%>

    <script type="text/javascript">


        var oShell = new ActiveXObject("Shell.Application");
        var WshShell = new ActiveXObject("WScript.Shell");

        //var ExePath = "C:\\Perso\\";
        function AddImage(EmpCode) {

           // debugger;
            try {

                var gridView = document.getElementById('<%= this.gvEmpDetails.ClientID %>');
                gridView.disabled = true;
                gridView.style.cursor = 'wait';
                var gridViewControls = gridView.getElementsByTagName("a");

                var img = "Image";


                for (i = 0; i < gridViewControls.length; i++) {
                    gridViewControls[i].disabled = true;
                    gridViewControls[i].style.cursor = 'wait';
                }

                //var Return = WshShell.Run("C:/Enrollment/NativeTemplate.exe " + val + " ISO Enrollment Image", 1, true);


              //  alert("C:/Parso/Image_Uploader/Image_Uploader.exe " + EmpCode);

                var Return = WshShell.Run("C:/Perso/Image_Uploader/Image_Uploader.exe " + EmpCode + " " + img, 1, true);


                gridView.disabled = false;
                gridView.style.cursor = 'default';
                for (i = 0; i < gridViewControls.length; i++) {
                    gridViewControls[i].disabled = false;
                    gridViewControls[i].style.cursor = 'default';
                }


                __doPostBack("<%=gvEmpDetails.UniqueID %>", "");

            }
            catch (ex) {
                alert(ex.Message);
            }
        }


//        var oShell = new ActiveXObject("Shell.Application");
//        var WshShell = new ActiveXObject("WScript.Shell");

//        var ExePath = "C:\\Perso\\";
//        function AddImage(EmpCode) {
//            try {

//                var gridView = document.getElementById('<%= this.gvEmpDetails.ClientID %>');
//                gridView.disabled = true;
//                gridView.style.cursor = 'wait';
//                var gridViewControls = gridView.getElementsByTagName("a");

//                for (i = 0; i < gridViewControls.length; i++) {
//                    gridViewControls[i].disabled = true;
//                    gridViewControls[i].style.cursor = 'wait';
//                }

//                //var Return = WshShell.Run("C:/Enrollment/NativeTemplate.exe " + val + " ISO Enrollment Image", 1, true);

//                var Return = WshShell.Run(ExePath + "\\Image\\Image_Capture.exe " + EmpCode, 1, true);


//                gridView.disabled = false;
//                gridView.style.cursor = 'default';
//                for (i = 0; i < gridViewControls.length; i++) {
//                    gridViewControls[i].disabled = false;
//                    gridViewControls[i].style.cursor = 'default';
//                }


//                __doPostBack("<%=gvEmpDetails.UniqueID %>", "");

//            }
//            catch (ex) {
//                alert(ex.Message);
//            }
        //        }


        function Sign(EmpCode) {

            // debugger;
            try {

                var gridView = document.getElementById('<%= this.gvEmpDetails.ClientID %>');
                gridView.disabled = true;
                gridView.style.cursor = 'wait';
                var gridViewControls = gridView.getElementsByTagName("a");

                var sign = "Signature";


                for (i = 0; i < gridViewControls.length; i++) {
                    gridViewControls[i].disabled = true;
                    gridViewControls[i].style.cursor = 'wait';
                }

                var Return = WshShell.Run("C:/Perso/Image_Uploader/Image_Uploader.exe " + EmpCode + " " + sign, 1, true);

                gridView.disabled = false;
                gridView.style.cursor = 'default';
                for (i = 0; i < gridViewControls.length; i++) {
                    gridViewControls[i].disabled = false;
                    gridViewControls[i].style.cursor = 'default';
                }

                __doPostBack("<%=gvEmpDetails.UniqueID %>", "");

            }
            catch (ex) {
                alert(ex.Message);
            }
        }



//        function Sign(EmpCode) {
//            try {
//                var gridView = document.getElementById('<%= this.gvEmpDetails.ClientID %>');
//                gridView.disabled = true;
//                gridView.style.cursor = 'wait';
//                var gridViewControls = gridView.getElementsByTagName("a");

//                for (i = 0; i < gridViewControls.length; i++) {
//                    gridViewControls[i].disabled = true;
//                    gridViewControls[i].style.cursor = 'wait';
//                }

//                var Return = WshShell.Run(ExePath + "SignBio\\NativeTemplate.exe " + EmpCode + " ISO Enrollment Sign", 1, true);

//                gridView.disabled = false;
//                gridView.style.cursor = 'default';
//                for (i = 0; i < gridViewControls.length; i++) {
//                    gridViewControls[i].disabled = false;
//                    gridViewControls[i].style.cursor = 'default';
//                }

//                __doPostBack("<%=gvEmpDetails.UniqueID %>", "");

//            }
//            catch (ex) {
//                alert(ex.Message);
//            }
//        }

        function Bio(EmpCode) {
            try {

                var gridView = document.getElementById('<%= this.gvEmpDetails.ClientID %>');
                gridView.disabled = true;
                gridView.style.cursor = 'wait';
                var gridViewControls = gridView.getElementsByTagName("a");

                for (i = 0; i < gridViewControls.length; i++) {
                    gridViewControls[i].disabled = true;
                    gridViewControls[i].style.cursor = 'wait';
                }

                var Return = WshShell.Run(ExePath + "SignBio\\NativeTemplate.exe " + EmpCode + " ISO Enrollment Bio", 1, true);

                gridView.disabled = false;
                gridView.style.cursor = 'default';
                for (i = 0; i < gridViewControls.length; i++) {
                    gridViewControls[i].disabled = false;
                    gridViewControls[i].style.cursor = 'default';
                }

                __doPostBack("<%=gvEmpDetails.UniqueID %>", "");
                /// _doPostBack('<%= UpdatePanel2.ClientID %>', '');


            }
            catch (ex) {
                alert(ex.Message);
            }
        }
    </script>
    <script type="text/javascript">

        function WriteTemplateOnCard(EmpCode, EmpName) {
            debugger;
            try {
                var MasterKey = "";
                var ServerFlag = false;
                $.ajax({
                    url: "SACPersoDataCapture.aspx/GetMasterKey",
                    type: "POST",
                    dataType: "json",
                    data: "",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d == "") {
                            alert("Master Key not found. Please Verify Master card first.");
                            navigateToUrl("SACMasterCard.aspx");
                            ServerFlag = false;
                        }
                        else {
                            MasterKey = msg.d;
                            ServerFlag = true;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });
                if (ServerFlag == false) {
                    return false;
                }

                var ECode = EmpCode;
                var EName = EmpName;
                $.ajax({
                    url: "SACPersoDataCapture.aspx/GetISOTemplate",
                    type: "POST",
                    dataType: "json",
                    data: "{'EmpCode':'" + EmpCode + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {


                        if (msg.d == "False") {
                            alert("Cannot write on card,Employee not enrolled or card is already issued.");
                        }
                        else {
                            var xmlDoc = $.parseXML(msg.d);
                            var xml = $(xmlDoc);
                            var Finger_Template = xml.find("Table1");

                            var EmployeeCode = $(Finger_Template).find("EmployeeCode").text().lpad("0", 10);
                            var AverageQuality = $(Finger_Template).find("FingerQualityAvg").text();
                            var FirstFingerNo = $(Finger_Template).find("Template1FingerNo").text();
                            var FirstFingerQuality = $(Finger_Template).find("Template1Quality").text();
                            var Template1 = $(Finger_Template).find("Template1").text();
                            var SecondFingerNo = $(Finger_Template).find("Template2FingerNo").text();
                            var SecondFingerQuality = $(Finger_Template).find("Template2Quality").text();
                            var Template2 = $(Finger_Template).find("Template2").text();


                            //                            var Template1 = $(Finger_Template).find("ISOTemplate1").text().toUpperCase();
                            //                            var Template2 = $(Finger_Template).find("ISOTemplate2").text().toUpperCase();
                            //                            var ActivationDate = $(Finger_Template).find("ActivationDate").text();
                            //                            ActivationDate = parseInt(ActivationDate, 10).toString(16).toUpperCase();
                            //                            var ExpiryDate = $(Finger_Template).find("ExpiryDate").text();
                            //                            ExpiryDate = parseInt(ExpiryDate, 10).toString(16).toUpperCase();
                            //                            var AadharNo = $(Finger_Template).find("AadharNo").text();
                            //                            var CenterCode = $(Finger_Template).find("CenterCode").text();
                            //                            var LocationCode = $(Finger_Template).find("LocationCode").text();



                            var objReadCard;
                            var DataInitialize;
                            var cardCSNR = "";
                            objReadCard = new ActiveXObject("ContactlessCardRW.Card");
                            DataInitialize = objReadCard.Initialise();
                            if (DataInitialize != "") {
                                alert("Card reader not connected or Error in card Initialization");
                                return false;
                            }
                            var data;
                            data = objReadCard.ConnectToCard();
                            if (data != "") {
                                alert("Card not detected");
                                return false;
                            }
                            //alert(((AverageQuality > 100) ? 100 : AverageQuality).toString(16).lpad("0", 2));
                            var WriteData = "";
                            WriteData = ConvertStringToHex(EmployeeCode).toUpperCase() + "464D5200" + "312E3000" + "0101" + ((AverageQuality > 99) ? 99 : AverageQuality).toString(16).lpad("0", 2) + "02" + ((Template1.length / 2) + (Template2.length / 2) + 8).toString(16).lpad("0", 4) + FirstFingerNo.toString(16).lpad("0", 2) + ((FirstFingerQuality > 99) ? 99 : FirstFingerQuality).toString(16).lpad("0", 2) + (Template1.length / 2).toString(16).lpad("0", 4) + Template1 + SecondFingerNo.toString(16).lpad("0", 2) + ((SecondFingerQuality > 99) ? 99 : SecondFingerQuality).toString(16).lpad("0", 2) + (Template2.length / 2).toString(16).lpad("0", 4) + Template2 + "00" + "00000000000000" + "00000000";
                            //                            var EmployeeCode = ECode;
                            //                            var EmployeeCodePrefix = EmployeeCode.substr(0, 2);
                            //                            var EmpCode = EmployeeCode.substr(2, EmployeeCode.length);
                            //                            var EmpName = EName.rpad(" ", 16);

                            //                            WriteData = ConvertStringToHex(EmployeeCodePrefix) + ConvertStringToHex(EmpCode).toUpperCase() + ConvertStringToHex(EmpName).toUpperCase() + ActivationDate + ExpiryDate + "00" + "01" + "02" + (Template1.length).toString(16).lpad("0", 4).toUpperCase() + (Template2.length).toString(16).lpad("0", 4).toUpperCase() + AadharNo.lpad("0", 12) + "0000" + CenterCode + LocationCode + Template1 + Template2;


                            //alert(WriteData);
                          /*  var Sector = 16;
                            for (var i = 0, j = 64; i < 4 && Sector < 28; i++, j++) {
                                if (i == 3) {
                                    data = objReadCard.Authenticate(j, "FFFFFFFFFFFF", 96);
                                    if (data != "") {
                                        alert("Unable to Authenticate Block " + j);

                                        $find("mpePersonaliseCard").hide();
                                        return false;
                                    }
                                    var key = MasterKey + "FF078069" + MasterKey;
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

                                        $find("mpePersonaliseCard").hide();
                                        return false;
                                    }
                                    strWriteData = objReadCard.Convert(strWriteData);
                                    data = objReadCard.WriteData(strWriteData, parseInt(j, 10).toString(16).toUpperCase());
                                    if (data != "") {
                                        alert("Card Error: Unable to write in the card.Sector " + Sector + " Block " + j);
                                        return false;
                                    }
                                }
                            }*/


                            //                            var Sector = 35;
                            //                            for (var i = 0, j = 176; i < 16 && Sector < 40; i++, j++) {
                            //                                if (i == 15) {
                            //                                    data = objReadCard.Authenticate(j, "FFFFFFFFFFFF", 96);
                            //                                    if (data != "") {
                            //                                        alert("Unable to Authenticate Block " + j);

                            //                                        $find("mpePersonaliseCard").hide();
                            //                                        return false;
                            //                                    }
                            //                                    var key = "FFFFFFFFFFFF" + "FF078069" + "FFFFFFFFFFFF";
                            //                                    key = objReadCard.Convert(key);
                            //                                    data = objReadCard.WriteData(key, parseInt(j, 10).toString(16).toUpperCase());
                            //                                    if (data != "") {
                            //                                        alert("Card Error: Unable to write in the card.Sector " + Sector + " Block " + j);
                            //                                        return false;
                            //                                    }
                            //                                    //alert("Key Sector " + Sector + " Block " + j + " : " + key);
                            //                                    i = 0 - 1;
                            //                                    Sector = Sector + 1;
                            //                                }
                            //                                else {
                            //                                    var strWriteData = WriteData.substring(0, 32).rpad("0", 32);
                            //                                    WriteData = WriteData.substring(32, WriteData.length);
                            //                                    //alert("Data Sector " + Sector + " Block " + j + " : " + strWriteData);
                            //                                    data = objReadCard.Authenticate(j, "FFFFFFFFFFFF", 96);
                            //                                    if (data != "") {
                            //                                        alert("Unable to Authenticate Block " + j);

                            //                                        $find("mpePersonaliseCard").hide();
                            //                                        return false;
                            //                                    }
                            //                                    strWriteData = objReadCard.Convert(strWriteData);
                            //                                    data = objReadCard.WriteData(strWriteData, parseInt(j, 10).toString(16).toUpperCase());
                            //                                    if (data != "") {
                            //                                        alert("Card Error: Unable to write in the card.Sector " + Sector + " Block " + j);
                            //                                        return false;
                            //                                    }
                            //                                }
                            //                            }
                            alert("Template written on Card");
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
    <script type="text/javascript">
        function Personalisation(EmpCode) {

            try {
                var MasterKey = "";
                var DosKey = "";
                var ServerFlag = false;
                $.ajax({
                    url: "SACPersoDataCapture.aspx/GetMasterKey",
                    type: "POST",
                    dataType: "json",
                    data: "",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d == "") {
                            alert("Master Key not found. Please Verify Master card first.");
                            navigateToUrl("SACMasterCard.aspx");
                            ServerFlag = false;
                        }
                        else {
                            MasterKey = msg.d;
                            ServerFlag = true;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });
                if (ServerFlag == false) {
                    return false;
                }

                $.ajax({
                    url: "SACPersoDataCapture.aspx/CheckLostCard",
                    type: "POST",
                    dataType: "json",
                    data: "{'EmployeeCode':'" + EmpCode + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d == "True") {
                            // Go for Personalisation
                            $find("mpeLostCard").hide();
                            $find("mpePersonaliseCard").show();
                            GetPersonalisationDetails(EmpCode);
                            ServerFlag = true;
                        }
                        else {
                            // Go for Lost Card
                            document.getElementById("<%= lblEmployeeCodeLostCard.ClientID %>").value = EmpCode;
                            $find("mpePersonaliseCard").hide();
                            $find("mpeLostCard").show();
                            ServerFlag = false;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });
                if (ServerFlag == false) {
                    return false;
                }
                return false;
            }
            catch (ex) {
                alert(ex.Message);
            }
        }
    </script>
    <script type="text/javascript">
        function SaveLostCard(EmpCode, Reason, AdditionalInfo, LostDate) {
            try {
                var EmpCode = document.getElementById("<%= lblEmployeeCodeLostCard.ClientID %>").value;
                var Reason = document.getElementById("<%= ddlReason.ClientID %>").value;
                var AdditionalInfo = document.getElementById("<%= txtAdditionalInfo.ClientID %>").value;
                var LostDate = document.getElementById("<%= txtLostDate.ClientID %>").value;
                $.ajax({
                    url: "SACPersoDataCapture.aspx/SaveLostCard",
                    type: "POST",
                    dataType: "json",
                    data: "{'EmpCode':'" + EmpCode + "','Reason':'" + Reason + "','AdditionalInfo':'" + AdditionalInfo + "','LostDate':'" + LostDate + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d != "False") {
                            // Go for Personalisation
                            $find("mpeLostCard").hide();
                            $find("mpePersonaliseCard").show();
                            document.getElementById("<%= LogId.ClientID %>").value = msg.d;
                            GetPersonalisationDetails(EmpCode);
                        }
                        else {
                            alert("Database Error : SaveLostCard");
                            //$find("mpePersonaliseCard").hide();
                            //$find("mpeLostCard").show();
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });
                return false;
            }
            catch (ex) {

                alert(ex.Message);
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function GetPersonalisationDetails(EmpCode) {
            debugger;
            try {
                var ServerFlag = false;
                $.ajax({
                    url: "SACPersoDataCapture.aspx/GetEmployeeDetails",
                    type: "POST",
                    dataType: "json",
                    data: "{'EmployeeCode':'" + EmpCode + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d == "") {
                            alert("Internal Server Error. Please try again.");
                            ServerFlag = false;
                        }
                        else {
                            //alert(msg.d);
                            var xmlDoc = $.parseXML(msg.d);
                            var xml = $(xmlDoc);
                            var EmpDetails = xml.find("Table1");
                            document.getElementById("<%= txtEmployeeCode.ClientID %>").value = $(EmpDetails).find("EmpCode").text();
                            document.getElementById("<%= txtEmployeeName.ClientID %>").value = $(EmpDetails).find("EmpName").text();
                            document.getElementById("<%= txtDOB.ClientID %>").value = $(EmpDetails).find("DOB").text();
                            document.getElementById("<%= ddlGender.ClientID %>").value = $(EmpDetails).find("Gender").text();
                            document.getElementById("<%= txtCardExpiryDate.ClientID %>").value = $(EmpDetails).find("ExpiryDate").text();
                            document.getElementById("<%= txtCardExpiryTime.ClientID %>").value = "23:59";
                            ServerFlag = true;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });
                if (ServerFlag == false) {
                    return false;
                }
                return false;
            }
            catch (ex) {
                alert(ex.Message);
                return false;
            }
        }
    </script>
    <script language="JavaScript" type="text/javascript">
        // accumulate values to put into text area
        var accumulated_output_info;

        // add a labeled value to the text area
        function accumulate_output(str) {
            accumulated_output_info = accumulated_output_info + str + "\n";
        }

        // add a bit string to the output, inserting spaces as designated
        function accumulate_bitstring(label, ary, spacing) {
            var i;

            accumulated_output_info += label;

            // add bits
            for (i = 1; i < ary.length; i++) {
                if ((i % spacing) == 1)
                    accumulated_output_info += " "; // time to add a space
                accumulated_output_info += ary[i]; // and the bit
            }

            // insert trailing end-of-line
            accumulated_output_info += "\n";
        }

        // special value stored in x[0] to indicate a problem
        var ERROR_VAL = -9876;

        // initial permutation (split into left/right halves )
        // since DES numbers bits starting at 1, we will ignore x[0]
        var IP_perm = new Array(-1,
	58, 50, 42, 34, 26, 18, 10, 2,
	60, 52, 44, 36, 28, 20, 12, 4,
	62, 54, 46, 38, 30, 22, 14, 6,
	64, 56, 48, 40, 32, 24, 16, 8,
	57, 49, 41, 33, 25, 17, 9, 1,
	59, 51, 43, 35, 27, 19, 11, 3,
	61, 53, 45, 37, 29, 21, 13, 5,
	63, 55, 47, 39, 31, 23, 15, 7);

        // final permutation (inverse initial permutation)
        var FP_perm = new Array(-1,
	40, 8, 48, 16, 56, 24, 64, 32,
	39, 7, 47, 15, 55, 23, 63, 31,
	38, 6, 46, 14, 54, 22, 62, 30,
	37, 5, 45, 13, 53, 21, 61, 29,
	36, 4, 44, 12, 52, 20, 60, 28,
	35, 3, 43, 11, 51, 19, 59, 27,
	34, 2, 42, 10, 50, 18, 58, 26,
	33, 1, 41, 9, 49, 17, 57, 25);

        // per-round expansion
        var E_perm = new Array(-1,
	32, 1, 2, 3, 4, 5,
	4, 5, 6, 7, 8, 9,
	8, 9, 10, 11, 12, 13,
	12, 13, 14, 15, 16, 17,
	16, 17, 18, 19, 20, 21,
	20, 21, 22, 23, 24, 25,
	24, 25, 26, 27, 28, 29,
	28, 29, 30, 31, 32, 1);

        // per-round permutation
        var P_perm = new Array(-1,
	16, 7, 20, 21, 29, 12, 28, 17,
	1, 15, 23, 26, 5, 18, 31, 10,
	2, 8, 24, 14, 32, 27, 3, 9,
	19, 13, 30, 6, 22, 11, 4, 25);

        // note we do use element 0 in the S-Boxes
        var S1 = new Array(
	14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7,
	0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8,
	4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0,
	15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13);
        var S2 = new Array(
	15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10,
	3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5,
	0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15,
	13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9);
        var S3 = new Array(
	10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8,
	13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1,
	13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7,
	1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12);
        var S4 = new Array(
	7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15,
	13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9,
	10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4,
	3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14);
        var S5 = new Array(
	2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9,
	14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6,
	4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14,
	11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3);
        var S6 = new Array(
	12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11,
	10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8,
	9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6,
	4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13);
        var S7 = new Array(
	4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1,
	13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6,
	1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2,
	6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12);
        var S8 = new Array(
	13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7,
	1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2,
	7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8,
	2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11);

        //, first, key, permutation
        var PC_1_perm = new Array(-1,
        // C subkey bits
	57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18,
	10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36,
        // D subkey bits
	63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22,
	14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4);

        //, per-round, key, selection, permutation
        var PC_2_perm = new Array(-1,
	14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10,
	23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2,
	41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48,
	44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32);

        // save output in case we want to reformat it later
        var DES_output = new Array(65);

        // remove spaces from input
        function remove_spaces(instr) {
            var i;
            var outstr = "";

            for (i = 0; i < instr.length; i++)
                if (instr.charAt(i) != " ")
                // not a space, include it
                    outstr += instr.charAt(i);

            return outstr;
        }

        // split an integer into bits
        // ary   = array to store bits in
        // start = starting subscript
        // bitc  = number of bits to convert
        // val   = number to convert
        function split_int(ary, start, bitc, val) {
            var i = start;
            var j;
            for (j = bitc - 1; j >= 0; j--) {
                // isolate low-order bit
                ary[i + j] = val & 1;
                // remove that bit
                val >>= 1;
            }
        }

        // get the message to encrypt/decrypt
        function get_value(bitarray, str, isASCII) {
            var i;
            var val; // one hex digit

            // insert note we probably are ok
            bitarray[0] = -1;

            if (isASCII) {
                // check length of data
                if (str.length != 8) {
                    window.alert("Message and key must be 64 bits (8 ASCII characters)");
                    bitarray[0] = ERROR_VAL;
                    return
                }

                // have ASCII data
                for (i = 0; i < 8; i++) {
                    split_int(bitarray, i * 8 + 1, 8, str.charCodeAt(i));
                }
            }
            else {
                // have hex data - remove any spaces they used, then convert
                str = remove_spaces(str);

                // check length of data
                if (str.length != 16) {
                    window.alert("Message and key must be 64 bits (16 hex digits)");
                    bitarray[0] = ERROR_VAL;
                    return;
                }

                for (i = 0; i < 16; i++) {
                    // get the next hex digit
                    val = str.charCodeAt(i);

                    // do some error checking
                    if (val >= 48 && val <= 57)
                    // have a valid digit 0-9
                        val -= 48;
                    else if (val >= 65 && val <= 70)
                    // have a valid digit A-F
                        val -= 55;
                    else if (val >= 97 && val <= 102)
                    // have a valid digit A-F
                        val -= 87;
                    else {
                        // not 0-9 or A-F, complain
                        window.alert(str.charAt(i) + " is not a valid hex digit");
                        bitarray[0] = ERROR_VAL;
                        return;
                    }

                    // add this digit to the array
                    split_int(bitarray, i * 4 + 1, 4, val - 48);
                }
            }
        }

        // format the output in the desired form
        // if -1, use existing value
        // -- uses the global array DES_output
        function format_DES_output() {
            var i;
            var bits;
            var str = "";

            // what type of data do we have to work with?
            if (false) {
                // convert each set of bits back to ASCII
                for (i = 1; i <= 64; i += 8) {
                    str += String.fromCharCode(
                        DES_output[i] * 128 + DES_output[i + 1] * 64 +
                        DES_output[i + 2] * 32 + DES_output[i + 3] * 16 +
                        DES_output[i + 4] * 8 + DES_output[i + 5] * 4 +
                        DES_output[i + 6] * 2 + DES_output[i + 7]);
                }
            }
            else {
                // output hexdecimal data
                for (i = 1; i <= 64; i += 4) {
                    bits = DES_output[i] * 8 + DES_output[i + 1] * 4 +
                   DES_output[i + 2] * 2 + DES_output[i + 3];

                    // 0-9 or A-F?
                    if (bits <= 9)
                        str += String.fromCharCode(bits + 48);
                    else
                        str += String.fromCharCode(bits + 87);
                }
            }
            // copy to textbox
            return str;
        }

        // copy bits in a permutation
        //   dest = where to copy the bits to
        //   src  = Where to copy the bits from
        //   perm = The order to copy/permute the bits
        // note: since DES ingores x[0], we do also
        function permute(dest, src, perm) {
            var i;
            var fromloc;

            for (i = 1; i < perm.length; i++) {
                fromloc = perm[i];
                dest[i] = src[fromloc];
            }
        }

        // do an array XOR
        // assume all array entries are 0 or 1
        function xor(a1, a2) {
            var i;

            for (i = 1; i < a1.length; i++)
                a1[i] = a1[i] ^ a2[i];
        }

        // process one S-Box, return integer from S-Box
        function do_S(SBox, index, inbits) {
            // collect the 6 bits into a single integer
            var S_index = inbits[index] * 32 + inbits[index + 5] * 16 +
                 inbits[index + 1] * 8 + inbits[index + 2] * 4 +
                 inbits[index + 3] * 2 + inbits[index + 4];

            // do lookup
            return SBox[S_index];
        }

        // do one round of DES encryption
        function des_round(L, R, KeyR) {
            var E_result = new Array(49);
            var S_out = new Array(33);

            // copy the existing L bits, then set new L = old R
            var temp_L = new Array(33);
            for (i = 0; i < 33; i++) {
                // copy exising L bits
                temp_L[i] = L[i];

                // set L = R
                L[i] = R[i];
            }

            // expand R using E permutation
            permute(E_result, R, E_perm);
            accumulate_bitstring("E   : ", E_result, 6);
            accumulate_bitstring("KS  : ", KeyR, 6);

            // exclusive-or with current key
            xor(E_result, KeyR);
            accumulate_bitstring("E xor KS: ", E_result, 6);

            // put through the S-Boxes
            split_int(S_out, 1, 4, do_S(S1, 1, E_result));
            split_int(S_out, 5, 4, do_S(S2, 7, E_result));
            split_int(S_out, 9, 4, do_S(S3, 13, E_result));
            split_int(S_out, 13, 4, do_S(S4, 19, E_result));
            split_int(S_out, 17, 4, do_S(S5, 25, E_result));
            split_int(S_out, 21, 4, do_S(S6, 31, E_result));
            split_int(S_out, 25, 4, do_S(S7, 37, E_result));
            split_int(S_out, 29, 4, do_S(S8, 43, E_result));
            accumulate_bitstring("Sbox: ", S_out, 4);

            // do the P permutation
            permute(R, S_out, P_perm);
            accumulate_bitstring("P   :", R, 8);

            // xor this with old L to get the new R
            xor(R, temp_L);
            accumulate_bitstring("L[i]:", L, 8);
            accumulate_bitstring("R[i]:", R, 8);
        }

        // shift the CD values left 1 bit
        function shift_CD_1(CD) {
            var i;

            // note we use [0] to hold the bit shifted around the end
            for (i = 0; i <= 55; i++)
                CD[i] = CD[i + 1];

            // shift D bit around end
            CD[56] = CD[28];
            // shift C bit around end
            CD[28] = CD[0];
        }

        // shift the CD values left 2 bits
        function shift_CD_2(CD) {
            var i;
            var C1 = CD[1];

            // note we use [0] to hold the bit shifted around the end
            for (i = 0; i <= 54; i++)
                CD[i] = CD[i + 2];

            // shift D bits around end
            CD[55] = CD[27];
            CD[56] = CD[28];
            // shift C bits around end
            CD[27] = C1;
            CD[28] = CD[0];
        }


        // do the actual DES encryption/decryption
        function des_encrypt(inData, Key, do_encrypt) {
            var tempData = new Array(65); // output bits
            var CD = new Array(57); 	// halves of current key
            var KS = new Array(16); 	// per-round key schedules
            var L = new Array(33); 	// left half of current data
            var R = new Array(33); 	// right half of current data
            var result = new Array(65); // DES output
            var i;

            // do the initial key permutation
            permute(CD, Key, PC_1_perm);
            accumulate_bitstring("CD[0]: ", CD, 7);

            // create the subkeys
            for (i = 1; i <= 16; i++) {
                // create a new array for each round
                KS[i] = new Array(49);

                // how much should we shift C and D?
                if (i == 1 || i == 2 || i == 9 || i == 16)
                    shift_CD_1(CD);
                else
                    shift_CD_2(CD);
                accumulate_bitstring("CD[" + i + "]: ", CD, 7);

                // create the actual subkey
                permute(KS[i], CD, PC_2_perm);
                accumulate_bitstring("KS[" + i + "]: ", KS[i], 6);
            }

            // handle the initial permutation
            permute(tempData, inData, IP_perm);

            // split data into L/R parts
            for (i = 1; i <= 32; i++) {
                L[i] = tempData[i];
                R[i] = tempData[i + 32];
            }
            accumulate_bitstring("L[0]: ", L, 8);
            accumulate_bitstring("R[0]: ", R, 8);

            // encrypting or decrypting?
            if (do_encrypt) {
                // encrypting
                for (i = 1; i <= 16; i++) {
                    accumulate_output("Round " + i);
                    des_round(L, R, KS[i]);
                }
            }
            else {
                // decrypting
                for (i = 16; i >= 1; i--) {
                    accumulate_output("Round " + (17 - i));
                    des_round(L, R, KS[i]);
                }
            }

            // create the 64-bit preoutput block = R16/L16
            for (i = 1; i <= 32; i++) {
                // copy R bits into left half of block, L bits into right half
                tempData[i] = R[i];
                tempData[i + 32] = L[i];
            }
            accumulate_bitstring("LR[16] ", tempData, 8);

            // do final permutation and return result
            permute(result, tempData, FP_perm);
            return result;
        }
        // do encrytion/decryption
        // do_encrypt is TRUE for encrypt, FALSE for decrypt
        function do_des(do_encrypt) {


            var inData = new Array(65); // input message bits
            var Key = new Array(65);

            accumulated_output_info = "";

            // get the message from the user
            // also check if it is ASCII or hex
            get_value(inData, Data, false);

            // problems??
            if (inData[0] == ERROR_VAL) {
                //document.stuff.details.value = accumulated_output_info;
                return;
            }
            accumulate_bitstring("Input bits:", inData, 8);

            // get the key from the user
            get_value(Key, MyKey, false);

            // problems??
            if (Key[0] == ERROR_VAL) {
                //document.stuff.details.value = accumulated_output_info;
                return;
            }
            accumulate_bitstring("Key bits:", Key, 8);

            // do the encryption/decryption, put output in DES_output for display
            DES_output = des_encrypt(inData, Key, do_encrypt)

            accumulate_bitstring("Output ", DES_output, 8);
            // process output
            return format_DES_output();
            //document.stuff.details.value = accumulated_output_info;
        }

        // do Triple-DES encrytion/decryption
        // do_encrypt is TRUE for encrypt, FALSE for decrypt
        function do_tdes(do_encrypt) {
            var inData = new Array(65); // input message bits
            var tempdata = new Array(65); // interm result bits
            var KeyA = new Array(65);
            var KeyB = new Array(65);

            accumulated_output_info = "";

            // get the message from the user
            // also check if it is ASCII or hex
            get_value(inData, Data,
		document.stuff.intype[0].checked);

            // problems??
            if (inData[0] == ERROR_VAL) {
                //document.stuff.details.value = accumulated_output_info;
                return;
            }
            accumulate_bitstring("Input bits:", inData, 8);

            // get the key part A from the user
            get_value(KeyA, MyKey, false);
            // problems??
            if (KeyA[0] == ERROR_VAL) {
                //document.stuff.details.value = accumulated_output_info;
                return;
            }
            accumulate_bitstring("Key A bits:", KeyA, 8);


            // get the key part B from the user
            get_value(KeyB, document.stuff.keyb.value, false);
            // problems??
            if (KeyB[0] == ERROR_VAL) {
                //document.stuff.details.value = accumulated_output_info;
                return;
            }
            accumulate_bitstring("Key B bits:", KeyB, 8);

            if (do_encrypt) {
                // TDES encrypt = DES encrypt/decrypt/encrypt
                accumulate_output("---- Starting first encryption ----");
                tempdata = des_encrypt(inData, KeyA, true);
                accumulate_output("---- Starting second decryption ----");
                tempdata = des_encrypt(tempdata, KeyB, false);
                accumulate_output("---- Starting third encryption ----");
                DES_output = des_encrypt(tempdata, KeyA, true);
            }
            else {
                // TDES decrypt = DES decrypt/encrypt/decrypt
                accumulate_output("---- Starting first decryption ----");
                tempdata = des_encrypt(inData, KeyA, false);
                accumulate_output("---- Starting second encryption ----");
                tempdata = des_encrypt(tempdata, KeyB, true);
                accumulate_output("---- Starting third decryption ----");
                DES_output = des_encrypt(tempdata, KeyA, false);
            }

            accumulate_bitstring("Output ", DES_output, 8);

            // process output
            format_DES_output();
            //document.stuff.details.value = accumulated_output_info;
        }
    </script>
    <script type="text/javascript">
        var dscrc8_table = [
        0, 94, 188, 226, 97, 63, 221, 131, 194, 156, 126, 32, 163, 253, 31, 65,
      157, 195, 33, 127, 252, 162, 64, 30, 95, 1, 227, 189, 62, 96, 130, 220,
       35, 125, 159, 193, 66, 28, 254, 160, 225, 191, 93, 3, 128, 222, 60, 98,
      190, 224, 2, 92, 223, 129, 99, 61, 124, 34, 192, 158, 29, 67, 161, 255,
       70, 24, 250, 164, 39, 121, 155, 197, 132, 218, 56, 102, 229, 187, 89, 7,
      219, 133, 103, 57, 186, 228, 6, 88, 25, 71, 165, 251, 120, 38, 196, 154,
      101, 59, 217, 135, 4, 90, 184, 230, 167, 249, 27, 69, 198, 152, 122, 36,
      248, 166, 68, 26, 153, 199, 37, 123, 58, 100, 134, 216, 91, 5, 231, 185,
      140, 210, 48, 110, 237, 179, 81, 15, 78, 16, 242, 172, 47, 113, 147, 205,
       17, 79, 173, 243, 112, 46, 204, 146, 211, 141, 111, 49, 178, 236, 14, 80,
      175, 241, 19, 77, 206, 144, 114, 44, 109, 51, 209, 143, 12, 82, 176, 238,
       50, 108, 142, 208, 83, 13, 239, 177, 240, 174, 76, 18, 145, 207, 45, 115,
      202, 148, 118, 40, 171, 245, 23, 73, 8, 86, 180, 234, 105, 55, 213, 139,
       87, 9, 235, 181, 54, 104, 138, 212, 149, 203, 41, 119, 244, 170, 72, 22,
      233, 183, 85, 11, 136, 214, 52, 106, 43, 117, 151, 201, 74, 20, 246, 168,
      116, 42, 200, 150, 21, 75, 169, 247, 182, 232, 10, 84, 215, 137, 107, 53];






        function CardPersonalisation2() {
            debugger;
            try {
                var MasterKey = "";
                var DosKey = "";
                var WriteData = "";
                var ServerFlag = false;
                $.ajax({
                    url: "SACPersoDataCapture.aspx/GetMasterKey",
                    type: "POST",
                    dataType: "json",
                    data: "",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d == "") {
                            alert("Master Key not found. Please Verify Master card first.");
                            navigateToUrl("SACMasterCard.aspx");
                            ServerFlag = false;
                        }
                        else {
                            MasterKey = msg.d;
                            ServerFlag = true;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });
                if (ServerFlag == false) {
                    navigateToUrl("SACMasterCard.aspx");
                    return false;
                }


                //Reading Company Code

                $.ajax({
                    url: "SACPersoDataCapture.aspx/GetCompanyCode",
                    type: "POST",
                    dataType: "json",
                    data: "",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d == "") {
                            alert("Company Code was not found. Please Verify Master card first.");
                            navigateToUrl("SACMasterCard.aspx");
                            ServerFlag = false;
                        }
                        else {
                            CompCode = msg.d;
                            ServerFlag = true;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });

                if (ServerFlag == false) {
                    navigateToUrl("SACMasterCard.aspx");
                    return false;
                }




                var objReadCard;
                var DataInitialize;
                var cardCSNR = "";
                objReadCard = new ActiveXObject("ContactlessCardRW.Card");
                DataInitialize = objReadCard.Initialise();
                if (DataInitialize != "") {
                    alert("Card reader not connected or Error in card Initialization");

                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                var data;
                data = objReadCard.ConnectToCard();
                if (data != "") {
                    alert("Card not detected");

                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                cardCSNR = objReadCard.CSNR.replace(/ /g, "").substring(0, 8);
                cardCSNR = ReverseCSNR(cardCSNR);
                var CSNRExist = false;
                $.ajax({
                    url: "SACPersoDataCapture.aspx/CheckCSNR",
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
                    return false;
                }

                var CMSCalulatedKey = XOR("0000" + cardCSNR, "434D53434D53");
                //alert(CMSCalulatedKey);
                // ***********Sector 0 Start ******************************************************** //
                data = objReadCard.Authenticate(1, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 1");
                    $find("mpePersonaliseCard").hide();

                    return false;
                }
                WriteData = "";
                WriteData = "FF0F0400000000000248000000000000";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "1");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 0 Block 1");
                    $find("mpePersonaliseCard").hide();

                    return false;
                }
                data = objReadCard.Authenticate(2, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 2");
                    $find("mpePersonaliseCard").hide();

                    return false;
                }
                WriteData = "";
                WriteData = "00000000000000000000010002000300";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "2");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 0 Block 2");
                    return false;
                }

                data = objReadCard.Authenticate(3, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 3");

                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                WriteData = "";
                WriteData = CMSCalulatedKey + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "3");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 0 Block 3");

                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                // ***********Sector 0 End ******************************************************** //

                // ***********Sector 1 Start ******************************************************** //

                var EmpName = document.getElementById("<%= txtEmployeeName.ClientID %>").value.rpad(" ", 32);
                var WriteData = "";
                WriteData = EmpName.substr(0, 16);
                data = objReadCard.Authenticate(4, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 4");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "04");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 4");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                WriteData = EmpName.substr(16, 32);
                data = objReadCard.Authenticate(5, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 5");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "05");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 5");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                var DOB = document.getElementById("<%= txtDOB.ClientID %>").value;
                var DOE = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var ExpiryTime = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                DOB = DOB.replace(/\//g, "");
                DOE = DOE.replace(/\//g, "");
                ExpiryTime = ExpiryTime.replace(/:/g, "");
                var Gender = document.getElementById("<%= ddlGender.ClientID %>").value;
                if (Gender == "M") {
                    Gender = "01";
                }
                else {
                    Gender = "02";
                }
                WriteData = "";
                //WriteData = DOB + Gender + "FFFFFFFFFFFFFFFFFFFFFF";
                WriteData = DOB + Gender + DOE + ExpiryTime + "0000000000";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(6, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 6");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "6");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 6");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                WriteData = "";
                data = objReadCard.Authenticate(7, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 7");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                WriteData = "";
                WriteData = CMSCalulatedKey + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "7");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 7");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                // *********** Sector 1 End******************************************************** //
                // ***********Sector 2 Start ******************************************************** //
                var EmployeeCode2 = document.getElementById("<%= txtEmployeeCode.ClientID %>").value.lpad("0", 10);
                WriteData = "";
                WriteData = ConvertStringToHex(EmployeeCode2) + "000000000000";
                WriteData = PadBytes(WriteData, 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(8, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 8");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "08");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 8");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                var DOE2 = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var ExpiryTime2 = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                DOE2 = DOE2.replace(/\//g, "");
                ExpiryTime2 = ExpiryTime2.replace(/:/g, "");
                WriteData = "";
                WriteData = DOE2 + ExpiryTime2 + "00" + "00" + "00" + "00000000000000";
                WriteData = PadBytes(WriteData, 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(9, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 9");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "09");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 9");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                WriteData = "";
                data = objReadCard.Authenticate(11, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 11");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                debugger;
                var Key2 = CalculateCMSKeyWithRandomNo("738740688017", "0000" + cardCSNR);
                WriteData = "";
                WriteData = Key2 + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "0B");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 11");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                // *********** Sector 2 End******************************************************** //

                // ***********Sector 3 Start ******************************************************** //
                var EmployeeCode3 = document.getElementById("<%= txtEmployeeCode.ClientID %>").value.lpad("0", 10);
                WriteData = "";
                WriteData = ConvertStringToHex(EmployeeCode3) + "00" + "01010101" + "00";
                WriteData = PadBytes(WriteData, 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(12, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 12");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "0C");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 3 Block 12");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                var DOE3 = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var ExpiryTime3 = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                DOE3 = DOE3.replace(/\//g, "");
                ExpiryTime3 = ExpiryTime3.replace(/:/g, "");
                WriteData = "";
                WriteData = DOE3 + ExpiryTime3 + "00" + "00" + "00" + "000000" + "00" + "00" + "00" + "00";
                WriteData = PadBytes(WriteData, 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(13, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 13");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "0D");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 3 Block 13");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                WriteData = "";
                data = objReadCard.Authenticate(15, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 15");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
            
                var Key3 = CalculateCMSKeyWithRandomNo("738740688017", "0000" + cardCSNR);
                WriteData = "";
                WriteData = Key3 + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "0F");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 3 Block 15");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                // *********** Sector 3 End******************************************************** //


                //*******below code to lock sector 13 with master key**********************************************
                data = objReadCard.Authenticate(55, MasterKey, 96);
                if (data != "") {
                    alert("Authentication Error Sector 13 Block 55");
                    $find("mpePersonaliseCard").hide();
                    return;
                }

                data = objReadCard.WriteMasterKey(MasterKey, "37");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 13 Block 55");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                //***************end***********************************************************************



                //*********************'below code to lock sector 14 with master key*********************************************

                data = objReadCard.Authenticate(59, MasterKey, 96);
                if (data != "") {
                    alert("Authentication Error Sector 14 Block 59");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                data = objReadCard.WriteMasterKey(MasterKey, "3B");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 14 Block 59");
                    $find("mpePersonaliseCard").hide();
                    return;
                }

                //***************************************end********************************************************************


                // ***********************************Sector 13 Start ************************************************//

              /*  WriteData = MasterKey + "FF078069" + "FFFFFFFFFFFF";
                WriteData = PadBytes(WriteData, 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(55, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 55");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "37");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 13 Block 55");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                */
                // ***********************************Sector 13 End ************************************************//
                
                // ***********************************Sector 14 Start ************************************************//
               /* var RandomNo = "738740688017";
                WriteData = "";
                WriteData = "00000000" + RandomNo + RandomNo;
                WriteData = PadBytes(WriteData, 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(56, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 56");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "38");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 14 Block 56");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                WriteData = MasterKey + "FF078069" + "FFFFFFFFFFFF";
                WriteData = PadBytes(WriteData, 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(59, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 59");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "3B");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 14 Block 59");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                */
                // ***********************************Sector 14 End ************************************************//

                // ***********************************Sector 15 Start ************************************************//
                var CompanyCode15 = CompCode;
                var SiteID15 = "01";
                var DOE15 = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var ExpiryTime15 = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                DOE15 = DOE15.replace(/\//g, "");
                ExpiryTime15 = ExpiryTime15.replace(/:/g, "");
                var CheckSum15 = "78";
                WriteData = "";
                WriteData = PadBytes(CompanyCode15 + SiteID15 + "00" + "0000" + DOE15 + ExpiryTime15 + CheckSum15 + "0000" + "00", 32);
                //WriteData = PadBytes(CompanyCode15 + SiteID15 + "00" + "0002" + DOE15 + ExpiryTime15 + CheckSum15 + "0248" + "00", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(60, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 60");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "3C");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 15 Block 60");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                WriteData = "";
                data = objReadCard.Authenticate(63, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 63");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                WriteData = "";
                WriteData = CMSCalulatedKey + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "3F");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 15 Block 63");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                // ***********************************Sector 15 End ************************************************//

                var empcode = document.getElementById("<%= txtEmployeeCode.ClientID %>").value
                // var Password = "";
                var Password = RandomNo1();

                var ExpiryDate = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var LogId = document.getElementById("<%= LogId.ClientID %>").value;
                // Database Updation Start
                var UpdateDatabase = false;
                $.ajax({
                    url: "SACPersoDataCapture.aspx/CompletePersonalisation",
                    type: "POST",
                    dataType: "json",
                    //data: "{'EmpCode':'" + empcode + "','pin':'" + pin + "','CSNR':'" + cardCSNR + "','Password':'" + Password + "'}",
                    data: "{'EmpCode':'" + empcode + "','pin':'" + pin + "','CSNR':'" + cardCSNR + "','Password':'" + Password + "','ExpiryDate':'" + ExpiryDate + "','LogId':'" + LogId + "'}",
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
                // Database Updation End
                // Send Mail Start
              //  var SendMail = false;
              //  $.ajax({
                //    url: "SACPersoDataCapture.aspx/SendMail",
                //    type: "POST",
                 //   dataType: "json",
                 //   data: "{'EmpCode':'" + empcode + "','pin':'" + pin + "','CSNR':'" + cardCSNR + "','Password':'" + Password + "'}",
                  //  async: false,
                  //  contentType: "application/json; charset=utf-8",
                   // success: function (msg) {
                        //  alert(msg.d);
                     //   if (msg.d == "True") {
                        //    SendMail = true;
                         //   alert("Card personalised Successfully.");

                      //  }
                      //  else {
                        //    alert("Card personalised successfull but mail not send");
                        //    SendMail = false;
                       // }
                   // },
                   // error: function () { alert(arguments[2]); }
             //   });
               // if (SendMail == false) {
                    // return false;
                //}
                // Send Mail End
                alert("Card personalised Successfully.");
                $find("mpePersonaliseCard").hide();
                return false;
            }
            catch (ex) {
                alert(ex.Message);
                return false;
            }
        }
























        var DosKeyPerso = "";
        var Data = "";
        var MyKey = "";
        var pin = "";




















        function CardPersonalisation44() {
            debugger;
          
            var MasterKey = "";
            var DosKey = "";
            var WriteData = "";
            var ServerFlag = false;
            var CompCode = "";
            var cardCSNR = "";

            //Reading Master Key

            $.ajax({
                url: "SACPersoDataCapture.aspx/GetMasterKey",
                type: "POST",
                dataType: "json",
                data: "",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    if (msg.d == "") {
                        alert("Master Key not found. Please Verify Master card first.");
                        navigateToUrl("SACMasterCard.aspx");
                        ServerFlag = false;
                    }
                    else {
                        MasterKey = msg.d;
                        ServerFlag = true;
                    }
                },
                error: function () { alert(arguments[2]); }
            });

            //Reading Company Code
            
                $.ajax({
            url: "SACPersoDataCapture.aspx/GetCompanyCode",
            type: "POST",
            dataType: "json",
            data: "",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (msg) {
            if (msg.d == "") {
            alert("Company Code was not found. Please Verify Master card first.");
            navigateToUrl("SACMasterCard.aspx");
            ServerFlag = false;
            }
            else {
            CompCode = msg.d;
            ServerFlag = true;
            }
            },
            error: function () { alert(arguments[2]); }
            });

            if (ServerFlag == false) {
            navigateToUrl("SACMasterCard.aspx");
            return false;
            }

            var objReadCard;
            var DataInitialize;
            var cardCSNR = "";
            var ReadData;

            objReadCard = new ActiveXObject("ContactlessCardRW.Card");
            DataInitialize = objReadCard.Initialise();

            if (DataInitialize != "") {
                alert("Card reader not connected or Error in card Initialization");
                $find("mpePersonaliseCard").hide();
                return false;
            }

            var data;
            data = objReadCard.ConnectToCard();
            if (data != "") {
                alert("Error in connecting to card");
                $find("mpePersonaliseCard").hide();
                return false;
            }

            data = objReadCard.LoadKey(objReadCard.Cardrawkeybuf);            
            if (data != "") {
                $find("mpePersonaliseCard").hide();
                return false;
            }


            cardCSNR = objReadCard.CSNR.replace(/ /g, "").substring(0, 8);
            cardCSNR = ReverseCSNR(cardCSNR);
            var CSNRExist = false;
            $.ajax({
                url: "SACPersoDataCapture.aspx/CheckCSNR",
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
                return false;
            }

            var CMSCalulatedKey = XOR("0000" + cardCSNR, "434D53434D53");

            //*******************Checking for personalized card************************************************************

            data = objReadCard.Authenticate(8, objReadCard.Cardrawkeybuf, 96);
            if (data != "") {
                alert("Authentication Error Sector 2 Block 8");
                $find("mpePersonaliseCard").hide();
                return false;
            }

            ReadData = objReadCard.ReadTAEMP(MasterKey).substr(0, 8);


            if (ReadData != "" && ReadData != "00000000") {
                alert("Card Is Already personalized or Card Error");
                $find("mpePersonaliseCard").hide();
                return false;
            }

          

            //**********End of checking for personalization


            //*****************Read App Code from sec 0********************************************************
            data = objReadCard.Authenticate(3, objReadCard.Cardrawkeybuf, 96);
            if (data != "") {
                alert("Authentication Error Sector 0");
                $find("mpePersonaliseCard").hide();
                return;
            }
            else {
                var OldDataBlock1 = objReadCard.ReadBlock(1);
                var OldDataBlock2 = objReadCard.ReadBlock(2);

                var Stemp = "";

                for (var i = 0; i < OldDataBlock1.length; i++) {
                    if (OldDataBlock1.substr(i, 1) != " ") {
                        Stemp += OldDataBlock1.substr(i, 1);
                    }
                }

                OldDataBlock1 = Stemp;

            }
            //****************end of reading app code***********************************************************


            //***********Locking Sector 0 with cms key ********************************************************
            WriteData = "";
            data = objReadCard.WriteRawKey("3");
            if (data != "") {
                alert("Card Error: Unable to write in the card.Sector 0 Block 3");
                $find("mpePersonaliseCard").hide();
                return false;
            }


            //*********************end***********************************************************************


            // ***********Sector 1 Start ******************************************************** //

         
            var EmpName = document.getElementById("<%= txtEmployeeName.ClientID %>").value.rpad(" ", 32);
            var WriteData = "";
            WriteData = EmpName.substr(0, 16);
            data = objReadCard.Authenticate(4, "FFFFFFFFFFFF", 96);
            if (data != "") {
                alert("Unable to Authenticate Block 4");
//                document.getElementById("<%= lblPersoReportCardValidation.ClientID %>").innerHTML = "Master Card And Dos Card Validation ..... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportReaderInit.ClientID %>").innerHTML = "Reader Initialisation .................................... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCardConn.ClientID %>").innerHTML = "Connecting Card .......................................... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCSNRValidation.ClientID %>").innerHTML = "Card Validation ............................................. <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCardPersonalisation.ClientID %>").innerHTML = "Card Personalisation .................................. <img src=\"Images/error-detected.gif\">";
//                document.getElementById("<%= lblPersoReportComplete.ClientID %>").innerHTML = "Finalising ....................................................... <img src=\"Images/error-detected.gif\">";
//                document.getElementById("<%= lblPersoReportEmail.ClientID %>").innerHTML = "Sending Email .............................................. <img src=\"Images/error-detected.gif\">";
//                $find("mpePersonaliseCard").hide();
//                $find("mpePersoReport").show();
                return false;
            }
            data = objReadCard.WriteData(WriteData, "04");
            if (data != "") {
                alert("Card Error: Unable to write in the card.Sector 1 Block 4");
//                document.getElementById("<%= lblPersoReportCardValidation.ClientID %>").innerHTML = "Master Card And Dos Card Validation ..... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportReaderInit.ClientID %>").innerHTML = "Reader Initialisation .................................... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCardConn.ClientID %>").innerHTML = "Connecting Card .......................................... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCSNRValidation.ClientID %>").innerHTML = "Card Validation ............................................. <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCardPersonalisation.ClientID %>").innerHTML = "Card Personalisation .................................. <img src=\"Images/error-detected.gif\">";
//                document.getElementById("<%= lblPersoReportComplete.ClientID %>").innerHTML = "Finalising ....................................................... <img src=\"Images/error-detected.gif\">";
//                document.getElementById("<%= lblPersoReportEmail.ClientID %>").innerHTML = "Sending Email .............................................. <img src=\"Images/error-detected.gif\">";
//                $find("mpePersonaliseCard").hide();
//                $find("mpePersoReport").show();
                return false;
            }
            WriteData = EmpName.substr(16, 32);
            data = objReadCard.Authenticate(5, "FFFFFFFFFFFF", 96);
            if (data != "") {
                alert("Unable to Authenticate Block 5");
//                document.getElementById("<%= lblPersoReportCardValidation.ClientID %>").innerHTML = "Master Card And Dos Card Validation ..... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportReaderInit.ClientID %>").innerHTML = "Reader Initialisation .................................... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCardConn.ClientID %>").innerHTML = "Connecting Card .......................................... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCSNRValidation.ClientID %>").innerHTML = "Card Validation ............................................. <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCardPersonalisation.ClientID %>").innerHTML = "Card Personalisation .................................. <img src=\"Images/error-detected.gif\">";
//                document.getElementById("<%= lblPersoReportComplete.ClientID %>").innerHTML = "Finalising ....................................................... <img src=\"Images/error-detected.gif\">";
//                document.getElementById("<%= lblPersoReportEmail.ClientID %>").innerHTML = "Sending Email .............................................. <img src=\"Images/error-detected.gif\">";
//                $find("mpePersonaliseCard").hide();
//                $find("mpePersoReport").show();
                return false;
            }
            data = objReadCard.WriteData(WriteData, "05");
            if (data != "") {
                alert("Card Error: Unable to write in the card.Sector 1 Block 5");
//                document.getElementById("<%= lblPersoReportCardValidation.ClientID %>").innerHTML = "Master Card And Dos Card Validation ..... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportReaderInit.ClientID %>").innerHTML = "Reader Initialisation .................................... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCardConn.ClientID %>").innerHTML = "Connecting Card .......................................... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCSNRValidation.ClientID %>").innerHTML = "Card Validation ............................................. <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCardPersonalisation.ClientID %>").innerHTML = "Card Personalisation .................................. <img src=\"Images/error-detected.gif\">";
//                document.getElementById("<%= lblPersoReportComplete.ClientID %>").innerHTML = "Finalising ....................................................... <img src=\"Images/error-detected.gif\">";
//                document.getElementById("<%= lblPersoReportEmail.ClientID %>").innerHTML = "Sending Email .............................................. <img src=\"Images/error-detected.gif\">";
//                $find("mpePersonaliseCard").hide();
//                $find("mpePersoReport").show();
                return false;
            }
            var DOB = document.getElementById("<%= txtDOB.ClientID %>").value;
            //alert(DOB);
            var DOE = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
            // alert(DOE);
            var ExpiryTime = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
            alert(ExpiryTime);
            DOB = DOB.replace(/\//g, "");
            // DOB = DOB.replace(/-/g, "");
            alert(DOB);
            DOE = DOE.replace(/\//g, "");
            alert(DOE);
            ExpiryTime = ExpiryTime.replace(/:/g, "");
            alert(ExpiryTime);
            var Gender = document.getElementById("<%= ddlGender.ClientID %>").value;
            // alert(Gender)
            if (Gender == "M") {
                Gender = "01";
            }
            else {
                Gender = "02";
            }
            WriteData = "";

            WriteData = DOB + Gender + DOE + ExpiryTime + "0000000000";
            alert(WriteData);
            WriteData = objReadCard.Convert(WriteData);
            data = objReadCard.Authenticate(6, "FFFFFFFFFFFF", 96);
            if (data != "") {
                alert("Unable to Authenticate Block 6");
                $find("mpePersonaliseCard").hide();
                return false;
            }
            data = objReadCard.WriteData(WriteData, "6");
            if (data != "") {
                alert("Card Error: Unable to write in the card.Sector 1 Block 6");
                $find("mpePersonaliseCard").hide();
                return false;
            }







//            var DOB = document.getElementById("<%= txtDOB.ClientID %>").innerHTML;
//            var DOE = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
//            var ExpiryTime = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
//            DOB = DOB.replace(/\//g, "");
//            DOE = DOE.replace(/\//g, "");
//            ExpiryTime = ExpiryTime.replace(/:/g, "");
//            var Gender = document.getElementById("<%= ddlGender.ClientID %>").innerHTML;
//            if (Gender == "M") {
//                Gender = "01";
//            }
//            else {
//                Gender = "02";
//            }
//            WriteData = "";
//          //  WriteData = DOB + Gender + "FFFFFFFFFFFFFFFFFFFFFF";
//            WriteData = DOB + Gender + DOE + ExpiryTime + "0000000000";
//            WriteData = objReadCard.Convert(WriteData);
//            data = objReadCard.Authenticate(6, "FFFFFFFFFFFF", 96);
//            if (data != "") {
//                alert("Unable to Authenticate Block 6");
////                document.getElementById("<%= lblPersoReportCardValidation.ClientID %>").innerHTML = "Master Card And Dos Card Validation ..... <img src=\"Images/check.png\">";
////                document.getElementById("<%= lblPersoReportReaderInit.ClientID %>").innerHTML = "Reader Initialisation .................................... <img src=\"Images/check.png\">";
////                document.getElementById("<%= lblPersoReportCardConn.ClientID %>").innerHTML = "Connecting Card .......................................... <img src=\"Images/check.png\">";
////                document.getElementById("<%= lblPersoReportCSNRValidation.ClientID %>").innerHTML = "Card Validation ............................................. <img src=\"Images/check.png\">";
////                document.getElementById("<%= lblPersoReportCardPersonalisation.ClientID %>").innerHTML = "Card Personalisation .................................. <img src=\"Images/error-detected.gif\">";
////                document.getElementById("<%= lblPersoReportComplete.ClientID %>").innerHTML = "Finalising ....................................................... <img src=\"Images/error-detected.gif\">";
////                document.getElementById("<%= lblPersoReportEmail.ClientID %>").innerHTML = "Sending Email .............................................. <img src=\"Images/error-detected.gif\">";
////                $find("mpePersonaliseCard").hide();
////                $find("mpePersoReport").show();
//                return false;
//            }
//            data = objReadCard.WriteData(WriteData, "6");
//            if (data != "") {
//                alert("Card Error: Unable to write in the card.Sector 1 Block 6");
////                document.getElementById("<%= lblPersoReportCardValidation.ClientID %>").innerHTML = "Master Card And Dos Card Validation ..... <img src=\"Images/check.png\">";
////                document.getElementById("<%= lblPersoReportReaderInit.ClientID %>").innerHTML = "Reader Initialisation .................................... <img src=\"Images/check.png\">";
////                document.getElementById("<%= lblPersoReportCardConn.ClientID %>").innerHTML = "Connecting Card .......................................... <img src=\"Images/check.png\">";
////                document.getElementById("<%= lblPersoReportCSNRValidation.ClientID %>").innerHTML = "Card Validation ............................................. <img src=\"Images/check.png\">";
////                document.getElementById("<%= lblPersoReportCardPersonalisation.ClientID %>").innerHTML = "Card Personalisation .................................. <img src=\"Images/error-detected.gif\">";
////                document.getElementById("<%= lblPersoReportComplete.ClientID %>").innerHTML = "Finalising ....................................................... <img src=\"Images/error-detected.gif\">";
////                document.getElementById("<%= lblPersoReportEmail.ClientID %>").innerHTML = "Sending Email .............................................. <img src=\"Images/error-detected.gif\">";
////                $find("mpePersonaliseCard").hide();
////                $find("mpePersoReport").show();
//                return false;
//            }

            WriteData = "";
            data = objReadCard.Authenticate(7, "FFFFFFFFFFFF", 96);
            if (data != "") {
                alert("Unable to Authenticate Block 7");
//                document.getElementById("<%= lblPersoReportCardValidation.ClientID %>").innerHTML = "Master Card And Dos Card Validation ..... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportReaderInit.ClientID %>").innerHTML = "Reader Initialisation .................................... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCardConn.ClientID %>").innerHTML = "Connecting Card .......................................... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCSNRValidation.ClientID %>").innerHTML = "Card Validation ............................................. <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCardPersonalisation.ClientID %>").innerHTML = "Card Personalisation .................................. <img src=\"Images/error-detected.gif\">";
//                document.getElementById("<%= lblPersoReportComplete.ClientID %>").innerHTML = "Finalising ....................................................... <img src=\"Images/error-detected.gif\">";
//                document.getElementById("<%= lblPersoReportEmail.ClientID %>").innerHTML = "Sending Email .............................................. <img src=\"Images/error-detected.gif\">";
//                $find("mpePersonaliseCard").hide();
//                $find("mpePersoReport").show();
                return false;
            }
            WriteData = "";
            WriteData = CMSCalulatedKey + "FF078069" + "FFFFFFFFFFFF";
            WriteData = objReadCard.Convert(WriteData);
            data = objReadCard.WriteData(WriteData, "7");
            if (data != "") {
                alert("Card Error: Unable to write in the card.Sector 1 Block 7");
//                document.getElementById("<%= lblPersoReportCardValidation.ClientID %>").innerHTML = "Master Card And Dos Card Validation ..... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportReaderInit.ClientID %>").innerHTML = "Reader Initialisation .................................... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCardConn.ClientID %>").innerHTML = "Connecting Card .......................................... <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCSNRValidation.ClientID %>").innerHTML = "Card Validation ............................................. <img src=\"Images/check.png\">";
//                document.getElementById("<%= lblPersoReportCardPersonalisation.ClientID %>").innerHTML = "Card Personalisation .................................. <img src=\"Images/error-detected.gif\">";
//                document.getElementById("<%= lblPersoReportComplete.ClientID %>").innerHTML = "Finalising ....................................................... <img src=\"Images/error-detected.gif\">";
//                document.getElementById("<%= lblPersoReportEmail.ClientID %>").innerHTML = "Sending Email .............................................. <img src=\"Images/error-detected.gif\">";
//                $find("mpePersonaliseCard").hide();
//                $find("mpePersoReport").show();
                return false;
            }
            // *********** Sector 1 End******************************************************** //


          
            // ***********************************Sector 15 Start ************************************************//
            var CompanyCode15 = CompCode;
            var SiteID15 = "00";
            var DOE15 = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
            var ExpiryTime15 = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
            DOE15 = DOE15.replace(/\//g, "");
            ExpiryTime15 = ExpiryTime15.replace(/:/g, "");
            var CheckSum15 = "78";
            WriteData = "";
            WriteData = PadBytes(CompanyCode15 + SiteID15 + "00" + "0000" + DOE15 + ExpiryTime15 + CheckSum15 + "0000" + "00", 32);
         
            WriteData = objReadCard.Convert(WriteData);
            data = objReadCard.Authenticate(60, "FFFFFFFFFFFF", 96);
            if (data != "") {
                alert("Unable to Authenticate Block 60");
                $find("mpePersonaliseCard").hide();
                return false;
            }
            data = objReadCard.WriteData(WriteData, "3C");
            if (data != "") {
                alert("Card Error: Unable to write in the card.Sector 15 Block 60");
                $find("mpePersonaliseCard").hide();
                return false;
            }

            WriteData = "";
            data = objReadCard.Authenticate(63, "FFFFFFFFFFFF", 96);
            if (data != "") {
                alert("Unable to Authenticate Block 63");
                $find("mpePersonaliseCard").hide();
                return false;
            }
            WriteData = "";
            WriteData = CMSCalulatedKey + "FF078069" + "FFFFFFFFFFFF";
            WriteData = objReadCard.Convert(WriteData);
            data = objReadCard.WriteData(WriteData, "3F");
            if (data != "") {
                alert("Card Error: Unable to write in the card.Sector 15 Block 63");
                $find("mpePersonaliseCard").hide();
                return false;
            }
            // ***********************************Sector 15 End ************************************************//


            //*****'Following code writes the sector 1 (reallocated) with the card holders data and the random no***********

          /*  data = objReadCard.Authenticate(7, objReadCard.Cardrawkeybuf, 96);
            if (data != "") {
                data = objReadCard.Authenticate(7, "FFFFFFFFFFFF", 96);

                if (data != "") {
                    alert("Authentication Error Sector 1 Block 7");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
            }
            else {
                data = objReadCard.WriteRawKey("7");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 7");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }


            }

            var EmpName = document.getElementById("<%= txtEmployeeName.ClientID %>").value.rpad(" ", 32);
            alert(EmpName);
            WriteData = "";
            WriteData = EmpName.substr(0, 16);
            alert(WriteData);
            data = objReadCard.Authenticate(4, "FFFFFFFFFFFF", 96);
            alert(data);
            if (data != "") {
                alert("Unable to Authenticate Block 4");
                $find("mpePersonaliseCard").hide();
                return false;
            }

            data = objReadCard.WriteData(WriteData, "04");
            alert(data);
            if (data != "") {
                alert("Card Error: Unable to write in the card.Sector 1 Block 4");
                $find("mpePersonaliseCard").hide();
                return false;
            }
            WriteData = EmpName.substr(16, 16);

            data = objReadCard.WriteData(WriteData, "05");
            if (data != "") {
                alert("Card Error: Unable to write in the card.Sector 1 Block 5");
                $find("mpePersonaliseCard").hide();
                return false;
            }

            */
//            var DOB = document.getElementById("<%= txtDOB.ClientID %>").value;
//            //alert(DOB);
//            var DOE = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
//           // alert(DOE);
//            var ExpiryTime = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
//            alert(ExpiryTime);
//            DOB = DOB.replace(/\//g, "");
//           // DOB = DOB.replace(/-/g, "");
//            alert(DOB);
//            DOE = DOE.replace(/\//g, "");
//            alert(DOE);
//            ExpiryTime = ExpiryTime.replace(/:/g, "");
//            alert(ExpiryTime);
//            var Gender = document.getElementById("<%= ddlGender.ClientID %>").value;
//           // alert(Gender)
//            if (Gender == "M") {
//                Gender = "01";
//            }
//            else {
//                Gender = "02";
//            }
//            WriteData = "";

//            WriteData = DOB + Gender + DOE + ExpiryTime + "0000000000";
//            alert(WriteData);
//            WriteData = objReadCard.Convert(WriteData);
//            data = objReadCard.Authenticate(6, "FFFFFFFFFFFF", 96);
//            if (data != "") {
//                alert("Unable to Authenticate Block 6");
//                $find("mpePersonaliseCard").hide();
//                return false;
//            }
//            data = objReadCard.WriteData(WriteData, "6");
//            if (data != "") {
//                alert("Card Error: Unable to write in the card.Sector 1 Block 6");
//                $find("mpePersonaliseCard").hide();
//                return false;
//            }

            //*****************End*******************************************************************************************

            //*********************'below code to lock sector 14 with master key*********************************************

            data = objReadCard.Authenticate(59, MasterKey, 96);
            if (data != "") {
                alert("Authentication Error Sector 14 Block 59");
                $find("mpePersonaliseCard").hide();
                return false;
            }

            data = objReadCard.WriteMasterKey(MasterKey, "3B");
            if (data != "") {
                alert("Card Error: Unable to write in the card.Sector 14 Block 59");
                $find("mpePersonaliseCard").hide();
                return;
            }

            //***************************************end********************************************************************
            //*******below code to lock sector 13 with master key**********************************************
            data = objReadCard.Authenticate(55, MasterKey, 96);
            if (data != "") {
                alert("Authentication Error Sector 13 Block 55");
                $find("mpePersonaliseCard").hide();
                return;
            }

            data = objReadCard.WriteMasterKey(MasterKey, "37");
            if (data != "") {
                alert("Card Error: Unable to write in the card.Sector 13 Block 55");
                $find("mpePersonaliseCard").hide();
                return false;
            }

            //***************end***********************************************************************

            //*********below code to write Application code in sector 0*********************************
            var AppId = new Array();
            AppId[0] = "0400";
            AppId[1] = "0148";
            AppId[2] = "0248";

            data = objReadCard.Authenticate(3, objReadCard.Cardrawkeybuf, 96);
            if (data != "") {
                data = objReadCard.Authenticate(3, "FFFFFFFFFFFF", 96);

                if (data != "") {
                    alert("Authentication Error Sector 0 Block 3");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
            }
            else {
                WriteData = "";
                WriteData = OldDataBlock1.substr(0, 4) + AppId[0] + AppId[1] + AppId[2];
                WriteData = WriteData.rpad("0", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "1");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 0 Block 1");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
               
            }
            //***********End***************************************************************************

            //*** Sector 2 start

            var EmployeeCode = "";
            var strEmpcd = "";
            var ReadData;
            var key = "";
            var RandomNo = "";
            var Key_a_Buf = "";

            RandomNo = objReadCard.CalculateRandomNo().toString();
            RandomNo = objReadCard.Convert(RandomNo);
            ReadData = objReadCard.ProgramRNDs(2, MasterKey, RandomNo);

            var EmployeeCode2 = document.getElementById("<%= txtEmployeeCode.ClientID %>").value;
           // strEmpcd = EmployeeCode2.substr(EmployeeCode2.length - 8).lpad("0", 8);
            strEmpcd = EmployeeCode2.lpad("0", 8);
            for (var i = 0; i < strEmpcd.length; i++) {
                EmployeeCode += 0 + strEmpcd.substr(i, 1);
            }


            if (ReadData == "") {
                alert("Card Error while  reading");
                $find("mpePersonaliseCard").hide();
                return false;
            }

            Key_a_Buf = "FFFFFF";
            key = objReadCard.CalculatetApplKey(parseInt(ReadData), RandomNo, Key_a_Buf);
           // var Key3 = CalculateCMSKeyWithRandomNo("738740688017", "0000" + cardCSNR);

            var key2 = ConvertStringToHex(key);

            if (key == "") {
                alert("Card Error - Presonalizing for Time Attendance.");
                $find("mpePersonaliseCard").hide();
                return false;
            }

            Data = objReadCard.Authenticate(11, "FFFFFFFFFFFF", 96);

            if (Data != "") {
                alert("Authentication Error Sector 2 Block 11");
                $find("mpePersonaliseCard").hide();
                return false;
            }
            else {
                WriteData = "";

                WriteData = EmployeeCode.rpad("0", 32);
                WriteData = objReadCard.Convert(WriteData);
                Data = objReadCard.WriteData(WriteData, "08");
                if (Data != "") {
                    alert("Card Error - Presonalizing for Time Attendance.Sector 2 Block 8");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                //writting calculated key in 11 block sec 2


                //sac
             //  var Key19 = CalculateCMSKeyWithRandomNo("738740688017", "0000" + cardCSNR)
               // alert(Key19);
             //  WriteData = "";
             //   WriteData = PadBytes(Key19 + "78778869" + MasterKey, 32)
              //  WriteData = objReadCard.Convert(WriteData);
              //  Data = objReadCard.WriteData(WriteData, "0B");


                //old perso
                var Key2 = CalculateCMSKeyWithRandomNo("738740688017", "0000" + cardCSNR);
                WriteData = "";
                WriteData = Key2 + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
               Data = objReadCard.WriteData(WriteData, "0B");
                //orignal code
               // WriteData = "";
               // WriteData = key;
               // WriteData = objReadCard.Convert(WriteData);
               // Data = objReadCard.WriteData(WriteData, "B");
                if (Data != "") {
                    alert("Card Error - Presonalizing for Time Attendance.Sector 2 Block 11");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                WriteData = "";
             

                var DOE2 = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var ExpiryTime2 = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                DOE2 = DOE2.replace(/\//g, "");
                ExpiryTime2 = ExpiryTime2.replace(/:/g, "");
                WriteData = "";
                WriteData = DOE2 + ExpiryTime2 + "00" + "00" + "00" + "00000000000000";
                WriteData = PadBytes(WriteData, 32);


                WriteData = objReadCard.Convert(WriteData);
                Data = objReadCard.WriteData(WriteData, "9");
                if (Data != "") {
                    alert("Card Error - Presonalizing for Time Attendance.Sector 2 Block 9");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }


            }

            //** sector 2 end

            //*** sector 3 start
            RandomNo = objReadCard.CalculateRandomNo().toString();
            RandomNo = objReadCard.Convert(RandomNo);

            ReadData = objReadCard.ProgramRNDs(3, MasterKey, RandomNo);
            if (ReadData == "") {
                alert("Card Error - Presonalizing for Access Control.");
                $find("mpePersonaliseCard").hide();
                return false;
            }

            Key_a_Buf = "FFFFFF";
            key = objReadCard.CalculatetApplKey(parseInt(ReadData), RandomNo, Key_a_Buf);
            var key3 = ConvertStringToHex(key);

            if (key == "") {
                alert("Card Error - Presonalizing for Access Control.");
                $find("mpePersonaliseCard").hide();
                return false;
            }

            Data = objReadCard.Authenticate(15, "FFFFFFFFFFFF", 96);

            if (Data != "") {
                alert("Authentication Error Sector 3 Block 15");
                $find("mpePersonaliseCard").hide();
                return false;
            }

            else {
                WriteData = "";
                WriteData = EmployeeCode.rpad("0", 32);
                WriteData = objReadCard.Convert(WriteData);
                Data = objReadCard.WriteData(WriteData, "C");
                if (Data != "") {
                    alert("Card Error - Presonalizing for Access Control.Sector 3 Block 12");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                WriteData = "";
               

                var DOE3 = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var ExpiryTime3 = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                DOE3 = DOE3.replace(/\//g, "");
                ExpiryTime3 = ExpiryTime3.replace(/:/g, "");
                WriteData = "";
                WriteData = DOE3 + ExpiryTime3 + "00" + "00" + "00" + "000000" + "00" + "00" + "00" + "00";
                WriteData = PadBytes(WriteData, 32);


                WriteData = objReadCard.Convert(WriteData);
                Data = objReadCard.WriteData(WriteData, "D");
                if (Data != "") {
                    alert("Card Error - Presonalizing for Access Control.Sector 3 Block 13");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                WriteData = "";
                WriteData = WriteData.rpad("0", 32);
                WriteData = objReadCard.Convert(WriteData);
                Data = objReadCard.WriteData(WriteData, "E");
                if (Data != "") {
                    alert("Card Error - Presonalizing for Access Control.Sector 3 Block 14");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }


                //writting calculated key in 15 block sector 3

                //sac
               // var Key19 = CalculateCMSKeyWithRandomNo("738740688017", "0000" + cardCSNR)
              //  alert(Key19);
              //  WriteData = "";
             //   WriteData = PadBytes(Key19 + "78778869" + MasterKey, 32)
              //  WriteData = objReadCard.Convert(WriteData);
              //  Data = objReadCard.WriteData(WriteData, "0B");


                //old perso
                var Key2 = CalculateCMSKeyWithRandomNo("738740688017", "0000" + cardCSNR);
                WriteData = "";
                WriteData = Key2 + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                Data = objReadCard.WriteData(WriteData, "F");

               //original code
               // WriteData = "";
               // WriteData = key;
                //WriteData = objReadCard.Convert(WriteData);
                //Data = objReadCard.WriteData(WriteData, "F");
                if (Data != "") {
                    alert("Card Error - Presonalizing for Time Attendance.Sector 3 Block 15");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
            }

            //**** sector 3 end

              var empcode = document.getElementById("<%= txtEmployeeCode.ClientID %>").value
                // var Password = "";
                var Password = RandomNo1();

                var ExpiryDate = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var LogId = document.getElementById("<%= LogId.ClientID %>").value;
                // Database Updation Start
                var UpdateDatabase = false;
                $.ajax({
                    url: "SACPersoDataCapture.aspx/CompletePersonalisation",
                    type: "POST",
                    dataType: "json",
                    //data: "{'EmpCode':'" + empcode + "','pin':'" + pin + "','CSNR':'" + cardCSNR + "','Password':'" + Password + "'}",
                    data: "{'EmpCode':'" + empcode + "','pin':'" + pin + "','CSNR':'" + cardCSNR + "','Password':'" + Password + "','ExpiryDate':'" + ExpiryDate + "','LogId':'" + LogId + "'}",
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
                // Database Updation End
                // Send Mail Start
//                var SendMail = false;
//                $.ajax({
//                    url: "SACPersoDataCapture.aspx/SendMail",
//                   type: "POST",
//                    dataType: "json",
//                    data: "{'EmpCode':'" + empcode + "','pin':'" + pin + "','CSNR':'" + cardCSNR + "','Password':'" + Password + "'}",
//                    async: false,
//                    contentType: "application/json; charset=utf-8",
//                    success: function (msg) {
//                         alert(msg.d);
//                        if (msg.d == "True") {
//                            SendMail = true;

//                            alert("Card personalised Successfully.");
//                        }
//                        else {
//                            alert("Card personalised successfull but mail not send");
//                            SendMail = false;
//                        }
//                    },
//                    error: function () { alert(arguments[2]); }
//                });


//                if (SendMail == false)
//                 {
//                    // return false;
//                 }

                 // Send Mail End


                 alert("Card personalised Successfully.");
                $find("mpePersonaliseCard").hide();
                return false;
            }


            function CardPersonalisation2() {
                debugger;

                var MasterKey = "";
                var DosKey = "";
                var WriteData = "";
                var ServerFlag = false;
                var CompCode = "";
                var cardCSNR = "";

                //Reading Master Key

                $.ajax({
                    url: "SACPersoDataCapture.aspx/GetMasterKey",
                    type: "POST",
                    dataType: "json",
                    data: "",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d == "") {
                            alert("Master Key not found. Please Verify Master card first.");
                            navigateToUrl("SACMasterCard.aspx");
                            ServerFlag = false;
                        }
                        else {
                            MasterKey = msg.d;
                            ServerFlag = true;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });

                //Reading Company Code

                $.ajax({
                    url: "SACPersoDataCapture.aspx/GetCompanyCode",
                    type: "POST",
                    dataType: "json",
                    data: "",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d == "") {
                            alert("Company Code was not found. Please Verify Master card first.");
                            navigateToUrl("SACMasterCard.aspx");
                            ServerFlag = false;
                        }
                        else {
                            CompCode = msg.d;
                            ServerFlag = true;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });

                if (ServerFlag == false) {
                    navigateToUrl("SACMasterCard.aspx");
                    return false;
                }

                var objReadCard;
                var DataInitialize;
                var cardCSNR = "";
                var ReadData;

                objReadCard = new ActiveXObject("ContactlessCardRW.Card");
                DataInitialize = objReadCard.Initialise();

                if (DataInitialize != "") {
                    alert("Card reader not connected or Error in card Initialization");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                var data;
                data = objReadCard.ConnectToCard();
                if (data != "") {
                    alert("Error in connecting to card");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                data = objReadCard.LoadKey(objReadCard.Cardrawkeybuf);
                if (data != "") {
                    $find("mpePersonaliseCard").hide();
                    return false;
                }


                cardCSNR = objReadCard.CSNR.replace(/ /g, "").substring(0, 8);
                cardCSNR = ReverseCSNR(cardCSNR);
                var CSNRExist = false;
                $.ajax({
                    url: "SACPersoDataCapture.aspx/CheckCSNR",
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
                    return false;
                }

                var CMSCalulatedKey = XOR("0000" + cardCSNR, "434D53434D53");

                //*******************Checking for personalized card************************************************************

                data = objReadCard.Authenticate(8, objReadCard.Cardrawkeybuf, 96);
                if (data != "") {
                    alert("Authentication Error Sector 2 Block 8");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                ReadData = objReadCard.ReadTAEMP(MasterKey).substr(0, 8);


                if (ReadData != "" && ReadData != "00000000") {
                    alert("Card Is Already personalized or Card Error");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }



                //**********End of checking for personalization


                //*****************Read App Code from sec 0********************************************************
                data = objReadCard.Authenticate(3, objReadCard.Cardrawkeybuf, 96);
                if (data != "") {
                    alert("Authentication Error Sector 0");
                    $find("mpePersonaliseCard").hide();
                    return;
                }
                else {
                    var OldDataBlock1 = objReadCard.ReadBlock(1);
                    var OldDataBlock2 = objReadCard.ReadBlock(2);

                    var Stemp = "";

                    for (var i = 0; i < OldDataBlock1.length; i++) {
                        if (OldDataBlock1.substr(i, 1) != " ") {
                            Stemp += OldDataBlock1.substr(i, 1);
                        }
                    }

                    OldDataBlock1 = Stemp;

                }
                //****************end of reading app code***********************************************************


                //***********Locking Sector 0 with cms key ********************************************************
                WriteData = "";
                data = objReadCard.WriteRawKey("3");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 0 Block 3");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }


                //*********************end***********************************************************************


                // ***********Sector 1 Start ******************************************************** //

                var EmpName = document.getElementById("<%= txtEmployeeName.ClientID %>").innerHTML.rpad(" ", 32);
                var WriteData = "";
                WriteData = EmpName.substr(0, 16);
                data = objReadCard.Authenticate(4, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 4");
                    //                document.getElementById("<%= lblPersoReportCardValidation.ClientID %>").innerHTML = "Master Card And Dos Card Validation ..... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportReaderInit.ClientID %>").innerHTML = "Reader Initialisation .................................... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCardConn.ClientID %>").innerHTML = "Connecting Card .......................................... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCSNRValidation.ClientID %>").innerHTML = "Card Validation ............................................. <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCardPersonalisation.ClientID %>").innerHTML = "Card Personalisation .................................. <img src=\"Images/error-detected.gif\">";
                    //                document.getElementById("<%= lblPersoReportComplete.ClientID %>").innerHTML = "Finalising ....................................................... <img src=\"Images/error-detected.gif\">";
                    //                document.getElementById("<%= lblPersoReportEmail.ClientID %>").innerHTML = "Sending Email .............................................. <img src=\"Images/error-detected.gif\">";
                    //                $find("mpePersonaliseCard").hide();
                    //                $find("mpePersoReport").show();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "04");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 4");
                    //                document.getElementById("<%= lblPersoReportCardValidation.ClientID %>").innerHTML = "Master Card And Dos Card Validation ..... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportReaderInit.ClientID %>").innerHTML = "Reader Initialisation .................................... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCardConn.ClientID %>").innerHTML = "Connecting Card .......................................... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCSNRValidation.ClientID %>").innerHTML = "Card Validation ............................................. <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCardPersonalisation.ClientID %>").innerHTML = "Card Personalisation .................................. <img src=\"Images/error-detected.gif\">";
                    //                document.getElementById("<%= lblPersoReportComplete.ClientID %>").innerHTML = "Finalising ....................................................... <img src=\"Images/error-detected.gif\">";
                    //                document.getElementById("<%= lblPersoReportEmail.ClientID %>").innerHTML = "Sending Email .............................................. <img src=\"Images/error-detected.gif\">";
                    //                $find("mpePersonaliseCard").hide();
                    //                $find("mpePersoReport").show();
                    return false;
                }
                WriteData = EmpName.substr(16, 32);
                data = objReadCard.Authenticate(5, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 5");
                    //                document.getElementById("<%= lblPersoReportCardValidation.ClientID %>").innerHTML = "Master Card And Dos Card Validation ..... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportReaderInit.ClientID %>").innerHTML = "Reader Initialisation .................................... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCardConn.ClientID %>").innerHTML = "Connecting Card .......................................... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCSNRValidation.ClientID %>").innerHTML = "Card Validation ............................................. <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCardPersonalisation.ClientID %>").innerHTML = "Card Personalisation .................................. <img src=\"Images/error-detected.gif\">";
                    //                document.getElementById("<%= lblPersoReportComplete.ClientID %>").innerHTML = "Finalising ....................................................... <img src=\"Images/error-detected.gif\">";
                    //                document.getElementById("<%= lblPersoReportEmail.ClientID %>").innerHTML = "Sending Email .............................................. <img src=\"Images/error-detected.gif\">";
                    //                $find("mpePersonaliseCard").hide();
                    //                $find("mpePersoReport").show();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "05");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 5");
                    //                document.getElementById("<%= lblPersoReportCardValidation.ClientID %>").innerHTML = "Master Card And Dos Card Validation ..... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportReaderInit.ClientID %>").innerHTML = "Reader Initialisation .................................... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCardConn.ClientID %>").innerHTML = "Connecting Card .......................................... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCSNRValidation.ClientID %>").innerHTML = "Card Validation ............................................. <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCardPersonalisation.ClientID %>").innerHTML = "Card Personalisation .................................. <img src=\"Images/error-detected.gif\">";
                    //                document.getElementById("<%= lblPersoReportComplete.ClientID %>").innerHTML = "Finalising ....................................................... <img src=\"Images/error-detected.gif\">";
                    //                document.getElementById("<%= lblPersoReportEmail.ClientID %>").innerHTML = "Sending Email .............................................. <img src=\"Images/error-detected.gif\">";
                    //                $find("mpePersonaliseCard").hide();
                    //                $find("mpePersoReport").show();
                    return false;
                }
                var DOB = document.getElementById("<%= txtDOB.ClientID %>").value;
                //alert(DOB);
                var DOE = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                // alert(DOE);
                var ExpiryTime = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                alert(ExpiryTime);
                DOB = DOB.replace(/\//g, "");
                // DOB = DOB.replace(/-/g, "");
                alert(DOB);
                DOE = DOE.replace(/\//g, "");
                alert(DOE);
                ExpiryTime = ExpiryTime.replace(/:/g, "");
                alert(ExpiryTime);
                var Gender = document.getElementById("<%= ddlGender.ClientID %>").value;
                // alert(Gender)
                if (Gender == "M") {
                    Gender = "01";
                }
                else {
                    Gender = "02";
                }
                WriteData = "";

                WriteData = DOB + Gender + DOE + ExpiryTime + "0000000000";
                alert(WriteData);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(6, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 6");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "6");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 6");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }







           

                WriteData = "";
                data = objReadCard.Authenticate(7, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 7");
                    //                document.getElementById("<%= lblPersoReportCardValidation.ClientID %>").innerHTML = "Master Card And Dos Card Validation ..... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportReaderInit.ClientID %>").innerHTML = "Reader Initialisation .................................... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCardConn.ClientID %>").innerHTML = "Connecting Card .......................................... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCSNRValidation.ClientID %>").innerHTML = "Card Validation ............................................. <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCardPersonalisation.ClientID %>").innerHTML = "Card Personalisation .................................. <img src=\"Images/error-detected.gif\">";
                    //                document.getElementById("<%= lblPersoReportComplete.ClientID %>").innerHTML = "Finalising ....................................................... <img src=\"Images/error-detected.gif\">";
                    //                document.getElementById("<%= lblPersoReportEmail.ClientID %>").innerHTML = "Sending Email .............................................. <img src=\"Images/error-detected.gif\">";
                    //                $find("mpePersonaliseCard").hide();
                    //                $find("mpePersoReport").show();
                    return false;
                }
                WriteData = "";
                WriteData = CMSCalulatedKey + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "7");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 7");
                    //                document.getElementById("<%= lblPersoReportCardValidation.ClientID %>").innerHTML = "Master Card And Dos Card Validation ..... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportReaderInit.ClientID %>").innerHTML = "Reader Initialisation .................................... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCardConn.ClientID %>").innerHTML = "Connecting Card .......................................... <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCSNRValidation.ClientID %>").innerHTML = "Card Validation ............................................. <img src=\"Images/check.png\">";
                    //                document.getElementById("<%= lblPersoReportCardPersonalisation.ClientID %>").innerHTML = "Card Personalisation .................................. <img src=\"Images/error-detected.gif\">";
                    //                document.getElementById("<%= lblPersoReportComplete.ClientID %>").innerHTML = "Finalising ....................................................... <img src=\"Images/error-detected.gif\">";
                    //                document.getElementById("<%= lblPersoReportEmail.ClientID %>").innerHTML = "Sending Email .............................................. <img src=\"Images/error-detected.gif\">";
                    //                $find("mpePersonaliseCard").hide();
                    //                $find("mpePersoReport").show();
                    return false;
                }
                // *********** Sector 1 End******************************************************** //


                //*** Sector 2 start

                var EmployeeCode = "";
                var strEmpcd = "";
                var ReadData;
                var key = "";
                var RandomNo = "";
                var Key_a_Buf = "";

                RandomNo = objReadCard.CalculateRandomNo().toString();
                RandomNo = objReadCard.Convert(RandomNo);
                ReadData = objReadCard.ProgramRNDs(2, MasterKey, RandomNo);

                var EmployeeCode2 = document.getElementById("<%= txtEmployeeCode.ClientID %>").value;
                // strEmpcd = EmployeeCode2.substr(EmployeeCode2.length - 8).lpad("0", 8);
                strEmpcd = EmployeeCode2.lpad("0", 8);
                for (var i = 0; i < strEmpcd.length; i++) {
                    EmployeeCode += 0 + strEmpcd.substr(i, 1);
                }


                if (ReadData == "") {
                    alert("Card Error while  reading");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                Key_a_Buf = "FFFFFF";
                key = objReadCard.CalculatetApplKey(parseInt(ReadData), RandomNo, Key_a_Buf);


                if (key == "") {
                    alert("Card Error - Presonalizing for Time Attendance.");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                Data = objReadCard.Authenticate(11, "FFFFFFFFFFFF", 96);

                if (Data != "") {
                    alert("Authentication Error Sector 2 Block 11");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                else {
                    WriteData = "";

                    WriteData = EmployeeCode.rpad("0", 32);
                    WriteData = objReadCard.Convert(WriteData);
                    Data = objReadCard.WriteData(WriteData, "08");
                    if (Data != "") {
                        alert("Card Error - Presonalizing for Time Attendance.Sector 2 Block 8");
                        $find("mpePersonaliseCard").hide();
                        return false;
                    }

                    //writting calculated key in 11 block sec 2
                    WriteData = "";
                    WriteData = key;
                    Data = objReadCard.WriteData(WriteData, "B");
                    if (Data != "") {
                        alert("Card Error - Presonalizing for Time Attendance.Sector 2 Block 11");
                        $find("mpePersonaliseCard").hide();
                        return false;
                    }

                    WriteData = "";


                    var DOE2 = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                    var ExpiryTime2 = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                    DOE2 = DOE2.replace(/\//g, "");
                    ExpiryTime2 = ExpiryTime2.replace(/:/g, "");
                    WriteData = "";
                    WriteData = DOE2 + ExpiryTime2 + "00" + "00" + "00" + "00000000000000";
                    WriteData = PadBytes(WriteData, 32);


                    WriteData = objReadCard.Convert(WriteData);
                    Data = objReadCard.WriteData(WriteData, "9");
                    if (Data != "") {
                        alert("Card Error - Presonalizing for Time Attendance.Sector 2 Block 9");
                        $find("mpePersonaliseCard").hide();
                        return false;
                    }


                }

                //** sector 2 end



                //*** sector 3 start
                RandomNo = objReadCard.CalculateRandomNo().toString();
                RandomNo = objReadCard.Convert(RandomNo);

                ReadData = objReadCard.ProgramRNDs(3, MasterKey, RandomNo);
                if (ReadData == "") {
                    alert("Card Error - Presonalizing for Access Control.");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                Key_a_Buf = "FFFFFF";
                key = objReadCard.CalculatetApplKey(parseInt(ReadData), RandomNo, Key_a_Buf);

                if (key == "") {
                    alert("Card Error - Presonalizing for Access Control.");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                Data = objReadCard.Authenticate(15, "FFFFFFFFFFFF", 96);

                if (Data != "") {
                    alert("Authentication Error Sector 3 Block 15");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                else {
                    WriteData = "";
                    WriteData = EmployeeCode.rpad("0", 32);
                    WriteData = objReadCard.Convert(WriteData);
                    Data = objReadCard.WriteData(WriteData, "C");
                    if (Data != "") {
                        alert("Card Error - Presonalizing for Access Control.Sector 3 Block 12");
                        $find("mpePersonaliseCard").hide();
                        return false;
                    }

                    WriteData = "";


                    var DOE3 = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                    var ExpiryTime3 = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                    DOE3 = DOE3.replace(/\//g, "");
                    ExpiryTime3 = ExpiryTime3.replace(/:/g, "");
                    WriteData = "";
                    WriteData = DOE3 + ExpiryTime3 + "00" + "00" + "00" + "000000" + "00" + "00" + "00" + "00";
                    WriteData = PadBytes(WriteData, 32);


                    WriteData = objReadCard.Convert(WriteData);
                    Data = objReadCard.WriteData(WriteData, "D");
                    if (Data != "") {
                        alert("Card Error - Presonalizing for Access Control.Sector 3 Block 13");
                        $find("mpePersonaliseCard").hide();
                        return false;
                    }

                    WriteData = "";
                    WriteData = WriteData.rpad("0", 32);
                    WriteData = objReadCard.Convert(WriteData);
                    Data = objReadCard.WriteData(WriteData, "E");
                    if (Data != "") {
                        alert("Card Error - Presonalizing for Access Control.Sector 3 Block 14");
                        $find("mpePersonaliseCard").hide();
                        return false;
                    }


                    //writting calculated key in 15 block sector 3

                    WriteData = "";
                    WriteData = key;
                    Data = objReadCard.WriteData(WriteData, "F");
                    if (Data != "") {
                        alert("Card Error - Presonalizing for Time Attendance.Sector 3 Block 15");
                        $find("mpePersonaliseCard").hide();
                        return false;
                    }
                }

                //**** sector 3 end

                //*******below code to lock sector 13 with master key**********************************************
                data = objReadCard.Authenticate(55, MasterKey, 96);
                if (data != "") {
                    alert("Authentication Error Sector 13 Block 55");
                    $find("mpePersonaliseCard").hide();
                    return;
                }

                data = objReadCard.WriteMasterKey(MasterKey, "37");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 13 Block 55");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                //***************end***********************************************************************





                //*********************'below code to lock sector 14 with master key*********************************************

                data = objReadCard.Authenticate(59, MasterKey, 96);
                if (data != "") {
                    alert("Authentication Error Sector 14 Block 59");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                data = objReadCard.WriteMasterKey(MasterKey, "3B");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 14 Block 59");
                    $find("mpePersonaliseCard").hide();
                    return;
                }

                //***************************************end********************************************************************
                // ***********************************Sector 15 Start ************************************************//
                var CompanyCode15 = CompCode;
                var SiteID15 = "00";
                var DOE15 = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var ExpiryTime15 = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                DOE15 = DOE15.replace(/\//g, "");
                ExpiryTime15 = ExpiryTime15.replace(/:/g, "");
                var CheckSum15 = "78";
                WriteData = "";
                WriteData = PadBytes(CompanyCode15 + SiteID15 + "00" + "0000" + DOE15 + ExpiryTime15 + CheckSum15 + "0000" + "00", 32);

                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(60, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 60");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "3C");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 15 Block 60");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                WriteData = "";
                data = objReadCard.Authenticate(63, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 63");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                WriteData = "";
                WriteData = CMSCalulatedKey + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "3F");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 15 Block 63");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                // ***********************************Sector 15 End ************************************************//


             

              

                //*********below code to write Application code in sector 0*********************************
                var AppId = new Array();
                AppId[0] = "0400";
                AppId[1] = "0148";
                AppId[2] = "0248";

                data = objReadCard.Authenticate(3, objReadCard.Cardrawkeybuf, 96);
                if (data != "") {
                    data = objReadCard.Authenticate(3, "FFFFFFFFFFFF", 96);

                    if (data != "") {
                        alert("Authentication Error Sector 0 Block 3");
                        $find("mpePersonaliseCard").hide();
                        return false;
                    }
                }
                else {
                    WriteData = "";
                    WriteData = OldDataBlock1.substr(0, 4) + AppId[0] + AppId[1] + AppId[2];
                    WriteData = WriteData.rpad("0", 32);
                    WriteData = objReadCard.Convert(WriteData);
                    data = objReadCard.WriteData(WriteData, "1");
                    if (data != "") {
                        alert("Card Error: Unable to write in the card.Sector 0 Block 1");
                        $find("mpePersonaliseCard").hide();
                        return false;
                    }

                }
                //***********End***************************************************************************


            

                var empcode = document.getElementById("<%= txtEmployeeCode.ClientID %>").value
                // var Password = "";
                var Password = RandomNo1();

                var ExpiryDate = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var LogId = document.getElementById("<%= LogId.ClientID %>").value;
                // Database Updation Start
                var UpdateDatabase = false;
                $.ajax({
                    url: "SACPersoDataCapture.aspx/CompletePersonalisation",
                    type: "POST",
                    dataType: "json",
                    //data: "{'EmpCode':'" + empcode + "','pin':'" + pin + "','CSNR':'" + cardCSNR + "','Password':'" + Password + "'}",
                    data: "{'EmpCode':'" + empcode + "','pin':'" + pin + "','CSNR':'" + cardCSNR + "','Password':'" + Password + "','ExpiryDate':'" + ExpiryDate + "','LogId':'" + LogId + "'}",
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
           
                alert("Card personalised Successfully.");
                $find("mpePersonaliseCard").hide();
                return false;
            }







            function CardPersonalisation() {
                debugger;
            try {
              
                var MasterKey = "";
                var DosKey = "";
                var WriteData = "";
                var ServerFlag = false;
                var CompCode = "";
                $.ajax({
                    url: "SACPersoDataCapture.aspx/GetMasterKey",
                    type: "POST",
                    dataType: "json",
                    data: "",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d == "") {
                            alert("Master Key not found. Please Verify Master card first.");
                            navigateToUrl("SACMasterCard.aspx");
                            ServerFlag = false;
                        }
                        else {
                            MasterKey = msg.d;
                            ServerFlag = true;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });

               $.ajax({
                    url: "SACPersoDataCapture.aspx/GetCompanyCode",
                    type: "POST",
                    dataType: "json",
                    data: "",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d == "") {
                            alert("Company Code was not found. Please Verify Master card first.");
                            navigateToUrl("SACMasterCard.aspx");
                            ServerFlag = false;
                        }
                        else {
                            CompCode = msg.d;
                            ServerFlag = true;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });

                if (ServerFlag == false) {
                    navigateToUrl("SACMasterCard.aspx");
                    return false;
                }

                var objReadCard;
                var DataInitialize;
                var cardCSNR = "";
                objReadCard = new ActiveXObject("ContactlessCardRW.Card");
                DataInitialize = objReadCard.Initialise();
                if (DataInitialize != "") {
                    alert("Card reader not connected or Error in card Initialization");

                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                var data;
                data = objReadCard.ConnectToCard();
                if (data != "") {
                    alert("Card not detected");

                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                cardCSNR = objReadCard.CSNR.replace(/ /g, "").substring(0, 8);
                cardCSNR = ReverseCSNR(cardCSNR);
                var CSNRExist = false;
                $.ajax({
                    url: "SACPersoDataCapture.aspx/CheckCSNR",
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
                    return false;
                }

                var CMSCalulatedKey = XOR("0000" + cardCSNR, "434D53434D53");
                alert(CMSCalulatedKey);


                //*******************Checking for personalized card************************************************************

                data = objReadCard.Authenticate(8, objReadCard.Cardrawkeybuf, 96);
                if (data != "") {
                    alert("Authentication Error Sector 2 Block 8");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                ReadData = objReadCard.ReadTAEMP(MasterKey).substr(0, 8);


                if (ReadData != "" && ReadData != "00000000") {
                    alert("Card Is Already personalized or Card Error");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }



                //**********End of checking for personalization
                // ***********Sector 0 Start ******************************************************** //
                data = objReadCard.Authenticate(1, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 1");
                    $find("mpePersonaliseCard").hide();

                    return false;
                }
                WriteData = "";
                // WriteData = "FF0F0400000000000248000000000000";
                WriteData =    "FF0F0400014804480000000000000000";
               // 000000000248000000000000";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "1");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 0 Block 1");
                    $find("mpePersonaliseCard").hide();

                    return false;
                }
                data = objReadCard.Authenticate(2, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 2");
                    $find("mpePersonaliseCard").hide();

                    return false;
                }
                WriteData = "";
                WriteData = "00000000000000000000010002000300";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "2");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 0 Block 2");
                    return false;
                }

                data = objReadCard.Authenticate(3, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 3");

                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                WriteData = "";
                WriteData = CMSCalulatedKey + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "3");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 0 Block 3");

                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                // ***********Sector 0 End ******************************************************** //

                // ***********Sector 1 Start ******************************************************** //

                var EmpName = document.getElementById("<%= txtEmployeeName.ClientID %>").value.rpad(" ", 32);
               
                var WriteData = "";
                WriteData = EmpName.substr(0, 16);
                data = objReadCard.Authenticate(4, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 4");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "04");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 4");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                WriteData = EmpName.substr(16, 32);
                data = objReadCard.Authenticate(5, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 5");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "05");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 5");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                var DOB = document.getElementById("<%= txtDOB.ClientID %>").value;
                var DOE = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var ExpiryTime = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                DOB = DOB.replace(/\//g, "");
                DOE = DOE.replace(/\//g, "");
                ExpiryTime = ExpiryTime.replace(/:/g, "");
                var Gender = document.getElementById("<%= ddlGender.ClientID %>").value;
                if (Gender == "M") {
                    Gender = "01";
                }
                else {
                    Gender = "02";
                }
                WriteData = "";
                //WriteData = DOB + Gender + "FFFFFFFFFFFFFFFFFFFFFF";
                WriteData = DOB + Gender + DOE + ExpiryTime + "0000000000";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(6, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 6");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "6");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 6");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                WriteData = "";
                data = objReadCard.Authenticate(7, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 7");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                WriteData = "";
                WriteData = CMSCalulatedKey + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "7");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 7");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                // *********** Sector 1 End******************************************************** //
                // ***********Sector 2 Start ******************************************************** //
                var EmployeeCode2 = document.getElementById("<%= txtEmployeeCode.ClientID %>").value.lpad("0", 8);
                //strEmpcd = EmployeeCode2.lpad("0", 8);

                //alredy
                //EmployeeCode2 = EmployeeCode2.substr(EmployeeCode2.length - 8).lpad("0", 8);


             // var temp = "";
                //for (var a = 0; a < strEmpcd.length; a++) {

                //   temp = temp + "0" + strEmpcd.charAt(a);
              //  }
              //  EmployeeCode2 = temp;
                WriteData = "";
                WriteData = ConvertStringToHex(EmployeeCode2) + "000000000000";
               // WriteData = EmployeeCode2 + "000000000000";
               // WriteData = EmployeeCode2.rpad("0", 32);
                 WriteData = PadBytes(WriteData, 32);
                 // WriteData = ConvertStringToHex(WriteData);
                
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(8, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 8");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "08");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 8");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                var DOE2 = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var ExpiryTime2 = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                DOE2 = DOE2.replace(/\//g, "");
                ExpiryTime2 = ExpiryTime2.replace(/:/g, "");
                WriteData = "";
                WriteData = DOE2 + ExpiryTime2 + "00" + "00" + "00" + "00000000000000";
                WriteData = PadBytes(WriteData, 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(9, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 9");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "09");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 9");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                WriteData = "";
                data = objReadCard.Authenticate(11, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 11");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                var Key2 = CalculateCMSKeyWithRandomNo("738740688017", "0000" + cardCSNR);
                alert(Key2);
                WriteData = "";
                WriteData = Key2 + "FF078069" + "FFFFFFFFFFFF";
                alert(WriteData);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "0B");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 11");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                // *********** Sector 2 End******************************************************** //

                // ***********Sector 3 Start ******************************************************** //
                var EmployeeCode3 = document.getElementById("<%= txtEmployeeCode.ClientID %>").value;
               EmployeeCode3 = EmployeeCode3.substr(EmployeeCode3.length - 8).lpad("0", 8);
              // var temp = "";
             // for (var a = 0; a < EmployeeCode3.length; a++) {
                    
                 //  temp = temp + "0" + EmployeeCode3.charAt(a);
             //   }
              // EmployeeCode3 = temp;
               
                WriteData = "";
                //            
              //  WriteData = EmployeeCode3 + "00" + "01010101" + "000000";
                WriteData = ConvertStringToHex(EmployeeCode3) + "00" + "01010101" + "00";
               // WriteData = EmployeeCode3.rpad("0", 32);
                 WriteData = PadBytes(WriteData, 32);
                // WriteData = ConvertStringToHex(WriteData);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(12, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 12");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "0C");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 3 Block 12");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                var DOE3 = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var ExpiryTime3 = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                DOE3 = DOE3.replace(/\//g, "");
                ExpiryTime3 = ExpiryTime3.replace(/:/g, "");
                WriteData = "";
                WriteData = DOE3 + ExpiryTime3 + "00" + "00" + "00" + "000000" + "00" + "00" + "00" + "00";
                WriteData = PadBytes(WriteData, 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(13, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 13");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "0D");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 3 Block 13");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                WriteData = "";
                data = objReadCard.Authenticate(15, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 15");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                var Key3 = CalculateCMSKeyWithRandomNo("738740688017", "0000" + cardCSNR);
                alert(Key3);
                WriteData = "";
                WriteData = Key3 + "FF078069" + "FFFFFFFFFFFF";
                alert(WriteData);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "0F");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 3 Block 15");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                // *********** Sector 3 End******************************************************** //

                // ***********************************Sector 13 Start ************************************************//

                // WriteData = MasterKey + "FF078069" + "FFFFFFFFFFFF";
                WriteData = MasterKey + "FF078069" + MasterKey;
                //WriteData = MasterKey;
                WriteData = PadBytes(WriteData, 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(55, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 55");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "37");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 13 Block 55");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                // ***********************************Sector 13 End ************************************************//
                
                // ***********************************Sector 14 Start ************************************************//
              var RandomNo = "738740688017";
                WriteData = "";
                WriteData = "00000000" + RandomNo + RandomNo;
                WriteData = PadBytes(WriteData, 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(56, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 56");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "38");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 14 Block 56");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }


                WriteData = MasterKey + "FF078069" + MasterKey;
              //  WriteData = MasterKey + "FF078069" + "FFFFFFFFFFFF";
               // WriteData = MasterKey;
                WriteData = PadBytes(WriteData, 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(59, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 59");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "3B");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 14 Block 59");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
               
                // ***********************************Sector 14 End ************************************************//
               
              
                // ***********************************Sector 15 Start ************************************************//
                //var CompanyCode15 = "0022";
                var CompanyCode15 = CompCode;
                var SiteID15 = "00";
                var DOE15 = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var ExpiryTime15 = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                DOE15 = DOE15.replace(/\//g, "");
                ExpiryTime15 = ExpiryTime15.replace(/:/g, "");
                var CheckSum15 = "78";
                WriteData = "";
                WriteData = PadBytes(CompanyCode15 + SiteID15 + "00" + "0000" + DOE15 + ExpiryTime15 + CheckSum15 + "0000" + "00", 32);
               
                //WriteData = PadBytes(CompanyCode15 + SiteID15 + "00" + "0002" + DOE15 + ExpiryTime15 + CheckSum15 + "0248" + "00", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(60, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 60");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "3C");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 15 Block 60");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }

                WriteData = "";
                data = objReadCard.Authenticate(63, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 63");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                WriteData = "";
                WriteData = CMSCalulatedKey + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "3F");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 15 Block 63");
                    $find("mpePersonaliseCard").hide();
                    return false;
                }
                // ***********************************Sector 15 End ************************************************//

                var empcode = document.getElementById("<%= txtEmployeeCode.ClientID %>").value
                // var Password = "";
                var Password = RandomNo1();

                var ExpiryDate = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var LogId = document.getElementById("<%= LogId.ClientID %>").value;
                // Database Updation Start
                var UpdateDatabase = false;
                $.ajax({
                    url: "SACPersoDataCapture.aspx/CompletePersonalisation",
                    type: "POST",
                    dataType: "json",
                    //data: "{'EmpCode':'" + empcode + "','pin':'" + pin + "','CSNR':'" + cardCSNR + "','Password':'" + Password + "'}",
                    data: "{'EmpCode':'" + empcode + "','pin':'" + pin + "','CSNR':'" + cardCSNR + "','Password':'" + Password + "','ExpiryDate':'" + ExpiryDate + "','LogId':'" + LogId + "'}",
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
                // Database Updation End
                // Send Mail Start
             //   var SendMail = false;
              //  $.ajax({
              //      url: "SACPersoDataCapture.aspx/SendMail",
                //    type: "POST",
                 //   dataType: "json",
                 //   data: "{'EmpCode':'" + empcode + "','pin':'" + pin + "','CSNR':'" + cardCSNR + "','Password':'" + Password + "'}",
                  //  async: false,
                  //  contentType: "application/json; charset=utf-8",
                   // success: function (msg) {
                        //  alert(msg.d);
                       // if (msg.d == "True") {
                        //    SendMail = true;
                         //   alert("Card personalised Successfully.");

                       // }
                      //  else {
                         //   alert("Card personalised successfull but mail not send");
                         //   SendMail = false;
                       // }
                  //  },
                  //  error: function () { alert(arguments[2]); }
             //   });
                //if (SendMail == false) {
                    // return false;
              //  }
                // Send Mail End
                alert("Card personalised Successfully.");

                $find("mpePersonaliseCard").hide();
                return false;
            }
            catch (ex) {
                alert(ex.Message);
                return false;
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




        function RandomNo1() {
            try {
                var min = 10000000;
                var max = 99999999;
                var num = Math.floor(Math.random() * (max - min + 1)) + min;
                return num;
            }
            catch (ex) {
                alert(ex.Message);
            }
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

        function CreatePin(CSNR) {
            try {
                var Pin = "";

                var xor = XOR("01010101", CSNR);
                var array = [];
                var xorVal = xor;
                for (var i = 0; i < 4; i++) {
                    var rnd = xorVal.substring(0, 2);
                    xorVal = xorVal.substring(2, xorVal.length);
                    var val = parseInt(rnd, 16);
                    var tempVal = val.toString();
                    var oriVal = tempVal;
                    var Flag = 10;
                    while (Flag > 9) {
                        Flag = 0;
                        for (var j = 0; j < oriVal.length; j++) {
                            var digit = tempVal.substring(0, 1);
                            tempVal = tempVal.substring(1, tempVal.length);
                            Flag = Flag + parseInt(digit);
                        }
                        oriVal = Flag.toString();
                        tempVal = Flag.toString();

                    }
                    Pin = Pin + Flag;

                }
                return Pin;
            }
            catch (e) {
                alert(e);
            }
        }
        function CalculateCMSKeyWithRandomNo(RandomNo, CSNR) {
            try {
                var tempR = RandomNo;
                var tempC = CSNR;
                var CalculatedKey = "";
                for (var i = 0; i < 12; i = i + 2) {
                    var rnd = tempR.substring(0, 2);
                    tempR = tempR.substring(2, tempR.length);
                    var CRC = rnd;
                    for (var j = 0; j < 12; j = j + 2) {
                        var csn = tempC.substring(0, 2);
                        var CRC1 = "";
                        tempC = tempC.substring(2, tempC.length);


                        CRC1 = XOR(CRC, csn);
                        var temp = dscrc8_table[parseInt(CRC1, 16)];
                        CRC = temp.toString(16).toUpperCase();
                        if (CRC.length == 1) {
                            CRC = "0" + CRC;
                        }
                    }
                    tempC = CSNR;
                    CalculatedKey = CalculatedKey + CRC;

                }
                tempR = RandomNo;
                return CalculatedKey;
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

        function XOR(val1, val2) {
            try {
                var result = "";
                if (val1.length == val2.length) {
                    for (var i = 0; i < val1.length; i++) {
                        var a = padZero(parseInt(val1.substr(i, 1), 16).toString(2));
                        var b = padZero(parseInt(val2.substr(i, 1), 16).toString(2));

                        //var a = padZero(parseInt(val1[i], 16).toString(2));
                        //var b = padZero(parseInt(val2[i], 16).toString(2));
                        var exor = "";
                        for (var j = 0; j < a.length; j++) {
                            exor += a.substr(j, 1) ^ b.substr(j, 1);
                        }
                        result += (parseInt(exor, 2).toString(16)).toUpperCase();
                    }
                }
                return result;
            }
            catch (ex) {
                alert(ex.Message + " XOR");
            }
        }

        function padZero(val) {
            try {
                for (var i = val.length; i < 4; i++) {
                    val = '0' + val;
                }
                return val;
            }
            catch (ex) {
                alert(ex.Message + " padZero");
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

        //pads right
        String.prototype.rpad = function (padString, length) {
            var str = this;
            while (str.length < length)
                str = str + padString;
            return str;
        }

    </script>
    <script type="text/javascript">
        function CancelLostCard() {
            try {
                $find("mpeLostCard").hide();
                //$find("mpePersonaliseCard").show();
            }
            catch (ex) {
                alert(ex.Message);
            }
        }
        function CancelPersonaliseCard() {
            try {
                $find("mpePersonaliseCard").hide();
            }
            catch (ex) {
                alert(ex.Message);
            }
        }
    </script>
    <style type="text/css">
        .style37
        {
            width: 123%;
        }
        .display
        {
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdConn" runat="server" />
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Perso Data Capture" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div class="DivEmpDetails">
        <asp:Panel ID="Panel3" runat="server" DefaultButton="btnSearch">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 40%; text-align: left;">
                    </td>
                    <td style="width: 60%; text-align: right;">
                        <asp:DropDownList ID="ddlEmployeeType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="EmployeeType_OnSelectIndexChange">
                            <asp:ListItem Text="ALL" Selected="True" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Employee" Value="E"></asp:ListItem>
                            <asp:ListItem Text="Non-Employee" Value="NE"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnReset" runat="server" Text="Reset" Style="float: right;" CssClass="ButtonControl"
                            OnClick="btnReset_Click" />
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                            OnClick="btnSearch_Click" />
                        <asp:TextBox ID="txtEmpName" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtEmpName" runat="server" TargetControlID="txtEmpName"
                            WatermarkText="Employee Name" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:TextBox ID="txtEmpID" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtEmpID" runat="server" TargetControlID="txtEmpID"
                            WatermarkText="Employee Code" WatermarkCssClass="watermark">
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
                                <asp:GridView ID="gvEmpDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                    GridLines="None" AllowPaging="True" OnRowCommand="gvEmpDetails_RowCommand" OnRowDataBound="gvEmpDetails_RowDataBound">
                                    <RowStyle CssClass="gvRow" />
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                    <PagerStyle CssClass="gvPager" />
                                    <EmptyDataTemplate>
                                        <div>
                                            <span>No Employees found.</span>
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Employee Code" ItemStyle-Wrap="true" SortExpression="EPD_EMPID">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEmp" runat="server" CausesValidation="False" CommandName="View"
                                                    CommandArgument='<%#Eval("EPD_EMPID") %>' Text='<%#Eval("EPD_EMPID") %>' ForeColor="#3366FF"> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("Emp_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- HeaderStyle-CssClass="display" ItemStyle-CssClass="display"--%>
                                       <%--ItemStyle-CssClass="hideCol" HeaderStyle-CssClass="hideCol"--%>

                                        <asp:TemplateField HeaderText="Photo" visible= "false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblImage" runat="server" Text='<%#Eval("EmpImage")%>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="lnkImageCapture" runat="server" CommandName="AddImg" CommandArgument='<%#Eval("EPD_EMPID") %>'
                                                    Text="Capture" ForeColor="#3366FF" Visible="false"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkImageEdit" runat="server" CommandName="EditImg" CommandArgument='<%#Eval("EPD_EMPID") %>'
                                                    Text="Modify" ForeColor="#3366FF" Visible="false"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Signature" visible= "false" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblSign" runat="server" Text='<%#Eval("EmpSign")%>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="lnkSignCapture" runat="server" CommandName="AddSign" CommandArgument='<%#Eval("EPD_EMPID") %>'
                                                    Text="Capture" ForeColor="#3366FF" Visible="false"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkSignEdit" runat="server" CommandName="EditSign" CommandArgument='<%#Eval("EPD_EMPID") %>'
                                                    Text="Modify" ForeColor="#3366FF" Visible="false"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Finger Print" visible= "false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBio1" runat="server" Text='<%#Eval("ISOTempMin1")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblBio2" runat="server" Text='<%#Eval("ISOTempMin2")%>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="lnkBioCapture" runat="server" CommandName="AddBio" CommandArgument='<%#Eval("EPD_EMPID") %>'
                                                    Text="Capture" ForeColor="#3366FF" Visible="false"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkBioEdit" runat="server" CommandName="EditBio" CommandArgument='<%#Eval("EPD_EMPID") %>'
                                                    Text="Modify" ForeColor="#3366FF" Visible="false"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkWriteOnCard" runat="server" CommandName="Write" CommandArgument='<%#Eval("EPD_EMPID") %>'
                                                    Text=" | Write On Card" ForeColor="#3366FF" Visible="false"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Print Card" visible= "false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkCardPrinting" runat="server" CommandName="Card" Text="Print"
                                                    CommandArgument='<%#Eval("EPD_EMPID") %>' ForeColor="#3366FF"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Personalize Card" visible= "false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPerso" runat="server" CommandName="PersoView" Text="Diasble"
                                                    Enabled="false" Font-Bold="true" ForeColor="#3366FF"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: left; width: 15%;">
                                                    <span>Go To : </span>
                                                    <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChangePage">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="gvPrevious"
                                                        CssClass="ButtonControl" />
                                                    <asp:Label ID="lblShowing" runat="server" Text="Showing "></asp:Label>
                                                    <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="gvNext" CssClass="ButtonControl" />
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
                                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
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
        <asp:Panel ID="pnlAddEmployee" runat="server" CssClass="PopupPanel" Width="80%">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <legend style="color: Green; padding-left: 5px; padding-right: 5px; font-weight: bold;">
                            Employee Personal Details :</legend>
                        <table id="table2" runat="server" width="100%" height="90%" border="0" cellpadding="0"
                            cellspacing="0" class="TableClass">
                            <tr id="Tr121" runat="server">
                                <td id="as" class="TDClassLabel1" runat="server" style="text-align: left; font-weight: bold;">
                                    Employee Id :<label class="CompulsaryLabel"></label>
                                </td>
                                <td id="Td1" style="height: 10px; width: 200px;" class="TDClassControl1" runat="server">
                                    <asp:Label ID="EPD_EMPLOYEEID" runat="server" Style="text-transform: uppercase;"
                                        Width="180px"></asp:Label>
                                    <br />
                                </td>
                                <td id="Td2" class="TDClassLabel1" runat="server" style="text-align: left; font-weight: bold;">
                                    Full Name :<label class="CompulsaryLabel"></label>
                                </td>
                                <td id="Td3" runat="server" class="style42" style="height: 10px; width: 200px;">
                                    <asp:Label ID="EPD_FIRST_NAME" runat="server" Style="text-transform: uppercase;"
                                        ClientIDMode="Static" Width="180px"></asp:Label>
                                    <br />
                                </td>
                                <td id="Td6" class="TDClassLabel1" style="height: 10px; width: 200px; text-align: left;
                                    font-weight: bold;" runat="server">
                                    Salutation :&nbsp;
                                </td>
                                <td id="Td7" style="height: 10px; width: 100px;" colspan="3" runat="server" class="TDClassControl1">
                                    <asp:Label ID="EPD_SALUTATION" runat="server" Width="180px" Style="text-transform: uppercase;"></asp:Label>
                                    <br />
                                </td>
                                <td id="Td43" rowspan="6" class="TDClassControl1" colspan="2" style="height: 5px;"
                                    runat="server" align="right">
                                    <asp:Image ID="imgEmployeeImage" ImageUrl="~/Handler1.ashx" runat="server" Height="100px"
                                        Style="text-align: center;" Width="89px" ClientIDMode="Static" BorderWidth="1px"
                                        ImageAlign="Middle" TabIndex="5" />
                                    <p>
                                        <asp:Label ID="lblImageSize" runat="server" Text="" CssClass="ErrorLabel"></asp:Label></p>
                                </td>
                            </tr>
                            <tr id="Tr5" runat="server">
                                <td id="Td124" class="TDClassLabel1" runat="server" style="padding-right: 50px; text-align: left;
                                    font-weight: bold;">
                                    Gender :<label class="CompulsaryLabel"></label>
                                </td>
                                <td id="Td32" runat="server" class="style42">
                                    <asp:Label ID="lblGender" runat="server" Width="180px" Style="text-transform: uppercase;"></asp:Label>
                                    <asp:DropDownList ID="EPD_GENDER" runat="server" ClientIDMode="Static" TabIndex="7"
                                        Width="180px" Style="display: none;">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="M">Male</asp:ListItem>
                                        <asp:ListItem Value="F">Female</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                </td>
                                <%--<td id="Td41" class="style37" runat="server" style="text-align: left;padding-right:54px;">
                                Nick Name :&nbsp;
                            </td>
                            <td id="Td42" style="height: 10px; width: 100px;padding-right:17px;" runat="server" class="TDClassControl1">
                                <asp:Label ID="EPD_NICKNAME" runat="server" Width="180px"></asp:Label>
                                <br />
                            </td>--%>
                                <td id="Td17" class="TDClassLabel1" style="height: 10px; text-align: left; font-weight: bold;"
                                    runat="server">
                                    Marital Status : &nbsp;
                                </td>
                                <td id="Td18" style="height: 10px; width: 200px;" runat="server" class="TDClassControl1">
                                    <asp:DropDownList ID="EPD_MARITAL_STATUS" runat="server" TabIndex="9" Width="180px"
                                        Style="display: none;">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblMartialStatus" runat="server" Width="180px"></asp:Label>
                                </td>
                                <td id="Td15" class="TDClassLabel1" style="height: 10px; text-align: left; font-weight: bold;"
                                    runat="server">
                                    DOB :<label class="CompulsaryLabel"></label>
                                </td>
                                <td id="Td16" style="height: 10px; width: 200px;" runat="server" class="TDClassControl1">
                                    <asp:Label ID="DOB" runat="server" ClientIDMode="Static" Width="180px"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            <tr id="Tr8" runat="server">
                                <td id="Td8" class="TDClassLabel1" runat="server" style="text-align: left; width: 40px;
                                    font-weight: bold;">
                                    Card No :
                                </td>
                                <td id="Td9" style="height: 5px; width: 200px;" runat="server" class="TDClassControl1">
                                    <asp:Label ID="EPD_CARD_NO" runat="server" MaxLength="8" Width="180px"></asp:Label>
                                    <br />
                                </td>
                                <td id="Td25" class="TDClassLabel1" runat="server" style="text-align: left; font-weight: bold;">
                                    PAN : &nbsp;
                                </td>
                                <td id="Td26" style="height: 5px; width: 200px;" runat="server" class="TDClassControl1">
                                    <asp:Label ID="EPD_PAN" runat="server" Width="180px"></asp:Label>
                                </td>
                                <td id="Td109" class="TDClassLabel1" style="height: 10px; height: 25px; text-align: left;
                                    font-weight: bold;" runat="server">
                                    Aadhar-Card :
                                </td>
                                <td style="padding-left: 3px;">
                                    <asp:Label ID="txtAadhar" runat="server" ClientIDMode="Static" Width="200px"></asp:Label>
                                </td>
                            </tr>
                            <tr id="Tr1" runat="server">
                                <td id="Td139" class="TDClassLabel1" style="height: 10px; text-align: left; width: 200px;
                                    font-weight: bold;" runat="server">
                                    E-mail Id: &nbsp;
                                </td>
                                <td id="Td143" style="height: 10px; width: 80px;" runat="server" class="TDClassControl1">
                                    <asp:Label ID="EPD_EMAIL" runat="server" Width="180px"></asp:Label>
                                </td>
                                <td id="Td14" class="TDClassLabel1" style="height: 10px; text-align: left; font-weight: bold;"
                                    runat="server">
                                    Domicile : &nbsp;
                                </td>
                                <td id="Td24" style="margin-left: 40px; padding-right: 10px;" runat="server" class="style42">
                                    <asp:Label CssClass="TextControl" ID="EPD_DOMICILE" runat="server" Width="180px"></asp:Label>
                                </td>
                                <td id="Td45" class="TDClassLabel1" runat="server" style="text-align: left; font-weight: bold;">
                                    Religion : &nbsp;
                                </td>
                                <td id="Td46" style="height: 10px; width: 200px;" runat="server" class="TDClassControl1">
                                    <asp:DropDownList ID="EPD_RELIGION" runat="server" TabIndex="20" Width="180px" Style="display: none">
                                        <asp:ListItem Value="null">Select</asp:ListItem>
                                        <asp:ListItem Value="M">Male</asp:ListItem>
                                        <asp:ListItem Value="F">Female</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblReligion" runat="server" ClientIDMode="Static" Width="180px"></asp:Label>
                                </td>
                            </tr>
                            <tr id="Tr6" runat="server">
                                <td id="Td47" class="TDClassLabel1" style="height: 5px; text-align: left; font-weight: bold;"
                                    runat="server">
                                    Blood Group :
                                </td>
                                <td id="Td48" style="height: 10px; width: 200px;" runat="server" class="TDClassControl1">
                                    <asp:Label ID="EPD_BLOODGROUP" runat="server" Width="180px"></asp:Label>
                                </td>
                                <td id="Td49" class="TDClassLabel1" style="height: 5px; text-align: left; font-weight: bold;"
                                    runat="server">
                                    Doctor : &nbsp;
                                </td>
                                <td id="Td50" style="height: 5px; width: 200px;" runat="server" class="TDClassControl1">
                                    <asp:Label ID="EPD_DOCTOR" Style="text-transform: capitalize;" runat="server" Width="180px"></asp:Label>
                                     <asp:Label ID="lblPersoReportCardValidation" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <table>
                        <tr>
                            <td style="height: 2%;">
                            </td>
                        </tr>
                    </table>
                    <table id="table10" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                        class="TableClass">
                        <tr>
                            <td style="width: 100%">
                                <fieldset>
                                    <legend style="color: Green; padding-left: 5px; padding-right: 5px; font-weight: bold;">
                                        Employee Official Details : </legend>
                                    <table id="table6" runat="server" width="100%" height="90%" border="0" cellpadding="0"
                                        cellspacing="0" class="TableClass">
                                        <tr id="Tr12" runat="server">
                                            <td id="Td60" class="TDClassLabel1" style="height: 10px; text-align: left; font-weight: bold;"
                                                runat="server">
                                                Joining :<label class="CompulsaryLabel"></label>
                                            </td>
                                            <td id="Td61" class="TDClassControl1" style="height: 10px; text-align: left;" runat="server">
                                                <asp:Label ID="joindt" runat="server" ClientIDMode="Static" Width="80px"></asp:Label>
                                            </td>
                                            <td id="Td62" class="TDClassLabel1" style="height: 10px; text-align: left; width: 180px;
                                                font-weight: bold;" runat="server">
                                                Confirmation :
                                            </td>
                                            <td id="Td63" class="TDClassControl1" style="height: 10px; text-align: left;" runat="server">
                                                <asp:Label ID="Confdt1" runat="server" ClientIDMode="Static" Width="80px"></asp:Label>
                                            </td>
                                            <td id="Td72" class="TDClassLabel1" style="height: 10px; text-align: left; font-weight: bold;"
                                                runat="server">
                                                Centre :<label class="CompulsaryLabel"></label>
                                            </td>
                                            <td id="Td73" class="TDClassControl1" style="height: 10px; text-align: left;" runat="server">
                                                <asp:Label ID="lblcompany" runat="server" ClientIDMode="Static" Width="80px"></asp:Label>
                                                <asp:DropDownList ID="ddlcompany" runat="server" ClientIDMode="Static" Width="120px"
                                                    Style="display: none;">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                            <td id="Td69" class="TDClassLabel1" style="height: 10px; text-align: left; font-weight: bold;"
                                                runat="server">
                                                Unit :<label class="CompulsaryLabel"></label>
                                            </td>
                                            <td id="Td70" class="TDClassControl1" style="height: 10px; text-align: left;" runat="server">
                                                <asp:Label ID="lbllocation" runat="server" ClientIDMode="Static" Width="80px"></asp:Label>
                                                <asp:DropDownList ID="ddllocation" runat="server" ClientIDMode="Static" Width="120px"
                                                    Style="display: none;">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr id="Tr114" runat="server">
                                            <td id="Td99" class="TDClassLabel1" style="height: 4px; text-align: left;" runat="server">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td71" class="TDClassLabel1" style="height: 10px; text-align: left; font-weight: bold;"
                                                runat="server">
                                                Entity :<label class="CompulsaryLabel"></label>
                                            </td>
                                            <td id="Td118" class="TDClassControl1" style="height: 10px; text-align: left;" runat="server">
                                                <asp:Label ID="lbldivision" runat="server" ClientIDMode="Static" Width="80px"></asp:Label>
                                                <asp:DropDownList ID="ddldivision" runat="server" TabIndex="44" ClientIDMode="Static"
                                                    Width="120px" Style="display: none;">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                            <td id="Td74" class="TDClassLabel1" style="height: 10px; text-align: left; font-weight: bold;"
                                                runat="server">
                                                Group :<label class="CompulsaryLabel"></label>
                                            </td>
                                            <td id="Td97" class="TDClassControl1" style="height: 10px; text-align: left;" runat="server">
                                                <asp:Label ID="lbldepartment" runat="server" ClientIDMode="Static" Width="80px"></asp:Label>
                                                <asp:DropDownList ID="ddldepartment" runat="server" TabIndex="45" ClientIDMode="Static"
                                                    Width="120px" Style="display: none;">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                            <td id="Td75" class="TDClassLabel1" style="height: 10px; text-align: left; font-weight: bold;"
                                                runat="server">
                                                Designation :<label class="CompulsaryLabel"></label>
                                            </td>
                                            <td id="Td76" class="TDClassControl1" style="height: 10px; text-align: left;" runat="server">
                                                <asp:Label ID="lbldesignation" runat="server" ClientIDMode="Static" Width="80px"></asp:Label>
                                                <asp:DropDownList ID="ddldesignation" runat="server" ClientIDMode="Static" Width="120px"
                                                    Style="display: none;">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                            <td id="Td77" class="TDClassLabel1" style="height: 10px; text-align: left; font-weight: bold;"
                                                runat="server">
                                                Category :<label class="CompulsaryLabel"></label>
                                            </td>
                                            <td id="Td78" class="TDClassControl1" style="height: 10px; text-align: left;" runat="server">
                                                <asp:Label ID="lblCategory" runat="server" ClientIDMode="Static" Width="80px"></asp:Label>
                                                <asp:DropDownList ID="ddlcategory" runat="server" ClientIDMode="Static" Width="120px"
                                                    Style="display: none;">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr id="Tr14" runat="server">
                                            <td id="Td68" class="TDClassLabel1" style="height: 4px" runat="server">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td80" class="TDClassLabel1" style="height: 10px; text-align: left; font-weight: bold;"
                                                runat="server">
                                                Section :<label class="CompulsaryLabel"></label>
                                            </td>
                                            <td id="Td81" class="TDClassControl1" style="height: 10px; text-align: left;" runat="server">
                                                <asp:Label ID="lblgroup" runat="server" ClientIDMode="Static" Width="80px"></asp:Label>
                                                <asp:DropDownList ID="ddlgroup" runat="server" TabIndex="48" ClientIDMode="Static"
                                                    Width="120px" Style="display: none;">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                            <td id="Td88" class="TDClassLabel1" style="height: 10px; text-align: left; font-weight: bold;"
                                                runat="server">
                                                Division :<label class="CompulsaryLabel"></label>
                                            </td>
                                            <td id="Td89" class="TDClassControl1" style="height: 10px; text-align: left;" runat="server">
                                                <asp:Label ID="lblgrade" runat="server" ClientIDMode="Static" Width="80px"></asp:Label>
                                                <asp:DropDownList ID="ddlgrade" runat="server" ClientIDMode="Static" Width="120px"
                                                    Style="display: none;">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                            <td id="Td90" class="TDClassLabel1" style="height: 10px; text-align: left; font-weight: bold;"
                                                runat="server">
                                                Status :<%--<label class="CompulsaryLabel">*</label>--%></td>
                                            <td id="Td91" class="TDClassControl1" style="height: 10px; text-align: left;" runat="server">
                                                <asp:Label ID="lblstatus" runat="server" ClientIDMode="Static" Width="80px"></asp:Label>
                                                <asp:DropDownList ID="ddlstatus" runat="server" ClientIDMode="Static" Width="120px"
                                                    Style="display: none;">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                            <%--<td id="Td105" class="TDClassLabel1" style="height: 10px; text-align: left;font-weight:bold;" runat="server">
                                            Reporting Person :
                                        </td>
                                        <td id="Td106" class="TDClassControl1" style="height: 10px; text-align: left;" runat="server">
                                          <asp:Label ID="lblManager" runat="server" ClientIDMode="Static" Width="80px"></asp:Label>
                                            <asp:DropDownList ID="ddlManager" runat="server" ClientIDMode="Static" Width="120px"  style="display:none;">
                                            </asp:DropDownList>
                                      
                                        </td>--%>
                                            <td id="Td92" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                                <%-- Active :<label class="CompulsaryLabel">*</label>--%>
                                            </td>
                                            <td id="Td93" class="TDClassControl1" style="height: 10px; text-align: left;" runat="server"
                                                colspan="2">
                                                <asp:RadioButtonList ID="Rbtnchecked" runat="server" RepeatDirection="Horizontal"
                                                    TabIndex="52" ClientIDMode="Static" Width="100px" Visible="false">
                                                    <asp:ListItem Value="False" Text="Active" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="True" Text="InActive"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td51" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                            </td>
                                            <td id="Td102" class="TDClassControl1" style="height: 10px" runat="server">
                                            </td>
                                            <td id="Td103" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                runat="server">
                                            </td>
                                            <td id="Td104" class="TDClassControl1" style="height: 10px" runat="server" colspan="2">
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                    <table id="table9" runat="server" width="100%" border="0" cellpadding="3" cellspacing="3">
                        <tr id="Tr15" runat="server">
                            <td id="Td96" align="center" runat="server">
                                <asp:Button ID="Btnclear1" runat="server" Text="Close" CssClass="ButtonControl" OnClick="Btnclear_Click" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td align="center" class="style39">
                                <asp:Label ID="lblSaveMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger ControlID="Btnclear1" />--%>
                    <%--<asp:PostBackTrigger ControlID="btnAdd" />--%>
                    <%--<asp:PostBackTrigger ControlID="btnImage" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Button ID="Button1" runat="server" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="mpeAddEmployee" runat="server" TargetControlID="Button1"
            BehaviorID="ModalBehaviour" PopupControlID="pnlAddEmployee" BackgroundCssClass="modalBackground"
            Enabled="true">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlLostCard" runat="server" CssClass="PopupPanel">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="lblEmployeeCodeLostCard" runat="server" Style="display: none;"></asp:TextBox>
                                <%--<asp:Label ID="lblEmployeeCodeLostCard" runat="server" Text="gdfgdf" Style="display: none;"></asp:Label>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="color: Red; font-size: 10px;">
                                <asp:Label ID="Label1" runat="server" Text="Card already issued, if lost please the fill the below details before personalizing a new card."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblReason" runat="server" Text="Reason">
                
                                </asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlReason" runat="server">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtReason" runat="server">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAdditionalInfo" runat="server" Text="Additional Info">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdditionalInfo" runat="server">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                <asp:Label ID="lblFileUpload" runat="server" Text="File Upload"></asp:Label>
                            </td>
                            <td>
                                <asp:FileUpload ID="fudFile" runat="server" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <asp:Label ID="lblLostDate" runat="server" Text="Lost Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLostDate" onkeyPress="javascript: return false" runat="server"
                                    Height="18px" Width="111px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="caltxtLostDate" TargetControlID="txtLostDate" PopupButtonID="txtLostDate"
                                    runat="server" Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;" colspan="2">
                                <asp:Button ID="btnSaveLostCard" runat="server" Text="Save And Continue" CssClass="ButtonControl"
                                    ValidationGroup="addreason" OnClientClick="return SaveLostCard();" />
                                <asp:Button ID="btnCancelLostCard" runat="server" Text="Cancel" CssClass="ButtonControl"
                                    OnClientClick="CancelLostCard()" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Button ID="btnLostCardDummy" runat="server" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="mpeLostCard" runat="server" TargetControlID="btnLostCardDummy"
            BehaviorID="mpeLostCard" PopupControlID="pnlLostCard" BackgroundCssClass="modalBackground"
            Enabled="true">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPersonaliseCard" runat="server" CssClass="PopupPanel" Width="30%">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td style="vertical-align: top;" class="style37">
                                <asp:Panel ID="pnlPersonalisation" runat="server" Style="width: 100%; text-align: center;">
                                    <table style="margin: 0 auto;">
                                        <tr>
                                            <td class="heading" colspan="3">
                                                Personalisation Info
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 50%;">
                                                <asp:Label ID="lblEmployeeCode" runat="server" Text="Employee Code :"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 50%;">
                                                <asp:TextBox ID="txtEmployeeCode" runat="server" ValidationGroup="CardPerso" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td rowspan="10" style="vertical-align: top">
                                                <iframe frameborder="no" src="ImageUploadIframe.aspx" id="imgiFrame" style="border: none;
                                                    height: 200px" scrolling="no" runat="server" visible="false"></iframe>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name :"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="TextControl" TabIndex="2"
                                                    ValidationGroup="CardPerso" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblDOB" runat="server" Text="Date Of Birth :"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtDOB" runat="server" CssClass="TextControl" TabIndex="3" ValidationGroup="CardPerso"
                                                    ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label2" runat="server" Text="Gender :"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlGender" runat="server" AutoPostBack="true" CssClass="ComboControl"
                                                    TabIndex="4" Enabled="False" Width="120px" ValidationGroup="CardPerso">
                                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblCardExpiryDate" runat="server" Text="Expiry Date :"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtCardExpiryDate" runat="server" CssClass="TextControl" MaxLength="10"
                                                    onkeydown="date_dash(this,event)" onkeypress="return IsNumber(event)" onkeyup="date_dash(this,event)"
                                                    TabIndex="5" ValidationGroup="CardPerso"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="caltxtCardExpiryDate" runat="server" TargetControlID="txtCardExpiryDate"
                                                    PopupButtonID="txtCardExpiryDate" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblCardExpiryTime" runat="server" Text="Expiry Time :"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtCardExpiryTime" runat="server" CssClass="TextControl" MaxLength="5"
                                                    onkeydown="Time_Colon(this,event)" onkeypress="return IsNumber(event)" onkeyup="Time_Colon(this,event)"
                                                    TabIndex="2" ValidationGroup="CardPerso"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblEmployeeType" runat="server" Text="Employee Type :"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlEmployeeTypePerso" runat="server" CssClass="ComboControl"
                                                    TabIndex="4" Enabled="true" Width="120px" ValidationGroup="CardPerso">
                                                    <asp:ListItem Value="P">Permanent</asp:ListItem>
                                                    <asp:ListItem Value="T">Temporary</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblLocationCode" runat="server" Text="Location Code :"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlLocationCode" runat="server" CssClass="ComboControl" TabIndex="4"
                                                    Enabled="true" Width="120px" ValidationGroup="CardPerso">
                                                    <asp:ListItem Value="S">SAC</asp:ListItem>
                                                    <asp:ListItem Value="D">DES</asp:ListItem>
                                                    <asp:ListItem Value="B">BOPAL</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblApplication" runat="server" Text="Application :"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:CheckBoxList ID="chkApplication" runat="server" BorderColor="Gray" BorderWidth="1"
                                                    TabIndex="3" Style="text-align: left;" ValidationGroup="CardPerso">
                                                    <asp:ListItem> Time Attendance </asp:ListItem>
                                                    <asp:ListItem> Access Control </asp:ListItem>
                                                </asp:CheckBoxList>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: center;">
                                            </td>
                                            <td colspan="2" style="text-align: center;">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;" colspan="2">
                                                <asp:Button ID="btnPersonaliseCard" runat="server" Text="Submit" CssClass="ButtonControl"
                                                    OnClientClick="CardPersonalisation();" ValidationGroup="CardPerso" />
                                                <asp:Button ID="btnCancelPersonaliseCard" runat="server" Text="Cancel" CssClass="ButtonControl"
                                                    OnClientClick="CancelPersonaliseCard()" />
                                                <asp:HiddenField ID="hdnPerDate" runat="server" />
                                                <asp:HiddenField ID="hdnCardNum" runat="server" />
                                                <asp:HiddenField ID="hdnCSNR" runat="server" />
                                                <asp:HiddenField ID="LogId" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Button ID="btnPersonalisedCardDummy" runat="server" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="mpePersonaliseCard" runat="server" TargetControlID="btnPersonalisedCardDummy"
            BehaviorID="mpePersonaliseCard" PopupControlID="pnlPersonaliseCard" BackgroundCssClass="modalBackground"
            Enabled="true">
        </ajaxToolkit:ModalPopupExtender>
    </div>
    <table style="width: 100%;">
        <tr>
            <td style="width: 100%; text-align: center; font-weight: bold; color: Black; font-size: medium">
                The links in <font color="red">RED</font>color indicates that one/more data is yet
                to be captured.
                <asp:Label ID="lblPersoReportReaderInit" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblPersoReportCardConn" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblPersoReportCSNRValidation" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblPersoReportCardPersonalisation" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblPersoReportComplete" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblPersoReportEmail" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
