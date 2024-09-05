using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lk_Default : System.Web.UI.Page
{
    string userId = "0";
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        // авторедирект для всех, кто случайно сюда попал (страница отключена)
        Response.Redirect("download.aspx");

        if (Session["lk_userId"] != null)
            userId = Session["lk_userId"].ToString();
        if (userId != "0")
            GetUserData();
    }

    protected void GetUserData()
    {
        // получить имя, логин, количество денег, подписку
        string SQLstr = "proc_LK_getMainData @userId";
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        conn.Open();
        SqlDataReader r = q.ExecuteReader();
        if(r.Read())
        {
            lUserName.Text = GetFullName(r["userName"].ToString(), r["userSecondName"].ToString(), r["userFamily"].ToString());
            lUserMail.Text = r["userMail"].ToString();
            lUserMoney.Text = (new cProcs()).GetMoneyStr(r["userMoney"].ToString());
            lSubTill.Text = GetSubscription(r["subEnd"].ToString());
            conn.Close();
        }
        else
        {
            conn.Close();
            Response.Redirect("login.aspx");
        }
    }

    protected string GetFullName(string strName, string strSecondName, string strFamily)
    {
        string res = strFamily;
        if (res != "")
            res += " ";
        res += strName;
        if (res != "")
            res += " ";
        res += strSecondName;
        return res;
    }

    protected string GetSubscription(string strSub)
    {
        string res = strSub;
        if (res == "")
        {
            res = "нет подписки";
            lSubLinkText.Text = "Получить подписку";
        }
        else
        {
            DateTime dt = DateTime.Parse(strSub);
            if (dt < DateTime.Now)
                res = "подписка истекла";
            else
                res = "до " + dt.ToString("dd.MM.yy");
            lSubLinkText.Text = "Продлить подписку";
        }
        return res;
    }
}