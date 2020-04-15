<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true" CodeBehind="LevelAccessEdit.aspx.cs" Inherits="UNO.LevelAccessEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

 <script type="text/javascript">
    function xyz(){

        $(function () 
        {
        //debugger;
             $("[id*=imgOrdersShow]").each(function () {
                 if ($(this)[0].src.indexOf("minus") != -1) {
                     $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                     $(this).next().remove();
                 }
             });

             $("[id*=imgProductsShow]").each(function () {
                 if ($(this)[0].src.indexOf("minus") != -1) {
                     $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                     $(this).next().remove();
                 }
             });


       });


    }
     
     
    </script>
<style type="text/css">
      body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .Grid td
        {
          width:8%;
            color: black;
            font-size: 10pt;
            line-height: 200%;
            font-weight: bold;
            
            text-align: center;
        }
        .Grid th
        {
            background-color: #47a3da;
            color: White;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
        }
        .ChildGrid td
        {
          width:15%;
            color: black;
            font-size: 10pt;
            font-weight: bold;
            line-height: 200%;
            text-align: center;
        }
        .ChildGrid th
        {
            background-color: #47a3da;
            color: White;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
        }
        .Nested_ChildGrid td
        {
           width:10%;
            color: black;
            font-size: 10pt;
            font-weight: bold;
            line-height: 200%;
        }
        .Nested_ChildGrid th
        {
            background-color: #47a3da;
            color: White;
            font-size: 10pt;
            text-align: center;
            line-height: 200%;
        }
        .Grid
        {
        	width:100%;
        	background-color:#48a4db ;
        	}
        	
        	.ChildGrid
        	{
        		width:100%;
        		background-color:#88c4e8;
        		}
        		
        		.Nested_ChildGrid
        		{
        			width:100%;
        			background-color:#c8e4f4;
        			}
        			.DivGrid
        			{
        				top:5%;
        				
        				}
        	
    .style37
    {
        width: 272px;
    }
    .style38
    {
        width: 167px;
    }
        	
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:Table ID="tblHead" runat="server" HorizontalAlign="Center" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" CssClass="CompulsaryLabel">
                <asp:Label ID="lblHead" runat="server" Text="Levels" ForeColor="RoyalBlue" Font-Size="20px"
                    Width="100%" CssClass="heading">
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
<div class="DivGrid">
    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp;
            </td>
            <td class="style37">
                &nbsp;
            </td>
            <td class="style38">
                &nbsp;
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;
                <asp:Label ID="Label2" runat="server" Text="Level Code :"></asp:Label>
            </td>
            <td class="style37">
                &nbsp;
                <asp:TextBox ID="txtLevelCode" runat="server" Enabled="False"></asp:TextBox>
             
            </td>
            <td align="right" class="style38">
                &nbsp;
                <asp:Label ID="Label1" runat="server" Text="Level Descripton :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtLevelDescrip" runat="server" Style="text-transform: capitalize;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td class="style37">
                &nbsp;
            </td>
            <td class="style38">
                &nbsp;
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    <div style="max-height: 400px; width:80%  ;  text-align:center; margin-left:10% ; border:2px solid black; overflow:scroll; border-radius:5px">

<asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" CssClass="Grid" ShowHeader="false" 
        DataKeyNames="modules" OnRowCommand="gvCustomers_RowCommand"  
        BorderStyle="None" onrowdatabound="gvCustomers_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgOrdersShow" runat="server" OnClick="Show_Hide_OrdersGrid"
                        ImageUrl="~/images/plus.png" CommandArgument="Show" />
                    <asp:Panel ID="pnlOrders" runat="server" Visible="false" Style="position: relative">
                        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" PageSize="5" ShowHeader="false" 
                            AllowPaging="false" OnPageIndexChanging="OnOrdersGrid_PageIndexChanging" CssClass="ChildGrid"
                            PagerStyle-HorizontalAlign="Center" PagerStyle-ForeColor="Black" PagerStyle-Font-Bold="true"
                            DataKeyNames="ID" onrowdatabound="gvOrders_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgProductsShow" runat="server" OnClick="Show_Hide_ProductsGrid"
                                            ImageUrl="~/images/plus.png" CommandArgument="Show" />
                                        <asp:Panel ID="pnlProducts" runat="server" Visible="false" Style="position: relative">
                                            <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" PageSize="2" ShowHeader="false" 
                                                AllowPaging="false" DataKeyNames="ID" onrowdatabound="gvProducts_RowDataBound" OnPageIndexChanging="OnProductsGrid_PageIndexChanging" PagerStyle-HorizontalAlign="Center" PagerStyle-ForeColor="Black" PagerStyle-Font-Bold="true"
                                                CssClass="Nested_ChildGrid">
                                                <Columns>
                                               
                                                    <asp:BoundField ItemStyle-Width="500px" DataField="name" />
                                           
                                                    <asp:TemplateField HeaderText="Operation">
                                                        <ItemTemplate>
                                                            <%--<asp:Button runat="server" Text="Edit" ID="testub" />--%>
                                                             <asp:CheckBox ID="CheckBox3" runat="server"  oncheckedchanged="CheckBox3_CheckedChanged" AutoPostBack="true"/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                        
                                <asp:BoundField ItemStyle-Width="500px" DataField="name" />
                                <asp:TemplateField HeaderText="Operation">
                                    <ItemTemplate>
                                        <%--<asp:Button runat="server" Text="Edit" ID="testub" />--%>
                                         <asp:CheckBox ID="CheckBox2" runat="server"  oncheckedchanged="CheckBox2_CheckedChanged" AutoPostBack="true"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                           
                                 
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField ItemStyle-Width="27%" DataField="modules"/>
            <asp:TemplateField HeaderText="Operation">
                <ItemTemplate>
                <asp:CheckBox ID="CheckBox1" runat="server" 
        oncheckedchanged="CheckBox1_CheckedChanged"  AutoPostBack="false"/>
                
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    </div>

     </ContentTemplate>
        
        </asp:UpdatePanel>


     <center>
    <table>
    <tr>
        <td>
            <asp:Button runat="server" Text="Save" ID="btnSave" onclick="btnSave_Click" 
        Width="69px" CssClass="ButtonControl"/>
        <td>
            <asp:Button runat="server" Text="Cancel" ID="btnCancel" onclick="btnCancel_Click" 
        Width="69px" CssClass="ButtonControl" />
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align:center; color:Red">
            <asp:Label ID="lblMessage" runat="server" Text="" ></asp:Label>

        </td>
    </tr>
    

    </table>
        </center>
        

        
       

        
</div>


</asp:Content>
