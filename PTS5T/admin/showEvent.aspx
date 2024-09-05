<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin.master" AutoEventWireup="true" CodeFile="showEvent.aspx.cs" Inherits="admin_showEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
    <h1><asp:Literal ID="lEventTitle" runat="server" /></h1>
    <div style="font-size: 14px; color: 333;">
        <p><b>Время: </b><asp:Literal ID="lTime" runat="server" /></p>
        <p><b>Пользователь : </b><asp:Literal ID="lUser" runat="server" /></p>
        <p><b>Объект: </b><asp:Literal ID="lObj" runat="server" /></p>
        <p><b>Ссылка: </b><asp:Literal ID="lObjHref" runat="server" /></p>
    </div>
</asp:Content>

