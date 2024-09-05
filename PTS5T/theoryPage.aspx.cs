using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class TheoryPage : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string theoryId = "0", psevdoName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.RouteData.Values["psevdoName"] != null)
            psevdoName = Page.RouteData.Values["psevdoName"].ToString();
        if (Request.QueryString["theoryId"] != null)
            theoryId = Request.QueryString["theoryId"];
        if (theoryId == "0" && psevdoName == "")
            Response.Redirect("/theory");
        else
            loadTheory();
    }

    protected void loadTheory()
    {
        string SQLstr = "proc_adminLoadTheory @theoryId, @psevdoName";
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@theoryId", theoryId);
        q.Parameters.AddWithValue("@psevdoName", psevdoName);
        SqlDataReader r = q.ExecuteReader();
        if (r.Read())
        {
            if (bool.Parse(r["isPublished"].ToString()))
            {
                lTitle.Text = r["theoryTitle"].ToString();
                lText.Text = r["theoryText"].ToString();
                lTime.Text = "<p class=\"published\">Опубликовано <time>" +  DateTime.Parse(r["timePublished"].ToString()).ToString("dd.MM.yy") + "</time></p>";
                loadSeo(r["seoTitle"].ToString(), r["seoDesc"].ToString(), r["seoKeys"].ToString(), r["theoryTitle"].ToString(), r["theoryDesc"].ToString(), r["theoryTags"].ToString());
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
            Response.Redirect("/theory");
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