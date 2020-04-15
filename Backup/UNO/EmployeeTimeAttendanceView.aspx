<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ModuleMain.master"
    CodeBehind="EmployeeTimeAttendanceView.aspx.cs" Inherits="UNO.EmployeeTimeAttendanceView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
        .style37
        {
            width: 32%;
        }
        .style38
        {
            font-size: 9pt;
            font-family: verdana;
            color: #515456;
            border: 0px solid #CCCCCC;
            text-align: left;
            padding-left: 4px;
            width: 32%;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvTimeAttendanceView.ClientID%>');

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
        //Added by Pooja Yadav
        function GetSelectedValue() {
            var list = document.getElementById("<% =rblScheduleType.ClientID %>");
            var rdbtnLstValues = list.getElementsByTagName("input");
            var Checkdvalue;
            for (var i = 0; i < rdbtnLstValues.length; i++) {
                if (rdbtnLstValues[parseInt(i)].checked) {
                    Checkdvalue = rdbtnLstValues[parseInt(i)].value;
                    break;
                }
            }
            if (Checkdvalue == "Pattern") {
                FillPattern();
                ckhForDaily();
            }
            else if (Checkdvalue == "Fixed") {
                FillFixed();
            }
        }

        function FillFixed() {

            document.getElementById('<%=lblShiftType.ClientID%>').style.display = "block";
            document.getElementById('<%=ddlShiftPattern.ClientID%>').style.display = "none"
            document.getElementById('<%=ddlShift.ClientID%>').style.display = "block";
            $("#<%= ddweekoff.ClientID%> ").prop("disabled", false);
            $("#<%= ddWeekend.ClientID%>").prop("disabled", false);
        }


        function FillPattern() {

            document.getElementById('<%=lblShiftType.ClientID%>').style.display = "block";
            document.getElementById('<%=ddlShiftPattern.ClientID%>').style.display = "block";
            document.getElementById('<%=ddlShift.ClientID%>').style.display = "none"
        }

        function ckhForDaily() {
            var value = $("#<%= ddlShiftPattern.ClientID%> option:selected").text();
            var seperateby = value.lastIndexOf("-");
            //gets only the extension
            var ddlVal = value.substring(seperateby);

            if (ddlVal.slice(-5) == 'DAILY') {
                $("#<%= ddweekoff.ClientID%> ").prop("disabled", true);
                $("#<%= ddWeekend.ClientID%>").prop("disabled", true);

            }
            else {
                $("#<%= ddweekoff.ClientID%> ").prop("disabled", false);
                $("#<%= ddWeekend.ClientID%>").prop("disabled", false);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Attendence Configuration" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div class="DivEmpDetails">
        <table style="width: 100%;">
            <tr>
                <td style="width: 50%; text-align: left;">
                    <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click" />
                    <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                        Style="display: none" OnClick="btnDelete_Click" />
                </td>
                <td style="width: 50%; text-align: right;">
                    <asp:Button runat="server" ID="cmdReset" Text="Reset" Style="float: right; margin-left: 4px;"
                        CssClass="ButtonControl" OnClick="cmdReset_Click" />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                        OnClick="btnSearch_Click" />
                    <asp:TextBox ID="textshiftid" runat="server" Style="float: right;" CssClass="searchTextBox"
                        onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtzonename" runat="server" TargetControlID="textshiftid"
                        WatermarkText="Shift ID" WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    <asp:TextBox ID="textempid" runat="server" Style="float: right;" CssClass="searchTextBox"
                        onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twezoneid" runat="server" TargetControlID="textempid"
                        WatermarkText="Employee ID" WatermarkCssClass="watermark">
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
                            <asp:GridView ID="gvTimeAttendanceView" runat="server" AutoGenerateColumns="false"
                                Width="100%" GridLines="None" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvTimeAttendanceView_PageIndexChanging"
                                OnRowCommand="gvTimeAttendanceView_RowCommand">
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
                                                CommandArgument='<%#Eval("ROWID") %>' Text="Edit" ForeColor="#3366FF"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ETC_EMP_ID" HeaderText="Employee ID" SortExpression="ID">
                                        <%--  <ItemStyle HorizontalAlign="Left" Width="10%"  />--%>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                                    <asp:BoundField DataField="ETC_MINIMUM_SWIPE" HeaderText="Minimum swipe" SortExpression="Description">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ETC_SHIFTCODE" HeaderText="Shift / Pattern Id" SortExpression="Shift Code">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ETC_WEEKEND" HeaderText="Weekend" SortExpression="Weekend">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ETC_WEEKOFF" HeaderText="Weekoff" SortExpression="Weekoff">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ETC_SHIFT_START_DATE" HeaderText="Shift Start Date" SortExpression="Shift Start Date">
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
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
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
                <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
            <asp:AsyncPostBackTrigger ControlID="btnDelete" />
            <asp:AsyncPostBackTrigger ControlID="gvTimeAttendanceView" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnOk" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Button ID="btnTemp" runat="server" Text="temp" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="pops_modalTimeExtender" runat="server" TargetControlID="btnTemp"
        PopupControlID="pnlTime" DropShadow="true" BackgroundCssClass="modalBackground"
        CancelControlID="btnCancel">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlTime" runat="server" CssClass="PopupPanel" Style="background-color: White;
        border: 5px solid #000000;" Width="650px">
        <asp:UpdatePanel ID="updatepnlZone" runat="server">
            <ContentTemplate>
                <table class="TableClass" style="width: 95%; margin-left: 2%">
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <span class="text"><strong>MODIFY DETAILS</strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            Minimum Swipe:
                        </td>
                        <td class="style37">
                            <asp:TextBox ID="txtMinSwipe" runat="server" TabIndex="1"></asp:TextBox>
                        </td>
                        <td class="TDClassLabel">
                            <%-- Shift Code:--%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtShiftCode" runat="server" TabIndex="2" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            Week End:
                        </td>
                        <td class="style37">
                            <asp:DropDownList ID="ddWeekend" runat="server" TabIndex="3" ClientIDMode="Static"
                                Width="173px">
                                <%--  <asp:ListItem Value="null">Select One</asp:ListItem>
                                <asp:ListItem Value="MON">Monday</asp:ListItem>
                                <asp:ListItem Value="TUE">Tuesday</asp:ListItem>
                                <asp:ListItem Value="WED">Wednseday</asp:ListItem>
                                <asp:ListItem Value="THR">Thursday</asp:ListItem>
                                <asp:ListItem Value="FRI">Friday</asp:ListItem>
                                <asp:ListItem Value="SAT">Saturday</asp:ListItem>
                                <asp:ListItem Value="SUN">Sunday</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td class="TDClassLabel">
                            Week OFF:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddweekoff" runat="server" ClientIDMode="Static" CssClass="ComboControl"
                                TabIndex="4" ValidationGroup="A" Width="173px">
                                <%-- <asp:ListItem Value="null">Select One</asp:ListItem>
                                <asp:ListItem Value="MON">Monday</asp:ListItem>
                                <asp:ListItem Value="TUE">Tuesday</asp:ListItem>
                                <asp:ListItem Value="WED">Wednseday</asp:ListItem>
                                <asp:ListItem Value="THR">Thursday</asp:ListItem>
                                <asp:ListItem Value="FRI">Friday</asp:ListItem>
                                <asp:ListItem Value="SAT">Saturday</asp:ListItem>
                                <asp:ListItem Value="SUN">Sunday</asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator1" Operator="NotEqual" ControlToValidate="ddweekoff"
                                ControlToCompare="ddWeekend" Display="None" runat="server" ErrorMessage="Week end and week off can't be same."
                                ValidationGroup="A"></asp:CompareValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                TargetControlID="CompareValidator1" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="TDClassLabel">
                            Shift Start Date:
                        </td>
                        <td class="style37">
                            <asp:TextBox ID="txtshiftStartDate" runat="server" TabIndex="5" ClientIDMode="Static"
                                MaxLength="10"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calfrm" TargetControlID="txtshiftStartDate" PopupButtonID="txtshiftStartDate"
                                runat="server" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <%--     <td class="TDClassControl">
                            <asp:DropDownList ID="ddlScheduleType" runat="server" CssClass="ComboControl" TabIndex="6"
                                ClientIDMode="Static" >
                                <asp:ListItem Value="null">Select One</asp:ListItem>
                                <asp:ListItem Value="Pattern">Pattern</asp:ListItem>
                                <asp:ListItem Value="Fixed">Fixed</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlScheduleType" runat="server" ControlToValidate="ddlScheduleType"
                                ErrorMessage="Plz. select  Shedule Type." ForeColor="Red" Style="font-family: Verdana;
                                font-size: 9pt;" InitialValue="null" Font-Size="Medium" ValidationGroup="ADD"
                                Display="None"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceddlScheduleType" runat="server" TargetControlID="rfvddlScheduleType"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>--%>
                    </tr>
                    <tr>
                        <td class="TDClassLabel">
                            <asp:Label ID="Label2" runat="server" Text="Schedule Type"></asp:Label>
                            :
                        </td>
                        <td class="style38">
                            <asp:RadioButtonList ID="rblScheduleType" runat="server" CssClass="ComboControl"
                                TabIndex="4" ClientIDMode="Static" RepeatDirection="Horizontal" onchange="GetSelectedValue();"
                                onclick="GetSelectedValue();">
                                <asp:ListItem Value="Pattern">Pattern</asp:ListItem>
                                <asp:ListItem Value="Fixed" Selected="True">Fixed</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvddlScheduleType" runat="server" ControlToValidate="rblScheduleType"
                                ErrorMessage="Plz. select  Shedule Type." ForeColor="Red" Style="font-family: Verdana;
                                font-size: 9pt;" InitialValue="null" Font-Size="Medium" ValidationGroup="ADD"
                                Display="None"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceddlScheduleType" runat="server" TargetControlID="rfvddlScheduleType"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td class="TDClassLabel">
                             <asp:Label ID="lblShiftType" runat="server" Text="Select Shift :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlShiftPattern" runat="server" onchange="ckhForDaily();" onkeyup="ckhForDaily();"
                                onkeydown="ckhForDaily();" Width="173px" style="display:none;">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlShift" runat="server" Width="173px" style="display:none;">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hdnRowID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnOk" runat="server" Text="Modify" CssClass="ButtonControl" OnClick="btnOk_Click"
                                ValidationGroup="A" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="ButtonControl"
                                OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rblScheduleType" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
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
<%--  <ItemStyle HorizontalAlign="Left" Width="10%"  />--%>
