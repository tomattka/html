using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class psevdourl_temp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void bConvert_Click(object sender, EventArgs e)
    {
        lRes.Text = new cProcs().TitleToPsevdo(tbTitle.Text);
    }

}