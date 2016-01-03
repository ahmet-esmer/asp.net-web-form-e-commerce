using System;
using System.Text;
using BusinessLayer;
using DataAccessLayer;
using LoggerLibrary;
using MailLibrary;
using ModelLayer;

public partial class Market_Siparis_Ozeti : System.Web.UI.Page
{
    private int siparisId = 0;
    private int uyeId = 0;

    SiparisResponse siparis;
    StringBuilder sbHesap = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoginKontrol();

            if (Request.Params["siparisId"] != null)
            {
                siparisId = Convert.ToInt32(Request.Params["siparisId"]);
                SiparisSonuIslemler();
                SiparisDetay(siparisId);
            }
        }
    }

    private void LoginKontrol()
    {
        if (KullaniciOperasyon.LoginKontrol())
        {
            uyeId = KullaniciOperasyon.GetId();
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }

        if (Request.Cookies["lensOptik"] == null)
            Response.Redirect("~/Default.aspx");
    }

    private void SiparisSonuIslemler()
    {
        MenuMarket1.ActifMenu(MenuMap.SiparisOzeti);

        if (siparisId == 0)
        {
            Mesaj.Alert("* Sipariş İşleminiz Hata ile Sonuçlandı."+
                        "* Sepetteki ürünleri silmeyiniz.<br/>" +
                        "* ödeme tekrarı yapmayınız.<br/>" +
                        "* lütfen müşteri temsilcimiz ile iletişime geçiniz.<br/>");
            return;
        }
        else if (Request.Params["islem"] == "Havale")
        {
            Mesaj.Info("* Havale ile sipariş işleminiz başarı ile gerçekleşti.<br/>"+
                       "* 7 gün içerisinde ödemesi gerçekleşmeyen havale/eft"+
                       "  siparişleriniz, tarafımızdan iptal edilecektir.<br/>"+
                       "* Havale İşlemi yapacağınız Banka Bilgileri e-posta adresinize gönderilmiştir.");
        }
        else
        {
            Mesaj.Successful("Sipariş İşleminiz Başarı ile Gerçekleşmiştir.");
        }


        if (Request.Cookies["lensOptik"] != null)
        {
            Response.Cookies["lensOptik"].Expires = DateTime.Now.AddDays(-1);
            Session["adresMenu"] = null;
            Session["onayMenu"] = null;
            Session["odemeMenu"] = null;
            Session["siparisMesaj"] = null;  
        }
    }

    #region Sipariş Detay
    private void SiparisDetay(int siparisId)
    {
        try
        {
            siparis = SiparisDB.Detay(siparisId, uyeId);

            if (siparis.OdemeTipi == "Havale")
            {
                havaleBilgi.Visible = true;
                HesapBilgiGetir(siparis.BankaAdi);
            }

            lblTarih.Text = siparis.Tarih;
            lblToplam.Text = siparis.FiyatToplam;
            lblTeslimAlan.Text = siparis.TeslimAlan;
            lblTeslimatAdresi.Text = siparis.Adres;  
            lblSiparisBaslik.Text = siparis.OdemeTipi;
            lblsiparisId.Text = siparis.SiparisNo;
            lblUyeAdi.Text = siparis.UyeAdi;

            SiparisEpostaGonder(siparisId);
        }
        catch (Exception hata)
        {
            LoggerLibrary.LogManager.SqlDB.Write("Sipariş Özeti Listeleme hatası..", hata);
        }
    }
    #endregion

    #region Banka Hesap Bilgisi Getirme
    private void HesapBilgiGetir(string bankaAdi)
    {
        try
        {
            BankaHesaplari hesap = BankaHesaplariDB.GetName(bankaAdi);
            lblHesapAdi.Text = hesap.HesapAdi;
            lblBankaAdi.Text = hesap.BankaAdi;
            lblSube.Text = hesap.Sube;
            lblSubeKodu.Text = hesap.SubeKod;
            lblHesapNo.Text = hesap.HesapNo;
            lblIban.Text = hesap.Iban;
            lblHesapTuru.Text = hesap.HesapTipi;

            sbHesap.Append("<table cellpadding='0' cellspacing='0' style='width:100%;border:1px solid #d9dada;background-color:#fafafa;margin:20px 0px;font-family:Verdana;'>");
            sbHesap.Append("<tr><td colspan='2' style='background-color:#f2f3f4;height:25px;padding-top:10px;padding-left:15px;font-size:13px;fontweight:700;color:#525252;border-bottom:1px solid #d9dada;' ><b>Havale yapılacak banka bilgisi</b> </td></tr>");
            sbHesap.Append("<tr ><td style='height:5px;padding-top:15px;padding-bottom:10px;width:150px;padding-left:15px;font-size:12px;color:#069; border-bottom:2px solid #fff;' >Hesap Adı </td>");
            sbHesap.Append("<td style='color:#292929; border-bottom:2px solid #fff;font-size:12px;' > " + hesap.HesapAdi + " </td></tr>");

            sbHesap.Append("<tr ><td style='height:5px;padding-top:15px;padding-bottom:10px;width:150px;padding-left:15px;font-size:12px;color:#069;border-bottom:2px solid #fff;' >Banka Adı </td>");
            sbHesap.Append("<td style='color:#292929; border-bottom:2px solid #fff;font-size:12px;' > " + hesap.BankaAdi + " </td></tr>");

            sbHesap.Append("<tr><td style='height:5px;padding-top:15px;padding-bottom:10px;width:150px;padding-left:15px;font-size:12px;color:#069;border-bottom:2px solid #fff;' > Şube </td>");
            sbHesap.Append("<td style='color:#292929; border-bottom:2px solid #fff;font-size:12px;' > " + hesap.Sube + " </td></tr>");

            sbHesap.Append("<tr><td style='height:5px;padding-top:15px;padding-bottom:10px;width:150px;padding-left:15px;font-size:12px;color:#069;border-bottom:2px solid #fff;' > Şube Kodu </td>");
            sbHesap.Append("<td style='color:#292929; border-bottom:2px solid #fff;font-size:12px;' >" + hesap.SubeKod + " </td></tr>");

            sbHesap.Append("<tr><td style='height:5px;padding-top:15px;padding-bottom:10px;width:150px;padding-left:15px;font-size:12px;color:#069;border-bottom:2px solid #fff;' > Hesap No </td>");
            sbHesap.Append("<td style='color:#292929; border-bottom:2px solid #fff;font-size:12px;' >" + hesap.HesapNo + " </td></tr>");

            sbHesap.Append("<tr><td style='height:5px;padding-top:15px;padding-bottom:10px;width:150px;padding-left:15px;font-size:12px;color:#069;border-bottom:2px solid #fff;' > Iban </td>");
            sbHesap.Append("<td style='color:#292929; border-bottom:2px solid #fff;font-size:12px;' >" + hesap.Iban + " </td></tr>");

            sbHesap.Append("<tr><td style='height:5px;padding-top:15px;padding-bottom:10px;width:150px;padding-left:15px;font-size:12px;color:#069;' > Hesap Tipi </td>");
            sbHesap.Append("<td style='color:#292929;font-size:12px;' >" + hesap.HesapTipi + " </td></tr>");
            sbHesap.Append("</table>");
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write( bankaAdi + " Havale ile Sipariş Banka Hesabı hatası.", hata);
        }
    }
    #endregion

    #region Sipariş Detay Eposta Gönder
    private void SiparisEpostaGonder(int siparisId)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table cellpadding='0' cellspacing='0'  style='width:100%;border:1px solid #d9dada;background-color:#fafafa;margin:20px 0px; font-family:Verdana;' >");
            sb.Append("<tr><td colspan='4' style='background-color:#f2f3f4;height:25px;padding-top:10px;padding-left:15px;font-size:15px;fontweight:700;color:#525252;border-bottom:1px solid #d9dada;' > <b>Sipariş No:" + siparis.SiparisNo+ "</b></td>");

            sb.Append("<td  style='background-color:#f2f3f4;height:25px;padding-top:10px;font-size:13px;fontweight:700;color:#525252;border-bottom:1px solid #d9dada;' >" + siparis.Tarih + "</td> </tr>");

            sb.Append("<tr><td colspan='5' style='height:150px;padding-top:15px;padding-left:15px;background-color:#fafafa;font-size:12px;color:#525252;line-height:20px; vertical-align:top;'  > <b>Sayın " + siparis.UyeAdi + "</b> <br/>lensoptik.com.tr internet sitesinten yapmış olduğunuz alışveriş tarafımıza ulaşmıştır, alışveriş detayı aşağıdaki gibidir.  </td> </tr>");
            sb.Append("<tr><td colspan='5' style='background-color:#f2f3f4;height:25px;padding-top:10px;padding-left:15px;font-size:13px;fontweight:700;color:#525252;border-bottom:1px solid #d9dada;border-top:1px solid #d9dada;' ><b> Sipariş Ettiğiniz Ürünlerin Bilgileri</b> </td> </tr>");

            sb.Append("<tr>");
            sb.Append("<td style='font-size:12px;color:#292929;background-color:#f2f3f4;height:35px;border-bottom:1px solid #d9dada;vertical-align:middle;padding-left:15px;' >");
            sb.Append("Ürün Resmi");
            sb.Append("</td>");
            sb.Append("<td style='font-size:12px;color:#292929;background-color:#f2f3f4;height:35px;border-bottom:1px solid #d9dada;vertical-align:middle;' >");
            sb.Append("Ürün Adı");
            sb.Append("</td>");
            sb.Append("<td style='font-size:12px;color:#292929;background-color:#f2f3f4;height:35px;border-bottom:1px solid #d9dada;vertical-align:middle;' >");
            sb.Append("KDV");
            sb.Append("</td>");
            sb.Append("<td style='font-size:12px;color:#292929;background-color:#f2f3f4;height:35px;border-bottom:1px solid #d9dada;vertical-align:middle;' >");
            sb.Append("Adet");
            sb.Append("</td>");
            sb.Append("<td style='font-size:12px;color:#292929;background-color:#f2f3f4;height:35px;border-bottom:1px solid #d9dada;vertical-align:middle;' >");
            sb.Append("Birim Toplam");
            sb.Append("</td>");
            sb.Append("</tr>");

            foreach (SiparisDetay s in SiparisDB.DetayUrunListe(siparisId))
            {
                sb.Append("<tr>");
                sb.Append("<td style='height:70px;vertical-align:middle;text-align:left;border-bottom:1px solid #d9dada;'>");
                sb.Append("<a href='http://www.lensoptik.com.tr/Mail/" + s.urunId.ToString() +"/SiparisUrun.aspx" +"' target='_blank'>");
                sb.Append("<img src='http://www.lensoptik.com.tr/Products/Small/" + s.resimAdi + "' width='70px' />");
                sb.Append("</a>");
                sb.Append("</td>");
                sb.Append("<td style='vertical-align:middle;text-align:left;border-bottom:1px solid #d9dada;font-size:12px;'>");
                sb.Append(s.urunAdi);
                sb.Append("</td>");
                sb.Append("<td  style='vertical-align:middle;text-align:left;border-bottom:1px solid #d9dada;font-size:12px;'>");
                sb.Append(" % " + s.urunKDV);
                sb.Append("</td>");
                sb.Append("<td style='vertical-align:middle;text-align:left;border-bottom:1px solid #d9dada;font-size:12px;'>");
                sb.Append(s.adet);
                sb.Append("</td>");
                sb.Append("<td style='vertical-align:middle;text-align:left;border-bottom:1px solid #d9dada;font-size:12px;'>");
                sb.Append(SiparisOperasyon.BirimToplam(s.fiyat, s.adet));
                sb.Append("</td>");
                sb.Append("</tr>");
            }

            sb.Append("<tr>");
            sb.Append("<td colspan='4' style='width:80%;text-align:right;vertical-align:middle;padding-right:20px;font-size:12px;color:#292929;background-color:#f2f3f4;height:40px;border-bottom:1px solid #fff;' > Birim Toplam: </td>");

            sb.Append("<td style='width:20%;vertical-align:middle;font-size:12px;color:#292929;background-color:#f2f3f4;height:40px;border-bottom:1px solid #fff;' > " + siparis.BirimFiyat + "</td>");
            sb.Append("</tr>");

            if (siparis.Indirim != 0 )
            {
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:right;vertical-align:middle;padding-right:20px;font-size:12px;color:#D2063F;background-color:#f2f3f4;height:40px;border-bottom:1px solid #fff;' > İndirimi </td>");
                sb.Append("<td style='vertical-align:middle;font-size:12px;color:#D2063F;background-color:#f2f3f4;height:40px;border-bottom:1px solid #fff;' > - " + siparis.Indirim.ToString("C") + "</td>");
                sb.Append("</tr>");
            }

            if (Request.Params["islem"] == "Havale")
            {
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:right;vertical-align:middle;padding-right:20px;font-size:12px;color:#D2063F;background-color:#f2f3f4;height:40px;border-bottom:1px solid #fff;' > Havale İndirimi </td>");
                sb.Append("<td style='vertical-align:middle;font-size:12px;color:#D2063F;background-color:#f2f3f4;height:40px;border-bottom:1px solid #fff;' > - " + siparis.HavaleVeKapi + "</td>");
                sb.Append("</tr>");
            }
            else if (Request.Params["islem"] == "Kapida")
            {
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:right;vertical-align:middle;padding-right:20px;font-size:12px;color:#D2063F;background-color:#f2f3f4;height:40px;border-bottom:1px solid #fff;' > Kapıda Ödeme Farkı </td>");
                sb.Append("<td style='vertical-align:middle;font-size:12px;color:#D2063F;background-color:#f2f3f4;height:40px;border-bottom:1px solid #fff;' > + " + siparis.HavaleVeKapi + "</td>");
                sb.Append("</tr>");
            }

            sb.Append("<tr>");
            sb.Append("<td colspan='4' style='text-align:right;vertical-align:middle;padding-right:20px;font-size:12px;color:#292929;background-color:#f2f3f4;height:40px;border-bottom:1px solid #fff;' > KDV Toplam: </td>");

            sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;background-color:#f2f3f4;height:40px;border-bottom:1px solid #fff;' > " + siparis.KDVToplam + "</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td colspan='4' style='text-align:right;vertical-align:middle;padding-right:20px;font-size:12px;color:#292929;background-color:#f2f3f4;height:40px;border-bottom:1px solid #fff;' > Kargo Ücreti (kvd dahil):</td>");
            sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;background-color:#f2f3f4;height:40px;border-bottom:1px solid #fff;' >" + siparis.KargoFiyat  + "</td>");
            sb.Append("</tr>");

          

            sb.Append("<tr>");
            sb.Append("<td colspan='4' style='text-align:right;vertical-align:middle;padding-right:20px;font-size:12px;color:#292929;background-color:#f2f3f4;height:40px;'> Toplam Ödeme:</td>");
            sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;background-color:#f2f3f4;height:40px;' > " + siparis.FiyatToplam + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");

            string metinBirlestir = sb.ToString() + sbHesap.ToString() + SonNot();

            MailManager.User.Send(siparis.Mail, "lensoptik.com.tr siparişiniz", metinBirlestir, ProcessType.Async);
            //MailManager.Admin.Send("siparis@lensoptik.com.tr", "Yeni Şipariş", sb.ToString());
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Sipariş Özeti", hata);
        }
    }
    #endregion

    private string SonNot()
    {
        StringBuilder son = new StringBuilder();
        son.Append("<table cellpadding='0' cellspacing='0' style='width:100%;border:1px solid #d9dada;background-color:#fafafa;margin:20px 0px; font-family:Verdana;' >");
        son.Append("<tr><td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;border-bottom:1px solid #fff;padding-left:15px;'>");
        son.Append("Online alışverişinizde bizi tercih ettiğiniz için teşekkür ederiz.");
        son.Append("</td> </tr>");
        son.Append("<tr><td style='vertical-align:middle;font-size:12px;color:#069;height:40px;padding-left:15px;text-decoration:underline;'> <a style='font-size:12px;color:#069;'  href='http://www.lensoptik.com.tr' target='_blank'> www.lensoptik.com.tr </a> </td> </tr>");
        son.Append("</table>");
        return son.ToString();
    }
}