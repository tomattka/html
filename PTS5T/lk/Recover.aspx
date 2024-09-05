<%@ Page Title="" Language="C#" MasterPageFile="~/lk/mpLK.master" AutoEventWireup="true" CodeFile="Recover.aspx.cs" Inherits="lk_Recover" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sph" Runat="Server">
    <div class="loginform" style="text-align: center; border: none; padding-top: 0px;">
        <h1>Восстановление пароля</h1>
        <p>Введите свой e-mail для получения инструкций по восстановлению пароля.</p>
        <asp:TextBox ID="tbMail" runat="server" placeholder="E-mail" />
        <asp:Literal ID="lErr" runat="server" />
        <asp:LinkButton ID="lbRecover" runat="server" Text="Далее" CssClass="lk-button" OnClick="LbRecover_Click" />
    </div>
</asp:Content>

