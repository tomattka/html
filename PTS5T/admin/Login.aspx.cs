using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Login : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckHttps();
        if (Request.QueryString["do"] == "logout")
            logout();
    }

    protected void bLogin_Click(object sender, EventArgs e)
    {
        checkLogin();
    }
       
    protected void CheckHttps()
    {
        switch (Request.Url.Scheme)
        {
            case "http":
                var path = "https://" + Request.Url.Host + Request.Url.PathAndQuery;
                Response.Status = "301 Moved Permanently";
                Response.AddHeader("Location", path.ToLower());
                break;
        }
    }

    protected void logout()
    {
        Session["admin_logged"] = "off";
        Session["userName"] = null;
        Session["userId"] = null;
        Response.Redirect("login.aspx");
    }

    protected void checkLogin()
    {
        if (tbLogin.Text != "" && tbPass.Text != "")
        {
            string SQLstr = "proc_checkAccess @login, @pass";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand q = new SqlCommand(SQLstr, conn);
            q.Parameters.AddWithValue("@login", tbLogin.Text);
            q.Parameters.AddWithValue("@pass", tbPass.Text);
            conn.Open();
            string strMess = q.ExecuteScalar().ToString();
            conn.Close();

            if (strMess=="login denied" || strMess == "access denied")
            {
                if (strMess == "access denied")
                    lErr.Text = "<span style=\"color: red; font-size: 12px;\">Пользователю не разрешен доступ в систему управления.</span>";
                else
                    lErr.Text = "<span style=\"color: red; font-size: 12px;\">Неверная пара логин-пароль.</span>";
            }
            else
                letUserIn(strMess);
        }
        else
            lErr.Text = "<span style=\"color: red; font-size: 12px;\">Введите логин и пароль.</span>";
    }

    protected void letUserIn(string userId)
    {
        Session.Timeout = 180;
        Session["admin_logged"] = "on";
        Session["userName"] = tbLogin.Text;
        Session["userId"] = userId;
        Response.Redirect("default.aspx");
    }
}