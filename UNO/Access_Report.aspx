<%@ Page Language="C#" MasterPageFile="~/ModuleMain.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="Access_Report.aspx.cs" Inherits="MMWebReports.Access_Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <link rel='stylesheet' href='./calendar.css' title='calendar'>
    <script language="javascript" src="calendar.js"></script>
    <script type="text/javascript" language="javascript">




        //***** Function formating the Date for inputbox *****

        function fnSetDateFormat(oDateFormat) {
            oDateFormat['FullYear']; 	//Example = 2007
            oDateFormat['Year']; 		//Example = 07
            oDateFormat['FullMonthName']; //Example = January
            oDateFormat['MonthName']; 	//Example = Jan
            oDateFormat['Month']; 		//Example = 01
            oDateFormat['Date']; 		//Example = 01
            oDateFormat['FullDay']; 		//Example = Sunday
            oDateFormat['Day']; 			//Example = Sun
            oDateFormat['Hours']; 		//Example = 01
            oDateFormat['Minutes']; 		//Example = 01
            oDateFormat['Seconds']; 		//Example = 01

            var sDateString;

            //Example = 01/01/00  dd/mm/yy
            //sDateString = oDateFormat['Date'] +"/"+ oDateFormat['Month'] +"/"+ oDateFormat['Year'];		

            //Example = 01/01/0000  dd/mm/yyyy
            //Commented by
            sDateString = oDateFormat['Date'] + "/" + oDateFormat['Month'] + "/" + oDateFormat['FullYear'];

            //Example = 0000-01-01 yyyy/mm/dd
            //sDateString = oDateFormat['FullYear'] +"-"+ oDateFormat['Month'] +"-"+ oDateFormat['Date'];

            //Example = Jan-01-0000 Mmm/dd/yyyy
            //sDateString = oDateFormat['MonthName'] +"-"+ oDateFormat['Date'] +"-"+ oDateFormat['FullYear'];

            // sDateString = oDateFormat['Month'] + "/" + oDateFormat['Date'] + "/" + oDateFormat['FullYear'];

            return sDateString;
        }

        function fnCharAlphaNumeric(evnt) {

            var charCode = (evnt.which) ? evnt.which : event.keyCode
            if ((charCode >= 97 && charCode <= 122) || (charCode >= 65 && charCode <= 90) || (charCode >= 48 && charCode <= 57) ||

			 (charCode == 8)) {
                return true
            }
            else {
                return false
            }
        }
        function fnDisplayEmployeeName() {

            //Nandkishor
            var xEmp_code = document.getElementById('ctl00_PrescientAuthorContent_EmpCode').value;
            var list5 = document.getElementById('ctl00_PrescientAuthorContent_ListBox1');
            var blnEmployeeExists = false

            for (var i = 0; i < list5.options.length; i++) {
                if (list5.options[i].value == xEmp_code) {
                    document.getElementById('ctl00_PrescientAuthorContent_lblEmployeeName').innerHTML = list5.options[i].text.substring(0, 20);
                }
            }
            //Nandkishor
        }

        function ShowPrint() {
            document.getElementById('Print').style.display = "inline";
        }

        function HidePrint() {
            document.getElementById('Print').style.display = "none";
        }

        function FillDate(str) {
            if (str.value == "")
                str.value = document.getElementById('DateHdn').value;
        }

        function ResetHdn(str) {
            document.getElementById(str).value = "";

        }



        function DispText() {
            //  document.getElementById('ctl00_ProjectContent_DDO_pnl').style.display="inline";     
            document.getElementById('Panel1').style.display = "inline";
            document.getElementById('_ListBox1').style.display = "none";
            document.getElementById('ListBox2').style.display = "none";
            document.getElementById('ListBox3').style.display = "none";
            document.getElementById('ListBox4').style.display = "none";
            document.getElementById('ListBox5').style.display = "none";
            document.getElementById('ListBox6').style.display = "none";
            //document.getElementById('ListBox7').style.display="none";     

            common();
            document.getElementById('EmpCode').style.display = "inline";
            document.getElementById('EmpCode').value = "";
            document.getElementById('EmpCode').focus();
            document.getElementById('HeadLbl').innerHTML = "";
            //document.getElementById('LstTitle').innerHTML="&nbsp;Enter Employee Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            // document.getElementById('ctl00_ProjectContent_LstTitle').style.display="none";
            document.getElementById('LstTitle').innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Enter Employee Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp";
        }

        function show1() {

            document.getElementById('Panel1').style.display = "inline";
            document.getElementById('ListBox1').style.display = "inline";
            document.getElementById('ListBox2').style.display = "none";
            document.getElementById('ListBox3').style.display = "none";
            document.getElementById('ListBox4').style.display = "none";

            document.getElementById('ListBox5').style.display = "none";
            document.getElementById('ListBox6').style.display = "none";
            List1('ListBox1', 'EmployeeHdn');
            common();
            document.getElementById('HeadLbl').innerHTML = "Select Employees";
            document.getElementById('LstTitle').innerHTML = "&nbsp;Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Code";

        }
        function List1(str1, str2) {
            list = document.getElementById(str1);
            hdn = document.getElementById(str2).value;

            for (var i = 0; i < list.options.length; i++) {
                if (hdn.indexOf("'" + list.options[i].value + "'") != -1)
                    list.options[i].selected = true;
                else
                    list.options[i].selected = false;

            }

        }
        function show2() {
            document.getElementById('Panel1').style.display = "inline";
            document.getElementById('ListBox1').style.display = "none";
            document.getElementById('ListBox2').style.display = "inline";
            document.getElementById('ListBox3').style.display = "none";

            document.getElementById('ListBox4').style.display = "none";
            document.getElementById('ListBox5').style.display = "none";
            document.getElementById('ListBox6').style.display = "none";
            List1('ListBox2', 'ComapnyHdn');
            common();
            document.getElementById('HeadLbl').innerHTML = "Select Reader";
            document.getElementById('LstTitle').innerHTML = "&nbsp;Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Reader Code(Controller)";

        }
        function show3() {
            document.getElementById('Panel1').style.display = "inline";
            document.getElementById('ListBox1').style.display = "none";
            document.getElementById('ListBox2').style.display = "none";

            document.getElementById('ListBox3').style.display = "inline";
            document.getElementById('ListBox4').style.display = "none";
            document.getElementById('ListBox5').style.display = "none";
            document.getElementById('ListBox6').style.display = "none";
            common();
            List1('ListBox3', 'LocationHdn');
            document.getElementById('HeadLbl').innerHTML = "Select Reader";
            document.getElementById('LstTitle').innerHTML = "&nbsp;Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Code";

        }

        function show4() {
            document.getElementById('Panel1').style.display = "inline";
            document.getElementById('ListBox1').style.display = "none";
            document.getElementById('ListBox2').style.display = "none";

            document.getElementById('ListBox3').style.display = "none";
            document.getElementById('ListBox4').style.display = "inline";
            document.getElementById('ListBox5').style.display = "none";
            document.getElementById('ListBox6').style.display = "none";
            common();
            List1('ListBox4', 'DivisionHdn');
            document.getElementById('HeadLbl').innerHTML = "Select Divisions";
            document.getElementById('LstTitle').innerHTML = "&nbsp;Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Code";

        }

        function show5() {
            document.getElementById('Panel1').style.display = "inline";
            document.getElementById('ListBox1').style.display = "none";
            document.getElementById('ListBox2').style.display = "none";

            document.getElementById('ListBox3').style.display = "none";
            document.getElementById('ListBox4').style.display = "none";
            document.getElementById('ListBox5').style.display = "inline";
            document.getElementById('ListBox6').style.display = "none";
            common();
            List1('ListBox5', 'DepartmentHdn');
            document.getElementById('HeadLbl').innerHTML = "Select Departments";
            document.getElementById('LstTitle').innerHTML = "&nbsp;Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Code";

        }

        function show6() {
            document.getElementById('Panel1').style.display = "inline";
            document.getElementById('ListBox1').style.display = "none";
            document.getElementById('ListBox2').style.display = "none";

            document.getElementById('ListBox3').style.display = "none";
            document.getElementById('ListBox4').style.display = "none";
            document.getElementById('ListBox5').style.display = "none";
            document.getElementById('ListBox6').style.display = "inline";
            common();
            List1('ListBox6', 'ShiftHdn');
            document.getElementById('HeadLbl').innerHTML = "Select Shifts";
            document.getElementById('LstTitle').innerHTML = "Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Description";

        }


        function common() {
            //        toggleDisabled(document.getElementById("HeadPnl"));


            document.getElementById('RadioButtonList1_0').disabled = true;
            document.getElementById('RadioButtonList1_1').disabled = true;
            document.getElementById('RadioButtonList2_0').disabled = true;
            document.getElementById('RadioButtonList2_1').disabled = true;
            //   document.getElementById('RadioButtonList3_0').disabled=true;
            //   document.getElementById('RadioButtonList3_1').disabled=true;
            //  document.getElementById('RadioButtonList4_0').disabled=true;
            //  document.getElementById('RadioButtonList4_1').disabled=true;
            //  document.getElementById('RadioButtonList5_0').disabled=true;
            //  document.getElementById('RadioButtonList5_1').disabled=true;
            //  document.getElementById('RadioButtonList6_0').disabled=true;
            //  document.getElementById('RadioButtonList6_1').disabled=true;
            document.getElementById('From_Date').disabled = true;
            document.getElementById('View').disabled = true;
            document.getElementById('Button4').disabled = true;
            document.getElementById('Button5').disabled = true;


        }

        function EnableControls() {
            document.getElementById('RadioButtonList1_0').disabled = false;
            document.getElementById('RadioButtonList1_1').disabled = false;
            document.getElementById('RadioButtonList2_0').disabled = false;
            document.getElementById('RadioButtonList2_1').disabled = false;
            // document.getElementById('RadioButtonList3_0').disabled=false;
            //  document.getElementById('RadioButtonList3_1').disabled=false;
            //  document.getElementById('RadioButtonList4_0').disabled=false;
            //  document.getElementById('RadioButtonList4_1').disabled=false;
            //  document.getElementById('RadioButtonList5_0').disabled=false;
            //  document.getElementById('RadioButtonList5_1').disabled=false;
            //  document.getElementById('RadioButtonList6_0').disabled=false;
            //  document.getElementById('RadioButtonList6_1').disabled=false;
            document.getElementById('From_Date').disabled = false;
            document.getElementById('View').disabled = false;
            document.getElementById('Button4').disabled = false;
            document.getElementById('Button5').disabled = false;
            document.getElementById('Panel1').style.display = "none";

            document.getElementById('HeadLbl').innerHTML = "";
        }

        function DisableControls() {
            document.getElementById('RadioButtonList2_0').disabled = true;
            document.getElementById('RadioButtonList2_1').disabled = true;
            //   document.getElementById('RadioButtonList3_0').disabled=true;
            //  document.getElementById('RadioButtonList3_1').disabled=true;
            // document.getElementById('RadioButtonList4_0').disabled=true;
            // document.getElementById('RadioButtonList4_1').disabled=true;
            // document.getElementById('RadioButtonList5_0').disabled=true;
            // document.getElementById('RadioButtonList5_1').disabled=true;
            document.getElementById('RadioButtonList2_0').checked = true;
            //  document.getElementById('RadioButtonList3_0').checked=true; 
            //  document.getElementById('RadioButtonList4_0').checked=true; 
            // document.getElementById('RadioButtonList5_0').checked=true; 
            // document.getElementById('RadioButtonList6_0').checked=true; 
            document.getElementById('RadioButtonList1_0').disabled = false;
            document.getElementById('RadioButtonList1_1').disabled = false;
            // document.getElementById('RadioButtonList6_0').disabled=true; 
            // document.getElementById('RadioButtonList6_1').disabled=true;
        }

        function EnableRestControls() {
            document.getElementById('RadioButtonList1_0').disabled = false;
            document.getElementById('RadioButtonList1_1').disabled = false;
            document.getElementById('RadioButtonList2_0').disabled = false;
            document.getElementById('RadioButtonList2_1').disabled = false;
            //  document.getElementById('RadioButtonList3_0').disabled=false;
            //  document.getElementById('RadioButtonList3_1').disabled=false;
            // document.getElementById('RadioButtonList4_0').disabled=false;
            // document.getElementById('RadioButtonList4_1').disabled=false;
            // document.getElementById('RadioButtonList5_0').disabled=false;
            // document.getElementById('RadioButtonList5_1').disabled=false;
            //document.getElementById('RadioButtonList6_0').disabled=false;
            // document.getElementById('RadioButtonList6_1').disabled=false;
        }

        function OkClick() {
            var first = "";
            var count = 0;
            if (document.getElementById('ListBox1').style.display == "inline") {
                var list1 = document.getElementById('ListBox1');

                for (var i = 0; i < list1.options.length; i++) {
                    if (list1.options[i].selected) {
                        if (first == "")
                            first = first + "('" + list1.options[i].value + "'";
                        else
                            first = first + ",'" + list1.options[i].value + "'";
                        count = count + 1;
                    }
                }
                if (count > 1000) {
                    alert("Can not select more than 1000 Employees\n          Total Selected Records :" + count);
                    return false;
                }
                if (first == "")
                    alert("select atleast one item");
                else {
                    EnableControls();
                    document.getElementById('EmployeeHdn').value = first + ")";
                    DisableControls();
                }
            }

            if (document.getElementById('ListBox2').style.display == "inline") {
                var list2 = document.getElementById('ListBox2');

                for (var i = 0; i < list2.options.length; i++) {
                    if (list2.options[i].selected) {
                        if (first == "")
                            first = first + "('" + list2.options[i].value + "'";
                        else
                            first = first + ",'" + list2.options[i].value + "'";
                    }
                }
                if (first == "")
                    alert("select atleast one item");
                else {
                    EnableControls();
                    document.getElementById('ComapnyHdn').value = first + ")";
                }
            }

            if (document.getElementById('ListBox3').style.display == "inline") {
                var list3 = document.getElementById('ListBox3');

                for (var i = 0; i < list3.options.length; i++) {
                    if (list3.options[i].selected) {
                        if (first == "")
                            first = first + "('" + list3.options[i].value + "'";
                        else
                            first = first + ",'" + list3.options[i].value + "'";
                    }
                }
                if (first == "")
                    alert("select atleast one item");
                else {
                    EnableControls();
                    document.getElementById('LocationHdn').value = first + ")";
                }
            }


            if (document.getElementById('ListBox4').style.display == "inline") {
                var list4 = document.getElementById('ListBox4');

                for (var i = 0; i < list4.options.length; i++) {
                    if (list4.options[i].selected) {
                        if (first == "")
                            first = first + "('" + list4.options[i].value + "'";
                        else
                            first = first + ",'" + list4.options[i].value + "'";
                    }
                }
                if (first == "")
                    alert("select atleast one item");
                else {
                    EnableControls();
                    document.getElementById('DivisionHdn').value = first + ")";
                }
            }


            if (document.getElementById('ListBox5').style.display == "inline") {
                var list5 = document.getElementById('ListBox5');

                for (var i = 0; i < list5.options.length; i++) {
                    if (list5.options[i].selected) {
                        if (first == "")
                            first = first + "('" + list5.options[i].value + "'";
                        else
                            first = first + ",'" + list5.options[i].value + "'";
                    }
                }
                if (first == "")
                    alert("select atleast one item");
                else {
                    EnableControls();
                    document.getElementById('DepartmentHdn').value = first + ")";
                }
            }


            if (document.getElementById('ListBox6').style.display == "inline") {
                var list6 = document.getElementById('ListBox6');

                for (var i = 0; i < list6.options.length; i++) {
                    if (list6.options[i].selected) {
                        if (first == "")
                            first = first + "('" + list6.options[i].value + "'";
                        else
                            first = first + ",'" + list6.options[i].value + "'";
                    }
                }
                if (first == "")
                    alert("select atleast one item");
                else {
                    EnableControls();
                    if (document.getElementById('RadioButtonList1_1').checked == true)
                        DisableControls();
                    document.getElementById('ShiftHdn').value = first + ")";
                }
            }

            if (document.getElementById('EmpCode').style.display == "inline") {
                if (document.getElementById('EmpCode').value == "") {
                    alert("Please enter Employee Code");
                    document.getElementById('EmpCode').focus();
                }
                else {
                    EnableControls();
                    DisableControls();
                }
            }



        }

        function CancelClick() {
            // if(document.getElementById('ListBox1').style.display=="inline") 
            document.getElementById('LstTitle').innerHTML = "";

            if (document.getElementById('RadioButtonList1_1').checked == true) {
                if (document.getElementById('EmployeeHdn').value != "") {
                    DisableControls();
                    document.getElementById('Panel1').style.display = "none";
                    document.getElementById('HeadLbl').innerHTML = "";
                    document.getElementById('From_Date').disabled = false;
                    document.getElementById('View').disabled = false;
                    document.getElementById('Button4').disabled = false;
                    document.getElementById('Button5').disabled = false;
                    //  if(document.getElementById('ShiftHdn').value=="")
                    //document.getElementById('RadioButtonList6_0').checked=true;
                    return;
                }
            }
            EnableControls();
            document.getElementById('Panel1').style.display = "none";
            if (document.getElementById('EmployeeHdn').value == "")
                document.getElementById('RadioButtonList1_0').checked = true;

            if (document.getElementById('ComapnyHdn').value == "")
                document.getElementById('RadioButtonList2_0').checked = true;

            // if(document.getElementById('LocationHdn').value=="")
            // document.getElementById('RadioButtonList3_0').checked=true;

            //  if(document.getElementById('DivisionHdn').value=="")
            //  document.getElementById('RadioButtonList4_0').checked=true;

            //  if(document.getElementById('DepartmentHdn').value=="")
            // document.getElementById('RadioButtonList5_0').checked=true;

            // if(document.getElementById('ShiftHdn').value=="")
            // document.getElementById('RadioButtonList6_0').checked=true;

            document.getElementById('HeadLbl').innerHTML = "";



        }
        
        
        
        
  
    
    </script>
    <script type="text/javascript" language="javascript">

        function OnShow(action) {
            if (ValidateData()) {
                var formName = document.aspnetForm;
                //var sBody = getRequestBody(formName, "SHOW");
                var sBody = "ACTION=SHOW";
                var url = "Access_Report.aspx?" + sBody;
                formName.action = url;
                formName.submit();
            }
        }
        function OnPrint() {
            msg = confirm('Do you really want to Print this Report? ');
            if (msg == false)
                return;
            var formName = document.aspnetForm;
            var sBody = getRequestBody(formName, "PRINT");
            var url = "Access_Report.aspx?" + sBody;
            formName.action = url;
            formName.submit();
        }
        function HandleAction(formAction, recomputeParentControlName) {
            var formName = document.aspnetForm;
            var sBody = getRequestBody(formName, formAction);

            if (formAction == 'SHOW') {
                if (!ValidateData())
                    return;
            }
            httpRequest = getXMLHTTPRequest();
            httpRequest.onreadystatechange = function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var doc;
                        doc = httpRequest.responseXML;
                        if (doc) {
                            if (formAction.search(/RECOMPUTE/) != -1) {
                            }
                            else {
                                updateReport(httpRequest, 'reportDiv');
                                if (!getMessageString(doc, 'messageDiv', 'messageDiv'))
                                    resetAllControls();
                            }
                        }
                    }
                }
            }
            httpRequest.open('POST', 'Access_Report.aspx', true);
            httpRequest.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            httpRequest.send(sBody);
        }

        function ValidationReport() {
            if (document.getElementById('From_Date').value == "") {
                alert("Please enter From Date.");
                document.getElementById('From_Date').focus();
                return false
            }


            if (document.getElementById('To_Date').value == "") {
                alert("Please enter To Date.");
                document.getElementById('To_Date').focus();
                return false
            }

            if (!CompareDates(document.getElementById('From_Date').value, document.getElementById('To_Date').value)) {
                alert("Success");
                alert("From Date must be less than or equals to To Date");
                document.getElementById('To_Date').focus();
                return
            }


        }

        function ValidateData() {
            var formName = document.aspnetForm;
            //			if(formName.Employee_Code_editable.value == "None")
            //			{
            //				alert("Please select Employee Code");
            //				formName.Employee_Code_editable.focus();
            //				return false;
            //			}
            if (!(validateString(formName.From_Date, "Please enter Date"))) {
                return false
            }
            if (!(isDate(formName.From_Date.value))) {
                formName.From_Date.select();
                return
            }
            if (!CompareDates(formName.From_Date, formName.DateHdn)) {
                alert("Post dated Reports can not be viewed");
                formName.From_Date.select();
                return
            }
            if (formName.TimeCheckBox.checked) {
                if (!(validateString(formName.From_Time, "Please enter From Time"))) {
                    return false
                }
                if (!(IsValidTime(formName.From_Time))) {
                    formName.From_Time.select();
                    return false
                }
                if (!(validateString(formName.To_Time, "Please enter To Time"))) {
                    return false
                }
                if (!(IsValidTime(formName.To_Time))) {
                    formName.To_Time.select();
                    return false
                }

                if (!CompareDates(formName.ctl00_PrescientAuthorContent_From_Date, formName.ctl00_PrescientAuthorContent_To_Date)) {
                    alert("From Date must be less than or equals to To Date");
                    formName.ctl00_PrescientAuthorContent_From_Date.focus();
                    return
                }

                if (!TimeCompare(formName.From_Time, formName.To_Time, "From Time should not be less than To Time")) {
                    formName.From_Time.select();
                    return false
                }

            }
            return true;
        }

        function CompareDates(d1, d2) {
            var start = d1.value.toUpperCase();
            var end = d2.value.toUpperCase();
            var arrDate = start.split('/');
            var arrDate1 = end.split('/');
            var d1 = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
            var d2 = new Date(arrDate1[2], arrDate1[1] - 1, arrDate1[0]);
            if (d1 > d2) {
                //alert("End date must be lessthan startdate");
                return false;
            }
            return true;
        }

        function ShowTime(str) {
            var formName = document.aspnetForm;
            if (str.checked) {
                document.getElementById('Time_Panel').style.visibility = "visible";
                document.getElementById('From_Time').focus();
            }
            else {
                document.getElementById('Time_Panel').style.visibility = "hidden";
                document.getElementById('From_Time').value = "";
                document.getElementById('To_Time').value = "";
            }

        }

        function fnCloseReport() {
            // alert("Hii");

            // window.location("BaseDataDashboard.aspx");
            location.href("AccessDashboard.aspx");
        }
		
		
    </script>
    <table cellspacing="0" class="MenuTable">
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <h1 class="heading" style='text-align: center; font-family: tahoma; font-size: x-large;'>
        Access Report</h1>
    <asp:Panel runat="server" ID="HeadPanel" ClientIDMode="Static" Width="943px">
        <table class="TableClass" cellspacing="1" align="center" style="margin-left:12%;">
            <%--<tr   >
   <!-- <asp:Panel runat="server" ID="HeadPnl" > -->
    <td style=" height: 75px; width: 15%; font-weight: bold; color: black; clip: rect(auto 2px auto auto); border-left-color: lightsteelblue; border-bottom-color: lightsteelblue; border-top-style: solid; border-top-color: lightsteelblue; border-right-style: solid; border-left-style: solid; border-right-color: lightsteelblue; border-bottom-style: solid; vertical-align: middle;" align="left" >
     
    
                
        </td>
   <td style=" height: 75px; width: 15%; font-weight: bold; color: black; clip: rect(auto 2px auto auto); border-left-color: lightsteelblue; border-bottom-color: lightsteelblue; border-top-style: solid; border-top-color: lightsteelblue; border-right-style: solid; border-left-style: solid; border-right-color: lightsteelblue; border-bottom-style: solid; vertical-align: middle;" align="left" >
                
                </td>
                
    <td style=" height: 75px; width: 15%; font-weight: bold; color: black; clip: rect(auto 2px auto auto); border-left-color: lightsteelblue; border-bottom-color: lightsteelblue; border-top-style: solid; border-top-color: lightsteelblue; border-right-style: solid; border-left-style: solid; border-right-color: lightsteelblue; border-bottom-style: solid; vertical-align: middle;" align="left">
                
                </td>
          <td align="left" style="font-weight: bold; border-left-color: lightsteelblue; border-bottom-color: lightsteelblue;
              vertical-align: middle; width: 15%; clip: rect(auto 2px auto auto); color: black;
              border-top-style: solid; border-top-color: lightsteelblue; border-right-style: solid;
              border-left-style: solid; height: 75px; border-right-color: lightsteelblue; border-bottom-style: solid">
          </td>
          <td align="left" style="font-weight: bold; border-left-color: lightsteelblue; border-bottom-color: lightsteelblue;
              vertical-align: middle; width: 15%; clip: rect(auto 2px auto auto); color: black;
              border-top-style: solid; border-top-color: lightsteelblue; border-right-style: solid;
              border-left-style: solid; height: 75px; border-right-color: lightsteelblue; border-bottom-style: solid">
          </td>
    <td style="; height: 75px; width: 15%; font-weight: bold; color: black; clip: rect(auto 2px auto auto); border-left-color: lightsteelblue; border-bottom-color: lightsteelblue; border-top-style: solid; border-top-color: lightsteelblue; border-right-style: solid; border-left-style: solid; border-right-color: lightsteelblue; border-bottom-style: solid; vertical-align: middle;" >                        
                </td>
     
   <td align="center"  valign="middle" style=" height: 75px;width: 10%; font-weight: bold; color: black; clip: rect(auto 2px auto auto); border-left-color: lightsteelblue; border-bottom-color: lightsteelblue; border-top-style: solid; border-top-color: lightsteelblue; border-right-style: solid; border-left-style: solid; border-right-color: lightsteelblue; border-bottom-style: solid; vertical-align: middle;"  >
           
            
    </td>       
                 <!--   </asp:Panel> -->
  </tr>--%>
            <tr>
                <td colspan="7" class="TDClassForButton" style="height: 12px;">
                    <asp:Panel runat="server" ID="HeadPnl" Width="625px">
                        <%--<table class="TableClass" style="width: 45%; height: 75px"> --%>
                        <table class="TableClass" style="width: 40%; height: 75px">
                            <tr>
                                <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;"
                                    class="style26">
                                    <table style="height: 75px; width: 102px;">
                                        <tr>
                                            <td class="style29" style="text-align:center;">
                                                &nbsp;Employee
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center" class="style29">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow" CssClass="radiobutton"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                                    <asp:ListItem Value="1" onclick="show1()" style = "padding-left:17px;">Select</asp:ListItem>
                                                    <%--<asp:ListItem Value="2" >Enter Code</asp:ListItem> --%>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="EmployeeHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;"
                                    class="style30">
                                    <table style="height: 75px;width:100px;">
                                        <tr>
                                            <td style="text-align:center;">
                                                Reader&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ComapnyHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1" onclick="show2()"   style = "padding-left:17px;">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ComapnyHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;"
                                    class="style31">
                                    <table style="height: 70px;">
                                        <tr>
                                            <td style="text-align:center;">
                                                &nbsp;Access
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:CheckBox ID="chkGrantAccess" runat="server" Text=" Grant" Width="114px" Height="16px" />
                                                <br />
                                                <asp:CheckBox ID="chkDeniedAccess" runat="server" Text=" Denied" Width="114px"  style = "padding-left:8px;"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <%--  NEW CALENDER--%>
                                    <table>
                                        <tr>
                                            <td colspan="2" style="height: 26px; text-align: left">
                                                &nbsp; Date
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="text" name='From_Date' id='txtCalendarFrom' maxlength="10" readonly="readonly" />
                                            </td>
                                            <td>
                                                <img src='images/calendar.gif' onmouseover="fnInitCalendar(this, 'txtCalendarFrom', 'style=calendar.css,close=true')">
                                                </img>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="text" name='To_Date' id='txtCalendarTo' maxlength="10" readonly="readonly" />
                                            </td>
                                            <td>
                                                <img src='images/calendar.gif' onmouseover="fnInitCalendar(this, 'txtCalendarTo', 'style=calendar.css,close=true')">
                                                </img>
                                            </td>
                                        </tr>
                                    </table>
                                    <%--  NEW CALENDER--%>
                                </td>
                                <td style="display: none; border: thin solid lightsteelblue; text-align: left; font-weight: bold;"
                                    class="style25">
                                    <table id="table4" runat="server" style="text-align: right; vertical-align: top">
                                        <tr>
                                            <td colspan="2" style="height: 26px; text-align: left">
                                                <asp:CheckBox ID="TimeCheckBox" runat="server" onclick="ShowTime(this)" Text="Check for  Timings"
                                                    Width="125px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Time_Panel" Style="visibility: hidden" runat="server">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 65px; height: 25px">
                                                                From :
                                                            </td>
                                                            <td align="left" style="height: 25px">
                                                                <asp:TextBox ID="From_Time" runat="server" onkeyup="fnColon(this,event)" onkeypress="findspace(event)"
                                                                    Width="50px" MaxLength="5" Height="15px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 65px; height: 22px">
                                                                To :
                                                            </td>
                                                            <td align="left" style="height: 22px">
                                                                <asp:TextBox ID="To_Time" runat="server" onkeyup="fnColon(this,event)" onkeypress="findspace(event)"
                                                                    Width="50px" MaxLength="5" Height="15px">00:00</asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="text-align: center; border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                                    border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                                    font-weight: bold;">
                                    <table style="height: 50px;width:120px;">
                                        <tr>
                                            <td>
                                                <asp:Button ID="View" class="ButtonControl" Style="width: 80%" runat="server" Text="View"
                                                    ClientIDMode="Static" OnClientClick='return ValidationReport()' OnClick="View_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button4" runat="server" Width="80%" CssClass="ButtonControl" OnClick="Button4_Click"
                                                    Text="Reset" ClientIDMode="Static" />
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td>
                                                <%-- <asp:Button ID="Button5" runat="server" CssClass="ButtonControl" Text="Close" OnClick="Button5_Click" />--%>
                                        <%--        <input type="Button" class="ButtonControl" style="width: 80%" value="Close" id="Button5"
                                                    onclick='fnCloseReport()' />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>--%>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    &nbsp; &nbsp;&nbsp;
                    <%--	<input type ="Button" class= "ButtonControl" value= "Print" id="Print" onclick='OnPrint("PRINT")' />
                    --%>
                </td>
            </tr>
            <div runat="server" id="d1">
            </div>
            <tr>
                <td align="center" colspan="7" style="vertical-align: middle">
                    <asp:Panel ID="Panel1" runat="server" ClientIDMode="Static" Style="display: none">
                        <table style="width: 400px">
                            <tr>
                                <td>
                                    <asp:Label ID="HeadLbl" runat="server" Text="" Font-Bold="True" Font-Size="Medium"
                                        ClientIDMode="Static"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="LstTitle" runat="server" BackColor="AliceBlue" BorderStyle="Double"
                                        ClientIDMode="Static" Font-Bold="True" Font-Names="Courier New" ForeColor="Black"
                                        Style="text-align: left; font-weight: bold; font-family: 'Courier New', Monospace;"
                                        Width="98%"></asp:Label>
                                    <asp:ListBox ID="ListBox1" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox2" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox3" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox4" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox5" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox6" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 37px; vertical-align: bottom;">
                                    <input id="Button1" class="button" name="Ok" style="width: 71px" type="button" value="OK"
                                        onclick="OkClick()" />
                                </td>
                                <td align="center" style="height: 37px; vertical-align: bottom; width: 170px;">
                                    <input id="Button2" class="button" name="Cancel" style="width: 71px" type="button"
                                        value="Cancel" onclick="CancelClick()" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table align="center" style="width: 71%; margin-left: 39px;">
        <tr>
            <td colspan="5" align="right" style="height: 44px">
                <%--  <asp:Label ID="HeadLbl" runat="server" Font-Bold="True" Font-Size="Medium"  ClientIDMode="Static"></asp:Label> --%>
                <asp:HiddenField ID="DateHdn" runat="server" ClientIDMode="Static" />
                &nbsp;<asp:TextBox ID="EmpCode" MaxLength="8" onkeypress="return fnCharAlphaNumeric(event)"
                    onblur="fnDisplayEmployeeName()" runat="server" Visible="False"></asp:TextBox>
                <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="ButtonControl" OnClick="Close_Click"
                    Visible="False" />
            </td>
        </tr>
        <tr>
            <td style="text-align: right" class="style28">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="738px" Font-Names="Verdana"
                    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                    WaitMessageFont-Size="14pt" BorderStyle="Solid" Visible="False">
                </rsweb:ReportViewer>
                <input type="button" class="ButtonControl" style="width: 120px; visibility: hidden;"
                    value="Export To Excel" id="Excel_Export_Button" onclick='OnExcelExportDownLoad("EXCELDOWNLOAD")' />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Close" Text="Close" Visible="false" runat="server" CssClass="ButtonControl"
                    OnClientClick="return CloseFun()" />
                &nbsp;&nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style28">
                &nbsp; &nbsp; &nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <table align="center">
        <tr>
            <%--	<td></td>--%>
            <%--<td></td>--%>
            <td align="left" class="style27">
                <asp:HyperLink ID="Back_to_Module_Dashboard" CssClass="Back_LogoutLink" runat="server"
                    NavigateUrl="../../BaseModule/UI/BaseModule_Dashboard.aspx" Visible="False">Back to TK Web Reports Dashboard</asp:HyperLink>
            </td>
        </tr>
    </table>
    <table class="MessageContainerTable" width="98.8%">
        <tr>
            <td colspan="4" align="center" valign="middle">
                <asp:Label ID="MessageLabel" runat="server" CssClass="ErrMessageStyle"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style25
        {
            height: 75px;
            width: 5%;
        }
        .style26
        {
            height: 75px;
            width: 9%;
        }
        .TableClass
        {
            height: 259px;
            width: 33%;
        }
        .style27
        {
            width: 40%;
        }
        .style28
        {
            width: 300px;
        }
        .style29
        {
            width: 117px;
        }
        .style30
        {
            height: 75px;
            width: 134px;
        }
        #txtCalendarFrom
        {
            width: 108px;
        }
        #txtCalendarTo
        {
            width: 108px;
        }
        .style31
        {
            width: 2%;
        }
    </style>
</asp:Content>
