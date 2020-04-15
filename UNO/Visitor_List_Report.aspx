<%@ Page Language="C#" MasterPageFile="~/ModuleMain.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="Visitor_List_Report.aspx.cs" Inherits="UNO.Visitor_List_Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" src="Scripts/calendar.js" type="text/javascript"></script>
     <!--In Progress UI CSS -->
    <link href="ProgressBar/CSS/container.css" rel="stylesheet" type="text/css" />
    <!--In Progress UI Dependencies -->
    <script src="ProgressBar/Scripts/utilities.js" type="text/javascript"></script>
    <script src="ProgressBar/Scripts/container-min.js" type="text/javascript"></script>
    <script src="ProgressBar/Scripts/InProgress.js" type="text/javascript"></script>
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function RptVisitorSpecificDetails(strVal, strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSection, strGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, DEmployee, DCompany, DLocation, DDivision, DDepartment, DCategory, DSection, DGrade, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, Panel1, txtCalenderform, btnView, btnReset, HeadLbl, txtReader, ddlPersonalType,stVisitorCompany,lstVisitorCompany,txtVisitorCompany,RBDVisCompany) {


            var strcom, strloc, strdiv, strdept, strcat, Shift, strSec, strGr, strchecked = "";
            var lBox, DummylBox;
            var myData = [];

            strcom = document.getElementById(strcomany).value;
            strloc = document.getElementById(strlocation).value;
            strdiv = document.getElementById(strdivision).value;
            strdept = document.getElementById(strdepartment).value;
            if (strShift != "")
                Shift = document.getElementById(strShift).value;
            strcat = "";
            if (strSection != "")
                strSec = document.getElementById(strSection).value;
            else
                strSec = "";
            if (strGrade != "")
                strGr = document.getElementById(strGrade).value;
            else
                strGr = "";

            strchecked = strVal;
            if (strcom == '')
                strcom = "'N'";
            if (strloc == '')
                strloc = "'N'";
            if (strdiv == '')
                strdiv = "'N'";
            if (strdept == '')
                strdept = "'N'";
            if (strcat == '')
                strcat = "'N'";
            if (Shift == '')
                Shift = "'N'";
            if (strSec == '')
                strSec = "'N'";
            if (strGr == '')
                strGr = "'N'";


            if (ddlPersonalType != "") {

                if (document.getElementById(strEmployee).value != "" || document.getElementById(strcomany).value != "" || document.getElementById(strlocation).value != "" || document.getElementById(strdivision).value != "" || document.getElementById(strdepartment).value != "") {
                    document.getElementById(ddlPersonalType).disabled = true;
                }

                if (strSec != "") {
                    if (document.getElementById(strSection).value != "")
                        document.getElementById(ddlPersonalType).disabled = true;

                }


                if (strGrade != "") {
                    if (document.getElementById(strGrade).value != "")
                        document.getElementById(ddlPersonalType).disabled = true;

                }
                if (strShift != "") {
                    if (document.getElementById(strShift).value != "")
                        document.getElementById(ddlPersonalType).disabled = true;
                }
            }
            var EmpType;
            if (ddlPersonalType != "") {
                EmpType = document.getElementById(ddlPersonalType).value;
            }
            else
                EmpType = '\'\'';

            //Employee
            if (strchecked == 'EMP') {

                if ($('#' + RbdEmp + ' input:checked').val() == '1') {
                    
                    if (document.getElementById('<%=rdbVisitorEmployee.ClientID%>').value == "1")
                        document.getElementById(HeadLbl).innerHTML = "Select Employees";
                    else
                        document.getElementById(HeadLbl).innerHTML = "Select Visitors";

                    if (RbdSection != "" && RbdGrade != "") {
                        $('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                    }
                    else {
                        $('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                    }


                    VRptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, lstVisitorCompany, txtVisitorCompany);

                    document.getElementById(LstEmployee).style.display = "inline";
                    document.getElementById(txtSearchEmp).style.display = "inline";
                    document.getElementById(btnOK).style.display = "inline";
                    document.getElementById(btnCancel).style.display = "inline";
                    document.getElementById(Panel1).style.display = "inline";
                    document.getElementById(HeadLbl).style.display = "inline";
                    document.getElementById(txtSearchEmp).value = "";
                    document.getElementById(btnView).disabled = true;
                    document.getElementById(btnReset).disabled = true;
                    $('#' + LstEmployee).empty();
                    var list = document.getElementById(LstEmployee);
                    var list1 = document.getElementById(DEmployee);
                    var j = 0;
                    for (i = 0; i <= list1.options.length - 1; i++) {
                        list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                        j++;
                    }

                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    document.getElementById(btnView).disabled = false;
                    document.getElementById(btnReset).disabled = false;
                    return true;
                }
                return true;
            }
            document.getElementById(btnOK).style.display = "inline";
            document.getElementById(btnCancel).style.display = "inline";
            document.getElementById(Panel1).style.display = "inline";
            document.getElementById(btnView).disabled = true;
            document.getElementById(btnReset).disabled = true;
            document.getElementById(HeadLbl).style.display = "inline";



            //added by vaibhav
            //Visitor Company
            if (strchecked == 'VISCOM') {

           
            if ($('#' + RBDVisCompany + ' input:checked').val() == '1') {
                    document.getElementById(HeadLbl).innerHTML = "Select Visitor Company";

                    if (RbdSection != "") {
                        $('#' + [RbdEmp, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdCompany].join(', #')).find('input').prop('disabled', false);
                    }
                    else {
                        $('#' + [RbdEmp, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdCompany].join(', #')).find('input').prop('disabled', false);
                    }

                    VRptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, lstVisitorCompany, txtVisitorCompany);
                    document.getElementById(lstVisitorCompany).style.display = "inline";
                    document.getElementById(txtVisitorCompany).style.display = "inline";
                    document.getElementById(txtVisitorCompany).value = "";

                    //alert(lstVisitorCompany.lenghth)
//                    lBox = $('#' + lstVisitorCompany);
//                    DummylBox = $('#' + DCompany);
//                    $('#' + lstVisitorCompany).empty();
//                    $('#' + DCompany).empty();
                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    document.getElementById(btnView).disabled = false;
                    document.getElementById(btnReset).disabled = false;
                    return true;
                }
            }



            //Company
            if (strchecked == 'COM') {
                if ($('#' + RbdCompany + ' input:checked').val() == '1') {
                    document.getElementById(HeadLbl).innerHTML = "Select Centres";

                    if (RbdSection != "") {
                        $('#' + [RbdEmp, RBDVisCompany, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
                    }
                    else {
                        $('#' + [RbdEmp, RBDVisCompany, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
                    }

                    VRptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, lstVisitorCompany, txtVisitorCompany);
                    document.getElementById(LstCompany).style.display = "inline";
                    document.getElementById(txtCompany).style.display = "inline";
                    document.getElementById(txtCompany).value = "";
                    lBox = $('#' + LstCompany);
                    DummylBox = $('#' + DCompany);
                    $('#' + LstCompany).empty();
                    $('#' + DCompany).empty();
                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    document.getElementById(btnView).disabled = false;
                    document.getElementById(btnReset).disabled = false;
                    return true;
                }
            }
            //Location
            if (strchecked == 'LOC') {
                if ($('#' + RbdLocation + ' input:checked').val() == '1') {
                    document.getElementById(HeadLbl).innerHTML = "Select Units";
                    if (RbdSection != "") {
                        $('#' + [RbdEmp, RBDVisCompany, RbdCompany, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', false);
                    }
                    else {
                        $('#' + [RbdEmp, RBDVisCompany, RbdCompany, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', false);
                    }



                    VRptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, lstVisitorCompany, txtVisitorCompany);
                    document.getElementById(LstLocation).style.display = "inline";
                    document.getElementById(txtDivision).style.display = "inline";
                    document.getElementById(txtDivision).value = "";
                    lBox = $('#' + LstLocation);
                    DummylBox = $('#' + DLocation);
                    $("#" + LstLocation).empty();
                    $("#" + DLocation).empty();
                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    document.getElementById(btnView).disabled = false;
                    document.getElementById(btnReset).disabled = false;
                    return true;
                }
            }
            //Division
            else if (strchecked == 'DIV') {
                if ($('#' + RbdDivision + ' input:checked').val() == '1') {
                    document.getElementById(HeadLbl).innerHTML = "Select Entities";

                    if (RbdSection != "") {
                        $('#' + [RbdEmp, RBDVisCompany, RbdCompany, RbdLocation, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', false);
                    }
                    else {
                        $('#' + [RbdEmp, RBDVisCompany, RbdCompany, RbdLocation, RbdShift].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', false);
                    }

                    VRptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, lstVisitorCompany, txtVisitorCompany);
                    document.getElementById(LstDivision).style.display = "inline";
                    document.getElementById(txtLocation).style.display = "inline";
                    document.getElementById(txtLocation).value = "";
                    lBox = $('#' + LstDivision);
                    DummylBox = $('#' + DDivision);
                    $("#" + LstDivision).empty();
                    $("#" + DDivision).empty();
                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    document.getElementById(btnView).disabled = false;
                    document.getElementById(btnReset).disabled = false;
                    return true;
                }
            }
            //Department
            else if (strchecked == 'DEP') {
                if ($('#' + RbdDepartment + ' input:checked').val() == '1') {

                    document.getElementById(HeadLbl).innerHTML = "Select Groups";
                    if (RbdSection != "") {
                        $('#' + [RbdEmp, RBDVisCompany, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdShift, RbdGrade].join(', #')).find('input').prop('disabled', false);
                    }
                    else {
                        $('#' + [RbdEmp, RBDVisCompany, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdShift].join(', #')).find('input').prop('disabled', false);
                    }

                    VRptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, lstVisitorCompany, txtVisitorCompany);
                    document.getElementById(LstDepartment).style.display = "inline";
                    document.getElementById(txtDepartment).style.display = "inline";
                    document.getElementById(txtDepartment).value = "";
                    lBox = $('#' + LstDepartment);
                    DummylBox = $('#' + DDepartment);
                    $("#" + LstDepartment).empty();
                    $("#" + DDepartment).empty();
                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    document.getElementById(btnView).disabled = false;
                    document.getElementById(btnReset).disabled = false;
                    return true;
                }
            }
            //Shift
            else if (strchecked == 'SFT') {
                if ($('#' + RbdShift + ' input:checked').val() == '1') {
                    document.getElementById(HeadLbl).innerHTML = "Select Shifts";

                    if (RbdSection != "") {
                        $('#' + [RbdEmp, RBDVisCompany, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdGrade, RbdSection].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdSection].join(', #')).find('input').prop('disabled', false);
                    }
                    else {
                        $('#' + [RbdEmp, RBDVisCompany, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                    }


                    VRptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, lstVisitorCompany, txtVisitorCompany);
                    document.getElementById(LstCategory).style.display = "inline";
                    document.getElementById(txtShift).style.display = "inline";
                    document.getElementById(txtShift).value = "";
                    lBox = $('#' + LstCategory);
                    DummylBox = $('#' + DCategory);
                    $("#" + LstCategory).empty();
                    $("#" + DCategory).empty();
                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    document.getElementById(btnView).disabled = false;
                    document.getElementById(btnReset).disabled = false;
                    return true;
                }

            }


            //Grade
            else if (strchecked == 'GRD') {
                if ($('#' + RbdGrade + ' input:checked').val() == '1') {
                    document.getElementById(HeadLbl).innerHTML = "Select Divisions";

                    if (RbdSection != "" && RbdGrade != "") {
                        $('#' + [RbdEmp, RBDVisCompany, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdSection].join(', #')).find('input').prop('disabled', false);
                    }
                    else {
                        $('#' + [RbdEmp, RBDVisCompany, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                    }


                    VRptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, lstVisitorCompany, txtVisitorCompany);
                    document.getElementById(LstGrade).style.display = "inline";
                    document.getElementById(txtGrade).style.display = "inline";
                    document.getElementById(txtGrade).value = "";
                    lBox = $('#' + LstGrade);
                    DummylBox = $('#' + DGrade);
                    $("#" + LstGrade).empty();
                    $("#" + DGrade).empty();
                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    document.getElementById(btnView).disabled = false;
                    document.getElementById(btnReset).disabled = false;
                    return true;
                }
            }

            //Section(group)
            else if (strchecked == 'GRP') {
                if ($('#' + RbdSection + ' input:checked').val() == '1') {
                    document.getElementById(HeadLbl).innerHTML = "Select Sections";

                    if (RbdSection != "") {
                        $('#' + [RbdEmp, RBDVisCompany, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdGrade].join(', #')).find('input').prop('disabled', true);


                    }
                    else {
                        $('#' + [RbdEmp, RBDVisCompany, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                    }


                    VRptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, lstVisitorCompany, txtVisitorCompany);
                    document.getElementById(LstSection).style.display = "inline";
                    document.getElementById(txtSection).style.display = "inline";
                    document.getElementById(txtSection).value = "";
                    lBox = $('#' + LstSection);
                    DummylBox = $('#' + DSection);
                    $("#" + LstSection).empty();
                    $("#" + DSection).empty();
                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    document.getElementById(btnView).disabled = false;
                    document.getElementById(btnReset).disabled = false;
                    return true;
                }
            }


            $.ajax({
                url: "Filter/Filter.asmx/FillEntitySpecificDetails",
                type: "POST",
                dataType: "json",
                timeout: 10000,
                data: "{'strcom':'" + escape(strcom) + "','strloc':'" + escape(strloc) + "','strdiv':'" + escape(strdiv) + "','strdept':'" + escape(strdept) + "','strcat':'" + escape(strcat) + "','strchecked':'" + escape(strchecked) + "','strshift':'" + escape(Shift) + "','strSec':'" + escape(strSec) + "','strGRD':'" + escape(strGr) + "','EmpType':'" + escape(EmpType) + "'}",

                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                    myData = $.parseJSON(msg.d);
                    if (myData.length > 0 && myData.length != null) {
                        var listItems = [];
                        for (var i = 0; i < myData.length; i++) {
                            listItems.push('<option value="' + myData[i].ID + '">' + myData[i].Value + '</option>');
                        }
                    }
                    $(lBox).append(listItems.join(''));
                    $(DummylBox).append(listItems.join(''));
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                    alert("Error...\n" + textStatus + "\n" + errorThrown);
                }
            });
        }



        function ResetHdn(str) {
            document.getElementById(str).value = "";

        }

        function Reset() {


            var yyyy = new Date().getFullYear().toString();
            var mm = (new Date().getMonth() + 1).toString();
            var dd = new Date().getDate().toString();
            var Month = (mm[1] ? mm : "0" + mm[0]);
            var ToDate = (dd[1] ? dd : "0" + dd[0]);

            $('#' + ["<%=txtFromDate.ClientID%>"].join(', #')).prop('value', "01" + "/" + Month + "/" + yyyy);
            $('#' + ["<%=txtToDate.ClientID%>"].join(', #')).prop('value', ToDate + "/" + Month + "/" + yyyy);
            $("select" + "#" + "<%=ddlPersonnelType.ClientID%>").prop('selectedIndex', 0);
            $('#' + ['<%=ddlPersonnelType.ClientID%>'].join(', #')).prop('disabled', false);
            $('#' + ['<%=EmployeeHdn.ClientID%>', '<%=VisitorCompanyHdn.ClientID%>', '<%=ComapnyHdn.ClientID%>', '<%=ShiftHdn.ClientID%>', '<%=GradeHdn.ClientID%>', '<%=GroupHdn.ClientID%>', '<%=LocationHdn.ClientID%>', '<%=DivisionHdn.ClientID%>', '<%=DepartmentHdn.ClientID%>'].join(', #')).prop('value', "");
            $('#' + ['rdbEmployee_0', 'rdbVisitorCompny_0', 'rdbComapny_0', 'rdbLocation_0', 'rdbDivision_0', 'rdbDepartment_0', 'rdbShift_0', 'rdbtnGroup_0', 'rdbtnGrade_0'].join(', #')).prop('checked', true);
            $('#' + ['rdbEmployee', 'rdbVisitorCompny', 'rdbComapny', 'rdbLocation', 'rdbDivision', 'rdbDepartment', 'rdbShift', 'rdbtnGroup', 'rdbtnGrade'].join(', #')).find('input').prop('disabled', false);
            return false;
        }

        function ValidationReport() {

          
            if (document.getElementById('<%=txtFromDate.ClientID%>').value == "" || document.getElementById('<%=txtFromDate.ClientID%>').value.contains('Select')) {
                alert("Please enter From Date.");
                document.getElementById('<%=txtFromDate.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value == "" || document.getElementById('<%=txtToDate.ClientID%>').value.contains('Select')) {
                alert("Please enter To Date.");
                document.getElementById('<%=txtToDate.ClientID%>').focus();
                return false;
            }
            var strFrom = document.getElementById('<%=txtFromDate.ClientID%>').value;
            var strTo = document.getElementById('<%=txtToDate.ClientID%>').value;
            if (CompareDates(strTo, strFrom) == true) {

                alert("From Date must be less than or equals to To Date");
                document.getElementById('<%=txtToDate.ClientID%>').focus();
                return false;
            }

            InProgressLoading(true);
            return true;

        }

        function VRptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, lstVisitorCompany, txtVisitorCompany) {
            if (txtSection != "") {
                document.getElementById(txtSection).style.display = "none";
                document.getElementById(LstSection).style.display = "none";
            }
            if (txtGrade != "") {
                document.getElementById(txtGrade).style.display = "none";
                document.getElementById(LstGrade).style.display = "none";
            }

            if (txtReader != "") {
                document.getElementById(txtReader).style.display = "none";
            }

            document.getElementById(txtSearchEmp).style.display = "none";
            document.getElementById(txtCompany).style.display = "none";
            document.getElementById(txtDivision).style.display = "none";
            document.getElementById(txtLocation).style.display = "none";
            document.getElementById(txtDepartment).style.display = "none";
            document.getElementById(txtShift).style.display = "none";
            document.getElementById(LstEmployee).style.display = "none";
            document.getElementById(LstCompany).style.display = "none";
            document.getElementById(LstLocation).style.display = "none";
            document.getElementById(LstDivision).style.display = "none";
            document.getElementById(LstDepartment).style.display = "none";
            document.getElementById(LstCategory).style.display = "none";

            document.getElementById(lstVisitorCompany).style.display = "none";
            document.getElementById(txtVisitorCompany).style.display = "none";



        }
        function CompareDates(d1, d2) {
            var start = d1.toUpperCase();
            var end = d2.toUpperCase();
            var arrDate = start.split('/');
            var arrDate1 = end.split('/');
            var d1 = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
            var d2 = new Date(arrDate1[2], arrDate1[1] - 1, arrDate1[0]);
            if (d1 > d2) {
                return false;
            }
            return true;
        }

        function SwitchVisitorEmployee(LstEmployee, lstDEmployee, rdbVisitorEmployee) {

            var lBox, LDummy, SelVal;
            var myData = [];
            lBox = $('#' + LstEmployee);
            LDummy = $('#' + lstDEmployee);
            $('#' + LstEmployee).empty();
            $('#' + lstDEmployee).empty();


            if ($('#' + rdbVisitorEmployee + ' input:checked').val() == '1') {
                document.getElementById('<%=lblEmployee.ClientID%>').innerHTML = "Employee";
                $("#TDCompany").hide();
                SelVal = 'EMP';
            }
            else {
                document.getElementById('<%=lblEmployee.ClientID%>').innerHTML = "Visitor";
                //vaibhav
             
                $("#TDCompany").show();
                
   
                SelVal = 'VIS';
                $('#' + ['<%=rdbComapny.ClientID%>', '<%=rdbLocation.ClientID%>', '<%=rdbDivision.ClientID%>', '<%=rdbDepartment.ClientID%>', '<%=rdbShift.ClientID%>', '<%=rdbtnGroup.ClientID%>', '<%=rdbtnGrade.ClientID%>'].join(', #')).find('input').prop('disabled', true);
            }
            //var strEmpType = '\'\'';
            var strEmpType = 'E';

            $.ajax({
                url: "Filter/Filter.asmx/FillEmployeeVisitorDetails",
                type: "POST",
                dataType: "json",
                timeout: 10000,
                data: "{'strCommandType':'" + escape(SelVal) + "','PersonnelType':'" + escape(strEmpType) + "'}",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                    myData = $.parseJSON(msg.d);
                    if (myData.length > 0 && myData.length != null) {
                        var listItems = [];
                        for (var i = 0; i < myData.length; i++) {
                            listItems.push('<option value="' + myData[i].ID + '">' + myData[i].Value + '</option>');
                        }
                    }
                    $(lBox).append(listItems.join(''));
                    $(LDummy).append(listItems.join(''));

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                    alert("Error...\n" + textStatus + "\n" + errorThrown);
                }
            });
        }
     
		
    </script>
    <script language="javascript" type="text/javascript">

        function VRptCloseClick(strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSec, strGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstShift, LstSection, LstGrade, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, Panel1, txtCalendarFrom, btnView, btnReset, VisitorCompanyHdn, lstVisitorCoampany, txtSerchVisitorCompany, rdbVisitorCompny) {
        
            if (document.getElementById(strEmployee).value == "") {
                document.getElementById(LstEmployee).style.display = "none";
                document.getElementById(RbdEmp + '_0').checked = true;
            }

            if (document.getElementById(strcomany).value == "") {
                document.getElementById(LstCompany).style.display = "none";
                document.getElementById(RbdCompany + '_0').checked = true;
            }

            if (document.getElementById(strlocation).value == "") {
                document.getElementById(LstLocation).style.display = "none";
                document.getElementById(RbdLocation + '_0').checked = true;
            }

            if (document.getElementById(strdivision).value == "") {
                document.getElementById(LstDivision).style.display = "none";
                document.getElementById(RbdDivision + '_0').checked = true;
            }

            if (document.getElementById(strdepartment).value == "") {
                document.getElementById(LstDepartment).style.display = "none";
                document.getElementById(RbdDepartment + '_0').checked = true;
            }

            if (document.getElementById(strShift).value == "") {
                document.getElementById(LstShift).style.display = "none";
                document.getElementById(RbdShift + '_0').checked = true;
            }

            if (document.getElementById(VisitorCompanyHdn).value == "") {
                document.getElementById(lstVisitorCoampany).style.display = "none";
                document.getElementById(rdbVisitorCompny + '_0').checked = true;
            }

            if (strSec != "") {
                $('#' + [txtSearchEmp, txtCompany, txtLocation, txtDivision, txtDepartment, txtShift, txtSection].join(', #')).find('input').prop('value', "");
                if (document.getElementById(strSec).value == "") {
                    document.getElementById(LstSection).style.display = "none";
                    document.getElementById(RbdSection + '_0').checked = true;
                }

            }
            else {
                $('#' + [txtSearchEmp, txtCompany, txtLocation, txtDivision, txtDepartment, txtShift].join(', #')).find('input').prop('value', "");
            }

            if (strGrade != "") {

                if (document.getElementById(strGrade).value == "") {
                    document.getElementById(LstGrade).style.display = "none";
                    document.getElementById(RbdGrade + '_0').checked = true;
                }

            }



            document.getElementById(txtCalendarFrom).disabled = false;
            document.getElementById(btnOK).style.display = "none";
            document.getElementById(btnCancel).style.display = "none";
            document.getElementById(Panel1).style.display = "none";
            document.getElementById(btnView).disabled = false;
            document.getElementById(btnReset).disabled = false;
        }






        //Reports Okclick
      
        function VRptOkClick(strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSec, strGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstShift, LstSection, LstGrade, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, Panel1, txtCalendarFrom, btnView, btnReset, VisitorCompanyHdn, lstVisitorCoampany, txtSerchVisitorCompany) {
            //debugger;
 
            var first = "";
            var count = 0;
            var list1;
            var HiddenVal;
            var entity;
            var strSearchBox;

            if (document.getElementById(LstEmployee).style.display == "inline") {

                //alert("hello from RptOkClick");
                list1 = document.getElementById(LstEmployee);
                HiddenVal = strEmployee;
                entity = "Employees";
                strSearchBox = txtSearchEmp;
            }

            if (document.getElementById(LstCompany).style.display == "inline") {
                list1 = document.getElementById(LstCompany);
                HiddenVal = strcomany;
                entity = "centres";
                strSearchBox = strcomany;
            }

            if (document.getElementById(LstLocation).style.display == "inline") {
                list1 = document.getElementById(LstLocation);
                HiddenVal = strlocation;
                entity = "Units";
                strSearchBox = txtLocation;
            }

            if (document.getElementById(LstDivision).style.display == "inline") {
                list1 = document.getElementById(LstDivision);
                HiddenVal = strdivision;
                entity = "Entities";
                strSearchBox = txtDivision;
            }

            if (document.getElementById(LstDepartment).style.display == "inline") {
                list1 = document.getElementById(LstDepartment);
                HiddenVal = strdepartment;
                entity = "Groups";
                strSearchBox = txtDepartment;
            }

            if (document.getElementById(LstShift).style.display == "inline") {
                list1 = document.getElementById(LstShift);
                HiddenVal = strShift;
                entity = "Shifts";
                strSearchBox = txtShift;
            }
            if (LstSection != "") {

                if (document.getElementById(LstSection).style.display == "inline") {
                    list1 = document.getElementById(LstSection);
                    HiddenVal = strSec;
                    entity = "Sections";
                    strSearchBox = txtSection;
                }
            }

            if (LstGrade != "") {

                if (document.getElementById(LstGrade).style.display == "inline") {
                    list1 = document.getElementById(LstGrade);
                    HiddenVal = strGrade;
                    entity = "Divisions";
                    strSearchBox = txtGrade;
                }
            }
            //added by vaibhav 
            if (lstVisitorCoampany != "") {

                if (document.getElementById(lstVisitorCoampany).style.display == "inline") {
                    list1 = document.getElementById(lstVisitorCoampany);
                    HiddenVal = VisitorCompanyHdn;
                    entity = "VisitorCompany";
                    strSearchBox = txtSerchVisitorCompany;
                }
            }

            ///
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
                alert("Can not select more than 1000" + entity + "\n  Total Selected Records :" + count);
                return false;
            }
            if (first == "") {
                alert("select atleast one item");
                return false;
            }
            else
                document.getElementById(HiddenVal).value = first + ")";

            ////
            document.getElementById(txtCalendarFrom).disabled = false;
            document.getElementById(btnView).disabled = false;
            document.getElementById(btnReset).disabled = false;
            list1.style.display = "none";
            document.getElementById(btnOK).style.display = "none";
            document.getElementById(btnCancel).style.display = "none";
            document.getElementById(strSearchBox).style.display = "none";
            document.getElementById(Panel1).style.display = "none";
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
        Visitor List</h1>
    <asp:Panel runat="server" ID="HeadPanel" ClientIDMode="Static" Width="100%">
        <div style="height: 8px;">
        </div>
        <div style="width: 100%; color: Black; font-weight: bold;" align="center">
            <asp:RadioButtonList ID="rdbVisitorEmployee" runat="server" RepeatDirection="Horizontal"
                CssClass="chkOptionalRadio" Height="22px" Width="209px">
                <asp:ListItem Value="0">By Visitor </asp:ListItem>
                <asp:ListItem Value="1" Selected="True">By Employee </asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <table class="TableClass" cellspacing="1" align="center" width="100%">
            <tr>
                <td style="padding-left: 3%; color: Black; display: none;">
                    Personnel Type:
                    <asp:DropDownList ID="ddlPersonnelType" runat="server" AutoPostBack="true">
                        <asp:ListItem Value="E" Text="Employee" />
                        <asp:ListItem Value="NE" Text="Non Employee" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="7" class="TDClassForButton" style="height: 12px">
                    <asp:Panel runat="server" ID="HeadPnl">
                        <table class="TableClass" style="width: 100%; height: 75px; padding-left: 3%;">
                            <tr>

                                <td class="tdStyle">
                                    <table style="height: 75px; width: 100px;">
                                        <tr>
                                            <td class="style29">
                                                <asp:Label ID="lblEmployee" runat="server" Text="Employee"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left" class="style29">
                                                <asp:RadioButtonList ID="rdbEmployee" runat="server" RepeatLayout="Flow" CssClass="radiobutton"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="EmployeeHdn" runat="server" ClientIDMode="Static" />
                                </td>

                                <td class="tdStyle" id="TDCompany" style="display:none;">
                                    <table style="height: 75px; width: 100px;">
                                        <tr>
                                            <td class="style29">
                                                <asp:Label ID="Label1" runat="server" Text="Visitor Company"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left" class="style29">
                                                <asp:RadioButtonList ID="rdbVisitorCompny" runat="server" RepeatLayout="Flow" CssClass="radiobutton"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="VisitorCompanyHdn" runat="server" ClientIDMode="Static" />
                                </td>

                                <td class="tdStyle">
                                    <table style="height: 75px; width: 100px;">
                                        <tr>
                                            <td>
                                                Centre&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbComapny" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ComapnyHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ComapnyHdn" runat="server" ClientIDMode="Static" />
                                </td>

                                <td class="tdStyle">
                                    <table style="height: 75px; width: 105px;">
                                        <tr>
                                            <td>
                                                &nbsp;Unit
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbLocation" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('LocationHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="LocationHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 75px;">
                                        <tr>
                                            <td>
                                                &nbsp;Entity
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbDivision" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('DivisionHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="DivisionHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 75px;" id="TABLE2">
                                        <tr>
                                            <td>
                                                &nbsp;Group
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbDepartment" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('DepartmentHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="DepartmentHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="display: none" class="tdStyle">
                                    <table style="height: 75px;" id="TABLE4">
                                        <tr>
                                            <td>
                                                &nbsp;Shift
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbShift" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ShiftHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ShiftHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 75px;" id="TABLE1">
                                        <tr>
                                            <td>
                                                &nbsp;Division
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbtnGrade" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('GradeHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="GradeHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 75px;" id="TABLE3">
                                        <tr>
                                            <td>
                                                &nbsp;Section
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbtnGroup" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('GroupHdn')">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="GroupHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="tdStyle">
                                    <table>
                                        <tr>
                                            <td style="text-align: left">
                                                From Date
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtFromDate" onkeyPress="javascript: return false" runat="server"
                                                    ClientIDMode="Static" Height="18px" Width="111px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtfromDate_CalendarExtender1" TargetControlID="txtFromDate"
                                                    PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                To Date
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtToDate" runat="server" onkeyPress="javascript: return false"
                                                    Height="16px" Width="112px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtToDate"
                                                    PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="tdStyle">
                                    <table style="height: 50px; width: 100px">
                                        <tr>
                                            <td>
                                                <asp:Button ID="View" class="ButtonControl" Style="width: 80%" runat="server" Text="View"
                                                    ClientIDMode="Static" OnClick="View_Click" OnClientClick="return ValidationReport();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button4" runat="server" Width="80%" CssClass="ButtonControl" OnClientClick="return Reset();"
                                                    Text="Reset" ClientIDMode="Static" onclick="Button4_Click"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="Button" class="ButtonControl" style="width: 80%" value="Close" id="Button5"
                                                    onclick='navigateToUrl("Uno_Dashboard.aspx");' />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    &nbsp; &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" colspan="7" style="vertical-align: middle">
                    <asp:Panel ID="Panel1" runat="server" ClientIDMode="Static" Style="display: none">
                        <table style="width: 400px">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="HeadLbl" runat="server" Text="" Font-Bold="True" Font-Size="Medium"
                                        ClientIDMode="Static"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtEmployeeSearch" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                         
                                    <asp:TextBox ID="txtSerchVisitorCompany" runat="server" placeholder="Search" Width="100%"></asp:TextBox>

                                    <asp:TextBox ID="txtCompany" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtLocation" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtDivision" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtDepartment" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtShift" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtGrade" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtGroup" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="LstTitle" runat="server" BackColor="AliceBlue" BorderStyle="Double"
                                        ClientIDMode="Static" Font-Bold="True" Font-Names="Courier New" ForeColor="Black"
                                        Style="text-align: left; font-weight: bold; font-family: 'Courier New', Monospace;"
                                        Width="100%"></asp:Label>

                                    <asp:ListBox ID="lstEmployee" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="100%"></asp:ListBox>

                                      <asp:ListBox ID="lstVisitorCoampany" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Width="100%"></asp:ListBox>

                                    <asp:ListBox ID="lstCompany" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstLocation" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstDivision" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstDepartment" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstShift" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    &nbsp;
                                    <asp:ListBox ID="lstGrade" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstGroup" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>

                                    <asp:ListBox ID="lstEmployeeDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>


                                 <asp:ListBox ID="lstVisitorCoampanyDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>


                                    <asp:ListBox ID="lstCompanyDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>

                                    <asp:ListBox ID="lstLocationDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstDivisionDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstDepartmentDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox12" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstGradeDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstGroupDummy" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 37px; vertical-align: bottom;">
                                </td>
                                <td align="center" style="height: 37px; vertical-align: bottom; width: 170px;">
                                    <input id="btnOK" class="ButtonControl" name="Ok" style="width: 71px" type="button"
                                        value="OK" runat="server" />
                                    &nbsp;&nbsp;
                                    <input id="btnCancel" class="ButtonControl" name="Cancel" style="width: 71px" type="button"
                                        value="Cancel" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table align="center" style="width: 90%; margin-left: 39px;">
        <tr>
            <td colspan="5" align="right" style="height: 30px">
                <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="ButtonControl" Visible="False"
                    Width="90px" Height="23px" OnClientClick="navigateToUrl('Visitor_List_Report.aspx');return false;" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="viewer" runat="server" Height="100%" BorderStyle="Solid" BorderColor="#0066FF"
                    BorderWidth="3px" Width="100%" Visible="false">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="380px"
                        Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                        WaitMessageFont-Size="14pt" BorderStyle="None">
                    </rsweb:ReportViewer>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .tdStyle
        {
            border-right: lightsteelblue thin solid;
            border-top: lightsteelblue thin solid;
            border-left: lightsteelblue thin solid;
            border-bottom: lightsteelblue thin solid;
            width: 10%;
            font-weight: bold;
            color: Black;
        }
        .chkOptionalRadio input[type="radio"]
        {
            margin-right: 4px;
        }
    </style>
</asp:Content>
