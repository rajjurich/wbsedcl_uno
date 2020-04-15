<%@ Page Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="BioMetric_Template_Configuration.aspx.cs" Inherits="UNO.BioMetric_Template_Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css" />
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css" />
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css" />
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script>
        //Resolved Bug 294 - Swapnil Start
        function pageLoad() {

        }

        function SetFocus() {
        }
        //Resolved Bug 294 - Swapnil end

        function SetListStyle() {
            location.reload();
        }        


    </script>
    <script type="text/javascript" language="javascript">
        function onUpdating() {
        }

        function onUpdated() {
        }
    </script>
    <script type="text/javascript">
        function ResetAll() {
        }


        function DisplayLabelText() {
        }
        function DisplayLabelTextEntity() {
        }
    </script>
    <style>
        .textBold
        {
            text-align: center;
            vertical-align: middle;
            font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;
            font-weight: bold;
            color: Black;
            font-size: x-large;
        }
        
        .watermark
        {
            color: Gray;
            font-size: xx-small;
            height: 17px;
            width: 120px;
            border-radius: 15px;
            margin-right: 10px;
        }
        .searchTextBox
        {
            height: 17px;
            width: 120px;
            font-size: xx-small;
            border-radius: 15px;
            margin-right: 10px;
        }
        .DivEmpDetails
        {
            text-align: center;
            width: 95%; /*border: 1px solid #333333;*/
            border-radius: 15px;
            background-color: #47A3DA;
            margin: 10px 10px 10px 10px;
            padding: 10px 10px 10px 10px; /*min-height: 200px;*/ /*font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;*/
            box-shadow: 10px 10px 5px #888888;
        }
        .gvHeader
        {
            background-color: transparent;
            border: 0px solid #66B7F5;
            max-height: 29px;
            height: 29px;
            min-height: 29px;
        }
        gvAlternateRow
        {
        }
        .gvRow
        {
            border-bottom: 1px solid #C3C3C3;
            max-height: 26px;
            height: 26px;
            min-height: 26px;
        }
        .gvPager
        {
            vertical-align: bottom;
        }
        .center
        {
            text-align: center;
        }
        .Hide
        {
            display: none;
        }
    </style>
    <!--[if IE]>
<style>
    .DivEmpDetails {
                     text-align: center;
            width: 95%;
            border: 1px solid #333333;
            border-radius: 15px;
            margin: 10px 10px 10px 10px;
            padding: 10px 10px 10px 10px;
            background-color:#53AEF3;
            font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;
    }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <br />
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="BioMetric Template Configuration" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <div style="position:absolute; left:39%">
    <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
            <div style="position:absolute; left:1%">
                <asp:Label ID="Label2" runat="server" Text="Number Of Finger to be capture : " style="color: black;"></asp:Label>
                <asp:DropDownList ID="ddlNoofFig" runat="server">
                   <asp:ListItem Enabled="false">0</asp:ListItem>
                   <asp:ListItem Enabled="false">1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                </asp:DropDownList>
                
                </div>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
          <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
          <br /><br /><br />
         </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
            <div style="position:relative;">
                <span style=" position:absolute; top:26px; left:-4px;">
                <%--GroupName="LH"RadioButton--%>
                 <span style="padding: 6px 3px 2px 3px; background-color:  red; border-radius:  50%;"><asp:CheckBox ID="LHC1" runat="server" AutoPostBack="True" OnCheckedChanged="LHC1Changed" ToolTip="Use For Access" /></span><br/>
                 <span style="padding: 6px 3px 2px 3px; background-color:  green; border-radius:  50%;"><asp:CheckBox ID="LHR1" runat="server"  Enabled="false" AutoPostBack="True" OnCheckedChanged="LHR1Changed" ToolTip="Use For Time Attendance" /></span></span>

                <span style=" position:absolute; top:-1px; left:29px;">
                <span style="padding: 6px 3px 2px 3px; background-color:  red; border-radius:  50%;"><asp:CheckBox ID="LHC2" runat="server" AutoPostBack="True" OnCheckedChanged="LHC2Changed" ToolTip="Use For Access"/></span><br/>               
                <span style="padding: 6px 3px 2px 3px; background-color:  green; border-radius:  50%;"><asp:CheckBox ID="LHR2" runat="server" Enabled="false"  AutoPostBack="True" OnCheckedChanged="LHR2Changed" ToolTip="Use For Time Attendance"/></span></span>

                <span style=" position:absolute; top:-11px; left:64px;">
                <span style="padding: 6px 3px 2px 3px; background-color:  red; border-radius:  50%;"><asp:CheckBox ID="LHC3" runat="server" AutoPostBack="True" OnCheckedChanged="LHC3Changed" ToolTip="Use For Access"/></span><br/>               
                <span style="padding: 6px 3px 2px 3px; background-color:  green; border-radius:  50%;"><asp:CheckBox ID="LHR3" runat="server" Enabled="false"  AutoPostBack="True" OnCheckedChanged="LHR3Changed" ToolTip="Use For Time Attendance"/></span></span>

                <span style=" position:absolute; top:5px; left:92px;">
                <span style="padding: 6px 3px 2px 3px; background-color:  red; border-radius:  50%;"><asp:CheckBox ID="LHC4" runat="server" AutoPostBack="True" OnCheckedChanged="LHC4Changed" ToolTip="Use For Access"/></span><br/>               
                <span style="padding: 6px 3px 2px 3px; background-color:  green; border-radius:  50%;"><asp:CheckBox ID="LHR4" runat="server" Enabled="false"  AutoPostBack="True" OnCheckedChanged="LHR4Changed" ToolTip="Use For Time Attendance"/></span></span>

                <span style=" position:absolute; top:79px; left:135px;">
                <span style="padding: 6px 3px 2px 3px; background-color:  red; border-radius:  50%;"><asp:CheckBox ID="LHC5" runat="server" AutoPostBack="True" OnCheckedChanged="LHC5Changed" ToolTip="Use For Access"/></span><br/>               
                <span style="padding: 6px 3px 2px 3px; background-color:  green; border-radius:  50%;"><asp:CheckBox ID="LHR5" runat="server" Enabled="false"  AutoPostBack="True" OnCheckedChanged="LHR5Changed" ToolTip="Use For Time Attendance"/></span></span>

                <asp:Image ID="imgLeft" runat="server" ImageUrl="~/images/LeftHand.png" />

                <span style=" position:absolute; top:80px; left:160px;">
                <%--GroupName="RH"--%>
                <span style="padding: 6px 3px 2px 3px; background-color:  red; border-radius:  50%;"><asp:CheckBox ID="RHC1" runat="server" AutoPostBack="True" OnCheckedChanged="RHC1Changed" ToolTip="Use For Access"/></span><br/>
                <span style="padding: 6px 3px 2px 3px; background-color:  green; border-radius:  50%;"><asp:CheckBox ID="RHR1" runat="server" Enabled="false"  AutoPostBack="True" OnCheckedChanged="RHR1Changed" ToolTip="Use For Time Attendance"/></span></span>

                <span style=" position:absolute; top:6px; left:204px;">
                <span style="padding: 6px 3px 2px 3px; background-color:  red; border-radius:  50%;"><asp:CheckBox ID="RHC2" runat="server" AutoPostBack="True" OnCheckedChanged="RHC2Changed" ToolTip="Use For Access"/></span><br/>               
                <span style="padding: 6px 3px 2px 3px; background-color:  green; border-radius:  50%;"><asp:CheckBox ID="RHR2" runat="server" Enabled="false" AutoPostBack="True" OnCheckedChanged="RHR2Changed" ToolTip="Use For Time Attendance"/></span></span>

                <span style=" position:absolute; top:-9px; left:233px;">
                <span style="padding: 6px 3px 2px 3px; background-color:  red; border-radius:  50%;"><asp:CheckBox ID="RHC3" runat="server" AutoPostBack="True" OnCheckedChanged="RHC3Changed" ToolTip="Use For Access"/></span><br/>               
                <span style="padding: 6px 3px 2px 3px; background-color:  green; border-radius:  50%;"><asp:CheckBox ID="RHR3" runat="server" Enabled="false" AutoPostBack="True" OnCheckedChanged="RHR3Changed" ToolTip="Use For Time Attendance"/></span></span>

                <span style=" position:absolute; top:2px; left:266px;">
                <span style="padding: 6px 3px 2px 3px; background-color:  red; border-radius:  50%;"><asp:CheckBox ID="RHC4" runat="server" AutoPostBack="True" OnCheckedChanged="RHC4Changed" ToolTip="Use For Access"/></span><br/>               
                <span style="padding: 6px 3px 2px 3px; background-color:  green; border-radius:  50%;"><asp:CheckBox ID="RHR4" runat="server" Enabled="false" AutoPostBack="True" OnCheckedChanged="RHR4Changed" ToolTip="Use For Time Attendance"/></span></span>

                <span style=" position:absolute; top:28px; left:294px;">
                <span style="padding: 6px 3px 2px 3px; background-color:  red; border-radius:  50%;"><asp:CheckBox ID="RHC5" runat="server" AutoPostBack="True" OnCheckedChanged="RHC5Changed" ToolTip="Use For Access"/></span><br/>               
                <span style="padding: 6px 3px 2px 3px; background-color:  green; border-radius:  50%;"><asp:CheckBox ID="RHR5" runat="server" Enabled="false" AutoPostBack="True" OnCheckedChanged="RHR5Changed" ToolTip="Use For Time Attendance"/></span></span>
                <asp:Image ID="imgRight" runat="server" ImageUrl="~/images/RightHand.png" />
                </div>
            </asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
          <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
          <br /><br />
         </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
            <div style="position:absolute; left:40%">
                <asp:Button ID="btnBioMetric" runat="server" Text="" OnClick="btnBioMetric_Click" Font-Bold="True"  />
                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    </div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server"  style="position:absolute; left:40%; top:60%">
            <ContentTemplate>
                <div style="text-align: center">
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBioMetric" />
            </Triggers>
        </asp:UpdatePanel>

       <asp:UpdatePanel ID="UpdatePanel1" runat="server"  style="position:absolute; left:40%; top:70%; color:black; font-size:medium">
            <ContentTemplate>
                <div><br /><br />
                    <div style="text-align:center"><b >NOTE*</b></div>
                     <span style="background-color: red;"><b><asp:CheckBox ID="CheckBox1" runat="server" checked ="true" Text="  Use For Enroll" ToolTip="Use For Enroll"/></b></span><br />
                     <span style="background-color: green;"><b><asp:CheckBox ID="RadioButton1" runat="server" checked ="true" Text="  Use For Time Attendance" ToolTip="Use For Time Attendance"/></b></span><br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
