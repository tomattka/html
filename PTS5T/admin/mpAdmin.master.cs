using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_mpAdmin : System.Web.UI.MasterPage
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckHttps();
        checkSession();
        getEvents();
    }



    protected void CheckHttps()
    {
        switch (Request.Url.Scheme)
        {
            case "http":
                var path = "https://" + Request.Url.Host + Request.Url.PathAndQuery;
                Response.Status = "301 Moved Permanently";
                Response.AddHeader("Location", path.ToLower());
                break;
        }
    }

    protected void checkSession()
    {
        if (Session["admin_logged"] != null)
            if (Session["admin_logged"].ToString() == "on")
                lUser.Text = Session["userName"].ToString();
            else
                Response.Redirect("login.aspx");
        else
            Response.Redirect("login.aspx");
    }

    protected void getEvents()
    {
        string eventsHtml = "";
        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "proc_adminGetEvents 1, 10";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        SqlDataReader r = q.ExecuteReader();
        while (r.Read())
        {
            string strUserName = r["userLogin"].ToString();
            string strTitle = r["eventTypeTemplate"].ToString();
            strTitle = strTitle.Replace("<userName>", strUserName);

            bool isMale = true;
            if (r["isUserMale"].ToString() != "")
                isMale = bool.Parse(r["isUserMale"].ToString());
            if (isMale)
                strTitle = strTitle.Replace("<femLetter>", "");
            else
                strTitle = strTitle.Replace("<femLetter>", "а");

            eventsHtml += "<div class=\"eventItem\"><h3><a href=\"showEvent.aspx?eventId=" + r["eventId"] + "\">" + strTitle + "</a></h3><time>" + DateTime.Parse(r["eventTime"].ToString()).ToString("dd.MM.yy HH:mm") + "</time> | <a href=\"showEvent.aspx?eventId=" + r["eventId"] + "\">Подробнее &raquo;</a></div>";
        }
        r.Close();
        conn.Close();
        lEvents.Text = eventsHtml;
    }

}
