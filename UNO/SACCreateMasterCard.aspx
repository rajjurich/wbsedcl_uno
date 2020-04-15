<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="SACCreateMasterCard.aspx.cs" Inherits="UNO.SACCreateMasterCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CreateMasterCard() {
            debugger;
            try {
                var MasterKey = document.getElementById("<%= txtMasterkey.ClientID %>").value;
                var UserId = document.getElementById("<%= txtUserID.ClientID %>").value;
                var Password = document.getElementById("<%= txtPassword.ClientID %>").value;
                var CompanyCode = document.getElementById("<%= txtCompanyCode.ClientID %>").value;

                if (UserId.trim() == "") {
                    alert("Please enter User ID");
                    return false;
                }
                if (Password.trim() == "") {
                    alert("Please enter Password");
                    return false;
                }
                if (CompanyCode.trim() == "") {
                    alert("Please enter Company Code");
                    return false;
                }
                if (MasterKey.trim() == "") {
                    alert("Please enter Master Key");
                    return false;
                }
                if (UserId.length <6) {
                    alert("UserID should be 6 Characters Long.");
                    return false;
                }
                if (Password.length <6) {
                    alert("Password should be 6 Characters Long.");
                    return false;
                }
                if (CompanyCode.length <4) {
                    alert("CompanyCode should be 4 Characters Long.");
                    return false;
                }
                if (MasterKey.length <12) {
                    alert("Master Key should be 12 Characters Long.");
                    return false;
                }

                //key generation by Manoj Logic
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
                var KeyB = "";
                var CompanyName = "CMSCMS";
               // var HexCSNR = ConvertStringToHex(cardCSNR);
                var lpadCSNR = "0000" + cardCSNR
                var HexCompanyName = ConvertStringToHex(CompanyName);
               // HexCompanyName =  "0000"+ HexCompanyName ;
                KeyB = XOR(lpadCSNR, HexCompanyName);

                var KeyA = "";
                var Seed = "SACHIN";

                var HexUserId = ConvertStringToHex(UserId);
                var HexSeed = ConvertStringToHex(Seed);
                //  alert(HexSeed);
                var tempBuff = AddHex(HexUserId, HexSeed);
                //alert(tempBuff);
                var HexPassword = ConvertStringToHex(Password);
                var HexCompanyCode = ConvertStringToHex(CompanyCode);
                KeyA = XOR(tempBuff, HexPassword);
                alert(KeyA);

                var WriteData = "";
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
                WriteData = MasterKey + "01010101010101010000";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(8, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 8");
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "08");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 8");
                    return false;
                }
                WriteData = KeyA + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(11, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 11");
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "0B");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 11");
                    return false;
                }
                // Added  by Aarti and Pooja

                /*CompanyCode*/
                // WriteData = CompanyCode + "000000000101010101010101004D";
                WriteData = CompanyCode + "000000000000000000000000004D";
                alert(WriteData);
               WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(60, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Sector 15 Block 60 ");
                    return false;
                }

                data = objReadCard.WriteData(WriteData, "3C");
                if (data != "") {
                    alert("Unable to Write in the card.Sector 15 Block 60");
                    return false;
                }

                WriteData = KeyB + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(63, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 63");
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "3F");
                if (data != "") {
                    alert("Card Error: Unable to write in the card. Sector 15 Block 63");
                    return false;
                }

                alert("Master Card Created Successfully.");
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

        function AddHex(val1, val2) {
            try {
               // alert(val1 + " " + val2);
                var result = "";
                if (val1.length == val2.length) {
                    for (var i = 0; i < val1.length; i = i + 2) {
                        var a = padZero(parseInt(val1.substr(i, 2), 16));
                        var b = padZero(parseInt(val2.substr(i, 2), 16));
                        var exor = "";
                        exor = a + b;
                       // alert("a : " + a + ", b : " + b + ", exor : " + exor);
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
    <center>
    <table>
        <tr>
            <td colspan="2" class="heading">
                Create Master Card
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblUserID" runat="server" Text="UserID :"></asp:Label>
                <label class="CompulsaryLabel">
                    *</label>
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtUserID" runat="server" ValidationGroup="Submit" MaxLength="6"></asp:TextBox>
                 <ajaxToolkit:FilteredTextBoxExtender ID="FTEtxtUserID" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters"
                                                            TargetControlID="txtUserID"/>
                <asp:RequiredFieldValidator ID="rfvtxtUserID" runat="server" ErrorMessage="UserID Required"
                    ControlToValidate="txtUserID" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtUserID" runat="server" TargetControlID="rfvtxtUserID"
                    PopupPosition="Right">
                </ajaxToolkit:ValidatorCalloutExtender>
                <asp:RegularExpressionValidator ID="revtxtUserID" runat="server" ErrorMessage="UserID should be 6 Characters Long."
                    ControlToValidate="txtUserID" ValidationGroup="Submit" ValidationExpression="^.{6,6}$"
                    Display="None" Enabled="true"></asp:RegularExpressionValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtUserID" runat="server" TargetControlID="revtxtUserID"
                    PopupPosition="Right">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblPassword" runat="server" Text="Password :"></asp:Label>
                <label class="CompulsaryLabel">
                    *</label>
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtPassword" runat="server" MaxLength="6"></asp:TextBox>
                 <ajaxToolkit:FilteredTextBoxExtender ID="FTtxtPassword" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters"
                                                            TargetControlID="txtPassword"/>
                <asp:RequiredFieldValidator ID="rfvtxtPassword" runat="server" ErrorMessage="Password Required"
                    ControlToValidate="txtPassword" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtPassword" runat="server" TargetControlID="rfvtxtPassword"
                    PopupPosition="Right">
                </ajaxToolkit:ValidatorCalloutExtender>
                <asp:RegularExpressionValidator ID="revtxtPassword" runat="server" ErrorMessage="Password should be 6 Characters Long."
                    ControlToValidate="txtPassword" ValidationGroup="Submit" ValidationExpression="^.{6,6}$"
                    Display="None" Enabled="true"></asp:RegularExpressionValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtPassword" runat="server" TargetControlID="revtxtPassword"
                    PopupPosition="Right">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
        </tr>

        <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblCompanyCode" runat="server" Text="CompanyCode :"></asp:Label>
                <label class="CompulsaryLabel">
                    *</label>
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtCompanyCode" runat="server" MaxLength="4"></asp:TextBox>
                  <ajaxToolkit:FilteredTextBoxExtender ID="FTEtxtCompanyCode" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtCompanyCode"/>
                <asp:RequiredFieldValidator ID="rfvtxtCompanyCode" runat="server" ErrorMessage="Company Code Required."
                    ControlToValidate="txtCompanyCode" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtCompanyCode" runat="server" TargetControlID="rfvtxtCompanyCode"
                    PopupPosition="Right">
                </ajaxToolkit:ValidatorCalloutExtender>
                <asp:RegularExpressionValidator ID="revtxtCompanyCode" runat="server" ErrorMessage="CompanyCode should be 4 Characters Long."
                    ControlToValidate="txtCompanyCode" ValidationGroup="Submit" ValidationExpression="^.{4,4}$"
                    Display="None" Enabled="true"></asp:RegularExpressionValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtCompanyCode" runat="server" TargetControlID="revtxtCompanyCode"
                    PopupPosition="Right">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblMasterkey" runat="server" Text="Master Key :"></asp:Label>
                <label class="CompulsaryLabel">
                    *</label>
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtMasterkey" runat="server" MaxLength="12" TextMode="Password"></asp:TextBox>
                     
                <asp:RequiredFieldValidator ID="rfvtxtMasterkey" runat="server" ErrorMessage="Master Key Required"
                    ControlToValidate="txtMasterkey" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMasterkey" FilterType="Custom" ValidChars="abcdefABCDEF0123456789" />
                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtMasterkey" runat="server" TargetControlID="rfvtxtMasterkey"
                    PopupPosition="Right">
                </ajaxToolkit:ValidatorCalloutExtender>


                <%--<asp:RegularExpressionValidator ID="revtxtMasterkey" runat="server" ErrorMessage="Master Key should be 12 Characters Long and A-F characters only."
                    ControlToValidate="txtMasterkey" ValidationGroup="Submit" ValidationExpression="^[a-fA-F0-9]+$"
                    Display="None" ></asp:RegularExpressionValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtMasterkey" runat="server" TargetControlID="revtxtMasterkey"
                    PopupPosition="Right">
                </ajaxToolkit:ValidatorCalloutExtender>--%>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSubmit" runat="server" Text="Save" ValidationGroup="Submit" CssClass="ButtonControl"
                    OnClientClick="CreateMasterCard();" />
            </td>
        </tr>
    </table>
    </center>
</asp:Content>
