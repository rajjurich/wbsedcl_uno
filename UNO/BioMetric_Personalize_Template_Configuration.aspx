<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="BioMetric_Personalize_Template_Configuration.aspx.cs" Inherits="UNO.BioMetric_Personalize_Template_Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script src="Scripts/Validation.js" type="text/javascript"></script>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" />
<script type="text/javascript">
    $(function () {
        $("[id$=txtSearch]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/BioMetric_Personalize_Template_Configuration.aspx/GetCustomers") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        $("#txtSearch").attr("disabled", false); 
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('-')[0],
                                val: item.split('-')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("[id$=hfId]").val(i.item.val);
            },
            minLength: 1
        });
    });  
</script>





    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Bio Matric Employee Template Configuration"
                    ForeColor="RoyalBlue" Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <asp:Panel ID="Panel2" runat="server" DefaultButton="btnSearch">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 20%; text-align: left;">
                            <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" OnClick="btnAdd_Click" />
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                                OnClick="btnDelete_Click" />
                        </td>
                        <td style="width: 40%; text-align: right;">
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                                OnClientClick="return ResetAll();" />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right: 3px;"
                                CssClass="ButtonControl" OnClick="btnSearch_Click" />
                            <asp:TextBox ID="txtEntityName" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtEntityName" runat="server" TargetControlID="txtEntityName"
                                WatermarkText="Entity Name" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:TextBox ID="txtEntityId" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtEntityId" runat="server" TargetControlID="txtEntityId"
                                WatermarkText="Entity Id" WatermarkCssClass="watermark">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; background-color: #EFF8FE; padding: 0px;" colspan="3">
                            <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                                <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvEmployeeAccess" runat="server" DataKeyNames="ID,EmpId,EMPNAME,FingureForTA"
                                        AutoGenerateColumns="false" Width="100%" GridLines="None" AllowPaging="true"
                                        PageSize="10" OnRowCommand="gvEmployeeAccess_RowCommand">
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
                                            <asp:TemplateField HeaderText="Select" AccessibleHeaderText="Select" SortExpression="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="DeleteRows" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" AccessibleHeaderText="Edit" SortExpression="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Modify" CommandArgument='<%# Eval("ID") %>'
                                                        ForeColor="#3366FF">Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmpId" HeaderText="Employee ID" SortExpression="EmpId">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EMPNAME" HeaderText="Employee Name" SortExpression="EMPNAME">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FingureForTA" HeaderText="Fingure For Time Attandance"
                                                SortExpression="FingureForTA">
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
            </asp:Panel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Button ID="btnDummyAdd" runat="server" Text="dummy" Style="display: none;" />
    <asp:Button ID="btnDummyEdit" runat="server" Text="dummy" Style="display: none;" />
    <asp:Panel ID="pnlAddEmployeeAccess" runat="server" CssClass="PopupPanel" Style="width: 27%">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table id="table2" runat="server" width="100%" border="0" cellpadding="0" cellspacing="5"
                    class="TableClass">
                    <tr>
                        <td style="text-align: center;">
                            Employee :<label class="CompulsaryLabel">*</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearch" runat="server" Width="227px" />
                            <asp:HiddenField ID="hfId" runat="server" />                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            
                                <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                                        <div style="position: relative;">
                                            <span style="position: absolute; top: 40px; left: -2px;">
                                                <%--GroupName="LH"RadioButton--%>
                                                <span style="padding: 6px 3px 2px 3px; background-color: green; border-radius: 50%;">
                                                    <asp:CheckBox ID="LHR1" runat="server" Enabled="false" AutoPostBack="True" OnCheckedChanged="LHR1Changed"
                                                        ToolTip="Use For Time Attendance" /></span></span><span style="position: absolute;
                                                            top: 14px; left: 26px;"><span style="padding: 6px 3px 2px 3px; background-color: green;
                                                                border-radius: 50%;">
                                                                <asp:CheckBox ID="LHR2" runat="server" Enabled="false" AutoPostBack="True" OnCheckedChanged="LHR2Changed"
                                                                    ToolTip="Use For Time Attendance" /></span></span><span style="position: absolute;
                                                                        top: 3px; left: 59px;"><span style="padding: 6px 3px 2px 3px; background-color: green;
                                                                            border-radius: 50%;">
                                                                            <asp:CheckBox ID="LHR3" runat="server" Enabled="false" AutoPostBack="True" OnCheckedChanged="LHR3Changed"
                                                                                ToolTip="Use For Time Attendance" /></span></span>
                                            <span style="position: absolute; top: 18px; left: 89px;"><span style="padding: 6px 3px 2px 3px;
                                                background-color: green; border-radius: 50%;">
                                                <asp:CheckBox ID="LHR4" runat="server" Enabled="false" AutoPostBack="True" OnCheckedChanged="LHR4Changed"
                                                    ToolTip="Use For Time Attendance" /></span></span><span style="position: absolute;
                                                        top: 92px; left: 135px;"><span style="padding: 6px 3px 2px 3px; background-color: green;
                                                            border-radius: 50%;">
                                                            <asp:CheckBox ID="LHR5" runat="server" Enabled="false" AutoPostBack="True" OnCheckedChanged="LHR5Changed"
                                                                ToolTip="Use For Time Attendance" /></span></span>
                                            <asp:Image ID="imgLeft" runat="server" ImageUrl="~/images/LeftHand.png" />
                                            <span style="position: absolute; top: 93px; left: 160px;">
                                                <%--GroupName="RH"--%>
                                                <span style="padding: 6px 3px 2px 3px; background-color: green; border-radius: 50%;">
                                                    <asp:CheckBox ID="RHR1" runat="server" Enabled="false" AutoPostBack="True" OnCheckedChanged="RHR1Changed"
                                                        ToolTip="Use For Time Attendance" /></span></span><span style="position: absolute;
                                                            top: 18px; left: 201px;"><span style="padding: 6px 3px 2px 3px; background-color: green;
                                                                border-radius: 50%;">
                                                                <asp:CheckBox ID="RHR2" runat="server" Enabled="false" AutoPostBack="True" OnCheckedChanged="RHR2Changed"
                                                                    ToolTip="Use For Time Attendance" /></span></span><span style="position: absolute;
                                                                        top: 4px; left: 231px;"><span style="padding: 6px 3px 2px 3px; background-color: green;
                                                                            border-radius: 50%;">
                                                                            <asp:CheckBox ID="RHR3" runat="server" Enabled="false" AutoPostBack="True" OnCheckedChanged="RHR3Changed"
                                                                                ToolTip="Use For Time Attendance" /></span></span>
                                            <span style="position: absolute; top: 13px; left: 264px;"><span style="padding: 6px 3px 2px 3px;
                                                background-color: green; border-radius: 50%;">
                                                <asp:CheckBox ID="RHR4" runat="server" Enabled="false" AutoPostBack="True" OnCheckedChanged="RHR4Changed"
                                                    ToolTip="Use For Time Attendance" /></span></span><span style="position: absolute;
                                                        top: 41px; left: 290px;"><span style="padding: 6px 3px 2px 3px; background-color: green;
                                                            border-radius: 50%;">
                                                            <asp:CheckBox ID="RHR5" runat="server" Enabled="false" AutoPostBack="True" OnCheckedChanged="RHR5Changed"
                                                                ToolTip="Use For Time Attendance" /></span></span>
                                            <asp:Image ID="imgRight" runat="server" ImageUrl="~/images/RightHand.png" />
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnSubmitAdd" runat="server" CssClass="ButtonControl" Text="Save"
                                ValidationGroup="Add" OnClick="btnSubmitAdd_Click" Visible="false" />
                            <asp:Button ID="btnSubmitEdit" runat="server" CssClass="ButtonControl" Text="Update"
                                ValidationGroup="Add" OnClick="btnSubmitEdit_Click" Visible="false" />
                            <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" Text="Cancel"
                                OnClick="btnCancelAdd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Label ID="lblErrorAdd" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeAddEmployeeAccess" runat="server" TargetControlID="btnDummyAdd"
        PopupControlID="pnlAddEmployeeAccess" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancelAdd">
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
    <script type="text/javascript">
<!--

        //alert(myfilter);
//-->
    </script>
</asp:Content>
