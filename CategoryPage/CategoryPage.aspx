<%@ Page Title="" Language="C#" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="CategoryPage.aspx.cs" Inherits="WeExchange.CategoryPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CategoryPage.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrap">
    <div id="category_title" runat="server"></div>
    <div class="top">
        <button id="degree_id" style="display: none"></button>
        <p id='degree' style="margin-bottom: 20px;">
            <font>Degree</font>
            <asp:RadioButtonList runat="server" ID="degree_list" RepeatDirection="Horizontal" OnSelectedIndexChanged="AddItems" CellSpacing="20" AutoPostBack="true">
                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                <asp:ListItem Value="1" >Totally new</asp:ListItem>
                <asp:ListItem Value="2" >80% or 90% new</asp:ListItem>
                <asp:ListItem Value="3" >Others</asp:ListItem>
            </asp:RadioButtonList>
        </p>
    </div>

    <div class="bottom">
        <font style="font-weight: 600;">Sort By</font>
        <asp:DropDownList ID="select" runat="server" OnSelectedIndexChanged="AddItems" AutoPostBack="true">
            <asp:ListItem>Popularity ascending</asp:ListItem>
            <asp:ListItem Selected="True">Popularity descending</asp:ListItem>
            <asp:ListItem>Price ascending</asp:ListItem>
            <asp:ListItem>Price descending</asp:ListItem>
            <asp:ListItem>Time ascending</asp:ListItem>
            <asp:ListItem>Time descending</asp:ListItem>
        </asp:DropDownList>
    </div>
</div>


    <div id="grid_table" runat="server">
    </div>
</asp:Content>
