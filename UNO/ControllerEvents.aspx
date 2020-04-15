<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="ControllerEvents.aspx.cs" Inherits="UNO.RDLC.ControllerEvents" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: 10%">
    <h1 style="text-align:center">Controller Events Report</h1>

    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Header" runat="server">
                    <div id="Content" style="margin-left: 33%; border: 1px solid; width: 350px; background:#F8F8FA">
                        <table id="tblHead" cellspacing="3px" style="text-align: left; height: 93px; width: 350px;">
                            <tr >
                                <td >
                                    Controller Id
                                </td>
                                <td>
                                  <%--  <asp:TextBox ID="txtControlerId" runat="server" Width="75px"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlControllerId" runat="server">
                                    </asp:DropDownList>
                                 <%--   <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtControlerId"
                                        Display="none" ErrorMessage="Please select log month" SetFocusOnError="True"
                                        InitialValue="Select One" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    <ajaxtoolkit:validatorcalloutextender id="VCEddlReason1" runat="server" targetcontrolid="rfv"
                                        popupposition="right">
                            </ajaxtoolkit:validatorcalloutextender>--%>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="style37">
                                    Select log Month & Year
                                </td>
                                <td class="style1">
                                    <asp:DropDownList ID="ddlMonth" runat="server" ValidationGroup="Add">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="Reqddlmonth" runat="server" ControlToValidate="ddlMonth"
                                        Display="none" ErrorMessage="Please select log month" SetFocusOnError="True"
                                        InitialValue="Select One" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    <ajaxtoolkit:validatorcalloutextender id="VCEddlReason" runat="server" targetcontrolid="Reqddlmonth"
                                        popupposition="right">
                            </ajaxtoolkit:validatorcalloutextender>
                                </td>
                                <td class="style37">
                                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="ButtonControl" ValidationGroup="Add"
                                        OnClick="btnView_Click1" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    From:
                                    <%--<asp:TextBox ID="txtFrom" runat="server" Width="50px"></asp:TextBox>--%>
                                    <asp:DropDownList ID="txtFrom" runat="server">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>17</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>21</asp:ListItem>
                                    <asp:ListItem>22</asp:ListItem>
                                    <asp:ListItem>23</asp:ListItem>
                                    <asp:ListItem>24</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>26</asp:ListItem>
                                    <asp:ListItem>27</asp:ListItem>
                                    <asp:ListItem>28</asp:ListItem>
                                    <asp:ListItem>29</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>31</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    To:
                                   <%-- <asp:TextBox ID="txtTo" runat="server" Width="50px"></asp:TextBox>--%>
                                    <asp:DropDownList ID="txtTo" runat="server">
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                        <asp:ListItem>24</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>26</asp:ListItem>
                                        <asp:ListItem>27</asp:ListItem>
                                        <asp:ListItem>28</asp:ListItem>
                                        <asp:ListItem>29</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>31</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="ButtonControl" OnClick="btnReset_Click1" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Body" runat="server" Visible="false">
                <div style="height:5%; text-align:right;margin-left: 15%;  width: 70%">
                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="ButtonControl" 
            onclick="btnClose_Click"/>
                </div>
                    <div style="margin-left: 15%; margin-top: 1%; border: 1px solid black; width: 70%">
                        <rsweb:reportviewer id="ReportViewer1" runat="server" width="100%">
            </rsweb:reportviewer>
                    </div>
                </asp:Panel>
                </div>
            </ContentTemplate>

            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnview" />
            </Triggers>
        </asp:UpdatePanel>
        
                
    </div>
</asp:Content>
