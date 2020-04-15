<%--<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ShiftMasterView.aspx.cs" Inherits="UNO.ShiftMasterView" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ShiftMasterView.aspx.cs" Inherits="UNO.ShiftMasterView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css" />
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript">

        function WeekEndDiffTIme() {

            document.getElementById('<%=WShft_Frm.ClientID%>').value = "";
            document.getElementById('<%=WShft_To.ClientID%>').value = "";
            document.getElementById('<%=WLnch_Frm.ClientID%>').value = "";
            document.getElementById('<%=WLnch_To.ClientID%>').value = "";
            document.getElementById('<%=WTot_WrKHr.ClientID%>').value = "";

            if (document.getElementById('<%=WChkShft1.ClientID%>').checked == true) {
                document.getElementById('<%=WShft_Frm.ClientID%>').disabled = false;
                document.getElementById('<%=WShft_To.ClientID%>').disabled = false;
                document.getElementById('<%=WLnch_Frm.ClientID%>').disabled = false;
                document.getElementById('<%=WLnch_To.ClientID%>').disabled = false;
                document.getElementById('<%=WTot_WrKHr.ClientID%>').disabled = false;
                document.getElementById('<%=WChkShft.ClientID%>').disabled = false;
                document.getElementById('<%=WTot_WrKHr.ClientID%>').disabled = false;
            }
            else {
                document.getElementById('<%=WShft_Frm.ClientID%>').disabled = true;
                document.getElementById('<%=WShft_To.ClientID%>').disabled = true;
                document.getElementById('<%=WLnch_Frm.ClientID%>').disabled = true;
                document.getElementById('<%=WLnch_To.ClientID%>').disabled = true;
                document.getElementById('<%=WTot_WrKHr.ClientID%>').disabled = true;
                document.getElementById('<%=WChkShft.ClientID%>').disabled = true;
                document.getElementById('<%=WTot_WrKHr.ClientID%>').disabled = true;
            }

        }

        function ValidateShiftAllocationType(RbdAllocation) {
            if ($('#' + RbdAllocation.id + ' input:checked').val() == 'AUTO') {
                document.getElementById('<%=ASStartTime.ClientID%>').disabled = false;
                document.getElementById('<%=ASEndTime.ClientID%>').disabled = false;
                document.getElementById('<%=ASEndTime.ClientID%>').focus();
                document.getElementById('<%=ASStartTime.ClientID%>').focus();
                $('#' + ["<%=ASStartTime.ClientID%>", "<%=ASEndTime.ClientID%>"].join(', #')).prop('value', "");
                document.getElementById(RbdAllocation).focus();


            }
            else {
                document.getElementById('<%=ASStartTime.ClientID%>').disabled = true;
                document.getElementById('<%=ASEndTime.ClientID%>').disabled = true;
                $('#' + ["<%=ASStartTime.ClientID%>", "<%=ASEndTime.ClientID%>"].join(', #')).prop('value', "");
            }
            return false;
        }


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;


            return true;
        }

        function ValidateShft_To(sender, args) {

            if (document.getElementById('Shft_To').value != "" && document.getElementById('Shft_Frm').value != "") {
                if (document.getElementById('Shft_To').value != document.getElementById('Shft_Frm').value) {
                    args.IsValid = true;
                }
                else {
                    //sender.innerHTML = "Shift in time and out time can not be same.";
                    args.IsValid = false;
                }
            }
            else
            { args.IsValid = true; }
        }

        function Confirmationbox() {
            var result = confirm('Are you sure you want to delete selected User(s)?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }

        function handleAdd() {
            if (!Page_ClientValidate())
                return;

            var msg = confirm('Save Record?');

            if (msg == false) {
                return false;
            }
        }

        function returnviewmode() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";
            window.location = "ShiftMastreView.aspx";
        }

        function clearFunctionMessageDiv() {
            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";
        }


        function parseTime(s) {
            var c = s.split(':');
            return parseInt(c[0]) * 60 + parseInt(c[1]);
        }

        function clearFunction() {
            document.getElementById('Shft_id').value = "";
            document.getElementById('Shft_desc').value = "";
            document.getElementById('Shft_Type').value = "Select One";
            document.getElementById('Shft_Frm').value = "";
            document.getElementById('Shft_To').value = "";
            document.getElementById('Lnch_Frm').value = "";
            document.getElementById('Lnch_To').value = "";
            document.getElementById('Tot_WrKHr').value = "";
            document.getElementById('hdnTot_WrKHr').value = "";
            document.getElementById('Label1').value = "";
            document.getElementById('ChkShft').checked = false;

        }

        function DateDiff2() {
            startTime = document.getElementById('Shft_Frm').value;
            startTime1 = new Date(startTime);
            endTime = document.getElementById('Shft_To').value;
            endTime1 = new Date(endTime);
            var timeDiff = endTime1.getHours() - startTime1.getHours();
            document.getElementById('Tot_WrKHr').value = timeDiff;
            document.getElementById('hdnTot_WrKHr').value = timeDiff;
        }

        function difference() {

            var stime = document.getElementById("Shft_Frm").value;
            var etime = document.getElementById("Shft_To").value;

            var stime1 = parseInt(document.getElementById("Shft_Frm").value);
            var etime1 = parseInt(document.getElementById("Shft_To").value);
            if (isNaN(stime) || stime > 23 || stime < 0) {
                alert("Invalid start time");
                document.getElementById("Shft_Frm").value = "";
                return false;
            }
            if (isNaN(etime) || etime > 23 || etime < 0) {
                alert("Invalid end time");
                document.getElementById("Shft_To").value = "";
                return false;
            }
            if (etime >= stime) {
                var diff = (etime - stime);
            }
            else {
                var diff = Math.abs(stime - etime - 24);
            }
            document.getElementById("Tot_WrKHr").value = diff;
            document.getElementById('hdnTot_WrKHr').value = diff;

        }

        function captureStartTime() {
            var startTime = null;
            var endTime = null;
            startTime = document.getElementById('Shft_Frm').value;
            endTime = document.getElementById('Shft_To').value;
            var timeDiff = parseTime(endTime) - parseTime(startTime);

            var hours = parseInt(timeDiff / 60);
            var minutes = parseInt(timeDiff % 60);
            document.getElementById('Tot_WrKHr').value = hours + ":" + minutes;
            document.getElementById('hdnTot_WrKHr').value = hours + ":" + minutes;
        }

        function timeDiff() {
            var time1 = null;
            var time2 = null;
            time2 = document.getElementById('Shft_Frm').value;
            time1 = document.getElementById('Shft_To').value;
            var time1array = time1.split(":");
            var time2array = time2.split(":");
            var hour1 = parseInt(time1array[0]);
            var min1 = parseInt(time1array[1]);
            var hour2 = parseInt(time2array[0]);
            var min2 = parseInt(time2array[1]);
            var result = (hour1 + min1 / 60) - (hour2 + min2 / 60);
            document.getElementById('Tot_WrKHr').value = result;
            document.getElementById('hdnTot_WrKHr').value = result;
        }
        function CheckValid() {
            var start = new Date("01/01/1900 " + startTime + ":00");
            var end = new Date("01/01/1900 " + endTime + ":00");
            var Mid = new Date("01/02/1900 " + endTime + ":00");
            var LnchStrt = new Date("01/01/1900 " + lnchInTime + ":00");
            var LnchEnd = new Date("01/01/1900 " + lnchOutTime + ":00");
            if (val == "General" && val == "Afternoon") {
                if (start > end)
                { alert('Shift Start time should not be greater than Shift End time'); return false; }
                if (LnchStrt > LnchEnd)
                { alert('Lunch Shift Start Time should not be greater than Shift Lunch End Time'); return false; }
            }
            if (val == "Night") {
                if (Mid > start)
                { alert('Shift End time should not be greater than Shift Start time'); return false; }
            }
        }


        function CheckValidAutoSearchStart() {

            var StartTime = document.getElementById('Shft_Frm').value;
            var ASStartTime = document.getElementById('ASStartTime').value;
            var EndTime = document.getElementById('Shft_To').value;
            var ASEndTime = document.getElementById('ASEndTime').value;

            var start = new Date("01/01/1900 " + StartTime + ":00");
            var autoSearchStart = new Date("01/01/1900 " + ASStartTime + ":00");
            var end = new Date("01/01/1900 " + EndTime + ":00");
            var autoSearchEnd = new Date("01/01/1900 " + ASEndTime + ":00");

            var val = document.getElementById('Shft_Type').value;

            var RBID = '<%=RBShiftALType.ClientID %>';
            var RB1 = document.getElementById(RBID);
            var radio = RB1.getElementsByTagName("input");
            if (radio[1].checked) { }
            else
            { return true; }

            if (val == "General" || val == "Afternoon" || val == "Morning") {
                if (start == autoSearchStart)
                { alert('Shift Start and Auto Search start should not be Equal.'); return false; }
                if (end == autoSearchEnd)
                { alert('Shift end and Auto Search end should not be Equal.'); return false; }

                if (autoSearchStart > start)
                { alert('Auto search start time should be before Shift Start time'); return false; }

                if (autoSearchEnd < end)
                { alert('Auto search end time should be after Shift end time'); return false; }
            }

            if (val == "Night") {
                if (EndTime < StartTime) {
                    if (StartTime >= "00:00") {
                        var earlierdate = new Date('01/01/1900' + ' ' + StartTime);
                    }
                    else {
                        var earlierdate = new Date('01/02/1900' + ' ' + StartTime);
                    }

                    if (EndTime <= "00:00") {
                        var laterdate = new Date('01/01/1900' + ' ' + EndTime);
                    }
                    else {
                        var laterdate = new Date('01/02/1900' + ' ' + EndTime);
                    }
                }
                else {
                    var earlierdate = new Date('01/01/1900' + ' ' + StartTime);
                    var laterdate = new Date('01/01/1900' + ' ' + EndTime);
                }

                if (ASEndTime < ASStartTime) {
                    if (ASStartTime >= "00:00") {
                        var DautoSearchStart = new Date('01/01/1900' + ' ' + ASStartTime);
                    }
                    else {
                        var DautoSearchStart = new Date('01/02/1900' + ' ' + ASStartTime);
                    }

                    if (ASEndTime <= "00:00") {
                        var DautoSearchEnd = new Date('01/01/1900' + ' ' + ASEndTime);
                    }
                    else {
                        var DautoSearchEnd = new Date('01/02/1900' + ' ' + ASEndTime);
                    }
                }
                else {
                    var DautoSearchStart = new Date('01/01/1900' + ' ' + ASStartTime);
                    var DautoSearchEnd = new Date('01/01/1900' + ' ' + ASEndTime);
                }

                if (start == autoSearchStart)
                { alert('Shift Start and Auto Search start should not be Equal.'); return false; }
                if (end == autoSearchEnd)
                { alert('Shift end and Auto Search end should not be Equal.'); return false; }

                if (DautoSearchStart > start)
                { alert('Auto search start time should be before Shift Start time'); return false; }

                if (DautoSearchEnd < end)
                { alert('Auto search end time should be after Shift end time'); return false; }
            }
        }

        function get_time_difference(from) {

            var msecPerMinute = 1000 * 60;
            var msecPerHour = msecPerMinute * 60;
            var msecPerDay = msecPerHour * 24;
            var ShiftEarlySearch = document.getElementById('<%=EarlySearchHours.ClientID%>').value;
            var ShiftLateSearch = document.getElementById('<%=LateSearchHours.ClientID%>').value;
            var startTime, endTime, lnchInTime, lnchOutTime;


            if (from == "N") {

                startTime = document.getElementById("<%=Shft_Frm.ClientID%>").value;
                endTime = document.getElementById('Shft_To').value;
                lnchInTime = document.getElementById('Lnch_Frm').value;
                lnchOutTime = document.getElementById('Lnch_To').value;

            }

            if (from == "W") {
                if (document.getElementById('WChkShft1').checked == false)
                { return; }
                startTime = document.getElementById('WShft_Frm').value;
                endTime = document.getElementById('WShft_To').value;
                lnchInTime = document.getElementById('WLnch_Frm').value;
                lnchOutTime = document.getElementById('WLnch_To').value;
            }

            var val = document.getElementById('Shft_Type').value;
            var diff = null;
            var LnchDiff = null;


            if (val == "General") {

                var start = new Date("01/01/1900 " + startTime + ":00");
                var end = new Date("01/01/1900 " + endTime + ":00");

                var LnchStrt = new Date("01/01/1900 " + lnchInTime + ":00");
                var LnchEnd = new Date("01/01/1900 " + lnchOutTime + ":00");
                LnchDiff = LnchEnd.getTime() - LnchStrt.getTime();

                if (ValidateTime(startTime) && ValidateTime(endTime) && ValidateTime(lnchInTime) && ValidateTime(lnchOutTime)) {
                    if (startTime == endTime)
                    { alert('Shift Start and  Shift End  should not be same'); return false; }
                    if (lnchInTime == lnchOutTime)
                    { alert('Break Start and  Break End  should not be same'); return false; }
                    if (start > end)
                    { alert('Shift Start should not be greater than Shift End '); return false; }
                    if (LnchStrt > LnchEnd)
                    { alert('Break  Start should not be greater than Shift Break End '); return false; }
                    if (LnchStrt < start)
                    { alert('Break Start should be greater than Shift Start '); return false; }
                    if (end < LnchEnd)
                    { alert('Break End should not be greater than Shift End '); return false; }
                }
                if (from == "N") {
                    if (document.getElementById('ChkShft').checked) {

                        diff = (end.getTime() - start.getTime()) - (LnchEnd.getTime() - LnchStrt.getTime());
                    }
                    else { diff = end.getTime() - start.getTime(); }
                }
                if (from == "W") {
                    diff = end.getTime() - start.getTime();
                }

                //for lunch
                var time_difference_lunch = new Object();
                time_difference_lunch.hours = Math.floor(LnchDiff / 1000 / 60 / 60);
                LnchDiff -= time_difference_lunch.hours * 1000 * 60 * 60;
                if (time_difference_lunch.hours < 10) time_difference_lunch.hours = "0" + time_difference_lunch.hours;

                time_difference_lunch.minutes = Math.floor(LnchDiff / 1000 / 60);
                LnchDiff -= time_difference_lunch.minutes * 1000 * 60;
                if (time_difference_lunch.minutes < 10) time_difference_lunch.minutes = "0" + time_difference_lunch.minutes;

                var time_difference = new Object();
                time_difference.hours = Math.floor(diff / 1000 / 60 / 60);
                diff -= time_difference.hours * 1000 * 60 * 60;
                if (time_difference.hours < 10) time_difference.hours = "0" + time_difference.hours;

                time_difference.minutes = Math.floor(diff / 1000 / 60);
                diff -= time_difference.minutes * 1000 * 60;
                if (time_difference.minutes < 10) time_difference.minutes = "0" + time_difference.minutes;

                if (from == 'N') {

                    document.getElementById("<%=Tot_BrHr.ClientID%>").value = time_difference_lunch.hours + ":" + time_difference_lunch.minutes;
                    document.getElementById("<%=Tot_WrKHr.ClientID%>").value = time_difference.hours + ":" + time_difference.minutes;

                    document.getElementById("<%=hdnTot_BrHr.ClientID%>").value = time_difference_lunch.hours + ":" + time_difference_lunch.minutes;
                    document.getElementById("<%=hdnTot_WrKHr.ClientID%>").value = time_difference.hours + ":" + time_difference.minutes;
                }
                if (from == "W") {
                    document.getElementById('WTot_WrKHr').value = time_difference.hours + ":" + time_difference.minutes;
                }
            }


            if (val == "Morning") {

                var start = new Date("01/01/1900 " + startTime + ":00");
                var end = new Date("01/01/1900 " + endTime + ":00");

                var LnchStrt = new Date("01/01/1900 " + lnchInTime + ":00");
                var LnchEnd = new Date("01/01/1900 " + lnchOutTime + ":00");
                LnchDiff = LnchEnd.getTime() - LnchStrt.getTime();

                if (ValidateTime(startTime) && ValidateTime(endTime) && ValidateTime(lnchInTime) && ValidateTime(lnchOutTime)) {
                    if (startTime == endTime)
                    { alert('Shift Start and  Shift End  should not be same'); return false; }
                    if (lnchInTime == lnchOutTime)
                    { alert('Break Start and  Break End  should not be same'); return false; }
                    if (start > end)
                    { alert('Shift Start should not be greater than Shift End '); return false; }
                    if (LnchStrt > LnchEnd)
                    { alert('Break  Start should not be greater than Shift Break End '); return false; }
                    if (LnchStrt < start)
                    { alert('Break Start should not be greater than Shift Start '); return false; }
                    if (end < LnchEnd)
                    { alert('Break End should not be greater than Shift End '); return false; }
                }
                if (from == "N") {
                    if (document.getElementById('ChkShft').checked) {

                        diff = (end.getTime() - start.getTime()) - (LnchEnd.getTime() - LnchStrt.getTime());
                    }
                    else { diff = end.getTime() - start.getTime(); }
                }
                if (from == "W") {
                    diff = end.getTime() - start.getTime();
                }

                //for lunch
                var time_difference_lunch = new Object();
                time_difference_lunch.hours = Math.floor(LnchDiff / 1000 / 60 / 60);
                LnchDiff -= time_difference_lunch.hours * 1000 * 60 * 60;
                if (time_difference_lunch.hours < 10) time_difference_lunch.hours = "0" + time_difference_lunch.hours;

                time_difference_lunch.minutes = Math.floor(LnchDiff / 1000 / 60);
                LnchDiff -= time_difference_lunch.minutes * 1000 * 60;
                if (time_difference_lunch.minutes < 10) time_difference_lunch.minutes = "0" + time_difference_lunch.minutes;

                var time_difference = new Object();
                time_difference.hours = Math.floor(diff / 1000 / 60 / 60);
                diff -= time_difference.hours * 1000 * 60 * 60;
                if (time_difference.hours < 10) time_difference.hours = "0" + time_difference.hours;

                time_difference.minutes = Math.floor(diff / 1000 / 60);
                diff -= time_difference.minutes * 1000 * 60;
                if (time_difference.minutes < 10) time_difference.minutes = "0" + time_difference.minutes;

                if (from == 'N') {
                    document.getElementById('Tot_BrHr').value = time_difference_lunch.hours + ":" + time_difference_lunch.minutes;
                    document.getElementById('Tot_WrKHr').value = time_difference.hours + ":" + time_difference.minutes;
                    document.getElementById('<%=hdnTot_BrHr.ClientID%>').value = time_difference_lunch.hours + ":" + time_difference_lunch.minutes;
                    document.getElementById('<%=hdnTot_WrKHr.ClientID%>').value = time_difference.hours + ":" + time_difference.minutes;
                }
                if (from == "W") {
                    document.getElementById('WTot_WrKHr').value = time_difference.hours + ":" + time_difference.minutes;
                }
            }

            if (val == "Afternoon") {
                var start = new Date("01/01/1900 " + startTime + ":00");
                var end = new Date("01/01/1900 " + endTime + ":00");

                var LnchStrt = new Date("01/01/1900 " + lnchInTime + ":00");
                var LnchEnd = new Date("01/01/1900 " + lnchOutTime + ":00");
                LnchDiff = LnchEnd.getTime() - LnchStrt.getTime();

                if (ValidateTime(startTime) && ValidateTime(endTime) && ValidateTime(lnchInTime) && ValidateTime(lnchOutTime)) {

                    if (startTime == endTime)
                    { alert('Shift Start and  Shift End  should not be same'); return false; }
                    if (lnchInTime == lnchOutTime)
                    { alert('Break Start and  Break End  should not be same'); return false; }

                    if (start > end)
                    { alert('Shift Start should not be greater than Shift End'); return false; }
                    if (LnchStrt > LnchEnd)
                    { alert('Break Shift Start should not be greater than Shift Break End'); return false; }
                    if (LnchStrt < start)
                    { alert('Break Start should not be greater than Shift Start Time'); return false; }
                    if (end < LnchEnd)
                    { alert('Break End should not be greater than Shift End'); return false; }
                }
                if (from == "N") {
                    if (document.getElementById('ChkShft').checked) {
                        diff = (end.getTime() - start.getTime()) - (LnchEnd.getTime() - LnchStrt.getTime());
                    }
                    else { diff = end.getTime() - start.getTime(); }
                }
                if (from == "W") {
                    diff = end.getTime() - start.getTime();
                }

                //for lunch

                var time_difference_lunch = new Object();
                time_difference_lunch.hours = Math.floor(LnchDiff / 1000 / 60 / 60);
                LnchDiff -= time_difference_lunch.hours * 1000 * 60 * 60;
                if (time_difference_lunch.hours < 10) time_difference_lunch.hours = "0" + time_difference_lunch.hours;

                time_difference_lunch.minutes = Math.floor(LnchDiff / 1000 / 60);
                LnchDiff -= time_difference_lunch.minutes * 1000 * 60;
                if (time_difference_lunch.minutes < 10) time_difference_lunch.minutes = "0" + time_difference_lunch.minutes;

                var time_difference = new Object();
                time_difference.hours = Math.floor(diff / 1000 / 60 / 60);
                diff -= time_difference.hours * 1000 * 60 * 60;
                if (time_difference.hours < 10) time_difference.hours = "0" + time_difference.hours;

                time_difference.minutes = Math.floor(diff / 1000 / 60);
                diff -= time_difference.minutes * 1000 * 60;
                if (time_difference.minutes < 10) time_difference.minutes = "0" + time_difference.minutes;
                //            document.getElementById('Tot_WrKHr').value = time_difference.hours + ":" + time_difference.minutes;

                if (from == "N") {
                    document.getElementById('Tot_BrHr').value = time_difference_lunch.hours + ":" + time_difference_lunch.minutes;
                    document.getElementById('Tot_WrKHr').value = time_difference.hours + ":" + time_difference.minutes;
                    document.getElementById('<%=hdnTot_BrHr.ClientID%>').value = time_difference_lunch.hours + ":" + time_difference_lunch.minutes;
                    document.getElementById('<%=hdnTot_WrKHr.ClientID%>').value = time_difference.hours + ":" + time_difference.minutes;
                }
                if (from == "W") {
                    document.getElementById('WTot_WrKHr').value = time_difference.hours + ":" + time_difference.minutes;
                }
            }


            if (val == "Night") {
                var StartTime = document.getElementById('Shft_Frm').value;
                var EndTime = document.getElementById('Shft_To').value;
                var lnchInTime = document.getElementById('Lnch_Frm').value;
                var lnchOutTime = document.getElementById('Lnch_To').value;

                if (ValidateTime(EndTime) && ValidateTime(StartTime) && ValidateTime(lnchInTime) && ValidateTime(lnchOutTime)) {
                    if (EndTime == StartTime)
                    { alert('Shift Start and Shift End should not be Equal.'); return false; }

                    if (lnchInTime == lnchOutTime)
                    { alert('Break Start and Break End should not be Equal.'); return false; }

                }

                if (EndTime < StartTime) {
                    if (StartTime >= "00:00") {
                        var earlierdate = new Date('01/01/1900' + ' ' + StartTime);
                    }
                    else {
                        var earlierdate = new Date('01/02/1900' + ' ' + StartTime);
                    }

                    if (EndTime <= "00:00") {
                        var laterdate = new Date('01/01/1900' + ' ' + EndTime);
                    }
                    else {
                        var laterdate = new Date('01/02/1900' + ' ' + EndTime);
                    }
                }
                else {
                    var earlierdate = new Date('01/01/1900' + ' ' + StartTime);
                    var laterdate = new Date('01/01/1900' + ' ' + EndTime);
                }


                if (lnchOutTime > lnchInTime) {
                    if ((lnchInTime >= "00:00" && lnchInTime < "12:00")) {
                        if (earlierdate.getDate() == 1 && laterdate.getDate() == 1)
                        { var Lnhin = new Date('01/01/1900' + ' ' + lnchInTime); }
                        else
                        { var Lnhin = new Date('01/02/1900' + ' ' + lnchInTime); }
                    }
                    else {
                        var Lnhin = new Date('01/01/1900' + ' ' + lnchInTime);
                    }

                    if ((lnchOutTime >= "00:00" && lnchOutTime < "12:00")) {

                        if (earlierdate.getDate() == 1 && laterdate.getDate() == 1)
                        { var lnhOut = new Date('01/01/1900' + ' ' + lnchOutTime); }
                        else
                        { var lnhOut = new Date('01/02/1900' + ' ' + lnchOutTime); }
                    }
                    else {
                        var lnhOut = new Date('01/01/1900' + ' ' + lnchOutTime);
                    }
                }
                else {
                    var Lnhin = new Date('01/01/1900' + ' ' + lnchInTime);
                    var lnhOut = new Date('01/01/1900' + ' ' + lnchOutTime);
                }

                if (ValidateTime(startTime) && ValidateTime(endTime) && ValidateTime(lnchInTime) && ValidateTime(lnchOutTime)) {

                    if (startTime == endTime)
                    { alert('Shift Start and  Shift End  should not be same'); return false; }
                    if (lnchInTime == lnchOutTime)
                    { alert('Break Start and  Break End  should not be same'); return false; }

                    //                    if (Lnhin > lnhOut)
                    //                    { AssignErrorMsg('Break End should be greater than Break Start'); return false; }
                    if (Lnhin < earlierdate)
                    { alert('Break Start should not be less than Shift Start'); return false; }
                    if (Lnhin > laterdate)
                    { alert('Break Start should not be less than Shift Start'); return false; }
                    if (laterdate < lnhOut)
                    { alert('Break End should not be greater than Shift End'); return false; }

                }
                if (from == "N") {
                    if (document.getElementById('ChkShft').checked) {

                        // diff = (end.getTime() - start.getTime()) - (LnchEnd.getTime() - LnchStrt.getTime());
                        var difference = (laterdate.getTime() - earlierdate.getTime()) - (lnhOut.getTime() - Lnhin.getTime());
                    }
                    else { var difference = laterdate.getTime() - earlierdate.getTime(); }
                }
                if (from == "W") {
                    //diff = end.getTime() - start.getTime();
                    var difference = laterdate.getTime() - earlierdate.getTime();
                }


                var date1 = new Date("08/05/2015 " + document.getElementById('Lnch_Frm').value + ":00");
                var date2 = new Date("08/06/2015 " + document.getElementById('Lnch_To').value + ":00");

                var diffe = date2.getTime() - date1.getTime();

                var mNsec = diffe;
                var hh = Math.floor(mNsec / 1000 / 60 / 60);
                mNsec -= hh * 1000 * 60 * 60;
                var mm = Math.floor(mNsec / 1000 / 60);
                //                mNsec -= mm * 1000 * 60;
                //                var ss = Math.floor(mNsec / 1000);
                //                mNsec -= ss * 1000;



                // var difference = laterdate.getTime() - earlierdate.getTime();
                var Lnchdifference = lnhOut.getTime() - Lnhin.getTime();

                //for lunch
                LnchDiff = Math.abs(Lnchdifference);


                var daysLnchDifference = Math.floor(LnchDiff / 1000 / 60 / 60 / 24);
                LnchDiff -= daysLnchDifference * 1000 * 60 * 60 * 24

                var hoursLnchDifference = Math.floor(LnchDiff / 1000 / 60 / 60);
                LnchDiff -= hoursLnchDifference * 1000 * 60 * 60

                var minutesLnchDifference = Math.floor(LnchDiff / 1000 / 60);
                LnchDiff -= minutesLnchDifference * 1000 * 60

                var secondsLnchDifference = Math.floor(LnchDiff / 1000);

                if (hoursLnchDifference < 10) hoursLnchDifference = "0" + hoursLnchDifference;
                if (minutesLnchDifference == 0) minutesLnchDifference = "0" + minutesLnchDifference;

                diff = Math.abs(difference);

                var daysDifference = Math.floor(diff / 1000 / 60 / 60 / 24);
                diff -= daysDifference * 1000 * 60 * 60 * 24

                var hoursDifference = Math.floor(diff / 1000 / 60 / 60);
                diff -= hoursDifference * 1000 * 60 * 60

                var minutesDifference = Math.floor(diff / 1000 / 60);
                diff -= minutesDifference * 1000 * 60

                var secondsDifference = Math.floor(diff / 1000);

                if (hoursDifference < 10) hoursDifference = "0" + hoursDifference;
                if (minutesDifference == 0) minutesDifference = "0" + minutesDifference;

                if (from == "N") {
                    //document.getElementById('Tot_BrHr').value = lpad(hh,2) + ":" + lpad(mm,2);
                    document.getElementById('Tot_BrHr').value = hoursLnchDifference + ":" + minutesLnchDifference;
                    document.getElementById('Tot_WrKHr').value = hoursDifference + ":" + minutesDifference;
                    document.getElementById('<%=hdnTot_BrHr.ClientID%>').value = hoursLnchDifference + ":" + minutesLnchDifference;
                    document.getElementById('<%=hdnTot_WrKHr.ClientID%>').value = hoursDifference + ":" + minutesDifference;
                }
                if (from == "W") {
                    document.getElementById('WTot_WrKHr').value = hoursDifference + ":" + minutesDifference;
                }
            }

            if (document.getElementById('Tot_BrHr').value == "NaN:NaN")
            { document.getElementById('Tot_BrHr').value = ""; }
            if (document.getElementById('Tot_WrKHr').value == "NaN:NaN")
            { document.getElementById('Tot_WrKHr').value = ""; }
            if (document.getElementById('WTot_WrKHr').value == "NaN:NaN")
            { document.getElementById('WTot_WrKHr').value = ""; }

//            if (ShiftEarlySearch == "00" || ShiftEarlySearch == "0") {
//                document.getElementById('<%=lblError.ClientID%>').innerHTML = "Shift Early Search  Hours cant be 00 or 0";
//                return false;
//            }
//            if (ShiftLateSearch == "00" || ShiftLateSearch == "0") {
//                document.getElementById('<%=lblError.ClientID%>').innerHTML = "Shift Late Search Hours cant be 00 or 0";
//                return false;
//            }
//            AssignErrorMsg('');
//            return true;

        }

        function ValidateWShft_Frm(sender, args) {
            var WShft_Frm = document.getElementById('WShft_Frm').value;
            if (document.getElementById('WChkShft1').checked == true) {
                if (WShft_Frm == "") {
                    //sender.innerHTML = "Please enter Shift Start";
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }
            }
            else {
                args.IsValid = true;
            }

        }
        function ValidateWShft_To(sender, args) {
            var WShft_To = document.getElementById('WShft_To').value;
            if (document.getElementById('WChkShft1').checked == true) {
                if (WShft_To == "") {
                    //sender.innerHTML = "Please enter Shift End"; 
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }
            }
            else {
                args.IsValid = true;
            }

        }

        function ValidateWLnch_Frm(sender, args) {
            var WLnch_Frm = document.getElementById('WLnch_Frm').value;
            if (document.getElementById('WChkShft1').checked == true) {
                if (WLnch_Frm == "") {
                    //sender.innerHTML = "Please enter Lunch Start"; 
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }
            }
            else {
                args.IsValid = true;
            }

        }
        function ValidateWLnch_To(sender, args) {
            var WLnch_To = document.getElementById('WLnch_To').value;
            if (document.getElementById('WChkShft1').checked == true) {
                if (WLnch_To == "") {
                    //sender.innerHTML = "Please enter Lunch End"; 
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }
            }
            else {
                args.IsValid = true;
            }

        }
        //ValidateASStartTime

        function ValidateASStartTime(sender, args) {

            var ASStartTime = document.getElementById('ASStartTime').value;
            var RBID = '<%=RBShiftALType.ClientID %>';
            var RB1 = document.getElementById(RBID);
            var radio = RB1.getElementsByTagName("input");
            //        if (radio[1].value == "Auto") {
            if (radio[1].checked) {
                if (ASStartTime == "") {
                    //sender.innerHTML = "Please enter Shift Auto Search Start Time";
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }
            }
            else {
                args.IsValid = true;
            }
        }


        function ValidateASEndTime(sender, args) {
            var ASEndTime = document.getElementById('ASEndTime').value;
            var RBID = '<%=RBShiftALType.ClientID %>';
            var RB1 = document.getElementById(RBID);
            var radio = RB1.getElementsByTagName("input");
            if (radio[1].checked) {
                if (ASEndTime == "") {
                    //sender.innerHTML = "Please enter Shift Auto Search End Time";
                    args.IsValid = false;
                }
                else { args.IsValid = true; }
            }
            else {
                args.IsValid = true;
            }
        }

        function TotTimeDifference() {

            var StartTime = document.getElementById('Shft_Frm').value;
            var EndTime = document.getElementById('Shft_To').value;
            var lnchInTime = document.getElementById('Lnch_Frm').value;
            var lnchOutTime = document.getElementById('Lnch_To').value;
            var earlierdate = new Date('12/12/1900' + ' ' + StartTime);
            var laterdate = new Date('12/13/1900' + ' ' + EndTime);
            var Lnhin = new Date('12/12/1900' + ' ' + lnchInTime);
            var lnhOut = new Date('12/13/1900' + ' ' + lnchOutTime);
            var difference = laterdate.getTime() - earlierdate.getTime();
            var lnchDiff = lnhOut.getTime() - Lnhin.getTime();
            var diff = Math.abs(difference - lnchDiff);

            var daysDifference = Math.floor(diff / 1000 / 60 / 60 / 24);
            diff -= daysDifference * 1000 * 60 * 60 * 24

            var hoursDifference = Math.floor(diff / 1000 / 60 / 60);
            diff -= hoursDifference * 1000 * 60 * 60

            var minutesDifference = Math.floor(diff / 1000 / 60);
            diff -= minutesDifference * 1000 * 60

            var secondsDifference = Math.floor(diff / 1000);
            document.getElementById('Tot_WrKHr').value = hoursDifference + ":" + minutesDifference;
            document.getElementById('hdnTot_WrKHr').value = hoursDifference + ":" + minutesDifference;
        }



        function HideLabel(labelID) {
            setTimeout("HideLabelHelper('" + labelID + "');", 3000);
        }

        function HideLabelHelper(labelID) {
            document.getElementById(labelID).style.display = "none";
        }

        function ResetAll() {

            $('#' + ["<%=txtDescription.ClientID%>", "<%=txtID.ClientID%>"].join(', #')).prop('value', "");
            document.getElementById('<%=lblMessages.ClientID%>').innerHTML = "";
            document.getElementById('<%=txtDescription.ClientID%>').focus();
            document.getElementById('<%=txtID.ClientID%>').focus();
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvShift.ClientID%>').focus();

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
    <style type="text/css">
        .test
        {
            color: Gray;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Shift Master View" ForeColor="RoyalBlue"
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
                        <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click" />
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                            OnClick="btnDelete_Click" />
                    </td>
                    <td style="width: 50%; text-align: right;">
                        <asp:Button runat="server" ID="cmdReset" Text="Reset" Style="float: right;" CssClass="ButtonControl"
                            OnClick="cmdReset_Click" />
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;"
                            CssClass="ButtonControl" OnClick="btnSearch_Click" />
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtID" runat="server" TargetControlID="txtID"
                            WatermarkCssClass="watermark" WatermarkText="ID">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:TextBox ID="txtDescription" runat="server" Style="float: right;" CssClass="searchTextBox"
                            onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                        <asp:TextBox ID="txtID" runat="server" Style="float: right;" CssClass="searchTextBox"
                            onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtDescription" runat="server" TargetControlID="txtDescription"
                            WatermarkText="Description" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                        <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                            <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvShift" runat="server" AutoGenerateColumns="false" Width="100%"
                                    GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="gvShift_RowCommand"
                                    OnRowDataBound="gvShift_RowDataBound">
                                    <RowStyle CssClass="gvRow" />
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                    <PagerStyle CssClass="gvPager" />
                                    <EmptyDataTemplate>
                                        <div>
                                            <span>No Shift found.</span>
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" SortExpression="Select" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ShowHeader="False" SortExpression="Edit" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                    Text="Edit" ForeColor="#3366FF" CommandArgument='<%#Eval("SHIFT_ID")%>'></asp:LinkButton>
                                                <asp:HiddenField ID="hdnFlag" runat="server" Value='<%#Eval("Flag") %>' />
                                                <asp:HiddenField ID="hdnRowID" runat="server" Value='<%#Eval("REC_ID") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SHIFT_ID" HeaderText="ID" SortExpression="ID"></asp:BoundField>
                                        <asp:BoundField DataField="SHIFT_DESCRIPTION" HeaderText="Description" ItemStyle-Width="10%"
                                            SortExpression="Description">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SHIFT_TYPE" HeaderText="Type" SortExpression="Type">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SHIFT_START" HeaderText="Shift Start" SortExpression="Shift Start"
                                            ItemStyle-Width="10%">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SHIFT_END" HeaderText="Shift End" SortExpression="Shift End">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SHIFT_BREAK_START" HeaderText="Lunch Start" SortExpression="Lunch Start">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SHIFT_BREAK_END" HeaderText="Lunch End" SortExpression="Lunch End">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SHIFT_BREAK_HRS" HeaderText="Break HRs" SortExpression="Break HRs">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SHIFT_WORKHRS" HeaderText="Total Work Hours" SortExpression="Total Work Hours">
                                            <ItemStyle Width="10%" />
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
    <asp:Button ID="btnDummyAddEdit" runat="server" Text="dummy" Style="display: none;" />
    <asp:Panel ID="pnlAddEditShift" runat="server" CssClass="PopupPanel" Width="50%"
        Style="overflow: auto;">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="table4" runat="server" width="100%" height="50%" border="0" cellpadding="0"
                    cellspacing="5" class="TableClass">
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Shift ID :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%; margin-left: 40px;">
                            <asp:TextBox ID="Shft_id" runat="server" onkeypress="return IsAlphanumericWithoutspace(event)"
                                CssClass="TextControl" MaxLength="2" Style="text-transform: uppercase;" ClientIDMode="Static"
                                TabIndex="1" ValidationGroup="Submit"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvShft_id" runat="server" ControlToValidate="Shft_id"
                                Display="None" ErrorMessage="Please enter Shift ID" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvShft_id" runat="server" TargetControlID="rfvShft_id"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Shift Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:TextBox ID="Shft_desc" runat="server" onkeypress="return IsAlphanumeric(event)"
                                CssClass="TextControl" MaxLength="20" Style="text-transform: capitalize;" ClientIDMode="Static"
                                TabIndex="2" ValidationGroup="Submit"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvShft_desc" runat="server" ControlToValidate="Shft_desc"
                                Display="None" ErrorMessage="Please enter Shift Description" ForeColor="Red"
                                SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvShft_desc" runat="server" TargetControlID="rfvShft_desc"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Type :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:DropDownList ID="Shft_Type" runat="server" ClientIDMode="Static" TabIndex="3"
                                Width="173px" ValidationGroup="Submit">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvShft_Type" runat="server" ControlToValidate="Shft_Type"
                                Display="None" ErrorMessage="Please select Shift Type" ForeColor="Red" InitialValue="-1"
                                SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvShft_Type" runat="server" TargetControlID="rfvShft_Type"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Shift Start :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:TextBox ID="Shft_Frm" runat="server" CssClass="TextControl" MaxLength="5" onkeyup="fnColon(this,event)"
                                onkeypress="findspace(event)" ClientIDMode="Static" TabIndex="4" ValidationGroup="Submit"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTEShft_Frm" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="Shft_Frm" ValidChars=":" />
                            <%--<span style="font-size: smaller; color: Gray;">24 Hrs Format</span>--%>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                TargetControlID="Shft_Frm" WatermarkText="24 Hrs Format" WatermarkCssClass="test">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:RequiredFieldValidator ID="rfvShft_Frm" runat="server" ControlToValidate="Shft_Frm"
                                Display="None" ErrorMessage="Please enter Shift Start " ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvShft_Frm" runat="server" TargetControlID="rfvShft_Frm"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revShft_Frm" runat="server" ControlToValidate="Shft_Frm"
                                ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevShft_Frm" runat="server" TargetControlID="revShft_Frm"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Shift End :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:TextBox ID="Shft_To" runat="server" CssClass="TextControl" MaxLength="5" onkeyup="fnColon(this,event)"
                                onkeypress="findspace(event)" ClientIDMode="Static" TabIndex="5" ValidationGroup="Submit"
                                onkeydown="get_time_difference('N')"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTEShft_To" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="Shft_To" ValidChars=":" />
                            <%--<span style="font-size: smaller; color: Gray;">24 Hrs Format</span>--%>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                TargetControlID="Shft_To" WatermarkText="24 Hrs Format" WatermarkCssClass="test">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:RequiredFieldValidator ID="rfvShft_To" runat="server" ControlToValidate="Shft_To"
                                Display="None" ErrorMessage="Please enter Shift End" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvShft_To" runat="server" TargetControlID="rfvShft_To"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revShft_To" runat="server" ControlToValidate="Shft_To"
                                ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Submit">
                            </asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevShft_To" runat="server" TargetControlID="revShft_To"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="cvShft_To" runat="server" ControlToValidate="Shft_To" Display="None"
                                ForeColor="Red" ValidateEmptyText="True" ClientValidationFunction="ValidateShft_To"
                                ValidationGroup="Submit"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcecvShft_To" runat="server" TargetControlID="cvShft_To"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Break Start :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:TextBox ID="Lnch_Frm" runat="server" CssClass="TextControl" MaxLength="5" onkeyup="fnColon(this,event)"
                                onkeypress="findspace(event)" Style="text-transform: capitalize;" ClientIDMode="Static"
                                TabIndex="6" ValidationGroup="Submit"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTELnch_Frm" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="Lnch_Frm" ValidChars=":" />
                            <%--<span style="font-size: smaller; color: Gray;">24 Hrs Format</span>--%>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server"
                                TargetControlID="Lnch_Frm" WatermarkText="24 Hrs Format" WatermarkCssClass="test">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:RequiredFieldValidator ID="rfvLnch_Frm" runat="server" ControlToValidate="Lnch_Frm"
                                Display="None" ErrorMessage="Please enter Lunch Start" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvLnch_Frm" runat="server" TargetControlID="rfvLnch_Frm"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revLnch_Frm" runat="server" ControlToValidate="Lnch_Frm"
                                ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Submit">
                            </asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevLnch_Frm" runat="server" TargetControlID="revLnch_Frm"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Break End :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:TextBox ID="Lnch_To" runat="server" onkeyup="fnColon(this,event)" onkeypress="findspace(event)"
                                CssClass="TextControl" MaxLength="5" Style="text-transform: capitalize;" ClientIDMode="Static"
                                TabIndex="7" ValidationGroup="Submit" onkeydown="get_time_difference('N')"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTELnch_To" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="Lnch_To" ValidChars=":" />
                            <%--<span style="font-size: smaller; color: Gray;">24 Hrs Format</span>--%>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server"
                                TargetControlID="Lnch_To" WatermarkText="24 Hrs Format" WatermarkCssClass="test">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:RequiredFieldValidator ID="rfvLnch_To" runat="server" ControlToValidate="Lnch_To"
                                Display="None" ErrorMessage="Please enter Lunch End" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvLnch_To" runat="server" TargetControlID="rfvLnch_To"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revLnch_To" runat="server" ControlToValidate="Lnch_To"
                                ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Submit">
                            </asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevLnch_To" runat="server" TargetControlID="revLnch_To"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Deduct Break Hours From Total Work Hours :
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="ChkShft" ClientIDMode="Static" TabIndex="8" ValidationGroup="Submit"
                                onClick="get_time_difference('N')" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Total Break Hours :
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:TextBox ID="Tot_BrHr" runat="server" CssClass="TextControl" MaxLength="5" Enabled="false"
                                Style="text-transform: capitalize;" ClientIDMode="Static" TabIndex="9" ValidationGroup="Submit"></asp:TextBox>
                            <%--<span style="font-size: smaller; color: Gray;">24 Hrs Format</span>--%>
                            <asp:HiddenField ID="hdnTot_BrHr" runat="server" />
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server"
                                TargetControlID="Tot_BrHr" WatermarkText="24 Hrs Format" WatermarkCssClass="test">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Total Work Hours :
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:TextBox ID="Tot_WrKHr" runat="server" CssClass="TextControl" MaxLength="5" Enabled="false"
                                Style="text-transform: capitalize;" ClientIDMode="Static" TabIndex="9" ValidationGroup="Submit"></asp:TextBox>
                            <asp:HiddenField ID="hdnTot_WrKHr" runat="server" />
                            <%--<span style="font-size: smaller; color: Gray;">24 Hrs Format</span>--%>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server"
                                TargetControlID="Tot_WrKHr" WatermarkText="24 Hrs Format" WatermarkCssClass="test">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Weekend different timings :
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="WChkShft1" ClientIDMode="Static" TabIndex="10" onclick="WeekEndDiffTIme();"
                                ValidationGroup="Submit" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Week end Shift Start :<label class="CompulsaryLabel"></label>
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:TextBox ID="WShft_Frm" runat="server" CssClass="TextControl" MaxLength="5" onkeyup="fnColon(this,event)"
                                onkeypress="findspace(event)" ClientIDMode="Static" TabIndex="11" ValidationGroup="Submit"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTEWShft_Frm" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="WShft_Frm" ValidChars=":" />
                            <%--<span style="font-size: smaller; color: Gray;">24 Hrs Format</span>--%>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server"
                                TargetControlID="WShft_Frm" WatermarkText="24 Hrs Format" WatermarkCssClass="test">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:CustomValidator ID="cvWShft_Frm" runat="server" ControlToValidate="WShft_Frm"
                                Display="None" ForeColor="Red" ValidateEmptyText="True" ClientValidationFunction="ValidateWShft_Frm"
                                ValidationGroup="Submit"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcecvWShft_Frm" runat="server" TargetControlID="cvWShft_Frm"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revWShft_Frm" runat="server" ControlToValidate="WShft_Frm"
                                ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevWShft_Frm" runat="server" TargetControlID="revWShft_Frm"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Week end Shift End :<label class="CompulsaryLabel"></label>
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:TextBox ID="WShft_To" runat="server" CssClass="TextControl" MaxLength="5" onkeyup="fnColon(this,event)"
                                onkeypress="findspace(event)" ClientIDMode="Static" TabIndex="12" ValidationGroup="Submit"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTEWShft_To" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="WShft_To" ValidChars=":" />
                            <%--<span style="font-size: smaller; color: Gray;">24 Hrs Format</span>--%>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server"
                                TargetControlID="WShft_To" WatermarkText="24 Hrs Format" WatermarkCssClass="test">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:CustomValidator ID="cvWShft_To" runat="server" ControlToValidate="WShft_To"
                                Display="None" ForeColor="Red" ValidateEmptyText="True" ClientValidationFunction="ValidateWShft_To"
                                ValidationGroup="Submit"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcecvWShft_To" runat="server" TargetControlID="cvWShft_To"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revWShft_To" runat="server" ControlToValidate="WShft_To"
                                ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Submit">
                            </asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevWShft_To" runat="server" TargetControlID="revWShft_To"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Week end Break Start :<label class="CompulsaryLabel"></label>
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:TextBox ID="WLnch_Frm" runat="server" CssClass="TextControl" MaxLength="5" onkeyup="fnColon(this,event)"
                                onkeypress="findspace(event)" Style="text-transform: capitalize;" ClientIDMode="Static"
                                TabIndex="13" ValidationGroup="Submit"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTEWLnch_Frm" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="WLnch_Frm" ValidChars=":" />
                            <%--<span style="font-size: smaller; color: Gray;">24 Hrs Format</span>--%>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server"
                                TargetControlID="WLnch_Frm" WatermarkText="24 Hrs Format" WatermarkCssClass="test">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:CustomValidator ID="cvWLnch_Frm" runat="server" ControlToValidate="WLnch_Frm"
                                Display="None" ForeColor="Red" ValidateEmptyText="True" ClientValidationFunction="ValidateWLnch_Frm"
                                ValidationGroup="Submit"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcecvWLnch_Frm" runat="server" TargetControlID="cvWLnch_Frm"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revWLnch_Frm" runat="server" ControlToValidate="WLnch_Frm"
                                ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Submit">
                            </asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevWLnch_Frm" runat="server" TargetControlID="revWLnch_Frm"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Week end Break End :<label class="CompulsaryLabel"></label>
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:TextBox ID="WLnch_To" runat="server" onkeyup="fnColon(this,event)" onkeypress="findspace(event)"
                                CssClass="TextControl" MaxLength="5" Style="text-transform: capitalize;" ClientIDMode="Static"
                                TabIndex="14" ValidationGroup="Submit"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTEWLnch_To" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="WLnch_To" ValidChars=":" />
                            <%--<span style="font-size: smaller; color: Gray;">24 Hrs Format</span>--%>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server"
                                TargetControlID="WLnch_To" WatermarkText="24 Hrs Format" WatermarkCssClass="test">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:CustomValidator ID="cvWLnch_To" runat="server" ControlToValidate="WLnch_To"
                                Display="None" ForeColor="Red" ValidateEmptyText="True" ClientValidationFunction="ValidateWLnch_To"
                                ValidationGroup="Submit"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcecvWLnch_To" runat="server" TargetControlID="cvWLnch_To"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revWLnch_To" runat="server" ControlToValidate="WLnch_To"
                                ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Submit">
                            </asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevWLnch_To" runat="server" TargetControlID="revWLnch_To"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CheckBox runat="server" ID="WChkShft" ClientIDMode="Static" TabIndex="7" Visible="False"
                                ValidationGroup="Submit" />
                            <asp:TextBox ID="WTot_WrKHr" runat="server" CssClass="TextControl" MaxLength="5"
                                onfocus="get_time_difference('W')" Style="text-transform: capitalize; display: none"
                                ClientIDMode="Static" TabIndex="8" ValidationGroup="Submit"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Shift Allocation Type :
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:RadioButtonList ID="RBShiftALType" runat="server" RepeatDirection="Horizontal"
                                onchange="return ValidateShiftAllocationType(this);" ClientIDMode="Static" TabIndex="15"
                                ValidationGroup="Submit">
                                <asp:ListItem Value="ASSIGNED">Assigned</asp:ListItem>
                                <asp:ListItem Value="AUTO">Auto</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Shift Auto Search Start Time :
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:TextBox ID="ASStartTime" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="5" onkeypress="findspace(event)" onkeyup="fnColon(this,event)" Style="text-transform: capitalize;"
                                TabIndex="16" ValidationGroup="Submit"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTEASStartTime" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="ASStartTime" ValidChars=":" />
                            <%--<span style="font-size: smaller; color: Gray;">24 Hrs Format</span>--%>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender11" runat="server"
                                TargetControlID="ASStartTime" WatermarkText="24 Hrs Format" WatermarkCssClass="test">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:CustomValidator ID="cvASStartTime" runat="server" ControlToValidate="ASStartTime"
                                Display="None" ForeColor="Red" ValidateEmptyText="True" ClientValidationFunction="ValidateASStartTime"
                                ValidationGroup="Submit"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcecvASStartTime" runat="server" TargetControlID="cvASStartTime"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revASStartTime" runat="server" ControlToValidate="ASStartTime"
                                ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevASStartTime" runat="server" TargetControlID="revASStartTime"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Shift Auto Search End Time :
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:TextBox ID="ASEndTime" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="5" onkeypress="findspace(event)" onkeyup="fnColon(this,event)" Style="text-transform: capitalize;"
                                TabIndex="17" ValidationGroup="Submit"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTEASEndTime" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="ASEndTime" ValidChars=":" />
                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender12" runat="server"
                                TargetControlID="ASEndTime" WatermarkText="24 Hrs Format" WatermarkCssClass="test">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:CustomValidator ID="cvASEndTime" runat="server" ControlToValidate="ASEndTime"
                                Display="None" ForeColor="Red" ValidateEmptyText="True" ClientValidationFunction="ValidateASEndTime"
                                ValidationGroup="Submit"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcecvASEndTime" runat="server" TargetControlID="cvASEndTime"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revASEndTime" runat="server" ControlToValidate="ASEndTime"
                                ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevASEndTime" runat="server" TargetControlID="revASEndTime"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Shift Early Search Hours :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:TextBox ID="EarlySearchHours" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="5" onkeypress="findspace(event)" onkeyup="fnColon(this,event)" Style="text-transform: capitalize;"
                                TabIndex="18" ValidationGroup="Submit"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTEEarlySearchHours" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="EarlySearchHours" ValidChars=":" />
                            <asp:RequiredFieldValidator ID="rfvEarlySearchHours" runat="server" ControlToValidate="EarlySearchHours"
                                Display="None" ErrorMessage="Please enter Shift Early Search Hours" ForeColor="Red"
                                SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvEarlySearchHours" runat="server"
                                TargetControlID="rfvEarlySearchHours" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revEarlySearchHours" runat="server" ControlToValidate="ASStartTime"
                                ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevEarlySearchHours" runat="server"
                                TargetControlID="revEarlySearchHours" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDClassLabel" style="height: 10px; width: 50%">
                            Shift Late Search Hours :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="height: 10px; width: 50%">
                            <asp:TextBox ID="LateSearchHours" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="5" onkeypress="findspace(event)" onkeyup="fnColon(this,event)" Style="text-transform: capitalize;"
                                TabIndex="19" ValidationGroup="Submit"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTELateSearchHours" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="LateSearchHours" ValidChars=":" />
                            <asp:RequiredFieldValidator ID="rfvLateSearchHours" runat="server" ControlToValidate="LateSearchHours"
                                Display="None" ErrorMessage="Please enter Shift Late Search Hours" ForeColor="Red"
                                SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvLateSearchHours" runat="server" TargetControlID="rfvLateSearchHours"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revLateSearchHours" runat="server" ControlToValidate="ASStartTime"
                                ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevLateSearchHours" runat="server" TargetControlID="revLateSearchHours"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center" style="height: 40px">
                            <asp:Button ID="BtnSave" runat="server" CssClass="ButtonControl" Text="Save" TabIndex="20"
                                onfocus="get_time_difference('N'),get_time_difference('W')" OnClick="BtnSave_Cilck"
                                ValidationGroup="Submit" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="ButtonControl" Text="Cancel"
                                TabIndex="21" OnClick="btnCancel_Click" CausesValidation="False" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Label ID="lblError" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <%--  <Triggers>
                <asp:PostBackTrigger ControlID="Shft_Type" />
            </Triggers>--%>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeAddEditShift" runat="server" TargetControlID="btnDummyAddEdit"
        PopupControlID="pnlAddEditShift" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancel">
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
