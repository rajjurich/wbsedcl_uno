<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" CodeBehind="Personalization.aspx.cs" Inherits="UNO.Personalization" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script> 
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />   
    <link href="Styles/Modelpop.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
       

        function handleAdd() {
            if (!Page_ClientValidate())
                return;
        }
       
function __doPostBack(eventTarget,eventArgument ) {
    //alert(eventTarget);
    document.forms[0].__EVENTTARGET.value = eventTarget;
    document.forms[0].__EVENTARGUMENT.value = eventArgument;
    document.forms[0].submit();
}

function ABC() {
    __doPostBack('btnActicveX', '');
    
        }


        function ReadMasterKeyData() {
            try {                  
                 if (!Page_ClientValidate())
                      return;
                var objReadCard;
                var DataInitialize;
                   objReadCard = new ActiveXObject("ContactlessCardRW.Card");
                   DataInitialize = objReadCard.Initialise();                   
                   document.getElementById('hdnData').value = DataInitialize;
                   if (DataInitialize != "") {                     
                       alert("Omnikey reader not connected or Error in card Initialization");
                       return false;
                   }
                   var data;
                   data = objReadCard.ConnectToCard();                   
                   document.getElementById('hdnData').value = data;
                   if (data != "") {
                       alert("Error in connecting to card");
                       return false;
                   }

                   var Masterkey = objReadCard.ReadMasterKey(document.getElementById('txtuid').value, document.getElementById('txtpass').value);
                   if (Masterkey != "000000000000" && Masterkey != "" && Masterkey.length == 12) {
                       document.getElementById('hdnMasterKey').value = Masterkey;
                   }
                   else {
                       alert("Authentication Failed");
                       return false;
                   }

                   var CompanyCd = objReadCard.ReadCMP(document.getElementById('txtuid').value, document.getElementById('txtpass').value);
                   document.getElementById('hdnCompSiteCd').value = CompanyCd;

                   alert("Place User Card on reader");
                   return true;
                  
            }
            catch (e) {
                alert(e.message);
                data = e.Message;
                return false;
            }
        }

        function Personalization() {

            try {
                debugger;
                if (!Page_ClientValidate())
                    return;
                var DataInitialize;
                var WriteData;
                var ReadData;
                var objReadCard;
                objReadCard = new ActiveXObject("ContactlessCardRW.Card");

                var chkBoxList = document.getElementById("chkApplication");
                var chkBoxCount = chkBoxList.getElementsByTagName("input");
                if (chkBoxCount[0].checked == false && chkBoxCount[1].checked == false) {
                    alert("Please select Application");
                    return false;                
                }
                

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

                data = objReadCard.LoadKey(objReadCard.Cardrawkeybuf);
                // alert("LoadKey successful");
                if (data != "") {
                    alert("LoadKey error");
                    return false;
                }               

                //***********************'Writing Company Code Site id********************************************************

                var EmployeeCode = "";
                var strEmpcd = "";
                var MasterKey = document.getElementById('hdnMasterKey').value;
                var MasterDetails = document.getElementById('hdnCompSiteCd').value;               
                var MasterCompCd = MasterDetails.substr(0, 4);
                var MasterSiteId = MasterDetails.substr(4, 3);
                document.getElementById('hdnNewCSNR').value = objReadCard.CSNR;                
                strEmpcd = document.getElementById('txtEmpCd').value.substr(2, 8);

                for (var i = 0; i < strEmpcd.length; i++) {
                    EmployeeCode += 0 + strEmpcd.substr(i, 1);                 
                }
              


                //*******************Checking for personalized card************************************************************

                data = objReadCard.Authenticate(8, objReadCard.Cardrawkeybuf, 96);
                if (data != "") {
                    alert("Authentication Error Sector 2 Block 8");
                    return false;
                }

                ReadData = objReadCard.ReadTAEMP(MasterKey).substr(0, 8);
               

                if (ReadData != "" && ReadData != "00000000") {
                    alert("Card Is Already personalized or Card Error");
                    return false;
                }

                ReadData = "";
                ReadData = objReadCard.ReadACEMP(MasterKey).substr(0, 8);
              

                if (ReadData != "" && ReadData != "00000000") {
                    alert("Card Is Already personalized or Card Error");
                    return false;
                }


               
                data = objReadCard.Authenticate(63, objReadCard.Cardrawkeybuf, 96);
                if (data != "") {
                    data = objReadCard.Authenticate(63, "FFFFFFFFFFFF", 96);

                    if (data != "") {
                        alert("Authentication Error Sector 15 Block 63");
                        return false;
                    }
                }
                else {
                    data = objReadCard.WriteRawKey("3F");
               
                    if (data != "") {
                        alert("Card Error: Unable to write in the card.Sector 15 Block 63");
                        return false;
                    }


                    var SiteId = parseInt(MasterSiteId.lpad("0", 3));
                    var Compcd = MasterCompCd.lpad("0", 4);
                    var Adddata = "";
                    Adddata = Adddata.rpad("0", 26);                  
                    WriteData = objReadCard.Convert(Compcd) + objReadCard.Convert(SiteId.toString(16)) + objReadCard.Convert(Adddata);                 
                    data = objReadCard.WriteData(WriteData, "3C");
                   
                    if (data != "") {
                        alert("Card Error: Unable to write in the card.Sector 15 Block 60");
                        return false;
                    }
                }
              
                WriteData = "";
                data = objReadCard.Authenticate(60, objReadCard.Cardrawkeybuf, 96);
                if (data != "") {
                    data = objReadCard.Authenticate(60, "FFFFFFFFFFFF", 96);

                    if (data != "") {
                        alert("Authentication Error Sector 15 Block 60");
                        return false;
                    }
                }
                else {
                    ReadData = objReadCard.ReadBlock(60);
                    var Stemp = "";

                    for (var i = 0; i < ReadData.length; i++) {
                        if (ReadData.substr(i, 1) != " ") {
                            Stemp += ReadData.substr(i, 1);
                        }
                    }

                    ReadData = Stemp;
              

                    var ExpiryDt = document.getElementById('txtCardExpiryDate').value;
                    WriteData = ExpiryDt.replace(/\//g, "");

                    var ExpiryTime = document.getElementById('txtCardExpiryTime').value;
                    WriteData += ExpiryTime.replace(":", "");

            
                    var strBCDDate = "";
                    var strLRC = "";

                    strBCDDate = objReadCard.ConvertBCD(WriteData);

                    var intLRC = 0;
                    var utf8 = unescape(encodeURIComponent(strBCDDate));
                  
                    for (var i = 0; i < utf8.length; i++) {
                        var asciiByte = [];                        
                        var LRC = utf8.charCodeAt(i);
                        intLRC = intLRC + parseInt(LRC);
                    }
                 
                    strLRC = intLRC.toString(16);                   
                    strLRC = objReadCard.Convert(strLRC.substr(strLRC.length - 2, 2));
                    WriteData = objReadCard.Convert(ReadData.substr(0, 12)) + strBCDDate + strLRC + objReadCard.Convert(ReadData.substr(ReadData.length - 6, 6));
                   

                    data = objReadCard.WriteData(WriteData, "3C");

                    if (data != "") {
                        alert("Card Error: Unable to write in the card.Sector 15 Block 60");
                        return false;
                    }
                }
             

                //*****************Read App Code from sec 0********************************************************
                data = objReadCard.Authenticate(3, objReadCard.Cardrawkeybuf, 96);
                if (data != "") {
                    alert("Authentication Error Sector 0");
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
                    return false;
                }


                //*********************end***********************************************************************

                //*****'Following code writes the sector 1 (reallocated) with the card holders data and the random no***********

                data = objReadCard.Authenticate(6, objReadCard.Cardrawkeybuf, 96);
                if (data != "") {
                    data = objReadCard.Authenticate(6, "FFFFFFFFFFFF", 96);

                    if (data != "") {
                        alert("Authentication Error Sector 1 Block 6");
                        return false;
                    }
                }
                else {
                    data = objReadCard.WriteRawKey("7");
                    if (data != "") {
                        alert("Card Error: Unable to write in the card.Sector 1 Block 7");
                        return false;
                    }

                
                }

                var EmpName = document.getElementById('txtEmpName').value.rpad(" ", 32);            
                WriteData = "";
                WriteData = EmpName.substr(0, 16);               
                data = objReadCard.WriteData(WriteData, "04");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 4");
                    return false;
                }              
                WriteData = EmpName.substr(16, 16);
             
                data = objReadCard.WriteData(WriteData, "05");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 5");
                    return false;
                }
            

                var Gender = document.getElementById("ddlGender").value;
             
                if (Gender == "M") {
                    Gender = objReadCard.Convert("01");
                }
                else {
                    Gender = objReadCard.Convert("02");
                }

                var DOB = document.getElementById('txtDOB').value;
                DOB = DOB.replace(/\//g, "");
              

                WriteData = "";
                var AddString = "";
                AddString = AddString.rpad("F", 22);
                WriteData = objReadCard.Convert(DOB) + Gender + objReadCard.Convert(AddString);            
                data = objReadCard.WriteData(WriteData, "6");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 6");
                    return false;
                }
            
                //*****************End*******************************************************************************************
                //*********************'below code to lock sector 14 with master key*********************************************

                data = objReadCard.Authenticate(59, MasterKey, 96);
                if (data != "") {
                    alert("Authentication Error Sector 14 Block 59");
                    return false;
                }

                data = objReadCard.WriteMasterKey(MasterKey, "3B");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 14 Block 59");
                    return;
                }
                
                //***************************************end********************************************************************
                //*******below code to lock sector 13 with master key**********************************************
                data = objReadCard.Authenticate(55, MasterKey, 96);
                if (data != "") {
                    alert("Authentication Error Sector 13 Block 55");
                    return;
                }

                data = objReadCard.WriteMasterKey(MasterKey, "37");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 13 Block 55");
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
                        return false;
                    }
                  //  alert("Successfully written data in block 1 " + data);
                }
                //***********End***************************************************************************

                //*******************below code to write Access AppCode in block 0 sector 15**************
                data = objReadCard.Authenticate(60, objReadCard.Cardrawkeybuf, 96);
                if (data != "") {
                    data = objReadCard.Authenticate(60, "FFFFFFFFFFFF", 96);

                    if (data != "") {
                        alert("Authentication Error Sector 15 Block 60");
                        return false;
                    }
                }
                else {
                    ReadData = objReadCard.ReadBlock(60);
                    var Stemp = "";

                    for (var i = 0; i < ReadData.length; i++) {
                        if (ReadData.substr(i, 1) != " ") {
                            Stemp += ReadData.substr(i, 1);
                        }
                    }

                    ReadData = Stemp; 
                    WriteData = "";
                    WriteData = ReadData.substr(0, 26) + AppId[2] + ReadData.substr(ReadData.length - 2, 2);              
                    WriteData = objReadCard.Convert(WriteData);
                    data = objReadCard.WriteData(WriteData, "3C");
                    if (data != "") {
                        Message = "Card Error: Unable to write in the card.Sector 15 Block 60";
                        return false;
                    }

                 //   alert("Data successfully written in block 60 " + data);
                }
                //******************************End****************************************
                //*******************writting code for Time Attendance & Access Control******************************
                
                var chkBoxList = document.getElementById("chkApplication");
                var chkBoxCount = chkBoxList.getElementsByTagName("input");              
                if (chkBoxCount[0].checked) {
                    if (Time_Attd_Perso(MasterKey, EmployeeCode) != true) {
                        return false;
                    }

                }
                if (chkBoxCount[1].checked) {
                    if (Access_Sys_Perso(MasterKey, EmployeeCode) != true) {
                        return false;
                    }

                }

                 alert("Card Personalized Successfully.");           
            }
            catch (e) {
                alert(e.message);
                data = e.Message;
                return false;
            }
         }


         function Time_Attd_Perso(masterkey, empcode) {
             try {

                 var Data = "";
                 var WriteData = "";
                 var ReadData;
                 var key = "";
                 var RandomNo = "";
                 var Key_a_Buf = "";

                 var objReadCard;
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
              
                 data = objReadCard.LoadKey(objReadCard.Cardrawkeybuf);              
                 if (data != "") {
                     alert("LoadKey error");
                     return false;
                 }

                 RandomNo = objReadCard.CalculateRandomNo().toString();              
                 RandomNo = objReadCard.Convert(RandomNo); 
                 ReadData = objReadCard.ProgramRNDs(2, masterkey, RandomNo);
                 
                 
                 if (ReadData == "") {
                     alert("Card Error - Presonalizing for Time Attendance.");
                     return false;
                 }

                 Key_a_Buf = "FFFFFF";
                 key = objReadCard.CalculatetApplKey(parseInt(ReadData), RandomNo, Key_a_Buf);
                

                 if (key == "") {
                     alert("Card Error - Presonalizing for Time Attendance.");
                     return false;
                 }

                 Data = objReadCard.Authenticate(11, "FFFFFFFFFFFF", 96);

                 if (Data != "") {
                     alert("Authentication Error Sector 2 Block 11");
                     return false;
                 }
                 else {
                     WriteData = "";
                     WriteData = empcode.rpad("0", 32);                
                     WriteData = objReadCard.Convert(WriteData);                
                        Data = objReadCard.WriteData(WriteData,"08");
                        if (Data != "")
                        {
                            alert("Card Error - Presonalizing for Time Attendance.Sector 2 Block 8");
                            return false;
                        }

                     //writting calculated key in 11 block sec 2
                     WriteData = "";
                     WriteData = key;                 
                        Data = objReadCard.WriteData(WriteData,"B");
                        if (Data != "")
                        {
                            Message = "Card Error - Presonalizing for Time Attendance.Sector 2 Block 11";                         
                            return false;
                        }

                     WriteData = "";
                     WriteData = WriteData.rpad("0", 32);
                     WriteData = objReadCard.Convert(WriteData);                
                        Data = objReadCard.WriteData(WriteData,"9");
                        if (Data != "")
                        {
                            alert("Card Error - Presonalizing for Time Attendance.Sector 2 Block 9");                       
                            return false;
                        }
                 }
                 return true;
             }
             catch (e) {
                 alert(e.message);
                 data = e.Message;
                 return false;
             }
        }
      
        function Access_Sys_Perso(masterkey, empcode) {
            try {

                //alert("In Access_Sys_Perso masterkey - " + masterkey + " empcd - " + empcode);

                var Data = "";
                var WriteData = "";
                var ReadData;
                var key = "";
                var RandomNo;
                var Key_a_Buf = "";

                var objReadCard;
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

                
                data = objReadCard.LoadKey(objReadCard.Cardrawkeybuf);
                // alert("LoadKey successful");
                if (data != "") {
                    alert("LoadKey error");
                    return false;
                }

                RandomNo = objReadCard.CalculateRandomNo().toString();
                RandomNo = objReadCard.Convert(RandomNo);

                ReadData = objReadCard.ProgramRNDs(3, masterkey, RandomNo);
                if (ReadData == "") {
                    alert("Card Error - Presonalizing for Access Control.");
                    return false;
                }

                Key_a_Buf = "FFFFFF";
                key = objReadCard.CalculatetApplKey(parseInt(ReadData), RandomNo, Key_a_Buf);
             
                if (key == "") {
                    alert("Card Error - Presonalizing for Access Control.");
                    return false;
                }

                Data = objReadCard.Authenticate(15, "FFFFFFFFFFFF", 96);

                if (Data != "") {
                    alert("Authentication Error Sector 3 Block 15");
                    return false;
                }

                else {
                    WriteData = "";
                    WriteData = empcode.rpad("0", 32);                
                    WriteData = objReadCard.Convert(WriteData);                
                    Data = objReadCard.WriteData(WriteData, "C");
                    if (Data != "") {
                        alert("Card Error - Presonalizing for Access Control.Sector 3 Block 12");
                        return false;
                    }

                    WriteData = "";
                    WriteData = WriteData.rpad("0",32);
                    WriteData = objReadCard.Convert(WriteData);
                    Data = objReadCard.WriteData(WriteData, "D");
                    if (Data != "") {
                        alert("Card Error - Presonalizing for Access Control.Sector 3 Block 13");
                        return false;
                    }

                    WriteData = "";
                    WriteData = WriteData.rpad("0",32);
                    WriteData = objReadCard.Convert(WriteData);
                    Data = objReadCard.WriteData(WriteData, "E");
                    if (Data != "") {
                        alert("Card Error - Presonalizing for Access Control.Sector 3 Block 14");
                        return false;
                    }

                    //writting calculated key in 15 block sec 3
                    WriteData = "";
                    WriteData = key;

                    Data = objReadCard.WriteData(WriteData, "F");
                    if (Data != "") {
                        alert("Card Error - Presonalizing for Time Attendance.Sector 3 Block 15");
                        return false;
                    }
                }
                return true;
            }
            catch (e) {
                alert(e.message);
                data = e.Message;
                return false;
            }
      }

      
              //pads left
        String.prototype.lpad = function(padString, length) {
	        var str = this;
            while (str.length < length)
                str = padString + str;
            return str;
        }
 
        //pads right
        String.prototype.rpad = function(padString, length) {
	        var str = this;
            while (str.length < length)
                str = str + padString;
            return str;
        }

        function Assignvalue() {
            document.getElementById('hdnDuplicate').value = "Duplicate";
        }
    </script>
<object id="Card"
classid ="CLSID:E421E41E-100C-48AB-8CC7-5B6B78DC1C76"
codebase="ContactlessCardRWPerso.CAB#version=1,0,0,0">
</object>
 <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"
        CombineScripts="false">
 </ajaxToolkit:ToolkitScriptManager>--%>
<table id="table1" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0" class="TableClass">
<tr>
<td align="right" style = "width:33%" class="LinkControl">	
<%--<asp:HyperLink ID ="Back_to_Dashboard" CssClass="LinkControl" runat="server" 
        NavigateUrl="~/CardDashboard.aspx" ForeColor="Blue">Back to Card Data DashBoard</asp:HyperLink>--%>
</td>
</tr>
</table>
<table id="table2" runat="server" width="100%" height="10%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td>
<h3 class="heading">Personalize Card</h3>
</td>
</tr>
</table>

<%--<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>--%>

<asp:Panel ID="pnlCardDetails" ClientIDMode="Static" runat="server" Width="95%" Height="150px" BorderWidth="1" BorderColor="Gray" HorizontalAlign="Center" align="Center" CssClass="srcColor" >
<asp:Label ID="Label1" runat = "server" Text = "Master Card Details" Font-Bold="true" Font-Size="Medium" ></asp:Label>

<table id="table5" runat="server" width="100%" >
    <caption>
        <br />
        <tr>
            <td class="TDClassLabel">
                User ID :
            </td>
            <td class="TDClassControl">
                <asp:TextBox ID="txtuid" runat="server" ClientIDMode="Static" 
                    CssClass="TextControl" MaxLength="6" 
                    onkeypress="return IsAlphanumericWithoutspace(event)"></asp:TextBox>
                <br/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtuid" Display="Dynamic" 
                    ErrorMessage="Please enter User ID" ForeColor="Red" SetFocusOnError="True" 
                    ValidationGroup="validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegExp1" runat="server" 
                    ControlToValidate="txtuid" ErrorMessage="User Id length must be 6 characters" 
                    ValidationExpression="^[a-zA-Z0-9\s]{6}$">
        </asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="TDClassLabel">
                Password :
            </td>
            <td class="TDClassControl">
                <asp:TextBox ID="txtpass" runat="server" ClientIDMode="Static" 
                    CssClass="TextControl" MaxLength="6" 
                    onkeypress="return IsAlphanumericWithoutspace(event)" TextMode="Password"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtpass" Display="Dynamic" 
                    ErrorMessage="Please enter Password" ForeColor="Red" SetFocusOnError="True" 
                    ValidationGroup="validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                    ControlToValidate="txtpass" ErrorMessage="Password length must be 6 characters" 
                    ValidationExpression="^[a-zA-Z0-9\s]{6}$">
        </asp:RegularExpressionValidator>
            </td>
        </tr>
    </caption>
</table> 
<table id="tbl" runat="server" width="100%" >
    <tr>
    <td style="width:40%"></td>   
    <td>
    <asp:Button ID="btnAccept" runat="server" ClientIDMode="Static" Text="OK"  Width = "90%"
            CssClass="ButtonControl" OnClientClick="return ReadMasterKeyData()"  OnClick="btnAccept_Click"/>
    </td>
    <td>
    <asp:Button ID="btnCancel" runat="server" ClientIDMode="Static" Text="Cancel" Width = "90%"
             CssClass="ButtonControl" onclick="btnCancel_Click" />  

    </td>  
    <td style="width:30%"></td>   
    </tr>
    </table>     
</asp:Panel>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>

<asp:Panel ID="pnlPersoCard" runat="server" ScrollBars="None" Width="95%" Height="250px" BorderWidth="1" BorderColor="Gray" HorizontalAlign="Center" align="Center"
        CssClass="srcColor" ClientIDMode="Static">
<table id="table3" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0" class="TableClass">
    <caption>
        <br />
        <tr>
            <td class="TDClassLabel" style="height: 10px">
                Employee Code :
            </td>
            <td class="TDClassControl" colspan="2" style="height: 10px">
                <asp:TextBox ID="txtEmpCd" runat="server" ClientIDMode="Static" 
                    CssClass="TextControl" MaxLength="10" onblur="__doPostBack('Button2','')"
                    Width="30%" TabIndex="1"></asp:TextBox>
                <br/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtEmpCd" Display="Dynamic" 
                    ErrorMessage="Please enter Employee Code" ForeColor="Red" 
                    SetFocusOnError="True" ValidationGroup="validate"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="TDClassLabel" style="height: 10px">
                Employee Name :
            </td>
            <td class="TDClassControl" colspan="2" style="height: 10px">
                <asp:TextBox ID="txtEmpName" runat="server" ClientIDMode="Static" 
                    CssClass="TextControl" Width="30%" TabIndex="2" ReadOnly = "true"></asp:TextBox>              
            </td>
        </tr>
        <tr>
            <td class="TDClassLabel" style="height: 10px">
                Date Of Birth :
            </td>
            <td class="TDClassControl" colspan="2" style="height: 10px">
                <asp:TextBox ID="txtDOB" runat="server" ClientIDMode="Static" 
                    CssClass="TextControl" Width="30%" TabIndex="3" ReadOnly = "true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="TDClassLabel" style="height: 10px">
                Gender :
            </td>
            <td class="TDClassControl" colspan="2" style="height: 10px">
                <asp:DropDownList ID="ddlGender" runat="server" AutoPostBack="true" 
                    ClientIDMode="Static" CssClass="ComboControl" Width="30%" TabIndex="4" 
                    ReadOnly = "true" Enabled="False">
                    <asp:ListItem Value="M">Male</asp:ListItem>
                    <asp:ListItem Value="F">Female</asp:ListItem>
                </asp:DropDownList>
                <%-- <br/>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
            ErrorMessage="Please select Gender" ControlToValidate="ddlGender" 
            SetFocusOnError="True" Display="Dynamic" ForeColor="Red" InitialValue="Select"
            ValidationGroup="validate"></asp:RequiredFieldValidator>  --%>
            </td>
        </tr>
        <tr>
            <td class="TDClassLabel" style="height: 10px">
                Card Expiry Date :
            </td>
            <td class="TDClassControl" style="height: 10px">
                <asp:TextBox ID="txtCardExpiryDate" runat="server" ClientIDMode="Static" 
                    CssClass="TextControl" MaxLength="10" onkeydown="date_dash(this,event)" 
                    onkeypress="return IsNumber(event)" onkeyup="date_dash(this,event)" 
                    Width="30%" TabIndex="5"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvDate" runat="server" 
                    ControlToValidate="txtCardExpiryDate" Display="Dynamic" 
                    ErrorMessage="Please enter Card Expiry date." ForeColor="Red" 
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtCardExpiryDate" Display="Dynamic" 
                    ErrorMessage="Please enter valid date" ForeColor="Red" SetFocusOnError="true" 
                    ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="TDClassLabel" style="height: 10px">
                Card Expiry Time :
            </td>
            <td class="TDClassControl" colspan="2" style="height: 10px">
                <asp:TextBox ID="txtCardExpiryTime" runat="server" ClientIDMode="Static" 
                    CssClass="TextControl" MaxLength="5" onkeydown="Time_Colon(this,event)" 
                    onkeypress="return IsNumber(event)" onkeyup="Time_Colon(this,event)" 
                    Width="30%" TabIndex="2"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtCardExpiryTime" Display="Dynamic" 
                    ErrorMessage="Please enter Card Expiry Time." ForeColor="Red" 
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ControlToValidate="txtCardExpiryTime" Display="Dynamic" 
                    ErrorMessage="Please enter valid time" ForeColor="Red" SetFocusOnError="true" 
                    ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$"></asp:RegularExpressionValidator>
                <%--<asp:RegularExpressionValidator  ValidationExpression="[0-2][0-3]:[0-5][0-9]"
            ID="ExpiryTime" runat="server" ForeColor="Red" ErrorMessage="Invalid Time" 
            ControlToValidate="txtCardExpiryTime" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>--%>
            </td>
        </tr>
        <tr>
            <td class="TDClassLabel" style="height: 10px">
                Application :
            </td>
            <td class="TDClassControl" colspan="2" style="height: 10px">
                <asp:CheckBoxList ID="chkApplication" runat="server" BorderColor="Gray" 
                    BorderWidth="1" ClientIDMode="Static" Width="30%" TabIndex="3">
                    <asp:ListItem> Time Attendance </asp:ListItem>
                    <asp:ListItem> Access Control </asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
    </caption>
    </table>
    <br />
    <table id="table4" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0" class="TableClass">
    <tr>
    <td class = "TDClassLabel" style="height: 10px;width:40% "></td>
    <td >
		<asp:Button ID="btnOk" runat="server" ClientIDMode="Static" Text="Ok" Width="95%"
            CssClass="ButtonControl" OnClientClick="return Personalization()" 
            OnClick="btnOk_Click" TabIndex="4" />
          
     </td>
      <td >
		<asp:Button ID="btnExit" runat="server" ClientIDMode="Static" Text="Cancel" Width="95%"
            CssClass="ButtonControl" onclick="btnExit_Click" TabIndex="5" />
          
     </td>
      <td class = "TDClassLabel" style="height: 10px;width:30% "></td>
     </tr>
    </table>
   <%--<input id="Button1" type="button" value="button" style="display:none" runat="server" clientidmode="Static" onserverclick="Button1_ServerClick" />--%>
   <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Button" ClientIDMode="Static" style="display:none" />
   
   <%--<input type ="hidden" name ="__EVENTTARGET" value ="">
   <input type ="hidden" name ="__EVENTARGUMENT" value =""> --%>
    
  
</asp:Panel>
  <asp:Button ID="btntest" runat="server" Text="Button" ClientIDMode="Static" style="display:none" />
  
 <ajaxtoolkit:modalpopupextender id="ModalPopupExtender" runat="server" backgroundcssclass="modalBackground"
            cancelcontrolid="CancelButton" dropshadow="true" 
                 popupcontrolid="Panel1" popupdraghandlecontrolid="Panel3" targetcontrolid="btntest"></ajaxtoolkit:modalpopupextender>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="display:none"   Width="400px" Height="150px">
            <div>
                <p style="text-align: center">
                <table align = "center">                                           
                            A card has already been issued for this employee.Do you wish to issue a Duplicate card ?If Yes then select previous card status & click ok button
                                
                        <tr>
                            <td class="TDClassLabel" style= "width:150px">
                                Select Status Of the Previous Card:<label class="CompulsaryLabel">*</label></td>
                            <td class="TDClassLabel" style= "width:150px">
                                <asp:DropDownList ID="ddlCardStatus" ClientIDMode="Static" runat="server" 
                                  Width="100%"  CssClass="ComboControl">     
                                <%--<asp:ListItem > Select One </asp:ListItem>--%>
                                <asp:ListItem Value="1"> Lost Card </asp:ListItem>
                                <asp:ListItem Value="2"> Damage Card </asp:ListItem>
                                <asp:ListItem Value="3"> Modify Card </asp:ListItem>
                                </asp:DropDownList>
                                
                                 <br />
                        <asp:RequiredFieldValidator ID="rfv1" runat="server" 
                                ErrorMessage="Please select previous card status." ControlToValidate="ddlCardStatus" 
                                SetFocusOnError="True" Display="Dynamic" InitialValue = "Select One" ForeColor="Red" >
                        </asp:RequiredFieldValidator>
                            </td>
                        </tr>                        
                    
                </table>
                    <asp:Button ID="OkButton" CssClass="ButtonControl" runat="server"  Text="OK" 
                     Width = "20%" onclick="OkButton_Click" OnClientClick="Assignvalue()"/>    
                     <asp:Button ID="CancelButton" CssClass="ButtonControl" runat="server" Text="Cancel"  Width = "20%" />                
                </p>
            </div>
        </asp:Panel>

</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server">
   <ProgressTemplate>       
       Personalizing Card please wait ............
   </ProgressTemplate>
</asp:UpdateProgress>

    <asp:HiddenField ID="hdnCSNR" ClientIDMode="Static" runat="server" />    
    <asp:HiddenField ID="hdnCardNum" ClientIDMode="Static" runat="server"/>     
    <asp:HiddenField ID="hdnPerDate" ClientIDMode="Static" runat="server"/>     
    <asp:HiddenField ID="hdnMasterKey" ClientIDMode="Static" runat="server"/>   
    <asp:HiddenField ID="hdnCompSiteCd" ClientIDMode="Static" runat="server"/>   
    <asp:HiddenField ID="hdnData" ClientIDMode="Static" runat="server"/>   
    <asp:HiddenField ID="hdnNewCSNR" ClientIDMode="Static" runat="server"/>   
    <asp:HiddenField ID="hdnDuplicate" ClientIDMode="Static" runat="server"/>  
    <%--<input type="text" id="txtdata" style="width:400px" />
    <input type="text" id="txtTAkey" style="width:400px" />
    <input type="text" id="txtACkey" style="width:400px" />--%>
</asp:Content>
