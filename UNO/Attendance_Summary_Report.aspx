<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="Attendance_Summary_Report.aspx.cs" Inherits="UNO.Attendance_Summary_Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">


       <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>

    <script type="text/javascript" language="javascript">

        function FilterEmployee() {

            var list = document.getElementById("<%=ListBox1.ClientID%>");
            var list1 = document.getElementById("<%=ListBox7.ClientID%>");
            var textEmpToSearch = document.getElementById("<%=txtEmployeeSearch.ClientID%>");

            //alert(textEmpToSearch);

            var items = 0;

            for (items = list1.options.length - 1; items >= 0; items--) {
                list.options[items] = null;
            }



            var i = 0, j = 0;
            var strList;

            var strTxt = textEmpToSearch.value.toLowerCase().replace(/\s/g, '');
            var strLength = textEmpToSearch.value.length;


            //  alert(strTxt);
            //  alert(strLength);

            for (i = 0; i <= list1.options.length - 1; i++) {

                strList = list1.options[i].text.substring(0, strLength).toLowerCase().replace(/\s/g, '');
                // alert(strList);

                if (strList == strTxt) {
                    //  alert("Condtion  Satisfied");
                    list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                    j++;
                }

            }

        }

        

        function FilterLocation() {

            var list = document.getElementById("<%=ListBox3.ClientID%>");
            var list1 = document.getElementById("<%=ListBox9.ClientID%>");
            var textEmpToSearch = document.getElementById("<%=txtLocation.ClientID%>");

            //alert(textEmpToSearch);

            var items = 0;

            for (items = list1.options.length - 1; items >= 0; items--) {
                list.options[items] = null;
            }



            var i = 0, j = 0;
            var strList;

            var strTxt = textEmpToSearch.value.toLowerCase().replace(/\s/g, '');
            var strLength = textEmpToSearch.value.length;


            //  alert(strTxt);
            //  alert(strLength);

            for (i = 0; i <= list1.options.length - 1; i++) {

                strList = list1.options[i].text.substring(0, strLength).toLowerCase().replace(/\s/g, '');
                // alert(strList);

                if (strList == strTxt) {
                    //  alert("Condtion  Satisfied");
                    list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                    j++;
                }

            }

        }



        function FilterDivision() {

            var list = document.getElementById("<%=ListBox4.ClientID%>");
            var list1 = document.getElementById("<%=ListBox10.ClientID%>");
            var textEmpToSearch = document.getElementById("<%=txtDivision.ClientID%>");

            //alert(textEmpToSearch);

            var items = 0;

            for (items = list1.options.length - 1; items >= 0; items--) {
                list.options[items] = null;
            }



            var i = 0, j = 0;
            var strList;

            var strTxt = textEmpToSearch.value.toLowerCase().replace(/\s/g, '');
            var strLength = textEmpToSearch.value.length;


            //  alert(strTxt);
            //  alert(strLength);

            for (i = 0; i <= list1.options.length - 1; i++) {

                strList = list1.options[i].text.substring(0, strLength).toLowerCase().replace(/\s/g, '');
                // alert(strList);

                if (strList == strTxt) {
                    //  alert("Condtion  Satisfied");
                    list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                    j++;
                }

            }

        }

      



        function ResetHdn(str) {
            document.getElementById(str).value = "";

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
        function show1() {

            document.getElementById('Panel1').style.display = "inline";
            document.getElementById('ListBox1').style.display = "inline";
            document.getElementById('ListBox3').style.display = "none";
            document.getElementById('ListBox4').style.display = "none";
            document.getElementById('txtemp').style.display = "none";

            List1('ListBox1', 'EmployeeHdn');
            common();
            document.getElementById('HeadLbl').innerHTML = "Select Employees";
            document.getElementById('LstTitle').innerHTML = "&nbsp;Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Code";

            document.getElementById("<%=txtEmployeeSearch.ClientID%>").style.display = "inline";
            
            document.getElementById("<%=txtLocation.ClientID%>").style.display = "none";
            document.getElementById("<%=txtDivision.ClientID%>").style.display = "none";
         

        }
        function showTextBox() {

            document.getElementById('Panel1').style.display = "inline";
            document.getElementById("<%=txtEmployeeSearch.ClientID%>").style.display = "none";
            document.getElementById('txtemp').style.display = "inline";
            document.getElementById('ListBox1').style.display = "none";
            document.getElementById('ListBox3').style.display = "none";
            document.getElementById('ListBox4').style.display = "none";
            document.getElementById('LstTitle').style.display = "none";
            common();
            document.getElementById('HeadLbl').innerHTML = "Enter Employee Code";


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

        function show3() {
            document.getElementById('Panel1').style.display = "inline";
            document.getElementById('ListBox1').style.display = "none";
            document.getElementById('ListBox3').style.display = "inline";
            document.getElementById('ListBox4').style.display = "none";
            document.getElementById('txtemp').style.display = "none";
            common();
            List1('ListBox3', 'LocationHdn');
            document.getElementById('HeadLbl').innerHTML = "Select Locations";
            document.getElementById('LstTitle').innerHTML = "&nbsp;Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Code";

            document.getElementById("<%=txtEmployeeSearch.ClientID%>").style.display = "none";
       
            document.getElementById("<%=txtLocation.ClientID%>").style.display = "inline";
            document.getElementById("<%=txtDivision.ClientID%>").style.display = "none";
          

        }

        function show4() {
            document.getElementById('Panel1').style.display = "inline";
            document.getElementById('ListBox1').style.display = "none";
            document.getElementById('ListBox3').style.display = "none";
            document.getElementById('ListBox4').style.display = "inline";
            document.getElementById('txtemp').style.display = "none";
            common();
            List1('ListBox4', 'DivisionHdn');
            document.getElementById('HeadLbl').innerHTML = "Select Divisions";
            document.getElementById('LstTitle').innerHTML = "&nbsp;Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Code";

            document.getElementById("<%=txtEmployeeSearch.ClientID%>").style.display = "none";
        
            document.getElementById("<%=txtLocation.ClientID%>").style.display = "none";
            document.getElementById("<%=txtDivision.ClientID%>").style.display = "inline";


        }



        function common() {
            document.getElementById('RadioButtonList1_0').disabled = true;
            document.getElementById('RadioButtonList1_1').disabled = true;
            document.getElementById('RadioButtonList3_0').disabled = true;
            document.getElementById('RadioButtonList3_1').disabled = true;
            document.getElementById('RadioButtonList4_0').disabled = true;
            document.getElementById('RadioButtonList4_1').disabled = true;
            document.getElementById('View').disabled = true;
//            document.getElementById('RadioButtonList1_2').disabled = true;
        }

        function EnableControls() {
            document.getElementById('RadioButtonList1_0').disabled = false;
            document.getElementById('RadioButtonList1_1').disabled = false;
            document.getElementById('RadioButtonList3_0').disabled = false;
            document.getElementById('RadioButtonList3_1').disabled = false;
            document.getElementById('RadioButtonList4_0').disabled = false;
            document.getElementById('RadioButtonList4_1').disabled = false;
            document.getElementById('View').disabled = false;
            document.getElementById('Panel1').style.display = "none";
//            document.getElementById('RadioButtonList1_2').disabled = false;
        }

        function DisableControls() {

            document.getElementById('RadioButtonList3_0').disabled = true;
            document.getElementById('RadioButtonList3_1').disabled = true;
            document.getElementById('RadioButtonList4_0').disabled = true;
            document.getElementById('RadioButtonList4_1').disabled = true;
            document.getElementById('RadioButtonList3_0').checked = true;
            document.getElementById('RadioButtonList4_0').checked = true;
            document.getElementById('RadioButtonList1_0').disabled = false;
            document.getElementById('RadioButtonList1_1').disabled = false;
//            document.getElementById('RadioButtonList1_2').disabled = false;
        }
        function OkClick() {
            var first = "";
            var count = 0;
            if (document.getElementById('ListBox1').style.display == "inline") {
                var list1 = document.getElementById('ListBox1');

                for (var i = 0; i < list1.options.length; i++) {
                    if (list1.options[i].selected) {
                        if (first == "")
                            first = first + "''" + list1.options[i].value + "''";
                        else
                            first = first + ",''" + list1.options[i].value + "''";
                        count = count + 1;
                    }
                }
                if (count > 1000) {
                    alert("Can not select more than 1000 Employees\n          Total Selected Records :" + count);
                    return false;
                }
                //   alert(first);
                if (first == "")
                    alert("select atleast one item");
                else {
                    EnableControls();
//                    document.getElementById('RadioButtonList1_2').disabled = false;
                    document.getElementById('EmployeeHdn').value = first;
                    //  alert(document.getElementById('EmployeeHdn').value);
                    //  document.getElementById('RadioButtonList1_2').disabled = true;
                    DisableControls();

                }
            }


            if (document.getElementById('ListBox3').style.display == "inline") {
                var list3 = document.getElementById('ListBox3');

                for (var i = 0; i < list3.options.length; i++) {
                    if (list3.options[i].selected) {
                        if (first == "")
                            first = first + "''" + list3.options[i].value + "''";
                        else
                            first = first + ",''" + list3.options[i].value + "''";
                    }
                }
                if (first == "")
                    alert("select atleast one item");
                else {
                    EnableControls();
                    document.getElementById('LocationHdn').value = first;
                }
            }

            if (document.getElementById('ListBox4').style.display == "inline") {
                var list4 = document.getElementById('ListBox4');

                for (var i = 0; i < list4.options.length; i++) {
                    if (list4.options[i].selected) {
                        if (first == "")
                            first = first + "''" + list4.options[i].value + "''";
                        else
                            first = first + ",''" + list4.options[i].value + "''";
                    }
                }
                if (first == "")
                    alert("select atleast one item");
                else {
                    EnableControls();
                    document.getElementById('DivisionHdn').value = first;
                }
            }

            if (document.getElementById('txtemp').style.display == "inline") {
                first = document.getElementById('txtemp').value;
                if (first == "")
                    alert("Please enter employee code");
                else {
                    EnableControls();
                    document.getElementById('EmployeeHdn').value = first;
                    document.getElementById('DivisionHdn').value = "";
                    document.getElementById('LocationHdn').value = "";
                    DisableControls();
                }
            }


        }

        function CancelClick() {

            document.getElementById('LstTitle').innerHTML = "";

            if (document.getElementById('RadioButtonList1_1').checked == true) {
                if (document.getElementById('EmployeeHdn').value != "") {
                    DisableControls();
                    document.getElementById('Panel1').style.display = "none";
                    document.getElementById('HeadLbl').innerHTML = "";
                    document.getElementById('View').disabled = false;

                    return;
                }
            }

//            if (document.getElementById('RadioButtonList1_2').checked == true) {
//                if (document.getElementById('EmployeeHdn').value != "") {
//                    DisableControls();
//                    document.getElementById('Panel1').style.display = "none";
//                    document.getElementById('HeadLbl').innerHTML = "";
//                    document.getElementById('View').disabled = false;
//                    return;
//                }
//            }

            EnableControls();



            document.getElementById('Panel1').style.display = "none";
            if (document.getElementById('EmployeeHdn').value == "")
                document.getElementById('RadioButtonList1_0').checked = true;

            if (document.getElementById('LocationHdn').value == "")
                document.getElementById('RadioButtonList3_0').checked = true;

            if (document.getElementById('DivisionHdn').value == "")
                document.getElementById('RadioButtonList4_0').checked = true;

            document.getElementById('HeadLbl').innerHTML = "";


            document.getElementById("<%=txtEmployeeSearch.ClientID%>").innerHTML = "";
    
            document.getElementById("<%=txtLocation.ClientID%>").innerHTML = "";
            document.getElementById("<%=txtDivision.ClientID%>").innerHTML = "";
        

            document.getElementById("<%=txtEmployeeSearch.ClientID%>").value = "";

            document.getElementById("<%=txtLocation.ClientID%>").value = "";
            document.getElementById("<%=txtDivision.ClientID%>").value = "";
   



        }

        function ValidationReport() {


            if (document.getElementById('ddlFromMonth').value == "0") {
                alert("Please enter Month.");
                document.getElementById('ddlFromMonth').focus();
                return false;
            }


            if (document.getElementById('ddlFromYear').value == "0") {
                alert("Please enter Year.");
                document.getElementById('ddlFromYear').focus();
                return false;
            }
        }



    </script>
    <div style="width: 100%; text-align: center">
        <br />
        <h1 class="heading" style='text-align: center; font-family: tahoma; font-size: x-large;
            width: 100%;'>
            Attendance Summary Report</h1>
        <br />
        <center>
            <div style="width: 100%; text-align: center;" id="DivSearch" runat="server">
                <table class="TableClass" cellspacing="1" align="center" runat="server" id="ShowTable" style="display: none"
                     width="100%">
                    <tr>
                        <td class="TDClassForButton" style="height: 12px; text-align:center;">
                            <%--   <asp:Panel runat="server" ID="HeadPnl" Width="681px">--%>
                            <table class="TableClass" style="width: 40%; height: 75px;padding-left:24%">
                                <tr>
                                    <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;"
                                        class="style26">
                                        <table style="height: 75px; width: 140px;">
                                            <tr>
                                                <td class="style29">
                                                    &nbsp;Employee
                                                </td>
                                            </tr>
                                        <tr>
                                                <td style="text-align: left" class="style29">
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow" CssClass="radiobutton"
                                                        ClientIDMode="Static">
                                                        <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('EmployeeHdn')">All</asp:ListItem>
                                                        <asp:ListItem Value="1" onclick="show1()">Select</asp:ListItem>
                                                       <%-- <asp:ListItem Value="2" onclick="showTextBox()">Select One</asp:ListItem>--%>
                                                        <%--<asp:ListItem Value="2" >Enter Code</asp:ListItem> --%>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="EmployeeHdn" runat="server" ClientIDMode="Static" />
                                    </td>
                                    <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                        border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                        border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                        <table style="height: 75px; width: 140px;">
                                            <tr>
                                                <td>
                                                    &nbsp;Unit
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">
                                                    <asp:RadioButtonList ID="RadioButtonList3" runat="server" CssClass="radiobutton"
                                                        RepeatLayout="Flow" ClientIDMode="Static">
                                                        <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('LocationHdn')">All</asp:ListItem>
                                                        <asp:ListItem Value="1" onclick="show3()">Select</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="LocationHdn" runat="server" ClientIDMode="Static" />
                                    </td>
                                    <td style="width: 10%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                        border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                        border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                        <table style="height: 75px; width: 140px;">
                                            <tr>
                                                <td>
                                                    &nbsp;Entity
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">
                                                    <asp:RadioButtonList ID="RadioButtonList4" runat="server" CssClass="radiobutton"
                                                        RepeatLayout="Flow" ClientIDMode="Static">
                                                        <asp:ListItem Value="0" Selected="True" onclick="ResetHdn('DivisionHdn')">All</asp:ListItem>
                                                        <asp:ListItem Value="1" onclick="show4()">Select</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="DivisionHdn" runat="server" ClientIDMode="Static" />
                                    </td>
                                    <td style="width: 20%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                                        border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                                        border-bottom: lightsteelblue thin solid; font-weight: bold;">
                                        <table width="140px">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlFromMonth" Width="140px" runat="server" class="chosen-select" ClientIDMode="Static">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlFromYear" Width="140px" runat="server" class="chosen-select" ClientIDMode="Static">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: center; border-right: lightsteelblue thin solid; border-top: lightsteelblue thin solid;
                                        border-left: lightsteelblue thin solid; border-bottom: lightsteelblue thin solid;
                                        font-weight: bold;">
                                        <table style="height: 50px;width:140px;">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="View" class="ButtonControl" Style="width: 80%" runat="server" Text="View"
                                                        ClientIDMode="Static" OnClientClick='return ValidationReport()' OnClick="View_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnReset" runat="server" Width="80%" CssClass="ButtonControl" OnClick="btnReset_Click"
                                                        Text="Reset" ClientIDMode="Static" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnClosePage" class="ButtonControl" Style="width: 80%" Text="Close"
                                                        runat="server" OnClick="btnClosePage_Click" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <%--    </asp:Panel>--%>
                            &nbsp; &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="7" style="vertical-align: middle;">
                            <%--           <asp:Panel ID="Panel1" runat="server" ClientIDMode="Static" Style="display: none">--%>
                            <asp:Panel ID="Panel1" runat="server" ClientIDMode="Static" Style="display: none"  >
                            <table style="width: 400px;">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="HeadLbl" runat="server" Text="" Font-Bold="True" Font-Size="Medium"
                                            ClientIDMode="Static"></asp:Label>
                                    </td>
                                </tr>

                                  <tr>
                                    <td>
                                      <asp:TextBox ID="txtEmployeeSearch" runat="server" placeholder="Search" onkeyup="FilterEmployee()" Width="175%"></asp:TextBox>
                                      <asp:TextBox ID="txtLocation" runat="server" placeholder="Search" onkeyup="FilterLocation()" Width="175%"></asp:TextBox>
                                       <asp:TextBox ID="txtDivision" runat="server" placeholder="Search" onkeyup="FilterDivision()" Width="175%"></asp:TextBox>
                                        
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
                                        <asp:ListBox ID="ListBox3" runat="server" ClientIDMode="Static" Font-Bold="True"
                                            Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                        <asp:ListBox ID="ListBox4" runat="server" ClientIDMode="Static" Font-Bold="True"
                                            Font-Names="Courier New" Rows="10" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                        <asp:TextBox ID="txtemp" MaxLength="8" ClientIDMode="Static" onkeypress="return fnCharAlphaNumeric(event)"
                                            runat="server"></asp:TextBox>
                                        &nbsp;

                                         
                                        <asp:ListBox ID="ListBox7" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple" Style="display:none"
                                        Width="100%"></asp:ListBox>

                                        
                                          <asp:ListBox ID="ListBox9" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple" Style="display:none"
                                        Width="100%"></asp:ListBox>
                                                  <asp:ListBox ID="ListBox10" runat="server" ClientIDMode="Static" Font-Bold="True"
                                        Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple" Style="display:none"
                                        Width="100%"></asp:ListBox>
                                                 


                                    </td>
                                </tr>
                                <tr>
                                   
                                                <td align="center" style="height: 37px; vertical-align: bottom;">
                                                    
                                                </td>
                                                <td  align="center" style="height: 37px; vertical-align: bottom; width: 170px;">
                                                <input id="Button1" class="ButtonControl" name="Ok" style="width: 71px" type="button"
                                                        value="OK" onclick="OkClick()" />&nbsp;&nbsp;
                                                    <input id="Button2" class="ButtonControl" name="Cancel" style="width: 71px" type="button"
                                                        value="Cancel" onclick="CancelClick()" />
                                                </td>
                                        
                                    <%--    <td align="center" style="height: 37px; vertical-align: bottom;">
                                        <input id="Button1" class="button" name="Ok" style="width: 71px" type="button" value="OK"
                                            onclick="OkClick()" />
                                    </td>
                                    <td align="center" style="height: 37px; vertical-align: bottom; width: 170px;">
                                        <input id="Button2" class="button" name="Cancel" style="width: 71px" type="button"
                                            value="Cancel" onclick="CancelClick()" />
                                    </td>--%>
                                </tr>
                            </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
        <table align="center" style="width: 90%; margin-left: 5%;">
            <tr>
                <td colspan="5" align="right" style="height: 44px">
                    <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="ButtonControl" Visible="False"
                        OnClick="btnClose_Click" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width:100%;" class="style28">
                    
                   <asp:Panel ID="viewer" runat="server" Height="410px" BorderStyle="Solid" BorderColor="#0066FF" BorderWidth="3px" Width="100%" Visible="false">
                
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Font-Names="Verdana"
                        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                        WaitMessageFont-Size="14pt" BorderStyle="None" Visible="false">
                    </rsweb:ReportViewer>
                  </asp:Panel>
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
                <%--  <asp:Label ID="HeadLbl" runat="server" Font-Bold="True" Font-Size="Medium"  ClientIDMode="Static"></asp:Label> --%><%--	<td></td>--%>
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
    </div>

    
    
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
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style26
        {
            height: 75px;
            width: 9%;
        }
        .TableClass
        {
            height: 259px;
            width: 93%;
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
        #txtCalendarFrom
        {
            width: 108px;
        }
        #txtCalendarTo
        {
            width: 108px;
        }
    </style>
</asp:Content>
