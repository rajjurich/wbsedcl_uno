<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="SACMasterCard.aspx.cs" Inherits="UNO.SACMasterCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ValidateMasterCard() {
            debugger;
            try {
                if (document.getElementById("<%= txtMasterCardID.ClientID %>").value == "") {
                    alert("Please Enter Card ID");
                    return false;
                }
                else
                    if (document.getElementById("<%= txtMasterCardPassword.ClientID %>").value == "") {
                        alert("Please Enter Password");
                        return false;
                    }
                   
                        else {
                            var objReadCard;
                            var DataInitialize;
                            objReadCard = new ActiveXObject("ContactlessCardRW.Card");
                            //alert(objReadCard);
                            DataInitialize = objReadCard.Initialise();
                            if (DataInitialize != "") {
                                alert("Omnikey reader not connected or Error in card Initialization");
                                return false;
                            }
                            var data;
                            data = objReadCard.ConnectToCard();
                            document.getElementById("<%= hdnMasterKey.ClientID %>").value = data;
                            if (data != "") {
                                alert("Error in connecting to card");
                                return false;
                            }


                            var Useid = objReadCard.userid;
                            var password = objReadCard.pwd;

                            var Masterkey = objReadCard.ReadMasterKey(document.getElementById("<%= txtMasterCardID.ClientID %>").value, document.getElementById("<%= txtMasterCardPassword.ClientID %>").value);
                            if (Masterkey != "000000000000" && Masterkey != "" && Masterkey.length == 12) {
                                '<%Session["MasterKey"] = "' + Masterkey + '"; %>';
                            }
                            else {
                                //
                                Masterkey = "";
                                CompanyCode = "";
                                $.ajax({
                                    url: "SACMasterCard.aspx/SetMasterKey",
                                    type: "POST",
                                    dataType: "json",
                                    data: "{'MasterKey':'" + Masterkey + "','CompanyCode':'" + CompanyCode + "'}",
                                    async: false,
                                    contentType: "application/json; charset=utf-8",
                                    success: function (msg) {

                                        if (msg.d == "True") {
                                            ServerFlag = true;
                                        }
                                        else {
                                            ServerFlag = false;
                                        }
                                    },
                                    error: function () { alert(arguments[2]); }
                                });
                                //
                                alert("Authentication Failed");
                                return false;
                            }
                            //var CompanyCd = objReadCard.ReadCMP(document.getElementById("<%= txtMasterCardID.ClientID %>").value, document.getElementById("<%= txtMasterCardPassword.ClientID %>").value);
                            // Added  by Aarti and Pooja
                            //
                            var UserId = document.getElementById("<%= txtMasterCardID.ClientID %>").value;
                            var Password = document.getElementById("<%= txtMasterCardPassword.ClientID %>").value;
                            var KeyA = "";
                            var Seed = "SACHIN";

                            var HexUserId = ConvertStringToHex(UserId);
                            var HexSeed = ConvertStringToHex(Seed);                            
                            var tempBuff = AddHex(HexUserId, HexSeed);                           
                            var HexPassword = ConvertStringToHex(Password);
                            //var HexCompanyCode = ConvertStringToHex(CompanyCode);
                            KeyA = XOR(tempBuff, HexPassword);


                            ////////Added by Aarti
                            cardCSNR = objReadCard.CSNR.replace(/ /g, "").substring(0, 8);
                            cardCSNR = ReverseCSNR(cardCSNR);
                            var KeyB = "";
                            var CompanyName = "CMSCMS";
                            // var HexCSNR = ConvertStringToHex(cardCSNR);
                            var lpadCSNR = "0000" + cardCSNR
                            var HexCompanyName = ConvertStringToHex(CompanyName);
                            // HexCompanyName =  "0000"+ HexCompanyName ;
                            KeyB = XOR(lpadCSNR, HexCompanyName);


                        //    data = objReadCard.Authenticate(08, KeyB, 96);
                           // if (data != "") {
                             //   alert("Unable to Authenticate Block 8");
                             //   return false;
                           // }
                           // data = objReadCard.LoadKey(KeyB);
                           // if (data != "") {
                           //     alert("Unable to Load key to Block 8");
                            //    return false;
                         //   }

                          //  data = objReadCard.ReadBlock(08).replace(/ /g, "");
                            //  if (data == "00000000000000000000000000000000" || data == "" || data.length < 32) {

                             //     alert("Unable to Authenticate Master Card");
                            //      return false;
                            //  }







                            //data = objReadCard.Authenticate(60, KeyA, 96);
                            data = objReadCard.Authenticate(60, KeyB, 96);
                            if (data != "") {
                                alert("Unable to Authenticate Block 60");
                                return false;
                            }

                            data = objReadCard.LoadKey(KeyB);
                            if (data != "") {
                                alert("Unable to Load key to Block 60");
                                return false;
                            }


                            data = objReadCard.ReadBlock(60);
                           // alert(data);
                            data = objReadCard.ReadBlock(60).replace(/ /g, "");
                            //alert(data);
                            var CompanyCode = data.substr(0, 4);

                           // alert(CompanyCode);
                            //

                            var ServerFlag = false;
                            $.ajax({
                                url: "SACMasterCard.aspx/SetMasterKey",
                                type: "POST",
                                dataType: "json",
                                data: "{'MasterKey':'" + Masterkey + "','CompanyCode':'" + CompanyCode + "'}",
                                async: false,
                                contentType: "application/json; charset=utf-8",
                                success: function (msg) {
                                    //alert(msg.d);
                                    if (msg.d == "True") {
                                        ServerFlag = true;
                                    }
                                    else {
                                        ServerFlag = false;
                                    }
                                },
                                error: function () { alert(arguments[2]); }
                            });
                            if (ServerFlag == true) {
                                alert("Master Card Validation Complete.");
                            }
                            else {
                                alert("Cannot Validate Master Card");
                            }
                            document.getElementById("<%= txtMasterCardID.ClientID %>").value = "";
                            document.getElementById("<%= txtMasterCardPassword.ClientID %>").value = "";
                          // indow.location.href = "SACDOSCard.aspx";
                            return false;
                        }
            }
            catch (ex) {
                //alert(ex.Message);
                return false;
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
        function ResetMasterCard() {
            try {
                document.getElementById("<%= txtMasterCardID.ClientID %>").value = "";
                document.getElementById("<%= txtMasterCardPassword.ClientID %>").value = "";
                return false;
            }
            catch (ex) {
                alert(ex.Message);
                return false;
            }
        }

        function AddHex(val1, val2) {
            try {
                //alert(val1 + " " + val2);
                var result = "";
                if (val1.length == val2.length) {
                    for (var i = 0; i < val1.length; i = i + 2) {
                        var a = padZero(parseInt(val1.substr(i, 2), 16));
                        var b = padZero(parseInt(val2.substr(i, 2), 16));
                        var exor = "";

                        exor = a + b;
                        //alert("a : " + a + ", b : " + b + ", exor : " + exor);
                        result += (parseInt(exor, 10).toString(16)).toUpperCase();
                    }
                }
                return result;
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMasterCard" runat="server" Style="width: 100%; text-align: center;">
        <table style="margin: 0 auto;">
            <tr>
                <td colspan="2" class="heading">
                    Master Card Validation
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMasterCardID" runat="server" Text="UserID :"></asp:Label>
                    <label class="CompulsaryLabel">
                        *</label>
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtMasterCardID" runat="server" MaxLength="6" Width="75px" ValidationGroup="MasterCard"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtMasterCardID" runat="server" ErrorMessage="Please Enter master card ID"
                        ControlToValidate="txtMasterCardID" Display="None" ValidationGroup="MasterCard"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtMasterCardID" runat="server" TargetControlID="rfvtxtMasterCardID"
                        PopupPosition="Right" BehaviorID="behrfvtxtMasterCardID">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMasterCardPassword" runat="server" Text="Password :"></asp:Label>
                    <label class="CompulsaryLabel">
                        *</label>
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtMasterCardPassword" runat="server" MaxLength="6" TextMode="Password"
                        Width="75px" ValidationGroup="MasterCard"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtMasterCardPassword" runat="server" ErrorMessage="Please Enter Password"
                        ControlToValidate="txtMasterCardPassword" Display="None" ValidationGroup="MasterCard"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtMasterCardPassword" runat="server"
                        TargetControlID="rfvtxtMasterCardPassword" PopupPosition="Right">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Button ID="btnSubmitMaster" runat="server" Text="Submit" CssClass="ButtonControl"
                        ValidationGroup="MasterCard" OnClientClick="return ValidateMasterCard();" />
                    <asp:Button ID="btnCancelMaster" runat="server" Text="Reset" CssClass="ButtonControl"
                        OnClientClick="return ResetMasterCard();" />
                    <asp:HiddenField ID="hdnMasterKey" runat="server" />
                    <asp:HiddenField ID="hdnCompSiteCd" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
