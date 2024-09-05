using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_editPage : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string userId = "2"; // admin
    string pageId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] != null)
            userId = Session["userId"].ToString();
        if (Request.QueryString["pageId"] == null)
            Response.Redirect("pages.aspx");
        else
            pageId = Request.QueryString["pageId"];
        if (tbTitle.Text == "" && tbTime.Text == "")
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
            tbTitle.Text = r["pageTitle"].ToString();
            tbTime.Text = DateTime.Parse(r["timePublished"].ToString()).ToString("dd.MM.yy HH:mm");
            tbDesc.Text = r["pageDesc"].ToString();
            tbText.Text = r["pageText"].ToString();
            tbTags.Text = r["pageTags"].ToString();
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
            Response.Redirect("pages.aspx");
        }
        r.Close();
        conn.Close();

    }

    protected void bSave_Click(object sender, EventArgs e)
    {
        if (checkFormats())
        {
            // обновляем запись в БД
            string SQLstr = "proc_adminUpdatePage @pageId, @pageTitle, @pageDesc, @pageText, @pageTags, @isPublished, @timePublished, @timeEdited, @userEdited, @seoTitle, @seoDesc, @seoKeys, @psevdoName";

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand q = new SqlCommand(SQLstr, conn);
            q.Parameters.AddWithValue("@pageId", pageId);
            q.Parameters.AddWithValue("@pageTitle", tbTitle.Text);
            q.Parameters.AddWithValue("@pageDesc", tbDesc.Text);
            q.Parameters.AddWithValue("@pageText", tbText.Text);
            q.Parameters.AddWithValue("@pageTags", tbTags.Text);
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

            Response.Redirect("pages.aspx?status=edited");
        }
    }

    protected void bCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("pages.aspx");
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