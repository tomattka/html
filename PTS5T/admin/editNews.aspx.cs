using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_editNews : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string userId = "2"; // admin
    string newsId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] != null)
            userId = Session["userId"].ToString();
        if (Request.QueryString["newsId"] == null)
            Response.Redirect("news.aspx");
        else
            newsId = Request.QueryString["newsId"];
        if (tbTitle.Text == "" && tbTime.Text == "")
            loadNews();
    }

    protected void loadNews()
    {
        string SQLstr = "proc_adminLoadNews @newsId";
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@newsId", newsId);
        SqlDataReader r = q.ExecuteReader();
        if (r.Read())
        {
            tbTitle.Text = r["newsTitle"].ToString();
            tbTime.Text = DateTime.Parse(r["timePublished"].ToString()).ToString("dd.MM.yy HH:mm");
            tbDesc.Text = r["newsDesc"].ToString();
            tbText.Text = r["newsText"].ToString();
            tbTags.Text = r["newsTags"].ToString();
            chPublish.Checked = bool.Parse(r["isPublished"].ToString());
            tbSeoTitle.Text = r["seoTitle"].ToString();
            tbSeoDesc.Text = r["seoDesc"].ToString();
            tbSeoKeys.Text = r["seoKeys"].ToString();
            tbPsevdo.Text = r["psevdoName"].ToString();
        }
        else
        {
            r.Close();
            conn.Close();
            Response.Redirect("news.aspx");
        }
        r.Close();
        conn.Close();

    }

    protected void bSave_Click(object sender, EventArgs e)
    {
        if (checkFormats())
        {
            // обновляем запись в БД
            string SQLstr = "proc_adminUpdateNews @newsId, @newsTitle, @newsDesc, @newsText, @newsTags, @isPublished, @timePublished, @timeEdited, @userEdited, @seoTitle, @seoDesc, @seoKeys, @psevdoName";

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand q = new SqlCommand(SQLstr, conn);
            q.Parameters.AddWithValue("@newsId", newsId);
            q.Parameters.AddWithValue("@newsTitle", tbTitle.Text);
            q.Parameters.AddWithValue("@newsDesc", tbDesc.Text);
            q.Parameters.AddWithValue("@newsText", tbText.Text);
            q.Parameters.AddWithValue("@newsTags", tbTags.Text);
            q.Parameters.AddWithValue("@isPublished", chPublish.Checked);
            q.Parameters.AddWithValue("@timePublished", DateTime.Parse(tbTime.Text));
            q.Parameters.AddWithValue("@timeEdited", DateTime.Now);
            q.Parameters.AddWithValue("@userEdited", userId);
            q.Parameters.AddWithValue("@seoTitle", tbSeoTitle.Text);
            q.Parameters.AddWithValue("@seoDesc", tbSeoDesc.Text);
            q.Parameters.AddWithValue("@seoKeys", tbSeoKeys.Text);
            q.Parameters.AddWithValue("@psevdoName", tbPsevdo.Text);
            conn.Open();
            q.ExecuteNonQuery();
            conn.Close();

            Response.Redirect("news.aspx?status=edited");
        }
    }

    protected void bCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("news.aspx");
    }

    protected bool checkFormats()
    {
        lErrTitle.Visible = false;
        lErrTime.Visible = false;

        bool res = true;
        if (tbTitle.Text == "")
        {
            lErrTitle.Visible = true;
            res = false;
        }
        DateTime dt;
        bool res2 = DateTime.TryParse(tbTime.Text, out dt);
        if (!res2)
        {
            lErrTime.Visible = true;
            res = false;
        }

        return res;
    }

}