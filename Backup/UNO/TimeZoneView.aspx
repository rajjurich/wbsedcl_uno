<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="TimeZoneView.aspx.cs" Inherits="UNO.TimeZoneView1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/default.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script type="text/javascript" language="javascript">

        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvTimezone.ClientID %>');

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
    //Added by Pooja Yadav
        function ValidateAddGrid(GridValue) {
            if (GridValue == "A") {
                if (!CheckValidation('<%=txtTZID.ClientID %>', '<%=txtTZDesc.ClientID %>', '<%=lblError.ClientID %>', '<%=gvPeriods.ClientID %>'))
                    return false;
                else {
                  //  document.getElementById('<%=btnSubmitAdd.ClientID%>').disabled = true;
                    return true;
                }
            }
            if (GridValue == "E") {
                if (!CheckValidation('<%=txtTID.ClientID %>', '<%=txtTDesc.ClientID %>', '<%=lblMsg.ClientID %>', '<%=gvEditPeriod.ClientID %>'))
                    return false;
                else {
                    //document.getElementById('<%=btnSubmitEdit.ClientID%>').disabled = true;
                    return true;
                }

            }
            return false;
        }

        function CheckValidation(txtTZID, txtTZDesc, lblError, gvPeriods) {
            // debugger;
            var startTime;
            var EndTime;
            var intCount = 0;
            var lblErr = document.getElementById(lblError);
            document.getElementById(lblError).innerHTML = "";
            var GrdPeriods = document.getElementById(gvPeriods);
            if (document.getElementById(txtTZID).value == "") {
                lblErr.innerHTML = "Please enter timezone code.";
                return false;
            }
            if (document.getElementById(txtTZDesc).value == "") {
                lblErr.innerHTML = "Please enter timezone description.";
                return false;
            }


            for (var i = 1; i <= GrdPeriods.rows.length - 1; i++) {

                startTime = GrdPeriods.rows[i].cells[1].children[0].value;
                EndTime = GrdPeriods.rows[i].cells[2].children[0].value;
                var Sun = GrdPeriods.rows[i].cells[3].children[0].checked;
                var Mon = GrdPeriods.rows[i].cells[4].children[0].checked;
                var Tue = GrdPeriods.rows[i].cells[5].children[0].checked;
                var Wed = GrdPeriods.rows[i].cells[6].children[0].checked;
                var Thr = GrdPeriods.rows[i].cells[7].children[0].checked;
                var Fri = GrdPeriods.rows[i].cells[8].children[0].checked;
                var Sat = GrdPeriods.rows[i].cells[9].children[0].checked;
                var H1 = GrdPeriods.rows[i].cells[10].children[0].checked;
                var H2 = GrdPeriods.rows[i].cells[11].children[0].checked;


                var ftsp = startTime.split(':');
                var ttsp = EndTime.split(':');
                var ftseconds = (+ftsp[0]) * 60 * 60 + (+ftsp[1]) * 60;
                var ttseconds = (+ttsp[0]) * 60 * 60 + (+ttsp[1]) * 60;

                if (startTime != "") {
                    if (!(ValidateTime(startTime))) {
                        lblErr.innerHTML = "Please enter valid start time in row:" + i;
                        return false;
                    }
                }
                if (EndTime != "") {
                    if (!(ValidateTime(EndTime))) {
                        lblErr.innerHTML = "Please enter valid end time in row:" + i;
                        return false;
                    }
                }

                if (startTime != "" && EndTime == "") {
                    lblErr.innerHTML = "Please enter end time in row:" + i;
                    return false;
                }
                if (startTime == "" && EndTime != "") {
                    lblErr.innerHTML = "Please enter start time in row:" + i;
                    return false;
                }

                if (startTime != "" && EndTime != "") {
                    if (ftseconds >= ttseconds) {
                        lblErr.innerHTML = "End time should be greater than start time in row:" + i;
                        return false;
                    }

                }

                if (startTime != "" && EndTime != "") {
                    if (Sun == false && Mon == false && Tue == false && Wed == false && Thr == false && Fri == false && Sat == false && H1 == false && H2 == false) {
                        lblErr.innerHTML = "It is mandatory to check the day(s) along with the time in row:" + i;
                        return false;
                    }
                }

                if (startTime == "" && EndTime == "") {
                    if (Sun == true || Mon == true || Tue == true || Wed == true || Thr == true || Fri == true || Sat == true || H1 == true || H2 == true) {
                        lblErr.innerHTML = "Please enter start Time and end time in row:" + i;
                        return false;
                    }
                }

                if (startTime != "" || EndTime != "" || Sun == true || Mon == true || Tue == true || Wed == true || Thr == true || Fri == true || Sat == true || H1 == true || H2 == true) {

                    intCount = parseInt(intCount) + 1;
                }

                if (startTime != "" && EndTime != "") {

                    for (var j = 1; j <= i - 1; j++) {
                        var start = GrdPeriods.rows[j].cells[1].children[0].value;
                        var End = GrdPeriods.rows[j].cells[2].children[0].value;
                        if (start == "" || End == "") {
                            lblErr.innerHTML = "Please enter values in row:" + j;
                            return false;
                        }

                    }
                }


            }

            if (intCount == 0) {
                lblErr.innerHTML = "Please enter atleast one record";
                return false;
            }

            return true;
        }

        function ValidateTime(timeStr) {

            if ((timeStr.search(/^\d{1,2}:\d{2}([ap]m)?$/) != -1) &&
            (timeStr.substr(0, 2) >= 0 && timeStr.substr(0, 2) <= 24) &&
            (timeStr.substr(3, 2) >= 0 && timeStr.substr(3, 2) <= 59))
                return true;
            else
                return false;

        }

        function fnColon(ctrl, e) {
            var unicode = e.keyCode
            if (unicode != 8) {
                if (ctrl.getAttribute && ctrl.value.length == 2) {
                    ctrl.value = ctrl.value + ":";
                }
            }
        }


        function ResetAll() {

            $('#' + ["<%=txtTimezoneDesc.ClientID%>", "<%=txtTimezoneId.ClientID%>"].join(', #')).prop('value', "");
            document.getElementById('<%=lblMessages.ClientID%>').innerHTML = "";
            document.getElementById('<%=txtTimezoneDesc.ClientID%>').focus();
            document.getElementById('<%=txtTimezoneId.ClientID%>').focus();
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvTimezone.ClientID%>').focus();
            
           // return false;
        }

        function CloseEdit() {
            $find('mpeBEditTZ').hide();
            return false;
        }
    </script>
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
    </style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="TimeZone View" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <asp:Panel ID="Panel3" runat="server" DefaultButton="btnSearch">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%; text-align: left;">
                            <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAddNew_Click" />
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                                OnClick="btnDelete_Click" />
                            <asp:Button runat="server" ID="Button1" Text="Delete" CssClass="ButtonControl" Style="display: none"
                                OnClick="btnDelete_Click" />
                        </td>
                        <td style="width: 50%; text-align: right;">
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;" OnClientClick="return ResetAll();" />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;margin-right:3px;" CssClass="ButtonControl"
                                OnClick="btnSearch_Click" />
                            <asp:TextBox ID="txtTimezoneDesc" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtTZDesc" runat="server" TargetControlID="txtTimezoneDesc"
                                WatermarkText="Description" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:TextBox ID="txtTimezoneId" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtTZID" runat="server" TargetControlID="txtTimezoneId"
                                WatermarkText="Timezone ID" WatermarkCssClass="watermark">
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
                                    <asp:GridView ID="gvTimezone" runat="server" AutoGenerateColumns="false" Width="100%"
                                        GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="gvTimezone_RowCommand"
                                        OnRowDataBound="gvTimezone_RowDataBound">
                                        <RowStyle CssClass="gvRow" />
                                        <HeaderStyle CssClass="gvHeader" />
                                        <AlternatingRowStyle BackColor="#F0F0F0" />
                                        <PagerStyle CssClass="gvPager" />
                                        <EmptyDataTemplate>
                                            <div>
                                                <span>No Timezone found.</span>
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
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="true" CommandName="Modify"
                                                        Text="Edit" ForeColor="#3366FF" CommandArgument='<%#Eval("TZ_CODE")%>'></asp:LinkButton>
                                                      <asp:HiddenField ID="hdnRowID" runat="server" Value='<%#Eval("TZ_ID") %>' />   
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TZ_CODE" HeaderText="CODE" SortExpression="CODE" ItemStyle-Width="5%">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TZ_DESCRIPTION" HeaderText="Description" ItemStyle-Wrap="true"
                                                ItemStyle-Width="10%" SortExpression="Description"></asp:BoundField>
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
                                                        <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="ButtonControl"
                                                            OnClick="gvPrevious" />
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
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div>
                    <table style="width: 100%;">
                        <tr>
                            <td style="color: black; font-weight: 300;text-align:left;padding-left:30px;">
                                <a style="color: blue;"><b>
                                    <asp:LinkButton ID="lnkAcesslevel" runat="server" Text="Go To Access Level" Style="color: Blue;"
                                        OnClientClick="navigateToUrl('AccessLevelBrowse.aspx');return false;"></asp:LinkButton></b></a>
                            </td>
                           
                            <td style="color: black; font-weight: 300;text-align:right;padding-right:30px;">
                                <a style="color: blue;"><b>
                                    <asp:LinkButton ID="lnkController" runat="server" Text="Go To Controller View" OnClientClick="navigateToUrl('ControllerView.aspx');return false;"
                                        Style="color: Blue;"></asp:LinkButton></b></a>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="gvTimezone" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                <%--  <asp:AsyncPostBackTrigger ControlID="btnSubmitAdd" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnSubmitEdit" />
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Panel ID="pnlAddTZ" runat="server" CssClass="PopupPanel" Style="display: block">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table id="table4" runat="server" width="100%" height="90%" border="0" cellpadding="0"
                    cellspacing="0" class="TableClass">
                    <tr>
                        <td style="text-align: right;">
                            Timezone Code :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtTZID" runat="server" CssClass="TextControl" TabIndex="1" ClientIDMode="Static"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTEtxtTZID" runat="server" FilterType="Numbers"
                                TargetControlID="txtTZID" />
                        </td>
                        <td style="text-align: right;">
                            Timezone Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtTZDesc" runat="server" CssClass="TextControl" MaxLength="50"
                                TabIndex="2" ClientIDMode="Static" style="text-transform:capitalize"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTBEtxtTZDesc" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers
" TargetControlID="txtTZDesc" />
                        </td>
                    </tr>
                </table>
                <div id="divTimezone" class="DivEmpDetails" clientidmode="Static">
                    <table id="table5" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                        <tr>
                            <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvPeriods" runat="server" AutoGenerateColumns="False" Width="100%"
                                            GridLines="None" AllowPaging="false">
                                            <RowStyle CssClass="gvRow" />
                                            <HeaderStyle CssClass="gvHeader" ForeColor="#47A3DA" />
                                            <AlternatingRowStyle BackColor="#F0F0F0" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Period">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPeriod" runat="server" Width="50px" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Start Time" SortExpression="Start Time" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtStartTime" runat="server" CssClass="TextControl" MaxLength="5"
                                                            Width="90%" TabIndex="2" onkeyup="fnColon(this,event)" onkeypress="findspace(event)"></asp:TextBox>
                                                        <br />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FTEtxtStartTime" runat="server" FilterType="Numbers,Custom"
                                                            TargetControlID="txtStartTime" ValidChars=":" />
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End Time" SortExpression="End Time" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEndTime" runat="server" CssClass="TextControl" MaxLength="5"
                                                            Width="90%" TabIndex="2" onkeyup="fnColon(this,event)" onkeypress="findspace(event)"></asp:TextBox>
                                                        <br />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FTEtxtEndTime" runat="server" FilterType="Numbers,Custom"
                                                            TargetControlID="txtEndTime" ValidChars=":" />
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SUN">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSun" runat="server" />
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MON">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkMon" runat="server" />
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TUE">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkTue" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WED">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkWed" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="THR">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkThr" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FRI">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkFri" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SAT">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSat" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="H1">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkH1" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="H2">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkH2" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
                <table id="table3" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnSubmitAdd" runat="server" CssClass="ButtonControl" TabIndex="4"
                                Text="Save" OnClick="btnSubmitAdd_Click" OnClientClick="return ValidateAddGrid('A');"/>
                            <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" TabIndex="5"
                                Text="Cancel" OnClick="btnCancelAdd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Label ID="lblError" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCancelAdd" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="btntemp" runat="server" Style="display: none;"></asp:Button>
    <asp:Button ID="btnAddDummy" runat="server" Style="display: none;"></asp:Button>
    <ajaxToolkit:ModalPopupExtender ID="mpeAddTZ" runat="server" TargetControlID="btnAddDummy"
        PopupControlID="pnlAddTZ" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btntemp">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlEditTZ" runat="server" CssClass="PopupPanel" Style="display: block">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <table id="table6" runat="server" width="100%" height="90%" border="0" cellpadding="0"
                    cellspacing="0" class="TableClass">
                    <tr>
                        <td style="text-align: right;">
                            Timezone Code :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtTID" runat="server" CssClass="TextControl" TabIndex="1" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            Timezone Description :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtTDesc" runat="server" CssClass="TextControl" MaxLength="50" TabIndex="2" style="text-transform:capitalize"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div id="div1" class="DivEmpDetails" clientidmode="Static">
                    <table id="table1" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                        <tr>
                            <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvEditPeriod" runat="server" AutoGenerateColumns="False" Width="100%"
                                            GridLines="None" AllowPaging="false">
                                            <RowStyle CssClass="gvRow" />
                                            <HeaderStyle CssClass="gvHeader" ForeColor="#47A3DA" />
                                            <AlternatingRowStyle BackColor="#F0F0F0" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Period">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPeriod" runat="server" Width="50px" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Start Time" SortExpression="Start Time" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtStartTimeE" runat="server" CssClass="TextControl" MaxLength="5"
                                                            Width="90%" TabIndex="2" onkeyup="fnColon(this,event)" onkeypress="findspace(event)"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End Time" SortExpression="End Time" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEndTimeE" runat="server" CssClass="TextControl" MaxLength="5"
                                                            Width="90%" TabIndex="2" onkeyup="fnColon(this,event)" onkeypress="findspace(event)"></asp:TextBox>
                                                        <br />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FTEtxtEndTimeEdit" runat="server" FilterType="Numbers,Custom"
                                                            TargetControlID="txtEndTimeE" ValidChars=":" />
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SUN">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSun" runat="server" />
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MON">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkMon" runat="server" />
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TUE">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkTue" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WED">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkWed" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="THR">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkThr" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FRI">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkFri" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SAT">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSat" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="H1">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkH1" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="H2">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkH2" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
                <table id="table2" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableClass">
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnSubmitEdit" runat="server" CssClass="ButtonControl" TabIndex="4"
                                Text="Save" OnClick="btnSubmitEdit_Click" OnClientClick="return ValidateAddGrid('E');"/>
                            <asp:Button ID="btnCancelEdit" runat="server" CssClass="ButtonControl" TabIndex="5"
                                ClientIDMode="Static" Text="Cancel" OnClientClick="return CloseEdit();"/>
                            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Label ID="lblMsg" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:LinkButton ID="lnkDummyEdit" runat="server" Style="display: none;">edit</asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="mpeEditTZ" runat="server" TargetControlID="lnkDummyEdit"
        PopupControlID="pnlEditTZ" BackgroundCssClass="modalBackground" Enabled="true" BehaviorID="mpeBEditTZ"
        CancelControlID="btnCancelEdit">
    </ajaxToolkit:ModalPopupExtender>
    
  
</asp:Content>
