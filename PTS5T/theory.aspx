<%@ Page Title="Теория ПТС5Т, автоматическая торговля на forex" Language="C#" MasterPageFile="~/mpPage.master" AutoEventWireup="true" CodeFile="theory.aspx.cs" Inherits="Theory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sph" Runat="Server">    
    <script src="/js/articles.js"></script>
    <div class="row">
            <div class="col-12 col-lg-3 col-xl-3 cats">
                <h2>Разделы <a id="catroll" href="#" onclick="catRoll(); return false;">(+)</a></h2>
                <ul id="catul">
                    <asp:Literal ID="lCats" runat="server" />
                    <li> </li>
                </ul>
            </div>
        <div class="col-12 col-lg-9 col-xl-9">
        <h1><asp:Literal ID="lCatTitle" runat="server" /></h1>
            <ul class="news">
                <asp:Literal ID="lTheory" runat="server" />
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

