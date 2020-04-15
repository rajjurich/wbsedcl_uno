<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    UICulture="en-GB" CodeBehind="ESS_Dashboard.aspx.cs" Inherits="UNO.ESS_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".webticker").webTicker();

        });

        function findspace(evnt) {

            var keyASCII = (evnt.which) ? evnt.which : event.keyCode;
            var keyValue = String.fromCharCode(keyASCII);

            if (!(keyASCII >= '48' && keyASCII <= '57')) {
                window.event.keyCode = 0;
            }
        }
        function fnColon(ctrl, e) {
            var unicode = e.keyCode
            if (unicode != 8) {
                if (ctrl.getAttribute && ctrl.value.length == 2) {
                    ctrl.value = ctrl.value + ":";
                }
            }
        }

        function mainRequestClose() {

            $('#' + '<%=PnlMain.ClientID %>').hide();
            $find('mpeNewRequest').hide();
            $('#' + ["<%=txtFrmDate.ClientID%>", "<%=txtToDate.ClientID%>"].join(', #')).prop('value', "");
            navigateToUrl("ESS_Dashboard.aspx");
            return false;
        }

        function closemsgPopup() {
            navigateToUrl("ESS_Dashboard.aspx");
            $find('<%=mpeReportingMgr.ClientID %>').hide();
            return false;
        }

        function ddlRequestChange() {
            var val = $('#' + '<%=DropDownList1.ClientID%>').val();

            if (val == "0") {
                $('#' + '<%=btnClose.ClientID %>').hide();
                $('#' + ["<%=Panel1.ClientID%>", "<%=PnlLvReq.ClientID%>", "<%=pnlODreq.ClientID%>", "<%=pnlOutPass.ClientID%>", "<%=pnlComOffReq.ClientID%>", "<%=pnlOptHoliday.ClientID%>"].join(', #')).css('display', 'none');
            }

            else if (val == "1") {

                $('#' + '<%=btnClose.ClientID %>').hide();
                $('#' + '<%=PnlLvReq.ClientID %>').css('display', 'block');
                $('#' + ["<%=errorLabelLV.ClientID%>"].join(', #')).prop('innerHTML', "");
                $('#' + ["<%=TextBox3.ClientID%>"].join(', #')).prop('value', "");
                $('#' + ["<%=Panel1.ClientID%>", "<%=pnlODreq.ClientID%>", "<%=pnlOutPass.ClientID%>", "<%=pnlComOffReq.ClientID%>", "<%=pnlOptHoliday.ClientID%>"].join(', #')).css('display', 'none');
                $("select" + "#" + "<%=ddlReasonTypeLVReq.ClientID%>").prop('selectedIndex', 0);
            }

            else if (val == "2") {
                $('#' + '<%=btnClose.ClientID %>').hide();
                $('#' + '<%=Panel1.ClientID %>').css('display', 'block');
                $('#' + ["<%=eoorLabelManulAtt.ClientID%>"].join(', #')).prop('innerHTML', "");
                $('#' + ["<%=frm_time.ClientID%>", "<%=To_Time.ClientID%>", "<%=txt_Remarks.ClientID%>"].join(', #')).prop('value', "");
                $('#' + ["<%=PnlLvReq.ClientID%>", "<%=pnlODreq.ClientID%>", "<%=pnlOutPass.ClientID%>", "<%=pnlComOffReq.ClientID%>", "<%=pnlOptHoliday.ClientID%>"].join(', #')).hide();
                $("select" + "#" + "<%=ddlReason.ClientID%>").prop('selectedIndex', 0);
            }


            else if (val == "3") {
                $('#' + '<%=btnClose.ClientID %>').hide();
                $('#' + '<%=pnlODreq.ClientID %>').css('display', 'block');
                $('#' + ["<%=PnlLvReq.ClientID%>", "<%=Panel1.ClientID%>", "<%=pnlOutPass.ClientID%>", "<%=pnlComOffReq.ClientID%>", "<%=pnlOptHoliday.ClientID%>"].join(', #')).hide();
                $('#' + ["<%=txtAdditionalInfoOD.ClientID%>"].join(', #')).prop('value', "");
                $('#' + ["<%=erroLabelOutDoor.ClientID%>"].join(', #')).prop('innerHTML', "");
                $("select" + "#" + "<%=ddlReasonOD.ClientID%>").prop('selectedIndex', 0);
            }

            else if (val == "4") {
                $('#' + '<%=btnClose.ClientID %>').hide();
                $('#' + '<%=pnlOutPass.ClientID %>').css('display', 'block');
                $('#' + ["<%=PnlLvReq.ClientID%>", "<%=Panel1.ClientID%>", "<%=pnlODreq.ClientID%>", "<%=pnlComOffReq.ClientID%>", "<%=pnlOptHoliday.ClientID%>"].join(', #')).css('display', 'none');
                $('#' + ["<%=txtAdditionInfoOup.ClientID%>"].join(', #')).prop('value', "");
                $('#' + ["<%=errorLabelOutPass.ClientID%>"].join(', #')).prop('innerHTML', "");
                $("select" + "#" + "<%=ddlResonOutPaas.ClientID%>").prop('selectedIndex', 0);
            }
            else if (val == "6") {
                $('#' + '<%=btnClose.ClientID %>').hide();
                $('#' + '<%=pnlComOffReq.ClientID %>').css('display', 'block');
                $('#' + ["<%=PnlLvReq.ClientID%>", "<%=Panel1.ClientID%>", "<%=pnlODreq.ClientID%>", "<%=pnlOutPass.ClientID%>", "<%=pnlOptHoliday.ClientID%>"].join(', #')).css('display', 'none');
                $('#' + ["<%=txtRemarkCoOff.ClientID%>"].join(', #')).prop('value', "");
                $('#' + ["<%=errorLableCompOff.ClientID%>"].join(', #')).prop('innerHTML', "");
                $("select" + "#" + "<%=ddlReasonComOff.ClientID%>").prop('selectedIndex', 0);
            }
            else if (val == "7") {
                $('#' + '<%=btnClose.ClientID %>').hide();
                $('#' + '<%=pnlOptHoliday.ClientID %>').css('display', 'block');
                $('#' + ["<%=PnlLvReq.ClientID%>", "<%=Panel1.ClientID%>", "<%=pnlODreq.ClientID%>", "<%=pnlOutPass.ClientID%>", "<%=pnlComOffReq.ClientID%>"].join(', #')).css('display', 'none');
                $('#' + ["<%=lblHolidaymsg.ClientID%>"].join(', #')).prop('innerHTML', "");
            }
            return false;

        }
        function ClosePanel(strPanel) {
            if (strPanel == "L")
                $('#' + '<%=PnlLvReq.ClientID %>').css('display', 'none');

            if (strPanel == "M")
                $('#' + '<%=Panel1.ClientID %>').css('display', 'none');

            if (strPanel == "OD")
                $('#' + '<%=pnlODreq.ClientID %>').css('display', 'none');

            if (strPanel == "OP")
                $('#' + '<%=pnlOutPass.ClientID %>').css('display', 'none');
            if (strPanel == "C")
                $('#' + '<%=pnlComOffReq.ClientID %>').css('display', 'none');
            if (strPanel == "HO")
                $('#' + '<%=pnlOptHoliday.ClientID %>').css('display', 'none');

            $('#' + '<%=btnClose.ClientID %>').show();
            $("select" + "#" + "<%=DropDownList1.ClientID%>").prop('selectedIndex', 0);
            if (strPanel == "L" || strPanel == "OD" || strPanel == "C" || strPanel == "OP")
                $('#' + "<%=hdnClick.ClientID%>").prop('value', "1");

            return false;
        }

        function callength(id) {

            var s = document.getElementById(id);
            if (s.value.length > 100) {

                return false;
            }

        }
      
    </script>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        @media (max-width: 1024px)
        {
            #tblAnnouncement, #tblBirdays
            {
                /* width: 1232px;*/
                z-index: 0;
            }
        }
        .calWeeklyOff
        {
            /*  color: Red;*/ /*background: #A80000 50% top repeat-x;
            font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;
             /*  text-align: right;*/ /* background-color: #D0D0D0 !important;*/ /* color: #CCFF66 !important;*/
            color: White; /* color: #FFFF66 !important;*/
        }
        
        .calAbsent
        {
        }
        
        .calPresent
        {
            /* color: #66FF00 !important;*/
        }
        .otherMonth
        {
            background-color: #D0D0D0 !important;
        }
        .SelectedDate
        {
            clear: both; /*  background-color: #FFFFCC !important;*/
            color: Navy !important;
        }
        
        .BlnakValue
        {
            visibility: hidden;
        }
        .pointer
        {
            cursor: pointer;
        }
        .cssVEh
        {
            background-color: Silver;
            filter: alpha(opacity=50);
            opacity: 0.9;
        }
        .selectedDatesBackcolr
        {
            background-color: Red;
        }
        .Daystyle
        {
            background: url(images/day-bg.png);
        }
        .img
        {
            background: url(images/day-bg.png);
        }
        #Calendar1
        {
            background: url(images/day-bg.png);
        }
        .calMainMonthd
        {
            font-size: 48px; /* font-family: Georgia, "Times New Roman" , Times, serif;*/
            font-family: Arial;
            font-weight: 400;
            background-color: #333333;
            border-width: 1px; /*   background: #eeeeee url(images/h_3.gif) 100% top*/
        }
        .Daystyle
        {
            border-radius: 3px;
        }
        .style38
        {
            height: 24px;
        }
        .Daystyle:hover
        {
            background-color: Red;
        }
        .Highlight
        {
            border: solid 2px Red;
            background-color: Red;
        }
        .Normal
        {
            border: solid 1px LightGray;
            background-color: Yellow;
        }
        .shadowcell
        {
        }
    </style>
    <script type="text/jscript">

        $("#TextBox1").timepicki();




        $.fn.timepicki = function (options) {
            alert("hiii");
            var defaults = {

            };

            var settings = $.extend({}, defaults, options);

            return this.each(function () {

                var ele = $(this);
                var ele_hei = ele.outerHeight();
                var ele_lef = ele.position().left;
                ele_hei += 10;
                $(ele).wrap("<div class='time_pick'>");
                var ele_par = $(this).parents(".time_pick");
                ele_par.append("<div class='timepicker_wrap'><div class='arrow_top'></div><div class='time'><div class='prev'></div><div class='ti_tx'></div><div class='next'></div></div><div class='mins'><div class='prev'></div><div class='mi_tx'></div><div class='next'></div></div><div class='meridian'><div class='prev'></div><div class='mer_tx'></div><div class='next'></div></div></div>");
                var ele_next = $(this).next(".timepicker_wrap");
                var ele_next_all_child = ele_next.find("div");
                ele_next.css({ "top": ele_hei + "px", "left": ele_lef + "px" });
                $(document).on("click", function (event) {
                    if (!$(event.target).is(ele_next)) {
                        if (!$(event.target).is(ele)) {
                            var tim = ele_next.find(".ti_tx").html();
                            var mini = ele_next.find(".mi_tx").text();
                            var meri = ele_next.find(".mer_tx").text();
                            if (tim.length != 0 && mini.length != 0 && meri.length != 0) {
                                ele.val(tim + " : " + mini + " : " + meri);
                            }
                            if (!$(event.target).is(ele_next) && !$(event.target).is(ele_next_all_child)) {
                                ele_next.fadeOut();
                            }
                        }
                        else {
                            set_date();
                            ele_next.fadeIn();
                        }
                    }
                });
                function set_date() {
                    var d = new Date();
                    var ti = d.getHours();
                    var mi = d.getMinutes();
                    var mer = "AM";
                    if (12 < ti) {
                        ti -= 12;
                        mer = "PM";
                    }
                    //console.log(ele_next);
                    if (ti < 10) {
                        ele_next.find(".ti_tx").text("0" + ti);
                    }
                    else {
                        ele_next.find(".ti_tx").text(ti);
                    }
                    if (mi < 10) {
                        ele_next.find(".mi_tx").text("0" + mi);
                    }
                    else {
                        ele_next.find(".mi_tx").text(mi);
                    }
                    if (mer < 10) {
                        ele_next.find(".mer_tx").text("0" + mer);
                    }
                    else {
                        ele_next.find(".mer_tx").text(mer);
                    }
                }


                var cur_next = ele_next.find(".next");
                var cur_prev = ele_next.find(".prev");


                $(cur_prev).add(cur_next).on("click", function () {
                    //console.log("click");
                    var cur_ele = $(this);
                    var cur_cli = null;
                    var ele_st = 0;
                    var ele_en = 0;
                    if (cur_ele.parent().attr("class") == "time") {
                        //alert("time");
                        cur_cli = "time";
                        ele_en = 12;
                        var cur_time = null;
                        cur_time = ele_next.find("." + cur_cli + " .ti_tx").text();
                        cur_time = parseInt(cur_time);
                        //console.log(ele_next.find("." + cur_cli + " .ti_tx"));
                        if (cur_ele.attr("class") == "next") {
                            //alert("nex");
                            if (cur_time == 12) {
                                ele_next.find("." + cur_cli + " .ti_tx").text("01");
                            }
                            else {
                                cur_time++;

                                if (cur_time < 10) {
                                    ele_next.find("." + cur_cli + " .ti_tx").text("0" + cur_time);
                                }
                                else {
                                    ele_next.find("." + cur_cli + " .ti_tx").text(cur_time);
                                }
                            }

                        }
                        else {
                            if (cur_time == 1) {
                                ele_next.find("." + cur_cli + " .ti_tx").text(12);
                            }
                            else {
                                cur_time--;
                                if (cur_time < 10) {
                                    ele_next.find("." + cur_cli + " .ti_tx").text("0" + cur_time);
                                }
                                else {
                                    ele_next.find("." + cur_cli + " .ti_tx").text(cur_time);
                                }
                            }
                        }

                    }
                    else if (cur_ele.parent().attr("class") == "mins") {
                        //alert("mins");
                        cur_cli = "mins";
                        ele_en = 59;
                        var cur_mins = null;
                        cur_mins = ele_next.find("." + cur_cli + " .mi_tx").text();
                        cur_mins = parseInt(cur_mins);
                        if (cur_ele.attr("class") == "next") {
                            //alert("nex");
                            if (cur_mins == 59) {
                                ele_next.find("." + cur_cli + " .mi_tx").text("00");
                            } else {
                                cur_mins++;
                                if (cur_mins < 10) {
                                    ele_next.find("." + cur_cli + " .mi_tx").text("0" + cur_mins);
                                }
                                else {
                                    ele_next.find("." + cur_cli + " .mi_tx").text(cur_mins);
                                }
                            }
                        }
                        else {

                            if (cur_mins == 0) {
                                ele_next.find("." + cur_cli + " .mi_tx").text(59);
                            }
                            else {
                                cur_mins--;

                                if (cur_mins < 10) {
                                    ele_next.find("." + cur_cli + " .mi_tx").text("0" + cur_mins);
                                }
                                else {
                                    ele_next.find("." + cur_cli + " .mi_tx").text(cur_mins);
                                }

                            }

                        }
                    }
                    else {
                        //alert("merdian");
                        ele_en = 1;
                        cur_cli = "meridian";
                        var cur_mer = null;
                        cur_mer = ele_next.find("." + cur_cli + " .mer_tx").text();
                        if (cur_ele.attr("class") == "next") {
                            //alert(cur_mer);
                            if (cur_mer == "AM") {
                                ele_next.find("." + cur_cli + " .mer_tx").text("PM");
                            }
                            else {
                                ele_next.find("." + cur_cli + " .mer_tx").text("AM");
                            }
                        } else {
                            if (cur_mer == "AM") {
                                ele_next.find("." + cur_cli + " .mer_tx").text("PM");
                            }
                            else {
                                ele_next.find("." + cur_cli + " .mer_tx").text("AM");
                            }
                        }
                    }


                });

            });
        };
        function chkDate() {

            try {
                var fdate = document.getElementById('<%= txtFrmDate.ClientID %>').value;

                var splitfdate = fdate.split('/');
                //alert(splitfdate[2]);
                var concateFdate = splitfdate[1] + "/" + splitfdate[0] + "/" + splitfdate[2];
                //alert('test');
                //alert(concateFdate);
                var frmDate = new Date(concateFdate);

                var tdate = document.getElementById('<%= txtToDate.ClientID %>').value;
                var splitTdate = tdate.split('/');
                var concateTdate = splitTdate[1] + "/" + splitTdate[0] + "/" + splitTdate[2];
                //alert(concateTdate);
                var toDate = new Date(concateTdate);

                if (frmDate > toDate) {
                    //alert('inside if');
                    var x = document.getElementById('<%= lblDateError.ClientID %>');
                    x.innerHTML = "Please select To Date Greater than or equal to From Date";
                    x.style.display = "block";
                    x.style.color = "red";

                    var ddl = document.getElementById('<%= DropDownList1.ClientID %>');
                    ddl.style.display = "none";

                    var req = document.getElementById('<%= lblReq.ClientID %>');
                    req.style.display = "none";


                }
                else {
                    var x = document.getElementById('<%= lblDateError.ClientID %>');
                    x.innerHTML = "";

                    var ddl = document.getElementById('<%= DropDownList1.ClientID %>');
                    ddl.style.display = "block";


                    var req = document.getElementById('<%= lblReq.ClientID %>');
                    req.style.display = "block";

                }
            }
            catch (e) {
                alert(e);
            }
        }

    </script>
    <style>
        .tdHeight
        {
            height: 15px;
        }
        .tdHeightMyProfile
        {
            height: 10px;
            text-align: center;
        }
        .thStyleByvaibhav
        {
            padding-left: 10px;
            height: 25px;
            background-color: #333333;
        }
        
        
        
        
        
        .myButton
        {
            -moz-box-shadow: 0px 10px 14px -7px #276873;
            -webkit-box-shadow: 0px 10px 14px -7px #276873;
            box-shadow: 0px 10px 14px -7px #276873;
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #599bb3), color-stop(1, #0b0e0f));
            background: -moz-linear-gradient(top, #599bb3 5%, #0b0e0f 100%);
            background: -webkit-linear-gradient(top, #599bb3 5%, #0b0e0f 100%);
            background: -o-linear-gradient(top, #599bb3 5%, #0b0e0f 100%);
            background: -ms-linear-gradient(top, #599bb3 5%, #0b0e0f 100%);
            background: linear-gradient(to bottom, #599bb3 5%, #0b0e0f 100%);
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#599bb3', endColorstr='#0b0e0f',GradientType=0);
            background-color: #599bb3;
            -moz-border-radius: 8px;
            -webkit-border-radius: 8px;
            border-radius: 8px;
            display: inline-block;
            cursor: pointer;
            color: #ffffff;
            font-family: arial;
            font-size: 12px;
            padding: 1px 44px;
            text-decoration: none;
            text-shadow: 0px 1px 0px #3d768a;
        }
        .myButton:hover
        {
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #0b0e0f), color-stop(1, #599bb3));
            background: -moz-linear-gradient(top, #0b0e0f 5%, #599bb3 100%);
            background: -webkit-linear-gradient(top, #0b0e0f 5%, #599bb3 100%);
            background: -o-linear-gradient(top, #0b0e0f 5%, #599bb3 100%);
            background: -ms-linear-gradient(top, #0b0e0f 5%, #599bb3 100%);
            background: linear-gradient(to bottom, #0b0e0f 5%, #599bb3 100%);
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0b0e0f', endColorstr='#599bb3',GradientType=0);
            background-color: #0b0e0f;
        }
        .myButton:active
        {
            position: relative;
            top: 1px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.Calendar1.ClientID %>');

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
        var myVar = "";
        function Showalert() {
            myVar = setTimeout(function () {
                var mpeNewReq = $find("mpeNewRequest");
                mpeNewReq.hide();
                clearTimeout(myVar);
            }, 1500);

        }
        function ShowModelPopUp() {

            var mpeNewReq = $find("mpeNewRequest");
            mpeNewReq.show();
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%">
        <%--       <table width="100%" style="margin-left: 0px; margin-right: 0px" id="tblAnnouncement">
            <tr>
            
                <td colspan="3">
                    <ul id="webticker1" style="z-index:0;" class="webticker">
                        <li id='item1'>
                            <asp:Label ID="lblAnnouncement" runat="server"></asp:Label>
                        </li>
                    </ul>
                </td>
            </tr>
        </table>--%>
        <table width="100%">
            <tr>
                <td style="text-align: right; padding-right: 24%; display: none;">
                    <asp:Button ID="btnNewReq" runat="server" Text="New Request" OnClick="btnNewReq_Click"
                        CssClass="myButton" ValidationGroup="validateofficial" />
                </td>
            </tr>
        </table>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
        <table width="100%" id="tblshow" style="padding-bottom: 20px; border-color: White;"
            border="1">
            <tr>
                <td style="width: 22%; height: 100%; border-radius: 10px;" valign="top">
                    <%--<table style="display: none; " width="100%" bordercolor="#47a3da"
                        borderwidth="1px">
                        <tr>
                            <td style="width: 100%; text-align: center;">
                                <div style="width: 100%; text-align: center; height: 2%; color: White;">
                                    Monthly Attendence</div>
                                <div style="width: 100%; height: 40%;">
                                    <div style="left: 2%;">
                                        <ul>
                                            <li>
                                                <asp:Label ID="Label8" runat="server" Text="Present Days"></asp:Label></li>
                                            <li>
                                                <asp:Label ID="Label9" runat="server" Text="Absent Days"></asp:Label></li>
                                            <li>
                                                <asp:Label ID="Label10" runat="server" Text="Outdoor Days"></asp:Label></li>
                                            <li>
                                                <asp:Label ID="Label11" runat="server" Text="OutPass Days"></asp:Label></li>
                                            <li>
                                                <asp:Label ID="Label12" runat="server" Text="Extra Days"></asp:Label></li>
                                            <li>
                                                <asp:Label ID="Label13" runat="server" Text="LWP Days"></asp:Label></li>
                                            <li>
                                                <asp:Label ID="Label14" runat="server" Text="Latecomming"></asp:Label></li>
                                        </ul>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>--%>
                    <table width="100%" bordercolor="#47a3da" borderwidth="1px" style="position: relative;">
                        <tr>
                            <th style="background-color: #333333; color: White;" class="thStyleByvaibhav ">
                                Current Month Attendance Summary
                            </th>
                        </tr>
                        <tr style="color: Black;">
                            <td style="width: 100%; text-align: left; vertical-align: top;">
                                <div style="width: 100%; height: 40%;">
                                    <table style="width: 100%; border-radius: 10px;">
                                         <tr style="display:none">
                                            <td style="font-weight: bold; background-color: #E8E8E8; width: 50%;" class="tdHeight">
                                                <asp:Label ID="Label3" runat="server" Text="Present Days"></asp:Label>
                                            </td>
                                            <td style="background-color: #E8E8E8; text-align: center;">
                                                <asp:Label ID="lblPrDays" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display:none">
                                            <td style="font-weight: bold; width: 70%;" class="tdHeight">
                                                Absent Days
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblAbDays" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display:none">
                                            <td style="font-weight: bold; background-color: #E8E8E8; width: 70%;" class="tdHeight">
                                                Outdoor Days
                                            </td>
                                            <td style="background-color: #E8E8E8; text-align: center;" class="tdHeight">
                                                <asp:Label ID="lblOutDoorDays" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display:none">
                                            <td style="font-weight: bold; width: 70%;" class="tdHeight">
                                                Outpass Days
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblOutPassDays" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display:none">
                                            <td style="font-weight: bold; background-color: #E8E8E8; width: 70%;" class="tdHeight">
                                                Days With Extra Hrs.
                                            </td>
                                            <td style="background-color: #E8E8E8; text-align: center;">
                                                <asp:Label ID="lblExtraHours" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display:none">
                                            <td style="font-weight: bold; width: 70%;" class="tdHeight">
                                                LWP Days
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblLWP" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display:none">
                                            <td style="font-weight: bold; background-color: #E8E8E8; width: 70%;" class="tdHeight">
                                                LateComing Days
                                            </td>
                                            <td style="background-color: #E8E8E8; text-align: center;">
                                                <asp:Label ID="lblLateComing" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; background-color: #fff; width: 70%;" class="tdHeight">
                                                Arrival In Time
                                            </td>
                                            <td style="background-color: #fff; text-align: center;">
                                                <asp:Label ID="lblArrivalInTime" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; background-color: #E8E8E8; width: 70%;" class="tdHeight">
                                                Arrival In Grace Time
                                            </td>
                                            <td style="background-color: #E8E8E8; text-align: center;">
                                                <asp:Label ID="lblArrivalInGraceTime" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; background-color: #fff; width: 70%;" class="tdHeight">
                                                Arrival Late
                                            </td>
                                            <td style="background-color: #fff; text-align: center;">
                                                <asp:Label ID="lblArrivalLate" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; background-color: #E8E8E8; width: 70%;" class="tdHeight">
                                                Absent
                                            </td>
                                            <td style="background-color: #E8E8E8; text-align: center;">
                                                <asp:Label ID="lblAbsent" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; background-color: #fff; width: 70%;" class="tdHeight">
                                                Departure Before Time
                                            </td>
                                            <td style="background-color: #fff; text-align: center;">
                                                <asp:Label ID="lblDepartureBeforeTime" runat="server"></asp:Label>
                                            </td>
                                        </tr>

                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" bordercolor="#47a3da" borderwidth="1px" style="position: relative;
                        border-bottom: 0px; border-top: 0px ; display:none">
                        <tr>
                            <th style="background-color: #333333; color: White;" class="thStyleByvaibhav">
                                Requests Pending For Approval
                            </th>
                        </tr>
                        <tr style="color: Black;">
                            <td style="width: 100%; text-align: left; vertical-align: top;">
                                <div style="width: 100%; height: 40%;">
                                    <table style="width: 100%; border-radius: 10px;">
                                        <tr style="color: Black">
                                            <td style="font-weight: bold; background-color: #E8E8E8; width: 70%;" class="tdHeight">
                                                <asp:Label ID="lblLVReqtxt" runat="server" Text="Leave"></asp:Label>
                                            </td>
                                            <td style="background-color: #E8E8E8; text-align: center;">
                                                <asp:Label ID="lblLVReq" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="color: Black">
                                            <td style="font-weight: bold; width: 70%;" class="tdHeight">
                                                <asp:Label ID="lblMAReqtxt" runat="server" Text="Manual"></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblMAReq" runat="server">  </asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="color: Black">
                                            <td style="font-weight: bold; background-color: #E8E8E8; width: 70%;" class="tdHeight">
                                                <asp:Label ID="lblODreqtxt" runat="server" Text="Outdoor"></asp:Label>
                                            </td>
                                            <td style="background-color: #E8E8E8; text-align: center;">
                                                <asp:Label ID="lblODreq" runat="server">  </asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="color: Black">
                                            <td style="font-weight: bold; width: 70%;" class="tdHeight">
                                                <asp:Label ID="lblOPReqtxt" runat="server" Text="Outpass"></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblOPReq" runat="server">  </asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="color: Black">
                                            <td style="font-weight: bold; background-color: #E8E8E8; width: 70%;" class="tdHeight">
                                                <asp:Label ID="lblCoReqtxt" runat="server" Text=" Com-Off"></asp:Label>
                                            </td>
                                            <td style="background-color: #E8E8E8; text-align: center;">
                                                <asp:Label ID="lblCoReq" runat="server">  </asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <asp:Button ID="Button1" runat="server" Text="Click To View" Width="100%" CssClass="myButton"
                                    ValidationGroup="validateofficial" OnClick="Button1_Click" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" bordercolor="#47a3da" borderwidth="1px" style="position: relative;
                        padding-bottom: 20px;  display:none">
                        <tr>
                            <th style="background-color: #333333; color: White;" class="thStyleByvaibhav">
                                Requests Awaiting Your Approval
                            </th>
                        </tr>
                        <tr style="color: Black;">
                            <td style="width: 100%; text-align: left; vertical-align: top;">
                                <div style="width: 100%; height: 40%;">
                                    <table style="width: 100%; border-radius: 10px;">
                                        <tr style="color: Black">
                                            <td style="font-weight: bold; background-color: #E8E8E8; width: 70%;" class="tdHeight">
                                                <asp:Label ID="lblW8ReqLvtxt" runat="server" Text="Leave"></asp:Label>
                                            </td>
                                            <td style="background-color: #E8E8E8; text-align: center;">
                                                <asp:Label ID="lblW8ReqLv" runat="server">  </asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="color: Black">
                                            <td style="font-weight: bold; width: 70%;" class="tdHeight">
                                                <asp:Label ID="lblW8ReqMatxt" runat="server" Text="Manual"></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblW8ReqMa" runat="server">  </asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="color: Black">
                                            <td style="font-weight: bold; background-color: #E8E8E8; width: 70%;" class="tdHeight">
                                                <asp:Label ID="lblW8ReqODtxt" runat="server" Text="Outdoor"></asp:Label>
                                            </td>
                                            <td style="background-color: #E8E8E8; text-align: center;">
                                                <asp:Label ID="lblW8ReqOD" runat="server">  </asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="color: Black">
                                            <td style="font-weight: bold; width: 70%;" class="tdHeight">
                                                <asp:Label ID="lblW8ReqOPtxt" runat="server" Text="Outpass"></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblW8ReqOP" runat="server">  </asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="color: Black">
                                            <td style="font-weight: bold; background-color: #E8E8E8; width: 70%;" class="tdHeight">
                                                <asp:Label ID="lblW8ReqCOtxt" runat="server" Text="Com-Off"></asp:Label>
                                            </td>
                                            <td style="background-color: #E8E8E8; text-align: center;">
                                                <asp:Label ID="lblW8ReqCO" runat="server">  </asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <asp:Button ID="Button2" runat="server" Text="Click To Approve" Width="100%" CssClass="myButton"
                                    ValidationGroup="validateofficial" OnClick="btnreqAwat_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 56%; height: 100%; font-family: Arial; padding-bottom: 0px;">
                  
                            <asp:Calendar ID="Calendar1" runat="server" BackColor="#404040" DayStyle-HorizontalAlign="Right"
                                SelectedDayStyle-Font-Size="Larger" SelectionMode="Day" BorderColor="#47a3da"
                                BorderWidth="2px" Font-Names="Arial" Font-Size="9pt" ForeColor="white" Height="450px"
                                NextPrevFormat="FullMonth" Width="100%" OnDayRender="Calendar1_DayRender" ViewStateMode="Enabled"
                                OnLoad="Calendar1_Load" OnVisibleMonthChanged="Calendar1_VisibleMonthChanged">
                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" VerticalAlign="Middle" />
                                <%--  <OtherMonthDayStyle ForeColor="#47a3da" BackColor="White"/>--%>
                                <%--    <WeekendDayStyle  BackColor="White" ForeColor="Black" />--%>
                                <SelectedDayStyle BackColor="#FFFFCC" ForeColor="BlueViolet" Font-Size="Smaller"
                                    CssClass="shadowcell" />
                                <TitleStyle Font-Bold="True" Font-Size="28pt" ForeColor="White" BorderWidth="1px"
                                    CssClass="calMainMonthd" />
                                <%--  <DayStyle  />--%>
                                <TodayDayStyle BackColor="#47afda" ForeColor="White" CssClass="shadowcell" />
                                <TodayDayStyle BackColor="#2C3539" ForeColor="White" CssClass="shadowcell" />
                                <%--     <DayStyle BackColor="#333333" Font-Size="Smaller" BorderColor="#47a3da" ForeColor="White"
                                    HorizontalAlign="Right" Wrap="true" BorderWidth="2px" />--%>
                                <DayStyle BackColor="#D0D0D0" Font-Size="9pt" BorderColor="#47a3da" ForeColor="Black"
                                    HorizontalAlign="Right" Wrap="true" BorderWidth="2px" CssClass="shadowcell" />
                                <%-- <DayStyle BackColor="White" Font-Size="Smaller" BorderColor="#47a3da"  ForeColor="Black"  BorderWidth="2px" CssClass="Daystyle"/>--%>
                            </asp:Calendar>
                       
                </td>                
                <td style="width: 22%; height: 100%">
                    <table width="100%" bordercolor="#47a3da" borderwidth="1px" style="position: relative;
                        border-bottom: 0px; margin-top: 0px;">
                        <tr>
                            <th class="thStyleByvaibhav" style="color: White" colspan="2">
                                My Profile
                            </th>
                        </tr>
                        <tr>
                            <td style="height: 126px; text-align: center;" colspan="2">
                                <asp:Image ID="imgEmployeeImage" runat="server" Height="125px" Style="text-align: right;"
                                    Width="139px" ClientIDMode="Static" ImageAlign="Middle" />
                                <br />
                            </td>
                        </tr>
                        <tr style="color: Black">
                            <td style="text-align: center;" colspan="2">
                                <asp:Label ID="lblEmpName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="color: Black">
                            <td style="width: 100%; text-align: left;" colspan="2" style="color: Black">
                                <%--     <div style="width: 100%; text-align: center; height: 2%;">
                                My Profile</div>--%>
                                <div style="width: 100%; height: 37%;">
                                    <table style="width: 100%; border-radius: 10px; text-align: left;">
                                        <tr>
                                            <td style="font-weight: bold; width: 30%; background-color: #E8E8E8; text-align: left"
                                                class="tdHeightMyProfile">
                                                <asp:Label ID="Label4" runat="server" Text="ID"></asp:Label>
                                            </td>
                                            <td style="background-color: #E8E8E8; text-align: left;">
                                                <asp:Label ID="lblEmpID" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; width: 30%; text-align: left;" class="tdHeightMyProfile">
                                                <asp:Label ID="Label6" runat="server" Text="Designation">
                                                </asp:Label>
                                            </td>
                                            <td style="text-align: left;" class="tdHeightMyProfile">
                                                <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; text-align: left; width: 30%; background-color: #E8E8E8;"
                                                class="tdHeightMyProfile">
                                                <asp:Label ID="Label7" runat="server" Text="Email"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblMailID" runat="server" Style="background-color: #E8E8E8;"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; width: 30%; text-align: left;" class="tdHeightMyProfile">
                                                <asp:Label ID="Label15" runat="server" Text="Location"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; width: 30%; text-align: left; background-color: #E8E8E8;
                                                text-align: left;" class="tdHeightMyProfile">
                                                <asp:Label ID="Label16" runat="server" Text="Reporting"></asp:Label>
                                            </td>
                                            <td style="background-color: #E8E8E8; text-align: left;">
                                                <asp:Label ID="lblR1" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; width: 30%; text-align: left;" class="tdHeightMyProfile">
                                                <asp:Label ID="Label17" runat="server" Text="Department"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <%--<tr><td style="font-weight:bold;width:70%;"class="tdHeightMyProfile">LWP Days :</td><td><asp:Label ID="lblManager" runat="server"></asp:Label></td></tr>--%>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%;" bordercolor="#47a3da" borderwidth="1px">
                        <tr>
                            <th class="thStyleByvaibhav" colspan="2" style="color: White">
                                Leave Details
                            </th>
                            <%--   <th style="text-align: center; background-color: #333333;">
                            Leave Details
                        </th>--%>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 155px; vertical-align: top;">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" BorderStyle="None"
                                    HeaderStyle-ForeColor="White" Width="100%" GridLines="None">
                                    <EmptyDataTemplate>
                                        No Records Found</EmptyDataTemplate>
                                    <RowStyle ForeColor="Black" />
                                    <AlternatingRowStyle BackColor="#E8E8E8" />
                                    <Columns>
                                        <asp:BoundField HeaderText="" DataField="" />
                                        <asp:BoundField HeaderText="" HeaderStyle-BackColor="#333333" ItemStyle-HorizontalAlign="Center"
                                            DataField="LV_LEAVE_ID" />
                                        <asp:BoundField HeaderText="OP.Bal" HeaderStyle-BackColor="#333333" ItemStyle-HorizontalAlign="Center"
                                            DataField="Lv_Opbalance" />
                                        <asp:BoundField HeaderText="Used" HeaderStyle-BackColor="#333333" ItemStyle-HorizontalAlign="Center"
                                            DataField="LV_AVAILED" />
                                        <asp:BoundField HeaderText="Cut" HeaderStyle-BackColor="#333333" ItemStyle-HorizontalAlign="Center"
                                            DataField="LV_CUT" />
                                        <asp:BoundField HeaderText="Available" HeaderStyle-BackColor="#333333" ItemStyle-HorizontalAlign="Center"
                                            DataField="LV_AVAILABLE" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
         </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Calendar1" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div id="updateProgressDiv" style="display: none; height: 0px; width: 0px">
                        <img src="images/276.GIF" alt="Loading ...." />
                        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
                            TargetControlID="UpdatePanel1">
                            <Animations>
                        <OnUpdating>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="calender1" Enabled="false" />
                                <ScriptAction Script="onUpdating();" />  
                                <FadeOut Duration="1.0" Fps="24" minimumOpacity=".5" />
                            </Parallel> 
                        </OnUpdating>
                        <OnUpdated>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="calender1" Enabled="true" />
                                <ScriptAction Script="onUpdated();" /> 
                                <FadeIn Duration="1.0" Fps="24"  minimumOpacity=".5"/>
                            </Parallel> 
                        </OnUpdated>
                            </Animations>
                        </ajaxToolkit:UpdatePanelAnimationExtender>
        <%--  <table width="100%" id="tblBirdays">
            <tr>
                <td colspan="3" style="padding-top: 0px;">
                    <ul id="Ul1" class="webticker">
                        <li id='Li1'>
                            <asp:Label ID="lblBirdays" runat="server"></asp:Label>
                        </li>
                    </ul>
                </td>
            </tr>
        </table>--%>
        <asp:Panel ID="pnl_del_project" runat="server" CssClass="PopupPanel">
            <asp:UpdatePanel ID="up1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="PnlMain" Visible="true" runat="server" Style="background-color: White;
                        height: auto; border: 0px solid #000000; border-radius: 25px; color: Black;"
                        Width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="4" style="text-align: center; margin-bottom: 2%">
                                    <asp:Label ID="Label1" runat="server" Text="New Request" Font-Bold="True" Font-Size="Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    From Date:<font color="red">*</font>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFrmDate" runat="server" onchange="chkDate()" TabIndex="1" onKeyPress="javascript: return false "
                                        onKeydown="javascript: return false "></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrmDate"
                                        PopupButtonID="txtFrmDate" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td style="padding-left: 2%">
                                    <span style="white-space: nowrap">To Date:<font color="red">*</font></span>
                                </td>
                                <td style="padding-left: 3%">
                                    <asp:TextBox ID="txtToDate" runat="server" onchange="chkDate()" TabIndex="2" onKeyPress="javascript: return false "
                                        onKeydown="javascript: return false "></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                        PopupButtonID="txtToDate" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblReq" runat="server" Text="Select Request:"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="DropDownList1" runat="server" TabIndex="3" Width="145px" onchange="ddlRequestChange();">
                                        <%--  <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                        Width="173px">--%>
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">Leave Request</asp:ListItem>
                                        <asp:ListItem Value="2">Manual Attendance</asp:ListItem>
                                        <asp:ListItem Value="3">Outdoor</asp:ListItem>
                                        <asp:ListItem Value="4">Outpass</asp:ListItem>
                                        <asp:ListItem Value="6">Com-Off</asp:ListItem>
                                        <asp:ListItem Value="7">Optional Holiday</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="4" align="center">
                                    <asp:Label ID="lblDateError" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="4" align="center">
                                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="ButtonControl" Style="margin-top: 1%"
                                        OnClientClick="return mainRequestClose();" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="Panel1" runat="server" Style="background-color: White; height: auto;
                        border: 0px solid #000000; border-radius: 25px; color: Black; display: none"
                        Width="100%">
                        <table id="Table1" runat="server" width="100%">
                            <tr>
                                <td colspan="4" style="text-align: center;" class="style38">
                                    <asp:Label ID="Label21" runat="server" Text="Manual Attendance" Font-Bold="True"
                                        Font-Size="Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    In Time: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:TextBox ID="frm_time" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                        placeholder="24 Hrs Format" ValidationGroup="Ma" MaxLength="5" onkeypress="findspace(event)"
                                        onkeyup="fnColon(this,event)" TabIndex="2"></asp:TextBox>
                                    <%--  <asp:RequiredFieldValidator ID="Reqfrm_time" runat="server" ControlToValidate="frm_time"
                                        Display="none" ErrorMessage="Please enter In Time" SetFocusOnError="True" ForeColor="Red"
                                        ValidationGroup="Ma"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCEfrm_time" runat="server" TargetControlID="Reqfrm_time"
                                        PopupPosition="right">
                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="frm_time"
                                        Display="None" ValidationGroup="Ma" ErrorMessage="Please enter date in 24 hours format"
                                        ForeColor="Red" ValidationExpression="([01]?[0-9]|2[0-3]):[0-5][0-9]"></asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                        TargetControlID="RegularExpressionValidator2" PopupPosition="right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td style="padding-left: 3%">
                                    Out Time: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:TextBox ID="To_Time" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                        placeholder="24 Hrs Format" ValidationGroup="Ma" MaxLength="5" onkeypress="findspace(event)"
                                        onkeyup="fnColon(this,event)" TabIndex="3"></asp:TextBox>
                                    <%--      <asp:RequiredFieldValidator ID="ReqTo_Time" runat="server" ControlToValidate="To_Time"
                                        Display="none" ErrorMessage="Please enter Out Time" SetFocusOnError="True" ForeColor="Red"
                                        ValidationGroup="Ma"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCETo_Time" runat="server" TargetControlID="ReqTo_Time"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="To_Time"
                                        Display="None" ValidationGroup="Ma" ErrorMessage="Please enter date in 24 hours format"
                                        ForeColor="Red" ValidationExpression="([01]?[0-9]|2[0-3]):[0-5][0-9]"></asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                        TargetControlID="RegularExpressionValidator4" PopupPosition="right">
                                    </ajaxToolkit:ValidatorCalloutExtender>

                                    <asp:CompareValidator ID="CVmanual" runat="server" ErrorMessage="Outtime should be greater than In time"
                                    ControlToValidate="To_Time" ControlToCompare="frm_time" Operator="GreaterThan" Type="String" 
                                    ValidationGroup="Ma" Display="None">
                                    </asp:CompareValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE_MA" runat="server" TargetControlID="CVmanual"
                                    PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Reason: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReason" runat="server" CssClass="ComboControl" TabIndex="4"
                                        ValidationGroup="Ma" ClientIDMode="Static" Height="19px" Width="173px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="ReqddlReason" runat="server" ControlToValidate="ddlReason"
                                        Display="none" ErrorMessage="Please enter Reason ID" SetFocusOnError="True" InitialValue="Select One"
                                        ForeColor="Red" ValidationGroup="Ma"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCEddlReason" runat="server" TargetControlID="ReqddlReason"
                                        PopupPosition="right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td style="padding-left: 3%">
                                    Additional Info.
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_Remarks" CssClass="TextControl" Style="resize: none" runat="server"
                                        TextMode="MultiLine" onkeypress="return callength(this.id);"
                                        Width="173px"></asp:TextBox>
                                        <%--onkeyDown="return checkTextAreaMaxLength(this,event,'50');"--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Button ID="btnSaveManualAtt" runat="server" CssClass="ButtonControl" Text="Save"
                                        ValidationGroup="Ma" OnClick="btnSaveManualAtt_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="ButtonControl" Text="Cancel"
                                        CausesValidation="false" OnClientClick=" return ClosePanel('M');" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Label ID="eoorLabelManulAtt" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="PnlLvReq" runat="server" Style="background-color: White; height: auto;
                        border: 0px solid #000000; border-radius: 25px; color: Black; display: none"
                        Width="100%">
                        <table id="Table2" runat="server" width="100%">
                            <tr>
                                <td colspan="4" style="text-align: center;" class="style38">
                                    <asp:Label ID="Label24" runat="server" Text="Leave Request" Font-Bold="True" Font-Size="Large"></asp:Label>
                                </td>
                                <td class="style38" style="text-align: center;" rowspan="6">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Leave Code: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddleaveType" runat="server" CssClass="ComboControl" TabIndex="4"
                                        Width="100%" ValidationGroup="Lv" ClientIDMode="Static">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="ReqddleaveType" runat="server" ControlToValidate="ddleaveType"
                                        Display="none" ErrorMessage="Please enter Leave Code" SetFocusOnError="True"
                                        InitialValue="Select One" ForeColor="Red" ValidationGroup="Lv"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCEddleaveType" runat="server" TargetControlID="ReqddleaveType"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td style="padding-left: 3%">
                                    Additional Info.
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox3" CssClass="TextControl" Style="resize: none" runat="server"
                                        TextMode="MultiLine" Width="173px" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Reason: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReasonTypeLVReq" runat="server" ClientIDMode="Static" CssClass="ComboControl"
                                        Width="100%" ValidationGroup="Lv" TabIndex="4">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlReasonTypeLVReq"
                                        Display="none" ErrorMessage="Please enter Reason Code" ForeColor="Red" InitialValue="Select One"
                                        SetFocusOnError="True" ValidationGroup="Lv"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                        TargetControlID="RequiredFieldValidator3" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td style="padding-left: 3%">
                                    Select Day:
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbtLeaveType1" runat="server" CssClass="" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="F" Selected="True">Fullday</asp:ListItem>
                                        <asp:ListItem Value="H">HalfDay</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Button ID="btnSaveLvReq" runat="server" CssClass="ButtonControl" Text="Save"
                                        ValidationGroup="Lv" OnClick="btnSaveLvReq_Click" />
                                    <asp:Button ID="btnCancelLvReq" runat="server" CssClass="ButtonControl" Text="Cancel"
                                        CausesValidation="false" OnClientClick=" return ClosePanel('L');" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Label ID="errorLabelLV" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlODreq" runat="server" Style="background-color: White; height: auto;
                        border: 0px solid #000000; border-radius: 25px; color: Black; display: none"
                        Width="100%">
                        <table id="Table3" runat="server" width="100%">
                            <tr>
                                <td colspan="4" style="text-align: center;" class="style38">
                                    <asp:Label ID="Label25" runat="server" Text="Outdoor Request" Font-Bold="True" Font-Size="Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Reason: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReasonOD" runat="server" CssClass="ComboControl" TabIndex="4"
                                        ClientIDMode="Static" Height="19px" Width="162px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlReason" runat="server" ErrorMessage="Please Select Reason Code"
                                        InitialValue="Select One" ControlToValidate="ddlReason" Display="None"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlReason" runat="server" TargetControlID="rfvddlReason"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td style="padding-left: 3%">
                                    Additional Info.
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAdditionalInfoOD" runat="server" Style="resize: none" CssClass="TextControl"
                                        MaxLength="150" TabIndex="5" Width="173px" onkeypress="return this.value.length&lt;=50"
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Button ID="btnSaveOdReq" runat="server" CssClass="ButtonControl" Text="Save"
                                        TabIndex="6" ValidationGroup="add" OnClick="btnSaveOdReq_Click" />
                                    <asp:Button ID="btnCancelOdreq" runat="server" CssClass="ButtonControl" Text="Cancel"
                                        CausesValidation="false" OnClientClick=" return ClosePanel('OD');" TabIndex="7" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Label ID="erroLabelOutDoor" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlComOffReq" runat="server" Style="background-color: White; height: auto;
                        border: 0px solid #000000; display: none; border-radius: 25px; color: Black;"
                        Width="100%">
                        <table id="Table4" runat="server" width="100%">
                            <tr>
                                <td colspan="4" style="text-align: center;" class="style38">
                                    <asp:Label ID="Label20" runat="server" Text="Comp-Off Request" Font-Bold="True" Font-Size="Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Reason:<font color="red">*</font>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReasonComOff" runat="server" CssClass="ComboControl" TabIndex="4"
                                        ValidationGroup="CompOff" ClientIDMode="Static" Height="19px" Width="162px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Reason Code"
                                        ValidationGroup="CompOff" InitialValue="Select One" ControlToValidate="ddlReasonComOff"
                                        Display="None"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                        TargetControlID="RequiredFieldValidator1" PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td style="padding-left: 3%">
                                    Additional Info.
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemarkCoOff" runat="server" Style="resize: none" CssClass="TextControl"
                                        MaxLength="150" Width="173px" onkeypress="return this.value.length&lt;=50" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="4">
                                    <div id="updateProgressDivPopUp" style="display: none; height: 40px; width: 40px">
                                        <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                                    </div>
                                    <%--  <div class="DivEmpDetails">--%>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvPopUp" runat="server" AutoGenerateColumns="False" Width="100%"
                                                AllowSorting="True" AllowPaging="true" ClientIDMode="Static" PageSize="5" GridLines="None"
                                                OnSelectedIndexChanged="gvPopUp_SelectedIndexChanged" OnPageIndexChanging="gvPopUp_PageIndexChanging">
                                                <RowStyle CssClass="gvRow" />
                                                <HeaderStyle CssClass="headerstyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                                <PagerStyle CssClass="gvPager" ForeColor="black" />
                                                <EmptyDataRowStyle BackColor="#edf5ff" Height="100px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <div>
                                                        <span>No Records found.</span>
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select" SortExpression="Select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="SaveRows" runat="server" ClientIDMode="Static" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Leave Code" ShowHeader="False" SortExpression="Leave Code"
                                                        Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeaveCode" runat="server" Text='<%# Eval("LEAVECODE") %>'> </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TDAY DATE" ShowHeader="False" SortExpression="TDAY DATE"
                                                        Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFromDT" runat="server" Text='<%# Eval("TDAY_DATE") %>'> </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TDAY STATUS" ShowHeader="False" SortExpression="TDAY STATUS"
                                                        Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSTATUS" runat="server" Text='<%# Eval("TDAY_STATUS") %>'> </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="gvPopUp" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Button ID="btnComOffReq" runat="server" CssClass="ButtonControl" Text="Save"
                                        ValidationGroup="CompOff" OnClick="btnComOffReq_Click" />
                                    <asp:Button ID="btnCancelCoOff" runat="server" CssClass="ButtonControl" CausesValidation="false"
                                        Text="Cancel" OnClientClick=" return ClosePanel('C');" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Label ID="errorLableCompOff" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlOutPass" runat="server" Style="background-color: White; height: auto;
                        border: 0px solid #000000; display: none; border-radius: 25px; color: Black;"
                        Width="100%">
                        <table id="Table5" runat="server" width="100%">
                            <tr>
                                <td colspan="4" style="text-align: center;" class="style38">
                                    <asp:Label ID="Label26" runat="server" Text="OutPass" Font-Bold="True" Font-Size="Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    In Time: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInOuP" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                        placeholder="24 Hrs Format" ValidationGroup="Oup" MaxLength="5" onkeypress="findspace(event)"
                                        onkeyup="fnColon(this,event)" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtInOuP"
                                        Display="none" ErrorMessage="Please enter In Time" SetFocusOnError="True" ForeColor="Red"
                                        ValidationGroup="Oup"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                        TargetControlID="RequiredFieldValidator2" PopupPosition="right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtInOuP"
                                        Display="None" ValidationGroup="Oup" ErrorMessage="Please enter date in 24 hours format"
                                        ForeColor="Red" ValidationExpression="([01]?[0-9]|2[0-3]):[0-5][0-9]"></asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                        TargetControlID="RegularExpressionValidator1" PopupPosition="right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td style="padding-left: 3%">
                                    Out Time:&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInTimeOutP" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                        placeholder="24 Hrs Format" ValidationGroup="Oup" MaxLength="5" onkeypress="findspace(event)"
                                        onkeyup="fnColon(this,event)" TabIndex="3"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtInTimeOutP"
                                        Display="None" ValidationGroup="Oup" ErrorMessage="Please enter date in 24 hours format"
                                        ForeColor="Red" ValidationExpression="([01]?[0-9]|2[0-3]):[0-5][0-9]"></asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                        TargetControlID="RegularExpressionValidator3" PopupPosition="right">
                                    </ajaxToolkit:ValidatorCalloutExtender>


                                     <asp:CompareValidator ID="CV_OutPass" runat="server" ErrorMessage="Outtime should be greater than In time"
                                    ControlToValidate="txtInTimeOutP" ControlToCompare="txtInOuP" Operator="GreaterThan" Type="String" 
                                    ValidationGroup="Oup" Display="None">
                                    </asp:CompareValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE_Outpass" runat="server" TargetControlID="CV_OutPass"
                                    PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Reason: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlResonOutPaas" runat="server" CssClass="ComboControl" TabIndex="4"
                                        ValidationGroup="Oup" ClientIDMode="Static" Height="19px" Width="173px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlResonOutPaas"
                                        Display="none" ErrorMessage="Please enter Reason ID" SetFocusOnError="True" InitialValue="Select One"
                                        ForeColor="Red" ValidationGroup="Oup"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                        TargetControlID="RequiredFieldValidator5" PopupPosition="right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td style="padding-left: 3%">
                                    Additional Info.
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAdditionInfoOup" CssClass="TextControl" Style="resize: none"
                                        runat="server" TextMode="MultiLine" onkeyDown="return checkTextAreaMaxLength(this,event,'50');"
                                        Width="173px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Button ID="btnSaveOutpass" runat="server" CssClass="ButtonControl" Text="Save"
                                        ValidationGroup="Oup" OnClick="btnSaveOutpass_Click" />
                                    <asp:Button ID="btnCancelOutPass" runat="server" CssClass="ButtonControl" Text="Cancel"
                                        CausesValidation="false" OnClientClick=" return ClosePanel('OP');" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Label ID="errorLabelOutPass" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hdnClick" runat="server" ClientIDMode="Static" />
                    </asp:Panel>
                    <asp:Panel ID="pnlOptHoliday" runat="server" Style="background-color: White; height: auto;
                        border: 0px solid #000000; border-radius: 25px; color: Black;display:none" Width="100%">
                        <table id="Table6" runat="server" width="100%">
                            <tr>
                                <td colspan="4" style="text-align: center;" class="style38">
                                    <asp:Label ID="Label2" runat="server" Text="Optional Holiday" Font-Bold="True" Font-Size="Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="max-height: 20em; overflow: auto;">
                                        <asp:GridView ID="gvHolidayList" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                            AllowSorting="false" Width="100%">
                                            <RowStyle CssClass="gvRow" />
                                            <EmptyDataTemplate>
                                                <div>
                                                    <span>No Records found.</span>
                                                </div>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                            <AlternatingRowStyle BackColor="#F0F0F0" />
                                            <PagerStyle CssClass="gvPager" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select" SortExpression="Select">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="selectChk" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                                        <asp:HiddenField ID="hdnHolidayId" runat="server" Value='<%#Eval("HOLIDAY_ID")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="HOLIDAY_DESCRIPTION" HeaderText="Holiday" SortExpression="Holiday">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HOLIDAY_DATE" HeaderText="Date" SortExpression="Date">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Button ID="btnSAveHoliday" runat="server" CssClass="ButtonControl" Text="Save"
                                        OnClick="btnSAveHoliday_Click"  CausesValidation="false"/>
                                    <asp:Button ID="btnHCancel" runat="server" CssClass="ButtonControl" Text="Cancel"  OnClientClick=" return ClosePanel('HO');"/>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Label ID="lblHolidaymsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Button ID="btn_sub" runat="server" Style="visibility: hidden;" Text="test" />
        <asp:Button ID="Button4" runat="server" Style="visibility: hidden;" Text="test" />
        <ajaxToolkit:ModalPopupExtender ID="mpeNewReq" runat="server" Enabled="True" BackgroundCssClass="cssVEh"
            BehaviorID="mpeNewRequest" TargetControlID="btn_sub" PopupControlID="pnl_del_project"
            CancelControlID="Button4">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="ReportingMgrError" runat="server" CssClass="PopupPanel">
            <asp:UpdatePanel ID="upnMsg" runat="server">
                <ContentTemplate>
                    <div>
                        <div>
                            <asp:Label runat="server" ID="lblMsg" Text=""></asp:Label>
                        </div>
                        <div style="text-align: center; padding-top: 8%">
                            <asp:Button ID="btnCloseMsg" runat="server" Text="Close" CssClass="ButtonControl"
                                OnClientClick="return closemsgPopup();" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Button ID="Button3" runat="server" Style="display: none;" Text="test" />
        <ajaxToolkit:ModalPopupExtender ID="mpeReportingMgr" runat="server" Enabled="True"
            BackgroundCssClass="cssVEh" TargetControlID="Button3" PopupControlID="ReportingMgrError">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Label runat="server" ID="Error"></asp:Label>
    </div>
    </div>
</asp:Content>
