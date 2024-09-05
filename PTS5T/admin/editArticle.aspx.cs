using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_editArticle : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string userId = "2"; //Change!
    string articleId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] != null)
            userId = Session["userId"].ToString();
        if (Request.QueryString["articleId"] == null)
            Response.Redirect("articles.aspx");
        else
            articleId = Request.QueryString["articleId"];
        loadCategories();
        if (tbTitle.Text == "" && tbTime.Text == "")
            loadArticle();
    }

    protected void loadArticle()
    {
        string SQLstr = "proc_adminLoadArticle @articleId";
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@articleId", articleId);
        SqlDataReader r = q.ExecuteReader();
        if (r.Read())
        {
            tbTitle.Text = r["articleTitle"].ToString();
            tbTime.Text = DateTime.Parse(r["timePublished"].ToString()).ToString("dd.MM.yy HH:mm");
            lbCat.SelectedValue = r["catTitle"].ToString();
            tbDesc.Text = r["articleDesc"].ToString();
            tbText.Text = r["articleText"].ToString();
            tbTags.Text = r["articleTags"].ToString();
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
            Response.Redirect("articles.aspx");
        }
        r.Close();
        conn.Close();

    }

    protected void loadCategories()
    {
        if (lbCat.Items.Count == 0)
        {
            string SQLstr = "select catId, catTitle from articleCategories order by catTitle";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand q = new SqlCommand(SQLstr, conn);
            SqlDataReader r = q.ExecuteReader();
            while (r.Read())
            {
                lbCat.Items.Add(r["catTitle"].ToString());

            }
            r.Close();
            conn.Close();
        }
    }

    protected void bSave_Click(object sender, EventArgs e)
    {
        if (checkFormats())
        { 
            // обновляем запись в БД
            string SQLstr = "proc_adminUpdateArticle @articleId, @articleTitle, @articleDesc, @articleText, @articleTags, @isPublished, @timePublished, @timeEdited, @userEdited, @seoTitle, @seoDesc, @seoKeys, @catTitle, @psevdoName";

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand q = new SqlCommand(SQLstr, conn);
            q.Parameters.AddWithValue("@articleId", articleId);
            q.Parameters.AddWithValue("@articleTitle", tbTitle.Text);
            q.Parameters.AddWithValue("@articleDesc", tbDesc.Text);
            q.Parameters.AddWithValue("@articleText", tbText.Text);
            q.Parameters.AddWithValue("@articleTags", tbTags.Text);
            q.Parameters.AddWithValue("@isPublished", chPublish.Checked);
            q.Parameters.AddWithValue("@timePublished", DateTime.Parse(tbTime.Text));
            q.Parameters.AddWithValue("@timeEdited", DateTime.Now);
            q.Parameters.AddWithValue("@userEdited", userId);
            q.Parameters.AddWithValue("@seoTitle", tbSeoTitle.Text);
            q.Parameters.AddWithValue("@seoDesc", tbSeoDesc.Text);
            q.Parameters.AddWithValue("@seoKeys", tbSeoKeys.Text);
            q.Parameters.AddWithValue("@catTitle", lbCat.SelectedValue);
            q.Parameters.AddWithValue("@psevdoName", tbPsevdo.Text);
            conn.Open();
            q.ExecuteNonQuery();
            conn.Close();

            Response.Redirect("articles.aspx?status=edited");
        }
    }

    protected void bCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("articles.aspx");
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