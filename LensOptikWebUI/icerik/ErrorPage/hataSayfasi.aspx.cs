using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Icerik_Hata_Sayfasi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["errorurl"] != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Beklenmedik bir hata oluştu.<br/>");
                sb.Append("Hata kaydı yapıldı ve en kısa sürede hata çözümlenecektir.<br/>");

                sb.Append("Bu hata ile ilgili bir mesajınız varsa lütfen ");
                sb.Append("<a href='../iletisim.aspx' class='link'>");
                sb.Append("burayı tıklayın.");
                sb.Append("</a><br/>");

                sb.Append("<a href='../../Default.aspx' class='link'>");
                sb.Append("Ana sayfaya dönmek için tıklayın.");
                sb.Append("</a><br/>");
                sb.Append("<p>&nbsp; </p>");

                Mesaj.ErrorSis(sb.ToString());
            }
        }
        catch (Exception ex)
        {
            LoggerLibrary.LogManager.SqlDB.Write("Hata Sayfası", ex);
        }
    }
}


//                string adres = Request.QueryString["errorurl"];
//                adres = adres.Replace("|", "&");

//                StringBuilder sb = new StringBuilder();
//                sb.Append("İşleminiz hata ile sonuçlandı.<br/>");
//                sb.Append("<a href='Iletisim.aspx' class='link'>");
//                sb.Append("Hatanın devam etmesi halinde bizimle iletişime geçiniz.");
//                sb.Append("</a><br/>");
//                sb.Append("Lütfen Alttaki linkten devam ediniz.</br>");

//                sb.Append("<a href='../Default.aspx' class='link'>");
//                sb.Append("www.lensoptik.com.tr");
//                sb.Append("</a><br/>");

//                sb.Append("<a class='link' href='" + adres + "'>" + adres + "</a>");
//                sb.Append("<br/> <br/>");