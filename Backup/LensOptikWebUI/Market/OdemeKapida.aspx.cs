using System;
using System.Data;
using System.Web;
using BusinessLayer;
using BusinessLayer.Security;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using SecureCookie;
using ServiceLayer.ExtensionMethods;
using ServiceLayer.Messages.Sepet;

public partial class Market_Odeme_Kapida : System.Web.UI.Page
{
    private int uyeId = 0;
    private decimal kapidaFiyatFarkki = 0;
    private decimal uyePuan = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Menu.ActifMenu(MenuMap.Odeme);
        ucMenuOdeme.ActifMenu(MenuMap.kapida);
        LoginKontrol();

        //SslHelper sslHelper = new SslHelper();
        //sslHelper.EnsureHTTPS();

        if (!IsPostBack)
        {
            KapidaOdemeFarki();
            KapidaOdemeSepetListele(uyeId);
            UyePuan(uyeId);

            Mesaj.Info("* Ödemesini; siparişinizi teslim alırken kargo"+
                       " görevlisine nakit olarak yapabilirsiniz.<br/> "+
                       "* Siparişinizi teyid etmek üzere müşteri hizmetlerimiz"+ 
                       " sizinle irtibata geçecektir.");
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

    #region Kapıda Ödeme Sepet Listele İşlemi
    private void KapidaOdemeSepetListele(int uyeId)
    {
        try
        {
            SepetResponse response = SepetDB.GetListFor(uyeId).ConvertToSepetResponse();

            gvwSepetKapida.DataSource = response.SepetGride; ;
            gvwSepetKapida.DataBind();

            if (response.Indirim > 0)
            {
                trIndirim.Visible = true;
                lblIndirim.Text = "- " + response.Indirim.ToString("c");
            }

            lblBirimToplam.Text = response.BirimFiyatToplam.ToString("c");
            lblKargoToplam.Text = response.KargoFiyat.ToString("c");
            lblKdvToplam.Text = response.KDVToplam.ToString("c");
            lblToplam.Text = response.FiyatToplam.ToString("c");
            lblKapidaFiyatFarki.Text = "+ " + kapidaFiyatFarkki.ToString("c");

            response.FiyatToplam += kapidaFiyatFarkki;
            lblToplam.Text = response.FiyatToplam.ToString("c");
            Session["FiyatToplam"] = response.FiyatToplam.ToString();
        }
        catch (Exception ex)
        {
            Mesaj.Alert("Kapıda ödeme işlemi sepet listeleme hatası.");
            LogManager.Mail.Write("Kapıda ödeme işlemi sepet listeleme hatası..", ex);
        }
    }
    #endregion

    #region Kapıda Ödemeyle ile  Kayıt İşlemi
    protected void btnKapidaOdeme_Click(object sender, EventArgs e)
    {
        try
        {
            HttpCookie siparisBilgi = Request.Cookies["lensOptik"];
            siparisBilgi = HttpSecureCookie.Decode(siparisBilgi);

            if (Session["FiyatToplam"] == null && Session["KapiFarki"] == null)
            {
                KapidaOdemeFarki();
                KapidaOdemeSepetListele(uyeId);
            }     

            if (ckbUyePuan.Checked)
            {
                decimal FiyatToplam = 0;
                UyePuan(uyeId);
                FiyatToplam = Convert.ToDecimal(Session["FiyatToplam"]);
                FiyatToplam = FiyatToplam - uyePuan ;
                Session["FiyatToplam"] = FiyatToplam.ToString();
            }

            SiparisKayit siparis = new SiparisKayit
            {
                UyeId = uyeId,
                SiparisNo = siparisBilgi["siparisNo"],
                SiparisTuru = SiparisTuru.KapidaOdeme,
                BankaAdi = " ",
                BirimToplam = Convert.ToDecimal(siparisBilgi["BirimFiyatToplam"]),
                KdvTutar = Convert.ToDecimal(siparisBilgi["kdvToplam"]),
                GenelToplam = Convert.ToDecimal(siparisBilgi["FiyatToplam"]),
                KargoId = Convert.ToInt32(siparisBilgi["kargoId"]),
                KargoFiyat = Convert.ToDecimal(siparisBilgi["kargoFiyat"]),
                TaksitMiktari = 0,
                AylikTaksitTutari = Convert.ToDecimal(Session["KapiFarki"]),
                TaksitliGenelToplam = Convert.ToDecimal(Session["FiyatToplam"]),
                AdresId = Convert.ToInt32(siparisBilgi["adresId"]),
                FaturaId = Convert.ToInt32(siparisBilgi["faturaId"]),
                KullanilanPara = uyePuan,
                SiparisDurum = SiparisDurum.OnayBekliyor,
                Indirim = Convert.ToDecimal(siparisBilgi["indirim"]),
                Mesaj = Session["siparisMesaj"] != null ? Session["siparisMesaj"].ToString() : ""
            };

            int siparisId = SiparisDB.Kayit(siparis);

            Response.Redirect("SiparisOzeti.aspx?siparisId=" + siparisId.ToString() + "&islem=Kapida", false);
        }
        catch (Exception hata)
        {
            Mesaj.Alert("Kapıda Ödemeyle İle Satış İşlemi Hatası");
            LogManager.Mail.Write("Kapıda Ödemeyle İle Satış İşlemi Hatası", hata);
        }
    }
    #endregion

    #region  Kargo Bilgisi  Listele
    private void KapidaOdemeFarki()
    {
        try
        {
            string str = "SELECT top(1)kapidaOdemeFark FROM dbo.tbl_kargolar";
            kapidaFiyatFarkki = (decimal)SqlHelper.ExecuteScalar(CommandType.Text, str);

            Session["KapiFarki"] = kapidaFiyatFarkki.ToString(); 
        }
        catch (Exception hata)
        {
            LogManager.Mail.Write("Kargo kapıda ödeme fiyat farkı hatası..", hata);
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
}