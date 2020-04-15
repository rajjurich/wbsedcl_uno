<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="TimeEntry.aspx.cs" Inherits="UNO.TimeEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script type="text/javascript" src="riceBox/jquery-1.4.4.js"></script>--%>
    <script type="text/javascript" src="Scripts/Ricebox/ricePhoto1.js">  </script>
    <link rel="stylesheet" type="text/css" href="http://cdn.webrupee.com/font">
    <script src="http://cdn.webrupee.com/js" type="text/javascript"></script>
    <link rel="stylesheet" href="Scripts/TimePickerJquery/include/ui-1.10.0/ui-lightness/jquery-ui-1.10.0.custom.min.css"
        type="text/css" />
    <link rel="stylesheet" href="Scripts/TimePickerJquery/jquery.ui.timepicker.css?v=0.3.3" type="text/css" />
    <script type="text/javascript" src="Scripts/TimePickerJquery/include/jquery-1.9.0.min.js"></script>
    <script type="text/javascript" src="Scripts/TimePickerJquery/include/ui-1.10.0/jquery.ui.core.min.js"></script>
    <script type="text/javascript" src="Scripts/TimePickerJquery/include/ui-1.10.0/jquery.ui.widget.min.js"></script>
    <script type="text/javascript" src="Scripts/TimePickerJquery/include/ui-1.10.0/jquery.ui.tabs.min.js"></script>
    <script type="text/javascript" src="Scripts/TimePickerJquery/include/ui-1.10.0/jquery.ui.position.min.js"></script>
    <script type="text/javascript" src="Scripts/TimePickerJquery/jquery.ui.timepicker.js?v=0.3.3"></script>
    <style>
        .gridview
        {
            border: 5px solid #059EDC;
            border-radius: 15px;
        }
        .gridAlternate
        {
            background-color: #B2D3E0;
            border-bottom: 1px solid #444444;
        }
        .txtDays
        {
            width: 40px;
            max-width: 40px;
        }
        .calSelectedDates
        {
            background: #eeeeee url(images/h_31.gif) 50% top no-repeat;
        }
        
        .calAssignedActivity
        {
            background: #FF557F url(images/h_31.gif) 50% top no-repeat;
        }
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .display
        {
            display: none;
        }
    </style>
    <script>
        function validTime(sender) {
            try {
                var intRegex = /^\d+$/;
                var time = sender.value;
                var seperator = time.indexOf(":");
                var Hours = 0;
                var Mins = 0;
                if (time == "") {
                    return true;
                }
                if (seperator == -1) {
                    alert("Time entered is not in valid format");
                    sender.focus();
                    return false;
                }
                else {

                    Hours = parseInt(time.substring(0, seperator));
                    Mins = parseInt(time.substring(seperator + 1, time.length));
                    if (!intRegex.test(Hours)) {
                        alert("Not a valid Time1");
                        sender.focus();
                        return false;
                    }
                    else if (!intRegex.test(Mins)) {
                        alert(Mins);
                        alert("Not a valid Time");
                        sender.focus();
                        return false;
                    }
                    else if (Hours > 24) {
                        alert("hours should be less than 24");
                        sender.focus();
                        return false;
                    }
                    else if (Mins > 60) {
                        alert("Mins should be less than 60");
                        sender.focus();
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            }
            catch (ex) {
                alert(ex.Message);
            }
        }
    </script>
    <script>
        function calculateTotal(sender) {
            try {
                var RowNumber = sender.id;
                var prefix = "";
                var control = "";
                while (RowNumber.indexOf('_') != -1) {
                    prefix += RowNumber.substring(0, RowNumber.indexOf('_') + 1);
                    control = RowNumber.substring(0, RowNumber.indexOf('_') + 1);
                    RowNumber = RowNumber.substring((RowNumber.indexOf('_') + 1), RowNumber.length);
                }


                var TotalHours = 0;
                var TotalMins = 0;
                if ((document.getElementById(prefix.substring(0, 40) + "Sunday_" + RowNumber)).value != "") {
                    var current = (document.getElementById(prefix.substring(0, 40) + "Sunday_" + RowNumber)).value;
                    var seperator = current.indexOf(':');
                    var hours = current.substring(0, seperator);
                    var minutes = current.substring(seperator + 1, current.lenght);

                    TotalHours = parseInt(TotalHours) + parseInt(hours);
                    TotalMins = parseInt(TotalMins) + parseInt(minutes);
                    if (parseInt(TotalMins) > parseInt("60")) {
                        TotalHours = parseInt(TotalHours) + 1;
                        TotalMins = parseInt(TotalMins) - 60;
                    }
                }
                if ((document.getElementById(prefix.substring(0, 40) + "Monday_" + RowNumber)).value != "") {
                    var current = (document.getElementById(prefix.substring(0, 40) + "Monday_" + RowNumber)).value;
                    var seperator = current.indexOf(':');
                    var hours = current.substring(0, seperator);
                    var minutes = current.substring(seperator + 1, current.lenght);

                    TotalHours = parseInt(TotalHours) + parseInt(hours);
                    TotalMins = parseInt(TotalMins) + parseInt(minutes);
                    if (parseInt(TotalMins) > parseInt("60")) {
                        TotalHours = parseInt(TotalHours) + 1;
                        TotalMins = parseInt(TotalMins) - 60;
                    }

                }
                if ((document.getElementById(prefix.substring(0, 40) + "Tuesday_" + RowNumber)).value != "") {
                    var current = (document.getElementById(prefix.substring(0, 40) + "Tuesday_" + RowNumber)).value;
                    var seperator = current.indexOf(':');
                    var hours = current.substring(0, seperator);
                    var minutes = current.substring(seperator + 1, current.lenght);

                    TotalHours = parseInt(TotalHours) + parseInt(hours);
                    TotalMins = parseInt(TotalMins) + parseInt(minutes);
                    if (parseInt(TotalMins) > parseInt("60")) {
                        TotalHours = parseInt(TotalHours) + 1;
                        TotalMins = parseInt(TotalMins) - 60;
                    }

                }
                if ((document.getElementById(prefix.substring(0, 40) + "Wednesday_" + RowNumber)).value != "") {
                    var current = (document.getElementById(prefix.substring(0, 40) + "Wednesday_" + RowNumber)).value;
                    var seperator = current.indexOf(':');
                    var hours = current.substring(0, seperator);
                    var minutes = current.substring(seperator + 1, current.lenght);

                    TotalHours = parseInt(TotalHours) + parseInt(hours);
                    TotalMins = parseInt(TotalMins) + parseInt(minutes);
                    if (parseInt(TotalMins) > parseInt("60")) {
                        TotalHours = parseInt(TotalHours) + 1;
                        TotalMins = parseInt(TotalMins) - 60;
                    }

                }
                if ((document.getElementById(prefix.substring(0, 40) + "Thursday_" + RowNumber)).value != "") {
                    var current = (document.getElementById(prefix.substring(0, 40) + "Thursday_" + RowNumber)).value;
                    var seperator = current.indexOf(':');
                    var hours = current.substring(0, seperator);
                    var minutes = current.substring(seperator + 1, current.lenght);

                    TotalHours = parseInt(TotalHours) + parseInt(hours);
                    TotalMins = parseInt(TotalMins) + parseInt(minutes);
                    if (parseInt(TotalMins) > parseInt("60")) {
                        TotalHours = parseInt(TotalHours) + 1;
                        TotalMins = parseInt(TotalMins) - 60;
                    }

                }
                if ((document.getElementById(prefix.substring(0, 40) + "Friday_" + RowNumber)).value != "") {
                    var current = (document.getElementById(prefix.substring(0, 40) + "Friday_" + RowNumber)).value;
                    var seperator = current.indexOf(':');
                    var hours = current.substring(0, seperator);
                    var minutes = current.substring(seperator + 1, current.lenght);

                    TotalHours = parseInt(TotalHours) + parseInt(hours);
                    TotalMins = parseInt(TotalMins) + parseInt(minutes);
                    if (parseInt(TotalMins) > parseInt("60")) {
                        TotalHours = parseInt(TotalHours) + 1;
                        TotalMins = parseInt(TotalMins) - 60;
                    }

                }
                if ((document.getElementById(prefix.substring(0, 40) + "Saturday_" + RowNumber)).value != "") {
                    var current = (document.getElementById(prefix.substring(0, 40) + "Saturday_" + RowNumber)).value;
                    var seperator = current.indexOf(':');
                    var hours = current.substring(0, seperator);
                    var minutes = current.substring(seperator + 1, current.lenght);

                    TotalHours = parseInt(TotalHours) + parseInt(hours);
                    TotalMins = parseInt(TotalMins) + parseInt(minutes);
                    if (parseInt(TotalMins) > parseInt("60")) {
                        TotalHours = parseInt(TotalHours) + 1;
                        TotalMins = parseInt(TotalMins) - 60;
                    }

                }
                document.getElementById("ContentPlaceHolder1_ContentPlaceHolder1_gvEntry_lblTotal_" + RowNumber).innerHTML = TotalHours + ":" + TotalMins;
            }
            catch (ex) {
                alert(ex.Message);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%; height: 100%;">
        <table style="width: 100%; height: 100%; border: 0px solid #059EDC;">
            <tr>
                <td style="width: 20%; font-family: Arial;">
                    <div class="gridview" style="margin: 0% 10% 0% 20%;">
                        <asp:Calendar ID="calOverview" runat="server" FirstDayOfWeek="Sunday" SelectionMode="Day"
                            ShowGridLines="True" OnSelectionChanged="calOverview_SelectionChanged" Height="100px"
                            Width="100px" Style="max-height: 100px; max-width: 100px; font-size: xx-small;
                            border: 0px solid #059EDC; background-color: #059EDC; min-height: 100px; min-width: 100%;">
                            <SelectedDayStyle CssClass="calSelectedDates" />
                            <TitleStyle BackColor="#059EDC" />
                        </asp:Calendar>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 80%; font-size: small; font-family: Arial; text-align: center;
                    max-height: 200px; overflow: auto;">
                    <div class="gridview">
                        <asp:GridView ID="gvEntry" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvEntry_RowDataBound"
                            Width="100%" GridLines="None" BorderStyle="None">
                            <EmptyDataTemplate>
                                <span>No Activities to Display.</span>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#059EDC" />
                            <AlternatingRowStyle CssClass="gridAlternate" />
                            <Columns>
                                <asp:BoundField DataField="ProjectId" HeaderText="Project Code" ItemStyle-Width="10%"
                                    ItemStyle-CssClass="display" HeaderStyle-CssClass="display" />
                                <asp:BoundField DataField="MilestoneId" HeaderText="Milestone Id" ItemStyle-CssClass="display"
                                    HeaderStyle-CssClass="display" />
                                <asp:BoundField DataField="WBSID" HeaderText="WBS ID" ItemStyle-CssClass="display"
                                    HeaderStyle-CssClass="display" />
                                <asp:BoundField DataField="activity_id" HeaderText="Activity Id" ItemStyle-CssClass="display"
                                    HeaderStyle-CssClass="display" />
                                <%--<asp:BoundField DataField="Description" HeaderText="Activity" ItemStyle-Width="10%" />--%>
                                <asp:TemplateField HeaderText="Activity of Current Week" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlActivites" OnSelectedIndexChanged="ddlActivites_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem>Select.....</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sun" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFlagSunday" runat="server" Text="NEW" Style="display: none;"></asp:Label>
                                        <asp:TextBox ID="txtTimeEntrySunday" runat="server" CssClass="txtDays" onchange="calculateTotal(this);"
                                            onblur="return validTime(this);"></asp:TextBox>
                                        <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('#ContentPlaceHolder1_ContentPlaceHolder1_gvEntry_txtTimeEntrySunday_' + <%# Container.DataItemIndex %>).timepicker({
                                                showPeriodLabels: false
                                            });
                                        });

                                        </script>
                                        <pre id="script_noPeriodLabels" style="display: none" class="code">$('#timepicker').timepicker({showPeriodLabels: false,});</pre>
                                        <asp:HyperLink ID="lnkExpenseEntrySunday" runat="server" CssClass="WebRupee" title="<b>Conveyance Details</b>"
                                            rel="ricePhoto[frame]">Rs.</asp:HyperLink>
                                        <%--<asp:Button ID="btnExpenseEntrySunday" runat="server" Text="Rs." CssClass="WebRupee"
                                        CommandName="ExpenseEntry" CommandArgument='<%#Eval("PROJECT_CODE") + ";" +Eval("MILESTONE_ID") + ";" + Eval("WBS_ID") + ";" + Eval("ACTIVITY_ID") + ";" + "00009127" + ";" + "Sunday"%>' />--%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="gvcells" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mon" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFlagMonday" runat="server" Text="NEW" Style="display: none;"></asp:Label>
                                        <asp:TextBox ID="txtTimeEntryMonday" runat="server" CssClass="txtDays" onchange="calculateTotal(this);"
                                            onblur="return validTime(this);"></asp:TextBox>
                                        <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('#ContentPlaceHolder1_ContentPlaceHolder1_gvEntry_txtTimeEntryMonday_' + <%# Container.DataItemIndex %>).timepicker({
                                                showPeriodLabels: false
                                            });
                                        });

                                        </script>
                                        <pre id="script_noPeriodLabels" style="display: none" class="code">$('#timepicker').timepicker({showPeriodLabels: false,});</pre>
                                        <asp:HyperLink ID="lnkExpenseEntryMonday" runat="server" CssClass="WebRupee" title="<b>Conveyance Details</b>"
                                            rel="ricePhoto[frame]">Rs.</asp:HyperLink>
                                        <%--<asp:Button ID="btnExpenseEntryMonday" runat="server" Text="Rs." CssClass="WebRupee"
                                        CommandName="ExpenseEntry" CommandArgument='<%#Eval("PROJECT_CODE") + ";" +Eval("MILESTONE_ID") + ";" + Eval("WBS_ID") + ";" + Eval("ACTIVITY_ID") + ";" + "00009127" + ";" + "Monday"%>' />--%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="gvcells" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tue" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFlagTuesday" runat="server" Text="NEW" Style="display: none;"></asp:Label>
                                        <asp:TextBox ID="txtTimeEntryTuesday" runat="server" CssClass="txtDays" onchange="calculateTotal(this);"
                                            onblur="return validTime(this);"></asp:TextBox>
                                        <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('#ContentPlaceHolder1_ContentPlaceHolder1_gvEntry_txtTimeEntryTuesday_' + <%# Container.DataItemIndex %>).timepicker({
                                                showPeriodLabels: false
                                            });
                                        });

                                        </script>
                                        <pre id="script_noPeriodLabels" style="display: none" class="code">$('#timepicker').timepicker({showPeriodLabels: false,});</pre>
                                        <asp:HyperLink ID="lnkExpenseEntryTuesday" runat="server" CssClass="WebRupee" title="<b>Conveyance Details</b>"
                                            rel="ricePhoto[frame]">Rs.</asp:HyperLink>
                                        <%--<asp:Button ID="btnExpenseEntryTuesday" runat="server" Text="Rs." CssClass="WebRupee"
                                        CommandName="ExpenseEntry" CommandArgument='<%#Eval("PROJECT_CODE") + ";" +Eval("MILESTONE_ID") + ";" + Eval("WBS_ID") + ";" + Eval("ACTIVITY_ID") + ";" + "00009127" + ";" + "Tuesday"%>' />--%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="gvcells" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Wed" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFlagWednesday" runat="server" Text="NEW" Style="display: none;"></asp:Label>
                                        <asp:TextBox ID="txtTimeEntryWednesday" runat="server" CssClass="txtDays" onchange="calculateTotal(this);"
                                            onblur="return validTime(this);"></asp:TextBox>
                                        <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('#ContentPlaceHolder1_ContentPlaceHolder1_gvEntry_txtTimeEntryWednesday_' + <%# Container.DataItemIndex %>).timepicker({
                                                showPeriodLabels: false
                                            });
                                        });

                                        </script>
                                        <pre id="script_noPeriodLabels" style="display: none" class="code">$('#timepicker').timepicker({showPeriodLabels: false,});</pre>
                                        <asp:HyperLink ID="lnkExpenseEntryWednesday" runat="server" CssClass="WebRupee" title="<b>Conveyance Details</b>"
                                            rel="ricePhoto[frame]">Rs.</asp:HyperLink>
                                        <%--<asp:Button ID="btnExpenseEntryWednesday" runat="server" Text="Rs." CssClass="WebRupee"
                                        CommandName="ExpenseEntry" CommandArgument='<%#Eval("PROJECT_CODE") + ";" +Eval("MILESTONE_ID") + ";" + Eval("WBS_ID") + ";" + Eval("ACTIVITY_ID") + ";" + "00009127" + ";" + "Wednesday"%>' />--%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="gvcells" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Thu" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFlagThursday" runat="server" Text="NEW" Style="display: none;"></asp:Label>
                                        <asp:TextBox ID="txtTimeEntryThursday" runat="server" CssClass="txtDays" onchange="calculateTotal(this);"
                                            onblur="return validTime(this);"></asp:TextBox>
                                        <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('#ContentPlaceHolder1_ContentPlaceHolder1_gvEntry_txtTimeEntryThursday_' + <%# Container.DataItemIndex %>).timepicker({
                                                showPeriodLabels: false
                                            });
                                        });

                                        </script>
                                        <pre id="script_noPeriodLabels" style="display: none" class="code">$('#timepicker').timepicker({showPeriodLabels: false,});</pre>
                                        <asp:HyperLink ID="lnkExpenseEntryThursday" runat="server" CssClass="WebRupee" title="<b>Conveyance Details</b>"
                                            rel="ricePhoto[frame]">Rs.</asp:HyperLink>
                                        <%--<asp:Button ID="btnExpenseEntryThursday" runat="server" Text="Rs." CssClass="WebRupee"
                                        CommandName="ExpenseEntry" CommandArgument='<%#Eval("PROJECT_CODE") + ";" +Eval("MILESTONE_ID") + ";" + Eval("WBS_ID") + ";" + Eval("ACTIVITY_ID") + ";" + "00009127" + ";" + "Thursday"%>' />--%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="gvcells" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fri" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFlagFriday" runat="server" Text="NEW" Style="display: none;"></asp:Label>
                                        <asp:TextBox ID="txtTimeEntryFriday" runat="server" CssClass="txtDays" onchange="calculateTotal(this);"
                                            onblur="return validTime(this);"></asp:TextBox>
                                        <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('#ContentPlaceHolder1_ContentPlaceHolder1_gvEntry_txtTimeEntryFriday_' + <%# Container.DataItemIndex %>).timepicker({
                                                showPeriodLabels: false
                                            });
                                        });

                                        </script>
                                        <pre id="script_noPeriodLabels" style="display: none" class="code">$('#timepicker').timepicker({showPeriodLabels: false,});</pre>
                                        <asp:HyperLink ID="lnkExpenseEntryFriday" runat="server" CssClass="WebRupee" title="<b>Conveyance Details</b>"
                                            rel="ricePhoto[frame]">Rs.</asp:HyperLink>
                                        <%--<asp:Button ID="btnExpenseEntryFriday" runat="server" Text="Rs." CssClass="WebRupee"
                                        CommandName="ExpenseEntry" CommandArgument='<%#Eval("PROJECT_CODE") + ";" +Eval("MILESTONE_ID") + ";" + Eval("WBS_ID") + ";" + Eval("ACTIVITY_ID") + ";" + "00009127" + ";" + "Friday"%>' />--%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="gvcells" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sat" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFlagSaturday" runat="server" Text="NEW" Style="display: none;"
                                            onblur="return validTime(this);"></asp:Label>
                                        <asp:TextBox ID="txtTimeEntrySaturday" runat="server" CssClass="txtDays" onchange="calculateTotal(this);"></asp:TextBox>
                                        <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('#ContentPlaceHolder1_ContentPlaceHolder1_gvEntry_txtTimeEntrySaturday_' + <%# Container.DataItemIndex %>).timepicker({
                                                showPeriodLabels: false
                                            });
                                        });

                                        </script>
                                        <pre id="script_noPeriodLabels" style="display: none" class="code">$('#timepicker').timepicker({showPeriodLabels: false,});</pre>
                                        <asp:HyperLink ID="lnkExpenseEntrySaturday" runat="server" CssClass="WebRupee" title="<b>Conveyance Details</b>"
                                            rel="ricePhoto[frame]">Rs.</asp:HyperLink>
                                        <%--<asp:Button ID="btnExpenseEntrySaturday" runat="server" Text="Rs." CssClass="WebRupee"
                                        CommandName="ExpenseEntry" CommandArgument='<%#Eval("PROJECT_CODE") + ";" +Eval("MILESTONE_ID") + ";" + Eval("WBS_ID") + ";" + Eval("ACTIVITY_ID") + ";" + "00009127" + ";" + "Saturday"%>' />--%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="gvcells" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Time" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text="00:00"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Completed">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCompleted" runat="server" OnSelectedIndexChanged="ddlCompleted_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Button ID="btnSaveTimeEntry" runat="server" Text="Save" OnClick="btnSaveTimeEntry_Click" />
                    <asp:Button ID="btnSubmitTimeEntry" runat="server" Text="Submit" OnClick="btnSubmitTimeEntry_Click" />
                    <asp:Button ID="btnAddNewRow" runat="server" Text="Add New Row To Grid" OnClick="btnAddNewRow_Click" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblError" runat="server" Text="" Visible="false"></asp:Label>
    </div>
</asp:Content>
