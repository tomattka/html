using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_ArticleCats : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string userId = "2"; //admin

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] != null)
            userId = Session["userId"].ToString();
        loadCats();
    }

    protected void loadCats()
    {
        pCats.Controls.Clear();

        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "select catId, catTitle, isPublished, psevdoName from articleCategories order by catTitle";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        SqlDataReader r = q.ExecuteReader();
        while (r.Read())
        {
            Literal l = new Literal();
            l.Text = "<div class=\"newsItem\"><h2><a href=\"" + getPageAdress(r["catId"].ToString(), r["psevdoName"].ToString()) + "\" target=\"_blank\">" + r["catTitle"].ToString() + "</a></h2><span>";
            pCats.Controls.Add(l);

            if (r["catId"].ToString() != "1")
            {
                ImageButton ib = new ImageButton();
                ib.ID = "ibEdit" + r["catId"].ToString();
                ib.ImageUrl = "img/edit.gif";
                ib.AlternateText = ib.ToolTip = "Редактировать";
                ib.Click += ibEdit_Click;
                pCats.Controls.Add(ib);

                ib = new ImageButton();
                ib.ID = "ibDelete" + r["catId"].ToString();
                ib.ImageUrl = "img/delete.gif";
                ib.AlternateText = ib.ToolTip = "Удалить";
                string askText = "Удалить категорию \"" + r["catTitle"].ToString() + "\"?";
                ib.OnClientClick = "return confirm('" + askText + "')";
                ib.Click += ibDelete_Click;
                pCats.Controls.Add(ib);
            }

            l = new Literal();
            l.Text = "</span>";
            l.Text += "<p style=\"font-size: 12px; margin: 5px auto; color: #787878\">" + Request.Url.Host + getPageAdress(r["catId"].ToString(), r["psevdoName"].ToString()) + "</p>";
            l.Text += "<p style=\"height: 1px;\">&nbsp;</p></div>";
            pCats.Controls.Add(l);
        }
        r.Close();
        conn.Close();
    }

    protected string getPageAdress(string catId, string psevdoName)
    {
        string res = "";
        if (psevdoName != "")
            res += "/articles/" + psevdoName;
        else
            res += "/articles.aspx?catId=" + catId;
        return res;
    }

    protected void ibEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = (sender as ImageButton);
        Response.Redirect("editArtCat.aspx?catId=" + ib.ID.Remove(0, 6));
    }

    protected void ibDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        string catId = ib.ID.Remove(0, 8);
        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "proc_adminDeleteArtCat @catId, @userId, @eventTime";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@catId", catId);
        q.Parameters.AddWithValue("@userId", userId);
        q.Parameters.AddWithValue("@eventTime", DateTime.Now);
        q.ExecuteNonQuery();

        conn.Close();
        Response.Redirect("artcats.aspx?status=deleted");
    }

}