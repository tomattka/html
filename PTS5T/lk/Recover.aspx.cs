using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lk_Recover : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LbRecover_Click(object sender, EventArgs e)
    {
        if (new cProcs().MailExists(tbMail.Text)) // проверить, есть ли е-мейл в базе
        {
            // сгенерировать новый пароль
            string strPass = (new cProcs()).GenerateRandomCode(8);

            // записать новый пароль в базу
            SqlConnection conn = new SqlConnection(connStr);
            string SQLstr = "update users set userPass=@userPass where userMail=@userMail";
            SqlCommand q = new SqlCommand(SQLstr, conn);
            q.Parameters.AddWithValue("@userMail", tbMail.Text);
            q.Parameters.AddWithValue("@userPass", strPass);
            conn.Open();
            q.ExecuteNonQuery();
            conn.Close();

            // Формируем текст
            string strText = "<p>Здравствуйте! Вы запросили восстановление пароля на сайте <a href=\"http://pts5t.ru\">pts5t.ru</a></p>";
            strText += "<p>Для входа на сайт для вас сгенерирован временный пароль. Используйте следующие данные для входа:</p>";
            strText += "<p><b>Ваш логин:</b> " + tbMail.Text + "</b><br />";
            strText += "<b>Ваш пароль:</b> " + strPass + "</p>";
            strText += "<p>Вы можете изменить пароль в личном кабинете.</p>";

            // Отправляем письмо
            (new cProcs()).SendLetter(tbMail.Text, "Восстановление пароля", strText);

            // переадресовать на соответствующую страницу
            Response.Redirect("/page/password_recovery");

        }
        else // если нет, вывести ошибку
            lErr.Text = "<p style=\"font-size: 12px;\"><span style=\"color: red; font-size: 14px;\">Данный адрес электронной почты не зарегистрирован.</span> <br /> Попробуйте ввести другой e-mail, или воспользуйтесь <a href=\"/#getit\">формой для регистрации</a></p>";


    }
}