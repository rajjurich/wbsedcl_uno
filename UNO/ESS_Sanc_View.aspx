<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ESS_Sanc_View.aspx.cs" Inherits="UNO.ESS_Sanc_View" Culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvManualAttnd.ClientID %>');

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

    <script language="javascript" type="text/javascript">
        var submit = 0;
        function CheckIsRepeat() {
            if (++submit > 1) {
                alert('An attempt was made to submit this form more than once; this extra attempt will be ignored.');
                return false;
            }
        }
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">

        function clearFunction(x) {
            document.getElementById(x).innerHTML = "&nbsp;&nbsp;";
        }


        function hidecolumn() {
            //alert("abc");
            // var GridViewId = document.getElementById('<%= gvManualAttnd.ClientID %>');
            //alert(GridViewId);
            //alert("abs3")
            //for (var intC = 0; intC < [GridViewId].Rows.Count; intC++)

            //  [GridViewId].Rows[intC].Cells[2].Style.Add("display", "block");

            //alert([GridViewId].Rows[intC].Cells[2]);

            //alert("abs");
        }

        function hideColumn1() {

            //  rows = document.getElementById('<%= gvManualAttnd.ClientID %>').rows;
            // alert(rows);
            // for (i = 0; i < rows.length; i++) {
            //   alert("abc");
            //  rows[i].cells[1].style.display = "block";
            //}
        }
        function handleApprove() {
                
                                
            if (!validateCheckBoxes())
                return false;


            var msg = confirm("Record(s) marked for Approval. Continue? ");
            if (msg == false) {
                clearGridCheckBoxes();
                return false;
            }
        }

        function handleReject() {

            if (!validateCheckBoxes())
                return false;


            var msg = confirm("Record(s) marked for Reject. Continue? ");
            if (msg == false) {
                clearGridCheckBoxes();
                return false;
            }
        }

        function validateCheckFocus() {

            var isValid = false;

            var gridView = document.getElementById('<%= gvManualAttnd.ClientID %>');

            var inputs = gridView.getElementsByTagName('input');

            for (var i = 0; i < inputs.length; i++) {

                var elementRef = inputs[i];

                if ((elementRef.type == 'checkbox') && (elementRef.checked == true)) {

                    document.getElementById('btnApprove').focus();
                    isValid = true;

                    return true;


                }
            }

        }

        function clearGridCheckBoxes() {
            var gridRef = document.getElementById('<%= gvManualAttnd.ClientID %>');
            var inputElementArray = gridRef.getElementsByTagName('input');
            var cntchk = 0;
            for (var i = 0; i < inputElementArray.length; i++) {
                var elementRef = inputElementArray[i];

                if ((elementRef.type == 'checkbox')) {
                    elementRef.checked = false;
                }
            }
        }


        function validateCheckBoxes() {

            var isValid = false;

            var gridView = document.getElementById('<%= gvManualAttnd.ClientID %>');

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





        //        function Button1_onclick() {

        //        }

        //        window.onload = hidecolumn();

    </script>
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Sanction" ForeColor="RoyalBlue" Font-Size="20px"
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
                        <asp:Button runat="server" ID="btnApprove" Text="Approve" CssClass="ButtonControl"
                            OnClientClick="return handleApprove()" OnClick="btnApprove_Click" ClientIDMode="Static" />
                        <asp:Button runat="server" ID="btnDelete" Text="Reject" CssClass="ButtonControl"
                            OnClientClick="return handleReject()" ClientIDMode="Static" OnClick="btnDelete_Click" />
                        Select Entity Type:
                        <asp:DropDownList ID="cmbEntity" runat="server" CssClass="ComboControl" AutoPostBack="True"
                            OnSelectedIndexChanged="cmbEntity_SelectedIndexChanged">
                            <asp:ListItem Value="LA">Leave Application</asp:ListItem>
                            <asp:ListItem Value="MA">Manual Attendance</asp:ListItem>
                            <asp:ListItem Value="OD">OutDoor Duty</asp:ListItem>
                            <asp:ListItem Value="GP">Out-Pass</asp:ListItem>
                            <asp:ListItem Value="CO">Compensatory Off</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; &nbsp; &nbsp;
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ComboControl" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" Height="16px" >
                            <asp:ListItem Value="ALL" Selected="True">All</asp:ListItem>
                            <asp:ListItem Value="N">Pending</asp:ListItem>
                            <asp:ListItem Value="A">Approved</asp:ListItem>
                            <asp:ListItem Value="R">Rejected</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 60%; text-align: right;">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                            OnClick="btnSearch_Click" />
                        <asp:TextBox ID="txtTodate" runat="server" Style="float: right;" CssClass="searchTextBox" onKeyPress="javascript: return false " onKeydown="javascript: return false "></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCallStatus" runat="server" TargetControlID="txtTodate"
                            WatermarkText="Search by To Date" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTodate"
                            PopupButtonID="txtTodate" Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtTodate"
                            Display="None" ValidationGroup="Search" ErrorMessage="Please enter date in dd/mm/yyyy format"
                            ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                            TargetControlID="RegularExpressionValidator3" PopupPosition="Right">
                        </ajaxToolkit:ValidatorCalloutExtender>
                        <asp:TextBox ID="txtFromDate" runat="server" Style="float: right;" CssClass="searchTextBox"  onKeyPress="javascript: return false " onKeydown="javascript: return false "></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWEtxtCallDate" runat="server" TargetControlID="txtFromDate"
                            WatermarkText="Search by From Date" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <ajaxToolkit:CalendarExtender ID="caltxtCallDate" runat="server" TargetControlID="txtFromDate"
                            PopupButtonID="txtFromDate" Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtFromDate"
                            Display="None" ValidationGroup="Search" ErrorMessage="Please enter date in dd/mm/yyyy format"
                            ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                            TargetControlID="RegularExpressionValidator4" PopupPosition="Right">
                        </ajaxToolkit:ValidatorCalloutExtender>
                        <asp:TextBox ID="txtEmpCode" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                            TargetControlID="txtEmpCode" WatermarkText="Search by Employee code" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:TextBox ID="txtName" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                            TargetControlID="txtName" WatermarkText="Search by Name" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                        <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                            <img src="images/276.gif" alt="Loading ...." />
                        </div>
                        
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                  <asp:GridView ID="gvManualAttnd" runat="server" AutoGenerateColumns="False" Width="100%"
                                    AllowPaging="True" OnPageIndexChanging="gvManualAttnd_PageIndexChanging" 
                                    OnRowEditing="gvManualAttnd_RowEditing" ClientIDMode="Static" OnRowCommand="gvManualAttnd_RowCommand2"
                                    PageSize="10" GridLines="None" OnRowDataBound="gvManualAttnd_RowDataBound">
                                    <RowStyle CssClass="gvRow"  ForeColor="Black" />
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle BackColor="#F0F0F0"  />
                                    <PagerStyle CssClass="gvPager" />
                                    <EmptyDataTemplate>
                                        <div>
                                            <span>No Records.</span>
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" SortExpression="Select" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                              <asp:CheckBox ID="ChkAll" runat="server" Text=" Select All"  AutoPostBack="true"
                            ClientIDMode="Static" Onclick="validateCheckFocus()" 
                            oncheckedchanged="ChkAll_CheckedChanged" />
                                        </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                                <span style="visibility:hidden"> Select All</span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" SortExpression="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                    Text="Add Remark" ForeColor="Black"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID" SortExpression="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Code" SortExpression="EmpCode">
                                            <ItemTemplate>
                                                 <asp:Label ID="lblEmpCode" runat="server" Text='<%#Eval("EmpID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" SortExpression="EmpCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LeaveCode" SortExpression="LeaveCode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblleavecde" runat="server" Text='<%#Eval("LeaveCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From Date" SortExpression="FromDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFrmDt" runat="server" Text='<%#Eval("FromDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To Date" SortExpression="ToDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblToDt" runat="server" Text='<%#Eval("ToDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStats" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reason" SortExpression="Reason">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mgrid" SortExpression="Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMgrID" runat="server" Text='<%#Eval("Mgrid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MgrRemark" SortExpression="MgrRemark" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMgrRemark" runat="server" Text='<%#Eval("MgrRemark") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Code" SortExpression="Project Code" >
                                            <ItemTemplate>
                                                <asp:Label ID="projectCode" runat="server" Text='<%#Eval("ProjectCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="TotalDays" SortExpression="TotalDays">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalDays" runat="server" Text='<%#Eval("TotalDays") %>'></asp:Label>
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
                <div>
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <%-- <asp:AsyncPostBackTrigger ControlID="btnAdd" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="gvManualAttnd" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                <%--   <asp:AsyncPostBackTrigger ControlID="btnSubmitAdd" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmitEdit" />--%>
            </Triggers>
        </asp:UpdatePanel>
        <div class="DivEmpDetails">
            <div style="color: Black; text-align: left">
                <b>Requests Pending For Approval : </b>Leave Application : <b><font color="white">
                    <asp:Label runat="server" ID="lblLa" Style="padding-right: 8%"></asp:Label></font></b>
                Manual Attendance : <b><font color="white">
                    <asp:Label runat="server" ID="lblMa" Style="padding-right: 8%"> </asp:Label></font></b>
                OutDoor Duty : <b><font color="white">
                    <asp:Label runat="server" ID="lblOd" Style="padding-right: 8%"> </asp:Label></font></b>
                Out-Pass : <b><font color="white">
                    <asp:Label runat="server" ID="lblGP" Style="padding-right: 8%"> </asp:Label></font></b>
                Compensatory Off : <b><font color="white">
                    <asp:Label runat="server" ID="lblCo" Style="padding-right: 8%"></asp:Label></font></b>
            </div>
        </div>
    </center>
    <asp:Panel ID="pnlAddCall" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Size="Large" Text="Please Enter Comment"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Comment : <font color="red">*</font>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSanctRemarks" runat="server" TextMode="MultiLine" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Comment"
                                ValidationGroup="Add" ControlToValidate="txtSanctRemarks" Display="None"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                TargetControlID="RequiredFieldValidator1" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnSave" CssClass="ButtonControl" runat="server"  Text="Save" ValidationGroup="Add" OnClick="btnSave_Click" />
                            <asp:Button ID="btnclose" CssClass="ButtonControl" runat="server" Text="Cancel" OnClick="btnclose_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Label ID="lblPnlMesg" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddCall" runat="server" TargetControlID="Button2"
        PopupControlID="pnlAddCall" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnclose">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
