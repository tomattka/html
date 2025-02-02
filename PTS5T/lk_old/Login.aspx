﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="lk_Login" %>

<!doctype html>
<html>
<head runat="server">
<meta charset="utf-8">
<title>ПТС5Т</title>
	<!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"> <!-- Подстройка под разные типы девайсов -->

    <!-- Bootstrap -->
    <link rel="stylesheet" href="../bootstrap-4.1.3-dist/css/bootstrap.css">
	<script src="../bootstrap-4.1.3-dist/js/bootstrap.min.js"></script>
	<script src="../bootstrap-4.1.3-dist/js/jquery-1.11.3.min.js"></script>	
	<script src="../bootstrap-4.1.3-dist/js/bootstrap.bundle.js"></script>
	
	<!-- Additional CSS -->
	<link rel="stylesheet" href="../style/style.css">	
    <link rel="stylesheet" href="../style/page.css">
    <link rel="stylesheet" href="../style/lk.css">
    	
	<script src="/script.js"></script>
	
	
</head>

<body>
	<div class="navbar container-fluid sticky-top topmenu">
		<div class="container-fluid row">
				<div class="col logo">
                <a href="/"><img src="../img/logo.png" width="100" /></a>
                </div>
				<div class="col-9 menu">
					<nav>
						<a class="about" href="/#h-about">О системе</a>                        
			            <a class="theory" href="/page/theory">Теория</a>
			            <a class="news" href="/news">Новости</a>
			            <a class="articles" href="/articles">Статьи</a>
			            <a class="instructions" href="/page/instructions">Инструкции</a>
			            <a class="contacts" href="/contacts">Контакты</a>	
					</nav>
				</div>
				<div class="col srch"><input ID="tbSearch" type="text" class="search" placeholder="Поиск" MaxLength="50" onkeydown="tbSearch_keyDown()" /></div>
				<div class="col lk"><a href="/lk/">Личный кабинет</a></div>	
		</div>
	</div>

<!-- Content -->

    <div class="container page">
        <form id="fPage" runat="server">
            <article>
                <h1>Вход в личный кабинет</h1>
                <div class="loginform">
                        <asp:TextBox ID="tbLogin" runat="server" placeholder="Логин" />
                        <asp:TextBox ID="tbPass" runat="server" TextMode="Password" placeholder="Пароль"/>
                        <asp:Literal ID="lErr" runat="server" />
                        <a href="/lk/recover.aspx" class="recover">восстановить данные</a>
                        <asp:LinkButton ID="lbLogin" runat="server" Text="Вход" CssClass="lk-button" OnClick="lbLogin_Click" />
                        <a href="/#getit" class="register">Регистрация</a>
                </div>
            </article>
        </form>
    </div>
	

<!-- Footer-->
	
	<footer class="container-fluid">
		<div class="container">
			<div class="social">
				<a href="https://vk.com/id288391221" id="soc1">&nbsp;</a>
				<a href="https://ok.ru/microinvestor01" id="soc2">&nbsp;</a>
				<a href="https://www.facebook.com/forexpts5t" id="soc3">&nbsp;</a>
				<a href="https://twitter.com/pts5t" id="soc4">&nbsp;</a>
				<a href="https://www.instagram.com/pts5t/" id="soc5">&nbsp;</a>
				<a href="https://www.youtube.com/channel/UCSRvO9NSZUPjfemKZ6gc9vQ" id="soc6">&nbsp;</a>
			</div>
		</div>		

		<nav>
			<a class="about" href="/#h-about">О системе</a>                        
			<a class="theory" href="/page/theory">Теория</a>
			<a class="news" href="/news">Новости</a>
			<a class="articles" href="/articles">Статьи</a>
			<a class="instructions" href="/page/instructions">Инструкции</a>
			<a class="contacts" href="/contacts">Контакты</a>	
		</nav>

	</footer>

	<div class="preload">
		<img src="img/list-icons/5t_hl.jpg" alt="Изображение" />
		<img src="img/list-icons/server_hl.jpg" alt="Изображение" />
		<img src="img/list-icons/terminal_hl.jpg" alt="Изображение" />
		<img src="img/list-icons/money_hl.jpg" alt="Изображение" />
		<img src="img/list-icons/time_hl.jpg" alt="Изображение" />
		<img src="img/list-icons/round_up_hl.jpg" alt="Изображение" />
		<img src="img/list-icons/question_hl.jpg" alt="Изображение" />
		<img src="img/list-icons/config_hl.jpg" alt="Изображение" />
		<img src="img/social_new/vk_hover.png" alt="Изображение" />
		<img src="img/social_new/ok_hover.png" alt="Изображение" />
		<img src="img/social_new/insta_hover.png" alt="Изображение" />
		<img src="img/social_new/fb_hover.png" alt="Изображение" />
		<img src="img/social_new/twitter_hover.png" alt="Изображение" />
		<img src="img/menu/5t_hover.png"  alt="Изображение"/>
		<img src="img/menu/book_hover.png" alt="Изображение" />
		<img src="img/menu/news_hover.png" alt="Изображение" />
		<img src="img/menu/articles_hover.png" alt="Изображение" />
		<img src="img/menu/instructions_hover.png" alt="Изображение" />
		<img src="img/menu/support_hover.png" alt="Изображение" />
		<img src="img/menu/contacts_hover.png" alt="Изображение" />
	</div>
	
<!-- Yandex.Metrika counter -->
<script type="text/javascript" >
   (function(m,e,t,r,i,k,a){m[i]=m[i]||function(){(m[i].a=m[i].a||[]).push(arguments)};
   m[i].l=1*new Date();k=e.createElement(t),a=e.getElementsByTagName(t)[0],k.async=1,k.src=r,a.parentNode.insertBefore(k,a)})
   (window, document, "script", "https://mc.yandex.ru/metrika/tag.js", "ym");

   ym(44859973, "init", {
        clickmap:true,
        trackLinks:true,
        accurateTrackBounce:true
   });
</script>
<noscript><div><img src="https://mc.yandex.ru/watch/44859973" style="position:absolute; left:-9999px;" alt="" /></div></noscript>
<!-- /Yandex.Metrika counter -->
	
</body>
</html>
