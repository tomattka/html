<%@ Page Title="" Language="C#" MasterPageFile="~/mpPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sph" Runat="Server">
        <h1>Поиск</h1>
        <p style="margin: 60px 0px 0px 0px; text-align: center;"><asp:Literal ID="lSearched" runat="server" /></p>
        <ul class="news" style="margin-top: 0px;">
            <asp:Literal ID="lRes" runat="server" />
        </ul>
</asp:Content>

