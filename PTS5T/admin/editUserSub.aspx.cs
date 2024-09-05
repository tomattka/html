using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_editUserSub : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string actionUserId = "2"; // admin
    string userId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] != null)
            actionUserId = Session["userId"].ToString();
        if (Request.QueryString["userId"] == null)
            Response.Redirect("users.aspx");
        else
            userId = Request.QueryString["userId"];

        if (lCurrentSub.Text == "")
            loadCurrentSub();
    }

    protected void bSave_Click(object sender, EventArgs e)
    {
        string SQLstr = "proc_adminChangeUserSub @userId, @subStart, @subEnd, @actingUser, @timeUpdated";
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        q.Parameters.AddWithValue("@subStart", cStart.SelectedDate);
        q.Parameters.AddWithValue("@subEnd", cEnd.SelectedDate);
        q.Parameters.AddWithValue("@actingUser", actionUserId);
        q.Parameters.AddWithValue("@timeUpdated", DateTime.Now);
        conn.Open();
        q.ExecuteNonQuery();
        conn.Close();
        loadCurrentSub();
    }

    protected void bCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("users.aspx");
    }

    protected void loadCurrentSub()
    {
        string SQLstr = "select subStart, subEnd from user_sub where userId=@userId";
        SqlConnection conn = new SqlConnection(connStr);
        DateTime dtStart = DateTime.Today;
        DateTime dtEnd = DateTime.Today.AddMonths(1);
        string strCurrentSub = "Нет подписки";

        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        SqlDataReader r = q.ExecuteReader();
        if (r.Read())
        {
            dtStart = DateTime.Parse(r["subStart"].ToString());
            dtEnd = DateTime.Parse(r["subEnd"].ToString());
            strCurrentSub = "С " + dtStart.ToString("dd.MM.yyyy") + " до " + dtEnd.ToString("dd.MM.yyyy");
        }
        r.Close();
        lCurrentSub.Text = strCurrentSub;

        SQLstr = "select max(userLogin) from users where userId=@userId";
        q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        lUser.Text = q.ExecuteScalar().ToString();

        conn.Close();

        cStart.SelectedDate = cStart.VisibleDate = dtStart;
        cEnd.SelectedDate = cEnd.VisibleDate = dtEnd;
    }
}