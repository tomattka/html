<%@ Page Title="" Language="C#" MasterPageFile="~/lk/mpLK.master" AutoEventWireup="true" CodeFile="Info.aspx.cs" Inherits="lk_Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphLK" Runat="Server">
    <form class="col-9 pers" runat="server">
        <h1>Данные пользователя</h1>
        <h3>Ваше имя:</h3>
        <asp:TextBox ID="tbFamily" runat="server" placeholder="Фамилия" CssClass="i3" /><asp:TextBox ID="tbName" runat="server" placeholder="Имя" CssClass="i3" /><asp:TextBox ID="tbSecondName" runat="server" placeholder="Отчество" CssClass="i3" />
        <h3>Контакты:</h3>
        <asp:TextBox ID="tbMail" runat="server" placeholder="E-mail" CssClass="i3" CausesValidation="true" /><asp:TextBox ID="tbPhone" runat="server" placeholder="Телефон" CssClass="i3" /><asp:TextBox ID="tbSkype" runat="server" placeholder="Скайп" CssClass="i3" />
            <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Укажите ваш E-mail!" ControlToValidate="tbMail" Display="Dynamic" SetFocusOnError="True"><p class="err">Укажите E-mail!</p></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rev" runat="server" ErrorMessage="Укажите корректный E-mail!" Display="Dynamic" ControlToValidate="tbMail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"><p class="err">Укажите корректный E-mail!</p></asp:RegularExpressionValidator>
        <p class="err" style="display: none;">Неверный формат</p>
        <h3>Прочие данные</h3>
        <asp:TextBox ID="tbCity" runat="server" placeholder="Город" CssClass="i2" /><asp:TextBox ID="tbSocial" runat="server" placeholder="Соцсети" CssClass="i2" />
        <h3>Пароль:</h3>
        <asp:TextBox ID="tbPass1" runat="server" placeholder="Пароль" CssClass="i2" CausesValidation="True" TextMode="Password" /><asp:TextBox ID="tbPass2" runat="server" placeholder="Повторите пароль" CssClass="i2" TextMode="Password" />
        <asp:CompareValidator ID="cvPass" runat="server" ErrorMessage="Пароли не совпадают!" ControlToValidate="tbPass1" ControlToCompare="tbPass2" Display="Dynamic"><p class="err">Пароли не совпадают!</p></asp:CompareValidator>
        <asp:Literal ID="lSucc" runat="server" Visible="false"><p class="succ mess">Данные успешно сохранены.</p></asp:Literal>
        <asp:LinkButton ID="lbSave" runat="server" CssClass="lk-button btn-center" Text="Сохранить" OnClick="lbSave_Click" />
    </form>
</asp:Content>

