using System;
using System.Data.SqlClient;
using System.Web.UI;
using BusinessLayer;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using BusinessLayer.PagingLink;
using System.Collections.Generic;

public partial class kampanya_urunler : Page
{
    private int kampanyaId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (RouteData.Values["kampanyaId"] != null)
        {
            kampanyaId = Convert.ToInt32(RouteData.Values["kampanyaId"]);
            UrunListele();
            PageMetaTag();
        }
    }

    #region Ürün listele işlemi
    private void UrunListele()
    {
        try
        {
            List<Urun> urunler = UrunDB.HediyeliUrunler(kampanyaId);

            if (urunler.Count > 0)
            {
                lbllistelemeBilgisi.Text = String.Format("<b>{0}</b> adet kampanyalı ürün bulundu.", urunler.Count);
            }
            else
            {
                lbllistelemeBilgisi.Text = "Kampanya süresi dolmuştur.";
            }

            ucProductList.ProductsToDisplay = urunler;  
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Ürün Listeleme Hatası", ex);
        }
    }
    #endregion

    #region Sayfa meta tağ getirme işlemi
    public void PageMetaTag()
    {
        try
        {
            UrunHediyeBaslik title = HediyeUrunDB.GetTitle(kampanyaId);
            Page.Title = title.Title;
            Page.MetaKeywords = title.Title;
            hlKampanya.Text = title.Title;
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Kategori Metatag:", hata);
        }
    }
    #endregion
}