<%@ Page Title="" Language="C#" MasterPageFile="~/lk/mpLK.master" AutoEventWireup="true" CodeFile="Notifications.aspx.cs" Inherits="lk_Notifications" %>

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
            <h1>Рассылка</h1>
            <div class="subs">
                <p><label><asp:CheckBox ID="chSystem" runat="server" Enabled="false" Checked="true" /> Получать системные уведомления</label></p>
                <p><label><asp:CheckBox ID="chSub" runat="server" /> Получать рассылку на почту</label></p>
                <asp:Literal ID="lSucc" runat="server" Visible="false"><p class="succ_mess">Данные успешно сохранены.</p></asp:Literal>
                <asp:HiddenField ID="hfLoaded" Value="0" runat="server" />
                <asp:LinkButton ID="lbSave" runat="server" CssClass="lk-button save" Text="Сохранить" OnClick="lbSave_Click" />
            </div>
        </div>        
    </div>
</asp:Content>

