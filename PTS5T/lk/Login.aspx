<%@ Page Title="" Language="C#" MasterPageFile="~/lk/mpLK.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="lk_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sph" Runat="Server">
    <h1>Вход в личный кабинет</h1>
    <div class="loginform">
        <asp:TextBox ID="tbLogin" runat="server" placeholder="Логин" />
        <asp:TextBox ID="tbPass" runat="server" TextMode="Password" placeholder="Пароль"/>
        <asp:Literal ID="lErr" runat="server" />
        <a href="/lk/recover.aspx" class="recover">восстановить данные</a>
        <asp:LinkButton ID="lbLogin" runat="server" Text="Вход" CssClass="lk-button" OnClick="lbLogin_Click" />
        <a href="/#getit" class="register">Регистрация</a>
    </div>
</asp:Content>

