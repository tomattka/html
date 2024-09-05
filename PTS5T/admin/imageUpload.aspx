<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin.master" AutoEventWireup="true" CodeFile="ImageUpload.aspx.cs" Inherits="admin_ImageUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
    <h1>Загрузка изображения на сервер</h1>    
    <p>&nbsp;</p>
	<div id="additional" style="display: none; float: left; ">		
        <table>
            <tr>
                <td style="padding-right: 20px;">Ширина:<br /><asp:TextBox ID="tbWidth" runat="server" Width="100" Text="1920" /></td>
                <td style="padding-right: 20px;">Высота:<br /><asp:TextBox ID="tbHeight" runat="server" Width="100" Text="1280" /></td>
            </tr>
        </table>
    </div>    
    <a href="#" id="show" onclick="showAdditional(); return false" style="display: block; float: left; padding-top: 21px;">Дополнительно</a>
    <div style="clear:both"></div>

    <p>&nbsp;</p>
    <p><asp:FileUpload ID="fu" runat="server" Font-Size="14" /> <asp:Button ID="bUpload" runat="server" Text="Загрузить" Font-Size="14" OnClick="bUpload_Click" /></p>
    <p>&nbsp;</p>
    <p><asp:TextBox ID="tbLink" runat="server" Width="362" Text="Ссылка на объект появится после загрузки" ForeColor="DimGray" /> <button onclick="copyText('cph1_tbLink'); return false;">Копировать</button></p>
    <script src="//code.jquery.com/jquery-3.3.1.min.js"></script>
	<link rel="stylesheet" href="https://cdn.jsdelivr.net/gh/fancyapps/fancybox@3.5.2/dist/jquery.fancybox.min.css" />
	<script src="https://cdn.jsdelivr.net/gh/fancyapps/fancybox@3.5.2/dist/jquery.fancybox.min.js"></script>
    <p><asp:Literal ID="lPic" runat="server" /></p>
</asp:Content>

