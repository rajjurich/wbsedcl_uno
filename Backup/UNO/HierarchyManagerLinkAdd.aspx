<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="True"
    EnableEventValidation="false" CodeBehind="HierarchyManagerLinkAdd.aspx.cs" Inherits="UNO.HierarchyManagerLinkAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Scripts/Choosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
        .style37
        {
            height: 15px;
        }
    </style>
    <script language="javascript" type="text/javascript" src="Scripts/Validation.js"></script>
    <script src="Scripts/Jquery.min.1.8.2.js" type="text/javascript"></script>
    <script src="Scripts/1.8.23-jquery-ui.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        //Added by Pooja Yadav
        function Validation() {

            var first = "";
            var count = 0;

            if (document.getElementById("<%=lstManager.ClientID%>").selectedIndex == -1) {
                document.getElementById('<%=lblMessages.ClientID%>').innerHTML = 'Please select the Reporting Person';
                return false;
            }
            var first = "";
            var count = 0;
            var emp = document.getElementById('<%=EmployeeHdn.ClientID%>').value;
            var com = document.getElementById('<%=ComapnyHdn.ClientID%>').value;
            var loc = document.getElementById('<%=LocationHdn.ClientID%>').value;
            var div = document.getElementById('<%=DivisionHdn.ClientID%>').value;
            var dep = document.getElementById('<%=DepartmentHdn.ClientID%>').value;
            if (emp == "" && com == "" && loc == "" && div == "" && dep == "") {

                var msg = confirm("No Entity Selected,Are you Sure To continue?");
                if (msg == false) {
                    return false;
                }
            }
            return true;


        }
        //Added by Pooja Yadav
        function Reset() {
            $("#<%=RbdEmp.ClientID %>,#<%=RbdCompany.ClientID %>,#<%=RbdLocation.ClientID %>,#<%=RbdDivision.ClientID %>,#<%=RbdDepartment.ClientID %>").find('input').prop('disabled', false);
            $("#RbdEmp_0,#RbdCompany_0,#RbdLocation_0,#RbdDivision_0,#RbdDepartment_0").prop("checked", true);
            $("#EmployeeHdn,#ComapnyHdn,#LocationHdn,#DivisionHdn,#DepartmentHdn,#ShiftHdn").prop("value", "");
            document.getElementById('<%=EmployeeHdn.ClientID%>').value = "";
            document.getElementById('<%=ComapnyHdn.ClientID%>').value = "";
            document.getElementById('<%=LocationHdn.ClientID%>').value = "";
            document.getElementById('<%=DivisionHdn.ClientID%>').value = "";
            document.getElementById('<%=DepartmentHdn.ClientID%>').value = "";
            document.getElementById('<%=btnSubmitAdd.ClientID%>').disabled = false;
            document.getElementById('<%=lblMessages.ClientID%>').innerHTML = "";
            //return false;
        }

        $(document).ready(function () {

            $("#<%=RbdEmp.ClientID %> input").change(function () {
                if ($(this).val() == "0") {
                    $("#detail").css("display", "none");
                }
            });

            $("#<%=RbdCompany.ClientID %> input").change(function () {
                if ($(this).val() == "0") {
                    $("#detail").css("display", "none");
                }
            });

            $("#<%=RbdLocation.ClientID %> input").change(function () {
                if ($(this).val() == "0") {
                    $("#detail").css("display", "none");
                }
            });

            $("#<%=RbdDivision.ClientID %> input").change(function () {
                if ($(this).val() == "0") {
                    $("#detail").css("display", "none");
                }
            });

            $("#<%=RbdDepartment.ClientID %> input").change(function () {
                if ($(this).val() == "0") {
                    $("#detail").css("display", "none");
                }
            });
            
        });





        //        var prm = Sys.WebForms.PageRequestManager.getInstance();

        //        prm.add_endRequest(function () {
        //            $("#<%=RbdEmp.ClientID %>").click(function () {
        //                alert('clicked me');
        //            });
        //        });

        function ToggleSelectionOption() {
            alert('hello');
        }

      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Employee Manager Hierarchy Add" ForeColor="RoyalBlue"
                    Font-Size="20px" Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div>
        <table style="margin-left: 35%;">
            <tr>
                <td>
                    <asp:TextBox ID="TextBox1" onkeyup="return SearchList();" runat="server" Height="23px"
                        Style="display: none;" ClientIDMode="Static" Width="157px"></asp:TextBox>
                    <br />
                    Reporting Person :
                    <asp:ListBox ID="lstManager" runat="server" ClientIDMode="Static" Font-Bold="True"
                        class="chosen-select" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Single"
                        Width="250px" Height="45px" AutoPostBack="True" OnSelectedIndexChanged="lstManager_SelectedIndexChanged">
                    </asp:ListBox>
                </td>
            </tr>
        </table>
    </div>
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
                        <td style="text-align: left">
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
                width: 7%">
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
                border-bottom: lightsteelblue thin solid; font-weight: bold;">
                <table style="height: 75px;">
                    <tr>
                        <td>
                            Location
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
                border-bottom: lightsteelblue thin solid; font-weight: bold;">
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
                                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                <asp:ListItem Value="1">Select</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="DivisionHdn" runat="server" ClientIDMode="Static" />
            </td>
            <td style="width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                border-bottom: lightsteelblue thin solid; font-weight: bold;">
                <table style="height: 75px;" id="TABLE2">
                    <tr>
                        <td class="style37">
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
            <td style="display: none; width: 7%; height: 75px; text-align: left; border-right: lightsteelblue thin solid;
                border-top: lightsteelblue thin solid; border-left: lightsteelblue thin solid;
                border-bottom: lightsteelblue thin solid; font-weight: bold;">
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
                                <asp:ListItem Value="1" onclick="showCategory()">Select</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="ShiftHdn" runat="server" ClientIDMode="Static" />
            </td>
        </tr>
    </table>
    <div id="detail" style="display: none">
        <table style="width: 60%; margin-left: 15%;">
            <tr>
                <td>
                    <asp:TextBox ID="txtSearchEmp" runat="server" placeholder="Search" Style="display: none"
                        ClientIDMode="Static" Width="250px"></asp:TextBox>
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
                        Width="250px" Height="119px"></asp:ListBox>
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
                        Width="250px" Height="119px"></asp:ListBox>
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
                </td>
                <td class="TDClassControl" id="Btntd" clientidmode="Static" style="width: 14%; display: none;"
                    runat="server">
                    <table style="height: 100px">
                        <tr>
                            <td class="TDClassControl">
                                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                <input id="cmdEntityAllRight" class="ButtonControl" runat="server" value="&gt;&gt;"
                                    style="width: 38px" type="button" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TDClassControl">
                                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                <input id="Button1" class="ButtonControl" value="&gt;" runat="server" style="width: 38px"
                                    type="button" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TDClassControl">
                                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                <input id="Button3" class="ButtonControl" value="&lt;" runat="server" style="width: 38px"
                                    type="button" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TDClassControl">
                                <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                <input id="Button2" class="ButtonControl" value="&lt;&lt;" runat="server" style="width: 38px"
                                    type="button" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:ListBox ID="LstEntitySelected" runat="server" ClientIDMode="Static" Style="display: none"
                        Font-Bold="True" Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                        Width="250px" Height="119px"></asp:ListBox>
                </td>
            </tr>
        </table>
    </div>
    <table id="Table4" runat="server" class="tableclass" style="margin-left: 10%; width: 76%;">
        <tr>
            <td colspan="4" class="TDClassControl" style="text-align: center; padding-right: 44px;">
                <asp:Button ID="btnOK" runat="server" class="ButtonControl" Text="Ok" OnClick="btnOK_Click"
                    Style="display: none;" />
                <input id="Button4" class="ButtonControl" name="Close" type="button" value="Close"
                    runat="server" style="display: none;" />
            </td>
        </tr>
    </table>
    <div class="DivEmpDetails">
        <table style="width: 100%;">
            <tr>
                <td style="background-color: #EFF8FE; padding: 0px;" colspan="3">
                    <div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
                        <img src="images/icon-loading-animated.gif" alt="Loading ...." />
                    </div>
                    <asp:UpdatePanel ID="upnGrid" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvEmpMgrAdd" runat="server" AutoGenerateColumns="false" Width="100%"
                                GridLines="None" AllowPaging="true" PageSize="10">
                                <RowStyle CssClass="gvRow" />
                                <HeaderStyle CssClass="gvHeader" />
                                <AlternatingRowStyle BackColor="#F0F0F0" />
                                <PagerStyle CssClass="gvPager" />
                                <EmptyDataTemplate>
                                    <div>
                                        <span>No records found.</span>
                                    </div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="EOD_EMPID" HeaderText="Employee ID"></asp:BoundField>
                                    <asp:BoundField DataField="Empname" HeaderText="Employee Name"></asp:BoundField>
                                    <asp:BoundField DataField="EOD_DIVISION_ID" HeaderText="Division"></asp:BoundField>
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
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnOK" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <table style="width: 100%;">
        <tr>
            <td align="center" colspan="2">
                &nbsp;
                <asp:Button ID="Button5" runat="server" Style="display: none;" Text="test" />
                <asp:Button ID="btnSubmitAdd" runat="server" CssClass="ButtonControl" OnClick="btnSubmitAdd_Click"
                    Text="Save" OnClientClick="return Validation();" />
                <asp:Button ID="btnReset" runat="server" CssClass="ButtonControl" Text="Reset" OnClientClick="return Reset();" />
                <asp:Button ID="btnCancelAdd" runat="server" CssClass="ButtonControl" Text="Cancel"
                    OnClick="btnCancelAdd_Click" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblMessages" runat="server" Text="" Visible="true" CssClass="ErrorLabel"></asp:Label>
            </td>
        </tr>
    </table>
    <%--  </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lstManager" />
            <asp:PostBackTrigger ControlID="btnOK" />
            <asp:PostBackTrigger ControlID="btnSubmitAdd" />
        </Triggers>
    </asp:UpdatePanel>--%>
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
