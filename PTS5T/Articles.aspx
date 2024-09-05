<%@ Page Title="Статьи об автоматической и ручной торговле на рынке Форекс" Language="C#" MasterPageFile="~/mpPage.master" AutoEventWireup="true" CodeFile="Articles.aspx.cs" Inherits="Articles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sph" Runat="Server">    
    <script src="/js/articles.js"></script>
    <div class="row">
            <div class="col-12 col-lg-3 col-xl-3 cats">
                <h2>Категории <a id="catroll" href="#" onclick="catRoll(); return false;">(+)</a></h2>
                <ul id="catul">
                    <asp:Literal ID="lCats" runat="server" />
                    <li> </li>
                </ul>
                <div id="informer-money" class="informer">
                    <!--  FOREXPF.RU - Forex start -->
                    <table width="186" border="1" style="border-collapse: collapse; text-align:center; font-size:11px"><tr bgcolor=""><Td height="10" valign="top" colspan="3"><a href="http://www.profinance.ru/" title="Forex: курсы валют" target="_blank" class="forexpf_">Forex: Курсы валют</a></Td></tr><tr bgcolor="F6EDDD"><td><a href="http://www.profinance.ru/chart/eurusd/" title="EUR/USD" target="_blank" class="forexpf_">EUR/USD</a></td><td id="euusb">0.00</td><td id="euusa">0.00</td></tr><tr bgcolor=""><td><a href="http://www.profinance.ru/chart/gbpusd/" title="GBP/USD" target="_blank" class="forexpf_">GBP/USD</td><td id="gbusb">0.00</td><td id="gbusa">0.00</td></tr><tr bgcolor="F6EDDD"><td><a href="http://www.profinance.ru/chart/usdchf/" title="USD/CHF" target="_blank" class="forexpf_">USD/CHF</td><td id="uschb">0.00</td><td id="uscha">0.00</td></tr><tr bgcolor=""><td><a href="http://www.profinance.ru/chart/usdjpy/" title="USD/JPY" target="_blank" class="forexpf_">USD/JPY</td><td id="usjpb">0.00</td><td id="usjpa">0.00</td></tr><tr bgcolor="F6EDDD"><Td height="10" valign="top" colspan="3" id="frxtm">Данные на 00:00 мск</td></tr></table><script src="https://informers.forexpf.ru/forex.php?id=479A"></script>
                    <!--  FOREXPF.RU - Forex end -->
                </div>
                <div id="informer-forex" class="informer d-none d-xl-block">
                    <table border="1" cellpadding="0" cellspacing="10" bgcolor="#FBFBFB" width="100%" style="border-collapse:collapse; border-color:#D7D7D7;"><tr><td><a href="http://www.profinance.ru" id="forexpf_forex" style="font-size: 13px; margin-bottom: 5px; font-weight:bold">Валютный рынок Forex</a><br><script charset="utf-8" src="https://informers.forexpf.ru/export/news.js"></script></td></tr></table>
                </div>

                <div id="informer-fond" class="informer d-none d-xl-block">
                    <table border="1" cellpadding="0" cellspacing="10" bgcolor="#FBFBFB" width="100%" style="border-collapse:collapse;border-color:#D7D7D7;"><tr><td><a href="http://www.profinance.ru" id="forexpf_fond" style="font-size: 13px; margin-bottom: 5px; font-weight:bold">Фондовый рынок</a><br><script src="https://informers.forexpf.ru/export/fond.js"></script></td></tr></table>
                </div>
            </div>
        <div class="col-12 col-lg-9 col-xl-9">
        <h1><asp:Literal ID="lCatTitle" runat="server" /></h1>
            <ul class="news">
                <asp:Literal ID="lArticles" runat="server" />
            </ul>   
        <div id="informer-forex-2" class="informer informer-bottom d-block d-xl-none">
                <table border="1" cellpadding="0" cellspacing="10" bgcolor="#FBFBFB" width="100%" style="border-collapse:collapse; border-color:#D7D7D7;"><tr><td><a href="http://www.profinance.ru" id="forexpf_forex" style="font-size: 13px; margin-bottom: 5px; font-weight:bold">Валютный рынок Forex</a><br><script charset="utf-8" src="https://informers.forexpf.ru/export/news.js"></script></td></tr></table>
        </div> 
        <div id="informer-fond-2" class="informer informer-bottom d-block d-xl-none">
                <table border="1" cellpadding="0" cellspacing="10" bgcolor="#FBFBFB" width="100%" style="border-collapse:collapse;border-color:#D7D7D7;"><tr><td><a href="http://www.profinance.ru" id="forexpf_fond" style="font-size: 13px; margin-bottom: 5px; font-weight:bold">Фондовый рынок</a><br><script src="https://informers.forexpf.ru/export/fond.js"></script></td></tr></table>
        </div>
        </div>    
    </div>
</asp:Content>

