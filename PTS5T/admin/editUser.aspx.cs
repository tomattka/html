using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_editUser : System.Web.UI.Page
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
        if (tbName.Text == "" && tbMail.Text == "")
            loadUserData();
    }

    protected void loadUserData()
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
            if (r["emailValidated"].ToString() != "")
                chValid.Checked = bool.Parse(r["emailValidated"].ToString());
            else
                chValid.Checked = false;
            tbPhone.Text = r["phone"].ToString();
            tbSkype.Text = r["skype"].ToString();
            if (r["birthDate"].ToString() != "")
                tbBirth.Text = DateTime.Parse(r["birthDate"].ToString()).ToString("dd.MM.yyyy");
            else
                tbBirth.Text = "";
                tbCity.Text = r["city"].ToString();
            tbSocial.Text = r["social"].ToString();
            tbOtherContacts.Text = r["otherContacts"].ToString();
            if (r["isActive"].ToString() != "")
                chActive.Checked = bool.Parse(r["isActive"].ToString());
            else
                chActive.Checked = false;

            ddlGender.SelectedValue = "Мужской";
            if (r["isUserMale"].ToString() != "")
                if (bool.Parse(r["isUserMale"].ToString())==false) ddlGender.SelectedValue = "Женский";
            hfPass.Value = r["userPass"].ToString();

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

    protected void bSave_Click(object sender, EventArgs e)
    {
        if (checkFormats())
        {
            string SQLstr = "proc_adminUpdateUser @userId, @userName, @isUserMale, @userMail, @userSecondName, @userFamily, @emailValidated";
            SQLstr += ", @birthDate, @city, @phone, @skype, @social, @otherContacts, @isActive, @actionUserId, @eventTime";

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand q = new SqlCommand(SQLstr, conn);
            q.Parameters.AddWithValue("@userId", userId);
            q.Parameters.AddWithValue("@userName", tbName.Text);
            if (ddlGender.SelectedValue == "Мужской")
                q.Parameters.AddWithValue("@isUserMale", true);
            else
                q.Parameters.AddWithValue("@isUserMale", false);
            q.Parameters.AddWithValue("@userMail", tbMail.Text);
            q.Parameters.AddWithValue("@userSecondName", tbSecondName.Text);
            q.Parameters.AddWithValue("@userFamily", tbFamily.Text);
            q.Parameters.AddWithValue("@emailValidated", chValid.Checked);
            if (tbBirth.Text != "")
                q.Parameters.AddWithValue("@birthDate", DateTime.Parse(tbBirth.Text).Date);
            else
                q.Parameters.AddWithValue("@birthDate", DBNull.Value);
            q.Parameters.AddWithValue("@city", tbCity.Text);
            q.Parameters.AddWithValue("@phone", tbPhone.Text);
            q.Parameters.AddWithValue("@skype", tbSkype.Text);
            q.Parameters.AddWithValue("@social", tbSocial.Text);
            q.Parameters.AddWithValue("@otherContacts", tbOtherContacts.Text);
            q.Parameters.AddWithValue("@isActive", chActive.Checked);
            q.Parameters.AddWithValue("@actionUserId", actionUserId);
            q.Parameters.AddWithValue("@eventTime", DateTime.Now);
            conn.Open();
            q.ExecuteNonQuery();
            conn.Close();

            Response.Redirect("users.aspx?status=edited");
        }
    }

    protected void bPassSave_Click(object sender, EventArgs e)
    {
        if (checkPasswords())
        {
            string SQLstr = "proc_adminUpdatePassword @userId, @newPass, @actingUserId, @timeChanged";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand q = new SqlCommand(SQLstr, conn);
            q.Parameters.AddWithValue("@newPass", tbNewPass.Text);
            q.Parameters.AddWithValue("@userId", userId);
            q.Parameters.AddWithValue("@actingUserId", actionUserId);
            q.Parameters.AddWithValue("@timeChanged", DateTime.Now);
            conn.Open();
            q.ExecuteNonQuery();
            conn.Close();

            hfPass.Value = tbNewPass.Text;
            tbNewPass.Text = tbNewPassAgain.Text = tbOldPass.Text = "";
            lPassSucc.Visible = true;
        }
    }

    protected bool checkPasswords()
    {
        lErrOldPass.Visible = false;
        lErrNewPass.Visible = false;
        lErrAgain.Visible = false;
        lPassSucc.Visible = false;

        bool res = true;
        if (tbNewPass.Text.Length < 5)
        {
            lErrNewPass.Visible = true;
            res = false;
        }
        if (tbNewPass.Text != tbNewPassAgain.Text)
        {
            lErrAgain.Visible = true;
            res = false;
        }
        if (tbOldPass.Text != hfPass.Value)
        {
            lErrOldPass.Visible = true;
            res = false;
        }

        return res;
    }

        protected void bCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("users.aspx");
    }

    protected bool checkFormats()
    {
        lErrName.Visible = false;
        lErrMail.Visible = false;
        lErrDate.Visible = false;

        bool res = true;
        if (tbName.Text == "")
        {
            lErrName.Visible = true;
            res = false;
        }

        // проверка почты
        if (tbMail.Text == "")
        {
            lErrMail.Text = "<span style=\"color: red; margin - left: 10px; \">введите адрес почты!</span>";
            lErrMail.Visible = true;
            res = false;
        }
        else
        {
            string strMail = tbMail.Text;
            if (!(strMail.IndexOf("@") > -1 && strMail.IndexOf(".") > -1 && strMail.IndexOf(".") - strMail.IndexOf("@") > 2 && strMail.IndexOf(".") < strMail.Length-1))
            {
                lErrMail.Text = "<span style=\"color: red; margin - left: 10px; \">некорректный формат почты!</span>";
                lErrMail.Visible = true;
                res = false;
            }
        }

        // проверка даты рождения
        if (tbBirth.Text != "")
        {
            DateTime dt;
            bool res2 = DateTime.TryParse(tbBirth.Text, out dt);
            if (!res2)
            {
                lErrDate.Visible = true;
                res = false;
            }
        }
               
        return res;
    }

}