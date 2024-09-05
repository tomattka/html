using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class News : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        loadNews();
        loadSeo("Новости сайта ПТС5Т об обновлениях системы и событиях на рынке форекс", "");
    }

    protected void loadNews()
    {
        lNews.Text = "";

        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "select newsId, newsTitle, newsDesc, timePublished, psevdoName from news where isPublished=1 order by timePublished desc";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        SqlDataReader r = q.ExecuteReader();
        while (r.Read())
        {
            lNews.Text += "<li><a href=\"" + getPageAdress(r["newsId"].ToString(), r["psevdoName"].ToString()) + "\">" + r["newsTitle"] + "</a></h2><span>" + r["newsDesc"];
            lNews.Text += "</span><time>Опубликовано  " + DateTime.Parse(r["timePublished"].ToString()).ToString("dd.MM.yyyy") + "</time></li>";

        }
        r.Close();
        conn.Close();

        // deleting the odd bottom lane

        if (lNews.Text != "")
        {
            lNews.Text = lNews.Text.Insert(lNews.Text.LastIndexOf("<li>") + 3, " style=\"border:none;\"");
        }
        else
            lNews.Text = "<p style=\"font-size: 18px; text-align: center;\">Новостей пока нет.</p>";
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