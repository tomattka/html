using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class testMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void bSend_Click(object sender, EventArgs e)
    {
        SendLetter("tomattka@gmail.com", "test", "hi");
    }

    protected bool SendLetter(string strTo, string strSubj, string strText)
    {
        bool res = true;
        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("noreply@pts5t.ru", "Сайт ПТС5Т");
            mail.To.Add(new MailAddress(strTo));
            mail.Subject = strSubj;

            mail.IsBodyHtml = true;
            mail.Body = strText;

            SmtpClient client = new SmtpClient();
            client.Host = "pts5t.ru";
            client.Port = 587;
            client.Credentials = new NetworkCredential("noreply@pts5t.ru", "2H0qth#6");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
            mail.Dispose();
        }
        catch (Exception e)
        {
            res = false;
        }
        return res;
    }

}