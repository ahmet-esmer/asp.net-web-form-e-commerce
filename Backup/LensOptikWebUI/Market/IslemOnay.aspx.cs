using System;
using System.Web;
using System.Web.UI;
using BusinessLayer;
using BusinessLayer.Security;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using SecureCookie;
using ServiceLayer.ExtensionMethods;
using ServiceLayer.Messages.Sepet;

public partial class Market_IslemOnay : System.Web.UI.Page
{
    private int uyeId = 0;
    private string uyeAdi = null;
    private int adresId = 0;
    private int faturaId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoginKontrol();

        //SslHelper sslHelper = new SslHelper();
        //sslHelper.EnsureHTTPS();

        if (!IsPostBack)
        {
            if (Session["siparisMesaj"] != null)
            {
                txtSiparisNot.Text = Session["siparisMesaj"].ToString(); 
            }

            if (Session["adresId"] != null && Session["faturaId"] != null)
            {
                adresId =  Convert.ToInt32(Session["adresId"]);
                faturaId = Convert.ToInt32(Session["faturaId"]);

                if (adresId == 0)
                    Response.Redirect("TeslimatBilgisi.aspx");
                else if (faturaId == 0)
                    Response.Redirect("TeslimatBilgisi.aspx");

                SeciliAdres(adresId);
                SeciliFatura(faturaId);
                SepetListeleme(uyeId);
            }
            else
            {
                Response.Redirect("TeslimatBilgisi.aspx");
            }
        }
        Session["onayMenu"] = "True";
    }

    private void LoginKontrol()
    {
        if (KullaniciOperasyon.LoginKontrol())
        {
            uyeId = KullaniciOperasyon.GetId();
            uyeAdi = KullaniciOperasyon.GetName();
            lblAlici.Text = uyeAdi;
            lblSozlesmeTarih.Text = DateTime.Now.ToShortDateString();
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    #region Adres getir
    private void SeciliAdres(int id)
    {
        try
        {
            KullaniciAdres adres = KullaniciAdresDB.Getir(id);

            lblTesAlan.Text = adres.TeslimAlan;
            lblTesAdres.Text = adres.Adres;
            lblTelefon.Text = adres.Telefon;
            lblSehir.Text = KullaniciAdresDB.KullaniciSehirIslem(adres.SehirId, adres.Sehir);

            lblSozAdres.Text = adres.Adres;
            lblSozAd.Text = adres.TeslimAlan;
            lblSozTel.Text = adres.Telefon;
        }
        catch (Exception ex)
        {
            Mesaj.Alert("Adres Listeleme");
            LogManager.SqlDB.Write("Onay adres listeleme", ex);
        }
    }
    #endregion

    #region Fatura getir
    private void SeciliFatura(int id)
    {
        try
        {
            KullaniciFatura fatura = KullaniciFaturaDB.Getir(id);

            if (fatura.FaturaCinsi)
            {
                lblFatAd.Text =  "<b>Ad Soya:</b>"  + fatura.AdSoyad ;
                lblFatTC.Text = "<b>T.C No:</b> " + fatura.TCNo +"<br/>";
            }
            else
            {
                lblFatAd.Text = "<b>Fatura Ünvanı: </b> " + fatura.Unvan;
                lblFatVergiDai.Text = "<b>Vergi Dairesi: </b>"+ fatura.VergiDairesi+"<br />";
                lblFatVergiNo.Text = "<b>Vergi No: </b>" + fatura.VergiNo + "<br/>";
            }

            lblFatAdres.Text =  fatura.FaturaAdresi;
        }
        catch (Exception ex)
        {
            Mesaj.Alert("Fatura listeleme hatası");
            LogManager.Mail.Write("Fatura listeleme hatası", ex);
        }
    }
    #endregion

    #region SepetListele İşlemi
    private void SepetListeleme(int uyeId)
    {
        try
        {
            SepetResponse response = SepetDB.GetListFor(uyeId).ConvertToSepetResponse();

            if (response.Indirim > 0)
            {
                trIndirim.Visible = true;
                lblIndirim.Text = "- " + response.Indirim.ToString("c");
            }

            lblBirimToplam.Text = response.BirimFiyatToplam.ToString("c");
            lblKdvToplam.Text = response.KDVToplam.ToString("c");
            lblToplam.Text = response.FiyatToplam.ToString("c");
            lblSozToplamFiyat.Text = response.FiyatToplam.ToString("c");
            lblKargoToplam.Text = response.KargoFiyat.ToString("c");
            lblSozKargo.Text = response.KargoFiyat.ToString("c");

            gvwIslemOnay.DataSource = response.SepetGride;
            gvwIslemOnay.DataBind();
            gvwSozlesme.DataSource = response.SepetGride;
            gvwSozlesme.DataBind();

            HttpCookie siparisBilgisi = new HttpCookie("lensOptik");
            siparisBilgisi.Values.Add("siparisNo", SiparisDB.SiparisNo(uyeId));
            siparisBilgisi.Values.Add("adresId", adresId.ToString());
            siparisBilgisi.Values.Add("faturaId", faturaId.ToString());
            siparisBilgisi.Values.Add("BirimFiyatToplam", response.BirimFiyatToplam.ToString());
            siparisBilgisi.Values.Add("kdvToplam", response.KDVToplam.ToString());
            siparisBilgisi.Values.Add("kargoFiyat", response.KargoFiyat.ToString());
            siparisBilgisi.Values.Add("kargoId","0");// Kullanıcı Kargo seçme özelligi eklenme olasılığı için
            siparisBilgisi.Values.Add("FiyatToplam", response.FiyatToplam.ToString());
            siparisBilgisi.Values.Add("indirim", response.Indirim.ToString());

            siparisBilgisi.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Add(HttpSecureCookie.Encode(siparisBilgisi));
        }
        catch (Exception ex)
        {
            Mesaj.Alert("Sepet Listeleme hatası.");
            LogManager.SqlDB.Write("Sipariş Onay Sepet Listeleme  hatası", ex);
        }
    }
    #endregion

    protected void btnOdeme_Click(object sender, ImageClickEventArgs e)
    {
        if(ckbSozlesmeOnay.Checked)
        {
            if (!string.IsNullOrWhiteSpace(txtSiparisNot.Text))
            {
              Session["siparisMesaj"] = txtSiparisNot.Text;  
            }
            
            Response.Redirect("~/Market/Odeme.aspx");
        }
        else
        {
            Mesaj.Alert("Lütfen Satış Sözleşmesini Okuyup Onaylayın.");
            return;
        }
    }
}