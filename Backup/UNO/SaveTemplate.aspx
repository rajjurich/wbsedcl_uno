<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="SaveTemplate.aspx.cs" Inherits="UNO.SaveTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var isItemSelected = false;
        function ClientItemSelected(sender, e) {
            $get("<%=hfCustomerId.ClientID %>").value = e.get_value();
            isItemSelected = true;
        }

        function suggestionListPopulating(source, e) {

            var textboxControl = $(source.get_element()); // Get the textbox control.

            textboxControl.css('background', '#FFF url(img/progress-anim.gif) no-repeat right '); // Put a background image for the textbox.
            isItemSelected = false;
        }

        function suggestionListPopulated(source, e) {

            var textboxControl = $(source.get_element()); // Get the textbox control.

            textboxControl.css('background-image', 'none'); // Remove background image from the textbox.
            isItemSelected = false;
        }
        function ClientItemSelected1(sender, e) {
            $get("<%=hfEmpName.ClientID %>").value = e.get_value();
            isItemSelected = true;

        }

       
 
    

    </script>
    <script type="text/javascript">
        if ("ActiveXObject" in window) { }
        else { alert("This page will work only in IE 8 & 9"); window.history.back(); }
      
    </script>
    <script type="text/javascript">

        function Print() {
            var conn = document.getElementById('<%= hfconnection.ClientID %>').value;
            var EmpName = "";
            var EmpCode = "";
            var Category = "";
            var list = document.getElementById('<%= lstEmplyee.ClientID %>');
            var count = $("#lstEmplyee option").length;
            var Return = 0;
            var WshShell = new ActiveXObject("WScript.Shell");
            $("#lstEmplyee option").each(function () {
                if (Return != 1) {
                    var values;

                    var selectValues = $(this).text();

                    if (selectValues != "") {
                        if (selectValues.indexOf("|") > 1) {
                            values = selectValues.split("|");
                            EmpCode = values[0];
                            EmpName = values[1].replace(/ /g, ',');


                        }
                    }
                    try {
                        Category = "0";
                        var anchors = document.getElementsByTagName("a");
                        for (var i = 0; i < anchors.length; i++) {
                            anchors[i].onclick = function () { return (false); };
                        }
                        var PrintFromPageName="Card";
                        Return = WshShell.Run("C:/CardDesign/KCards.exe " + EmpCode + " Print " + conn + " " + EmpName + " " + Category + " " + PrintFromPageName, 1, true);

                        if (Return != "1") {
                            $(this).remove();
                        }
                        var anchors = document.getElementsByTagName("a");
                        for (var i = 0; i < anchors.length; i++) {
                            anchors[i].onclick = function () { return (true); };
                        }
                    } catch (e) {
                        alert(e.Message);
                    }
                }
            });
        }
        function Add() {

            if (!isItemSelected) {
                alert("Please select item from the list only!");
            }
            else {

                var Check = document.getElementById('<%= rd1.ClientID %>').checked;
                var list = document.getElementById('<%= lstEmplyee.ClientID %>');
                if (Check == true) {
                    var empName = document.getElementById('<%= txtEmpName.ClientID %>').value;

                    if (empName != "") {

                        if (empName.indexOf("|") > 1) {

                            var values = empName.split("|");

                            var FinalValue = $.trim(values[1]) + "   |   " + values[0];
                            var count = 0;
                            for (var i = 0; i < list.options.length; i++) {
                                if ($.trim(list.options[i].value) == $.trim(FinalValue)) {
                                    count = 1;
                                    alert("Card Holder already exist.");
                                }
                            }
                            if (count == 0) {
                                list.add(new Option(FinalValue, FinalValue));

                            }
                            document.getElementById('<%= txtEmpName.ClientID %>').value = "";
                            isItemSelected = false;
                            return false;
                        }
                    }
                }
                else {
                    var EmpSearch = document.getElementById('<%= txtEmpSearch.ClientID %>').value;
                    if (EmpSearch != "") {

                        var count = 0;
                        for (var i = 0; i < list.options.length; i++) {

                            if ($.trim(list.options[i].value) == $.trim(EmpSearch)) {
                                count = 1;
                                alert("Card Holder already exist.");
                            }
                        }
                        if (count == 0) {
                            list.add(new Option(EmpSearch, EmpSearch));

                        }
                        document.getElementById('<%= txtEmpSearch.ClientID %>').value = "";
                        isItemSelected = false;
                    }
                }

            }
        }
        function Remove() {
            var list = document.getElementById('<%= lstEmplyee.ClientID %>');

            $("#lstEmplyee option:selected").each(function () {
                $(this).remove();

            });

            return false;

        }

    </script>
    <script type="text/javascript">
        function VisibleTextBox() {
            var rd1 = document.getElementById('<%= rd1.ClientID %>');
            var txtEmpName = document.getElementById('<%= txtEmpName.ClientID %>');
            var txtEmpSearch = document.getElementById('<%= txtEmpSearch.ClientID %>');
            if (rd1.checked) {
                txtEmpName.disabled = false;
                txtEmpSearch.disabled = true;
            }
            else {
                txtEmpName.disabled = true;
                txtEmpSearch.disabled = false;
            }
        }

      
    </script>
    <style type="text/css">
        .displayNone
        {
            display: none;
        }
        .displayBlock
        {
            display: block;
        }
        .ModalPopupBG
        {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .HellowWorldPopup
        {
            min-width: 200px;
            min-height: 150px;
            background: white;
            border: 1px solid black;
            border-bottom-style: groove;
            border-radius: 20px;
            padding: 10px;
            box-shadow: inset 0 -5px 15px rgba(255,255,255,0.4), inset -2px -1px 40px rgba(0,0,0,0.4), 0 0 1px #000;
        }
        .ModalButton
        {
            display: none;
        }
        .SelectEmp
        {
            margin-top: 15px;
        }
        .listCss
        {
            height: 2px;
        }
        .autocomplete
        {
            margin: 0px !important;
            background-color: White;
            color: windowtext;
            border: buttonshadow;
            border-width: 1px;
            border-style: solid;
            cursor: 'default';
            overflow: auto;
            font-family: Courier New; /* font-size:13px;*/
            text-align: left;
            list-style-type: none;
            margin-left: 1px;
            padding-left: 1px;
            max-height: 200px;
            width: auto;
        }
        .CssListBox
        {
            border: 1px solid #00AFDF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfconnection" runat="server" />
    <asp:HiddenField ID="hfEmpcode" runat="server" />
    <asp:HiddenField ID="hfCategory" runat="server" />
    <div style="margin-top: 20px">
        <div style="margin-bottom: 5px; margin-left: 4%">
        </div>
        <input runat="server" id="btnSelectEmp" type="button" value="Select employee to print card"
            clientidmode="Static" class="ButtonControl" style="margin-top: 20px; display: none" />
        <div id="empDetail" runat="server" style="margin-left: 4%;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div style="width: 100%; height: auto; margin-bottom: 30px; margin-top: 15px;">
                        <div style="width: 35%; float: left; height: 325px; border: 1px solid #00AFDF; padding-left: 1%;
                            padding-top: 0.5%">
                            <asp:RadioButton ID="rd1" GroupName="emp" runat="server" Text=" Search  card holder by employee name:"
                                Checked="true" onclick="VisibleTextBox();"></asp:RadioButton>
                            <br />
                            <asp:TextBox ID="txtEmpName" runat="server" Width="97%" Style="font-family: Courier New"
                                onblur="checkItemSelected();"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="GetdataByName"
                                MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="1"
                                FirstRowSelected="true" CompletionListCssClass="autocomplete" OnClientPopulating="suggestionListPopulating"
                                OnClientPopulated="suggestionListPopulated" TargetControlID="txtEmpName" UseContextKey="True"
                                OnClientItemSelected="ClientItemSelected1">
                            </ajaxToolkit:AutoCompleteExtender>
                            <br />
                            <br />
                            <asp:RadioButton ID="rd2" GroupName="emp" runat="server" Text="Search card holder by employee id:"
                                onclick="VisibleTextBox()"></asp:RadioButton>
                            <br />
                            <asp:TextBox ID="txtEmpSearch" runat="server" Width="97%" Enabled="false" Style="font-family: Courier New"
                                onblur="checkItemSelected();"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" FirstRowSelected="true"
                                runat="server" ServiceMethod="GetdataById" MinimumPrefixLength="1" CompletionInterval="10"
                                EnableCaching="true" CompletionSetCount="1" CompletionListCssClass="autocomplete"
                                OnClientPopulating="suggestionListPopulating" OnClientPopulated="suggestionListPopulated"
                                TargetControlID="txtEmpSearch" UseContextKey="True" OnClientItemSelected="ClientItemSelected">
                            </ajaxToolkit:AutoCompleteExtender>
                        </div>
                        <div style="float: left; width: 25%; vertical-align: middle; text-align: center;">
                            <input type="button" id="btnSubmit" class="ButtonControl" value=">>" onclick="return Add()" />
                            <input type="button" id="btnRemove1" class="ButtonControl" value="<<" onclick="return Remove()" />
                        </div>
                        <div>
                            <asp:ListBox ID="lstEmplyee" runat="server" Height="325px" Width="35%" CssClass="CssListBox"
                                ClientIDMode="Static" SelectionMode="Multiple"></asp:ListBox>
                        </div>
                        <div style="width: 100%; text-align: center;">
                            <input type="button" id="btnPrint" onclick="return Print();" value="Print Card" class="ButtonControl" />
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="hfCustomerId" runat="server" />
        <asp:HiddenField ID="hfEmpName" runat="server" />
    </div>
</asp:Content>
