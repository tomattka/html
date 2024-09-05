<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin.master" AutoEventWireup="true" CodeFile="ArtCats.aspx.cs" Inherits="admin_ArticleCats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
    <h1>Категории <a href="addArtCat.aspx">Добавить новую</a></h1>
    <div style="height: 20px;">&nbsp;</div>
    <asp:Panel ID="pCats" runat="server"  />
        <script>
            window.onload = function () {
                const urlParams = new URLSearchParams(window.location.search);
                const status = urlParams.get('status');
                switch (status) {
                    case "edited": alert("Категория успешно отредактирована!"); break;
                    case "added": alert("Категория успешно добавлена!"); break;
                    case "deleted": alert("Категория успешно удалена!"); break;
                }
                if (window.location.href.indexOf("?")>0)
                        window.location.replace(window.location.href.substr(0, window.location.href.indexOf("?")));
                }
    </script>
</asp:Content>

