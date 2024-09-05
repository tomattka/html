<%@ Page Title="Новости системы ПТС5Т" Language="C#" MasterPageFile="~/mpPage.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sph" Runat="Server">
    <div class="row">
        <div class="col-12 col-lg-3 col-xl-3 order-2 order-xl-1 cats">
                <div id="informer-money" class="informer">
                    <!--  FOREXPF.RU - Forex start -->
                    <table width="186" border="1" style="border-collapse: collapse; text-align:center; font-size:11px"><tr bgcolor=""><Td height="10" valign="top" colspan="3"><a href="http://www.profinance.ru/" title="Forex: курсы валют" target="_blank" class="forexpf_">Forex: Курсы валют</a></Td></tr><tr bgcolor="F6EDDD"><td><a href="http://www.profinance.ru/chart/eurusd/" title="EUR/USD" target="_blank" class="forexpf_">EUR/USD</a></td><td id="euusb">0.00</td><td id="euusa">0.00</td></tr><tr bgcolor=""><td><a href="http://www.profinance.ru/chart/gbpusd/" title="GBP/USD" target="_blank" class="forexpf_">GBP/USD</td><td id="gbusb">0.00</td><td id="gbusa">0.00</td></tr><tr bgcolor="F6EDDD"><td><a href="http://www.profinance.ru/chart/usdchf/" title="USD/CHF" target="_blank" class="forexpf_">USD/CHF</td><td id="uschb">0.00</td><td id="uscha">0.00</td></tr><tr bgcolor=""><td><a href="http://www.profinance.ru/chart/usdjpy/" title="USD/JPY" target="_blank" class="forexpf_">USD/JPY</td><td id="usjpb">0.00</td><td id="usjpa">0.00</td></tr><tr bgcolor="F6EDDD"><Td height="10" valign="top" colspan="3" id="frxtm">Данные на 00:00 мск</td></tr></table><script src="https://informers.forexpf.ru/forex.php?id=479A"></script>
                    <!--  FOREXPF.RU - Forex end -->
                </div>
                <div id="informer-forex" class="informer">
                    <table border="1" cellpadding="0" cellspacing="10" bgcolor="#FBFBFB" width="100%" style="border-collapse:collapse; border-color:#D7D7D7;"><tr><td><a href="http://www.profinance.ru" id="forexpf_forex" style="font-size: 13px; margin-bottom: 5px; font-weight:bold">Валютный рынок Forex</a><br><script charset="utf-8" src="https://informers.forexpf.ru/export/news.js"></script></td></tr></table>
                </div>

                <div id="informer-fond" class="informer">
                    <table border="1" cellpadding="0" cellspacing="10" bgcolor="#FBFBFB" width="100%" style="border-collapse:collapse;border-color:#D7D7D7;"><tr><td><a href="http://www.profinance.ru" id="forexpf_fond" style="font-size: 13px; margin-bottom: 5px; font-weight:bold">Фондовый рынок</a><br><script src="https://informers.forexpf.ru/export/fond.js"></script></td></tr></table>
                </div>
            </div>
        <div class="col-12 col-lg-9 col-xl-9 order-1 order-xl-2">
            <h1>Новости системы ПТС5Т</h1>
                <ul class="news">
                    <asp:Literal ID="lNews" runat="server" />
                </ul>
        </div>
    </div>
</asp:Content>

