using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Users : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    string actingUserId = "2"; //admin

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] != null)
            actingUserId = Session["userId"].ToString();
        getUsers();
        tbSearch.Focus();
    }

    protected void bSearch_Click(object sender, EventArgs e)
    {
        getUsers();
    }

    protected void bClear_Click(object sender, EventArgs e)
    {
        tbSearch.Text = "";
        tbSearch.Focus();
        getUsers();
    }

    protected void getUsers()
    {
        pUsers.Controls.Clear();

        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "proc_getUserList @searchText";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@searchText", tbSearch.Text);
        SqlDataReader r = q.ExecuteReader();
        while (r.Read())
        {
            Literal l = new Literal();
            l.Text = "<div class=\"newsItem\"><h2><a href=\"editUser.aspx?userId=" + r["userId"] + "\"><b>" + r["userLogin"] + "</b></a></h2><span>";
            pUsers.Controls.Add(l);

            ImageButton ib = new ImageButton();
            ib.ID = "ibEdit" + r["userId"];
            ib.ImageUrl = "img/user_edit.gif";
            ib.AlternateText = "Редактировать";
            ib.ToolTip = "Редактировать";
            ib.Click += ibEdit_Click;
            pUsers.Controls.Add(ib);

            ib = new ImageButton();
            ib.ID = "ibSub" + r["userId"];
            ib.ImageUrl = "img/user_sub.gif";
            ib.AlternateText = ib.ToolTip = "Подписка";
            ib.Click += ibSub_Click;
            pUsers.Controls.Add(ib);

            ib = new ImageButton();
            ib.ID = "ibMoney" + r["userId"];
            ib.ImageUrl = "img/user_money.gif";
            ib.AlternateText = ib.ToolTip = "Счёт";
            ib.Click += ibMoney_Click;
            pUsers.Controls.Add(ib);

        // Кнопка назначения администратора

            ib = new ImageButton();
            ib.ID = "ibAdmin" + r["userId"];
            ib.ImageUrl = "img/user_key.gif";

            bool isAdmin = false;
            if (r["isUserAdmin"].ToString() != "")
            isAdmin = bool.Parse(r["isUserAdmin"].ToString());
            string askText = "";
            if (isAdmin)
            {
                ib.ImageUrl = "img/user_closeAccess.gif";
                askText = "Забрать права администратора у пользователя " + r["userLogin"] + "?";
                ib.AlternateText = ib.ToolTip = "Снять права администратора";
            }
            else
            {
                ib.ImageUrl = "img/user_key.gif";
                askText = "Назначить администратором пользователя " + r["userLogin"] + "?";
                ib.AlternateText = ib.ToolTip = "Назначить администратором";
            }

            ib.Style.Add("margin-left", "20px");
            ib.OnClientClick = "return confirm('" + askText + "')";
            ib.Click += ibAdmin_Click;
            pUsers.Controls.Add(ib);

        // Кнопка блокировки

            ib = new ImageButton();
            ib.ID = "ibBlock" + r["userId"];

            bool isActive = true;
            if (r["isActive"].ToString() != "")
                isActive = bool.Parse(r["isActive"].ToString());
            askText = "";
            if (isActive)
            {
                ib.ImageUrl = "img/user_block.gif";
                askText = "Заблокировать пользователя " + r["userLogin"] + "?";
                ib.AlternateText = ib.ToolTip = "Заблокировать";
            }
            else
            {
                ib.ImageUrl = "img/user_accept.gif";
                askText = "Разблокировать пользователя " + r["userLogin"] + "?";
                ib.AlternateText = ib.ToolTip = "Разблокировать";
            }

            ib.OnClientClick = "return confirm('" + askText + "')";
            ib.Click += ibBlock_Click;
            pUsers.Controls.Add(ib);

            bool isDeletable = true;
            if (r["allowDelete"].ToString() != "")
                isDeletable = bool.Parse(r["allowDelete"].ToString());
            if (isDeletable)
            {

                ib = new ImageButton();
                ib.ID = "ibDelete" + r["userId"];
                ib.ImageUrl = "img/user_delete.gif";
                ib.AlternateText = ib.ToolTip = "Удалить";
                askText = "Удалить пользователя " + r["userLogin"] + "? ДЕСТВИЕ НЕЛЬЗЯ БУДЕТ ОТМЕНИТЬ, будут удалены все связанные с пользователем данные.";
                ib.OnClientClick = "return confirm('" + askText + "')";
                ib.Click += ibDelete_Click;
                pUsers.Controls.Add(ib);
            }

            string userFullName = r["userName"].ToString() + " " + r["userSecondName"].ToString();
            if (r["userFamily"] != null)
                userFullName = r["userFamily"].ToString() + " " + userFullName;

            l = new Literal();
            l.Text = "</span><p>" + userFullName + "</p></div>";
            pUsers.Controls.Add(l);
        }
        r.Close();
        conn.Close();
    }

    protected void ibEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        Response.Redirect("editUser.aspx?userId=" + ib.ID.Remove(0, 6));
    }

    protected void ibSub_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        Response.Redirect("editUserSub.aspx?userId=" + ib.ID.Remove(0, 5));
    }

    protected void ibMoney_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        Response.Redirect("editUserMoney.aspx?userId=" + ib.ID.Remove(0, 7));
    }

    protected void ibAdmin_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        string userId = ib.ID.Remove(0, 7);

        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "proc_adminChangeUserAdminStatus @userId, @actingUserId, @eventTime";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        q.Parameters.AddWithValue("@actingUserId", actingUserId);
        q.Parameters.AddWithValue("@eventTime", DateTime.Now);
        bool isAdmin = bool.Parse(q.ExecuteScalar().ToString());
        conn.Close();
        if (isAdmin)
            Response.Redirect("users.aspx?status=admined");
        else
            Response.Redirect("users.aspx?status=unadmined");
    }

    protected void ibBlock_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        string userId = ib.ID.Remove(0, 7);

        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "proc_adminChangeUserBlockStatus @userId, @actingUserId, @eventTime";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        q.Parameters.AddWithValue("@actingUserId", actingUserId);
        q.Parameters.AddWithValue("@eventTime", DateTime.Now);
        bool isActive = bool.Parse(q.ExecuteScalar().ToString());
        conn.Close();
        if (isActive)
            Response.Redirect("users.aspx?status=unblocked");
        else
            Response.Redirect("users.aspx?status=blocked");
    }

    protected void ibDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        string userId = ib.ID.Remove(0, 8);

        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "proc_adminDeleteUser @deleteUserId, @actingUserId, @eventTime";
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@deleteUserId", userId);
        q.Parameters.AddWithValue("@actingUserId", actingUserId);
        q.Parameters.AddWithValue("@eventTime", DateTime.Now);
        q.ExecuteNonQuery();

        conn.Close();
        Response.Redirect("users.aspx?status=deleted");


    }

}