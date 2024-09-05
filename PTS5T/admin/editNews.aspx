<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin_wide.master" AutoEventWireup="true" CodeFile="editNews.aspx.cs" Inherits="admin_editNews" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
    <h1>Редактировать новость</h1>
    	<div class="editForm">
			<label>Название новости: 
                <asp:Literal ID="lErrTitle" runat="server" Visible="false"><span style="color: red; margin-left: 10px;">введите заголовок!</span></asp:Literal>
                <asp:TextBox ID="tbTitle" runat="server" CssClass="wideStroke" /></label>
			<label>Дата:
                <asp:Literal ID="lErrTime" runat="server" Visible="false"><span style="color: red; margin-left: 10px;"> неверный формат даты!</span></asp:Literal>
                <asp:TextBox ID="tbTime" runat="server" CssClass="wideStroke" /></label>
			<label>Краткое описание: <asp:TextBox ID="tbDesc" runat="server" CssClass="wideBlock" TextMode="MultiLine" /></label>
			<label>Текст: <asp:TextBox ID="tbText" runat="server" CssClass="wideBlock" TextMode="MultiLine" /></label>
                    <p><a href="ImageUpload.aspx" target="_blank">Загрузка изображений</a></p>
            <br />
			<label>Теги: <asp:TextBox ID="tbTags" runat="server" CssClass="wideStroke" /></label>
			<label><asp:CheckBox ID="chPublish" runat="server" /> Опубликовать</label>
			<a href="#" id="show" onclick="showAdditional(); return false">Дополнительно</a><br>
			<div id="additional" style="display: none;">		
			<label>Seo Title: <asp:TextBox ID="tbSeoTitle" runat="server" CssClass="wideStroke" /></label>
			<label>Seo Keys: <asp:TextBox ID="tbSeoKeys" runat="server" CssClass="wideStroke" /></label>
			<label>Seo Desc: <asp:TextBox ID="tbSeoDesc" runat="server" CssClass="wideBlock" TextMode="MultiLine" /></label>
			<label>Псевдоним: <asp:TextBox ID="tbPsevdo" runat="server" CssClass="wideStroke" /></label>
			</div>
			<div class="buttons"><asp:Button ID="bSave" runat="server" Text="Сохранить" OnClick="bSave_Click" /><asp:Button ID="bCancel" runat="server" Text="Отменить" OnClick="bCancel_Click" /></div>
		</div>
    <script src="https://cloud.tinymce.com/5/tinymce.min.js?apiKey=h2zggpush1xxs30f2cif2znq8t6kocmfm4lszg1lx0bnolog"></script>
    <script src="editor_init.js"></script>
</asp:Content>

