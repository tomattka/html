using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Theory : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string userId = "2"; //admin

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] != null)
            userId = Session["userId"].ToString();
        loadArticles();
    }

    protected void loadArticles()
    {
        pArticles.Controls.Clear();

        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "proc_adminGetTheory";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        SqlDataReader r = q.ExecuteReader();
        while (r.Read())
        {
            Literal l = new Literal();
            l.Text = "<div class=\"newsItem\"><h2><a href=\"" + getPageAdress(r["theoryId"].ToString(), r["psevdoName"].ToString()) + "\" target=\"_blank\">";
            l.Text += r["theoryTitle"].ToString() + "</a></h2><span>";
            pArticles.Controls.Add(l);

            ImageButton ib = new ImageButton();
            ib.ID = "ibEdit" + r["theoryId"].ToString();
            ib.ImageUrl = "img/edit.gif";
            ib.AlternateText = ib.ToolTip = "Редактировать";
            ib.Click += ibEdit_Click;
            pArticles.Controls.Add(ib);

            ib = new ImageButton();
            ib.ID = "ibPublish" + r["theoryId"].ToString();

            bool isPublished = true;
            isPublished = bool.Parse(r["isPublished"].ToString());

            string askText = "";
            if (isPublished)
            {
                ib.ImageUrl = "img/pause.gif";
                askText = "Снять с публикации страницу \"" + r["theoryTitle"].ToString() + "\"?";
                ib.AlternateText = ib.ToolTip = "Снять с публикации";
            }
            else
            {
                ib.ImageUrl = "img/play.gif";
                askText = "Опубликовать страницу \"" + r["theoryTitle"].ToString() + "\"?";
                ib.AlternateText = ib.ToolTip = "Опубликовать";
            }

            ib.OnClientClick = "return confirm('" + askText + "')";
            ib.Click += ibPublish_Click;
            pArticles.Controls.Add(ib);

            ib = new ImageButton();
            ib.ID = "ibDelete" + r["theoryId"].ToString();
            ib.ImageUrl = "img/delete.gif";
            ib.AlternateText = ib.ToolTip = "Удалить";

            askText = "Удалить страницу \"" + r["theoryTitle"].ToString() + "\"?";
            ib.OnClientClick = "return confirm('" + askText + "')";
            ib.Click += ibDelete_Click;
            pArticles.Controls.Add(ib);

            l = new Literal();
            l.Text = "</span>";
            l.Text += "<p style=\"font-size: 12px; margin: 5px auto; color: #787878\">" + Request.Url.Host + getPageAdress(r["theoryId"].ToString(), r["psevdoName"].ToString()) + "</p>";
            l.Text += "<p><time>" + DateTime.Parse(r["timePublished"].ToString()).ToString("dd MMMM yyyy в HH:mm") + "</time> опубликовал";

            bool isMale = true;
            if (r["isUserMale"].ToString() != "")
                isMale = bool.Parse(r["isUserMale"].ToString());
            if (isMale != true)
                l.Text += "a";

            l.Text += " <a href=\"editUser.aspx?userId=" + r["userId"].ToString() + "\"><b>" + r["userName"] + "</b></a></p></div>";
            pArticles.Controls.Add(l);

        }
        r.Close();
        conn.Close();
    }

    protected string getPageAdress(string theoryId, string psevdoName)
    {
        string res = "";
        if (psevdoName != "")
            res += "/theory-page/" + psevdoName;
        else
            res += "/theoryPage.aspx?theoryId=" + theoryId;
        return res;
    }

    protected void ibEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        Response.Redirect("editTheory.aspx?theoryId=" + ib.ID.Remove(0, 6));
    }

    protected void ibPublish_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        string theoryId = ib.ID.Remove(0, 9);
        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "proc_adminChangeTheoryPublishStatus @theoryId, @userId, @eventTime";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@theoryId", theoryId);
        q.Parameters.AddWithValue("@userId", userId);
        q.Parameters.AddWithValue("@eventTime", DateTime.Now);
        bool isPublished = bool.Parse(q.ExecuteScalar().ToString());
        conn.Close();
        if (isPublished)
            Response.Redirect("theory.aspx?status=published");
        else
            Response.Redirect("theory.aspx?status=unpublished");

    }

    protected void ibDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        string theoryId = ib.ID.Remove(0, 8);
        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "proc_adminDeleteTheory @theoryId, @userId, @eventTime";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@theoryId", theoryId);
        q.Parameters.AddWithValue("@userId", userId);
        q.Parameters.AddWithValue("@eventTime", DateTime.Now);
        q.ExecuteNonQuery();

        conn.Close();
        Response.Redirect("theory.aspx?status=deleted");
    }
}