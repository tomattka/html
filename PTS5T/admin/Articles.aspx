<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin.master" AutoEventWireup="true" CodeFile="Articles.aspx.cs" Inherits="admin_News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
    <h1>Статьи <a href="addArticle.aspx">Добавить новую</a><a href="ArtCats.aspx">Категории</a></h1>
    <asp:Panel ID="pArticles" runat="server" />
	<nav class="pages" style="display: none;"><a href="#">&laquo;</a><a href="#">1</a><a href="#">2</a><a href="#" class="active">3</a><a href="#">4</a><a href="#">5</a><a href="#">&raquo;</a></nav>
    <script>
        window.onload = function () {
            const urlParams = new URLSearchParams(window.location.search);
            const status = urlParams.get('status');
            switch (status) {
                case "edited": alert("Статья успешно отредактирована!"); break;
                case "added": alert("Статья успешно добавлена!"); break;
                case "deleted": alert("Статья успешно удалена!"); break;
                case "published": alert("Статья успешно опубликована!"); break;
                case "unpublished": alert("Публикация статьи успешно приостановлена!"); break;
            }
            
            if (window.location.href.indexOf("?")>0)
                window.location.replace(window.location.href.substr(0, window.location.href.indexOf("?")));
        }
    </script>
</asp:Content>

