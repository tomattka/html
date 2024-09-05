<%@ Page Title="" Language="C#" MasterPageFile="~/lk/mpLK.master" AutoEventWireup="true" CodeFile="Subscription.aspx.cs" Inherits="lk_Subscription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphLK" Runat="Server">
    <div class="col-9 sub">
        <form runat="server">
        <h1>Подписка</h1>
        <p class="till"><asp:Literal ID="lSub" runat="server" /><!--a href="#">продлить</a--></p>            
        <h2>Покупка/продление подписки</h2>
        <ul>
        <li><label><asp:RadioButton ID="rb1M" runat="server" GroupName="rbl" Checked="true" />На 1 месяц <span class="dots">................................................................................................................................................</span> <span class="price">100 Б&#8381;</span></label></li>
        <li><label><asp:RadioButton ID="rb3M" runat="server" GroupName="rbl" />На 3 месяца <span class="dots">.............................................................................................................................................</span> <span class="price">270 Б&#8381;</span></label></li>
        <li><label><asp:RadioButton ID="rb6M" runat="server" GroupName="rbl" />На 6 месяцев <span class="dots">..........................................................................................................................................</span> <span class="price">480 Б&#8381;</span></label></li>
        </ul>
            <asp:Literal ID="lErr" runat="server" />
            <!--
            <p class="err">Недостаточно денег для продления подписки! <a href="#">Пополнить баланс</a></p>
            -->
            <asp:LinkButton ID="lbProlong" runat="server" Text="Продлить подписку" CssClass="lk-button btn-center" OnClick="LbProlong_Click" />
        </form>
    </div>        
</asp:Content>

