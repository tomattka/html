<%@ Page Title="" Language="C#" MasterPageFile="~/lk/mpLK.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="lk_Download" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sph" Runat="Server">
    <div class="row">
        <div class="col-12 col-lg-3 col-xl-3 cats">
            <h2>Личный кабинет <a id="catroll" href="#" onclick="catRoll(); return false;">(+)</a></h2>
            <ul id="catul">
                <li><a href="default.aspx">Скачать ПТС5Т</a></li>
                <li><a href="info.aspx">Данные пользователя</a></li>
                <li><a href="notifications.aspx">Рассылка</a></li>
                <li><a href="login.aspx?do=logout">Выход</a></li>
                <li> </li>
            </ul>
        </div>
        <div class="col-12 col-lg-9 col-xl-9">
            <h1>Скачать ПТС5Т</h1>
            <div style="margin: 90px auto 120px auto; text-align: center">
            <p>Ссылка на скачивание:</p>
            <p><a href="/program/PTS5T.zip" target="_blank">https://pts5t.ru/program/PTS5T.zip</a></p>
            </div>
        </div>        
    </div>
</asp:Content>

