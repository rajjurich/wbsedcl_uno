<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" CodeBehind="RawCard.aspx.cs" Inherits="UNO.RawCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <link href="Styles/Style.css" rel="stylesheet" type="text/css" />   
    <script language="javascript" type="text/javascript" src="Scripts/Validation1.js"></script>  
    <script language="javascript" type="text/javascript">       

        function ValidateData() {
            if (!Page_ClientValidate())
                return;
        }

        function ReadMasterKeyData() {
            try {

                 var objReadCard;
                 objReadCard = new ActiveXObject("ContactlessCardRW.Card");

                if (!Page_ClientValidate())
                    return;

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

                alert("Place User Card on reader");
                return true;

            }
            catch (e) {
                alert(e.message);              
                return false;
            }
        }

        function Reinitialize() {
            try {              
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
//                 else {
//                     alert("Company code or site id match successfully");
//                 }

            //*********end of reading User Card***************************************************************

            //*****************Read App Code from sec 0********************************************************
            Data = obj_Card.Authenticate(3, obj_Card.Cardrawkeybuf, 96);
            if (Data != "")
            {
                alert("Authentication Error Sector 0 Block 3");               
                return false;
            }
            else
            {
                var OldDataBlock1 = obj_Card.ReadBlock(1);
                var OldDataBlock2 = obj_Card.ReadBlock(2);
               // alert(OldDataBlock1);
            }
            //****************end of reading app code***********************************************************

            //****************Read Sec 14 block 0 for Time Attendance & Access Random No*********************

            //Authenticate sec 14 with Master Key
            Data = obj_Card.Authenticate(59, MasterKey, 96);
            if (Data != "")
            {
                Data = obj_Card.Authenticate(59, "FFFFFFFFFFFF", 96);

                if (Data != "")
                {
                    alert("Authentication Error Sector 14 Block 59");                 
                    return false;
                }
            }
            else
            {
                var ReadData = obj_Card.ReadBlock(56);
                var Stemp = "";

                for (var i = 0; i < ReadData.length; i++)
                {
                    if (ReadData.substring(i, 1) != " ")
                    {
                        Stemp += ReadData.substring(i, 1);
                    }
                }
              //  alert(Stemp);
                Stemp = obj_Card.Convert(Stemp);
                Rnd[3] = Stemp.substring(Stemp.length - 6, 6);
                Rnd[2] = Stemp.substring(4, 6);
             
            }
            //****************End*************************************************************************
            //***********below code for making sector 0 block 1,2 raw*********************************
            Data = obj_Card.Authenticate(2, obj_Card.Cardrawkeybuf, 96);
            if (Data != "")
            {
                Data = obj_Card.Authenticate(2, "FFFFFFFFFFFF", 96);

                if (Data != "")
                {
                    alert("Authentication Error Sector 0 Block 2");                 
                    return false;
                }
            }
            else
            {
                var WriteData = "00000000000000000000020002000200";
                Data = obj_Card.WriteBlock(2, WriteData);
                if (Data != "")
                {
                    alert("Making Sector Raw.Card Error,Sector 0 Block 2");                    
                    return false;
                }
               // alert("Making Sector Raw.Sector 0 Block 2" + Data);

                var WriteData1 = "FF0F0000000000000000000000000000";
                Data = obj_Card.WriteBlock(1, WriteData1);
                if (Data != "")
                {
                    alert("Making Sector Raw.Card Error,Sector 0 Block 1");
                    return false;
                }
              //  alert("Making Sector Raw.Sector 0 Block 1" + Data);
            }
            //**********End***************************************************************
            //***********below code for making sector 1 raw*********************************

            Data = obj_Card.Authenticate(6, obj_Card.Cardrawkeybuf, 96);
            if (Data != "")
            {
                alert("Authentication Error Sector 1 Block 6");               
                return false;
            }
            else
            {
                var WriteData = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
                if (obj_Card.WriteBlock(6, WriteData) != "")
                {
                    alert("Error in writting data to Sector 1 Block 6");                    
                    return false;
                }
            }
          //  alert("writting data to Sector 1 Block 6" + Data);

            Data = obj_Card.Authenticate(7, obj_Card.Cardrawkeybuf, 96);
            if (Data != "")
            {
                Data = obj_Card.Authenticate(7, "FFFFFFFFFFFF", 96);

                if (Data != "")
                {
                    alert("Authentication Error Sector 1 Block 7");                 
                    return false;
                }
            }
            else
            {
                Data = obj_Card.MakeSectorRaw("7", "FFFFFFFFFFFF", "FFFFFFFFFFFF");
                if (Data != "")
                {
                    alert("Making Sector Raw.Card Error,Sector 1 Block 7");                  
                    return false;
                }
           //     alert("Making Sector Raw.Sector 1 Block 7" + Data);
            }

            //**********************End************************************************    
            
            //**************************Raw Sector 2******************************************
                var RandomNo = Rnd[2];
                if (obj_Card.RndSecRaw(RandomNo, "08", "02") != true)
                {
                    alert("Making Sector Raw for Time Attendance. Card Error, Sector 2 Block 8");                    
                    return false;
                }

                if(obj_Card.RndSecRaw(RandomNo,"11","02") != true)
                {
                    alert("Making Sector Raw for Time Attendance. Card Error, Sector 2 Block 11");                    
                    return false;
                }
            
            //*************************Raw Sector 3************************************************
                var RandomNo1 = Rnd[3];
                if (obj_Card.RndSecRaw(RandomNo1, "12", "03") != true)
                {
                    alert("Making Sector Raw for Time Attendance. Card Error, Sector 2 Block 8");                    
                    return false;
                }
                if (obj_Card.RndSecRaw(RandomNo1,"15", "03") != true)
                {
                    alert("Making Sector Raw for Access Control.Card Error,Sector 3 Block 15");                  
                    return false;
                }
            
            //****************************End************************************************

            //***********below code for making sector 13 raw*********************************
                Data = obj_Card.Authenticate(55, MasterKey, 96);
                if (Data != "")
                {
                    Data = obj_Card.Authenticate(55, "FFFFFFFFFFFF", 96);
                    if (Data != "")
                    {
                       alert("Authentication Error Sector 13 Block 37");                     
                       return false;
                    }
                }
                else
                {
                    Data = obj_Card.MakeSectorRaw("37", "FFFFFFFFFFFF", "FFFFFFFFFFFF");
                    if (Data != "")
                    {
                        alert("Making Sector Raw.Card Error,Sector 13 Block 55");                       
                        return false;
                    }
                 //   alert("Making Sector Raw.Sector 13 Block 55" + Data);
                }

            //**********************End************************************************

            //***********below code for making sector 14 raw*********************************
                Data = obj_Card.Authenticate(59, MasterKey , 96);
                if (Data != "")
                {
                    Data = obj_Card.Authenticate(59, "FFFFFFFFFFFF", 96);

                    if (Data != "")
                    {
                        alert("Authentication Error Sector 14 Block 59");                     
                        return false;
                    }
                }
                else
                {
                    Data = obj_Card.MakeSectorRaw("3B", "FFFFFFFFFFFF", "FFFFFFFFFFFF");
                    if (Data != "")
                    {
                        alert("Making Sector Raw.Card Error,Sector 14 Block 59");                       
                        return false;
                    }
                   // alert("Making Sector Raw.Sector 14 Block 59" + Data);
                }

            //**********************End************************************************          
           
            //**********************Raw Sector 15 block 60********************************
                Data = obj_Card.Authenticate(63, obj_Card.Cardrawkeybuf, 96);
                 if (Data != "")
                 {
                     Data = obj_Card.Authenticate(2, "FFFFFFFFFFFF", 96);

                     if (Data != "")
                     {
                         alert("Authentication Error Sector 15 Block 60");                         
                         return false;
                     }
                 }
                 else
                 {
                     var ReadData = obj_Card.ReadBlock(60);
                     var Stemp = "";

                     for (var i = 0; i < ReadData.length; i++)
                     {
                         if (ReadData.substr(i, 1) != " ")
                         {
                             Stemp += ReadData.substr(i, 1);
                         }
                     }
                   
                     ReadData = Stemp;

                     var Writedata = "000000000000000000000000000000" + ReadData.substr(ReadData.length - 2, 2);
                    // alert(Writedata);
                     Data = obj_Card.WriteBlock(60, Writedata);
                     if (Data != "")
                     {
                         alert("Making Sector Raw.Card Error,Sector 15 Block 60");                      
                         return false;
                     }
                   //  alert("Making Sector Raw.Sector 15 Block 60" + Data);
                 }

                 alert("Card Reinitialized Successfully.");         
                 return true;
            //*************end************************************************************
      
            }
            catch (e) {
                alert(e.message);
                return false;
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
<h3 class="heading">Reinitialize Card</h3>
</td>
</tr>

</table>
<%--<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>--%>

<asp:Panel ID="pnlCardDetails" ClientIDMode="Static" runat="server" Width="95%" Height="150px" BorderWidth="1" BorderColor="Gray" HorizontalAlign="Center" align="Center" CssClass="srcColor" >
<asp:Label ID="Label1" runat = "server" Text = "Master Card Details" Font-Bold="true" Font-Size="Medium" ></asp:Label>

<table id="table4" runat="server" width="100%" >
<tr>
<td class = "TDClassLabel" >User ID :  </td>
	<td class = "TDClassControl" >
		<asp:TextBox CssClass ="TextControl" ClientIDMode="Static" id="txtuid" MaxLength="6"   runat="server" onkeypress="return IsAlphanumericWithoutspace(event)" ></asp:TextBox>
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
    <asp:Button ID="btnAccept" runat="server" ClientIDMode="Static" Text="OK" Width = "90%"
            CssClass="ButtonControl" OnClientClick="return ReadMasterKeyData()" OnClick="btnAccept_Click" />
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
<asp:Panel ID="pnlRawCard" runat="server" ScrollBars="None" Width="95%" Height="200px"
        CssClass="srcColor" ClientIDMode="Static">
<table id="table3" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0" class="TableClass">
<tr>
<td align ="center">
<asp:Label runat = "server" Text = "Please do not remove User Card" Font-Bold="true" Font-Size="Medium" ></asp:Label>
</td>
</tr>
</table>
<br /><br />
<table id="Table5" runat="server" width="100%" >
    <tr>
    <td style="width:40%"></td>   
    <td>
    <asp:Button ID="btnok" runat="server" ClientIDMode="Static" Text="OK" Width = "70%"
            CssClass="ButtonControl" OnClientClick="return Reinitialize()" />
    </td>
    <td>
    <asp:Button ID="btnExit" runat="server" ClientIDMode="Static" Text="Cancel" Width = "60%"
             CssClass="ButtonControl" onclick="btnExit_Click" />  
    </td>  
    <td style="width:30%"></td>   
    </tr>
    </table>     
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>

<asp:UpdateProgress ID="UpdateProgress1" runat="server">
   <ProgressTemplate>       
       Reinitializing Card please wait ............
   </ProgressTemplate>
</asp:UpdateProgress>
    <asp:HiddenField ID="hdnMasterKey" ClientIDMode="Static" runat="server"/>   
    <asp:HiddenField ID="hdnCompSiteCd" ClientIDMode="Static" runat="server"/>  
    <asp:HiddenField ID="hdnUserId" ClientIDMode="Static" runat="server"/>  
    <asp:HiddenField ID="hdnPassword" ClientIDMode="Static" runat="server"/>  
</asp:Content>
