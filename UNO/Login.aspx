<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UNO.Login" %>

<!DOCTYPE html>
<!--[if lt IE 7]> <html class="lt-ie9 lt-ie8 lt-ie7" lang="en"> <![endif]-->
<!--[if IE 7]> <html class="lt-ie9 lt-ie8" lang="en"> <![endif]-->
<!--[if IE 8]> <html class="lt-ie9" lang="en"> <![endif]-->
<!--[if gt IE 8]><!-->
<html lang="en">
<!--<![endif]-->
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>UNO : Login Form</title>
    <link rel="stylesheet" href="Styles/Login.css">
    <!--[if lt IE 9]><script src="Scripts/html5.js"></script><![endif]-->
    <style>
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .popupPannel
        {
            background-color: White;
            border: 5px solid #000000;
            border-radius: 25px;
            color: Black;
            padding: 10px 10px 5px 10px;
            vertical-align: middle;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <section class="container">
        <div class="login">
            <h1>
                Login to UNO</h1>
            <label style="color:Red;"> Oops!! Session expired due to long period of inactivity</label> <br />
            <label style="color:Red;text-align:center;padding-left:32%;"> Please log in again.</label>
            <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </ajaxToolkit:ToolkitScriptManager>
            <p>
                <asp:TextBox ID="txtUserName" runat="server" placeholder="Username"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtUserName" runat="server" ErrorMessage="Please Enter Username"
                    ControlToValidate="txtUserName" Display="None" ValidationGroup="Login"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtUserName" runat="server" TargetControlID="rfvtxtUserName"
                    PopupPosition="Right">
                </ajaxToolkit:ValidatorCalloutExtender>
                <%--<input type="text" name="login" value="" placeholder="Username or Email">--%></p>
            <p>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtPassword" runat="server" ErrorMessage="Please Enter Password"
                    ControlToValidate="txtPassword" Display="None" ValidationGroup="Login"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtPassword" runat="server" TargetControlID="rfvtxtPassword"
                    PopupPosition="Right">
                </ajaxToolkit:ValidatorCalloutExtender>
                <%--<input type="password" name="password" value="" placeholder="Password">--%></p>
            <%--<p class="remember_me">
                <label>
                    <input type="checkbox" name="remember_me" id="remember_me">
                    Remember me on this computer
                </label>
            </p>--%>
            <p class="submit">
                <asp:Button ID="btnLogin" runat="server" Text="Login" ValidationGroup="Login" OnClick="btnLogin_Click" />
                <%--<input type="submit" name="commit" value="Login">--%>
            </p>
        </div>
        <div class="login-help">
            <p>
                Forgot your password? <a id="lnkForgotPassword" runat="server">Click here to reset it</a>.</p>
        </div>
    </section>
    <% if (Session["uid"] == null || Session["uid"].ToString() == "")
       { %>
    <asp:Panel ID="pnlForgot" runat="server" CssClass="popupPannel" Style="min-width: 150px;
        min-height: 50px;">
        <table style="width: 100%; height: 100%;">
            <tr>
                <td>
                    <span>Enter Employee ID</span>
                </td>
                <td>
                    <asp:TextBox ID="txtEmployeeID" runat="server" ValidationGroup="Forgot" MaxLength="8"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmployeeID" runat="server" ErrorMessage="Enter Employee ID"
                        Display="None" ControlToValidate="txtEmployeeID" ValidationGroup="Forgot"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvEmployeeID" runat="server" TargetControlID="rfvEmployeeID"
                        PopupPosition="Right">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" valign="middle">
                    <div id="messageDiv" runat="server" clientidmode="Static" class="MessageClass">
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <asp:Button ID="btnOk" runat="server" Text="Ok" CssClass=" ButtonControl" ValidationGroup="Forgot"
                        OnClick="btnOk_Click"></asp:Button>
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass=" ButtonControl">
                    </asp:Button>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeForgot" runat="server" TargetControlID="lnkForgotPassword"
        PopupControlID="pnlForgot" Enabled="true" BackgroundCssClass="modalBackground"
        CancelControlID="btnCancel">
    </ajaxToolkit:ModalPopupExtender>
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
    <%} %>
    </form>
</body>
</html>
