<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="UNO.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="UNO Home Page" />
    <meta name="keywords" content="UNO" />
    <meta name="author" content="CMS" />
    <link rel="shortcut icon" href="../favicon.ico">
    <link rel="stylesheet" type="text/css" href="Styles/demo.css" />
    <link rel="stylesheet" type="text/css" href="Styles/style.css" />
    <link rel="stylesheet" type="text/css" href="Styles/jquery.jscrollpane.css" media="all" />
    <link href='http://fonts.googleapis.com/css?family=PT+Sans+Narrow&v1' rel='stylesheet'
        type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Coustard:900' rel='stylesheet'
        type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Rochester' rel='stylesheet' type='text/css' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin-top: 50px;">
        <div id="ca-container" class="ca-container" style="height:  550px;">
            <div class="ca-wrapper" style="overflow: hidden;text-align:  center;background-color:  white;">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/wbsedcl.png" style="height:  100%;"/>                
            </div>
        </div>
    </div>
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.easing.1.3.js"></script>
    <!-- the jScrollPane script -->
    <script type="text/javascript" src="Scripts/jquery.mousewheel.js"></script>
    <script type="text/javascript" src="Scripts/jquery.contentcarousel.js"></script>
    <script type="text/javascript">
        $('#ca-container').contentcarousel();
    </script>
</asp:Content>
