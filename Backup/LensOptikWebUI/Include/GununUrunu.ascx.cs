using System;
using BusinessLayer;
using BusinessLayer.Cashing;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;

public partial class include_gununUrunu : System.Web.UI.UserControl
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GununUrunuGetirme();
        }
    }

    private void GununUrunuGetirme()
    {
        try
        {
            if (CacheStorage.Exists(CacheKeys.ProductOfDay))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.ProductOfDay))
                    {
                        CacheStorage.Store(CacheKeys.ProductOfDay, GunuUrunuDB.Get(), "tbl_gununUrunleri");
                    }
                }
            }

            GununUrunu urun = Cache[CacheKeys.ProductOfDay] as GununUrunu;

            lblYuzde.Text = urun.IndirimYuzde.ToString();
            hlUrunDetay.NavigateUrl = LinkBulding.Urun(urun.KategoriAdi, urun.Id, urun.UrunAdi);
            imgGununUrunu.ImageUrl = "~/Products/Small/" + urun.ResimAdi;
            imgGununUrunu.AlternateText = urun.UrunAdi;

            if (urun.UrunAdi != null)
                lblGunUrunAdi.Text = urun.UrunAdi.ToUpper();
           

            lblUrunFiyat1.Text = AritmetikIslemler.UrunFiyatIndirim(urun.UrunFiyat, urun.UIndirimFiyat, urun.Doviz, urun.KDV);
            lblUrunFiyat2.Text =AritmetikIslemler.UrunFiyatIndirimVarmi(urun.UrunFiyat, urun.UIndirimFiyat, urun.Doviz, urun.KDV);
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Günün Ürünü Listeleme Hatası", hata);
        }

    }

}