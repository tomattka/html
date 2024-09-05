<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
    <h1>Добро пожаловать!</h1>
    <p>&nbsp;</p>
    <p>Вы находитесь на главной странице системы управления сайтом ПТС5Т. Здесь для удобства представлены быстрые действия:</p>
    <table style="margin: 20px 0px 200px 0px;">
        <tr>
            <td style="width: 300px; vertical-align: top;">
                <p>— <a href="addNews.aspx">Добавить новость</a></p>
                <p>— <a href="addArticle.aspx">Добавить статью</a></p>
                <p>— <a href="addPage.aspx">Добавить страницу</a></p>
                <p>— <a href="addTheory.aspx">Добавить страницу теории</a></p>
                <p>&nbsp;</p>
                <p>— <a href="ImageUpload.aspx">Загрузка изображений</a></p>
            </td>
            <td style="width: 300px; vertical-align: top;">
                <p>— <a href="users.aspx">Управление пользователями</a></p>
                <p>— <a href="events.aspx">События</a></p>
                <p>— <a href="mailing.aspx">Рассылка</a></p>
            </td>
        </tr>
    </table>
    
    <p>&nbsp;</p>
    
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
</asp:Content>

