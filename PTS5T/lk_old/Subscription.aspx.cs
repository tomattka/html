using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lk_Subscription : System.Web.UI.Page
{
    string userId = "0";
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
    DateTime dtSubNow = DateTime.Now;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Считать ID пользователя
        if (Session["lk_userId"] != null)
            userId = Session["lk_userId"].ToString();
        if (userId != "0") // Вывести текущий срок подписки
            GetCurrentSub();
    }

    protected void GetCurrentSub()
    {
        string SQLstr = "proc_LK_getSubTill @userId";
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        conn.Open();
        string strSubTill = q.ExecuteScalar().ToString();
        conn.Close();
        if (strSubTill == "none")
            lSub.Text = "Нет подписки";
        else
        {
            dtSubNow = DateTime.Parse(strSubTill);
            lSub.Text = "До " + dtSubNow.ToString("d MMMM yyyy г.");
        }
    }

    protected void LbProlong_Click(object sender, EventArgs e)
    {
        // Получить сумму пользователя
        string SQLstr = "proc_LK_getUserBalance @userId";
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userId", userId);
        conn.Open();
        int iMoney = int.Parse(q.ExecuteScalar().ToString());
        conn.Close();


        // Сравнить со стоимостью подписки
        int iMonths = 1;
        int iCost = 100;

        if (rb3M.Checked)
        {
            iMonths = 3;
            iCost = 270;
        }

        if (rb6M.Checked)
        {
            iMonths = 6;
            iCost = 480;
        }

        if (iMoney >= iCost) // Если ОК, пополнить подписку, списать деньги
        {
            // вызвать процедуру, передать ей конечную и сегодняшнюю дату (будет использоваться только, если подписки нет)
            DateTime dtNewSubEnd = dtSubNow.AddMonths(iMonths);
            SQLstr = "proc_LK_buySubscription @userId, @dateNow, @subEnd, @moneyCost";
            q = new SqlCommand(SQLstr, conn);
            q.Parameters.AddWithValue("@userId", userId);
            q.Parameters.AddWithValue("@dateNow", DateTime.Now);
            q.Parameters.AddWithValue("@subEnd", dtNewSubEnd);
            q.Parameters.AddWithValue("@moneyCost", iCost);
            conn.Open();
            q.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("/page/sub_prolong");

        }
        else // Если нет, выдать ошибку
            lErr.Text = "<p class=\"err\">Недостаточно денег для продления подписки! <a href=\"addMoney.aspx\">Пополнить баланс</a></p>";


    }


}