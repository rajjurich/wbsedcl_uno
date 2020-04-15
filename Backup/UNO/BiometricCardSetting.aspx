<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="BiometricCardSetting.aspx.cs" Inherits="UNO.BiometricCardSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function IsNumber3(evnt, obj) {
            //alert(obj);
            var charCode = (evnt.which) ? evnt.which : event.keyCode
            if ((charCode >= 48 && charCode <= 57) || (charCode == 8)) {
                //alert(obj.value + String.fromCharCode(charCode));
                if (obj.value + String.fromCharCode(charCode) < 101) {
                    return true
                }
                else {
                    return false
                }
            }
            else {
                return false
            }
        }

        function cvLeftHand_ClientValidate(source, args) {
            if (document.getElementById("<%= Left1.ClientID %>").checked || document.getElementById("<%= Left2.ClientID %>").checked || document.getElementById("<%= Left3.ClientID %>").checked || document.getElementById("<%= Left4.ClientID %>").checked || document.getElementById("<%= Left5.ClientID %>").checked) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }

        }
        function cvRightHand_ClientValidate(source, args) {
            if (document.getElementById("<%= Right6.ClientID %>").checked || document.getElementById("<%= Right7.ClientID %>").checked || document.getElementById("<%= Right8.ClientID %>").checked || document.getElementById("<%= Right9.ClientID %>").checked || document.getElementById("<%= Right10.ClientID %>").checked) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="table10" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
        class="TableClass">
        <tr>
            <td align="right" style="width: 33%" class="LinkControl">
                <%--<asp:HyperLink ID="Back_to_View" CssClass="LinkControl" runat="server" NavigateUrl="~/CardDashboard.aspx"
                    ForeColor="Blue">Back to Card Managment Dashboard</asp:HyperLink>--%>
            </td>
        </tr>
    </table>
    <table id="table1" runat="server" width="100%" height="10%" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td>
                <h3 class="heading">
                    Bio-Metric and Card Settings</h3>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlWraper" runat="server" Width="100%">
        <table width="100%">
            <tr>
                <td align="center" style="width: 50%">
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlBioMetricSettings" runat="server">
                                    <table width="100%" border="0" class="TableClass">
                                        <tr>
                                            <td style="width: 70%" class="TDClassLabel">
                                                <span>Finger Quality Score</span>
                                            </td>
                                            <td style="width: 30%" class="TDClassControl">
                                                <asp:TextBox ID="txtScore" runat="server" Width="50px" MaxLength="3" onkeypress="return IsNumber3(event,this)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvScore" runat="server" ErrorMessage="*" ControlToValidate="txtScore"
                                                    ForeColor="Red" ToolTip="Finger Quality Score Required." CssClass="validation"
                                                    ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 70%" class="TDClassLabel">
                                                <span>Number of Retries</span>
                                            </td>
                                            <td style="width: 30%" class="TDClassControl">
                                                <asp:TextBox ID="txtRetries" runat="server" Width="50px" MaxLength="3" onkeypress="return IsNumber3(event,this)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvRetries" runat="server" ErrorMessage="*" ControlToValidate="txtRetries"
                                                    ForeColor="Red" ValidationGroup="Submit" ToolTip="Enter No. of Retries."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 70%" class="TDClassLabel">
                                                <span>Timeout</span>
                                            </td>
                                            <td style="width: 30%" class="TDClassControl">
                                                <asp:TextBox ID="txtTimeout" runat="server" Width="50px" MaxLength="3" onkeypress="return IsNumber3(event,this)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvTimeout" runat="server" ErrorMessage="*" ControlToValidate="txtTimeout"
                                                    ForeColor="Red" ValidationGroup="Submit" ToolTip="Enter Timeout."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlCardSettings" runat="server">
                                    <table width="100%" class="TableClass">
                                        <tr>
                                            <td style="width: 70%" class="TDClassLabel">
                                                <span style="width: 100%;">Select Native Sector for Card Writting</span>
                                            </td>
                                            <td style="width: 30%" class="TDClassControl">
                                                <asp:DropDownList ID="ddlNativeSector" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvNativeSector" runat="server" ErrorMessage="*"
                                                    ForeColor="Red" ControlToValidate="ddlNativeSector" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 70%" class="TDClassLabel">
                                                <span>Select ISO Sector for Card Writting</span>
                                            </td>
                                            <td style="width: 30%" class="TDClassControl">
                                                <asp:DropDownList ID="ddlISOSector" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvISOSector" runat="server" ErrorMessage="*" ControlToValidate="ddlISOSector"
                                                    ForeColor="Red" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="display: inline; min-width: 50%; width: 50%; min-height: 100%;">
                    <div  style="background-image:url('images/palm.bmp');background-repeat:no-repeat;width:30%; height: 274px;position:absolute;margin-left:-7%;z-index:-3;">
                        <!-- Left Hand Start-->
                        

                        <!-- Right Hand Start-->
                    </div>
                    <div style="z-index:1;width:30%;position:absolute;height:30%;opacity:0.6;margin-left:-7%;z-index:0;">
                        <asp:RadioButton ID="Left5" GroupName="LeftHand" runat="server" Style="position: relative;
                            left: 19px; top: 82px;" ValidationGroup="Submit" />
                        <asp:RadioButton ID="Left4" GroupName="LeftHand" runat="server" Style="position: relative;
                            left: 30px; top: 55px;" ValidationGroup="Submit" />
                        <asp:RadioButton ID="Left3" GroupName="LeftHand" runat="server" Style="position: relative;
                            left: 47px; top: 47px;" ValidationGroup="Submit" />
                        <asp:RadioButton ID="Left2" GroupName="LeftHand" runat="server" Style="position: relative;
                            left: 61px; top: 60px;" ValidationGroup="Submit" />
                        <asp:RadioButton ID="Left1" GroupName="LeftHand" runat="server" Style="position: relative;
                            left: 91px; top: 132px;" ValidationGroup="Submit" />
                        <asp:CustomValidator ID="cvLeftHand" runat="server" ErrorMessage="*" ValidationGroup="Submit"
                            ClientValidationFunction="cvLeftHand_ClientValidate"></asp:CustomValidator>
                        <!-- Left Hand End-->
                        <!-- Right Hand Start-->
                        <asp:RadioButton ID="Right6" GroupName="RightHand" runat="server" Style="position: relative;
                            left: 126px; top: 134px;" ValidationGroup="Submit" />
                        <asp:RadioButton ID="Right7" GroupName="RightHand" runat="server" Style="position: relative;
                            left: 154px; top: 61px;" ValidationGroup="Submit" />
                        <asp:RadioButton ID="Right8" GroupName="RightHand" runat="server" Style="position: relative;
                            left: 168px; top: 47px;" ValidationGroup="Submit" />
                        <asp:RadioButton ID="Right9" GroupName="RightHand" runat="server" Style="position: relative;
                            left: 186px; top: 55px;" ValidationGroup="Submit" />
                        <asp:RadioButton ID="Right10" GroupName="RightHand" runat="server" Style="position: relative;
                            left: 199px; top: 82px;" ValidationGroup="Submit" />
                        <asp:CustomValidator ID="cvRightHand" runat="server" ErrorMessage="*" ValidationGroup="Submit"
                            ClientValidationFunction="cvRightHand_ClientValidate"></asp:CustomValidator>
                            </div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="padding-top:11%">
                    <table style="height: 100%;" cellspacing="10px">
                        <tr>
                            <td>
                                <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="ButtonControl"
                                    Width="100px" OnClick="btnModify_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="ButtonControl" Width="100px"
                                    ValidationGroup="Submit" OnClick="btnSave_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="ButtonControl"
                                    Width="100px" OnClick="btnCancel_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="ButtonControl" Width="100px"
                                    OnClick="btnClose_Click" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
