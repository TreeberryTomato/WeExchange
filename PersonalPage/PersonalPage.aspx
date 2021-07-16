﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="PersonalPage.aspx.cs" Inherits="WeExchange.PersonalPage.PersonalPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="PersonalPage.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
        <div id="account_title">Profile</div>
        <div><asp:Image ID="profile" runat="server"/>
            <asp:FileUpload ID="upload_button" runat="server"/>
        </div>
        <p style="margin-top: 30px; margin-left: 250px;">Name
            <asp:TextBox type="text" ID="name" class="info_items" runat="server"/>
        </p>
        <p style="margin-top: 30px; margin-left: 250px;">Gender
            <asp:DropDownList ID="gender" runat="server">
                <asp:ListItem>Male</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>
                <asp:ListItem>Secret</asp:ListItem>
            </asp:DropDownList>
        </p>
        <div style="margin-top: 20px; margin-left: 250px;"><div style="float: left; height: 200px; margin-top: 5px;">Contact Info</div>
            <div style="height: 200px; float: left;">
                <textarea runat="server" class="info_items" ID="contact_info" cols="50" rows="3" placeholder="contact_info"/>
            </div>
        </div>
        <asp:ImageButton runat="server" ID="modify_button" OnClick="Modify" ImageUrl="~/images/edit.png" />
        <asp:Button id="save" runat="server" Text="Save" OnClick="Update"/>
        <asp:Button id="cancel" runat="server" Text="Cancel" OnClick="Cancel"/>
    </div>
</asp:Content>
