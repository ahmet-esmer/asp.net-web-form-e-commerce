using System;
using System.Data;
using System.Text;
using DataAccessLayer;
using LoggerLibrary;
using MailLibrary;
using ModelLayer;
using BusinessLayer.Cashing;

public partial class Icerik_Iletisim : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IletisimBilgisi();
        }
    }

    private void IletisimBilgisi()
    {
        try
        {

            if (CacheStorage.Exists(CacheKeys.Contact))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.Contact))
                    {
                        CacheStorage.Store(CacheKeys.Contact, IletisimDB.Get());
                    }
                }
            }

            Iletisim iletisim = Cache[CacheKeys.Contact] as Iletisim;

            lblEposta.Text = iletisim.Eposta;
            lblAdres.Text = iletisim.Adres;
            lblFirma.Text = iletisim.Firma;
            lblTelefon.Text = iletisim.Telefon;


            if (!string.IsNullOrEmpty(iletisim.Faks))
            {
                rvFaks.Visible = true;
                lblFaks.Text = iletisim.Faks;
            }

            if (!string.IsNullOrEmpty(iletisim.Yetkili))
            {
                rvYetkili.Visible = true;
                lblYetkili.Text = iletisim.Yetkili;
            }

            if (!string.IsNullOrEmpty(iletisim.VergiNo))
            {
                rvVergiNo.Visible = true;
                lblVergiNo.Text = iletisim.VergiNo;
            }

            if (!string.IsNullOrEmpty(iletisim.VergiDairesi))
            {
                rvVergiDa.Visible = true;
                lblVergiDaire.Text = iletisim.VergiDairesi;
            }
        }
        catch (Exception hata)
        {
            Mesaj.ErrorSis("İletişim Bilgileri Listeleme Hatası");
            LogManager.SqlDB.Write("İletişim Bilgileri Listeleme Hatası", hata);
        }
    }

    protected void btnMesajGonder_Click(object sender, EventArgs e)
    {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<b>Gönderen:</b>" + txtAdSoyad.Text.ToString() + " </br>");
                sb.Append("<b>E- Posta:</b>" + txtEposta.Text.ToString() + "</br>");
                sb.Append("<b>İrtibat Telefonu: </b>" + txtTelefon.Text.ToString() + "</br>");
                sb.Append("<b>Konu: </b>" + txtKonu.Text.ToString() + "</br>");
                sb.Append("<b>Mesaj: </b>" + txtMesaj.Text.ToString() + "</br>");


                if (Request.QueryString["arama"] != null)
                {
                    MailManager.Admin.Send("destek@lensoptik.com.tr", "Biz Sizi Arayalım Formu", sb.ToString());
                }
                else
                {
                    MailManager.Admin.Send("İletişim Formu", sb.ToString());
                }

                Mesaj.Successful("İletiniz Başarı İle Gönderildi.");

                txtAdSoyad.Text = "";
                txtEposta.Text = "";
                txtKonu.Text = "";
                txtMesaj.Text = "";
                txtTelefon.Text = "";
            }
            catch (Exception hata)
            {
                Mesaj.ErrorSis("İletişim Formu Gönderimde Hata Oluştu..");
                LogManager.Mail.Write("İletişim Formu Gönderim Hatası..", hata);
            }
    }
}