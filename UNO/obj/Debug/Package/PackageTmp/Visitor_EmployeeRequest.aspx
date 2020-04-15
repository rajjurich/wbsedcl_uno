<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="True"
    EnableEventValidation="false" UICulture="en-GB" Culture="en-GB" CodeBehind="Visitor_EmployeeRequest.aspx.cs"
    Inherits="UNO.Visitor_EmployeeRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <style type="text/css">
        .boxclose{
          float:right;
           right:-5px;
           width:26px;
           height:26px;
           top:-7px;
           position:absolute;
         color: #fff;
        border: 1px solid #AEAEAE;
    
        background: #605F61;
        font-size: 31px;
        font-weight: bold;
        display: block;
        line-height: 0px;
        padding: 11px 3px;       
}

.boxclose:before {
    content: "×";
}
        .CloseBtn
        {
           
            color:Blue;
            float: right;
            font-family:Times New Roman;
            font-size: 15px;
            font-weight:bolder;
            border: 0px solid black;
            border-width: 0px;
            font-style :inherit;

        }
          
        .TDClassControl1
        {
            font-size: 9pt;
            font-family: verdana;
            color: #515456;
            border: 0px solid #CCCCCC;
            border-width: 0px;
            text-align: left;
            padding-left: 4px;
            width: 21%;
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
            text-align: left;
            list-style-type: none;
            margin-left: 1px;
            padding-left: 1px;
            max-height: 200px;
            width: auto;
        }
        .doc_lbl
        {
            text-align: center;
        }
        .pop
        {
            height:280px;
            width:470px;
            position:fixed;
            bottom:50%;
            border:2px solid;
            padding:10px;
            background:#FFFFFF;
            
   }
   .Img
   {
        text-align: center;
       
       }
    </style>
    
    
    
    
    <script src="Scripts/Choosen/chosen.jquery.js" type="text/javascript"></script>
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css" />
    <script type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript">


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 43) {
                return true;
            }

            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            else {
                return true;
            }
        }
        //        var interval;
        var rfid;
        var obj = new ActiveXObject("VehicleManagementLib.Class1");
        var flag = false;
        function start1() {
            //alert("hiii");
            try {

                obj.getData();

                rfid = obj.Inventory();

            }
            catch (e) {

                alert(e.Message);
            }
        }
        function validateRFID(sender, args) {
            // start1();
            var boolflag = null;
            $.ajax({
                type: "POST",
                async: false,
                url: "VEH_VehicleEnromentl.aspx/SaveDate",
                // data: "{'data':'" + JSON.stringify(objList) + "'}",
                data: "{'data':'" + rfid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == true) {
                        boolflag = true;
                    }
                    else if (msg.d == false) {
                        boolflag = false;
                        // alert(rfid);
                        alert("RFID Not register");

                    }

                },
                error: function () { alert(arguments[2]); }
            });
            if (boolflag == true) {

                var txtRfid = document.getElementById('txtRFID');
                txtRfid.value = rfid;
            }

        }
    </script>
    <script language="javascript" type="text/javascript">

        $('#ddlApprovalAuthority').chosen({
            search_contains: true
        });

        function findspace(evnt) {

            var keyASCII = (evnt.which) ? evnt.which : event.keyCode;
            var keyValue = String.fromCharCode(keyASCII);

            if (!(keyASCII >= '48' && keyASCII <= '57')) {
                window.event.keyCode = 0;
            }
        }
        function fnColon(ctrl, e) {
            var unicode = e.keyCode
            if (unicode != 8) {
                if (ctrl.getAttribute && ctrl.value.length == 2) {
                    ctrl.value = ctrl.value + ":";
                }
            }
        }


        function PopUpmsg() {

            if (Page_ClientValidate("addVisitor")) {
                document.getElementById('<%= btnSave.ClientID %>').style.display = 'none';
                document.getElementById('<%= btnReset1.ClientID %>').style.display = 'none';
                document.getElementById('<%= Btnclear1.ClientID %>').style.display = 'none';
            }


            var VisitorType = document.getElementById("<%=drpnationality.ClientID %>").value;
            if (VisitorType == 'Foreigner') {
                alert("Request will not be forwarded unless Visitor documents are uploaded");
                return true;
            }
            else {
                alert("Document can be uploaded through \"Capture\" link, How ever it is optional for Indian visitor");
                return true;
            }
        }

        function Edit_PopUpmsg() {
            if (Page_ClientValidate("EditVisitor")) {
                document.getElementById('<%= btnSubmitEdit.ClientID %>').style.display = 'none';
                document.getElementById('<%= btnflush.ClientID %>').style.display = 'none';

            }

            var VisitorType = document.getElementById("<%=drpnationalityedit.ClientID %>").value;
            if (VisitorType == 'Foreigner') {
                alert("Request will not be forwarded unless Visitor documents are uploaded");
                return true;
            }

        }
    </script>
    <script language="javascript" type="text/javascript">
        function timeValOP(txtTime, timeValue) {


            var ft = document.getElementById(txtTime.id).value;

            var fptime = timeValue;

            var ftm = fptime.split(':');

            var ftsp = ft.split(':');

            var Totalftime = (+ftm[0]) * 60 * 60 + (+ftm[1]) * 60

            var ftseconds = (+ftsp[0]) * 60 * 60 + (+ftsp[1]) * 60

            var message = timeValue == "09:30" ? "You are allowing visitor before 09:30" : "You are allowing visitor after 18:00";

            if (timeValue == "09:30") {
                if (ftseconds < Totalftime) {
                    alert(message);
                    return true;
                }
            }
            else {
                if (ftseconds > Totalftime) {
                    alert(message);
                    return true;
                }
            }


        }

    </script>
    <script type="text/javascript">
        function timeVal() {


            if (document.getElementById('<%= txtAppoinmentFromTime.ClientID %>') != "NaN" && document.getElementById('<%= txtAppoinmentToTime.ClientID %>') != "NaN") {
                var ft = document.getElementById('<%= txtAppoinmentFromTime.ClientID %>').value;

                var tt = document.getElementById('<%= txtAppoinmentToTime.ClientID %>').value;

                var ftsp = ft.split(':');
                var ttsp = tt.split(':');

                var ftseconds = (+ftsp[0]) * 60 * 60 + (+ftsp[1]) * 60
                var ttseconds = (+ttsp[0]) * 60 * 60 + (+ttsp[1]) * 60

                if (ftseconds > ttseconds) {

                    alert("Out Time cannot be less than In time");
                    document.getElementById('<%= txtAppoinmentToTime.ClientID %>').value = "";
                }
            }

        }


        function timeValEdit() {


            if (document.getElementById('<%= txtAppoinMentFromtimeEdit.ClientID %>') != "NaN" && document.getElementById('<%= txtAppointmentToTimeEdit.ClientID %>') != "NaN") {
                var ft = document.getElementById('<%= txtAppoinMentFromtimeEdit.ClientID %>').value;

                var tt = document.getElementById('<%= txtAppointmentToTimeEdit.ClientID %>').value;

                var ftsp = ft.split(':');
                var ttsp = tt.split(':');

                var ftseconds = (+ftsp[0]) * 60 * 60 + (+ftsp[1]) * 60
                var ttseconds = (+ttsp[0]) * 60 * 60 + (+ttsp[1]) * 60

                if (ftseconds > ttseconds) {

                    alert("Out Time cannot be less than In time");
                    document.getElementById('<%= txtAppointmentToTimeEdit.ClientID %>').value = "";
                }
            }

        }
    
    </script>
    <script language="javascript" type="text/javascript">
     
        function ResetAll() {
            $('#' + ["<%=txtLevelID.ClientID%>", "<%=txtUserID.ClientID%>"].join(', #')).prop('value', "");
            document.getElementById('<%=txtUserID.ClientID%>').focus();
            document.getElementById('<%=lblMsg.ClientID%>').innerHTML = "";
            document.getElementById('<%=txtLevelID.ClientID%>').focus();
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=GvVisitor.ClientID%>').focus();
            return false;
        }
        function callexe(emp, con, url, reqid,type) {
        
                try {
                  
                    var WshShell = new ActiveXObject("WScript.Shell");
                    var Return = WshShell.Run("C:/Visitor/ScanDocument/AutoScan.exe " + emp + " " + con + " " + url+ " "+ reqid, 1, true);
                    // return false;
                   
                    if (Return=="0") {
                      
                        document.getElementById("<%=Btnclear1.ClientID %>").Enabled=false;
                        document.getElementById("<%=lblSaveMessages.ClientID %>").innerHTML = "Please Select Atleast One Document.";
                        if (type="edit") {
                            alert("dfff");
                            document.getElementById("<%=ModalPopupExtender1.ClientID %>").display="inline";    
                        }
                        else {
                            <%Session["save"] = "1";%>
                        }
                    }
                    else {
                        document.getElementById("<%=Btnclear1.ClientID %>").Enabled = false;
                        document.getElementById("<%=lblSaveMessages.ClientID %>").innerHTML = "Record Saved Successfully";
                        if (type="edit") {   <%Session["edit"] = "";%>}
                        else {<%Session["save"] = "";%> }
                    }
                }
                catch (ex) {
                   // alert(ex.Message);
                }
        }


         function CallAutoScan(visitorid, connection,reqid) {
            try {
            
                var url = "";
                //            if (!window.location.origin) {
                //                // url = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '') + "/unotest14/ScanDocument.aspx";
                //                url = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '') + "/ScanDocument.aspx";
                //                url = url.replace(/ /g, ',');
                //            }
                //            else {
                //               // url = window.location.origin + "/unotest14/ScanDocument.aspx";
                //                url = window.location.origin + "/ScanDocument.aspx"; // For Save Document
                //                url = url.replace(/ /g, ',');
                //            }
                url = window.location.href
                var filename = url.substr(url.lastIndexOf("/") + 1);
                url = url.replace(filename, "ScanDocument.aspx");
                var emp="\"\"";

                //  connection = connection.replace(/ /g, ',');
                var connection = document.getElementById('<%= hdConn.ClientID %>').value;
                alert(visitorid);
                var empCode = visitorid;
                var WshShell = new ActiveXObject("WScript.Shell");
                var Return = WshShell.Run("C:/VisitorRequest/ScanDocument/AutoScan.exe " + emp + " " + connection + " " + url+" "+reqid, 1, true);
                // return false;
               
               
            }
            catch (ex) {
                alert(ex.Message);
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function checkNum() {

            if ((event.keyCode > 64 && event.keyCode < 91) || (event.keyCode > 96 && event.keyCode < 123) || event.keyCode == 8 || event.keyCode == 32)
                return true;
            else {
               // alert("Please enter only char");
                return false;
            }
        }

</script>

    <style type="text/css">
        .cssVEh
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
            
        }
        .modal {
                position: fixed;
                top: 50%;
                 left: 50%;
                   transform: translate(-50%, -50%);
            }
        .hideCol
        {
            display: none;
        }
        .style43
        {
            font-size: 9pt;
            font-family: verdana;
            color: #515456;
            border: 0px solid #CCCCCC;
            text-align: left;
            padding-left: 0px;
            width: 220px;
            height: 40px;
        }
        .style45
        {
            height: 29px;
            width: 220px;
        }
        
        
        .TDClassLabel1vaibhav
        {
            font-family: Verdana;
            font-size: 9pt;
            color: #515456;
            border: 0px solid #CCCCCC;
            border-width: 0px;
            text-align: right;
            padding-right: 4px;
            width: 24%;
            height: 10px;
        }
        .Display
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdConn" runat="server" />
    <asp:TableRow runat="server">
        <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
            <asp:Label ID="lblHead" runat="server" Text="Visitor Request Master" ForeColor="RoyalBlue"
                Font-Size="20px" Width="100%" CssClass="heading">
            </asp:Label>
        </asp:TableCell>
    </asp:TableRow>
    <%-- <asp:UpdatePanel ID="up1" runat="server"  UpdateMode="Conditional" ScriptMode="Release">--%>
    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="DivEmpDetails">
                <asp:Panel runat="server" ID="test" DefaultButton="btnSearch">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: left; width: 40%;">
                                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="ButtonControl" OnClick="btnNew_Click" />
                                <asp:Button ID="btnDel" runat="server" Text="Delete" CssClass="ButtonControl" OnClick="btnDel_Click" />
                            </td>
                            <td style="text-align: right; width: 40%;">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                                    OnClick="btnReset_Click" />
                                &nbsp;
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;"
                                    CssClass="ButtonControl" OnClick="btnSearch_Click" />
                                &nbsp;
                                <asp:TextBox ID="txtUserID" runat="server" Style="float: right;" Width="113px" Height="20px"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtUserID" runat="server" TargetControlID="txtUserID"
                                    WatermarkText="Mobile No." WatermarkCssClass="watermark">
                                </ajaxToolkit:TextBoxWatermarkExtender>
                                <asp:TextBox ID="txtLevelID" runat="server" Style="float: right; margin-left: 55;"
                                    Width="113px" Height="20px"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtLevelID" runat="server" TargetControlID="txtLevelID"
                                    WatermarkText="Visitor Name " WatermarkCssClass="watermark">
                                </ajaxToolkit:TextBoxWatermarkExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color: #EFF8FE; padding: 0px;" colspan="3">
                                <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                                    <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GvVisitor" runat="server" AutoGenerateColumns="false" Width="100%"
                                            GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="GvVisitor_RowCommand"
                                            OnRowDataBound="GvVisitor_RowDataBound">
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
                                                        <asp:CheckBox ID="DeleteRows" runat="server" ClientIDMode="Static" Onclick="validateCheckFocus()" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkModify" runat="server" ForeColor="#3366FF" CommandName="Modify"
                                                            Autopostback="true" CommandArgument='<%#Eval("RequestID")%>'>Edit</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RequestID" HeaderText="Visitor Name" ItemStyle-CssClass="Display"
                                                    HeaderStyle-CssClass="Display" />
                                                <%--<asp:BoundField DataField="Visitor_Name" HeaderText="Name" ItemStyle-Wrap="true"
                                                    ItemStyle-Width="150px" />--%>
                                                <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" Width="11%" />
                                                    <ItemTemplate>
                                                        <div style="width: 180px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                            <%# Eval("Visitor_Name")%>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="VisitorCompany" HeaderText="Company" ItemStyle-Wrap="true"
                                                    ItemStyle-Width="150px" />--%>
                                                <asp:TemplateField HeaderText="Company" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" Width="11%" />
                                                    <ItemTemplate>
                                                        <div style="width: 180px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                            <%# Eval("VisitorCompany")%>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="nature_of_work" HeaderText="Nature Of Work" />
                                                <asp:BoundField DataField="mobileNo" HeaderText="Mobile No." />
                                                <asp:BoundField DataField="appointment_from_date" HeaderText="From Date" />
                                                <asp:BoundField DataField="appointment_to_date" HeaderText="To Date" />
                                                <asp:BoundField DataField="Visitor_Allowed_From_time" HeaderText="Allowed From Time" />
                                                <asp:BoundField DataField="visitor_Allowed_To_Time" HeaderText="Allowed To Time" />
                                                <asp:BoundField DataField="RequestedDate" HeaderText="Request Date" />
                                                <asp:BoundField DataField="status" HeaderText="Request Status" />
                                                <%--     <asp:TemplateField HeaderText="Scan Document" SortExpression="Scan Document" HeaderStyle-CssClass="center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkScanDocumnent" runat="server" CausesValidation="False" CommandName="ScanDocumnent"
                                                    CommandArgument='<%#Eval("RequestID") %>' Text="Capture" Font-Bold="true" ForeColor="#3366FF"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Document Upload" SortExpression="Scan Document" HeaderStyle-CssClass="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkScanDocumnent" runat="server" CausesValidation="False" CommandName="ScanDocumnent"
                                                            CommandArgument='<%#Eval("RequestID") %>' Text="Capture" Font-Bold="true" ForeColor="#3366FF"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
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
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100%; text-align: center; font-weight: bold; color: Black; font-size: medium">
                        Records in <font color="red">RED</font>&nbsp; indicates requests which are awaiting
                        Visitor documents upload. Document upload is mandatory for foreign visitors.
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <div class="DivEmpDetails" style="display: none;">
                        <div style="color: White; text-align: left">
                            <b>RFID Tag Details : </b>Tag issued : <b><font color="white">
                                <asp:Label runat="server" ID="lblIsshued" Style="padding-right: 10%"> </asp:Label></font></b>
                            Tag in Inventory : <b><font color="white">
                                <asp:Label runat="server" ID="lblInventory" Style="padding-right: 10%"> </asp:Label></font></b>
                            Total Tags : <b><font color="white">
                                <asp:Label runat="server" ID="lblTotalTags" Style="padding-right: 10%"> </asp:Label></font></b>
                            Total Enabled : <b><font color="white">
                                <asp:Label runat="server" ID="lblEnabled" Style="padding-right: 10%"> </asp:Label></font></b>
                            Total Disabled : <b><font color="white">
                                <asp:Label runat="server" ID="lblDisabled" Style="padding-right: 10%"> </asp:Label></font></b>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanelmsg" runat="server">
                <ContentTemplate>
                    <div style="width: 100%; height: 2%;" align="center">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMsg" runat="server" Style="color: Red;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Button ID="btn_sub" runat="server" Style="display: none;" Text="test" />
            <asp:Button ID="Button1" runat="server" Text="Yes" Style="display: none;" />
            <asp:Button ID="Button2" runat="server" Text="No" Style="display: none;" />
            <%-- <ajaxToolkit:ModalPopupExtender ID="mpeDelVehicle" runat="server" Enabled="True"
        BackgroundCssClass="cssVEh" TargetControlID="btn_sub" PopupControlID="pnl_del_project"
        OkControlID="btn_cl">
    </ajaxToolkit:ModalPopupExtender>--%>
            <asp:Panel ID="pnlAddEmployee" runat="server" CssClass="PopupPanel" Width="65%">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label ID="Label4" runat="server" Text="New Visitor" ForeColor="RoyalBlue" Font-Size="20px"
                                        Width="100%" CssClass="heading">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="2" align="right" class="TDClassLabel1vaibhav" style="text-align: right;
                                    font-weight: bold;" width="20%">
                                    <asp:Label ID="Label27" runat="server" Text="Search Visitor"></asp:Label>
                                </td>
                                <td align="left" width="80%">
                                    <%--<asp:TextBox ID="txtSerchVisitor" runat="server" Width="356px" style="border-radius:10px" ></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddelSerchVisitor" runat="server" AutoPostBack="true" TabIndex="20"
                                        Width="356px" class="chosen-select" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                                        Font-Bold="True">
                                    </asp:DropDownList>
                                    <%--<asp:ListBox ID="lstVisitor" runat="server"></asp:ListBox>--%>
                            </tr>
                        </table>
                        <table style="width: 100%; padding: 1px;">
                            <tr>
                                <td width="72%">
                                    <fieldset style="width: 99%; border-radius: 10px;">
                                        <legend style="color: Green; padding-left: 5px; padding-right: 5px; font-weight: bold;">
                                            Visitor Details :</legend>
                                        <table id="table2" runat="server" height="90%" border="0" cellpadding="0" width="100%"
                                            style="margin-left: 6px; padding: 5px;" cellspacing="0">
                                            <tr id="Tr19" runat="server">
                                                <td class="TDClassLabel1vaibhav" runat="server" style="text-align: left; font-weight: bold;">
                                                    First Name :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td50" style="height: 10px; width: 200px;" class="TDClassControl1" runat="server">
                                                    <asp:TextBox ID="txtVisitorName" runat="server" MaxLength="20" ValidationGroup="addVisitor"
                                                        Width="141px" onKeyPress="return checkNum()"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter First Name"
                                                        ControlToValidate="txtVisitorName" Display="None" ValidationGroup="addVisitor"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                        TargetControlID="RequiredFieldValidator3" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                           
                                                    <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                        FilterType="UppercaseLetters,LowercaseLetters" TargetControlID="txtVisitorName" />
                                                    <br />--%>
                                                </td>
                                                <td id="Td51" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    font-weight: bold;">
                                                    Middle Name :&nbsp;<label class="CompulsaryLabel"></label>
                                                </td>
                                                <td id="Td52" runat="server" class="style45">
                                                    <asp:TextBox ID="txtMiddleName" runat="server" MaxLength="25" Width="160px"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                        FilterType="UppercaseLetters,LowercaseLetters" TargetControlID="txtMiddleName" />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr id="Tr121" runat="server">
                                                <td id="ass3" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    font-weight: bold;">
                                                    Last Name :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td1" style="height: 10px; width: 200px;" class="TDClassControl1" runat="server">
                                                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="20" ValidationGroup="addVisitor"
                                                        Width="141px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="Please Enter Last Name"
                                                        ControlToValidate="txtLastName" Display="None" ValidationGroup="addVisitor" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server"
                                                        TargetControlID="RequiredFieldValidator21" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                        FilterType="UppercaseLetters,LowercaseLetters" TargetControlID="txtLastName" />
                                                    <br />
                                                </td>
                                                <td id="Td2" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    font-weight: bold;">
                                                    Company Name :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td3" runat="server" class="style45">
                                                    <asp:TextBox ID="txtComapany" runat="server" MaxLength="50" ValidationGroup="addVisitor"
                                                        Width="160px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Company Name"
                                                        ControlToValidate="txtComapany" Display="None" ValidationGroup="addVisitor" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                        TargetControlID="RequiredFieldValidator4" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr id="Tr5" runat="server">
                                                <td id="Td124" class="TDClassLabel1vaibhav" runat="server" style="padding-right: 50px;
                                                    text-align: left; font-weight: bold;">
                                                    Mobile No :<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td32" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="txtmob" runat="server" onkeypress="return isNumberKey(event);" MaxLength="3"
                                                        ValidationGroup="addVisitor" Width="30px"></asp:TextBox>
                                                    <asp:TextBox ID="txtMobilleNo" runat="server" MaxLength="10" ValidationGroup="addVisitor"
                                                        Width="106px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter Mobile No."
                                                        ControlToValidate="txtmob" Display="None" ValidationGroup="addVisitor" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender27" runat="server"
                                                        TargetControlID="RequiredFieldValidator8" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Mobile No."
                                                        ControlToValidate="txtMobilleNo" Display="None" ValidationGroup="addVisitor"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                        TargetControlID="RequiredFieldValidator5" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        FilterType="Numbers" TargetControlID="txtMobilleNo" />
                                                    <br />
                                                </td>
                                                <td id="Td17" class="TDClassLabel1vaibhav" style="height: 10px; text-align: left;
                                                    font-weight: bold;" runat="server">
                                                    Nature Of Work :<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td18" runat="server" class="style43">
                                                    <asp:DropDownList ID="ddlNatureOfVisit" runat="server" TabIndex="9" Width="160px"
                                                        ValidationGroup="addVisitor">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Offical">Offical</asp:ListItem>
                                                        <asp:ListItem Value="Interview">Interview</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Select Nature Of Work"
                                                        ControlToValidate="ddlNatureOfVisit" Display="None" ValidationGroup="addVisitor"
                                                        InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                        TargetControlID="RequiredFieldValidator6" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr id="Tr13" runat="server">
                                                <td id="Td41" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    width: 40px; font-weight: bold;">
                                                    Designation :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td42" style="height: 5px; width: 200px;" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="txtDesignation" runat="server" Width="141px" MaxLength="50" ValidationGroup="addVisitor"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Please Enter Visitor Designation"
                                                        ControlToValidate="txtDesignation" Display="None" ValidationGroup="addVisitor"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                                        TargetControlID="RequiredFieldValidator15" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                                <td rowspan="2" style="display: none;">
                                    <fieldset style="width: 100%; border-radius: 10px;">
                                        <legend style="color: Green; padding-left: 5px; padding-right: 5px; font-weight: bold;">
                                            Visitor Access Details :</legend>
                                        <div style="border: 1px groove lightgray; max-width: 161%; max-height: 362px; height: 387px;
                                            width: 100%; overflow: auto; border-radius: 10px;">
                                            <asp:GridView ID="gvControllerAdd" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvControllerAdd_RowDataBound"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-BackColor="lightgray" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkControllerADD" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="AL_ID" HeaderStyle-BackColor="lightgray" HeaderText="ID"
                                                        ItemStyle-HorizontalAlign="Center" />
                                                    <%--                  <asp:BoundField DataField="id" ItemStyle-CssClass="hideCol" HeaderStyle-CssClass="hideCol" />--%>
                                                    <asp:BoundField DataField="AL_DESCRIPTION" HeaderStyle-BackColor="lightgray" HeaderText="Description"
                                                        ItemStyle-HorizontalAlign="Left" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <fieldset style="width: 99%; border-radius: 10px;">
                                        <legend style="color: Green; padding-left: 5px; padding-right: 5px; font-weight: bold;">
                                            Visit Details :</legend>
                                        <table id="table1" runat="server" height="90%" border="0" cellpadding="0" width="100%"
                                            style="margin-left: 5px; padding: 5px;" cellspacing="0">
                                            <tr id="Tr8" runat="server">
                                                <td id="Td8" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    width: 40px; font-weight: bold;">
                                                    Appointment From Date:
                                                    <%-- <label class="CompulsaryLabel">*</label>--%>
                                                </td>
                                                <td id="Td9" style="height: 5px; width: 200px;" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="txtAppoinmentFromDate" runat="server" ValidationGroup="addVisitor"
                                                        onKeyPress="javascript: return false " onkeyDown="javascript: return false" Width="141px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtAppoinmentFromDate"
                                                        PopupButtonID="txtAppoinmentFromDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please Select From Date"
                                                        ControlToValidate="txtAppoinmentFromDate" Display="None" ValidationGroup="addVisitor"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                                        TargetControlID="RequiredFieldValidator11" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <asp:CompareValidator ID="CmpValtxtRFID_IssueDate" runat="server" ValidationGroup="addVisitor"
                                                        ErrorMessage="From date should be less than to date" Display="None" ControlToValidate="txtAppoinmentFromDate"
                                                        ControlToCompare="txtAppoinmentToDate" Type="Date" Operator="LessThanEqual">
                                                    </asp:CompareValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server"
                                                        PopupPosition="Right" TargetControlID="CmpValtxtRFID_IssueDate">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <br />
                                                </td>
                                                <td id="Td25" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    font-weight: bold;">
                                                    Appointment To Date :
                                                    <%--<label class="CompulsaryLabel">*</label>--%>
                                                </td>
                                                <td id="Td26" runat="server" class="style45">
                                                    <asp:TextBox ID="txtAppoinmentToDate" runat="server" ValidationGroup="addVisitor"
                                                        onKeyPress="javascript: return false " onkeyDown="javascript: return false" Width="141px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtAppoinmentToDate"
                                                        PopupButtonID="txtAppoinmentToDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please Select To Date"
                                                        ControlToValidate="txtAppoinmentToDate" Display="None" ValidationGroup="addVisitor"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                        TargetControlID="RequiredFieldValidator12" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr id="Tr1" runat="server">
                                                <td id="Td4" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    font-weight: bold;">
                                                    Appointment From Time :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td5" style="height: 10px; width: 200px;" class="TDClassControl1" runat="server">
                                                    <asp:TextBox ID="txtAppoinmentFromTime" runat="server" MaxLength="5" onkeyup="fnColon(this,event)"
                                                        ValidationGroup="addVisitor" onblur="return timeValOP(this,'09:30')" onchange="return timeVal()"
                                                        onkeypress="findspace(event)" ClientIDMode="Static" Width="141px"></asp:TextBox>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                        TargetControlID="txtAppoinmentFromTime" WatermarkText="HH:MM" WatermarkCssClass="watermark">
                                                    </ajaxToolkit:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Please Enter Appointment From Time"
                                                        ControlToValidate="txtAppoinmentFromTime" Display="None" ValidationGroup="addVisitor"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server"
                                                        TargetControlID="RequiredFieldValidator17" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FTEtxtAppoinmentFromTime" runat="server"
                                                        FilterType="Numbers,Custom" TargetControlID="txtAppoinmentFromTime" ValidChars=":" />
                                                    <asp:RegularExpressionValidator ID="REVtxtAppoinmentFromTime" runat="server" ControlToValidate="txtAppoinmentFromTime"
                                                        ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="addVisitor"></asp:RegularExpressionValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender24" runat="server"
                                                        TargetControlID="REVtxtAppoinmentFromTime" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <br />
                                                </td>
                                                <td id="Td6" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    font-weight: bold;">
                                                    Appointment To Time :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td7" runat="server" class="style45">
                                                    <asp:TextBox ID="txtAppoinmentToTime" runat="server" MaxLength="5" onkeyup="fnColon(this,event)"
                                                        ValidationGroup="addVisitor" onblur="return timeValOP(this,'18:00')" onkeypress="findspace(event)"
                                                        onchange="return timeVal()" ClientIDMode="Static" Width="141px"></asp:TextBox>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                                        TargetControlID="txtAppoinmentToTime" WatermarkText="HH:MM" WatermarkCssClass="watermark">
                                                    </ajaxToolkit:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Please Enter Appointment To Time"
                                                        ControlToValidate="txtAppoinmentToTime" Display="None" ValidationGroup="addVisitor"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="regextxttxtAppoinmentToTime" runat="server" ControlToValidate="txtAppoinmentToTime"
                                                        ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="addVisitor"></asp:RegularExpressionValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server"
                                                        TargetControlID="RequiredFieldValidator18" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FTEtxtAppoinmentToTime" runat="server" FilterType="Numbers,Custom"
                                                        TargetControlID="txtAppoinmentToTime" ValidChars=":" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender25" runat="server"
                                                        TargetControlID="regextxttxtAppoinmentToTime" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr id="Tr2" runat="server">
                                                <td id="Td11" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    width: 40px; font-weight: bold;">
                                                    Approval Authority :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td12" style="height: 5px; width: 200px;" runat="server" class="TDClassControl1">
                                                    <asp:DropDownList ID="ddlApprovalAuthority" ValidationGroup="addVisitor" runat="server"
                                                        TabIndex="20" Width="200px" class="chosen-select">
                                                    </asp:DropDownList>
                                                    <br />
                                                </td>

                                                <td id="Td64" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    width: 40px; font-weight: bold;">
                                                    Location :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>

                                                <td id="Td62" style="height: 5px; width: 200px;" runat="server" class="TDClassControl1">
                                                    <asp:DropDownList ID="ddl_location" ValidationGroup="addVisitor" runat="server" TabIndex="20" 
                                                     Width="200px" class="chosen-select">
                                                    </asp:DropDownList>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr style="height: 10px;">
                                                <td>
                                                </td>
                                            </tr>
                                            <tr id="Tr21" runat="server">
                                                <td id="Td58" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    width: 40px; font-weight: bold;">
                                                    Nationality :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td59" style="height: 5px; width: 200px;" runat="server" class="TDClassControl1">
                                                    <asp:DropDownList ID="drpnationality" TabIndex="20" Width="200px" runat="server"
                                                        ValidationGroup="addVisitor">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Indian">Indian</asp:ListItem>
                                                        <asp:ListItem Value="Foreigner">Foreigner</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr id="Tr17" runat="server">
                                                <td id="Td47" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    width: 40px; font-weight: bold;">
                                                    Purpose Of Visit :
                                                </td>
                                                <td id="Td48" style="height: 5px;" runat="server" class="TDClassControl1" colspan="3"
                                                    onpaste="return true;">
                                                    <br />
                                                    <asp:TextBox ID="txtPurposeAdd" Width="447px" runat="server" TextMode="MultiLine"
                                                        Style="max-width: 601px; min-width: 447px; min-height: 40px; max-height: 40px;
                                                        resize: none;" CssClass="TextControl" TabIndex="3" Height="50px" MaxLength="150"
                                                        onkeyDown="checkLength(this,'150');" onkeyUp="checkLength(this,'150');"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="Tr3" runat="server">
                                                <td id="Td14" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    width: 40px; font-weight: bold;">
                                                    Additional Information,If any :
                                                </td>
                                                <td id="Td15" style="height: 5px;" runat="server" class="TDClassControl1" colspan="3">
                                                    <br />
                                                    <asp:TextBox ID="txtAdditionalInfoAdd" Width="447px" runat="server" TextMode="MultiLine"
                                                        Style="max-width: 601px; min-width: 447px; min-height: 40px; max-height: 40px;
                                                        resize: none;" CssClass="TextControl" TabIndex="3" Height="50px" MaxLength="150"
                                                        onkeyDown="checkLength(this,'150');" onkeyUp="checkLength(this,'150');"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                        <table id="table10" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                            class="TableClass">
                        </table>
                        <table id="table9" runat="server" width="100%" border="0" cellpadding="3" cellspacing="3">
                            <tr id="Tr15" runat="server">
                                <td id="Td10" align="center" runat="server">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="ButtonControl" ValidationGroup="addVisitor"
                                        OnClientClick="return PopUpmsg();" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnReset1" runat="server" Text="Reset" CssClass="ButtonControl" OnClick="btnReset1_Click" />
                                    <asp:Button ID="Btnclear1" runat="server" Text="Cancel" CssClass="ButtonControl"
                                        OnClick="Btnclear1_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td align="center" class="style39">
                                    <asp:Label ID="lblSaveMessages" Style="text-align: center;" runat="server" Text=""
                                        CssClass="ErrorLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:PostBackTrigger ControlID="ddelSerchVisitor" />--%>
                        <%-- <asp:PostBackTrigger ControlID="btnNew" />--%>
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Button ID="Button8" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="mpeAddEmployee" runat="server" TargetControlID="Button8"
                BehaviorID="ModalBehaviour" PopupControlID="pnlAddEmployee" BackgroundCssClass="modalBackground"
                Enabled="true">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlEditVisitor" runat="server" CssClass="PopupPanel" Width="85%">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Edit Visitor" ForeColor="RoyalBlue" Font-Size="20px"
                                        Width="100%" CssClass="heading">
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%; padding: 1px;">
                            <tr>
                                <td width="75%">
                                    <fieldset style="width: 99%; border-radius: 10px;">
                                        <legend style="color: Green; padding-left: 5px; padding-right: 5px; font-weight: bold;">
                                            Visitor Details :</legend>
                                        <table id="table3" runat="server" height="90%" border="0" cellpadding="0" width="100%"
                                            style="margin-left: 6px; padding: 5px;" cellspacing="0">
                                            <tr id="Tr18" runat="server">
                                                <td id="Td49" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    font-weight: bold;">
                                                    First Name :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td54" style="height: 10px; width: 200px;" class="TDClassControl1" runat="server">
                                                    <asp:TextBox ID="txtVisitornameEdit" runat="server" MaxLength="20" ValidationGroup="EditVisitor"
                                                        Width="141px" onKeyPress="return checkNum()"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Enter First Name"
                                                        ControlToValidate="txtVisitornameEdit" Display="None" ValidationGroup="EditVisitor"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server"
                                                        TargetControlID="RequiredFieldValidator9" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        FilterType="UppercaseLetters,LowercaseLetters" TargetControlID="txtVisitornameEdit" />--%>
                                                    <br />
                                                </td>
                                                <td id="Td55" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    font-weight: bold;">
                                                    Middle Name :&nbsp;<label class="CompulsaryLabel"></label>
                                                </td>
                                                <td id="Td56" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="txtMiddleNameEdit" runat="server" MaxLength="20" Width="160px"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                        FilterType="UppercaseLetters,LowercaseLetters" TargetControlID="txtMiddleNameEdit" />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr id="Tr4" runat="server">
                                                <td id="Td13" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    font-weight: bold;">
                                                    Last Name :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td16" style="height: 10px; width: 200px;" class="TDClassControl1" runat="server">
                                                    <asp:TextBox ID="txtlastNameEdit" runat="server" MaxLength="20" ValidationGroup="EditVisitor"
                                                        Width="141px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Please Enter Last Name"
                                                        ControlToValidate="txtlastNameEdit" Display="None" ValidationGroup="EditVisitor"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" runat="server"
                                                        TargetControlID="RequiredFieldValidator22" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                        FilterType="UppercaseLetters,LowercaseLetters" TargetControlID="txtlastNameEdit" />
                                                    <br />
                                                </td>
                                                <td id="Td19" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    font-weight: bold;">
                                                    Company Name :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td20" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="txtCompanyNameEdit" runat="server" ValidationGroup="EditVisitor"
                                                        Width="141px" MaxLength="50"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Enter Company Name"
                                                        ControlToValidate="txtCompanyNameEdit" Display="None" ValidationGroup="EditVisitor"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                                        TargetControlID="RequiredFieldValidator10" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr id="Tr6" runat="server">
                                                <td id="Td21" class="TDClassLabel1vaibhav" runat="server" style="padding-right: 50px;
                                                    text-align: left; font-weight: bold;">
                                                    Mobile No :<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td22" runat="server" class="TDClassControl1" style="width: 200px;">
                                                    <asp:TextBox ID="txtmobedit" runat="server" onkeypress="return isNumberKey(event);"
                                                        number MaxLength="3" ValidationGroup="EditVisitor" Width="30px"></asp:TextBox>
                                                    <asp:TextBox ID="txtMobileNoEdit" runat="server" MaxLength="15" ValidationGroup="EditVisitor"
                                                        Width="100px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Please Enter Mobile No."
                                                        ControlToValidate="txtmobedit" Display="None" ValidationGroup="EditVisitor"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" runat="server"
                                                        TargetControlID="RequiredFieldValidator16" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Please Enter Mobile No."
                                                        ControlToValidate="txtCompanyNameEdit" Display="None" ValidationGroup="EditVisitor"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
                                                        TargetControlID="RequiredFieldValidator13" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        FilterType="Numbers" TargetControlID="txtMobileNoEdit" />
                                                    <br />
                                                </td>
                                                <td id="Td23" class="TDClassLabel1vaibhav" style="height: 10px; text-align: left;
                                                    font-weight: bold;" runat="server">
                                                    Nature Of Work : &nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td24" runat="server" class="TDClassControl1">
                                                    <asp:DropDownList ID="ddlnatureOfworkEdit" runat="server" TabIndex="9" Width="160px"
                                                        ValidationGroup="EditVisitor">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Offical">Offical</asp:ListItem>
                                                        <asp:ListItem Value="Interview">Interview</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select Nature Of Work"
                                                        ControlToValidate="ddlnatureOfworkEdit" Display="None" ValidationGroup="EditVisitor"
                                                        InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                        TargetControlID="RequiredFieldValidator7" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr id="Tr14" runat="server">
                                                <td id="Td43" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    width: 40px; font-weight: bold;">
                                                    Designation :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td44" style="height: 5px; width: 200px;" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="txtDesignationEdit" runat="server" ValidationGroup="EditVisitor"
                                                        Width="141px" MaxLength="50"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please Enter Designation"
                                                        ControlToValidate="txtDesignationEdit" Display="None" ValidationGroup="EditVisitor"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
                                                        TargetControlID="RequiredFieldValidator14" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                                <td rowspan="2" style="display: none;">
                                    <fieldset style="width: 100%; border-radius: 10px;">
                                        <legend style="color: Green; padding-left: 5px; padding-right: 5px; font-weight: bold;">
                                            Visitor Access Details :</legend>
                                        <div style="border: 1px groove lightgray; max-width: 161%; max-height: 362px; height: 387px;
                                            width: 100%; overflow: auto; border-radius: 10px;">
                                            <asp:GridView ID="gvControllerEdit" runat="server" AutoGenerateColumns="false" DataKeyNames="Al_id"
                                                OnRowDataBound="gvControllerEdit_RowDataBound" Width="100%" Style="margin-left: 0">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-BackColor="lightgray" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkControllerEdit" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="AL_ID" HeaderStyle-BackColor="lightgray" HeaderText="ID"
                                                        ItemStyle-HorizontalAlign="Center" />
                                                    <%--                  <asp:BoundField DataField="id" ItemStyle-CssClass="hideCol" HeaderStyle-CssClass="hideCol" />--%>
                                                    <asp:BoundField DataField="AL_DESCRIPTION" HeaderStyle-BackColor="lightgray" HeaderText="Description"
                                                        ItemStyle-HorizontalAlign="Left" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <fieldset style="width: 99%; border-radius: 10px;">
                                        <legend style="color: Green; padding-left: 5px; padding-right: 5px; font-weight: bold;">
                                            Visit Details :</legend>
                                        <table id="table4" runat="server" height="90%" border="0" cellpadding="0" width="100%"
                                            style="margin-left: 5px; padding: 5px" cellspacing="0">
                                            <tr id="Tr7" runat="server">
                                                <td id="Td27" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    width: 40px; font-weight: bold;">
                                                    Appointment From Date:
                                                    <%--<label class="CompulsaryLabel">*</label>--%>
                                                </td>
                                                <td id="Td28" style="height: 5px; width: 200px;" runat="server" class="TDClassControl1">
                                                    <asp:TextBox ID="txtAppoinmentFromDateEdit" runat="server" Width="141px" onKeyPress="javascript: return false "
                                                        onkeyDown="javascript: return false"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAppoinmentFromDateEdit"
                                                        PopupButtonID="txtAppoinmentFromDateEdit" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select From Date"
                                                        ControlToValidate="txtAppoinmentFromDateEdit" Display="None" ValidationGroup="EditVisitor"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                        TargetControlID="RequiredFieldValidator1" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="EditVisitor"
                                                        ErrorMessage="From date should be less than to date" Display="None" ControlToValidate="txtAppoinmentFromDateEdit"
                                                        ControlToCompare="txtAppoinmentToDateEdit" Type="Date" Operator="LessThanEqual">
                                                    </asp:CompareValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender26" runat="server"
                                                        PopupPosition="Right" TargetControlID="CompareValidator1">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <br />
                                                </td>
                                                <td id="Td29" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    font-weight: bold;">
                                                    Appointment To Date :
                                                    <%-- &nbsp;<label class="CompulsaryLabel">*</label>--%>
                                                </td>
                                                <td id="Td30" runat="server" class="style45">
                                                    <asp:TextBox ID="txtAppoinmentToDateEdit" runat="server" Width="141px" onKeyPress="javascript: return false "
                                                        onkeyDown="javascript: return false"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtAppoinmentToDateEdit"
                                                        PopupButtonID="txtAppoinmentToDateEdit" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select To Date"
                                                        ControlToValidate="txtAppoinmentToDateEdit" Display="None" ValidationGroup="EditVisitor"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                        TargetControlID="RequiredFieldValidator2" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr id="Tr10" runat="server">
                                                <td id="Td34" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    font-weight: bold;">
                                                    Appointment From Time :&nbsp;&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td35" style="height: 10px; width: 200px;" class="TDClassControl1" runat="server">
                                                    <asp:TextBox ID="txtAppoinMentFromtimeEdit" runat="server" Width="141px" ValidationGroup="EditVisitor"
                                                        MaxLength="5" onkeyup="fnColon(this,event)" onkeypress="findspace(event)" onblur="return timeValOP(this,'09:30')"
                                                        onchange="timeValEdit()"></asp:TextBox>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server"
                                                        TargetControlID="txtAppoinMentFromtimeEdit" WatermarkText="HH:MM" WatermarkCssClass="watermark">
                                                    </ajaxToolkit:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="Please Enter Appointment From Time"
                                                        ControlToValidate="txtAppoinMentFromtimeEdit" Display="None" ValidationGroup="EditVisitor"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server"
                                                        TargetControlID="RequiredFieldValidator19" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <asp:RegularExpressionValidator ID="REVtxtAppoinMentFromtimeEdit" runat="server"
                                                        ControlToValidate="txtAppoinMentFromtimeEdit" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$"
                                                        ErrorMessage="You must enter a valid time. Format: HH:MM" Display="None" ForeColor="Red"
                                                        SetFocusOnError="true" ValidationGroup="EditVisitor"></asp:RegularExpressionValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server"
                                                        TargetControlID="REVtxtAppoinMentFromtimeEdit" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FTEtxtAppoinMentFromtimeEdit" runat="server"
                                                        FilterType="Numbers,Custom" TargetControlID="txtAppoinMentFromtimeEdit" ValidChars=":" />
                                                    <br />
                                                </td>
                                                <td id="Td36" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    font-weight: bold;">
                                                    Appointment To Time :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td37" runat="server" class="style45">
                                                    <asp:TextBox ID="txtAppointmentToTimeEdit" runat="server" Width="141px" ValidationGroup="EditVisitor"
                                                        MaxLength="5" onkeyup="fnColon(this,event)" onkeypress="findspace(event)" onblur="return timeValOP(this,'18:00')"
                                                        onchange="timeValEdit()"></asp:TextBox>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server"
                                                        TargetControlID="txtAppointmentToTimeEdit" WatermarkText="HH:MM" WatermarkCssClass="watermark">
                                                    </ajaxToolkit:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="Please Enter Appointment To Time"
                                                        ControlToValidate="txtAppointmentToTimeEdit" Display="None" ValidationGroup="EditVisitor"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server"
                                                        TargetControlID="RequiredFieldValidator20" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <asp:RegularExpressionValidator ID="REtxtAppointmentToTimeEdit" runat="server" ControlToValidate="txtAppointmentToTimeEdit"
                                                        ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="You must enter a valid time. Format: HH:MM"
                                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="EditVisitor"></asp:RegularExpressionValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender23" runat="server"
                                                        TargetControlID="REtxtAppointmentToTimeEdit" PopupPosition="Right">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FTEtxtAppointmentToTimeEdit" runat="server"
                                                        FilterType="Numbers,Custom" TargetControlID="txtAppointmentToTimeEdit" ValidChars=":" />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr id="Tr9" runat="server">
                                                <td id="Td31" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    width: 40px; font-weight: bold;">
                                                    Approval Authority :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td33" style="height: 5px; width: 200px;" runat="server" class="TDClassControl1">
                                                    <asp:DropDownList ID="ddApprovalEdit" runat="server" TabIndex="20" Width="200px"
                                                        ValidationGroup="EditVisitor" class="chosen-select">
                                                    </asp:DropDownList>
                                                    <%--                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Select Approval Authority"
                                                ControlToValidate="ddApprovalEdit" Display="None" ValidationGroup="EditVisitor"
                                                InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                    <br />
                                                </td>
                                                <td id="Td65" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    width: 40px; font-weight: bold;">
                                                    Location :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>

                                                <td id="Td66" style="height: 5px; width: 200px;" runat="server" class="TDClassControl1">
                                                    <asp:DropDownList ID="ddl_location_edit" ValidationGroup="EditVisitor" runat="server"
                                                        TabIndex="20" Width="200px" class="chosen-select">
                                                    </asp:DropDownList>
                                                    <br />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td style="height: 10px;">
                                                </td>
                                            </tr>
                                            <tr id="Tr22" runat="server">
                                                <td id="Td60" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    width: 40px; font-weight: bold;">
                                                    Nationality :&nbsp;<label class="CompulsaryLabel">*</label>
                                                </td>
                                                <td id="Td61" style="height: 5px; width: 200px;" runat="server" class="TDClassControl1">
                                                    <asp:DropDownList ID="drpnationalityedit" runat="server" TabIndex="20" Width="200px"
                                                        ValidationGroup="EditVisitor">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Indian">Indian</asp:ListItem>
                                                        <asp:ListItem Value="Foreigner">Foreigner</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Select Approval Authority"
                                                ControlToValidate="ddApprovalEdit" Display="None" ValidationGroup="EditVisitor"
                                                InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr id="Tr16" runat="server">
                                                <td id="Td45" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    width: 40px; font-weight: bold;">
                                                    Purpose Of Visit :
                                                </td>
                                                <td id="Td46" style="height: 5px;" runat="server" class="TDClassControl1" colspan="3">
                                                    <br />
                                                    <asp:TextBox ID="txtPurposeEdit" Width="601px" runat="server" TextMode="MultiLine"
                                                        Style="max-width: 601px; min-width: 601px; min-height: 40px; max-height: 40px"
                                                        CssClass="TextControl" TabIndex="3" Height="50px" MaxLength="150" onkeyDown="checkLength(this,'150');"
                                                        onkeyUp="checkLength(this,'150');"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="Tr11" runat="server">
                                                <td id="Td38" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    width: 40px; font-weight: bold;">
                                                    Additional Information,If any :
                                                </td>
                                                <td id="Td39" style="height: 5px;" runat="server" class="TDClassControl1" colspan="3">
                                                    <br />
                                                    <asp:TextBox ID="txtAdditionalInformationEdit" Width="601px" runat="server" TextMode="MultiLine"
                                                        Style="max-width: 601px; min-width: 601px; min-height: 40px; max-height: 40px"
                                                        CssClass="TextControl" TabIndex="3" Height="50px" MaxLength="150" onkeyDown="checkLength(this,'150');"
                                                        onkeyUp="checkLength(this,'150');"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="Tr20" runat="server">
                                                <td id="Td53" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                                    width: 40px; font-weight: bold;">
                                                    Remarks :
                                                </td>
                                                <td id="Td57" style="height: 5px;" runat="server" class="TDClassControl1" colspan="3">
                                                    <br />
                                                    <asp:TextBox ID="txt_remarks" Width="601px" runat="server" TextMode="MultiLine" ReadOnly="true"
                                                        Style="max-width: 601px; min-width: 601px; min-height: 40px; max-height: 40px"
                                                        CssClass="TextControl" TabIndex="3" Height="50px" MaxLength="150" onkeyDown="checkLength(this,'150');"
                                                        onkeyUp="checkLength(this,'150');"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                        <table id="table5" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                            class="TableClass">
                        </table>
                        <table id="table6" runat="server" width="100%" border="0" cellpadding="3" cellspacing="3">
                            <tr id="Tr12" runat="server">
                                <td id="Td40" align="center" runat="server">
                                    <asp:Button ID="btnSubmitEdit" runat="server" Text="Save" ValidationGroup="EditVisitor"
                                        OnClientClick="return Edit_PopUpmsg();" CssClass="ButtonControl" OnClick="btnSubmitEdit_Click" />
                                    <asp:Button ID="btnflush" runat="server" Text="Cancel" CssClass="ButtonControl" OnClick="btnflush_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td align="center" class="style39">
                                    <asp:Label ID="lblerrlMsg" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Button ID="Button5" runat="server" Style="display: none;" Text="test" />
            <asp:Button ID="Button6" runat="server" Text="Yes" Style="display: none;" />
            <asp:Button ID="Button7" runat="server" Text="No" Style="display: none;" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" Enabled="True"
                BackgroundCssClass="cssVEh" TargetControlID="Button6" PopupControlID="pnlEditVisitor"
                OkControlID="Button7">
            </ajaxToolkit:ModalPopupExtender>
            <%--add for documment Shrikant--%>
            <asp:Panel ID="Pannel_Document" runat="server" CssClass="PopupPanel" Width="45%">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label ID="lbl_documnet" runat="server" Text="Document Upload" ForeColor="RoyalBlue"
                                        Font-Size="20px" Width="100%" CssClass="heading">
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <fieldset style="width: 98%; border-radius: 10px;">
                                        <table style="width: 100%; padding: 5px;">
                                            <tr id="Tr23" runat="server">
                                                <td>
                                                    <asp:FileUpload ID="FileUpload1" AllowMultiple="true" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="UploadButton1" runat="server" Text="Upload File" OnClick="UploadButton1_Click"
                                                        CssClass="ButtonControl" />
                                                    <br />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_Message" runat="server" Text="" Style="color: Red;" />
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <fieldset style="width: 98%; border-radius: 10px;">
                                        <table style="width: 100%; padding: 5px;">
                                            <tr>
                                                <td style="background-color: #EFF8FE; padding: 0px;" colspan="3">
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <%--<asp:GridView ID="GV_Documment" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="GV_Documment_RowCommand"
                                                                OnRowDataBound="GV_Documment_RowDataBound">
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
                                                                <asp:BoundField DataField="FilePath" HeaderText="File" ItemStyle-CssClass="doc_lbl" />
                                                              <asp:TemplateField HeaderText="File" ItemStyle-CssClass="doc_lbl">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkFile" runat="server" ForeColor="#3366FF" CommandName="File"
                                                                                Autopostback="true" CommandArgument='<%#Eval("FilePath")%>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                            
                                                                <asp:TemplateField HeaderText="Remove" ItemStyle-CssClass="doc_lbl">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkReomve" runat="server" ForeColor="#3366FF" CommandName="Remove"
                                                                                Autopostback="true" CommandArgument='<%#Eval("FilePath")%>'>Remove</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>--%>
                                                           <asp:GridView ID="GV_Documment" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="GV_Documment_RowCommand"
                                                                OnRowDataBound="GV_Documment_RowDataBound">
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
                                                                    <asp:BoundField DataField="FilePath" HeaderText="File" ItemStyle-CssClass="doc_lbl" />
                                                                          <asp:TemplateField HeaderText="View" ItemStyle-CssClass="doc_lbl">  
                                                                    <ItemTemplate>  
                                                                     <asp:LinkButton ID="lnkbtn" runat="server" Text="View" CommandName="View"  CommandArgument='<%#Eval("URL")%>'/>
                                                                       
                                                                </ItemTemplate>  
                                                                </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Remove" ItemStyle-CssClass="doc_lbl">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkReomve" runat="server" ForeColor="#3366FF" CommandName="Remove"
                                                                                Autopostback="true" CommandArgument='<%#Eval("FilePath")%>'>Remove</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>


                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <table id="table7" runat="server" width="100%" border="0" cellpadding="3" cellspacing="3">
                                <tr id="Tr24" runat="server">
                                    <td id="Td63" align="center" runat="server">
                                        <asp:Button ID="Btn_Cancel_Doc" runat="server" Text="Cancel" CssClass="ButtonControl"
                                            OnClick="Btn_Cancel_Doc_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" runat="server">
                                        <asp:Label ID="lbl_document" runat="server" Style="color: Red;"></asp:Label>
                                        
                                    </td>
                                </tr>
                            </table>
                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:AsyncPostBackTrigger ControlID="UploadButton1" EventName="Click" />--%>
                        <asp:PostBackTrigger ControlID="UploadButton1" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Panel ID="pnlViewImage" runat="server" CssClass="PopupPanel" Width="50%" Height="50%" BorderStyle="Groove" BorderWidth="2px" BorderColor="DarkGray">
            <table><tr><td> <a href="#" class="boxclose" onclick="return ShowModalPopup()" ></a></td></tr></table>
           
               <%-- <asp:Button ID="btnCloseImage" runat="server" text="CLOSE" CssClass="CloseBtn" OnClientClick="return ShowModalPopup();"
                    onclick="btnCloseImage_Click" />--%>
                <asp:Image ID="Im" runat="server" Height="100%" ImageAlign="Middle" BorderStyle="Solid" BorderColor="Black" BorderWidth="4px" />
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="modalPnlViewImage" runat="server" Enabled="true" BehaviorID="mpe"
                BackgroundCssClass="cssVEh" TargetControlID="btnPnlViewImage" PopupControlID="pnlViewImage">
            </ajaxToolkit:ModalPopupExtender>
        

            <asp:Button ID="btnPnlViewImage" runat="server" Text="No" Style="display: none;" />

            <asp:Button ID="Btn_pnldoc" runat="server" Text="No" Style="display: none;" />
            <asp:Button ID="Btn_pnldoc2" runat="server" Text="No" Style="display: none;" />
            <ajaxToolkit:ModalPopupExtender ID="ModelPopupFor_Document" runat="server" Enabled="true"
                BackgroundCssClass="cssVEh" TargetControlID="Btn_pnldoc" PopupControlID="Pannel_Document"
                OkControlID="Btn_pnldoc2" BehaviorID="mpeDoc" >
            </ajaxToolkit:ModalPopupExtender>
            <%--End for documment--%>
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


                function ShowModalPopup() {
                    $find("mpe").hide();
                    $find("mpeDoc").show(); 
                    return false;
                }

            </script>
            <script type="text/javascript">
                function abc() {
                    var config = {
                        '.chosen-select': {},
                        '.chosen-select-deselect': { allow_single_deselect: true },
                        '.chosen-select-no-single': { disable_search_threshold: 10 },
                        '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                        '.chosen-select-width': { width: "95%" }
                        //chosen:showing_dropdown
                    }
                    for (var selector in config) {
                        $(selector).chosen(config[selector]);
                    }

                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
