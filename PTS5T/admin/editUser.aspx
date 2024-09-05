<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin.master" AutoEventWireup="true" CodeFile="editUser.aspx.cs" Inherits="admin_editUser"  ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
    <h1>Изменить данные пользователя</h1>
    	<div class="editForm">
            <label>Фамилия: <asp:TextBox ID="tbFamily" runat="server" CssClass="wideStroke" /></label>
			<label>Имя: 
                <asp:Literal ID="lErrName" runat="server" Visible="false"><span style="color: red; margin-left: 10px;">введите имя!</span></asp:Literal>
                <asp:TextBox ID="tbName" runat="server" CssClass="wideStroke" /></label>
			<label>Отчество: <asp:TextBox ID="tbSecondName" runat="server" CssClass="wideStroke" /></label>
            <label>Пол: <asp:DropDownList ID="ddlGender" runat="server">
                        <asp:ListItem Value="Мужской" />
                        <asp:ListItem Value="Женский" />
                        </asp:DropDownList></label><br /><br />
            <label>E-Mail: 
                <asp:Literal ID="lErrMail" runat="server" Visible="false"><span style="color: red; margin-left: 10px;">введите адрес почты!</span></asp:Literal>
                <asp:TextBox ID="tbMail" runat="server" CssClass="wideStroke" /></label>
            <label><asp:CheckBox ID="chValid" runat="server" /> Почта подтверждена</label><br /><br />
            <label>Телефон: <asp:TextBox ID="tbPhone" runat="server" CssClass="wideStroke" /></label>
            <label>Скайп: <asp:TextBox ID="tbSkype" runat="server" CssClass="wideStroke" /></label>
            <label>Дата рождения: 
                <asp:Literal ID="lErrDate" runat="server" Visible="false"><span style="color: red; margin-left: 10px;">неверный формат даты!</span></asp:Literal>
                <asp:TextBox ID="tbBirth" runat="server" CssClass="wideStroke" /></label>
            <label>Город: <asp:TextBox ID="tbCity" runat="server" CssClass="wideStroke" /></label>
            <label>Соцсети: <asp:TextBox ID="tbSocial" runat="server" CssClass="wideBlock" TextMode="MultiLine" /></label>
            <label>Другие контакты: <asp:TextBox ID="tbOtherContacts" runat="server" CssClass="wideBlock" TextMode="MultiLine" /></label>            
            <label><asp:CheckBox ID="chActive" runat="server" /> Доступ к сайту разрешён</label>
            
            <div class="buttons" style="margin-bottom:30px;"><asp:Button ID="bSave" runat="server" Text="Сохранить" OnClick="bSave_Click" /><asp:Button ID="bCancel" runat="server" Text="Отменить" OnClick="bCancel_Click" /></div>
			
            <label>Старый пароль: 
                <asp:Literal ID="lErrOldPass" runat="server" Visible="false"><span style="color: red; margin-left: 10px;">неверный пароль!</span></asp:Literal>
                <asp:TextBox ID="tbOldPass" runat="server" CssClass="wideStroke" TextMode="Password" /></label>
			<label>Новый пароль: <asp:Literal ID="lErrNewPass" runat="server" Visible="false"><span style="color: red; margin-left: 10px;">пароль должен содержать не менее 5 символов!</span></asp:Literal>
            <asp:TextBox ID="tbNewPass" runat="server" CssClass="wideStroke" TextMode="Password" /></label>
			<label>Повторите новый пароль: 
                <asp:Literal ID="lErrAgain" runat="server" Visible="false"><span style="color: red; margin-left: 10px;">пароли не совпадают!</span></asp:Literal>
                <asp:TextBox ID="tbNewPassAgain" runat="server" CssClass="wideStroke" TextMode="Password" /></label>
			<asp:Literal ID="lPassSucc" runat="server" Visible="false"><span style="color: green; margin-left: 10px;">Пароль успешно обновлён!</span></asp:Literal>
            
            <div class="buttons"><asp:Button ID="bSavePass" runat="server" Text="Обновить пароль" OnClick="bPassSave_Click" Width="200" /></div>
            <asp:HiddenField ID="hfPass" runat="server" Visible="false" />
            </div>
</asp:Content>

