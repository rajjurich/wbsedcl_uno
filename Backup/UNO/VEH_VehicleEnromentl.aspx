<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    Culture="en-GB" CodeBehind="VEH_VehicleEnromentl.aspx.cs" Inherits="UNO.VehicleEnromentl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="Scripts/Choosen/chosen.css" />
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <style type="text/css">
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
    </style>
    <script type="text/javascript">
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
            start1();
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
    <style type="text/css">
        .cssVEh
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .style38
        {
            width: 133px;
        }
        .hideCol
        {
            display: none;
        }
        .style40
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TableRow runat="server">
        <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
            <asp:Label ID="lblHead" runat="server" Text="Vehicle Dashboard" ForeColor="RoyalBlue"
                Font-Size="20px" Width="100%" CssClass="heading">
            </asp:Label>
        </asp:TableCell>
    </asp:TableRow>
    <div class="DivEmpDetails">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: left; width: 5%;">
                    <asp:Button ID="btnNew" runat="server" Text="New Tag Issue" CssClass="ButtonControl" OnClick="btnNew_Click" />
                </td>
                <td style="width: 40%; color: #003300; text-align: left;">
                    <%--<asp:Button ID="btnDel" runat="server" Text="Delete" CssClass="ButtonControl" />--%>
                </td>
                <td style="text-align: right; width: 40%;">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                        OnClick="btnSearch_Click" />
                    <asp:TextBox ID="txtUserID" runat="server" Style="float: right;" CssClass="searchTextBox"
                        onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtUserID" runat="server" TargetControlID="txtUserID"
                        WatermarkText="Search by EmpID OR VisitorID " WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    <asp:TextBox ID="txtLevelID" runat="server" Style="float: right; margin-left: 55;"
                        onkeydown="return (event.keyCode!=13);" CssClass="searchTextBox"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtLevelID" runat="server" TargetControlID="txtLevelID"
                        WatermarkText="Search by vehicle " WatermarkCssClass="watermark">
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
                            <asp:GridView ID="gvVehicleEntries" runat="server" AutoGenerateColumns="false" Width="100%"
                                GridLines="None" AllowPaging="true" PageSize="15" OnRowCommand="gvVehicleEntries_RowCommand">
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
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkModify" runat="server" ForeColor="#3366FF" CommandName="Modify"
                                                CommandArgument='<%#Eval("ID")%>'>Edit</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="Vehicle Registration Number">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkVehRegNo" runat="server" ForeColor="#3366FF" Text='<%#Eval("VehicleRegistrationNumber")%>' CommandName="VehData"
                                                CommandArgument='<%#Eval("ID")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="vehicleName" HeaderText="Vehicle Description" />
                                    <asp:BoundField DataField="rfidIssueDate" HeaderText="RFID Issue Date" />
                                    <asp:BoundField DataField="rfidValidityDate" HeaderText="RFID Validity Date" />
                                    <asp:BoundField DataField="entityId" HeaderText="Custudian(Employee ID)" />
                                    <asp:BoundField DataField="rfId" HeaderText="RFID" />
                                    <asp:TemplateField HeaderText="Enabled/Disabled">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnlStatus" runat="server" ForeColor="#3366FF" CommandName="ChangeStatus"
                                                CommandArgument='<%#Eval("ID")%>' Text='<%#Eval("isEnabledStatus")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="History">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkHistory" Text="View History" CommandName="VehHistory" CommandArgument='<%#Eval("ID")%>'
                                                runat="server" ForeColor="#3366FF"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kill Tag">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" ForeColor="#3366FF" CommandName="Remove"
                                                CommandArgument='<%#Eval("ID")%>'>Kill</asp:LinkButton>
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
                                <SortedAscendingHeaderStyle ForeColor="White" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%-- <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
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
                    </ajaxToolkit:UpdatePanelAnimationExtender>--%>
                </td>
            </tr>
        </table>
    </div>
      <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
    <div class="DivEmpDetails">
            <div style="color: White; text-align: left">
                <b>RFID Tag Details : </b>
                Tag issued : <b><font color="white">
                    <asp:Label runat="server" ID="lblIsshued" Style="padding-right: 10%"> </asp:Label></font></b>
                Tag in Inventory : <b><font color="white">
                    <asp:Label runat="server" ID="lblInventory" Style="padding-right: 10%"> </asp:Label></font></b>
                Total Tags : <b><font color="white">
                    <asp:Label runat="server" ID="lblTotalTags" Style="padding-right: 10%"> </asp:Label></font></b>
                      Total Enabled : <b><font color="white">
                    <asp:Label runat="server" ID="lblEnabled" Style="padding-right: 10%"> </asp:Label></font></b>
                Total Disabled : <b><font color="white">
                    <asp:Label runat="server" ID="lblDisabled" Style="padding-right: 10%"> </asp:Label></font></b>
              
               <%-- Compensatory Off : <b><font color="white">
                    <asp:Label runat="server" ID="lblCo" Style="></asp:Label></font></b>--%>
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
    <asp:Panel ID="plnAddActivity" runat="server" Width="45%" Height="54%" Style="background-color: White;
        border: 5px solid #000000; border-radius: 25px; color: Black;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <%--    <div style="width: 66%; background-color: White; position: absolute; border-radius: 10px;
                    left: 7%; height: 265px;">--%>
                <%-- <table style="width: 42%; display: block; margin-left: 8%; margin-top: 5%; position: absolute;">
                        <tr>
                            <td align="center">
                             <asp:Label ID="Label3" runat="server" Text="Select Type"></asp:Label>
                                 
                            </td>
                            <td>
                                <div>
                                    
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                gvDisabled
                            </td>
                            <td>
                                gvDisabled
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label3" runat="server" Text="Employee/Visitor ID "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtempVistorId" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                gvDisabled
                            </td>
                            <td>
                                gvDisabled
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label4" runat="server" Text="RFID"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRFID" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                gvDisabled
                            </td>
                            <td>
                                gvDisabled
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label7" runat="server" Text="Vehicle Registration Number"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRegNumber" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                gvDisabled
                            </td>
                            <td>
                                gvDisabled
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                gvDisabled
                            </td>
                            <td style="text-align: right;">
                            </td>
                        </tr>
                    </table>
                    <table style="width: 47%; margin-left: 53%; margin-top: 2.5%; position: absolute;">
                        <tr>
                            <td align="center">
                                gvDisabled
                            </td>
                            <td>
                                gvDisabled
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label1" runat="server" Text="Vehicle Name"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtVehicleName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                gvDisabled
                            </td>
                            <td>
                                gvDisabled
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label2" runat="server" Text="Model Name"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtModelName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                gvDisabled
                            </td>
                            <td>
                                gvDisabled
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label5" runat="server" Text="Make"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMake" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                gvDisabled
                            </td>
                            <td>
                                gvDisabled
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label6" runat="server" Placeholder="DD-MM-YY" Text="RFID Validity Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRFID_ValidityDate" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="caltxtPlannedStartDateEdit" runat="server" TargetControlID="txtRFID_ValidityDate"
                                    PopupButtonID="txtRFID_ValidityDate" Format="dd-MM-yyyy">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvtxtPlannedStartDateEdit" runat="server" ErrorMessage="Please Select Planned Start Date"
                                    ControlToValidate="txtRFID_ValidityDate" Display="None" ValidationGroup="add"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtPlannedStartDateEdit" runat="server"
                                    TargetControlID="rfvtxtPlannedStartDateEdit" PopupPosition="Right">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                gvDisabled
                            </td>
                            <td>
                                gvDisabled
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                gvDisabled
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table style="margin-top: 24%; margin-left: 8%;">
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Purpose" Style="margin-left: 45%;"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtxPurpose" runat="server" TextMode="MultiLine" Height="43px" Style="margin-left: 9%;
                                    margin-top: 8%;" Width="243%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSave0" runat="server" CssClass="ButtonControl" OnClick="btnSave_Click"
                                    Text="Save" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancel" runat="server" CssClass="ButtonControl" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                --%>
                <table style="width: 95%; margin-top: 3%; margin-left: 3%;">
                    <tr >
                        <td class="style38">
                            <asp:Label ID="Label3" runat="server" Text="Select Type"></asp:Label><br>
                             <asp:Label ID="lblDept" runat="server" Text="Select Department"></asp:Label>
                        </td>
                        <td style="padding-left:6px">
                           
                            <asp:DropDownList ID="ddlType0"  runat="server" Height="20px" Width="173px" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                AutoPostBack="True">
                                <asp:ListItem Value="0">Employee</asp:ListItem>
                                <asp:ListItem Value="1">Department</asp:ListItem>
                            </asp:DropDownList>
                         
                           <asp:DropDownList ID="ddlSelectedDep" runat="server" class="chosen-select" AutoPostBack="true"
                                ValidationGroup="add" Width="173px" onselectedindexchanged="ddlSelectedDep_SelectedIndexChanged" 
                             >
                                <asp:ListItem>Select One</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td rowspan="10" style="text-align: center;">
                            <div style="border: 1px groove lightgray; max-width: 236px; max-height: 271px; height: 387px;width:236px; overflow: auto;border-radius:10px;">
                                <asp:GridView ID="gvControllerAdd" runat="server" AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" HeaderStyle-BackColor="#47a3da" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkControllerADD" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ControllerID" HeaderText="ID" HeaderStyle-BackColor="#47a3da" />
                                        <asp:BoundField DataField="id" ItemStyle-CssClass="hideCol" HeaderStyle-CssClass="hideCol" />
                                        <asp:BoundField DataField="controllerDescription" HeaderText="Description"  HeaderStyle-BackColor="#47a3da"  ItemStyle-HorizontalAlign="Left"/>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                  
                 <%--   <tr runat="server" id="trDep">
                  <td class="style38"><asp:Label ID="lblDept" runat="server" Text="Select Department"></asp:Label>
                                               </td>
             
                    
                        <td style="width:50px;">
                      
                            <asp:DropDownList ID="ddlDep" runat="server" 
                                onselectedindexchanged="DropDownList1_SelectedIndexChanged"
                                ValidationGroup="add"  Width="173px">
                                <asp:ListItem>Select One</asp:ListItem>
                            </asp:DropDownList>
                        </td>
             
                    
                    </tr>--%>
                    <tr>
                        <td class="style38">
                            <asp:Label ID="Label9" runat="server"></asp:Label>
                        </td>
                        <td>
                            <span style="color: Red; font-size: medium;">*</span><%-- <asp:CusomValidator ID="CustomValidator1" Display="None" runat="server" ValidationGroup="add" ControlToValidate="txtRFID" ClientValidationFunction="validateRFID" ErrorMessage="RFID not Register"></asp:CustomValidator>
                               <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtenderRFID" runat="server"
                                TargetControlID="CustomValidator1" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>--%><%--<asp:TextBox ID="txtempVistorId" 
                                ValidationGroup="add" MaxLength="20" runat="server"></asp:TextBox>--%><asp:DropDownList ID="txtempVistorId" runat="server" class="chosen-select" 
                                ValidationGroup="add" Width="173px">
                                <asp:ListItem>Select One</asp:ListItem>
                            </asp:DropDownList>
                            <%--   <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="GetdataByName"
                                MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="1"
                                FirstRowSelected="true" CompletionListCssClass="autocomplete" TargetControlID="txtempVistorId"
                                UseContextKey="True">
                            </ajaxToolkit:AutoCompleteExtender>--%>
                            <%--
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="GetdataByName"
                                MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="1"
                                FirstRowSelected="true" CompletionListCssClass="autocomplete" OnClientPopulating="suggestionListPopulating"
                                OnClientPopulated="suggestionListPopulated" TargetControlID="txtEmpName" UseContextKey="True"
                                OnClientItemSelected="ClientItemSelected1">
                            </asp:AutoCompleteExtender>--%>
                            <asp:RequiredFieldValidator ID="rfvtxtDescriptionAdd" runat="server" ErrorMessage="Please Enter Visitor/Employee IDb here"
                                ControlToValidate="txtempVistorId" Display="None" ValidationGroup="add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtDescriptionAdd" runat="server"
                                TargetControlID="rfvtxtDescriptionAdd" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="style38">
                            <asp:Label ID="Label11" runat="server" Text="RFID"></asp:Label>
                        </td>
                        <td>
                            <span style="color: Red; font-size: medium;">*</span><asp:TextBox ID="txtRFID" onblur="return validateRFID();"
                                runat="server" ClientIDMode="Static"></asp:TextBox>
                            <%-- <asp:CusomValidator ID="CustomValidator1" Display="None" runat="server" ValidationGroup="add" ControlToValidate="txtRFID" ClientValidationFunction="validateRFID" ErrorMessage="RFID not Register"></asp:CustomValidator>
                               <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtenderRFID" runat="server"
                                TargetControlID="CustomValidator1" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtRFID" Display="None" 
                                ErrorMessage="Please Enter RFID here" ValidationGroup="add"></asp:RequiredFieldValidator>
                            <asp:Button ID="rfidRead" runat="server" CssClass="ButtonControl" 
                                OnClientClick="validateRFID();" Style="display: none" Text="Read" />
                            <%--     <input id="Button3" type="button" value="read" onclick="start(this);" />--%>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" 
                                runat="server" PopupPosition="Right" TargetControlID="RequiredFieldValidator3">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="style38">
                            <asp:Label ID="Label7" runat="server" Text="Vehicle Registration Number"></asp:Label>
                        </td>
                        <td>
                            <span style="color: Red; font-size: medium;">*</span><asp:TextBox ID="txtRegNumber"
                                runat="server" MaxLength="20" ValidationGroup="add"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter vehicle Registration no.here"
                                ControlToValidate="txtRegNumber" Display="None" ValidationGroup="add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                TargetControlID="RequiredFieldValidator2" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="style38">
                            <asp:Label ID="Label10" runat="server" Text="Vehicle Name"></asp:Label>
                        </td>
                        <td>
                            <span style="color: Red; font-size: medium;">*</span><asp:TextBox ID="txtVehicleName"
                                runat="server" MaxLength="50" ValidationGroup="add"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtVehicleName"
                                Display="None" ErrorMessage="Please Enter vehicle Name here" ValidationGroup="add"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style38">
                            <asp:Label ID="Label1" runat="server" Text="Make"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtMake" runat="server"></asp:TextBox>
                        </td>
                        <tr>
                            <td class="style38">
                                <asp:Label ID="Label2" runat="server" Text="Model Name"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:TextBox ID="txtModelName" MaxLength="50" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                              <tr>
                            <td class="style38">
                                <asp:Label ID="Label6" runat="server" Placeholder="DD-MM-YY" Text="RFID Issue Date"></asp:Label>
                            </td>
                            <td>
                                <span style="color: Red; font-size: medium;">*</span><asp:TextBox ID="txtRFID_IssueDate"
                                    onKeyPress="javascript: return false;" runat="server" ValidationGroup="add"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                    Format="dd/MM/yyyy" PopupButtonID="txtRFID_IssueDate" TargetControlID="txtRFID_IssueDate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtRFID_IssueDate"
                                    Display="None" ErrorMessage="Please Select RFID Issue Date" ValidationGroup="add"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4"
                                    runat="server" PopupPosition="Right" TargetControlID="RequiredFieldValidator10">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="style38">
                                <asp:Label ID="Label21" runat="server" Placeholder="DD-MM-YY" Text="RFID Validity Date"></asp:Label>
                            </td>
                            <td>
                                <span style="color: Red; font-size: medium;">*</span><asp:TextBox ID="txtRFID_ValidityDate"
                                    onKeyPress="javascript: return false;" runat="server" ValidationGroup="add"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtRFID_ValidityDate_CalendarExtender" runat="server"
                                    Format="dd/MM/yyyy" PopupButtonID="txtRFID_ValidityDate" TargetControlID="txtRFID_ValidityDate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvtxtPlannedStartDateEdit" runat="server" ControlToValidate="txtRFID_ValidityDate"
                                    Display="None" ErrorMessage="Please Select RFID Validity Date" ValidationGroup="add"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="rfvtxtPlannedStartDateEdit_ValidatorCalloutExtender"
                                    runat="server" PopupPosition="Right" TargetControlID="rfvtxtPlannedStartDateEdit">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="style38">
                                <asp:Label ID="Label8" runat="server" Text="Purpose"></asp:Label>
                            </td>
                            <td>   &nbsp;
                                <asp:TextBox ID="txtxPurpose" runat="server" TextMode="MultiLine" Width="174px" Height="39px" style="resize:none"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="btnSave0" runat="server" CssClass="ButtonControl" OnClick="btnSave_Click"
                                    Text="Save" ValidationGroup="add" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="ButtonControl" OnClick="btnCancel_Click"
                                    Text="Cancel" />
                            </td>
                        </tr>
                </table>
                <%-- </div>--%>
            </ContentTemplate>
            <Triggers>
                <%--   <asp:AsyncPostBackTrigger ControlID="btnNew" />--%>
                 <asp:PostBackTrigger ControlID="txtempVistorId" />
                 <asp:AsyncPostBackTrigger ControlID="ddlSelectedDep" />
             <asp:AsyncPostBackTrigger ControlID="btnNew" />
                <asp:PostBackTrigger ControlID="btnSave0" />
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:AsyncPostBackTrigger ControlID="ddlType0" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="btntempadd" runat="server" Text="Dummy" Style="display: none;" />
        <asp:Button ID="Button7" runat="server" Text="Dummy" Style="display: none;" />
    <%--    <asp:Button ID="Buttonnew" runat="server" Text="Dummy" Style="display: none;" />--%>
    <!----Edit Code By vaibhav!-->
    <asp:Panel ID="pnlEditVehicle" runat="server" Width="52%" Height="50%" Style="background-color: White;
        border: 5px solid #000000; border-radius: 25px; color: Black;">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <%--  <div style="width: 66%; background-color: White; position: absolute; border-radius: 10px;
                    left: 7%; height: 265px;">--%>
                <table style="width: 95%; margin-top: 3%; margin-left: 3%;">
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Select Type"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:DropDownList ID="ddlEditType" runat="server" Height="20px" Width="149px" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                <asp:ListItem Value="0">Employee</asp:ListItem>
                                <asp:ListItem Value="1">Department</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td rowspan="10" style="text-align: center;">
                            <%-- <div style="max-width: 120px; max-height: 200px; overflow: auto">--%>
                            <div style="border: 1px groove lightgray; max-width: 236px; max-height: 255px; height: 369px;width:236px; overflow: auto;border-radius:10px;">
                                <asp:GridView ID="gvControllerEdit" runat="server" AutoGenerateColumns="false" DataKeyNames="ControllerID" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" HeaderStyle-BackColor="#47a3da">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkControllerEdit" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ControllerID" HeaderText="ID" HeaderStyle-BackColor="#47a3da" />
                                        <asp:BoundField DataField="id" ItemStyle-CssClass="hideCol" HeaderStyle-CssClass="hideCol" />
                                       
                                        <asp:BoundField DataField="controllerDescription"  HeaderText="Description" HeaderStyle-BackColor="#47a3da" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="Employee/Visitor ID "></asp:Label>
                        </td>
                        <td>
                            <span style="color: Red; font-size: medium;">*</span><asp:TextBox ID="txtEditempVistorId" class="chosen-select"
                                MaxLength="20" ValidationGroup="edit" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Visitor/Employee ID here"
                                ControlToValidate="txtEditempVistorId" Display="None" ValidationGroup="edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                TargetControlID="RequiredFieldValidator5" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="RFID"></asp:Label>
                        </td>
                        <td>
                            <span style="color: Red; font-size: medium;">*</span><asp:TextBox ID="txtEditRFID"
                                Enabled="false" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter RFID here"
                                ControlToValidate="txtEditRFID" Display="None" ValidationGroup="edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                TargetControlID="RequiredFieldValidator6" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label16" runat="server" Text="Vehicle Registration Number"></asp:Label>
                        </td>
                        <td>
                            <span style="color: Red; font-size: medium;">*</span><asp:TextBox ID="txtEditRegNumber"
                                runat="server" MaxLength="20" ValidationGroup="edit"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Enter vehicle Registration no.here"
                                ControlToValidate="txtEditRegNumber" Display="None" ValidationGroup="edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                TargetControlID="RequiredFieldValidator7" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label20" runat="server" Text="Vehicle Name"></asp:Label>
                        </td>
                        <td>
                            <span style="color: Red; font-size: medium;">*</span><asp:TextBox ID="txtEditVehicleName"
                                runat="server" ValidationGroup="edit"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtEditVehicleName"
                                Display="None" ErrorMessage="Please Enter vehicle Name here" ValidationGroup="edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator9_ValidatorCalloutExtender"
                                runat="server" PopupPosition="Right" TargetControlID="RequiredFieldValidator9">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="Make"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtEditMake" MaxLength="50" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                        <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Placeholder="DD-MM-YY" Text="RFID Issue Date"></asp:Label>
                        </td>
                        <td>
                            <span style="color: Red; font-size: medium;">*</span><asp:TextBox ID="txtEditRFID_IssueDate"
                                onKeyPress="javascript: return false;" ValidationGroup="edit" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEditRFID_IssueDate"
                                PopupButtonID="txtEditRFID_IssueDate" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Select RFID Issue Date"
                                ControlToValidate="txtEditRFID_IssueDate" Display="None" ValidationGroup="edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                TargetControlID="RequiredFieldValidator4" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label17" runat="server" Placeholder="DD-MM-YY" Text="RFID Validity Date"></asp:Label>
                        </td>
                        <td>
                            <span style="color: Red; font-size: medium;">*</span><asp:TextBox ID="txtEditRFID_ValidityDate"
                                onKeyPress="javascript: return false;" ValidationGroup="edit" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEditRFID_ValidityDate"
                                PopupButtonID="txtEditRFID_ValidityDate" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Select RFID Validity Date"
                                ControlToValidate="txtEditRFID_ValidityDate" Display="None" ValidationGroup="edit"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                TargetControlID="RequiredFieldValidator8" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label15" runat="server" Text="Model Name"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtEditModelName" MaxLength="50" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label18" runat="server" Text="Purpose"></asp:Label>
                        </td>
                        <td>
                           &nbsp;
                            <asp:TextBox ID="txtEditPurpose" runat="server" MaxLength="400" 
                                TextMode="MultiLine" Width="174px" Height="39px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="ButtonControl" OnClick="btnUpdate_Click"
                                Text="Save" />
                            <asp:Button ID="btnUpdateCancel" runat="server" CssClass="ButtonControl" Text="Cancel"
                                OnClick="btnUpdateCancel_Click" />
                        </td>
                    </tr>
                    <%-- <tr>
                           
                            <td colspan="4">
                                <asp:GridView ID="gvController" runat="server" Width="572px">
                              
                                </asp:GridView> </td>
                        </tr>--%>
                </table>
                <%--   </div>--%>
            </ContentTemplate>
            <Triggers>
      
                <asp:AsyncPostBackTrigger ControlID="btnUpdateCancel" />
                      
                <asp:AsyncPostBackTrigger ControlID="btnUpdateCancel" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <!----Edit Code By vaibhav!-->
    <asp:Button ID="btntest" runat="server" Text="Dummy" Style="display: none;" />
    <asp:Button ID="btnD" runat="server" Text="Dummy" Style="display: none;" />
    <asp:Button ID="btndummyEdit" runat="server" Text="Dummy" Style="display: none;" />
    <asp:Button ID="btndummyEdit2" runat="server" Text="Dummy" Style="display: none;" />
    <asp:Panel ID="pnl_del_project" runat="server" Style="background-color: White; border: 5px solid #000000;
        border-radius: 25px; color: Black;" Width="25%" Height="15%">
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <table width="100%" style="margin-top: 10%;">
                    <tr>
                        <td style="text-align: center;">
                            <asp:Label ID="Label19" runat="server" Text="Do you want Permanently kill the tag"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btn_del_submit" runat="server" Text="Yes" CssClass="ButtonControl"
                                OnClick="btn_del_submit_Click" />
                            <asp:Button ID="btn_cl" runat="server" Text="No" OnClick="btn_cl_Click" CssClass="ButtonControl" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="btn_sub" runat="server" Style="display: none;" Text="test" />
    <asp:Button ID="Button1" runat="server" Text="Yes" Style="display: none;" />
    <asp:Button ID="Button2" runat="server" Text="No" Style="display: none;" />



    <ajaxToolkit:ModalPopupExtender ID="mpeDelVehicle" runat="server" Enabled="True"
        BackgroundCssClass="cssVEh" TargetControlID="btn_sub" PopupControlID="pnl_del_project"
        OkControlID="btn_cl">
    </ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="mpeAddVehicle" runat="server" BackgroundCssClass="cssVEh"
        Enabled="true" PopupControlID="plnAddActivity" TargetControlID="Button7" OkControlID="btntempadd">
    </ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="mpeEditVehicle" runat="server" BackgroundCssClass="cssVEh"
        Enabled="true" PopupControlID="pnlEditVehicle" TargetControlID="btndummyEdit"
        OkControlID="btndummyEdit2">
    </ajaxToolkit:ModalPopupExtender>
    

        <asp:Panel ID="Panel2" runat="server" Style="background-color: White; border: 5px solid #000000;
        border-radius: 25px; color: Black; height:auto" Width="56%">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
           <table width="100%">
           <tr>
               
           <td align="center" class="style40" style="width:50%"><asp:Label ID="Label22"  Text="Vehicle Registration No." style="font-weight:bold" runat="server"></asp:Label> :<asp:Label ID="lblVehId" style="font-weight:bold" runat="server"></asp:Label></td>
           <td align="center" colspan="2" style="width:50%">
               <asp:Label ID="Label25" runat="server" style="font-weight:bold" 
                   Text="Vehicle Description"></asp:Label>
               <asp:Label ID="lblVehicleDescription" runat="server" style="font-weight:bold"></asp:Label>
               </td>
           </tr>
             <tr>
           <td align="center" class="style40" colspan="2">
               &nbsp;&nbsp;&nbsp;
               <asp:Label ID="Label26" runat="server" style="font-weight:bold" 
                   Text="Custudian Name"></asp:Label>
               <asp:Label ID="VehHolder" runat="server" style="font-weight:bold"></asp:Label>
                 </td>
                 <td style="width:2%">
                     <asp:ImageButton ID="imgExpotBtn" runat="server" 
                         ImageUrl="~/images/Excelicon.png" onclick="imgExpotBtn_Click" />
                 </td>
           </tr>
           </table>
                <table width="100%" style="margin-top: 0%;">
                    <tr>
                        <td style="text-align: center;">
                        <div style="">
                              <asp:GridView ID="gvVehHistory" runat="server" AutoGenerateColumns="true" Width="100%"
                                                AllowSorting="True" AllowPaging="true" ClientIDMode="Static" 
                                  PageSize="5" GridLines="None" onpageindexchanging="gvVehHistory_PageIndexChanging"
                                            >
                                                <RowStyle CssClass="gvRow" />
                                                <HeaderStyle CssClass="headerstyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                                <PagerStyle ForeColor="Black"/>
                                                <EmptyDataRowStyle BackColor="#edf5ff" Height="100px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <div>
                                                        <span>No Records found.</span>
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                            <%--<asp:BoundField DataField="" HeaderText="" />
                                            <asp:BoundField DataField="" HeaderText="" />
                                            <asp:BoundField DataField="" HeaderText="" />
                                            <asp:BoundField DataField="" HeaderText="" />--%>
                                                </Columns>
                                            </asp:GridView>
                                            </div>
                        </td>
                    </tr>
              <tr>
           <td align="center">
           <asp:Button ID="btnokHistory" runat="server" Text="Ok" CssClass="ButtonControl" 
                   onclick="btnokHistory_Click" />
           </td>
           </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvVehHistory" />
            <asp:PostBackTrigger ControlID="imgExpotBtn" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button3" runat="server" Text="No" Style="display: none;" />
     <asp:Button ID="Button5" runat="server" Text="No" Style="display: none;" />
      <ajaxToolkit:ModalPopupExtender ID="mpeHistory" runat="server" Enabled="True"
        BackgroundCssClass="cssVEh" TargetControlID="Button3" PopupControlID="Panel2" CancelControlID="Button5"
        >
    </ajaxToolkit:ModalPopupExtender>


    
        <asp:Panel ID="pnlVehData" runat="server" Style="background-color: White; border: 5px solid #000000;
        border-radius: 25px; color: Black;height:auto" Width="56%">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
           <table width="100%">
           <tr>
               
           <td align="center" class="style40"><asp:Label ID="Label23"  Text="Vehicle Registration No." style="font-weight:bold" runat="server"></asp:Label> :<asp:Label ID="Label24" style="font-weight:bold" runat="server"></asp:Label></td>
           <td align="center">
               <asp:Label ID="Label27" runat="server" style="font-weight:bold" 
                   Text="Vehicle Description"></asp:Label>
               <asp:Label ID="Label28" runat="server" style="font-weight:bold"></asp:Label>
               </td>
           </tr>
             <tr>
           <td align="center" class="style40" colspan="2">
               <asp:Label ID="Label29" runat="server" style="font-weight:bold" 
                   Text="Custudian Name"></asp:Label>
               <asp:Label ID="Label30" runat="server" style="font-weight:bold"></asp:Label>
                 </td>
           </tr>
               <tr>
                   <td align="center" class="style40" colspan="2">
                       &nbsp;</td>
               </tr>
           </table>
                <table width="100%" style="margin-top: 0%;">
                    <tr>
                        <td style="text-align: center;">
                              <asp:GridView ID="gvVehicledata" runat="server" AutoGenerateColumns="true" Width="100%" 
                                                AllowSorting="True" AllowPaging="true" ClientIDMode="Static" 
                                  PageSize="5" GridLines="None" onpageindexchanging="gvVehicledata_PageIndexChanging"
                                            >
                                                <RowStyle CssClass="gvRow" />
                                                <HeaderStyle CssClass="headerstyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                                <PagerStyle CssClass="gvPager" />
                                                <EmptyDataRowStyle BackColor="#edf5ff" Height="100px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <div>
                                                        <span>No Records found.</span>
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                          <%--  <asp:BoundField DataField="" HeaderText="" />
                                            <asp:BoundField DataField="" HeaderText="" />
                                            <asp:BoundField DataField="" HeaderText="" />
                                            <asp:BoundField DataField="" HeaderText="" />--%>
                                                </Columns>
                                            </asp:GridView>
                        </td>
                    </tr>
           <tr>
           <td align="center">
           <asp:Button ID="btnokVehData" runat="server" Text="Ok" CssClass="ButtonControl" 
                   onclick="btnokVehData_Click" />
           </td>
           </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button4" runat="server" Text="No" Style="display: none;" />
        <asp:Button ID="Button6" runat="server" Text="No" Style="display: none;" />
      <ajaxToolkit:ModalPopupExtender ID="mpeVehData" runat="server" Enabled="True"
        BackgroundCssClass="cssVEh" TargetControlID="Button4" PopupControlID="pnlVehData" CancelControlID="Button6"
        >
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
            //            chosen:showing_dropdown
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
    </script>
</asp:Content>
