<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="UserAccessPermissionBrowse.aspx.cs" Inherits="UNO.UserAccessPermissionBrowse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">--%>
    <%--<link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">--%>
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script type="text/javascript" src="Scripts/filter.js"> </script>
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script type="text/javascript">
        var myfilter;

        function ValidateELListBoxAdd(sender, args) {
            var options = document.getElementById("<%=lstSEntityAdd.ClientID%>").options;
            if (options.length > 0) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }

        function ValidateALListBoxAdd(sender, args) {
            var options = document.getElementById("<%=lstSALAdd.ClientID%>").options;
            if (options.length > 0) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }

        function ValidateALListBoxEdit(sender, args) {
            var options = document.getElementById("<%=lstSALEdit.ClientID%>").options;
            if (options.length > 0) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }

        function ValidateALListBoxCountAdd(sender, args) {
            var options = document.getElementById("<%=lstSALAdd.ClientID%>").options;
            if (options.length > 4) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        function ValidateALListBoxCountEdit(sender, args) {
            //alert('hello');
            var options = document.getElementById("<%=lstSALEdit.ClientID%>").options;
            if (options.length > 4) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }

        }

        function check() {
            try {
                //alert(myfilter);
                myfilter.set(document.getElementById("<%=txtAEntityDescAdd.ClientID%>").value);

            }
            catch (e) {
                alert(e.Message);
            }

            //alert(document.getElementById("<%=txtAEntityDescAdd.ClientID%>").value);
        }
        function getList() {
            //myfilter = new filterlist(document.getElementById("<%=lstAEntityAdd.ClientID%>"));
            //alert("123");
        }


        function FilterList() {

            var list = document.getElementById("<%=lstAEntityAdd.ClientID%>");
            var list1 = document.getElementById("<%=ListBox1.ClientID%>");
            var textEmpToSearch = document.getElementById("<%=txtAEntityDescAdd.ClientID%>");

            //alert(list1.options.length);
            //alert(textEmpToSearch.value);

            var items = 0;

            for (items = list1.options.length - 1; items >= 0; items--) {
                list.options[items] = null;
            }

            var i = 0, j = 0;
            var strList;

            var strTxt = textEmpToSearch.value.toLowerCase().replace(/\s/g, '');
            var strLength = textEmpToSearch.value.length;

            //alert(strTxt);
            //alert(strLength);


            for (i = 0; i <= list1.options.length - 1; i++) {

                strList = list1.options[i].text.substring(0, strLength).toLowerCase().replace(/\s/g, '');
                //alert(strList);

                if (strList == strTxt) {
                    //  alert("Condtion  Satisfied");
                    list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                    j++;
                }

            }
            if (isNaN(textEmpToSearch.value) == false) {
                FilterListByID();
            }
        }


        function FilterListByID() {

            var list = document.getElementById("<%=lstAEntityAdd.ClientID%>");
            var list1 = document.getElementById("<%=ListBox1.ClientID%>");
            var textEmpToSearch = document.getElementById("<%=txtAEntityDescAdd.ClientID%>");

            //alert(list1.options.length);
            //alert(textEmpToSearch.value);

            var items = 0;

            for (items = list1.options.length - 1; items >= 0; items--) {
                list.options[items] = null;
            }

            var i = 0, j = 0;
            var strList;

            var strTxt = textEmpToSearch.value.toLowerCase().replace(/\s/g, '');
            var strLength = textEmpToSearch.value.length;

            //alert(strTxt);
            //alert(strLength);
            strList = list1.options[i].value.substring(0, strLength).toLowerCase().replace(/\s/g, '');
           // alert(strList)


            for (i = 0; i <= list1.options.length - 1; i++) {

                strList = list1.options[i].value.substring(0, strLength).toLowerCase().replace(/\s/g, '');

                //alert(strList);

                if (strList == strTxt) {
                    //  alert("Condtion  Satisfied");
                    list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                    j++;
                }

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
            var gridView = $get('<%= this.gvEmployeeAccess.ClientID %>');

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
            $('#' + ["<%=txtEntityName.ClientID%>", "<%=txtLevelDescription .ClientID%>"].join(', #')).prop('value', "");
            document.getElementById('<%=lblMessages.ClientID%>').innerHTML = "";
            document.getElementById('<%=txtLevelDescription .ClientID%>').focus();
            document.getElementById('<%=txtEntityName.ClientID%>').focus();
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvEmployeeAccess.ClientID%>').focus();
            return false;
        }
    </script>
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
                <asp:Label ID="lblHead" runat="server" Text="Employee Access Configuration" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <asp:Panel ID="Panel2" runat="server" DefaultButton="btnSearch">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 20%; text-align: left;">
                            <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click" />
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                                OnClick="btnDelete_Click" />
                        </td>
                        <td style="width: 40%; text-align: center;">
                            <span style="color: White;">Entity Type :</span>
                            <asp:DropDownList ID="ddlEntityType" runat="server" Style="min-width: 120px; max-width: 120px;"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlEntityType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40%; text-align: right;">
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                                OnClientClick="return ResetAll();" />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;margin-right:3px;" CssClass="ButtonControl"
                                OnClick="btnSearch_Click" />
                            <asp:TextBox ID="txtEntityName" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtEntityName" runat="server" TargetControlID="txtEntityName"
                                WatermarkText="Entity Name" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:TextBox ID="txtLevelDescription" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtLevelDescription" runat="server"
                                TargetControlID="txtLevelDescription" WatermarkText="Level Description"
                                WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="3">
                            <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                                <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvEmployeeAccess" runat="server" DataKeyNames="ID,AL_ID,ENTITY_TYPE"
                                        AutoGenerateColumns="false" Width="100%" GridLines="None" AllowPaging="true"
                                        PageSize="10" OnRowCommand="gvEmployeeAccess_RowCommand">
                                        <RowStyle CssClass="gvRow" />
                                        <HeaderStyle CssClass="gvHeader" />
                                        <AlternatingRowStyle BackColor="#F0F0F0" />
                                        <PagerStyle CssClass="gvPager" />
                                        <EmptyDataTemplate>
                                            <div>
                                                <span>No Records found.</span>
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" AccessibleHeaderText="Select" SortExpression="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="DeleteRows" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" AccessibleHeaderText="Edit" SortExpression="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Modify" CommandArgument='<%# Eval("ID") %>'
                                                        ForeColor="#3366FF">Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DES" HeaderText="Entity Name" SortExpression="DES">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AL_DESCRIPTION" HeaderText="Level Description" SortExpression="AL_DESCRIPTION">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ENTITY_TYPE" HeaderText="AL From" SortExpression="ENTITY_TYPE">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
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
                                    <asp:AsyncPostBackTrigger ControlID="ddlEntityType" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
                                TargetControlID="UpdatePanel1">
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
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Button ID="btnDummyAdd" runat="server" Text="dummy" Style="display: none;" />
    <asp:Button ID="btnDummyEdit" runat="server" Text="dummy" Style="display: none;" />
    <asp:Panel ID="pnlAddEmployeeAccess" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="table2" runat="server" width="100%" border="0" cellpadding="0" cellspacing="5"
                    class="TableClass">
                    <tr>
                        <td style="text-align: right;">
                            UAP Id :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox CssClass="TextControl" ID="txtUAPIDAdd" MaxLength="15" runat="server"
                                ClientIDMode="Static" ReadOnly="True" ValidationGroup="Add"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Entity :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList ID="ddlEntityAdd" runat="server" AutoPostBack="True" onchange="getList();"
                                ValidationGroup="Add" OnSelectedIndexChanged="ddlEntityAdd_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlEntityAdd" runat="server" ControlToValidate="ddlEntityAdd"
                                Display="None" ErrorMessage="Please select Entity." ForeColor="Red" InitialValue="0"
                                ValidationGroup="Add" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlEntityAdd" runat="server" TargetControlID="rfvddlEntityAdd"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="display:none;">
                            Entity Specification :<label class="CompulsaryLabel">*</label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td style="text-align: center;">
                                        Available
                                    </td>
                                    <td>
                                    </td>
                                    <td style="text-align: center;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 50%; text-align: left;" colspan="2">
                                                    <asp:TextBox ID="txtAEntityDescAdd" CssClass="searchTextBox" runat="server" Width="80%"
                                                        onKeyUp="FilterList()"></asp:TextBox>
                                                    <%-- onkeyup="onCheck(this,'lstAEntityAdd','DESC');"--%>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtAEntityDescAdd" runat="server" TargetControlID="txtAEntityDescAdd"
                                                        WatermarkText="Search by Description" WatermarkCssClass="watermark">
                                                    </ajaxToolkit:TextBoxWatermarkExtender>
                                                </td>
                                                <td style="width: 50%; text-align: right; display: none">
                                                    <asp:TextBox ID="txtAEntityCodeAdd" CssClass="searchTextBox" runat="server"></asp:TextBox>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtAEntityCodeAdd" runat="server" TargetControlID="txtAEntityCodeAdd"
                                                        WatermarkText="Search by Code" WatermarkCssClass="watermark">
                                                    </ajaxToolkit:TextBoxWatermarkExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                    </td>
                                    <td >
                                        <table style="width: 100%; display: none;">
                                            <tr>
                                                <td style="width: 50%; text-align: left;">
                                                    <asp:TextBox ID="txtSEntityDescAdd" CssClass="searchTextBox" runat="server" onkeyup="onCheck(this,'lstSEntityAdd','DESC');"></asp:TextBox>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtSEntityDescAdd" runat="server" TargetControlID="txtSEntityDescAdd"
                                                        WatermarkText="Search by Description" WatermarkCssClass="watermark">
                                                    </ajaxToolkit:TextBoxWatermarkExtender>
                                                </td>
                                                <td style="width: 50%; text-align: right;">
                                                    <asp:TextBox ID="txtSEntityCodeAdd" CssClass="searchTextBox" runat="server" onkeyup="onCheck(this,'lstSEntityAdd','CODE');"></asp:TextBox>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtSEntityCodeAdd" runat="server" TargetControlID="txtSEntityCodeAdd"
                                                        WatermarkText="Search by Code" WatermarkCssClass="watermark">
                                                    </ajaxToolkit:TextBoxWatermarkExtender>
                                                </td>
                                            </tr>
                                        </table>
                                        Selected Records : <asp:Label ID="lblCount" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lstAEntityAdd" runat="server" Height="100px" Width="300px" ForeColor="Black"
                                            Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple">
                                        </asp:ListBox>
                                        <asp:ListBox ID="ListBox1" runat="server" Height="100px" Width="300px" ForeColor="Black"
                                            Style="display: none" Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static"
                                            SelectionMode="Multiple"></asp:ListBox>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdEntityAllRightAdd" runat="server" Text="&gt;&gt;" Width="39px"
                                                        CssClass="ButtonControl" CausesValidation="False" OnClick="cmdEntityAllRightAdd_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdEntityRightAdd" runat="server" Text="&gt;" Width="39px" CssClass="ButtonControl"
                                                        CausesValidation="False" OnClick="cmdEntityRightAdd_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdEntityLeftAdd" runat="server" Text="&lt;" Width="39px" CssClass="ButtonControl"
                                                        CausesValidation="False" OnClick="cmdEntityLeftAdd_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdEntityAllLeftAdd" runat="server" Text="&lt;&lt;" Width="39px"
                                                        CssClass="ButtonControl" CausesValidation="False" OnClick="cmdEntityAllLeftAdd_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstSEntityAdd" runat="server" ForeColor="Black" Font-Names="Courier New"
                                            CssClass="TextControl" Height="100px" Width="300px" ClientIDMode="Static" SelectionMode="Multiple"
                                            AutoPostBack="True" CausesValidation="True" ValidationGroup="Add"></asp:ListBox>
                                        <asp:CustomValidator ID="cvlstSEntityAdd" runat="server" ErrorMessage="Please select Entity Specification(s)."
                                            ControlToValidate="lstSEntityAdd" Display="None" ValidateEmptyText="true" ClientValidationFunction="ValidateELListBoxAdd"
                                            ValidationGroup="Add"></asp:CustomValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcecvlstSEntityAdd" runat="server" TargetControlID="cvlstSEntityAdd"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Access Level :<label class="CompulsaryLabel">*</label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td>
                                        Available
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        Selected
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lstAALAdd" runat="server" Height="100px" Width="300px" ForeColor="Black"
                                            Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple">
                                        </asp:ListBox>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdALLALRightAdd" runat="server" Text="&gt;&gt;" Width="39px" CssClass="ButtonControl"
                                                        CausesValidation="False" OnClick="cmdALLALRightAdd_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdALRightAdd" runat="server" Text="&gt;" Width="39px" CssClass="ButtonControl"
                                                        CausesValidation="False" OnClick="cmdALRightAdd_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdALLeftAdd" runat="server" Text="&lt;" Width="39px" CssClass="ButtonControl"
                                                        CausesValidation="False" OnClick="cmdALLeftAdd_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdALLALLeftAdd" runat="server" Text="&lt;&lt;" Width="39px" CssClass="ButtonControl"
                                                        CausesValidation="False" OnClick="cmdALLALLeftAdd_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstSALAdd" runat="server" Height="100px" Width="300px" ForeColor="Black"
                                            Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple"
                                            AutoPostBack="false" CausesValidation="True" ValidationGroup="Add"></asp:ListBox>
                                        <asp:CustomValidator ID="cvlstSALAdd" runat="server" ControlToValidate="lstSALAdd"
                                            Display="None" ValidateEmptyText="True" ErrorMessage="Please select access level(s)."
                                            ClientValidationFunction="ValidateALListBoxAdd" ValidationGroup="Add"></asp:CustomValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcecvlstSALAdd" runat="server" TargetControlID="cvlstSALAdd"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:CustomValidator ID="cvlstSALAdd1" runat="server" ControlToValidate="lstSALAdd"
                                            Display="None" ValidateEmptyText="True" ErrorMessage="You can select maximum 4 AL(s)."
                                            ClientValidationFunction="ValidateALListBoxCountAdd" ValidationGroup="Add"></asp:CustomValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcecvlstSALAdd1" runat="server" TargetControlID="cvlstSALAdd1"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnSubmitAdd" runat="server" CssClass="ButtonControl" Text="Save"
                                ValidationGroup="Add" OnClick="btnSubmitAdd_Click" />
                            <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" Text="Cancel"
                                OnClick="btnCancelAdd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Label ID="lblErrorAdd" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlEntityAdd" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeAddEmployeeAccess" runat="server" TargetControlID="btnDummyAdd"
        PopupControlID="pnlAddEmployeeAccess" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancelAdd">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlEditEmployeeAccess" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table id="table1" runat="server" width="100%" border="0" cellpadding="0" cellspacing="5"
                    class="TableClass">
                    <tr>
                        <td style="text-align: right; display: none;">
                            UAP Id :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px; display: none;">
                            <asp:TextBox CssClass="TextControl" ID="txtUPAIDEdit" runat="server" ReadOnly="True"
                                Visible="false" ValidationGroup="Edit"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Entity :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList ID="ddlEntityEdit" runat="server" CssClass="ComboControl" AutoPostBack="True"
                                ValidationGroup="Edit" Enabled="false">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlEntityEdit" runat="server" ControlToValidate="ddlEntityEdit"
                                Display="None" ErrorMessage="Please select Entity." ForeColor="Red" InitialValue="0"
                                ValidationGroup="Edit" SetFocusOnError="True" Enabled="false"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlEntityEdit" runat="server" TargetControlID="rfvddlEntityEdit"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Entity Specification :<label class="CompulsaryLabel">*</label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lstAEntityEdit" runat="server" Height="100px" Width="300px" ForeColor="Black"
                                            Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" Enabled="False">
                                        </asp:ListBox>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdEntityAllRightEdit" runat="server" Text="&gt;&gt;" Width="39px"
                                                        CssClass="ButtonControl" CausesValidation="False" Visible="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdEntityRightEdit" runat="server" Text="&gt;" Width="39px" CssClass="ButtonControl"
                                                        CausesValidation="False" Visible="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdEntityLeftEdit" runat="server" Text="&lt;" Width="39px" CssClass="ButtonControl"
                                                        CausesValidation="False" Visible="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdEntityAllLeftEdit" runat="server" Text="&lt;&lt;" Width="39px"
                                                        CssClass="ButtonControl" CausesValidation="False" Visible="False" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstSEntityEdit" runat="server" CssClass="TextControl" Height="100px"
                                            Width="280px" ClientIDMode="Static" SelectionMode="Multiple" Visible="False">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Access Level :<label class="CompulsaryLabel">*</label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td>
                                        Available
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        Selected
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lstAALEdit" runat="server" Height="100px" Width="300px" ForeColor="Black"
                                            Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple">
                                        </asp:ListBox>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdALLALRightEdit" runat="server" Text="&gt;&gt;" Width="39px" CssClass="ButtonControl"
                                                        CausesValidation="False" OnClick="cmdALLALRightEdit_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdALRightEdit" runat="server" Text="&gt;" Width="39px" CssClass="ButtonControl"
                                                        CausesValidation="False" OnClick="cmdALRightEdit_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdALLeftEdit" runat="server" Text="&lt;" Width="39px" CssClass="ButtonControl"
                                                        CausesValidation="False" OnClick="cmdALLeftEdit_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdALLALLeftEdit" runat="server" Text="&lt;&lt;" Width="39px" CssClass="ButtonControl"
                                                        CausesValidation="False" OnClick="cmdALLALLeftEdit_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstSALEdit" runat="server" Height="100px" Width="300px" ForeColor="Black"
                                            Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" SelectionMode="Multiple"
                                            AutoPostBack="false" CausesValidation="True" ValidationGroup="Edit"></asp:ListBox>
                                        <asp:CustomValidator ID="cvlstSALEdit" runat="server" ControlToValidate="lstSALEdit"
                                            Display="None" ValidateEmptyText="True" ErrorMessage="Please select access level(s)."
                                            ClientValidationFunction="ValidateALListBoxEdit" ValidationGroup="Edit"></asp:CustomValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcecvlstSALEdit" runat="server" TargetControlID="cvlstSALEdit"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:CustomValidator ID="cvlstSALEdit1" runat="server" ControlToValidate="lstSALEdit"
                                            Display="None" ValidateEmptyText="True" ErrorMessage="You can select maximum 4 AL(s)."
                                            ClientValidationFunction="ValidateALListBoxCountEdit" ValidationGroup="Edit"></asp:CustomValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcecvlstSALEdit1" runat="server" TargetControlID="cvlstSALEdit1"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnSubmitEdit" runat="server" CssClass="ButtonControl" Text="Save"
                                ValidationGroup="Edit" OnClick="btnSubmitEdit_Click" />
                            <asp:Button ID="btnCancelEdit" runat="server" CssClass="ButtonControl" Text="Cancel"
                                OnClick="btnCancelEdit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Label ID="lblErrorEdit" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeEditEmployeeAccess" runat="server" TargetControlID="btnDummyEdit"
        PopupControlID="pnlEditEmployeeAccess" BackgroundCssClass="modalBackground" Enabled="true"
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
    </script>
    <script type="text/javascript">
<!--

        //alert(myfilter);
//-->
    </script>
</asp:Content>
