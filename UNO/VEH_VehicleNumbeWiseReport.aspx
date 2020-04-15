<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="VEH_VehicleNumbeWiseReport.aspx.cs" Inherits="UNO.VEH_VehicleNumbeWiseReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style28
        {
            width: 90%;
        }
    </style>
    <%--<script type="text/javascript">

    function AllSelectedListBox() {
           var LstEmployee = document.getElementById('LstEmployee');
            var LstEntitySelected = document.getElementById('LstEntitySelected');
   for (var i = 0; i <= LstEmployee.options.length - 1; i++) {
            var newOption = window.document.createElement('OPTION');
            newOption.text = LstEmployee.options[i].text;
            newOption.value = LstEmployee.options[i].value;

            //                alert(LstEmployee.options[i].text);
            LstEntitySelected.options.add(newOption);


        }
        //            $("#LstEmployee").empty();
        LstEmployee.options.length = 0;
    }
    function removeEntitySelectedListBox() {

        var LstEmployee = document.getElementById('LstEmployee');
        var LstEntitySelected = document.getElementById('LstEntitySelected');


        for (var i = 0; i <= LstEntitySelected.options.length - 1; i++) {
            var newOption = window.document.createElement('OPTION');
            newOption.text = LstEntitySelected.options[i].text;
            newOption.value = LstEntitySelected.options[i].value;


            LstEmployee.options.add(newOption);


        }

        LstEntitySelected.options.length = 0;
    }
   
    function ReturnFillEntityAvailable() {
            var LstEmployee = document.getElementById('LstEmployee');
            var LstEntitySelected = document.getElementById('LstEntitySelected');

            for (var i = LstEntitySelected.options.length - 1; i >= 0; i--) {
                if (LstEntitySelected.options[i].selected) {
                    var newOption = window.document.createElement('OPTION');
                    newOption.text = LstEntitySelected.options[i].text;
                    newOption.value = LstEntitySelected.options[i].value;

                    LstEntitySelected.options.remove(i);
                    LstEmployee.options.add(newOption);

                }

            }
        }
     function FillEntitySeletedListBox() {

              
              
                var LstEmployee = document.getElementById('LstEmployee');
                var LstEntitySelected = document.getElementById('LstEntitySelected');

                for (var i = LstEmployee.options.length - 1; i >= 0; i--) {
                    if (LstEmployee.options[i].selected) {

                        var newOption = window.document.createElement('OPTION');

                        newOption.text = LstEmployee.options[i].text;
                        newOption.value = LstEmployee.options[i].value;
                        // alert(LstEmployee.options[i].text);
                        LstEmployee.remove(i);
                        //LstEmployee.options.remove(i);
                        LstEntitySelected.options.add(newOption);


                    }
                }
            }
</script>--%>
    <%--<script type="text/javascript">
 var myVals = new Array();
    function CacheValues() {
        var l = document.getElementById('LstEmployee');

        for (var i = 0; i < l.options.length; i++) {
            myVals[i] = l.options[i].text;
        }
    }

    function SearchList() {
        var l = document.getElementById('LstEmployee');
        var tb = document.getElementById('TextBox1');

        l.options.length = 0;

        if (tb.value == "") {
            for (var i = 0; i < myVals.length; i++) {
                l.options[l.options.length] = new Option(myVals[i]);
            }
        }
        else {

            for (var i = 0; i < myVals.length; i++) {
                if (myVals[i].toLowerCase().indexOf(tb.value.toLowerCase()) != -1) {
                    l.options[l.options.length] = new Option(myVals[i]);
                }
                else {
                    // do nothing
                }
            }
        }
    }
    function ClearSelection(lb) {
        lb.selectedIndex = -1;
    }

</script>--%>
    <script type="Text/JavaScript">
        function checkListBox(sender, args) {
            //alert("ss");
            var listBox = document.getElementById(sender.controltovalidate);


            args.IsValid = (listBox.options.length > 0)
        }
    </script>
    <script type="text/javascript">
        var myValsEmp = new Array();
        function CacheValues() {



            var l = document.getElementById('<%=LstEmployee.ClientID%>');
            for (var i = 0; i < l.options.length; i++) {
                myValsEmp[i] = l.options[i].text;
            }

        }

        function SearchList() {

            var l = null;
            myVals = null;

            //if (document.getElementById('LstEmployee').style.display == "inline") {
            l = document.getElementById('LstEmployee');
            myVals = myValsEmp;
            //}

            //alert("InSearch");

            //alert(myVals.length);

            var tb = document.getElementById('TextBox1');

            l.options.length = 0;



            if (tb.value == "") {
                for (var i = 0; i < myVals.length; i++) {
                    l.options[l.options.length] = new Option(myVals[i]);
                }
            }
            else {

                for (var i = 0; i < myVals.length; i++) {
                    if (myVals[i].toLowerCase().indexOf(tb.value.toLowerCase()) != -1) {
                        l.options[l.options.length] = new Option(myVals[i]);
                    }
                    else {
                        // do nothing
                    }
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                        <asp:Label ID="lblHead" runat="server" Text="Report Based On Vehicle Numbers" ForeColor="RoyalBlue"
                            Font-Size="20px" Width="100%" CssClass="heading">
                        </asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <table style="margin-left: 24%;">
                <tr>
                    <td>
                        <%-- <asp:TextBox ID="txtSerch" runat="server" placeholder="Serarch Vehicle Number" style="width:100%;"></asp:TextBox>
                        --%>
                        <asp:TextBox ID="TextBox1" ClientIDMode="Static" runat="server" onkeyup="return SearchList();"
                            placeholder="Search Vehicle Number" Style="width: 100%;"></asp:TextBox>
                        <br />
                        <asp:ListBox ID="LstEmployee" runat="server" ClientIDMode="Static" Font-Bold="True"
                            Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                            Width="250px" Height="92px"></asp:ListBox>
                    </td>
                    <td id="Btntd" class="TDClassControl" clientidmode="Static" style="width: 9%;">
                        <table style="height: 82px">
                            <tr>
                                <td class="TDClassControl">
                                    <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                    <%--<input id="cmdEntityAllRight"  onclick="AllSelectedListBox()"  value="&gt;&gt;"   style="width:28px" type="button"  />--%>
                                    <asp:Button ID="btnMoveAll" runat="server" Text=">>" Style="width: 28px" OnClick="btnMoveAll_Click" />
                                </td>
                            </tr>
                            <caption>
                                <tr>
                                    <td class="TDClassControl">
                                        <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                        <%--<input id="Button1"  value="&gt;"  onclick="FillEntitySeletedListBox()" style="width:28px" type="button"  />--%>
                                        <asp:Button ID="btnMoveSingle" runat="server" Text=">" Style="width: 28px" OnClick="btnMoveSingle_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDClassControl">
                                        <%--<input id="Button3"  value="&lt;"  onclick="ReturnFillEntityAvailable()"  style="width:28px" type="button"  />--%>
                                        <asp:Button ID="btnReturnSingle" runat="server" Text="<" Style="width: 28px" OnClick="btnReturnSingle_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDClassControl">
                                        <%--   <asp:ListItem Value="2" >Enter Code</asp:ListItem>--%>
                                        <%--<input id="Button2"  value="&t;l&lt;" onclick="removeEntitySelectedListBox()"   style="width:28px" type="button"  />--%>
                                        <asp:Button ID="btnReturnAll" runat="server" Text="<<" Style="width: 28px" OnClick="btnReturnAll_Click" />
                                    </td>
                                </tr>
                            </caption>
                        </table>
                    </td>
                    <td>
                        <asp:ListBox ID="LstEntitySelected" runat="server" ClientIDMode="Static" Font-Bold="True"
                            Font-Names="Courier New" ForeColor="Black" Rows="10" SelectionMode="Multiple"
                            Width="250px" Height="113px"></asp:ListBox>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="LstEntitySelected"
                            Display="None" ForeColor="Red" SetFocusOnError="True" ValidationGroup="validateofficial"
                            ErrorMessage="Please select atleast one vehicle" ClientValidationFunction="checkListBox"
                            ValidateEmptyText="True" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                            TargetControlID="CustomValidator1" Enabled="True">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
            </table>
            <table id="Table4" runat="server" class="tableclass" style="margin-left: 10%; width: 76%;">
                <tr>
                    <td colspan="4" class="TDClassControl" style="text-align: center; padding-right: 44px;">
                        <%-- <input id="btnOK" class="ButtonControl" style=" margin-left:4%;" name="Ok"   type="button" value="OK" />
 <input id="Button4" class="ButtonControl" style="width:12%;" name="Close"   type="button" value="Close" />--%>
                        <asp:Button ID="btnOK" class="ButtonControl" runat="server" Style="margin-left: 8%;"
                            Text="Generate Report" OnClick="btnOK_Click" ValidationGroup="validateofficial" />
                        <asp:Button ID="btnClose" class="ButtonControl" runat="server" Text="Reset" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnMoveAll" />
            <asp:AsyncPostBackTrigger ControlID="btnMoveSingle" />
            <asp:AsyncPostBackTrigger ControlID="btnReturnSingle" />
            <asp:AsyncPostBackTrigger ControlID="btnReturnAll" />
            <asp:AsyncPostBackTrigger ControlID="btnOK" />
        </Triggers>
    </asp:UpdatePanel>
    <table align="center" style="width: 90%; margin-left: 10%;">
        <tr>
            <td style="text-align: right" class="style28">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Font-Names="Verdana"
                    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                    WaitMessageFont-Size="14pt" BorderStyle="Solid">
                </rsweb:ReportViewer>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style28" align="center">
                <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
