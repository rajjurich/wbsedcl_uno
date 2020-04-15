﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="VEH_RFID_Inventory.aspx.cs" Inherits="UNO.VEH_RFID_Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var interval;
        var obj = new ActiveXObject("VehicleManagementLib.Class1");
        //alert(obj);
        var flag = false;
        var errmsgflag = false;
        function startRead() {
            try {
               // alert(obj);
                obj.getData();
              interval= setInterval(function () {    
                    try {

                        if (flag == false) {
                            var rfid = obj.Inventory();
                            if (rfid != "") {
                                alert(rfid);
                                var ListBox1 = document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_LstRFIDList');
                                var newOption = window.document.createElement('OPTION');
                                //alert(newOption);
                                newOption.text = rfid;
                                ListBox1.options.add(newOption);
                                flag = true;
                            }
                        }
                        else {
                            var flagExists = false;
                            var rfid = obj.Inventory();
                            var ListBox1 = document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_LstRFIDList');
                            if (rfid != "") {
                                //alert(rfid);
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
        }
        function dispose1() {
        clearInterval(interval);
            //CollectGarbage();
    }

    function set_ErrorLabel() {

                    if (errmsgflag == true) {
                       // alert(errmsgflag);
                        document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_lblMsg').innerHTML = "Records Saved Successflly";
                    }
    }
        function saveRFID() {
            var objList = new Array();
           $("#ContentPlaceHolder1_ContentPlaceHolder1_LstRFIDList option").each(function () {
               objList.push($(this).text()); // this line push all text in array


            });
           //alert(objList);
            if (objList.length != 0) {

                $.ajax({
                    type: "POST",
                    url: "VEH_RFID_Inventory.aspx/SaveDate",
                    data: "{'data':'" + objList + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // var lblmsg = document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_lblMsg');
                        //  alert(msg.d);
                        if (msg.d == 'succses') {
                            errmsgflag = true;
                            set_ErrorLabel();
                            // alert(errmsgflag);
                        }

                    },

                    error: function () { alert(arguments[2]); }
                });

            }
            else {

                document.getElementById('ContentPlaceHolder1_ContentPlaceHolder1_lblMsg').innerHTML = "Records Tag Record Found";
            }
            //alert(errmsgflag);
            //set_ErrorLabel();
            clearInterval(interval);
        }
       

    </script>
    <style type="text/css">
        .style37
        {
            width: 266px;
        }
        .style38
        {
            width: 225px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                        <asp:Label ID="lblHead" runat="server" Text="RFID Inventory" ForeColor="RoyalBlue"
                            Font-Size="20px" Width="100%" CssClass="heading">
                        </asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <table style="width: 100%;">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td class="style38">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        &nbsp; &nbsp;
                        <asp:ListBox ID="LstRFIDList" runat="server" BackColor="#CCCCCC" Height="154px" Width="310px">
                        </asp:ListBox>
                        &nbsp;
                    </td>
                </tr>

                <tr>
                    <td align="center" colspan="3">
                        &nbsp; &nbsp;
                        <asp:Button ID="Button1" runat="server" class="ButtonControl" OnClientClick="startRead()"
                            Text="Start Reading" />
                        <asp:Button ID="Button2" runat="server" class="ButtonControl" 
                            Text="Stop Reading" OnClientClick="dispose1()" Visible="False" />
                        <asp:Button ID="btnsave" runat="server" class="ButtonControl" Text="Save"  OnClientClick="saveRFID()"/>
                        &nbsp;
                    </td>
                </tr>
            </table>
               <div style="width:100%;height:2%;" align="center">
    <table>
    <tr>
            <td>
             <asp:Label ID="lblMsg" runat="server" style="color:Red;"></asp:Label>
            </td>
            </tr>
    </table>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
