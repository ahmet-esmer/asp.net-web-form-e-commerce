using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Icerik_Validation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["errorurl"] != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Güvenlik riski oluşturabilecek bir veri girişine rastlandı.<br/>");
                sb.Append("Lütfen formlardan ve adres çubuğundan girdiginiz verileri kontrol ediniz..<br/>");
                sb.Append("Hata raporu teknik departmanımıza iletildi. <br/>");
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


