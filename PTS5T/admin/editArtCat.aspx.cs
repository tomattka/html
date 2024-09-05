using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_editArtCat : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string userId = "2"; // admin
    string catId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] != null)
            userId = Session["userId"].ToString();
        if (Request.QueryString["catId"] == null)
            Response.Redirect("artcats.aspx");
        else
            catId = Request.QueryString["catId"];
        if (tbTitle.Text == "")
            loadCat();
    }

    protected void loadCat()
    {
        string SQLstr = "select * from articleCategories where catId=@catId";
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@catId", catId);
        SqlDataReader r = q.ExecuteReader();
        if (r.Read())
        {
            tbTitle.Text = r["catTitle"].ToString();
            tbDesc.Text = r["catDesc"].ToString();
            tbText.Text = r["catText"].ToString();
            tbTags.Text = r["catTags"].ToString();
            tbSeoTitle.Text = r["seoTitle"].ToString();
            tbSeoDesc.Text = r["seoDesc"].ToString();
            tbSeoKeys.Text = r["seoKeys"].ToString();
            tbPsevdo.Text = r["psevdoName"].ToString();
        }
        else
        {
            r.Close();
            conn.Close();
            Response.Redirect("artcats.aspx");
        }
        r.Close();
        conn.Close();
    }

    protected void bSave_Click(object sender, EventArgs e)
    {
        if (checkFormats())
        {
            // обновляем запись в БД
            string SQLstr = "proc_adminUpdateArtCat @catId, @catTitle, @catDesc, @catText, @catTags, @seoTitle, @seoDesc, @seoKeys, @timeEdited, @psevdoName, @userEdited";

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand q = new SqlCommand(SQLstr, conn);
            q.Parameters.AddWithValue("@catId", catId);
            q.Parameters.AddWithValue("@catTitle", tbTitle.Text);
            q.Parameters.AddWithValue("@catDesc", tbDesc.Text);
            q.Parameters.AddWithValue("@catText", tbText.Text);
            q.Parameters.AddWithValue("@catTags", tbTags.Text);
            q.Parameters.AddWithValue("@seoTitle", tbSeoTitle.Text);
            q.Parameters.AddWithValue("@seoDesc", tbSeoDesc.Text);
            q.Parameters.AddWithValue("@seoKeys", tbSeoKeys.Text);
            q.Parameters.AddWithValue("@timeEdited", DateTime.Now);
            q.Parameters.AddWithValue("@userEdited", userId);
            q.Parameters.AddWithValue("@psevdoName", tbPsevdo.Text);
            conn.Open();
            q.ExecuteNonQuery();
            conn.Close();

            Response.Redirect("artcats.aspx?status=edited");
        }
    }

    protected void bCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("artcats.aspx");
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