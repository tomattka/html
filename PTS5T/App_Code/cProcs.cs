using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Сводное описание для ClassMain
/// </summary>
public class cProcs
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;

    public cProcs()
    {
        //
        // TODO: добавьте логику конструктора
        //
    }

    public string GenerateRandomCode(int iLengh)
    {
        string strSybols = "aAbBcCdDeEfFgGhHiIjJkKlLmMoOpPqQrRsStTuUvVwWxXwWzZ0123456789";
        int iMax = strSybols.Length - 1;
        string strRes = "";
        Random rnd = new Random();
        for (int i = 0; i < iLengh; i++)
        {
            int iRandom = rnd.Next(iMax);
            strRes += strSybols[iRandom];
        }
        return strRes;
    }

    public string TitleToPsevdo(string strTitle)
    {
        string res = "";
        strTitle = strTitle.ToLower();
        string sourceSymbols = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя0123456789 -_abcdefghijklmnopqrstuvwxyz";
        string[] resultSymbols = new string[] { "a", "b", "v", "g", "d", "e", "e", "zh", "z", "i", "j", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "h", "c", "ch", "sh", "shch", "", "y", "", "e", "yu", "ya", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "_", "-", "_", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "x" };

        for (int i = 0; i < strTitle.Length; i++)
        {
            int j = sourceSymbols.IndexOf(strTitle[i]);
            if (j > -1)
                res += resultSymbols[j];
        }

        return res;
    }

    public bool SendLetter(string strTo, string strSubj, string strText)
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

    public string GetMoneyStr(string strMoney)
    {
        string res = strMoney;
        if (res.Length > 3) // тысячи
            res = res.Insert(res.Length - 3, " ");

        if (res.Length > 7) // миллионы
            res = res.Insert(res.Length - 7, " ");

        if (res == "")
            res = "0";

        return res;
    }

    public bool MailExists(string strMail)
    {
        bool res = false;

        SqlConnection conn = new SqlConnection(connStr);
        string SQLstr = "select count(*) from users where userMail=@userMail";
        SqlCommand q = new SqlCommand(SQLstr, conn);
        q.Parameters.AddWithValue("@userMail", strMail);
        conn.Open();
        string strRes = q.ExecuteScalar().ToString();
        conn.Close();
        if (strRes != "0")
            res = true;
        return res;
    }


}