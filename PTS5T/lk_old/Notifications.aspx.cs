using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lk_Notifications : System.Web.UI.Page
{
    string userId = "0";
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Считать ID пользователя
        if (Session["lk_userId"] != null)
            userId = Session["lk_userId"].ToString();
        LoadSubStatus();
    }

    protected void lbSave_Click(object sender, EventArgs e)
    {
        string SQLstr = "update users set sendSubscription=@sendSubscription where userId=@userId";
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        q.Parameters.AddWithValue("@sendSubscription", chSub.Checked.ToString());
        conn.Open();
        q.ExecuteNonQuery();
        conn.Close();
        lSucc.Visible = true;
    }

    protected void LoadSubStatus()
    {
        if (hfLoaded.Value == "0")
        {
            string SQLstr = "select top 1 sendSubscription from users where userId=@userId";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand q = new SqlCommand(SQLstr, conn);
            q.Parameters.AddWithValue("@userId", userId);
            SqlDataReader r = q.ExecuteReader();
            if (r.Read())
            {
                chSub.Checked = bool.Parse(r["sendSubscription"].ToString());
            }
            r.Close();
            conn.Close();
            hfLoaded.Value = "1";
        }
    }



}