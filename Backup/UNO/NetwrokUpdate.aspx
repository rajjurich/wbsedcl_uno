<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="NetwrokUpdate.aspx.cs" Inherits="UNO.NetwrokUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style37
        {
            font-size: 9pt;
            font-family: verdana;
            color: #515456;
            border: 0px solid #CCCCCC;
            text-align: left;
            padding-left: 4px;
            width: 43%;
        }
        .style38
        {
            width: 43%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        function returnviewmode() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";
            window.location = "ActivityBrowser.aspx";
        }
        function clearFunction() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";

        }
        //    function handleErase() {
        //      
        //                    if (document.getElementById('chkEraseUpdateProfile').checked == true)
        //                    { 
        //                        var msg = confirm("All existing profiles will be deleted, Do you want to continue? ");
        //                        if (msg == false) {
        //                            document.getElementById("chkEraseUpdateProfile").checked = false;          
        //                                return false;
        //                                            }
        //                    }

        //        }

        function handleErase() {

            var msg = confirm("All existing profiles will be deleted, Do you want to continue? ");
            if (msg == false) {
                return false;
            }
        }

        function handleChange() {
            var ck_arr = document.getElementById('divEraseUpdate');
            if (document.getElementById('chkUAC').checked == true) {
                ck_arr.disabled = false;
            }

            if (document.getElementById('chkUAC').checked == false) {
                ck_arr.disabled = true;
            }
        }
                   
                    
    </script>
    <table width="100%">
        <tr>
            <td colspan="3" align="center">
                <h3 class="heading">
                    Network
                </h3>
            </td>
        </tr>
    </table>
    <table width="100%" align="right">
        <tr>
            <td height="25px" class="style37" style="text-align: right;">
                &nbsp;
            </td>
            <td height="25px" align="left">
                <asp:Label ID="Label2" runat="server" Text="Select Entity to update" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style38">
                &nbsp;
            </td>
            <td>
                <%--  <table style="margin-left:30%; margin-right:30%;">
                    <tr>
                        <td colspan="2" align="left" class="TDClassControl">--%>
                <asp:CheckBox ID="chkAll" runat="server" Text="All" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td align="left" class="style37">
                &nbsp;
            </td>
            <td align="left">
                <asp:CheckBox ID="chkAccessLevel" runat="server" Text="Access Level" />
            </td>
        </tr>
        <tr>
            <td align="left" class="style37">
                &nbsp;
            </td>
            <td align="left">
                <asp:CheckBox ID="chkAccessPoint" runat="server" Text="Access Point" />
            </td>
        </tr>
        <tr>
            <td align="left" class="style37">
                &nbsp;
            </td>
            <td align="left">
                <asp:CheckBox ID="chkController" runat="server" Text="Controller" />
            </td>
        </tr>
        <tr>
            <td align="left" class="style37">
                &nbsp;
            </td>
            <td align="left">
                <asp:CheckBox ID="chkTimeZone" runat="server" Text="Time Zone" />
            </td>
        </tr>
        <tr>
            <td align="left" class="style38">
                &nbsp;
            </td>
            <td align="left">
                <asp:CheckBox ID="chkUAC" runat="server" Text=" User Access Config" OnClick="JavaScript:handleChange();"
                    ClientIDMode="Static" />
            </td>
            <td align="left">
                <div id="divEraseUpdate" runat="server" clientidmode="Static">
                    <asp:Button ID="cmdEraseUpdateProfile" runat="server" Text="Erase and Update Profiles"
                        OnClientClick="return handleErase()" CssClass="ButtonControl" OnClick="cmdEraseUpdateProfile_Click"
                        ClientIDMode="Static" />
                </div>
            </td>
        </tr>
        <tr>
            <td align="left" class="style37">
            </td>
            <td align="left">
                <asp:CheckBox ID="chkABP" runat="server" Text="Anti-PassBack" ClientIDMode="Static" />
            </td>
            <td align="left">
                <div id="divABP" runat="server" clientidmode="Static">
                    <asp:CheckBox ID="chkResetAllABP" runat="server" Text="Reset All APB" 
                        ClientIDMode="Static" oncheckedchanged="chkResetAllABP_CheckedChanged" />
                </div>
            </td>
            <%--           </tr>
                </table>
            </td>--%>
            <%--<td align="left" class = "style24">  
          <asp:CheckBox ID="chkAll" runat="server" Text="All" />
         </td>
          <td align="left"  class = "style25">  
          <asp:CheckBox ID="chkAccessLevel" runat="server" Text="Access Level" 
                  Visible="False" />
         </td>
          <td align="left" class = "style25">  
          <asp:CheckBox ID="chkAccessPoint" runat="server" Text="Access Point" 
                  Visible="False" />
         </td>
          <td align="left" class = "style26">  
          <asp:CheckBox ID="chkController" runat="server" Text="Controller" Visible="False" />
         </td>
          <td align="left"  class = "style27">  
          <asp:CheckBox ID="chkTimeZone" runat="server" Text="Time Zone" Visible="False" />
         </td>
          <td align="left"  class = "TDClassControl">  
          <asp:CheckBox ID="chkUAC" runat="server" Text=" User Access Config" 
                  Visible="False" />
         </td>--%>
        </tr>
        <tr>
            <td align="center" class="style38">
                &nbsp;
            </td>
            <td  align="center">
                <div id="messageDiv" runat="server" clientidmode="Static" class="MessageClass" align="left">
                    <asp:Button ID="cmdUpdate" runat="server" Text="Update Network" CssClass="ButtonControl"
                        OnClick="cmdUpdate_Click" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
