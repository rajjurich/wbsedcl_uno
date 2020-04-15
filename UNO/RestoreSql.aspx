<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="RestoreSql.aspx.cs" Inherits="UNO.RestoreSql" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .modalBackground
        {
            height: 100%; 
            background-color: #EBEBEB;
            filter: alpha(opacity=70);
            opacity: 0.9;
        }
    </style>


    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script type="text/javascript" language="javascript">
        function ShowPopup(msg) {
            alert(msg);
        }
        function ConfirmRestore() {

            var r = confirm("Do you want to restore database, existing database will be replaced!");
            if (r == true) {
                document.getElementById("<%=lblMessage.ClientID%>").value = "Sql restoring you may leave this page after completion restore information show on page.";
                return true;
            }
            else {
                return false;
            }
        }

        function RadioCheck(rb) {

            var gv = document.getElementById("<%=gvBackupData.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {

                if (rbs[i].type == "radio") {

                    if (rbs[i].checked && rbs[i] != rb) {

                        rbs[i].checked = false;

                        break;

                    }

                }

            }

        }
        //Added by Pooja Yadav
        function ResetValues() {
            $('#' + ["<%=txtDbName.ClientID%>", "<%=txtTodate.ClientID%>"].join(', #')).prop('value', "");
            document.getElementById('<%=txtTodate.ClientID%>').focus();
            document.getElementById('<%=txtDbName.ClientID%>').focus();
            document.getElementById('<%=lblMessage.ClientID%>').innerHTML = "";
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvBackupData.ClientID%>').focus();
            document.getElementById('<%=lblMsg.ClientID%>').innerHTML = "";
            document.getElementById('<%=txtPassword.ClientID%>').innerHTML = "";

            return false;
        }   
    </script>
    <script type="text/javascript">
        function updateProgress(percentage) {
            document.getElementById('ProgressBar').style.width = percentage + "%";
        }
    </script>
    <h3 id="lbltitle" runat="server" class="heading">
        Restore Sql Backup</h3>
    <div style="margin-top: 2%" runat="server">
        <center>
            <font color="red">
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label></font>
            <div class="DivEmpDetails">
                <asp:Panel ID="Panel3" runat="server" DefaultButton="btnSearch">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%; text-align: left;">
                                <asp:Button ID="btnRestore" runat="server" Text="Restore" CssClass="ButtonControl"
                                    OnClick="btnRestore_Click" />
                            </td>
                            <td style="width: 50%; text-align: right;">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" Style="float: right;" CssClass="ButtonControl"
                                    OnClientClick="return ResetValues();" />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;"
                                    CssClass="ButtonControl" OnClick="btnSearch_Click" />
                                <asp:TextBox ID="txtTodate" runat="server" Style="float: right;" CssClass="searchTextBox"
                                    onKeyPress="javascript: return false " onKeyDown="javascript: return false "></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCallStatus" runat="server" TargetControlID="txtTodate"
                                    WatermarkText="Date" WatermarkCssClass="watermark">
                                </ajaxToolkit:TextBoxWatermarkExtender>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTodate"
                                    PopupButtonID="txtTodate" Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                                <asp:TextBox ID="txtDbName" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                    TargetControlID="txtDbName" WatermarkText="DataBase Name" WatermarkCssClass="watermark">
                                </ajaxToolkit:TextBoxWatermarkExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                                <%--    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>--%>
                                <asp:GridView ID="gvBackupData" runat="server" AutoGenerateColumns="False" Width="100%"
                                    AllowPaging="True" ClientIDMode="Static" PageSize="10" GridLines="None">
                                    <RowStyle CssClass="gvRow" />
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                    <PagerStyle CssClass="gvPager" />
                                    <EmptyDataTemplate>
                                        <div>
                                            <span>No Records.</span>
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" SortExpression="Select">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="DeleteRows" GroupName="del" runat="server" ClientIDMode="Static"
                                                    onclick="RadioCheck(this);" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DataBase Name" SortExpression="dbName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("dbName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Created Date" SortExpression="createdDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("createdDate") %>'></asp:Label>
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("ctime") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: left; width: 15%;">
                                                    <span>Go To : </span>
                                                    <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChangePage">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="gvPrevious"
                                                        CssClass="ButtonControl" />
                                                    <asp:Label ID="lblShowing" runat="server" Text="Showing "></asp:Label>
                                                    <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="gvNext" CssClass="ButtonControl" />
                                                </td>
                                                <td style="text-align: right; width: 15%;">
                                                    <asp:Label ID="lblTotal" runat="server" Text="Total Records"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </PagerTemplate>
                                </asp:GridView>
                                <%--  </ContentTemplate>
                                   </asp:UpdatePanel>--%>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <%-- <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div>
                     
                          
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvBackupData" />
                    <asp:AsyncPostBackTrigger ControlID="btnRestore" />
                </Triggers>
            </asp:UpdatePanel>--%>
            <font color="red">
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></font>
        </center>
    </div>
    <asp:Panel ID="pnlPassword" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <table id="tblpassword" runat="server" visible="true">
                    <tr>
                        <td style="height: 34px;">
                            Do you want to restore database, existing database will be replaced. If Yes, please
                            enter admin's password to proceed : &nbsp&nbsp<asp:TextBox ID="txtPassword" runat="server"
                                TextMode="Password" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; height: 20px;">
                            <asp:Button ID="btnCheck" runat="server" CssClass="ButtonControl" Text="Verify" TabIndex="8"
                                OnClick="btnCheck_Click" />
                            <asp:Button ID="btncancel" runat="server" CssClass="ButtonControl" Text="Cancel"
                                OnClientClick="return ResetValues();" TabIndex="10" />
                        </td>
                    </tr>
                </table>
                <table id="tblConfirm" runat="server" visible="false">
                    <tr>
                        <td style="height: 34px;">
                            Are you sure you want to continue ? If Yes, then type "Yes" in the given box .
                        </td>
                        <td>
                            <asp:TextBox ID="txtConfirm" runat="server" MaxLength="3" ValidationGroup="Save"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtConfirm"
                            Display="None" ErrorMessage="Please type 'Yes' to proceed" ValidationGroup="Save">
                            </asp:RequiredFieldValidator>

                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                            TargetControlID="RequiredFieldValidator1"    PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            
                            <asp:CompareValidator ID="CompareValidator1" runat="server" Display="None"
                             ValueToCompare="yes" ControlToValidate="txtConfirm" ErrorMessage="Please type 'Yes' to proceed"
                             ValidationGroup="Save">
                            </asp:CompareValidator>

                            <ajaxToolkit:ValidatorCalloutExtender ID="vceComptxtConf" runat="server" 
                            TargetControlID="CompareValidator1"   PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnConfirm" runat="server" Text="Restore" CssClass="ButtonControl"
                                OnClick="btnConfirm_Click" ValidationGroup="Save" />
                            <asp:Button ID="btnCnfCancel" runat="server" CssClass="ButtonControl" Text="Cancel"
                                OnClientClick="return ResetValues();" TabIndex="10" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td style="height: 20px; text-align: center;" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRestore" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <div class="progress progress-striped active progress-success" style="height: 43px">
        <div id="ProgressBar" class="progress-bar" role="progressbar" runat="server" aria-valuemin="0"
            aria-valuemax="100" style="width: 0%">
        </div>
    </div>
    <asp:Button ID="Button7" runat="server" Style="display: none;" Text="test" />
    <asp:Button ID="btntadd" runat="server" Text="Yes" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="mpePassword" runat="server" Enabled="true" PopupControlID="pnlPassword"
        TargetControlID="Button7" OkControlID="btntadd" CancelControlID="btncancel" BackgroundCssClass="modalBackground"
        DropShadow="true">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
