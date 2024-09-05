<%@ Page Title="" Language="C#" MasterPageFile="~/mpPage.master" AutoEventWireup="true" CodeFile="Article.aspx.cs" Inherits="Article" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sph" Runat="Server">
    <article>
        <h1><asp:Literal ID="lTitle" runat="server" /></h1>
        <asp:Literal ID="lText" runat="server" />
        <asp:Literal ID="lTime" runat="server" />
    </article>
</asp:Content>

