<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" CodeBehind="EmployeePinConfigView.aspx.cs" Inherits="UNO.EmployeePinConfigView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script type="text/javascript" language="javascript">
        function Confirm() {
            var confi = confirm("Are you Sure!");
            if (confi == true) {
                return true;
            }
            else {
                window.location = 'EmployeePinConfigView.aspx';
            }
        }
        function onUpdating() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            // make it visible
            updateProgressDiv.style.display = '';

            //  get the gridview element        
            var gridView = $get('<%= this.gvCardConfig.ClientID %>');

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
        function ResetAll() {

            $('#' + ["<%=textzoneid.ClientID%>", "<%=textzonename .ClientID%>"].join(', #')).prop('value', "");
            document.getElementById('<%=lblMessages.ClientID%>').innerHTML = "";
            document.getElementById('<%=textzonename .ClientID%>').focus();
            document.getElementById('<%=textzoneid.ClientID%>').focus();
            document.getElementById('<%=btnSearch.ClientID%>').click();
            document.getElementById('<%=gvCardConfig.ClientID%>').focus();
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
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
 <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Employee Pin Config" ForeColor="RoyalBlue"
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
                    <asp:Button runat="server" ID="btnAdd" Text="New" CssClass="ButtonControl" onclick="btnAdd_Click" 
                        />
                    <asp:Button runat="server" ID="btnDelete" Text="Delete" 
                        CssClass="ButtonControl" onclick="btnDelete_Click"   />
                </td>
         
                <td style="width: 50%; text-align: right;">
                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;" OnClientClick="return ResetAll();" />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right;margin-right:3px;" 
                        CssClass="ButtonControl" onclick="btnSearch_Click"  />
                    <asp:TextBox ID="textzonename" runat="server" onkeydown="return (event.keyCode!=13);" Style="float: right;" 
                        CssClass="searchTextBox"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="twetxtzonename" runat="server" TargetControlID="textzonename"
                        WatermarkText="Card No" WatermarkCssClass="watermark">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    <asp:TextBox ID="textzoneid" runat="server" MaxLength="10" Style="float: right;" 
                        CssClass="searchTextBox"></asp:TextBox>
                     <ajaxToolkit:TextBoxWatermarkExtender ID="twezoneid" runat="server" TargetControlID="textzoneid"
                        WatermarkText="Employee ID" WatermarkCssClass="watermark">
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
                            <asp:GridView ID="gvCardConfig" runat="server"  AutoGenerateColumns="False" Width="100%"
                                GridLines="None" AllowPaging="true" PageSize="10" 
                                onrowcommand="gvCardConfig_RowCommand" 
                                onpageindexchanging="gvCardConfig_PageIndexChanging">
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
                                     <asp:TemplateField HeaderText="Select" AccessibleHeaderText="Select" SortExpression="Select" ItemStyle-Width="5%" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="DeleteRows" runat="server" />
                                        </ItemTemplate>
                                         <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Change Status" AccessibleHeaderText="Edit" SortExpression="Edit" ItemStyle-Width="10%" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Modify"  CommandArgument='<%# Eval("cc_emp_id") %>' OnClientClick = "Confirm()" ForeColor="#3366FF">Change Status</asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Change Pin" AccessibleHeaderText="Pin" SortExpression="Pin" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPin" runat="server" CommandName="ModifyPin"  CommandArgument='<%# Eval("cc_emp_id") %>'  OnClientClick = "Confirm()" ForeColor="#3366FF">Change Pin</asp:LinkButton>
                                        </ItemTemplate>
                                         <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="name" HeaderText="Employee Name" />
                       <asp:BoundField DataField="CC_EMP_ID" HeaderText="Employee ID" SortExpression="ID">
                                <%--  <ItemStyle HorizontalAlign="Left" Width="10%"  />--%>
                            </asp:BoundField>
                            <asp:BoundField DataField="card_code" HeaderText="Card Code" ItemStyle-Width="10%"
                                SortExpression="Card Code" Visible="false">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Pin" HeaderText="Pin" SortExpression="Pin">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Usecount" HeaderText="Usecount" SortExpression="Usecount"
                                ItemStyle-Width="10%" Visible="false">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ignore_APB" HeaderText="Ignore APB" SortExpression="Ignore APB" Visible="false">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                             <asp:BoundField DataField="status" HeaderText="Status" SortExpression="Status">
                             <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Activation_date" HeaderText="Activation Date" SortExpression="Activation Date" Visible="false" />
                            <asp:BoundField DataField="Expiry_Date" HeaderText="Expiry Date" SortExpression="Expiry Date" Visible="false" />
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
                           
                        </Triggers>
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
</div>

 

            <asp:Panel ID="pnlmodify" runat="server" Width="60%" Height="20%" Style="background-color: White;
        border: 5px solid #000000; border-radius: 25px; color: Black;">
            <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
      <table id="Table4" runat="server" class="tableclass" width="100%" style="margin:2%;margin-top:1%;">
 
  <tr>
  <td class="style37" >
      <asp:Label ID="lblMinimumswipeT" runat="server" Text="Use Count :"></asp:Label><label class="CompulsaryLabel">*</label>
      <br /></td>
  <td class="TDClassControl">
      <%--<asp:TextBox ID="txtshiftcode" Width="50px" class="TextControl" runat="server" ontextchanged="txtshiftcode_TextChanged"></asp:TextBox>--%>
          <asp:TextBox ID="Card_Cd" runat="server" ClientIDMode="Static" 
          CssClass="TextControl" Height="16px" MaxLength="8"  style="display:none;"
           TabIndex="2" 
          Enabled="False"></asp:TextBox>
      <asp:TextBox ID="txtMinimumSwipe" runat="server" ClientIDMode="Static" 
          CssClass="TextControl" MaxLength="2" 
           Width="117px"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter User Count here"
                                ControlToValidate="txtMinimumSwipe" Display="None" ValidationGroup="add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                TargetControlID="RequiredFieldValidator3" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>

      </td>

  
  <td class="style38">
  <asp:Label ID="Label1" runat="server" Text="Pin"></asp:Label>
         <label class="CompulsaryLabel">*</label>    
       </td>
   <td class="TDClassControl">
                             <asp:TextBox ID="Pin" runat="server" 
    onkeypress="return IsNumber(event)" CssClass="TextControl"
                        MaxLength="6" TabIndex="3" ClientIDMode="Static"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="rfvtxtDescriptionAdd" runat="server" ErrorMessage="Please Enter PIN here"
                                ControlToValidate="Pin" Display="None" ValidationGroup="add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtDescriptionAdd" runat="server"
                                TargetControlID="rfvtxtDescriptionAdd" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
                    
             
                     
  </td>
  </tr>
  <tr>
   <td class="style37">Activation Date :
       <label class="CompulsaryLabel">*</label> </td>
  <td class="TDClassControl" >
      <asp:TextBox ID="Act_Date" runat="server" ClientIDMode="Static" 
          CssClass="TextControl" MaxLength="10" onkeydown="date_dash(this,event)" 
          onkeypress="return IsNumber(event)" onkeyup="date_dash(this,event)" 
          TabIndex="8"></asp:TextBox>
      <ajaxToolkit:CalendarExtender ID="cat01" runat="server" Format="dd/MM/yyyy" 
          PopupButtonID="Act_Date" TargetControlID="Act_Date">
      </ajaxToolkit:CalendarExtender>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter activation date here"
                                ControlToValidate="Act_Date" Display="None" ValidationGroup="add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                TargetControlID="RequiredFieldValidator1" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>
      
  </td>
  <td class="style38">
      Expiry Date :<label class="CompulsaryLabel">*</label></td>
  <td class="TDClassControl">
                    <asp:TextBox ID="Exp_Date1" runat="server" ClientIDMode="Static" 
                        CssClass="TextControl" MaxLength="10" onkeydown="date_dash(this,event)" 
                        onkeypress="return IsNumber(event)" onkeyup="date_dash(this,event)" 
                        TabIndex="9"></asp:TextBox>
                              <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
          PopupButtonID="Exp_Date1" TargetControlID="Exp_Date1">
      </ajaxToolkit:CalendarExtender>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter expiry date here"
                                ControlToValidate="Exp_Date1" Display="None" ValidationGroup="add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                TargetControlID="RequiredFieldValidator2" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>


  </td>

  </tr>
  <tr>
  <td class="style37">
      <asp:Label ID="Label3" runat="server" Text="Status"></asp:Label>
      </td>
  <td class="TDClassControl" >
                    <asp:CheckBox ID="Chk_Sts" runat="server" Checked="true" ClientIDMode="Static" 
                        TabIndex="7" />
      </td>
      <td class="style38">
          </td>
  
      <td class="TDClassControl">
          &nbsp;</td>


  </tr>
  <tr>
   <td class="style37" >
                    &nbsp;<asp:CheckBox ID="Chk_IgrAPB" runat="server" Checked="true" style="display:none;"
                        ClientIDMode="Static" TabIndex="5" />
                </td>
          

                   <td class="TDClassControl">
                    <%--<input type="text" class= "TDClassControl"  onkeypress="return IsNumber(event)"  id = "Act_Date" Visible = "true" runat="server" onkeyup="date_dash(this,event)" onkeydown="date_dash(this,event)" maxlength="10" clientidmode="Static" />--%>
                       <asp:CheckBox ID="Chk_SetAPB" runat="server" Checked="true" style="display:none;"
                           ClientIDMode="Static" TabIndex="6" />
                    

                </td>
              <td class="style38" >
                    &nbsp;</td>
             
             <td>
             
                 &nbsp;</td>
  
  </tr>
  <tr>
  <td colspan="4" style="text-align:center;">
  <asp:Button ID="Save" runat="server" CssClass="ButtonControl" Text="Save" TabIndex="8"
                   ValidationGroup="ADD" onclick="Save_Click" />
                    <asp:Button ID="Cancel" runat="server" CssClass="ButtonControl" 
          Text="Cancel" TabIndex="8" onclick="Cancel_Click"
                    />
  </td>
  </tr>
  </table>
             </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
      <asp:Button ID="btndummyEdit2" runat="server" Text="Dummy" Style="display: none;" />
    <asp:Button ID="btndummyEdit" runat="server" Text="Dummy" Style="display: none;" />

       <ajaxToolkit:ModalPopupExtender ID="mpeditPopup" runat="server" BackgroundCssClass="cssVEh"
        Enabled="true" PopupControlID="pnlmodify" TargetControlID="btndummyEdit"
        OkControlID="btndummyEdit2">
    </ajaxToolkit:ModalPopupExtender>
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

</asp:Content>
