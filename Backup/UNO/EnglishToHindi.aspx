<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="EnglishToHindi.aspx.cs" Inherits="UNO.EnglishToHindi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--  <script src="Scripts/keyboard.js" type="text/javascript"></script>--%>
    <link href="Styles/Keyboard.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/jscript">
        $(document).ready(function ready() {
            $("#txtId").click(function () {
                $('#txtId_awTable').show();
                $('#txtEmpName_awTable').hide();
                $('#txtSalutation_awTable').hide();
                $('#txtJoinDate_awTable').hide();
                $('#txtCompanyName_awTable').hide();
                $('#txtLocName_awTable').hide();
                $('#txtDiviName_awTable').hide();
                $('#txtDepaName_awTable').hide();
                $('#txtDesigName_awTable').hide();
                $('#txtCateName_awTable').hide();
                $('#txtGroupName_awTable').hide();
                $('#txtGradeName_awTable').hide();
                $('#txtCardExDate_awTable').hide();
                $('#txtGender_awTable').hide();
                $('#txtBloodGrp_awTable').hide();
                $('#txtDOB_awTable').hide(); 
            });
            $("#txtEmpName").click(function () {
                $('#txtId_awTable').hide();
                $('#txtEmpName_awTable').show();
                $('#txtSalutation_awTable').hide();
                $('#txtJoinDate_awTable').hide();
                $('#txtCompanyName_awTable').hide();
                $('#txtLocName_awTable').hide();
                $('#txtDiviName_awTable').hide();
                $('#txtDepaName_awTable').hide();
                $('#txtDesigName_awTable').hide();
                $('#txtCateName_awTable').hide();
                $('#txtGroupName_awTable').hide();
                $('#txtGradeName_awTable').hide();
                $('#txtCardExDate_awTable').hide();
                $('#txtGender_awTable').hide();
                $('#txtBloodGrp_awTable').hide();
                $('#txtDOB_awTable').hide(); 
            });
            $("#txtSalutation").click(function () {
                $('#txtId_awTable').hide();
                $('#txtEmpName_awTable').hide();
                $('#txtSalutation_awTable').show();
                $('#txtJoinDate_awTable').hide();
                $('#txtCompanyName_awTable').hide();
                $('#txtLocName_awTable').hide();
                $('#txtDiviName_awTable').hide();
                $('#txtDepaName_awTable').hide();
                $('#txtDesigName_awTable').hide();
                $('#txtCateName_awTable').hide();
                $('#txtGroupName_awTable').hide();
                $('#txtGradeName_awTable').hide();
                $('#txtCardExDate_awTable').hide();
                $('#txtGender_awTable').hide();
                $('#txtBloodGrp_awTable').hide();
                $('#txtDOB_awTable').hide(); 
            });
            $("#txtJoinDate").click(function () {
                $('#txtId_awTable').hide();
                $('#txtEmpName_awTable').hide();
                $('#txtSalutation_awTable').hide();
                $('#txtJoinDate_awTable').show();
                $('#txtCompanyName_awTable').hide();
                $('#txtLocName_awTable').hide();
                $('#txtDiviName_awTable').hide();
                $('#txtDepaName_awTable').hide();
                $('#txtDesigName_awTable').hide();
                $('#txtCateName_awTable').hide();
                $('#txtGroupName_awTable').hide();
                $('#txtGradeName_awTable').hide();
                $('#txtCardExDate_awTable').hide();
                $('#txtGender_awTable').hide();
                $('#txtBloodGrp_awTable').hide();
                $('#txtDOB_awTable').hide(); 
            });
            $("#txtCompanyName").click(function () {
                $('#txtId_awTable').hide();
                $('#txtEmpName_awTable').hide();
                $('#txtSalutation_awTable').hide();
                $('#txtJoinDate_awTable').hide();
                $('#txtCompanyName_awTable').show();
                $('#txtLocName_awTable').hide();
                $('#txtDiviName_awTable').hide();
                $('#txtDepaName_awTable').hide();
                $('#txtDesigName_awTable').hide();
                $('#txtCateName_awTable').hide();
                $('#txtGroupName_awTable').hide();
                $('#txtGradeName_awTable').hide();
                $('#txtCardExDate_awTable').hide();
                $('#txtGender_awTable').hide();
                $('#txtBloodGrp_awTable').hide();
                $('#txtDOB_awTable').hide(); 
            });
            $("#txtLocName").click(function () {
                $('#txtId_awTable').hide();
                $('#txtEmpName_awTable').hide();
                $('#txtSalutation_awTable').hide();
                $('#txtJoinDate_awTable').hide();
                $('#txtCompanyName_awTable').hide();
                $('#txtLocName_awTable').show();
                $('#txtDiviName_awTable').hide();
                $('#txtDepaName_awTable').hide();
                $('#txtDesigName_awTable').hide();
                $('#txtCateName_awTable').hide();
                $('#txtGroupName_awTable').hide();
                $('#txtGradeName_awTable').hide();
                $('#txtCardExDate_awTable').hide();
                $('#txtGender_awTable').hide();
                $('#txtBloodGrp_awTable').hide();
                $('#txtDOB_awTable').hide(); 
            });
            $("#txtDiviName").click(function () {
                $('#txtId_awTable').hide();
                $('#txtEmpName_awTable').hide();
                $('#txtSalutation_awTable').hide();
                $('#txtJoinDate_awTable').hide();
                $('#txtCompanyName_awTable').hide();
                $('#txtLocName_awTable').hide();
                $('#txtDiviName_awTable').show();
                $('#txtDepaName_awTable').hide();
                $('#txtDesigName_awTable').hide();
                $('#txtCateName_awTable').hide();
                $('#txtGroupName_awTable').hide();
                $('#txtGradeName_awTable').hide();
                $('#txtCardExDate_awTable').hide();
                $('#txtGender_awTable').hide();
                $('#txtBloodGrp_awTable').hide();
                $('#txtDOB_awTable').hide(); 
            });
            $("#txtDepaName").click(function () {
                $('#txtId_awTable').hide();
                $('#txtEmpName_awTable').hide();
                $('#txtSalutation_awTable').hide();
                $('#txtJoinDate_awTable').hide();
                $('#txtCompanyName_awTable').hide();
                $('#txtLocName_awTable').hide();
                $('#txtDiviName_awTable').hide();
                $('#txtDepaName_awTable').show();
                $('#txtDesigName_awTable').hide();
                $('#txtCateName_awTable').hide();
                $('#txtGroupName_awTable').hide();
                $('#txtGradeName_awTable').hide();
                $('#txtCardExDate_awTable').hide();
                $('#txtGender_awTable').hide();
                $('#txtBloodGrp_awTable').hide();
                $('#txtDOB_awTable').hide(); 
            });
            $("#txtDesigName").click(function () {
                $('#txtId_awTable').hide();
                $('#txtEmpName_awTable').hide();
                $('#txtSalutation_awTable').hide();
                $('#txtJoinDate_awTable').hide();
                $('#txtCompanyName_awTable').hide();
                $('#txtLocName_awTable').hide();
                $('#txtDiviName_awTable').hide();
                $('#txtDepaName_awTable').hide();
                $('#txtDesigName_awTable').show();
                $('#txtCateName_awTable').hide();
                $('#txtGroupName_awTable').hide();
                $('#txtGradeName_awTable').hide();
                $('#txtCardExDate_awTable').hide();
                $('#txtGender_awTable').hide();
                $('#txtBloodGrp_awTable').hide();
                $('#txtDOB_awTable').hide(); 
            });
            $("#txtCateName").click(function () {
                $('#txtId_awTable').hide();
                $('#txtEmpName_awTable').hide();
                $('#txtSalutation_awTable').hide();
                $('#txtJoinDate_awTable').hide();
                $('#txtCompanyName_awTable').hide();
                $('#txtLocName_awTable').hide();
                $('#txtDiviName_awTable').hide();
                $('#txtDepaName_awTable').hide();
                $('#txtDesigName_awTable').hide();
                $('#txtCateName_awTable').show();
                $('#txtGroupName_awTable').hide();
                $('#txtGradeName_awTable').hide();
                $('#txtCardExDate_awTable').hide();
                $('#txtGender_awTable').hide();
                $('#txtBloodGrp_awTable').hide();
                $('#txtDOB_awTable').hide(); 
            });
            $("#txtGroupName").click(function () {
                $('#txtId_awTable').hide();
                $('#txtEmpName_awTable').hide();
                $('#txtSalutation_awTable').hide();
                $('#txtJoinDate_awTable').hide();
                $('#txtCompanyName_awTable').hide();
                $('#txtLocName_awTable').hide();
                $('#txtDiviName_awTable').hide();
                $('#txtDepaName_awTable').hide();
                $('#txtDesigName_awTable').hide();
                $('#txtCateName_awTable').hide();
                $('#txtGroupName_awTable').show();
                $('#txtGradeName_awTable').hide();
                $('#txtCardExDate_awTable').hide();
                $('#txtGender_awTable').hide();
                $('#txtBloodGrp_awTable').hide();
                $('#txtDOB_awTable').hide(); 
            });
            $("#txtGradeName").click(function () {
                $('#txtId_awTable').hide();
                $('#txtEmpName_awTable').hide();
                $('#txtSalutation_awTable').hide();
                $('#txtJoinDate_awTable').hide();
                $('#txtCompanyName_awTable').hide();
                $('#txtLocName_awTable').hide();
                $('#txtDiviName_awTable').hide();
                $('#txtDepaName_awTable').hide();
                $('#txtDesigName_awTable').hide();
                $('#txtCateName_awTable').hide();
                $('#txtGroupName_awTable').hide();
                $('#txtGradeName_awTable').show();
                $('#txtCardExDate_awTable').hide();
                $('#txtGender_awTable').hide();
                $('#txtBloodGrp_awTable').hide();
                $('#txtDOB_awTable').hide(); 
            });
            $("#txtCardExDate").click(function () {
                $('#txtId_awTable').hide();
                $('#txtEmpName_awTable').hide();
                $('#txtSalutation_awTable').hide();
                $('#txtJoinDate_awTable').hide();
                $('#txtCompanyName_awTable').hide();
                $('#txtLocName_awTable').hide();
                $('#txtDiviName_awTable').hide();
                $('#txtDepaName_awTable').hide();
                $('#txtDesigName_awTable').hide();
                $('#txtCateName_awTable').hide();
                $('#txtGroupName_awTable').hide();
                $('#txtGradeName_awTable').hide();
                $('#txtCardExDate_awTable').show();
                $('#txtGender_awTable').hide();
                $('#txtBloodGrp_awTable').hide();
                $('#txtDOB_awTable').hide(); 
            });
            $("#txtGender").click(function () {
                $('#txtId_awTable').hide();
                $('#txtEmpName_awTable').hide();
                $('#txtSalutation_awTable').hide();
                $('#txtJoinDate_awTable').hide();
                $('#txtCompanyName_awTable').hide();
                $('#txtLocName_awTable').hide();
                $('#txtDiviName_awTable').hide();
                $('#txtDepaName_awTable').hide();
                $('#txtDesigName_awTable').hide();
                $('#txtCateName_awTable').hide();
                $('#txtGroupName_awTable').hide();
                $('#txtGradeName_awTable').hide();
                $('#txtCardExDate_awTable').hide();
                $('#txtGender_awTable').show();
                $('#txtBloodGrp_awTable').hide();
                $('#txtDOB_awTable').hide(); 
            });
            $("#txtDOB").click(function () {
                $('#txtId_awTable').hide();
                $('#txtEmpName_awTable').hide();
                $('#txtSalutation_awTable').hide();
                $('#txtJoinDate_awTable').hide();
                $('#txtCompanyName_awTable').hide();
                $('#txtLocName_awTable').hide();
                $('#txtDiviName_awTable').hide();
                $('#txtDepaName_awTable').hide();
                $('#txtDesigName_awTable').hide();
                $('#txtCateName_awTable').hide();
                $('#txtGroupName_awTable').hide();
                $('#txtGradeName_awTable').hide();
                $('#txtCardExDate_awTable').hide();
                $('#txtGender_awTable').hide();
                $('#txtBloodGrp_awTable').hide();
                $('#txtDOB_awTable').show(); 
            });
            $("#txtBloodGrp").click(function () {
                $('#txtId_awTable').hide();
                $('#txtEmpName_awTable').hide();
                $('#txtSalutation_awTable').hide();
                $('#txtJoinDate_awTable').hide();
                $('#txtCompanyName_awTable').hide();
                $('#txtLocName_awTable').hide();
                $('#txtDiviName_awTable').hide();
                $('#txtDepaName_awTable').hide();
                $('#txtDesigName_awTable').hide();
                $('#txtCateName_awTable').hide();
                $('#txtGroupName_awTable').hide();
                $('#txtGradeName_awTable').hide();
                $('#txtCardExDate_awTable').hide();
                $('#txtGender_awTable').hide();
                $('#txtBloodGrp_awTable').show();
                $('#txtDOB_awTable').hide(); 
            });

        });
        function GetValue() {

            try {
            
                var EmpCode = $('#ContentPlaceHolder1_ContentPlaceHolder1_lblId').html();
                var txtid = document.getElementById('txtId').value;
                var txtEmpName = document.getElementById('txtEmpName').value;
                var txtSalutation = document.getElementById('txtSalutation').value;
                var txtJoinDate = document.getElementById('txtJoinDate').value;
                var txtCompanyName = document.getElementById('txtCompanyName').value;
                var txtLocName = document.getElementById('txtLocName').value;
                var txtDiviName = document.getElementById('txtDiviName').value;
                var txtDepaName = document.getElementById('txtDepaName').value;
                var txtDesigName = document.getElementById('txtDesigName').value;
                var txtCateName = document.getElementById('txtCateName').value;
                var txtGroupName = document.getElementById('txtGroupName').value;
                var txtGradeName = document.getElementById('txtGradeName').value;
                //     var txtEmpNickName = document.getElementById('txtEmpNickName').value;
                var txtEmpNickName = "";
                var txtCardExDate = document.getElementById('txtCardExDate').value;
                var txtGender = document.getElementById('txtGender').value;
                var txtDOB = document.getElementById('txtDOB').value;
                //                var txtDomi = document.getElementById('txtDomi').value;
                var txtDomi = "";
                var txtBloodGrp = document.getElementById('txtBloodGrp').value;

              
                $.ajax({
                    url: "EnglishToHindi.aspx/InsertData",
                    type: "POST",
                    dataType: "json",
                    async: false,
                    data: "{'EmpCode':'" + EmpCode + "','EmpId':'" + txtid + "','txtEmpName':'" + txtEmpName + "','txtSalutation':'" + txtSalutation + "','txtJoinDate':'" + txtJoinDate + "'," +
                            "'txtCompanyName':'" + txtCompanyName + "','txtLocName':'" + txtLocName + "','txtDiviName':'" + txtDiviName + "','txtDepaName':'" + txtDepaName + "','txtDesigName':'" + txtDesigName + "', " +
                            "'txtCateName':'" + txtCateName + "','txtGroupName':'" + txtGroupName + "','txtGradeName':'" + txtGradeName + "','txtEmpNickName':'" + txtEmpNickName + "','txtCardExDate':'" + txtCardExDate + "','txtGender':'" + txtGender + "', " +
                            "'txtDOB':'" + txtDOB + "','txtDomi':'" + txtDomi + "','txtBloodGrp':'" + txtBloodGrp + "'}",

                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        alert(msg.d);
                    }
                 , error: function () { alert(arguments[2]); }
                });
              
            }
            catch (e)
            {
                alert(e);
            }
        }
    </script>
    <center>
        <asp:HiddenField ID="hdfValues" runat="server" />
        <div style="width: 100%; text-align: center">
        <center>
            <table style="width: 80%;text-align:center; margin-right:0px">
                <tr style="text-align:left;">
                    <td>
                        <h2>
                            Fields</h2>
                    </td>
                    <td>
                        <h2>
                            English</h2>
                    </td>
                    <td>
                        <h2>
                            Hindi</h2>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td >
                        <asp:Label ID="Label1" runat="server" Text="Employee Code"></asp:Label>
                    </td>
                    <td >
                        <asp:Label ID="lblId" runat="server"></asp:Label>
                    </td>
                    <td>
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtId", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Employee Name"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                    </td>
                    <td>
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtEmpName", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Salutation"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSALUTATION" runat="server"></asp:Label>
                    </td>
                    <td>
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtSalutation", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Joining Date"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblJoin_Date" runat="server"></asp:Label>
                    </td>
                    <td>
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtJoinDate", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Company"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCompanyName" runat="server"></asp:Label>
                    </td>
                    <td>
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtCompanyName", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Location"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblLocationName" runat="server"></asp:Label>
                    </td>
                    <td>
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtLocName", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Division"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDiviName" runat="server"></asp:Label>
                    </td>
                    <td>
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtDiviName", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Department"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDepaName" runat="server"></asp:Label>
                    </td>
                    <td>
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtDepaName", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Designation"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDesigName" runat="server"></asp:Label>
                    </td>
                    <td>
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtDesigName", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Category"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCateName" runat="server"></asp:Label>
                    </td>
                    <td>
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtCateName", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Group Name"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                    </td>
                    <td>
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtGroupName", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="Grade Name"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblGradeName" runat="server"></asp:Label>
                    </td>
                    <td>
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtGradeName", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;display:none;">
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="Employee Nickname"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEmpNickName" runat="server"></asp:Label>
                    </td>
                    <td>
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtEmpNickName", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="Card Expiry Date"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCardExDate" runat="server"></asp:Label>
                    </td>
                    <td>
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtCardExDate", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="Gender"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblGender" runat="server"></asp:Label>
                    </td>
                    <td>
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtGender", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>
                        <asp:Label ID="Label16" runat="server" Text="D.O.B."></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDOB" runat="server"></asp:Label>
                    </td>
                    <td >
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtDOB", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;display:none">
                    <td>
                        <asp:Label ID="Label17" runat="server" Text="Domicile"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDomi" runat="server"></asp:Label>
                    </td>
                    <td >
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtDomi", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>
                        <asp:Label ID="Label18" runat="server" Text="Blood Group"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblBloodGrp" runat="server"></asp:Label>
                    </td>
                    <td >
                        <script type="text/javascript">
                            CreateCustomHindiTextBox("txtBloodGrp", "", 49, true);       
                        </script>
                    </td>
                </tr>
                <tr style="text-align:center">
                    <td colspan="3">
                    <br />
                        <asp:Button ID="Button1" runat="server" CssClass="ButtonControl" Text="Save" 
                            OnClientClick="GetValue()" onclick="Button1_Click1" />
                              <asp:Button ID="btnCancel" runat="server" CssClass="ButtonControl" 
                            Text="Cancel" onclick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
            </center>
        </div>
    </center>
    <script type="text/javascript">

        var hdfVal = $('#ContentPlaceHolder1_ContentPlaceHolder1_hdfValues').val();
        var res = hdfVal.split("^");
     
        document.getElementById('txtId').value = res[3];
     
        document.getElementById('txtEmpName').value = res[2];
        document.getElementById('txtSalutation').value = res[4];
        document.getElementById('txtJoinDate').value = res[5];
        document.getElementById('txtCompanyName').value = res[6];
        document.getElementById('txtLocName').value = res[7];
        document.getElementById('txtDiviName').value = res[8];
        document.getElementById('txtDepaName').value = res[9];
        document.getElementById('txtDesigName').value = res[10];
        document.getElementById('txtCateName').value = res[11];
        document.getElementById('txtGroupName').value = res[12];
        document.getElementById('txtGradeName').value = res[13];
        document.getElementById('txtEmpNickName').value = res[14];
        document.getElementById('txtCardExDate').value = res[15];
        document.getElementById('txtGender').value = res[16];
        document.getElementById('txtDOB').value = res[17];
        document.getElementById('txtDomi').value = res[18];
        document.getElementById('txtBloodGrp').value = res[19];

    </script>
</asp:Content>
