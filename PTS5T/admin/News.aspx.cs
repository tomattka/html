using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_News : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string userId = "2"; //admin

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] != null)
            userId = Session["userId"].ToString();
        loadNews();
    }

    protected void loadNews()
    {
        pNews.Controls.Clear();

        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "proc_adminGetNews";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        SqlDataReader r = q.ExecuteReader();
        while (r.Read())
        {
            Literal l = new Literal();
            l.Text = "<div class=\"newsItem\"><h2><a href=\"" + getPageAdress(r["newsId"].ToString(), r["psevdoName"].ToString()) + "\" target=\"_blank\">";
            l.Text += r["newsTitle"].ToString() + "</a></h2><span>";
            pNews.Controls.Add(l);

            ImageButton ib = new ImageButton();
            ib.ID = "ibEdit" + r["newsId"].ToString();
            ib.ImageUrl = "img/edit.gif";
            ib.AlternateText = ib.ToolTip = "Редактировать";
            ib.Click += ibEdit_Click;
            pNews.Controls.Add(ib);

            ib = new ImageButton();
            ib.ID = "ibPublish" + r["newsId"].ToString();

            bool isPublished = true;
            isPublished = bool.Parse(r["isPublished"].ToString());

            string askText = "";
            if (isPublished)
            {
                ib.ImageUrl = "img/pause.gif";
                askText = "Снять с публикации новость \"" + r["newsTitle"].ToString() + "\"?";
                ib.AlternateText = ib.ToolTip = "Снять с публикации";
            }
            else
            {
                ib.ImageUrl = "img/play.gif";
                askText = "Опубликовать новость \"" + r["newsTitle"].ToString() + "\"?";
                ib.AlternateText = ib.ToolTip = "Опубликовать";
            }

            ib.OnClientClick = "return confirm('" + askText + "')";
            ib.Click += ibPublish_Click;
            pNews.Controls.Add(ib);

            ib = new ImageButton();
            ib.ID = "ibDelete" + r["newsId"].ToString();
            ib.ImageUrl = "img/delete.gif";
            ib.AlternateText = ib.ToolTip = "Удалить";

            askText = "Удалить новость \"" + r["newsTitle"].ToString() + "\"?";
            ib.OnClientClick = "return confirm('" + askText + "')";
            ib.Click += ibDelete_Click;
            pNews.Controls.Add(ib);

            l = new Literal();
            l.Text = "</span>";
            l.Text += "<p style=\"font-size: 12px; margin: 5px auto; color: #787878\">" + Request.Url.Host + getPageAdress(r["newsId"].ToString(), r["psevdoName"].ToString()) + "</p>";
            l.Text += "<p><time>" + DateTime.Parse(r["timePublished"].ToString()).ToString("dd MMMM yyyy в HH:mm") + "</time> опубликовал";

            bool isMale = true;
            if (r["isUserMale"].ToString() != "")
                isMale = bool.Parse(r["isUserMale"].ToString());
            if (isMale != true)
                l.Text += "a";

            l.Text += " <a href=\"editUser.aspx?userId=" + r["userId"].ToString() + "\"><b>" + r["userName"] + "</b></a></p></div>";
            pNews.Controls.Add(l);

        }
        r.Close();
        conn.Close();
    }

    protected string getPageAdress(string newsId, string psevdoName)
    {
        string res = "";
        if (psevdoName != "")
            res += "/news/" + psevdoName;
        else
            res += "/newsItem.aspx?newsId=" + newsId;
        return res;
    }

    protected void ibEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        Response.Redirect("editNews.aspx?newsId=" + ib.ID.Remove(0, 6));
    }

    protected void ibPublish_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        string newsId = ib.ID.Remove(0, 9);
        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "proc_adminChangeNewsPublishStatus @newsId, @userId, @eventTime";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@newsId", newsId);
        q.Parameters.AddWithValue("@userId", userId);
        q.Parameters.AddWithValue("@eventTime", DateTime.Now);
        bool isPublished = bool.Parse(q.ExecuteScalar().ToString());
        conn.Close();
        if (isPublished)
            Response.Redirect("news.aspx?status=published");
        else
            Response.Redirect("news.aspx?status=unpublished");

    }

    protected void ibDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        string articleId = ib.ID.Remove(0, 8);
        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "proc_adminDeleteNews @newsId, @userId, @eventTime";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@newsId", articleId);
        q.Parameters.AddWithValue("@userId", userId);
        q.Parameters.AddWithValue("@eventTime", DateTime.Now);
        q.ExecuteNonQuery();

        conn.Close();
        Response.Redirect("news.aspx?status=deleted");
    }

}