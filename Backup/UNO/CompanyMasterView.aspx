<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="CompanyMasterView.aspx.cs" Inherits="UNO.CompanyMasterView" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css>"--%>
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drops
        {
            left: -9000px;
        }
    </style>
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script type="text/javascript">
        //Resolved Bug 270 - Swapnil Start
        function pageLoad() {
            var popup = $find('<%=mpeAddCompany.ClientID%>');
            popup.add_shown(SetFocus);
        }

        function SetFocus() {
            $get('<%=txtCompanyIDAdd.ClientID%>').focus();
        }
        //Resolved Bug 270 - Swapnil end
        function maximumLimit(sender, value) {
            try {
                if (sender.value.length > value) {
                    sender.value = sender.value.substr(0, value);
                }
            }
            catch (ex) {
                alert(ex.Message);
            }
        }

        function cHK_CompEdit() {

            var IsCheched = document.getElementById('<%=ChkAddressEdit.ClientID%>').checked;
            if (IsCheched) {

                $('#ddlHOStateEdit').addClass("chosen-select");
                document.getElementById('txtHOAddressEdit').value = document.getElementById('txtRegisteredAddressEdit').value
                document.getElementById('txtHOPinEdit').value = document.getElementById('txtRegisteredPinEdit').value
                document.getElementById('txtHOCityEdit').value = document.getElementById('txtRegisteredCityEdit').value
                document.getElementById('ddlHOStateEdit').value = document.getElementById('ddlRegisteredStateEdit').value
                document.getElementById('txtHOPhone1Edit').value = document.getElementById('txtRegisteredPhone1Edit').value
                document.getElementById('txtHOPhone2Edit').value = document.getElementById('txtRegisteredPhone2Edit').value
                document.getElementById('txtHOAddressEdit').disabled = true;
                document.getElementById('txtHOPinEdit').disabled = true;
                document.getElementById('txtHOCityEdit').disabled = true;
                $('#ddlHOStateEdit').prop('disabled', true).trigger("chosen:updated");
                document.getElementById('txtHOPhone1Edit').disabled = true;
                document.getElementById('txtHOPhone2Edit').disabled = true;

            }
            else {
                $('#ddlHOStateEdit').addClass("chosen-select");
                document.getElementById('txtHOAddressEdit').value = ""
                document.getElementById('txtHOPinEdit').value = ""
                document.getElementById('txtHOCityEdit').value = ""
                document.getElementById('ddlHOStateEdit').value = ""
                $("select" + "#" + "<%=ddlHOStateEdit.ClientID%>").prop('selectedIndex', 0);
                document.getElementById('txtHOPhone1Edit').value = ""
                document.getElementById('txtHOPhone2Edit').value = ""
                document.getElementById('txtHOAddressEdit').disabled = false;
                document.getElementById('txtHOPinEdit').disabled = false;
                document.getElementById('txtHOCityEdit').disabled = false;
                $('#ddlHOStateEdit').prop('disabled', false).trigger("chosen:updated");
                document.getElementById('txtHOPhone1Edit').disabled = false;
                document.getElementById('txtHOPhone2Edit').disabled = false;
            }
            validateChosen();
        }


        function cHK_CompAdd() {

            var IsChecked = document.getElementById('<%=ChkAddressAdd.ClientID%>').checked;

            if (IsChecked) {
                document.getElementById('txtHOAddressAdd').value = document.getElementById('txtRegisteredAddressAdd').value;
                document.getElementById('txtHOPinAdd').value = document.getElementById('txtRegisteredPinAdd').value;
                document.getElementById('txtHOCityAdd').value = document.getElementById('txtRegisteredCityAdd').value;
                document.getElementById('ddlHOStateAdd').value = document.getElementById('ddlRegisteredStateAdd').value;
                document.getElementById('txtHOPhone1Add').value = document.getElementById('txtRegisteredPhone1Add').value;
                document.getElementById('txtHOPhone2Add').value = document.getElementById('txtRegisteredPhone2Add').value;
                document.getElementById('txtHOAddressAdd').disabled = true;
                document.getElementById('txtHOPinAdd').disabled = true;
                document.getElementById('txtHOCityAdd').disabled = true;
                document.getElementById('<%=ddlHOStateAdd.ClientID%>').disabled = true;
                $('#ddlHOStateAdd').prop('disabled', true).trigger("chosen:updated");
                document.getElementById('txtHOPhone1Add').disabled = true;
                document.getElementById('txtHOPhone2Add').disabled = true;
            }
            else {

                document.getElementById('txtHOAddressAdd').value = "";
                document.getElementById('txtHOPinAdd').value = "";
                document.getElementById('txtHOCityAdd').value = ""
                document.getElementById('ddlHOStateAdd').value = ""
                $("select" + "#" + "<%=ddlHOStateAdd.ClientID%>").prop('selectedIndex', 0);
                document.getElementById('txtHOPhone1Add').value = ""
                document.getElementById('txtHOPhone2Add').value = ""
                document.getElementById('txtHOAddressAdd').disabled = false;
                document.getElementById('txtHOPinAdd').disabled = false;
                document.getElementById('txtHOCityAdd').disabled = false;
                $('#ddlHOStateAdd').prop('disabled', false).trigger("chosen:updated");
                document.getElementById('txtHOPhone1Add').disabled = false;
                document.getElementById('txtHOPhone2Add').disabled = false;
            }
        }

     
    </script>
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvCompany.ClientID %>');

            // get the bounds of both the gridview and the progress div
            var gridViewBounds = Sys.UI.DomElement.getBounds(gridView);
            var updateProgressDivBounds = Sys.UI.DomElement.getBounds(updateProgressDiv);

            //    do the math to figure out where to position the element (the center of the gridview)
            var x = gridViewBounds.x + Math.round(gridViewBounds.width / 2) - Math.round(updateProgressDivBounds.width / 2);
            var y = gridViewBounds.y + Math.round(gridViewBounds.height / 2) - Math.round(updateProgressDivBounds.height / 2);

            //    set the progress element to this position
            Sys.UI.DomElement.setLocation(updateProgressDiv, x, y);
        }

        function onUpdated() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it invisible
            updateProgressDiv.style.display = 'none';
        }
        function ResetAll() {
            $('#' + ["<%=txtCompanyName.ClientID%>", "<%=txtCompanyID.ClientID%>"].join(', #')).prop('value', "");           
            document.getElementById('<%=btnSearch.ClientID%>').click();           
           
        }

    </script>
    <script type="text/jscript">
        $(document).ready(function () {
            //called when key is pressed in textbox
            $("#txtHOPinAdd").keypress(function (e) {
                //if the letter is not digit then display error and don't type anything
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    //display error message
                    //                     $("#errmsg").html("Digits Only").show().fadeOut("slow");
                    return false;
                }
            });
        });
    </script>
    <style>
        .textBold
        {
            text-align: center;
            vertical-align: middle;
            font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;
            font-weight: bold;
            color: Black;
            font-size: x-large;
        }
        
        .watermark
        {
            color: Gray;
            font-size: xx-small;
            height: 17px;
            width: 120px;
            border-radius: 15px;
            margin-right: 10px;
        }
        .searchTextBox
        {
            height: 17px;
            width: 120px;
            font-size: xx-small;
            border-radius: 15px;
            margin-right: 10px;
        }
        .DivEmpDetails
        {
            text-align: center;
            width: 95%; /*border: 1px solid #333333;*/
            border-radius: 15px;
            background-color: #47A3DA;
            margin: 10px 10px 10px 10px;
            padding: 10px 10px 10px 10px; /*min-height: 200px;*/ /*font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;*/
            box-shadow: 10px 10px 5px #888888;
        }
        .gvHeader
        {
            background-color: transparent;
            border: 0px solid #66B7F5;
            max-height: 29px;
            height: 29px;
            min-height: 29px;
        }
        gvAlternateRow
        {
        }
        .gvRow
        {
            border-bottom: 1px solid #C3C3C3;
            max-height: 26px;
            height: 26px;
            min-height: 26px;
        }
        .gvPager
        {
            vertical-align: bottom;
        }
        .center
        {
            text-align: center;
        }
    </style>
    <!--[if IE]>
<style>
    .DivEmpDetails {
                     text-align: center;
            width: 95%;
            border: 1px solid #333333;
            border-radius: 15px;
            margin: 10px 10px 10px 10px;
            padding: 10px 10px 10px 10px;
            background-color:#53AEF3;
            font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;
    }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Company View" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:UpdatePanel ID="UpnlMain" runat="server">
        <ContentTemplate>
            <center>
                <div class="DivEmpDetails">
                    <asp:Panel ID="Panel3" runat="server" DefaultButton="btnSearch">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 50%; text-align: left;">
                                    <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click" />
                                    <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                                        OnClick="btnDelete_Click" />
                                </td>
                                <td style="width: 50%; text-align: right;">
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                                        OnClientClick="return ResetAll();" onclick="btnReset_Click" />
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;"
                                        CssClass="ButtonControl" OnClick="btnSearch_Click" />
                                    <asp:TextBox ID="txtCompanyName" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCompanyName" runat="server" TargetControlID="txtCompanyName"
                                        WatermarkText="Description" WatermarkCssClass="watermark">
                                    </ajaxToolkit:TextBoxWatermarkExtender>
                                    <asp:TextBox ID="txtCompanyID" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCompanyID" runat="server" TargetControlID="txtCompanyID"
                                        WatermarkText="ID" WatermarkCssClass="watermark">
                                    </ajaxToolkit:TextBoxWatermarkExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                                    <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                                        <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvCompany" runat="server" AutoGenerateColumns="False" Width="100%"
                                                GridLines="None" AllowPaging="True" OnPageIndexChanging="gvCompany_PageIndexChanging"
                                                OnRowCommand="gvCompany_RowCommand" OnRowDataBound="gvCompany_RowDataBound">
                                                <RowStyle CssClass="gvRow" />
                                                <HeaderStyle CssClass="gvHeader" />
                                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                                <PagerStyle CssClass="gvPager" />
                                                <EmptyDataTemplate>
                                                    <div>
                                                        <span>No Companies found.</span>
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select" SortExpression="Select" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                                            <asp:HiddenField ID="hdnFlag" runat="server" Value='<%#Eval("flag")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" SortExpression="Edit" ItemStyle-Width="5%" HeaderStyle-CssClass="center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                                Text="Edit" ForeColor="#3366FF" CommandArgument='<%#Eval("company_id")%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="company_id" HeaderText="ID" SortExpression="ID" ItemStyle-Width="5%">
                                                        <%--  <ItemStyle HorizontalAlign="Left" Width="10%"  />--%>
                                                        <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="company_name" HeaderText="Description" ItemStyle-Wrap="true"
                                                        SortExpression="Name">
                                                        <ItemStyle Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="company_address" HeaderText="Address" ItemStyle-Width="10%"
                                                        ItemStyle-Wrap="true" Visible="false" SortExpression="Address">
                                                        <ItemStyle Width="10%" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="COMPANY_PIN" HeaderText="Pin" SortExpression="Pin" ItemStyle-Width="10%">
                                                        <ItemStyle Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="COMPANY_CITY" HeaderText="City" ItemStyle-Width="10%"
                                                        SortExpression="City">
                                                        <ItemStyle Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="COMPANY_STATE" HeaderText="State" ItemStyle-Width="10%"
                                                        SortExpression="State">
                                                        <ItemStyle Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="COMPANY_PHONE1" HeaderText="Phone1" ItemStyle-Width="10%"
                                                        SortExpression="Phone1" Visible="false">
                                                        <ItemStyle Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="COMPANY_PHONE2" HeaderText="Phone2" ItemStyle-Width="10%"
                                                        SortExpression="Phone2" Visible="false">
                                                        <ItemStyle Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="COMPANY_RO_ADDRESS1" HeaderText="ROAddress" Visible="false"
                                                        SortExpression="ROAddress" />
                                                    <asp:BoundField DataField="COMPANY_RO_PIN" HeaderText="ROPin" Visible="false" SortExpression="ROPin" />
                                                    <asp:BoundField DataField="COMPANY_RO_CITY" HeaderText="ROCity" Visible="false" SortExpression="ROCity" />
                                                    <asp:BoundField DataField="COMPANY_RO_STATE" HeaderText="ROState" Visible="false"
                                                        SortExpression="ROState" />
                                                    <asp:BoundField DataField="COMPANY_RO_PHONE1" HeaderText="ROPhone1" Visible="false"
                                                        SortExpression="ROPhone1" />
                                                    <asp:BoundField DataField="COMPANY_RO_PHONE2" HeaderText="ROPhone2" Visible="false"
                                                        SortExpression="ROPhone2" />
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
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
                                        TargetControlID="UpdatePanel2">
                                        <Animations>
                        <OnUpdating>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="btnSearch" Enabled="false" />
                                <ScriptAction Script="onUpdating();" />  
                                <FadeOut Duration="1.0" Fps="24" minimumOpacity=".5" />
                            </Parallel> 
                        </OnUpdating>
                        <OnUpdated>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="btnSearch" Enabled="true" />
                                <ScriptAction Script="onUpdated();" /> 
                                <FadeIn Duration="1.0" Fps="24"  minimumOpacity=".5"/>
                            </Parallel> 
                        </OnUpdated>
                                        </Animations>
                                    </ajaxToolkit:UpdatePanelAnimationExtender>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div>
                            <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <%--  <asp:PostBackTrigger ControlID="btnAdd" />--%>
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                        <asp:AsyncPostBackTrigger ControlID="gvCompany" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="btnSubmitEdit" />
                    </Triggers>
                </asp:UpdatePanel>
            </center>
            <asp:Panel ID="pnlAddCompany" runat="server" CssClass="PopupPanel">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table id="table4" runat="server" width="100%" height="90%" border="0" cellpadding="0"
                            cellspacing="0" class="TableClass">
                            <tr>
                                <td style="text-align: right;">
                                    Company ID :<label class="CompulsaryLabel">*</label>
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtCompanyIDAdd" runat="server" onkeypress="return IsAlphanumericWithoutspace(event)"
                                        CssClass="TextControl" MaxLength="8" TabIndex="1" Style="text-transform: uppercase;"
                                        ClientIDMode="Static" ValidationGroup="Add"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtCompanyIDAdd" runat="server" ControlToValidate="txtCompanyIDAdd"
                                        Display="None" ErrorMessage="Please enter Company Id." ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtCompanyIDAdd" runat="server" TargetControlID="rfvtxtCompanyIDAdd"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td style="text-align: right;">
                                    Company Description :<label class="CompulsaryLabel">*</label>
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtDescriptionAdd" runat="server" onkeypress="return IsAlphanumeric(event)"
                                        CssClass="TextControl" MaxLength="50" TabIndex="2" Style="text-transform: capitalize;"
                                        ClientIDMode="Static" ValidationGroup="Add" onkeyup="cHK_CompAdd()"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtDescriptionAdd" runat="server" ControlToValidate="txtDescriptionAdd"
                                        Display="None" ErrorMessage="Please enter Company Description." ForeColor="Red"
                                        SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtDescriptionAdd" runat="server"
                                        TargetControlID="rfvtxtDescriptionAdd" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    Address :
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtRegisteredAddressAdd" runat="server" Style="text-transform: capitalize;
                                        min-width: 250px; max-width: 250px; min-height: 50px; max-height: 50px" CssClass="TextControl"
                                        TabIndex="3" Height="50px" MaxLength="150" TextMode="MultiLine" ClientIDMode="Static"
                                        ValidationGroup="Add" onkeypress="maximumLimit(this, 149);" Width="250px" onkeyup="cHK_CompAdd()"></asp:TextBox>
                                </td>
                                <td style="text-align: right;">
                                    City :
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtRegisteredCityAdd" runat="server" CssClass="TextControl" MaxLength="15"
                                        TabIndex="4" onkeypress="return IsChar(event)" Style="text-transform: capitalize;"
                                        ClientIDMode="Static" ValidationGroup="Add" onkeyup="cHK_CompAdd()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    Pin:
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtRegisteredPinAdd" runat="server" onkeypress="return IsNumber(event)"
                                        CssClass="TextControl" MaxLength="8" TabIndex="5" Style="text-transform: capitalize;"
                                        ClientIDMode="Static" ValidationGroup="Add" onkeyup="cHK_CompAdd()"></asp:TextBox>
                                </td>
                                <td style="text-align: right;">
                                    State :
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:DropDownList ID="ddlRegisteredStateAdd" runat="server" ClientIDMode="Static"
                                        TabIndex="6" ValidationGroup="Add" onchange="cHK_CompAdd()" class="chosen-select">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlRegisteredStateAdd" runat="server" ControlToValidate="ddlRegisteredStateAdd"
                                        Display="None" ErrorMessage="Please select State." ForeColor="Red" InitialValue="null"
                                        SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlRegisteredStateAdd" runat="server"
                                        TargetControlID="rfvddlRegisteredStateAdd" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <caption>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Phone No 1 :
                                    </td>
                                    <td style="padding-left: 10px;">
                                        <asp:TextBox ID="txtRegisteredPhone1Add" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                            MaxLength="15" onkeypress="return IsNumber(event)" TabIndex="7" ValidationGroup="Add"
                                            onkeyup="cHK_CompAdd()"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        Phone No 2 :
                                    </td>
                                    <td style="padding-left: 10px;">
                                        <asp:TextBox ID="txtRegisteredPhone2Add" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                            MaxLength="15" onkeypress="return IsNumber(event)" TabIndex="8" ValidationGroup="Add"
                                            onkeyup="cHK_CompAdd()"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <b style="text-align: justify">Same Address</b>
                                    </td>
                                    <td colspan="3" style="padding-left: 10px;">
                                        <asp:CheckBox ID="ChkAddressAdd" runat="server" onclick="cHK_CompAdd()" Style="text-align: left"
                                            TabIndex="9" Text="Click here is same as above" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Head Office Address :
                                    </td>
                                    <td style="padding-left: 10px;">
                                        <asp:TextBox ID="txtHOAddressAdd" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                            Height="50px" MaxLength="50" onkeypress="return IsAlphanumericN(event)" Style="text-transform: capitalize;
                                            min-width: 250px; max-width: 250px; min-height: 50px; max-height: 50px" TabIndex="10"
                                            TextMode="MultiLine" ValidationGroup="Add" Width="250px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        City :
                                    </td>
                                    <td style="padding-left: 10px;">
                                        <asp:TextBox ID="txtHOCityAdd" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                            MaxLength="15" onkeypress="return IsChar(event)" Style="text-transform: capitalize;"
                                            TabIndex="11" ValidationGroup="Add"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Pin :
                                    </td>
                                    <td style="padding-left: 10px;">
                                        <asp:TextBox ID="txtHOPinAdd" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                            MaxLength="8" onkeypress="return IsNumber(event)" TabIndex="12" ValidationGroup="Add"></asp:TextBox>
                                        <%--           <asp:CompareValidator ID="cv" runat="server" ControlToValidate="txtHOPinAdd" Type="Integer"
   Operator="DataTypeCheck" ValidationGroup="Add" ErrorMessage="Value must be an number!" Display="None"/>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="cv"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                                    </td>
                                    <td style="text-align: right;">
                                        State :
                                    </td>
                                    <td style="padding-left: 10px;">
                                        <asp:DropDownList ID="ddlHOStateAdd" runat="server" ClientIDMode="Static" TabIndex="13"
                                            ValidationGroup="Add" class="chosen-select">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvddlHOStateAdd" runat="server" ControlToValidate="ddlHOStateAdd"
                                            Display="None" ErrorMessage="Please select State" ForeColor="Red" InitialValue="null"
                                            SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlHOStateAdd" runat="server" PopupPosition="Right"
                                            TargetControlID="rfvddlHOStateAdd">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Phone No 1 :
                                    </td>
                                    <td style="padding-left: 10px;">
                                        <asp:TextBox ID="txtHOPhone1Add" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                            MaxLength="15" onkeypress="return IsNumber(event)" TabIndex="14" ValidationGroup="Add"></asp:TextBox>
                                        <%--  <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtHOPhone1Add" Type="Integer"
   Operator="DataTypeCheck" ValidationGroup="Add" ErrorMessage="Value must be an number!" Display="None"/>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="CompareValidator1"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                                    </td>
                                    <td style="text-align: right;">
                                        Phone No 2 :
                                    </td>
                                    <td style="padding-left: 10px;">
                                        <asp:TextBox ID="txtHOPhone2Add" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                            MaxLength="15" onkeypress="return IsNumber(event)" TabIndex="15" ValidationGroup="Add"></asp:TextBox>
                                        <%--     <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtHOPhone2Add" Type="Integer"
   Operator="DataTypeCheck" ValidationGroup="Add" ErrorMessage="Value must be an number!" Display="None"/>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="CompareValidator2"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center;">
                                        <asp:Button ID="btnSubmitAdd" runat="server" CssClass="ButtonControl" OnClick="btnSubmitAdd_Click"
                                            TabIndex="16" Text="Save" ValidationGroup="Add" />
                                        <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" OnClick="btnCancelAdd_Click"
                                            TabIndex="17" Text="Cancel" />
                                        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center;">
                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Text="" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </caption>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Button runat="server" ID="btntempAdd" Text="New" Style="display: none;" />
            <ajaxToolkit:ModalPopupExtender ID="mpeAddCompany" runat="server" TargetControlID="btntempAdd"
                PopupControlID="pnlAddCompany" BackgroundCssClass="modalBackground" Enabled="true">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlEditCompany" runat="server" CssClass="PopupPanel">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table id="table1" runat="server" width="100%" height="90%" border="0" cellpadding="0"
                            cellspacing="0" class="TableClass">
                            <tr>
                                <td style="text-align: right;">
                                    Company ID :<label class="CompulsaryLabel">*</label>
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtCompanyIDEdit" runat="server" onkeypress="return IsAlphanumericWithoutspace(event)"
                                        CssClass="TextControl" MaxLength="8" TabIndex="1" Style="text-transform: uppercase;"
                                        ClientIDMode="Static" ValidationGroup="Edit"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtCompanyIDEdit" runat="server" ControlToValidate="txtCompanyIDEdit"
                                        Display="None" ErrorMessage="Please enter Company Id." ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtCompanyIDEdit" runat="server"
                                        TargetControlID="rfvtxtCompanyIDEdit" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td style="text-align: right;">
                                    Company Description :<label class="CompulsaryLabel">*</label>
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtDesciptionEdit" runat="server" onkeypress="return IsAlphanumeric(event);"
                                        CssClass="TextControl" MaxLength="50" TabIndex="2" Style="text-transform: capitalize;"
                                        ClientIDMode="Static" ValidationGroup="Edit" onkeyup="cHK_CompEdit();"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtDesciptionEdit" runat="server" ControlToValidate="txtDesciptionEdit"
                                        Display="None" ErrorMessage="Please enter Company Description." ForeColor="Red"
                                        SetFocusOnError="True" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtDesciptionEdit" runat="server"
                                        TargetControlID="rfvtxtDesciptionEdit" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    Address :
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtRegisteredAddressEdit" runat="server" Style="text-transform: capitalize;
                                        min-width: 250px; max-width: 250px; min-height: 50px; max-height: 50px" CssClass="TextControl"
                                        TabIndex="3" Height="50px" MaxLength="150" onkeypress="maximumLimit(this, 149);"
                                        TextMode="MultiLine" ClientIDMode="Static" ValidationGroup="Edit" Width="250px"
                                        onkeyup="cHK_CompEdit();"></asp:TextBox>
                                </td>
                                <td style="text-align: right;">
                                    City :
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtRegisteredCityEdit" runat="server" CssClass="TextControl" MaxLength="15"
                                        TabIndex="4" onkeypress="return IsChar(event)" Style="text-transform: capitalize;"
                                        ClientIDMode="Static" ValidationGroup="Edit" onkeyup="cHK_CompEdit();"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    Pin:
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtRegisteredPinEdit" runat="server" onkeypress="return IsNumber(event)"
                                        CssClass="TextControl" MaxLength="8" TabIndex="5" Style="text-transform: capitalize;"
                                        ClientIDMode="Static" ValidationGroup="Edit" onkeyup="cHK_CompEdit();"></asp:TextBox>
                                </td>
                                <td style="text-align: right;">
                                    State :
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:DropDownList ID="ddlRegisteredStateEdit" runat="server" ClientIDMode="Static"
                                        TabIndex="6" ValidationGroup="Edit" onchange="cHK_CompEdit();" class="chosen-select">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlRegisteredStateEdit" runat="server" ControlToValidate="ddlRegisteredStateEdit"
                                        Display="None" ErrorMessage="Please select State." ForeColor="Red" InitialValue="null"
                                        SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlRegisteredStateEdit" runat="server"
                                        TargetControlID="rfvddlRegisteredStateEdit" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    Phone No 1 :
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtRegisteredPhone1Edit" runat="server" CssClass="TextControl" onkeypress="return IsNumber(event)"
                                        TabIndex="7" MaxLength="15" ClientIDMode="Static" ValidationGroup="Edit" onkeyup="cHK_CompEdit();"></asp:TextBox>
                                </td>
                                <td style="text-align: right;">
                                    Phone No 2 :
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtRegisteredPhone2Edit" runat="server" CssClass="TextControl" MaxLength="11"
                                        onkeypress="return IsNumber(event)" TabIndex="8" ClientIDMode="Static" ValidationGroup="Edit"
                                        onkeyup="cHK_CompEdit();"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <b style="text-align: justify">Same Address</b>
                                </td>
                                <td style="padding-left: 10px;" colspan="3">
                                    <asp:CheckBox ID="ChkAddressEdit" runat="server" Style="text-align: left" onclick="cHK_CompEdit()"
                                        Text="Click here is same as above" TabIndex="9" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    Head Office Address :
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtHOAddressEdit" runat="server" CssClass="TextControl" TabIndex="10"
                                        Height="50px" MaxLength="50" Style="text-transform: capitalize; min-width: 250px;
                                        max-width: 250px; min-height: 50px; max-height: 50px" onkeypress="return IsAlphanumericN(event)"
                                        TextMode="MultiLine" ClientIDMode="Static" ValidationGroup="Edit" Width="250px"></asp:TextBox>
                                </td>
                                <td style="text-align: right;">
                                    City :
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtHOCityEdit" runat="server" CssClass="TextControl" TabIndex="11"
                                        onkeypress="return IsChar(event)" MaxLength="15" Style="text-transform: capitalize;"
                                        ClientIDMode="Static" ValidationGroup="Edit"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    Pin :
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:TextBox ID="txtHOPinEdit" runat="server" onkeypress="return IsNumber(event)"
                                        CssClass="TextControl" MaxLength="8" ClientIDMode="Static" TabIndex="12" ValidationGroup="Edit"></asp:TextBox>
                                    <%--   <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtHOPinEdit" Type="Integer"
   Operator="DataTypeCheck" ValidationGroup="Add" ErrorMessage="Value must be an number!" Display="None"/>

        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="CompareValidator5"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                                </td>
                                <td style="text-align: right;">
                                    State :
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:DropDownList ID="ddlHOStateEdit" runat="server" ClientIDMode="Static" TabIndex="13"
                                        ValidationGroup="Edit" class="chosen-select">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvddlHOStateEdit" runat="server" ControlToValidate="ddlHOStateEdit"
                                        Display="None" ErrorMessage="Please select State" ForeColor="Red" InitialValue="null"
                                        SetFocusOnError="True" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlHOStateEdit" runat="server" TargetControlID="rfvddlHOStateEdit"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <caption>
                                <br />
                                <tr>
                                    <td colspan="4">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Phone No 1 :
                                    </td>
                                    <td style="padding-left: 10px;">
                                        <asp:TextBox ID="txtHOPhone1Edit" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                            MaxLength="15" onkeypress="return IsNumber(event)" TabIndex="14" ValidationGroup="Edit"></asp:TextBox>
                                        <%--  <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtHOPhone1Edit" Type="Integer"
   Operator="DataTypeCheck" ValidationGroup="Add" ErrorMessage="Value must be an number!" Display="None"/>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="CompareValidator3"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                                    </td>
                                    <td style="text-align: right;">
                                        Phone No 2 :
                                    </td>
                                    <td style="padding-left: 10px;">
                                        <asp:TextBox ID="txtHOPhone2Edit" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                            MaxLength="11" onkeypress="return IsNumber(event)" TabIndex="15" ValidationGroup="Edit"></asp:TextBox>
                                        <%--  <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtHOPhone2Edit" Type="Integer"
   Operator="DataTypeCheck" ValidationGroup="Add" ErrorMessage="Value must be an number!" Display="None"/>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="CompareValidator4"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center;">
                                        <asp:Button ID="btnSubmitEdit" runat="server" CssClass="ButtonControl" OnClick="btnSubmitEdit_Click"
                                            TabIndex="16" Text="Save" ValidationGroup="Edit" />
                                        <asp:Button ID="btnCancelEdit" runat="server" CssClass="ButtonControl" OnClick="btnCancelEdit_Click"
                                            TabIndex="17" Text="Cancel" />
                                        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center;">
                                        <asp:Label ID="lblErrorEdit" runat="server" CssClass="ErrorLabel" Text="" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </caption>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:LinkButton ID="lnkDummyEdit" runat="server" Style="display: none;">edit</asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="mpeEditCompany" runat="server" TargetControlID="lnkDummyEdit"
                PopupControlID="pnlEditCompany" BackgroundCssClass="modalBackground" Enabled="true"
                CancelControlID="btnCancelEdit">
            </ajaxToolkit:ModalPopupExtender>
            <script src="Scripts/Choosen/chosen.jquery.js" type="text/javascript"></script>
            <script src="Scripts/Choosen/docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
            <script type="text/javascript">

                var config = {
                    '.chosen-select': {},
                    '.chosen-select-deselect': { allow_single_deselect: true },
                    '.chosen-select-no-single': { disable_search_threshold: 10 },
                    '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                    '.chosen-select-width': { width: "95%" }
                }
                for (var selector in config) {
                    $(selector).chosen(config[selector]);
                }

                function validateChosen() {
                    var config = {
                        '.chosen-select': {},
                        '.chosen-select-deselect': { allow_single_deselect: true },
                        '.chosen-select-no-single': { disable_search_threshold: 10 },
                        '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                        '.chosen-select-width': { width: "95%" }
                    }
                    for (var selector in config) {
                        $(selector).chosen(config[selector]);
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
