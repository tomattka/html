<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="admin_News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
    <h1>Новости <a href="addNews.aspx">Добавить новую</a></h1>
    <asp:Panel ID="pNews" runat="server" />
	<script>
        window.onload = function () {
            const urlParams = new URLSearchParams(window.location.search);
            const status = urlParams.get('status');
            switch (status) {
                case "edited": alert("Новость успешно отредактирована!"); break;
                case "added": alert("Новость успешно добавлена!"); break;
                case "deleted": alert("Новость успешно удалена!"); break;
                case "published": alert("Новость успешно опубликована!"); break;
                case "unpublished": alert("Публикация новости успешно приостановлена!"); break;
            }
        }
        
            if (window.location.href.indexOf("?")>0)
                window.location.replace(window.location.href.substr(0, window.location.href.indexOf("?")));
    </script>
</asp:Content>

