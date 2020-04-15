<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="EventBrowser.aspx.cs" Inherits="UNO.EventBrowser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="SlickGrid/Styles/slick.grid.css" rel="stylesheet" type="text/css" />
    <link href="SlickGrid/Styles/slick.pager.css" rel="stylesheet" type="text/css" />
    <link href="SlickGrid/Styles/Smoothness/jquery-ui-1.8.16.custom.css" rel="stylesheet"
        type="text/css" />
    <link href="SlickGrid/Styles/examples.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        
    </style>
    <%--    Added by Pooja Yadav--%>
<%--    <script type="text/javascript" src="https://getfirebug.com/firebug-lite.js"></script>--%>
    <script src="SlickGrid/Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="SlickGrid/Scripts/jquery.event.drag-2.2.js" type="text/javascript"></script>
    <script src="SlickGrid/Scripts/slick.core.js" type="text/javascript"></script>
    <script src="SlickGrid/Scripts/slick.grid.js" type="text/javascript"></script>
    <script src="SlickGrid/Scripts/dataview.js" type="text/javascript"></script>
    <script src="SlickGrid/Scripts/slick.pager.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">


        window.setInterval(StartTimer, 90000);

        function StartTimer() {

            var Release = document.getElementById("<%=CmdPause.ClientID%>").value;
            if (Release == "Release") {
                document.getElementById('<%=btnFillData.ClientID%>').click();
                FillData();

            }
            else {
            }
        }

        function ChangeText() {
            if (document.getElementById("<%=CmdPause.ClientID%>").value == "Release") {
                document.getElementById("<%=CmdPause.ClientID%>").value = "Start";
                return false;
            }
            else {
                document.getElementById("<%=CmdPause.ClientID%>").value = "Release";
                return false;
            }
        }
        /////

        var sortcol = "Event_Datetime";
        var searchString = "";
        var pager;
        var columnpicker;
        var columnFilters = {};
        var dataView;
        var grid;
        var columns = [
                                { id: "Event_Type", name: "Type", field: "Event_Type", minWidth: 60, maxWidth: 60 },
                                { id: "Datetime", name: "Datetime", field: "Event_Datetime", formatter: dateFormatter, minWidth: 120, maxWidth: 120, sortable: true },
                                { id: "Empname", name: "Name", field: "Empname", width: 150 },
                                { id: "Event_Employee_Code", name: "Emp Code", field: "Event_Employee_Code", minWidth: 85, maxWidth: 85 },
                                { id: "Event_Card_Code", name: "Card Code", field: "Event_Card_Code", minWidth: 75, maxWidth: 75 },
                                { id: "ReaderD", name: "Reader", field: "ReaderD" },
                                { id: "CtrlD", name: "Controller", field: "CtrlD" },
                                { id: "Event_Status", name: "Status", field: "Event_Status", width: 100 },
                                { id: "Event_Alarm_Type", name: "A Type", field: "Event_Alarm_Type" },
                                { id: "Event_Alarm_Action", name: "A Action", field: "Event_Alarm_Action" },

                          ];

        var options = {
            enableCellNavigation: true,
            enableColumnReorder: false,
            autoHeight: false
        };



        function dateFormatter(row, cell, value, columnDef, dataContext) {

            var a = [];
            var a = value.toString().split(',');
            var month = value.getMonth() + 1;
            var dt = value.getDate();
            var hrs = value.getHours();
            var min = value.getMinutes();

            return (dt.toString().length == 1 ? ("0" + dt) : dt) + "/" + (month.toString().length == "1" ? "0" + month : month) + '/' + value.getFullYear() + '   ' + (hrs.toString().length == 1 ? ("0" + hrs) : hrs) + ":" + (min.toString().length == 1 ? ("0" + min) : min);
        }

        $(function () {
            FillData();

        });

        function FillData() {


            var myData = [];
            var Dt = [];
            var txtUserID;
            var txtLevelID;
            var RBLDataType = $("#<%=RBLDataType.ClientID%>").find(":checked").val();
            txtUserID = "";
            txtUserID = document.getElementById("<%=txtEmpID.ClientID %>").value;
            txtLevelID = document.getElementById("<%=txtEmpName.ClientID %>").value;
            dataView = new Slick.Data.DataView();
            grid = new Slick.Grid("#GridEvent", dataView, columns, options);

            var pager = new Slick.Controls.Pager(dataView, grid, $("#pager"));

            dataView.onRowCountChanged.subscribe(function (e, args) {
                grid.updateRowCount();
                grid.render();

                highlightthecells();
            });

            dataView.onRowsChanged.subscribe(function (e, args) {
                grid.invalidateRows(args.rows);
                grid.render();
                highlightthecells();

            });
          

            myData = $.parseJSON(document.getElementById('<%=hdnRecords.ClientID%>').value);
            if (myData != null) {
                for (var i = 0; i < myData.length; i++) {

                    Dt[i] =
                                                            {
                                                                id: "id_" + i,
                                                                Event_Type: myData[i].Event_Type,
                                                                Event_Datetime: new Date(parseInt(myData[i].Event_Datetime.substr(6))),
                                                                Empname: myData[i].Empname,
                                                                Event_Employee_Code: myData[i].Event_Employee_Code,
                                                                Event_Card_Code: myData[i].Event_Card_Code,
                                                                ReaderD: myData[i].ReaderD,
                                                                CtrlD: myData[i].CtrlD,
                                                                Event_Status: myData[i].Event_Status,
                                                                Event_Alarm_Type: myData[i].Event_Alarm_Type,
                                                                Event_Alarm_Action: myData[i].Event_Alarm_Action

                                                            };
                }
            }

            $("#" + "<%=txtEmpName.ClientID%>").keyup(function (e) {
                columnFilters["Empname"] = $.trim($(this).val());
                dataView.refresh();
            });

            $("#" + "<%=txtEmpID.ClientID%>").keyup(function (e) {
                columnFilters["Event_Employee_Code"] = $.trim($(this).val());
                dataView.refresh();
            });

            $("#" + "<%=txtEmpName.ClientID%>").keydown(function (e) {
                columnFilters["Empname"] = $.trim($(this).val());
                dataView.refresh();
            });

            $("#" + "<%=txtEmpID.ClientID%>").keydown(function (e) {
                columnFilters["Event_Employee_Code"] = $.trim($(this).val());
                dataView.refresh();
            });

            dataView.setItems(Dt);
            dataView.setFilter(filter);
            grid.onSort.subscribe(function (e, args) {
                var comparer = function (a, b) {
                    return (a[args.sortCol.field] > b[args.sortCol.field]) ? 1 : -1;
                }
                dataView.sort(comparer, args.sortAsc);
            });

            dataView.onPagingInfoChanged.subscribe(function (e, pagingInfo) {
                var isLastPage = pagingInfo.pageNum == pagingInfo.totalPages - 1;
                var enableAddRow = isLastPage || pagingInfo.pageSize == 0;
                var options = grid.getOptions();
                if (options.enableAddRow != enableAddRow) {
                    grid.setOptions({ enableAddRow: enableAddRow });
                }
            });


        } //MAIN fUNCTION CLOSE

        function filter(item) {
            for (var columnId in columnFilters) {
                if (columnId !== undefined && columnFilters[columnId] !== "") {
                    var c = grid.getColumns()[grid.getColumnIndex(columnId)];
                    var search = columnFilters[columnId].toLowerCase().replace(/\s/g, '');
                    if (item[c.field] != null) {
                        if (!(item[c.field].toLowerCase().replace(/\s/g, '').indexOf(search) != -1)) {
                            return false;
                        }
                    }
                    else
                        return true;
                }
            }
            return true;
        }

        function FillDataBasedOnRBL() {
            document.getElementById('<%=btnFillData.ClientID%>').click();
            FillData();
        }
        function OnLoad() {
            if (screen.width == 1024) { $("#div1").css("margin-left", "0%"); }
            else if (screen.width == 1366) { $("#div1").css("margin-left", "1%"); }
            else if (screen.width == 1360) { $("#div1").css("margin-left", "1%"); }
            else if (screen.width == 1280) { $("#div1").css("margin-left", "1%"); }

        }
        window.onload = OnLoad;

        function highlightthecells() {
            if (!("ActiveXObject" in window)) {



                //var tabs = getDiv(document.body, 'slick-cell l7 r7');
                var j0 = getDiv(document.body, "slick-cell l0 r0");
                var j1 = getDiv(document.body, "slick-cell l1 r1");
                var j2 = getDiv(document.body, "slick-cell l2 r2");
                var j3 = getDiv(document.body, "slick-cell l3 r3");
                var j4 = getDiv(document.body, "slick-cell l4 r4");
                var j5 = getDiv(document.body, "slick-cell l5 r5");
                var j6 = getDiv(document.body, "slick-cell l6 r6");

                var j8 = getDiv(document.body, "slick-cell l8 r8");
                var j9 = getDiv(document.body, "slick-cell l9 r9");
                var j7 = getDiv(document.body, "slick-cell l7 r7");
                for (var i = 0; i < j7.length; i++) {

                    if (j7[i].textContent == "Access Granted") {

                        $(j7[i]).addClass("active");
                        $(j0[i]).addClass("active");
                        $(j1[i]).addClass("active");
                        $(j2[i]).addClass("active");
                        $(j3[i]).addClass("active");
                        $(j4[i]).addClass("active");
                        $(j5[i]).addClass("active");
                        $(j6[i]).addClass("active");
                        $(j8[i]).addClass("active");
                        $(j9[i]).addClass("active");

                    }
                    else {
                        $(j0[i]).addClass("inactive");
                        $(j7[i]).addClass("inactive");
                        $(j1[i]).addClass("inactive");
                        $(j2[i]).addClass("inactive");
                        $(j3[i]).addClass("inactive");
                        $(j4[i]).addClass("inactive");
                        $(j5[i]).addClass("inactive");
                        $(j6[i]).addClass("inactive");
                        $(j8[i]).addClass("inactive");
                        $(j9[i]).addClass("inactive");
                    }

                }
            }
        }

        function getDiv(node, classname) {
            var a = [];
            var re = new RegExp('(^| )' + classname + '( |$)');
            var els = node.getElementsByTagName("*");
            for (var i = 0, j = els.length; i < j; i++)
                if (re.test(els[i].className)) a.push(els[i]);
            return a;
        }
        

       
    </script>
    <style>
        /*added by vaibhav */
        .DivEmpDetailsByVaibhav
        {
            text-align: center;
            margin-left: 14%;
            width: 990px; /*border: 1px solid #333333;*/
            border-radius: 15px;
            background-color: #47A3DA;
            color: Black; /*margin: 10px 10px 10px 10px;*/
            padding: 10px 10px 10px 10px; /*min-height: 200px;*/ /*font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;*/
            box-shadow: 10px 10px 5px #888888;
            
        }
        .active
        {
            background-color:Green;   
            color:Black;
        }
        .inactive
        
        {
            background-color:red;   
            color:Black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Event Viewer" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div class="DivEmpDetailsByVaibhav" >
        <asp:Panel ID="Panel1" runat="server" >
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: left; width: 5%;" valign="top">
                        <asp:Button ID="CmdPause" runat="server" Text="Release" CssClass="ButtonControl"
                            OnClientClick="return ChangeText();" />
                    </td>
                    <td style="width: 40%; color: #003300; display: inline;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: right;">
                                    <span>Select Event Type :</span>
                                </td>
                                <td style="text-align: left;padding-left:5px;">
                                    <asp:RadioButtonList ID="RBLDataType" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="True" ForeColor="#003300" Width="80%" onchange="FillDataBasedOnRBL();">
                                        <asp:ListItem Value="A" Selected="True">All</asp:ListItem>
                                        <asp:ListItem Value="01">Card</asp:ListItem>
                                        <asp:ListItem Value="02">Alarm</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <%--<td style="text-align: left;width:20%;">
                                    <span>Select Level:</span>
                                </td>--%>
                                <td style="text-align: left;width:10%;" valign="top">
                                    <asp:DropDownList ID="ddlLevel" runat="server" onchange="FillDataBasedOnRBL();">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem>Peripheral</asp:ListItem>
                                        <asp:ListItem>Second Level</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="text-align: left; width: 30%;" valign="middle">
                        <asp:TextBox ID="txtEmpName" MaxLength="12" runat="server" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtEEmpName" runat="server" TargetControlID="txtEmpName"
                            WatermarkText="Name" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:TextBox ID="txtEmpID" runat="server" CssClass="searchTextBox" Width="50%"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtEEmpID" runat="server" TargetControlID="txtEmpID"
                            WatermarkText="Employee ID" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:Button ID="btnFillData" runat="server" Text="Search" Style="visibility: hidden;"
                            OnClick="btnFillData_Click" CssClass="ButtonControl" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #EFF8FE; padding: 0px;" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div style="width: 100%; text-align: center; margin-left: 0%;" id="div1">
                                    <div style="width: 100%; text-align: center;" id="EventDiv">
                                        <div id="GridEvent" style="width: 935px; height: 400px;">
                                        </div>
                                        <div id="pager" style="width: 935px; height: 40px;">
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="btnFillData" />--%>
                                <asp:PostBackTrigger ControlID="btnFillData" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdnRecords" runat="server" ClientIDMode="Static" />
        </asp:Panel>
    </div>
</asp:Content>
