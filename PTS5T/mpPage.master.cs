using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mpMain : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckHttps();
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


}
