<%@ Page Title="" Language="C#" MasterPageFile="~/lk/mpLK.master" AutoEventWireup="true" CodeFile="Info.aspx.cs" Inherits="lk_Info" %>

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
            <h1>Данные пользователя</h1>
            <div class="user">
                    <p class="part">Ваше имя</p>
                    <div class="row">
                        <div class="col-12 col-lg-4 col-xl-4"><asp:TextBox ID="tbFamily" runat="server" placeholder="Фамилия" /></div>
                        <div class="col-12 col-lg-4 col-xl-4"><asp:TextBox ID="tbName" runat="server" placeholder="Имя" /></div>
                        <div class="col-12 col-lg-4 col-xl-4"><asp:TextBox ID="tbSecondName" runat="server" placeholder="Отчество" /></div>
                    </div>
                    <p class="part">Контакты</p>
                    <div class="row">
                        <div class="col-12 col-lg-4 col-xl-4"><asp:TextBox ID="tbMail" runat="server" placeholder="E-mail" CausesValidation="true" />
                        <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Укажите ваш E-mail!" ControlToValidate="tbMail" Display="Dynamic" SetFocusOnError="True"><p class="err">Укажите E-mail!</p></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rev" runat="server" ErrorMessage="Укажите корректный E-mail!" Display="Dynamic" ControlToValidate="tbMail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"><p class="err">Укажите корректный E-mail!</p></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-12 col-lg-4 col-xl-4"><asp:TextBox ID="tbPhone" runat="server" placeholder="Телефон" /></div>
                        <div class="col-12 col-lg-4 col-xl-4"><asp:TextBox ID="tbSkype" runat="server" placeholder="Скайп" /></div>
                    </div>
                    <p class="part">Прочие данные</p>
                    <div class="row">
                        <div class="col-12 col-lg-6 col-xl-6"><asp:TextBox ID="tbCity" runat="server" placeholder="Город" /></div>
                        <div class="col-12 col-lg-6 col-xl-6"><asp:TextBox ID="tbSocial" runat="server" placeholder="Соцсети" /></div>
                    </div>
                    <p class="part">Пароль</p>
                    <div class="row">
                        <div class="col-12 col-lg-6 col-xl-6"><asp:TextBox ID="tbPass1" runat="server" placeholder="Пароль" CausesValidation="True" TextMode="Password" />
                            <asp:CompareValidator ID="cvPass" runat="server" ErrorMessage="Пароли не совпадают!" ControlToValidate="tbPass1" ControlToCompare="tbPass2" Display="Dynamic"><p class="err">Пароли не совпадают!</p></asp:CompareValidator>
                        </div>
                        <div class="col-12 col-lg-6 col-xl-6"><asp:TextBox ID="tbPass2" runat="server" placeholder="Повторите пароль" TextMode="Password" /></div>
                    </div>
                    <asp:Literal ID="lSucc" runat="server" Visible="false"><p class="succ_mess">Данные успешно сохранены.</p></asp:Literal>
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="lk-button save" Text="Сохранить" OnClick="lbSave_Click" />
                </div>
        </div>        
    </div>
</asp:Content>

