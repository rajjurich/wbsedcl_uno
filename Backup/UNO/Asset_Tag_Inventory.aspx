<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="Asset_Tag_Inventory.aspx.cs" Inherits="UNO.Asset_Tag_Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script type="text/javascript">
        var interval;
        var obj = new ActiveXObject("VehicleManagementLib.Class1");
        var flag = false;
        var errmsgflag = false;
        function startRead() {
            try {
                document.getElementById('<%= lblPnlNew.ClientID %>').innerHTML = "";
                obj.getData();
                interval = setInterval(function () {
                    try {

                        if (flag == false) {
                            var rfid = obj.Inventory();
                            if (rfid != "") {
                                var ListBox1 = document.getElementById('<%= LstRFIDList.ClientID %>');
                                var newOption = window.document.createElement('OPTION');
                                newOption.text = rfid;
                                ListBox1.options.add(newOption);
                                flag = true;

                            }
                        }
                        else {
                            var flagExists = false;
                            var rfid = obj.Inventory();
                            var ListBox1 = document.getElementById('<%= LstRFIDList.ClientID %>');
                            if (rfid != "") {

                                for (var i = 0; i < ListBox1.options.length; i++) {
                                    if (rfid == ListBox1.options[i].text) {
                                        flagExists = true;
                                    }
                                }

                                if (flagExists == false) {
                                    var newOption = window.document.createElement('OPTION');
                                    newOption.text = rfid;
                                    ListBox1.options.add(newOption);
                                }
                            }
                        }

                    }
                    catch (e) {
                        alert(e.Message);
                    }
                }, 100);

            }
            catch (e) {

                alert(e.Message);

            }
            return false;
        }
        function dispose1() {
            clearInterval(interval);
            return false;
        }
        function test() {

            return false;
        }

        function set_ErrorLabel() {
            if (errmsgflag == true) {
                document.getElementById('<%= lblPnlNew.ClientID %>').innerHTML = "Records Saved Successflly";
            }
        }
        function saveRFID() {
            document.getElementById('<%= lblPnlNew.ClientID %>').innerHTML = "";
            dispose1();
            var objList = "";
            $("#ContentPlaceHolder1_ContentPlaceHolder1_LstRFIDList option").each(function (i) {
                if (objList == "") {
                    objList = $(this).text();
                } else {
                    objList = objList + ',' + $(this).text();
                }
            });


            if (objList.length != 0) {
                $.ajax({
                    url: "Asset_Tag_Inventory.aspx/SaveData",
                    type: "POST",
                    dataType: "json",
                    data: "{'Tags':'" + objList + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {

                        document.getElementById('<%= lblPnlNew.ClientID %>').innerHTML = msg.d;
                        $('#ContentPlaceHolder1_ContentPlaceHolder1_LstRFIDList > option').remove();
                    },
                    error: function () { alert(arguments[2]); }
                });

            }
            else {

                document.getElementById('<%= lblPnlNew.ClientID %>').innerHTML = "No Record Found";
            }
            // clearInterval(interval);

            return false;
        }
  

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            buttonClick();
        });


        function buttonClick() {
            $('#Rd1').click(function () {
                $('#FromToTag').show();
                $('#ReaderTag').hide();
                document.getElementById('<%= lblPnlNew.ClientID %>').innerHTML = "";

            });
            $('#Rd2').click(function () {
                $('#ReaderTag').show();
                $('#FromToTag').hide();
                document.getElementById('<%= lblPnlNew.ClientID %>').innerHTML = "";
           
            });
        }

        
         

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Asset Tag Inventory" ForeColor="RoyalBlue"
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
                        <asp:Button runat="server" ID="btnNew"  Text="New" CssClass="ButtonControl" OnClick="btnNew_Click" />
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="ButtonControl"
                            ValidationGroup="Search" OnClick="btnDelete_Click" />

                    </td>
                    <td>
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" Style="float: right;"
                            onclick="btnReset_Click" />

                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="float: right; margin-right:3px;" CssClass="ButtonControl"
                            OnClick="btnSearch_Click" />
                        <asp:TextBox ID="txtTagNo" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="WetxtControllerCode" runat="server" TargetControlID="txtTagNo"
                            WatermarkText="Number" WatermarkCssClass="watermark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:TextBox ID="txtTagStatus" runat="server" Style="float: right;" CssClass="searchTextBox"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="WetxtControllerDescription" runat="server"
                            TargetControlID="txtTagStatus" WatermarkText="Status" WatermarkCssClass="watermark">
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
                                <asp:GridView ID="grdAsset" runat="server" AutoGenerateColumns="false" Width="100%"
                                    GridLines="None" AllowPaging="true" PageSize="10" OnRowCommand="grdAsset_RowCommand"
                                    OnRowDataBound="grdAsset_RowDataBound">
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
                                                <asp:CheckBox ID="DeleteRows" runat="server" />
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("Tag_Id") %>'
                                                    CommandName="Modify" ForeColor="#3366FF" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="History">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkHistory" runat="server" CommandArgument='<%#Eval("TagEPCID") %>'
                                                    CommandName="History" ForeColor="#3366FF" Enabled="false" Text="History"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="TagEPCID" HeaderText="EPC ID" />
                                        <asp:BoundField DataField="OldStatus" HeaderText="Old Status" />
                                        <asp:BoundField DataField="CurStatus" HeaderText="Current Status" />

                                        <asp:BoundField DataField="Asset" HeaderText="Mapped With" />

                                        <asp:BoundField DataField="CreatedOn" HeaderText="Date" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOldStatus" runat="server" Text='<%#Eval("OldStatus")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblCurStatus" runat="server" Text='<%#Eval("CurStatus")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowID" runat="server" Text='<%#Eval("TagEPCID")%>' Visible="false"></asp:Label>
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
                    <asp:Label ID="lblMessages" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdAsset" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
              
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <asp:Panel ID="pnlAddCall" runat="server" CssClass="PopupPanel" Width="50%">
        <asp:UpdatePanel ID="UpPopUp" runat="server">
            <ContentTemplate>
                <div>
                    <table style="width: 100%" cellpadding="2" cellspacing="2">
                        <tr>
                            <td colspan="4" style="text-align: center">
                                <asp:Label ID="Label1" runat="server" Text="Tag Entry" ForeColor="RoyalBlue" Font-Size="20px"
                                    Visible="false" Width="100%" CssClass="heading">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <input id="Rd1" type="radio" value="From Tag To To Tag" name="Select"   checked="checked"/>Enter
                                Tag No
                                <input id="Rd2" type="radio" value="Read From Device" name="Select"  />Read From
                                Device
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <table id="FromToTag" width="100%">
                                    <tr>
                                        <td>
                                            From Tag No:<font color="red">*</font>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFromTag" runat="server" MaxLength="19"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtToTag" runat="server" ErrorMessage="Please Enter From Tag No"
                                                ValidationGroup="Add" ControlToValidate="txtFromTag" Display="None"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtToTag" runat="server" TargetControlID="rfvtxtToTag"
                                                PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            <asp:RegularExpressionValidator ValidationExpression="^(0|[1-9][0-9]*)$" ID="revtxtFromTag"
                                                runat="server" Display="None" ValidationGroup="Add" ErrorMessage="Allow only numbers"
                                                ControlToValidate="txtFromTag"></asp:RegularExpressionValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtFromTag" runat="server" TargetControlID="revtxtFromTag"
                                                PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td style="padding-left: 6px">
                                            To Tag No:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtToTag" runat="server" MaxLength="19"></asp:TextBox>

                                            <asp:RegularExpressionValidator ValidationExpression="^(0|[1-9][0-9]*)$" ID="revtxtToTag"
                                                runat="server" Display="None" ValidationGroup="Add" ErrorMessage="Allow only numbers"
                                                ControlToValidate="txtToTag"></asp:RegularExpressionValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerevtxtToTag" runat="server" TargetControlID="revtxtToTag"
                                                PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>

                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Tag Number should not be less than From Tag Number"
                                             ControlToValidate="txtToTag" ControlToCompare="txtFromTag" Type="Integer" Operator="GreaterThanEqual"  ValidationGroup="Add"
                                             Display="None" >
                                            </asp:CompareValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                TargetControlID="CompareValidator1" PopupPosition="Right">
                                            </ajaxToolkit:ValidatorCalloutExtender>

                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center">
                                            <asp:Button ID="btnSaveTags" runat="server" class="ButtonControl" Text="Save" ValidationGroup="Add"
                                                OnClick="btnSaveTags_Click" />
                                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="false"
                                                CssClass="ButtonControl" TabIndex="8" OnClick="btnCancelNew_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <table id="ReaderTag" style="display: none; width: 100%; text-align: center;">
                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            <asp:ListBox ID="LstRFIDList" runat="server" BackColor="#CCCCCC" Height="154px" Width="280px">
                                            </asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Button ID="Button1" runat="server" class="ButtonControl" OnClientClick="return startRead()"
                                                CausesValidation="false" Text="Start Reading" />
                                            <asp:Button ID="Button2" runat="server" class="ButtonControl" Text="Stop Reading"
                                                CausesValidation="false" OnClientClick="return dispose1()" Visible="False" />
                                            <asp:Button ID="btnsave" runat="server" class="ButtonControl" Text="Save" OnClientClick="return saveRFID()"
                                                CausesValidation="false" />
                                            <asp:Button ID="btnCancelRead" Text="Cancel" runat="server" CausesValidation="false"
                                                CssClass="ButtonControl" TabIndex="8" OnClick="btnCancelNew_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center;">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center;">
                                <asp:Label ID="lblPnlNew" runat="server" Text="" ClientIDMode="Static" CssClass="ErrorLabel"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSaveTags" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAddCall" runat="server" TargetControlID="Button3"
        PopupControlID="pnlAddCall" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancel">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlEdit" runat="server" CssClass="PopupPanel" Width="35%">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <table style="width: 100%">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label2" runat="server" Text="Edit Tag" ForeColor="RoyalBlue" Visible="false"
                                    Font-Size="20px" Width="100%" CssClass="heading">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                EPC ID: <font color="red">*</font>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditEpcid" runat="server" MaxLength="19" TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtEditEpcid" runat="server" ErrorMessage="Please Enter EPC ID"
                                    ValidationGroup="Edit" ControlToValidate="txtEditEpcid" Display="None"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtEditEpcid" runat="server" TargetControlID="rfvtxtEditEpcid"
                                    PopupPosition="Right">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                            <td style="padding-left: 1%">
                                Date :
                            </td>
                            <td>
                                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center;">
                                <br />
                                <asp:Button ID="btnSaveEdit" runat="server" CssClass="ButtonControl" Text="Save"
                                    ValidationGroup="Edit" OnClick="btnSaveEdit_Click" />
                                <asp:Button ID="btnCancelEdit" runat="server" CausesValidation="false" CssClass="ButtonControl"
                                    Text="Cancel" OnClick="btnCancelEdit_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center;">
                                <asp:Label ID="Label3" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeEdit" runat="server" TargetControlID="Button4"
        PopupControlID="pnlEdit" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancelEdit">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlHistory" runat="server" CssClass="PopupPanel" Width="40%">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div>
                    <table style="width: 100%">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label4" runat="server" Text="Edit Tag" ForeColor="RoyalBlue" Visible="false"
                                    Font-Size="20px" Width="100%" CssClass="heading">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="grdHistory" runat="server" AutoGenerateColumns="false" Width="100%"
                                    GridLines="None" AllowPaging="true" PageSize="10">
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
                                        <asp:BoundField DataField="TagEPCID" HeaderText="EPC ID" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <asp:BoundField DataField="OldStatus" HeaderText="Old Status" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="CurStatus" HeaderText="Current Status" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="CreatedOn" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <PagerTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: left; width: 15%;">
                                                    <span>Go To : </span>
                                                    <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChangePage_History">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="gvPrevious_History"
                                                        CssClass="ButtonControl" />
                                                    <asp:Label ID="lblShowing" runat="server" Text="Showing "></asp:Label>
                                                    <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="gvNext_History" CssClass="ButtonControl" />
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
                        <tr>
                            <td colspan="4" style="text-align: center;">
                                <br />
                                <br />
                                <asp:Button ID="btnHistoryCancel" runat="server" CausesValidation="false" CssClass="ButtonControl"
                                    Text="Cancel" OnClick="btnHistoryCancel_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center;">
                                <asp:Label ID="Label6" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeHistory" runat="server" TargetControlID="Button7"
        PopupControlID="pnlHistory" BackgroundCssClass="modalBackground" Enabled="true"
        CancelControlID="btnCancelEdit">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
