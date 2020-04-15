<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="Card_View.aspx.cs" Inherits="UNO.Card_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link rel="stylesheet" href="Scripts/Choosen/chosen.css" />
    <script language="javascript">

        $(document).ready(function ready() {
            var empCode = document.getElementById('<%= hfEmpCode.ClientID %>').value;
            var element = document.getElementById('<%= btnAdd.ClientID %>');
            var conn = document.getElementById('<%= hfconnection.ClientID %>').value;
            var gridView = document.getElementById('<%= gvCardView.ClientID %>');
          
            $("#btnAdd").click(function () {
                try {
                    gridView.disabled = true;
                    gridView.style.cursor = 'wait';
                    var anchors = document.getElementsByTagName("a");
                    for (var i = 0; i < anchors.length; i++) {
                        anchors[i].onclick = function () { return (false); };
                    }
                    var WshShell = new ActiveXObject("WScript.Shell");
                    var Return = WshShell.Run("C:/CardDesign/KCards.exe " + empCode + " New " + conn, 1, true);
                    gridView.disabled = false;
                    gridView.style.cursor = 'default';
                    for (var i = 0; i < anchors.length; i++) {
                        anchors[i].onclick = function () { return (true); };
                    }
                } catch (e) {
                    alert(e.Message);
                }


            });
        });
        function CallExe(RowId, TempName, Category) {
            try {
                var empCode1 = document.getElementById('<%= hfEmpCode.ClientID %>').value;
                var conn1 = document.getElementById('<%= hfconnection.ClientID %>').value;
                var WshShell = new ActiveXObject("WScript.Shell");
             
                var str = conn1 + " " + RowId + " " + TempName + " " + Category;
              
                var gridView = document.getElementById('<%= gvCardView.ClientID %>');
                gridView.disabled = true;
                gridView.style.cursor = 'wait';
                var anchors = document.getElementsByTagName("a");
                for (var i = 0; i < anchors.length; i++) {
                    anchors[i].onclick = function () { return (false); };
                }
                var Return = WshShell.Run("C:/CardDesign/KCards.exe " + empCode1 + " Edit " + str, 1, true);
                gridView.disabled = false;
                gridView.style.cursor = 'default';
                var anchors = document.getElementsByTagName("a");
                for (var i = 0; i < anchors.length; i++) {
                    anchors[i].onclick = function () { return (true); };
                }
            } catch (e) {
            alert(e.Message);
            }
    }
    window.onunload = function () {
        //alert('Bye.');
    }
    </script>
        <script type="text/javascript">
            if ("ActiveXObject" in window) { }
            else { alert("This page will work only in IE 8 & 9"); window.history.back(); }
      
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:HiddenField ID="hfEmpCode" runat="server" />
<asp:HiddenField ID="hfconnection" runat="server" />

    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Card View" ForeColor="RoyalBlue" Font-Size="20px"
                    Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <div class="DivEmpDetails">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 70%; text-align: left;">
                        <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl"  ClientIDMode="Static"  OnClick="btnAdd_Click"/>
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                            OnClick="btnDelete_Click" />
                            <b>Issuing Authority :</b> <span  style="color:Yellow;font-weight:bold"><asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                            (<asp:Label ID="lblCode"  runat="server"  Text=""></asp:Label>)</span>
                           &nbsp;  &nbsp;  <b>Change Issuing Authority :</b>
                        <asp:DropDownList ID="ddlIssuingAuth" runat="server" Class="chosen-select" 
                            onselectedindexchanged="ddlIssuingAuth_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                          
                    </td>
                    <td style="width: 30%; text-align: right;">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" CssClass="ButtonControl"
                            OnClick="btnSearch_Click" />
                        <asp:TextBox ID="txtCtlrID" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCtlrID" runat="server" TargetControlID="txtCtlrID"
                            WatermarkText="Search by Category Name" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:TextBox ID="txtCtlrDesc" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCtlrDesc" runat="server" TargetControlID="txtCtlrDesc"
                            WatermarkText="Search by Template Name" WatermarkCssClass="watermark">
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
                                <asp:GridView ID="gvCardView" runat="server" AutoGenerateColumns="false" Width="100%"
                                    GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="gvCardView_RowCommand" 
                                    OnPageIndexChanging="gvCardView_PageIndexChanging" 
                                    onrowdatabound="gvCardView_RowDataBound">
                                    <RowStyle CssClass="gvRow" />
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                    <PagerStyle CssClass="gvPager" />
                                    <EmptyDataTemplate>
                                        <div>
                                            <span>No Record found.</span>
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
                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                    Text="Edit" ForeColor="#3366FF" CommandArgument='<%#Eval("intId")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Template Name"  HeaderStyle-CssClass="center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%#Eval("intId")%>'></asp:Label>
                                                 <asp:Label ID="lblTemplate" runat="server" Text='<%#Eval("nvarTemplateName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Category Name"  HeaderStyle-CssClass="center">
                                            <ItemTemplate>
                                             <asp:Label ID="lblCategoryId" runat="server" Text='<%#Eval("OCE_ID")%>'></asp:Label>
                                                 <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("OCE_DESCRIPTION")%>'></asp:Label>
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
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblMessages" runat="server" Text="" Visible="false" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="gvCardView" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            </Triggers>
        </asp:UpdatePanel>
    </center>
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
