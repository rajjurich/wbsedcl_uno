<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Page Language="C#" MasterPageFile="~/ModuleMain.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="AccessLevelReport_SecondLevel.aspx.cs" Inherits="UNO.AccessLevelReport_SecondLevel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13)) { return false; }
        }
        document.onkeypress = stopRKey;
    </script>
    <script type="text/javascript" language="javascript">

        function ResetHdn(str) {
            document.getElementById(str).value = "";

        }

        function fnCloseReport() {
            navigateToUrl('Uno_Dashboard.aspx');
        }

        function AccessLevelEntityDetails(strVal, strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSection, strGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, DEmployee, DCompany, DLocation, DDivision, DDepartment, DCategory, DSection, DGrade, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, Panel1, txtCalenderform, btnView, btnReset, HeadLbl, txtReader, ddlPersonalType, LstReader, txtZone, LstZone, rbdReader, rbdZone, ddlLevel, ddlStatus) {


            var strcom, strloc, strdiv, strdept, strcat, Shift, strSec, strGr, strchecked = "";
            var lBox, DummylBox;
            var myData = [];
            var LoginUser = document.getElementById('hdnUser').value;

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
                    document.getElementById(ddlStatus).disabled = true;
                    document.getElementById(ddlLevel).disabled = true;
                }

                if (strSec != "") {
                    if (document.getElementById(strSection).value != "") {
                        document.getElementById(ddlPersonalType).disabled = true;
                        document.getElementById(ddlStatus).disabled = true;
                        document.getElementById(ddlLevel).disabled = true;
                    }

                }


                if (strGrade != "") {
                    if (document.getElementById(strGrade).value != "") {
                        document.getElementById(ddlPersonalType).disabled = true;
                        document.getElementById(ddlStatus).disabled = true;
                        document.getElementById(ddlLevel).disabled = true;
                    }

                }
                if (strShift != "") {
                    if (document.getElementById(strShift).value != "") {
                        document.getElementById(ddlPersonalType).disabled = true;
                        document.getElementById(ddlStatus).disabled = true;
                        document.getElementById(ddlLevel).disabled = true;
                    }
                }
            }

            //Employee
            if (strchecked == 'EMP') {

                if ($('#' + RbdEmp + ' input:checked').val() == '1') {

                    document.getElementById(HeadLbl).innerHTML = "Select Employees";
                    if (RbdSection != "" && RbdGrade != "") {
                        $('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                    }
                    else {
                        $('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                    }



                    RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, LstReader, txtZone, LstZone);

                    document.getElementById(LstEmployee).style.display = "inline";
                    document.getElementById(txtSearchEmp).style.display = "inline";
                    document.getElementById(btnOK).style.display = "inline";
                    document.getElementById(btnCancel).style.display = "inline";
                    document.getElementById(Panel1).style.display = "inline";
                    document.getElementById(HeadLbl).style.display = "inline";
                    document.getElementById(btnView).disabled = true;
                    document.getElementById(btnReset).disabled = true;
                    document.getElementById(txtSearchEmp).value = "";

                    if (LoginUser == 'admin') {
                        $('#' + LstEmployee).empty();
                        var list = document.getElementById(LstEmployee);
                        var list1 = document.getElementById(DEmployee);
                        var j = 0;
                        for (i = 0; i <= list1.options.length - 1; i++) {
                            list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                            j++;
                        }
                    }

                }
                else {
                    document.getElementById(Panel1).style.display = "none";
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

            //Company
            if (strchecked == 'COM') {
                if ($('#' + RbdCompany + ' input:checked').val() == '1') {
                    document.getElementById(HeadLbl).innerHTML = "Select Companies";

                    if (RbdSection != "") {
                        $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
                    }
                    else {
                        $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
                    }

                    RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, LstReader, txtZone, LstZone);
                    document.getElementById(LstCompany).style.display = "inline";
                    document.getElementById(txtCompany).style.display = "inline";
                    document.getElementById(txtCompany).value = "";
                    if (LoginUser == 'admin') {

                        lBox = $('#' + LstCompany);
                        DummylBox = $('#' + DCompany);
                        $('#' + LstCompany).empty();
                        $('#' + DCompany).empty();
                    }
                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    return true;
                }
            }
            //Location
            if (strchecked == 'LOC') {
                if ($('#' + RbdLocation + ' input:checked').val() == '1') {
                    document.getElementById(HeadLbl).innerHTML = "Select Locations";
                    if (RbdSection != "") {
                        $('#' + [RbdEmp, RbdCompany, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', false);
                    }
                    else {
                        $('#' + [RbdEmp, RbdCompany, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', false);
                    }



                    RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, LstReader, txtZone, LstZone);
                    document.getElementById(LstLocation).style.display = "inline";
                    document.getElementById(txtDivision).style.display = "inline";
                    document.getElementById(txtDivision).value = "";
                    if (LoginUser == 'admin') {
                                                                lBox = $('#' + LstLocation);
                                                                DummylBox = $('#' + DLocation);
                                                                $("#" + LstLocation).empty();
                                                                $("#" + DLocation).empty();
                    }
                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    return true;
                }
            }
            //Division
            else if (strchecked == 'DIV') {
                if ($('#' + RbdDivision + ' input:checked').val() == '1') {
                    document.getElementById(HeadLbl).innerHTML = "Select Divisions";

                    if (RbdSection != "") {
                        $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', false);
                    }
                    else {
                        $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdShift].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', false);
                    }

                    RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, LstReader, txtZone, LstZone);
                    document.getElementById(LstDivision).style.display = "inline";
                    document.getElementById(txtLocation).style.display = "inline";
                    document.getElementById(txtLocation).value = "";
                    if (LoginUser == 'admin') {
                                                                lBox = $('#' + LstDivision);
                                                                DummylBox = $('#' + DDivision);
                                                                $("#" + LstDivision).empty();
                                                                $("#" + DDivision).empty();
                    }
                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    return true;
                }
            }
            //Department
            else if (strchecked == 'DEP') {
                if ($('#' + RbdDepartment + ' input:checked').val() == '1') {

                    document.getElementById(HeadLbl).innerHTML = "Select Departments";
                    if (RbdSection != "") {
                        $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdShift, RbdGrade].join(', #')).find('input').prop('disabled', false);
                    }
                    else {
                        $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdShift].join(', #')).find('input').prop('disabled', false);
                    }

                    RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, LstReader, txtZone, LstZone);
                    document.getElementById(LstDepartment).style.display = "inline";
                    document.getElementById(txtDepartment).style.display = "inline";
                    document.getElementById(txtDepartment).value = "";
                    if (LoginUser == 'admin') {
                                                                lBox = $('#' + LstDepartment);
                                                                DummylBox = $('#' + DDepartment);
                                                                $("#" + LstDepartment).empty();
                                                                $("#" + DDepartment).empty();
                    }
                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    return true;
                }
            }
            //Shift
            else if (strchecked == 'SFT') {
                if ($('#' + RbdShift + ' input:checked').val() == '1') {
                    document.getElementById(HeadLbl).innerHTML = "Select Shifts";

                    if (RbdSection != "") {
                        $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdGrade, RbdSection].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdSection].join(', #')).find('input').prop('disabled', false);
                    }
                    else {
                        $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                    }


                    RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, LstReader, txtZone, LstZone);
                    document.getElementById(LstCategory).style.display = "inline";
                    document.getElementById(txtShift).style.display = "inline";
                    document.getElementById(txtShift).value = "";
                    if (LoginUser == 'admin') {
                        lBox = $('#' + LstCategory);
                        DummylBox = $('#' + DCategory);
                        $("#" + LstCategory).empty();
                        $("#" + DCategory).empty();
                    }
                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    return true;
                }

            }


            //Grade
            else if (strchecked == 'GRD') {
                if ($('#' + RbdGrade + ' input:checked').val() == '1') {
                    document.getElementById(HeadLbl).innerHTML = "Select Grades";

                    if (RbdSection != "" && RbdGrade != "") {
                        $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                        $('#' + [RbdSection].join(', #')).find('input').prop('disabled', false);
                    }
                    else {
                        $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                    }


                    RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, LstReader, txtZone, LstZone);
                    document.getElementById(LstGrade).style.display = "inline";
                    document.getElementById(txtGrade).style.display = "inline";
                    document.getElementById(txtGrade).value = "";
                    if (LoginUser == 'admin') {
                        lBox = $('#' + LstGrade);
                        DummylBox = $('#' + DGrade);
                        $("#" + LstGrade).empty();
                        $("#" + DGrade).empty();
                    }
                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    return true;
                }
            }

            //Section(group)
            else if (strchecked == 'GRP') {
                if ($('#' + RbdSection + ' input:checked').val() == '1') {
                    document.getElementById(HeadLbl).innerHTML = "Select Groups";

                    if (RbdSection != "") {
                        $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdGrade].join(', #')).find('input').prop('disabled', true);


                    }
                    else {
                        $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                    }


                    RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, LstReader, txtZone, LstZone);
                    document.getElementById(LstSection).style.display = "inline";
                    document.getElementById(txtSection).style.display = "inline";
                    document.getElementById(txtSection).value = "";
                    if (LoginUser == 'admin') {
                        lBox = $('#' + LstSection);
                        DummylBox = $('#' + DSection);
                        $("#" + LstSection).empty();
                        $("#" + DSection).empty();
                    }
                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    return true;
                }
            }

            //Reader

            if (strchecked == 'RDR') {

                if ($('#' + rbdReader + ' input:checked').val() == '1') {

                    document.getElementById(HeadLbl).innerHTML = "Select Readers";

                    RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, LstReader, txtZone, LstZone);

                    document.getElementById(LstReader).style.display = "inline";
                    document.getElementById(txtReader).style.display = "inline";
                    document.getElementById(btnOK).style.display = "inline";
                    document.getElementById(btnCancel).style.display = "inline";
                    document.getElementById(Panel1).style.display = "inline";
                    document.getElementById(HeadLbl).style.display = "inline";
                    document.getElementById(btnView).disabled = true;
                    document.getElementById(btnReset).disabled = true;

                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    return true;
                }
                return true;
            }

            //Zone

            //Employee
            if (strchecked == 'ZNE') {

                if ($('#' + rbdZone + ' input:checked').val() == '1') {

                    document.getElementById(HeadLbl).innerHTML = "Select Zones";
                    RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, LstReader, txtZone, LstZone);

                    document.getElementById(LstZone).style.display = "inline";
                    document.getElementById(txtZone).style.display = "inline";
                    document.getElementById(btnOK).style.display = "inline";
                    document.getElementById(btnCancel).style.display = "inline";
                    document.getElementById(Panel1).style.display = "inline";
                    document.getElementById(HeadLbl).style.display = "inline";
                    document.getElementById(btnView).disabled = true;
                    document.getElementById(btnReset).disabled = true;

                }
                else {
                    document.getElementById(Panel1).style.display = "none";
                    return true;
                }
                return true;
            }
            

            if (LoginUser == 'admin') {

                $.ajax({
                    url: "Filter/Filter.asmx/FillEntitySpecificDetails",
                    type: "POST",
                    dataType: "json",
                    timeout: 10000,
                    data: "{'strcom':'" + escape(strcom) + "','strloc':'" + escape(strloc) + "','strdiv':'" + escape(strdiv) + "','strdept':'" + escape(strdept) + "','strcat':'" + escape(strcat) + "','strchecked':'" + escape(strchecked) + "','strshift':'" + escape(Shift) + "','strSec':'" + escape(strSec) + "','strGRD':'" + escape(strGr) + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        //alert("AdminUser");
                       // alert(msg.d);
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
            else if (LoginUser == 'notAdmin') {
             //  alert("notAdminUser");
            }

        }



        function RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType, LstReader, txtZone, LstZone) {
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
                document.getElementById(LstReader).style.display = "none";
            }

            if (txtZone != "") {
                document.getElementById(txtZone).style.display = "none";
                document.getElementById(LstZone).style.display = "none";
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

        }

        /////
        function navigateToUrl(url) {
            var f = document.createElement("FORM");
            f.action = url;

            var indexQM = url.indexOf("?");
            if (indexQM >= 0) {
                // the URL has parameters => convert them to hidden form inputs
                var params = url.substring(indexQM + 1).split("&");
                for (var i = 0; i < params.length; i++) {
                    var keyValuePair = params[i].split("=");
                    var input = document.createElement("INPUT");
                    input.type = "hidden";
                    input.name = keyValuePair[0];
                    input.value = keyValuePair[1];
                    f.appendChild(input);
                }
            }

            document.body.appendChild(f);
            f.submit();
        }

        ///

        //Reports Okclick
        function AccessLevelOkClick(strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSec, strGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstShift, LstSection, LstGrade, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, Panel1, txtCalendarFrom, btnView, btnReset, lstReader, Readerhdn, txtReader, lstZone, ZoneHdn, txtZone) {
            var first = "";
            var count = 0;
            var list1;
            var HiddenVal;
            var entity;
            var strSearchBox;
            if (document.getElementById(LstEmployee).style.display == "inline") {
                list1 = document.getElementById(LstEmployee);
                HiddenVal = strEmployee;
                entity = "Employees";
                strSearchBox = txtSearchEmp;
            }

            if (document.getElementById(LstCompany).style.display == "inline") {
                list1 = document.getElementById(LstCompany);
                HiddenVal = strcomany;
                entity = "Companies";
                strSearchBox = strcomany;
            }

            if (document.getElementById(LstLocation).style.display == "inline") {
                list1 = document.getElementById(LstLocation);
                HiddenVal = strlocation;
                entity = "Locations";
                strSearchBox = txtLocation;
            }

            if (document.getElementById(LstDivision).style.display == "inline") {
                list1 = document.getElementById(LstDivision);
                HiddenVal = strdivision;
                entity = "Divisions";
                strSearchBox = txtDivision;
            }

            if (document.getElementById(LstDepartment).style.display == "inline") {
                list1 = document.getElementById(LstDepartment);
                HiddenVal = strdepartment;
                entity = "Departments";
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

            if (document.getElementById(lstReader).style.display == "inline") {
                list1 = document.getElementById(lstReader);
                HiddenVal = Readerhdn;
                entity = "Reader";
                strSearchBox = txtReader;
            }

            if (document.getElementById(lstZone).style.display == "inline") {
                list1 = document.getElementById(lstZone);
                HiddenVal = ZoneHdn;
                entity = "Zone";
                strSearchBox = txtZone;
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

        function AccessLevelCancelClick(strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSec, strGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstShift, LstSection, LstGrade, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, Panel1, txtCalendarFrom, btnView, btnReset, lstReader, rblReader, ReaderHdn, lstZone, ZoneHdn, txtZone, rblZone) {
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
            if (document.getElementById(ReaderHdn).value == "") {
                document.getElementById(lstReader).style.display = "none";
                document.getElementById(rblReader + '_0').checked = true;
            }

            if (document.getElementById(ZoneHdn).value == "") {
                document.getElementById(lstZone).style.display = "none";
                document.getElementById(rblZone + '_0').checked = true;
            }


            document.getElementById(txtCalendarFrom).disabled = false;
            document.getElementById(btnOK).style.display = "none";
            document.getElementById(btnCancel).style.display = "none";
            document.getElementById(Panel1).style.display = "none";
            document.getElementById(btnView).disabled = false;
            document.getElementById(btnReset).disabled = false;
        }
  
    
    </script>
    <script type="text/javascript" language="javascript">

        function OnShow(action) {
            if (ValidateData()) {
                var formName = document.aspnetForm;
                var sBody = "ACTION=SHOW";
                var url = "Late_Report.aspx?" + sBody;
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
            var url = "Late_Report.aspx?" + sBody;
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
            httpRequest.open('POST', 'Late_Report.aspx', true);
            httpRequest.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            httpRequest.send(sBody);
        }

        function ValidationReport() {
            if (document.getElementById('txtCalendarFrom').value == "") {
                alert("Please enter Date.");
                document.getElementById('txtCalendarFrom').focus();
                return false
            }
        }

        function ValidateData() {
            var formName = document.aspnetForm;

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
		
		
    </script>
    <asp:Panel runat="server" ID="HeadPanel" ClientIDMode="Static" Width="100%" Style="padding-left: 7%;">
        <h1 class="heading" style='text-align: center; font-family: Tahoma; font-size: x-large;'>
            Access Control Details</h1>
        <table class="TableClass" cellspacing="1" align="center" width="100%" style="padding-top: 1%;">
            <tr>
                <td colspan="7" class="TDClassForButton" style="height: 15px">
                    <asp:Panel runat="server" ID="Panel2">
                        <asp:HiddenField ID="hdnUser" runat="server" ClientIDMode="Static" />
                        <table class="TableClass" style="width: 100%; height: 25px; padding-left: 3%;">
                            <tr>
                                <td style="border: thin solid lightsteelblue; text-align: left; width: 30%; font-weight: bold;
                                    display: none;" class="style26">
                                    &nbsp Personnel Type: &nbsp
                                    <asp:DropDownList ID="ddlPersonnelType" runat="server" AutoPostBack="true">
                                        <asp:ListItem Value="E" Text="Employee" />
                                        <asp:ListItem Value="NE" Text="Non Employee" />
                                    </asp:DropDownList>
                                </td>
                                <td style="border: thin solid lightsteelblue; text-align: left; width: 25%; font-weight: bold;"
                                    class="style26">
                                    &nbsp Status : &nbsp
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem Value="3" Text="ALL" />
                                        <asp:ListItem Value="0" Text="Granted" />
                                        <asp:ListItem Value="1" Text="Denied" />
                                    </asp:DropDownList>
                                </td>
                                <td style="border: thin solid lightsteelblue; text-align: left; width: 25%; font-weight: bold;"
                                    class="style26">
                                    &nbsp Level : &nbsp
                                    <asp:DropDownList ID="ddlTA" runat="server">
                                        <asp:ListItem Value="0" Text="ALL" />
                                        <asp:ListItem Value="P" Text="Peripheral" />
                                        <asp:ListItem Value="S" Text="Second Level" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="7" class="TDClassForButton" style="height: 12px">
                    <asp:Panel runat="server" ID="HeadPnl">
                        <table class="TableClass" style="width: 100%; height: 75px; padding-left: 3%;">
                            <tr>
                                <td style="border: thin solid lightsteelblue; text-align: left; width: 10%; font-weight: bold;"
                                    class="style26">
                                    <table style="height: 75px; width: 100%;">
                                        <tr>
                                            <td class="style29">
                                                &nbsp;Employee
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left" class="style29">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow" CssClass="radiobutton"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="EmployeeHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="border: thin solid lightsteelblue; width: 10%; text-align: left; font-weight: bold;"
                                    class="style30">
                                    <table style="height: 75px; width: 100%;">
                                        <tr>
                                            <td>
                                                Company&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ComapnyHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ComapnyHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px; width: 100%;">
                                        <tr>
                                            <td>
                                                Location
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="RadioButtonList3" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('LocationHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="LocationHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="height: 75px; text-align: left; width: 10%; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px; width: 100%;">
                                        <tr>
                                            <td>
                                                &nbsp;Division
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="RadioButtonList4" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('DivisionHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="DivisionHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px; width: 100%;" id="TABLE2">
                                        <tr>
                                            <td>
                                                &nbsp;Department
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="RadioButtonList5" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('DepartmentHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="DepartmentHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="height: 75px; text-align: left; width: 10%; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold; display: none">
                                    <table style="height: 75px; width: 100%;" id="TABLE3">
                                        <tr>
                                            <td>
                                                &nbsp;Shift
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="RadioButtonList6" runat="server" CssClass="radiobutton"
                                                    RepeatLayout="Flow" ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ShiftHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ShiftHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="height: 75px; text-align: left; width: 10%; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px; width: 100%;" id="TABLE1">
                                        <tr>
                                            <td>
                                                &nbsp;Grade
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbtnGrade" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('GradeHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="GradeHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="height: 75px; text-align: left; width: 10%; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px; width: 100%;" id="TABLE3">
                                        <tr>
                                            <td>
                                                &nbsp;Group
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rdbtnGroup" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('GroupHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="GroupHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="height: 75px; text-align: left; width: 10%; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px; width: 100%;" id="TABLE5">
                                        <tr>
                                            <td>
                                                &nbsp;Reader
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rblReader" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ReaderHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ReaderHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="height: 75px; text-align: left; width: 10%; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <table style="height: 75px; width: 100%;" id="TABLE6">
                                        <tr>
                                            <td>
                                                &nbsp;Zone
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rblZone" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('ZoneHdn')">&nbsp All</asp:ListItem>
                                                    <asp:ListItem Value="1">&nbsp Select</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="ZoneHdn" runat="server" ClientIDMode="Static" />
                                </td>
                                <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                    border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                    <%--  old Calender --%>
                                    <%--  NEW CALENDER--%>
                                    <table>
                                        <tr>
                                            <td style="text-align: left">
                                                From Date
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCalendarFrom" onkeyPress="javascript: return false" runat="server"
                                                    ClientIDMode="Static" Height="18px" Width="111px" onkeydown="javascript: return false"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtfromDate_CalendarExtender1" TargetControlID="txtCalendarFrom"
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
                                                    Height="16px" Width="112px" onkeydown="javascript: return false"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtToDate"
                                                    PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
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
                                    <table style="height: 50px; width: 100px">
                                        <tr>
                                            <td>
                                                <asp:Button ID="View" class="ButtonControl" Style="width: 80%" runat="server" Text="View"
                                                    ClientIDMode="Static" OnClick="View_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button4" runat="server" Width="80%" CssClass="ButtonControl" OnClick="Button4_Click"
                                                    Text="Reset" ClientIDMode="Static" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%-- <asp:Button ID="Button5" runat="server" CssClass="ButtonControl" Text="Close" OnClick="Button5_Click" />--%>
                                                <input type="Button" class="ButtonControl" style="width: 80%" value="Close" id="Button5"
                                                    onclick='fnCloseReport()' />
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
                                    <asp:TextBox ID="txtCompany" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtLocation" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtDivision" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtDepartment" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtShift" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtGrade" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtGroup" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtReader" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtZone" runat="server" placeholder="Search" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="LstTitle" runat="server" BackColor="AliceBlue" BorderStyle="Double"
                                        ClientIDMode="Static" Font-Bold="True" Font-Names="Courier New" ForeColor="Black"
                                        Style="text-align: left; font-weight: bold; font-family: 'Courier New', Monospace;"
                                        Width="100%"></asp:Label>
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
                                    <asp:ListBox ID="lstGrade" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstGroup" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstReader" runat="server" ClientIDMode="Static" Font-Bold="true"
                                        Font-Name="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstZone" runat="server" ClientIDMode="Static" Font-Bold="true" Font-Name="Courier New"
                                        ForeColor="Black" Rows="10" SelectionMode="Multiple" Style="display: none" Width="100%">
                                    </asp:ListBox>
                                    <asp:ListBox ID="ListBox7" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox8" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox9" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox10" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="ListBox11" runat="server" ClientIDMode="Static" Font-Bold="True"
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
                                    <asp:ListBox ID="lstReaderDummy" runat="server" ClientIDMode="Static" Font-Bold="true"
                                        Font-Name="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                    <asp:ListBox ID="lstZoneDummy" runat="server" ClientIDMode="Static" Font-Bold="true"
                                        Font-Name="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                        Style="display: none" Width="100%"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 37px; vertical-align: bottom; width: 170px;" colspan="2">
                                    <input id="Button1" class="ButtonControl" name="Ok" style="width: 71px" type="button"
                                        value="OK" runat="server" />
                                    &nbsp;&nbsp;
                                    <input id="Button2" class="ButtonControl" name="Cancel" style="width: 71px" type="button"
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
            <td style="text-align: right" class="style28" colspan="5">
                <div align="right" style="width: 88%;">
                    <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="ButtonControl" OnClick="Close_Click"
                        Visible="False" Width="90px" Height="23px" />
                </div>
            </td>
        </tr>
        <tr style="height: 15px;">
            <td>
            </td>
        </tr>
        <tr>
            <td style="text-align: right" class="style28">
                <div align="center" style="width: 100%;">
                    <asp:Panel ID="viewer" runat="server" Height="410px" BorderStyle="Solid" BorderColor="#0066FF"
                        BorderWidth="3px" Width="77%" Visible="false">
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                            BorderStyle="None" Visible="False" Width="100%">
                        </rsweb:ReportViewer>
                    </asp:Panel>
                    <asp:HiddenField ID="DateHdn" runat="server" ClientIDMode="Static" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Close" Text="Close" Visible="false" runat="server" CssClass="ButtonControl"
                        OnClientClick="return CloseFun()" OnClick="Close_Click1" />
                    &nbsp;&nbsp;
                </div>
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
            width: 93%;
            color: Black;
        }
        .style27
        {
            width: 40%;
        }
        .style28
        {
            width: 100%;
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
        }
    </style>
</asp:Content>
