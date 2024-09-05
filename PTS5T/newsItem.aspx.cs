using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class newsItem : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string newsId = "0", psevdoName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.RouteData.Values["psevdoName"] != null)
            psevdoName = Page.RouteData.Values["psevdoName"].ToString();
        if (Request.QueryString["newsId"] != null)
            newsId = Request.QueryString["newsId"];
        if (newsId == "0" && psevdoName == "")
            Response.Redirect("/news.aspx");
        else
            loadNews();
    }

    protected void loadNews()
    {
        string SQLstr = "proc_adminLoadNews @newsId, @psevdoName";
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@newsId", newsId);
        q.Parameters.AddWithValue("@psevdoName", psevdoName);
        SqlDataReader r = q.ExecuteReader();
        if (r.Read())
        {
            if (bool.Parse(r["isPublished"].ToString()))
            {
                lTitle.Text = r["newsTitle"].ToString();
                lText.Text = r["newsText"].ToString();
                lTime.Text = "<p class=\"published\">Опубликовано <time>" + DateTime.Parse(r["timePublished"].ToString()).ToString("dd.MM.yy") + "</time></p>";
                loadSeo(r["seoTitle"].ToString(), r["seoDesc"].ToString(), r["seoKeys"].ToString(), r["newsTitle"].ToString(), r["newsDesc"].ToString(), r["newsTags"].ToString());
            }
            else
            {
                lTitle.Text = "Новость недоступна";
                lText.Text = "<p style=\"text-align: center;\">Публикация новости приостановлена.</p>";
            }
        }
        else
        {
            r.Close();
            conn.Close();
            Response.Redirect("/news.aspx");
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