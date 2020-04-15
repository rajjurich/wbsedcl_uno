<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" CodeBehind="EnglishToHindiView.aspx.cs" Inherits="UNO.EnglishToHindiView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Employee Details (English-Hindi Translation)" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
</asp:Table>
<center>
 <div class="DivEmpDetails">
        <table style="width: 100%;">
            <tr>
                <td style="width: 50%; text-align: left;">
                  
                    <asp:Button runat="server" ID="btnDelete" Text="Delete" 
                        CssClass="ButtonControl" onclick="btnDelete_Click" />
                </td>
                <td style="width: 50%; text-align: right;">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;" 
                        CssClass="ButtonControl" onclick="btnSearch_Click" />
                    <asp:TextBox ID="txtCtlrID" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCtlrID" runat="server" TargetControlID="txtCtlrID"
                        WatermarkText="Search by Employee Name" WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    <asp:TextBox ID="txtCtlrDesc" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtCtlrDesc" runat="server" TargetControlID="txtCtlrDesc"
                        WatermarkText="Search by Employee Id" WatermarkCssClass="watermark">
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
                            <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false" Width="100%"
                                GridLines="None" AllowPaging="true" PageSize="10" 
                                onrowcommand="gvView_RowCommand" 
                                onpageindexchanging="gvView_PageIndexChanging" >
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
                                    <asp:TemplateField HeaderText="Edit"  SortExpression="Edit" ItemStyle-Width="5%" HeaderStyle-CssClass="center" >                                        
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                Text="Edit" ForeColor="#3366FF" CommandArgument='<%#Eval("EPD_EMPID")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EPD_EMPID" HeaderText="Employee Id" SortExpression="EPD_EMPID" ItemStyle-Width="5%">                                      
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EMP_NAME" HeaderText="Employee Name" ItemStyle-Wrap="true" ItemStyle-Width="10%"
                                        SortExpression="EMPLOYEE NAME"></asp:BoundField>
                                           <asp:BoundField DataField="^EMPLOYEE ID" HeaderText="Employee Id(Hindi)" ItemStyle-Wrap="true" ItemStyle-Width="10%"
                                        SortExpression="EMPLOYEE ID"></asp:BoundField>
                                           <asp:BoundField DataField="^EMPLOYEE NAME" HeaderText="Employee Name(Hindi)" ItemStyle-Wrap="true" ItemStyle-Width="10%"
                                        SortExpression="EMPLOYEE NAME"></asp:BoundField>
                                         
                                           <asp:BoundField DataField="^LOCATION NAME" HeaderText="Location" ItemStyle-Wrap="true" ItemStyle-Width="10%"
                                        SortExpression="LOCATION NAME"></asp:BoundField>
                                           <asp:BoundField DataField="^DEPARTMENT NAME" HeaderText="Department" ItemStyle-Wrap="true" ItemStyle-Width="10%"
                                        SortExpression="DEPARTMENT NAME"></asp:BoundField>
                                     
                                
                                   
                                                               
                                </Columns>
                                <PagerTemplate>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: left; width: 15%;">
                                                <span>Go To : </span><asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ChangePage">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="gvPrevious" CssClass="ButtonControl" />
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

            <asp:AsyncPostBackTrigger ControlID="btnDelete" />
            <asp:AsyncPostBackTrigger ControlID="gvView" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
         
          
        </Triggers>
    </asp:UpdatePanel>
</center>
</asp:Content>
