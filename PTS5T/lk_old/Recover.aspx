<%@ Page Title="" Language="C#" MasterPageFile="~/mpPage.master" AutoEventWireup="true" CodeFile="Recover.aspx.cs" Inherits="lk_Recover" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sph" Runat="Server">
    
    <link rel="stylesheet" href="../style/lk.css">

    <article>
        <h1>Восстановление пароля</h1>
        <div class="loginform" style="text-align: center; border: none; padding-top: 0px;">
            <p>Введите свой e-mail для получения инструкций по восстановлению пароля.</p>
            <asp:TextBox ID="tbMail" runat="server" placeholder="E-mail" />
            <asp:Literal ID="lErr" runat="server" />
            <!--p style="font-size: 12px;"><span style="color: red; font-size: 14px;">Данный адрес электронной почты не зарегистрирован.</span> <br /> Попробуйте ввести другой e-mail, или воспользуйтесь <a href="/#getit">формой для регистрации</a></p-->
            <asp:LinkButton ID="lbRecover" runat="server" Text="Далее" CssClass="lk-button" OnClick="LbRecover_Click" />
        </div>
    </article>
</asp:Content>

