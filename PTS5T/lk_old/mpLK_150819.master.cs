using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lk_mpLK : System.Web.UI.MasterPage
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
    }

    protected void CheckLogin()
    {
        if (Session["lk_userId"] == null || Session["lk_userId"].ToString() == "0")
            Response.Redirect("login.aspx");
    }
}
