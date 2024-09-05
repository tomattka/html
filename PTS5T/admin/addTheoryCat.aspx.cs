using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_AddTheoryCat : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string userId = "2"; //Admin

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] != null)
            userId = Session["userId"].ToString();
        tbText.Focus();
    }

    protected void bAdd_Click(object sender, EventArgs e)
    {
        if (checkFormats())
            addArticle();
    }

    protected void bCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("theoryCats.aspx");
    }

    protected void addArticle()
    {
        string SQLstr = "proc_addTheoryCat @catTitle, @catDesc, @catText, @catTags, @seoTitle, @seoDesc, @seoKeys,	@timePublished, @psevdoName, @userPublished";
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@catTitle", tbTitle.Text);
        q.Parameters.AddWithValue("@catDesc", tbDesc.Text);
        q.Parameters.AddWithValue("@catText", tbText.Text);
        q.Parameters.AddWithValue("@catTags", tbTags.Text);
        q.Parameters.AddWithValue("@seoTitle", tbSeoTitle.Text);
        q.Parameters.AddWithValue("@seoDesc", tbSeoDesc.Text);
        q.Parameters.AddWithValue("@seoKeys", tbSeoKeys.Text);
        q.Parameters.AddWithValue("@timePublished", DateTime.Now);
        q.Parameters.AddWithValue("@userPublished", userId);

        // добавляем псевдоним
        string strPsevdo = tbPsevdo.Text;
        if (strPsevdo == "")
            strPsevdo = new cProcs().TitleToPsevdo(tbTitle.Text);
        q.Parameters.AddWithValue("@psevdoName", strPsevdo);

        conn.Open();
        q.ExecuteNonQuery();
        conn.Close();
        Response.Redirect("theoryCats.aspx?status=added");
    }

    protected bool checkFormats()
    {
        lErrTitle.Visible = false;

        bool res = true;
        if (tbTitle.Text == "")
        {
            lErrTitle.Visible = true;
            res = false;
        }
        return res;
    }


}