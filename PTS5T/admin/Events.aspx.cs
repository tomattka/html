using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Events : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string userId = "2"; //admin
    int currentPage = 1;
    int inPage = 20;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] != null)
            userId = Session["userId"].ToString();
        if (Request.QueryString["page"] != null)
            int.TryParse(Request.QueryString["page"], out currentPage);
        loadEvents();

    }

    protected void loadEvents()
    {
        pEvents.Controls.Clear();
        loadPager();
        string SQLstr = "proc_adminGetEvents @pageNum, @inPage";
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@inPage", inPage);
        q.Parameters.AddWithValue("@pageNum", currentPage);
        conn.Open();
        SqlDataReader r = q.ExecuteReader();
        while(r.Read())
        {
            Literal l = new Literal();
            l.Text = "<div class=\"newsItem\"><h2><a href=\"showEvent.aspx?eventId=" + r["eventId"].ToString() + "\">";
            l.Text += r["eventTypeName"].ToString() + "</a></h2><span>";
            pEvents.Controls.Add(l);

            ImageButton ib = new ImageButton();
            ib.ID = "ibDelete" + r["eventId"].ToString();
            ib.ImageUrl = "img/delete.gif";
            ib.AlternateText = ib.ToolTip = "Удалить";

            string askText = "Удалить событие \"" + r["eventTypeName"].ToString() + "\"?";
            ib.OnClientClick = "return confirm('" + askText + "')";
            ib.Click += ibDelete_Click;
            pEvents.Controls.Add(ib);

            l = new Literal();
            l.Text = "</span><p><time>" + DateTime.Parse(r["eventTime"].ToString()).ToString("dd MMMM yyyy в HH:mm") + "</time> совершил";

            bool isMale = true;
            if (r["isUserMale"].ToString() != "")
                isMale = bool.Parse(r["isUserMale"].ToString());
            if (isMale != true)
                l.Text += "a";

            l.Text += " <a href=\"editUser.aspx?userId=" + r["userId"].ToString() + "\"><b>" + r["userLogin"] + "</b></a></p></div>";
            pEvents.Controls.Add(l);
        }
        conn.Close();

    }

    protected void ibDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        string eventId = ib.ID.Remove(0, 8);
        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "delete from admin_events where eventId=@eventId";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@eventId", eventId);
        q.ExecuteNonQuery();

        conn.Close();
        Response.Redirect("events.aspx?status=deleted");
    }

    protected void loadPager()
    {
        string SQLstr = "proc_adminCountEventsPages @inPage";
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@inPage", inPage);
        string strCnt = q.ExecuteScalar().ToString();
        conn.Close();

        int iCnt = int.Parse(strCnt);
        string strPager = "";
        if (iCnt > 1)
        {
            if (currentPage == 1)
                strPager += "<a href=\"events.aspx?page=" + currentPage.ToString() + "\">&laquo;</a>";
            else
                strPager += "<a href=\"events.aspx?page=" + (currentPage - 1).ToString() + "\">&laquo;</a>";

            for (int i = 1; i < iCnt + 1; i++)
            {
                strPager += "<a href=\"events.aspx?page=" + i.ToString() + "\"";
                if (i == currentPage)
                    strPager += " class=\"active\"";
                strPager += ">" + i.ToString() + "</a>";
            }

            if (currentPage == iCnt)
                strPager += "<a href=\"events.aspx?page=" + currentPage.ToString() + "\">&raquo;</a>";
            else
                strPager += "<a href=\"events.aspx?page=" + (currentPage + 1).ToString() + "\">&raquo;</a>";
        }
        lPager.Text = strPager;
    }

    protected void lbDeleteAll_Click(object sender, EventArgs e)
    {
        string SQLstr = "delete from admin_events";
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand q = new SqlCommand(SQLstr, conn);
        conn.Open();
        q.ExecuteNonQuery();
        conn.Close();
        loadEvents();
    }


}