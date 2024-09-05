<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin.master" AutoEventWireup="true" CodeFile="theory.aspx.cs" Inherits="admin_Theory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
    <h1>Теория <a href="addTheory.aspx">Добавить страницу</a><a href="theoryCats.aspx">Разделы</a></h1>
    <asp:Panel ID="pArticles" runat="server" />
	<nav class="pages" style="display: none;"><a href="#">&laquo;</a><a href="#">1</a><a href="#">2</a><a href="#" class="active">3</a><a href="#">4</a><a href="#">5</a><a href="#">&raquo;</a></nav>
    <script>
        window.onload = function () {
            const urlParams = new URLSearchParams(window.location.search);
            const status = urlParams.get('status');
            switch (status) {
                case "edited": alert("Страница теории успешно отредактирована!"); break;
                case "added": alert("Страница теории успешно добавлена!"); break;
                case "deleted": alert("Страница теории успешно удалена!"); break;
                case "published": alert("Страница теории успешно опубликована!"); break;
                case "unpublished": alert("Публикация страницы успешно приостановлена!"); break;
            }
            
            if (window.location.href.indexOf("?")>0)
                window.location.replace(window.location.href.substr(0, window.location.href.indexOf("?")));
        }
    </script>
</asp:Content>

