using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Theory : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string catId = "0", psevdoName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.RouteData.Values["psevdoName"] != null)
            psevdoName = Page.RouteData.Values["psevdoName"].ToString();
        if (psevdoName == "all")
            psevdoName = "";
        if (Request.QueryString["catId"] != null)
            catId = Request.QueryString["catId"];
        LoadTheoryCats();
        LoadTheory();
        loadSeo("Теория ПТС5Т, автоматическая торговля на forex.", "");
    }

    protected void LoadTheoryCats()
    {
        lCats.Text = "";
        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "select catId, catTitle, psevdoName from theoryCategories order by catTitle";
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

    protected void LoadTheory()
    {
        lTheory.Text = "";

        // Loading a category title
        LoadCatTitle();

        // Loading articles

        string SQLstr = "proc_getTheoryByPsevdo @catId, @psevdoName";

        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@catId", catId);
        q.Parameters.AddWithValue("@psevdoName", psevdoName);

        SqlDataReader r = q.ExecuteReader();
        while (r.Read())
        {
            lTheory.Text += "<li><a href=\"" + getPageAdress(r["theoryId"].ToString(), r["psevdoName"].ToString()) + "\">";
            lTheory.Text += r["theoryTitle"].ToString() + "</a><span>" + r["theoryDesc"] + "</span><time>Опубликовано " + DateTime.Parse(r["timePublished"].ToString()).ToString("dd.MM.yyyy") + "</time></li>";
        }

        r.Close();
        conn.Close();

        // deleting the odd bottom lane

        if (lTheory.Text != "")
        {
            lTheory.Text = lTheory.Text.Insert(lTheory.Text.LastIndexOf("<li>") + 3, " style=\"border:none;\"");
        }
        else
            lTheory.Text = "<p style=\"font-size: 18px; text-align: center;\">В данном разделе пока нет страниц.</p>";
    }

    protected string getPageAdress(string articleId, string psevdoName)
    {
        string res = "";
        if (psevdoName != "")
            res += "/theory-page/" + psevdoName;
        else
            res += "/theoryPage.aspx?theoryId=" + articleId;
        return res;
    }

    protected string getCategoryAdress(string catId, string psevdoName)
    {
        string res = "";
        if (psevdoName != "")
            res += "/theory/" + psevdoName;
        else
            res += "/theory.aspx?catId=" + catId;
        return res;
    }

    protected void LoadCatTitle()
    {
        lCatTitle.Text = "Теория PTS5T";

        if (catId != "0" || psevdoName != "")
        {
            SqlConnection conn = new SqlConnection(connStr);
            string SQLstr = "select catTitle from theoryCategories where ";
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