<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DirectoryPopup.aspx.cs"    Inherits="UNO.DirectoryPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" href="Styles/default.css" />
    <script language="javascript" type="text/javascript">
  
        function SelectAndClose() {
            var txtValue = document.getElementById('_browseTextBox');
            var txtId = document.getElementById("hdnPathTextBoxId").value;
            window.opener.document.getElementById(txtId).value = txtValue.value;
            window.self.close();
            return false;
        }

    </script>
    <title>Browse Directory</title>
      <style type="text/css">
      
        .tableOutlineWt
        {
            border-right: #cccccc 1px solid;
            border-top: #666666 1px solid;
            margin-top: 0px;
            overflow: auto;
            border-left: #333333 1px solid;
            padding-top: 0px;
            border-bottom: #cccccc 1px solid;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table  border="0">
          
           
            <tr>
                <td>
                    <asp:TextBox ID="_browseTextBox" runat="server" CssClass="toolbar" Width="250px" />
                </td>
            </tr>
            <tr>
                <td>
                    <div class="tableOutlineWt" style="width: 250px; height: 180px; background-color: white">
                        <table cellspacing="0" cellpadding="4" bgcolor="#ffffff" border="0">
                            <tr>
                                <td>
                                    <asp:TreeView ID="TrDirectoryView" runat="server" Height="150px" NodeIndent="16"
                                        Width="180px">
                                        <ParentNodeStyle Font-Bold="False" ImageUrl="~/Images/Folder.jpg" />
                                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                            VerticalPadding="0px" />
                                        <LeafNodeStyle ImageUrl="~/Images/Folder.jpg" />
                                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                            NodeSpacing="0px" VerticalPadding="2px" ImageUrl="~/Images/Folder.jpg" />
                                    </asp:TreeView>
                                    <asp:HiddenField ID="hdnPathTextBoxId" runat="server" ClientIDMode="Static" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="btnClose" runat="server" CssClass="ButtonControl" Text="Ok" TabIndex="6" Width="50px"
                        OnClientClick="SelectAndClose();" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
