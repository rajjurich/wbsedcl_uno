<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="Uno_Dashboard.aspx.cs" Inherits="UNO.Uno_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .content
        {
            height: 93%;
            width: 100%;
            background-image: url('images/Core.gif');
            background-position: center;
            background-repeat: no-repeat;
            background-color: White;
            background-size: 400px 400px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
    </div>
</asp:Content>
