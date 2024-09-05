using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lk_Money : System.Web.UI.Page
{
    string userId = "0";
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lk_userId"] != null)
            userId = Session["lk_userId"].ToString();
        if (userId != "0")
            GetMoneyData();
    }

    protected void GetMoneyData()
    {
        // вывести сумму
        string SQLstr = "proc_LK_getUserBalance @userId";
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        conn.Open();
        string strMoney = q.ExecuteScalar().ToString();
        conn.Close();
        lMoney.Text = (new cProcs().GetMoneyStr(strMoney));

        // вывести лог
        string strLogHtml = "";
        SQLstr = "proc_LK_getUserTransactions @userId";
        q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        conn.Open();
        SqlDataReader r = q.ExecuteReader();
        while (r.Read())
        {
            strLogHtml += "<tr><td class=\"ttl\">" + r["transTypeName"];
            if (r["comment"].ToString() != "")
                strLogHtml += ". " + r["comment"];
            strLogHtml += " <span>" + GetTransData(r["transTime"].ToString()) + "</span></td><td class=\"summ\">";
            strLogHtml += (new cProcs()).GetMoneyStr(r["amount"].ToString()) + " Б&#8381;</td></tr>";
        }
        conn.Close();

        if (strLogHtml == "")
            strLogHtml = "<p style=\"text-align: center;\">Нет истории транзакций.</p>";
        else
            strLogHtml = "<table class=\"log\">" + strLogHtml + "</table>";
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



}