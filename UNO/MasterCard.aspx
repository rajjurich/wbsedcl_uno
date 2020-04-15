<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="MasterCard.aspx.cs" Inherits="UNO.MasterCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation1.js"></script>
    <script language="javascript" type="text/javascript">

        //        function ValidateData() {           
        //            if (!Page_ClientValidate())
        //                return;
        //        }

        function CreateMasterCard() {
            try {
                if (!Page_ClientValidate())
                    return;

                var obj_Card;
                obj_Card = new ActiveXObject("ContactlessCardRW.Card");

                var DataIntial = "";
                var Compdetails = new Array();
                var MasterCarddetails = new Array();

                Compdetails[0] = document.getElementById("txtCompanyCode").value;
                Compdetails[1] = document.getElementById("txtSiteId").value;
                //                alert(Compdetails[0]);
                //                alert(Compdetails[1]);
                MasterCarddetails[0] = document.getElementById("txtUserId").value;
                MasterCarddetails[1] = document.getElementById("txtPassword").value;
                MasterCarddetails[2] = document.getElementById("txtMasterKey").value;
                //                alert(MasterCarddetails[0]);
                //                alert(MasterCarddetails[1]);
                //                alert(MasterCarddetails[2]);

                DataIntial = obj_Card.Initialise();
                if (DataIntial != "") {
                    alert("Omnikey reader not connected or Error in card Initialization");
                    return false;
                }

                var Data = "";
                Data = obj_Card.ConnectToCard();
                if (Data != "") {
                    alert("Error in connecting");
                    return false;
                }

                Data = obj_Card.WriteMasterCard(Compdetails, MasterCarddetails);
                if (DataIntial != "") {
                    alert("Error in creating master card");
                    return false;
                }
                else {
                    alert("Master card created successfully");
                    return false;
                }
            }
            catch (e) {
                alert(e.message);
                return false;
            }
        }

    </script>
    <object id="Card" classid="CLSID:E421E41E-100C-48AB-8CC7-5B6B78DC1C76" codebase="ContactlessCardRWPerso.CAB#version=1,0,0,0">
    </object>
    <table id="table1" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
        class="TableClass">
        <tr>
            <td align="right" style="width: 33%" class="LinkControl">
                <%--<asp:HyperLink ID ="Back_to_Dashboard" CssClass="LinkControl" runat="server" 
        NavigateUrl="~/CardDashboard.aspx" ForeColor="Blue">Back to Card Data DashBoard</asp:HyperLink>--%>
            </td>
        </tr>
    </table>
    <table id="table2" runat="server" width="100%" height="10%" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td>
                <h3 class="heading">
                    Configure Master Card</h3>
            </td>
        </tr>
    </table>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Personalization" runat="server" ScrollBars="None" Width="90%" 
                BorderWidth="0" BorderColor="Gray" HorizontalAlign="Center" align="Center" CssClass="srcColor"
                ClientIDMode="Static">
                <table id="table3" runat="server" width="95%" border="0" cellpadding="0" cellspacing="0"
                    class="TableClass">
                    <br />
                    <tr>
                        <td class="TDClassLabel" style="height: 10px">
                            Company Code :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px">
                            <asp:TextBox CssClass="TextControl" ID="txtCompanyCode" MaxLength="4" Text="0000"
                                runat="server" onkeypress="return IsNumber(event)" TabIndex="1" ClientIDMode="Static"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvcompcd" runat="server" ErrorMessage="Please enter Company Code"
                                ControlToValidate="txtCompanyCode" SetFocusOnError="True" Display="Dynamic" ForeColor="Red"
                                ValidationGroup="Card"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Company Code length must be 4 characters"
                                ControlToValidate="txtCompanyCode" ValidationExpression="^[0-9\s]{4}$">
                            </asp:RegularExpressionValidator>
                        </td>
                        <td class="TDClassLabel" style="height: 10px">
                            Site ID :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px">
                            <asp:TextBox CssClass="TextControl" ID="txtSiteId" MaxLength="3" Text="000" runat="server"
                                onkeypress="return IsNumber(event)" TabIndex="2" ClientIDMode="Static"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvsiteid" runat="server" ErrorMessage="Please enter Site Id"
                                ControlToValidate="txtSiteId" SetFocusOnError="True" Display="Dynamic" ForeColor="Red"
                                ValidationGroup="Card"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Site Id length must be 3 characters"
                                ControlToValidate="txtSiteId" ValidationExpression="^[0-9\s]{3}$">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px">
                            User ID :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px">
                            <asp:TextBox CssClass="TextControl" ID="txtUserId" MaxLength="6" runat="server" onkeypress="return IsAlphanumericWithoutspace(event)"
                                TabIndex="3" ClientIDMode="Static"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvUid" runat="server" ErrorMessage="Please enter User Id"
                                ControlToValidate="txtUserId" SetFocusOnError="True" Display="Dynamic" ForeColor="Red"
                                ValidationGroup="Card"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegExp1" runat="server" ErrorMessage="User Id length must be 6 characters"
                                ControlToValidate="txtUserId" ValidationExpression="^[a-zA-Z0-9\s]{6}$">
                            </asp:RegularExpressionValidator>
                        </td>
                        <td class="TDClassLabel" style="height: 10px">
                            Confirm User ID :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px">
                            <asp:TextBox CssClass="TextControl" ID="txtConfirmUId" MaxLength="6" runat="server"
                                onkeypress="return IsAlphanumericWithoutspace(event)" TabIndex="4" ClientIDMode="Static"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Confirm User Id"
                                ControlToValidate="txtConfirmUId" SetFocusOnError="True" Display="Dynamic" ForeColor="Red"
                                ValidationGroup="Card"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="compval" Display="dynamic" ControlToValidate="txtConfirmUId"
                                ControlToCompare="txtUserId" ForeColor="red" BackColor="yellow" Type="String"
                                Text="The User Ids you typed do not match" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px">
                            Password :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px">
                            <asp:TextBox CssClass="TextControl" ID="txtPassword" MaxLength="6" runat="server"
                                onkeypress="return IsAlphanumericWithoutspace(event)" TabIndex="5" ClientIDMode="Static"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvPass" runat="server" ErrorMessage="Please enter Password"
                                ControlToValidate="txtPassword" SetFocusOnError="True" Display="Dynamic" ForeColor="Red"
                                ValidationGroup="Card"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Password length must be 6 characters"
                                ControlToValidate="txtPassword" ValidationExpression="^[a-zA-Z0-9\s]{6}$">
                            </asp:RegularExpressionValidator>
                        </td>
                        <td class="TDClassLabel" style="height: 10px">
                            Confirm Password :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px">
                            <asp:TextBox CssClass="TextControl" ID="txtConfirmPass" MaxLength="6" runat="server"
                                onkeypress="return IsAlphanumericWithoutspace(event)" TabIndex="6" ClientIDMode="Static"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Confirm Password"
                                ControlToValidate="txtConfirmPass" SetFocusOnError="True" Display="Dynamic" ForeColor="Red"
                                ValidationGroup="Card"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="comval1" Display="dynamic" ControlToValidate="txtConfirmPass"
                                ControlToCompare="txtPassword" ForeColor="red" BackColor="yellow" Type="String"
                                Text="The Passwords you typed do not match" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px">
                            Master Key :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px">
                            <asp:TextBox CssClass="TextControl" ID="txtMasterKey" MaxLength="12" runat="server"
                                onkeypress="return IsAlphanumericWithoutspace(event)" TabIndex="7" ClientIDMode="Static"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvMaster" runat="server" ErrorMessage="Please enter Master Key"
                                ControlToValidate="txtMasterKey" SetFocusOnError="True" Display="Dynamic" ForeColor="Red"
                                ValidationGroup="Card"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Master Key length must be 12 characters"
                                ControlToValidate="txtMasterKey" ValidationExpression="^[a-zA-Z0-9\s]{12}$">
                            </asp:RegularExpressionValidator>
                        </td>
                        <td class="TDClassLabel" style="height: 10px">
                            Confirm Master Key :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px">
                            <asp:TextBox CssClass="TextControl" ID="txtConfirmMasterKey" MaxLength="12" runat="server"
                                onkeypress="return IsAlphanumericWithoutspace(event)" TabIndex="8" ClientIDMode="Static"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Confirm Master Key"
                                ControlToValidate="txtConfirmMasterKey" SetFocusOnError="True" Display="Dynamic"
                                ForeColor="Red" ValidationGroup="Card"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="comval2" Display="dynamic" ControlToValidate="txtMasterKey"
                                ControlToCompare="txtConfirmMasterKey" ForeColor="red" BackColor="yellow" Type="String"
                                Text="The Master Keys you typed do not match" runat="server" />
                        </td>
                    </tr>
                </table>
                <br />
                <table id="table4" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                    class="TableClass">
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 40%">
                        </td>
                        <td style="height: 10px; width: 10%">
                            <asp:Button ID="btnOk" runat="server" ClientIDMode="Static" Text="Ok" TabIndex="9"
                                Width="90%" CssClass="ButtonControl" OnClientClick="return CreateMasterCard()" />
                            <%-- ValidationGroup ="Card"  OnClientClick="return ValidateData()"/>--%>
                        </td>
                        <td style="height: 10px; width: 10%">
                            <asp:Button ID="btnCancel" runat="server" ClientIDMode="Static" Text="Cancel" TabIndex="10"
                                Width="90%" CssClass="ButtonControl" />
                        </td>
                        <td class="TDClassLabel" style="height: 10px; width: 40%">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            Configuring Card please wait ............
        </ProgressTemplate>
    </asp:UpdateProgress>
     <script src="Scripts/Choosen/chosen.jquery.js" type="text/javascript"></script>
    <script src="Scripts/Choosen/docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "95%" }
            //            chosen:showing_dropdown
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
    </script>
</asp:Content>
