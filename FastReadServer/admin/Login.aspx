<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FastReadServer.admin.Login" %>

<!DOCTYPE html>
<!--[if lt IE 7]> <html class="lt-ie9 lt-ie8 lt-ie7" lang="en"> <![endif]-->
<!--[if IE 7]> <html class="lt-ie9 lt-ie8" lang="en"> <![endif]-->
<!--[if IE 8]> <html class="lt-ie9" lang="en"> <![endif]-->
<!--[if gt IE 8]><!-->
<html lang="en">
<!--<![endif]-->
<head>
    <title>登录 -- 快速阅读后台管理</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <link rel="stylesheet" href="css/quickreading/login.css">
    <!--[if lt IE 9]><script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script><![endif]-->
</head>
<body>
    <section class="container">
        <div class="login">
            <h1>快速阅读后台管理</h1>
            <form runat="server">

                <div class="form-group">
                    <label for="email" class="col-md-4 control-label">用户名</label>

                    <div class="col-md-6">
                        <input runat="server" id="txtUserName" type="text" class="form-control" name="email" value="" required autofocus />

                    </div>
                </div>

                <div class="form-group">
                    <label for="email" class="col-md-4 control-label">密码</label>

                    <div class="col-md-6">
                        <input runat="server" id="txtPwd" type="password" name="password" value="" class="form-control" required autofocus />

                    </div>
                </div>

                <p class="remember_me">


                    <asp:CheckBox runat="server" ID="chkRememberMe" Text="记住帐号" />

                </p>
                <p class="submit">
                    <asp:Button runat="server" ID="btnLogin" Text="登录" OnClick="btnLogin_Click" /></p>

                <p style="color: red">
                    <asp:Label runat="server" ID="lblMsg"></asp:Label></p>
            </form>
        </div>


    </section>


</body>
</html>
