<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="SACCapturePersoDetails.aspx.cs" Inherits="UNO.SACCapturePersoDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvEmpDetails.ClientID %>');

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
    <script type="text/javascript">
        var oShell = new ActiveXObject("Shell.Application");
        var WshShell = new ActiveXObject("WScript.Shell");
        var commandtoRun = "C:\\Program Files\\CMS\\Enrollment\\NativeTemplate.exe";
        function Image(val) {
            try {
                var gridView = document.getElementById('<%= this.gvEmpDetails.ClientID %>');
                gridView.disabled = true;
                gridView.style.cursor = 'wait';
                var gridViewControls = gridView.getElementsByTagName("a");

                for (i = 0; i < gridViewControls.length; i++) {
                    gridViewControls[i].disabled = true;
                    gridViewControls[i].style.cursor = 'wait';
                }

                //var Return = WshShell.Run("C:/Enrollment/NativeTemplate.exe " + val + " ISO Enrollment Image", 1, true);

                var Return = WshShell.Run("C:/Enrollment/Image_Capture.exe " + val, 1, true);


                gridView.disabled = false;
                gridView.style.cursor = 'default';
                for (i = 0; i < gridViewControls.length; i++) {
                    gridViewControls[i].disabled = false;
                    gridViewControls[i].style.cursor = 'default';
                }

            }
            catch (e) {
                alert(e.Message);
            }

        }
        function Sign(val) {
            try {
                var gridView = document.getElementById('<%= this.gvEmpDetails.ClientID %>');
                gridView.disabled = true;
                gridView.style.cursor = 'wait';
                var gridViewControls = gridView.getElementsByTagName("a");

                for (i = 0; i < gridViewControls.length; i++) {
                    gridViewControls[i].disabled = true;
                    gridViewControls[i].style.cursor = 'wait';
                }

                var Return = WshShell.Run("C:/Enrollment/NativeTemplate.exe " + val + " ISO Enrollment Sign", 1, true);

                gridView.disabled = false;
                gridView.style.cursor = 'default';
                for (i = 0; i < gridViewControls.length; i++) {
                    gridViewControls[i].disabled = false;
                    gridViewControls[i].style.cursor = 'default';
                }
            }
            catch (e) {
                alert(e.Message);
            }

        }
        function Finger(val) {
            try {
                var gridView = document.getElementById('<%= this.gvEmpDetails.ClientID %>');
                gridView.disabled = true;
                gridView.style.cursor = 'wait';
                var gridViewControls = gridView.getElementsByTagName("a");

                for (i = 0; i < gridViewControls.length; i++) {
                    gridViewControls[i].disabled = true;
                    gridViewControls[i].style.cursor = 'wait';
                }

                var Return = WshShell.Run("C:/Enrollment/NativeTemplate.exe " + val + " ISO Enrollment Bio", 1, true);

                gridView.disabled = false;
                gridView.style.cursor = 'default';
                for (i = 0; i < gridViewControls.length; i++) {
                    gridViewControls[i].disabled = false;
                    gridViewControls[i].style.cursor = 'default';
                }

            } catch (e) {
                alert(e.Message);
            }


        }
       
    </script>
    <script type="text/javascript">
        function WriteOnCard() {
            try {

                var EmployeeCode = document.getElementById("<%= lblEmpCode.ClientID %>").value;
                //alert(EmployeeCode);

                $.ajax({
                    url: "SACCapturePersoDetails.aspx/GetISOTemplate",
                    type: "POST",
                    dataType: "json",
                    data: "{'EmployeeCode':'" + EmployeeCode + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {


                        if (msg.d == "False") {
                            alert("Can not write on card,employee not enrolled or card is already issued.");
                        }
                        else {
                            //  alert(msg.d);
                            var xmlDoc = $.parseXML(msg.d);
                            var xml = $(xmlDoc);
                            var Finger_Template = xml.find("Table1");

                            var Template1 = $(Finger_Template).find("ISOTemplate1").text().toUpperCase();
                            var Template2 = $(Finger_Template).find("ISOTemplate2").text().toUpperCase();
                            var ActivationDate = $(Finger_Template).find("ActivationDate").text();
                            ActivationDate = parseInt(ActivationDate, 10).toString(16).toUpperCase();
                            var ExpiryDate = $(Finger_Template).find("ExpiryDate").text();
                            ExpiryDate = parseInt(ExpiryDate, 10).toString(16).toUpperCase();
                            var AadharNo = $(Finger_Template).find("AadharNo").text();
                            var CenterCode = $(Finger_Template).find("CenterCode").text();
                            var LocationCode = $(Finger_Template).find("LocationCode").text();



                            var objReadCard;
                            var DataInitialize;
                            var cardCSNR = "";
                            objReadCard = new ActiveXObject("ContactlessCardRW.Card");
                            DataInitialize = objReadCard.Initialise();
                            if (DataInitialize != "") {
                                alert("Omnikey reader not connected or Error in card Initialization");
                                return false;
                            }
                            var data;
                            data = objReadCard.ConnectToCard();
                            if (data != "") {
                                alert("Card not detected");
                                //alert("Error in connecting to card");
                                return false;
                            }

                            var WriteData = "";
                            // var EmployeeCode = EmpCode;
                            var EmployeeCodePrefix = EmployeeCode.substring(0, 2);
                            var EmpCode = EmployeeCode.substring(2, EmployeeCode.length);
                            var EmpName = document.getElementById("<%= lblEmpName.ClientID %>").value.rpad(" ", 16);
                            // var EmpName = employeeName.rpad(" ", 16);
                            WriteData = ConvertStringToHex(EmployeeCodePrefix) + ConvertStringToHex(EmpCode).toUpperCase() + ConvertStringToHex(EmpName).toUpperCase() + ActivationDate + ExpiryDate + "00" + "01" + "02" + (Template1.length).toString(16).lpad("0", 4).toUpperCase() + (Template2.length).toString(16).lpad("0", 4).toUpperCase() + AadharNo.lpad("0", 12) + "0000" + CenterCode + LocationCode + Template1 + Template2;


                            //  alert(WriteData);
                            var Sector = 35;
                            for (var i = 0, j = 176; i < 16 && Sector < 40; i++, j++) {
                                if (i == 15) {
                                    data = objReadCard.Authenticate(j, "FFFFFFFFFFFF", 96);
                                    if (data != "") {
                                        alert("Unable to Authenticate Block " + j);
                                        return false;
                                    }
                                    var key = "FFFFFFFFFFFF" + "FF078069" + "FFFFFFFFFFFF";
                                    key = objReadCard.Convert(key);
                                    data = objReadCard.WriteData(key, parseInt(j, 10).toString(16).toUpperCase());
                                    if (data != "") {
                                        alert("Card Error: Unable to write in the card.Sector " + Sector + " Block " + j);
                                        return false;
                                    }
                                    // alert("Key Sector " + Sector + " Block " + j + " : " + key);
                                    i = 0 - 1;
                                    Sector = Sector + 1;
                                }
                                else {
                                    var strWriteData = WriteData.substring(0, 32).rpad("0", 32);
                                    WriteData = WriteData.substring(32, WriteData.length);
                                    alert("Data Sector " + Sector + " Block " + j + " : " + strWriteData);
                                    data = objReadCard.Authenticate(j, "FFFFFFFFFFFF", 96);
                                    if (data != "") {
                                        alert("Unable to Authenticate Block " + j);
                                        return false;
                                    }
                                    strWriteData = objReadCard.Convert(strWriteData);
                                    data = objReadCard.WriteData(strWriteData, parseInt(j, 10).toString(16).toUpperCase());
                                    if (data != "") {
                                        alert("Card Error: Unable to write in the card.Sector " + Sector + " Block " + j);
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                });
            }
            catch (ex) {
                alert(ex.Message);
            }
        }

        function ConvertStringToHex(strg) {
            try {
                var hex, i;
                var str = strg;
                var result = "";
                for (i = 0; i < str.length; i++) {
                    hex = str.charCodeAt(i).toString(16);
                    result += hex;
                }
                return result;
            }
            catch (ex) {
                alert(ex.Message + " ConvertStringToHex");
            }
        }

        function dateFromISO(s) {
            s = s.split(/\D/);
            return new Date(Date.UTC(s[0], --s[1] || '', s[2] || '', s[3] || '', s[4] || '', s[5] || '', s[6] || ''))
        }

        //pads right
        String.prototype.rpad = function (padString, length) {
            var str = this;
            while (str.length < length)
                str = str + padString;
            return str;
        }

        String.prototype.lpad = function (padString, length) {
            var str = this;
            while (str.length < length)
                str = padString + str;
            return str;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Employee View" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div id="divMain">
        <div class="DivEmpDetails">
         <asp:Panel ID="Panel3" runat="server" DefaultButton="btnSearch">

            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%; text-align: left;">
                    </td>
                    <td style="width: 50%; text-align: right;">
                        <asp:Button ID="btnReset" runat="server" Text="Reset" Style="float: right;" CssClass="ButtonControl"
                            OnClick="btnReset_Click1" />
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                            OnClick="btnSearch_Click" />
                         <asp:TextBox ID="txtEmpName" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtEmpName" runat="server" TargetControlID="txtEmpName"
                            WatermarkText="Search by Employee Name" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:TextBox ID="txtEmpID" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtEmpID" runat="server" TargetControlID="txtEmpID"
                            WatermarkText="Search by Employee ID" WatermarkCssClass="watermark">
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
                                <asp:GridView ID="gvEmpDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                    GridLines="None" AllowPaging="True" OnRowDataBound="gvEmpDetails_RowDataBound"
                                    OnRowCommand="gvEmpDetails_RowCommand">
                                    <RowStyle CssClass="gvRow" />
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                    <PagerStyle CssClass="gvPager" />
                                    <EmptyDataTemplate>
                                        <div>
                                            <span>No Employees found.</span>
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="EPD_EMPID" HeaderText="Employee Code" ItemStyle-Width="100%"
                                            ItemStyle-Wrap="true" SortExpression="EPD_EMPID">
                                            <ItemStyle Width="10%" Wrap="True" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("Emp_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Photo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblImage" runat="server" Text='<%#Eval("EmpImage")%>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="lnkImage" runat="server" CausesValidation="False" CommandName="AddImg"
                                                    CommandArgument='<%#Eval("EPD_EMPID") %>' Text="Capture" ForeColor="#3366FF"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Signature">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSign" runat="server" Text='<%#Eval("EmpSign")%>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="lnkSign" runat="server" CausesValidation="False" CommandName="AddSign"
                                                    CommandArgument='<%#Eval("EPD_EMPID") %>' Text="Capture" ForeColor="#3366FF"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Finger Print">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBio1" runat="server" Text='<%#Eval("ISOTempMin1")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblBio2" runat="server" Text='<%#Eval("ISOTempMin2")%>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="lnkBio" runat="server" CausesValidation="False" CommandName="AddBio"
                                                    CommandArgument='<%#Eval("EPD_EMPID") %>' Text="Capture" ForeColor="#3366FF"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkWriteOnCard" runat="server" CausesValidation="False" CommandName="Write"
                                                    Visible="false" CommandArgument='<%#Eval("EPD_EMPID") %>' Text=" | Write On Card"
                                                    ForeColor="#3366FF"></asp:LinkButton>
                                            </ItemTemplate>
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
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
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
        </asp:Panel>
        </div>
    </div>
    <asp:Panel ID="Panel1" runat="server" CssClass="PopupPanel">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Label ID="Label21" runat="server" Text="Employee Info" Font-Bold="True" Font-Size="Large"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Employee Code :
                        </td>
                        <td>
                            <asp:TextBox ID="lblEmpCode" runat="server" Text="" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Employee Name :
                        </td>
                        <td>
                            <asp:TextBox ID="lblEmpName" runat="server" Text="" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Department :
                        </td>
                        <td>
                            <asp:TextBox ID="lblDep" runat="server" Text="" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Designation :
                        </td>
                        <td>
                            <asp:TextBox ID="lblDesig" runat="server" Text="" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <br />
                            <asp:Button ID="btnSaveManualAtt" runat="server" CssClass="ButtonControl" OnClientClick="WriteOnCard();"
                                Text="Ok" OnClick="btnSaveManualAtt_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="ButtonControl" Text="Cancel"
                                OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSaveManualAtt" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddCall" runat="server" TargetControlID="Button2"
        PopupControlID="Panel1" BackgroundCssClass="modalBackground" Enabled="true" CancelControlID="btnCancel">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
