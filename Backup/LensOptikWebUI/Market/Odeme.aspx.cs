using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessLayer.Security;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using SecureCookie;
using ServiceLayer.ExtensionMethods;
using ServiceLayer.Payment;

public partial class Market_Odeme : Page
{
    private int uyeId = 0;
    private decimal uyePuan = 0;

    private BankRequest bankRequest;
    private HttpCookie siparisBilgi;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoginKontrol();
        Session["odemeMenu"] = "True";
        ucMenuOdeme.ActifMenu(MenuMap.kart);

        

        //SslHelper sslHelper = new SslHelper();
        //sslHelper.EnsureHTTPS();

        if (!IsPostBack)
        {
            UyePuan(uyeId);
            yil();
            BankalariListele();
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
            Response.Redirect("~/Market/Sepet.aspx");
    }

    #region Kredi Kart Yıl Tarihi
    protected void yil()
    {
        ddlYil.ClearSelection();
        ListItem item = new ListItem();
        item.Text = "Yıl";
        item.Value = "0";
        item.Selected = true;
        ddlYil.Items.Add(item);

        int yil = Convert.ToInt32(DateTime.Now.Year);

        int yilSon = yil + 10;

        for (int i = yilSon; i >= yil; i--)
        {
            ddlYil.Items.Insert(1, new ListItem(i.ToString(), i.ToString()));
        }
    }
    #endregion

    #region DropDouwn Banka  Listeleme
    private void BankalariListele()
    {
        try
        {
            ddlBankalar.DataSource = BankaDB.ListeWeb();
            ddlBankalar.DataTextField = "BankaAdi";
            ddlBankalar.DataValueField = "Id";
            ddlBankalar.DataBind();

            ListItem item = new ListItem();
            item.Text = "-- Lütfen Bankanızı Seçiniz. --";
            item.Value = "0";
            item.Selected = true;
            ddlBankalar.Items.Add(item);
        }
        catch (Exception ex)
        {
            Mesaj.Alert("Banka Listeleme hatası</br> Lütfen tekrar deneyiniz." );
            LogManager.SqlDB.Write("Ödeme Banka Listeleme hatası", ex);
        }
    }
    #endregion

    #region Ödeme İşlemi
    protected void btnOdemeYap_Click(object sender, EventArgs e)
    {
        try
        {
            #region KULANICI GİRİŞ DOĞRULAMA
            StringBuilder denetim = new StringBuilder();
            if (ddlBankalar.SelectedValue.ToString() == "0")
                denetim.Append("* Lütfen Kredi Kartınızın Ait Olduğu Bankayı Seçiniz.<br/>");
            if (string.IsNullOrWhiteSpace(txtKartAdSoyad.Text))
                denetim.Append("* Lütfen Kredi Kart Üstündeki İsmi Giriniz.<br/>");
            if (string.IsNullOrWhiteSpace(txtKartNo1.Text))
                denetim.Append("* Lütfen Kredi Kart Numarası Giriniz.<br/>");
            if (string.IsNullOrWhiteSpace(txtKartNo2.Text))
                denetim.Append("* Lütfen Kredi Kart Numarası Giriniz.<br/>");
            if (string.IsNullOrWhiteSpace(txtKartNo3.Text))
                denetim.Append("* Lütfen Kredi Kart Numarası Giriniz.<br/>");
            if (string.IsNullOrWhiteSpace(txtKartNo4.Text))
                denetim.Append("* Lütfen Kredi Kart Numarası Giriniz.<br/>");
            if (ddlAy.SelectedValue.ToString() == "0")
                denetim.Append("* Lütfen Kredi Kartı Son Kullanma Tarihi Ay Olarak Seçiniz.<br/>");
            if (ddlYil.SelectedValue.ToString() == "0")
                denetim.Append("* Lütfen Yıl Seçiniz.<br/>");
            if (string.IsNullOrWhiteSpace(txtGuvenlikKodu.Text))
                denetim.Append("* Lütfen Kredi Kartı Güvenlik Kodunu Giriniz.<br/>");
            if (Request.Form["taksit"] == null )
                denetim.Append("* Lütfen Taksit Miktarı Seçiniz.<br/>");


            if (denetim.Length > 10)
            {
                Mesaj.Alert(denetim.ToString());
                return;
            }
            #endregion

            bankRequest = new BankRequest();
      
            bankRequest.KrediKart.AdSoyad = GenelFonksiyonlar.ToTitleCase(txtKartAdSoyad.Text);
            bankRequest.KrediKart.No = txtKartNo1.Text + txtKartNo2.Text + txtKartNo3.Text + txtKartNo4.Text;
            bankRequest.KrediKart.CV2 = txtGuvenlikKodu.Text;
            bankRequest.KrediKart.Ay = ddlAy.SelectedValue;
            bankRequest.KrediKart.Yil = ddlYil.SelectedValue;
            bankRequest.KrediKart.UserId = uyeId;

            bankRequest.Taksit = Convert.ToInt32(Request.Form["taksit"]);
            bankRequest.BankaId = Convert.ToInt32(ddlBankalar.SelectedValue);
            
            siparisBilgi = Request.Cookies["lensOptik"];
            siparisBilgi = HttpSecureCookie.Decode(siparisBilgi);

            bankRequest.TaksitToplam  = Convert.ToDecimal(siparisBilgi["FiyatToplam"]);

            bankRequest.SiparisNo = siparisBilgi["siparisNo"].ToString();

            if (ckbUyePuan.Checked)
            {
                UyePuan(uyeId);
                bankRequest.TaksitToplam -= uyePuan;
            }

            //Aylık taksit miktarı ve Toplam fiyat hesaplama 
            bankRequest.FiyatTaksitUygula();

          

            PaymentManager paymentManeger = new PaymentManager();
            bankRequest.PaymentMessage = paymentManeger.Process(bankRequest);

            if (bankRequest.PaymentMessage.Success)
            {
                SiparisKayit();
                Response.Redirect("~/Market/SiparisOzeti.aspx?siparisId=" + bankRequest.SiparisId.ToString() + "&islem=Kart", false);
            }
            else
            {
                MesajScript();
                Mesaj.Alert("Banka işlem dönüşü: " + bankRequest.PaymentMessage.RedMesaj);
                BankaDB.BankaGeriBildirimKaydet(bankRequest);
            }
        }
        catch (Exception hata)
        {
            MesajScript();
            Mesaj.Alert("Ödeme işlemi hata ile sonuçlandı.");
            LogManager.SqlDB.Write("Kredi kartı ile ödeme yapılırken hata oluştu", hata);
        }
    }
 #endregion 

    #region Kredi kartı ile sipariş Kayıt
    private void SiparisKayit()
    {
        try
        {
            SiparisKayit siparis = new SiparisKayit
            {
                UyeId = uyeId,
                SiparisTuru = SiparisTuru.KrediKart,
                SiparisNo = siparisBilgi["siparisNo"],
                BankaAdi = ddlBankalar.SelectedItem.ToString(),
                BirimToplam = Convert.ToDecimal(siparisBilgi["BirimFiyatToplam"]),
                KdvTutar = Convert.ToDecimal(siparisBilgi["kdvToplam"]),
                GenelToplam = Convert.ToDecimal(siparisBilgi["FiyatToplam"]),
                KargoId = Convert.ToInt16(siparisBilgi["kargoId"]),
                KargoFiyat = Convert.ToDecimal(siparisBilgi["kargoFiyat"]),
                TaksitMiktari = bankRequest.Taksit,
                AylikTaksitTutari = bankRequest.TaksitFiyat,
                TaksitliGenelToplam = bankRequest.TaksitToplam,
                AdresId = Convert.ToInt32(siparisBilgi["adresId"]),
                FaturaId = Convert.ToInt32(siparisBilgi["faturaId"]),
                KullanilanPara = uyePuan,
                SiparisDurum = SiparisDurum.SiparisHazirlaniyor,
                Indirim = Convert.ToDecimal(siparisBilgi["indirim"]),
                Mesaj = Session["siparisMesaj"] != null ? Session["siparisMesaj"].ToString() : ""
            };

            bankRequest.SiparisId = SiparisDB.Kayit(siparis);
            KrediKartDB.Kaydet(bankRequest);
        }
        catch (Exception ex)
        {
            Mesaj.Alert("Kredi Kartı ile Ödeme Başarı ile Gelçekleşti <br/>"+
                        " Sipariş Kayıt edilirken hata oluştu müşteri "+
                        "temsilcimiz ile iletişime geçiniz.");
            LogManager.SqlDB.Write("Kredi kart ödeme yapıldı sipariş kaydı gelçekleşmedi hatası.", ex);
        }
    }
    #endregion

    private void UyePuan(int uyeId)
    {
        try
        {
            uyePuan = KullaniciPuanDB.KullaniciPuan(uyeId);
            if (uyePuan != 0)
            {
                lblKulaniciPuan.Text = uyePuan.ToString("C");
                pnlPuan.Visible = true;
            }
        }
        catch (Exception hata)
        {
            LogManager.Mail.Write("Puan hesaplama işlemi.", hata);
        }
    }

    private void MesajScript()
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page),
            "taksitSec", "BankaTaksitListe(" + bankRequest.Taksit + ")", true);
    }

}