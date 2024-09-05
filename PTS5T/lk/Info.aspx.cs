using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lk_Info : System.Web.UI.Page
{
    string userId = "0";
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Считать ID пользователя
        if (Session["lk_userId"] != null)
            userId = Session["lk_userId"].ToString();
        if (userId != "0" && tbMail.Text == "")
            LoadUserData();
    }

    protected void LoadUserData()
    {
        string SQLstr = "select * from users where userId=@userId";
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        SqlDataReader r = q.ExecuteReader();
        if (r.Read())
        {
            tbFamily.Text = r["userFamily"].ToString();
            tbName.Text = r["userName"].ToString();
            tbSecondName.Text = r["userSecondName"].ToString();
            tbMail.Text = r["userMail"].ToString();
            tbPhone.Text = r["phone"].ToString();
            tbSkype.Text = r["skype"].ToString();
            tbCity.Text = r["city"].ToString();
            tbSocial.Text = r["social"].ToString();

        }
        else
        {
            r.Close();
            conn.Close();
            Response.Redirect("users.aspx");
        }
        r.Close();
        conn.Close();
    }

    protected void lbSave_Click(object sender, EventArgs e)
    {
        string SQLstr = "proc_LK_UpdateUser @userId, @userName, @userMail, @userSecondName, @userFamily";
        SQLstr += ", @city, @phone, @skype, @social, @pass, @eventTime";

        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        q.Parameters.AddWithValue("@userName", tbName.Text);
        q.Parameters.AddWithValue("@userMail", tbMail.Text);
        q.Parameters.AddWithValue("@userSecondName", tbSecondName.Text);
        q.Parameters.AddWithValue("@userFamily", tbFamily.Text);
        q.Parameters.AddWithValue("@city", tbCity.Text);
        q.Parameters.AddWithValue("@phone", tbPhone.Text);
        q.Parameters.AddWithValue("@skype", tbSkype.Text);
        q.Parameters.AddWithValue("@social", tbSocial.Text);
        q.Parameters.AddWithValue("@pass", tbPass1.Text);
        q.Parameters.AddWithValue("@eventTime", DateTime.Now);
        conn.Open();
        q.ExecuteNonQuery();
        conn.Close();

        lSucc.Visible = true;
    }
}