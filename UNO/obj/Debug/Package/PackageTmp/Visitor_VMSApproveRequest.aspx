 <%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="Visitor_VMSApproveRequest.aspx.cs" Inherits="UNO.Visitor_VMSApproveRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .hide
        {
            display: none;
        }
    </style>
    <style type="text/css">
        .cssVEh
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .hideCol
        {
            display: none;
        }
        .style42
        {
            height: 10px;
            width: 201px;
            margin: 0px;
            padding: 0px;
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
            width: 22%;
            height: 40px;
        }
        .Display
        {
            display: none;
        }
        .style46
        {
            font-family: Verdana;
            font-size: 9pt;
            color: #515456;
            border: 0px solid #CCCCCC;
            text-align: right;
            padding-right: 4px;
            width: 112px;
        }
        .TDClassControl1
        {
            font-size: 9pt;
            font-family: verdana;
            color: #515456;
            border: 0px solid #CCCCCC;
            border-width: 0px;
            text-align: left;
            padding-left: 1px;
            width: 24%;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function chkForBlacklist() {
            var IsBlacklisted = document.getElementById('<%=hdnIsblacklisted.ClientID%>').value;
            if (IsBlacklisted == "True") {
                var msg = confirm("Are you sure you want to approve blacklisted visitor?");
                if (msg == false)
                    return false;
                else
                    return true;
            }
            else
                return true;
        }

        function ResetAll() {
            $('#' + ["<%=txtVisitorName.ClientID%>"].join(', #')).prop('value', "");
            $("select" + "#" + "<%=ddlStatus.ClientID%>").prop('selectedIndex', 0);
            document.getElementById('<%=txtVisitorName.ClientID%>').focus();
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvVisitorApproval.ClientID%>').focus();
            return false;
        }

        function approveClick() {
            var msg = confirm("Are you sure you want to Approve request ? ");
            return msg;
        }
        function Reject() {

            var msg = confirm("Are you sure you want to Reject request ? ");
            return msg;
        }
        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Visitor Request Approval" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div class="DivEmpDetails">
        <table style="width: 100%;">
            <tr>
                <%--  <td style="width: 20%; text-align: left;">
                        
                     <asp:Button ID="Button1" runat="server" Text="New" Style="float: right;" CssClass="ButtonControl"
                         />
                           <asp:Button ID="Button2" runat="server" Text="New" Style="float: right;" CssClass="ButtonControl"
                         />
                    </td>
                --%>
                <td style="width: 40%; text-align: left;">
                    Status :
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ComboControl" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                        <asp:ListItem Value="ALL">All</asp:ListItem>
                        <asp:ListItem Value="N">Pending</asp:ListItem>
                        <asp:ListItem Value="A">Approved</asp:ListItem>
                        <asp:ListItem Value="R">Rejected</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 40%; text-align: right;">
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                        OnClick="btnReset_Click" />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;"
                        CssClass="ButtonControl" OnClick="btnSearch_Click" />
                    <%--<asp:TextBox ID="txtTodate" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCallStatus" runat="server" TargetControlID="txtTodate"
                            WatermarkText="Search by To Date" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTodate"
                            PopupButtonID="txtTodate" Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtTodate"
                            Display="None" ValidationGroup="Search" ErrorMessage="Please enter date in dd/mm/yyyy format"
                            ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                            TargetControlID="RegularExpressionValidator3" PopupPosition="Right">
                        </ajaxToolkit:ValidatorCalloutExtender>

                        <asp:TextBox ID="txtFromDate" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWEtxtCallDate" runat="server" TargetControlID="txtFromDate"
                            WatermarkText="Search by From Date" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <ajaxToolkit:CalendarExtender ID="caltxtCallDate" runat="server" TargetControlID="txtFromDate"
                            PopupButtonID="txtFromDate" Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtFromDate"
                            Display="None" ValidationGroup="Search" ErrorMessage="Please enter date in dd/mm/yyyy format"
                            ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                            TargetControlID="RegularExpressionValidator4" PopupPosition="Right">
                        </ajaxToolkit:ValidatorCalloutExtender>

                        <asp:TextBox ID="txtVisitorId" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                            TargetControlID="txtVisitorId" WatermarkText="Search by Visitor ID" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>--%>
                    <asp:TextBox ID="txtVisitorName" runat="server" Style="float: right;" CssClass=""></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                        TargetControlID="txtVisitorName" WatermarkText="Visitor Name" WatermarkCssClass="">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="2">
                    <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                        <img src="images/276.gif" alt="Loading ...." />
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvVisitorApproval" runat="server" AutoGenerateColumns="False" Width="100%"
                                AllowPaging="True" ClientIDMode="Static" PageSize="10" GridLines="None" OnRowCommand="gvVisitorApproval_RowCommand"
                                OnPageIndexChanging="gvVisitorApproval_PageIndexChanging" OnRowDataBound="gvVisitorApproval_RowDataBound">
                                <RowStyle CssClass="gvRow" ForeColor="Black" />
                                <HeaderStyle CssClass="gvHeader" />
                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                <PagerStyle CssClass="gvPager" />
                                <EmptyDataTemplate>
                                    <div>
                                        <span>No Records.</span>
                                    </div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Row ID" SortExpression="ID" HeaderStyle-CssClass="hide"
                                        ItemStyle-CssClass="hide">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblID" runat="server" Text='<%#Eval("ID") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approve/Reject">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblVisitorID" CommandName="Modify" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                Text="Select" ForeColor="#47A3DA"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Visitor Name" >
                                            <ItemTemplate>
                                                 <asp:Label ID="lblVisitorName" runat="server" Text='<%#Eval("VisitorName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Company"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblcompany" runat="server" Text='<%#Eval("VisitorCompany") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="To Meet" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblMeet" runat="server" Text='<%#Eval("To_Meet") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Purpose" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblPurpose" runat="server" Text='<%#Eval("nature_of_work") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    <asp:BoundField DataField="VisitorName" HeaderText="Visitor Name" />
                                    <asp:BoundField DataField="VisitorCompany" HeaderText="Visitor Company" />
                                    <asp:BoundField DataField="To_Meet" HeaderText="To Meet" />
                                    <asp:BoundField DataField="nature_of_work" HeaderText="Nature Of Work" />
                                    <asp:BoundField DataField="status" HeaderText=" Request Status" />
                                    <asp:BoundField DataField="RequestDate" HeaderText=" Request Date" />
                                    <asp:TemplateField HeaderText="Documnet">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbl_Document" CommandName="View_Document" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                Text="View Document" ForeColor="#47A3DA"></asp:LinkButton>
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
                            <div style="visibility: visible; background: #2376A8; height: 10px;">
                            </div>
                            <div style="visibility: visible; background: #2376A8;">
                                <div style="text-align: center;">
                                    Requests Pending For Approval : <b><font color="white">
                                        <asp:Label runat="server" ID="lblPending" Style="padding-right: 8%"></asp:Label></font></b>
                                    Total Approved Request : <b><font color="white">
                                        <asp:Label runat="server" ID="lblApprove" Style="padding-right: 8%"> </asp:Label></font></b>
                                    Total Rejected Request : <b><font color="white">
                                        <asp:Label runat="server" ID="lblrejected" Style="padding-right: 8%"> </asp:Label></font></b>
                                    Total Request : <b><font color="white">
                                        <asp:Label runat="server" ID="lbltotalreq" Style="padding-right: 8%"> </asp:Label></font></b>
                                </div>
                            </div>
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
    </div>
    <asp:UpdatePanel ID="UpdatePanelmsg" runat="server">
        <ContentTemplate>
            <div style="width: 100%; height: 2%;" align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblMsg" runat="server" Style="color: Red;" class="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlAddEmployee" runat="server" CssClass="PopupPanel" Width="85%">
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <table style="width: 100%; padding: 1px;">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="Label4" runat="server" Text="Visitor Request Approval" ForeColor="RoyalBlue"
                                Font-Size="20px" Width="100%" CssClass="heading">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 98%; border-radius: 10px;">
                                <legend style="color: Green; padding-left: 5px; padding-right: 5px; font-weight: bold;">
                                    Visitor Details :</legend>
                                <table id="table2" runat="server" height="90%" border="0" cellpadding="0" width="100%"
                                    cellspacing="0" style="margin-left: 30px;">
                                    <tr id="Tr121" runat="server">
                                        <td id="as" class="style46" runat="server" style="text-align: left; font-weight: bold;">
                                            Visitor Name :&nbsp;
                                        </td>
                                        <td id="Td1" style="height: 10px; width: 200px;" class="TDClassControl1" runat="server">
                                            <asp:Label ID="lblVisitorName" runat="server" Text=""></asp:Label>
                                            <br />
                                        </td>
                                        <td id="Td2" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                            font-weight: bold;">
                                            Company Name :&nbsp;
                                        </td>
                                        <td id="Td3" runat="server" class="TDClassControl1">
                                            <asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr id="Tr5" runat="server">
                                        <td id="Td124" class="TDClassLabel1vaibhav" runat="server" style="padding-right: 50px;
                                            text-align: left; font-weight: bold;">
                                            Mobile No :<label class="CompulsaryLabel"></label>
                                        </td>
                                        <td id="Td32" runat="server" class="TDClassControl1">
                                            <asp:Label ID="lblMobileNo" runat="server" Text=""></asp:Label>
                                            <br />
                                        </td>
                                        <td id="Td17" class="TDClassLabel1vaibhav" style="height: 10px; text-align: left;
                                            font-weight: bold;" runat="server">
                                            Nature Of Work : &nbsp;
                                        </td>
                                        <td id="Td18" runat="server" class="TDClassControl1">
                                            <asp:Label ID="lblNaturOfWork" runat="server" Text=""></asp:Label>
                                            &nbsp;<br />
                                        </td>
                                    </tr>
                                    <tr id="Tr13" runat="server">
                                        <td id="Td41" class="style46" runat="server" style="text-align: left; font-weight: bold;">
                                            Designation :
                                        </td>
                                        <td id="Td42" style="height: 40px; width: 200px;" runat="server" class="TDClassControl1">
                                            <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td rowspan="2">
                            <fieldset style="width: 77%; border-radius: 10px;">
                                <legend style="color: Green; padding-left: 5px; padding-right: 5px; font-weight: bold;">
                                    Visitor Access Details :</legend>
                                <div style="border: 1px groove lightgray; max-width: 236px; max-height: 365px; height: 387px;
                                    width: 202px; overflow: auto; border-radius: 10px;">
                                    <asp:GridView ID="gvControllerEdit" runat="server" AutoGenerateColumns="false" Width="100%"
                                        DataKeyNames="Al_id" OnSelectedIndexChanged="gvControllerEdit_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" HeaderStyle-BackColor="lightgray" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkControllerEdit" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AL_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-BackColor="lightgray" />
                                            <%--                  <asp:BoundField DataField="id" ItemStyle-CssClass="hideCol" HeaderStyle-CssClass="hideCol" />--%>
                                            <asp:BoundField DataField="AL_DESCRIPTION" HeaderText="Description" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-BackColor="lightgray" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 98%; border-radius: 10px;">
                                <legend style="color: Green; padding-left: 5px; padding-right: 5px; font-weight: bold;">
                                    Visit Details :</legend>
                                <table id="table1" runat="server" height="90%" border="0" cellpadding="0" width="100%"
                                    cellspacing="0" style="margin-left: 5px;">
                                    <tr id="Tr8" runat="server">
                                        <td id="Td8" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                            width: 40px; font-weight: bold;">
                                            Appointment From Date:
                                        </td>
                                        <td id="Td9" style="height: 40px; width: 200px;" runat="server" class="TDClassControl1">
                                            <asp:Label ID="lblfromDate" runat="server" Text=""></asp:Label>
                                            <br />
                                        </td>
                                        <td id="Td25" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                            font-weight: bold;">
                                            Appointment To Date : &nbsp;
                                        </td>
                                        <td id="Td26" runat="server" class="TDClassControl1">
                                            <asp:Label ID="lblToDate" runat="server" Text=""></asp:Label>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr id="Tr1" runat="server">
                                        <td id="Td4" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                            font-weight: bold;">
                                            Appointment From Time :&nbsp;
                                        </td>
                                        <td id="Td5" style="height: 40px; width: 200px;" class="TDClassControl1" runat="server">
                                            <asp:Label ID="lblFromTime" runat="server" Text=""></asp:Label>
                                            <br />
                                        </td>
                                        <td id="Td6" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                            font-weight: bold;">
                                            Appointment To Time :
                                        </td>
                                        <td id="Td7" runat="server" class="TDClassControl1">
                                            <asp:Label ID="lblToTime" runat="server" Text=""></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr id="Tr_Nationality" runat="server">
                                        <td id="Td_Nationality" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                            font-weight: bold;">
                                            Nationality :
                                        </td>
                                        <td id="Td_lbl_Nationality" style="height: 40px; width: 200px;" runat="server" class="TDClassControl1">
                                            <asp:Label ID="lbl_Nationality" runat="server" Text=""></asp:Label>
                                            <br />
                                        </td>
                                        <td id="Td13" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                            font-weight: bold;">
                                            Purpose of visit :
                                        </td>
                                        <td id="Td16" style="height: 40px; width: 200px;" runat="server" class="TDClassControl1">
                                            <asp:Label ID="lblPurposeOfVisit" runat="server" Text=""></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <%-- <tr id="Tr4" runat="server">
                                        <td id="Td13" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                            width: 40px; font-weight: bold;">
                                            Purpose of visit :
                                        </td>
                                        <td id="Td16" colspan="3" style="height: 40px; width: 200px;" runat="server" class="TDClassControl1">
                                            <asp:Label ID="lblPurposeOfVisit" runat="server" Text=""></asp:Label>
                                            <br />
                                        </td>
                                    </tr>--%>
                                    <tr id="Tr2" runat="server">
                                        <td id="Td11" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                            width: 40px; font-weight: bold;">
                                            Additional Information :
                                        </td>
                                        <td id="Td12" colspan="3" style="height: 40px; width: 200px;" runat="server" class="TDClassControl1">
                                            <asp:Label ID="lblRemarks" runat="server" Text=""></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr id="Tr3" runat="server">
                                        <td id="Td14" class="TDClassLabel1vaibhav" runat="server" style="text-align: left;
                                            width: 40px; font-weight: bold;">
                                            Remarks if any :
                                        </td>
                                        <td id="Td15" style="height: 5px;" runat="server" class="TDClassControl1" colspan="3">
                                            <br />
                                            <asp:TextBox ID="txtAdditionalInfoAdd" Height="35px" Width="534px" MaxLength="50"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;
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
                            <asp:Button ID="btnSave" runat="server" Text="Approve" CssClass="ButtonControl" ValidationGroup="add"
                                OnClick="btnSave_Click" OnClientClick="return approveClick();" />
                            <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="ButtonControl"
                                ValidationGroup="add" OnClientClick="return Reject();" OnClick="btnReject_Click" />
                            <asp:Button ID="Btnclear1" runat="server" Text="Cancel" CssClass="ButtonControl"
                                OnClick="Btnclear1_Click" />
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
                <asp:HiddenField ID="hdnIsblacklisted" runat="server" ClientIDMode="Static" />
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button8" runat="server" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddEmployee" runat="server" TargetControlID="Button8"
        BehaviorID="ModalBehaviour" PopupControlID="pnlAddEmployee" BackgroundCssClass="modalBackground"
        Enabled="true">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="Pannel_Document" runat="server" CssClass="PopupPanel" Width="45%">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lbl_documnet" runat="server" Text="Uploaded Document" ForeColor="RoyalBlue"
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
                                    <tr>
                                        <td style="background-color: #EFF8FE; padding: 0px;" colspan="3">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
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
                                                            <asp:TemplateField HeaderText="View" ItemStyle-CssClass="center">
                                                                <ItemTemplate >
                                                                    <asp:LinkButton ID="lnk_View" runat="server" ForeColor="#3366FF" CommandName="View" CommandArgument='<%#Eval("URL")%>'>View</asp:LinkButton>
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
                        <%--<tr>
                            <td id="Td19" align="center" runat="server">
                                <asp:Label ID="lbl_document" runat="server" Style="color: Red;"></asp:Label>
                            </td>
                        </tr>--%>
                    </table>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="UploadButton1" EventName="Click" />--%>
                <%--<asp:PostBackTrigger ControlID="UploadButton1" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Btn_pnldoc" runat="server" Text="No" Style="display: none;" />
    <asp:Button ID="Btn_pnldoc2" runat="server" Text="No" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="ModelPopupFor_Document" runat="server" Enabled="true"
        BackgroundCssClass="cssVEh" TargetControlID="Btn_pnldoc" PopupControlID="Pannel_Document"
        OkControlID="Btn_pnldoc2">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="Panel_View_Document" runat="server" CssClass="PopupPanel" Width="60%">
        <asp:UpdatePanel ID="UpdatePanel_Document" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lbl_View_Document" runat="server" Text="View Document" ForeColor="RoyalBlue"
                                Font-Size="20px" Width="100%" CssClass="heading">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <fieldset style="width: 100%; border-radius: 10px;">
                                <table style="width: 100%; padding: 5px;">
                                    <tr>
                                        <td style="background-color: #EFF8FE; padding: 0px;">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:Image ID="document_Image" Width="750px" Height="500px" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <table id="table3" runat="server" width="100%" border="0" cellpadding="3" cellspacing="3">
                        <tr id="Tr4" runat="server">
                            <td id="Td19" align="center" runat="server">
                                <asp:Button ID="btn_cancel_view_doc" runat="server" Text="Cancel" 
                                    CssClass="ButtonControl" onclick="btn_cancel_view_doc_Click"
                                   />
                            </td>
                        </tr>
                        <%--<tr>
                            <td id="Td19" align="center" runat="server">
                                <asp:Label ID="lbl_document" runat="server" Style="color: Red;"></asp:Label>
                            </td>
                        </tr>--%>
                    </table>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="UploadButton1" EventName="Click" />--%>
                <%--<asp:PostBackTrigger ControlID="UploadButton1" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Bnt_View_Document" runat="server" Text="No" Style="display: none;" />
    <asp:Button ID="Bnt_View_Document_2" runat="server" Text="No" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="ModelPopupFor_Document_View" runat="server" Enabled="true"
        BackgroundCssClass="cssVEh" TargetControlID="Bnt_View_Document" PopupControlID="Panel_View_Document" 
        OkControlID="Bnt_View_Document_2">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
