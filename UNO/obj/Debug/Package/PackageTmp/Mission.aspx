
<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master"
    CodeBehind="Mission.aspx.cs" Inherits="UNO.Mission1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Styles/demo.css" />
       <link href='http://fonts.googleapis.com/css?family=PT+Sans+Narrow&v1' rel='stylesheet'
        type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Coustard:900' rel='stylesheet'
        type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Rochester' rel='stylesheet' type='text/css' />

    <link rel="stylesheet" type="text/css" href="Styles/style.css" />
   <style type="text/css">
       
    <%-- <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">--%>
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css"/>
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
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
<style type="text/css">

  .heading1
        {
            background: #47a3da;
            padding: 5px;
            text-align: left;
            font-size: 20px;
            color: #fff;
            font-family: 'HLHC';
            text-shadow: 2px 2px 2px #666666;
        }
        .our-mission-content
        {
            padding: 10px;
            color: #222222;
            background: #fff;
            font-family: 'Segoe UI';
            position: relative;
        }
        .our-mission-content span
        {
            color: #000;
            font-size: 40px;
            text-align: center;
            display: inline-block;
            margin: 50px 0 34px 0%;
            border-bottom: 1px solid #bf2c2f;
            font-weight: 900 !important;
        }
        .bulb-art
        {
            background: url(images/Bulb_art_cms.jpg) no-repeat;
            position: absolute;
            height: 170px;
            width: 258px;
            top: 0;
            left: 0px;
            opacity: 0.2;
        }
        .our-mission-content p
        {
            font-size: 16px;
            color: #333333;
            text-align: center;
        }
        .shadow-mission
        {
            background: url(images/our-mission-shadow.png) no-repeat;
            width: 100%;
            height: 21px;
        }
        .mission-body
        {
            position: relative;
        }
        .mission-body span
        {
            color: #000;
            text-transform: uppercase;
            border-bottom: 1px solid #106891;
            margin: 8px 0 34px 0%;
        }
        .mission-body p
        {
            padding: 0 20px 10px 20px;
        }
        .art-2
        {
            background: url(images/improve_data_quality.png) no-repeat;
            top: 0;
            width: 100%;
            height: 100%;
            position: absolute;
            opacity: 0.6;
        }
        #wrapper
        {
            width: 1031px;
            margin: 0 auto 10px;
            position: relative;
        }
        .t-con
        {
            font-family: 'OpenSansLight';
        }
        #about_bg
        {
            background: #fff;
            opacity: 0.9;
            margin-top: 10px;
            position: relative;
        }
        
        .shadow-mission
        {
            background: url(images/our-mission-shadow.png) no-repeat;
            width: 100%;
            height: 21px;
        }


</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <center>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
              <asp:Panel ID="Visi" runat="server">
                <div class="t-con">
                <div id="wrapper" class="t-con">
                    <div class="art-2">
                        &nbsp;</div>
                    <div id="about_bg">
                        <div class="heading1" style="text-align:center;">
                            Vision And Mission</div>
                        <div class="our-mission-content">
                            <span>OUR VISION</span>
                            <p runat="server" id="vision">
                            </p>
                            <div class="bulb-art">
                                &nbsp;</div>
                        </div>
                        <div class="shadow-mission">
                            &nbsp;</div>
                        <div class="our-mission-content mission-body">
                            <span>OUR MISSION</span>
                            <p runat="server" id="Mission">
                            </p>
                            <br />
                            
                        </div>
                    </div>
                </div>
            </div>
      </asp:Panel>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </center>
 

 <asp:LinkButton ID="lnkDummy" runat="server" Style="display: none;">dummy</asp:LinkButton>
        <asp:Panel ID="pnlMessage" runat="server" CssClass="popupPannel" Style="min-width: 150px;
            min-height: 50px;">
            <table style="width: 100%; height: 100%; min-height: 100%; max-width: 100%;">
                <tr>
                    <td style="padding-bottom: 20px;">
                        <asp:Label ID="lblMessage" runat="server" Text="" Style="font-family: Arial; font-weight: bold;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnMessageOK" runat="server" Text="OK" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="mpeMessage" runat="server" TargetControlID="lnkDummy"
            PopupControlID="pnlMessage" OkControlID="btnMessageOK" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        



 

</asp:Content>
