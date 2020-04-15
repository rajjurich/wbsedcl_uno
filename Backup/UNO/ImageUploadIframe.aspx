<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageUploadIframe.aspx.cs" Inherits="UNO.ImageUploadIframe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <asp:Image ID="imgEmployeeImage"  runat="server" Height="130px" Style="text-align: center" Width="116px" ClientIDMode="Static" />
                            <asp:FileUpload ID="FileUploadImages" runat="server" ClientIDMode="Static" />
                            <div style="margin-top:2%">  <asp:Button ID="BtSaveImage" runat="server" Text="Show Image" onclick="BtSaveImage_Click"  /></div>

        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
