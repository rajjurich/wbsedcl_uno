<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs"
    Inherits="UNO.ResetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script src="Scripts/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.complexify.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/default.css" />
    <title>Welcome to UNO</title>
    <style type="text/css">
        #demo
        {
            width: 380px;
            margin-right: auto;
            margin-left: auto;
        }
        
        #progressbar
        {
            width: 200px;
            height: 20px;
            display: block;
            border-left: 0px solid #ccc;
            border-right: 0px solid #ccc;
            border-top: 1px solid #ccc; /*  border-top-right-radius: 8px;
            border-top-left-radius: 8px;&*/
            overflow: hidden;
            background-color: white;
        }
        
        #progress
        {
            display: block;
            height: 50px;
            width: 0%;
        }
        
        .progressbarValid
        {
            background-color: green;
            background-image: -o-linear-gradient(-90deg, #8AD702 0%, #389100 100%);
            background-image: -moz-linear-gradient(-90deg, #8AD702 0%, #389100 100%);
            background-image: -webkit-linear-gradient(-90deg, #8AD702 0%, #389100 100%);
            background-image: -ms-linear-gradient(-90deg, #8AD702 0%, #389100 100%);
            background-image: linear-gradient(-90deg, #8AD702 0%, #389100 100%);
        }
        
        .progressbarInvalid
        {
            background-color: red;
            background-image: -o-linear-gradient(-90deg, #F94046 0%, #92080B 100%);
            background-image: -moz-linear-gradient(-90deg, #F94046 0%, #92080B 100%);
            background-image: -webkit-linear-gradient(-90deg, #F94046 0%, #92080B 100%);
            background-image: -ms-linear-gradient(-90deg, #F94046 0%, #92080B 100%);
            background-image: linear-gradient(-90deg, #F94046 0%, #92080B 100%);
        }
        
        #status
        {
            height: 30px;
            width: 200px;
            border: 1px solid #ccc; /*  border-bottom-right-radius: 8px;
            border-bottom-left-radius: 8px;*/
            background-color: white;
        }
        
        .password
        {
            width: 200px; /* border-radius: 8px;*/
            padding: 3px; /* box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);*/
            border: 1px solid #cccccc;
            font-family: "Helvetica Neue" , Helvetica, Arial, sans-serif;
            -webkit-text-security: disc;
            -webkit-appearance: textfield;
            outline: none;
        }
        
        #complexityLabel
        {
            width: 100%;
            text-align: center;
            margin-top: -4%;
        }
        
        #complexity
        {
            width: 100%;
            text-align: center;
            font-family: "Helvetica Neue" , "Helvetica" , Arial, sans-serif;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Reset Password" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div style="width: 100%; text-align: center; margin-top: 2%; color: Black;">
        <asp:Panel ID="ResetPwdPanel" runat="server" Visible="false">
            <center>
                <table>
                    <tr>
                        <td style="height: 3%" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; padding-top: 2%">
                            New Password
                            <label class="CompulsaryLabel">
                                *</label>
                        </td>
                        <td style="padding-left: 2%; padding-top: 2%">
                            <asp:TextBox ID="txtNewPwd" runat="server" CssClass="password" TextMode="Password"
                                ClientIDMode="Static"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ErrorMessage="Please Enter New Password"
                                ControlToValidate="txtNewPwd" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvNewPassword" runat="server" TargetControlID="rfvNewPassword"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <div id="progressbar">
                                <div id="progress">
                                </div>
                            </div>
                            <div id="status">
                                <div id="complexity">
                                    0%</div>
                                <div id="complexityLabel">
                                    Complexity</div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 3%" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 2%">
                            Confirm Password
                            <label class="CompulsaryLabel">
                                *</label>
                        </td>
                        <td style="padding-left: 2%; padding-top: 2%">
                            <asp:TextBox ID="txtConfirmPwd" runat="server" CssClass="password" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="Please Confirm Password"
                                ControlToValidate="txtConfirmPwd" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvConfirmPassword" runat="server" ErrorMessage="Confirm Password Does not match"
                                ControlToValidate="txtConfirmPwd" ControlToCompare="txtNewPwd" Display="None"
                                ValidationGroup="Submit"></asp:CompareValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvConfirmPassword" runat="server" TargetControlID="rfvConfirmPassword"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcecvConfirmPassword" runat="server" TargetControlID="cvConfirmPassword"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 3%" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; padding-top: 2%" colspan="2">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="ButtonControl"
                                ValidationGroup="Submit" OnClick="btnChangePwd_Click" />
                            <%--        <input type="button" id="btnCancel" value="Cancel" class="ButtonControl" />--%>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="ButtonControl"
                                OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Label ID="lblError" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </center>
        </asp:Panel>
        <asp:Label ID="lblExpired" runat="server" Text="" Style="color: #FF0000"></asp:Label>
    </div>
    </form>
</body>
<script type="text/javascript" language="javascript">

    $(function () {
        var txtNewPassId = "<%=txtNewPwd.ClientID%>";
     
        $("#" + txtNewPassId).complexify({}, function (valid, complexity) {

            if (!valid) {
                $('#progress').css({ 'width': complexity + '%' }).removeClass('progressbarValid').addClass('progressbarInvalid');
            } else {
                $('#progress').css({ 'width': complexity + '%' }).removeClass('progressbarInvalid').addClass('progressbarValid');
            }
            $('#complexity').html(Math.round(complexity) + '%');
        });
    });
</script>
</html>
