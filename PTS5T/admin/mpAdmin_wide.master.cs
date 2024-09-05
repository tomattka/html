using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_mpAdmin_wide : System.Web.UI.MasterPage
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckHttps();
        checkSession();
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

}
