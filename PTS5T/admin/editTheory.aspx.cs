using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_editTheory : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string userId = "2"; //Change!
    string theoryId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] != null)
            userId = Session["userId"].ToString();
        if (Request.QueryString["theoryId"] == null)
            Response.Redirect("theory.aspx");
        else
            theoryId = Request.QueryString["theoryId"];
        loadCategories();
        if (tbTitle.Text == "" && tbTime.Text == "")
            loadTheory();
    }

    protected void loadTheory()
    {
        string SQLstr = "proc_adminLoadTheory @theoryId";
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@theoryId", theoryId);
        SqlDataReader r = q.ExecuteReader();
        if (r.Read())
        {
            tbTitle.Text = r["theoryTitle"].ToString();
            tbTime.Text = DateTime.Parse(r["timePublished"].ToString()).ToString("dd.MM.yy HH:mm");
            lbCat.SelectedValue = r["catTitle"].ToString();
            tbDesc.Text = r["theoryDesc"].ToString();
            tbText.Text = r["theoryText"].ToString();
            tbTags.Text = r["theoryTags"].ToString();
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
            Response.Redirect("theory.aspx");
        }
        r.Close();
        conn.Close();

    }

    protected void loadCategories()
    {
        if (lbCat.Items.Count == 0)
        {
            string SQLstr = "select catId, catTitle from theoryCategories order by catTitle";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand q = new SqlCommand(SQLstr, conn);
            SqlDataReader r = q.ExecuteReader();
            while (r.Read())
            {
                lbCat.Items.Add(r["catTitle"].ToString());

            }
            r.Close();
            conn.Close();
        }
    }

    protected void bSave_Click(object sender, EventArgs e)
    {
        if (checkFormats())
        { 
            // обновляем запись в БД
            string SQLstr = "proc_adminUpdateTheory @theoryId, @theoryTitle, @theoryDesc, @theoryText, @theoryTags, @isPublished, @timePublished, @timeEdited, @userEdited, @seoTitle, @seoDesc, @seoKeys, @catTitle, @psevdoName";

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand q = new SqlCommand(SQLstr, conn);
            q.Parameters.AddWithValue("@theoryId", theoryId);
            q.Parameters.AddWithValue("@theoryTitle", tbTitle.Text);
            q.Parameters.AddWithValue("@theoryDesc", tbDesc.Text);
            q.Parameters.AddWithValue("@theoryText", tbText.Text);
            q.Parameters.AddWithValue("@theoryTags", tbTags.Text);
            q.Parameters.AddWithValue("@isPublished", chPublish.Checked);
            q.Parameters.AddWithValue("@timePublished", DateTime.Parse(tbTime.Text));
            q.Parameters.AddWithValue("@timeEdited", DateTime.Now);
            q.Parameters.AddWithValue("@userEdited", userId);
            q.Parameters.AddWithValue("@seoTitle", tbSeoTitle.Text);
            q.Parameters.AddWithValue("@seoDesc", tbSeoDesc.Text);
            q.Parameters.AddWithValue("@seoKeys", tbSeoKeys.Text);
            q.Parameters.AddWithValue("@catTitle", lbCat.SelectedValue);
            q.Parameters.AddWithValue("@psevdoName", tbPsevdo.Text);
            conn.Open();
            q.ExecuteNonQuery();
            conn.Close();

            Response.Redirect("theory.aspx?status=edited");
        }
    }

    protected void bCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("theory.aspx");
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