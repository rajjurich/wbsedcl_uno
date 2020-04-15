<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"  UICulture="en-GB" CodeBehind="Emp_Asset_Tag_Mapping.aspx.cs" Inherits="UNO.Emp_Asset_Tag_Mapping"  %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css" />
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script type="text/javascript"  language="javascript">

        function fnTestScript(msg) {
            $(document).ready(function () {
                //#ContentPlaceHolder1_ContentPlaceHolder1_ddlSearch
                $('#<%=ddlSearch.ClientID%>').change(function () {
                    //alert('dfgf');
                    $('#<%=txtSearch.ClientID%>').val("");
                });
            });
        }

        function getCode(chk) {
           
            var row = chk.parentNode.parentNode;
            var grdController = document.getElementById('<%=grdController.ClientID%>');

            var strVal = "";
            for (var i = 1; i <= grdController.rows.length - 1; i++) {

                var chk1 = grdController.rows[i].cells[0].children[0];

                if (chk1.checked == true) {
                    if(strVal=="")
                        strVal += grdController.rows[i].cells[0].children[1].value;
                    else
                        strVal += "," + grdController.rows[i].cells[0].children[1].value;
                }
            }
            if (strVal != "")
                document.getElementById('<%=hdnConcatenatedValue.ClientID%>').value = strVal;
        }

        function ctlrCheckBox(ID) {
            var grdController = document.getElementById('<%=grdController.ClientID%>');

            for (var i = 1; i <= grdController.rows.length - 1; i++) {

                var chk1 = grdController.rows[i].cells[0].children[0];
                var val = grdController.rows[i].cells[0].children[1].value;

                if (val == ID) {
                    chk1.checked = true;
                }
            }
        }

        function validateSave() {

            var ddlEmp = document.getElementById('<%= ddlEmp.ClientID %>');
            var ddlAsset = document.getElementById('<%= ddlAsset.ClientID %>');
            var ddlTag = document.getElementById('<%=ddlTag.ClientID %>');
            var txtFrom = document.getElementById('<%= txtValidFrom.ClientID %>');
            var txtTo = document.getElementById('<%= txtValidTo.ClientID %>');
            var lblMsg = document.getElementById('<%= lblMsg.ClientID %>');
            var grid = document.getElementById('<%= grdController.ClientID %>');
            
            if (ddlEmp.selectedIndex == 0) {
                lblMsg.innerHTML = "Please Select Employee.";
                return false;
            }
            if (ddlAsset.selectedIndex == 0) {
                lblMsg.innerHTML = "Please Select Asset.";
                return false;
            }
            if (ddlTag.selectedIndex == 0) {
                lblMsg.innerHTML = "Please Select Tag.";
                return false;
            }
            if (txtFrom.value == "" || txtTo.value == "") {
                lblMsg.innerHTML = "Please Enter Date(s).";
                return false;
            }
            else {
                var d1 = new Date(txtFrom.value);
                var d2 = new Date(txtTo.value);
                if (d2 < d1) {
                    lblMsg.innerHTML = "From Date Cannot Be Less Than Till Date.";
                    return false;
                }
            }
            
            var count = 0;
            var chk = grid.getElementsByTagName("input");
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].checked) {
                    count++;
                }
            }
            if (count == 0) {
                lblMsg.innerHTML = "Please Select Controller(s).";
                return false;
            }
            return true;
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
         <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Asset Monitoring Dashboard" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
        <asp:UpdatePanel runat="server" ID="upBody">
            <ContentTemplate>
                <div  class="DivEmpDetails">
                <table style="width: 100%;">
                    <tr> 
                        <td style="text-align:left">
                            <asp:Button runat="server" ID="btnNew" Text="New" CssClass="ButtonControl" 
                            onclick="btnNew_Click" OnClientClick="" />
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" 
                                CssClass="ButtonControl" onclick="btnDelete_Click"  />
                        </td>
                        <td style="text-align:right">
                            Search By : 
                            <asp:DropDownList ID="ddlSearch" runat="server" >
                            <asp:ListItem Text="Select" Value="5" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Asset Code" Value="0" ></asp:ListItem>
                                <asp:ListItem Text="Asset Description" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="Employee Code" Value="2" ></asp:ListItem>
                                <asp:ListItem Text="Employee Name" Value="3" ></asp:ListItem>
                                <asp:ListItem Text="Tag ID" Value="4" ></asp:ListItem>
                                
                            </asp:DropDownList>

                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        
                            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                CssClass="ButtonControl" onclick="btnSearch_Click" style="margin-right:3px;" />
                            <asp:Button ID="btnreset" runat="server" Text="Reset"
                                CssClass="ButtonControl" onclick="btnreset_Click" />

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 100%; background-color: #EFF8FE; ">
                            <asp:GridView ID="grdMapping" runat="server" AutoGenerateColumns="false" Width="100%" 
                                GridLines="None" AllowPaging="true" PageSize="10" 
                                onrowcommand="grdMapping_RowCommand">
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
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDelete" runat="server"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("EATM_ID")%>' 
                                            CommandName="Modify" ForeColor="#3366FF"  Text="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Asset_Id" HeaderText="Asset Code"/>
                                    <asp:BoundField DataField="Asset_Desc" HeaderText="Asset Description"/>
                                    <asp:BoundField DataField="Emp_Id" HeaderText="Employee Code"/>
                                    <asp:BoundField DataField="Name" HeaderText="Employee Name"/>
                                    <asp:BoundField DataField="Tag_Id" HeaderText="Tag"/>
                                    <asp:BoundField DataField="validFrom" HeaderText="Valid From"/>
                                    <asp:BoundField DataField="ValidTill" HeaderText="Valid Till"/>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="assetReturn" runat="server" CommandArgument='<%#Eval("EATM_ID")%>'
                                            CommandName="Return" ForeColor="#3366FF" Text="Return"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowID" runat="server" Text='<%#Eval("EATM_ID")%>' Visible="false"></asp:Label>
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
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
                
          <div style="text-align:center; " >
                    <asp:Label ID="lblMessages" runat="server" Text="" CssClass="ErrorLabel" ></asp:Label>
                </div>

    <asp:HiddenField ID="hdnConcatenatedValue" runat="server" />

        <asp:UpdatePanel ID="upPopUp" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlPopUp" class="PopupPanel" style="width:50% ;" runat="server">
                
                <asp:UpdatePanel ID="upInner" runat="server">
                    <ContentTemplate>
                
                    <table  style=" width:99% " >

                    <tr>
                        <td width="15%">
                            Employee :<font color="red">*</font>   
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlEmp" runat="server" Class="chosen-select" Width="99%" >
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlEmp" runat="server" ControlToValidate="ddlEmp"
                            InitialValue="Select" Display="None" ValidationGroup="Add">
                            </asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlEmp" runat="server" TargetControlID="rfvddlEmp"
                            PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>

                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                            Asset :<font color="red">*</font>
                        </td>
                        <td width="30%">
                            <asp:DropDownList ID="ddlAsset" runat="server" Class="chosen-select" AutoPostBack="true"
                                Width="98%" onselectedindexchanged="ddlAsset_SelectedIndexChanged" >
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlAsset" runat="server" ControlToValidate="ddlAsset"
                            InitialValue="Select" Display="None" ValidationGroup="Add">
                            </asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlAsset" runat="server" TargetControlID="rfvddlAsset"
                            PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td rowspan="4" width="50%" style="border:1px solid black; "  >
                            <fieldset style="overflow:auto ; width:99%; height:140px ; border:none;">
                                <legend style="text-align:center;" >Controller</legend>
                                <asp:GridView ID="grdController" runat="server" AutoGenerateColumns="false" 
                                Width="99%" >

                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" onClick="getCode(this)" />
                                                <asp:HiddenField ID="hdnController" runat="server" Value='<%#Eval("CtlrCode")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Description" DataField="CtlrDesc" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField HeaderText="Location" DataField="LocationID" ItemStyle-HorizontalAlign="Center"/>
                                    </Columns>
                                </asp:GridView>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Tag :<font color="red">*</font>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTag" runat="server" Class="chosen-select" Width="98%">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlTag" runat="server" ControlToValidate="ddlTag"
                            InitialValue="Select" Display="None" ValidationGroup="Add">
                            </asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvddlTag" runat="server" TargetControlID="rfvddlTag"
                            PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>

                        </td>
                    </tr>
                <tr>
                    <td>
                        Valid From :<font color="red">*</font>
                    </td>
                    <td>
                        <asp:TextBox ID="txtValidFrom" onkeyPress="javascript: return false" runat="server"
                        ClientIDMode="Static" Height="18px" Width="98%"></asp:TextBox>

                        <ajaxToolkit:CalendarExtender ID="txtValidFromCalendarExtender" TargetControlID="txtValidFrom"
                        PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>

                        <asp:RequiredFieldValidator ID="rfvtxtValidFrom" runat="server" ErrorMessage="Please Select From Date"
                        ControlToValidate="txtValidFrom" Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtfromDate1" runat="server" TargetControlID="rfvtxtValidFrom"
                        PopupPosition="Right">
                        </ajaxToolkit:ValidatorCalloutExtender>

                    </td>
                </tr>
                <tr>
                    <td>
                        Valid Till :<font color="red">*</font>
                    </td>
                    <td>
                        <asp:TextBox ID="txtValidTo" onkeyPress="javascript: return false" runat="server"
                        ClientIDMode="Static" Height="18px" Width="98%"></asp:TextBox>

                        <ajaxToolkit:CalendarExtender ID="txtValidToCalendarExtender" TargetControlID="txtValidTo"
                        PopupButtonID="txtshiftStartDate" runat="server" Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfvtxtValidTo" runat="server" ErrorMessage="Please Select From Date"
                        ControlToValidate="txtValidTo" Display="None" ValidationGroup="Add">
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvtxtValidTo"
                        PopupPosition="Right">
                        </ajaxToolkit:ValidatorCalloutExtender>

                        <%--<asp:CompareValidator ID="txtValidToCompValidator" runat="server" ErrorMessage="Please Select To date Greater than From Date" Type="Date"
                        ControlToValidate="txtValidTo" Display="None" ValidationGroup="Add" Operator="GreaterThanEqual" ControlToCompare="txtValidFrom" >
                        </asp:CompareValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vceCVtxtValidTo" runat="server" TargetControlID="txtValidToCompValidator"
                        PopupPosition="Right">
                        </ajaxToolkit:ValidatorCalloutExtender>--%>

                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align:center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="ButtonControl" 
                            onclick="btnSave_Click" ValidationGroup="Add" OnClientClick="return validateSave();" />   
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" style="margin-left:2%;" 
                            CssClass="ButtonControl" onclick="btnCancel_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align:center;">
                        <asp:Label ID="lblMsg" runat="server" Text="" CssClass="ErrorLabel" ></asp:Label>
                    </td>
                </tr>
            </table>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </asp:Panel>

            <asp:Button ID="dummy" runat="server" style="display:none" />

            <ajaxToolkit:ModalPopupExtender ID="mpeAddEdit" runat="server" PopupControlID="pnlPopUp" 
            TargetControlID="dummy" BackgroundCssClass="modalBackground" ></ajaxToolkit:ModalPopupExtender>

        </ContentTemplate>
        
        </asp:UpdatePanel>

        <asp:Panel ID="returnPopUp" runat="server" class="PopupPanel" style="width:20% ;" >
            <asp:UpdatePanel ID="returnUpdate" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td colspan="2" style="text-align:center;"> Are you sure you want to return asset ?</td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center;">
                                <asp:CheckBox ID="chkReturn" runat="server" Text="Keep Asset Tag Mapping"/> 
                            </td>
                        </tr>
                        <tr>
                            <td style=" float:right;" >
                                <asp:Button ID="btnOK" runat="server" Text="Ok" CssClass="ButtonControl" 
                                    onclick="btnOK_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="ButtonControl" 
                                    onclick="btnClose_Click"/>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>

            <asp:Button ID="dummy1" runat="server" style="display:none" />
        <ajaxToolkit:ModalPopupExtender ID="mpeReturnConfirm" runat="server" PopupControlID="returnPopUp" 
        TargetControlID="dummy1" BackgroundCssClass="modalBackground" >

        </ajaxToolkit:ModalPopupExtender>

     </ContentTemplate>
    </asp:UpdatePanel>

    <script src="Scripts/Choosen/chosen.jquery.js" type="text/javascript"></script>
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
