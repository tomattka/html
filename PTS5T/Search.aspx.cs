using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["text"] != null)
            DoSearch(Request.QueryString["text"].ToString());
        else
            lSearched.Text = "Поисковый запрос не задан.";
    }

    protected void DoSearch(string strSearch)
    {
        if (strSearch != "")
        {
            lSearched.Text = "<b>Вы искали: \"</b>" + strSearch + "\"";
            // Убрать лишние символы
            strSearch = strSearch.Replace("\"", "").Replace("'", "").Replace("%", "");
            // Разделить на слова
            string[] sep = { " ", "," };
            string[] arrSearch = strSearch.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            // Сгенерировать поисковый запрос
            string SQLstr = "select top 20 * from view_allContent where 1=1";

            for(int i=0;i<arrSearch.Length;i++)
            {
                SQLstr += " and (pageTitle like '%" + arrSearch[i] + "%' or pageDesc like '%" + arrSearch[i] + "%' or pageText like '%" + arrSearch[i] + "%')";
            }
            SQLstr += " order by  timePublished desc";

            // Вывести результат
            lRes.Text = "";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand q = new SqlCommand(SQLstr, conn);
            SqlDataReader r = q.ExecuteReader();
            while (r.Read())
            {
                lRes.Text += "<li><a href=\"" + getPageAdress(r["pageId"].ToString(), r["psevdoName"].ToString(), r["pageType"].ToString()) + "\">" + r["pageTitle"] + "</a></h2><span>" + r["pageDesc"];
                lRes.Text += "</span><time>Опубликовано  " + DateTime.Parse(r["timePublished"].ToString()).ToString("dd.MM.yyyy") + "</time></li>";

            }
            r.Close();
            conn.Close();

            if (lRes.Text == "")
                lRes.Text = "<p style=\"text-align: center;\">Ничего не найдено</p>";
            else
            {
                int iLastLi = lRes.Text.LastIndexOf("<li>");
                lRes.Text = lRes.Text.Insert(iLastLi + 3, " style=\"border-bottom: none;\"");
            }
        }
        else
            lSearched.Text = "Поисковый запрос не задан.";
    }

    protected string getPageAdress(string pageId, string psevdoName, string pageType)
    {
        string res = "";
        if (psevdoName != "")
            res += "/" + pageType + "/" + psevdoName;
        else
        {
            if (pageType == "news")
                res += "/newsItem.aspx?newsId=" + pageId;
            if (pageType == "page")
                res += "/page.aspx?pageId=" + pageId;
            if (pageType == "article")
                res += "/article.aspx?articleId=" + pageId;
        }

        if (res == "/page/contacts")
            res = "/contacts";

        return res;
    }



}