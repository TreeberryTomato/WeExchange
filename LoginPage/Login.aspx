<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WeExchange.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login</title>
    <link rel="stylesheet" href="Register&Login.css" type="text/css"/>
</head>
<body>
   
        <div id="content">
            <!--头部-->
            <div class="content-header clearfix">
                <a href="#" class="current">Log in</a>
                <a href="../RegisterPage/Register.aspx">Register</a>
            </div>
            <!--内容-->
            <div class="content-body">
                <!--登录-->
                <div class="dom" style="display: block;">
                    <form id="login" runat="server" method="post">
                        <div class="s1">
                            <h4>Account</h4>
                            <asp:TextBox runat="server" ID="user_name" name="user_name" placeholder="Username"/>
                        </div>
                        <div class="s1">
                            <h4>Password</h4>
                            <asp:TextBox runat="server" ID="psd" name="psd" type="password" placeholder="Please enter password"/>
                        </div>
                        <div class="s2">
                            <input type="checkbox"/>
                            <span>Remember password</span>
                        </div>
                        <asp:GridView ID="GridView1" runat="server" Style="margin-right: 0px"></asp:GridView>
                        <asp:Button id="loginbutton" class="btn" text="Login" OnClick="Checklogin" runat="server" />
                    </form>

                    <div class="dom-footer">
                        <div class="login-another">
                            <a href="#" style="color: rgb(117, 34, 34)">Forget password</a>
                            <span>|</span>
                            <span>Don't have an account?</span>
                            <a href="Register.aspx" style="color: rgb(117, 34, 34)">Join</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</body>
</html>
