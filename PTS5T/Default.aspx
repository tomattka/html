<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!doctype html>
<html lang="ru">
<head>
<meta charset="utf-8">
<title>ПТС-5Т - прогнозирующая торговая система, робот для форекс</title>
    <meta name="description" content="PTS-5T - робот для торговли на форекс. Это прибыльный советник для форекс, о чём свидетельствуют отзывы профессионалов!">
    <meta name="keywords" content="PTS-5T, ПТС-5Т, торговый робот форекс, советник для форекс">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <!-- Bootstrap -->
    <link rel="stylesheet" href="bootstrap-4.1.3-dist/css/bootstrap.css">
	<script src="bootstrap-4.1.3-dist/js/bootstrap.min.js"></script>
	<script src="bootstrap-4.1.3-dist/js/jquery-1.11.3.min.js"></script>	
	<script src="bootstrap-4.1.3-dist/js/bootstrap.bundle.js"></script>
	
	<!-- Additional CSS -->
	<link rel="stylesheet" href="style/style_new.css">	
    	<link rel="stylesheet" href="style/media_new.css" />

	<script src="js/script_new2.js"></script>

</head>

<body>

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

<form id="FGet" runat="server">
<nav class="navbar navbar-expand-custom navbar-light bg-light topmenu sticky-top">
	<a class="navbar-brand" href="/"><img src="img/logo.png" width="90" alt="PTS-5T"></a>
	<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
		<span class="navbar-toggler-icon"></span>
	</button>
	<div class="collapse navbar-collapse" id="navbarNav">
		<ul class="navbar-nav">
			<li class="nav-item">
				<a class="nav-link about-link" href="/#h-about">О системе</a> 
			</li>
			<li class="nav-item">
				<a class="nav-link theory" href="/page/theory">Теория</a>
			</li>
			<li class="nav-item">
				<a class="nav-link news" href="/news">Новости</a>
			</li>
			<li class="nav-item">
				<a class="nav-link articles" href="/articles">Статьи</a>
			</li>
			<li class="nav-item">
				<a class="nav-link instructions" href="/page/instructions">Инструкции</a>
			</li>
			<li class="nav-item">
				<a class="nav-link contacts" href="/contacts">Контакты</a>
			</li>
			<li class="nav-item lk">
				<a class="nav-link" href="/lk">Личный кабинет</a>
			</li>
		</ul>
	</div>
	<div class="right-menu">
        <input ID="tbSearch" type="text" class="srch" placeholder="Поиск" MaxLength="50" onkeydown="tbSearch_keyDown()" />
		<a href="/lk/" class="lk">Личный кабинет</a>
	</div>
</nav>

<!-- Блок №1 -->
<div class="container-fluid welcome">
		<div id="email" class="position-absolute"><a href="mailto:pts5t@yandex.ru">pts5t@yandex.ru</a></div>
		<div class="container content h-100">
				<div class="col-12 h-100 d-flex align-items-center justify-content-center">
						<div>
							<img src="img/logo.png" width="376" alt="ПТС-5Т">
							<div id="caption">Прогнозирующая торговая система</div>
						</div>
				</div>
		</div>
</div>

<!-- Блок №2 О ПТС-5Т-->
<div class="container-fluid">
		<div class="container about">
			<table>
				<tr>
					<td class="empty">&nbsp;</td>
					<td class="info">ПТС-5Т — торговый робот для форекс, позволяющий получать регулярную прибыль. Проверен опытными трейдерами!</td>
					<td class="empty">&nbsp;</td>
				</tr>
				<tr>
					<td class="box"><a href="#getit"><img src="img/download.jpg" alt="Получить ПТС-5Т" width="54"></a></td>
					<td><a href="#getit" class="get">Получить</a></td>
					<td class="empty">&nbsp;</td>
				</tr>
			</table>
		</div>
</div>

<div class="container-fluid purple-line"></div>

<!-- Блок №2 ПРЕИМУЩЕСТВА-->
<div class="container advantages">
	<h2>Преимущества использования системы&nbsp;ПТС-5Т</h2>
	<table>
		<tr>
			<td class="advantage" onmouseover="advOn(1);" onmouseout="advOut(1);">
				<h3>Доходность</h3>
				<img id="ia1" src="img/advantages/a1.png" alt="Доходность" />
				<p>Ежегодная прибыль в&nbsp;валюте депозита от&nbsp;30% на рынке ФОРЕКС и CFD.</p>
			</td>
			<td class="empty">&nbsp;</td>
			<td class="advantage" onmouseover="advOn(2);" onmouseout="advOut(2);">
				<h3>Доступность</h3>
				<img id="ia2" src="img/advantages/a2.png" alt="Доступность" />
				<p>Участие в&nbsp;тестировании новых&nbsp;версий на&nbsp;DEMO-счетах.</p>
			</td>
			<td class="empty">&nbsp;</td>
			<td class="advantage" onmouseover="advOn(3);" onmouseout="advOut(3);">
				<h3>Гибкость</h3>
				<img id="ia3" src="img/advantages/a3.png" alt="Гибкость" />
				<p>Рисками можно управлять самостоятельно и свести их к минимуму.</p>
			</td>
		</tr>
	</table>
	<table class="t2">
		<tr>
			<td class="advantage" onmouseover="advOn(4);" onmouseout="advOut(4);">
				<h3>Надёжность</h3>
				<img id="ia4" src="img/advantages/a4.png" alt="Надёжность" />
				<p>Вы сами выбираете, в какой системе хранить ваши средства.</p>
			</td>
			<td class="empty">&nbsp;</td>
			<td class="advantage" onmouseover="advOn(5);" onmouseout="advOut(5);">
				<h3>Простота</h3>
				<img id="ia5" src="img/advantages/a5.png" alt="Простота" />
				<p>Понятные инструкции и поддержка специалистов.</p>
			</td>
		</tr>
	</table>
	<a href="#getit" class="btn">Попробовать</a>
</div>
<div class="container-fluid">
	<div class="row">
		 <!-- О ПТС-5Т -->
		<div id="h-about" class="col-12 order-2 order-xl-1 more-about">
			<div class="container">
				<h2>О системе ПТС-5Т</h2>
				<ul>
					<li id="pts5t">Торговый робот ПТС-5Т разработан для автоматической торговли на&nbsp;рынках ФОРЕКС и CFD.</li>
					<li id="terminal">Торговым советником для форекс PTS-5T может воспользоваться каждый желающий.</li>
					<li id="money">Для тестирования предоставляется полный комплекс программного обеспечения: Расчётный Терминал «DATRASTAB PTS-5T», Терминал «Разметки Опорных Точек «ROT PTS-5T», Торговый терминал «Multiterm PTS-5T», Терминал «Графической Аналитики».</li>
					<li id="time">Для его освоения достаточно изучить инструкции, на что уйдет не более нескольких часов.</li>
					<li id="roundup">После установки и настройки, вы можете протестировать торговый робот на DEMO-счёте и убедится в его работоспособности и эффективности.</li>
					<li id="quest">После длительного тестирования принять решение, подходит вам Multiterm PTS-5T или нет.</li>
					<li id="config" style="border: none;">При необходимости наши специалисты всегда окажут вам поддержку.</li>
				</ul>
			</div>
		</div>

		<div class="container-fluid purple-line d-none d-xl-block order-xl-2"></div>
		 <!-- ВИДЕО -->
		<div class="col-12 order-1 order-xl-3 video">
			<div class="container embed-responsive embed-responsive-16by9">
				<iframe class="embed-responsive embed-responsive-16by9" src="https://www.youtube.com/embed/Hn9H5kXsWPA" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
			</div>
		</div>

	</div>
</div>

 <!-- ПОЛУЧИТЬ ПТС-5Т -->
<div id="getit" class="container-fluid">
	<div class="row">
		<div class="col"></div>
		<div class="col-10 col-xs-12 col-xl-4 align-items-center">
			<div id="getform" class="shadow">				
				<p><b>Протестируйте торговый робот ПТС-5Т!</b></p>
				<p class="longdesc">Для этого оставьте свой e-mail, на него будет выслана ссылка на скачивание и инструкция по установке системы.</p>
                <asp:TextBox ID="tbName" runat="server" AutoCompleteType="FirstName" placeholder="Имя" CausesValidation="true" />
                <asp:TextBox ID="tbMail" runat="server" AutoCompleteType="Email" placeholder="E-mail" CausesValidation="true" />
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Введите ваше имя!" ControlToValidate="tbName" Display="Dynamic" SetFocusOnError="True"><p style="color:red; margin:0;">Введите ваше имя!</p></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Укажите ваш E-mail!" ControlToValidate="tbMail" Display="Dynamic" SetFocusOnError="True"><p style="color:red; margin:0;">Укажите E-mail!</p></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rev" runat="server" ErrorMessage="Укажите корректный E-mail!" Display="Dynamic" ControlToValidate="tbMail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"><p style="color:red; margin:0;">Укажите корректный E-mail!</p></asp:RegularExpressionValidator>
                <asp:LinkButton ID="lbGet" runat="server" Text="Получить" OnClick="lbGet_Click" />
			</div>
		</div>
		<div class="col"></div>
	<div class="col-6 d-none d-xl-block">&nbsp;</div>
	
	</div>
</div>

<!-- FEEDBACK START-->

<div id="feedback" class="container-fluid d-none d-xl-block" style="padding: 0px;">
	<div class="container-fluid" style="padding: 0px;">
		
		<h2>Отзывы о торговле с ПТС-5Т</h2>

		<div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">

			<div class="carousel-inner">

				<div class="carousel-item active">
					<div class="row">
						<div class="col"></div>
						<div class="photo" style="padding-left: 30px;"><img src="img/feedback/alina.jpg" alt="Алина" width="90"></div>
						<div class="col-3">
							<p class="name">Алина</p><p>предприниматель</p><p><img src="img/feedback/5stars.gif" width="120" alt="5 звёзд" /></p><p>Одназначно рекомендую скачать и установить ПТС-5Т! Достойная торговая система.</p>
						</div>
						<div class="photo"><img src="img/feedback/rustam.jpg" alt="Рустам" width="90"></div>
						<div class="col-3">
								<p class="name">Рустам</p><p>трейдер</p><p><img src="img/feedback/5stars.gif" width="120" alt="5 звёзд" /></p><p>ПТС-5Т учитывает огромное количество параметров для того, чтобы торговля была действительно эффективной.</p>
						</div>
						<div class="photo"><img src="img/feedback/dmitry.jpg" alt="Дмитрий" width="90"></div>
						<div class="col-3" style="border: none; max-width: 19%; min-width: 19%;">
								<p class="name">Дмитрий</p><p>трейдер</p><p><img src="img/feedback/5stars.gif" width="120" alt="5 звёзд" /></p><p>Проверил советник ПТС-5Т на демо-счёте — стабильно в плюс. Скоро попробую на реальных деньгах)</p>
						</div>
					</div>
				</div>

				<div class="carousel-item">
					<div class="row">
						<div class="col"></div>
						<div class="photo" style="padding-left: 30px;"><img src="img/feedback/anatoly.jpg" alt="Анатолий" width="90"></div>
						<div class="col-3">
							<p class="name">Анатолий</p><p>трейдер</p><p><img src="img/feedback/5stars.gif" width="120" alt="5 звёзд" /></p><p>Можно использовать систему ПТС-5Т как для простой автоторговли, так и для получения различных аналитических даных. Имеет много настроек.</p>
						</div>
						<div class="photo"><img src="img/feedback/ivan.jpg" alt="Иван" width="90"></div>
						<div class="col-3">
								<p class="name">Иван</p><p>трейдер</p><p><img src="img/feedback/5stars.gif" width="120" alt="5 звёзд" /></p><p>Испытывал проблемы с установкой, но разработчики мне помогли. Спасибо!</p>
						</div>
						<div class="photo"><img src="img/feedback/airat.jpg" alt="Айрат" width="90"></div>
						<div class="col-3" style="border: none; max-width: 19%; min-width: 19%;">
								<p class="name">Айрат</p><p>программист</p><p><img src="img/feedback/5stars.gif" width="120" alt="5 звёзд" /></p><p>Рекомендую! Позволяет получать хорошую прибыль. Пользуюсь уже не первый месяц.</p>
						</div>
					</div>
				</div>
			</div>

			<a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev" style="width: 50px; background:none;">
				<span class="carousel-control-prev-icon" aria-hidden="true" style="background:none;"><img src="img/arrow_left.png" width="80" alt="Влево" /></span>
				<span class="sr-only">Previous</span>
			</a>
			<a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next" style="width: 160px;">
				<span class="carousel-control-next-icon" style="background-image:none;"><img src="img/arrow_right.png" width="80" alt="Вправо" /></span>
				<span class="sr-only">Next</span>
			</a>
		</div>
	</div>
</div>

<!-- FEEDBACK END-->
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

	<nav class="d-none d-xl-block">
		<a class="about-link" href="/#h-about">О системе</a>                        
		<a class="theory" href="/page/theory">Теория</a>
		<a class="news" href="/news">Новости</a>
		<a class="articles" href="/articles">Статьи</a>
		<a class="instructions" href="/page/instructions">Инструкции</a>
		<a class="contacts" href="/contacts">Контакты</a>	
	</nav>

	<div class="container copyright d-xl-none">&copy; 2019-2021 <a href="https://pts5t.ru">pts5t.ru</a></div>

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
		<img src="img/social_new/fb_hover.png" alt="Изображение" />
		<img src="img/social_new/insta_hover.png" alt="Изображение" />
		<img src="img/social_new/twitter_hover.png" alt="Изображение" />
		<img src="img/social_new/youtube_hover.png" alt="Изображение" />
		<img src="img/social_new/ok_hover.png" alt="Изображение" />
		<img src="img/menu/5t_hover.png"  alt="Изображение"/>
		<img src="img/menu/news_hover.png" alt="Изображение" />
		<img src="img/menu/articles_hover.png" alt="Изображение" />
		<img src="img/menu/instructions_hover.png" alt="Изображение" />
		<img src="img/menu/theory_hover.png" alt="Изображение" />
		<img src="img/menu/contacts_hover.png" alt="Изображение" />
		<img src="img/menu/lk_icon_hover.png" alt="Изображение" />
	</div>

</form>

</body>

</html>