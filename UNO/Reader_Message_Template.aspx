<%@ Page Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" CodeBehind="Reader_Message_Template.aspx.cs" Inherits="UNO.Reader_Message_Template" %>

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

        }

        function SetFocus() {
        }
        //Resolved Bug 294 - Swapnil end

        function SetListStyle() {
            location.reload();
        }        


    </script>
    <script type="text/javascript" language="javascript">
        function onUpdating() {
        }

        function onUpdated() {
        }
    </script>
    <script type="text/javascript">
        function ResetAll() {  
        }


        function DisplayLabelText() {
        }
        function DisplayLabelTextEntity() {
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
                <asp:Label ID="lblHead" runat="server" Text="Reader Message Template" ForeColor="RoyalBlue" Font-Size="20px"
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
                        <asp:TextBox ID="txtControllerName" onkeydown="return (event.keyCode!=13);" runat="server"
                            Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;<b>Search Controller</b>&nbsp;

                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtreaderID" runat="server" TargetControlID="txtControllerName"
                            WatermarkText="Controller Name" WatermarkCssClass="watermark">
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
                                            <span>No Reader Records found.</span>
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
                                                    CommandArgument='<%#Eval("ReaderID") %>' Text="Edit" ForeColor="#3366FF"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>                                        
                                        <asp:BoundField DataField="ReaderID" HeaderText="Reader ID" SortExpression="Reader ID" ItemStyle-Width="15%"  HeaderStyle-CssClass="Hide" ItemStyle-CssClass="Hide" >
                                            <ItemStyle HorizontalAlign="Center" Wrap="true"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ControllerName" HeaderText="Controller Name" SortExpression="Controller Name" ItemStyle-Width="15%">
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EventName" HeaderText="Event Name" SortExpression="Event Name" ItemStyle-Width="15%">
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EventMessage" HeaderText="Event Message" SortExpression="Event Message">
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" />
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
                            <asp:Label ID="Listctrl" runat="server" Font-Bold="true" ForeColor="RoyalBlue">Select Controller :</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="ComboControl" align="left">
                            <asp:DropDownList runat="server" ID="ctrllst" Width="150px" TabIndex="1" onchange="DisplayLabelText();"
                                ClientIDMode="Static">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLstctrl" runat="server" ControlToValidate="ctrllst"
                                Display="None" ErrorMessage="Please select Controller" ForeColor="Red" InitialValue="0"
                                SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerlstfileAdd" runat="server" TargetControlID="rfvLstctrl"
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
                            <asp:Label ID="Listctrl1" runat="server" Font-Bold="true" ForeColor="RoyalBlue">Select Event :</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="ComboControl" align="left">
                            <asp:DropDownList runat="server" ID="ctrllst1" Width="150px" TabIndex="1" onchange="DisplayLabelText();"
                                ClientIDMode="Static">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLstctrl1" runat="server" ControlToValidate="ctrllst1"
                                Display="None" ErrorMessage="Please select Event" ForeColor="Red" InitialValue="0"
                                SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvLstctrl1"
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
                        <td class="TDClassLabel">
                            <asp:Label ID="LblDesc" runat="server" ClientIDMode="Static">Description</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:TextBox ID="txtDesc" ClientIDMode="Static" runat="server" TabIndex="3" MaxLength="200"
                                Style="text-transform: capitalize;" 
                                onkeypress="return IsAlphanumeric(event)" Width="195px" 
                                TextMode="MultiLine"></asp:TextBox>
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
                                TabIndex="4" Text="Save" OnClick="BtnSave_Click" ValidationGroup="Add" />
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
    <asp:Panel ID="pnlModifyCommon" runat="server" CssClass="PopupPanel" Style="height: 25%;
        width: 55%">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="table1" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                    class="TableClass">
                    <tr>
                        <td class="TDClassLabel" align="right">
                            <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="RoyalBlue">Select Controller :*</asp:Label>
                        </td>
                        <td class="ComboControl" align="left">
                            <asp:DropDownList runat="server" ID="ddlCtrl" CssClass="ComboControl" Width="150px"
                                Enabled="false" TabIndex="1" ClientIDMode="Predictable" onchange="DisplayCtrlAdd();">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlctrlAdd" runat="server" ControlToValidate="ddlctrl"
                                Display="None" ErrorMessage="Please select Controller" ForeColor="Red" InitialValue="0"
                                SetFocusOnError="True" Visible="False" ValidationGroup="Modify"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerddlentityedit" runat="server" TargetControlID="rfvddlctrlAdd"
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
                            <asp:Label ID="Label2" runat="server" Font-Bold="true" ForeColor="RoyalBlue">Select Event :*</asp:Label>
                        </td>
                        <td class="ComboControl" align="left">
                            <asp:DropDownList runat="server" ID="ddlEvnt" CssClass="ComboControl" Width="150px"
                                Enabled="false" TabIndex="1" ClientIDMode="Predictable" onchange="DisplayEventAdd();">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlevntAdd" runat="server" ControlToValidate="ddlEvnt"
                                Display="None" ErrorMessage="Please select Event" ForeColor="Red" InitialValue="0"
                                SetFocusOnError="True" Visible="False" ValidationGroup="Modify"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvddlevntAdd"
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
                        <td class="TDClassLabel">
                            <asp:Label ID="lblName" runat="server" ClientIDMode="Static">Description</asp:Label><label
                                class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:TextBox ID="txtName" ClientIDMode="Static" runat="server" TabIndex="3" MaxLength="200"
                                Style="text-transform: capitalize;" 
                                onkeypress="return IsAlphanumeric(event)" Width="236px" 
                                TextMode="MultiLine"></asp:TextBox>
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
