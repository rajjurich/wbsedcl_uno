<%--<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" 
CodeBehind="EPDViewNew.aspx.cs" Inherits="UNO.EPDViewNew" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="EPDViewNew.aspx.cs" Inherits="UNO.EPDViewNew" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--  <link rel="stylesheet" href="Scripts/Choosen/docsupport/style.css">
    <link rel="stylesheet" href="Scripts/Choosen/docsupport/prism.css">--%>
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
       
    </style>
    <script src="Scripts/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvEmployee.ClientID %>');

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

        function GetValidEmpAge() {
            if (!getage())
                return false;
            else
                return true;
        }

      
        function getage() {
            var start = document.getElementById('DOB').value;

            var arrDate = start.split('/');

            var d1 = arrDate[2];
            // alert(d1);

            var ends = document.getElementById('joindt').value;

            var arrDate1 = ends.split('/');

            var d2 = arrDate1[2];

            // alert(d2);

            var d3 = d2 - d1;

            if (d3 < 21) {
                alert('Age cannot be less than 21');
                document.getElementById('joindt').value = "";
                document.getElementById('joindt').focus();

                return false;
            }
            return true;
        }

        function isValidateIssueDate(oSrc, args) {
            var formName = document.aspnetForm;
            if (!CompareDates(document.getElementById('DOB'), document.getElementById('joindt')))
                args.IsValid = false;
            else
                args.IsValid = true;
        }
        function CompareDates(d1, d2) {
            var start = d1.value.toUpperCase();
            var end = d2.value.toUpperCase();
            var arrDate = start.split('/');
            var arrDate1 = end.split('/');
            var d1 = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
            var d2 = new Date(arrDate1[2], arrDate1[1] - 1, arrDate1[0]);
            var d3 = d2 - d1;



            //alert(d3.)
            if (d1 >= d2) {
                return false;
            }
            return true;
        }



        function cHK_SYNC_ADD(cHK_CTRL) {

            if (cHK_CTRL.checked) {


                document.getElementById('EAD_ADDRESS2').value = document.getElementById('EAD_ADDRESS1').value
                document.getElementById('EAD_PIN2').value = document.getElementById('EAD_PIN1').value
                document.getElementById('EAD_CITY2').value = document.getElementById('EAD_CITY1').value
                document.getElementById('EAD_STATE2').value = document.getElementById('EAD_STATE1').value
                document.getElementById('EAD_COUNTRY2').value = document.getElementById('EAD_Country1').value
                document.getElementById('EAD_Phone3').value = document.getElementById('EAD_Phone1').value
                document.getElementById('EAD_Phone4').value = document.getElementById('EAD_Phone2').value


            }
            else {

                document.getElementById('EAD_ADDRESS2').value = ""
                document.getElementById('EAD_CITY2').value = ""
                document.getElementById('EAD_STATE2').selectedIndex = 0;
                document.getElementById('EAD_COUNTRY2').value = ""
                document.getElementById('EAD_Phone3').value = ""
                document.getElementById('EAD_Phone4').value = ""
                document.getElementById('EAD_PIN2').value = ""
            }
        }


        function SetMode() {
            document.getElementById('hdnEmployeeMode').value = "Modify";
        }


        function clearFunction2() {
            //            document.getElementById('messageDiv').innerHTML = "&nbsp;&nbsp;";
            document.getElementById('EPD_EMPLOYEEID').value = "";
            // document.getElementById('ContentPlaceHolder1_EPD_EMPLOYEEID').value = "";
            document.getElementById('EPD_CARD_NO').value = "";
            document.getElementById('EPD_FIRST_NAME').value = "";
            document.getElementById('EPD_LAST_NAME').value = "";
            document.getElementById('EPD_MIDDLE_NAME').value = "";
            document.getElementById('EPD_PREVIOUS_CODE').value = "";
            document.getElementById('EPD_TEMP_CARD_NO').value = "";
            document.getElementById('EPD_LAST_NAME').value = "";
            document.getElementById('EPD_GENDER').value = 0;
            document.getElementById('DOB').value = "";
            document.getElementById('EPD_NICKNAME').value = "";
            document.getElementById('EPD_MARITAL_STATUS').value = "Select";
            document.getElementById('EPD_SALUTATION').value = "";
            document.getElementById('EPD_REFERENCE_ONE').value = "";
            document.getElementById('EPD_REFERENCE_TWO').value = "";
            document.getElementById('EPD_RELIGION').value = "Select";
            document.getElementById('EPD_EMAIL').value = "";
            document.getElementById('EPD_DOMICILE').value = "";
            document.getElementById('EPD_PAN').value = "";
            document.getElementById('EPD_BLOODGROUP').value = "";
            document.getElementById('EPD_DOCTOR').value = "";
            document.getElementById('EAD_ADDRESS1').value = "";
            document.getElementById('EAD_CITY1').value = "";
            document.getElementById('EAD_PIN1').value = "";
            document.getElementById('EAD_STATE1').value = "Select";
            document.getElementById('EAD_Country1').value = "";
            document.getElementById('EAD_Phone1').value = "";
            document.getElementById('EAD_Phone2').value = "";
            document.getElementById('EAD_ADDRESS2').value = "";
            document.getElementById('EAD_CITY2').value = "";
            document.getElementById('EAD_PIN2').value = "";
            document.getElementById('EAD_STATE2').value = "Select";
            document.getElementById('EAD_COUNTRY2').value = "";
            document.getElementById('EAD_Phone3').value = "";
            document.getElementById('EAD_Phone4').value = "";
            document.getElementById('joindt').value = "";
            document.getElementById('Confdt').value = "";
            document.getElementById('Retdt').value = "";
            document.getElementById('ddlreason').value = "Select";
            document.getElementById('ddlcompany').value = "Select";
            document.getElementById('ddllocation').value = "Select";
            document.getElementById('ddldivision').value = "Select";
            document.getElementById('ddldesignation').value = "Select";
            document.getElementById('ddldepartment').value = "Select";
            document.getElementById('ddlgroup').value = "Select";
            document.getElementById('ddlcategory').value = "Select";
            document.getElementById('ddlgrade').value = "Select";
            document.getElementById('ddlstatus').value = "Select";

            // document.getElementById('imgEmployeeImage').ImageUrl = "";
            //        imgEmployeeImage
        }


        function clearviewstate() {
            document.getElementById('AppMode').value = null;
        }

    </script>
    <script type="text/javascript" language="javascript">
        function countrySubmit() {

            var chk = document.getElementById('ChkAddress').checked;


            if (chk.toString() == "true") {

                var UI = document.getElementById('EAD_Country1').value;

                document.getElementById('EAD_COUNTRY2').value = UI;
            }


        }


        function citySubmit() {

            var chk = document.getElementById('ChkAddress').checked;


            if (chk.toString() == "true") {

                var UI = document.getElementById('EAD_CITY1').value;

                document.getElementById('EAD_CITY2').value = UI;
            }


        }




        //Added by Pooja Yadav
        function stateSubmit(Add) {

            var chk = document.getElementById('ChkAddress').checked;
            if (Add == "L") {
                var UI = document.getElementById('EAD_STATE1').value;

                if (chk.toString() == "true")
                    document.getElementById('EAD_STATE2').value = UI;

                if (UI == "OT")
                    document.getElementById('EAD_Country1').value = 'Others';

                else
                    document.getElementById('EAD_Country1').value = 'India';
            }
            else if (Add = "P") {
                var UI = document.getElementById('EAD_STATE2').value;



                if (UI == "OT")
                    document.getElementById('EAD_COUNTRY2').value = 'Others';

                else
                    document.getElementById('EAD_COUNTRY2').value = 'India';
            }

        }



        //Added by Pooja Yadav
        function chkConfirmation() {

            var status = document.getElementById('ddlstatus').value;
            if (status == "C") {
                document.getElementById('<%=Confdt1.ClientID %>').disabled = false;
            }
            else {
                document.getElementById('<%=Confdt1.ClientID %>').disabled = true;
                document.getElementById('<%=Confdt1.ClientID %>').value = "";
            }

        }




        function pinSubmit() {

            var chk = document.getElementById('ChkAddress').checked;


            if (chk.toString() == "true") {

                var UI = document.getElementById('EAD_PIN1').value;

                document.getElementById('EAD_PIN2').value = UI;
            }


        }

        function phno1Submit() {

            var chk = document.getElementById('ChkAddress').checked;


            if (chk.toString() == "true") {

                var UI = document.getElementById('EAD_Phone1').value;

                document.getElementById('EAD_Phone3').value = UI;
            }


        }

        function phno2Submit() {

            var chk = document.getElementById('ChkAddress').checked;


            if (chk.toString() == "true") {

                var UI = document.getElementById('EAD_Phone2').value;

                document.getElementById('EAD_Phone4').value = UI;
            }


        }

        function addressSubmit() {

            var chk = document.getElementById('ChkAddress').checked;


            if (chk.toString() == "true") {

                var UI = document.getElementById('EAD_ADDRESS1').value;

                document.getElementById('EAD_ADDRESS2').value = UI;
            }


        }


        function ChkSelect() {

            var chk = document.getElementById('ChkAddress').checked;

            if (chk.toString() == "true") {


                document.getElementById('EAD_ADDRESS2').disabled = false;

                document.getElementById('EAD_CITY2').disabled = false;
                document.getElementById('EAD_PIN2').disabled = false;
                document.getElementById('EAD_STATE2').disabled = false;
                document.getElementById('EAD_COUNTRY2').disabled = false;
                document.getElementById('EAD_Phone3').disabled = false;
                document.getElementById('EAD_Phone4').disabled = false;


            }
            else {
                document.getElementById('EAD_ADDRESS2').disabled = true;
                document.getElementById('EAD_CITY2').disabled = true;
                document.getElementById('EAD_PIN2').disabled = true;
                document.getElementById('EAD_STATE2').disabled = true;
                document.getElementById('EAD_COUNTRY2').disabled = true;
                document.getElementById('EAD_Phone3').disabled = true;
                document.getElementById('EAD_Phone4').disabled = true;

            }


        }



        function AdjustWidth(ddl) {
            var maxWidth = 0;
            for (var i = 0; i < ddl.length; i++) {
                if (ddl.options[i].text.length > maxWidth) {
                    maxWidth = ddl.options[i].text.length;
                }
            }
            ddl.style.width = maxWidth * 8 + "px";

        }


        function retdt1change() {
            var redt = document.getElementById('Retdt1').value;
            var reason = document.getElementById('ddlreason').value;



            if (redt != "") {


                document.getElementById('ddlreason').disabled = false;


            }
            else {
                document.getElementById('ddlreason').disabled = true;
                document.getElementById('ddlreason').selectedIndex = 0;

            }



        }

        //Added by Pooja Yadav to check DOB
        function chkDOBDateRange(source, args) {

            var yyyy = new Date().getFullYear().toString();
            var mm = (new Date().getMonth() + 1).toString();
            var dd = new Date().getDate().toString();
            var Month = (mm[1] ? mm : "0" + mm[0]);
            var ToDate = (dd[1] ? dd : "0" + dd[0]);

            var startDate = ToDate + "/" + Month + "/" + yyyy;
            var EndDate = args.Value;
            //Year,month,date
            var strt = new Date(startDate.substring(6, 10), parseInt(startDate.substring(3, 5)) - 1, startDate.substring(0, 2));
            var End = new Date(EndDate.substring(6, 10), parseInt(EndDate.substring(3, 5)) - 1, EndDate.substring(0, 2));

            if (startDate == EndDate) {
                args.IsValid = true;
            }
            else if (strt < End) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }

        }
        function ResetAll() {

            $('#' + ["<%=txtCompanyID.ClientID%>", "<%=txtCompanyName.ClientID%>"].join(', #')).prop('value', "");
            $("select" + "#" + "<%=ddlEmpStatus.ClientID%>").prop('selectedIndex', 0);
            $('#' + ["<%=txtCompanyID.ClientID%>"]).focus();
            $('#' + ["<%=txtCompanyName.ClientID%>"]).focus();
            document.getElementById('<%=lblMessages.ClientID%>').innerHTML = "";
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvEmployee.ClientID%>').focus();
            return false;
        }


    </script>
    <style type="text/css">
        .ajax__myTab .ajax__tab_header
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: bold;
            color: #000000;
            border-left: solid 1px #666666;
            border-bottom: thin 1px #666666;
        }
        .ajax__myTab .ajax__tab_outer
        {
            padding-right: 4px;
            height: 20px;
            background-color: #fff;
            margin-right: 1px;
            border-right: solid 1px #666666;
            border-top: solid 1px #666666;
        }
        .ajax__myTab .ajax__tab_inner
        {
            padding-left: 4px;
            background-color: #fff;
        }
        .ajax__myTab .ajax__tab_tab
        {
            height: 18px;
            padding: 4px;
            margin: 0;
            color: Black;
        }
        .ajax__myTab .ajax__tab_hover .ajax__tab_outer
        {
            background-color: #c9c9c9;
        }
        .ajax__myTab .ajax__tab_hover .ajax__tab_inner
        {
            background-color: #c9c9c9;
        }
        .ajax__myTab .ajax__tab_hover .ajax__tab_tab
        {
            background-color: #c9c9c9;
            cursor: pointer;
        }
        .ajax__myTab .ajax__tab_active .ajax__tab_outer
        {
            background-color: #9ebae8;
            border-left: solid 1px #999999;
        }
        .ajax__myTab .ajax__tab_active .ajax__tab_inner
        {
            background-color: #9ebae8;
        }
        .ajax__myTab .ajax__tab_active .ajax__tab_tab
        {
            background-color: #9ebae8;
            cursor: inherit;
        }
        .ajax__myTab .ajax__tab_body
        {
            border: 1px solid #666666;
            padding: 6px;
            background-color: #ffffff;
        }
        .ajax__myTab .ajax__tab_disabled
        {
            color: Gray;
        }
        .style37
        {
            font-family: Verdana;
            font-size: 9pt;
            color: #515456;
            border: 0px solid #CCCCCC;
            text-align: right;
            padding-right: 4px;
            width: 96px;
            height: 10px;
        }
        .style39
        {
            height: 1%;
            width: 910px;
        }
        .style41
        {
            font-size: 9pt;
            font-family: verdana;
            color: #515456;
            border: 0px solid #CCCCCC;
            text-align: left;
            padding-left: 4px;
            height: 10px;
        }
        .style42
        {
            font-size: 9pt;
            font-family: verdana;
            color: #515456;
            border: 0px solid #CCCCCC;
            text-align: left;
            padding-left: 4px;
            width: 120px;
            height: 10px;
        }
    </style>
    <script type="text/javascript">

        function previewFile() {
            debugger;
            try {
                var preview = document.querySelector('#<%=imgEmployeeImage.ClientID %>');
                var file = document.querySelector('#<%=FileUploadImages.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {
                    //Added by Pooja Yadav start
                    var extension;
                    var dotPosition = file.name.lastIndexOf(".");
                    //gets only the extension
                    var extension = file.name.substring(dotPosition);
                    if (!(extension == ".jpg" || extension == ".jpeg" || extension == ".bmp" || extension == ".png")) {

                        document.getElementById("<%=lblImageSize.ClientID%>").innerHTML = "Please select proper image format i.e .JPG,.PNG,.BMP";
                        preview.src = "";
                        return false;
                    }

                    if (typeof ($("#<%=FileUploadImages.ClientID %>")[0].files) != "undefined") {
                        var size = parseFloat($("#<%=FileUploadImages.ClientID %>")[0].files[0].size / 1024).toFixed(2);

                        if (size >= parseFloat("1024")) {
                            document.getElementById("<%=lblImageSize.ClientID%>").innerHTML = "Image Size should be less than 1MB";
                            preview.src = "";
                            return false;
                        }
                        else {
                            document.getElementById("<%=lblImageSize.ClientID%>").innerHTML = "";
                            reader.readAsDataURL(file);
                            return true;
                        }
                    }
                    //End
                } else {
                    preview.src = "";
                    return false;
                }
            }
            catch (err) {
                document.getElementById("<%=lblImageSize.ClientID%>").innerHTML = err;
            }
        }

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnMain" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnEmployeeMode" runat="server" ClientIDMode="Static" />
            <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                        <asp:Label ID="lblHead" runat="server" Text="Employee Master" ForeColor="RoyalBlue"
                            Font-Size="20px" Width="100%" CssClass="heading">
                        </asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <center>
                <div class="DivEmpDetails">
                    <table style="width: 100%;" border="0">
                        <tr>
                            <td style="width: 40%; text-align: left;">
                                <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click" />
                                <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                                    OnClick="btnDelete_Click" />
                            </td>
                            <td style="width: 60%; text-align: right; color: Black;">
                                <b>Status : </b>
                                <asp:DropDownList ID="ddlEmpStatus" runat="server" AutoPostBack="True" Width="100px"
                                    OnSelectedIndexChanged="ddlEmpStatus_SelectedIndexChanged">
                                    <asp:ListItem>All</asp:ListItem>
                                    <asp:ListItem>Active</asp:ListItem>
                                    <asp:ListItem>InActive</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp&nbsp&nbsp
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                                    OnClientClick="return ResetAll();" />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;"
                                    CssClass="ButtonControl" OnClick="btnSearch_Click" />
                                <asp:TextBox ID="txtCompanyName" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCompanyName" runat="server" TargetControlID="txtCompanyName"
                                    WatermarkText="Name" WatermarkCssClass="watermark" BehaviorID="CompanyNameWatermark">
                                </ajaxToolkit:TextBoxWatermarkExtender>
                                <asp:TextBox ID="txtCompanyID" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCompanyID" runat="server" TargetControlID="txtCompanyID"
                                    WatermarkText="ID" WatermarkCssClass="watermark" BehaviorID="CompanyIDWatermark">
                                </ajaxToolkit:TextBoxWatermarkExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                                <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                                    <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false" Width="100%"
                                            GridLines="None" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvEmployee_PageIndexChanging"
                                            OnRowCommand="gvEmployee_RowCommand">
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
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" SortExpression="Edit" ItemStyle-Width="5%" HeaderStyle-CssClass="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit3"
                                                            OnClientClick="SetMode();" Text="Edit" ForeColor="#3366FF" CommandArgument='<%#Eval("EPD_empid")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EPD_empid" ControlStyle-Width="10%" HeaderText="Emp Id"
                                                    SortExpression="Emp Id">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EPD_Name" HeaderText="Name" SortExpression="Name">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <%--     <asp:BoundField DataField="EPD_CARD_id" HeaderText="Card No" SortExpression="Card No">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>--%>
                                                <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EOD_JOINING_DATE" HeaderText="Joining Date" SortExpression="Joining Date">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EOD_Active" HeaderText="Status" SortExpression="Status">
                                                    <ItemStyle HorizontalAlign="Center" />
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
                                        <asp:PostBackTrigger ControlID="btnDelete" />
                                        <asp:PostBackTrigger ControlID="btnAddE" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
                                    TargetControlID="UpdatePanel2">
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
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div>
                            <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlEmpStatus" />
                    </Triggers>
                </asp:UpdatePanel>
            </center>
            <asp:Panel ID="pnlAddEmployee" runat="server" CssClass="PopupPanel" Width="95%">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <fieldset>
                            <legend style="color: Green; padding-left: 5px; padding-right: 5px">Employee Personal
                                Details :</legend>
                            <table id="table2" runat="server" width="100%" height="90%" border="0" cellpadding="0"
                                cellspacing="0" class="TableClass">
                                <tr id="Tr1" runat="server">
                                    <td id="as" class="style37" runat="server" style="text-align: left; width">
                                        Employee Id :<label class="CompulsaryLabel">*</label>
                                    </td>
                                    <td style="height: 10px; width: 100px;" class="TDClassControl1" runat="server">
                                        <asp:TextBox CssClass="TextControl" ID="EPD_EMPLOYEEID" MaxLength="10" runat="server"
                                            Style="text-transform: uppercase;" TabIndex="1" ClientIDMode="Static" Width="80px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            FilterType="Numbers,UppercaseLetters,LowercaseLetters" TargetControlID="EPD_EMPLOYEEID" />
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvEmpId" runat="server" ControlToValidate="EPD_EMPLOYEEID"
                                            Display="None" ErrorMessage="Please enter Id" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
                                            TargetControlID="rfvEmpId" Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td id="Td1" class="style37" runat="server" style="text-align: left;">
                                        First Name :<label class="CompulsaryLabel">*</label>
                                    </td>
                                    <td id="Td2" runat="server" class="style42">
                                        <asp:TextBox ID="EPD_FIRST_NAME" MaxLength="50" runat="server" Style="text-transform: capitalize;"
                                            onkeypress="return IsChar(event)" TabIndex="3" ClientIDMode="Static" Width="80px"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="ReqFieldValidatorm_EPD_FIRST_NAME" runat="server"
                                            ErrorMessage="Please enter First Name" ControlToValidate="EPD_FIRST_NAME" SetFocusOnError="True"
                                            Display="None" ForeColor="Red" ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcerlstfileAdd" runat="server" TargetControlID="ReqFieldValidatorm_EPD_FIRST_NAME"
                                            Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td id="Td3" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                        Middle Name:
                                    </td>
                                    <td id="Td4" style="height: 10px; width: 100px;" runat="server" class="TDClassControl1">
                                        <asp:TextBox ID="EPD_MIDDLE_NAME" runat="server" CssClass="TextControl" MaxLength="50"
                                            onkeypress="return IsChar(event)" Style="text-transform: capitalize;" TabIndex="4"
                                            Width="80px"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td id="Td39" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                        Last Name:<label class="CompulsaryLabel">*</label>
                                    </td>
                                    <td id="Td40" style="height: 10px; width: 100px;" runat="server" class="TDClassControl1">
                                        <asp:TextBox ID="EPD_LAST_NAME" runat="server" CssClass="TextControl" MaxLength="50"
                                            onkeypress="return IsChar(event)" Style="text-transform: capitalize;" TabIndex="5"
                                            ClientIDMode="Static" Width="80px"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="EPD_LAST_NAME"
                                            Display="None" ErrorMessage="Please enter Last Name" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                            TargetControlID="rfvLastName" Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td id="Td43" rowspan="5" class="TDClassControl1" colspan="1" style="height: 5px;"
                                        runat="server" align="right">
                                        <asp:Image ID="imgEmployeeImage" ImageUrl="~/Handler1.ashx" runat="server" Height="100px"
                                            Style="text-align: center;" Width="89px" ClientIDMode="Static" BorderWidth="1px"
                                            ImageAlign="Middle" />
                                       
                                        <asp:FileUpload ID="FileUploadImages" Style="width: 80px;" runat="server" TabIndex="2"
                                            ClientIDMode="Static" onchange="previewFile();" />
                                        <p>
                                            <asp:Label ID="lblImageSize" runat="server" Text="Image Size should be less than 1MB"
                                                CssClass="ErrorLabel"></asp:Label></p>
                                    </td>
                                    <td>
                                     
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 1px;">
                                    </td>
                                </tr>
                                <tr id="Tr5" runat="server">
                                    <td id="Td5" class="TDClassLabel1" style="height: 10px; width: 60px; text-align: left;"
                                        runat="server">
                                        Salutation :&nbsp;
                                    </td>
                                    <td id="Td6" style="height: 10px; width: 100px;" runat="server" class="TDClassControl1">
                                        <asp:TextBox ID="EPD_SALUTATION" runat="server" CssClass="TextControl" MaxLength="8"
                                            TabIndex="7" Width="80px"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td id="Td14" class="style37" runat="server" style="padding-right: 50px; text-align: left">
                                        Gender :<label class="CompulsaryLabel">*</label>
                                    </td>
                                    <td id="Td32" runat="server" class="style42">
                                        <asp:DropDownList ID="EPD_GENDER" runat="server" ClientIDMode="Static" TabIndex="11"
                                            Width="80px">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvGender" runat="server" ErrorMessage="Please select Gender"
                                            ControlToValidate="EPD_GENDER" SetFocusOnError="True" Display="None" InitialValue="0"
                                            ForeColor="Red" ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                            TargetControlID="rfvGender" Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td id="Td41" class="style37" runat="server" style="text-align: left;">
                                        Nick Name :&nbsp;
                                    </td>
                                    <td id="Td42" style="height: 10px; width: 100px;" runat="server" class="TDClassControl1">
                                        <asp:TextBox ID="EPD_NICKNAME" runat="server" CssClass="TextControl" MaxLength="50"
                                            onkeypress="return IsChar(event)" Style="text-transform: capitalize;" TabIndex="6"
                                            Width="80px"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td id="Td17" class="TDClassLabel1" style="height: 10px; text-align: left" runat="server">
                                        Marital Status : &nbsp;
                                    </td>
                                    <td id="Td18" style="height: 10px; width: 100px;" runat="server" class="TDClassControl1">
                                        <asp:DropDownList ID="EPD_MARITAL_STATUS" runat="server" TabIndex="13" Width="80px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 1px;">
                                    </td>
                                </tr>
                                <tr id="Tr8" runat="server">
                                    <td id="Td7" class="style37" runat="server" style="text-align: left; width: 40px;">
                                        Card No :
                                    </td>
                                    <td id="Td8" style="height: 5px; width: 100px;" runat="server" class="TDClassControl1">
                                        <asp:TextBox ID="EPD_CARD_NO" runat="server" CssClass="TextControl" MaxLength="8"
                                            onkeypress="return IsAlphanumericWithoutspace(event)" Style="text-transform: uppercase;"
                                            TabIndex="8" Width="80px"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvcardNo" runat="server" ControlToValidate="EPD_CARD_NO"
                                            Display="None" ErrorMessage="Please enter Card No" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="validateofficial" Visible="False"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                            TargetControlID="rfvcardNo" Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                            FilterType="Numbers,Custom" TargetControlID="EPD_CARD_NO" ValidChars="ABCDEFabcdef" />
                                    </td>
                                    <td id="Td9" class="TDClassLabel1" style="height: 5px; text-align: left" runat="server">
                                        Previous Code :
                                    </td>
                                    <td id="Td10" runat="server" class="style42">
                                        <asp:TextBox CssClass="TextControl" ID="EPD_PREVIOUS_CODE" MaxLength="15" runat="server"
                                            onkeypress="return IsAlphanumeric(event)" TabIndex="9" Width="80px"></asp:TextBox>
                                    </td>
                                    <td id="Td12" class="TDClassLabel1" style="height: 5px; text-align: left; display: none"
                                        runat="server">
                                        Temp Card No :
                                    </td>
                                    <td id="Td13" style="height: 5px; width: 100px; display: none" runat="server" class="TDClassControl1">
                                        <asp:TextBox ID="EPD_TEMP_CARD_NO" runat="server" CssClass="TextControl" MaxLength="8"
                                            onkeypress="return IsAlphanumeric(event)" TabIndex="10" Width="80px"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td id="Td109" class="TDClassLabel1" style="height: 5px; text-align: left" runat="server">
                                        Aadhaar Card No :
                                    </td>
                                    <td id="Td110" style="height: 5px; width: 100px;" runat="server" class="TDClassControl1">
                                        <asp:TextBox ID="txtAadhar" runat="server" CssClass="TextControl" MaxLength="12"
                                            onkeypress="return IsAlphanumeric(event)" TabIndex="10" Width="80px"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td id="Td25" class="style37" runat="server" style="text-align: left;">
                                        PAN : &nbsp;
                                    </td>
                                    <td id="Td26" style="height: 5px; width: 100px;" runat="server" class="TDClassControl1">
                                        <asp:TextBox CssClass="TextControl" ID="EPD_PAN" MaxLength="10" runat="server" TabIndex="19"
                                            Width="80px" Style="text-transform: uppercase;" onkeypress="return IsAlphanumericWithoutspace(event)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="Tr6" runat="server">
                                    <td id="Td20" class="style37" runat="server" style="text-align: left; width: 60px;">
                                        Reference One : &nbsp;
                                    </td>
                                    <td id="Td21" style="height: 5px; width: 100px;" runat="server" class="TDClassControl1">
                                        <asp:TextBox CssClass="TextControl" ID="EPD_REFERENCE_ONE" MaxLength="100" runat="server"
                                            Style="text-transform: capitalize;" onkeypress="return IsChar(event)" TabIndex="14"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td id="Td22" class="TDClassLabel1" style="height: 5px; text-align: left;" runat="server">
                                        Reference Two : &nbsp;
                                    </td>
                                    <td id="Td23" runat="server" class="style42">
                                        <asp:TextBox CssClass="TextControl" ID="EPD_REFERENCE_TWO" MaxLength="100" runat="server"
                                            onkeypress="return IsChar(event)" TabIndex="15" Style="text-transform: capitalize;"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td id="Td47" class="TDClassLabel1" style="height: 5px; text-align: left;" runat="server">
                                        Blood Group :
                                    </td>
                                    <td id="Td48" style="height: 10px; width: 100px;" runat="server" class="TDClassControl1">
                                        <asp:TextBox CssClass="TextControl" ID="EPD_BLOODGROUP" MaxLength="10" runat="server"
                                            Style="text-transform: capitalize;" TabIndex="20" Width="80px"></asp:TextBox>
                                    </td>
                                    <td id="Td49" class="TDClassLabel1" style="height: 5px; text-align: left;" runat="server">
                                        Doctor : &nbsp;
                                    </td>
                                    <td id="Td50" style="height: 5px; width: 100px;" runat="server" class="TDClassControl1">
                                        <asp:TextBox CssClass="TextControl" ID="EPD_DOCTOR" MaxLength="50" Style="text-transform: capitalize;"
                                            runat="server" TabIndex="21" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td class="TDClassLabel1" style="height: 10px; text-align: left; width: 60px;" runat="server">
                                        E-mail Id: &nbsp;
                                    </td>
                                    <td style="height: 10px; width: 80px;" runat="server" class="TDClassControl1">
                                        <asp:TextBox CssClass="TextControl" ID="EPD_EMAIL" MaxLength="50" runat="server"
                                            TabIndex="17" Width="80px"></asp:TextBox>
                                    </td>
                                    <td id="Td11" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                        Nationality : &nbsp;
                                    </td>
                                    <td id="Td24" style="margin-left: 40px;" runat="server" class="style42">
                                        <asp:TextBox CssClass="TextControl" ID="EPD_DOMICILE" MaxLength="50" runat="server"
                                            Style="text-transform: capitalize;" onkeypress="return IsChar(event)" TabIndex="18"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td id="Td45" class="style37" runat="server" style="text-align: left;">
                                        Religion : &nbsp;
                                    </td>
                                    <td id="Td46" style="height: 10px; width: 100px;" runat="server" class="TDClassControl1">
                                        <asp:DropDownList ID="EPD_RELIGION" runat="server" TabIndex="16" Width="80px">
                                            <asp:ListItem Value="null">Select</asp:ListItem>
                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td id="Td15" class="TDClassLabel1" style="height: 10px; text-align: left" runat="server">
                                        DOB :<label class="CompulsaryLabel">*</label>
                                    </td>
                                    <td id="Td16" style="height: 10px; width: 100px;" runat="server" class="TDClassControl1">
                                        <asp:TextBox ID="DOB" runat="server" ClientIDMode="Static" MaxLength="10" Width="80px"
                                            onKeyPress="javascript: return false "></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calFrmDate" runat="server" TargetControlID="DOB"
                                            PopupButtonID="DOB" Format="dd/MM/yyyy" Enabled="True">
                                        </ajaxToolkit:CalendarExtender>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvDOB" runat="server" ControlToValidate="DOB" Display="None"
                                            ErrorMessage="Please enter Date Of Birth" SetFocusOnError="True" ForeColor="Red"
                                            ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                            TargetControlID="rfvDOB" Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:CustomValidator ID="CustomValidatorDeposit_Date" runat="server" ErrorMessage="DOB cannot be grater then current date."
                                            ValidationGroup="validateofficial" ControlToValidate="DOB" ForeColor="Red" Display="None"
                                            ClientValidationFunction="chkDOBDateRange"></asp:CustomValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender23" runat="server"
                                            PopupPosition="Right" TargetControlID="CustomValidatorDeposit_Date">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <table>
                            <tr>
                                <td style="height: 2%;">
                                </td>
                            </tr>
                        </table>
                        <table id="table10" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                            class="TableClass">
                            <tr>
                                <td style="width: 49%">
                                    <fieldset>
                                        <legend style="color: Green; padding-left: 5px; padding-right: 5px">Employee Address
                                            Details :</legend>
                                        <table width="100%">
                                            <tr>
                                                <td style="height: 6px;">
                                                </td>
                                            </tr>
                                            <tr runat="server">
                                                <td class="TDClassLabel1" runat="server" colspan="4" style="text-align: left;">
                                                    <b style="text-align: center; width: 100px;">Local Address </b>
                                                </td>
                                            </tr>
                                            <tr runat="server">
                                                <td class="TDClassLabel1" style="height: 10px; width: 10px; text-align: left;" runat="server"
                                                    rowspan="2">
                                                    Address 1 :
                                                </td>
                                                <td style="height: 10px;" runat="server" rowspan="2" class="TDClassControl1">
                                                    <asp:TextBox ID="EAD_ADDRESS1" runat="server" CssClass="TextControl" Height="40px"
                                                        MaxLength="50" Style="text-transform: capitalize; min-width: 120px; max-width: 120px;
                                                        min-height: 40px; max-height: 40px" TextMode="MultiLine" ClientIDMode="Static"
                                                        TabIndex="23" onkeyup="addressSubmit()" Width="120px"></asp:TextBox>
                                                </td>
                                                <td id="Td31" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                    runat="server">
                                                    Pin :
                                                </td>
                                                <td id="Td33" style="height: 10px;" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="EAD_PIN1" runat="server" onkeypress="return IsNumber(event)" CssClass="TextControl"
                                                        MaxLength="10" Style="text-transform: capitalize;" ClientIDMode="Static" TabIndex="25"
                                                        Width="80px" onkeyup="pinSubmit()"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr runat="server">
                                                <td id="Td19" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                    runat="server">
                                                    City :
                                                </td>
                                                <td id="Td37" style="height: 10px" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="EAD_CITY1" runat="server" CssClass="TextControl" MaxLength="15"
                                                        Style="text-transform: capitalize;" onkeyup="citySubmit()" ClientIDMode="Static"
                                                        TabIndex="24" Width="80px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr runat="server">
                                                <td id="Td27" runat="server" class="TDClassLabel1" style="height: 10px; text-align: left;">
                                                    State :
                                                </td>
                                                <td id="Td28" style="height: 10px" runat="server" class="TDClassControl1">
                                                    <asp:DropDownList ID="EAD_STATE1" runat="server" Width="120px" ClientIDMode="Static"
                                                        onblur="stateSubmit('L')" TabIndex="25">
                                                    </asp:DropDownList>
                                                </td>
                                                <td id="Td34" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                    runat="server">
                                                    Country :
                                                </td>
                                                <td id="Td35" style="height: 10px" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="EAD_Country1" ClientIDMode="Static" runat="server" CssClass="TextControl"
                                                        Width="80px" Style="text-transform: capitalize;" MaxLength="15" TabIndex="26"
                                                        onkeyup="countrySubmit()"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FTBEAD_Country1" runat="server" FilterType="UppercaseLetters,LowercaseLetters"
                                                        TargetControlID="EAD_Country1" />
                                                </td>
                                            </tr>
                                            <tr runat="server">
                                                <td id="Td29" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                                    Contact No 1:
                                                </td>
                                                <td id="Td30" style="height: 10px" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="EAD_Phone1" onkeypress="return IsNumber(event)" runat="server" CssClass="TextControl"
                                                        MaxLength="11" Style="text-transform: capitalize;" ClientIDMode="Static" TabIndex="27"
                                                        Width="80px" onkeyup="phno1Submit()"></asp:TextBox>
                                                </td>
                                                <td id="Td36" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                    runat="server">
                                                    Contact No 2:
                                                </td>
                                                <td id="Td38" style="height: 10px" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="EAD_Phone2" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                                        MaxLength="11" onkeyup="phno2Submit()" onkeypress="return IsNumber(event)" Style="text-transform: capitalize;"
                                                        TabIndex="28" Width="80px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td style="height: 2%">
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%;">
                                            <tr id="Tr2" runat="server">
                                                <td id="Td44" class="TDClassLabel1" runat="server" colspan="4" style="text-align: left">
                                                    <b style="text-align: center; width: 100px;">Permanent Address</b>
                                                    <asp:CheckBox ID="ChkAddress" ClientIDMode="Static" Checked="false" runat="server"
                                                        Style="text-align: left" onclick="cHK_SYNC_ADD(this)" onmouseup="ChkSelect();"
                                                        Text=" If same as Local Address" TabIndex="67" />
                                                </td>
                                            </tr>
                                            <tr id="Tr3" runat="server">
                                                <td id="Td52" class="TDClassLabel1" style="height: 10px; width: 10px; text-align: left"
                                                    runat="server" rowspan="2">
                                                    Address 1 :
                                                </td>
                                                <td id="Td53" style="height: 10px" runat="server" rowspan="2" class="TDClassControl1">
                                                    <asp:TextBox ID="EAD_ADDRESS2" runat="server" CssClass="TextControl" Height="40px"
                                                        MaxLength="50" Style="text-transform: capitalize; min-width: 120px; max-width: 120px;
                                                        min-height: 40px; max-height: 40px" TextMode="MultiLine" ClientIDMode="Static"
                                                        TabIndex="30" Width="120px"></asp:TextBox>
                                                </td>
                                                <td id="Td54" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                    runat="server">
                                                    Pin :
                                                </td>
                                                <td id="Td55" class="TDClassControl1" style="height: 10px" runat="server">
                                                    <asp:TextBox ID="EAD_PIN2" runat="server" onkeypress="return IsNumber(event)" CssClass="TextControl"
                                                        MaxLength="10" Style="text-transform: capitalize;" ClientIDMode="Static" TabIndex="31"
                                                        Width="80px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="Tr4" runat="server">
                                                <td id="Td56" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                    runat="server">
                                                    City :
                                                </td>
                                                <td id="Td57" style="height: 10px" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="EAD_CITY2" runat="server" CssClass="TextControl" MaxLength="15"
                                                        Style="text-transform: capitalize;" ClientIDMode="Static" TabIndex="30" Width="80px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="Tr7" runat="server">
                                                <td id="Td58" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                                    State :
                                                </td>
                                                <td id="Td59" style="height: 10px" runat="server" class="TDClassControl1">
                                                    <asp:DropDownList ID="EAD_STATE2" runat="server" ClientIDMode="Static" onchange="fillcountry()"
                                                        TabIndex="32" Width="120px" onblur="stateSubmit('P')">
                                                    </asp:DropDownList>
                                                </td>
                                                <td id="Td82" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                    runat="server">
                                                    Country :
                                                </td>
                                                <td id="Td83" style="height: 10px" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="EAD_COUNTRY2" ClientIDMode="Static" runat="server" CssClass="TextControl"
                                                        MaxLength="15" Style="text-transform: capitalize;" TabIndex="33" Width="80px"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FTBEAD_COUNTRY2" runat="server" FilterType="UppercaseLetters,LowercaseLetters"
                                                        TargetControlID="EAD_COUNTRY2" />
                                                </td>
                                            </tr>
                                            <tr id="Tr9" runat="server">
                                                <td id="Td84" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                                    Contact No 1:
                                                </td>
                                                <td id="Td85" style="height: 10px; text-align: left;" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="EAD_Phone3" runat="server" onkeypress="return IsNumber(event)" CssClass="TextControl"
                                                        MaxLength="11" Style="text-transform: capitalize;" ClientIDMode="Static" Width="80px"
                                                        TabIndex="34"></asp:TextBox>
                                                </td>
                                                <td id="Td86" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                    runat="server">
                                                    Contact No 2:
                                                </td>
                                                <td id="Td87" style="height: 10px;" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="EAD_Phone4" runat="server" ClientIDMode="Static" CssClass="TextControl"
                                                        MaxLength="11" onkeypress="return IsNumber(event)" Style="text-transform: capitalize;"
                                                        TabIndex="35" Width="80px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 1%;">
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                                <td style="width: 2%">
                                </td>
                                <td style="width: 49%">
                                    <fieldset>
                                        <legend style="color: Green; padding-left: 5px; padding-right: 5px">Employee Official
                                            Details : </legend>
                                        <table id="table6" runat="server" width="100%" height="90%" border="0" cellpadding="0"
                                            cellspacing="0" class="TableClass">
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr id="Tr10" runat="server">
                                                <td id="Td60" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                                    Joining :<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td61" class="TDClassControl1" style="height: 10px" runat="server">
                                                    <asp:TextBox ID="joindt" runat="server" ClientIDMode="Static" MaxLength="10" Width="80px"
                                                        onKeyPress="javascript: return false "></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="joindt"
                                                        PopupButtonID="joindt" Format="dd/MM/yyyy" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvJOD" runat="server" ControlToValidate="joindt"
                                                        Display="None" ErrorMessage="Please enter Date" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                        TargetControlID="rfvJOD" Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="isValidateDDMMYYYYDate"
                                                        ControlToValidate="joindt" Display="None" ErrorMessage="Please enter valid Date"
                                                        ForeColor="Red" SetFocusOnError="True"></asp:CustomValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server"
                                                        TargetControlID="CustomValidator1" Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <asp:CustomValidator ID="CustomValidatorJoindt" runat="server" ClientValidationFunction="isValidateIssueDate"
                                                        ControlToValidate="joindt" Display="None" ErrorMessage="Joining Date Cannot Be Less Than or Equal To DOB"
                                                        ForeColor="Red" SetFocusOnError="True"></asp:CustomValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server"
                                                        TargetControlID="CustomValidatorJoindt" Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                <td id="Td62" class="TDClassLabel1" style="height: 10px; text-align: left; width: 180px;
                                                    padding-left: 15px;" runat="server">
                                                    Confirmation :
                                                </td>
                                                <td id="Td63" class="TDClassControl1" style="height: 10px" runat="server">
                                                    <%-- <input type="text" class="DOB" style="width: 80px" onkeypress="return IsNumber(event)"
                                                id="Confdt" runat="server"
                                                maxlength="10" clientidmode="Static" tabindex="40" />--%>
                                                    <asp:TextBox ID="Confdt1" runat="server" ClientIDMode="Static" MaxLength="10" Width="80px"
                                                        onKeyPress="javascript: return false " Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="Confdt1"
                                                        PopupButtonID="Confdt1" Format="dd/MM/yyyy" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Confdt1"
                                                        Display="None" ErrorMessage="Please enter Date" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="validateofficial" Visible="False"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server"
                                                        TargetControlID="RequiredFieldValidator6" Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="isValidateDDMMYYYYDate"
                                                        ControlToValidate="Confdt1" Display="None" ErrorMessage="Please enter valid Date"
                                                        ForeColor="Red" SetFocusOnError="True" ValidationGroup="validateofficial"></asp:CustomValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server"
                                                        TargetControlID="CustomValidator2" Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <asp:CompareValidator ID="vcConfdt1" runat="server" ErrorMessage="Confirmation Date should be greater then To Joining"
                                                        ControlToCompare="joindt" ControlToValidate="Confdt1" ValidationGroup="validateofficial"
                                                        Type="Date" Operator="GreaterThanEqual" Display="None"></asp:CompareValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcevctxtToDate" runat="server" TargetControlID="vcConfdt1"
                                                        PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr id="Tr17" runat="server">
                                                <td id="Td101" class="TDClassLabel1" style="height: 4px" runat="server">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Td64" class="TDClassLabel1" style="height: 10px; text-align: left" runat="server">
                                                    Employee Left :
                                                </td>
                                                <td id="Td65" class="TDClassControl1" style="height: 10px" runat="server">
                                                    <%--   <input type="text" class="DOB" style="width: 80px" onkeypress="return IsNumber(event)"
                                                id="Retdt" runat="server" onkeyup="date_dash(this,event)" onkeydown="date_dash(this,event)"
                                                maxlength="10" clientidmode="Static" readonly="readonly" tabindex="41" />--%>
                                                    <asp:TextBox ID="Retdt1" runat="server" ClientIDMode="Static" MaxLength="10" Width="80px"
                                                        onKeyPress="javascript: return false " onblur="retdt1change();" Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="Retdt1"
                                                        PopupButtonID="Retdt1" Format="dd/MM/yyyy" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Retdt1"
                                                        Display="None" ErrorMessage="Please enter Date" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="validateofficial" Visible="False"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server"
                                                        TargetControlID="RequiredFieldValidator7" Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="isValidateDDMMYYYYDate"
                                                        ControlToValidate="Retdt1" Display="None" ValidationGroup="validateofficial"
                                                        ErrorMessage="Please enter valid Date" ForeColor="Red" SetFocusOnError="True"
                                                        Visible="False"></asp:CustomValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server"
                                                        TargetControlID="CustomValidator3" Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <asp:CompareValidator ID="cvRetdt1" runat="server" ErrorMessage="Retirement Date should be greater then To Joining Date and Confirmation Date"
                                                        ControlToCompare="Confdt1" ControlToValidate="Retdt1" ValidationGroup="validateofficial"
                                                        Type="Date" Operator="GreaterThanEqual" Display="None"></asp:CompareValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" runat="server"
                                                        TargetControlID="cvRetdt1" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <asp:CompareValidator ID="CompareValidatorRetdt1" runat="server" ErrorMessage="Retirement Date should be greater then To Joining Date and Confirmation Date"
                                                        ControlToCompare="joindt" ControlToValidate="Retdt1" ValidationGroup="validateofficial"
                                                        Type="Date" Operator="GreaterThanEqual" Display="None"></asp:CompareValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server"
                                                        TargetControlID="CompareValidatorRetdt1" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                <td id="Td66" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                    runat="server">
                                                    Reason:
                                                </td>
                                                <td id="Td67" class="TDClassControl1" style="height: 10px" runat="server">
                                                    <asp:DropDownList ID="ddlreason" runat="server" Enabled="False" TabIndex="42" ClientIDMode="Static"
                                                        Width="80px" class="chosen-select">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr id="Tr16" runat="server">
                                                <td id="Td100" class="TDClassLabel1" style="height: 4px" runat="server">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Td72" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                                    Company :<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td73" class="TDClassControl1" style="height: 10px" runat="server">
                                                    <asp:DropDownList ID="ddlcompany" runat="server" ClientIDMode="Static" TabIndex="43"
                                                        Width="150px" class="chosen-select">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ControlToValidate="ddlcompany"
                                                        Display="None" ErrorMessage="Please select Company" ForeColor="Red" InitialValue="Select"
                                                        SetFocusOnError="True" ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                        TargetControlID="rfvCompany" Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                <td id="Td69" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                    runat="server">
                                                    Location :<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td70" class="TDClassControl1" style="height: 10px" runat="server">
                                                    <asp:DropDownList ID="ddllocation" runat="server" TabIndex="44" ClientIDMode="Static"
                                                        class="chosen-select">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="ddllocation"
                                                        Display="None" ErrorMessage="Please select Location" ForeColor="Red" InitialValue="Select"
                                                        SetFocusOnError="True" ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                        TargetControlID="rfvLocation" Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr id="Tr14" runat="server">
                                                <td id="Td99" class="TDClassLabel1" style="height: 4px" runat="server">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Td71" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                                    Division :<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td class="TDClassControl1" style="height: 10px" runat="server">
                                                    <asp:DropDownList ID="ddldivision" runat="server" TabIndex="45" ClientIDMode="Static"
                                                        Width="120px" class="chosen-select">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvDivision" runat="server" ControlToValidate="ddldivision"
                                                        Display="None" ErrorMessage="Please select Division" ForeColor="Red" InitialValue="Select"
                                                        SetFocusOnError="True" ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                                        TargetControlID="rfvDivision" Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                <td id="Td74" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                    runat="server">
                                                    Department :<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td97" class="TDClassControl1" style="height: 10px" runat="server">
                                                    <asp:DropDownList ID="ddldepartment" runat="server" TabIndex="46" ClientIDMode="Static"
                                                        Width="120px" class="chosen-select">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddldepartment"
                                                        Display="None" ErrorMessage="Please select Department" ForeColor="Red" InitialValue="Select"
                                                        SetFocusOnError="True" ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                                        TargetControlID="rfvDepartment" Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr id="Tr13" runat="server">
                                                <td id="Td98" class="TDClassLabel1" style="height: 4px" runat="server">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Td75" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                                    Designation :<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td76" class="TDClassControl1" style="height: 10px" runat="server">
                                                    <asp:DropDownList ID="ddldesignation" runat="server" TabIndex="47" ClientIDMode="Static"
                                                        Width="120px" class="chosen-select">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="ddldesignation"
                                                        Display="None" ErrorMessage="Please select Designation" ForeColor="Red" InitialValue="Select"
                                                        SetFocusOnError="True" ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                        TargetControlID="rfvDesignation" Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                <td id="Td77" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                    runat="server">
                                                    Category :<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td78" class="TDClassControl1" style="height: 10px" runat="server">
                                                    <asp:DropDownList ID="ddlcategory" runat="server" TabIndex="48" ClientIDMode="Static"
                                                        Width="120px" class="chosen-select">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlcategory"
                                                        Display="None" ErrorMessage="Please select Category" ForeColor="Red" InitialValue="Select"
                                                        SetFocusOnError="True" ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="vceCategory" runat="server" TargetControlID="rfvCategory"
                                                        Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr id="Tr12" runat="server">
                                                <td id="Td79" class="TDClassLabel1" style="height: 4px" runat="server">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Td80" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                                    Group :<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td81" class="TDClassControl1" style="height: 10px" runat="server">
                                                    <asp:DropDownList ID="ddlgroup" runat="server" TabIndex="49" ClientIDMode="Static"
                                                        Width="120px" class="chosen-select">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvGroup" runat="server" ControlToValidate="ddlgroup"
                                                        Display="None" ErrorMessage="Please select Group" ForeColor="Red" InitialValue="Select"
                                                        SetFocusOnError="True" ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server"
                                                        TargetControlID="rfvGroup" Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                <td id="Td88" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                    runat="server">
                                                    Grade :<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td89" class="TDClassControl1" style="height: 10px" runat="server">
                                                    <asp:DropDownList ID="ddlgrade" runat="server" TabIndex="50" ClientIDMode="Static"
                                                        Width="120px" class="chosen-select">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvGrade" runat="server" ControlToValidate="ddlgrade"
                                                        Display="None" ErrorMessage="Please select Grade" ForeColor="Red" InitialValue="Select"
                                                        SetFocusOnError="True" ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                                        TargetControlID="rfvGrade" Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr id="Tr11" runat="server">
                                                <td id="Td68" class="TDClassLabel1" style="height: 4px" runat="server">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Td90" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                                    Employment Status :<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td91" class="TDClassControl1" style="height: 10px; width: 10%" runat="server">
                                                    <asp:DropDownList ID="ddlstatus" runat="server" TabIndex="51" ClientIDMode="Static"
                                                        Width="120px" onblur="chkConfirmation()" onkeyup="chkConfirmation()" onkeydown="chkConfirmation()"
                                                        onchange="chkConfirmation()">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlstatus"
                                                        Display="None" ErrorMessage="Please select Status" ForeColor="Red" InitialValue="Select"
                                                        SetFocusOnError="True" ValidationGroup="validateofficial"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
                                                        TargetControlID="rfvStatus" Enabled="True">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                         
                                                <td id="Td105" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                                    Reporting Person :
                                                </td>
                                                <td id="Td106" class="TDClassControl1" style="height: 10px;" runat="server">
                                                    <asp:DropDownList ID="ddlManager" runat="server" ClientIDMode="Static" TabIndex="51"
                                                        Width="160px" class="chosen-select" onmouseover="AdjustWidth(this);">
                                                    </asp:DropDownList>
                                              
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Td51" class="TDClassLabel1" style="height: 10px; text-align: left;" runat="server">
                                                </td>
                                                <td id="Td102" class="TDClassControl1" style="height: 10px" runat="server">
                                                </td>
                                                <td id="Td103" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                    runat="server">
                                                </td>
                                                <td id="Td104" class="TDClassControl1" style="height: 10px" runat="server" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                      <td id="tdESSLabel" class="TDClassLabel1" style="height: 10px; width: 40%; text-align: left;
                                                    padding-left: 15px;" runat="server">
                                                    ESS Enabled :
                                                </td>
                                                <td id="tdESSchkBox" class="TDClassControl1" style="height: 10px" runat="server"
                                                    colspan="2">
                                                    <asp:CheckBox ID="chkEssEnable" runat="server" />
                                                </td>
                                                <td id="Td92" class="TDClassLabel1" style="height: 10px; text-align: left; padding-left: 15px;"
                                                    runat="server">
                                                    <%-- Active :<label class="CompulsaryLabel">*</label>--%>
                                                </td>
                                                <td id="Td93" class="TDClassControl1" style="height: 10px" runat="server" colspan="2">
                                                    <asp:RadioButtonList ID="Rbtnchecked" runat="server" RepeatDirection="Horizontal"
                                                        TabIndex="52" ClientIDMode="Static" Width="100px" Visible="false">
                                                        <asp:ListItem Value="False" Text="Active" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="True" Text="InActive"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="table9" runat="server" width="100%" border="0" cellpadding="3" cellspacing="3">
                                            <tr>
                                                <td style="height: 10px;">
                                                </td>
                                            </tr>
                                            <tr id="Tr15" runat="server">
                                                <td id="Td94" style="width: 50%" runat="server">
                                                </td>
                                                <td id="Td95" runat="server">
                                                </td>
                                                <td id="Td96" align="center" runat="server">
                                                    <asp:Button ID="btnAddE" runat="server" Text="Save" CssClass="ButtonControl" TabIndex="53"
                                                        ValidationGroup="validateofficial" OnClick="btnAddE_Click" />
                                                    &nbsp;
                                                    <asp:Button ID="Btnclear1" runat="server" Text="Cancel" CssClass="ButtonControl"
                                                        TabIndex="54" OnClick="Btnclear_Click" ValidationGroup="validateofficial" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td align="center" class="style39">
                                    <asp:Label ID="lblSaveMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnAddE" />
                        <asp:PostBackTrigger ControlID="btnAdd" />
                        <%--             <asp:PostBackTrigger ControlID="btnImage" />--%>
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Button ID="temp" runat="server" Style="display: none" />
            <asp:Button ID="tempAdd" runat="server" Style="display: none" />
            <asp:Button ID="tempCancel" runat="server" Style="display: none" />
            <asp:Button ID="Button1" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="mpeAddEmployee" runat="server" TargetControlID="Button1"
                BehaviorID="ModalBehaviour" PopupControlID="pnlAddEmployee" BackgroundCssClass="modalBackground"
                Enabled="true" CancelControlID="tempCancel">
            </ajaxToolkit:ModalPopupExtender>
        </ContentTemplate>
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
