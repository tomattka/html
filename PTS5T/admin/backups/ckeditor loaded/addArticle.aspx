<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin.master" AutoEventWireup="true" CodeFile="addArticle.aspx.cs" Inherits="admin_addArticle" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
    <h1>Добавить статью</h1>
				<div class="editForm">
					<label>Название статьи: 
                    <asp:Literal ID="lErrTitle" runat="server" Visible="false"><span style="color: red; margin-left: 10px;">введите заголовок!</span></asp:Literal>
                    <asp:TextBox ID="tbTitle" runat="server" CssClass="wideStroke" /></label>
					<label>Дата: 
                    <asp:Literal ID="lErrTime" runat="server" Visible="false"><span style="color: red; margin-left: 10px;"> неверный формат даты!</span></asp:Literal>
                    <asp:TextBox ID="tbTime" runat="server" CssClass="wideStroke" /></label>
					<label>Категория: <asp:DropDownList ID="lbCat" runat="server" CssClass="wideStroke" /></label>
					<label>Краткое описание: <asp:TextBox ID="tbDesc" runat="server" CssClass="wideBlock" TextMode="MultiLine" /></label>
					<label>Текст: <asp:TextBox ID="tbText" runat="server" CssClass="wideBlock" TextMode="MultiLine" /></label>
					<label>Теги: <asp:TextBox ID="tbTags" runat="server" CssClass="wideStroke" /></label>
					<label><asp:CheckBox ID="chPublish" runat="server" Checked="true" /> Опубликовать</label>
					<a href="#" id="show" onclick="showAdditional(); return false">Дополнительно</a><br>
					<div id="additional" style="display: none;">		
					<label>Seo Title: <asp:TextBox ID="tbSeoTitle" runat="server" CssClass="wideStroke" /></label>
					<label>Seo Keys: <asp:TextBox ID="tbSeoKeys" runat="server" CssClass="wideStroke" /></label>
					<label>Seo Desc: <asp:TextBox ID="tbSeoDesc" runat="server" CssClass="wideBlock" TextMode="MultiLine" /></label></div>
					<div class="buttons"><asp:Button ID="bAdd" runat="server" Text="Добавить" OnClick="bAdd_Click" /><asp:Button ID="bCancel" runat="server" Text="Отменить" OnClick="bCancel_Click" /></div>
				</div>
   <script src="/ckeditor/ckeditor.js"></script>
    <script>
         CKEDITOR.replace('ctl00$cph1$tbText');
      </script>
</asp:Content>

