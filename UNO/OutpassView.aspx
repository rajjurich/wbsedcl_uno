<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="OutpassView.aspx.cs" Inherits="UNO.OutpassView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvOutpass.ClientID %>');

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
    <%--<link href="Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript">

        function clearFunction(x) {
            document.getElementById(x).innerHTML = "&nbsp;&nbsp;";
        }
        function sameDate() {
            var fdate = document.getElementById('<%= txtFromDate.ClientID %>').value;
            if (fdate != "") {
                var splitfdate = fdate.split('/');
                var concateFdate = splitfdate[1] + "/" + splitfdate[0] + "/" + splitfdate[2];
                var frmDate = new Date(concateFdate);
                var todate = splitfdate[0] + "/" + splitfdate[1] + "/" + splitfdate[2];
                document.getElementById('<%= txtToDate.ClientID %>').value = todate;
            }
        }


        function chkDate() {

            try {
                var fdate = document.getElementById('<%= txtFromDate.ClientID %>').value;
                if (fdate != "") {
                    var splitfdate = fdate.split('/');
                    var concateFdate = splitfdate[1] + "/" + splitfdate[0] + "/" + splitfdate[2];
                    var frmDate = new Date(concateFdate);
                    var tdate = document.getElementById('<%= txtToDate.ClientID %>').value;
                    var splitTdate = tdate.split('/');
                    var concateTdate = splitTdate[1] + "/" + splitTdate[0] + "/" + splitTdate[2];
                    var toDate = new Date(concateTdate);

                    if (frmDate > toDate)
                        sameDate();
                }
            }
            catch (e) {
                alert(e);
            }
        }

        function handleAdd() {

            if (document.getElementById('<%=txtFromDate.ClientID%>').value == '') {
                document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please enter From Date';
                return false;
            }
            else if (document.getElementById('<%=txtToDate.ClientID%>').value == '') {
                document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please enter To Date';
                return false;
            }
            if (!CompareDates(document.getElementById('<%=txtFromDate.ClientID%>'), document.getElementById('<%=txtToDate.ClientID%>'))) {
                document.getElementById('<%= lblmsg.ClientID %>').innerHTML = "To Date should not be less than From date";
                return false;
            }
            else if (document.getElementById('<%=frm_time.ClientID%>').value == '') {
                document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please Enter In Time';
                return false;
            }
            else if (document.getElementById('<%=To_Time.ClientID%>').value == '') {
                document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please Enter Out Time';
                return false;
            }

            if (document.getElementById('<%=frm_time.ClientID%>').value != "") {
                if (!(ValidateTime(document.getElementById('<%=frm_time.ClientID%>').value))) {
                    document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please enter valid From Time';
                    return false;
                }
            }
            if (document.getElementById('<%=To_Time.ClientID%>').value != "") {
                if (!(ValidateTime(document.getElementById('<%=To_Time.ClientID%>').value))) {
                    document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please enter valid To Time';
                    return false;
                }
            }
            if ($("#<%=ddlReasonType.ClientID%> option:selected").val() == 'Select One') {
                document.getElementById('<%=lblmsg.ClientID%>').innerHTML = 'Please select Reason';
                return false;
            }
            document.getElementById('<%=lblmsg.ClientID%>').innerHTML = "";
            return true;

        }

      
    </script>
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="OutPass View" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <asp:Panel ID="panel1" runat="server" DefaultButton="btnSearch">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%; text-align: left;">
                            <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click"
                                CausesValidation="false" />
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                                ClientIDMode="Static" OnClick="btnDelete_Click1" />
                        </td>
                        <td style="width: 50%; text-align: right;">
                            <asp:TextBox ID="txtCompanyId" runat="server" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCallStatus" runat="server" TargetControlID="txtCompanyId"
                                WatermarkText="Employee Code" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                TargetControlID="txtCompanyName" WatermarkText="Employee Name" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="ButtonControl"
                                ClientIDMode="Static" OnClick="cmdSearch_Click" />
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
                                    <asp:GridView ID="gvOutpass" runat="server" AutoGenerateColumns="false" Width="100%"
                                        ClientIDMode="Static" GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="gvOutpass_RowCommand">
                                        <RowStyle CssClass="gvRow" />
                                        <HeaderStyle CssClass="gvHeader" />
                                        <AlternatingRowStyle BackColor="#F0F0F0" />
                                        <PagerStyle CssClass="gvPager" />
                                        <EmptyDataTemplate>
                                            <div>
                                                <span>No Records Found</span>
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" SortExpression="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" SortExpression="Edit" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandName="Edit1"
                                                        Text="Edit" CommandArgument='<%#Eval("OP_RECID") %>' ForeColor="#3366FF"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("OP_RECID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OP_EMPID" HeaderText="Employee Code" SortExpression="Employee Code">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Name" HeaderText="Employee Name" SortExpression="name">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FromDate" HeaderText="From Date" SortExpression="Out">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ToDate" HeaderText="To Date" SortExpression="Out">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="In_Time" HeaderText="InTime" SortExpression="In_Time">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Out_Time" HeaderText="OutTime" SortExpression="OutTime">
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
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <%-- <asp:AsyncPostBackTrigger ControlID="btnAdd" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="gvOutpass" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                <%--   <asp:AsyncPostBackTrigger ControlID="btnSubmitAdd" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmitEdit" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <table class="MessageContainerTable" width="98.8%">
        <tr>
            <td colspan="4" align="center" valign="middle">
                <div id="messageDiv" runat="server" clientidmode="Static" class="MessageClass" style="color: Red">
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Settings" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="table2" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                    class="TableClass">
                    <tr>
                        <td class="TDClassLabel" style="text-align: Right; width: 56%">
                            <asp:Label ID="Label1" runat="server" Text="Employee Code :"></asp:Label>
                            <label class="CompulsaryLabel">
                                *</label>
                        </td>
                        <td class="TDClassLabel" style="text-align: left;">
                            <asp:TextBox ID="txtEmployeeCode" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;">
                        </td>
                    </tr>
                    <tr id="Empdata" valign="top">
                        <td class="TDClassLabel">
                            Employee Name:<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassLabel" style="text-align: left" valign="top">
                            <input type="hidden" name="ListBox1Hidden" />
                            <asp:TextBox ID="txtEmployeeName" runat="server" ClientIDMode="Static" Enabled="false"
                                Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            From Date :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" onKeyPress="javascript: return false "
                                onKeydown="javascript: return false " onchange="chkDate()" onblur="sameDate()"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                PopupButtonID="txtFromDate" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            To Date :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" onKeyPress="javascript: return false "
                                onKeydown="javascript: return false " onchange="chkDate()"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                PopupButtonID="txtToDate" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="text-align: Right;">
                            From Time :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="text-align: left">
                            <asp:TextBox ID="frm_time" runat="server" CssClass="TextControl" MaxLength="5" onkeypress="findspace(event)"
                                onkeyup="fnColon(this,event)" TabIndex="4"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTEfrm_time" runat="server" FilterType="Custom,Numbers
" TargetControlID="frm_time" ValidChars=":" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            To Time :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:TextBox ID="To_Time" runat="server" CssClass="TextControl" MaxLength="5" onkeypress="findspace(event)"
                                onkeyup="fnColon(this,event)" TabIndex="4"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTETo_Time" runat="server" FilterType="Custom,Numbers
" TargetControlID="To_Time" ValidChars=":" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            Select Reason :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl">
                            <asp:DropDownList ID="ddlReasonType" runat="server" ClientIDMode="Static" Width="180px"
                                Height="20px">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1_empid11" runat="server" ControlToValidate="ddlReasonType"
                                Display="Dynamic" ErrorMessage="Please select Reason" ForeColor="Red" InitialValue="Select One"
                                SetFocusOnError="True" ValidationGroup="edit"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;">
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="TDClassLabel">
                            &nbsp;Remarks :
                        </td>
                        <td id="tdtextfrm_Time" runat="server" clientidmode="Static" class="TDClassControl"
                            style="height: 10px; width: 50%">
                            <asp:TextBox ID="txt_Remarks" runat="server" TextMode="MultiLine" Style="resize: none"
                                ClientIDMode="Static" Width="173px" onkeyDown="checkLength(this,'50');" onkeyUp="checkLength(this,'50');"
                                onkeypress="checkLength(this,'50');" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="TableClass" id="table3" runat="server" cellpadding="3" cellspacing="3"
                    width="95%">
                    <tr>
                        <td style="width: 7%">
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonControl" OnClientClick="return handleAdd()"
                                Text="Save" TabIndex="6" Style="float: right" OnClick="btnSave_Click"  ValidationGroup="edit" />
                        </td>
                        <td>
                            <asp:Button ID="Btnclear" runat="server" Text="Cancel" CssClass="ButtonControl" TabIndex="7"
                                CausesValidation="False" OnClick="Btnclear_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                            <asp:HiddenField ID="hdnRowID" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="btnModify" runat="server" Style="display: none" CssClass="ButtonControl"
        Font-Bold="true" TabIndex="5" Text="Modify" />
    <ajaxToolkit:ModalPopupExtender ID="mpModifyOutpass" runat="server" TargetControlID="btnModify"
        PopupControlID="Settings" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="Btnclear">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
