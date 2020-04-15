<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" CodeBehind="ViewCard.aspx.cs" Inherits="UNO.ViewCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <link href="Styles/Style.css" rel="stylesheet" type="text/css" />   
   <script language="javascript" type="text/javascript" src="Scripts/Validation1.js"></script>  
   <script language="javascript" type="text/javascript">       
//       function Confirm() {           
//           if (!Page_ClientValidate())
//               return;
       //       }
       window.onload = function () {
           alert("Place Master Card on reader");
           //document.getElementById('pnlViewCard').style.display = "none";
           //document.getElementById('pnlCardDetails').style.display = "block"; 
        };

       function ReadMasterKeyData() {
           try {
             //  alert("ReadMasterKeyData");

               if (!Page_ClientValidate())
                   return;

               var objReadCard;
               objReadCard = new ActiveXObject("ContactlessCardRW.Card");             

               var DataInitialize;
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

               document.getElementById('hdnUserId').value = document.getElementById('txtuid').value
               document.getElementById('hdnPassword').value = document.getElementById('txtpass').value

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

              // alert("Place User Card on reader");
//               return true;
               var r = confirm("Place User Card on reader");
                           if (r == true) {
                               if (ViewCard() != true) {
                                   return false;
                                }                            
                               }
                           else {
                                  return false;
                              }

                              document.getElementById('pnlViewCard').style.display = "inline";
                              document.getElementById('pnlCardDetails').style.display = "none";
                              return true;             

           }
           catch (e) {
               alert(e.message);
               return false;
           }
       }

       function ViewCard() {
           debugger;
           try {
               var obj_Card;
               obj_Card = new ActiveXObject("ContactlessCardRW.Card");

               var Data = "";
               var TimeAttendance = false;
               var AccessControl = false;
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

               var MasterKey = document.getElementById('hdnMasterKey').value
               var MasterDetails = document.getElementById('hdnCompSiteCd').value
               var MasterCompCd = MasterDetails.substr(0, 4);
               var MasterSiteId = MasterDetails.substr(4, 3);

               var UserCardDetails = "";
               UserCardDetails = obj_Card.ReadCMP(document.getElementById('hdnUserId').value, document.getElementById('hdnPassword').value);
               var UserCompCd = UserCardDetails.substr(0, 4);
               var UserSiteId = UserCardDetails.substr(4, 3);


               if (MasterCompCd != UserCompCd && MasterSiteId != UserSiteId) {
                   alert("Company code or site id does not match.Please Contact CMS Computers LTD");
                   return false;
               }
//               else {
//                   alert("Company code or site id match successfully");
//               }

               //*********end of reading User Card***************************************************************
               //***********************Reading Expiry Date & Time from Sector 15 Block 60************************

               Data = obj_Card.Authenticate(63, obj_Card.Cardrawkeybuf, 96);

               var ReadData = obj_Card.ReadBlock(60);
               var temp = "";
               for (var i = 0; i < ReadData.length; i++) {
                   if (ReadData.substr(i, 1) != " ") {
                       temp += ReadData.substr(i, 1);
                   }
               }
               ReadData = temp;               
               document.getElementById("txtCardExpiryDate").value = ReadData.substr(12, 2) + "/" + ReadData.substr(14, 2) + "/" + ReadData.substr(16, 4);               
               document.getElementById('txtCardExpiryTime').value = ReadData.substr(20, 2) + ":" + ReadData.substr(22, 2);

               //***********************End*******************************************************************

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
                   var ReadData1 = obj_Card.ReadBlock(56);
                   var Stemp = "";

                   for (var i = 0; i < ReadData1.length; i++) {
                       if (ReadData1.substr(i, 1) != " ") {
                           Stemp += ReadData1.substr(i, 1);
                       }
                   }

                   ReadData1 = obj_Card.Convert(Stemp);
                   Rnd[3] = ReadData1.substr(ReadData1.length - 6, 6);
                   Rnd[2] = ReadData1.substr(4, 6);
                 //  alert(ReadData1);
                   //****************End*************************************************************************

                   //***********Reading Sector 1 block 4 ********************************************************
                   Data = obj_Card.Authenticate(7, obj_Card.Cardrawkeybuf, 96);
                   if (Data != "") {
                       Data = obj_Card.Authenticate(7, "FFFFFFFFFFFF", 96);

                       if (Data != "") {
                           alert("Authentication Error Sector 1 Block 4");
                           return false;
                       }
                   }
                   else {
                       var ReadData2 = obj_Card.ReadBlock(4);
                       var Stemp1 = "";

                       for (var i = 0; i < ReadData2.length; i++) {
                           if (ReadData2.substr(i, 1) != " ") {
                               Stemp1 += ReadData2.substr(i, 1);
                           }
                       }

                       ReadData2 = obj_Card.Convert(Stemp1);
                       document.getElementById('txtEmpName').value = ReadData2.trim();

                       ReadData2 = "";
                       ReadData2 = obj_Card.ReadBlock(5);
                       Stemp1 = "";

                       for (var i = 0; i < ReadData2.length; i++) {
                           if (ReadData2.substr(i, 1) != " ") {
                               Stemp1 += ReadData2.substr(i, 1);
                           }
                       }

                       ReadData2 = obj_Card.Convert(Stemp1);
                       document.getElementById('txtEmpName').value = document.getElementById('txtEmpName').value + ReadData2.trim();

                       ReadData2 = "";
                       ReadData2 = obj_Card.ReadBlock(6);
                       Stemp1 = "";

                       for (var i = 0; i < ReadData2.length; i++) {
                           if (ReadData2.substr(i, 1) != " ") {
                               Stemp1 += ReadData2.substr(i, 1);
                           }
                       }

                       ReadData2 = Stemp1;

                       var Gender = ReadData2.substr(8, 2);

                       if (Gender == "01") {
                           document.getElementById('txtGender').value = "Male";
                       }
                       else {
                           document.getElementById('txtGender').value = "Female";
                       }

                       var DOB = ReadData2.substr(0, 8);

                       document.getElementById('txtDOB').value = DOB.substr(0, 2) + "/" + DOB.substr(2, 2) + "/" + DOB.substr(4, 4);

                   }

                   //**********End******************************************************************************

                   //*****************Read App Code from sec 0********************************************************
                   Data = obj_Card.Authenticate(3, obj_Card.Cardrawkeybuf, 96);
                   if (Data != "") {
                       alert("Authentication Error Sector 0");
                       return false;
                   }
                   else {
                       var OldDataBlock1 = obj_Card.ReadBlock(1);
                       var OldDataBlock2 = obj_Card.ReadBlock(2);

                       var Stemp1 = "";

                       for (var i = 0; i < OldDataBlock1.length; i++) {
                           if (OldDataBlock1.substr(i, 1) != " ") {
                               Stemp1 += OldDataBlock1.substr(i, 1);
                           }
                       }

                       OldDataBlock1 = Stemp1;

                       var Stemp2 = "";

                       for (var i = 0; i < OldDataBlock2.length; i++) {
                           if (OldDataBlock2.substr(i, 1) != " ") {
                               Stemp2 += OldDataBlock2.substr(i, 1);
                           }
                       }

                       OldDataBlock2 = Stemp2;

                       if (OldDataBlock1.substr(8, 4) == "0148") {
                           TimeAttendance = true;
                       }
                       if (OldDataBlock1.substr(12, 4) == "0248") {
                           AccessControl = true;
                       }
                       if (OldDataBlock2.substr(8, 4) == "0148") {
                           TimeAttendance = true;
                       }
                       if (OldDataBlock2.substr(12, 4) == "0248") {
                           AccessControl = true;
                       }

                       var chkBoxList = document.getElementById("chkApplication");
                       var chkBoxCount = chkBoxList.getElementsByTagName("input");

                       if (TimeAttendance == true) {
                           chkBoxCount[0].checked = true;
                           //document.getElementById("lstApplication").value = "Time Attendance";
                          // lstApplication.Items.Add("Time Attendance");
                           document.getElementById('txtEmpCd').value = obj_Card.ReadTAEMP(MasterKey);
                       }


                       if (AccessControl == true) {
                           chkBoxCount[1].checked = true;
                          // document.getElementById("lstApplication").value = document.getElementById("lstApplication").value + ", Access Control";
                          // lstApplication.Items.Add("Access Control");
                           document.getElementById('txtEmpCd').value = obj_Card.ReadACEMP(MasterKey);
                       }
                       if (document.getElementById('txtEmpCd').value == "") {
                           alert("Card is not personalized");
                           document.getElementById('pnlViewCard').style.display = "none";
                           document.getElementById('pnlCardDetails').style.display = "block";                        
                           return false;
                       }
                   }
                   //****************end of reading app code***********************************************************
                   //                pnlViewCard.Visible = true;
               document.getElementById('pnlViewCard').style.display = "block";
               document.getElementById('pnlCardDetails').style.display = "none";
               return true;             
               }
           }
           catch (e) {
               alert(e.message);
               return false;
           }

       }

       function btnOk_click() {
           var r = confirm("Do you want to view another card ?");
           if (r == true) {
               if (ViewCard() != true) {
                   return false;
               }
           }
           else {
               window.location.assign("CardDashboard.aspx")
               return true;
           }
       }
    </script>
<object id="Card"
classid ="CLSID:E421E41E-100C-48AB-8CC7-5B6B78DC1C76"
codebase="ContactlessCardRWPerso.CAB#version=1,0,0,0">
</object>
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
<h3 class="heading">Card Details</h3>
</td>
</tr>
</table> 

<%--<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>--%>


<asp:Panel ID="pnlCardDetails" ClientIDMode="Static" runat="server" Width="95%" Height="150px" BorderWidth="1" BorderColor="Gray" HorizontalAlign="Center" align="Center" CssClass="srcColor" >
<asp:Label ID="Label1" runat = "server" Text = "Master Card Details" Font-Bold="true" Font-Size="Medium" ></asp:Label>

<table id="table5" runat="server" width="100%" >
<tr>
<td class = "TDClassLabel" >User ID :  </td>
	<td class = "TDClassControl" >
		<asp:TextBox CssClass ="TextControl" ClientIDMode="Static" id="txtuid" MaxLength="6"   runat="server" onkeypress="return IsAlphanumericWithoutspace(event)"></asp:TextBox>
        <br/>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ErrorMessage="Please enter User ID" ControlToValidate="txtuid" 
            SetFocusOnError="True" Display="Dynamic" ForeColor="Red" 
            ValidationGroup="validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegExp1" runat="server"    
            ErrorMessage="User Id length must be 6 characters"
            ControlToValidate="txtuid" ValidationExpression="^[a-zA-Z0-9\s]{6}$">
        </asp:RegularExpressionValidator>
	</td>
</tr>
<tr>
<td class = "TDClassLabel" >Password :  </td>
	<td class = "TDClassControl" >
		<asp:TextBox CssClass ="TextControl" ClientIDMode="Static" id="txtpass" TextMode="Password"   MaxLength="6"  runat="server" onkeypress="return IsAlphanumericWithoutspace(event)"></asp:TextBox>        
        <br />
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="Please enter Password" ControlToValidate="txtpass" 
            SetFocusOnError="True" Display="Dynamic" ForeColor="Red" 
            ValidationGroup="validate"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"    
            ErrorMessage="Password length must be 6 characters"
            ControlToValidate="txtpass" ValidationExpression="^[a-zA-Z0-9\s]{6}$">
        </asp:RegularExpressionValidator>
	</td>
</tr>
</table> 
<table id="tbl" runat="server" width="100%" >
    <tr>
    <td style="width:40%"></td>   
    <td>
    <%--<asp:Button ID="btnAccept" runat="server" ClientIDMode="Static" Text="OK" Width = "90%"
            CssClass="ButtonControl" OnClientClick="return ReadMasterKeyData()" />--%>
            <input type="button" class="ButtonControl" onclick="return ReadMasterKeyData()" value="Ok" style="width:90%" />
    </td>
    <td>
    <asp:Button ID="btnCancel" runat="server" ClientIDMode="Static" Text="Cancel" Width = "90%"
             CssClass="ButtonControl"  />  
    </td>  
    <td style="width:30%"></td>   
    </tr>
    </table>     
</asp:Panel>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>

<asp:Panel ID="pnlViewCard" runat="server" ScrollBars="None" Width="95%" Height="250px" BorderWidth="1" BorderColor="Gray" HorizontalAlign="Center" align="Center"
        CssClass="srcColor" ClientIDMode="Static" style="display:none">
<table id="table3" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0" class="TableClass">
	<tr>
	<td class = "TDClassLabel" style="height: 10px"> Employee Name : </td>
	<td class = "TDClassControl" style="height: 10px">
		<asp:TextBox CssClass ="TextControl" id="txtEmpName" runat="server" Width="40%"  ReadOnly="true" ClientIDMode="Static"></asp:TextBox>        
        
     </td>
    </tr>       
    <tr>
	<td class = "TDClassLabel" style="height: 10px"> Employee Code : </td>
	<td class = "TDClassControl" style="height: 10px">
		<asp:TextBox CssClass ="TextControl" id="txtEmpCd"  runat="server" Width="40%"  ReadOnly="true" ClientIDMode="Static"></asp:TextBox>                    
	</td>  
    </tr>
     <tr>
	<td class = "TDClassLabel" style="height: 10px"> Date Of Birth : </td>
	<td class = "TDClassControl" style="height: 10px">
		<asp:TextBox CssClass ="TextControl" id="txtDOB" runat="server" Width="40%"  ReadOnly="true" ClientIDMode="Static"></asp:TextBox>   
           
	</td>
    </tr>
    <tr>
	<td class = "TDClassLabel" style="height: 10px"> Gender : </td>
	<td class = "TDClassControl" style="height: 10px">
		<asp:TextBox CssClass ="TextControl" id="txtGender" runat="server" Width="40%"  ReadOnly="true" ClientIDMode="Static"></asp:TextBox>        
         
	</td>
    </tr>
     <tr>
	<td class = "TDClassLabel" style="height: 10px"> Card Expiry Date : </td>
	<td class = "TDClassControl" style="height: 10px">
		<asp:TextBox CssClass ="TextControl" id="txtCardExpiryDate" runat="server" Width="40%"  ReadOnly="true" ClientIDMode="Static"></asp:TextBox>        
        <%--<input type="text" id="txtCardExpiryDate" name="ExpiryDate">--%>
	</td>
    </tr>
     <tr>
	<td class = "TDClassLabel" style="height: 10px"> Card Expiry Time : </td>
	<td class = "TDClassControl" style="height: 10px">
		<asp:TextBox CssClass ="TextControl" id="txtCardExpiryTime" runat="server" Width="40%"  ReadOnly="true" ClientIDMode="Static"></asp:TextBox>        
         
	</td>
    </tr>
     <tr>
	<td class = "TDClassLabel" style="height: 10px"> Application : </td>
	<td class = "TDClassControl" style="height: 10px">
		<%--<asp:ListBox ID = "lstApplication" runat ="server" ClientIDMode="Static" Width="40%"  ReadOnly="true" ></asp:ListBox>--%>
          <asp:CheckBoxList ID="chkApplication" runat="server" BorderColor="Gray" 
                    BorderWidth="1" ClientIDMode="Static" Width="40%" TabIndex="3">
                    <asp:ListItem> Time Attendance </asp:ListItem>
                    <asp:ListItem> Access Control </asp:ListItem>
           </asp:CheckBoxList>
         
	</td>
    </tr>
    </table>
    <br />
    <table id="table4" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0" class="TableClass">
    <tr>
    <td class = "TDClassLabel" style="height: 10px;width:40% "></td>
    <td  style="height: 10px;width:10% ">
		<%--<asp:Button ID="btnOk" runat="server" ClientIDMode="Static" Text="Ok" 
            Width="60%" CssClass="ButtonControl"  OnClientClick="return btnOk_click"/>--%>
            <input type="button" class="ButtonControl" onclick="return btnOk_click()" value="Ok" style="width:60%" />
          
     </td>
     
     </tr>
    </table>
   
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>

<asp:UpdateProgress ID="UpdateProgress1" runat="server">
   <ProgressTemplate>       
       Reading Card Details please wait ............
   </ProgressTemplate>
</asp:UpdateProgress>
<asp:HiddenField ID="hdnMasterKey" ClientIDMode="Static" runat="server"/>   
<asp:HiddenField ID="hdnCompSiteCd" ClientIDMode="Static" runat="server"/>  
<asp:HiddenField ID="hdnUserId" ClientIDMode="Static" runat="server"/>  
<asp:HiddenField ID="hdnPassword" ClientIDMode="Static" runat="server"/>  
<asp:HiddenField ID="hdnEmpName" ClientIDMode="Static" runat="server"/>  
<asp:HiddenField ID="hdnEmpId" ClientIDMode="Static" runat="server"/>  
<asp:HiddenField ID="hdnDOB" ClientIDMode="Static" runat="server"/>  
<asp:HiddenField ID="hdnExpiryDate" ClientIDMode="Static" runat="server"/>  
<asp:HiddenField ID="hdnExpiryTime" ClientIDMode="Static" runat="server"/>  

</asp:Content>
