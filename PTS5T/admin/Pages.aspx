<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin.master" AutoEventWireup="true" CodeFile="Pages.aspx.cs" Inherits="admin_Pages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
    <h1>Страницы <a href="addPage.aspx">Добавить новую</a></h1>
    <asp:Panel ID="pPages" runat="server" />
	<script>
        window.onload = function () {
            const urlParams = new URLSearchParams(window.location.search);
            const status = urlParams.get('status');
            switch (status) {
                case "edited": alert("Страница успешно отредактирована!"); break;
                case "added": alert("Страница успешно добавлена!"); break;
                case "deleted": alert("Страница успешно удалена!"); break;
                case "published": alert("Страница успешно опубликована!"); break;
                case "unpublished": alert("Публикация страницы успешно приостановлена!"); break;
            }
        }
        
            if (window.location.href.indexOf("?")>0)
                window.location.replace(window.location.href.substr(0, window.location.href.indexOf("?")));
    </script>
</asp:Content>

