<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ReadCSNR.aspx.cs" Inherits="UNO.ReadCSNR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="JavaScript" type="text/javascript">
        var objReadCard; var DataInitialize; var cardCSNR = "";
        objReadCard = new ActiveXObject("ContactlessCardRW.Card");
        $("#btnSave").hide();
        $(function () {
            $('#btnStart').click(function () {
            alert("grfsgs");
                ReadCSNR();
                $("#btnSave").show();
            });

            $('#btnSave').click(function () {
                debugger;
                var varCsnr = "";
                $("#lstCSNR option").each(function (i) {
                    if (varCsnr == "") {
                        varCsnr = $(this).text();
                    } else {
                        varCsnr = varCsnr + ',' + $(this).text();
                    }
                });

            var objList = new Array();
            $("#lstCSNR option").each(function () {
                objList.push($(this).text()); // this line push all text in array
            });

                if (varCsnr == "") {
                    alert("Please Read Card");
                }
                else {
                    $.ajax({
                        url: "ReadCSNR.aspx/SaveCSNR",
                        type: "POST",
                        data: "{'data':'" + objList + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        success: function (msg) {
                            //alert(msg.d);
                        },
                        error: function () { alert(arguments[2]); }
                    });
                }

            });
        });
        function ReadCSNR() {
            try {

                DataInitialize = objReadCard.Initialise();
                alert(DataInitialize);
                if (DataInitialize != "") {
                    alert("Omnikey reader not connected or Error in card Initialization");
                    return false;
                }
                var data;
                data = objReadCard.ConnectToCard();
                alert(data);
                if (data != "") {
                    alert("Error in connecting to card");
                    return false;
                }

                cardCSNR = objReadCard.CSNR.replace(/ /g, "").substring(0, 8);
                cardCSNR = ReverseCSNR(cardCSNR);
                // alert(cardCSNR);
                if ($("#lstCSNR option:contains('" + cardCSNR + "')").length > 0) {
                    alert("Already exists");
                }
                else {
                    $("#lstCSNR").append($('<option></option>').text(cardCSNR));
                }
                var count = $('#lstCSNR option').size();

                $("#lblCurrentCount").html(count);

            }
            catch (e) {
                alert(e.Message);

            }
        }




        function ReverseCSNR(csnr) {
            try {
                var temp = "";
                for (var i = 0; i < 4; i++) {
                    //temp = temp + csnr[csnr.length - 2] + csnr[csnr.length - 1];
                    temp = temp + csnr.substr(csnr.length - 2, 1) + csnr.substr(csnr.length - 1, 1);
                    csnr = csnr.substring(0, csnr.length - 2);
                }
                return temp;
            }
            catch (ex) {
                alert(ex.Message);
            }
        }
     

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-top: 2%; width: 100%">
        <center>
            <div style="font-weight: bold">
                <span style="padding-right: 1%">Total CSNR Count :</span>
                <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
            </div>
            <div style="margin-bottom: 1%; font-weight: bold;">
                <span style="padding-right: 1%">Current CSNR Count :</span> <span id="lblCurrentCount">
                    0</span>
            </div>
            <table width="50%">
                <tr>
                    <td>
                        Card Inventory
                    </td>
                    <td>
                        Current Captured CSNR
                    </td>
                    <td>
                        Card Issued
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox ID="lstInventory" ClientIDMode="Static" runat="server" Width="170px"
                            Height="250px"></asp:ListBox>
                          
                    </td>
                    <td >
                        <asp:ListBox ID="lstCSNR" ClientIDMode="Static" runat="server" Width="170px" Height="250px">
                        </asp:ListBox>
                        
                    </td>
                    <td >
                        <asp:ListBox ID="lstIshued" ClientIDMode="Static" runat="server" Width="250px" Height="250px">
                        </asp:ListBox>
                        
                    </td>
                </tr>
            </table>
            <div style="margin-top: 1%;">
                <input type="button" id="btnStart" value="Read" class="ButtonControl"/>
                <asp:Button ID="btnSave" runat="server" Text="Save" class="ButtonControl" ClientIDMode="Static">
                </asp:Button>
            </div>
        </center>
    </div>
</asp:Content>
