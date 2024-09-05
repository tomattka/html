<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin.master" AutoEventWireup="true" CodeFile="editUserSub.aspx.cs" Inherits="admin_editUserSub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
    <h1>Подписка пользователя <asp:Literal ID="lUser" runat="server" /></h1>
    <div class="allmoney" style="width: 325px; font-size: 20px;"><asp:Literal ID="lCurrentSub" runat="server" /></div>
    <div class="editForm">
        <table>
        <tr><td style="text-align: center; padding-bottom: 5px;"><b>Начало подписки</b></td><td style="text-align: center; padding-bottom: 5px;"><b>Конец подписки</b></td></tr>        
        <tr>
            <td><asp:Calendar ID="cStart" runat="server" /></td>
            <td><asp:Calendar ID="cEnd" runat="server" /></td>
        </tr>
            <tr class="buttons">
                <td style="text-align: center; padding-top: 5px;"><asp:Button ID="bSave" runat="server" Text="Сохранить" OnClick="bSave_Click" /></td>
                <td style="text-align: center; padding-top: 5px;"><asp:Button ID="bCancel" runat="server" Text="Отмена" OnClick="bCancel_Click" /></td>
            </tr>
    </table>
    </div>
</asp:Content>

