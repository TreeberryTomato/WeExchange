<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WeExchange.RegisterPage.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Register</title>
    <link rel="stylesheet" href="Register&Login.css" type="text/css"/>
</head>
<body>
   
        <div id="content">
            <!--头部-->
            <div class="content-header clearfix">
                <a href="../LoginPage/Login.aspx">Log in</a>
                <a href="#" class="current">Register</a>
            </div>
            <!--内容-->
            <div class="content-body">
                <!--注册-->
                <div class="dom" style="display: block;">
                    <form id="register" runat="server"  method="post">
                        <div class="s1">
                            <h4>Phone Number</h4>
                            <asp:TextBox runat="server" ID="phone" name="phone" placeholder="Fill in your mobile number as a login account"/>
                        </div>
                        <div class="s1">
                            <h4>User Name</h4>
                            <asp:TextBox runat="server" ID="username" name="username" placeholder="Fill in your username as the login account"/>
                        </div>
                        <div class="s1">
                            <h4>Name</h4>
                            <asp:TextBox runat="server" ID="name" name="name" placeholder="Fill in your real name"/>
                        </div>
                        <div class="s1">
                            <h4>Age</h4>
                            <asp:TextBox runat="server" ID="age" name="age" placeholder="Fill in your age"/>
                        </div>
                        <div class="s1">
                            <h4>Gender</h4>
                            <asp:DropDownList ID="gender" runat="server">
                                <asp:ListItem Value="1">Secret</asp:ListItem>
                                <asp:ListItem Value="2">Male</asp:ListItem>
                                <asp:ListItem Value="3">Female</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="s1">
                            <h4>Password</h4>
                            <asp:TextBox runat="server" ID="password" name="password" type="password" placeholder="6-30 digits in English, numbers, symbols, case sensitive"/>
                        </div>

                        <div class="s1">
                            <h4>Conform Password</h4>
                            <input id="password_confirm" name="password_confirm" type="password" placeholder="6-30 digits in English, numbers, symbols, case sensitive"/>
                        </div>

                        <div class="s1">
                            <h4>Email <span style="color: rgb(0, 128, 43)">(Optional)</span></h4>
                            <asp:TextBox runat="server" ID="email" name="email" type="email" placeholder="If you want to associate an email address, please fill in"/>
                        </div>
                        <asp:Button id="registerbutton" class="btn" text="Register" OnClick="CheckRegister" runat="server" />
                    </form>

                </div>
            </div>
        </div>
</body>
</html>
