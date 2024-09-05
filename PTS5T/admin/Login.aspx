<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="admin_Login" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta charset="utf-8">
    <title>Вход в систему управления ПТС5Т</title>
    <link rel="stylesheet" type="text/css" href="main.css" />
</head>
<body>    
    <div id="main">
	<header>
		<figure class="logo">
			<a href="/" target="_blank">
			<img src="img/logo.png" width="120" height="68" alt="ПТС5Т" /></a>
			<figcaption><a href="default.aspx">Система<br>управления<br>сайтом</a></figcaption>
		</figure>
	</header>
		<table width="100%" cellpadding="0" cellspacing="0" class="content">
		<tr>
			<td class="left">
				<div class="loginform">
					<h1>Вход в систему управления</h1>
					<div class="square">
                        <form id="fMain" runat="server">
					        <label>Логин: <asp:TextBox ID="tbLogin" runat="server" /></label>
					        <label>Пароль: <asp:TextBox ID="tbPass" runat="server" TextMode="Password" /></label>
                            <asp:Literal ID="lErr" runat="server" />
						        <p><a href="#">Восстановление пароля</a></p>
						        <div class="login-buttons">
						        <asp:Button ID="bLogin" runat="server" Text="Войти" OnClick="bLogin_Click" />
						        </div>
                        </form>
					</div>
				</div>
			</td>
		</tr>
		</table>
	<footer>&copy; 2018 ПТС5Т. Прогнозирующая торговая система <span>Разработано в <a href="http://crimea-websites.ru">Crimea Websites</a></span></footer>
	</div>
</body>
</html>
