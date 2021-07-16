<%@ Page Language="C#" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="ItemPage.aspx.cs" Inherits="WeExchange.ItemPage.ItemPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Item Detail</title>
    <link rel="stylesheet" href="itemPage.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--
<button id="item_id" value=" " style="display: none"></button>
    -->

<div class="item_info">
    <div class="slideshow">
        <div id="wrap" name="wrap" class="wrap" runat="server" style="left: 0px;">
             
        </div>
        <div class="buttons">
            <span class="on"></span>
            <span></span>
            <span></span>
            <span></span>
        </div>
        <a href="javascript:;" class="arrow arrow_left">&lt;</a>
        <a href="javascript:;" class="arrow arrow_right">&gt;</a>
    </div>

    <div class="description" id="description" name="description" runat="server">
        
    </div>
</div>

<script src="itemPage.js"></script>

</asp:Content>
