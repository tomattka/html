<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testMail.aspx.cs" Inherits="testMail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="bSend" runat="server" Text="Отправить" OnClick="bSend_Click" />
        </div>
    </form>
</body>
</html>
