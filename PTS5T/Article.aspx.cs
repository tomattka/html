using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Article : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string articleId = "0", psevdoName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.RouteData.Values["psevdoName"] != null)
            psevdoName = Page.RouteData.Values["psevdoName"].ToString();
        if (Request.QueryString["articleId"] != null)
            articleId = Request.QueryString["articleId"];
        if (articleId == "0" && psevdoName == "")
            Response.Redirect("/articles.aspx");
        else
            loadArticle();
    }

    protected void loadArticle()
    {
        string SQLstr = "proc_adminLoadArticle @articleId, @psevdoName";
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@articleId", articleId);
        q.Parameters.AddWithValue("@psevdoName", psevdoName);
        SqlDataReader r = q.ExecuteReader();
        if (r.Read())
        {
            if (bool.Parse(r["isPublished"].ToString()))
            {
                lTitle.Text = r["articleTitle"].ToString();
                lText.Text = r["articleText"].ToString();
                lTime.Text = "<p class=\"published\">Опубликовано <time>" +  DateTime.Parse(r["timePublished"].ToString()).ToString("dd.MM.yy") + "</time></p>";
                loadSeo(r["seoTitle"].ToString(), r["seoDesc"].ToString(), r["seoKeys"].ToString(), r["articleTitle"].ToString(), r["articleDesc"].ToString(), r["articleTags"].ToString());
            }
            else
            {
                lTitle.Text = "Статья недоступна";
                lText.Text = "<p style=\"text-align: center;\">Публикация статьи приостановлена.</p>";
            }
        }
        else
        {
            r.Close();
            conn.Close();
            Response.Redirect("/articles.aspx");
        }
        r.Close();
        conn.Close();

    }

    protected void loadSeo(string seoTitle, string seoDesc, string seoKeys, string pageTitle, string pageDesc, string pageKeys)
    {
        if (seoTitle == "")
            seoTitle = pageTitle;
        if (seoDesc == "")
            seoDesc = pageDesc;
        if (seoKeys == "")
            seoKeys = pageKeys;

        // Title
        Page.Title = seoTitle;

        // Add meta description tag
        if (seoDesc != "")
        {
            HtmlMeta metaDescription = new HtmlMeta();
            metaDescription.Name = "Description";
            metaDescription.Content = seoDesc;
            Page.Header.Controls.Add(metaDescription);
        }

        // Add meta keywords tag
        if (seoKeys != "")
        {
            HtmlMeta metaKeywords = new HtmlMeta();
            metaKeywords.Name = "Keywords";
            metaKeywords.Content = seoKeys;
            Page.Header.Controls.Add(metaKeywords);
        }
    }
}