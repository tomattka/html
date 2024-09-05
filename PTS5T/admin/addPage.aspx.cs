using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_addPage : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string userId = "2"; // на случай, если не определится далее

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] != null)
            userId = Session["userId"].ToString();
        if (tbTime.Text == "")
            tbTime.Text = DateTime.Now.ToString("dd.MM.yy HH:mm");
    }

    protected void bAdd_Click(object sender, EventArgs e)
    {
        if (checkFormats())
            addPage();
    }

    protected void bCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("pages.aspx");
    }

    protected void addPage()
    {
        string SQLstr = "proc_addPage @pageTitle, @timePublished, @pageDesc, @pageText, @pageTags, @isPublished, @userPublished, @seoTitle, @seoDesc, @seoKeys, @psevdoName";
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@pageTitle", tbTitle.Text);
        q.Parameters.AddWithValue("@timePublished", DateTime.Parse(tbTime.Text));
        q.Parameters.AddWithValue("@pageDesc", tbDesc.Text);
        q.Parameters.AddWithValue("@pageText", tbText.Text);
        q.Parameters.AddWithValue("@pageTags", tbTags.Text);
        q.Parameters.AddWithValue("@isPublished", chPublish.Checked);
        q.Parameters.AddWithValue("@userPublished", userId);
        q.Parameters.AddWithValue("@seoTitle", tbSeoTitle.Text);
        q.Parameters.AddWithValue("@seoDesc", tbSeoDesc.Text);
        q.Parameters.AddWithValue("@seoKeys", tbSeoKeys.Text);

        // добавляем псевдоним
        string strPsevdo = tbPsevdo.Text;
        if (strPsevdo == "")
            strPsevdo = new cProcs().TitleToPsevdo(tbTitle.Text);
        q.Parameters.AddWithValue("@psevdoName", strPsevdo);

        q.ExecuteNonQuery();
        conn.Close();
        Response.Redirect("pages.aspx?status=added");
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