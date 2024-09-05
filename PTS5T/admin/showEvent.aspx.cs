using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_showEvent : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string eventId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["eventId"] == null)
            Response.Redirect("events.aspx");
        else
            eventId = Request.QueryString["eventId"];
        showEvents();
    }

    protected void showEvents()
    {
        string SQLstr = "proc_adminGetEvent @eventId";
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@eventId", eventId);
        conn.Open();
        SqlDataReader r = q.ExecuteReader();
        if (r.Read())
        {
            lEventTitle.Text = r["eventTypeName"].ToString();
            lTime.Text = DateTime.Parse(r["eventTime"].ToString()).ToString("dd.MM.yyyy HH:mm");
            lUser.Text = "<a href=\"editUser.aspx?userId=" + r["userId"] + "\">" + r["userLogin"] + "</a>";
            lObj.Text = r["objectName"].ToString();
            if (r["href"].ToString() != "#")
                lObjHref.Text = "<a href=\"" + r["href"] + r["additionalId"] + "\">" + r["href"] + r["additionalId"] + "</a>";
            else
                lObjHref.Text = "нет ссылки (объект удалён)";
        }
        r.Close();
        conn.Close();
    }


}