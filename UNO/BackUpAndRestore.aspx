<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="BackUpAndRestore.aspx.cs" Inherits="UNO.BackUpAndRestore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <%--<link href="Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        function handleOperation() {
            var radioButtons = document.getElementById("<%=RBOperation.ClientID%>");
            var inputs = radioButtons.getElementsByTagName("input");

            var selected;
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].checked) {
                    selected = inputs[i];
                    break;
                }
            }
            if (selected.value == "BackUp") {
                document.getElementById("DivBackUp").style.display = "block"; document.getElementById("DivRestore").style.display = "none";


//                change on 13/Sept/2104 by Shrinith
                var txtClear1 = document.getElementById("<%=lblFilePath.ClientID%>")  
                if (txtClear1 != null) {
                    txtClear1.outerText = "";
                    txtClear1.value = "";
                    txtClear1.innerText = "";
                    txtClear1.innerHTML = "";
                    txtClear1.outerHTML = ""
                }

//                change on 13/Sept/2104 by Shrinith end
            
            }
            else if (selected.value == "Restore") {
                document.getElementById("DivBackUp").style.display = "none"; document.getElementById("DivRestore").style.display = "block";


                //                change on 13/Sept/2104 by Shrinith start

                var txtClear1 = document.getElementById("<%=lblFilePath.ClientID%>")
                if (txtClear1 != null) {
                    txtClear1.outerText = "";
                    txtClear1.value = "";
                    txtClear1.innerText = "";
                    txtClear1.innerHTML = "";
                    txtClear1.outerHTML = ""
                }

                //                change on 13/Sept/2104 by Shrinith end
            }
            else {
                document.getElementById("DivBackUp").style.display = "none"; document.getElementById("DivRestore").style.display = "none";

                var txtClear1 = document.getElementById("<%=lblFilePath.ClientID%>")
                if (txtClear1 != null) {
                    txtClear1.outerText = "";
                    txtClear1.value = "";
                    txtClear1.innerText = "";
                    txtClear1.innerHTML = "";
                    txtClear1.outerHTML = ""
                }
            
            }

        }
        function clearFunctionMessageDiv() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";

        }



    </script>
    <table id="table1" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
        class="TableClass">
        <tr>
            <td colspan="2" align="center" class="style23">
                <h3 class="heading">
                    BackUp and Restore</h3>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right" class="LinkControl">
                <asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="~/TADashboard.aspx"
                    ForeColor="Blue">Back to Access Management Dashboard</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td class="TDClassLabel" style="height: 10px; width: 50%">
                Select Operation :<label class="CompulsaryLabel">*</label>
            </td>
            <td class="TDClassControl" style="height: 10px; width: 50%; margin-left: 40px;">
                <asp:RadioButtonList ID="RBOperation" runat="server" RepeatDirection="Horizontal"
                    onClick="javascript:handleOperation();" ClientIDMode="Static">
                    <asp:ListItem Value="BackUp">BackUp</asp:ListItem>
                    <asp:ListItem Value="Restore">Restore</asp:ListItem>
                </asp:RadioButtonList>
                <%--<asp:CustomValidator ID="CVRBOperation" runat="server" ForeColor="Red" ClientValidationFunction = "checkRBModeType" ></asp:CustomValidator>--%>
            </td>
        </tr>
        <tr>
            <td class="TDClassLabel">
            </td>
            <td>
                <asp:Label ID="lblFilePath" runat="server"> </asp:Label>
            </td>
        </tr>
    </table>
    <div id="DivBackUp" style="display: none">
        <table id="table2" runat="server" width="100%" height="90%" border="0" cellpadding="0"
            cellspacing="0" class="TableClass">
            <tr>
                <td class="TDClassLabel" style="height: 10px; width: 50%">
                </td>
                <td class="TDClassControl" style="height: 10px; width: 50%; margin-left: 40px;">
                    <asp:Button CssClass="ButtonControl" ID="CmdDownLoad" runat="server" Text="Download Backup"
                        OnClick="CmdDownLoad_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="DivRestore" style="display: none">
        <table id="table3" runat="server" width="100%" height="90%" border="0" cellpadding="0"
            cellspacing="0" class="TableClass">
            <tr>
                <td class="TDClassLabel" style="height: 10px; width: 50%">
                    Upload File :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl" style="height: 10px; width: 50%; margin-left: 40px;">
                    <asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static" />
                </td>
            </tr>
            <tr>
                <td class="TDClassLabel" style="height: 10px; width: 50%">
                </td>
                <td class="TDClassControl" style="height: 10px; width: 50%; margin-left: 40px;">
                    <asp:Button CssClass="ButtonControl" ID="CmdUpload" runat="server" Text="Upload Backup"
                        OnClick="CmdUpload_Click" />
                </td>
            </tr>
        </table>
    </div>
    <table class="MessageContainerTable" width="100%">
        <tr>
            <td style="width: 100%" align="center" valign="middle">
                <div id="messageDiv" runat="server" clientidmode="Static" class="MessageClass">
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
