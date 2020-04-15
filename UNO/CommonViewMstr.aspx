<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="CommonViewMstr.aspx.cs" Inherits="UNO.CommonViewMstr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css" />
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css" />
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css" />
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script>
        //Resolved Bug 294 - Swapnil Start
        function pageLoad() {
            var popup = $find('<%=mpeAddCommon.ClientID%>');
            popup.add_shown(SetFocus);
        }

        function SetFocus() {
            $get('<%=lstFile.ClientID%>').focus();
        }
        //Resolved Bug 294 - Swapnil end

        function SetListStyle() {
            location.reload();
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
    </script>
    <script type="text/javascript">
        function ResetAll() {
            $('#' + ["<%=txtCompanyName.ClientID%>", "<%=txtCompanyID.ClientID%>"].join(', #')).prop('value', "");
            document.getElementById('<%=txtCompanyName.ClientID%>').focus();
            document.getElementById('<%=txtCompanyID.ClientID%>').focus();
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvCompany.ClientID%>').focus();

        }


        function DisplayLabelText() {
            if (document.getElementById('lstFile').value == "DEP") {

                document.getElementById('LblID').innerHTML = "Department ID :"
                document.getElementById('LblDesc').innerHTML = "Department Description :"

            }
            else if (document.getElementById('lstFile').value == "GRD") {

                document.getElementById('LblID').innerHTML = "Grade ID :"
                document.getElementById('LblDesc').innerHTML = "Grade Description :"

            }

            else if (document.getElementById('lstFile').value == "GRP") {

                document.getElementById('LblID').innerHTML = "Group ID :"
                document.getElementById('LblDesc').innerHTML = "Group Description :"

            }
            else if (document.getElementById('lstFile').value == "CAT") {

                document.getElementById('LblID').innerHTML = "Category ID :"
                document.getElementById('LblDesc').innerHTML = "Category Description :"

            }

            else if (document.getElementById('lstFile').value == "LOC") {

                document.getElementById('LblID').innerHTML = "Location ID :"
                document.getElementById('LblDesc').innerHTML = "Location Description :"

            }
            else if (document.getElementById('lstFile').value == "DES") {

                document.getElementById('LblID').innerHTML = "Designation ID :"
                document.getElementById('LblDesc').innerHTML = "Designation Description :"

            }
            else if (document.getElementById('lstFile').value == "DIV") {

                document.getElementById('LblID').innerHTML = "Division ID :"
                document.getElementById('LblDesc').innerHTML = "Division Description :"

            }
            else if (document.getElementById('lstFile').value == "Select One") {

                document.getElementById('LblID').innerHTML = "";
                document.getElementById('LblDesc').innerHTML = "";

            }
        }
        function DisplayLabelTextEntity() {

            if (document.getElementById('<%= ddlEntity.ClientID%>').value == "DEP") {

                document.getElementById('lblisd').innerHTML = "Department ID :"
                document.getElementById('lblName').innerHTML = "Department Description :"

            }
            else if (document.getElementById('<%= ddlEntity.ClientID%>').value == "GRD") {

                document.getElementById('lblisd').innerHTML = "Grade ID :"
                document.getElementById('lblName').innerHTML = "Grade Description :"

            }

            else if (document.getElementById('<%= ddlEntity.ClientID%>').value == "GRP") {

                document.getElementById('lblisd').innerHTML = "Group ID :"
                document.getElementById('lblName').innerHTML = "Group Description :"

            }
            else if (document.getElementById('<%= ddlEntity.ClientID%>').value == "CAT") {

                document.getElementById('lblisd').innerHTML = "Category ID :"
                document.getElementById('lblName').innerHTML = "Category Description :"

            }

            else if (document.getElementById('<%= ddlEntity.ClientID%>').value == "LOC") {

                document.getElementById('lblisd').innerHTML = "Location ID :"
                document.getElementById('lblName').innerHTML = "Location Description :"

            }
            else if (document.getElementById('<%= ddlEntity.ClientID%>').value == "DES") {

                document.getElementById('lblisd').innerHTML = "Designation ID :"
                document.getElementById('lblName').innerHTML = "Designation Description :"

            }
            else if (document.getElementById('<%= ddlEntity.ClientID%>').value == "DIV") {

                document.getElementById('lblisd').innerHTML = "Division ID :"
                document.getElementById('lblName').innerHTML = "Division Description :"

            }
            else if (document.getElementById('ddlEntity').value == "Select One") {

                document.getElementById('lblisd').innerHTML = "";
                document.getElementById('lblName').innerHTML = "";

            }
        }
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
         .Hide { display:none; }
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
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <br />
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Common View" ForeColor="RoyalBlue" Font-Size="20px"
                    Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%; text-align: left;">
                        <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click" />
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                            OnClick="btnDelete_Click" />
                    </td>
                    <td style="width: 50%; text-align: right;">
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                            OnClientClick="return ResetAll();" OnClick="btnReset_Click" />
                        <asp:Button ID="btnSearch" CssClass="ButtonControl" runat="server" Text="Search"
                            Style="float: right; margin-right: 3px;" OnClick="btnSearch_Click" />
                        <asp:TextBox ID="txtCompanyName" onkeydown="return (event.keyCode!=13);" runat="server"
                            Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <asp:TextBox ID="txtCompanyID" MaxLength="10" onkeydown="return (event.keyCode!=13);" runat="server"
                            Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <asp:DropDownList ID="ddlSearchEnt" runat="server" style="float:right; padding-right:10px; margin-right: 10px;"></asp:DropDownList>
                        <b>Select Code:</b>&nbsp;&nbsp;&nbsp;

                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCompanyID" runat="server" TargetControlID="txtCompanyID"
                            WatermarkText="ID" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCompanyName" runat="server" TargetControlID="txtCompanyName"
                            WatermarkText="Description" WatermarkCssClass="watermark">
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
                                <asp:GridView ID="gvCompany" runat="server" AutoGenerateColumns="false" Width="100%"
                                    GridLines="None" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvCompany_PageIndexChanging"
                                    OnRowCommand="gvCompany_RowCommand">
                                    <RowStyle CssClass="gvRow" />
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                    <PagerStyle CssClass="gvPager" />
                                    <EmptyDataTemplate>
                                        <div>
                                            <span>No Common Records found.</span>
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" SortExpression="Select" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" SortExpression="Edit" ItemStyle-Width="5%" HeaderStyle-CssClass="center">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                    Text="Update" ForeColor="#3366FF"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Text="Cancel" ForeColor="#3366FF"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit3"
                                                    CommandArgument='<%#Eval("ID") %>' Text="Edit" ForeColor="#3366FF"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ENTID" HeaderText="Code" SortExpression="Code" ItemStyle-Width="15%">
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ItemStyle-Width="15%">
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompanyID" HeaderText="Company ID" SortExpression="Company ID" ItemStyle-Width="15%"  HeaderStyle-CssClass="Hide" ItemStyle-CssClass="Hide" >
                                            <ItemStyle HorizontalAlign="Center" Wrap="true"  />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: left; width: 15%;">
                                                    <span>Go To :</span><asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ChangePage">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Button ID="btnPrevious" CssClass="ButtonControl" runat="server" Text="Previous"
                                                        OnClick="gvPrevious" />
                                                    <asp:Label ID="lblShowing" runat="server" Text="Showing "></asp:Label>
                                                    <asp:Button ID="btnNext" CssClass="ButtonControl" runat="server" Text="Next" OnClick="gvNext" />
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
        </div>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div style="text-align: center">
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <%-- <asp:AsyncPostBackTrigger ControlID="btnAdd" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="gvCompany" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                <asp:AsyncPostBackTrigger ControlID="BtnSave" />
                <asp:AsyncPostBackTrigger ControlID="btnModifySave" />
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Panel ID="pnlAddCommon" runat="server" CssClass="PopupPanel" Style="height: 200px;
        width: 55%">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table id="table4" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                    class="TableClass">
                     <tr>
                        <td class="TDClassLabel" align="right">
                            <asp:Label ID="Label2" runat="server" Font-Bold="true" ForeColor="RoyalBlue">Select Company :</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="ComboControl" align="left">
                            <asp:DropDownList runat="server" ID="ddlCompany" Width="150px" TabIndex="1" 
                                ClientIDMode="Static">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCompany"
                                Display="None" ErrorMessage="Please select Company" ForeColor="Red" InitialValue="0"
                                SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" align="right">
                            <asp:Label ID="LblList" runat="server" Font-Bold="true" ForeColor="RoyalBlue">Select Master :</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="ComboControl" align="left">
                            <asp:DropDownList runat="server" ID="lstFile" Width="150px" TabIndex="1" onchange="DisplayLabelText();"
                                ClientIDMode="Static">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLstfile" runat="server" ControlToValidate="lstFile"
                                Display="None" ErrorMessage="Please select Master" ForeColor="Red" InitialValue="0"
                                SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerlstfileAdd" runat="server" TargetControlID="rfvLstfile"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" align="right">
                            <asp:Label ID="LblID" runat="server" ClientIDMode="Static">ID</asp:Label><label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" align="right">
                            <asp:TextBox ID="txtID" ClientIDMode="Static" runat="server" MaxLength="10" TabIndex="2"
                                onkeypress="return IsAlphanumericWithoutspace(event);" 
                                Style="text-transform: uppercase" Width="193px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtid" runat="server" ControlToValidate="txtID"
                                Display="None" ErrorMessage="Please enter Id." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcertxtidAdd" runat="server" TargetControlID="rfvtxtid"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <%--<td> <asp:RequiredFieldValidator ID="ValID" runat="server" 
            ErrorMessage="Please enter ID" ControlToValidate="txtID" 
            SetFocusOnError="True" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator></td>--%>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            <asp:Label ID="LblDesc" runat="server" ClientIDMode="Static">Description</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:TextBox ID="txtDesc" ClientIDMode="Static" runat="server" TabIndex="3" MaxLength="40"
                                Style="text-transform: capitalize;" 
                                onkeypress="return IsAlphanumeric(event)" Width="195px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtdesc" runat="server" ControlToValidate="txtDesc"
                                Display="None" ErrorMessage="Please enter Description." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcertxtdescAdd" runat="server" TargetControlID="rfvtxtdesc"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <%-- <td>
          <asp:RequiredFieldValidator ID="ValDesc" runat="server" 
            ErrorMessage="Please enter Description" ControlToValidate="txtDesc" 
            SetFocusOnError="True" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
          </td>  --%>
                    <tr>
                        <td colspan="2" style="text-align: center; padding-top: 4%">
                            <asp:Button ID="BtnSave" runat="server" CssClass="ButtonControl" Font-Bold="true"
                                TabIndex="4" Text="Save" OnClick="BtnSave_Click" ValidationGroup="Add" />&nbsp;
                            <asp:Button ID="BtnCancel" runat="server" CausesValidation="False" CssClass="ButtonControl"
                                Font-Bold="true" TabIndex="5" Text="Cancel" OnClick="BtnCancel_Click" />
                        </td>
                        <%-- <td></td>--%>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding-bottom: 3%;">
                            <asp:Label ID="lblerror" Visible="true" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <%-- <asp:AsyncPostBackTrigger ControlID="btnAdd" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button1" runat="server" Style="display: none" CssClass="ButtonControl"
        Font-Bold="true" TabIndex="5" Text="Modify" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddCommon" runat="server" TargetControlID="Button1"
        PopupControlID="pnlAddCommon" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancel">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlModifyCommon" runat="server" CssClass="PopupPanel" Style="height: 200px;
        width: 55%">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="table1" runat="server" border="0" cellpadding="0" cellspacing="0"
                    class="TableClass">
                    <tr>
                        <td class="TDClassLabel" align="right">
                            <asp:Label ID="Label3" runat="server" Font-Bold="true" ForeColor="RoyalBlue"> Company :</asp:Label>
                        </td>
                        <td class="ComboControl" align="left">
                            <asp:DropDownList runat="server" ID="ddlcom" Width="150px" Enabled="false" TabIndex="1" 
                                ClientIDMode="Predictable">
                            </asp:DropDownList>                            
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" align="right">
                            <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="RoyalBlue"> Master :</asp:Label>
                        </td>
                        <td class="ComboControl" align="left">
                            <asp:DropDownList runat="server" ID="ddlEntity" CssClass="ComboControl" Width="150px"
                                Enabled="false" TabIndex="1" ClientIDMode="Predictable" onchange="DisplayLabelTextEntity();">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" align="right">
                            <asp:Label ID="lblisd" runat="server" ClientIDMode="Static">ID</asp:Label>
                        </td>
                        <td class="TDClassControl" align="right">
                            <asp:TextBox ID="txtisd" ClientIDMode="Static" runat="server" MaxLength="8" TabIndex="2"
                                onkeypress="return IsAlphanumericWithoutspace(event)" Enabled="false" 
                                Style="text-transform: uppercase" Width="234px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            <asp:Label ID="lblName" runat="server" ClientIDMode="Static">Description</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:TextBox ID="txtName" ClientIDMode="Static" runat="server" TabIndex="3" MaxLength="40"
                                Style="text-transform: capitalize;" 
                                onkeypress="return IsAlphanumeric(event)" Width="236px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rcftxtnameedit" runat="server" ControlToValidate="txtName"
                                Display="None" ErrorMessage="Please enter Description." ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Modify"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcertxtnameedit" runat="server" TargetControlID="rcftxtnameedit"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding-top: 4%">
                            <asp:Button ID="btnModifySave" runat="server" CssClass="ButtonControl" Font-Bold="true"
                                TabIndex="4" Text="Save" ValidationGroup="Modify" OnClick="btnModifySave_Click" />
                            &nbsp;
                            <asp:Button ID="btnModifyCancel" runat="server" CausesValidation="False" CssClass="ButtonControl"
                                Font-Bold="true" TabIndex="5" Text="Cancel" OnClick="btnModifyCancel_Click" />
                        </td>
                    </tr>
                       <tr>
                        <td colspan="2" style="text-align: center; padding-bottom: 3%;">
                            <asp:Label ID="lblEditError" Visible="true" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="btnModify" runat="server" Style="display: none" CssClass="ButtonControl"
        Font-Bold="true" TabIndex="5" Text="Modify" />
    <ajaxToolkit:ModalPopupExtender ID="mpModifyCommon" runat="server" TargetControlID="btnModify"
        PopupControlID="pnlModifyCommon" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnModifyCancel">
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
    </script>
</asp:Content>
