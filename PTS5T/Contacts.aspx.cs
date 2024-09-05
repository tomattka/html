using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Contacts : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string pageId="10";

    protected void Page_Load(object sender, EventArgs e)
    {
        loadPage();
    }

    protected void loadPage()
    {
        string SQLstr = "proc_adminLoadPage @pageId";
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@pageId", pageId);
        SqlDataReader r = q.ExecuteReader();
        if (r.Read())
        {
            lTitle.Text = r["pageTitle"].ToString();
            lText.Text = r["pageText"].ToString();
            loadSeo(r["seoTitle"].ToString(), r["seoDesc"].ToString(), r["seoKeys"].ToString(), r["pageTitle"].ToString(), r["pageDesc"].ToString(), r["pageTags"].ToString());
        }
        else
        {
            r.Close();
            conn.Close();
            Response.Redirect("default.aspx");
        }
        r.Close();
        conn.Close();

    }

    protected void lbSend_Click(object sender, EventArgs e)
    {
        // Сюда отправляются сообщения:
        string strMailTo = "pts5t@yandex.ru";

        // Формируем текст
        string strText = "<p><b>Имя:</b> " + tbName.Text + "</p>";
        strText += "<p><b>E-Mail:</b> " + tbMail.Text + "</p>";
        strText += "<p><b>Текст сообщения:</b><br /><i>" + tbMessage.Text + "</i></p>";

        // Отправляем письмо
        bool sent = (new cProcs()).SendLetter(strMailTo, "Сообщение с сайта ПТС5Т", strText);

        if (sent)
            Response.Redirect("/page/message_sent");
        else
            lErr.Text = "<p style=\"color: red; margin: 0; \">Возникла ошибка при отправке сообщения. Попробуйте позднее, или свяжитесь с нами другим способом.</p>";
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