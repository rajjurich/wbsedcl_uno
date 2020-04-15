<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="SACRawOLDCard.aspx.cs" Inherits="UNO.SACRawOLDCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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




           var DosKeyPerso = "";
           var Data = "";
           var MyKey = "";




           function RawCard() {
               debugger;
               try {
                   var MasterKey = "";
                   var DosKey = "";
                   var ServerFlag = false;
                   $.ajax({
                       url: "SACRawCard.aspx/GetMasterKey",
                       type: "POST",
                       dataType: "json",
                       data: "",
                       async: false,
                       contentType: "application/json; charset=utf-8",
                       success: function (msg) {
                           if (msg.d == "") {
                               alert("Master Key not found. Please Varify Master card first.");
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

                   var objReadCard;
                   var DataInitialize;
                   var cardCSNR = "";
                   var WriteData = "";
                   DosKey = DosKey.substring(2, (DosKey.length - 2));
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
                   //alert(cardCSNR);
                   var CMSCalulatedKey = XOR("0000" + cardCSNR, "434D53434D53");

                   /**************************************** Updating Database Start *********************************************/
                   var DatabaseUpdate = false;
                   $.ajax({
                       url: "SACRawCard.aspx/UpdateDatabase",
                       type: "POST",
                       dataType: "json",
                       data: "{'CSNR':'" + cardCSNR + "'}",
                       async: false,
                       contentType: "application/json; charset=utf-8",
                       success: function (msg) {
                           //alert(msg.d);
                           if (msg.d == "True") {
                               DatabaseUpdate = true;
                           }
                           else {
                               alert("Database Error - Cannot Reinitialised card. Try again later");
                               DatabaseUpdate = false;
                           }
                       },
                       error: function () { alert(arguments[2]); }
                   });
                   if (DatabaseUpdate == false) {
                       return false;
                   }

                   /**************************************** Updating Database End *********************************************/
                   /************************************** Erase Template Start ********************************************/
                   /* var Sector = 16;
                   for (var i = 0, j = 64; i < 4 && Sector < 28; i++, j++) {
                   if (i == 3) {
                   data = objReadCard.Authenticate(j, MasterKey, 96);
                   if (data != "") {
                   alert("Unable to Authenticate Block " + j);

                   $find("mpePersonaliseCard").hide();
                   return false;
                   }
                   var key = "FFFFFFFFFFFF" + "FF078069" + "FFFFFFFFFFFF";
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
                   WriteData = "0".rpad("0", 32);
                   //alert("Data Sector " + Sector + " Block " + j + " : " + strWriteData);
                   data = objReadCard.Authenticate(j, MasterKey, 96);
                   if (data != "") {
                   alert("Unable to Authenticate Block " + j);

                   $find("mpePersonaliseCard").hide();
                   return false;
                   }
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, parseInt(j, 10).toString(16).toUpperCase());
                   if (data != "") {
                   alert("Card Error: Unable to write in the card.Sector " + Sector + " Block " + j);
                   return false;
                   }
                   }
                   }
                   /************************************** Erase Template End ********************************************/

                   /****************************************Sector 0 Start *********************************************/
                   data = objReadCard.Authenticate(1, CMSCalulatedKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 1");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "01");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 0 Block 1");
                       return false;
                   }
                   data = objReadCard.Authenticate(2, CMSCalulatedKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 2");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "02");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 0 Block 02");
                       return false;
                   }
                   data = objReadCard.Authenticate(3, CMSCalulatedKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 3");
                       return false;
                   }
                   WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "03");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 0 Block 03");
                       return false;
                   }

                   /****************************************Sector 0 End *********************************************/
                   /****************************************Sector 1 Start *********************************************/
                   data = objReadCard.Authenticate(4, CMSCalulatedKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 4");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "04");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 1 Block 4");
                       return false;
                   }
                   data = objReadCard.Authenticate(5, CMSCalulatedKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 5");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "05");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 1 Block 5");
                       return false;
                   }
                   data = objReadCard.Authenticate(6, CMSCalulatedKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 6");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "06");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 1 Block 6");
                       return false;
                   }
                   data = objReadCard.Authenticate(7, CMSCalulatedKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 7");
                       return false;
                   }
                   WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "07");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 0 Block 7");
                       return false;
                   }
                   /****************************************Sector 1 End *********************************************/


                   /*  //****************Read Sec 14 block 0 for Time Attendance & Access Random No*********************

                   //Authenticate sec 14 with Master Key
                   var Rnd =  [];
                   Data = objReadCard.Authenticate(59, MasterKey, 96);
                   if (Data != "")
                   {
                   Data = objReadCard.Authenticate(59, "FFFFFFFFFFFF", 96);

                   if (Data != "")
                   {
                   alert("Authentication Error Sector 14 Block 59");
                 
                   return false;
                   }
                   }
                   else
                   {
                   var ReadData = objReadCard.ReadBlock(56);
                   var Stemp = "";
               
                   for (var i = 0; i < ReadData.length; i++)
                   {
                   if (ReadData.substring(i, 1) != "") {

                   Stemp += ReadData.substring(i, 1);
                   alert(Stemp);
                   }
                   }

                   Stemp = objReadCard.Convert(Stemp);
                   Rnd[3] = Stemp.substring(Stemp.length - 6, 6);
                   Rnd[2] = Stemp.substring(4, 6);
             
                   }
                   //****************End*************************************************************************




                   //**************************Raw Sector 2******************************************
                   var RandomNo = Rnd[2];
                   if (objReadCard.RndSecRaw(RandomNo, "08", "02") != true)
                   {
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "08");
                   if (data != "") {
                   alert("Card Error: Unable to write in the card.Sector 2 Block 8");
                   return false;



                   }
                   }

                   if (objReadCard.RndSecRaw(RandomNo, "08", "02") != true)
                   {
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "08");
                   if (data != "") {
                   alert("Card Error: Unable to write in the card.Sector 2 Block 8");
                   return false;



                   }
                   }
                   if (objReadCard.RndSecRaw(RandomNo, "09", "02") != true)
                   {
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "09");
                   if (data != "") {
                   alert("Card Error: Unable to write in the card.Sector 2 Block 9");
                   return false;
                   }



                   }

                   if (objReadCard.RndSecRaw(RandomNo, "10", "02") != true)
                   {
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "0A");
                   if (data != "") {
                   alert("Card Error: Unable to write in the card.Sector 2 Block 10");
                   return false;
                   }

                   }

                   if (objReadCard.RndSecRaw(RandomNo, "11", "02") != true)
                   {
                   WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "0B");
                   if (data != "") {
                   alert("Card Error: Unable to write in the card.Sector 2 Block 11");
                   return false;
                   }
                   }

              
            
                   //****************************End************************************************


            



                   /****************************************Sector 2 Start *********************************************/

                   //var Key2 = CalculateCMSKeyWithRandomNo("738740688017", "0000" + cardCSNR);

                   var Key2 = CalculateCMSKeyWithRandomNo("566216778359", "0000" + cardCSNR);
                   alert("key2 for tmh" + Key2);


                   data = objReadCard.Authenticate(11, Key2, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 11");
                       return false;
                   }
                   WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "0B");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 2 Block 11");
                       return false;
                   }


                   data = objReadCard.Authenticate(8, Key2, 96);
                   if (data != "") {

                       alert("Unable to Authenticate Block 8");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "08");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 2 Block 8");
                       return false;



                   }
                   data = objReadCard.Authenticate(9, Key2, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 9");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "09");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 2 Block 9");
                       return false;
                   }
                   data = objReadCard.Authenticate(10, Key2, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 10");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "0A");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 2 Block 10");
                       return false;
                   }

                   /****************************************Sector 2 End *********************************************/
                   /****************************************Sector 3 Start *********************************************/
                   var Key3 = CalculateCMSKeyWithRandomNo("738740688017", "0000" + cardCSNR);
                   data = objReadCard.Authenticate(12, Key3, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 12");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "0C");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 3 Block 12");
                       return false;
                   }
                   data = objReadCard.Authenticate(13, Key3, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 13");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "0D");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 3 Block 13");
                       return false;
                   }
                   data = objReadCard.Authenticate(14, Key3, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 14");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "0E");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 3 Block 14");
                       return false;
                   }
                   data = objReadCard.Authenticate(15, Key3, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 15");
                       return false;
                   }
                   WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "0F");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 3 Block 15");
                       return false;
                   }
                   /****************************************Sector 3 End *********************************************/
                   /****************************************Sector 13 Start *********************************************/
                   data = objReadCard.Authenticate(52, MasterKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 52 Please use correct mastercard");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "34");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 13 Block 52 Please use correct mastercard");
                       return false;
                   }
                   data = objReadCard.Authenticate(53, MasterKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 53");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "35");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 13 Block 53 Please use correct mastercard");
                       return false;
                   }
                   data = objReadCard.Authenticate(54, MasterKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 54");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "36");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 13 Block 54 Please use correct mastercard");
                       return false;
                   }
                   data = objReadCard.Authenticate(55, MasterKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 55");
                       return false;
                   }
                   WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "37");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 10 Block 55 Please use correct mastercard");
                       return false;
                   }
                   /****************************************Sector 13 End *********************************************/
                   /****************************************Sector 14 Start *********************************************/
                   data = objReadCard.Authenticate(56, MasterKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 56");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "38");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 14 Block 56 Please use correct mastercard");
                       return false;
                   }
                   data = objReadCard.Authenticate(57, MasterKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 57");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "39");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 14 Block 57 Please use correct mastercard");
                       return false;
                   }
                   data = objReadCard.Authenticate(58, MasterKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 58");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "3A");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 14 Block 58 Please use correct mastercard");
                       return false;
                   }
                   data = objReadCard.Authenticate(59, MasterKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 59");
                       return false;
                   }
                   WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "3B");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 14 Block 59 Please use correct mastercard");
                       return false;
                   }
                   /****************************************Sector 14 End *********************************************/
                   /****************************************Sector 15 Start *********************************************/
                   data = objReadCard.Authenticate(60, CMSCalulatedKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 60");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "3C");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 15 Block 60");
                       return false;
                   }
                   data = objReadCard.Authenticate(61, CMSCalulatedKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 61");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "3D");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 15 Block 61");
                       return false;
                   }
                   data = objReadCard.Authenticate(62, CMSCalulatedKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 62");
                       return false;
                   }
                   WriteData = PadBytes("00000000000000000000000000000000", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "3E");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 15 Block 62");
                       return false;
                   }
                   data = objReadCard.Authenticate(63, CMSCalulatedKey, 96);
                   if (data != "") {
                       alert("Unable to Authenticate Block 63");
                       return false;
                   }
                   WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, "3F");
                   if (data != "") {
                       alert("Card Error: Unable to write in the card.Sector 15 Block 63");
                       return false;
                   }
                   /****************************************Sector 15 End *********************************************/
                   /************************************** Erase Template Start ********************************************/
                   /* var Sector = 16;
                   for (var i = 0, j = 64; i < 4 && Sector < 28; i++, j++) {
                   if (i == 3) {
                   data = objReadCard.Authenticate(j, MasterKey, 96);
                   if (data != "") {
                   alert("Unable to Authenticate Block " + j);

                   $find("mpePersonaliseCard").hide();
                   return false;
                   }
                   var key = "FFFFFFFFFFFF" + "FF078069" + "FFFFFFFFFFFF";
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
                   WriteData = "0".rpad("0", 32);
                   //alert("Data Sector " + Sector + " Block " + j + " : " + strWriteData);
                   data = objReadCard.Authenticate(j, MasterKey, 96);
                   if (data != "") {
                   alert("Unable to Authenticate Block " + j);

                   $find("mpePersonaliseCard").hide();
                   return false;
                   }
                   WriteData = objReadCard.Convert(WriteData);
                   data = objReadCard.WriteData(WriteData, parseInt(j, 10).toString(16).toUpperCase());
                   if (data != "") {
                   alert("Card Error: Unable to write in the card.Sector " + Sector + " Block " + j);
                   return false;
                   }
                   }
                   }
                   /************************************** Erase Template End ********************************************/
                   alert("Card Reinitialised");
                   return false;
               }
               catch (ex) {
                   alert(ex.Message);
                   return false;
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
    <script language="javascript" type="text/javascript">

        function ValidateData() {
            if (!Page_ClientValidate())
                return;
        }

        
        function Reinitialize() {
            try {
                var MasterKey = "012345678901";
                var obj_Card;
                obj_Card = new ActiveXObject("ContactlessCardRW.Card");

                var Data = "";
                var Rnd = new Array();

                var DataInitialize;
                DataInitialize = obj_Card.Initialise();
                if (DataInitialize != "") {
                    alert("Omnikey reader not connected or Error in card Initialization");
                    return false;
                }

                Data = obj_Card.ConnectToCard();
                if (Data != "") {
                    alert("Error in connecting to card");
                    return false;
                }

                //*****************Read User Card Company Code & site id & Match with Master Card************

//                var MasterKey = document.getElementById('hdnMasterKey').value
//                var MasterDetails = document.getElementById('hdnCompSiteCd').value
//                var MasterCompCd = MasterDetails.substr(0, 4);
//                var MasterSiteId = MasterDetails.substr(4, 3);

//                var UserCardDetails = "";
//                UserCardDetails = obj_Card.ReadCMP(document.getElementById('hdnUserId').value, document.getElementById('hdnPassword').value);
//                var UserCompCd = UserCardDetails.substr(0, 4);
//                var UserSiteId = UserCardDetails.substr(4, 3);


//                if (MasterCompCd != UserCompCd && MasterSiteId != UserSiteId) {
//                    alert("Company code or site id does not match.Please Contact CMS Computers LTD");
//                    return false;
                //                }


                //                 else {
                //                     alert("Company code or site id match successfully");
                //                 }

                //*********end of reading User Card***************************************************************

                //*****************Read App Code from sec 0********************************************************
                Data = obj_Card.Authenticate(3, obj_Card.Cardrawkeybuf, 96);
                if (Data != "") {
          
                   Data = obj_Card.Authenticate(3, "FFFFFFFFFFFF", 96);
                   if (Data != "") {

                       
                        alert("Authentication Error Sector 0 Block 3");
                        return false;
                        //}
                    }
                }
                else {
                    var OldDataBlock1 = obj_Card.ReadBlock(1);
                    var OldDataBlock2 = obj_Card.ReadBlock(2);
                    // alert(OldDataBlock1);
                }
                //****************end of reading app code***********************************************************

                //****************Read Sec 14 block 0 for Time Attendance & Access Random No*********************

                //Authenticate sec 14 with Master Key
                Data = obj_Card.Authenticate(59, MasterKey, 96);
                if (Data != "") {
                    Data = obj_Card.Authenticate(59, "FFFFFFFFFFFF", 96);

                    if (Data != "") {
                        alert("Authentication Error Sector 14 Block 59");
                        return false;
                    }
                }
                else {
                    var ReadData = obj_Card.ReadBlock(56);
                    var Stemp = "";

                    for (var i = 0; i < ReadData.length; i++) {
                        if (ReadData.substr(i, 1) != " ") {
                            Stemp += ReadData.substr(i, 1);
                        }
                    }
                    //  alert(Stemp);
                    Stemp = obj_Card.Convert(Stemp);
                    Rnd[3] = Stemp.substr(Stemp.length - 6, 6);
                    Rnd[2] = Stemp.substr(4, 6);

                }
                //****************End*************************************************************************
                //***********below code for making sector 0 block 1,2 raw*********************************
                Data = obj_Card.Authenticate(2, obj_Card.Cardrawkeybuf, 96);
                if (Data != "") {
                    Data = obj_Card.Authenticate(2, "FFFFFFFFFFFF", 96);

                    if (Data != "") {
                        alert("Authentication Error Sector 0 Block 2");
                        return false;
                    }
                }
                else {
                    var WriteData = "00000000000000000000020002000200";
                    Data = obj_Card.WriteBlock(2, WriteData);
                    if (Data != "") {
                        alert("Making Sector Raw.Card Error,Sector 0 Block 2");
                        return false;
                    }
                    // alert("Making Sector Raw.Sector 0 Block 2" + Data);

                    var WriteData1 = "FF0F0000000000000000000000000000";
                    Data = obj_Card.WriteBlock(1, WriteData1);
                    if (Data != "") {
                        alert("Making Sector Raw.Card Error,Sector 0 Block 1");
                        return false;
                    }
                    //  alert("Making Sector Raw.Sector 0 Block 1" + Data);
                }
                //**********End***************************************************************
                //***********below code for making sector 1 raw*********************************

                Data = obj_Card.Authenticate(6, obj_Card.Cardrawkeybuf, 96);
                if (Data != "") {
                   Data = obj_Card.Authenticate(6, "FFFFFFFFFFFF", 96);
                   if (Data != "") {
                       alert("Authentication Error Sector 1 Block 6");
                       return false;
                   }
                }
                else {
                    var WriteData = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
                    if (obj_Card.WriteBlock(6, WriteData) != "") {
                        alert("Error in writting data to Sector 1 Block 6");
                        return false;
                    }
                }
                //  alert("writting data to Sector 1 Block 6" + Data);

                Data = obj_Card.Authenticate(7, obj_Card.Cardrawkeybuf, 96);
                if (Data != "") {
                    Data = obj_Card.Authenticate(7, "FFFFFFFFFFFF", 96);

                    if (Data != "") {
                        alert("Authentication Error Sector 1 Block 7");
                        return false;
                    }
                }
                else {
                    Data = obj_Card.MakeSectorRaw("7", "FFFFFFFFFFFF", "FFFFFFFFFFFF");
                    if (Data != "") {
                        alert("Making Sector Raw.Card Error,Sector 1 Block 7");
                        return false;
                    }
                    //     alert("Making Sector Raw.Sector 1 Block 7" + Data);
                }

                //**********************End************************************************    

                //**************************Raw Sector 2******************************************
                var RandomNo = Rnd[2];
                if (obj_Card.RndSecRaw(RandomNo, "08", "02") != true) {
                    alert("Making Sector Raw for Time Attendance. Card Error, Sector 2 Block 8");
                    return false;
                }

                if (obj_Card.RndSecRaw(RandomNo, "11", "02") != true) {
                    alert("Making Sector Raw for Time Attendance. Card Error, Sector 2 Block 11");
                    return false;
                }

                //*************************Raw Sector 3************************************************
                var RandomNo1 = Rnd[3];
                if (obj_Card.RndSecRaw(RandomNo1, "12", "03") != true) {
                    alert("Making Sector Raw for Time Attendance. Card Error, Sector 2 Block 8");
                    return false;
                }
                if (obj_Card.RndSecRaw(RandomNo1, "15", "03") != true) {
                    alert("Making Sector Raw for Access Control.Card Error,Sector 3 Block 15");
                    return false;
                }

                //****************************End************************************************

                //***********below code for making sector 13 raw*********************************
                Data = obj_Card.Authenticate(55, MasterKey, 96);
                if (Data != "") {
                    Data = obj_Card.Authenticate(55, "FFFFFFFFFFFF", 96);
                    if (Data != "") {
                        alert("Authentication Error Sector 13 Block 37");
                        return false;
                    }
                }
                else {
                    Data = obj_Card.MakeSectorRaw("37", "FFFFFFFFFFFF", "FFFFFFFFFFFF");
                    if (Data != "") {
                        alert("Making Sector Raw.Card Error,Sector 13 Block 55");
                        return false;
                    }
                    //   alert("Making Sector Raw.Sector 13 Block 55" + Data);
                }

                //**********************End************************************************

                //***********below code for making sector 14 raw*********************************

                Data = obj_Card.Authenticate(59, MasterKey, 96);
                if (Data != "") {
                    Data = obj_Card.Authenticate(59, "FFFFFFFFFFFF", 96);

                    if (Data != "") {
                        alert("Authentication Error Sector 14 Block 59");
                        return false;
                    }
                }
                else {
                    Data = obj_Card.MakeSectorRaw("3B", "FFFFFFFFFFFF", "FFFFFFFFFFFF");
                    if (Data != "") {
                        alert("Making Sector Raw.Card Error,Sector 14 Block 59");
                        return false;
                    }
                    // alert("Making Sector Raw.Sector 14 Block 59" + Data);
                }

                //**********************End************************************************          

                //**********************Raw Sector 15 block 60********************************
                Data = obj_Card.Authenticate(63, obj_Card.Cardrawkeybuf, 96);
                if (Data != "") {
                    Data = obj_Card.Authenticate(2, "FFFFFFFFFFFF", 96);

                    if (Data != "") {
                        alert("Authentication Error Sector 15 Block 60");
                        return false;
                    }
                }
                else {
                    var ReadData = obj_Card.ReadBlock(60);
                    var Stemp = "";

                    for (var i = 0; i < ReadData.length; i++) {
                        if (ReadData.substr(i, 1) != " ") {
                            Stemp += ReadData.substr(i, 1);
                        }
                    }

                    ReadData = Stemp;

                    //var Writedata = "000000000000000000000000000000" + ReadData.substr(ReadData.length - 2, 2);
                    var Writedata = "00000000000000000000000000000000";
                    // alert(Writedata);
                    Data = obj_Card.WriteBlock(60, Writedata);
                    if (Data != "") {
                        alert("Making Sector Raw.Card Error,Sector 15 Block 60");
                        return false;
                    }
                    //  alert("Making Sector Raw.Sector 15 Block 60" + Data);
                }
                RawCard();
                //alert("Card Reinitialized Successfully.");
                return true;
                //*************end************************************************************

            }
            catch (e) {
                alert(e.message);
                return false;
            }

        }
    </script>
 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlPersonalisation" runat="server" Style="width: 100%; text-align: center;">
        <table style="margin: 0 auto;">
            <tr>
                <td colspan="2" class="heading">
                    Please place card to reinitialise/disable and press OK
                </td>
            </tr>
             
            <tr>
             

                <td colspan="2" style="text-align: center;">
                    <asp:Button ID="btnSubmitPerso" runat="server" Text="OK" CssClass="ButtonControl"
                        OnClientClick="return Reinitialize();" ValidationGroup="CardPerso" 
                         />
                    <asp:Button ID="btnCancelPerso" runat="server" Text="Cancel" CssClass="ButtonControl"
                        OnClientClick="return false;" Style="display: none;" />
                </td>
               
            </tr>
             
        </table>
    </asp:Panel>
</asp:Content>
