<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="admin_Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
			<h1>Пользователи</h1>
                <div class="search">
                    <asp:TextBox ID="tbSearch" runat="server" MaxLength="500" /><asp:Button ID="bSearch" runat="server" Text="Иcкать" CssClass="btn" OnClick="bSearch_Click" /><asp:Button ID="bClear" runat="server" Text="Очистить" CssClass="btn" OnClick="bClear_Click" />
                </div>
                <asp:Panel ID="pUsers" runat="server" />
				<nav class="pages" style="display:none;"><a href="#">&laquo;</a><a href="#">1</a><a href="#">2</a><a href="#" class="active">3</a><a href="#">4</a><a href="#">5</a><a href="#">&raquo;</a></nav>
    <script>
        window.onload = function () {
            const urlParams = new URLSearchParams(window.location.search);
            const status = urlParams.get('status');
            switch (status) {
                case "edited": alert("Данные пользователя успешно сохранены!"); break;
                case "deleted": alert("Пользователь успешно удалён!"); break;
                case "blocked": alert("Пользователь успешно заблокирован!"); break;
                case "unblocked": alert("Пользователь успешно разблокирован!"); break;
                case "admined": alert("Пользователь успешно назначен администратором!"); break;
                case "unadmined": alert("Права администратора успешно отозваны!"); break;
            }
        }

            if (window.location.href.indexOf("?")>0)
                window.location.replace(window.location.href.substr(0, window.location.href.indexOf("?")));
    </script>
</asp:Content>

