<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" CodeBehind="EmployeePinConfigureADD.aspx.cs" Inherits="UNO.EmployeePinConfigureADD" Culture="en-GB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title></title>
    <%--<link href="Styles/Style.css" rel="Stylesheet" type="text/css" />--%>
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script language="javascript" type="text/javascript">



        function IsAlphanumeric(evnt) {


            var charCode = (evnt.which) ? evnt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) || (charCode >= 48 && charCode <= 57) ||
			(charCode >= 97 && charCode <= 122) || (charCode == 8) || (charCode == 32)) {
                return true
            }
            else {
                return false
            }
        }

        function IsNumericSwipe(evnt) {

            var charCode = (evnt.which) ? evnt.which : event.keyCode
            if ((charCode >= 48 && charCode <= 57) || (charCode == 8) || (charCode == 32)) {
                return true
            }
            else {
                return false
            }
        }


        function resetAll() {
            $("#<%=RbdEmp.ClientID %>,#<%=RbdCompany.ClientID %>,#<%=RbdLocation.ClientID %>,#<%=RbdDivision.ClientID %>,#<%=RbdDepartment.ClientID %>").find('input').prop('disabled', false);
            $("#RbdEmp_0,#RbdCompany_0,#RbdLocation_0,#RbdDivision_0,#RbdDepartment_0").prop("checked", true);
            $("#EmployeeHdn,#ComapnyHdn,#LocationHdn,#DivisionHdn,#DepartmentHdn").prop("value", "");
            $("#Chk_Sts").prop("checked", false);
            $('#' + ["<%=Exp_Date1.ClientID%>", "<%=Act_Date.ClientID%>", "<%=txtMinimumSwipe.ClientID%>", "<%=Pin.ClientID %>"].join(', #')).prop('value', "");
            return false;
        }

        function ValidateSave() {

            var first = "";
            var count = 0;
            var emp = document.getElementById('<%=EmployeeHdn.ClientID%>').value;
            var com = document.getElementById('<%=ComapnyHdn.ClientID%>').value;
            var loc = document.getElementById('<%=LocationHdn.ClientID %>').value;
            var div = document.getElementById('<%=DivisionHdn.ClientID %>').value;
            var dep = document.getElementById('<%=DepartmentHdn.ClientID %>').value;
            var sft = document.getElementById('<%=ShiftHdn.ClientID%>').value;
            if (ChkServerSideValidation()) {
                if (emp == "" && com == "" && loc == "" && div == "" && dep == "" && sft == "") {

                    var msg = confirm("No Entity Selected,Are you Sure To continue?");
                    if (msg == false) {
                        return false;
                    }
                    else {
                        if (!ChkServerSideValidation()) {
                            return false;
                        }
                        else return true;
                    }
                }
            }
            else {
                return false;
            }

        }

        function ChkServerSideValidation() {


        }

    </script>
    <style type="text/css">
        #form1
        {
            font-weight: 700;
        }
        
        body
        {
            color: #515456;
        }
        
        
        
        .style37
        {
            font-family: Verdana;
            font-size: 9pt;
            color: #515456;
            border: 0px solid #CCCCCC;
            text-align: right;
            padding-right: 4px;
            width: 30%;
        }
        .style38
        {
            font-family: Verdana;
            font-size: 9pt;
            color: #515456;
            border: 0px solid #CCCCCC;
            text-align: right;
            padding-right: 4px;
            width: 40%;
        }
        
        
        
        .TextControl
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblHederText" runat="server" Text="Employee Pin Config Master" CssClass="heading"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlFilter" runat="server">
        <table class="TableClass" style="width: 70%; height: 75px; margin-left: 15%;">
            <tr>
                <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;
                    width: 1%;">
                    <table style="height: 75px; width: 102px;">
                        <tr>
                            <td class="style29">
                                &nbsp;Employee
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" class="style29">
                                <asp:RadioButtonList ID="RbdEmp" runat="server" RepeatLayout="Flow" CssClass="radiobutton"
                                    ClientIDMode="Static">
                                    <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                    <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="EmployeeHdn" runat="server" ClientIDMode="Static" />
                </td>
                <td style="border: thin solid lightsteelblue; text-align: left; font-weight: bold;
                    width: 7%;display:none;">
                    <table style="height: 75px;">
                        <tr>
                            <td>
                                Company
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:RadioButtonList ID="RbdCompany" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                    ClientIDMode="Static">
                                    <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="ComapnyHdn" runat="server" ClientIDMode="Static" />
                </td>
                <td style="width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                    border-bottom: lightsteelblue thin solid; font-weight: bold;display:none">
                    <table style="height: 75px;">
                        <tr>
                            <td>
                                &nbsp;Location
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:RadioButtonList ID="RbdLocation" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                    ClientIDMode="Static">
                                    <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="LocationHdn" runat="server" ClientIDMode="Static" />
                </td>
                <td style="width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                    border-bottom: lightsteelblue thin solid; font-weight: bold;display:none">
                    <table style="height: 75px;">
                        <tr>
                            <td>
                                &nbsp;Division
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:RadioButtonList ID="RbdDivision" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                    ClientIDMode="Static">
                                    <asp:ListItem Value="0" Selected="True" onclick="showDivision()">All</asp:ListItem>
                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="DivisionHdn" runat="server" ClientIDMode="Static" />
                </td>
                <td style="width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                    border-bottom: lightsteelblue thin solid; font-weight: bold;display:none">
                    <table style="height: 75px;" id="TABLE2">
                        <tr>
                            <td>
                                &nbsp;Department
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:RadioButtonList ID="RbdDepartment" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                    ClientIDMode="Static">
                                    <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="DepartmentHdn" runat="server" ClientIDMode="Static" />
                </td>
                <td style="width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                    border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                    border-bottom: lightsteelblue thin solid; font-weight: bold;display:none">
                    <table style="height: 75px;" id="TABLE1">
                        <tr>
                            <td>
                                &nbsp;Category
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:RadioButtonList ID="RbdCategory" runat="server" CssClass="radiobutton" RepeatLayout="Flow"
                                    ClientIDMode="Static">
                                    <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="ShiftHdn" runat="server" ClientIDMode="Static" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PnlFilterListBox" runat="server">
        <div id="detail" style="display: none">
            <table style="width: 70%; margin-left: 15%;">
                <tr>
                    <td>
                        <asp:TextBox ID="txtSearchEmp" runat="server" Style="display: none" ClientIDMode="Static"
                            Width="250px"></asp:TextBox>
                        <asp:TextBox ID="txtCompany" runat="server" placeholder="Search" Style="display: none"
                            Width="250px"></asp:TextBox>
                        <asp:TextBox ID="txtLocation" runat="server" placeholder="Search" Style="display: none"
                            Width="250px"></asp:TextBox>
                        <asp:TextBox ID="txtDivision" runat="server" placeholder="Search" Style="display: none"
                            Width="250px"></asp:TextBox>
                        <asp:TextBox ID="txtDepartment" runat="server" placeholder="Search" Style="display: none"
                            Width="250px"></asp:TextBox>
                        <asp:TextBox ID="txtShift" runat="server" placeholder="Search" Style="display: none"
                            Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAvailableData" runat="server" Text="Available Data"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblSeletcedData" runat="server" Text="Selected Data"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox ID="LstEmployee" runat="server" ClientIDMode="Static" Style="display: none"
                            Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                            Width="280px" Height="119px"></asp:ListBox>
                        <asp:ListBox ID="LstCompany" runat="server" ClientIDMode="Static" Style="display: none"
                            Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                            Width="250px" Height="119px"></asp:ListBox>
                        <asp:ListBox ID="LstLocation" runat="server" ClientIDMode="Static" Style="display: none"
                            Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                            Width="250px" Height="119px"></asp:ListBox>
                        <asp:ListBox ID="LstDivision" runat="server" ClientIDMode="Static" Style="display: none"
                            Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                            Width="250px" Height="119px"></asp:ListBox>
                        <asp:ListBox ID="LstDepartment" runat="server" ClientIDMode="Static" Style="display: none"
                            Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                            Width="250px" Height="119px"></asp:ListBox>
                        <asp:ListBox ID="LstCategory" runat="server" ClientIDMode="Static" Style="display: none"
                            Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                            Width="250px" Height="119px"></asp:ListBox>
                        <asp:ListBox ID="lstEmployDummy" runat="server" ClientIDMode="Static" Style="display: none"
                            Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                            Width="80%" Height="119px"></asp:ListBox>
                        <asp:ListBox ID="LstCompanyDummy" runat="server" ClientIDMode="Static" Style="display: none"
                            Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                            Width="250px" Height="119px"></asp:ListBox>
                        <asp:ListBox ID="LstLocationDummy" runat="server" ClientIDMode="Static" Style="display: none"
                            Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                            Width="250px" Height="119px"></asp:ListBox>
                        <asp:ListBox ID="LstDivisionDummy" runat="server" ClientIDMode="Static" Style="display: none"
                            Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                            Width="250px" Height="119px"></asp:ListBox>
                        <asp:ListBox ID="LstDepartmentDummy" runat="server" ClientIDMode="Static" Style="display: none"
                            Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                            Width="250px" Height="119px"></asp:ListBox>
                        <asp:ListBox ID="LstCategoryDummy" runat="server" ClientIDMode="Static" Style="display: none"
                            Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                            Width="250px" Height="119px"></asp:ListBox>
                    </td>
                    <td id="Btntd" clientidmode="Static" style="display: none;" runat="server">
                        <table style="height: 100px">
                            <tr>
                                <td align="left">
                                    <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                    <input id="cmdEntityAllRight" class="ButtonControl" runat="server" value="&gt;&gt;"
                                        style="width: 50px" type="button" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                    <input id="Button1" class="ButtonControl" value="&gt;" runat="server" style="width: 50px"
                                        type="button" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                    <input id="Button3" class="ButtonControl" value="&lt;" runat="server" style="width: 50px"
                                        type="button" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                    <input id="Button2" class="ButtonControl" value="&lt;&lt;" runat="server" style="width: 50px"
                                        type="button" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:ListBox ID="LstEntitySelected" runat="server" ClientIDMode="Static" Style="display: none"
                            Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                            Width="280px" Height="119px"></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input id="btnOK" class="ButtonControl" style="display: none; width: 50px" name="Ok"
                            type="button" value="OK" runat="server" />
                        <input id="btnClose" class="ButtonControl" style="display: none; width: 50px" name="Close"
                            type="button" value="Close" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlConfigPart" runat="server" HorizontalAlign="Center">
        <table id="Table4" runat="server" class="tableclass" width="70%" style="margin-left: 15%;">
            <tr style="display: none;">
                <td class="style37">
                    <asp:Label ID="lblMinimumswipeT" runat="server" Text="Use Count :"></asp:Label><label
                        class="CompulsaryLabel">*</label>
                    <br />
                </td>
                <td class="TDClassControl">
                    <%--<asp:TextBox ID="txtshiftcode" Width="50px" class="TextControl" runat="server" ontextchanged="txtshiftcode_TextChanged"></asp:TextBox>--%>
                    <asp:TextBox ID="Card_Cd" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        Height="16px" MaxLength="8" Style="display: none;" onkeypress="return IsAlphanumericWithoutspace(event)"
                        TabIndex="2" Enabled="False"></asp:TextBox>
                    <asp:TextBox ID="txtMinimumSwipe" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="2" onkeypress="return IsNumericSwipe(event)" Width="117px"></asp:TextBox>
                </td>
                <td class="style38">
                    <asp:Label ID="Label1" runat="server" Text="Pin :"></asp:Label>
                   <label class="CompulsaryLabel">*</label> 
                </td>
                <td class="TDClassControl">
                    <asp:TextBox ID="Pin" runat="server" onkeypress="return IsNumber(event)" CssClass="TextControl"
                        MaxLength="6" TabIndex="3" ClientIDMode="Static"></asp:TextBox>
                    <%-- <asp:RequiredFieldValidator ID="rfvtxtDescriptionAdd" runat="server" ErrorMessage="Please Enter PIN here"
                                ControlToValidate="Pin" Display="None" ValidationGroup="add"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtDescriptionAdd" runat="server"
                                TargetControlID="rfvtxtDescriptionAdd" PopupPosition="Right">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                </td>
            </tr>
            <tr  style="display: none;">
                <td class="style37">
                    Activation Date :
                    <label class="CompulsaryLabel">
                        *</label>
                </td>
                <td class="TDClassControl">
                    <asp:TextBox ID="Act_Date" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="10" onkeypress="return false" onkeydown="return false" TabIndex="8"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="cat01" runat="server" Format="dd/MM/yyyy" PopupButtonID="Act_Date"
                        TargetControlID="Act_Date">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="style38">
                    Expiry Date :<label class="CompulsaryLabel">*</label>
                </td>
                <td class="TDClassControl">
                    <asp:TextBox ID="Exp_Date1" runat="server" ClientIDMode="Static" CssClass="TextControl"
                        MaxLength="10" onkeydown="date_dash(this,event)" onkeypress="return IsNumber(event)"
                        onkeyup="date_dash(this,event)" TabIndex="9"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="Exp_Date1" TargetControlID="Exp_Date1">
                    </ajaxToolkit:CalendarExtender>
                    <asp:CompareValidator ID="CompareValidator2" ValidationGroup="Date" ForeColor="Red" Display="None"
                                        runat="server" ControlToValidate="Exp_Date1" ControlToCompare="Act_Date"
                                        Operator="GreaterThan" Type="Date" ErrorMessage="Activation Date must be less than Expiry Date."></asp:CompareValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtExpiryDate" runat="server" TargetControlID="CompareValidator2"
                                                    PopupPosition="Right">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                
                </td>
            </tr>
            <tr style="display: none;">
                <td class="style37">
                    <asp:Label ID="Label3" runat="server" Text="Status"></asp:Label>
                </td>
                <td class="TDClassControl">
                    <asp:CheckBox ID="Chk_Sts" runat="server" Checked="true" ClientIDMode="Static" TabIndex="7" />
                </td>
                <td class="style38">
                </td>
                <td class="TDClassControl">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style37">
                    &nbsp;<asp:CheckBox ID="Chk_IgrAPB" runat="server" Checked="true" Style="display: none;"
                        ClientIDMode="Static" TabIndex="5" />
                </td>
                <td class="TDClassControl">
                    <%--<input type="text" class= "TDClassControl"  onkeypress="return IsNumber(event)"  id = "Act_Date" Visible = "true" runat="server" onkeyup="date_dash(this,event)" onkeydown="date_dash(this,event)" maxlength="10" clientidmode="Static" />--%>
                    <asp:CheckBox ID="Chk_SetAPB" runat="server" Checked="true" Style="display: none;"
                        ClientIDMode="Static" TabIndex="6" />
                </td>
                <td class="style38">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table id="table3" runat="server" cellpadding="3" cellspacing="3" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="CmdOk" runat="server" CssClass="ButtonControl" Text="Save" TabIndex="8"
                    OnClick="CmdOk_Click" />
                <asp:Button ID="btnReset" runat="server" CssClass="ButtonControl" CausesValidation="true"
                    Text="Reset" TabIndex="8" OnClientClick="return resetAll();" />
                <asp:Button ID="CmdCancel" runat="server" Text="Cancel" CssClass="ButtonControl"
                    TabIndex="9" CausesValidation="False" OnClick="CmdCancel_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblMsg" ClientIDMode="Static" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
