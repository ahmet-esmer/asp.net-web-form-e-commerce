using System;
using System.Text;
using System.Web;
using DataAccessLayer;
using LoggerLibrary;
using MailLibrary;
using ModelLayer;
using BusinessLayer;

public partial class Include_Ziyaretci_Mesaj_Kaydet : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethod]
    public static string Kayit(string adiSoyad, string ePosta, string sehirler, string ilceler, string mesaj, string guvenlik)
    {
        string retVal;

        try
        {
            
            if (HttpContext.Current.Session["randomStr"] == null)
            {
                return retVal = "Lütfen güvenlik kodunu doldurup yeniden deneyiniz...";
            }

            if (guvenlik == HttpContext.Current.Session["randomStr"].ToString())
            {
                StringBuilder denetim = new StringBuilder();
                if (string.IsNullOrEmpty(adiSoyad))
                    denetim.Append("* Lütfen Adınızı Yazınız.<br/>");
                if (string.IsNullOrEmpty(ePosta))
                    denetim.Append("* Lütfen E-Posta Adresi Alanını doldurunuz..<br/>");
                if (string.IsNullOrEmpty(sehirler))
                    denetim.Append("* Lütfen Şehir Seçiniz.<br/>");
                if (string.IsNullOrEmpty(ilceler))
                    denetim.Append("* Lütfen İlçe Seçiniz.<br/>");
                if (string.IsNullOrEmpty(mesaj))
                    denetim.Append("* Lütfen Yorum Alanını doldurunuz..<br/>");
                if (denetim.Length > 10)
                {
                    return retVal = denetim.ToString();
                }

                if (GenelFonksiyonlar.GecerliMailAdresi(ePosta))
                {
                    ZiyaretciDefteri defter = new ZiyaretciDefteri
                    {
                        adSoyad = adiSoyad,
                        ePosta = ePosta,
                        ilceAd = ilceler,
                        sehirAd = sehirler,
                        yorum = mesaj
                    };

                    int retValu = ZiyaretciDefteriDB.YorumKaydet(defter);

                    if (retValu == 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<b>Adı Soyadı : </b>" + adiSoyad + "<br>");
                        sb.Append("<b>E-Posta : </b>" + ePosta + "<br>");
                        sb.Append("<b>Konu</b><br>" + mesaj + "<br> <br> <br>");
                        sb.Append("<a href='http://www.lensoptik.com/Admin/Default.aspx'> Admin Panel Girişi</a>");

                        MailManager.Admin.Send("siparis@lensoptik.com.tr", "Ziyaretçi Defteri Formu",
                          sb.ToString(), ProcessType.Async);

                        retVal = Success("İletiniz yöneticiye gönderilmiştir en kısa sürede yayınlanacaktır");
                    }
                    else
                    {
                        retVal =  Alert("Bu içerige sahip bir yazı daha önce yayınlandı.");
                    }
                }
                else
                {
                    retVal =  Alert("Lütfen Geçerli E-Posta adresi yazınız.");
                }
            }
            else
            {
                retVal = Alert("Lütfen Güvenlik Kodunu Giriniz...");
            }
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Ziyaretçi defteri Ekleme", hata);
            retVal = "İşlem Hatası: Lütfen daha sonra tekrar deneyiniz.";
        }

        return retVal; 
    }

    private static string Alert(string mesaj)
    {
       return "<div id='Mesaj_No' class='Mesaj_No'> <div class='mesaj_clos'>&nbsp;</div>" + mesaj + "</div>" ;
    }

    private static string Success(string mesaj)
    {
        return "<div id='Mesaj_Ok' class='Mesaj_Ok'> <div class='mesaj_clos'>&nbsp;</div>" + mesaj + "</div>";
    }
}