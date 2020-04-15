<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="EnrollmentView.aspx.cs" Inherits="UNO.EnrollmentView" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvEmployee.ClientID %>');

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
    <script type="text/javascript" language="javascript">
        function clearFunction(x) {
            document.getElementById(x).innerHTML = "&nbsp;&nbsp;";
        }

        function handleDelete() {
            if (!validateCheckBoxes())
                return false;

            var msg = confirm("Record(s) marked for Deletion. Continue?");
            if (msg == false) {
                clearGridCheckBoxes();
                return false;
            }
        }

        function clearGridCheckBoxes() {
            var gridRef = document.getElementById('<%= gvEmployee.ClientID %>');
            var inputElementArray = gridRef.getElementsByTagName('input');
            var cntchk = 0;
            for (var i = 0; i < inputElementArray.length; i++) {
                var elementRef = inputElementArray[i];

                if ((elementRef.type == 'checkbox')) {
                    elementRef.checked = false;
                }
            }
        }

        function validateCheckFocus() {

            var isValid = false;

            var gridView = document.getElementById('<%= gvEmployee.ClientID %>');

            var inputs = gridView.getElementsByTagName('input');

            for (var i = 0; i < inputs.length; i++) {

                var elementRef = inputs[i];

                if ((elementRef.type == 'checkbox') && (elementRef.checked == true)) {
                    document.getElementById('btnDelete').focus();
                    isValid = true;

                    return true;


                }
            }

        }

        function validateCheckBoxes() {

            var isValid = false;

            var gridView = document.getElementById('<%= gvEmployee.ClientID %>');

            var inputs = gridView.getElementsByTagName('input');

            for (var i = 0; i < inputs.length; i++) {

                var elementRef = inputs[i];

                if ((elementRef.type == 'checkbox') && (elementRef.checked == true)) {
                    isValid = true;

                    return true;


                }
            }
            alert("Please select record");
            return false;
        }
    </script>
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="Label1" runat="server" Text="Employee Enrollment View" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%; text-align: left;">
                        <asp:DropDownList CssClass="ComboControl" ClientIDMode="Static" ID="ddlType" runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                            <asp:ListItem>All Employee</asp:ListItem>
                            <asp:ListItem>Not Enrolled</asp:ListItem>
                            <asp:ListItem>Card Not Issued</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" ClientIDMode="Static" CssClass="ButtonControl"
                            OnClick="btnDelete_Click" OnClientClick="return handleDelete()" />
                    </td>
                    <td style="width: 50%; text-align: right;">
                        <asp:TextBox ID="txtemp_code" runat="server" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                            TargetControlID="txtemp_code" WatermarkText="Search by Employee Code" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:TextBox ID="txtemp_name" runat="server" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWEtxtCallDate" runat="server" TargetControlID="txtemp_name"
                            WatermarkText="Search by Employee Name" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="ButtonControl"
                            ClientIDMode="Static" OnClick="btnSearch_Click" />
                        <asp:Button runat="server" ID="cmdReset" Text="Reset" CssClass="ButtonControl" OnClick="cmdReset_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                        <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                            <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <%--  <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="False"  Width="100%"
          GridLines="None" AllowPaging="true" PageSize="10"
         ondatabound="Grid_DataBound" 
        onpageindexchanging="Grid_PageIndexChanging" CellPadding="0" 
        onrowediting="Grid_RowEditing" >--%>
                                <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false" Width="100%"
                                    OnRowEditing="Grid_RowEditing" GridLines="None" AllowPaging="true" PageSize="10"
                                    OnDataBound="Grid_DataBound"  OnRowDataBound="gvEmployee_RowDataBound">
                                    <RowStyle CssClass="gvRow" />
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                    <PagerStyle CssClass="gvPager" />
                                    <EmptyDataTemplate>
                                        <div>
                                            <span>No Record found.</span>
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" SortExpression="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ShowHeader="False" SortExpression="Edit" ItemStyle-Width="10%">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                    Text="Update" ForeColor="#3366FF"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Text="Cancel" ForeColor="#3366FF"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                    Text="Edit" ForeColor="#3366FF"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EPD_EMPID" HeaderText="Employee ID" SortExpression="Employee ID">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EPD_CARD_ID" HeaderText="Card Code" SortExpression="Card Code">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EPD_EMP_NAME" HeaderText="Employee Name" SortExpression="Employee Name">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DEPARTMENT" HeaderText="Department" SortExpression="Department">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DESIGNATION" HeaderText="Designation" SortExpression="Designation">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="STATUS" HeaderText="Status" SortExpression="Status"></asp:BoundField>
                                        <asp:BoundField DataField="Format_Type" HeaderText="Template Format" SortExpression="Template Format">
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
                <div>
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <%-- <asp:AsyncPostBackTrigger ControlID="btnAdd" />--%>
              <%--  <asp:AsyncPostBackTrigger ControlID="btnDelete" />--%>
                <asp:AsyncPostBackTrigger ControlID="gvEmployee" />
                <%--<asp:AsyncPostBackTrigger ControlID="btnSearch" />--%>
                <%--   <asp:AsyncPostBackTrigger ControlID="btnSubmitAdd" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmitEdit" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <table class="MessageContainerTable" width="98.8%">
        <tr>
            <td colspan="4" align="center" valign="middle">
                <div id="messageDiv" runat="server" clientidmode="Static" class="MessageClass" style="color:Red">
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
