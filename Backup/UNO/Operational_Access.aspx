<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="Operational_Access.aspx.cs" Inherits="UNO.Operational_Access" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>

    <script language="javascript" type="text/javascript">

        function FillEntitySeletedListBoxBackup() {

            var LstEmployee = document.getElementById('LstEmployee');
            var LstEntitySelected = document.getElementById('LstEntitySelected');
            var LstEmployeeBackUp = document.getElementById('LstEmployee');
            for (var i = 0; i < LstEmployee.options.length; i++) {
                if (LstEmployee.options[i].selected) {

                    var newOption = window.document.createElement('OPTION');
                    newOption.text = LstEmployee.options[i].text;
                    newOption.value = LstEmployee.options[i].value;
                    //alert(LstEmployee.options[i].text);
                    // LstEmployee.options.remove(i);
                    LstEmployee.remove(i);
                    LstEntitySelected.options.add(newOption);


                }
            }
        }


        function disabledcontrols() {
            document.getElementById('LstEntitySelected').style.display = "none";
            document.getElementById('LstEmployee').style.display = "none";
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


        function okclick() {
            var first = "";
            var count = 0;



            if (document.getElementById('LstEmployee').style.display == "inline") {
                var list1 = document.getElementById('LstEntitySelected');

                for (var i = 0; i < list1.options.length; i++) {

                    if (first == "")
                        first = first + "('" + list1.options[i].value + "'";
                    else
                        first = first + ",'" + list1.options[i].value + "'";
                    count = count + 1;

                }

                if (first == "")
                    alert("select atleast one item");
                else {
                    document.getElementById('EmployeeHdn').value = first + ")";
                    document.getElementById('LstEntitySelected').style.display = "none";
                    document.getElementById('Btntd').style.display = "none";
                    document.getElementById('LstEmployee').style.display = "none";
                    document.getElementById('txtSearchEmp').style.display = "none";

                    document.getElementById('btnOK').style.display = "none";
                    document.getElementById('btnClose').style.display = "none";
                    document.getElementById('txtSearchEmp').value = "";

                    document.getElementById('RbdEmp_0').disabled = true;
                    document.getElementById('RbdEmp_1').disabled = true;

                }


            }

            if (document.getElementById('LstCompany').style.display == "inline") {
                var list1 = document.getElementById('LstEntitySelected');

                for (var i = 0; i < list1.options.length; i++) {

                    if (first == "")
                        first = first + "('" + list1.options[i].value + "'";
                    else
                        first = first + ",'" + list1.options[i].value + "'";
                    count = count + 1;

                }

                if (first == "")
                    alert("select atleast one item");
                else {
                    document.getElementById('ComapnyHdn').value = first + ")";
                    document.getElementById('LstCompany').style.display = "none";
                    document.getElementById('LstEntitySelected').style.display = "none";
                    document.getElementById('Btntd').style.display = "none";
                    document.getElementById('txtSearchEmp').style.display = "none";

                    document.getElementById('btnOK').style.display = "none";
                    document.getElementById('btnClose').style.display = "none";
                    document.getElementById('txtSearchEmp').value = "";


                    document.getElementById('RbdCompany_0').disabled = true;
                    document.getElementById('RbdCompany_1').disabled = true;


                }


            }

            if (document.getElementById('LstLocation').style.display == "inline") {
                var list1 = document.getElementById('LstEntitySelected');

                for (var i = 0; i < list1.options.length; i++) {

                    if (first == "")
                        first = first + "('" + list1.options[i].value + "'";
                    else
                        first = first + ",'" + list1.options[i].value + "'";
                    count = count + 1;

                }

                if (first == "")
                    alert("select atleast one item");
                else {
                    document.getElementById('LocationHdn').value = first + ")";
                    document.getElementById('LstLocation').style.display = "none";
                    document.getElementById('LstEntitySelected').style.display = "none";
                    document.getElementById('Btntd').style.display = "none";
                    document.getElementById('txtSearchEmp').style.display = "none";

                    document.getElementById('btnOK').style.display = "none";
                    document.getElementById('btnClose').style.display = "none";
                    document.getElementById('txtSearchEmp').value = "";

                    document.getElementById('RbdLocation_0').disabled = true;
                    document.getElementById('RbdLocation_1').disabled = true;

                }

            }



            if (document.getElementById('LstDivision').style.display == "inline") {
                var list1 = document.getElementById('LstEntitySelected');

                for (var i = 0; i < list1.options.length; i++) {

                    if (first == "")
                        first = first + "('" + list1.options[i].value + "'";
                    else
                        first = first + ",'" + list1.options[i].value + "'";
                    count = count + 1;

                }

                if (first == "")
                    alert("select atleast one item");
                else {
                    document.getElementById('DivisionHdn').value = first + ")";
                    document.getElementById('LstDivision').style.display = "none";
                    document.getElementById('LstEntitySelected').style.display = "none";
                    document.getElementById('Btntd').style.display = "none";
                    document.getElementById('txtSearchEmp').style.display = "none";

                    document.getElementById('btnOK').style.display = "none";
                    document.getElementById('btnClose').style.display = "none";
                    document.getElementById('txtSearchEmp').value = "";

                    document.getElementById('RbdDivision_0').disabled = true;
                    document.getElementById('RbdDivision_1').disabled = true;
                }

            }
            if (document.getElementById('LstDepartment').style.display == "inline") {
                var list1 = document.getElementById('LstEntitySelected');

                for (var i = 0; i < list1.options.length; i++) {

                    if (first == "")
                        first = first + "('" + list1.options[i].value + "'";
                    else
                        first = first + ",'" + list1.options[i].value + "'";
                    count = count + 1;

                }

                if (first == "")
                    alert("select atleast one item");
                else {
                    document.getElementById('DepartmentHdn').value = first + ")";
                    document.getElementById('LstDepartment').style.display = "none";
                    document.getElementById('LstEntitySelected').style.display = "none";
                    document.getElementById('Btntd').style.display = "none";
                    document.getElementById('txtSearchEmp').style.display = "none";

                    document.getElementById('btnOK').style.display = "none";
                    document.getElementById('btnClose').style.display = "none";
                    document.getElementById('txtSearchEmp').value = "";

                    document.getElementById('RbdDepartment_0').disabled = true;
                    document.getElementById('RbdDepartment_1').disabled = true;
                }

            }


            if (document.getElementById('LstCategory').style.display == "inline") {
                var list1 = document.getElementById('LstEntitySelected');

                for (var i = 0; i < list1.options.length; i++) {

                    if (first == "")
                        first = first + "('" + list1.options[i].value + "'";
                    else
                        first = first + ",'" + list1.options[i].value + "'";
                    count = count + 1;


                }

                if (first == "")
                    alert("select atleast one item");
                else {
                    document.getElementById('ShiftHdn').value = first + ")";
                    document.getElementById('LstCategory').style.display = "none";
                    document.getElementById('LstEntitySelected').style.display = "none";
                    document.getElementById('Btntd').style.display = "none";
                    document.getElementById('txtSearchEmp').style.display = "none";

                    document.getElementById('btnOK').style.display = "none";
                    document.getElementById('btnClose').style.display = "none";
                    document.getElementById('txtSearchEmp').innerHTML = "";

                    document.getElementById('RbdCategory_0').disabled = true;
                    document.getElementById('RbdCategory_1').disabled = true;
                }

            }



        }




        function closeclick() {


            document.getElementById('btnOK').style.display = "none";
            document.getElementById('Button4').style.display = "none";


            document.getElementById('Btntd').style.display = "none";



            if (document.getElementById('EmployeeHdn').value == "")
                document.getElementById('LstEmployee').style.display = "none";
            document.getElementById('RbdEmp_0').checked = true;




            if (document.getElementById('ComapnyHdn').value == "")
                document.getElementById('LstCompany').style.display = "none";
            document.getElementById('RbdCompany_0').checked = true;

            if (document.getElementById('LocationHdn').value == "")
                document.getElementById('LstLocation').style.display = "none";
            document.getElementById('RbdLocation_0').checked = true;

            if (document.getElementById('DivisionHdn').value == "")
                document.getElementById('LstDivision').style.display = "none";
            document.getElementById('RbdDivision_0').checked = true;

            document.getElementById('LstEmployee').style.display = "none";
            document.getElementById('LstEntitySelected').style.display = "none";

            if (document.getElementById('ShiftHdn').value == "")
                document.getElementById('LstCategory').style.display = "none";
            document.getElementById('RbdCategory_0').checked = true;

            if (document.getElementById('DepartmentHdn').value == "")
                document.getElementById('LstDepartment').style.display = "none";
            document.getElementById('RbdDepartment_0').checked = true;




        }


        function HideControls() {
            document.getElementById('Btntd').style.display = "none";



            //                 if (document.getElementById('EmployeeHdn').value != "")
            document.getElementById('LstEmployee').style.display = "none";
            document.getElementById('LstEntitySelected').style.display = "none";
            document.getElementById('RbdEmp_0').checked = true;
            document.getElementById('RbdCompany_0').checked = true;
            document.getElementById('RbdLocation_0').checked = true;
            document.getElementById('RbdDivision_0').checked = true;
            document.getElementById('RbdDepartment_0').checked = true;
            document.getElementById('RbdCategory_0').checked = true;



            //                 if (document.getElementById('ComapnyHdn').value != "")
            //                     document.getElementById('LstCompany').style.display = "none";
            //                     document.getElementById('LstEntitySelected').style.display = "none";
            //                     document.getElementById('RbdCompany_1').checked = true;
            //                     document.getElementById('RbdLocation_0').checked = true;
            //                     document.getElementById('RbdDivision_0').checked = true;
            //                     document.getElementById('RbdDepartment_0').checked = true;
            //                     document.getElementById('RbdCategory_0').checked = true;


            //                 if (document.getElementById('LocationHdn').value != "")
            //                     document.getElementById('LstLocation').style.display = "none";
            //                     document.getElementById('LstEntitySelected').style.display = "none";
            //                     document.getElementById('RbdLocation_1').checked = true;

            //                 if (document.getElementById('DivisionHdn').value != "")
            //                     document.getElementById('LstDivision').style.display = "none";
            //                     document.getElementById('LstEntitySelected').style.display = "none";
            //                     document.getElementById('RbdDivision_1').checked = true;


            //                 if (document.getElementById('ShiftHdn').value != "")
            //                     document.getElementById('LstCategory').style.display = "none";
            //                 document.getElementById('LstEntitySelected').style.display = "none";
            //                 document.getElementById('RbdCategory_1').checked = true;

            //                 if (document.getElementById('DepartmentHdn').value != "")
            //                     document.getElementById('LstDepartment').style.display = "none";
            //                 document.getElementById('LstEntitySelected').style.display = "none";
            //                 document.getElementById('RbdDepartment_1').checked = true;
        }


        function AllSelectedListBox() {

            if (document.getElementById('RbdEmp_1').checked == true) {
                var LstEmployee = document.getElementById('LstEmployee');
                var LstEntitySelected = document.getElementById('LstEntitySelected');

            }
            if (document.getElementById('RbdCompany_1').checked == true) {

                var LstEmployee = document.getElementById('LstCompany');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdLocation_1').checked == true) {

                var LstEmployee = document.getElementById('LstLocation');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdDivision_1').checked == true) {

                var LstEmployee = document.getElementById('LstDivision');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdDepartment_1').checked == true) {

                var LstEmployee = document.getElementById('LstDepartment');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdCategory_1').checked == true) {

                var LstEmployee = document.getElementById('LstCategory');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }


            // for (var i = LstEmployee.options.length - 1; i >= 0; i--) {

            for (var i = 0; i <= LstEmployee.options.length - 1; i++) {
                var newOption = window.document.createElement('OPTION');
                newOption.text = LstEmployee.options[i].text;
                newOption.value = LstEmployee.options[i].value;

                //                alert(LstEmployee.options[i].text);
                LstEntitySelected.options.add(newOption);


            }
            //            $("#LstEmployee").empty();
            LstEmployee.options.length = 0;
        }

        function FillEntitySeletedListBox() {

            if (document.getElementById('RbdEmp_1').checked == true) {



                var LstEmployee = document.getElementById('LstEmployee');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdCompany_1').checked == true) {



                LstEmployee = document.getElementById('LstCompany');
                LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdLocation_1').checked == true) {

                var LstEmployee = document.getElementById('LstLocation');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdDivision_1').checked == true) {


                var LstEmployee = document.getElementById('LstDivision');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdDepartment_1').checked == true) {


                var LstEmployee = document.getElementById('LstDepartment');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdCategory_1').checked == true) {


                var LstEmployee = document.getElementById('LstCategory');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }




            for (var i = LstEmployee.options.length - 1; i >= 0; i--) {
                if (LstEmployee.options[i].selected) {

                    var newOption = window.document.createElement('OPTION');

                    newOption.text = LstEmployee.options[i].text;
                    newOption.value = LstEmployee.options[i].value;
                    // alert(LstEmployee.options[i].text);
                    LstEmployee.remove(i);
                    //LstEmployee.options.remove(i);
                    LstEntitySelected.options.add(newOption);


                }
            }
        }


        function removeEntitySelectedListBox() {

            if (document.getElementById('RbdEmp_1').checked == true) {
                var LstEmployee = document.getElementById('LstEmployee');
                var LstEntitySelected = document.getElementById('LstEntitySelected');

            }
            if (document.getElementById('RbdCompany_1').checked == true) {


                var LstEmployee = document.getElementById('LstCompany');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdLocation_1').checked == true) {

                var LstEmployee = document.getElementById('LstLocation');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdDivision_1').checked == true) {

                var LstEmployee = document.getElementById('LstDivision');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdDepartment_1').checked == true) {

                var LstEmployee = document.getElementById('LstDepartment');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdCategory_1').checked == true) {

                var LstEmployee = document.getElementById('LstCategory');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }
            // for (var i = LstEmployee.options.length - 1; i >= 0; i--) {

            for (var i = 0; i <= LstEntitySelected.options.length - 1; i++) {
                var newOption = window.document.createElement('OPTION');
                newOption.text = LstEntitySelected.options[i].text;
                newOption.value = LstEntitySelected.options[i].value;


                LstEmployee.options.add(newOption);


            }

            LstEntitySelected.options.length = 0;
        }





        function ReturnFillEntityAvailable() {

            if (document.getElementById('RbdEmp_1').checked == true) {
                var LstEmployee = document.getElementById('LstEmployee');
                var LstEntitySelected = document.getElementById('LstEntitySelected');

            }
            if (document.getElementById('RbdCompany_1').checked == true) {

                var LstEmployee = document.getElementById('LstCompany');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdLocation_1').checked == true) {

                var LstEmployee = document.getElementById('LstLocation');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdDivision_1').checked == true) {

                var LstEmployee = document.getElementById('LstDivision');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdDepartment_1').checked == true) {

                var LstEmployee = document.getElementById('LstDepartment');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }

            if (document.getElementById('RbdCategory_1').checked == true) {

                var LstEmployee = document.getElementById('LstCategory');
                var LstEntitySelected = document.getElementById('LstEntitySelected');
            }


            for (var i = LstEntitySelected.options.length - 1; i >= 0; i--) {
                if (LstEntitySelected.options[i].selected) {
                    var newOption = window.document.createElement('OPTION');
                    newOption.text = LstEntitySelected.options[i].text;
                    newOption.value = LstEntitySelected.options[i].value;

                    LstEntitySelected.options.remove(i);
                    LstEmployee.options.add(newOption);

                }

            }

        }

        function showEmpLoyee() {


            document.getElementById('LstEmployee').style.display = "inline";
            document.getElementById('LstCompany').style.display = "none";
            document.getElementById('LstLocation').style.display = "none";
            document.getElementById('LstDivision').style.display = "none";

            document.getElementById('Btntd').style.display = "inline";

            document.getElementById('LstDepartment').style.display = "none";
            document.getElementById('LstCategory').style.display = "none";

            document.getElementById('LstEntitySelected').options.length = 0;

            document.getElementById('LstEntitySelected').style.display = "inline";


            document.getElementById('btnOK').style.display = "inline";
            document.getElementById('Button4').style.display = "inline";


        }

        function showCompany() {

            document.getElementById('btnOK').style.display = "inline";
            document.getElementById('Button4').style.display = "inline";


            document.getElementById('LstEmployee').style.display = "none";
            document.getElementById('LstCompany').style.display = "inline";
            document.getElementById('LstLocation').style.display = "none";
            document.getElementById('LstDivision').style.display = "none";

            document.getElementById('LstDivision').style.display = "none";

            document.getElementById('LstDepartment').style.display = "none";
            document.getElementById('LstCategory').style.display = "none";

            if (document.getElementById('EmployeeHdn').value != "") {
                document.getElementById('RbdEmp_1').checked = true;
            }
            document.getElementById('RbdEmp_0').disabled = true;
            document.getElementById('RbdEmp_1').disabled = true;

            document.getElementById('LstEntitySelected').options.length = 0;
            document.getElementById('LstEntitySelected').style.display = "inline";

            document.getElementById('Btntd').style.display = "inline";

        }


        function showLocation() {

            document.getElementById('btnOK').style.display = "inline";
            document.getElementById('Button4').style.display = "inline";


            document.getElementById('LstEmployee').style.display = "none";
            document.getElementById('LstCompany').style.display = "none";
            document.getElementById('LstLocation').style.display = "inline";
            document.getElementById('LstDivision').style.display = "none";

            document.getElementById('LstDepartment').style.display = "none";
            document.getElementById('LstCategory').style.display = "none";

            //            document.getElementById('RbdEmp_0').checked = true;
            document.getElementById('RbdEmp_0').disabled = true;
            document.getElementById('RbdEmp_1').disabled = true;

            if (document.getElementById('LocationHdn').value != "") {
                document.getElementById('RbdLocation_1').checked = true;
            }



            document.getElementById('RbdCompany_0').disabled = true;
            document.getElementById('RbdCompany_1').disabled = true;

            document.getElementById('Btntd').style.display = "inline";
            document.getElementById('LstEntitySelected').options.length = 0;
            document.getElementById('LstEntitySelected').style.display = "inline";

        }

        function showDivision() {

            document.getElementById('btnOK').style.display = "inline";
            document.getElementById('Button4').style.display = "inline";


            document.getElementById('LstEmployee').style.display = "none";
            document.getElementById('LstCompany').style.display = "none";
            document.getElementById('LstLocation').style.display = "none";
            document.getElementById('LstDivision').style.display = "inline";

            document.getElementById('LstDepartment').style.display = "none";
            document.getElementById('LstCategory').style.display = "none";

            //  document.getElementById('RbdEmp_0').checked = true;
            document.getElementById('RbdEmp_0').disabled = true;
            document.getElementById('RbdEmp_1').disabled = true;

            //document.getElementById('RbdCompany_0').checked = true;
            document.getElementById('RbdCompany_0').disabled = true;
            document.getElementById('RbdCompany_1').disabled = true;

            // document.getElementById('RbdLocation_0').checked = true;
            document.getElementById('RbdLocation_0').disabled = true;
            document.getElementById('RbdLocation_1').disabled = true;

            if (document.getElementById('DivisionHdn').value != "") {
                document.getElementById('RbdDivision_1').checked = true;
            }

            document.getElementById('Btntd').style.display = "inline";
            document.getElementById('LstEntitySelected').options.length = 0;
            document.getElementById('LstEntitySelected').style.display = "inline";

        }

        function showDepartment() {

            document.getElementById('btnOK').style.display = "inline";
            document.getElementById('Button4').style.display = "inline";


            document.getElementById('LstEmployee').style.display = "none";
            document.getElementById('LstCompany').style.display = "none";
            document.getElementById('LstLocation').style.display = "none";
            document.getElementById('LstDivision').style.display = "none";

            document.getElementById('LstDepartment').style.display = "inline";
            document.getElementById('LstCategory').style.display = "none";


            document.getElementById('RbdEmp_0').disabled = true;
            document.getElementById('RbdEmp_1').disabled = true;


            document.getElementById('RbdCompany_0').disabled = true;
            document.getElementById('RbdCompany_1').disabled = true;

            document.getElementById('RbdLocation_0').disabled = true;
            document.getElementById('RbdLocation_1').disabled = true;


            document.getElementById('RbdDivision_0').disabled = true;
            document.getElementById('RbdDivision_1').disabled = true;

            document.getElementById('Btntd').style.display = "inline";
            document.getElementById('LstEntitySelected').options.length = 0;
            document.getElementById('LstEntitySelected').style.display = "inline";

            if (document.getElementById('DepartmentHdn').value != "") {
                document.getElementById('RbdDepartment_1').checked = true;
            }

        }

        function showCategory() {

            document.getElementById('btnOK').style.display = "inline";
            document.getElementById('Button4').style.display = "inline";


            document.getElementById('LstEmployee').style.display = "none";
            document.getElementById('LstCompany').style.display = "none";
            document.getElementById('LstLocation').style.display = "none";
            document.getElementById('LstDivision').style.display = "none";

            document.getElementById('LstDepartment').style.display = "none";
            document.getElementById('LstCategory').style.display = "inline";

            //  document.getElementById('RbdEmp_0').checked = true;
            document.getElementById('RbdEmp_0').disabled = true;
            document.getElementById('RbdEmp_1').disabled = true;

            //document.getElementById('RbdCompany_0').checked = true;
            document.getElementById('RbdCompany_0').disabled = true;
            document.getElementById('RbdCompany_1').disabled = true;

            //document.getElementById('RbdLocation_0').checked = true;
            document.getElementById('RbdLocation_0').disabled = true;
            document.getElementById('RbdLocation_1').disabled = true;

            // document.getElementById('RbdDivision_0').checked = true;
            document.getElementById('RbdDivision_0').disabled = true;
            document.getElementById('RbdDivision_1').disabled = true;



            document.getElementById('RbdDepartment_0').disabled = true;
            document.getElementById('RbdDepartment_1').disabled = true;

            document.getElementById('Btntd').style.display = "inline";
            document.getElementById('LstEntitySelected').options.length = 0;
            document.getElementById('LstEntitySelected').style.display = "inline";



            if (document.getElementById('ShiftHdn').value != "") {
                document.getElementById('RbdCategory_1').checked = true;

            }

        }

        //        function filtertexbox() {

        //            var txtValue = document.getElementById('TextBox1').value;
        //            var lstbox = document.getElementById('ListBox1');
        //            var opt;
        //            var array = new Array();
        //            var array1 = new Array();
        //            array = lstbox.options;
        //            if (txtValue != '') {
        //                for (var i = 0; i < array.length; i++) {
        //                    if (array[i].text.toLowerCase().indexOf(txtValue) != -1) {
        //                        opt = document.createElement("option");
        //                        opt.text = array[i].text;
        //                        opt.value = array[i].value;
        //                        array1.push(opt);
        //                    }
        //                }
        //                lstbox.options.length = 0;
        //                for (var i = 0; i < array1.length; i++) {
        //                    opt = document.createElement("option");
        //                    opt.text = array1[i].text;
        //                    opt.value = array1[i].value;
        //                    lstbox.add(opt);
        //                }
        //            }
        //            else {
        //                lstbox.options.length = 0;
        //                for (var i = 0; i < array.length; i++) {
        //                    opt = document.createElement("option");
        //                    opt.text = array[i].text;
        //                    opt.value = array[i].value;
        //                    lstbox.add(opt);
        //                }
        //            }
        //        }

        var myVals = new Array();
        function CacheValues() {
            var l = document.getElementById('lstManager');

            for (var i = 0; i < l.options.length; i++) {
                myVals[i] = l.options[i].text;
            }
        }
        function SearchList() {

            var l = document.getElementById('lstManager');
            var tb = document.getElementById('TextBox1');

            l.options.length = 0;

            if (tb.value == "") {
                for (var i = 0; i < myVals.length; i++) {
                    l.options[l.options.length] = new Option(myVals[i]);
                }
            }
            else {


                for (var i = 0; i < myVals.length; i++) {
                    if (myVals[i].toLowerCase().indexOf(tb.value.toLowerCase()) != -1) {
                        l.options[l.options.length] = new Option(myVals[i]);
                    }
                    else {
                        // do nothing
                    }
                }
            }
        }

        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                        <asp:Label ID="lblHead" runat="server" Text="Operation Level Access" ForeColor="RoyalBlue"
                            Font-Size="20px" Width="100%" CssClass="heading">
                        </asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <div>
                <table style="margin-left: 35%;">
                    <tr>
                        <td>
                           
                            <asp:Label ID="lblLEvel" runat="server" Text="Select Level:" Height="5px" ></asp:Label>
                            </td><td>
                            <asp:TextBox ID="TextBox1" onkeyup="return SearchList();" runat="server" Height="23px" style="display:none;"
                                ClientIDMode="Static" Width="157px"></asp:TextBox>
                            <br />
                            <asp:ListBox ID="lstManager" runat="server" ClientIDMode="Static" Font-Bold="True" class="chosen-select"
                                Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Single" Width="250px"
                                AutoPostBack="True" OnSelectedIndexChanged="lstManager_SelectedIndexChanged" Height="50px">
                            </asp:ListBox>
                        </td>
                    </tr>
                </table>
            </div>
            <table class="TableClass" style="width: 70%; height: 75px; margin-left: 15%;">
                <tr>
                    <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;
                        width: 1%;">
                        <table style="height: 75px; width: 102px;">
                            <tr>
                                <td class="style29">
                                    &nbsp;Employee
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <asp:RadioButtonList ID="RbdEmp" runat="server" RepeatLayout="Flow" CssClass="radiobutton"
                                        ClientIDMode="Static">
                                        <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                        <asp:ListItem Value="1" onclick="showEmpLoyee()">Select</asp:ListItem>
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
                                        <asp:ListItem Value="1" onclick="showCompany()">Select</asp:ListItem>
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
                                    &nbsp;Location
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <asp:RadioButtonList ID="RbdLocation" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                        ClientIDMode="Static">
                                        <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                        <asp:ListItem Value="1" onclick="showLocation()">Select</asp:ListItem>
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
                                    &nbsp;Division
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <asp:RadioButtonList ID="RbdDivision" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                        ClientIDMode="Static">
                                        <asp:ListItem Value="0" Selected="True" onclick="showDivision()">All</asp:ListItem>
                                        <asp:ListItem Value="1" onclick="showDivision()">Select</asp:ListItem>
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
                                    &nbsp;Department
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <asp:RadioButtonList ID="RbdDepartment" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                        ClientIDMode="Static">
                                        <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                        <asp:ListItem Value="1" onclick="showDepartment()">Select</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="DepartmentHdn" runat="server" ClientIDMode="Static" />
                    </td>
                    <td style=" width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
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
                                        <asp:ListItem Value="1" onclick="showCategory()">Select</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="ShiftHdn" runat="server" ClientIDMode="Static" />
                    </td>
                </tr>
            </table>
            <div>
                <table style="margin-left: 20%;">
                    <tr>
                        <td>
                            <asp:ListBox ID="LstEmployee" runat="server" ClientIDMode="Static" Style="display: none"
                                Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                Width="250px" Height="119px"></asp:ListBox>
                            <asp:ListBox ID="LstCompany" runat="server" ClientIDMode="Static" Style="display: none"
                                Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                Width="250px" Height="119px" ></asp:ListBox>
                            <asp:ListBox ID="LstLocation" runat="server" ClientIDMode="Static" Style="display: none"
                                Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                Width="250px" Height="119px"></asp:ListBox>
                            <asp:ListBox ID="LstDivision" runat="server" ClientIDMode="Static" Style="display: none"
                                Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                Width="250px" Height="119px"></asp:ListBox>
                            <asp:ListBox ID="LstDepartment" runat="server" ClientIDMode="Static" Style="display: none"
                                Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                Width="250px" Height="119px"></asp:ListBox>
                            <asp:ListBox ID="LstCategory" runat="server" ClientIDMode="Static" Style="display: none"
                                Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                Width="250px" Height="119px"></asp:ListBox>
                        </td>
                        <td class="TDClassControl" id="Btntd" clientidmode="Static" style="width: 14%; display: none;">
                            <table style="height: 100px">
                                <tr>
                                    <td class="TDClassControl">
                                        <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                        <input id="cmdEntityAllRight" class="ButtonControl" onclick="AllSelectedListBox()"
                                            value="&gt;&gt;" style="width: 38px" type="button" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDClassControl">
                                        <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                        <input id="Button1" class="ButtonControl" value="&gt;" onclick="FillEntitySeletedListBox()"
                                            style="width: 38px" type="button" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDClassControl">
                                        <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                        <input id="Button3" class="ButtonControl" value="&lt;" onclick="ReturnFillEntityAvailable()"
                                            style="width: 38px" type="button" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDClassControl">
                                        <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                        <input id="Button2" class="ButtonControl" value="&lt;&lt;" onclick="removeEntitySelectedListBox()"
                                            style="width: 38px" type="button" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:ListBox ID="LstEntitySelected" runat="server" ClientIDMode="Static" Style="display: none"
                                Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                                Width="250px" Height="119px"></asp:ListBox>
                        </td>
                    </tr>
                </table>
            </div>
            <table id="Table4" runat="server" class="tableclass" style="margin-left: 10%; width: 76%;">
                <tr>
                    <td colspan="4" class="TDClassControl" style="text-align: center; padding-right: 44px;">
                        <%--<input id="btnOK" runat="server" class="ButtonControl" style="width:7%; margin-left:4%;" name="Ok"   type="button" value="OK" onclick="return okclick();" />--%>
                        <asp:Button ID="btnOK" runat="server" ClientIDMode="Static" class="ButtonControl" style="display:none" OnClientClick="return okclick();"
                            Text="Ok" OnClick="btnOK_Click" />
                        <input id="Button4" class="ButtonControl" style="width: 7%;display:none;" name="Close" type="button"
                            value="Close" onclick="closeclick()" />
                    </td>
                </tr>
            </table>
            <div class="DivEmpDetails">
                <table style="width: 100%;">
                    <tr>
                        <td style="background-color: #EFF8FE; padding: 0px;" colspan="3">
                            <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                                <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                            </div>
                            <asp:GridView ID="gvEmpMgrAdd" runat="server" AutoGenerateColumns="false" Width="100%"
                                GridLines="None" AllowPaging="true" PageSize="10">
                                <RowStyle CssClass="gvRow" />
                                <HeaderStyle CssClass="gvHeader" />
                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                <PagerStyle CssClass="gvPager" />
                                <EmptyDataTemplate>
                                    <div>
                                        <span>No Users found.</span>
                                    </div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="EOD_EMPID" HeaderText="Employee ID"></asp:BoundField>
                                    <asp:BoundField DataField="Empname" HeaderText="Employee Name"></asp:BoundField>
                                    <asp:BoundField DataField="EOD_DIVISION_ID" HeaderText="Division"></asp:BoundField>
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
                        </td>
                    </tr>
                </table>
            </div>
            <table style="width: 100%;">
                <tr>
                    <td align="center" colspan="2">
                        &nbsp;
                        <asp:Button ID="Button5" runat="server" Style="display: none;" Text="test" />
                        <asp:Button ID="btnSubmitAdd" runat="server" CssClass="ButtonControl" OnClick="btnSubmitAdd_Click"
                            Text="Save" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" Text="Reset"  OnClick ="btnCancelAdd_Click"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
           <asp:PostBackTrigger ControlID="lstManager" />
        <asp:PostBackTrigger ControlID="btnOK" />
        <asp:PostBackTrigger ControlID="btnSubmitAdd" />
    </Triggers>
    </asp:UpdatePanel>

        <script src="Scripts/Choosen/chosen.jquery.js" type="text/javascript"></script>
    <script src="Scripts/Choosen/docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "95%" }
            //            chosen:showing_dropdown
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
       </script>
</asp:Content>
