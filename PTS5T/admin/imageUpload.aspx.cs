using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_ImageUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void bUpload_Click(object sender, EventArgs e)
    {
        if (fu.HasFile)
            UploadPic();
    }

    protected void UploadPic()
    {
        // сгенерировать код изображения
        string strCode = GenerateRandomCode(6);
        string strSourceName = fu.FileName;
        if (strSourceName.IndexOf(".") > 0)
            strSourceName = strSourceName.Remove(strSourceName.LastIndexOf("."));

        // сохранить изображение
        string strFileName = Request.PhysicalApplicationPath + "uploads\\admin\\" + strSourceName + "_" + strCode + ".jpg";
        System.Drawing.Image img = System.Drawing.Image.FromStream(fu.FileContent);

        // получаем размер
        int iWidth = 1920;
        int i = 0;
        if (int.TryParse(tbWidth.Text, out i))
            iWidth = i;
        i = 0;
        int iHeight = 1280;
        if (int.TryParse(tbHeight.Text, out i))
            iHeight = i;

        img = RezizeImage(img, iWidth, iHeight);
        SaveFile(img, strFileName);

        // вывести изображение и ссылку
        string strLink = "/uploads/admin/" + strSourceName + "_" + strCode + ".jpg";
        tbLink.Text = strLink;
        lPic.Text = "<a href=\"" + strLink + "\" data-fancybox=\"gallery\"><img src=\"" + strLink + "\" width=\"100\" /></a>";
    }

    protected string GenerateRandomCode(int iLengh)
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

    protected void SaveFile(System.Drawing.Image img, string fileName)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
        ImageCodecInfo ici = null;

        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.MimeType == "image/jpeg")
                ici = codec;
        }

        EncoderParameters ep = new EncoderParameters();
        ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)80);

        //-------------------------------------------------------

        img.Save(fileName, ici, ep);
    }

    private System.Drawing.Image RezizeImage(System.Drawing.Image img, int maxWidth, int maxHeight)
    {
        if (img.Height < maxHeight && img.Width < maxWidth) return img;
        using (img)
        {
            Double xRatio = (double)img.Width / maxWidth;
            Double yRatio = (double)img.Height / maxHeight;
            Double ratio = Math.Max(xRatio, yRatio);
            int nnx = (int)Math.Floor(img.Width / ratio);
            int nny = (int)Math.Floor(img.Height / ratio);
            Bitmap cpy = new Bitmap(nnx, nny, PixelFormat.Format32bppArgb);
            using (Graphics gr = Graphics.FromImage(cpy))
            {
                gr.Clear(Color.Transparent);

                // This is said to give best quality when resizing images
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;

                gr.DrawImage(img,
                    new Rectangle(0, 0, nnx, nny),
                    new Rectangle(0, 0, img.Width, img.Height),
                    GraphicsUnit.Pixel);
            }
            return cpy;
        }

    }

    private MemoryStream BytearrayToStream(byte[] arr)
    {
        return new MemoryStream(arr, 0, arr.Length);
    }
}