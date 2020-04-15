<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShiftPatternView.aspx.cs"
    EnableEventValidation="false" Inherits="UNO.ShiftPatternView" MasterPageFile="~/ModuleMain.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css" />
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css" />
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <style>
        .caps
        {
            text-transform: uppercase;
        }
    </style>
    <script type="text/javascript">
        function validateShiftEdit() {
            try {
                var isSelected = false;
                var strHDNValue = document.getElementById('HDNSelectedShiftEdit').value
                var selValue = new Array();
                selValue = strHDNValue.split('-');
                var len = selValue.length;
                var src = document.getElementById('lstAShiftEdit');
                var optionvalue = "";
                for (var count = 0; count < src.options.length; count++) {
                    if (src.options[count].selected == true) {
                        if (isSelected == false) {
                            isSelected = true;
                        }
                        var option = src.options[count];
                        optionvalue = option.value;
                    }
                }

                if (selValue[len - 1] == "OFF") {

                    alert("Please select shift applied for OFF day ");
                    //document.getElementById("ShiftLabel").innerHTML = "Please Selecy Shift for OFF";
                    return false;
                }

                var options = document.getElementById("<%=lstSShiftEdit.ClientID%>").options;
                var ddlReport = document.getElementById("<%=cmbShiftPatternTypeEdit.ClientID%>");
                var Value = ddlReport.options[ddlReport.selectedIndex].value;
                var _shiftCount = 0;
                for (var count = 0; count < options.length; count++) {
                    var option = options[count];
                    if (option.value != 'OFF') {
                        _shiftCount++;
                    }
                }
                if (_shiftCount >= 40) {
                    alert("You  can not select more than 40 shift(s).");
                    return false;
                }
                return true;
            }
            catch (e) {
                // alert(e);

            }
        }




        function validateShiftAdd() {
            try {
                var isSelected = false;
                var strHDNValue = document.getElementById('HDNSelectedShiftAdd').value

                var selValue = new Array();
                selValue = strHDNValue.split('-');
                var len = selValue.length;
                var src = document.getElementById('lstAShiftAdd');
                var optionvalue = "";
                for (var count = 0; count < src.options.length; count++) {
                    if (src.options[count].selected == true) {
                        if (isSelected == false) {
                            isSelected = true;
                        }
                        var option = src.options[count];
                        optionvalue = option.value;
                    }
                }

                if (selValue[len - 1] == "OFF") {

                    alert("Please select shift applied for OFF day ");
                    //document.getElementById("ShiftLabel").innerHTML = "Please Selecy Shift for OFF";
                    return false;
                }

                var options = document.getElementById("<%=lstSShiftAdd.ClientID%>").options;
                var ddlReport = document.getElementById("<%=cmbShiftPatternTypeAdd.ClientID%>");
                var Value = ddlReport.options[ddlReport.selectedIndex].value;
                var _shiftCount = 0;
                for (var count = 0; count < options.length; count++) {
                    var option = options[count];
                    if (option.value != 'OFF') {
                        _shiftCount++;
                    }
                }
                if (_shiftCount >= 40) {
                    alert("You  can not select more than 40 shift(s).");
                    return false;
                }
                return true;
            }
            catch (ex) {
                //alert(ex.Message);
            }
        }

        function listbox_move(listID, direction) {
            var listbox = document.getElementById(listID);
            var selIndex = listbox.selectedIndex;

            if (-1 == selIndex) {
                alert("Please select an option to move.");
                return;
            }

            var increment = -1;
            if (direction == 'up') {
                increment = -1;
            }
            else {
                increment = 1;
            }
            if ((selIndex + increment) < 0 ||
        (selIndex + increment) > (listbox.options.length)) {
                return;
            }



            var selValue = listbox.options[selIndex].value;
            var selText = listbox.options[selIndex].text;
            listbox.options[selIndex].value = listbox.options[selIndex + increment].value
            listbox.options[selIndex].text = listbox.options[selIndex + increment].text

            listbox.options[selIndex + increment].value = selValue;
            listbox.options[selIndex + increment].text = selText;

            listbox.selectedIndex = selIndex + increment;
        }
        function setHDNSelectedShift() {

            var list = document.getElementById("<%=lstSShiftAdd.ClientID%>");
            var HDNVal = "";
            for (var i = 0; i < list.options.length; i++) {
                HDNVal = HDNVal + list.options[i].value + '-';
            }
            HDNVal = HDNVal.substring(0, HDNVal.length - 1);

            document.getElementById('HDNSelectedShiftAdd').value = HDNVal;
            document.getElementById("<%=txtshiftAdd.ClientID%>").value = HDNVal;

        }

        function AllSelectedListBoxL() {

            var lstleft = document.getElementById('lstAShiftAdd');
            var lstright = document.getElementById('lstSShiftAdd');
            // for (var i = LstEmployee.options.length - 1; i >= 0; i--) {

            for (var i = 0; i <= lstleft.options.length - 1; i++) {
                var newOption = window.document.createElement('OPTION');
                newOption.text = lstleft.options[i].text;
                newOption.value = lstleft.options[i].value;

                if (lstright.innerHTML.indexOf(lstleft.options[i].text) > 0) {

                }
                else {
                    lstright.options.add(newOption);
                }




            }
            //            $("#LstEmployee").empty();
            //  lstleft.options.length = 0;
            setHDNSelectedShift()
        }
        function AllSelectedListBoxRight() {

            var lstleft = document.getElementById('lstAShiftAdd');
            var lstright = document.getElementById('lstSShiftAdd');



            for (var i = 0; i <= lstright.options.length - 1; i++) {
                var newOption = window.document.createElement('OPTION');
                newOption.text = lstright.options[i].text;
                newOption.value = lstright.options[i].value;
                if (lstleft.innerHTML.indexOf(lstright.options[i].text) > 0) {

                }
                else {
                    lstleft.options.add(newOption);
                }
                //  lstleft.options.add(newOption);
            }
            lstright.options.length = 0;
            setHDNSelectedShift()
        }

        function setHDNSelectedShiftEdit() {

            var list = document.getElementById("<%=lstSShiftEdit.ClientID%>");
            var HDNVal = "";
            for (var i = 0; i < list.options.length; i++) {
                HDNVal = HDNVal + list.options[i].value + '-';
            }
            HDNVal = HDNVal.substring(0, HDNVal.length - 1);

            document.getElementById('HDNSelectedShiftEdit').value = HDNVal;
            document.getElementById("<%=txtshiftEdit.ClientID%>").value = HDNVal;

        }
        function moveShiftToUP() {
            listbox_move('lstSShiftAdd', 'up')
            setHDNSelectedShift()
        }
        function moveShiftToDown() {
            listbox_move('lstSShiftAdd', 'down')
            setHDNSelectedShift()
        }
        function moveShiftToUPEdit() {
            listbox_move('lstSShiftEdit', 'up')
            setHDNSelectedShiftEdit()
        }
        function moveShiftToDownEdit() {
            listbox_move('lstSShiftEdit', 'down')
            setHDNSelectedShiftEdit()
        }

        function moveShiftToSelectedEdit() {

            if (validateOFFOnMoveEdit()) {
                listbox_moveacrossAToS('lstAShiftEdit', 'lstSShiftEdit');
                setHDNSelectedShiftEdit()
            }

        }
        function moveShiftToAvailableEdit() {

            listbox_moveacrossSToA('lstSShiftEdit', 'lstAShiftEdit');
            setHDNSelectedShiftEdit()
        }


        function moveShiftToSelectedAdd() {

            if (validateOFFOnMoveAdd()) {
                listbox_moveacrossAToS('lstAShiftAdd', 'lstSShiftAdd');
                setHDNSelectedShift()
            }

        }
        function moveShiftToAvailableAdd() {

            listbox_moveacrossSToA('lstSShiftAdd', 'lstAShiftAdd');
            setHDNSelectedShift()
        }


        function listbox_moveacrossAToS(sourceID, destID) {
            var src = document.getElementById(sourceID);
            var dest = document.getElementById(destID);

            for (var count = 0; count < src.options.length; count++) {

                if (src.options[count].selected == true) {
                    var option = src.options[count];

                    var newOption = document.createElement("option");
                    newOption.value = option.value;
                    newOption.text = option.text;

                    //newOption.selected = true;
                    try {
                        dest.add(newOption, null); //Standard
                        src.options[count].selected = false;
                        //src.remove(count, null);
                    } catch (error) {
                        dest.add(newOption); // IE only
                        src.options[count].selected = false;
                        //src.remove(count);
                    }
                    if (newOption.text == "")
                        count--;
                }
            }
        }

        function listbox_moveacrossSToA(sourceID, destID) {
            var src = document.getElementById(sourceID);
            var dest = document.getElementById(destID);

            for (var count = 0; count < src.options.length; count++) {

                if (src.options[count].selected == true) {
                    var option = src.options[count];

                    var newOption = document.createElement("option");
                    newOption.value = option.value;
                    newOption.text = option.text;
                    newOption.selected = true;
                    try {
                        //dest.add(newOption, null); //Standard
                        src.options[count].selected = false;
                        src.remove(count, null);
                    } catch (error) {
                        //dest.add(newOption); // IE only
                        src.options[count].selected = false;
                        src.remove(count);
                    }
                    count--;
                }
            }
        }
        function validateOFFOnMoveAdd() {
            var isSelected = false;
            var strHDNValue = document.getElementById('HDNSelectedShiftAdd').value
            var selValue = new Array();
            selValue = strHDNValue.split('-');
            var len = selValue.length;
            var src = document.getElementById('lstAShiftAdd');
            var optionvalue = "";
            for (var count = 0; count < src.options.length; count++) {
                if (src.options[count].selected == true) {
                    if (isSelected == false) {
                        isSelected = true;
                    }
                    var option = src.options[count];
                    optionvalue = option.value;
                }
            }

            if (isSelected == false) {
                alert("Please Select Shift");
            }

            if (selValue[len - 1] == 'OFF' && optionvalue == 'OFF') {
                alert("Please select shift applied for OFF day ");
                //document.getElementById("ShiftLabel").innerHTML = "Please Selecy Shift for OFF";
                return false;
            }

            var options = document.getElementById("<%=lstSShiftAdd.ClientID%>").options;
            var ddlReport = document.getElementById("<%=cmbShiftPatternTypeAdd.ClientID%>");
            var Value = ddlReport.options[ddlReport.selectedIndex].value;
            var _shiftCount = 0;
            for (var count = 0; count < options.length; count++) {
                var option = options[count];
                if (option.value != 'OFF') {
                    _shiftCount++;
                }
            }
            if (_shiftCount >= 40) {
                alert("You  can not select more than 40 shift(s).");
                return false;
            }
            return true;
        }


        function validateOFFOnMoveEdit() {
            var strHDNValue = document.getElementById('HDNSelectedShiftEdit').value
            var selValue = new Array();
            selValue = strHDNValue.split('-');
            var len = selValue.length;
            var src = document.getElementById('lstAShiftEdit');
            var optionvalue = "";
            for (var count = 0; count < src.options.length; count++) {
                if (src.options[count].selected == true) {
                    var option = src.options[count];
                    optionvalue = option.value;
                }
            }
            if (selValue[len - 1] == 'OFF' && optionvalue == 'OFF') {
                alert("Please select shift applied for OFF day ");
                return false;
            }

            var options = document.getElementById("<%=lstSShiftEdit.ClientID%>").options;
            var ddlReport = document.getElementById("<%=cmbShiftPatternTypeEdit.ClientID%>");
            var Value = ddlReport.options[ddlReport.selectedIndex].value;
            var _shiftCount = 0;
            for (var count = 0; count < options.length; count++) {
                var option = options[count];
                if (option.value != 'OFF') {
                    _shiftCount++;
                }
            }
            if (_shiftCount >= 40) {
                alert("You  can not select more than 40 shift(s).");
                return false;
            }
            return true;
        }




        function validateOFF() {
            var _first = "", _sec = "";
            var strHDNValue = document.getElementById('HDNSelectedShiftAdd').value
            var selValue = new Array();
            selValue = strHDNValue.split('-');
            var len = selValue.length;

            if (selValue[len - 1] == 'OFF') {
                alert("Please select shift applied for OFF day ");
                return false;
            }

            for (var count = 0; count < len; count++) {
                _first = selValue[count];
                _sec = selValue[count + 1];
                if (_first == 'OFF' && _sec == 'OFF') {
                    alert("Shift list can not contain two Consecutive OFF.Please select shift applied for OFF day.");
                    return false;
                }
            }
            return true;
        }
        function ValidateDataAdd() {

            if (handleAdd() == false) {
                return false
            }
        }

        function handleAdd() {
            if (!Page_ClientValidate())
                return;
            if (!validateOFF())
                return false;
        }




        function validateOFFEdit() {
            var _first = "", _sec = "";
            var strHDNValue = document.getElementById('HDNSelectedShiftEdit').value
            var selValue = new Array();
            selValue = strHDNValue.split('-');
            var len = selValue.length;

            if (selValue[len - 1] == 'OFF') {
                alert("Please select shift applied for OFF day ");
                return false;
            }

            for (var count = 0; count < len; count++) {
                _first = selValue[count];
                _sec = selValue[count + 1];
                if (_first == 'OFF' && _sec == 'OFF') {
                    alert("Shift list can not contain two Consecutive OFF.Please select shift applied for OFF day.");
                    return false;
                }
            }
            return true;
        }


        function ValidateShiftListBox(sender, args) {

            var options = document.getElementById("<%=lstSShiftAdd.ClientID%>").options;

            var ddlReport = document.getElementById("<%=cmbShiftPatternTypeAdd.ClientID%>");
            var Value = ddlReport.options[ddlReport.selectedIndex].value;
            var _shiftCount = 0;
            for (var count = 0; count < options.length; count++) {
                var option = options[count];
                if (option.value != 'OFF') {
                    _shiftCount++;
                }
            }

            if (options.length == 0) {
                sender.innerHTML = "Please select reader(s).";
                args.IsValid = false;
            }
            else if (_shiftCount > 40) {
                sender.innerHTML = "You can not select more than 40 shift(s).";
                args.IsValid = false;
            }
            else
            { args.IsValid = true; }



        }

        function ValidateShiftListBoxEdit(sender, args) {

            var options = document.getElementById("<%=lstSShiftEdit.ClientID%>").options;
            //            if (options.length == 0) {
            //                sender.innerHTML = "Please select reader(s).";
            //                args.IsValid = false;
            //            }
            //            else {
            //                args.IsValid = true;
            //            }

            var ddlReport = document.getElementById("<%=cmbShiftPatternTypeEdit.ClientID%>");
            var Value = ddlReport.options[ddlReport.selectedIndex].value;
            var _shiftCount = 0;
            for (var count = 0; count < options.length; count++) {
                var option = options[count];
                if (option.value != 'OFF') {
                    _shiftCount++;
                }
            }

            if (options.length == 0) {
                sender.innerHTML = "Please select reader(s).";
                args.IsValid = false;
            }
            else if (_shiftCount > 40) {
                sender.innerHTML = "You can not select more than 40 shift(s).";
                args.IsValid = false;
            }
            else
            { args.IsValid = true; }



        }








    </script>
    <script type="text/javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvTempCard.ClientID %>');

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
    <style>
        .textBold
        {
            text-align: center;
            vertical-align: middle;
            font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;
            font-weight: bold;
            color: Black;
            font-size: x-large;
        }
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.8;
        }
        .watermark
        {
            color: Gray;
            font-size: xx-small;
            height: 17px;
            width: 120px;
            border-radius: 15px;
            margin-right: 10px;
        }
        .searchTextBox
        {
            height: 17px;
            width: 120px;
            font-size: xx-small;
            border-radius: 15px;
            margin-right: 10px;
        }
        .PopupPanel1
        {
            background-color: white;
            border: 5px solid #000000;
            border-radius: 25px;
            color: Black;
            padding: 10px 10px 5px 10px;
            vertical-align: middle;
            text-align: center;
            width: 53%;
        }
        .ButtonControl
        {
            font-size: 11px;
            font-family: Verdana;
            color: Black;
            font-weight: bold;
            background-color: #99CCFF;
            border-color: #99CCFF;
            border-width: 1px; /*Swapnil Start*/
            border-radius: 25px; /*Swapnil End*/
            padding: 2px 10px 2px 10px;
            height: 23px;
        }
        .DivEmpDetails
        {
            text-align: center;
            width: 95%; /*border: 1px solid #333333;*/
            border-radius: 15px;
            background-color: #47A3DA;
            margin: 10px 10px 10px 10px;
            padding: 10px 10px 10px 10px; /*min-height: 200px;*/ /*font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;*/
            box-shadow: 10px 10px 5px #888888;
        }
        .gvHeader
        {
            background-color: transparent;
            border: 0px solid #66B7F5;
            max-height: 29px;
            height: 29px;
            min-height: 29px;
        }
        gvAlternateRow
        {
        }
        .gvRow
        {
            border-bottom: 1px solid #C3C3C3;
            max-height: 26px;
            height: 26px;
            min-height: 26px;
        }
        .gvPager
        {
            vertical-align: bottom;
        }
        .center
        {
            text-align: center;
        }
        .heading
        {
            text-align: center;
            font-family: Arial;
            font-size: 20px;
            font-weight: bold;
            color: RoyalBlue;
            margin-bottom: 0px;
        }
    </style>
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
    .style37
    {
        width: 135px;
    }
    .style38
    {
        height: 43px;
    }
    .style41
    {
        width: 101px;
    }
    </style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Shift Pattern View" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <table style="width: 99%;">
                <tr>
                    <td style="width: 50%; text-align: left;">
                        <asp:Button runat="server" ID="ButtonAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click" />
                        <asp:Button runat="server" ID="ButtonDelete" Text="Delete" CssClass="ButtonControl"
                            OnClick="ButtonDelete_Click" />
                    </td>
                    <td style="width: 50%; text-align: right;">
                        <asp:Button runat="server" ID="cmdReset" Text="Reset" Style="float: right; margin-left: 4px;"
                            CssClass="ButtonControl" OnClick="cmdReset_Click" />
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                            OnClick="btnSearch_Click" TabIndex="3" />
                        <asp:TextBox ID="txtCompanyName" runat="server" Style="float: right;" CssClass="searchTextBox"
                            onkeydown="return (event.keyCode!=13);" TabIndex="1"></asp:TextBox>
                        <asp:TextBox ID="txtCompanyID" runat="server" Style="float: right;" CssClass="searchTextBox"
                            onkeydown="return (event.keyCode!=13);" TabIndex="2"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCompanyID" runat="server" TargetControlID="txtCompanyID"
                            WatermarkText="ID" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCompanyName" runat="server" TargetControlID="txtCompanyName"
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
                                <asp:GridView ID="gvTempCard" runat="server" AutoGenerateColumns="False" Width="100%"
                                    GridLines="None" AllowPaging="True" OnPageIndexChanging="gvCompany_PageIndexChanging"
                                    OnRowCommand="gvCompany_RowCommand" DataKeyNames="SHIFT_PATTERN_ID">
                                    <RowStyle CssClass="gvRow" />
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                    <PagerStyle CssClass="gvPager" />
                                    <EmptyDataTemplate>
                                        <div>
                                            <span>No Records found.</span>
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" SortExpression="Select" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" SortExpression="Edit" ItemStyle-Width="5%" HeaderStyle-CssClass="center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                    Text="Edit" ForeColor="#3366FF" CommandArgument='<%#Eval("SHIFT_PATTERN_ID")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SHIFT_PATTERN_ID" HeaderText="ID" SortExpression="SHIFT_PATTERN_ID"
                                            ItemStyle-Width="10%" ItemStyle-CssClass="caps">
                                            <%--  <ItemStyle HorizontalAlign="Left" Width="10%"  />--%>
                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SHIFT_PATTERN_DESCRIPTION" HeaderText="Description" ItemStyle-Wrap="true"
                                            SortExpression="SHIFT_PATTERN_DESCRIPTION">
                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SHIFT_PATTERN_TYPE" HeaderText="Pattern Type" ItemStyle-Width="10%"
                                            ItemStyle-Wrap="true" SortExpression="SHIFT_PATTERN_TYPE">
                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SHIFT_PATTERN" HeaderText="Shift Pattern" SortExpression="SHIFT_PATTERN"
                                            ItemStyle-Width="10%">
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
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
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            </Triggers>
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
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--       <asp:AsyncPostBackTrigger ControlID="ButtonAdd" />--%>
                <asp:AsyncPostBackTrigger ControlID="ButtonDelete" />
                <asp:AsyncPostBackTrigger ControlID="gvTempCard" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmitAdd" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmitEdit" />
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Panel ID="pnlAddTC" runat="server" CssClass="PopupPanel1" Style="width: 70%;">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="table4" runat="server" height="100%" border="0" cellpadding="5" cellspacing="5"
                    class="TableClass">
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            Shift Pattern ID:<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtSpidAdd" runat="server" onkeypress="return IsAlphanumericWithoutspace(event)"
                                CssClass="TextControl" MaxLength="8" TabIndex="1" Style="text-transform: uppercase;"
                                ClientIDMode="Static"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtShiftID" runat="server" ControlToValidate="txtSpidAdd"
                                Display="None" ErrorMessage="Please enter Shift ID" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                TargetControlID="rfvtxtShiftID" Enabled="True" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtdescriptionAdd" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="8" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescriptionAdd" runat="server" ControlToValidate="txtdescriptionAdd"
                                Display="None" ErrorMessage="Please enter Shift Description" ForeColor="Red"
                                SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                TargetControlID="rfvDescriptionAdd" Enabled="True" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;" class="style37">
                            Pattern Type :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="padding-left: 10px;">
                            <asp:DropDownList ID="cmbShiftPatternTypeAdd" runat="server" AutoPostBack="True"
                                Width="172px" OnSelectedIndexChanged="cmbShiftPatternTypeAdd_SelectedIndexChanged"
                                TabIndex="3">
                                <asp:ListItem Value="0" Selected="True">Select One</asp:ListItem>
                                <asp:ListItem Value="DL">DAILY</asp:ListItem>
                                <asp:ListItem Value="WK">WEEKLY</asp:ListItem>
                                <asp:ListItem Value="BW">BI-WEEKLY</asp:ListItem>
                                <asp:ListItem Value="MN">MONTHLY</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvShiftPatternAdd" runat="server" ControlToValidate="cmbShiftPatternTypeAdd"
                                Display="None" ErrorMessage="Please Select Shift Pattern Type" ForeColor="Red"
                                SetFocusOnError="True" ValidationGroup="Add" InitialValue="0"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                TargetControlID="rfvShiftPatternAdd" Enabled="True" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;" class="style37">
                            Shift :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="padding-left: 10px;">
                            <table>
                                <tr>
                                    <td>
                                        <span id="ShiftLabel">Available Shift</span>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        Selected Shift<asp:HiddenField ID="HDNSelectedShiftAdd" runat="server" ClientIDMode="Static" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lstAShiftAdd" runat="server" Height="110px" ForeColor="Black" Width="212px"
                                            Font-Names="Courier New" ClientIDMode="Static" TabIndex="4" SelectionMode="Multiple"
                                            OnSelectedIndexChanged="lstAShiftAdd_SelectedIndexChanged"></asp:ListBox>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td class="TDClassControl">
                                                    <input type="button" name="cmdAllReaderRight" value=">>" onclick="AllSelectedListBoxL()"
                                                        class="ButtonControl" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TDClassControl">
                                                    <input type="button" name="cmdReaderRight" value=" > " onclick="moveShiftToSelectedAdd()"
                                                        class="ButtonControl" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TDClassControl">
                                                    <input type="button" name="cmdReaderleft" value=" &lt; " onclick="moveShiftToAvailableAdd()"
                                                        class="ButtonControl" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TDClassControl">
                                                    <input type="button" name="cmdAllReaderLeft" value="<<" onclick="AllSelectedListBoxRight()"
                                                        class="ButtonControl" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstSShiftAdd" runat="server" Width="212px" ForeColor="Black" Font-Names="Courier New"
                                            CssClass="TextControl" Height="110px" ClientIDMode="Static" CausesValidation="True"
                                            TabIndex="7" SelectionMode="Multiple"></asp:ListBox>
                                        <asp:CustomValidator ID="cvlstShiftAdd" runat="server" ErrorMessage="Please select Shift."
                                            ControlToValidate="lstSShiftAdd" Display="None" ForeColor="RED" ValidateEmptyText="true"
                                            ClientValidationFunction="ValidateShiftListBox" ValidationGroup="Add"></asp:CustomValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcerlstShiftAdd" runat="server" TargetControlID="cvlstShiftAdd"
                                            PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td class="TDClassControl">
                                                    <%--<asp:Button ID="cmdReaderRight" runat="server"  
              Text="&gt;" Width="19px" CssClass="ButtonControl" OnClientClick="moveShift()"
              CausesValidation="False"  />--%>
                                                    <input type="button" name="cmdShiftUp1" value="^" onclick="moveShiftToUP()" class="ButtonControl"
                                                        tabindex="8" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TDClassControl">
                                                    <%--<asp:Button ID="cmdReaderLeft" runat="server" Text="&lt;" Width="19px" 
                CssClass="ButtonControl" 
              CausesValidation="False" />--%>
                                                    <input type="button" name="cmdShiftDown1" value="v" onclick="moveShiftToDown()" class="ButtonControl"
                                                        title="Down" tabindex="9" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;" class="style37">
                            Shift Pattern :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="padding-left: 10px;">
                            <asp:TextBox CssClass="TextControl" ID="txtshiftAdd" runat="server" Width="90%" ClientIDMode="Static"
                                ReadOnly="True" TextMode="MultiLine" Height="47px" TabIndex="10" Style="max-width: 484px;
                                min-width: 484px; max-height: 47px; min-height: 47px;"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;" class="style38">
                            <asp:Button ID="btnSubmitAdd" runat="server" CssClass="ButtonControl" TabIndex="11"
                                Text="Save" OnClick="btnSaveAdd_Click" ValidationGroup="Add" OnClientClick="validateShiftAdd();" />
                            <asp:Button ID="btnCancelAd" runat="server" CssClass="ButtonControl" TabIndex="12"
                                Text="Cancel" OnClick="btnCancelAd_Click" />
                            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Label ID="lblErrorAdd" runat="server" Visible="False" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <%--   <asp:AsyncPostBackTrigger ControlID="btnSubmitAdd" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelAd" />--%>
                <asp:PostBackTrigger ControlID="cmbShiftPatternTypeAdd" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button1" runat="server" CssClass="ButtonControl" Text="Save" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddTC" runat="server" PopupControlID="pnlAddTC"
        BackgroundCssClass="modalBackground" Enabled="true" CancelControlID="btnCancelAd"
        TargetControlID="Button1">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlEditTC" runat="server" CssClass="PopupPanel1" Style="width: 70%;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table id="table1" runat="server" height="90%" border="0" cellpadding="5" cellspacing="5"
                    class="TableClass">
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;" class="style37">
                            Shift Pattern ID:<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtSpidEdit" runat="server" onkeypress="return IsAlphanumericWithoutspace(event)"
                                CssClass="TextControl" MaxLength="8" TabIndex="1" Style="text-transform: uppercase;"
                                ClientIDMode="Static"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtShiftidEdit" runat="server" ControlToValidate="txtSpidEdit"
                                Display="None" ErrorMessage="Please enter Shift ID" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Edit" Enabled="False"></asp:RequiredFieldValidator>
                            <%--                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" 
                        runat="server" TargetControlID="rfvtxtShiftidEdit" Enabled="True" PopupPosition="Right">   </ajaxToolkit:ValidatorCalloutExtender>   --%>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;" class="style37">
                            Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtdescriptionEdit" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                MaxLength="8" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtDescriptionEdit" runat="server" ControlToValidate="txtdescriptionEdit"
                                Display="None" ErrorMessage="Please enter Shift Description" ForeColor="Red"
                                SetFocusOnError="True" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                TargetControlID="rfvtxtDescriptionEdit" Enabled="True" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;" class="style37">
                            Pattern Type :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="padding-left: 10px;">
                            <asp:DropDownList ID="cmbShiftPatternTypeEdit" runat="server" CssClass="ComboControl"
                                class="chosen-select" Width="172px" OnSelectedIndexChanged="ShiftPatternTypeEdit_SelectedIndexChanged"
                                AutoPostBack="True" TabIndex="3">
                                <asp:ListItem Selected="True" Value="0">Select One</asp:ListItem>
                                <asp:ListItem Value="DL">DAILY</asp:ListItem>
                                <asp:ListItem Value="WK">WEEKLY</asp:ListItem>
                                <asp:ListItem Value="BW">BI-WEEKLY</asp:ListItem>
                                <asp:ListItem Value="MN">MONTHLY</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvtxtShiftPatternEdit" runat="server" ControlToValidate="cmbShiftPatternTypeEdit"
                                Display="None" ErrorMessage="Please Select Shift Pattern Type" ForeColor="Red"
                                SetFocusOnError="True" ValidationGroup="Edit" InitialValue="0"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                TargetControlID="rfvtxtShiftPatternEdit" Enabled="True" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;" class="style37">
                            Shift :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="padding-left: 10px;">
                            <table>
                                <tr>
                                    <td>
                                        Available Shift
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        Selected Shift<asp:HiddenField ID="HDNSelectedShiftEdit" runat="server" ClientIDMode="Static" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lstAShiftEdit" runat="server" Height="110px" Width="212px" ForeColor="Black"
                                            Font-Names="Courier New" CssClass="TextControl" ClientIDMode="Static" TabIndex="4"
                                            SelectionMode="Multiple" OnSelectedIndexChanged="lstAShiftEdit_SelectedIndexChanged">
                                        </asp:ListBox>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td class="TDClassControl">
                                                    <%--<asp:Button ID="cmdReaderRight" runat="server"  
              Text="&gt;" Width="19px" CssClass="ButtonControl" OnClientClick="moveShift()"
              CausesValidation="False"  />--%>
                                                    <input type="button" name="cmdReaderRightedit" value=">" onclick="moveShiftToSelectedEdit()"
                                                        class="ButtonControl" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TDClassControl">
                                                    <%--<asp:Button ID="cmdReaderLeft" runat="server" Text="&lt;" Width="19px" 
                CssClass="ButtonControl" 
              CausesValidation="False" />--%>
                                                    <input type="button" name="cmdReaderleftedit" value="&lt;" onclick="moveShiftToAvailableEdit()"
                                                        class="ButtonControl" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstSShiftEdit" runat="server" Width="212px" ForeColor="Black" Font-Names="Courier New"
                                            CssClass="TextControl" Height="110px" ClientIDMode="Static" CausesValidation="True"
                                            TabIndex="7" SelectionMode="Multiple" OnSelectedIndexChanged="lstSShiftEdit_SelectedIndexChanged">
                                        </asp:ListBox>
                                        <asp:CustomValidator ID="cvLstShiftEdit" runat="server" ErrorMessage="Please select Shift Type."
                                            ControlToValidate="lstSShiftEdit" Display="None" ForeColor="RED" ValidateEmptyText="true"
                                            ClientValidationFunction="ValidateShiftListBoxEdit" ValidationGroup="Edit"></asp:CustomValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                            TargetControlID="cvLstShiftEdit" PopupPosition="Right">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td class="TDClassControl">
                                                    <%--<asp:Button ID="cmdReaderRight" runat="server"  
              Text="&gt;" Width="19px" CssClass="ButtonControl" OnClientClick="moveShift()"
              CausesValidation="False"  />--%>
                                                    <input type="button" name="cmdShiftU1p" value="^" onclick="moveShiftToUPEdit()" class="ButtonControl"
                                                        tabindex="8" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TDClassControl">
                                                    <%--<asp:Button ID="cmdReaderLeft" runat="server" Text="&lt;" Width="19px" 
                CssClass="ButtonControl" 
              CausesValidation="False" />--%>
                                                    <input type="button" name="cmdShiftDown" value="v" onclick="moveShiftToDownEdit()"
                                                        class="ButtonControl" title="Down" tabindex="9" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;" class="style37">
                            Shift Pattern :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td class="TDClassControl" style="padding-left: 10px;">
                            <asp:TextBox CssClass="TextControl" ID="txtshiftEdit" runat="server" Width="90%"
                                ClientIDMode="Static" ReadOnly="True" TextMode="MultiLine" Height="47px" TabIndex="10"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnSubmitEdit" runat="server" CssClass="ButtonControl" TabIndex="11"
                                Text="Save" OnClick="btnSaveEdit_Click" ValidationGroup="Edit" OnClientClick="validateShiftEdit();" />
                            <asp:Button ID="btnCancelEdi" runat="server" CssClass="ButtonControl" TabIndex="12"
                                Text="Cancel" OnClick="btnCancelEdi_Click" />
                            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Label ID="lblErrorEdit" runat="server" Visible="False" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSubmitEdit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelEdi" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:LinkButton ID="lnkDummyEdit" runat="server" Style="display: none;">edit</asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="mpeEditTC" runat="server" TargetControlID="lnkDummyEdit"
        PopupControlID="pnlEditTC" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancelEdi">
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
