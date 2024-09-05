<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin.master" AutoEventWireup="true" CodeFile="TheoryCats.aspx.cs" Inherits="admin_TheoryCats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
    <h1>Разделы теории <a href="addTheoryCat.aspx">Добавить новый</a></h1>
    <div style="height: 20px;">&nbsp;</div>
    <asp:Panel ID="pCats" runat="server"  />
        <script>
            window.onload = function () {
                const urlParams = new URLSearchParams(window.location.search);
                const status = urlParams.get('status');
                switch (status) {
                    case "edited": alert("Раздел теории успешно отредактирован!"); break;
                    case "added": alert("Раздел теории успешно добавлен!"); break;
                    case "deleted": alert("Раздел теории успешно удален!"); break;
                }
                if (window.location.href.indexOf("?")>0)
                        window.location.replace(window.location.href.substr(0, window.location.href.indexOf("?")));
                }
    </script>
</asp:Content>

