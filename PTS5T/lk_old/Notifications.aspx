<%@ Page Title="" Language="C#" MasterPageFile="~/lk/mpLK.master" AutoEventWireup="true" CodeFile="Notifications.aspx.cs" Inherits="lk_Notifications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphLK" Runat="Server">
    <form class="col-9 pers" runat="server">
        <h1>Рассылка</h1>
        <h3 style="margin-top: 60px; margin-bottom: 0px;"><asp:CheckBox ID="chSystem" runat="server" Enabled="false" Checked="true" /> Получать системные уведомления</h3>
        <h3 style="margin: 15px 0px 0px 0px;"><label><asp:CheckBox ID="chSub" runat="server" /> Получать рассылку на почту</label></h3>
        <asp:Literal ID="lSucc" runat="server" Visible="false"><p class="succ mess" style="margin-top: 0px;">Данные успешно сохранены.</p></asp:Literal>
        <asp:HiddenField ID="hfLoaded" Value="0" runat="server" />
        <p>&nbsp;</p>
        <asp:LinkButton ID="lbSave" runat="server" CssClass="lk-button btn-center" Text="Сохранить" OnClick="lbSave_Click" />
    </form>
</asp:Content>

