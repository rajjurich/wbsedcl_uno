
<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master"
    CodeBehind="FeedBack.aspx.cs" Inherits="UNO.FeedBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Styles/demo.css" />
       <link href='http://fonts.googleapis.com/css?family=PT+Sans+Narrow&v1' rel='stylesheet'
        type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Coustard:900' rel='stylesheet'
        type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Rochester' rel='stylesheet' type='text/css' />
    <link rel="stylesheet" type="text/css" href="Styles/style.css" />
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
        .wrapper
        {
            width: 1031px;
            margin: 0 auto 30px;
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
                <div id="wrapper" class="wrapper">
               <div class="t-con">
               
                    <div id="about_bg">

                        <div class="heading1" style="text-align:center">
                            FeedBack
                        </div>
                              <div class="shadow-mission"></div>
                        <table style="margin-top: 4%;" cellpadding="1" cellspacing="1">
                            <tr style="padding-bottom: 2%">
                                <td align="left">
                                    Module :
                                </td>
                                <td align="left" style="padding-bottom: 2%">
                                    <asp:DropDownList ID="drpModule" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="padding-bottom: 2%">
                                <td align="left">
                                    Comment :
                                </td>
                                <td align="left" style="padding-bottom: 2%">
                                    <textarea id="txtComment" cols="60" rows="10" runat="server"></textarea>
                                </td>
                            </tr>
                            <tr style="padding-bottom: 2%">
                                <td align="left">
                                    Image Upload :
                                </td>
                                <td align="left" style="padding-bottom: 2%">
                                    <asp:FileUpload ID="ImageUpload" runat="server"  />
                                    <asp:Button runat="server" ID="UploadButton" Text="Upload" OnClick="UploadButton_Click"  c
                                        CssClass="ButtonControl" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-bottom: 2%">
                                    <asp:Label ID="lblfile" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblImageName" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr style="padding-bottom: 2%; text-align:center">
                                <td colspan="2" style="padding-bottom: 2%">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="ButtonControl" OnClick="btnSave_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnCloseFB" runat="server" Text="Close" CssClass="ButtonControl" 
                                        Visible="False"  />
                                </td>
                            </tr>
                            <tr style="padding-bottom: 2%">
                                <td colspan="2" style="padding-bottom: 2%">
                                    <asp:Label ID="lblFeedBackId" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                              <div class="shadow-mission"></div>
                    </div>
                    </div>
                    </div>
            </ContentTemplate>
            <Triggers>

        <%--    <asp:PostBackTrigger ControlID="ImageUpload" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </center>
 


        



 

</asp:Content>
