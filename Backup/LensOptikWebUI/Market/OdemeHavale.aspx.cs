using System;
using System.Web;
using BusinessLayer;
using BusinessLayer.Security;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using SecureCookie;
using ServiceLayer.ExtensionMethods;
using ServiceLayer.Messages.Sepet;

public partial class Market_Odeme_Havale : System.Web.UI.Page
{
    private int uyeId = 0;
    private decimal FiyatToplam = 0;
    private decimal uyePuan = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoginKontrol();
        ucMenuOdeme.ActifMenu(MenuMap.havale);
        Menu.ActifMenu(MenuMap.Odeme);

        //SslHelper sslHelper = new SslHelper();
        //sslHelper.EnsureHTTPS();

        if (!IsPostBack)
        {
            HavaleSepetListele(uyeId);
            HavaleBankaHesapListele();
            UyePuan(uyeId);
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

    #region DropDouwn Havale İçin Banka   Listeleme
    private void HavaleBankaHesapListele()
    {
        rptbankaHesaplari.DataSource = BankaHesaplariDB.HesapListe("web");
        rptbankaHesaplari.DataBind();
    }
    #endregion

    #region Havale için Sepet Listele İşlemi
    private void HavaleSepetListele(int uyeId)
    {
        try
        {
            string havaleMesaj = "";
            SepetResponse response = SepetDB.GetListFor(uyeId).ConvertToSepetResponseHavale();

            gvwSepetHavale.DataSource = response.SepetGride; ;
            gvwSepetHavale.DataBind();

           
            if (response.Indirim > 0)
            {
                trIndirim.Visible = true;
                lblIndirim.Text = "- " + response.Indirim.ToString("c");
            }

            lblBirimToplam.Text = response.BirimFiyatToplam.ToString("c");
            lblKargoToplam.Text = response.KargoFiyat.ToString("c");
            lblKdvToplam.Text = response.KDVToplam.ToString("c");
            lblToplam.Text = response.FiyatToplam.ToString("c");

            Session["havaleIndirim"] = response.HavaleIndirim.ToString();
            Session["kdvToplam"] = response.KDVToplam.ToString();
            Session["FiyatToplam"] = response.FiyatToplam.ToString();


            if (response.HavaleIndirim > 0)
            {
                trHavale.Visible = true;
                lblHavaleIndirim.Text = " - " + response.HavaleIndirim.ToString("c");

                havaleMesaj = "* Havale / EFT ile yapılan ödemelerde, ödeme tutarı üzerinden" +
                " <span style=' font-weight:bold;'>" + response.HavaleIndirim.ToString("c") +
                "</span> indirim yapılır. <br/>";
            }

            Mesaj.Info( havaleMesaj +
                " * 7 gün içerisinde ödemesi gerçekleşmeyen havale/eft siparişleriniz," +
                " tarafımızdan iptal edilecektir.<br/>" +
                " * Havale / EFT ile yaptığınız alışverişlerde, teslimat süresinin başlangıcı, siparişi" +
                " verdiginiz zaman değil, sipariş tutarının havale ile hesaplarımıza geçtiği andır."); 
       
        }
        catch (Exception ex)
        {
            Mesaj.Alert("Sepet Listeleme hatası.." + ex.ToString());
            LogManager.Mail.Write("Ödeme sayfası havale işlemi  sepet listeleme  hatası", ex);
        }
    }
    #endregion

    #region Banka Havalesi ile  ödeme Kayıt İşlemi
    protected void btnHavale_Click(object sender, EventArgs e)
    {
        try
        {
            string havaleBanka =  hdfHesap.Value.ToString();

            if (havaleBanka == "0")
            {
                Mesaj.Alert("Lütfen havale yapacağınız bankayı seçiniz.");
                return;
            }

            if (Session["FiyatToplam"] == null && 
                Session["havaleIndirim"] == null &&
                Session["kdvToplam"] == null)
            {
                HavaleSepetListele(uyeId);
            }

            if (ckbUyePuan.Checked)
            {
                UyePuan(uyeId);
                FiyatToplam = Convert.ToDecimal(Session["FiyatToplam"]);
                FiyatToplam = FiyatToplam - uyePuan;
                Session["FiyatToplam"] = FiyatToplam.ToString();
            }

            HttpCookie siparisBilgi = Request.Cookies["lensOptik"];
            siparisBilgi = HttpSecureCookie.Decode(siparisBilgi);

            SiparisKayit siparis = new SiparisKayit
            {
                UyeId = uyeId,
                SiparisNo = siparisBilgi["siparisNo"],
                SiparisTuru = SiparisTuru.Havele,
                BankaAdi = havaleBanka,
                BirimToplam = Convert.ToDecimal(siparisBilgi["BirimFiyatToplam"]),
                KdvTutar = Convert.ToDecimal(Session["kdvToplam"]),
                GenelToplam = Convert.ToDecimal(siparisBilgi["FiyatToplam"]),
                KargoId = Convert.ToInt16(siparisBilgi["kargoId"]),
                KargoFiyat = Convert.ToDecimal(siparisBilgi["kargoFiyat"]),
                TaksitMiktari = 0,
                AylikTaksitTutari = Convert.ToDecimal(Session["havaleIndirim"]),
                TaksitliGenelToplam = Convert.ToDecimal(Session["FiyatToplam"]),
                AdresId = Convert.ToInt32(siparisBilgi["adresId"]),
                FaturaId = Convert.ToInt32(siparisBilgi["faturaId"]),
                KullanilanPara = uyePuan,
                SiparisDurum = SiparisDurum.OnayBekliyor,
                Indirim = Convert.ToDecimal(siparisBilgi["indirim"]),
                Mesaj = Session["siparisMesaj"] != null ? Session["siparisMesaj"].ToString() : ""
            };

            int siparisId = SiparisDB.Kayit(siparis);

            Response.Redirect("~/Market/SiparisOzeti.aspx?siparisId=" + siparisId.ToString() + "&islem=Havale", false);
        }
        catch (Exception hata)
        {
            Mesaj.Alert("Havale İle Satış İşlemi Hatası");
            LogManager.Mail.Write("Havale İle Satış İşlemi Hatası", hata);
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
                pnlPuan.Visible = true;
                lblKulaniciPuan.Text = uyePuan.ToString("C");
            }
        }
        catch (Exception hata)
        {
            LogManager.Mail.Write("Puan hesaplama işlemi.", hata);
        }
    }
}