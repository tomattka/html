using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Articles : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string catId = "0", psevdoName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.RouteData.Values["psevdoName"] != null)
            psevdoName = Page.RouteData.Values["psevdoName"].ToString();
        if (psevdoName == "all")
            psevdoName = ""; // все статьи грузятся при catId=0
        if (Request.QueryString["catId"] != null)
            catId = Request.QueryString["catId"];
        LoadArtCats();
        LoadArticles();
        loadSeo("Статьи о том, как успешно торговать на forex. Полезные советы для начинающих трейдеров и актуальная информация о рынке форекс.", "");
    }

    protected void LoadArtCats()
    {
        lCats.Text = "";
        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "select catId, catTitle, psevdoName from articleCategories order by catTitle";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        SqlDataReader r = q.ExecuteReader();
        while (r.Read())
        {
            lCats.Text += "<li><a href=\"" + getCategoryAdress(r["catId"].ToString(), r["psevdoName"].ToString()) + "\" style=\"margin-right: 20px; font-size: 18px;\">" + r["catTitle"].ToString() + "</a></li>";

        }
        r.Close();
        conn.Close();
    }

    protected void LoadArticles()
    {
        lArticles.Text = "";

        // Loading a category title
        LoadCatTitle();

        // Loading articles

        string SQLstr = "proc_getArticlesByPsevdo @catId, @psevdoName";

        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@catId", catId);
        q.Parameters.AddWithValue("@psevdoName", psevdoName);

        SqlDataReader r = q.ExecuteReader();
        while (r.Read())
        {
            lArticles.Text += "<li><a href=\"" + getPageAdress(r["articleId"].ToString(), r["psevdoName"].ToString()) + "\">";
            lArticles.Text += r["articleTitle"].ToString() + "</a><span>" + r["articleDesc"] + "</span><time>Опубликовано " + DateTime.Parse(r["timePublished"].ToString()).ToString("dd.MM.yyyy") + "</time></li>";
        }

        r.Close();
        conn.Close();

        // deleting the odd bottom lane

        if (lArticles.Text != "")
        {
            lArticles.Text = lArticles.Text.Insert(lArticles.Text.LastIndexOf("<li>") + 3, " style=\"border:none;\"");
        }
        else
            lArticles.Text = "<p style=\"font-size: 18px; text-align: center;\">В данной категории пока нет статей.</p>";
    }

    protected string getPageAdress(string articleId, string psevdoName)
    {
        string res = "";
        if (psevdoName != "")
            res += "/article/" + psevdoName;
        else
            res += "/article.aspx?articleId=" + articleId;
        return res;
    }

    protected string getCategoryAdress(string catId, string psevdoName)
    {
        string res = "";
        if (psevdoName != "")
            res += "/articles/" + psevdoName;
        else
            res += "/articles.aspx?catId=" + catId;
        return res;
    }

    protected void LoadCatTitle()
    {
        lCatTitle.Text = "Cтатьи сайта PTS5T";

        if (catId != "0" || psevdoName != "")
        {
            SqlConnection conn = new SqlConnection(connStr);
            string SQLstr = "select catTitle from articleCategories where ";
            if (catId != "0")
                SQLstr += "catId=@catId";
            if (psevdoName != "")
                SQLstr += "psevdoName=@psevdoName";
            conn.Open();
            SqlCommand q = new SqlCommand(SQLstr, conn);
            if (catId != "0")
                q.Parameters.AddWithValue("@catId", catId);
            if (psevdoName != "")
                q.Parameters.AddWithValue("@psevdoName", psevdoName);
            SqlDataReader r = q.ExecuteReader();
            if (r.Read())
            {
                lCatTitle.Text = r["catTitle"].ToString();

            }
            r.Close();
            conn.Close();
        }
    }

    protected void loadSeo(string seoDesc, string seoKeys)
    {
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