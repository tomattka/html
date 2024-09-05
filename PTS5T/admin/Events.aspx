<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin.master" AutoEventWireup="true" CodeFile="Events.aspx.cs" Inherits="admin_Events" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
    <h1>События <asp:LinkButton ID="lbDeleteAll" runat="server" Text="Очистить" OnClientClick="return confirm('ВНИМАНИЕ! Все события будут удалены!')" OnClick="lbDeleteAll_Click" /></h1>
    <asp:Panel ID="pEvents" runat="server" />
    <nav class="pages"><asp:Literal ID="lPager" runat="server" /></nav>
    <nav class="pages" style="display: none;"><a href="#">&laquo;</a><a href="#">1</a><a href="#">2</a><a href="#" class="active">3</a><a href="#">4</a><a href="#">5</a><a href="#">&raquo;</a></nav>
</asp:Content>

