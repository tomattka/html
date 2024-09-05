<%@ Page Language="C#" AutoEventWireup="true" CodeFile="psevdourl_temp.aspx.cs" Inherits="psevdourl_temp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="position: absolute; width: 800px; margin: 100px auto auto -400px; left: 50%">
            <p><asp:TextBox ID="tbTitle" runat="server" Width="600" /> <asp:Button ID="bConvert" runat="server" Text="Конвертировать" OnClick="bConvert_Click" /></p>
            <p>&nbsp;</p>
            <p><asp:Literal ID="lRes" runat="server" /></p>
        </div>
    </form>
</body>
</html>
