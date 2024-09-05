<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TextEditor.aspx.cs" Inherits="admin_TextEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label>Текст: <asp:TextBox ID="tbText" runat="server" CssClass="wideBlock" TextMode="MultiLine" /></label>
        </div>
    </form>
    <script src="https://cloud.tinymce.com/5/tinymce.min.js?apiKey=h2zggpush1xxs30f2cif2znq8t6kocmfm4lszg1lx0bnolog"></script>
    <script src="editor_init.js"></script>
</body>
</html>
