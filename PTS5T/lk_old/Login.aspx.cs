using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lk_Login : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckHttps();
        if (Request.QueryString["do"] == "logout")
            Logout();
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

    protected void lbLogin_Click(object sender, EventArgs e)
    {
        СheckLogin();
    }

    protected void Logout()
    {
        Session["lk_userId"] = "0";
        Response.Redirect("login.aspx");
    }

    protected void СheckLogin()
    {
        if (tbLogin.Text != "" && tbPass.Text != "")
        {
            string SQLstr = "proc_LK_checkAccess @login, @pass";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand q = new SqlCommand(SQLstr, conn);
            q.Parameters.AddWithValue("@login", tbLogin.Text);
            q.Parameters.AddWithValue("@pass", tbPass.Text);
            conn.Open();
            string userId = q.ExecuteScalar().ToString();
            conn.Close();

            if (userId == "0")
            {
                lErr.Text = "<p class=\"err\">Данные для входа указаны неверно.<p>";
            }
            else
                LetUserIn(userId);
        }
        else
            lErr.Text = "<p class=\"err\">Введите логин и пароль.</p>";
    }

    protected void LetUserIn(string userId)
    {
        Session.Timeout = 180;
        Session["lk_userId"] = userId;
        Response.Redirect("download.aspx");
    }
}