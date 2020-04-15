<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="Manager_Dashboard.aspx.cs" Inherits="UNO.Manager_Dashboard" %>

<%@ Register Assembly="Syncfusion.EJ" Namespace="Syncfusion.JavaScript.DataVisualization.Models"
    TagPrefix="ej" %>
<%@ Register Assembly="Syncfusion.EJ" Namespace="Syncfusion.JavaScript.Models" TagPrefix="ej" %>
<%@ Register Assembly="Syncfusion.EJ" Namespace="Syncfusion.JavaScript.DataVisualization.Models"
    TagPrefix="ej" %>
<%@ Register assembly="Syncfusion.EJ" namespace="Syncfusion.JavaScript.DataVisualization.Models" tagprefix="ej" %>
<%@ Register assembly="Syncfusion.EJ" namespace="Syncfusion.JavaScript.DataVisualization.Models" tagprefix="ej" %>
<%@ Register assembly="Syncfusion.EJ" namespace="Syncfusion.JavaScript.DataVisualization.Models" tagprefix="ej" %>
<%@ Register assembly="Syncfusion.EJ" namespace="Syncfusion.JavaScript.DataVisualization.Models" tagprefix="ej" %>
<%@ Register assembly="Syncfusion.EJ" namespace="Syncfusion.JavaScript.DataVisualization.Models" tagprefix="ej" %>
<%@ Register assembly="Syncfusion.EJ" namespace="Syncfusion.JavaScript.DataVisualization.Models" tagprefix="ej" %>
<%@ Register assembly="Syncfusion.EJ" namespace="Syncfusion.JavaScript.DataVisualization.Models" tagprefix="ej" %>
<%@ Register assembly="Syncfusion.EJ" namespace="Syncfusion.JavaScript.DataVisualization.Models" tagprefix="ej" %>
<%@ Register assembly="Syncfusion.EJ" namespace="Syncfusion.JavaScript.DataVisualization.Models" tagprefix="ej" %>
<%@ Register assembly="Syncfusion.EJ" namespace="Syncfusion.JavaScript.DataVisualization.Models" tagprefix="ej" %>
<%@ Register assembly="Syncfusion.EJ" namespace="Syncfusion.JavaScript.DataVisualization.Models" tagprefix="ej" %>
<%@ Register assembly="Syncfusion.EJ" namespace="Syncfusion.JavaScript.DataVisualization.Models" tagprefix="ej" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .popup0
        {
            /*left: 265px !important;*/
            /*left: 20% !important;*/
            top: 250px !important;
        }
        .popup1
        {
             /* left: 702px !important;*/
             left: 51% !important;
            top: 250px !important;
        }
        .belowpopup
        {
          /*  left: 265px !important;*/
            /* left: 20% !important;*/
            top: 582px !important;
        }
        .tooltipDiv
        {
            background-color: #F0F8FF !important;
            color: white;
            border: 2px solid #6d6d6d;
            box-shadow: -3px -2px 3px #1F0000;
            opacity: 0.7;
            filter: alpha(opacity=70);
        }
        .DivSection
        {
            background-color: #F0F8FF !important;
            color: Black;
            border: 1px solid #6d6d6d;
            box-shadow: -3px -2px 3px #1F0000;
            filter: alpha(opacity=100);
            width: 400px;
            height: 300px;
        }
        .DivSectionBelow
        {
            background-color: #F0F8FF !important;
            color: Black;
            border: 1px solid #6d6d6d;
            box-shadow: -3px -2px 3px #1F0000;
            filter: alpha(opacity=100);
            width: 400px;
            height: 330px;
        }
        .SpanPresent
        {
            background-color: #F6B53F !important;
            color: White;
            border: 1px solid #6d6d6d;
            box-shadow: -3px -2px 3px #1F0000;
            filter: alpha(opacity=100);
            height: 114px;
        }
        .SpanPresent:hover
        {
            background-color: #F6B53F !important;
            color: Black;
            border: 1px solid #6d6d6d;
            box-shadow: -3px -2px 3px #1F0000;
            filter: alpha(opacity=30);
            cursor: pointer;
        }
        .SpanAbsent
        {
            background-color: #E94649 !important;
            color: White;
            border: 1px solid #6d6d6d;
            box-shadow: -3px -2px 3px #1F0000;
            filter: alpha(opacity=100);
            height: 114px;
        }
        .SpanAbsent:hover
        {
            background-color: #E94649 !important;
            color: Black;
            border: 1px solid #6d6d6d;
            box-shadow: -3px -2px 3px #1F0000;
            filter: alpha(opacity=30);
            cursor: pointer;
        }
        .SpanOutdoor
        {
            background-color: #C4C24A !important;
            color: White;
            border: 1px solid #6d6d6d;
            box-shadow: -3px -2px 3px #1F0000;
            filter: alpha(opacity=100);
            height: 114px;
        }
        .SpanOutdoor:hover
        {
            background-color: #C4C24A !important;
            color: Black;
            border: 1px solid #6d6d6d;
            box-shadow: -3px -2px 3px #1F0000;
            filter: alpha(opacity=30);
            cursor: pointer;
        }
        .SpanLeave
        {
            background-color: #6FAAB0 !important;
            color: White;
            border: 1px solid #6d6d6d;
            box-shadow: -3px -2px 3px #1F0000;
            filter: alpha(opacity=100);
            height: 114px;
        }
        .SpanLeave:hover
        {
            background-color: #6FAAB0 !important;
            color: Black;
            border: 1px solid #6d6d6d;
            box-shadow: -3px -2px 3px #1F0000;
            filter: alpha(opacity=30);
            cursor: pointer;
        }
        .ExcelDiv
        {
            background-color: #F0F8FF !important;
            color: Black;
            border: 1px solid #6d6d6d;
            box-shadow: -3px -2px 3px #1F0000;
            filter: alpha(opacity=30);
            height: 32px;
            padding-left: 3px;
            padding-top: 3px;
        }
        #Tooltip #value
        {
            text-align: center;
            float: right;
            height: 46px;
            width: 130px;
            background-color: #F0F8FF;
            padding-top: 2px;
            padding-left: 3px;
        }
        #Tooltip #value > div
        {
            /* margin: 3px 5px 5px 5px;*/
        }
        #Tooltip #efpercentage
        {
            font-size: 12px;
            font-family: segoe ui;
            color: #800000;
            font-weight: bold;
        }
        #Tooltip #ef
        {
            font-size: 15px; /*  font-family: segoe ui;*/
            font-weight: bold;
            color: Black;
        }
        .btn
        {
            visibility: hidden;
        }
        .Excelbtn
        {
            background: #3498db;
            background-image: -webkit-linear-gradient(top, #3498db, #2980b9);
            background-image: -moz-linear-gradient(top, #3498db, #2980b9);
            background-image: -ms-linear-gradient(top, #3498db, #2980b9);
            background-image: -o-linear-gradient(top, #3498db, #2980b9);
            background-image: linear-gradient(to bottom, #3498db, #2980b9);
            font-family: Arial;
            color: #ffffff;
            font-size: 100%;
            padding: 2px 10px 1px 10px;
            text-decoration: none;
            width: 45%;
        }
        
        .Excelbtn:hover
        {
            background: #3cb0fd;
            background-image: -webkit-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -moz-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -ms-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -o-linear-gradient(top, #3cb0fd, #3498db);
            background-image: linear-gradient(to bottom, #3cb0fd, #3498db);
            cursor: pointer;
            text-decoration: none;
        }
        .SearchBox
        {
            background-color: #989898;
            border: none;
        }
    </style>
    <script type="text/javascript">

        $(function Main() {
            
            $('.SpanPresent').bind('click', function (e) {

                if ($("#Present").text() == "0") { return; }
                //alert($("#Present").text());
                $find("bPresent").show();

            });
            $('.SpanOutdoor').bind('click', function (e) {

                if ($("#OutDoor").text() == "0") { return; }
                //alert($("#OutDoor").text());
                $find("bOutdoor").show();

            });
            $('.SpanLeave').bind('click', function (e) {

                if ($("#OnLeave").text() == "0") { return; }
                //alert($("#OnLeave").text());
                $find("bLeave").show();

            });
            $('.SpanAbsent').bind('click', function (e) {

                if ($("#Absent").text() == "0") { return; }
                //alert($("#Absent").text());
                $find("bAbsent").show();

            });
            $("#btnBack").css("visibility", "hidden");
            //  $("#btnTread").css("visibility", "hidden");
            // alert("start");
            $.ajax({
                type: "POST",
                url: "Manager_Dashboard.aspx/Dowork",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    var obj = $.parseJSON(msg.d);
                    window.Main = obj["Table"];
                    window.Absent = obj["Table1"];
                    window.Present = obj["Table2"];
                    window.Leave = obj["Table3"];
                    window.Outdoor = obj["Table4"];
                    for (var i = 0; i < obj["Table"].length; i++) {
                        if (obj["Table"][i]["XValue"] == "Absent") {
                            $("#Absent").text(obj["Table"][i]["YValue"]);
                        }
                        else if (obj["Table"][i]["XValue"] == "Present") {
                            $("#Present").text(obj["Table"][i]["YValue"]);
                        }
                        else if (obj["Table"][i]["XValue"] == "Leave") {
                            $("#OnLeave").text(obj["Table"][i]["YValue"]);
                        }
                        else if (obj["Table"][i]["XValue"] == "Outdoor") {
                            $("#OutDoor").text(obj["Table"][i]["YValue"]);
                        }
                    }
                    if ($("#Absent").text() == "") { $("#Absent").text("0") }
                    if ($("#Present").text() == "") { $("#Present").text("0") }
                    if ($("#OnLeave").text() == "") { $("#OnLeave").text("0") }
                    if ($("#OutDoor").text() == "") { $("#OutDoor").text("0") }




                    // alert(window.Main);
                    if (window.Main == "") {
                        window.Main = [{ XValue: "No Record", YValue: -1}];
                        //alert("window.Main:" + window.Main);
                    }

                },
                error: function (data, errorThrown) {
                    //alert('request failed :' + errorThrown);
                }
            });
            $.ajax({
                type: "POST",
                url: "Manager_Dashboard.aspx/BindClientSide",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    var obj = $.parseJSON(msg.d);
                    window.SplineArea = obj["Table"];
                    if (window.SplineArea == "") { window.SplineArea = [{ Xvalue: "No Record", YValue: 0}] }

                    window.Pie3D = obj["Table2"];
                    if (window.Pie3D == "") { window.Pie3D = [{ Xvalue: "No Record", YValue: -1}] }
                    //alert(window.SplineArea + ":" + window.Pie3D);
                },
                error: function (data, errorThrown) {
                    //  alert('request failed :' + errorThrown);
                }
            });
            $("#ExpenseChart").ejChart(
                        {
                            series: [{
                                type: 'pie',
                                name: "ExpenseChart",
                                //                                enableAnimation: true,
                                labelPosition: 'outside',
                                marker: { connectorLine: { height: 20 }, dataLabel: { visible: true} },
                                border: { width: 1 },
                                //                                explode: true,
                                dataSource: window.Main,
                                xName: "XValue",
                                yName: "YValue"

                            }, ],
                            pointRegionClick: 'onclick',
                            animationComplete: 'completeAnimation',
                            seriesRendering: "seriesRender",
                            loaded: 'chartLoaded'

                        });
            $("#ContentPlaceHolder1_ContentPlaceHolder1_target_WaitingPopup").hide();

            $("#SplineArea").ejChart(
                        {
                            series: [{
                                type: 'SplineArea',
                                enableAnimation: true,
                                explode: true,
                                dataSource: window.SplineArea,
                                xName: "Xvalue",
                                yName: "YValue"

                            }, ]
                        });
            $("#WaitingPopup_SplineArea_WaitingPopup").hide();
            $("#Pie3D").ejChart(
                        {
                            series: [{
                                type: 'Pie',
                                enableAnimation: true,
                                explode: true,
                                dataSource: window.Pie3D,
                                xName: "Xvalue",
                                yName: "YValue"

                            }, ]
                        });


            $("#WaitingPopup_Pie3D_WaitingPopup").hide();
            $('#SelectShift').change(function () {
                $("#WaitingPopup_Pie3D_WaitingPopup").show();

                var SelectedText = $(this).find(":selected").text();
                var SelectedValue = $(this).val();

                $.ajax({
                    type: "POST",
                    url: "Manager_Dashboard.aspx/GetShiftValue",
                    contentType: "application/json; charset=utf-8",
                    data: "{'value':'" + SelectedValue + "'}",
                    dataType: "json",
                    async: false,
                    success: function (msg) {
                        var obj = $.parseJSON(msg.d);
                        window.Pie3D = obj["Table1"];
                        window.error = null;

                        try {
                            $("#Pie3D").ejChart(
                                            {
                                                series: [{
                                                    type: 'Pie',
                                                    enableAnimation: true,
                                                    explode: false,
                                                    dataSource: window.error,
                                                    xName: "Xvalue1",
                                                    yName: "YValue1"

                                                }, ]
                                            });
                        } catch (e) { alert(e) }
                        if (window.Pie3D == "") {

                            window.Pie3D = [{ Xvalue: "No Record", YValue: -1}]
                            $("#Pie3D").ejChart(
                                            {
                                                series: [{
                                                    type: 'Pie',
                                                    enableAnimation: true,
                                                    explode: true,
                                                    dataSource: window.Pie3D,
                                                    xName: "Xvalue",
                                                    yName: "YValue"

                                                }, ]
                                            });
                        }
                        else {
                            $("#Pie3D").ejChart(
                                            {
                                                series: [{
                                                    type: 'Pie',
                                                    enableAnimation: true,
                                                    explode: true,
                                                    dataSource: window.Pie3D,
                                                    xName: "Xvalue",
                                                    yName: "YValue"

                                                }, ]
                                            });
                        }
                        $("#WaitingPopup_Pie3D_WaitingPopup").hide();
                    },
                    error: function (data, errorThrown) {
                        $("#WaitingPopup_Pie3D_WaitingPopup").hide();
                    }
                });

                $("#WaitingPopup_Pie3D_WaitingPopup").hide();

            });

            // setInterval(Main, 30000);
        });


        function onclick(sender) {

            if (sender.model.animationComplete) {
                var index = sender.data.region.Region.PointIndex;
                var name = sender.model.series[0].points[index].x;

                if (sender.model.series[0].name == "ExpenseChart")
                    $("#ExpenseChart").ejChart("option", { "drilldown": pieSeries(name) });
            }
        }
        function chartLoaded(sender) {
            sender.model.series[0].animation = false;
        }
        function completeAnimation(sender) {
            if (sender.model.series[0].name != "ExpenseChart")
                $("#btnBack").css("visibility", "visible");
            else
                $("#btnBack").css("visibility", "hidden");
        }
        function btnClick() {
            try {
                $("#ExpenseChart").ejChart("option", { "pie": pieSeries() });
            } catch (e) { }
            $("#ExpenseChart").ejChart("option", { "pie": pieSeries("ExpenseChart") });
            $("#btnBack").css("visibility", "hidden");
            // document.getElementById("back").style.visibility = "hidden";
        }
        function seriesRender(e) {
            //Adding text to the series points to chart
            $.each(e.data.series.points, function () {
                this.text = this.x + "- " + formatNumber(parseInt((this.YValues)));
            });
            if (Math.abs(e.model.m_AreaBounds.Width - e.model.m_AreaBounds.Height) < 50)
                e.data.series.pieCoefficient = 0.3;
            if (Math.abs(e.model.m_AreaBounds.Width - e.model.m_AreaBounds.Height) < 100)
                e.data.series.pieCoefficient = 0.4;
            else if (Math.abs(e.model.m_AreaBounds.Width - e.model.m_AreaBounds.Height) < 150)
                e.data.series.pieCoefficient = 0.5;
            else if (Math.abs(e.model.m_AreaBounds.Width - e.model.m_AreaBounds.Height) < 200)
                e.data.series.pieCoefficient = 0.6;
            else
                e.data.series.pieCoefficient = 0.8;
        }
        function formatNumber(number) {

            return Globalize.format(number, "n0"); //formating numbers  for spent amounts
        }


        function pieSeries(seriesName) {

            //Choosing the series points based on the series name to drilldown chart
            switch (seriesName) {
                case "Absent":
                    {
                        return {
                            series: [{
                                dataSource: window.Absent,
                                xName: "XValue",
                                yName: "YValue",
                                type: 'pie',
                                labelPosition: 'outside',
                                pieCoefficient: 0.4,
                                enableAnimation: true,
                                explode: false,
                                marker: {
                                    dataLabel: {
                                        visible: true,
                                        shape: 'none',
                                        connectorLine: { color: 'black', width: 0.5 },
                                        font: { fontFamily: 'Segoe UI', fontStyle: 'Normal ', fontWeight: 'Regular', size: '12px', color: '#707070', opacity: 1 }
                                    }
                                }

                            }],
                            legend: { visible: false }
                        };
                    }
                    break;
                case "Present":
                    {

                        return {
                            series: [{
                                dataSource: window.Present,
                                xName: "XValue",
                                yName: "YValue",
                                type: 'pie',
                                labelPosition: 'outside',
                                pieCoefficient: 0.4,
                                enableAnimation: true,
                                explode: false,
                                marker: {
                                    dataLabel: {
                                        visible: true,
                                        shape: 'none',
                                        connectorLine: { color: 'black', width: 0.5 },
                                        font: { fontFamily: 'Segoe UI', fontStyle: 'Normal ', fontWeight: 'Regular', size: '12px', color: '#707070', opacity: 1 }
                                    }
                                }
                            }],
                            legend: { visible: false }
                        };
                    }
                    break;
                case "Leave":
                    {
                        return {
                            series: [{
                                dataSource: window.Leave,
                                xName: "XValue",
                                yName: "YValue",
                                type: 'pie',
                                labelPosition: 'outside',
                                pieCoefficient: 0.4,
                                explode: false,
                                enableAnimation: true,
                                marker: {
                                    dataLabel: {
                                        visible: true,
                                        shape: 'none',
                                        connectorLine: { color: 'black', width: 0.5 },
                                        font: { fontFamily: 'Segoe UI', fontStyle: 'Normal ', fontWeight: 'Regular', size: '12px', color: '#707070', opacity: 1 }
                                    }
                                }
                            }],
                            legend: { visible: false }
                        };
                    }
                    break;
                case "Outdoor":
                    {
                        return {
                            series: [{
                                dataSource: window.Outdoor,
                                xName: "XValue",
                                yName: "YValue",
                                type: 'pie',
                                labelPosition: 'outside',
                                pieCoefficient: 0.4,
                                explode: false,
                                enableAnimation: true,
                                marker: {
                                    dataLabel: {
                                        visible: true,
                                        shape: 'none',
                                        connectorLine: { color: 'black', width: 0.5 },
                                        font: { fontFamily: 'Segoe UI', fontStyle: 'Normal ', fontWeight: 'Regular', size: '12px', color: '#707070', opacity: 1 }
                                    }
                                }
                            }],
                            legend: { visible: false }
                        };
                    }
                    break;
                case "ExpenseChart":
                    {
                        return {
                            series: [{
                                name: "ExpenseChart",
                                dataSource: window.Main,
                                xName: "XValue",
                                yName: "YValue",
                                type: 'pie',
                                labelPosition: 'outside',
                                pieCoefficient: 0.4,
                                explode: false,
//                                explodeOffset: 25,
                                enableAnimation: true,
                                marker: {
                                    dataLabel: {
                                        visible: true,
                                        shape: 'none',
                                        connectorLine: { color: 'black', width: 0.5 },
                                        font: { fontFamily: 'Segoe UI', fontStyle: 'Normal ', fontWeight: 'Regular', size: '12px', color: '#707070', opacity: 1 }
                                    }
                                }
                            }],
                            legend: { visible: false },
                            AnimationComplete: true
                        };
                    }
                    break;
                default:
                    {

                        return {
                            series: [{
                                name: "ExpenseChart",
                                dataSource: null,
                                xName: "XValue1",
                                yName: "YValue1",
                                type: 'pie',
                                labelPosition: 'outside',
                                pieCoefficient: 0.4,
                                explode: false,
//                                explodeOffset: 25,
                                enableAnimation: true,
                                marker: {
                                    dataLabel: {
                                        visible: true,
                                        shape: 'none',
                                        connectorLine: { color: 'black', width: 0.5 },
                                        font: { fontFamily: 'Segoe UI', fontStyle: 'Normal ', fontWeight: 'Regular', size: '12px', color: '#707070', opacity: 1 }
                                    }
                                }
                            }],
                            legend: { visible: false },
                            AnimationComplete: true
                        };

                    }
            }


        }
      
          
    </script>
    <script type="text/javascript">


        function isValidateIssueDate(oSrc, args) {

            if (!CompareDates(document.getElementById('<%= txtFromDate.ClientID %>'), document.getElementById('<%= txtToDate.ClientID %>'))) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }

        }
    </script>
    <script type="text/javascript">

        function SearchWord(txtSearch, TreeGrid) {

            $('#' + TreeGrid).find('tr:gt(0)').hide();
            var data = $('#' + txtSearch).val();
            var len = data.length;
            if (len > 0) {
                $('#' + TreeGrid).find('tbody tr').each(function () {
                    coldata = $(this).children().eq(0);
                    var temp = coldata.text().toUpperCase().indexOf(data.toUpperCase());
                    if (temp === 0) {
                        $(this).show();
                    }
                });
            } else {
                $('#' + TreeGrid).find('tr:gt(0)').show();
            }
        }
      
      
    </script>

       <script type="text/javascript">

           function sameDate() {

               var fdate = document.getElementById('<%= txtFromDate.ClientID %>').value;

               var splitfdate = fdate.split('/');
               //alert(splitfdate[2]);
               var concateFdate = splitfdate[1] + "/" + splitfdate[0] + "/" + splitfdate[2];
               // alert(concateFdate);
               //alert('test');
               //alert(concateFdate);
               var frmDate = new Date(concateFdate);

               // alert(frmDate);


               var todate = splitfdate[0] + "/" + splitfdate[1] + "/" + splitfdate[2];

               document.getElementById('<%= txtToDate.ClientID %>').value = todate;

           }




           function chkDate() {

               try {
                   var fdate = document.getElementById('<%= txtFromDate.ClientID %>').value;

                   var splitfdate = fdate.split('/');
                   var concateFdate = splitfdate[1] + "/" + splitfdate[0] + "/" + splitfdate[2];
                   var frmDate = new Date(concateFdate);
                   var tdate = document.getElementById('<%= txtToDate.ClientID %>').value;
                   var splitTdate = tdate.split('/');
                   var concateTdate = splitTdate[1] + "/" + splitTdate[0] + "/" + splitTdate[2];
                   var toDate = new Date(concateTdate);

                   if (frmDate > toDate) {

                       sameDate();


                   }
               }
               catch (e) {
                   alert(e);
               }
           }
    
    </script>

    <link href="Syncfusion/Content/ej/ej.widgets.core.min.css" rel="stylesheet" type="text/css" />
    <link href="Syncfusion/Content/ej/default-theme/ej.theme.min.css" rel="stylesheet"
        type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Manager Dashboard" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div id="Tooltip" style="display: none;">
        <div id="icon">
            <div id="eficon">
            </div>
        </div>
        <div id="value">
            <div>
                <label id="efpercentage">
                    Employee Count: &nbsp;#point.y#</label>
                <br />
                <label id="ef">
                    #point.x#</label>
            </div>
        </div>
    </div>
    <div style="text-align: center; width: 100%;">
        <center>
            <table cellpadding="20" cellspacing="20">
                <tr>
                    <td>
                        <div class="DivSection">
                            <div style="width: 100%; text-align: center; font-size: 20px; color: #565656;">
                                Attendance Today</div>
                            <div>
                                <div style="float: left; padding-left: 3px;">
                                    <ej:Button ID="btnBack" Text="Back" Size="Mini" ClientSideOnClick="btnClick" Type="Button"
                                        ClientIDMode="Static" runat="server">
                                    </ej:Button>
                                </div>
                                <div>
                                  <%--  <center>--%>
                                        <div>
                                            <ej:WaitingPopup ID="target" runat="server" CssClass="popup0" ShowOnInit="true">
                                            </ej:WaitingPopup>
                                        </div>
                                        <ej:Chart runat="server" ClientIDMode="Static" ID="ExpenseChart" OnClientLoad="loadTheme"
                                            CanResize="false"  OnClientPointRegionClick="onclick" OnClientSeriesRendering="completeAnimation"
                                            Height="250" Width="400" >
                                            <Legend Visible="false" Alignment="Far" Position="Top" />
                                            <CommonSeriesOptions Type="Pie" EnableAnimation="false">
                                                <Marker>
                                                    <DataLabel Shape="None" Visible="true" TextPosition="Bottom">
                                                        <Border Width="1" />
                                                        <ConnectorLine Height="20" />
                                                    </DataLabel>
                                                </Marker>
                                            </CommonSeriesOptions>
                                            <Series>
                                                <ej:Series Tooltip-Visible="true" Tooltip-Template="Tooltip">
                                                </ej:Series>
                                            </Series>
                                        </ej:Chart>
                                  <%--  </center>--%>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="DivSection">
                            <div style="width: 100%; text-align: center; font-size: 20px; color: #565656;">
                                Today's Trend</div>
                            <div>
                           
                                <div style="float: left; padding-left: 3px; padding-bottom: 5px;">
                                    <%--    <ej:Button ID="btnTread"  Text="Back" Size="Mini" ClientIDMode="Static"
                                        Type="Button" runat="server" >
                                    </ej:Button>--%>
                                </div>
                             
                                <div>
                                    <ej:WaitingPopup ID="WaitingPopup_SplineArea" CssClass="popup1" runat="server" ClientIDMode="Static"
                                        ShowOnInit="true">
                                    </ej:WaitingPopup>
                                </div>
                                <ej:Chart ID="SplineArea" ClientIDMode="Static" runat="server" Width="400" Height="250"
                                    CanResize="false">
                                    <PrimaryXAxis AxisLine-Visible="false" MajorGridLines-Visible="false" MinorGridLines-Visible="false"
                                        Title-Text="Today Trend" LabelIntersectAction="Rotate45" />
                                    <PrimaryYAxis Title-Text="Attendance Trend" MajorTickLines-Visible="false" AxisLine-Visible="false"
                                        LabelFormat="value" />
                                    <CommonSeriesOptions EnableAnimation="True" Type="SplineArea" Border-Color="transparent"
                                        Opacity="0.5" PieCoefficient="0.5" Border-Width="4" Fill="#333333" Marker-Shape="Circle"
                                        Marker-Size-Width="8" Marker-Size-Height="8" Marker-Visible="true" Marker-Fill="#C4C24A"
                                        Marker-Border-Color="#333333" Marker-Border-Width="4">
                                    </CommonSeriesOptions>
                                    <Series>
                                        <ej:Series Name="Today Trend" Fill="#C4C24A" Opacity="0.5" Tooltip-Visible="true"
                                            Tooltip-Template="Tooltip">
                                        </ej:Series>
                                    </Series>
                                    <Legend Visible="false"></Legend>
                                </ej:Chart>
                            
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="DivSectionBelow">
                            <div style="width: 100%; text-align: center; font-size: 20px; color: #565656; margin-bottom: 10px;">
                                In-Time Statistics</div>
                            <div style="float: left; padding-left: 3px">
                                <asp:DropDownList ID="SelectShift" runat="server" Width="150px" ClientIDMode="Static"
                                    Height="25px">
                                </asp:DropDownList>
                            </div>
                            <div>
                                <div>
                                    <ej:WaitingPopup ID="WaitingPopup_Pie3D" runat="server" CssClass="belowpopup" ClientIDMode="Static"
                                        ShowOnInit="true">
                                    </ej:WaitingPopup>
                                </div>
                                <ej:Chart ID="Pie3D" runat="server" ClientIDMode="Static" Width="400" Height="270"
                                    Depth="20" WallSize="20" Tilt="-30" Rotation="-30" PerspectiveAngle="90" SideBySideSeriesPlacement="false"
                                    EnableRotation="true" Enable3D="true" CanResize="false">
                                    <Series>
                                        <ej:Series Type="Pie" LabelPosition="Outside" PieCoefficient="0.7" XName="Xvalue"
                                            YName="YValue" StartAngle="145" ExplodeIndex="0">
                                            <Border Width="2" Color="White" />
                                        </ej:Series>
                                    </Series>
                                    <Legend Visible="true" Shape="Diamond" Position="Bottom"></Legend>
                                    <CommonSeriesOptions EnableAnimation="true" LabelPosition="Outside" Tooltip-Visible="true"
                                        Tooltip-Template="Tooltip" Tooltip-EnableAnimation="true">
                                        <Marker>
                                            <DataLabel Shape="None" Visible="false" TextPosition="Top">
                                                <Border Width="1" />
                                                <ConnectorLine Height="30" Color="Black" />
                                            </DataLabel>
                                        </Marker>
                                    </CommonSeriesOptions>
                                </ej:Chart>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="DivSectionBelow">
                            <table width="100%" cellspacing="15">
                                <tr>
                                    <td colspan="2">
                                        <div class="ExcelDiv">
                                            <asp:TextBox ID="txtFromDate" Height="22px" runat="server" Width="26%" onchange="chkDate()" onblur="sameDate()"  ></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate"
                                                Animated="true" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="txtFromDate" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                            <%--<asp:RequiredFieldValidator ID="rfvtxtFromDate" runat="server" ControlToValidate="txtFromDate"
                                                Display="none" ErrorMessage="Please enter From Date" SetFocusOnError="True" ForeColor="Red"
                                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtFromDate" runat="server" TargetControlID="rfvtxtFromDate"
                                                PopupPosition="right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            <asp:RegularExpressionValidator ID="revtxtFromDate" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ValidationGroup="Add" ErrorMessage="Please enter date in dd/MM/yyyy format"
                                                ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtFromDate" runat="server" TargetControlID="revtxtFromDate"
                                                PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtFromDate" runat="server" TargetControlID="txtFromDate"
                                                WatermarkText="From Date" WatermarkCssClass="watermark">
                                            </ajaxToolkit:TextBoxWatermarkExtender>--%>
                                            <asp:TextBox ID="txtToDate" Height="22px" runat="server" Width="26%"  onchange="chkDate()" ></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                                PopupButtonID="txtToDate" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                            <%--<asp:CustomValidator ID="CustomValidatorM_Lab_From" runat="server" ClientValidationFunction="isValidateIssueDate"
                                                ValidationGroup="Add" ControlToValidate="txtToDate" Display="None" ErrorMessage="To Date should not be less than From date"
                                                ForeColor="Red" SetFocusOnError="True"></asp:CustomValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vceCustomValidatorM_Lab_From" runat="server"
                                                TargetControlID="CustomValidatorM_Lab_From" PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            <asp:RegularExpressionValidator ID="revtxtToDate" runat="server" ControlToValidate="txtToDate"
                                                Display="None" ValidationGroup="Add" ErrorMessage="Please enter date in dd/MM/yyyy format"
                                                ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtToDate" runat="server" TargetControlID="revtxtToDate"
                                                PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="tewtxtToDate" runat="server" TargetControlID="txtToDate"
                                                WatermarkText="To Date" WatermarkCssClass="watermark">
                                            </ajaxToolkit:TextBoxWatermarkExtender>--%>
                                            <asp:Button ID="btnExcel" runat="server" CssClass="Excelbtn" ValidationGroup="Add"
                                                Text="Export Data to Excel" OnClick="btnExcel_Click" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 200px;">
                                        <div class="SpanPresent" style="vertical-align: middle; text-align: center;">
                                            <div>
                                                <span id="Present" style="font-size: 38px; font-weight: bold;"></span>
                                                <div style="font-size: 20px;">
                                                    Present Today</div>
                                            </div>
                                        </div>
                                    </td>
                                    <td style="width: 200px;">
                                        <div class="SpanOutdoor" style="vertical-align: middle; text-align: center;">
                                            <div>
                                                <span id="OutDoor" style="font-size: 38px; font-weight: bold;"></span>
                                                <div style="font-size: 20px;">
                                                    OutDoor Today</div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 200px;">
                                        <div class="SpanLeave" style="vertical-align: middle; text-align: center;">
                                            <span id="OnLeave" style="font-size: 38px; font-weight: bold;"></span>
                                            <div style="font-size: 20px;">
                                                Leave Today</div>
                                        </div>
                                    </td>
                                    <td style="width: 200px;">
                                        <div class="SpanAbsent" style="vertical-align: middle; text-align: center;">
                                            <span id="Absent" style="font-size: 38px; font-weight: bold;"></span>
                                            <div style="font-size: 20px;">
                                                Absent Today</div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <div style="height: 30px">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </div>
        </center>
        <div style="width: 40%">
            <div>
            </div>
        </div>
        <div style="float: left; width: 50%">
        </div>
    </div>
    <%--  popup window--%>
    <asp:Button ID="Button4" runat="server" Style="visibility: hidden;" Text="test" />
    <ajaxToolkit:ModalPopupExtender BehaviorID="bPresent" ID="mpePresent" runat="server"
        Enabled="True" BackgroundCssClass="cssVEh" TargetControlID="Button4" PopupControlID="pnlPresent"
        CancelControlID="btnClose">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlPresent" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="upn1" runat="server">
            <ContentTemplate>
                <div style="font-size: 20px; width: 100%; text-align: center; padding-bottom: 1px;">
                    <div style="text-align: center">
                        Today Present
                    </div>
                    <div style="float: right; padding-top: 1px; padding-bottom: 1px; padding-right: 1px;">
                        <asp:TextBox ID="txtSearch" ClientIDMode="Static" runat="server" CssClass="SearchBox"
                            onkeyup="SearchWord('txtSearch','TreeGridPresent')" onkeydown="SearchWord('txtSearch','TreeGridPresent')"
                            MaxLength="24"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtSearch" runat="server" TargetControlID="txtSearch"
                            WatermarkText="Search by name">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                    </div>
                    <div id="realTimeContents" style="background-color: White; width: 700px; box-shadow: -3px -2px 3px #1F0000;">
                        <ej:TreeGrid runat="server" ID="TreeGridPresent" ChildMapping="SubTasks" IdMapping="EPD_EMPID"
                            ClientIDMode="Static" TreeColumnIndex="0" ParentIdMapping="ParentId" EnableResize="true">
                            <Columns>
                                <ej:TreeGridColumn HeaderText="Employee Name" Width="200" Field="EPD_FIRST_NAME" />
                                <ej:TreeGridColumn HeaderText="Employee Code" Field="EPD_EMPID" />
                                <ej:TreeGridColumn HeaderText="Today In-Time" Field="TDAY_INTIME" />
                                <ej:TreeGridColumn HeaderText="Today Out-Time" Field="TDAY_OUTIME" />
                            </Columns>
                            <SizeSettings Width="100%" Height="350px" />
                        </ej:TreeGrid>
                    </div>
                </div>
                <br />
                <br />
                <center>
                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="ButtonControl" /></center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button2" runat="server" Style="visibility: hidden;" Text="test" />
    <ajaxToolkit:ModalPopupExtender BehaviorID="bAbsent" ID="ModalPopupExtender1" runat="server"
        Enabled="True" BackgroundCssClass="cssVEh" TargetControlID="Button2" PopupControlID="pnlAbsent"
        CancelControlID="btnAbsentClose">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlAbsent" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="font-size: 20px; width: 100%; text-align: center; padding-bottom: 1px;">
                    <div style="text-align: center">
                        Today Absent
                    </div>
                    <div style="float: right; padding-top: 2px; padding-bottom: 1px; padding-right: 1px;">
                        <asp:TextBox ID="txtAbsentSearch" ClientIDMode="Static" runat="server" CssClass="SearchBox"
                            onkeyup="SearchWord('txtAbsentSearch','TreeGridAbsent')" onkeydown="SearchWord('txtAbsentSearch','TreeGridAbsent')"
                            MaxLength="24"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtAbsentSearch" runat="server" TargetControlID="txtAbsentSearch"
                            WatermarkText="Search by name">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                    </div>
                </div>
                <div style="background-color: White; width: 700px; box-shadow: -3px -2px 3px #1F0000;">
                    <ej:TreeGrid runat="server" ID="TreeGridAbsent" ChildMapping="SubTasks" IdMapping="EPD_EMPID"
                        ClientIDMode="Static" TreeColumnIndex="0" ParentIdMapping="ParentId" EnableResize="true">
                        <Columns>
                            <ej:TreeGridColumn HeaderText="Employee Name" Field="EPD_FIRST_NAME" />
                            <ej:TreeGridColumn HeaderText="Employee Code" Field="EPD_EMPID" />
                        </Columns>
                        <SizeSettings Width="100%" Height="350px" />
                    </ej:TreeGrid>
                </div>
                <br />
                <br />
                <center>
                    <asp:Button ID="btnAbsentClose" runat="server" Text="Close" CssClass="ButtonControl" /></center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button3" runat="server" Style="visibility: hidden;" Text="test" />
    <ajaxToolkit:ModalPopupExtender BehaviorID="bOutdoor" ID="ModalPopupExtender2" runat="server"
        Enabled="True" BackgroundCssClass="cssVEh" TargetControlID="Button3" PopupControlID="pnlOutdoor"
        CancelControlID="btnOutdoorClose">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlOutdoor" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div style="font-size: 20px; width: 100%; text-align: center; padding-bottom: 1px;">
                    <div style="text-align: center">
                        Today Outdoor
                    </div>
                    <div style="float: right; padding-top: 2px; padding-bottom: 1px; padding-right: 1px;">
                        <asp:TextBox ID="txtOutdoorSearch" ClientIDMode="Static" runat="server" CssClass="SearchBox"
                            onkeyup="SearchWord('txtOutdoorSearch','TreeGridOutdoor')" onkeydown="SearchWord('txtOutdoorSearch','TreeGridOutdoor')"
                            MaxLength="24"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtOutdoorSearch" runat="server" TargetControlID="txtOutdoorSearch"
                            WatermarkText="Search by name">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                    </div>
                </div>
                <div style="background-color: White; width: 700px; box-shadow: -3px -2px 3px #1F0000;">
                    <ej:TreeGrid runat="server" ID="TreeGridOutdoor" ChildMapping="SubTasks" IdMapping="EPD_EMPID"
                        ClientIDMode="Static" TreeColumnIndex="0" ParentIdMapping="ParentId" EnableResize="true">
                        <Columns>
                            <ej:TreeGridColumn HeaderText="Employee Name" Field="EPD_FIRST_NAME" />
                            <ej:TreeGridColumn HeaderText="Employee Code" Field="EPD_EMPID" />
                            <ej:TreeGridColumn HeaderText="Today Status" Field="TDAY_STATUS" />
                        </Columns>
                        <SizeSettings Width="100%" Height="350px" />
                    </ej:TreeGrid>
                </div>
                <br />
                <br />
                <center>
                    <asp:Button ID="btnOutdoorClose" runat="server" Text="Close" CssClass="ButtonControl" /></center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button5" runat="server" Style="visibility: hidden;" Text="test" />
    <ajaxToolkit:ModalPopupExtender BehaviorID="bLeave" ID="ModalPopupExtender3" runat="server"
        Enabled="True" BackgroundCssClass="cssVEh" TargetControlID="Button5" PopupControlID="pnlLeave"
        CancelControlID="btnLeaveClose">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlLeave" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div style="font-size: 20px; width: 100%; text-align: center; padding-bottom: 1px;">
                    <div style="text-align: center; padding-top: 2px;">
                        Today Leave
                    </div>
                    <div style="float: right; padding-top: 2px; padding-bottom: 1px; padding-right: 1px;">
                        <asp:TextBox ID="txtLeaveSearch" ClientIDMode="Static" runat="server" CssClass="SearchBox"
                            onkeyup="SearchWord('txtLeaveSearch','TreeGridLeave')" onkeydown="SearchWord('txtLeaveSearch','TreeGridLeave')"
                            MaxLength="24"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtLeaveSearch" runat="server" TargetControlID="txtLeaveSearch"
                            WatermarkText="Search by name">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                    </div>
                </div>
                <div style="background-color: White; width: 700px; box-shadow: -3px -2px 3px #1F0000;">
                    <ej:TreeGrid runat="server" ID="TreeGridLeave" ChildMapping="SubTasks" IdMapping="EPD_EMPID"
                        TreeColumnIndex="0" ParentIdMapping="ParentId" EnableResize="true" ClientIDMode="Static">
                        <Columns>
                            <ej:TreeGridColumn HeaderText="Employee Name" Field="EPD_FIRST_NAME" />
                            <ej:TreeGridColumn HeaderText="Employee Code" Field="EPD_EMPID" />
                            <ej:TreeGridColumn HeaderText="Today Status" Field="TDAY_STATUS" />
                        </Columns>
                        <SizeSettings Width="100%" Height="350px" />
                    </ej:TreeGrid>
                </div>
                <br />
                <br />
                <center>
                    <asp:Button ID="btnLeaveClose" runat="server" Text="Close" CssClass="ButtonControl" /></center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
