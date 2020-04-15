<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="UNO.WebForm2" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    UICulture="en-GB" CodeBehind="ShiftScheduleCorrection.aspx.cs" Inherits="UNO.ShiftScheduleCorrection" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript">


     

        function MTimer() {
            var mT;
            mT = setTimeout(ClearMessage, 3000);
        }

       
        function ClearMessage() {

            document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = " ";
        }

          function RecSave(iCount) {
            document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = iCount + " Record(s) Saved Successfully";
            MTimer();
            // PageRefresh();
        }


        function IsAlphanumeric(evnt) {


            var charCode = (evnt.which) ? evnt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) || (charCode >= 48 && charCode <= 57) ||
			(charCode >= 97 && charCode <= 122) || (charCode == 8) || (charCode == 32)) {
                return true
            }
            else {
                return false
            }
        }

      

        function IsNumericSwipe(evnt) {

            var charCode = (evnt.which) ? evnt.which : event.keyCode
            if ((charCode >= 48 && charCode <= 57) || (charCode == 8) || (charCode == 32)) {
                return true
            }
            else {
                return false
            }
        }





        function PageRefresh() {

              $('#' + ["<%=txtFromDateSC.ClientID%>", "<%=txtToDateSC.ClientID%>"].join(', #')).prop('value', "");
            document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = "";
            document.getElementById('RbdEmp_0').checked = true;
            document.getElementById('RbdCompany_0').checked = true;
            document.getElementById('RbdLocation_0').checked = true;
            document.getElementById('RbdDivision_0').checked = true;
            document.getElementById('RbdDepartment_0').checked = true;
            document.getElementById('RbdCategory_0').checked = true;


            document.getElementById('RbdCompany_0').disabled = false;
            document.getElementById('RbdCompany_1').disabled = false;

            document.getElementById('RbdLocation_0').disabled = false;
            document.getElementById('RbdLocation_1').disabled = false;

            document.getElementById('RbdDivision_0').disabled = false;
            document.getElementById('RbdDivision_1').disabled = false;

            document.getElementById('RbdDepartment_0').disabled = false;
            document.getElementById('RbdDepartment_1').disabled = false;

            document.getElementById('RbdCategory_0').disabled = false;
            document.getElementById('RbdCategory_1').disabled = false;


            //document.getElementById('txtshiftcode').selectedIndex = 0;
            document.getElementById('txtshiftStartDate').value = "";
            document.getElementById('txtMinimumSwipe').value = "";
            document.getElementById('ddweekoff').selectedIndex = 0;
            document.getElementById('ddWeekend').selectedIndex = 0;



            document.getElementById('EmployeeHdn').value = "";
            document.getElementById('ComapnyHdn').value = "";
            document.getElementById('LocationHdn').value = "";
            document.getElementById('DivisionHdn').value = "";
            document.getElementById('DepartmentHdn').value = "";
            document.getElementById('ShiftHdn').value = "";

            document.getElementById('rblScheduleType_1').checked = true;


        }

        function disabledcontrols() {
            document.getElementById('LstEntitySelected').style.display = "none";
            document.getElementById('<%=LstEmployee.ClientID%>').style.display = "none";
            document.getElementById('LstCompany').style.display = "none";
            document.getElementById('LstLocation').style.display = "none";
            document.getElementById('LstDivision').style.display = "none";
            document.getElementById('LstDepartment').style.display = "none";
            document.getElementById('LstCategory').style.display = "none";
            document.getElementById('RbdEmp_1').checked = true;
            document.getElementById('RbdCompany_1').checked = true;
            document.getElementById('RbdLocation_1').checked = true;
            document.getElementById('RbdDivision_1').checked = true;
            document.getElementById('RbdDepartment_1').checked = true;
            document.getElementById('RbdCategory_1').checked = true;

            document.getElementById('Btntd').style.display = "none";

        }
        //Added by Pooja Yadav
        function Validation() {
            var first = "";
            var count = 0;

            if (document.getElementById('RbdEmp_1').checked == true) {
                if (document.getElementById('LstEntitySelected').options.length == 0) {
                    document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = 'Please select an entity';
                    return false;
                }
            }

            else if (document.getElementById('RbdCompany_1').checked == true) {
                if (document.getElementById('LstEntitySelected').options.length == 0) {
                    document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = 'Please select an entity';
                    return false;
                }
            }

            else if (document.getElementById('RbdLocation_1').checked == true) {
                if (document.getElementById('LstEntitySelected').options.length == 0) {
                    document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = 'Please select an entity';
                    return false;
                }
            }

            else if (document.getElementById('RbdDivision_1').checked == true) {
                if (document.getElementById('LstEntitySelected').options.length == 0) {
                    document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = 'Please select an entity';
                    return false;
                }
            }

            else if (document.getElementById('RbdDepartment_1').checked == true) {
                if (document.getElementById('LstEntitySelected').options.length == 0) {
                    document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = 'Please select an entity';
                    return false;
                }
            }

            else if (document.getElementById('RbdCategory_1').checked == true) {
                if (document.getElementById('LstEntitySelected').options.length == 0) {
                    document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = 'Please select an entity';
                    return false;
                }
            }
            else {
                return true;
            }

        }
      
        function SearchList() {

            var l = null;
            myVals = null;

            if (document.getElementById('<%=LstEmployee.ClientID%>').style.display == "inline") {
                //                l = document.getElementById('<%=LstEmployee.ClientID%>');
                //                myVals = myValsEmp;
            }
            else if (document.getElementById('LstCompany').style.display == "inline") {
                l = document.getElementById('LstCompany');
                myVals = myValsComp;
            }
            else if (document.getElementById('LstLocation').style.display == "inline") {
                l = document.getElementById('LstLocation');
                myVals = myValsLoc;
            }
            else if (document.getElementById('LstDivision').style.display == "inline") {
                l = document.getElementById('LstDivision');
                myVals = myValsDiv;
            }
            else if (document.getElementById('LstDepartment').style.display == "inline") {
                l = document.getElementById('LstDepartment');
                myVals = myValsDep;
            }
            else if (document.getElementById('LstCategory').style.display == "inline") {
                l = document.getElementById('LstCategory');
                myVals = myValsCat;
            }
            var list = document.getElementById("<%=LstEmployee.ClientID%>");
            var list1 = document.getElementById("<%=lstEmployDummy.ClientID%>");
            var textEmpToSearch = document.getElementById("<%=txtSearchEmp.ClientID%>");


            var items = 0;

            for (items = list1.options.length - 1; items >= 0; items--) {
                list.options[items] = null;
            }

            var i = 0, j = 0;
            var strList;
            var strTxt = textEmpToSearch.value.toLowerCase().replace(/\s/g, '');
            var strLength = textEmpToSearch.value.length;

            for (i = 0; i <= list1.options.length - 1; i++) {

                iSearchPos = list1.options[i].text.toLowerCase().replace(/\s/g, '').search(strTxt);


                if (iSearchPos >= 0) {

                    list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                    j++;
                }

            }


        }


        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }

        function backtoViewMode() {
            window.location = "EmployeeTimeAttendanceView.aspx"

        }





    </script>
       <script type="text/javascript">

           function ValidateShift() {

               var rblCorrection = document.getElementById('<%=rblCorrectionType.ClientID%>');
               var FromDate = document.getElementById('<%=txtFromDateSC.ClientID%>').value;
               var ToDate = document.getElementById('<%=txtToDateSC.ClientID%>').value;
               if ($('#' + rblCorrection.id + ' input:checked').val() == '1') {
                   document.getElementById('<%=txtToDateSC.ClientID%>').disabled = true;

                   if (FromDate != "") {
                       document.getElementById('<%=txtToDateSC.ClientID%>').value = document.getElementById('<%=txtFromDateSC.ClientID%>').value;
                   }
               }
               if ($('#' + rblCorrection.id + ' input:checked').val() == '2') {
                   document.getElementById('<%=txtFromDateSC.ClientID%>').disabled = false;
                   document.getElementById('<%=txtToDateSC.ClientID%>').disabled = false;
                   if (FromDate != "" && ToDate != "") {
                       if (FromDate == ToDate) {
                           document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = "Please select date range.";
                           return false;
                       }
                   }
               }

               if ($('#' + rblCorrection.id + ' input:checked').val() == '3') {
                   document.getElementById('<%=txtFromDateSC.ClientID%>').disabled = false;
                   document.getElementById('<%=txtToDateSC.ClientID%>').disabled = true;
                   document.getElementById('<%=txtToDateSC.ClientID%>').value = "";
               }
               return true;
           }


           function ValidateSave() {
               //debugger;
               var ddlshift = document.getElementById('<%=ddlShift.ClientID%>').value;
               if (ddlshift == "Select") {
                   document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = "Please select shift.";
                   return false;
               }
               var rblCorrection = document.getElementById('<%=rblCorrectionType.ClientID%>');
               var emp = document.getElementById('<%=EmployeeHdn.ClientID%>').value;
               var com = document.getElementById('ComapnyHdn').value;
               var loc = document.getElementById('LocationHdn').value;
               var div = document.getElementById('DivisionHdn').value;
               var dep = document.getElementById('DepartmentHdn').value;
               var sft = document.getElementById('ShiftHdn').value;
               if (ValidateShift()) {

                   if ($('#' + rblCorrection.id + ' input:checked').val() != '3') {

                       if (document.getElementById('<%=txtFromDateSC.ClientID%>').value == "") {
                           document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = "Please Enter From Date.";
                           return false;
                       }
                       if (document.getElementById('<%=txtToDateSC.ClientID%>').value == "") {
                           document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = "Please Enter To Date.";
                           return false;
                       }
                   }

                   if ($('#' + rblCorrection.id + ' input:checked').val() == '2') {

                       var strMsg = CompareDates(document.getElementById('<%=txtFromDateSC.ClientID%>'), document.getElementById('<%=txtToDateSC.ClientID%>'));
                       if (strMsg == "") {
                           document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = "";
                           document.getElementById('<%=hdnFromDate.ClientID%>').value = document.getElementById('<%=txtFromDateSC.ClientID%>').value;
                           document.getElementById('<%=hdnToDate.ClientID%>').value = document.getElementById('<%=txtToDateSC.ClientID%>').value;
                           __doPostBack("<%=btnSave.UniqueID %>", "");
                           return true;
                       }
                       else {
                           document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = strMsg;
                           return false;
                       }
                   }

                   if (emp == "" && com == "" && loc == "" && div == "" && dep == "" && sft == "") {
                       var msg = confirm("No Entity Selected,Are you Sure To continue?");
                       if (msg == false) {
                           return false;
                       }
                   }
                   document.getElementById('<%=lblErrorMsg.ClientID%>').innerHTML = "";
                   document.getElementById('<%=hdnFromDate.ClientID%>').value = document.getElementById('<%=txtFromDateSC.ClientID%>').value;
                   document.getElementById('<%=hdnToDate.ClientID%>').value = document.getElementById('<%=txtToDateSC.ClientID%>').value;
                   __doPostBack("<%=btnSave.UniqueID %>", "");
                   return true;
                  
               }

           }
           function ValidateDate() {
               ValidateShift();
           }

    </script>
    <%-- <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">--%>
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $(".webticker").webTicker();

        });
    </script>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .cssVEh
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
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
            font-size: 48px;
            font-family: Georgia, "Times New Roman" , Times, serif;
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
        .style39
        {
            width: 458px;
        }
        .attendLabel
        {
            text-align: right;
            margin-left: 15px;
            color: Aqua;
        }
        
        .calWeeklyOff
        {
            /*  color: Red;*/ /*background: #A80000 50% top repeat-x;
            font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;
             /*  text-align: right;*/
            background-color: #D0D0D0 !important;
        }
    </style>
    <script language="javascript" type="text/javascript">


        function pageloadShift() {


            document.getElementById('<%=lblShiftType.ClientID%>').style.display = "block";

            document.getElementById('<%=ddlShiftPattern.ClientID%>').style.display = "none";

            document.getElementById('<%=ddlShift.ClientID%>').style.display = "block"


        }


        function FillFixed() {

            document.getElementById('<%=lblShiftType.ClientID%>').style.display = "block";

            document.getElementById('<%=ddlShiftPattern.ClientID%>').style.display = "none"

            document.getElementById('<%=ddlShift.ClientID%>').style.display = "block";




        }


        function FillPattern() {

            document.getElementById('<%=lblShiftType.ClientID%>').style.display = "block";
            document.getElementById('<%=ddlShiftPattern.ClientID%>').style.display = "block";

            document.getElementById('<%=ddlShift.ClientID%>').style.display = "none"


        }

    </script>
    <script language="javascript" type="text/javascript">

        function pageloadShfitType() {



            document.getElementById('rblScheduleType_1').checked = true;
            document.getElementById('rblScheduleType_0').disabled = false;
            document.getElementById('rblScheduleType_1').disabled = false;

            document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_ddlShiftPattern').style.display = "none";

            document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_ddlShift').style.display = "block"
            //         

        }



        function EnableShift() {



            document.getElementById('rblScheduleType_1').checked = true;
            document.getElementById('rblScheduleType_0').disabled = false;
            document.getElementById('rblScheduleType_1').disabled = false;

            document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_ddlShiftPattern').style.display = "none";

            document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_ddlShift').style.display = "block"
            //         

        }


        function DisableShift() {

            //            document.getElementById('<%=lblShiftType.ClientID%>').disabled = false;

            //            document.getElementById('<%=ddlShiftPattern.ClientID%>').disabled = false;

            //            document.getElementById('<%=ddlShift.ClientID%>').disabled = true;

            document.getElementById('rblScheduleType_1').checked = true;
            document.getElementById('rblScheduleType_0').disabled = true;
            document.getElementById('rblScheduleType_1').disabled = false;

            document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_ddlShiftPattern').style.display = "none";

            document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_ddlShift').style.display = "block"
            //            document.getElementById('<%=ddlShiftPattern.ClientID%>').style.display = "none";

            //            document.getElementById('<%=ddlShift.ClientID%>').style.display = "block"




        }

    </script>
    <script type="text/jscript">

        $.fn.timepicki = function (options) {

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


    </script>
	 <script type="text/javascript">
        function hidedv() {
            document.getElementById('detail').style.display = "none";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td colspan="4" style="height: 25px;">
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td colspan="4" align="center">
                <asp:Label ID="lblHederText" runat="server" Text="Shift Schedule Correction" CssClass="heading"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" class="TableClass">
        <tr>
            <td style="width: 10px;">
            </td>
            <td style="width: 82%;" valign="top">
                <asp:Panel ID="pnlFilter" runat="server" Width="100%">
                    <table class="TableClass" style="width: 100%; height: 75px;">
                        <tr>
                            <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;
                                width: 5%;">
                                <table style="height: 75px; width: 25%;">
                                    <tr>
                                        <td class="style29">
                                            &nbsp;Employee
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left" class="style29">
                                            <asp:RadioButtonList ID="RbdEmp" runat="server" RepeatLayout="Flow" CssClass="radiobutton"
                                                ClientIDMode="Static" OnSelectedIndexChanged="RbdEmp_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                                <asp:ListItem Value="1" >Select</asp:ListItem>
                                                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                          
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="EmployeeHdn" runat="server" ClientIDMode="Static" />
                            </td>
                            <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;
                                width: 7%">
                                <table style="height: 75px;">
                                    <tr>
                                        <td>
                                            Company
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:RadioButtonList ID="RbdCompany" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                ClientIDMode="Static">
                                                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                                <asp:ListItem Value="1">Select</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="ComapnyHdn" runat="server" ClientIDMode="Static" />
                            </td>
                            <td style="width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                <table style="height: 75px;">
                                    <tr>
                                        <td>
                                            Location
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:RadioButtonList ID="RbdLocation" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                ClientIDMode="Static">
                                                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                                <asp:ListItem Value="1">Select</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="LocationHdn" runat="server" ClientIDMode="Static" />
                            </td>
                            <td style="width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                <table style="height: 75px;">
                                    <tr>
                                        <td>
                                            Division
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:RadioButtonList ID="RbdDivision" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                ClientIDMode="Static">
                                                <asp:ListItem Value="0"  Selected="True">All</asp:ListItem>
                                                <asp:ListItem Value="1" >Select</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="DivisionHdn" runat="server" ClientIDMode="Static" />
                            </td>
                            <td style="width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                <table style="height: 75px;" id="TABLE2">
                                    <tr>
                                        <td>
                                            Department
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:RadioButtonList ID="RbdDepartment" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                ClientIDMode="Static">
                                                <asp:ListItem Value="0"  Selected="True">All</asp:ListItem>
                                                <asp:ListItem Value="1">Select</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="DepartmentHdn" runat="server" ClientIDMode="Static" />
                            </td>
                            <td style="width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                <table style="height: 75px;" id="TABLE1">
                                    <tr>
                                        <td>
                                            &nbsp;Category
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:RadioButtonList ID="RbdCategory" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                ClientIDMode="Static">
                                                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                                <asp:ListItem Value="1" >Select</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="ShiftHdn" runat="server" ClientIDMode="Static" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PnlFilterListBox" runat="server">
                    <div id="detail" style="display: none">
                        <table style="width: 62%; margin-left: 15%;" border="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtSearchEmp" runat="server" placeholder="Search" Style="display: none"
                                        ClientIDMode="Static" Width="355px" ></asp:TextBox>
                                    <asp:TextBox ID="txtCompany" runat="server" placeholder="Search" Style="display: none"
                                        Width="355px"></asp:TextBox>
                                    <asp:TextBox ID="txtLocation" runat="server" placeholder="Search" Style="display: none"
                                        Width="355px"></asp:TextBox>
                                    <asp:TextBox ID="txtDivision" runat="server" placeholder="Search" Style="display: none"
                                        Width="355px"></asp:TextBox>
                                    <asp:TextBox ID="txtDepartment" runat="server" placeholder="Search" Style="display: none"
                                        Width="355px"></asp:TextBox>
                                    <asp:TextBox ID="txtShift" runat="server" placeholder="Search" Style="display: none"
                                        Width="355px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAvailableData" runat="server" Text="Available Data"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblSeletcedData" runat="server" Text="Selected Data" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ListBox ID="LstEmployee" runat="server" ClientIDMode="Static" Style="display: none"
                                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="355px" Height="119px"></asp:ListBox>
                                    <asp:ListBox ID="lstEmployDummy" runat="server" ClientIDMode="Static" Style="display: none"
                                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="355px" Height="119px"></asp:ListBox>
                                    <asp:ListBox ID="LstCompany" runat="server" ClientIDMode="Static" Style="display: none"
                                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="355px" Height="119px"></asp:ListBox>
                                    <asp:ListBox ID="LstLocation" runat="server" ClientIDMode="Static" Style="display: none"
                                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="355px" Height="119px"></asp:ListBox>
                                    <asp:ListBox ID="LstDivision" runat="server" ClientIDMode="Static" Style="display: none"
                                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="355px" Height="119px"></asp:ListBox>
                                    <asp:ListBox ID="LstDepartment" runat="server" ClientIDMode="Static" Style="display: none"
                                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="355px" Height="119px"></asp:ListBox>
                                    <asp:ListBox ID="LstCategory" runat="server" ClientIDMode="Static" Style="display: none"
                                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="355px" Height="119px"></asp:ListBox>
                                    <asp:ListBox ID="LstCompanyDummy" runat="server" ClientIDMode="Static" Style="display: none"
                                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="355px" Height="119px"></asp:ListBox>
                                    <asp:ListBox ID="LstLocationDummy" runat="server" ClientIDMode="Static" Style="display: none"
                                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="355px" Height="119px"></asp:ListBox>
                                    <asp:ListBox ID="LstDivisionDummy" runat="server" ClientIDMode="Static" Style="display: none"
                                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="355px" Height="119px"></asp:ListBox>
                                    <asp:ListBox ID="LstDepartmentDummy" runat="server" ClientIDMode="Static" Style="display: none"
                                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="355px" Height="119px"></asp:ListBox>
                                    <asp:ListBox ID="LstCategoryDummy" runat="server" ClientIDMode="Static" Style="display: none"
                                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="355px" Height="119px"></asp:ListBox>
                                </td>
                                <td id="Btntd" clientidmode="Static" style="display: none;" runat="server">
                                    <table style="height: 100px">
                                        <tr>
                                            <td align="left">
                                                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                                <input id="cmdEntityAllRight" class="ButtonControl" 
                                                    value="&gt;&gt;" style="width: 50px" type="button"  runat="server"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                                <input id="cmdEntityRight" class="ButtonControl" value="&gt;" runat="server"
                                                    style="width: 50px" type="button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                                <input id="cmdEntityLeft" class="ButtonControl" value="&lt;" runat="server"
                                                    style="width: 50px" type="button" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                                <input id="cmdEntityAllLeft" class="ButtonControl" value="&lt;&lt;" onclick="removeEntitySelectedListBox()"
                                                    style="width: 50px" type="button" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:ListBox ID="LstEntitySelected" runat="server" ClientIDMode="Static" Style="display: none"
                                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="360px" Height="119px"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center">
                                    <%-- <input id="btnOK" class="ButtonControl" style="display: none; width: 50px" name="Ok"
                                    type="button" value="OK" onclick="okclick()" runat="server" />--%>
                                    <asp:Button ID="btnOK" runat="server" Text="OK" class="ButtonControl" Style="display: none;
                                        width: 50px"  OnClick="btnOK_Click" />
                                    <input id="btnClose" class="ButtonControl" style="display: none; width: 50px" name="Close"
                                        type="button" value="Close"  runat="server"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="DivEmpDetails">
                        <table style="width: 100%;">
                            <tr>
                                <td style="background-color: #EFF8FE; padding: 0px;" colspan="3">
                                    <asp:UpdatePanel ID="upnGrid" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvEmpMgrAdd" runat="server" AutoGenerateColumns="false" Width="100%"
                                                AllowPaging="true" PageSize="5" GridLines="None">
                                                <RowStyle CssClass="gvRow" />
                                                <HeaderStyle CssClass="gvHeader" />
                                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                                <PagerStyle CssClass="gvPager" />
                                                <EmptyDataTemplate>
                                                    <div>
                                                        <span>No records found.</span>
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="EOD_EMPID" HeaderText="Employee ID"></asp:BoundField>
                                                    <asp:BoundField DataField="Empname" HeaderText="Employee Name"></asp:BoundField>
                                                    <%--<asp:BoundField DataField="EOD_DIVISION_ID" HeaderText="Division"></asp:BoundField>
                                                    --%>
                                                    <asp:BoundField DataField="EOD_JOINING_DATE" HeaderText="Joining Date"></asp:BoundField>
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
                                                <SortedAscendingHeaderStyle ForeColor="White" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnOK" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </td>
            <td style="width: 30%; height: 100%; font-family: Arial; vertical-align: top; display: none;"
                rowspan="4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Calendar ID="Calendar1" runat="server" BackColor="#404040" DayStyle-HorizontalAlign="Right"
                            SelectedDayStyle-Font-Size="Larger" SelectionMode="Day" BorderColor="Black" BorderWidth="2px"
                            Font-Names="Arial" Font-Size="10pt" ForeColor="white" Height="330px" NextPrevFormat="FullMonth"
                            Width="100%" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged"
                            ViewStateMode="Enabled" OnLoad="Calendar1_Load" OnVisibleMonthChanged="Calendar1_VisibleMonthChanged">
                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" VerticalAlign="Middle" />
                            <SelectedDayStyle BackColor="#FFFFCC" ForeColor="BlueViolet" Font-Size="Smaller" />
                            <TitleStyle Font-Bold="True" Font-Size="20pt" ForeColor="White" BorderWidth="1px"
                                CssClass="calMainMonthd" />
                            <TodayDayStyle BackColor="#47afda" ForeColor="White" />
                            <TodayDayStyle BackColor="#2C3539" ForeColor="White" />
                            <DayStyle BackColor="#D0D0D0" Font-Size="9pt" BorderColor="#47a3da" ForeColor="Black"
                                HorizontalAlign="Right" Wrap="true" BorderWidth="3px" />
                        </asp:Calendar>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Calendar1" />
                        <asp:AsyncPostBackTrigger ControlID="btnOK" />
						  <asp:AsyncPostBackTrigger ControlID="btnClose" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td style="width: 10px;">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
                <table border="0" style="display: none;">
                    <tr>
                        <td>
                            <asp:Button ID="btnNewReq" runat="server" Text="Selected Dates" OnClick="btnNewReq_Click" />
                            <%--   <asp:Label ID="Label1" runat="server" Text="Selected Dates :-"></asp:Label>--%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFrmDate" runat="server" Enabled="false" Width="100px" Height="30px"></asp:TextBox>
                            <asp:TextBox ID="txtToDate" runat="server" Enabled="false" Width="100px" Height="30px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 15px;">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="1">
                <table border="0" width="100%">
                    <tr>
                        <td align="center" style="border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                            border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                            font-weight: bold;">
                            <asp:Label ID="lblCorrectionType" runat="server" Text="Correction Type :-"></asp:Label>
                        </td>
                        <td style="border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                            border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                            font-weight: bold;">
                            <asp:RadioButtonList ID="rblCorrectionType" runat="server" CssClass="radiobutton"
                                AutoPostBack="false" RepeatDirection="Vertical" onchange="return ValidateShift();">
                                <asp:ListItem Value="1">One Day</asp:ListItem>
                                <asp:ListItem Value="2">Date Range</asp:ListItem>
                                <asp:ListItem Value="3">Onwards</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                            border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                            font-weight: bold;">
                            <asp:Label ID="Label5" runat="server" Text="Shift Type:"></asp:Label>
                        </td>
                        <td style="border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                            border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                            font-weight: bold;">
                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="ComboControl"
                                TabIndex="4" ClientIDMode="Static" RepeatDirection="Vertical">
                                <asp:ListItem Value="Pattern" onclick="FillPattern()">Pattern</asp:ListItem>
                                <asp:ListItem Value="Fixed" Selected="True" onclick="FillFixed()">Fixed</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvrblScheduleType" runat="server" ControlToValidate="rblScheduleType"
                                ErrorMessage="Plz. select shift type." ForeColor="Red" Style="font-family: Verdana;
                                font-size: 9pt;" Font-Size="Medium" ValidationGroup="ADD" Display="None"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerblScheduleType" runat="server" TargetControlID="rfvrblScheduleType"
                                PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td align="center" style="border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                            border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                            font-weight: bold; display: none; width: 10%;">
                            <asp:Label ID="Label1" runat="server" Text="Select Weekly Off :"></asp:Label><br />
                            <asp:Label ID="Label2" runat="server" Text="Select WeekEnd Off :"></asp:Label>
                        </td>
                        <td style="border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                            border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                            font-weight: bold; display: none;">
                            <asp:DropDownList ID="ddlWeeklyOffview" runat="server" CssClass="ComboControl">
                            </asp:DropDownList>
                            <br />
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ComboControl">
                            </asp:DropDownList>
                            <asp:RadioButtonList ID="rblScheduleType" runat="server" CssClass="ComboControl"
                                Style="display: none;" TabIndex="4" ClientIDMode="Static" RepeatDirection="Vertical">
                                <asp:ListItem Value="Fixed" Selected="True" onclick="FillFixed()">Fixed</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="center" style="border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                            border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                            font-weight: bold; width: 10%; text-align: left;">
                            <asp:Label ID="Label3" runat="server" Text="From Date:"></asp:Label><br />
                            <asp:Label ID="Label4" runat="server" Text="To Date:"></asp:Label>
                        </td>
                        <td style="border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                            border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                            font-weight: bold; margin-left: 3px;">
                              <asp:TextBox ID="txtFromDateSC" runat="server" Width="125px" onkeyPress="javascript: return false;ValidateDate();"
                                onkeyUp="javascript: return false;ValidateDate();" onkeyDown="javascript: return false;ValidateDate();"
                                onchange="ValidateDate();"></asp:TextBox><br />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                PopupButtonID="txtFromDateSC" TargetControlID="txtFromDateSC">
                            </ajaxToolkit:CalendarExtender>
                          <asp:TextBox ID="txtToDateSC" runat="server" Width="125px" onkeyPress="javascript: return false"
                                onkeyUp="javascript: return false;ValidateDate();" onkeyDown="javascript: return false;ValidateDate();"></asp:TextBox>
                            <asp:HiddenField ID="hdnFromDate" runat="server" />
                            <asp:HiddenField ID="hdnToDate" runat="server" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                PopupButtonID="txtToDateSC" TargetControlID="txtToDateSC">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="ComboControl"
                                Style="display: none;" TabIndex="4" ClientIDMode="Static" RepeatDirection="Vertical">
                                <%--     <asp:ListItem Value="Pattern" onclick="FillPattern()">Pattern</asp:ListItem>--%>
                                <asp:ListItem Value="Fixed" Selected="True" onclick="FillFixed()">Fixed</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="center" style="border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                            border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                            font-weight: bold;">
                            <asp:Label ID="lblShiftType" runat="server" Text="Select Shift :"></asp:Label>
                        </td>
                        <td style="width: 176px; border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                            border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                            font-weight: bold;">
                            <asp:Panel ID="pnlshifttype" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlShiftPattern" runat="server" CssClass="ComboControl">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="ComboControl">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">
                <asp:Button ID="btnSave" Text="Save" CssClass="ButtonControl" runat="server"  OnClick="OnSave_Click"></asp:Button>
                <asp:Button ID="btnReset" runat="server" CssClass="ButtonControl" Text="Reset" TabIndex="8"
                    OnClick="btnReset_Click" OnClientClick="return PageRefresh();" />
                <asp:Button ID="btnCancel" Text="Cancel" CssClass="ButtonControl" runat="server"
                    OnClientClick="PageRefresh();" OnClick="btnCancel_Click" Visible="false"></asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:UpdatePanel ID="upnlError" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblErrorMsg" runat="server" Visible="true" CssClass="ErrorLabel" Text=""></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <div align="center">
    </div>
</asp:Content>
