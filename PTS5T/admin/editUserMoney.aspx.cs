using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_editUserMoney : System.Web.UI.Page
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

        tbAmount.Focus();
        loadCurrentAmount();
        LoadTransHistory();
    }

    protected void LoadTransHistory()
    {
        // вывести лог
        string strLogHtml = "";
        string SQLstr = "proc_LK_getUserTransactions @userId";
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        conn.Open();
        SqlDataReader r = q.ExecuteReader();
        while (r.Read())
        {
            strLogHtml += "<p><b>" + (new cProcs()).GetMoneyStr(r["amount"].ToString()) + " Б&#8381;</b> " + GetTransData(r["transTime"].ToString()) + "<br>" + r["transTypeName"];
            if (r["comment"].ToString() != "")
                strLogHtml += ". " + r["comment"];
            strLogHtml += "</p>";
        }
        conn.Close();

        if (strLogHtml == "")
            strLogHtml = "<p>Нет истории транзакций.</p>";
        lTrans.Text = strLogHtml;
    }

    protected string GetTransData(string strTransTime)
    {
        string res = "";
        if (strTransTime != "")
        {
            DateTime dt = new DateTime(1900, 01, 01);
            DateTime.TryParse(strTransTime, out dt);
            if (dt.Year > 1900)
                res = dt.ToString("dd.MM.yy в HH:mm");
        }
        return res;
    }

    protected void bGo_Click(object sender, EventArgs e)
    {
        Int32 amt = int.Parse(tbAmount.Text);
        if (ddlAction.Text == "Вычесть")
            amt = amt * (-1);

        string SQLstr = "proc_adminAddUserMoney @userId, @addAmount, @comment, @actingUserId, @transactionTime";
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        q.Parameters.AddWithValue("@addAmount", amt);
        q.Parameters.AddWithValue("@comment", tbComment.Text);
        q.Parameters.AddWithValue("@actingUserId", actionUserId);
        q.Parameters.AddWithValue("@transactionTime", DateTime.Now);
        q.ExecuteNonQuery();
        conn.Close();
        //Response.Redirect("editUserMoney.aspx?userId=" + userId + "&result=success");
        Response.Redirect("editUserMoney.aspx?userId=" + userId);
    }

    protected void loadCurrentAmount()
    {
        string SQLstr = "proc_adminGetUserMoney @userId";
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        lAmount.Text = q.ExecuteScalar().ToString();

        SQLstr = "select max(userLogin) from users where userId=@userId";
        q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        lUser.Text = q.ExecuteScalar().ToString();

        conn.Close();
    }
}