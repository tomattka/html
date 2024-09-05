using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckHttps();
    }

    protected void CheckHttps()
    {
        switch (Request.Url.Scheme)
        {
            case "http":
                var path = "https://" + Request.Url.Host + "/";
                Response.Status = "301 Moved Permanently";
                Response.AddHeader("Location", path);
                break;
        }
    }

    protected void fGet_KeyDown(object sender, EventArgs e)
    {
        Response.Redirect("/page/contacts");
    }

    protected void lbGet_Click(object sender, EventArgs e)
    {
        // Проверить, есть ли такой E-mail в базе
        if (!new cProcs().MailExists(tbMail.Text))
        {
            // Генерируем случайный пароль
            string strPass = (new cProcs()).GenerateRandomCode(8);

            // Записываем пользователя в базу
            AddNewUser(tbName.Text, tbMail.Text, strPass);

            // Формируем текст
            string strText = "<p>Здравствуйте, " + tbName.Text + "! Вы успешно зарегистрированы на сайте ПТС-5Т.</p>";
            strText += "<p>Скачать систему ПТС-5Т можно по ссылке: <a href=\"https://pts5t.ru/program/PTS5T.zip\">https://pts5t.ru/program/PTS5T.zip</a></p>";
            strText += "<p>&nbsp;</p>";
            strText += "<p>Данные учётной записи:</p>";
            strText += "<p><b>Ваш логин:</b> " + tbMail.Text + "</b><br />";
            strText += "<b>Пароль:</b> " + strPass + "</p>";
            strText += "<p>Вы можете изменить пароль в личном кабинете.</p>";
            strText += "<p>&nbsp;</p>";
            strText += "<p>Инструкции по пользованию системой ПТС-5Т:  <a href=\"https://pts5t.ru/page/instructions\">https://pts5t.ru/page/instructions</a></p>";
            strText += "<p>&nbsp;</p>";
            strText += "<p style=\"font-size: 16px;\">Если потребуется помощь с установкой, пишите на e-mail <b>pts5t@yandex.ru</b> или в скайп <b>forexpts5t</b>.</p>";
            //strText += "<p><b>В подарок за регистрацию вам начислено 500 бонусных рублей!</b></p>";

            // Отправляем письмо
            (new cProcs()).SendLetter(tbMail.Text, "Регистрация на сайте ПТС-5Т", strText);
            SendNotificationToAdmin("pts5t@yandex.ru", tbName.Text, tbMail.Text);
            Response.Redirect("/page/registration_success");
        }
        else
            Response.Redirect("/page/already_registered");
    }

    protected void SendNotificationToAdmin(string adminMail, string userName, string userMail)
    {
        string strText = "<p>На сайте ПТС-5Т зарегистрирован новый пользователь.</p>";
        strText += "<p>Имя: " + userName + "</p><p>E-mail: " + userMail + "</p>";
        string strSubj = "На сайте ПТС-5Т зарегистрирован новый пользователь";
        (new cProcs()).SendLetter(adminMail, strSubj, strText);
    }

    protected void AddNewUser(string strName, string strMail, string strPass)
    {
        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "proc_siteAddNewUser @userName, @userMail, @userPass, @timeAdded";
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userName", strName);
        q.Parameters.AddWithValue("@userMail", strMail);
        q.Parameters.AddWithValue("@userPass", strPass);
        q.Parameters.AddWithValue("@timeAdded", DateTime.Now);
        conn.Open();
        q.ExecuteNonQuery();
        conn.Close();
    }
}